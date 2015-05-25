using System;
using System.Globalization;
using System.Windows.Forms;
using EnterpriseDT.Net.Ftp;
using FriosillaBarCode.Properties;
using Symbol.Barcode2;

namespace FriosillaBarCode
{
    public partial class FormConfiguracion : Form
    {
        public FormConfiguracion()
        {
            InitializeComponent();
            Icon = Resources.iconFriosilla;
        }

        private void FormConfiguracion_Load(object sender, EventArgs e)
        {
            // Ponemos la barra de carga
            Cursor.Current = Cursors.WaitCursor;

            CargarDatos();
            RellenarListBox();
            if (Util.GetSetting("EmulatorMode").Equals("false")) RellenarComboBoxDispositivos();
           
            // Quitamos la barra de carga
            Cursor.Current = Cursors.Default;
        }

        /// <summary>
        /// Carga los datos en el formulario de configuración
        /// </summary>
        private void CargarDatos()
        {
            Util.LeerConfig();
            textBoxServerName.Text = Util.GetSetting("ServerName");
            textBoxServerPort.Text = Util.GetSetting("ServerPort");
            textBoxServerDB.Text = Util.GetSetting("ServerDB");
            textBoxServerUser.Text = Util.GetSetting("ServerUser");
            textBoxServerPass.Text = Util.GetSetting("ServerPassword");
            textBoxServerExtraParams.Text = Util.GetSetting("ServerExtraParams");
            textBoxFtpServerIp.Text = Util.GetSetting("FtpServerIp");
            textBoxFtpServerPort.Text = Util.GetSetting("FtpServerPort");
            textBoxFtpServerUser.Text = Util.GetSetting("FtpServerUser");
            textBoxFtpServerPassword.Text = Util.GetSetting("FtpServerPassword");
            textBoxMaxQueries.Text = Util.GetSetting("MaxQueries");
            textBoxTimeout.Text = Util.GetSetting("MaxQueriesTimeout");
            textBoxScanDataSize.Text = Util.GetSetting("ScanDataSize");
            textBoxScanTimeout.Text = Util.GetSetting("ScanTimeout");
            textBoxTimeout.Text = Util.GetSetting("MaxQueriesTimeout");
            checkBoxConnected.Checked = Util.GetSetting("ConnectAtStart").Equals("true");
            checkBoxDeleteOldCodes.Checked = Util.GetSetting("DeleteOldCodes").Equals("true");
            checkBoxScanContinuous.Checked = Util.GetSetting("ScanContinuous").Equals("true");
            checkBoxSoundEnabled.Checked = Util.GetSetting("SoundEnabled").Equals("true");
            checkBoxOnlyDatamatrix.Checked = Util.GetSetting("ScanOnlyDataMatrix").Equals("true");
            checkBoxWriteLog.Checked = Util.GetSetting("WriteLog").Equals("true");
            checkBoxSendByFtp.Checked = Util.GetSetting("SendByFtp").Equals("true");
        }

        /// <summary>
        /// Rellena el listbox
        /// </summary>
        private void RellenarListBox()
        {
            listBoxPales.DataSource = Datos.ObtenerPales();
        }

        private void RellenarComboBoxDispositivos()
        {
            var availableDevices = Devices.SupportedDevices;

            foreach (var device in availableDevices)
            {
                if (device.DeviceType != DEVICETYPES.UNKNOWN)
                {
                    comboBoxDispositivoLector.Items.Add(device.DeviceType + " - " + device.FriendlyName);
                }
                else
                {
                    comboBoxDispositivoLector.Items.Add(device.FriendlyName);
                }       
            }

            // Si ha encontrado algún dispositivo lector, seleccionamos como predeterminado el que esté guardado en la configuración
            if (comboBoxDispositivoLector.Items.Count <= 0) return;
            var indiceSeleccionado = (comboBoxDispositivoLector.Items.Count - 1 >= Util.GetIntSetting("BarcodeDevice")) ? Util.GetIntSetting("BarcodeDevice") : 0;
            comboBoxDispositivoLector.SelectedIndex = indiceSeleccionado;
        }

        /// <summary>
        /// Comprueba si es posible conectarse a la BD
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonProbarBD_Click(object sender, EventArgs e)
        {
            // Ponemos la barra de carga
            Cursor.Current = Cursors.WaitCursor;
            
            // Creamos la cadena de conexión
            var cadenaConexion = Util.ObtenerCadenaConexion(textBoxServerName.Text, 
                textBoxServerPort.Text, 
                textBoxServerDB.Text, 
                textBoxServerUser.Text, 
                textBoxServerPass.Text, 
                textBoxServerExtraParams.Text);

            if (Util.AbrirConexion(cadenaConexion) == null)
            {
                MessageBox.Show("Result.de Conexión con la BD. Compruebe que los datos sean correctos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand,MessageBoxDefaultButton.Button1);
            }
            else
            {
                MessageBox.Show("Conexión con BD Correcta", "OK", MessageBoxButtons.OK, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            }

            // Ocultamos la barra de carga
            Cursor.Current = Cursors.Default;
        }

        /// <summary>
        /// Guarda los cambios y cierra el formulario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItemAceptar_Click(object sender, EventArgs e)
        {
            Util.SetSetting("ServerName",textBoxServerName.Text);
            Util.SetSetting("ServerPort", textBoxServerPort.Text);
            Util.SetSetting("ServerDB", textBoxServerDB.Text);
            Util.SetSetting("ServerUser", textBoxServerUser.Text);
            Util.SetSetting("ServerPassword", textBoxServerPass.Text);
            Util.SetSetting("ServerExtraParams", textBoxServerExtraParams.Text);
            Util.SetSetting("FtpServerIp", textBoxFtpServerIp.Text);
            Util.SetSetting("FtpServerPort", textBoxFtpServerPort.Text);
            Util.SetSetting("FtpServerUser", textBoxFtpServerUser.Text);
            Util.SetSetting("FtpServerPassword", textBoxFtpServerPassword.Text);
            Util.SetSetting("BarcodeDevice", comboBoxDispositivoLector.SelectedIndex.ToString(CultureInfo.InvariantCulture));
            Util.SetSetting("MaxQueries", textBoxMaxQueries.Text);
            Util.SetSetting("ScanDataSize", textBoxScanDataSize.Text);
            Util.SetSetting("ScanTimeout", textBoxScanTimeout.Text);
            Util.SetSetting("MaxQueriesTimeout", textBoxTimeout.Text);
            Util.SetSetting("ConnectAtStart", checkBoxConnected.Checked ? "true" : "false");
            Util.SetSetting("ScanContinuous",  checkBoxScanContinuous.Checked ? "true" : "false");
            Util.SetSetting("SoundEnabled", checkBoxSoundEnabled.Checked ? "true" : "false");
            Util.SetSetting("ScanOnlyDataMatrix", checkBoxOnlyDatamatrix.Checked ? "true" : "false");
            Util.SetSetting("DeleteOldCodes", checkBoxDeleteOldCodes.Checked ? "true" : "false");
            Util.SetSetting("WriteLog", checkBoxWriteLog.Checked ? "true" : "false");
            Util.SetSetting("SendByFtp", checkBoxSendByFtp.Checked ? "true" : "false");

            if (!Util.GuardarConfig())
            {
                MessageBox.Show("Error guardado la configuración", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
            }
            else
            {          
                Close();
                DialogResult = DialogResult.OK;
            }
        }

        /// <summary>
        /// Cierra el formulario sin guardar los cambios
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItemCancelar_Click(object sender, EventArgs e)
        {
            Util.LeerConfig();
            Close();
            DialogResult = DialogResult.Cancel;
        }

        /// <summary>
        /// Intenta descargar de la bd del servidor la lista de códigos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDescargar_Click(object sender, EventArgs e)
        {
            // Ponemos la barra de carga
            Cursor.Current = Cursors.WaitCursor;
            
            Datos.CargarPalesDeBd();
            RellenarListBox();

            // Ocultamos la barra de carga
            Cursor.Current = Cursors.Default;
        }

        #region agregar/editar/eliminar
        private void buttonAgregar_Click(object sender, EventArgs e)
        {
            if (textBoxEditarPale.Text == "")
            {
                MessageBox.Show("El código no puede estar vacío", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
            }
            else if (!Datos.AgregarPale(textBoxEditarPale.Text,"", true, true))
            {
                MessageBox.Show("Result.agregando código, asegúrese de que no está existe", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
            }
            else
            {
                MessageBox.Show("Código agregado correctamente", "OK", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
                textBoxEditarPale.Text = "";
            }
            RellenarListBox();
        }

        private void buttonEditar_Click(object sender, EventArgs e)
        {
            if (textBoxEditarPale.Text == "" || listBoxPales.SelectedItem == null)
            {
                MessageBox.Show("El código no puede estar vacío", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
            }
            else if (!Datos.EditarPale(listBoxPales.SelectedItem.ToString(), textBoxEditarPale.Text))
            {
                MessageBox.Show("Result.editando código", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
            }
            else
            {
                MessageBox.Show("Código editado correctamente", "OK", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
                RellenarListBox();
                textBoxEditarPale.Text = "";
                buttonEliminar.Enabled = false;
                buttonEditar.Enabled = false;
            }
        }

        private void buttonEliminar_Click(object sender, EventArgs e)
        {
            if (listBoxPales.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar un código para eliminar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
            }
            else if (!Datos.EliminarPale(textBoxEditarPale.Text))
            {
                MessageBox.Show("Result.eliminando código", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
            }
            else
            {
                MessageBox.Show("Código eliminado correctamente", "OK", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
                RellenarListBox();
                textBoxEditarPale.Text = "";
                buttonEliminar.Enabled = false;
                buttonEditar.Enabled = false;
            }
        }

        private void listBoxPales_SelectedValueChanged(object sender, EventArgs e)
        {
            buttonEditar.Enabled = true;
            buttonEliminar.Enabled = true;
            textBoxEditarPale.Text = listBoxPales.SelectedItem.ToString();
        }
        #endregion

        private void checkBoxConConexion_Click(object sender, EventArgs e)
        {
            if (!checkBoxConnected.Checked) return;
            var result = MessageBox.Show("¿Desea descargar la lista de códigos de la BD ahora?", "Descargar", MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);

            if (result.Equals(DialogResult.Yes))
            {
                Datos.CargarPalesDeBd();
            }
        }

        /// <summary>
        /// Campos que solo aceptan números
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnlyNumbersKeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
                e.Handled = true;
        }


        /*
        private void buttonProbarArc_Click(object sender, EventArgs e)
        {
            // Ponemos la barra de carga
            Cursor.Current = Cursors.WaitCursor; 
            
            var puerto = 1;

            try
            {
                puerto = int.Parse(textBoxFtpServerPort.Text);
            }
            catch(Exception)
            {
                 MessageBox.Show("Result. el puerto debe ser un número del 1 al 65535", "Result., MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
            }

            if (Util.PingHost(textBoxFtpServerIp.Text, puerto))
            {
                MessageBox.Show("Conexión Correcta", "OK", MessageBoxButtons.OK, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            }
            else
            {
                MessageBox.Show("Result.conectando con el servidor de archivos", "Result., MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
            }

            // Ocultamos la barra de carga
            Cursor.Current = Cursors.Default;
        }
*/

        private void linkLabelVaciarBD_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("¿Desea eliminar los datos locales?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);

            if (!result.Equals(DialogResult.Yes)) return;
            // Ponemos la barra de carga
            Cursor.Current = Cursors.WaitCursor;

            // Eliminamos los datos locales
            if (Datos.EliminarPales() && Datos.EliminarBarcodes())
            {
                MessageBox.Show("Datos locales eliminados correctamente", "Ok", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
                // Vaciamos la lista de palés
                listBoxPales.DataSource = null;
            }
            else
            {
                MessageBox.Show("Result.eliminando archivos locales. Consulte con su administrador", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
            }

            // Ocultamos la barra de carga
            Cursor.Current = Cursors.Default;
        }

        private void checkBoxDeleteOldCodes_Click(object sender, EventArgs e)
        {
            Util.SetSetting("DeleteOldCodes", checkBoxDeleteOldCodes.Checked.ToString());
        }

        private void buttonPorbarArc_Click(object sender, EventArgs e)
        {
            // Ponemos la barra de carga
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                var ftp = new ExFTPConnection
                {
                    ServerAddress = textBoxFtpServerIp.Text,
                    UserName = textBoxFtpServerUser.Text,
                    Password = textBoxFtpServerPassword.Text,
                    ServerPort = int.Parse(textBoxFtpServerPort.Text),
                    LicenseOwner = Util.GetSetting("FtpLicenseOwner"),
                    LicenseKey = Util.GetSetting("FtpLicenseKey")
                };
                
                ftp.Connect();
                ftp.Close();
                MessageBox.Show("OK");
            }
            catch (Exception)
            {
                MessageBox.Show("Error conectando con servidor FTP, compruebe los datos.");
            }
            finally
            {
                // Ocultamos la barra de carga
                Cursor.Current = Cursors.Default;
            }
        }
    }
}