namespace Asa.Media.Controls.PresentationClasses.OptionsControls.Settings
{
	partial class OptionSetColumnSettingsControl
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
			this.buttonXStation = new DevComponents.DotNetBar.ButtonX();
			this.buttonXLength = new DevComponents.DotNetBar.ButtonX();
			this.buttonXTallySpots = new DevComponents.DotNetBar.ButtonX();
			this.buttonXDay = new DevComponents.DotNetBar.ButtonX();
			this.buttonXMonthlySpots = new DevComponents.DotNetBar.ButtonX();
			this.buttonXLineId = new DevComponents.DotNetBar.ButtonX();
			this.buttonXRate = new DevComponents.DotNetBar.ButtonX();
			this.buttonXCost = new DevComponents.DotNetBar.ButtonX();
			this.buttonXWeeklySpots = new DevComponents.DotNetBar.ButtonX();
			this.buttonXProgram = new DevComponents.DotNetBar.ButtonX();
			this.buttonXTallyCost = new DevComponents.DotNetBar.ButtonX();
			this.buttonXTime = new DevComponents.DotNetBar.ButtonX();
			this.buttonXTotalSpots = new DevComponents.DotNetBar.ButtonX();
			this.buttonXLogo = new DevComponents.DotNetBar.ButtonX();
			this.buttonXAvgRate = new DevComponents.DotNetBar.ButtonX();
			this.checkEditApplyForAll = new DevExpress.XtraEditors.CheckEdit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditApplyForAll.Properties)).BeginInit();
			this.SuspendLayout();
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
			// buttonXOptionStation
			// 
			this.buttonXStation.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXStation.AutoCheckOnClick = true;
			this.buttonXStation.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXStation.Font = new System.Drawing.Font("Arial", 9.75F);
			this.buttonXStation.Location = new System.Drawing.Point(10, 9);
			this.buttonXStation.Name = "buttonXStation";
			this.buttonXStation.Size = new System.Drawing.Size(113, 27);
			this.buttonXStation.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXStation.TabIndex = 56;
			this.buttonXStation.Text = "Station";
			this.buttonXStation.TextColor = System.Drawing.Color.Black;
			this.buttonXStation.CheckedChanged += new System.EventHandler(this.OnSettingsChanged);
			// 
			// buttonXOptionLength
			// 
			this.buttonXLength.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXLength.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXLength.AutoCheckOnClick = true;
			this.buttonXLength.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXLength.Font = new System.Drawing.Font("Arial", 9.75F);
			this.buttonXLength.Location = new System.Drawing.Point(155, 97);
			this.buttonXLength.Name = "buttonXLength";
			this.buttonXLength.Size = new System.Drawing.Size(113, 27);
			this.buttonXLength.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXLength.TabIndex = 57;
			this.buttonXLength.Text = "Length";
			this.buttonXLength.TextColor = System.Drawing.Color.Black;
			this.buttonXLength.CheckedChanged += new System.EventHandler(this.OnSettingsChanged);
			// 
			// buttonXOptionTallySpots
			// 
			this.buttonXTallySpots.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXTallySpots.AutoCheckOnClick = true;
			this.buttonXTallySpots.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXTallySpots.Font = new System.Drawing.Font("Arial", 9.75F);
			this.buttonXTallySpots.Location = new System.Drawing.Point(10, 273);
			this.buttonXTallySpots.Name = "buttonXTallySpots";
			this.buttonXTallySpots.Size = new System.Drawing.Size(113, 27);
			this.buttonXTallySpots.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXTallySpots.TabIndex = 121;
			this.buttonXTallySpots.Text = "Tally Spots";
			this.buttonXTallySpots.TextColor = System.Drawing.Color.Black;
			this.buttonXTallySpots.CheckedChanged += new System.EventHandler(this.OnSettingsChanged);
			this.buttonXTallySpots.Click += new System.EventHandler(this.OnSpotsClick);
			// 
			// buttonXOptionDay
			// 
			this.buttonXDay.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXDay.AutoCheckOnClick = true;
			this.buttonXDay.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXDay.Font = new System.Drawing.Font("Arial", 9.75F);
			this.buttonXDay.Location = new System.Drawing.Point(10, 53);
			this.buttonXDay.Name = "buttonXDay";
			this.buttonXDay.Size = new System.Drawing.Size(113, 27);
			this.buttonXDay.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXDay.TabIndex = 58;
			this.buttonXDay.Text = "Day";
			this.buttonXDay.TextColor = System.Drawing.Color.Black;
			this.buttonXDay.CheckedChanged += new System.EventHandler(this.OnSettingsChanged);
			// 
			// buttonXOptionMonthlySpots
			// 
			this.buttonXMonthlySpots.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXMonthlySpots.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXMonthlySpots.Font = new System.Drawing.Font("Arial", 9.75F);
			this.buttonXMonthlySpots.Location = new System.Drawing.Point(10, 185);
			this.buttonXMonthlySpots.Name = "buttonXMonthlySpots";
			this.buttonXMonthlySpots.Size = new System.Drawing.Size(113, 27);
			this.buttonXMonthlySpots.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXMonthlySpots.TabIndex = 120;
			this.buttonXMonthlySpots.Text = "Monthly Spots";
			this.buttonXMonthlySpots.TextColor = System.Drawing.Color.Black;
			this.buttonXMonthlySpots.CheckedChanged += new System.EventHandler(this.OnSettingsChanged);
			this.buttonXMonthlySpots.Click += new System.EventHandler(this.OnSpotsClick);
			// 
			// buttonXOptionLineId
			// 
			this.buttonXLineId.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXLineId.AutoCheckOnClick = true;
			this.buttonXLineId.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXLineId.Font = new System.Drawing.Font("Arial", 9.75F);
			this.buttonXLineId.Location = new System.Drawing.Point(10, 229);
			this.buttonXLineId.Name = "buttonXLineId";
			this.buttonXLineId.Size = new System.Drawing.Size(113, 27);
			this.buttonXLineId.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXLineId.TabIndex = 118;
			this.buttonXLineId.Text = "Line ID";
			this.buttonXLineId.TextColor = System.Drawing.Color.Black;
			this.buttonXLineId.CheckedChanged += new System.EventHandler(this.OnSettingsChanged);
			// 
			// buttonXOptionRate
			// 
			this.buttonXRate.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXRate.AutoCheckOnClick = true;
			this.buttonXRate.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXRate.Font = new System.Drawing.Font("Arial", 9.75F);
			this.buttonXRate.Location = new System.Drawing.Point(10, 97);
			this.buttonXRate.Name = "buttonXRate";
			this.buttonXRate.Size = new System.Drawing.Size(113, 27);
			this.buttonXRate.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXRate.TabIndex = 60;
			this.buttonXRate.Text = "Rate";
			this.buttonXRate.TextColor = System.Drawing.Color.Black;
			this.buttonXRate.CheckedChanged += new System.EventHandler(this.OnSettingsChanged);
			// 
			// buttonXOptionCost
			// 
			this.buttonXCost.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXCost.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXCost.AutoCheckOnClick = true;
			this.buttonXCost.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXCost.Font = new System.Drawing.Font("Arial", 9.75F);
			this.buttonXCost.Location = new System.Drawing.Point(155, 229);
			this.buttonXCost.Name = "buttonXCost";
			this.buttonXCost.Size = new System.Drawing.Size(113, 27);
			this.buttonXCost.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXCost.TabIndex = 117;
			this.buttonXCost.Text = "Cost Column";
			this.buttonXCost.TextColor = System.Drawing.Color.Black;
			this.buttonXCost.CheckedChanged += new System.EventHandler(this.OnSettingsChanged);
			// 
			// buttonXOptionWeeklySpots
			// 
			this.buttonXWeeklySpots.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXWeeklySpots.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXWeeklySpots.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXWeeklySpots.Font = new System.Drawing.Font("Arial", 9.75F);
			this.buttonXWeeklySpots.Location = new System.Drawing.Point(155, 141);
			this.buttonXWeeklySpots.Name = "buttonXWeeklySpots";
			this.buttonXWeeklySpots.Size = new System.Drawing.Size(113, 27);
			this.buttonXWeeklySpots.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXWeeklySpots.TabIndex = 119;
			this.buttonXWeeklySpots.Text = "Weekly Spots";
			this.buttonXWeeklySpots.TextColor = System.Drawing.Color.Black;
			this.buttonXWeeklySpots.CheckedChanged += new System.EventHandler(this.OnSettingsChanged);
			this.buttonXWeeklySpots.Click += new System.EventHandler(this.OnSpotsClick);
			// 
			// buttonXOptionProgram
			// 
			this.buttonXProgram.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXProgram.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXProgram.AutoCheckOnClick = true;
			this.buttonXProgram.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXProgram.Font = new System.Drawing.Font("Arial", 9.75F);
			this.buttonXProgram.Location = new System.Drawing.Point(155, 9);
			this.buttonXProgram.Name = "buttonXProgram";
			this.buttonXProgram.Size = new System.Drawing.Size(113, 27);
			this.buttonXProgram.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXProgram.TabIndex = 62;
			this.buttonXProgram.Text = "Program";
			this.buttonXProgram.TextColor = System.Drawing.Color.Black;
			this.buttonXProgram.CheckedChanged += new System.EventHandler(this.OnSettingsChanged);
			// 
			// buttonXOptionTallyCost
			// 
			this.buttonXTallyCost.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXTallyCost.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXTallyCost.AutoCheckOnClick = true;
			this.buttonXTallyCost.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXTallyCost.Font = new System.Drawing.Font("Arial", 9.75F);
			this.buttonXTallyCost.Location = new System.Drawing.Point(155, 273);
			this.buttonXTallyCost.Name = "buttonXTallyCost";
			this.buttonXTallyCost.Size = new System.Drawing.Size(113, 27);
			this.buttonXTallyCost.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXTallyCost.TabIndex = 61;
			this.buttonXTallyCost.Text = "Tally Cost";
			this.buttonXTallyCost.TextColor = System.Drawing.Color.Black;
			this.buttonXTallyCost.CheckedChanged += new System.EventHandler(this.OnSettingsChanged);
			// 
			// buttonXOptionTime
			// 
			this.buttonXTime.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXTime.AutoCheckOnClick = true;
			this.buttonXTime.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXTime.Font = new System.Drawing.Font("Arial", 9.75F);
			this.buttonXTime.Location = new System.Drawing.Point(155, 53);
			this.buttonXTime.Name = "buttonXTime";
			this.buttonXTime.Size = new System.Drawing.Size(113, 27);
			this.buttonXTime.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXTime.TabIndex = 63;
			this.buttonXTime.Text = "Time";
			this.buttonXTime.TextColor = System.Drawing.Color.Black;
			this.buttonXTime.CheckedChanged += new System.EventHandler(this.OnSettingsChanged);
			// 
			// buttonXOptionTotalSpots
			// 
			this.buttonXTotalSpots.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXTotalSpots.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXTotalSpots.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXTotalSpots.Font = new System.Drawing.Font("Arial", 9.75F);
			this.buttonXTotalSpots.Location = new System.Drawing.Point(155, 185);
			this.buttonXTotalSpots.Name = "buttonXTotalSpots";
			this.buttonXTotalSpots.Size = new System.Drawing.Size(113, 27);
			this.buttonXTotalSpots.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXTotalSpots.TabIndex = 55;
			this.buttonXTotalSpots.Text = "Total Spots";
			this.buttonXTotalSpots.TextColor = System.Drawing.Color.Black;
			this.buttonXTotalSpots.CheckedChanged += new System.EventHandler(this.OnSettingsChanged);
			// 
			// buttonXOptionLogo
			// 
			this.buttonXLogo.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXLogo.AutoCheckOnClick = true;
			this.buttonXLogo.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXLogo.Font = new System.Drawing.Font("Arial", 9.75F);
			this.buttonXLogo.Location = new System.Drawing.Point(10, 141);
			this.buttonXLogo.Name = "buttonXLogo";
			this.buttonXLogo.Size = new System.Drawing.Size(113, 27);
			this.buttonXLogo.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXLogo.TabIndex = 64;
			this.buttonXLogo.Text = "Logo";
			this.buttonXLogo.TextColor = System.Drawing.Color.Black;
			this.buttonXLogo.CheckedChanged += new System.EventHandler(this.OnSettingsChanged);
			// 
			// buttonXOptionAvgRate
			// 
			this.buttonXAvgRate.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXAvgRate.AutoCheckOnClick = true;
			this.buttonXAvgRate.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXAvgRate.Font = new System.Drawing.Font("Arial", 9.75F);
			this.buttonXAvgRate.Location = new System.Drawing.Point(10, 318);
			this.buttonXAvgRate.Name = "buttonXAvgRate";
			this.buttonXAvgRate.Size = new System.Drawing.Size(113, 27);
			this.buttonXAvgRate.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXAvgRate.TabIndex = 54;
			this.buttonXAvgRate.Text = "Average Rate";
			this.buttonXAvgRate.TextColor = System.Drawing.Color.Black;
			this.buttonXAvgRate.CheckedChanged += new System.EventHandler(this.OnSettingsChanged);
			// 
			// checkEditApplyForAll
			// 
			this.checkEditApplyForAll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.checkEditApplyForAll.Location = new System.Drawing.Point(10, 371);
			this.checkEditApplyForAll.Name = "checkEditApplyForAll";
			this.checkEditApplyForAll.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
			this.checkEditApplyForAll.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.checkEditApplyForAll.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
			this.checkEditApplyForAll.Properties.Appearance.Options.UseBackColor = true;
			this.checkEditApplyForAll.Properties.Appearance.Options.UseForeColor = true;
			this.checkEditApplyForAll.Properties.Appearance.Options.UseTextOptions = true;
			this.checkEditApplyForAll.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.checkEditApplyForAll.Properties.AppearanceDisabled.Options.UseTextOptions = true;
			this.checkEditApplyForAll.Properties.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.checkEditApplyForAll.Properties.AppearanceFocused.Options.UseTextOptions = true;
			this.checkEditApplyForAll.Properties.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.checkEditApplyForAll.Properties.AppearanceReadOnly.Options.UseTextOptions = true;
			this.checkEditApplyForAll.Properties.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.checkEditApplyForAll.Properties.Caption = "Use these Settings for all Schedules";
			this.checkEditApplyForAll.Size = new System.Drawing.Size(252, 20);
			this.checkEditApplyForAll.StyleController = this.styleController;
			this.checkEditApplyForAll.TabIndex = 130;
			this.checkEditApplyForAll.CheckedChanged += new System.EventHandler(this.OnSettingsChanged);
			// 
			// OptionSetColumnSettingsControls
			// 
			this.Controls.Add(this.checkEditApplyForAll);
			this.Controls.Add(this.buttonXStation);
			this.Controls.Add(this.buttonXLength);
			this.Controls.Add(this.buttonXTallySpots);
			this.Controls.Add(this.buttonXAvgRate);
			this.Controls.Add(this.buttonXDay);
			this.Controls.Add(this.buttonXLogo);
			this.Controls.Add(this.buttonXMonthlySpots);
			this.Controls.Add(this.buttonXTotalSpots);
			this.Controls.Add(this.buttonXLineId);
			this.Controls.Add(this.buttonXTime);
			this.Controls.Add(this.buttonXRate);
			this.Controls.Add(this.buttonXTallyCost);
			this.Controls.Add(this.buttonXCost);
			this.Controls.Add(this.buttonXProgram);
			this.Controls.Add(this.buttonXWeeklySpots);
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "OptionSetColumnSettingsControl";
			this.Size = new System.Drawing.Size(279, 394);
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditApplyForAll.Properties)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraEditors.StyleController styleController;
		private DevComponents.DotNetBar.ButtonX buttonXStation;
		private DevComponents.DotNetBar.ButtonX buttonXLength;
		private DevComponents.DotNetBar.ButtonX buttonXTallySpots;
		private DevComponents.DotNetBar.ButtonX buttonXDay;
		private DevComponents.DotNetBar.ButtonX buttonXMonthlySpots;
		private DevComponents.DotNetBar.ButtonX buttonXLineId;
		private DevComponents.DotNetBar.ButtonX buttonXRate;
		private DevComponents.DotNetBar.ButtonX buttonXCost;
		private DevComponents.DotNetBar.ButtonX buttonXWeeklySpots;
		private DevComponents.DotNetBar.ButtonX buttonXProgram;
		private DevComponents.DotNetBar.ButtonX buttonXTallyCost;
		private DevComponents.DotNetBar.ButtonX buttonXTime;
		private DevComponents.DotNetBar.ButtonX buttonXTotalSpots;
		private DevComponents.DotNetBar.ButtonX buttonXLogo;
		private DevComponents.DotNetBar.ButtonX buttonXAvgRate;
		public DevExpress.XtraEditors.CheckEdit checkEditApplyForAll;
	}
}
