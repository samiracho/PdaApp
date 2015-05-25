namespace FriosillaBarCode
{
    partial class FormPrincipal
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenuPrincipal;

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
            this.mainMenuPrincipal = new System.Windows.Forms.MainMenu();
            this.menuItemopciones = new System.Windows.Forms.MenuItem();
            this.menuItemSalir = new System.Windows.Forms.MenuItem();
            this.comboBoxPales = new System.Windows.Forms.ComboBox();
            this.labelCola = new System.Windows.Forms.Label();
            this.labelColaDes = new System.Windows.Forms.Label();
            this.buttonEnviar = new System.Windows.Forms.Button();
            this.labelReaderStatus = new System.Windows.Forms.Label();
            this.labelEst = new System.Windows.Forms.Label();
            this.labelCodPen = new System.Windows.Forms.Label();
            this.labelCodPenDes = new System.Windows.Forms.Label();
            this.labelLecturaDes = new System.Windows.Forms.Label();
            this.labelLectura = new System.Windows.Forms.Label();
            this.buttonRecargarPales = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // mainMenuPrincipal
            // 
            this.mainMenuPrincipal.MenuItems.Add(this.menuItemopciones);
            this.mainMenuPrincipal.MenuItems.Add(this.menuItemSalir);
            // 
            // menuItemopciones
            // 
            this.menuItemopciones.Text = " Opciones";
            this.menuItemopciones.Click += new System.EventHandler(this.menuItemOpciones_Click);
            // 
            // menuItemSalir
            // 
            this.menuItemSalir.Text = "Salir";
            this.menuItemSalir.Click += new System.EventHandler(this.menuItemSalir_Click);
            // 
            // comboBoxPales
            // 
            this.comboBoxPales.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular);
            this.comboBoxPales.Location = new System.Drawing.Point(95, 13);
            this.comboBoxPales.Name = "comboBoxPales";
            this.comboBoxPales.Size = new System.Drawing.Size(370, 70);
            this.comboBoxPales.TabIndex = 3;
            this.comboBoxPales.TextChanged += new System.EventHandler(this.comboBoxPales_TextChanged);
            // 
            // labelCola
            // 
            this.labelCola.Location = new System.Drawing.Point(265, 234);
            this.labelCola.Name = "labelCola";
            this.labelCola.Size = new System.Drawing.Size(200, 33);
            this.labelCola.Text = "0";
            this.labelCola.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelColaDes
            // 
            this.labelColaDes.Location = new System.Drawing.Point(15, 234);
            this.labelColaDes.Name = "labelColaDes";
            this.labelColaDes.Size = new System.Drawing.Size(228, 33);
            this.labelColaDes.Text = "Códigos guardados:";
            // 
            // buttonEnviar
            // 
            this.buttonEnviar.Enabled = false;
            this.buttonEnviar.Font = new System.Drawing.Font("Tahoma", 20F, System.Drawing.FontStyle.Bold);
            this.buttonEnviar.Location = new System.Drawing.Point(15, 101);
            this.buttonEnviar.Name = "buttonEnviar";
            this.buttonEnviar.Size = new System.Drawing.Size(450, 121);
            this.buttonEnviar.TabIndex = 5;
            this.buttonEnviar.Text = "Enviar";
            this.buttonEnviar.Click += new System.EventHandler(this.buttonEnviar_Click);
            // 
            // labelReaderStatus
            // 
            this.labelReaderStatus.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Italic);
            this.labelReaderStatus.Location = new System.Drawing.Point(182, 296);
            this.labelReaderStatus.Name = "labelReaderStatus";
            this.labelReaderStatus.Size = new System.Drawing.Size(283, 29);
            this.labelReaderStatus.Text = "Listo";
            this.labelReaderStatus.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelEst
            // 
            this.labelEst.Location = new System.Drawing.Point(15, 296);
            this.labelEst.Name = "labelEst";
            this.labelEst.Size = new System.Drawing.Size(161, 29);
            this.labelEst.Text = "Estado lector:";
            // 
            // labelCodPen
            // 
            this.labelCodPen.Location = new System.Drawing.Point(309, 266);
            this.labelCodPen.Name = "labelCodPen";
            this.labelCodPen.Size = new System.Drawing.Size(156, 29);
            this.labelCodPen.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelCodPenDes
            // 
            this.labelCodPenDes.Location = new System.Drawing.Point(15, 266);
            this.labelCodPenDes.Name = "labelCodPenDes";
            this.labelCodPenDes.Size = new System.Drawing.Size(288, 29);
            this.labelCodPenDes.Text = "Cajas pendientes palé:";
            // 
            // labelLecturaDes
            // 
            this.labelLecturaDes.Location = new System.Drawing.Point(15, 330);
            this.labelLecturaDes.Name = "labelLecturaDes";
            this.labelLecturaDes.Size = new System.Drawing.Size(101, 29);
            this.labelLecturaDes.Text = "Lectura:";
            // 
            // labelLectura
            // 
            this.labelLectura.BackColor = System.Drawing.Color.White;
            this.labelLectura.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.labelLectura.ForeColor = System.Drawing.Color.Green;
            this.labelLectura.Location = new System.Drawing.Point(122, 330);
            this.labelLectura.Name = "labelLectura";
            this.labelLectura.Size = new System.Drawing.Size(343, 29);
            this.labelLectura.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // buttonRecargarPales
            // 
            this.buttonRecargarPales.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.buttonRecargarPales.Location = new System.Drawing.Point(15, 13);
            this.buttonRecargarPales.Name = "buttonRecargarPales";
            this.buttonRecargarPales.Size = new System.Drawing.Size(74, 66);
            this.buttonRecargarPales.TabIndex = 20;
            this.buttonRecargarPales.Text = "®";
            this.buttonRecargarPales.Click += new System.EventHandler(this.buttonRecargarPales_Click);
            // 
            // FormPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(192F, 192F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(480, 376);
            this.ControlBox = false;
            this.Controls.Add(this.buttonRecargarPales);
            this.Controls.Add(this.labelLecturaDes);
            this.Controls.Add(this.labelLectura);
            this.Controls.Add(this.labelCodPen);
            this.Controls.Add(this.labelCodPenDes);
            this.Controls.Add(this.labelEst);
            this.Controls.Add(this.labelReaderStatus);
            this.Controls.Add(this.buttonEnviar);
            this.Controls.Add(this.labelCola);
            this.Controls.Add(this.labelColaDes);
            this.Controls.Add(this.comboBoxPales);
            this.Location = new System.Drawing.Point(0, 52);
            this.Menu = this.mainMenuPrincipal;
            this.MinimizeBox = false;
            this.Name = "FormPrincipal";
            this.Text = "Friosilla BarCodes";
            this.Activated += new System.EventHandler(this.FormPrincipal_Activated);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxPales;
        private System.Windows.Forms.MenuItem menuItemopciones;
        private System.Windows.Forms.Label labelCola;
        private System.Windows.Forms.Label labelColaDes;
        private System.Windows.Forms.Button buttonEnviar;
        private System.Windows.Forms.Label labelReaderStatus;
        private System.Windows.Forms.MenuItem menuItemSalir;
        private System.Windows.Forms.Label labelEst;
        private System.Windows.Forms.Label labelCodPen;
        private System.Windows.Forms.Label labelCodPenDes;
        private System.Windows.Forms.Label labelLecturaDes;
        private System.Windows.Forms.Label labelLectura;
        private System.Windows.Forms.Button buttonRecargarPales;
    }
}

