﻿namespace Asa.Media.Controls.PresentationClasses.SnapshotControls.Settings
{
	partial class SnapshotColumnSettingsControl
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
			this.checkEditApplyForAll = new DevExpress.XtraEditors.CheckEdit();
			this.buttonXStation = new DevComponents.DotNetBar.ButtonX();
			this.buttonXAvgRate = new DevComponents.DotNetBar.ButtonX();
			this.buttonXTotalSpots = new DevComponents.DotNetBar.ButtonX();
			this.buttonXLineId = new DevComponents.DotNetBar.ButtonX();
			this.buttonXLength = new DevComponents.DotNetBar.ButtonX();
			this.buttonXTotalRow = new DevComponents.DotNetBar.ButtonX();
			this.buttonXDaypart = new DevComponents.DotNetBar.ButtonX();
			this.buttonXRate = new DevComponents.DotNetBar.ButtonX();
			this.buttonXLogo = new DevComponents.DotNetBar.ButtonX();
			this.buttonXTotalCost = new DevComponents.DotNetBar.ButtonX();
			this.buttonXTime = new DevComponents.DotNetBar.ButtonX();
			this.buttonXProgram = new DevComponents.DotNetBar.ButtonX();
			this.buttonXWeeklySpots = new DevComponents.DotNetBar.ButtonX();
			this.buttonXWeeklyCost = new DevComponents.DotNetBar.ButtonX();
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
			this.checkEditApplyForAll.Size = new System.Drawing.Size(262, 20);
			this.checkEditApplyForAll.StyleController = this.styleController;
			this.checkEditApplyForAll.TabIndex = 131;
			this.checkEditApplyForAll.CheckedChanged += new System.EventHandler(this.OnSettingsChanged);
			// 
			// buttonXStation
			// 
			this.buttonXStation.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXStation.AutoCheckOnClick = true;
			this.buttonXStation.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXStation.Font = new System.Drawing.Font("Arial", 9.75F);
			this.buttonXStation.Location = new System.Drawing.Point(10, 9);
			this.buttonXStation.Name = "buttonXStation";
			this.buttonXStation.Size = new System.Drawing.Size(113, 27);
			this.buttonXStation.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXStation.TabIndex = 144;
			this.buttonXStation.Text = "Station";
			this.buttonXStation.TextColor = System.Drawing.Color.Black;
			this.buttonXStation.CheckedChanged += new System.EventHandler(this.OnSettingsChanged);
			// 
			// buttonXAvgRate
			// 
			this.buttonXAvgRate.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXAvgRate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXAvgRate.AutoCheckOnClick = true;
			this.buttonXAvgRate.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXAvgRate.Font = new System.Drawing.Font("Arial", 9.75F);
			this.buttonXAvgRate.Location = new System.Drawing.Point(156, 269);
			this.buttonXAvgRate.Name = "buttonXAvgRate";
			this.buttonXAvgRate.Size = new System.Drawing.Size(113, 27);
			this.buttonXAvgRate.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXAvgRate.TabIndex = 142;
			this.buttonXAvgRate.Text = "Average Rate";
			this.buttonXAvgRate.TextColor = System.Drawing.Color.Black;
			this.buttonXAvgRate.CheckedChanged += new System.EventHandler(this.OnSettingsChanged);
			// 
			// buttonXTotalSpots
			// 
			this.buttonXTotalSpots.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXTotalSpots.AutoCheckOnClick = true;
			this.buttonXTotalSpots.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXTotalSpots.Font = new System.Drawing.Font("Arial", 9.75F);
			this.buttonXTotalSpots.Location = new System.Drawing.Point(10, 217);
			this.buttonXTotalSpots.Name = "buttonXTotalSpots";
			this.buttonXTotalSpots.Size = new System.Drawing.Size(113, 27);
			this.buttonXTotalSpots.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXTotalSpots.TabIndex = 143;
			this.buttonXTotalSpots.Text = "Total Spots";
			this.buttonXTotalSpots.TextColor = System.Drawing.Color.Black;
			this.buttonXTotalSpots.CheckedChanged += new System.EventHandler(this.OnSettingsChanged);
			// 
			// buttonXLineId
			// 
			this.buttonXLineId.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXLineId.AutoCheckOnClick = true;
			this.buttonXLineId.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXLineId.Font = new System.Drawing.Font("Arial", 9.75F);
			this.buttonXLineId.Location = new System.Drawing.Point(10, 321);
			this.buttonXLineId.Name = "buttonXLineId";
			this.buttonXLineId.Size = new System.Drawing.Size(113, 27);
			this.buttonXLineId.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXLineId.TabIndex = 153;
			this.buttonXLineId.Text = "Line ID";
			this.buttonXLineId.TextColor = System.Drawing.Color.Black;
			this.buttonXLineId.CheckedChanged += new System.EventHandler(this.OnSettingsChanged);
			// 
			// buttonXLength
			// 
			this.buttonXLength.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXLength.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXLength.AutoCheckOnClick = true;
			this.buttonXLength.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXLength.Font = new System.Drawing.Font("Arial", 9.75F);
			this.buttonXLength.Location = new System.Drawing.Point(156, 9);
			this.buttonXLength.Name = "buttonXLength";
			this.buttonXLength.Size = new System.Drawing.Size(113, 27);
			this.buttonXLength.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXLength.TabIndex = 145;
			this.buttonXLength.Text = "Length";
			this.buttonXLength.TextColor = System.Drawing.Color.Black;
			this.buttonXLength.CheckedChanged += new System.EventHandler(this.OnSettingsChanged);
			// 
			// buttonXTotalRow
			// 
			this.buttonXTotalRow.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXTotalRow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXTotalRow.AutoCheckOnClick = true;
			this.buttonXTotalRow.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXTotalRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.buttonXTotalRow.Location = new System.Drawing.Point(156, 321);
			this.buttonXTotalRow.Name = "buttonXTotalRow";
			this.buttonXTotalRow.Size = new System.Drawing.Size(113, 27);
			this.buttonXTotalRow.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXTotalRow.TabIndex = 152;
			this.buttonXTotalRow.Text = "Total Row";
			this.buttonXTotalRow.TextColor = System.Drawing.Color.Black;
			this.buttonXTotalRow.CheckedChanged += new System.EventHandler(this.OnSettingsChanged);
			// 
			// buttonXDaypart
			// 
			this.buttonXDaypart.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXDaypart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXDaypart.AutoCheckOnClick = true;
			this.buttonXDaypart.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXDaypart.Font = new System.Drawing.Font("Arial", 9.75F);
			this.buttonXDaypart.Location = new System.Drawing.Point(156, 61);
			this.buttonXDaypart.Name = "buttonXDaypart";
			this.buttonXDaypart.Size = new System.Drawing.Size(113, 27);
			this.buttonXDaypart.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXDaypart.TabIndex = 146;
			this.buttonXDaypart.Text = "Daypart";
			this.buttonXDaypart.TextColor = System.Drawing.Color.Black;
			this.buttonXDaypart.CheckedChanged += new System.EventHandler(this.OnSettingsChanged);
			// 
			// buttonXRate
			// 
			this.buttonXRate.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXRate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXRate.AutoCheckOnClick = true;
			this.buttonXRate.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXRate.Font = new System.Drawing.Font("Arial", 9.75F);
			this.buttonXRate.Location = new System.Drawing.Point(156, 113);
			this.buttonXRate.Name = "buttonXRate";
			this.buttonXRate.Size = new System.Drawing.Size(113, 27);
			this.buttonXRate.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXRate.TabIndex = 147;
			this.buttonXRate.Text = "Rate";
			this.buttonXRate.TextColor = System.Drawing.Color.Black;
			this.buttonXRate.CheckedChanged += new System.EventHandler(this.OnSettingsChanged);
			// 
			// buttonXLogo
			// 
			this.buttonXLogo.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXLogo.AutoCheckOnClick = true;
			this.buttonXLogo.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXLogo.Font = new System.Drawing.Font("Arial", 9.75F);
			this.buttonXLogo.Location = new System.Drawing.Point(10, 269);
			this.buttonXLogo.Name = "buttonXLogo";
			this.buttonXLogo.Size = new System.Drawing.Size(113, 27);
			this.buttonXLogo.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXLogo.TabIndex = 151;
			this.buttonXLogo.Text = "Logo";
			this.buttonXLogo.TextColor = System.Drawing.Color.Black;
			this.buttonXLogo.CheckedChanged += new System.EventHandler(this.OnSettingsChanged);
			// 
			// buttonXTotalCost
			// 
			this.buttonXTotalCost.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXTotalCost.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXTotalCost.AutoCheckOnClick = true;
			this.buttonXTotalCost.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXTotalCost.Font = new System.Drawing.Font("Arial", 9.75F);
			this.buttonXTotalCost.Location = new System.Drawing.Point(156, 217);
			this.buttonXTotalCost.Name = "buttonXTotalCost";
			this.buttonXTotalCost.Size = new System.Drawing.Size(113, 27);
			this.buttonXTotalCost.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXTotalCost.TabIndex = 148;
			this.buttonXTotalCost.Text = "Total Cost";
			this.buttonXTotalCost.TextColor = System.Drawing.Color.Black;
			this.buttonXTotalCost.CheckedChanged += new System.EventHandler(this.OnSettingsChanged);
			// 
			// buttonXTime
			// 
			this.buttonXTime.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXTime.AutoCheckOnClick = true;
			this.buttonXTime.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXTime.Font = new System.Drawing.Font("Arial", 9.75F);
			this.buttonXTime.Location = new System.Drawing.Point(10, 113);
			this.buttonXTime.Name = "buttonXTime";
			this.buttonXTime.Size = new System.Drawing.Size(113, 27);
			this.buttonXTime.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXTime.TabIndex = 150;
			this.buttonXTime.Text = "Time";
			this.buttonXTime.TextColor = System.Drawing.Color.Black;
			this.buttonXTime.CheckedChanged += new System.EventHandler(this.OnSettingsChanged);
			// 
			// buttonXProgram
			// 
			this.buttonXProgram.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXProgram.AutoCheckOnClick = true;
			this.buttonXProgram.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXProgram.Font = new System.Drawing.Font("Arial", 9.75F);
			this.buttonXProgram.Location = new System.Drawing.Point(10, 61);
			this.buttonXProgram.Name = "buttonXProgram";
			this.buttonXProgram.Size = new System.Drawing.Size(113, 27);
			this.buttonXProgram.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXProgram.TabIndex = 149;
			this.buttonXProgram.Text = "Program";
			this.buttonXProgram.TextColor = System.Drawing.Color.Black;
			this.buttonXProgram.CheckedChanged += new System.EventHandler(this.OnSettingsChanged);
			// 
			// buttonXWeeklySpots
			// 
			this.buttonXWeeklySpots.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXWeeklySpots.AutoCheckOnClick = true;
			this.buttonXWeeklySpots.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXWeeklySpots.Font = new System.Drawing.Font("Arial", 9.75F);
			this.buttonXWeeklySpots.Location = new System.Drawing.Point(10, 165);
			this.buttonXWeeklySpots.Name = "buttonXWeeklySpots";
			this.buttonXWeeklySpots.Size = new System.Drawing.Size(113, 27);
			this.buttonXWeeklySpots.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXWeeklySpots.TabIndex = 154;
			this.buttonXWeeklySpots.Text = "Weekly Spots";
			this.buttonXWeeklySpots.TextColor = System.Drawing.Color.Black;
			this.buttonXWeeklySpots.CheckedChanged += new System.EventHandler(this.OnSettingsChanged);
			// 
			// buttonXWeeklyCost
			// 
			this.buttonXWeeklyCost.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXWeeklyCost.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXWeeklyCost.AutoCheckOnClick = true;
			this.buttonXWeeklyCost.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXWeeklyCost.Font = new System.Drawing.Font("Arial", 9.75F);
			this.buttonXWeeklyCost.Location = new System.Drawing.Point(156, 165);
			this.buttonXWeeklyCost.Name = "buttonXWeeklyCost";
			this.buttonXWeeklyCost.Size = new System.Drawing.Size(113, 27);
			this.buttonXWeeklyCost.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXWeeklyCost.TabIndex = 155;
			this.buttonXWeeklyCost.Text = "Weekly Cost";
			this.buttonXWeeklyCost.TextColor = System.Drawing.Color.Black;
			this.buttonXWeeklyCost.CheckedChanged += new System.EventHandler(this.OnSettingsChanged);
			// 
			// SnapshotColumnSettingsControl
			// 
			this.Controls.Add(this.buttonXWeeklySpots);
			this.Controls.Add(this.buttonXWeeklyCost);
			this.Controls.Add(this.buttonXLength);
			this.Controls.Add(this.checkEditApplyForAll);
			this.Controls.Add(this.buttonXDaypart);
			this.Controls.Add(this.buttonXStation);
			this.Controls.Add(this.buttonXAvgRate);
			this.Controls.Add(this.buttonXTotalSpots);
			this.Controls.Add(this.buttonXLineId);
			this.Controls.Add(this.buttonXTotalRow);
			this.Controls.Add(this.buttonXRate);
			this.Controls.Add(this.buttonXLogo);
			this.Controls.Add(this.buttonXTotalCost);
			this.Controls.Add(this.buttonXTime);
			this.Controls.Add(this.buttonXProgram);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "SnapshotColumnSettingsControl";
			this.Size = new System.Drawing.Size(279, 394);
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditApplyForAll.Properties)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraEditors.StyleController styleController;
		public DevExpress.XtraEditors.CheckEdit checkEditApplyForAll;
		private DevComponents.DotNetBar.ButtonX buttonXStation;
		private DevComponents.DotNetBar.ButtonX buttonXAvgRate;
		private DevComponents.DotNetBar.ButtonX buttonXTotalSpots;
		private DevComponents.DotNetBar.ButtonX buttonXLineId;
		private DevComponents.DotNetBar.ButtonX buttonXLength;
		private DevComponents.DotNetBar.ButtonX buttonXTotalRow;
		private DevComponents.DotNetBar.ButtonX buttonXDaypart;
		private DevComponents.DotNetBar.ButtonX buttonXRate;
		private DevComponents.DotNetBar.ButtonX buttonXLogo;
		private DevComponents.DotNetBar.ButtonX buttonXTotalCost;
		private DevComponents.DotNetBar.ButtonX buttonXTime;
		private DevComponents.DotNetBar.ButtonX buttonXProgram;
		private DevComponents.DotNetBar.ButtonX buttonXWeeklySpots;
		private DevComponents.DotNetBar.ButtonX buttonXWeeklyCost;
	}
}
