using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Xml;

namespace FriosillaPda
{
    public partial class FriosillaPdaService : ServiceBase
    {
        private const string ErrorSuffix = "_error";

        public FriosillaPdaService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            EscribirLog("------------------------------------------------------------------");
            EscribirLog("Servicio Iniciado");
            EscribirLog("Monitorizando archivos en " + ConfigurationManager.AppSettings["WatchPath"]);
            EscribirLog("------------------------------------------------------------------");
            fileSystemWatcher1.Path = ConfigurationManager.AppSettings["WatchPath"];
        }

        protected override void OnStop()
        {
            EscribirLog("------------------------------------------------------------------");
            EscribirLog("Servicio Parado");
            EscribirLog("------------------------------------------------------------------");
            fileSystemWatcher1.Dispose();
        }

        private void fileSystemWatcher1_Created(object sender, FileSystemEventArgs e)
        {
            EscribirLog(e.FullPath);
            var backgroundThread = new Thread(ProcesarArchivo);
            backgroundThread.Start(e.FullPath);
        }

        private void ProcesarArchivo(object filePath)
        {
            var path = filePath.ToString();
            var f = new FileInfo(path);
            if (path.EndsWith(ErrorSuffix)) return;

            var timeOut = int.Parse(ConfigurationManager.AppSettings["TimeOutSeconds"]);

            while (timeOut > 0)
            {
                if (!IsFileLocked(f))
                {
                    try
                    {
                        EnviarBarcodesBd(path);
                        return;
                    }
                    catch (Exception ex)
                    {
                        EscribirLog(ex.Message);
                        File.Move(path, path + ErrorSuffix);
                        return;
                    }
                }
                
                Thread.Sleep(1000);
                timeOut--;
            }
        }

        public static void EnviarBarcodesBd(string path)
        {
            using (var conexionSql = AbrirConexion())
            {
                if (conexionSql == null) throw new Exception("Se perdió la conexión con la BD. Compruebe la conexión WIFI y vuelva a intentarlo.");

                using (var xmlTextReader = AbrirXmlParaLectura(path))
                {
                    var comprobarSiguienteCod = false;

                    while (xmlTextReader.Read())
                    {
                        if (xmlTextReader.Name != "add") continue;

                        var cCodPal = xmlTextReader.GetAttribute("cCodPal");
                        var cCodBar = xmlTextReader.GetAttribute("cCodBar");
                        var cLot = xmlTextReader.GetAttribute("cLot");
                        var rPes = Single.Parse(xmlTextReader.GetAttribute("rPes"), CultureInfo.InvariantCulture);
                        
                        var fecha = DateTime.ParseExact(xmlTextReader.GetAttribute("fecha"), "dd/MM/yy", null);
                        fecha = ObtenerSiglo(fecha.Year) < ObtenerSiglo(DateTime.Now.Year) ? fecha.AddYears(100) : fecha;
                        
                        var cScc = xmlTextReader.GetAttribute("cScc");
                        var cUni = xmlTextReader.GetAttribute("cUni");
                        var cBul = int.Parse(xmlTextReader.GetAttribute("cUni"),CultureInfo.InvariantCulture);
                        var ultimo = xmlTextReader.GetAttribute("ultimo").Equals("true");
                        var primero = xmlTextReader.GetAttribute("primero").Equals("true") || comprobarSiguienteCod;
                        var resultado = EjecutarStoredProcedure(conexionSql, cCodPal, cLot, rPes, fecha, cScc, cUni, cBul, cCodBar, ultimo, primero);
                        
                        if (resultado != 1)
                        {
                            comprobarSiguienteCod = true;
                            EscribirLog("Proc Alm. COD " + resultado);
                        }
                        else
                        {
                            comprobarSiguienteCod = false;
                        }
                    } 
                }
                File.Delete(path);
            }
        }

        /// <summary>
        /// Abre un archivo XML para solo lectura
        /// </summary>
        /// <param name="ruta"></param>
        /// <returns></returns>
        public static XmlTextReader AbrirXmlParaLectura(string ruta)
        {
            if (!File.Exists(ruta))
            {
                throw new FileNotFoundException(string.Format("Archivo de configuración '{0}' no encontrado.", ruta));
            }
            return new XmlTextReader(ruta);
        }

        private static int EjecutarStoredProcedure(SqlConnection conexion, string cCodPal, string cLot, float rPes, DateTime fecha, string cScc, string cUni, int cBul, string cCodBar, bool ultimoLote, bool primerLote)
        {
            using (var myCommand = new SqlCommand { CommandText = ConfigurationManager.AppSettings["InsertProcedureName"], Connection = conexion, CommandType = CommandType.StoredProcedure })
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

                if (result == 1)
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

        #region Métodos BD

        /// <summary>
        /// Obtiene una SqlConnection con la cadena de conexión especificada. Devuelve null en caso de error de conexión
        /// </summary>
        /// <returns></returns>
        public static SqlConnection AbrirConexion()
        {
            var cadenaConexion = ObtenerCadenaConexion(ConfigurationManager.AppSettings["ServerName"],
            ConfigurationManager.AppSettings["ServerPort"],
            ConfigurationManager.AppSettings["ServerDB"],
            ConfigurationManager.AppSettings["ServerUser"],
            ConfigurationManager.AppSettings["ServerPassword"],
            ConfigurationManager.AppSettings["ServerExtraParams"]);

            var connection = new SqlConnection(cadenaConexion);
            try
            {
                connection.Open();
                return connection;
            }
            catch (SqlException e)
            {
                EscribirLog(e.Message);
                return null;
            }
        }

        /// <summary>
        /// Construye una cadena de conexión sql
        /// </summary>
        /// <param name="server"></param>
        /// <param name="port"></param>
        /// <param name="db"></param>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <param name="extra"></param>
        /// <returns></returns>
        public static string ObtenerCadenaConexion(string server, string port, string db, string user, string password, string extra)
        {
            var cadenaConexion = "Data Source=" + server;
            cadenaConexion += string.IsNullOrEmpty(port) ? "" : "," + port;
            cadenaConexion += ";Initial Catalog = " + db + ";";
            cadenaConexion += "Integrated Security=False;uid = " + user + ";";
            cadenaConexion += "pwd = " + password + ";";
            cadenaConexion += extra;
            return cadenaConexion;
        }
        #endregion

        #region Utils

        /// <summary>
        /// Escribe en el archivo Log.txt una cadena de texto. (Utilizo este método para escribir los errores en el archivo Log.txt)
        /// </summary>
        /// <param name="message"></param>
        public static void EscribirLog(string message)
        {
            if (ConfigurationManager.AppSettings["WriteLog"].Equals("false")) return;

            FileStream fileStream = null;
            StreamWriter streamWriter = null;
            try
            {
                var logFilePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase) + @"\" + "Log.txt";

                if (logFilePath.Equals("")) return;
                var absolutePath = new Uri(logFilePath);
                var logFileInfo = new FileInfo(absolutePath.LocalPath);
                var logDirInfo = new DirectoryInfo(logFileInfo.DirectoryName);
                if (!logDirInfo.Exists) logDirInfo.Create();

                fileStream = !logFileInfo.Exists ? logFileInfo.Create() : new FileStream(absolutePath.LocalPath, FileMode.Append);
                streamWriter = new StreamWriter(fileStream, new UTF8Encoding(true, true));
                streamWriter.WriteLine(DateTime.Now.ToString(@"dd/MM/yyyy HH:mm:ss") + " " + message);
            }
            catch
            {
            }
            finally
            {
                if (streamWriter != null) streamWriter.Close();
                if (fileStream != null) fileStream.Close();
            }
        }

        private static bool IsFileLocked(FileInfo file)
        {
            FileStream stream = null;

            try
            {
                stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }

            //file is not locked
            return false;
        }

        public static int ObtenerSiglo(int anyo)
        {
            var siglo = anyo / 100;
            anyo %= 100;
            if (anyo > 0) siglo = siglo + 1;

            return siglo;
        }
        #endregion

    }
}
