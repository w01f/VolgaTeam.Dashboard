﻿namespace Asa.Media.Controls.PresentationClasses.Digital.Settings
{
	partial class DigitalListSettingsControl
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
		private  void InitializeComponent()
		{
			this.buttonXTargeting = new DevComponents.DotNetBar.ButtonX();
			this.buttonXRichMedia = new DevComponents.DotNetBar.ButtonX();
			this.buttonXStrategy = new DevComponents.DotNetBar.ButtonX();
			this.buttonXLocation = new DevComponents.DotNetBar.ButtonX();
			this.buttonXDimensions = new DevComponents.DotNetBar.ButtonX();
			this.SuspendLayout();
			// 
			// buttonXTargeting
			// 
			this.buttonXTargeting.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXTargeting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXTargeting.AutoCheckOnClick = true;
			this.buttonXTargeting.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXTargeting.Font = new System.Drawing.Font("Arial", 9.75F);
			this.buttonXTargeting.Location = new System.Drawing.Point(157, 81);
			this.buttonXTargeting.Name = "buttonXTargeting";
			this.buttonXTargeting.Size = new System.Drawing.Size(113, 45);
			this.buttonXTargeting.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXTargeting.TabIndex = 48;
			this.buttonXTargeting.Text = "Targeting";
			this.buttonXTargeting.TextColor = System.Drawing.Color.Black;
			this.buttonXTargeting.UseMnemonic = false;
			this.buttonXTargeting.CheckedChanged += new System.EventHandler(this.OnSettingsChanged);
			// 
			// buttonXRichMedia
			// 
			this.buttonXRichMedia.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXRichMedia.AutoCheckOnClick = true;
			this.buttonXRichMedia.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXRichMedia.Font = new System.Drawing.Font("Arial", 9.75F);
			this.buttonXRichMedia.Location = new System.Drawing.Point(20, 149);
			this.buttonXRichMedia.Name = "buttonXRichMedia";
			this.buttonXRichMedia.Size = new System.Drawing.Size(113, 45);
			this.buttonXRichMedia.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXRichMedia.TabIndex = 43;
			this.buttonXRichMedia.Text = "Rich Media";
			this.buttonXRichMedia.TextColor = System.Drawing.Color.Black;
			this.buttonXRichMedia.UseMnemonic = false;
			this.buttonXRichMedia.CheckedChanged += new System.EventHandler(this.OnSettingsChanged);
			// 
			// buttonXStrategy
			// 
			this.buttonXStrategy.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXStrategy.AutoCheckOnClick = true;
			this.buttonXStrategy.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXStrategy.Font = new System.Drawing.Font("Arial", 9.75F);
			this.buttonXStrategy.Location = new System.Drawing.Point(20, 81);
			this.buttonXStrategy.Name = "buttonXStrategy";
			this.buttonXStrategy.Size = new System.Drawing.Size(113, 45);
			this.buttonXStrategy.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXStrategy.TabIndex = 42;
			this.buttonXStrategy.Text = "Pricing Strategy";
			this.buttonXStrategy.TextColor = System.Drawing.Color.Black;
			this.buttonXStrategy.UseMnemonic = false;
			this.buttonXStrategy.CheckedChanged += new System.EventHandler(this.OnSettingsChanged);
			// 
			// buttonXLocation
			// 
			this.buttonXLocation.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXLocation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXLocation.AutoCheckOnClick = true;
			this.buttonXLocation.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXLocation.Font = new System.Drawing.Font("Arial", 9.75F);
			this.buttonXLocation.Location = new System.Drawing.Point(157, 13);
			this.buttonXLocation.Name = "buttonXLocation";
			this.buttonXLocation.Size = new System.Drawing.Size(113, 45);
			this.buttonXLocation.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXLocation.TabIndex = 40;
			this.buttonXLocation.Text = "Location";
			this.buttonXLocation.TextColor = System.Drawing.Color.Black;
			this.buttonXLocation.UseMnemonic = false;
			this.buttonXLocation.CheckedChanged += new System.EventHandler(this.OnSettingsChanged);
			// 
			// buttonXDimensions
			// 
			this.buttonXDimensions.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXDimensions.AutoCheckOnClick = true;
			this.buttonXDimensions.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXDimensions.Font = new System.Drawing.Font("Arial", 9.75F);
			this.buttonXDimensions.Location = new System.Drawing.Point(20, 13);
			this.buttonXDimensions.Name = "buttonXDimensions";
			this.buttonXDimensions.Size = new System.Drawing.Size(113, 45);
			this.buttonXDimensions.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXDimensions.TabIndex = 39;
			this.buttonXDimensions.Text = "Ad Dimensions";
			this.buttonXDimensions.TextColor = System.Drawing.Color.Black;
			this.buttonXDimensions.UseMnemonic = false;
			this.buttonXDimensions.CheckedChanged += new System.EventHandler(this.OnSettingsChanged);
			// 
			// DigitalListSettingsControl
			// 
			this.Appearance.PageClient.BackColor = System.Drawing.Color.White;
			this.Appearance.PageClient.Options.UseBackColor = true;
			this.Controls.Add(this.buttonXDimensions);
			this.Controls.Add(this.buttonXLocation);
			this.Controls.Add(this.buttonXTargeting);
			this.Controls.Add(this.buttonXStrategy);
			this.Controls.Add(this.buttonXRichMedia);
			this.Size = new System.Drawing.Size(291, 490);
			this.ResumeLayout(false);

		}

		#endregion
		private DevComponents.DotNetBar.ButtonX buttonXTargeting;
		private DevComponents.DotNetBar.ButtonX buttonXRichMedia;
		private DevComponents.DotNetBar.ButtonX buttonXStrategy;
		private DevComponents.DotNetBar.ButtonX buttonXLocation;
		private DevComponents.DotNetBar.ButtonX buttonXDimensions;
	}
}
