namespace FriosillaBarCode
{
    partial class FormConfiguracion
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenuConfiguracion;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mainMenuConfiguracion = new System.Windows.Forms.MainMenu();
            this.menuItemAceptar = new System.Windows.Forms.MenuItem();
            this.menuItemCancelar = new System.Windows.Forms.MenuItem();
            this.checkBoxConnected = new System.Windows.Forms.CheckBox();
            this.textBoxServerName = new System.Windows.Forms.TextBox();
            this.labelIp = new System.Windows.Forms.Label();
            this.labelPuerto = new System.Windows.Forms.Label();
            this.textBoxServerPort = new System.Windows.Forms.TextBox();
            this.labelPass = new System.Windows.Forms.Label();
            this.textBoxServerPass = new System.Windows.Forms.TextBox();
            this.labelUsuario = new System.Windows.Forms.Label();
            this.textBoxServerUser = new System.Windows.Forms.TextBox();
            this.linkLabelVaciarBD = new System.Windows.Forms.LinkLabel();
            this.tabControlOpciones = new System.Windows.Forms.TabControl();
            this.tabPagePales = new System.Windows.Forms.TabPage();
            this.textBoxEditarPale = new System.Windows.Forms.TextBox();
            this.listBoxPales = new System.Windows.Forms.ListBox();
            this.buttonDescargar = new System.Windows.Forms.Button();
            this.buttonEliminar = new System.Windows.Forms.Button();
            this.buttonEditar = new System.Windows.Forms.Button();
            this.buttonAgregar = new System.Windows.Forms.Button();
            this.tabPageEscaner = new System.Windows.Forms.TabPage();
            this.checkBoxOnlyDatamatrix = new System.Windows.Forms.CheckBox();
            this.textBoxScanDataSize = new System.Windows.Forms.TextBox();
            this.labelScanDataSize = new System.Windows.Forms.Label();
            this.textBoxScanTimeout = new System.Windows.Forms.TextBox();
            this.labelScanTimeot = new System.Windows.Forms.Label();
            this.checkBoxScanContinuous = new System.Windows.Forms.CheckBox();
            this.checkBoxSoundEnabled = new System.Windows.Forms.CheckBox();
            this.comboBoxDispositivoLector = new System.Windows.Forms.ComboBox();
            this.labelDispositivoLector = new System.Windows.Forms.Label();
            this.tabPageGeneral = new System.Windows.Forms.TabPage();
            this.checkBoxSendByFtp = new System.Windows.Forms.CheckBox();
            this.checkBoxWriteLog = new System.Windows.Forms.CheckBox();
            this.checkBoxDeleteOldCodes = new System.Windows.Forms.CheckBox();
            this.tabPageServidorBD = new System.Windows.Forms.TabPage();
            this.textBoxTimeout = new System.Windows.Forms.TextBox();
            this.labelTimeout = new System.Windows.Forms.Label();
            this.labelMaxQueries = new System.Windows.Forms.Label();
            this.textBoxMaxQueries = new System.Windows.Forms.TextBox();
            this.textBoxServerExtraParams = new System.Windows.Forms.TextBox();
            this.labelParametros = new System.Windows.Forms.Label();
            this.labelBD = new System.Windows.Forms.Label();
            this.textBoxServerDB = new System.Windows.Forms.TextBox();
            this.buttonProbarBD = new System.Windows.Forms.Button();
            this.tabPageSocket = new System.Windows.Forms.TabPage();
            this.textBoxFtpServerUser = new System.Windows.Forms.TextBox();
            this.textBoxFtpServerPassword = new System.Windows.Forms.TextBox();
            this.labelUsuarioFtp = new System.Windows.Forms.Label();
            this.labelPassFtp = new System.Windows.Forms.Label();
            this.textBoxFtpServerIp = new System.Windows.Forms.TextBox();
            this.textBoxFtpServerPort = new System.Windows.Forms.TextBox();
            this.labelIpArchivos = new System.Windows.Forms.Label();
            this.labelFtpServerPort = new System.Windows.Forms.Label();
            this.buttonPorbarArc = new System.Windows.Forms.Button();
            this.labelReiniciar = new System.Windows.Forms.Label();
            this.tabControlOpciones.SuspendLayout();
            this.tabPagePales.SuspendLayout();
            this.tabPageEscaner.SuspendLayout();
            this.tabPageGeneral.SuspendLayout();
            this.tabPageServidorBD.SuspendLayout();
            this.tabPageSocket.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenuConfiguracion
            // 
            this.mainMenuConfiguracion.MenuItems.Add(this.menuItemAceptar);
            this.mainMenuConfiguracion.MenuItems.Add(this.menuItemCancelar);
            // 
            // menuItemAceptar
            // 
            this.menuItemAceptar.Text = "Aceptar";
            this.menuItemAceptar.Click += new System.EventHandler(this.menuItemAceptar_Click);
            // 
            // menuItemCancelar
            // 
            this.menuItemCancelar.Text = "Cancelar";
            this.menuItemCancelar.Click += new System.EventHandler(this.menuItemCancelar_Click);
            // 
            // checkBoxConnected
            // 
            this.checkBoxConnected.Location = new System.Drawing.Point(0, 7);
            this.checkBoxConnected.Name = "checkBoxConnected";
            this.checkBoxConnected.Size = new System.Drawing.Size(470, 33);
            this.checkBoxConnected.TabIndex = 0;
            this.checkBoxConnected.Text = "Descargar palés al iniciar la aplicación";
            this.checkBoxConnected.Click += new System.EventHandler(this.checkBoxConConexion_Click);
            // 
            // textBoxServerName
            // 
            this.textBoxServerName.Location = new System.Drawing.Point(59, 4);
            this.textBoxServerName.MaxLength = 100;
            this.textBoxServerName.Name = "textBoxServerName";
            this.textBoxServerName.Size = new System.Drawing.Size(199, 41);
            this.textBoxServerName.TabIndex = 4;
            // 
            // labelIp
            // 
            this.labelIp.Location = new System.Drawing.Point(9, 5);
            this.labelIp.Name = "labelIp";
            this.labelIp.Size = new System.Drawing.Size(44, 40);
            this.labelIp.Text = "IP:";
            // 
            // labelPuerto
            // 
            this.labelPuerto.Location = new System.Drawing.Point(264, 5);
            this.labelPuerto.Name = "labelPuerto";
            this.labelPuerto.Size = new System.Drawing.Size(90, 40);
            this.labelPuerto.Text = "Puerto:";
            // 
            // textBoxServerPort
            // 
            this.textBoxServerPort.Location = new System.Drawing.Point(365, 4);
            this.textBoxServerPort.MaxLength = 5;
            this.textBoxServerPort.Name = "textBoxServerPort";
            this.textBoxServerPort.Size = new System.Drawing.Size(108, 41);
            this.textBoxServerPort.TabIndex = 5;
            this.textBoxServerPort.Tag = "";
            this.textBoxServerPort.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnlyNumbersKeyPress);
            // 
            // labelPass
            // 
            this.labelPass.Location = new System.Drawing.Point(288, 52);
            this.labelPass.Name = "labelPass";
            this.labelPass.Size = new System.Drawing.Size(66, 40);
            this.labelPass.Text = "Pass:";
            // 
            // textBoxServerPass
            // 
            this.textBoxServerPass.Location = new System.Drawing.Point(365, 51);
            this.textBoxServerPass.Name = "textBoxServerPass";
            this.textBoxServerPass.PasswordChar = '*';
            this.textBoxServerPass.Size = new System.Drawing.Size(108, 41);
            this.textBoxServerPass.TabIndex = 7;
            this.textBoxServerPass.Tag = "";
            // 
            // labelUsuario
            // 
            this.labelUsuario.Location = new System.Drawing.Point(9, 52);
            this.labelUsuario.Name = "labelUsuario";
            this.labelUsuario.Size = new System.Drawing.Size(99, 40);
            this.labelUsuario.Text = "Usuario:";
            // 
            // textBoxServerUser
            // 
            this.textBoxServerUser.Location = new System.Drawing.Point(114, 51);
            this.textBoxServerUser.Name = "textBoxServerUser";
            this.textBoxServerUser.Size = new System.Drawing.Size(144, 41);
            this.textBoxServerUser.TabIndex = 6;
            this.textBoxServerUser.Text = "Usuario";
            // 
            // linkLabelVaciarBD
            // 
            this.linkLabelVaciarBD.Location = new System.Drawing.Point(3, 201);
            this.linkLabelVaciarBD.Name = "linkLabelVaciarBD";
            this.linkLabelVaciarBD.Size = new System.Drawing.Size(233, 40);
            this.linkLabelVaciarBD.TabIndex = 1;
            this.linkLabelVaciarBD.Text = "Vacíar Datos locales";
            this.linkLabelVaciarBD.Click += new System.EventHandler(this.linkLabelVaciarBD_Click);
            // 
            // tabControlOpciones
            // 
            this.tabControlOpciones.Controls.Add(this.tabPagePales);
            this.tabControlOpciones.Controls.Add(this.tabPageEscaner);
            this.tabControlOpciones.Controls.Add(this.tabPageGeneral);
            this.tabControlOpciones.Controls.Add(this.tabPageServidorBD);
            this.tabControlOpciones.Controls.Add(this.tabPageSocket);
            this.tabControlOpciones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlOpciones.Location = new System.Drawing.Point(0, 0);
            this.tabControlOpciones.Name = "tabControlOpciones";
            this.tabControlOpciones.SelectedIndex = 0;
            this.tabControlOpciones.Size = new System.Drawing.Size(480, 376);
            this.tabControlOpciones.TabIndex = 13;
            // 
            // tabPagePales
            // 
            this.tabPagePales.Controls.Add(this.textBoxEditarPale);
            this.tabPagePales.Controls.Add(this.listBoxPales);
            this.tabPagePales.Controls.Add(this.buttonDescargar);
            this.tabPagePales.Controls.Add(this.buttonEliminar);
            this.tabPagePales.Controls.Add(this.buttonEditar);
            this.tabPagePales.Controls.Add(this.buttonAgregar);
            this.tabPagePales.Location = new System.Drawing.Point(0, 0);
            this.tabPagePales.Name = "tabPagePales";
            this.tabPagePales.Size = new System.Drawing.Size(480, 332);
            this.tabPagePales.Text = "Palés";
            // 
            // textBoxEditarPale
            // 
            this.textBoxEditarPale.Location = new System.Drawing.Point(3, 141);
            this.textBoxEditarPale.MaxLength = 2000;
            this.textBoxEditarPale.Name = "textBoxEditarPale";
            this.textBoxEditarPale.Size = new System.Drawing.Size(474, 41);
            this.textBoxEditarPale.TabIndex = 9;
            // 
            // listBoxPales
            // 
            this.listBoxPales.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.listBoxPales.Location = new System.Drawing.Point(3, 3);
            this.listBoxPales.Name = "listBoxPales";
            this.listBoxPales.Size = new System.Drawing.Size(474, 132);
            this.listBoxPales.TabIndex = 8;
            this.listBoxPales.SelectedValueChanged += new System.EventHandler(this.listBoxPales_SelectedValueChanged);
            // 
            // buttonDescargar
            // 
            this.buttonDescargar.Location = new System.Drawing.Point(92, 251);
            this.buttonDescargar.Name = "buttonDescargar";
            this.buttonDescargar.Size = new System.Drawing.Size(296, 67);
            this.buttonDescargar.TabIndex = 7;
            this.buttonDescargar.Text = "Descargar del Servidor";
            this.buttonDescargar.Click += new System.EventHandler(this.buttonDescargar_Click);
            // 
            // buttonEliminar
            // 
            this.buttonEliminar.Enabled = false;
            this.buttonEliminar.Location = new System.Drawing.Point(348, 188);
            this.buttonEliminar.Name = "buttonEliminar";
            this.buttonEliminar.Size = new System.Drawing.Size(129, 34);
            this.buttonEliminar.TabIndex = 6;
            this.buttonEliminar.Text = "Eliminar";
            this.buttonEliminar.Click += new System.EventHandler(this.buttonEliminar_Click);
            // 
            // buttonEditar
            // 
            this.buttonEditar.Enabled = false;
            this.buttonEditar.Location = new System.Drawing.Point(176, 188);
            this.buttonEditar.Name = "buttonEditar";
            this.buttonEditar.Size = new System.Drawing.Size(129, 34);
            this.buttonEditar.TabIndex = 5;
            this.buttonEditar.Text = "Editar";
            this.buttonEditar.Click += new System.EventHandler(this.buttonEditar_Click);
            // 
            // buttonAgregar
            // 
            this.buttonAgregar.Location = new System.Drawing.Point(3, 188);
            this.buttonAgregar.Name = "buttonAgregar";
            this.buttonAgregar.Size = new System.Drawing.Size(129, 34);
            this.buttonAgregar.TabIndex = 4;
            this.buttonAgregar.Text = "Agregar";
            this.buttonAgregar.Click += new System.EventHandler(this.buttonAgregar_Click);
            // 
            // tabPageEscaner
            // 
            this.tabPageEscaner.Controls.Add(this.labelReiniciar);
            this.tabPageEscaner.Controls.Add(this.checkBoxOnlyDatamatrix);
            this.tabPageEscaner.Controls.Add(this.textBoxScanDataSize);
            this.tabPageEscaner.Controls.Add(this.labelScanDataSize);
            this.tabPageEscaner.Controls.Add(this.textBoxScanTimeout);
            this.tabPageEscaner.Controls.Add(this.labelScanTimeot);
            this.tabPageEscaner.Controls.Add(this.checkBoxScanContinuous);
            this.tabPageEscaner.Controls.Add(this.checkBoxSoundEnabled);
            this.tabPageEscaner.Controls.Add(this.comboBoxDispositivoLector);
            this.tabPageEscaner.Controls.Add(this.labelDispositivoLector);
            this.tabPageEscaner.Location = new System.Drawing.Point(0, 0);
            this.tabPageEscaner.Name = "tabPageEscaner";
            this.tabPageEscaner.Size = new System.Drawing.Size(480, 332);
            this.tabPageEscaner.Text = "Escáner";
            // 
            // checkBoxOnlyDatamatrix
            // 
            this.checkBoxOnlyDatamatrix.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.checkBoxOnlyDatamatrix.Location = new System.Drawing.Point(3, 223);
            this.checkBoxOnlyDatamatrix.Name = "checkBoxOnlyDatamatrix";
            this.checkBoxOnlyDatamatrix.Size = new System.Drawing.Size(347, 35);
            this.checkBoxOnlyDatamatrix.TabIndex = 19;
            this.checkBoxOnlyDatamatrix.Text = "Solo DATAMATRIX ";
            // 
            // textBoxScanDataSize
            // 
            this.textBoxScanDataSize.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.textBoxScanDataSize.Location = new System.Drawing.Point(240, 90);
            this.textBoxScanDataSize.MaxLength = 5;
            this.textBoxScanDataSize.Name = "textBoxScanDataSize";
            this.textBoxScanDataSize.Size = new System.Drawing.Size(233, 38);
            this.textBoxScanDataSize.TabIndex = 15;
            this.textBoxScanDataSize.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnlyNumbersKeyPress);
            // 
            // labelScanDataSize
            // 
            this.labelScanDataSize.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.labelScanDataSize.Location = new System.Drawing.Point(3, 91);
            this.labelScanDataSize.Name = "labelScanDataSize";
            this.labelScanDataSize.Size = new System.Drawing.Size(227, 40);
            this.labelScanDataSize.Text = "Scan Data Size:";
            // 
            // textBoxScanTimeout
            // 
            this.textBoxScanTimeout.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.textBoxScanTimeout.Location = new System.Drawing.Point(240, 46);
            this.textBoxScanTimeout.MaxLength = 5;
            this.textBoxScanTimeout.Name = "textBoxScanTimeout";
            this.textBoxScanTimeout.Size = new System.Drawing.Size(233, 38);
            this.textBoxScanTimeout.TabIndex = 13;
            this.textBoxScanTimeout.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnlyNumbersKeyPress);
            // 
            // labelScanTimeot
            // 
            this.labelScanTimeot.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.labelScanTimeot.Location = new System.Drawing.Point(3, 47);
            this.labelScanTimeot.Name = "labelScanTimeot";
            this.labelScanTimeot.Size = new System.Drawing.Size(238, 40);
            this.labelScanTimeot.Text = "Scan Timeout (ms):";
            // 
            // checkBoxScanContinuous
            // 
            this.checkBoxScanContinuous.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.checkBoxScanContinuous.Location = new System.Drawing.Point(3, 180);
            this.checkBoxScanContinuous.Name = "checkBoxScanContinuous";
            this.checkBoxScanContinuous.Size = new System.Drawing.Size(347, 35);
            this.checkBoxScanContinuous.TabIndex = 17;
            this.checkBoxScanContinuous.Text = "Activar Escáner contínuo";
            // 
            // checkBoxSoundEnabled
            // 
            this.checkBoxSoundEnabled.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.checkBoxSoundEnabled.Location = new System.Drawing.Point(3, 137);
            this.checkBoxSoundEnabled.Name = "checkBoxSoundEnabled";
            this.checkBoxSoundEnabled.Size = new System.Drawing.Size(347, 35);
            this.checkBoxSoundEnabled.TabIndex = 16;
            this.checkBoxSoundEnabled.Text = "Activar Sonidos";
            // 
            // comboBoxDispositivoLector
            // 
            this.comboBoxDispositivoLector.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.comboBoxDispositivoLector.Location = new System.Drawing.Point(175, 3);
            this.comboBoxDispositivoLector.Name = "comboBoxDispositivoLector";
            this.comboBoxDispositivoLector.Size = new System.Drawing.Size(298, 38);
            this.comboBoxDispositivoLector.TabIndex = 8;
            // 
            // labelDispositivoLector
            // 
            this.labelDispositivoLector.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.labelDispositivoLector.Location = new System.Drawing.Point(3, 4);
            this.labelDispositivoLector.Name = "labelDispositivoLector";
            this.labelDispositivoLector.Size = new System.Drawing.Size(166, 40);
            this.labelDispositivoLector.Text = "Dispositivo";
            // 
            // tabPageGeneral
            // 
            this.tabPageGeneral.Controls.Add(this.checkBoxSendByFtp);
            this.tabPageGeneral.Controls.Add(this.checkBoxWriteLog);
            this.tabPageGeneral.Controls.Add(this.checkBoxDeleteOldCodes);
            this.tabPageGeneral.Controls.Add(this.linkLabelVaciarBD);
            this.tabPageGeneral.Controls.Add(this.checkBoxConnected);
            this.tabPageGeneral.Location = new System.Drawing.Point(0, 0);
            this.tabPageGeneral.Name = "tabPageGeneral";
            this.tabPageGeneral.Size = new System.Drawing.Size(480, 332);
            this.tabPageGeneral.Text = "General";
            // 
            // checkBoxSendByFtp
            // 
            this.checkBoxSendByFtp.Location = new System.Drawing.Point(0, 124);
            this.checkBoxSendByFtp.Name = "checkBoxSendByFtp";
            this.checkBoxSendByFtp.Size = new System.Drawing.Size(470, 33);
            this.checkBoxSendByFtp.TabIndex = 10;
            this.checkBoxSendByFtp.Text = "Enviar Códigos por FTP";
            // 
            // checkBoxWriteLog
            // 
            this.checkBoxWriteLog.Location = new System.Drawing.Point(0, 85);
            this.checkBoxWriteLog.Name = "checkBoxWriteLog";
            this.checkBoxWriteLog.Size = new System.Drawing.Size(470, 33);
            this.checkBoxWriteLog.TabIndex = 9;
            this.checkBoxWriteLog.Text = "Guardar Errores en Log";
            // 
            // checkBoxDeleteOldCodes
            // 
            this.checkBoxDeleteOldCodes.Location = new System.Drawing.Point(0, 46);
            this.checkBoxDeleteOldCodes.Name = "checkBoxDeleteOldCodes";
            this.checkBoxDeleteOldCodes.Size = new System.Drawing.Size(470, 33);
            this.checkBoxDeleteOldCodes.TabIndex = 8;
            this.checkBoxDeleteOldCodes.Text = "Eliminar palés antiguos al descargar";
            this.checkBoxDeleteOldCodes.Click += new System.EventHandler(this.checkBoxDeleteOldCodes_Click);
            // 
            // tabPageServidorBD
            // 
            this.tabPageServidorBD.Controls.Add(this.textBoxTimeout);
            this.tabPageServidorBD.Controls.Add(this.labelTimeout);
            this.tabPageServidorBD.Controls.Add(this.labelMaxQueries);
            this.tabPageServidorBD.Controls.Add(this.textBoxMaxQueries);
            this.tabPageServidorBD.Controls.Add(this.textBoxServerExtraParams);
            this.tabPageServidorBD.Controls.Add(this.labelParametros);
            this.tabPageServidorBD.Controls.Add(this.labelBD);
            this.tabPageServidorBD.Controls.Add(this.textBoxServerDB);
            this.tabPageServidorBD.Controls.Add(this.buttonProbarBD);
            this.tabPageServidorBD.Controls.Add(this.textBoxServerName);
            this.tabPageServidorBD.Controls.Add(this.labelPass);
            this.tabPageServidorBD.Controls.Add(this.textBoxServerPass);
            this.tabPageServidorBD.Controls.Add(this.labelIp);
            this.tabPageServidorBD.Controls.Add(this.labelUsuario);
            this.tabPageServidorBD.Controls.Add(this.textBoxServerPort);
            this.tabPageServidorBD.Controls.Add(this.textBoxServerUser);
            this.tabPageServidorBD.Controls.Add(this.labelPuerto);
            this.tabPageServidorBD.Location = new System.Drawing.Point(0, 0);
            this.tabPageServidorBD.Name = "tabPageServidorBD";
            this.tabPageServidorBD.Size = new System.Drawing.Size(480, 332);
            this.tabPageServidorBD.Text = "Servidor BD";
            // 
            // textBoxTimeout
            // 
            this.textBoxTimeout.Location = new System.Drawing.Point(365, 195);
            this.textBoxTimeout.MaxLength = 10;
            this.textBoxTimeout.Name = "textBoxTimeout";
            this.textBoxTimeout.Size = new System.Drawing.Size(108, 41);
            this.textBoxTimeout.TabIndex = 51;
            this.textBoxTimeout.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnlyNumbersKeyPress);
            // 
            // labelTimeout
            // 
            this.labelTimeout.Location = new System.Drawing.Point(264, 196);
            this.labelTimeout.Name = "labelTimeout";
            this.labelTimeout.Size = new System.Drawing.Size(99, 40);
            this.labelTimeout.Text = "Timeout:";
            // 
            // labelMaxQueries
            // 
            this.labelMaxQueries.Location = new System.Drawing.Point(7, 196);
            this.labelMaxQueries.Name = "labelMaxQueries";
            this.labelMaxQueries.Size = new System.Drawing.Size(145, 40);
            this.labelMaxQueries.Text = "MaxQueries:";
            // 
            // textBoxMaxQueries
            // 
            this.textBoxMaxQueries.Location = new System.Drawing.Point(158, 195);
            this.textBoxMaxQueries.MaxLength = 10;
            this.textBoxMaxQueries.Name = "textBoxMaxQueries";
            this.textBoxMaxQueries.Size = new System.Drawing.Size(100, 41);
            this.textBoxMaxQueries.TabIndex = 50;
            this.textBoxMaxQueries.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnlyNumbersKeyPress);
            // 
            // textBoxServerExtraParams
            // 
            this.textBoxServerExtraParams.Location = new System.Drawing.Point(365, 102);
            this.textBoxServerExtraParams.MaxLength = 1000;
            this.textBoxServerExtraParams.Name = "textBoxServerExtraParams";
            this.textBoxServerExtraParams.Size = new System.Drawing.Size(108, 41);
            this.textBoxServerExtraParams.TabIndex = 26;
            // 
            // labelParametros
            // 
            this.labelParametros.Location = new System.Drawing.Point(264, 102);
            this.labelParametros.Name = "labelParametros";
            this.labelParametros.Size = new System.Drawing.Size(99, 40);
            this.labelParametros.Text = "Params:";
            // 
            // labelBD
            // 
            this.labelBD.Location = new System.Drawing.Point(7, 99);
            this.labelBD.Name = "labelBD";
            this.labelBD.Size = new System.Drawing.Size(50, 40);
            this.labelBD.Text = "BD:";
            // 
            // textBoxServerDB
            // 
            this.textBoxServerDB.Location = new System.Drawing.Point(59, 98);
            this.textBoxServerDB.MaxLength = 150;
            this.textBoxServerDB.Name = "textBoxServerDB";
            this.textBoxServerDB.Size = new System.Drawing.Size(199, 41);
            this.textBoxServerDB.TabIndex = 18;
            // 
            // buttonProbarBD
            // 
            this.buttonProbarBD.Location = new System.Drawing.Point(144, 270);
            this.buttonProbarBD.Name = "buttonProbarBD";
            this.buttonProbarBD.Size = new System.Drawing.Size(196, 40);
            this.buttonProbarBD.TabIndex = 11;
            this.buttonProbarBD.Text = "Probar";
            this.buttonProbarBD.Click += new System.EventHandler(this.buttonProbarBD_Click);
            // 
            // tabPageSocket
            // 
            this.tabPageSocket.Controls.Add(this.textBoxFtpServerUser);
            this.tabPageSocket.Controls.Add(this.textBoxFtpServerPassword);
            this.tabPageSocket.Controls.Add(this.labelUsuarioFtp);
            this.tabPageSocket.Controls.Add(this.labelPassFtp);
            this.tabPageSocket.Controls.Add(this.textBoxFtpServerIp);
            this.tabPageSocket.Controls.Add(this.textBoxFtpServerPort);
            this.tabPageSocket.Controls.Add(this.labelIpArchivos);
            this.tabPageSocket.Controls.Add(this.labelFtpServerPort);
            this.tabPageSocket.Controls.Add(this.buttonPorbarArc);
            this.tabPageSocket.Location = new System.Drawing.Point(0, 0);
            this.tabPageSocket.Name = "tabPageSocket";
            this.tabPageSocket.Size = new System.Drawing.Size(472, 338);
            this.tabPageSocket.Text = "Ftp";
            // 
            // textBoxFtpServerUser
            // 
            this.textBoxFtpServerUser.Location = new System.Drawing.Point(125, 107);
            this.textBoxFtpServerUser.MaxLength = 100;
            this.textBoxFtpServerUser.Name = "textBoxFtpServerUser";
            this.textBoxFtpServerUser.Size = new System.Drawing.Size(138, 41);
            this.textBoxFtpServerUser.TabIndex = 49;
            // 
            // textBoxFtpServerPassword
            // 
            this.textBoxFtpServerPassword.Location = new System.Drawing.Point(365, 107);
            this.textBoxFtpServerPassword.MaxLength = 100;
            this.textBoxFtpServerPassword.Name = "textBoxFtpServerPassword";
            this.textBoxFtpServerPassword.PasswordChar = '*';
            this.textBoxFtpServerPassword.Size = new System.Drawing.Size(108, 41);
            this.textBoxFtpServerPassword.TabIndex = 50;
            this.textBoxFtpServerPassword.Tag = "";
            // 
            // labelUsuarioFtp
            // 
            this.labelUsuarioFtp.Location = new System.Drawing.Point(5, 109);
            this.labelUsuarioFtp.Name = "labelUsuarioFtp";
            this.labelUsuarioFtp.Size = new System.Drawing.Size(114, 40);
            this.labelUsuarioFtp.Text = "Usuario:";
            // 
            // labelPassFtp
            // 
            this.labelPassFtp.Location = new System.Drawing.Point(269, 109);
            this.labelPassFtp.Name = "labelPassFtp";
            this.labelPassFtp.Size = new System.Drawing.Size(90, 40);
            this.labelPassFtp.Text = "Pass:";
            // 
            // textBoxFtpServerIp
            // 
            this.textBoxFtpServerIp.Location = new System.Drawing.Point(55, 52);
            this.textBoxFtpServerIp.MaxLength = 100;
            this.textBoxFtpServerIp.Name = "textBoxFtpServerIp";
            this.textBoxFtpServerIp.Size = new System.Drawing.Size(208, 41);
            this.textBoxFtpServerIp.TabIndex = 43;
            // 
            // textBoxFtpServerPort
            // 
            this.textBoxFtpServerPort.Location = new System.Drawing.Point(365, 53);
            this.textBoxFtpServerPort.MaxLength = 5;
            this.textBoxFtpServerPort.Name = "textBoxFtpServerPort";
            this.textBoxFtpServerPort.Size = new System.Drawing.Size(108, 41);
            this.textBoxFtpServerPort.TabIndex = 44;
            this.textBoxFtpServerPort.Tag = "";
            this.textBoxFtpServerPort.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnlyNumbersKeyPress);
            // 
            // labelIpArchivos
            // 
            this.labelIpArchivos.Location = new System.Drawing.Point(5, 52);
            this.labelIpArchivos.Name = "labelIpArchivos";
            this.labelIpArchivos.Size = new System.Drawing.Size(44, 40);
            this.labelIpArchivos.Text = "IP:";
            // 
            // labelFtpServerPort
            // 
            this.labelFtpServerPort.Location = new System.Drawing.Point(269, 53);
            this.labelFtpServerPort.Name = "labelFtpServerPort";
            this.labelFtpServerPort.Size = new System.Drawing.Size(90, 40);
            this.labelFtpServerPort.Text = "Puerto:";
            // 
            // buttonPorbarArc
            // 
            this.buttonPorbarArc.Location = new System.Drawing.Point(149, 181);
            this.buttonPorbarArc.Name = "buttonPorbarArc";
            this.buttonPorbarArc.Size = new System.Drawing.Size(196, 40);
            this.buttonPorbarArc.TabIndex = 40;
            this.buttonPorbarArc.Text = "Probar";
            this.buttonPorbarArc.Click += new System.EventHandler(this.buttonPorbarArc_Click);
            // 
            // labelReiniciar
            // 
            this.labelReiniciar.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Italic);
            this.labelReiniciar.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.labelReiniciar.Location = new System.Drawing.Point(7, 275);
            this.labelReiniciar.Name = "labelReiniciar";
            this.labelReiniciar.Size = new System.Drawing.Size(466, 54);
            this.labelReiniciar.Text = "*Debe salir y volver a entrar en la aplicación para que estos cambios se hagan ef" +
                "ectivos.";
            // 
            // FormConfiguracion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(192F, 192F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(480, 376);
            this.ControlBox = false;
            this.Controls.Add(this.tabControlOpciones);
            this.Location = new System.Drawing.Point(0, 52);
            this.Menu = this.mainMenuConfiguracion;
            this.MinimizeBox = false;
            this.Name = "FormConfiguracion";
            this.Text = "Configuración";
            this.Load += new System.EventHandler(this.FormConfiguracion_Load);
            this.tabControlOpciones.ResumeLayout(false);
            this.tabPagePales.ResumeLayout(false);
            this.tabPageEscaner.ResumeLayout(false);
            this.tabPageGeneral.ResumeLayout(false);
            this.tabPageServidorBD.ResumeLayout(false);
            this.tabPageSocket.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem menuItemAceptar;
        private System.Windows.Forms.MenuItem menuItemCancelar;
        private System.Windows.Forms.CheckBox checkBoxConnected;
        private System.Windows.Forms.TextBox textBoxServerName;
        private System.Windows.Forms.Label labelIp;
        private System.Windows.Forms.Label labelPuerto;
        private System.Windows.Forms.TextBox textBoxServerPort;
        private System.Windows.Forms.Label labelPass;
        private System.Windows.Forms.TextBox textBoxServerPass;
        private System.Windows.Forms.Label labelUsuario;
        private System.Windows.Forms.TextBox textBoxServerUser;
        private System.Windows.Forms.LinkLabel linkLabelVaciarBD;
        private System.Windows.Forms.TabControl tabControlOpciones;
        private System.Windows.Forms.TabPage tabPageGeneral;
        private System.Windows.Forms.TabPage tabPageServidorBD;
        private System.Windows.Forms.Button buttonProbarBD;
        private System.Windows.Forms.Label labelBD;
        private System.Windows.Forms.TextBox textBoxServerDB;
        private System.Windows.Forms.TextBox textBoxServerExtraParams;
        private System.Windows.Forms.Label labelParametros;
        private System.Windows.Forms.TabPage tabPagePales;
        private System.Windows.Forms.Button buttonEliminar;
        private System.Windows.Forms.Button buttonEditar;
        private System.Windows.Forms.Button buttonAgregar;
        private System.Windows.Forms.Button buttonDescargar;
        private System.Windows.Forms.ListBox listBoxPales;
        private System.Windows.Forms.TextBox textBoxEditarPale;
        private System.Windows.Forms.TabPage tabPageSocket;
        private System.Windows.Forms.TextBox textBoxTimeout;
        private System.Windows.Forms.Label labelTimeout;
        private System.Windows.Forms.Label labelMaxQueries;
        private System.Windows.Forms.TextBox textBoxMaxQueries;
        private System.Windows.Forms.TextBox textBoxFtpServerIp;
        private System.Windows.Forms.TextBox textBoxFtpServerPort;
        private System.Windows.Forms.Label labelIpArchivos;
        private System.Windows.Forms.Label labelFtpServerPort;
        private System.Windows.Forms.Button buttonPorbarArc;
        private System.Windows.Forms.CheckBox checkBoxDeleteOldCodes;
        private System.Windows.Forms.TabPage tabPageEscaner;
        private System.Windows.Forms.TextBox textBoxScanTimeout;
        private System.Windows.Forms.Label labelScanTimeot;
        private System.Windows.Forms.CheckBox checkBoxScanContinuous;
        private System.Windows.Forms.CheckBox checkBoxSoundEnabled;
        private System.Windows.Forms.ComboBox comboBoxDispositivoLector;
        private System.Windows.Forms.Label labelDispositivoLector;
        private System.Windows.Forms.TextBox textBoxScanDataSize;
        private System.Windows.Forms.Label labelScanDataSize;
        private System.Windows.Forms.CheckBox checkBoxOnlyDatamatrix;
        private System.Windows.Forms.CheckBox checkBoxWriteLog;
        private System.Windows.Forms.TextBox textBoxFtpServerUser;
        private System.Windows.Forms.TextBox textBoxFtpServerPassword;
        private System.Windows.Forms.Label labelUsuarioFtp;
        private System.Windows.Forms.Label labelPassFtp;
        private System.Windows.Forms.CheckBox checkBoxSendByFtp;
        private System.Windows.Forms.Label labelReiniciar;
    }
}