namespace NewBizWiz.AdSchedule.Controls.PresentationClasses.OutputClasses.OutputControls.Calendar.SettingsViewers
{
    partial class DigitalViewerControl
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
			this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
			this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
			this.pnControls = new System.Windows.Forms.Panel();
			this.memoEditInfo = new DevExpress.XtraEditors.MemoEdit();
			this.labelControlWarning = new DevExpress.XtraEditors.LabelControl();
			this.pnButtons = new System.Windows.Forms.Panel();
			this.checkEditAllowEdit = new DevExpress.XtraEditors.CheckEdit();
			this.checkEditEnable = new DevExpress.XtraEditors.CheckEdit();
			this.superTooltip = new DevComponents.DotNetBar.SuperTooltip();
			this.buttonXShowImpressions = new DevComponents.DotNetBar.ButtonX();
			this.buttonXShowWebsites = new DevComponents.DotNetBar.ButtonX();
			this.buttonXShowDates = new DevComponents.DotNetBar.ButtonX();
			this.buttonXShowCPM = new DevComponents.DotNetBar.ButtonX();
			this.buttonXShowProduct = new DevComponents.DotNetBar.ButtonX();
			this.buttonXShowDimensions = new DevComponents.DotNetBar.ButtonX();
			this.buttonXShowInvestment = new DevComponents.DotNetBar.ButtonX();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			this.pnControls.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.memoEditInfo.Properties)).BeginInit();
			this.pnButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.checkEditAllowEdit.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditEnable.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// styleController
			// 
			this.styleController.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.styleController.Appearance.Options.UseFont = true;
			this.styleController.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.styleController.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
			this.styleController.AppearanceDisabled.Options.UseFont = true;
			this.styleController.AppearanceDisabled.Options.UseForeColor = true;
			this.styleController.AppearanceDropDown.Font = new System.Drawing.Font("Arial", 9.75F);
			this.styleController.AppearanceDropDown.Options.UseFont = true;
			this.styleController.AppearanceDropDownHeader.Font = new System.Drawing.Font("Arial", 9.75F);
			this.styleController.AppearanceDropDownHeader.Options.UseFont = true;
			this.styleController.AppearanceFocused.Font = new System.Drawing.Font("Arial", 9.75F);
			this.styleController.AppearanceFocused.Options.UseFont = true;
			this.styleController.AppearanceReadOnly.Font = new System.Drawing.Font("Arial", 9.75F);
			this.styleController.AppearanceReadOnly.ForeColor = System.Drawing.Color.Black;
			this.styleController.AppearanceReadOnly.Options.UseFont = true;
			this.styleController.AppearanceReadOnly.Options.UseForeColor = true;
			// 
			// defaultLookAndFeel
			// 
			this.defaultLookAndFeel.LookAndFeel.SkinName = "Money Twins";
			// 
			// pnControls
			// 
			this.pnControls.BackColor = System.Drawing.Color.AliceBlue;
			this.pnControls.Controls.Add(this.memoEditInfo);
			this.pnControls.Controls.Add(this.labelControlWarning);
			this.pnControls.Controls.Add(this.pnButtons);
			this.pnControls.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnControls.Enabled = false;
			this.pnControls.Location = new System.Drawing.Point(0, 34);
			this.pnControls.Name = "pnControls";
			this.pnControls.Size = new System.Drawing.Size(481, 404);
			this.pnControls.TabIndex = 104;
			// 
			// memoEditInfo
			// 
			this.memoEditInfo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.memoEditInfo.Location = new System.Drawing.Point(0, 112);
			this.memoEditInfo.Name = "memoEditInfo";
			this.memoEditInfo.Properties.ReadOnly = true;
			this.memoEditInfo.Size = new System.Drawing.Size(481, 234);
			this.memoEditInfo.StyleController = this.styleController;
			this.memoEditInfo.TabIndex = 4;
			// 
			// labelControlWarning
			// 
			this.labelControlWarning.AllowHtmlString = true;
			this.labelControlWarning.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.labelControlWarning.Appearance.ForeColor = System.Drawing.Color.Red;
			this.labelControlWarning.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.labelControlWarning.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.labelControlWarning.Location = new System.Drawing.Point(0, 346);
			this.labelControlWarning.Name = "labelControlWarning";
			this.labelControlWarning.Size = new System.Drawing.Size(481, 58);
			this.labelControlWarning.TabIndex = 105;
			this.labelControlWarning.Text = "<size=12><b>*TEXT-SIZE WARNING:</b></size>\r\nYou are adding a LOT of Text to this " +
    "slide. After the slide is generated, you MAY NEED to reduce the text size…";
			this.labelControlWarning.Visible = false;
			// 
			// pnButtons
			// 
			this.pnButtons.Controls.Add(this.buttonXShowImpressions);
			this.pnButtons.Controls.Add(this.buttonXShowWebsites);
			this.pnButtons.Controls.Add(this.buttonXShowDates);
			this.pnButtons.Controls.Add(this.buttonXShowCPM);
			this.pnButtons.Controls.Add(this.buttonXShowProduct);
			this.pnButtons.Controls.Add(this.buttonXShowDimensions);
			this.pnButtons.Controls.Add(this.buttonXShowInvestment);
			this.pnButtons.Controls.Add(this.checkEditAllowEdit);
			this.pnButtons.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnButtons.Location = new System.Drawing.Point(0, 0);
			this.pnButtons.Name = "pnButtons";
			this.pnButtons.Size = new System.Drawing.Size(481, 112);
			this.pnButtons.TabIndex = 103;
			// 
			// checkEditAllowEdit
			// 
			this.checkEditAllowEdit.Dock = System.Windows.Forms.DockStyle.Top;
			this.checkEditAllowEdit.Location = new System.Drawing.Point(0, 0);
			this.checkEditAllowEdit.Name = "checkEditAllowEdit";
			this.checkEditAllowEdit.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.checkEditAllowEdit.Properties.Appearance.Options.UseFont = true;
			this.checkEditAllowEdit.Properties.Appearance.Options.UseTextOptions = true;
			this.checkEditAllowEdit.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.checkEditAllowEdit.Properties.AutoHeight = false;
			this.checkEditAllowEdit.Properties.Caption = "Manual Edit Digital Product Info ";
			this.checkEditAllowEdit.Size = new System.Drawing.Size(481, 44);
			this.checkEditAllowEdit.TabIndex = 102;
			this.checkEditAllowEdit.CheckedChanged += new System.EventHandler(this.checkEditAllowEdit_CheckedChanged);
			// 
			// checkEditEnable
			// 
			this.checkEditEnable.Dock = System.Windows.Forms.DockStyle.Top;
			this.checkEditEnable.Location = new System.Drawing.Point(0, 0);
			this.checkEditEnable.Name = "checkEditEnable";
			this.checkEditEnable.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.checkEditEnable.Properties.Appearance.Options.UseFont = true;
			this.checkEditEnable.Properties.Appearance.Options.UseTextOptions = true;
			this.checkEditEnable.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.checkEditEnable.Properties.AutoHeight = false;
			this.checkEditEnable.Properties.Caption = "Show Digital Product Info on this PowerPoint Slide:";
			this.checkEditEnable.Size = new System.Drawing.Size(481, 34);
			this.checkEditEnable.TabIndex = 105;
			this.checkEditEnable.CheckedChanged += new System.EventHandler(this.checkEditEnable_CheckedChanged);
			// 
			// superTooltip
			// 
			this.superTooltip.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			// 
			// buttonXShowImpressions
			// 
			this.buttonXShowImpressions.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXShowImpressions.AutoCheckOnClick = true;
			this.buttonXShowImpressions.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXShowImpressions.Image = global::NewBizWiz.AdSchedule.Controls.Properties.Resources.DigitalInfoImpressions;
			this.buttonXShowImpressions.Location = new System.Drawing.Point(275, 50);
			this.buttonXShowImpressions.Name = "buttonXShowImpressions";
			this.buttonXShowImpressions.Size = new System.Drawing.Size(54, 54);
			this.superTooltip.SetSuperTooltip(this.buttonXShowImpressions, new DevComponents.DotNetBar.SuperTooltipInfo("", "", "Impressions", null, null, DevComponents.DotNetBar.eTooltipColor.Gray, false, false, new System.Drawing.Size(70, 18)));
			this.buttonXShowImpressions.TabIndex = 107;
			this.buttonXShowImpressions.TextColor = System.Drawing.Color.Black;
			this.buttonXShowImpressions.CheckedChanged += new System.EventHandler(this.buttonXShow_CheckedChanged);
			// 
			// buttonXShowWebsites
			// 
			this.buttonXShowWebsites.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXShowWebsites.AutoCheckOnClick = true;
			this.buttonXShowWebsites.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXShowWebsites.Image = global::NewBizWiz.AdSchedule.Controls.Properties.Resources.DigitalInfoWebsites;
			this.buttonXShowWebsites.Location = new System.Drawing.Point(3, 50);
			this.buttonXShowWebsites.Name = "buttonXShowWebsites";
			this.buttonXShowWebsites.Size = new System.Drawing.Size(54, 54);
			this.superTooltip.SetSuperTooltip(this.buttonXShowWebsites, new DevComponents.DotNetBar.SuperTooltipInfo("", "", "Websites", null, null, DevComponents.DotNetBar.eTooltipColor.Gray, true, false, new System.Drawing.Size(70, 18)));
			this.buttonXShowWebsites.TabIndex = 103;
			this.buttonXShowWebsites.TextColor = System.Drawing.Color.Black;
			this.buttonXShowWebsites.CheckedChanged += new System.EventHandler(this.buttonXShow_CheckedChanged);
			// 
			// buttonXShowDates
			// 
			this.buttonXShowDates.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXShowDates.AutoCheckOnClick = true;
			this.buttonXShowDates.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXShowDates.Image = global::NewBizWiz.AdSchedule.Controls.Properties.Resources.DigitalInfoDates;
			this.buttonXShowDates.Location = new System.Drawing.Point(207, 50);
			this.buttonXShowDates.Name = "buttonXShowDates";
			this.buttonXShowDates.Size = new System.Drawing.Size(54, 54);
			this.superTooltip.SetSuperTooltip(this.buttonXShowDates, new DevComponents.DotNetBar.SuperTooltipInfo("", "", "Campaign Dates", null, null, DevComponents.DotNetBar.eTooltipColor.Gray, false, false, new System.Drawing.Size(90, 18)));
			this.buttonXShowDates.TabIndex = 106;
			this.buttonXShowDates.TextColor = System.Drawing.Color.Black;
			this.buttonXShowDates.CheckedChanged += new System.EventHandler(this.buttonXShow_CheckedChanged);
			// 
			// buttonXShowCPM
			// 
			this.buttonXShowCPM.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXShowCPM.AutoCheckOnClick = true;
			this.buttonXShowCPM.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXShowCPM.Image = global::NewBizWiz.AdSchedule.Controls.Properties.Resources.DigitalInfoCPM;
			this.buttonXShowCPM.Location = new System.Drawing.Point(343, 50);
			this.buttonXShowCPM.Name = "buttonXShowCPM";
			this.buttonXShowCPM.Size = new System.Drawing.Size(54, 54);
			this.superTooltip.SetSuperTooltip(this.buttonXShowCPM, new DevComponents.DotNetBar.SuperTooltipInfo("", "", "CPM", null, null, DevComponents.DotNetBar.eTooltipColor.Gray, false, false, new System.Drawing.Size(40, 18)));
			this.buttonXShowCPM.TabIndex = 108;
			this.buttonXShowCPM.TextColor = System.Drawing.Color.Black;
			this.buttonXShowCPM.CheckedChanged += new System.EventHandler(this.buttonXShow_CheckedChanged);
			// 
			// buttonXShowProduct
			// 
			this.buttonXShowProduct.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXShowProduct.AutoCheckOnClick = true;
			this.buttonXShowProduct.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXShowProduct.Image = global::NewBizWiz.AdSchedule.Controls.Properties.Resources.DigitalInfoProduct;
			this.buttonXShowProduct.Location = new System.Drawing.Point(71, 50);
			this.buttonXShowProduct.Name = "buttonXShowProduct";
			this.buttonXShowProduct.Size = new System.Drawing.Size(54, 54);
			this.superTooltip.SetSuperTooltip(this.buttonXShowProduct, new DevComponents.DotNetBar.SuperTooltipInfo("", "", "Digital Product", null, null, DevComponents.DotNetBar.eTooltipColor.Gray, false, false, new System.Drawing.Size(90, 18)));
			this.buttonXShowProduct.TabIndex = 104;
			this.buttonXShowProduct.TextColor = System.Drawing.Color.Black;
			this.buttonXShowProduct.CheckedChanged += new System.EventHandler(this.buttonXShow_CheckedChanged);
			// 
			// buttonXShowDimensions
			// 
			this.buttonXShowDimensions.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXShowDimensions.AutoCheckOnClick = true;
			this.buttonXShowDimensions.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXShowDimensions.Image = global::NewBizWiz.AdSchedule.Controls.Properties.Resources.DigitalInfoDimensions;
			this.buttonXShowDimensions.Location = new System.Drawing.Point(139, 50);
			this.buttonXShowDimensions.Name = "buttonXShowDimensions";
			this.buttonXShowDimensions.Size = new System.Drawing.Size(54, 54);
			this.superTooltip.SetSuperTooltip(this.buttonXShowDimensions, new DevComponents.DotNetBar.SuperTooltipInfo("", "", "Ad-Size", null, null, DevComponents.DotNetBar.eTooltipColor.Gray, false, false, new System.Drawing.Size(70, 18)));
			this.buttonXShowDimensions.TabIndex = 105;
			this.buttonXShowDimensions.TextColor = System.Drawing.Color.Black;
			this.buttonXShowDimensions.CheckedChanged += new System.EventHandler(this.buttonXShow_CheckedChanged);
			// 
			// buttonXShowInvestment
			// 
			this.buttonXShowInvestment.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXShowInvestment.AutoCheckOnClick = true;
			this.buttonXShowInvestment.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXShowInvestment.Image = global::NewBizWiz.AdSchedule.Controls.Properties.Resources.DigitalInfoInvestment;
			this.buttonXShowInvestment.Location = new System.Drawing.Point(412, 50);
			this.buttonXShowInvestment.Name = "buttonXShowInvestment";
			this.buttonXShowInvestment.Size = new System.Drawing.Size(54, 54);
			this.superTooltip.SetSuperTooltip(this.buttonXShowInvestment, new DevComponents.DotNetBar.SuperTooltipInfo("", "", "Investment", null, null, DevComponents.DotNetBar.eTooltipColor.Gray, false, false, new System.Drawing.Size(70, 18)));
			this.buttonXShowInvestment.TabIndex = 109;
			this.buttonXShowInvestment.TextColor = System.Drawing.Color.Black;
			this.buttonXShowInvestment.CheckedChanged += new System.EventHandler(this.buttonXShow_CheckedChanged);
			// 
			// DigitalViewerControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.AliceBlue;
			this.Controls.Add(this.pnControls);
			this.Controls.Add(this.checkEditEnable);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "DigitalViewerControl";
			this.Size = new System.Drawing.Size(481, 438);
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			this.pnControls.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.memoEditInfo.Properties)).EndInit();
			this.pnButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.checkEditAllowEdit.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditEnable.Properties)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.StyleController styleController;
		private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
		private System.Windows.Forms.Panel pnControls;
		private DevExpress.XtraEditors.MemoEdit memoEditInfo;
		private DevExpress.XtraEditors.LabelControl labelControlWarning;
		private System.Windows.Forms.Panel pnButtons;
		private DevExpress.XtraEditors.CheckEdit checkEditAllowEdit;
		private DevExpress.XtraEditors.CheckEdit checkEditEnable;
		private DevComponents.DotNetBar.SuperTooltip superTooltip;
		private DevComponents.DotNetBar.ButtonX buttonXShowImpressions;
		private DevComponents.DotNetBar.ButtonX buttonXShowWebsites;
		private DevComponents.DotNetBar.ButtonX buttonXShowDates;
		private DevComponents.DotNetBar.ButtonX buttonXShowCPM;
		private DevComponents.DotNetBar.ButtonX buttonXShowProduct;
		private DevComponents.DotNetBar.ButtonX buttonXShowDimensions;
		private DevComponents.DotNetBar.ButtonX buttonXShowInvestment;
    }
}
