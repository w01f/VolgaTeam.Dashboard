namespace SyncPC
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.buttonSaveSettings = new System.Windows.Forms.Button();
            this.laPath = new System.Windows.Forms.Label();
            this.laMedia = new System.Windows.Forms.Label();
            this.textBoxStationName = new System.Windows.Forms.TextBox();
            this.buttonGo = new System.Windows.Forms.Button();
            this.textBoxPath = new System.Windows.Forms.TextBox();
            this.laTitle = new System.Windows.Forms.Label();
            this.pnTop = new System.Windows.Forms.Panel();
            this.laHelp = new System.Windows.Forms.Label();
            this.pbHelp = new System.Windows.Forms.PictureBox();
            this.pbLogo = new System.Windows.Forms.PictureBox();
            this.pnOptions = new System.Windows.Forms.Panel();
            this.laOptions = new System.Windows.Forms.Label();
            this.checkEditManual = new DevExpress.XtraEditors.CheckEdit();
            this.styleController = new DevExpress.XtraEditors.StyleController();
            this.checkEditAuto = new DevExpress.XtraEditors.CheckEdit();
            this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel();
            this.pnAuto = new System.Windows.Forms.Panel();
            this.comboBoxEditSelectMedia = new DevExpress.XtraEditors.ComboBoxEdit();
            this.laSelectMedia = new System.Windows.Forms.Label();
            this.pnManual = new System.Windows.Forms.Panel();
            this.pnTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHelp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
            this.pnOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditManual.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditAuto.Properties)).BeginInit();
            this.pnAuto.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditSelectMedia.Properties)).BeginInit();
            this.pnManual.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonSaveSettings
            // 
            this.buttonSaveSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSaveSettings.Enabled = false;
            this.buttonSaveSettings.Location = new System.Drawing.Point(480, 248);
            this.buttonSaveSettings.Name = "buttonSaveSettings";
            this.buttonSaveSettings.Size = new System.Drawing.Size(104, 30);
            this.buttonSaveSettings.TabIndex = 3;
            this.buttonSaveSettings.Text = "Save Settings";
            this.buttonSaveSettings.UseVisualStyleBackColor = true;
            this.buttonSaveSettings.Click += new System.EventHandler(this.buttonSaveSettings_Click);
            // 
            // laPath
            // 
            this.laPath.AutoSize = true;
            this.laPath.Location = new System.Drawing.Point(14, 69);
            this.laPath.Name = "laPath";
            this.laPath.Size = new System.Drawing.Size(146, 16);
            this.laPath.TabIndex = 9;
            this.laPath.Text = "Network Directory Path:";
            // 
            // laMedia
            // 
            this.laMedia.AutoSize = true;
            this.laMedia.Location = new System.Drawing.Point(14, 13);
            this.laMedia.Name = "laMedia";
            this.laMedia.Size = new System.Drawing.Size(100, 16);
            this.laMedia.TabIndex = 8;
            this.laMedia.Text = "Media Property:";
            // 
            // textBoxStationName
            // 
            this.textBoxStationName.Location = new System.Drawing.Point(14, 37);
            this.textBoxStationName.Name = "textBoxStationName";
            this.textBoxStationName.Size = new System.Drawing.Size(567, 22);
            this.textBoxStationName.TabIndex = 7;
            this.textBoxStationName.TextChanged += new System.EventHandler(this.textBoxStationName_TextChanged);
            // 
            // buttonGo
            // 
            this.buttonGo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonGo.Location = new System.Drawing.Point(14, 72);
            this.buttonGo.Name = "buttonGo";
            this.buttonGo.Size = new System.Drawing.Size(204, 30);
            this.buttonGo.TabIndex = 6;
            this.buttonGo.Text = "What is my Network Path?";
            this.buttonGo.UseVisualStyleBackColor = true;
            this.buttonGo.Click += new System.EventHandler(this.buttonGo_Click);
            // 
            // textBoxPath
            // 
            this.textBoxPath.Location = new System.Drawing.Point(14, 93);
            this.textBoxPath.Name = "textBoxPath";
            this.textBoxPath.Size = new System.Drawing.Size(567, 22);
            this.textBoxPath.TabIndex = 5;
            this.textBoxPath.TextChanged += new System.EventHandler(this.textBoxStationName_TextChanged);
            // 
            // laTitle
            // 
            this.laTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.laTitle.Location = new System.Drawing.Point(155, 10);
            this.laTitle.Name = "laTitle";
            this.laTitle.Size = new System.Drawing.Size(265, 59);
            this.laTitle.TabIndex = 5;
            this.laTitle.Text = "A Web Connection is Required\r\nto set up your software...";
            this.laTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnTop
            // 
            this.pnTop.Controls.Add(this.laHelp);
            this.pnTop.Controls.Add(this.pbHelp);
            this.pnTop.Controls.Add(this.pbLogo);
            this.pnTop.Controls.Add(this.laTitle);
            this.pnTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnTop.Location = new System.Drawing.Point(0, 0);
            this.pnTop.Name = "pnTop";
            this.pnTop.Size = new System.Drawing.Size(596, 79);
            this.pnTop.TabIndex = 6;
            // 
            // laHelp
            // 
            this.laHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.laHelp.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laHelp.ForeColor = System.Drawing.Color.Black;
            this.laHelp.Location = new System.Drawing.Point(409, 27);
            this.laHelp.Name = "laHelp";
            this.laHelp.Size = new System.Drawing.Size(110, 24);
            this.laHelp.TabIndex = 7;
            this.laHelp.Text = "Help";
            this.laHelp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pbHelp
            // 
            this.pbHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbHelp.Image = global::SyncPC.Properties.Resources.Help;
            this.pbHelp.Location = new System.Drawing.Point(525, 8);
            this.pbHelp.Name = "pbHelp";
            this.pbHelp.Size = new System.Drawing.Size(64, 63);
            this.pbHelp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbHelp.TabIndex = 6;
            this.pbHelp.TabStop = false;
            this.pbHelp.Click += new System.EventHandler(this.pbHelp_Click);
            this.pbHelp.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
            this.pbHelp.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseUp);
            // 
            // pbLogo
            // 
            this.pbLogo.Image = global::SyncPC.Properties.Resources.Logo;
            this.pbLogo.Location = new System.Drawing.Point(3, 10);
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.Size = new System.Drawing.Size(146, 59);
            this.pbLogo.TabIndex = 0;
            this.pbLogo.TabStop = false;
            // 
            // pnOptions
            // 
            this.pnOptions.Controls.Add(this.laOptions);
            this.pnOptions.Controls.Add(this.checkEditManual);
            this.pnOptions.Controls.Add(this.checkEditAuto);
            this.pnOptions.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnOptions.Location = new System.Drawing.Point(0, 79);
            this.pnOptions.Name = "pnOptions";
            this.pnOptions.Size = new System.Drawing.Size(596, 28);
            this.pnOptions.TabIndex = 7;
            // 
            // laOptions
            // 
            this.laOptions.AutoSize = true;
            this.laOptions.Location = new System.Drawing.Point(276, 6);
            this.laOptions.Name = "laOptions";
            this.laOptions.Size = new System.Drawing.Size(35, 16);
            this.laOptions.TabIndex = 8;
            this.laOptions.Text = "- or -";
            // 
            // checkEditManual
            // 
            this.checkEditManual.Location = new System.Drawing.Point(355, 3);
            this.checkEditManual.Name = "checkEditManual";
            this.checkEditManual.Properties.Caption = "Manually enter your settings";
            this.checkEditManual.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
            this.checkEditManual.Properties.RadioGroupIndex = 0;
            this.checkEditManual.Size = new System.Drawing.Size(226, 21);
            this.checkEditManual.StyleController = this.styleController;
            this.checkEditManual.TabIndex = 1;
            this.checkEditManual.TabStop = false;
            this.checkEditManual.CheckedChanged += new System.EventHandler(this.checkEdit_CheckedChanged);
            // 
            // styleController
            // 
            this.styleController.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.styleController.Appearance.Options.UseFont = true;
            this.styleController.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
            this.styleController.AppearanceDisabled.Options.UseFont = true;
            this.styleController.AppearanceDropDown.Font = new System.Drawing.Font("Arial", 9.75F);
            this.styleController.AppearanceDropDown.Options.UseFont = true;
            this.styleController.AppearanceDropDownHeader.Font = new System.Drawing.Font("Arial", 9.75F);
            this.styleController.AppearanceDropDownHeader.Options.UseFont = true;
            this.styleController.AppearanceFocused.Font = new System.Drawing.Font("Arial", 9.75F);
            this.styleController.AppearanceFocused.Options.UseFont = true;
            this.styleController.AppearanceReadOnly.Font = new System.Drawing.Font("Arial", 9.75F);
            this.styleController.AppearanceReadOnly.Options.UseFont = true;
            // 
            // checkEditAuto
            // 
            this.checkEditAuto.EditValue = true;
            this.checkEditAuto.Location = new System.Drawing.Point(12, 4);
            this.checkEditAuto.Name = "checkEditAuto";
            this.checkEditAuto.Properties.Caption = "Import Settings From the Internet";
            this.checkEditAuto.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
            this.checkEditAuto.Properties.RadioGroupIndex = 0;
            this.checkEditAuto.Size = new System.Drawing.Size(262, 21);
            this.checkEditAuto.StyleController = this.styleController;
            this.checkEditAuto.TabIndex = 0;
            this.checkEditAuto.CheckedChanged += new System.EventHandler(this.checkEdit_CheckedChanged);
            // 
            // defaultLookAndFeel
            // 
            this.defaultLookAndFeel.LookAndFeel.SkinName = "Money Twins";
            // 
            // pnAuto
            // 
            this.pnAuto.Controls.Add(this.comboBoxEditSelectMedia);
            this.pnAuto.Controls.Add(this.laSelectMedia);
            this.pnAuto.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnAuto.Location = new System.Drawing.Point(0, 107);
            this.pnAuto.Name = "pnAuto";
            this.pnAuto.Size = new System.Drawing.Size(596, 69);
            this.pnAuto.TabIndex = 8;
            // 
            // comboBoxEditSelectMedia
            // 
            this.comboBoxEditSelectMedia.Location = new System.Drawing.Point(14, 35);
            this.comboBoxEditSelectMedia.Name = "comboBoxEditSelectMedia";
            this.comboBoxEditSelectMedia.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEditSelectMedia.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.comboBoxEditSelectMedia.Size = new System.Drawing.Size(326, 22);
            this.comboBoxEditSelectMedia.StyleController = this.styleController;
            this.comboBoxEditSelectMedia.TabIndex = 10;
            this.comboBoxEditSelectMedia.SelectedIndexChanged += new System.EventHandler(this.comboBoxEditSelectMedia_SelectedIndexChanged);
            // 
            // laSelectMedia
            // 
            this.laSelectMedia.AutoSize = true;
            this.laSelectMedia.Location = new System.Drawing.Point(14, 11);
            this.laSelectMedia.Name = "laSelectMedia";
            this.laSelectMedia.Size = new System.Drawing.Size(167, 16);
            this.laSelectMedia.TabIndex = 9;
            this.laSelectMedia.Text = "Select Your Media Property";
            // 
            // pnManual
            // 
            this.pnManual.Controls.Add(this.buttonGo);
            this.pnManual.Controls.Add(this.laPath);
            this.pnManual.Controls.Add(this.textBoxPath);
            this.pnManual.Controls.Add(this.laMedia);
            this.pnManual.Controls.Add(this.textBoxStationName);
            this.pnManual.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnManual.Location = new System.Drawing.Point(0, 176);
            this.pnManual.Name = "pnManual";
            this.pnManual.Size = new System.Drawing.Size(596, 114);
            this.pnManual.TabIndex = 9;
            this.pnManual.Visible = false;
            // 
            // FormMain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(596, 290);
            this.Controls.Add(this.buttonSaveSettings);
            this.Controls.Add(this.pnManual);
            this.Controls.Add(this.pnAuto);
            this.Controls.Add(this.pnOptions);
            this.Controls.Add(this.pnTop);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " Software Setup";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.pnTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbHelp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
            this.pnOptions.ResumeLayout(false);
            this.pnOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditManual.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditAuto.Properties)).EndInit();
            this.pnAuto.ResumeLayout(false);
            this.pnAuto.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditSelectMedia.Properties)).EndInit();
            this.pnManual.ResumeLayout(false);
            this.pnManual.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonSaveSettings;
       private System.Windows.Forms.Button buttonGo;
       private System.Windows.Forms.TextBox textBoxPath;
       private System.Windows.Forms.Label laTitle;
       private System.Windows.Forms.Label laPath;
       private System.Windows.Forms.Label laMedia;
       private System.Windows.Forms.TextBox textBoxStationName;
       private System.Windows.Forms.Panel pnTop;
       private System.Windows.Forms.PictureBox pbLogo;
       private System.Windows.Forms.Panel pnOptions;
       private DevExpress.XtraEditors.CheckEdit checkEditAuto;
       private DevExpress.XtraEditors.StyleController styleController;
       private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
       private System.Windows.Forms.Label laOptions;
       private DevExpress.XtraEditors.CheckEdit checkEditManual;
       private System.Windows.Forms.Panel pnAuto;
       private DevExpress.XtraEditors.ComboBoxEdit comboBoxEditSelectMedia;
       private System.Windows.Forms.Label laSelectMedia;
       private System.Windows.Forms.Panel pnManual;
       private System.Windows.Forms.PictureBox pbHelp;
       private System.Windows.Forms.Label laHelp;

    }
}

