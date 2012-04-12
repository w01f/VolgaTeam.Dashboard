namespace OnlineScheduleBuilder.CustomControls
{
    partial class WebPackageControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pnHeader = new System.Windows.Forms.Panel();
            this.comboBoxEditSlideHeader = new DevExpress.XtraEditors.ComboBoxEdit();
            this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
            this.labelControlAdvertiser = new DevExpress.XtraEditors.LabelControl();
            this.labelControlPresentationDate = new DevExpress.XtraEditors.LabelControl();
            this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            this.panelExHeader = new DevComponents.DotNetBar.PanelEx();
            this.pictureBoxFormula = new System.Windows.Forms.PictureBox();
            this.checkEditFormulaImpressions = new DevExpress.XtraEditors.CheckEdit();
            this.checkEditFormulaInvestment = new DevExpress.XtraEditors.CheckEdit();
            this.checkEditFormulaCPM = new DevExpress.XtraEditors.CheckEdit();
            this.labelControlFormula = new DevExpress.XtraEditors.LabelControl();
            this.labelControlOutputStatus = new DevExpress.XtraEditors.LabelControl();
            this.pnMain = new System.Windows.Forms.Panel();
            this.pnHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditSlideHeader.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
            this.panelExHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFormula)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditFormulaImpressions.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditFormulaInvestment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditFormulaCPM.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnHeader
            // 
            this.pnHeader.Controls.Add(this.comboBoxEditSlideHeader);
            this.pnHeader.Controls.Add(this.labelControlAdvertiser);
            this.pnHeader.Controls.Add(this.labelControlPresentationDate);
            this.pnHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnHeader.Location = new System.Drawing.Point(0, 0);
            this.pnHeader.Name = "pnHeader";
            this.pnHeader.Size = new System.Drawing.Size(1000, 67);
            this.pnHeader.TabIndex = 100;
            // 
            // comboBoxEditSlideHeader
            // 
            this.comboBoxEditSlideHeader.Location = new System.Drawing.Point(3, 5);
            this.comboBoxEditSlideHeader.Name = "comboBoxEditSlideHeader";
            this.comboBoxEditSlideHeader.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEditSlideHeader.Properties.NullText = "Your Online Campaign";
            this.comboBoxEditSlideHeader.Size = new System.Drawing.Size(203, 22);
            this.comboBoxEditSlideHeader.StyleController = this.styleController;
            this.comboBoxEditSlideHeader.TabIndex = 100;
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
            // labelControlAdvertiser
            // 
            this.labelControlAdvertiser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControlAdvertiser.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControlAdvertiser.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControlAdvertiser.Location = new System.Drawing.Point(663, 3);
            this.labelControlAdvertiser.Name = "labelControlAdvertiser";
            this.labelControlAdvertiser.Size = new System.Drawing.Size(323, 55);
            this.labelControlAdvertiser.StyleController = this.styleController;
            this.labelControlAdvertiser.TabIndex = 6;
            this.labelControlAdvertiser.Text = "Advertiser\r\n\r\nDecision Maker";
            // 
            // labelControlPresentationDate
            // 
            this.labelControlPresentationDate.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControlPresentationDate.Location = new System.Drawing.Point(5, 35);
            this.labelControlPresentationDate.Name = "labelControlPresentationDate";
            this.labelControlPresentationDate.Size = new System.Drawing.Size(201, 23);
            this.labelControlPresentationDate.StyleController = this.styleController;
            this.labelControlPresentationDate.TabIndex = 5;
            this.labelControlPresentationDate.Text = "Presentation Date";
            // 
            // defaultLookAndFeel
            // 
            this.defaultLookAndFeel.LookAndFeel.SkinName = "Money Twins";
            // 
            // panelExHeader
            // 
            this.panelExHeader.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelExHeader.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.panelExHeader.Controls.Add(this.pictureBoxFormula);
            this.panelExHeader.Controls.Add(this.checkEditFormulaImpressions);
            this.panelExHeader.Controls.Add(this.checkEditFormulaInvestment);
            this.panelExHeader.Controls.Add(this.checkEditFormulaCPM);
            this.panelExHeader.Controls.Add(this.labelControlFormula);
            this.panelExHeader.Controls.Add(this.labelControlOutputStatus);
            this.panelExHeader.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelExHeader.Location = new System.Drawing.Point(0, 394);
            this.panelExHeader.Name = "panelExHeader";
            this.panelExHeader.Size = new System.Drawing.Size(1000, 36);
            this.panelExHeader.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelExHeader.Style.BackColor1.Color = System.Drawing.Color.AliceBlue;
            this.panelExHeader.Style.BackColor2.Color = System.Drawing.Color.AliceBlue;
            this.panelExHeader.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelExHeader.Style.BorderColor.Color = System.Drawing.Color.White;
            this.panelExHeader.Style.BorderSide = DevComponents.DotNetBar.eBorderSide.Top;
            this.panelExHeader.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelExHeader.Style.GradientAngle = 90;
            this.panelExHeader.TabIndex = 4;
            // 
            // pictureBoxFormula
            // 
            this.pictureBoxFormula.Image = global::OnlineScheduleBuilder.Properties.Resources.InvestmentLogoGray;
            this.pictureBoxFormula.Location = new System.Drawing.Point(31, 2);
            this.pictureBoxFormula.Name = "pictureBoxFormula";
            this.pictureBoxFormula.Size = new System.Drawing.Size(29, 32);
            this.pictureBoxFormula.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxFormula.TabIndex = 16;
            this.pictureBoxFormula.TabStop = false;
            // 
            // checkEditFormulaImpressions
            // 
            this.checkEditFormulaImpressions.Location = new System.Drawing.Point(274, 7);
            this.checkEditFormulaImpressions.Name = "checkEditFormulaImpressions";
            this.checkEditFormulaImpressions.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkEditFormulaImpressions.Properties.Appearance.Options.UseFont = true;
            this.checkEditFormulaImpressions.Properties.AutoWidth = true;
            this.checkEditFormulaImpressions.Properties.Caption = "Impressions";
            this.checkEditFormulaImpressions.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
            this.checkEditFormulaImpressions.Size = new System.Drawing.Size(98, 21);
            this.checkEditFormulaImpressions.StyleController = this.styleController;
            this.checkEditFormulaImpressions.TabIndex = 15;
            this.checkEditFormulaImpressions.CheckedChanged += new System.EventHandler(this.checkEditFormula_CheckedChanged);
            // 
            // checkEditFormulaInvestment
            // 
            this.checkEditFormulaInvestment.Location = new System.Drawing.Point(176, 7);
            this.checkEditFormulaInvestment.Name = "checkEditFormulaInvestment";
            this.checkEditFormulaInvestment.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkEditFormulaInvestment.Properties.Appearance.Options.UseFont = true;
            this.checkEditFormulaInvestment.Properties.AutoWidth = true;
            this.checkEditFormulaInvestment.Properties.Caption = "Investment";
            this.checkEditFormulaInvestment.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
            this.checkEditFormulaInvestment.Size = new System.Drawing.Size(92, 21);
            this.checkEditFormulaInvestment.StyleController = this.styleController;
            this.checkEditFormulaInvestment.TabIndex = 14;
            this.checkEditFormulaInvestment.CheckedChanged += new System.EventHandler(this.checkEditFormula_CheckedChanged);
            // 
            // checkEditFormulaCPM
            // 
            this.checkEditFormulaCPM.Location = new System.Drawing.Point(118, 7);
            this.checkEditFormulaCPM.Name = "checkEditFormulaCPM";
            this.checkEditFormulaCPM.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkEditFormulaCPM.Properties.Appearance.Options.UseFont = true;
            this.checkEditFormulaCPM.Properties.AutoWidth = true;
            this.checkEditFormulaCPM.Properties.Caption = "CPM";
            this.checkEditFormulaCPM.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
            this.checkEditFormulaCPM.Size = new System.Drawing.Size(52, 21);
            this.checkEditFormulaCPM.StyleController = this.styleController;
            this.checkEditFormulaCPM.TabIndex = 13;
            this.checkEditFormulaCPM.CheckedChanged += new System.EventHandler(this.checkEditFormula_CheckedChanged);
            // 
            // labelControlFormula
            // 
            this.labelControlFormula.Appearance.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelControlFormula.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControlFormula.Location = new System.Drawing.Point(66, 0);
            this.labelControlFormula.Name = "labelControlFormula";
            this.labelControlFormula.Size = new System.Drawing.Size(58, 36);
            this.labelControlFormula.StyleController = this.styleController;
            this.labelControlFormula.TabIndex = 12;
            this.labelControlFormula.Text = "Web\r\nFormula:";
            // 
            // labelControlOutputStatus
            // 
            this.labelControlOutputStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControlOutputStatus.Appearance.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelControlOutputStatus.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControlOutputStatus.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControlOutputStatus.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControlOutputStatus.Location = new System.Drawing.Point(466, 7);
            this.labelControlOutputStatus.Name = "labelControlOutputStatus";
            this.labelControlOutputStatus.Size = new System.Drawing.Size(520, 23);
            this.labelControlOutputStatus.StyleController = this.styleController;
            this.labelControlOutputStatus.TabIndex = 6;
            // 
            // pnMain
            // 
            this.pnMain.BackColor = System.Drawing.Color.AliceBlue;
            this.pnMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnMain.Location = new System.Drawing.Point(0, 67);
            this.pnMain.Name = "pnMain";
            this.pnMain.Size = new System.Drawing.Size(1000, 327);
            this.pnMain.TabIndex = 5;
            // 
            // WebPackageControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.pnMain);
            this.Controls.Add(this.panelExHeader);
            this.Controls.Add(this.pnHeader);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Name = "WebPackageControl";
            this.Size = new System.Drawing.Size(1000, 430);
            this.Load += new System.EventHandler(this.WebPackageControl_Load);
            this.pnHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditSlideHeader.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
            this.panelExHeader.ResumeLayout(false);
            this.panelExHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFormula)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditFormulaImpressions.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditFormulaInvestment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditFormulaCPM.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnHeader;
        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
        private DevComponents.DotNetBar.PanelEx panelExHeader;
        private DevExpress.XtraEditors.StyleController styleController;
        public DevExpress.XtraEditors.LabelControl labelControlPresentationDate;
        public DevExpress.XtraEditors.LabelControl labelControlAdvertiser;
        public DevExpress.XtraEditors.LabelControl labelControlOutputStatus;
        private System.Windows.Forms.Panel pnMain;
        public DevExpress.XtraEditors.ComboBoxEdit comboBoxEditSlideHeader;
        public System.Windows.Forms.PictureBox pictureBoxFormula;
        public DevExpress.XtraEditors.CheckEdit checkEditFormulaImpressions;
        public DevExpress.XtraEditors.CheckEdit checkEditFormulaInvestment;
        public DevExpress.XtraEditors.CheckEdit checkEditFormulaCPM;
        public DevExpress.XtraEditors.LabelControl labelControlFormula;

    }
}
