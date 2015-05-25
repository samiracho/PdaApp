using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Xml;
using FriosillaBarCode.Properties;

namespace FriosillaBarCode
{
    public struct Result
    {
        // ReSharper disable InconsistentNaming
        public const int ERROR_NUM_LOTES = -2;
        public const int ERROR_LONG_COD = -1;
        public const int ERROR_FORM_COD = 0;
        public const int OK = 1;
        public const int ERROR_CAJA_EXISTE = 2;
        public const int ERROR_LOTE_EXISTE = 3;
        public const int ERROR_PALET_EXISTE = 4;
        public const int ERROR_CAJA_NO_EXISTE = 5;
        public const int ERROR_COD_DUP = 6;
        public const int ERROR_PALE_O_COD_NULL = 7;
    }

    internal static class Util
    {        
        public static readonly string NombreArchivoConf = GetPath("App.config","");
        private static Dictionary<string, string> _settings;
        
        #region Métodos para leer/escribir del archivo de configuración

        /// <summary>
        /// Lee los parámetros del archivo de configuración
        /// </summary>
        public static void LeerConfig()
        {
            _settings = new Dictionary<string, string>();

            try
            {
                using (var xmlTextReader = AbrirXmlParaLectura(NombreArchivoConf))
                {
                    while (xmlTextReader.Read())
                    {
                        if (xmlTextReader.Name == "add")
                        {
                            _settings.Add(xmlTextReader.GetAttribute("key"), xmlTextReader.GetAttribute("value"));
                        }
                    }
                }
            }
            catch (Exception e)
            {
                EscribirLog(e.Message);
            }        
        }

        /// <summary>
        /// Obtiene un uint de una clave archivo de configuración
        /// </summary>
        /// <param name="settingName">Nombre de la clave del archivo de configuración</param>
        /// <returns></returns>
        public static uint GetUIntSetting(string settingName)
        {
            uint result = 0;
            try
            {
                result = uint.Parse(_settings[settingName]);
                return result;
            }
            catch (Exception e)
            {
                EscribirLog(e.Message);
                return result;
            }
        }

        /// <summary>
        /// Obtiene un uint de una clave archivo de configuración
        /// </summary>
        /// <param name="settingName">Nombre de la clave del archivo de configuración</param>
        /// <returns></returns>
        public static int GetIntSetting(string settingName)
        {
            int result = 0;
            try
            {
                result = int.Parse(_settings[settingName]);
                return result;
            }
            catch (Exception e)
            {
                EscribirLog(e.Message);
                return result;
            }
        }
        
        /// <summary>
        /// Obtiene un string de una clave archivo de configuración
        /// </summary>
        /// <param name="settingName">Nombre de la clave del archivo de configuración</param>
        /// <returns></returns>
        public static string GetSetting(string settingName)
        {
            var result = "";
            try
            {
                result = _settings[settingName];
                return result;
            }
            catch (Exception e)
            {
                EscribirLog(e.Message);
                return result;
            }
        }
        
        /// <summary>
        /// Escribe en la lista de settings el valor especificado (no lo guarda en el XML. Para guardar los cambios de forma persistente hay que llamar a GuardarConfig())
        /// </summary>
        /// <param name="setting"></param>
        /// <param name="value"></param>
        public static void SetSetting(string setting, string value)
        {
            _settings[setting] = value;
        }

        /// <summary>
        /// Devuelve un string de la lista de resources de la aplicación
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetResourceString(string name)
        {
            return Resources.ResourceManager.GetString(name);
        }

        /// <summary>
        /// Guarda los parámetros en el archivo de configuración
        /// </summary>
        public static bool GuardarConfig()
        {
            try
            {
                var xmlDocument = AbrirXmlParaEscritura(NombreArchivoConf);
                var nodeList = xmlDocument.SelectNodes("configuration/appSettings/add");

                foreach (XmlNode node in nodeList)
                {
                    node.Attributes["value"].Value = _settings[node.Attributes["key"].Value];
                }
                xmlDocument.Save(NombreArchivoConf);

                return true;
            }
            catch (Exception e)
            {
                EscribirLog(e.Message);
                return false;
            }
        }
        #endregion

        #region Utilidades XML

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

        /// <summary>
        /// Abre una archivo XML para lectura/escritura
        /// </summary>
        /// <param name="ruta"></param>
        /// <returns></returns>
        public static XmlDocument AbrirXmlParaEscritura(string ruta)
        {
            if (!File.Exists(ruta))
            {
                throw new FileNotFoundException(string.Format("Archivo de configuración '{0}' no encontrado.", ruta));
            }
            
            var document = new XmlDocument();
            document.Load(ruta);
            return document;
        }

        /// <summary>
        /// Obtiene la ruta completa de un archivo (nombre incluido) a partir de un path y el nombre del archivo.
        /// Comprueba si el path es absoluto o relativo al directorio de la aplicación
        /// </summary>
        /// <param name="nombreArchivo"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetPath(string nombreArchivo, string path)
        {
            var pathAplicacion = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
            string filePath;

            path = string.IsNullOrEmpty(path) ? "" : @path + @"\";

            if (Directory.Exists(pathAplicacion + @"\" + @path))
            {               
                filePath = pathAplicacion + @"\" + @path + nombreArchivo;
            }
            else
            {
                filePath = @path + nombreArchivo;
            }

            return filePath;
        }

        #endregion

        #region Métodos Base de Datos
        
        /// <summary>
        /// Obtiene una SqlConnection a partir de la cadena de conexión del archivo de configuración. Devuelve null en caso de error de conexión
        /// </summary>
        /// <returns></returns>
        public static SqlConnection AbrirConexion()
        {
            var cadenaConexion = MiCadenaConexion();
            return AbrirConexion( cadenaConexion);
        }

        /// <summary>
        /// Obtiene una SqlConnection con la cadena de conexión especificada. Devuelve null en caso de error de conexión
        /// </summary>
        /// <param name="cadenaConexion">Cadena de conexión sql</param>
        /// <returns></returns>
        public static SqlConnection AbrirConexion(string cadenaConexion)
        {       
            cadenaConexion = string.IsNullOrEmpty(cadenaConexion) ? MiCadenaConexion(): cadenaConexion;

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
            cadenaConexion += "pwd = " + password+";";
            cadenaConexion += extra;
            return cadenaConexion;     
        }

        /// <summary>
        /// Obtiene la cadena de conexión sql a partir de los datos almacenados en settings
        /// </summary>
        /// <returns></returns>
        public static string MiCadenaConexion()
        {
            var cadenaConexion = ObtenerCadenaConexion(_settings["ServerName"],
            _settings["ServerPort"],
            _settings["ServerDB"],
            _settings["ServerUser"],
            _settings["ServerPassword"],
            _settings["ServerExtraParams"]);

            return cadenaConexion;
        }
        #endregion

        #region Utilidades 

        /// <summary>
        /// Elimina los caracteres no numéricos de un string
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string EliminarCaracteresNoNumericos(string s)
        {
            var result = new StringBuilder(s.Length);
            foreach (var c in s)
            {
                var b = (byte)c;
                if (b >= 48 && b <= 57)
                    result.Append(c);
            }
            return result.ToString().Trim();
        }

        public static int ObtenerSiglo(int anyo)
        {
            var siglo = anyo / 100;
            anyo %= 100;
            if (anyo > 0) siglo = siglo + 1;

            return siglo;
        }

        public static void BeepError(Symbol.Audio.Controller audioController)
        {
            if (audioController!=null && GetSetting("SoundEnabled").Equals("true")) audioController.PlayAudio(300, 300);
        }

        public static void BeepOk(Symbol.Audio.Controller audioController)
        {
            if (audioController != null && GetSetting("SoundEnabled").Equals("true")) audioController.PlayAudio(300, 2670);
        }

        /// <summary>
        /// Hace ping a un host en un puerto en concreto
        /// </summary>
        /// <param name="hostUri"></param>
        /// <param name="portNumber"></param>
        /// <returns></returns>
        public static bool PingHost(string hostUri, int portNumber)
        {
            try
            {
                // ReSharper disable once UnusedVariable
                var client = new TcpClient(hostUri, portNumber);
                return true;
            }
            catch (Exception ex)
            {
                EscribirLog(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Crea una notificación emergente que se oculta automáticamente al cabo de unos segundos
        /// </summary>
        /// <param name="text"></param>
        /// <param name="caption"></param>
        public static void CrearNotificacion(string text, string caption)
        {
            var notificacion = new Microsoft.WindowsCE.Forms.Notification
            {
                InitialDuration = 1,
                Text = text,
                Caption = caption
            };
            notificacion.BalloonChanged += notification_BalloonChanged;
            notificacion.Visible = true;
        }

        private static void notification_BalloonChanged(object sender, Microsoft.WindowsCE.Forms.BalloonChangedEventArgs e)
        {
            if (e.Visible) return;
            var not = (Microsoft.WindowsCE.Forms.Notification)sender;
            not.Dispose();
        }

        /// <summary>
        /// Escribe en el archivo Log.txt una cadena de texto. (Utilizo este método para escribir los errores en el archivo Log.txt)
        /// </summary>
        /// <param name="message"></param>
        public static void EscribirLog(string message)
        {
            if (GetSetting("WriteLog").Equals("false")) return;

            FileStream fileStream = null;
            StreamWriter streamWriter = null;
            try
            {
                var logFilePath = GetPath("Log.txt","");

                if (logFilePath.Equals("")) return;
                var logFileInfo = new FileInfo(logFilePath);
                var logDirInfo = new DirectoryInfo(logFileInfo.DirectoryName);
                if (!logDirInfo.Exists) logDirInfo.Create();

                fileStream = !logFileInfo.Exists ? logFileInfo.Create() : new FileStream(logFilePath, FileMode.Append);
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
        #endregion
    }
}
