using System;
using System.Windows.Forms;
using System.IO;

namespace FriosillaBarCode
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [MTAThread]
        static void Main()
        {
            if (ComprobacionesIniciales())
            {
                Application.Run(new FormPrincipal());
            }
            else Application.Exit();
        }

        /// <summary>
        /// Comprueba que todos los ficheros necesarios para la aplicación sean accesibles
        /// </summary>
        private static bool ComprobacionesIniciales()
        {
            if (!File.Exists(Util.NombreArchivoConf))
            {
                var msg = String.Format("Falta el archivo {0}. Debe reinstalar la aplicación.", Util.NombreArchivoConf);
                MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                Util.EscribirLog(msg);
                return false;
            }
            Util.LeerConfig();

            if (!File.Exists(Datos.RutaAbsArchivoPales) || !File.Exists(Datos.RutaAbsArchivoBarcodes))
            {
                const string msg = "Faltan uno o más archivos de datos. Debe reinstalar la aplicación.";
                MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                Util.EscribirLog(msg);
                return false;
            }
            return true;
        }

    }
}