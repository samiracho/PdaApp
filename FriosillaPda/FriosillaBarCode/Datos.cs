using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Forms;
using System.Xml;
using EnterpriseDT.Net.Ftp;

namespace FriosillaBarCode
{
    /// <summary>
    /// Clase encargada de:
    /// Almacenar los códigos BIDI y los códigos de los palés en archivos XML
    /// Agregar/Editar/Eliminar datos en los archivos XML
    /// </summary>
    internal static class Datos
    {
        public static readonly string RutaAbsArchivoPales;
        public static readonly string RutaAbsArchivoBarcodes;
        private static readonly string NombreProcInsercion;
  
        static Datos()
        {
            var rutaDatos = Util.GetSetting("DataFolder");
            RutaAbsArchivoPales = Util.GetPath("Pales.xml", rutaDatos);
            RutaAbsArchivoBarcodes = Util.GetPath("BarCodes.xml", rutaDatos);
            NombreProcInsercion = Util.GetSetting("InsertProcedureName");
        }

        #region Procesar Barcodes

        /// <summary>
        /// Procesa los distintos tipos de códigos de barras
        /// </summary>
        /// <param name="cCodPal"></param>
        /// <param name="cCodBar"></param>
        public static int ProcesarBarcode(string cCodPal, string cCodBar)
        {
            cCodBar = Util.EliminarCaracteresNoNumericos(cCodBar);
            var tipoCodigo = cCodBar.Substring(0, 4);
            int resultado;
            switch (tipoCodigo)
            {
                case "9114":
                    resultado = InsertarLotesDeCajas(cCodBar, cCodPal);
                    break;
                case "9115":
                    resultado = InsertarLotesDePales(cCodBar, cCodPal);
                    break;
                default:
                    resultado = Result.ERROR_FORM_COD;
                    break;
            }
            return resultado;
        }

        private static int InsertarLotesDeCajas(string cCodBar, string cCodPal)
        {
            // Si el código ya existe salimos
            if (BarcodeExiste(cCodBar)) return Result.ERROR_COD_DUP;

            var barcodeLength = cCodBar.Length;
            if (cCodBar.Length < 52) return Result.ERROR_LONG_COD;

            // en c# los índices empiezan desde 0
            const int pos = -1;
            const int iOff = 2;
            var resultado = 0;
            var numLotes = int.Parse(cCodBar.Substring(50 + pos, 2));
            var document = Util.AbrirXmlParaEscritura(RutaAbsArchivoBarcodes);
            var cScc = cCodBar.Substring(7 + pos, 18);
            var cTmp = cCodBar.Substring(37 + pos, 6);
            var dFec = cTmp.Substring(5 + pos, 2) + "/" + cTmp.Substring(3 + pos, 2) + "/" + cTmp.Substring(1 + pos, 2);
            int cBul;
            string rPes, cLot, cUni;

            for (var i = 1; i <= numLotes; i++)
            {
                var offsetLote = (i - 1) * 33;
                if (barcodeLength < 112 + offsetLote) return Result.ERROR_LONG_COD;

                cBul = i == 1 ? 1 : 0;
                cLot = cCodBar.Substring(83 + offsetLote + iOff + pos, 12);
                rPes = cCodBar.Substring(83 + 18 + offsetLote + pos, 6);
                cUni = cCodBar.Substring(83 + 26 + offsetLote + pos, 4);

                try
                {
                    var rpesFloat = float.Parse(rPes) / 100;
                    var atributos = new Dictionary<string, string>
                    {
                        {"cCodPal", cCodPal},
                        {"cLot", cLot},
                        {"rPes", rpesFloat.ToString(CultureInfo.InvariantCulture)},
                        {"fecha", dFec},
                        {"cScc", cScc},
                        {"cUni", cUni},
                        {"cBul", cBul.ToString(CultureInfo.InvariantCulture)},
                        {"cCodBar", cCodBar},
                        {"primero", i == 1 ? "true" : "false"},
                        {"ultimo", i == numLotes ? "true" : "false"}
                    };

                    resultado = AgregarBarcodeXml(document, atributos);

                    if (resultado != Result.OK) return resultado;
                    if (i == numLotes) document.Save(RutaAbsArchivoBarcodes);
                }
                catch
                {
                    return Result.ERROR_FORM_COD;
                }
            }

            return resultado;
        }

        private static int InsertarLotesDePales(string cCodBar, string cCodPal)
        {
            // Si el código ya existe salimos
            if (BarcodeExiste(cCodBar))return Result.ERROR_COD_DUP;

            // en c# los índices empiezan desde 0
            const int pos = -1;
            var resultado = 0;
            var barcodeLength = cCodBar.Length;
            const int iOff = 0;
            var cScc = "";
            var iTotLot = int.Parse(cCodBar.Substring(50 + pos, 2));
            var iPosIni = 73;
            var document = Util.AbrirXmlParaEscritura(RutaAbsArchivoBarcodes);

            // Si el número de lotes es inválido
            if (iTotLot <= 0 || iTotLot > 20) return Result.ERROR_NUM_LOTES;

            for (var i = 1; i <= iTotLot; i++)
            {
                // si el código es demasiado corto
                if (barcodeLength < (iPosIni + 27)) return Result.ERROR_LONG_COD;

                var cBul = 0;
                if (i == 1)
                {
                    cScc = cCodBar.Substring(7 + pos, 18);
                    cBul = 1;
                }

                var cTmp = cCodBar.Substring(37 + pos, 6);
                var fecha = cTmp.Substring(5 + pos, 2) + "/" + cTmp.Substring(3 + pos, 2) + "/" +cTmp.Substring(1 + pos, 2);
                var cLot = cCodBar.Substring(iPosIni + iOff + pos, 12);
                cTmp = cCodBar.Substring(iPosIni + 16 + pos, 6);
                var cUni = cCodBar.Substring(iPosIni + 24 + pos, 4);

                try
                {
                    var rPes = float.Parse(cTmp)/100;
                    var atributos = new Dictionary<string, string>
                    {
                        {"cCodPal", cCodPal},
                        {"cLot", cLot},
                        {"rPes", rPes.ToString(CultureInfo.InvariantCulture)},
                        {"fecha", fecha},
                        {"cScc", cScc},
                        {"cUni", cUni},
                        {"cBul", cBul.ToString(CultureInfo.InvariantCulture)},
                        {"cCodBar", cCodBar},
                        {"primero", i == 1 ? "true" : "false"},
                        {"ultimo", i == iTotLot ? "true" : "false"}
                    };

                    resultado = AgregarBarcodeXml(document, atributos);

                    if (resultado != Result.OK) return resultado;

                    // Si se han leído correctamente todos los lotes los agregamos al fichero. Sino no agregamos ninguno
                    if (i == iTotLot)document.Save(RutaAbsArchivoBarcodes);

                    iPosIni += 35;
                }
                catch
                {
                    return Result.ERROR_FORM_COD;
                }
            }
            return resultado;
        }

        private static int EjecutarStoredProcedure(SqlConnection conexion, string cCodPal, string cLot, float rPes, DateTime fecha, string cScc, string cUni, int cBul, string cCodBar, bool ultimoLote, bool primerLote)
        {
            using (var myCommand = new SqlCommand { CommandText = NombreProcInsercion, Connection = conexion, CommandType = CommandType.StoredProcedure })
            {
                var transaction = conexion.BeginTransaction("TransaccionInsertar");
                myCommand.Transaction = transaction;

                var output = new SqlParameter("@RESULTADO", SqlDbType.TinyInt) { Direction = ParameterDirection.Output };
                myCommand.Parameters.AddWithValue("@COD_PAL", cCodPal);
                myCommand.Parameters.AddWithValue("@PAL3_LOT", cLot);
                myCommand.Parameters.AddWithValue("@PAL3_PES", rPes);
                myCommand.Parameters.AddWithValue("@PAL3_FCA", fecha);
                myCommand.Parameters.AddWithValue("@PAL3_SRE", cScc);
                myCommand.Parameters.AddWithValue("@PAL3_UNI", cUni);
                myCommand.Parameters.AddWithValue("@PAL3_OT2", cBul);
                myCommand.Parameters.AddWithValue("@PAL2_CB", cCodBar);
                myCommand.Parameters.AddWithValue("@ULTIMO_LOTE", ultimoLote);
                myCommand.Parameters.AddWithValue("@PRIMER_LOTE", primerLote);
                myCommand.Parameters.Add(output);
                myCommand.ExecuteNonQuery();
                var result = int.Parse(output.Value.ToString());

                if (result == Result.OK)
                {
                    transaction.Commit();
                }
                else
                {
                    transaction.Rollback();
                }

                return result;
            }
        }

        #endregion

        #region Agregar/Editar/Eliminar Barcodes


        /// <summary>
        /// Agrega un barcode al archivo XML de barcodes. Devuelve 1 Si ha ido bien y un código de error en caso contrario
        /// </summary>
        /// <param name="document">documento XML</param>
        /// <param name="atributos">Diccionario de atributos</param>
        /// <returns></returns>
        private static int AgregarBarcodeXml(XmlDocument document, Dictionary<string, string> atributos)
        {
            // Si el palé o el código están vacíos
            if ( atributos == null || string.IsNullOrEmpty(atributos["cCodPal"]) || string.IsNullOrEmpty(atributos["cCodBar"]))
            {
                return Result.ERROR_PALE_O_COD_NULL;
            }

            var nodo = document.SelectSingleNode("barcodes");
            var nuevoNodo = document.CreateNode(XmlNodeType.Element, "add", null);

            foreach (var atributo in atributos)
            {
                var atr = document.CreateAttribute(atributo.Key);
                atr.InnerText = atributo.Value;
                nuevoNodo.Attributes.Append(atr);
                nodo.AppendChild(nuevoNodo);
            }
            return Result.OK;
        }

        /// <summary>
        /// Inserta los barcodes en la base de datos leyendo del archivo XML de barcodes. 
        /// </summary>
        /// <returns></returns>
        public static void EnviarBarcodesBd()
        {
            var timeOut = Util.GetIntSetting("MaxQueriesTimeout");
            var watch = Stopwatch.StartNew();
            using (var conexionSql = Util.AbrirConexion())
            {
                if (conexionSql == null) throw new Exception("Se perdió la conexión con la BD. Compruebe la conexión WIFI y vuelva a intentarlo.");

                var document = Util.AbrirXmlParaEscritura(RutaAbsArchivoBarcodes);
                // Vamos a hacerlo en tandas. P.ejemplo, consultamos los nodos de diez en diez, los enviamos a la bd, y si ha ido bien los borramos del archivo xml.
                while (true)
                {
                    var barCodesList = document.SelectNodes("barcodes/add");
                    var barcodesCount = barCodesList.Count;
                    var tope = Util.GetIntSetting("MaxQueries") > barcodesCount ? barCodesList.Count : Util.GetIntSetting("MaxQueries");

                    // si ya no hay más barcodes hemos acabado
                    if (barcodesCount == 0) return;

                    // si se ha agotado el tiempo de ejecución
                    if (watch.ElapsedMilliseconds > timeOut * 1000)
                    {
                        throw new Exception("Agotado el tiempo de ejecución. Vuelva a intentarlo.");
                    }

                    // intentamos hacer las inserciones en la base de datos
                    for (var i = 0; i < tope; i++)
                    {
                        var cCodPal = barCodesList[i].Attributes["cCodPal"].Value;
                        var cCodBar = barCodesList[i].Attributes["cCodBar"].Value;
                        var cLot = barCodesList[i].Attributes["cLot"].Value;
                        var rPes = float.Parse(barCodesList[i].Attributes["rPes"].Value);
                        var fecha = DateTime.ParseExact(barCodesList[i].Attributes["fecha"].Value, "dd/MM/yy", null);
                        fecha = Util.ObtenerSiglo(fecha.Year) < Util.ObtenerSiglo(DateTime.Now.Year) ? fecha.AddYears(100) : fecha;
                        var cScc = barCodesList[i].Attributes["cScc"].Value;
                        var cUni = barCodesList[i].Attributes["cUni"].Value;
                        var cBul = int.Parse(barCodesList[i].Attributes["cUni"].Value);
                        var ultimo = barCodesList[i].Attributes["ultimo"].Value.Equals("true");
                        var primero = barCodesList[i].Attributes["primero"].Value.Equals("true");

                        var resultado = EjecutarStoredProcedure(conexionSql, cCodPal, cLot, rPes, fecha, cScc, cUni,cBul, cCodBar, ultimo, primero);
                        if (resultado != Result.OK) Util.EscribirLog("Proc Alm. COD " + resultado);
                        // Borramos el nodo
                        barCodesList[i].ParentNode.RemoveChild(barCodesList[i]);
                        document.Save(RutaAbsArchivoBarcodes);
                    }
                   
                }
            }
        }

        /// <summary>
        /// Envía el archivo de barcodes via ftp
        /// </summary>
        /// <param name="uploadEvent"></param>
        /// <param name="fileName"></param>
        public static void EnviarBarcodesFtp(FTPFileTransferEventHandler uploadEvent, string fileName)
        {
            var ftp = new ExFTPConnection
            {
                ServerAddress = Util.GetSetting("FtpServerIp"),
                UserName = Util.GetSetting("FtpServerUser"),
                Password = Util.GetSetting("FtpServerPassword"),
                ServerPort = Util.GetIntSetting("FtpServerPort"),
                LicenseOwner = Util.GetSetting("FtpLicenseOwner"),
                LicenseKey = Util.GetSetting("FtpLicenseKey"),
                TransferType = FTPTransferType.BINARY,
                DeleteOnFailure = true
            };

            ftp.Uploaded += uploadEvent;
            ftp.Connect();

            var i = 0;
            fileName = ( string.IsNullOrEmpty(fileName) ? "Barcodes" : fileName ) + ".xml";
            while (ftp.Exists(fileName))
            {
                i++;
                fileName = i+fileName;
            }
            ftp.UploadFile(RutaAbsArchivoBarcodes, fileName);

            ftp.Close();
        }

        /// <summary>
        /// Devuelve el número de barcodes almacenados en el archivo XML de barcodes 
        /// </summary>
        /// <returns></returns>
        public static int ContarBarcodes()
        {
            var count = 0;
            using (var xmlTextReader = Util.AbrirXmlParaLectura(RutaAbsArchivoBarcodes))
            {
                while (xmlTextReader.Read())
                {
                    var prim = xmlTextReader.GetAttribute("primero");
                    if (prim != null && prim.Equals("true")) count++;
                }
            }
            return count;
        }

        /// <summary>
        /// Elimina todos los barcodes del archivo XML de barcodes
        /// </summary>
        public static bool EliminarBarcodes()
        {
            var document = Util.AbrirXmlParaEscritura(RutaAbsArchivoBarcodes);
            var node = document.SelectSingleNode("barcodes");

            if (node != null)
            {
                node.RemoveAll();
                document.Save(RutaAbsArchivoBarcodes);
                return true;
            }
            return false;
        } 

        /// <summary>
        /// Comprobar si el barcode existe en el archivo XML de barcodes
        /// </summary>
        /// <param name="barcode">Barcode a comprobar</param>
        /// <returns></returns>
        public static bool BarcodeExiste(string barcode)
        {
            using (var xmlTextReader = Util.AbrirXmlParaLectura(RutaAbsArchivoBarcodes))
            {
                while (xmlTextReader.Read())
                {
                    if (xmlTextReader.GetAttribute("cCodBar") == barcode) return true;
                }
            }
            return false;
        }

        #endregion

        #region Agregar/Editar/Eliminar palés

        /// <summary>
        /// Obtiene una lista de palés del archivo de datos XML
        /// </summary>
        /// <returns></returns>
        public static List<string> ObtenerPales()
        { 
            var pales = new List<string>();

            using (var xmlTextReader = Util.AbrirXmlParaLectura(RutaAbsArchivoPales))
            {
                while (xmlTextReader.Read())
                {
                    if (xmlTextReader.GetAttribute("pale") != null)
                    {
                        pales.Add(xmlTextReader.GetAttribute("pale"));
                    }
                }
            }
            return pales;
        }

        /// <summary>
        /// Intenta conectar con la base de datos, descargarse la lista de palés y escribirlos en el archivo XML de palés. 
        /// Devuelve true si se ha conectado con la BD y false en caso contrario.
        /// </summary>
        public static bool CargarPalesDeBd()
        {
            var conectado = false;
            // Ponemos la barra de carga
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                RellenarArchivoPales();
                conectado = true;
            }
            catch (SqlException)
            {
                var result = MessageBox.Show("No se pudo conectar con el servidor de bases de datos. ¿Desea reintentar o cancelar y trabajar sin conexión?", "Alerta", MessageBoxButtons.RetryCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);

                if (result == DialogResult.Retry)
                {
                    CargarPalesDeBd();
                }
            }
            catch (Exception ex)
            {
                Util.EscribirLog(ex.Message);
                MessageBox.Show("Error Grave: " + ex.Message, "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                Application.Exit();
            }

            // Ocultamos la barra de carga
            Cursor.Current = Cursors.Default;

            return conectado;
        }

        public static bool AgregarPale(string pale, string pendientes, bool checkDuplicated, bool saveAfterAdd)
        {
            var document = Util.AbrirXmlParaEscritura(RutaAbsArchivoPales);
            return AgregarPale(document, pale, pendientes, checkDuplicated, saveAfterAdd);
        }

        /// <summary>
        /// Agrega un palé al fichero XML de palés
        /// </summary>
        /// <param name="document">XmlDocument del archivo XML de pales</param>
        /// <param name="pale">Código del palé a agregar</param>
        /// <param name="pendientes">Cajas pendientes</param>
        /// <param name="checkDuplicated">true para comprobar duplicados.</param>
        /// <param name="saveAfterAdd">true para guardar en el archivo inmediatamente después de agregar el palé</param>
        /// <returns></returns>
        private static bool AgregarPale( XmlDocument document, string pale, string pendientes, bool checkDuplicated, bool saveAfterAdd)
        {
            // Si no hay que comprobar duplicados lo agrego directamente
            if (!checkDuplicated)
            {
                AgregarPaleXml(document, pale, pendientes, saveAfterAdd);
                return true;
            }
            if (PaleExiste(pale)) return false;
            AgregarPaleXml(document, pale, pendientes, saveAfterAdd);
            return true;
        }

        /// <summary>
        /// Modifica un palé del archivo XML de palés
        /// </summary>
        /// <param name="pale">palé a modificar</param>
        /// <param name="paleNuevo">nuevo palé</param>
        public static bool EditarPale(string pale, string paleNuevo)
        {
            var archivoPales = Util.AbrirXmlParaEscritura(RutaAbsArchivoPales);
            var node = archivoPales.SelectSingleNode("pales/add[@pale='" + pale + "']");
            if (node == null) return false;
            node.Attributes["pale"].Value = paleNuevo;
            archivoPales.Save(RutaAbsArchivoPales);
            return true;
        }

        /// <summary>
        /// Elimina un palé del archivo XML de palés
        /// </summary>
        /// <param name="pale"></param>
        public static bool EliminarPale(string pale)
        {
            var archivoPales = Util.AbrirXmlParaEscritura(RutaAbsArchivoPales);
            var node = archivoPales.SelectSingleNode("pales/add[@pale='" + pale + "']");

            if (node == null) return false;
            node.ParentNode.RemoveChild(node);
            archivoPales.Save(RutaAbsArchivoPales);
            return true;
        }

        public static bool EliminarPales()
        {
            var document = Util.AbrirXmlParaEscritura(RutaAbsArchivoPales);
            return EliminarPales(document);
        }

        /// <summary>
        /// Elimina todos los palés del archivo XML de palés
        /// </summary>
        private static bool EliminarPales(XmlDocument document)
        {
            var node = document.SelectSingleNode("pales");
            if (node == null) return false;
            node.RemoveAll();
            document.Save(RutaAbsArchivoPales);
            return true;
        }

        /// <summary>
        /// Comprobar si el palé existe en el archivo XML de palés
        /// </summary>
        /// <param name="pale"></param>
        /// <returns></returns>
        private static bool PaleExiste(string pale)
        {
            // comprobar si existe
            using (var xmlTextReader = Util.AbrirXmlParaLectura(RutaAbsArchivoPales))
            {
                while (xmlTextReader.Read())
                {
                    if (xmlTextReader.GetAttribute("pale") == pale) return true;
                }
            }
            return false;
        }

        public static string ObtenerCajasPendientesPale(string pale)
        {
            // comprobar si existe
            using (var xmlTextReader = Util.AbrirXmlParaLectura(RutaAbsArchivoPales))
            {
                while (xmlTextReader.Read())
                {
                    if (xmlTextReader.GetAttribute("pale") == pale) return xmlTextReader.GetAttribute("pendientes");
                }
            }
            return "";
        }

        /// <summary>
        /// Rellena el archivo XML de palés desde la BD
        /// </summary>
        /// <returns></returns>
        private static void RellenarArchivoPales()
        {
            using (var connection = new SqlConnection(Util.MiCadenaConexion()))
            {
                var document = Util.AbrirXmlParaEscritura(RutaAbsArchivoPales);

                // Comprobamos si hay que borrar los palés antiguos
                var borrarAntiguos = Util.GetSetting("DeleteOldCodes").Equals("true");
                if (borrarAntiguos)
                {
                    EliminarPales(document);
                }

                connection.Open();
                var cmd = new SqlCommand(Util.GetSetting("QuerySelectPale"), connection);
                var reader = cmd.ExecuteReader();

                // Leemos y agregamos al fichero xml de palés, las id's de los palés
                while (reader.Read())
                {
                    // Si he borrado los antiguos, no quiero que compruebe si hay duplicados. Quiero que los agregue directamente al xml.
                    AgregarPale(document, reader[0].ToString(), reader[1].ToString(), !borrarAntiguos, false);
                }

                // Ahora guardamos los cambios en el archivo xml
                document.Save(RutaAbsArchivoPales);
            }
        }

        /// <summary>
        /// Agrega un palé nuevo al archivo XML de palés
        /// </summary>
        /// <param name="document">XmlDocument</param>
        /// <param name="pale">Código de palé</param>
        /// <param name="pendientes">Cajas pendientes</param>
        /// <param name="saveAfterAdd">true para guardar inmediatamente después de agregar el nodo al archivo XML</param>
        private static void AgregarPaleXml(XmlDocument document, string pale, string pendientes, bool saveAfterAdd)
        {
            if (string.IsNullOrEmpty(pale)) return;

            var nodo = document.SelectSingleNode("pales");
            var nuevoNodo = document.CreateNode(XmlNodeType.Element, "add", null);
            
            var atrPale = document.CreateAttribute("pale");
            atrPale.InnerText = pale;
            nuevoNodo.Attributes.Append(atrPale);

            var atrPen = document.CreateAttribute("pendientes");
            atrPen.InnerText = pendientes;
            nuevoNodo.Attributes.Append(atrPen);

            nodo.AppendChild(nuevoNodo);

            if (saveAfterAdd)
            {
                document.Save(RutaAbsArchivoPales);
            }
        }

        #endregion
            
    }
}
