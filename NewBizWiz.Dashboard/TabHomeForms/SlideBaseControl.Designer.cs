namespace NewBizWiz.Dashboard.TabHomeForms
{
	public partial class SlideBaseControl
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
			this.pnMain = new System.Windows.Forms.Panel();
			this.pnTop = new System.Windows.Forms.Panel();
			this.comboBoxEditSlideHeader = new DevExpress.XtraEditors.ComboBoxEdit();
			this.pnBottom = new System.Windows.Forms.Panel();
			this.simpleButtonSaveTemplate = new DevExpress.XtraEditors.SimpleButton();
			this.pbDescription = new System.Windows.Forms.PictureBox();
			this.checkEditSolutionNew = new DevExpress.XtraEditors.CheckEdit();
			this.pnSlideSelector = new System.Windows.Forms.Panel();
			this.buttonXLeadoff = new DevComponents.DotNetBar.ButtonX();
			this.buttonXCover = new DevComponents.DotNetBar.ButtonX();
			this.buttonXClientGoals = new DevComponents.DotNetBar.ButtonX();
			this.buttonXTargetCustomers = new DevComponents.DotNetBar.ButtonX();
			this.buttonXSummary = new DevComponents.DotNetBar.ButtonX();
			this.superTooltip = new DevComponents.DotNetBar.SuperTooltip();
			this.pnTop.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditSlideHeader.Properties)).BeginInit();
			this.pnBottom.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbDescription)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditSolutionNew.Properties)).BeginInit();
			this.pnSlideSelector.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnMain
			// 
			this.pnMain.BackColor = System.Drawing.Color.Transparent;
			this.pnMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnMain.Location = new System.Drawing.Point(0, 51);
			this.pnMain.Name = "pnMain";
			this.pnMain.Size = new System.Drawing.Size(789, 387);
			this.pnMain.TabIndex = 0;
			// 
			// pnTop
			// 
			this.pnTop.Controls.Add(this.comboBoxEditSlideHeader);
			this.pnTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnTop.Location = new System.Drawing.Point(0, 0);
			this.pnTop.Name = "pnTop";
			this.pnTop.Size = new System.Drawing.Size(789, 51);
			this.pnTop.TabIndex = 29;
			// 
			// comboBoxEditSlideHeader
			// 
			this.comboBoxEditSlideHeader.Location = new System.Drawing.Point(11, 14);
			this.comboBoxEditSlideHeader.Name = "comboBoxEditSlideHeader";
			this.comboBoxEditSlideHeader.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.comboBoxEditSlideHeader.Properties.Appearance.Options.UseFont = true;
			this.comboBoxEditSlideHeader.Properties.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.comboBoxEditSlideHeader.Properties.AppearanceDisabled.Options.UseFont = true;
			this.comboBoxEditSlideHeader.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Arial", 9.75F);
			this.comboBoxEditSlideHeader.Properties.AppearanceDropDown.Options.UseFont = true;
			this.comboBoxEditSlideHeader.Properties.AppearanceFocused.Font = new System.Drawing.Font("Arial", 9.75F);
			this.comboBoxEditSlideHeader.Properties.AppearanceFocused.Options.UseFont = true;
			this.comboBoxEditSlideHeader.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("Arial", 9.75F);
			this.comboBoxEditSlideHeader.Properties.AppearanceReadOnly.Options.UseFont = true;
			this.comboBoxEditSlideHeader.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.comboBoxEditSlideHeader.Size = new System.Drawing.Size(311, 22);
			this.comboBoxEditSlideHeader.TabIndex = 28;
			// 
			// pnBottom
			// 
			this.pnBottom.Controls.Add(this.simpleButtonSaveTemplate);
			this.pnBottom.Controls.Add(this.pbDescription);
			this.pnBottom.Controls.Add(this.checkEditSolutionNew);
			this.pnBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnBottom.Location = new System.Drawing.Point(0, 438);
			this.pnBottom.Name = "pnBottom";
			this.pnBottom.Size = new System.Drawing.Size(789, 74);
			this.pnBottom.TabIndex = 1;
			// 
			// simpleButtonSaveTemplate
			// 
			this.simpleButtonSaveTemplate.AllowFocus = false;
			this.simpleButtonSaveTemplate.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
			this.simpleButtonSaveTemplate.Image = global::NewBizWiz.Dashboard.Properties.Resources.SaveTemplate;
			this.simpleButtonSaveTemplate.Location = new System.Drawing.Point(712, 11);
			this.simpleButtonSaveTemplate.Name = "simpleButtonSaveTemplate";
			this.simpleButtonSaveTemplate.Size = new System.Drawing.Size(75, 48);
			this.simpleButtonSaveTemplate.TabIndex = 111;
			this.simpleButtonSaveTemplate.Click += new System.EventHandler(this.SaveTemplate_Click);
			// 
			// pbDescription
			// 
			this.pbDescription.Location = new System.Drawing.Point(11, 11);
			this.pbDescription.Name = "pbDescription";
			this.pbDescription.Size = new System.Drawing.Size(430, 48);
			this.pbDescription.TabIndex = 107;
			this.pbDescription.TabStop = false;
			// 
			// checkEditSolutionNew
			// 
			this.checkEditSolutionNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.checkEditSolutionNew.Location = new System.Drawing.Point(528, 27);
			this.checkEditSolutionNew.Name = "checkEditSolutionNew";
			this.checkEditSolutionNew.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.checkEditSolutionNew.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
			this.checkEditSolutionNew.Properties.Appearance.Options.UseFont = true;
			this.checkEditSolutionNew.Properties.Appearance.Options.UseForeColor = true;
			this.checkEditSolutionNew.Properties.AutoWidth = true;
			this.checkEditSolutionNew.Properties.Caption = "This is a NEW Solution";
			this.checkEditSolutionNew.Properties.RadioGroupIndex = 1;
			this.checkEditSolutionNew.Size = new System.Drawing.Size(157, 19);
			this.checkEditSolutionNew.TabIndex = 105;
			this.checkEditSolutionNew.TabStop = false;
			// 
			// pnSlideSelector
			// 
			this.pnSlideSelector.Controls.Add(this.buttonXLeadoff);
			this.pnSlideSelector.Controls.Add(this.buttonXCover);
			this.pnSlideSelector.Controls.Add(this.buttonXClientGoals);
			this.pnSlideSelector.Controls.Add(this.buttonXTargetCustomers);
			this.pnSlideSelector.Controls.Add(this.buttonXSummary);
			this.pnSlideSelector.Dock = System.Windows.Forms.DockStyle.Right;
			this.pnSlideSelector.Location = new System.Drawing.Point(789, 0);
			this.pnSlideSelector.Name = "pnSlideSelector";
			this.pnSlideSelector.Size = new System.Drawing.Size(130, 512);
			this.pnSlideSelector.TabIndex = 2;
			// 
			// buttonXLeadoff
			// 
			this.buttonXLeadoff.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXLeadoff.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXLeadoff.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXLeadoff.Image = global::NewBizWiz.Dashboard.Properties.Resources.SlideLeadoff;
			this.buttonXLeadoff.Location = new System.Drawing.Point(42, 116);
			this.buttonXLeadoff.Name = "buttonXLeadoff";
			this.buttonXLeadoff.Size = new System.Drawing.Size(80, 72);
			this.buttonXLeadoff.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.superTooltip.SetSuperTooltip(this.buttonXLeadoff, new DevComponents.DotNetBar.SuperTooltipInfo("", "", "Set the Tone with an Intro Slide", null, null, DevComponents.DotNetBar.eTooltipColor.Gray));
			this.buttonXLeadoff.TabIndex = 31;
			this.buttonXLeadoff.Click += new System.EventHandler(this.SlideType_Click);
			// 
			// buttonXCover
			// 
			this.buttonXCover.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXCover.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXCover.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXCover.Image = global::NewBizWiz.Dashboard.Properties.Resources.SlideCover;
			this.buttonXCover.Location = new System.Drawing.Point(42, 12);
			this.buttonXCover.Name = "buttonXCover";
			this.buttonXCover.Size = new System.Drawing.Size(80, 72);
			this.buttonXCover.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.superTooltip.SetSuperTooltip(this.buttonXCover, new DevComponents.DotNetBar.SuperTooltipInfo("", "", "Start with a Simple Cover Slide", null, null, DevComponents.DotNetBar.eTooltipColor.Gray));
			this.buttonXCover.TabIndex = 30;
			this.buttonXCover.Click += new System.EventHandler(this.SlideType_Click);
			// 
			// buttonXClientGoals
			// 
			this.buttonXClientGoals.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXClientGoals.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXClientGoals.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXClientGoals.Image = global::NewBizWiz.Dashboard.Properties.Resources.SlideClientGoals;
			this.buttonXClientGoals.Location = new System.Drawing.Point(42, 220);
			this.buttonXClientGoals.Name = "buttonXClientGoals";
			this.buttonXClientGoals.Size = new System.Drawing.Size(80, 72);
			this.buttonXClientGoals.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.superTooltip.SetSuperTooltip(this.buttonXClientGoals, new DevComponents.DotNetBar.SuperTooltipInfo("", "", "What are your Client’s Needs and Goals?", null, null, DevComponents.DotNetBar.eTooltipColor.Gray));
			this.buttonXClientGoals.TabIndex = 32;
			this.buttonXClientGoals.Click += new System.EventHandler(this.SlideType_Click);
			// 
			// buttonXTargetCustomers
			// 
			this.buttonXTargetCustomers.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXTargetCustomers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXTargetCustomers.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXTargetCustomers.Image = global::NewBizWiz.Dashboard.Properties.Resources.SlideTargetCustomers;
			this.buttonXTargetCustomers.Location = new System.Drawing.Point(42, 324);
			this.buttonXTargetCustomers.Name = "buttonXTargetCustomers";
			this.buttonXTargetCustomers.Size = new System.Drawing.Size(80, 72);
			this.buttonXTargetCustomers.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.superTooltip.SetSuperTooltip(this.buttonXTargetCustomers, new DevComponents.DotNetBar.SuperTooltipInfo("", "", "Who is your client’s target audience?", null, null, DevComponents.DotNetBar.eTooltipColor.Gray));
			this.buttonXTargetCustomers.TabIndex = 33;
			this.buttonXTargetCustomers.Click += new System.EventHandler(this.SlideType_Click);
			// 
			// buttonXSummary
			// 
			this.buttonXSummary.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXSummary.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXSummary.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXSummary.Image = global::NewBizWiz.Dashboard.Properties.Resources.SlideSummary;
			this.buttonXSummary.Location = new System.Drawing.Point(42, 428);
			this.buttonXSummary.Name = "buttonXSummary";
			this.buttonXSummary.Size = new System.Drawing.Size(80, 72);
			this.buttonXSummary.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.superTooltip.SetSuperTooltip(this.buttonXSummary, new DevComponents.DotNetBar.SuperTooltipInfo("", "", "Wrap up with a Summary Slide", null, null, DevComponents.DotNetBar.eTooltipColor.Gray));
			this.buttonXSummary.TabIndex = 34;
			this.buttonXSummary.Click += new System.EventHandler(this.SlideType_Click);
			// 
			// superTooltip
			// 
			this.superTooltip.DefaultTooltipSettings = new DevComponents.DotNetBar.SuperTooltipInfo("", "", "", null, null, DevComponents.DotNetBar.eTooltipColor.Gray);
			this.superTooltip.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			// 
			// SlideBaseControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.pnMain);
			this.Controls.Add(this.pnTop);
			this.Controls.Add(this.pnBottom);
			this.Controls.Add(this.pnSlideSelector);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MaximumSize = new System.Drawing.Size(919, 512);
			this.MinimumSize = new System.Drawing.Size(919, 512);
			this.Name = "SlideBaseControl";
			this.Size = new System.Drawing.Size(919, 512);
			this.pnTop.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditSlideHeader.Properties)).EndInit();
			this.pnBottom.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pbDescription)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditSolutionNew.Properties)).EndInit();
			this.pnSlideSelector.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel pnSlideSelector;
		protected System.Windows.Forms.Panel pnMain;
		protected System.Windows.Forms.Panel pnBottom;
		protected DevExpress.XtraEditors.ComboBoxEdit comboBoxEditSlideHeader;
		protected System.Windows.Forms.Panel pnTop;
		protected DevExpress.XtraEditors.CheckEdit checkEditSolutionNew;
		protected System.Windows.Forms.PictureBox pbDescription;
		private DevComponents.DotNetBar.ButtonX buttonXCover;
		private DevComponents.DotNetBar.SuperTooltip superTooltip;
		private DevComponents.DotNetBar.ButtonX buttonXLeadoff;
		private DevComponents.DotNetBar.ButtonX buttonXClientGoals;
		private DevComponents.DotNetBar.ButtonX buttonXTargetCustomers;
		private DevComponents.DotNetBar.ButtonX buttonXSummary;
		protected DevExpress.XtraEditors.SimpleButton simpleButtonSaveTemplate;
	}
}
