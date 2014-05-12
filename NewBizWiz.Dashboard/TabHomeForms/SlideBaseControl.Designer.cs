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
			DevExpress.Utils.SuperToolTip superToolTip2 = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipItem toolTipItem2 = new DevExpress.Utils.ToolTipItem();
			DevExpress.Utils.SuperToolTip superToolTip3 = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipItem toolTipItem3 = new DevExpress.Utils.ToolTipItem();
			DevExpress.Utils.SuperToolTip superToolTip4 = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipItem toolTipItem4 = new DevExpress.Utils.ToolTipItem();
			DevExpress.Utils.SuperToolTip superToolTip5 = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipItem toolTipItem5 = new DevExpress.Utils.ToolTipItem();
			DevExpress.Utils.SuperToolTip superToolTip6 = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipItem toolTipItem6 = new DevExpress.Utils.ToolTipItem();
			DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
			this.pnMain = new System.Windows.Forms.Panel();
			this.pnTop = new System.Windows.Forms.Panel();
			this.comboBoxEditSlideHeader = new DevExpress.XtraEditors.ComboBoxEdit();
			this.pnBottom = new System.Windows.Forms.Panel();
			this.checkEditSolutionOld = new DevExpress.XtraEditors.CheckEdit();
			this.checkEditSolutionNew = new DevExpress.XtraEditors.CheckEdit();
			this.pnSlideSelector = new System.Windows.Forms.Panel();
			this.simpleButtonSummary = new DevExpress.XtraEditors.SimpleButton();
			this.simpleButtonTargetCustomers = new DevExpress.XtraEditors.SimpleButton();
			this.simpleButtonClientGoals = new DevExpress.XtraEditors.SimpleButton();
			this.simpleButtonLeadoff = new DevExpress.XtraEditors.SimpleButton();
			this.simpleButtonCover = new DevExpress.XtraEditors.SimpleButton();
			this.pbDescription = new System.Windows.Forms.PictureBox();
			this.pictureEditSaveTemplate = new DevExpress.XtraEditors.PictureEdit();
			this.pnMain.SuspendLayout();
			this.pnTop.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditSlideHeader.Properties)).BeginInit();
			this.pnBottom.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.checkEditSolutionOld.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditSolutionNew.Properties)).BeginInit();
			this.pnSlideSelector.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbDescription)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureEditSaveTemplate.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// pnMain
			// 
			this.pnMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pnMain.Controls.Add(this.pnTop);
			this.pnMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnMain.Location = new System.Drawing.Point(0, 0);
			this.pnMain.Name = "pnMain";
			this.pnMain.Size = new System.Drawing.Size(824, 438);
			this.pnMain.TabIndex = 0;
			// 
			// pnTop
			// 
			this.pnTop.Controls.Add(this.pictureEditSaveTemplate);
			this.pnTop.Controls.Add(this.comboBoxEditSlideHeader);
			this.pnTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnTop.Location = new System.Drawing.Point(0, 0);
			this.pnTop.Name = "pnTop";
			this.pnTop.Size = new System.Drawing.Size(820, 51);
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
			this.pnBottom.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pnBottom.Controls.Add(this.pbDescription);
			this.pnBottom.Controls.Add(this.checkEditSolutionOld);
			this.pnBottom.Controls.Add(this.checkEditSolutionNew);
			this.pnBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnBottom.Location = new System.Drawing.Point(0, 438);
			this.pnBottom.Name = "pnBottom";
			this.pnBottom.Size = new System.Drawing.Size(919, 74);
			this.pnBottom.TabIndex = 1;
			// 
			// checkEditSolutionOld
			// 
			this.checkEditSolutionOld.Location = new System.Drawing.Point(679, 25);
			this.checkEditSolutionOld.Name = "checkEditSolutionOld";
			this.checkEditSolutionOld.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.checkEditSolutionOld.Properties.Appearance.ForeColor = System.Drawing.Color.White;
			this.checkEditSolutionOld.Properties.Appearance.Options.UseFont = true;
			this.checkEditSolutionOld.Properties.Appearance.Options.UseForeColor = true;
			this.checkEditSolutionOld.Properties.Caption = "I am updating an old presentation";
			this.checkEditSolutionOld.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
			this.checkEditSolutionOld.Properties.RadioGroupIndex = 1;
			this.checkEditSolutionOld.Size = new System.Drawing.Size(230, 21);
			this.checkEditSolutionOld.TabIndex = 106;
			this.checkEditSolutionOld.TabStop = false;
			// 
			// checkEditSolutionNew
			// 
			this.checkEditSolutionNew.Location = new System.Drawing.Point(443, 25);
			this.checkEditSolutionNew.Name = "checkEditSolutionNew";
			this.checkEditSolutionNew.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.checkEditSolutionNew.Properties.Appearance.ForeColor = System.Drawing.Color.White;
			this.checkEditSolutionNew.Properties.Appearance.Options.UseFont = true;
			this.checkEditSolutionNew.Properties.Appearance.Options.UseForeColor = true;
			this.checkEditSolutionNew.Properties.Caption = "I am building a new client solution";
			this.checkEditSolutionNew.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
			this.checkEditSolutionNew.Properties.RadioGroupIndex = 1;
			this.checkEditSolutionNew.Size = new System.Drawing.Size(230, 21);
			this.checkEditSolutionNew.TabIndex = 105;
			this.checkEditSolutionNew.TabStop = false;
			// 
			// pnSlideSelector
			// 
			this.pnSlideSelector.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pnSlideSelector.Controls.Add(this.simpleButtonSummary);
			this.pnSlideSelector.Controls.Add(this.simpleButtonTargetCustomers);
			this.pnSlideSelector.Controls.Add(this.simpleButtonClientGoals);
			this.pnSlideSelector.Controls.Add(this.simpleButtonLeadoff);
			this.pnSlideSelector.Controls.Add(this.simpleButtonCover);
			this.pnSlideSelector.Dock = System.Windows.Forms.DockStyle.Right;
			this.pnSlideSelector.Location = new System.Drawing.Point(824, 0);
			this.pnSlideSelector.Name = "pnSlideSelector";
			this.pnSlideSelector.Size = new System.Drawing.Size(95, 438);
			this.pnSlideSelector.TabIndex = 2;
			// 
			// simpleButtonSummary
			// 
			this.simpleButtonSummary.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
			this.simpleButtonSummary.Appearance.Options.UseBackColor = true;
			this.simpleButtonSummary.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
			this.simpleButtonSummary.Image = global::NewBizWiz.Dashboard.Properties.Resources.SlideSummary;
			this.simpleButtonSummary.Location = new System.Drawing.Point(5, 357);
			this.simpleButtonSummary.Name = "simpleButtonSummary";
			this.simpleButtonSummary.Size = new System.Drawing.Size(81, 72);
			toolTipItem2.Text = "Wrap up with a Summary Slide";
			superToolTip2.Items.Add(toolTipItem2);
			this.simpleButtonSummary.SuperTip = superToolTip2;
			this.simpleButtonSummary.TabIndex = 4;
			this.simpleButtonSummary.Click += new System.EventHandler(this.SlideType_Click);
			// 
			// simpleButtonTargetCustomers
			// 
			this.simpleButtonTargetCustomers.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
			this.simpleButtonTargetCustomers.Appearance.Options.UseBackColor = true;
			this.simpleButtonTargetCustomers.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
			this.simpleButtonTargetCustomers.Image = global::NewBizWiz.Dashboard.Properties.Resources.SlideTargetCustomers;
			this.simpleButtonTargetCustomers.Location = new System.Drawing.Point(5, 270);
			this.simpleButtonTargetCustomers.Name = "simpleButtonTargetCustomers";
			this.simpleButtonTargetCustomers.Size = new System.Drawing.Size(81, 72);
			toolTipItem3.Text = "Who is your client’s target audience?";
			superToolTip3.Items.Add(toolTipItem3);
			this.simpleButtonTargetCustomers.SuperTip = superToolTip3;
			this.simpleButtonTargetCustomers.TabIndex = 3;
			this.simpleButtonTargetCustomers.Click += new System.EventHandler(this.SlideType_Click);
			// 
			// simpleButtonClientGoals
			// 
			this.simpleButtonClientGoals.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
			this.simpleButtonClientGoals.Appearance.Options.UseBackColor = true;
			this.simpleButtonClientGoals.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
			this.simpleButtonClientGoals.Image = global::NewBizWiz.Dashboard.Properties.Resources.SlideClientGoals;
			this.simpleButtonClientGoals.Location = new System.Drawing.Point(4, 183);
			this.simpleButtonClientGoals.Name = "simpleButtonClientGoals";
			this.simpleButtonClientGoals.Size = new System.Drawing.Size(81, 72);
			toolTipItem4.Text = "What are your Client’s Needs and Goals?";
			superToolTip4.Items.Add(toolTipItem4);
			this.simpleButtonClientGoals.SuperTip = superToolTip4;
			this.simpleButtonClientGoals.TabIndex = 2;
			this.simpleButtonClientGoals.Click += new System.EventHandler(this.SlideType_Click);
			// 
			// simpleButtonLeadoff
			// 
			this.simpleButtonLeadoff.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
			this.simpleButtonLeadoff.Appearance.Options.UseBackColor = true;
			this.simpleButtonLeadoff.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
			this.simpleButtonLeadoff.Image = global::NewBizWiz.Dashboard.Properties.Resources.SlideLeadoff;
			this.simpleButtonLeadoff.Location = new System.Drawing.Point(4, 96);
			this.simpleButtonLeadoff.Name = "simpleButtonLeadoff";
			this.simpleButtonLeadoff.Size = new System.Drawing.Size(81, 72);
			toolTipItem5.Text = "Set the Tone with an Intro Slide";
			superToolTip5.Items.Add(toolTipItem5);
			this.simpleButtonLeadoff.SuperTip = superToolTip5;
			this.simpleButtonLeadoff.TabIndex = 1;
			this.simpleButtonLeadoff.Click += new System.EventHandler(this.SlideType_Click);
			// 
			// simpleButtonCover
			// 
			this.simpleButtonCover.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
			this.simpleButtonCover.Appearance.Options.UseBackColor = true;
			this.simpleButtonCover.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
			this.simpleButtonCover.Image = global::NewBizWiz.Dashboard.Properties.Resources.SlideCover;
			this.simpleButtonCover.Location = new System.Drawing.Point(5, 9);
			this.simpleButtonCover.Name = "simpleButtonCover";
			this.simpleButtonCover.Size = new System.Drawing.Size(81, 72);
			toolTipItem6.Text = "Start with a Simple Cover Slide";
			superToolTip6.Items.Add(toolTipItem6);
			this.simpleButtonCover.SuperTip = superToolTip6;
			this.simpleButtonCover.TabIndex = 0;
			this.simpleButtonCover.Click += new System.EventHandler(this.SlideType_Click);
			// 
			// pbDescription
			// 
			this.pbDescription.Location = new System.Drawing.Point(11, 11);
			this.pbDescription.Name = "pbDescription";
			this.pbDescription.Size = new System.Drawing.Size(430, 48);
			this.pbDescription.TabIndex = 107;
			this.pbDescription.TabStop = false;
			// 
			// pictureEditSaveTemplate
			// 
			this.pictureEditSaveTemplate.EditValue = global::NewBizWiz.Dashboard.Properties.Resources.SaveTemplate;
			this.pictureEditSaveTemplate.Location = new System.Drawing.Point(641, 9);
			this.pictureEditSaveTemplate.Name = "pictureEditSaveTemplate";
			this.pictureEditSaveTemplate.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.pictureEditSaveTemplate.Properties.Appearance.Options.UseBackColor = true;
			this.pictureEditSaveTemplate.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.pictureEditSaveTemplate.Size = new System.Drawing.Size(175, 32);
			toolTipItem1.Text = "Save this template for future  presentations…";
			superToolTip1.Items.Add(toolTipItem1);
			this.pictureEditSaveTemplate.SuperTip = superToolTip1;
			this.pictureEditSaveTemplate.TabIndex = 30;
			this.pictureEditSaveTemplate.Click += new System.EventHandler(this.pictureEditSaveTemplate_Click);
			this.pictureEditSaveTemplate.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
			this.pictureEditSaveTemplate.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseUp);
			// 
			// SlideBaseControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
			this.Controls.Add(this.pnMain);
			this.Controls.Add(this.pnSlideSelector);
			this.Controls.Add(this.pnBottom);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MaximumSize = new System.Drawing.Size(919, 512);
			this.MinimumSize = new System.Drawing.Size(919, 512);
			this.Name = "SlideBaseControl";
			this.Size = new System.Drawing.Size(919, 512);
			this.pnMain.ResumeLayout(false);
			this.pnTop.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditSlideHeader.Properties)).EndInit();
			this.pnBottom.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.checkEditSolutionOld.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditSolutionNew.Properties)).EndInit();
			this.pnSlideSelector.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pbDescription)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureEditSaveTemplate.Properties)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel pnSlideSelector;
		private DevExpress.XtraEditors.SimpleButton simpleButtonCover;
		private DevExpress.XtraEditors.SimpleButton simpleButtonLeadoff;
		private DevExpress.XtraEditors.SimpleButton simpleButtonSummary;
		private DevExpress.XtraEditors.SimpleButton simpleButtonTargetCustomers;
		private DevExpress.XtraEditors.SimpleButton simpleButtonClientGoals;
		protected System.Windows.Forms.Panel pnMain;
		protected System.Windows.Forms.Panel pnBottom;
		protected DevExpress.XtraEditors.ComboBoxEdit comboBoxEditSlideHeader;
		protected System.Windows.Forms.Panel pnTop;
		protected DevExpress.XtraEditors.CheckEdit checkEditSolutionNew;
		protected DevExpress.XtraEditors.CheckEdit checkEditSolutionOld;
		protected System.Windows.Forms.PictureBox pbDescription;
		private DevExpress.XtraEditors.PictureEdit pictureEditSaveTemplate;
	}
}
