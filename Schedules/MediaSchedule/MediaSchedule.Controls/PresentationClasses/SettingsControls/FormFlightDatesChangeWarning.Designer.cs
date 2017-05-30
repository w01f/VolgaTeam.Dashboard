namespace Asa.Media.Controls.PresentationClasses.SettingsControls
{
	partial class FormFlightDatesChangeWarning
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
			this.pbLogo = new System.Windows.Forms.PictureBox();
			this.buttonXSave = new DevComponents.DotNetBar.ButtonX();
			this.buttonXCancel = new DevComponents.DotNetBar.ButtonX();
			this.styleController = new DevExpress.XtraEditors.StyleController();
			this.pnTop = new System.Windows.Forms.Panel();
			this.labelControlTitle = new DevExpress.XtraEditors.LabelControl();
			this.pnBottomButtons = new System.Windows.Forms.Panel();
			this.buttonXKeepSpots = new DevComponents.DotNetBar.ButtonX();
			this.buttonXDoNotKeepSpots = new DevComponents.DotNetBar.ButtonX();
			((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			this.pnTop.SuspendLayout();
			this.pnBottomButtons.SuspendLayout();
			this.SuspendLayout();
			// 
			// pbLogo
			// 
			this.pbLogo.ForeColor = System.Drawing.Color.Black;
			this.pbLogo.Image = global::Asa.Media.Controls.Properties.Resources.FlightDatesFormEditLogo;
			this.pbLogo.Location = new System.Drawing.Point(20, 13);
			this.pbLogo.Name = "pbLogo";
			this.pbLogo.Size = new System.Drawing.Size(66, 65);
			this.pbLogo.TabIndex = 0;
			this.pbLogo.TabStop = false;
			// 
			// buttonXSave
			// 
			this.buttonXSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXSave.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXSave.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonXSave.Location = new System.Drawing.Point(249, 7);
			this.buttonXSave.Name = "buttonXSave";
			this.buttonXSave.Size = new System.Drawing.Size(85, 36);
			this.buttonXSave.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXSave.TabIndex = 2;
			this.buttonXSave.Text = "Save";
			// 
			// buttonXCancel
			// 
			this.buttonXCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonXCancel.Location = new System.Drawing.Point(364, 7);
			this.buttonXCancel.Name = "buttonXCancel";
			this.buttonXCancel.Size = new System.Drawing.Size(85, 36);
			this.buttonXCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXCancel.TabIndex = 3;
			this.buttonXCancel.Text = "Cancel";
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
			// pnTop
			// 
			this.pnTop.BackColor = System.Drawing.Color.Transparent;
			this.pnTop.Controls.Add(this.labelControlTitle);
			this.pnTop.Controls.Add(this.pbLogo);
			this.pnTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnTop.ForeColor = System.Drawing.Color.Black;
			this.pnTop.Location = new System.Drawing.Point(0, 0);
			this.pnTop.Name = "pnTop";
			this.pnTop.Size = new System.Drawing.Size(461, 118);
			this.pnTop.TabIndex = 5;
			// 
			// labelControlTitle
			// 
			this.labelControlTitle.AllowHtmlString = true;
			this.labelControlTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelControlTitle.Appearance.BackColor = System.Drawing.Color.White;
			this.labelControlTitle.Appearance.ForeColor = System.Drawing.Color.Black;
			this.labelControlTitle.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.labelControlTitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.labelControlTitle.Location = new System.Drawing.Point(105, 13);
			this.labelControlTitle.Name = "labelControlTitle";
			this.labelControlTitle.Size = new System.Drawing.Size(336, 93);
			this.labelControlTitle.StyleController = this.styleController;
			this.labelControlTitle.TabIndex = 1;
			this.labelControlTitle.Text = "<size=+6>You are changing your dates:</size>\r\n\r\n<color=gray><size=+4>And there ar" +
    "e {0}ly spots already placed in your schedule...</size></color>";
			// 
			// pnBottomButtons
			// 
			this.pnBottomButtons.BackColor = System.Drawing.Color.Transparent;
			this.pnBottomButtons.Controls.Add(this.buttonXSave);
			this.pnBottomButtons.Controls.Add(this.buttonXCancel);
			this.pnBottomButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnBottomButtons.ForeColor = System.Drawing.Color.Black;
			this.pnBottomButtons.Location = new System.Drawing.Point(0, 303);
			this.pnBottomButtons.Name = "pnBottomButtons";
			this.pnBottomButtons.Size = new System.Drawing.Size(461, 51);
			this.pnBottomButtons.TabIndex = 6;
			// 
			// buttonXKeepSpots
			// 
			this.buttonXKeepSpots.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXKeepSpots.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXKeepSpots.Checked = true;
			this.buttonXKeepSpots.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXKeepSpots.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonXKeepSpots.Location = new System.Drawing.Point(43, 149);
			this.buttonXKeepSpots.Name = "buttonXKeepSpots";
			this.buttonXKeepSpots.Size = new System.Drawing.Size(375, 36);
			this.buttonXKeepSpots.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXKeepSpots.TabIndex = 7;
			this.buttonXKeepSpots.Text = "Keep my Spot Count Placement";
			this.buttonXKeepSpots.Click += new System.EventHandler(this.OnKeepSpotsClick);
			// 
			// buttonXDoNotKeepSpots
			// 
			this.buttonXDoNotKeepSpots.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXDoNotKeepSpots.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXDoNotKeepSpots.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXDoNotKeepSpots.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonXDoNotKeepSpots.Location = new System.Drawing.Point(43, 224);
			this.buttonXDoNotKeepSpots.Name = "buttonXDoNotKeepSpots";
			this.buttonXDoNotKeepSpots.Size = new System.Drawing.Size(375, 36);
			this.buttonXDoNotKeepSpots.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXDoNotKeepSpots.TabIndex = 8;
			this.buttonXDoNotKeepSpots.Text = "Keep my Programs & Wipe my Spots";
			this.buttonXDoNotKeepSpots.UseMnemonic = false;
			this.buttonXDoNotKeepSpots.Click += new System.EventHandler(this.OnKeepSpotsClick);
			// 
			// FormFlightDatesChangeWarning
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.ClientSize = new System.Drawing.Size(461, 354);
			this.Controls.Add(this.buttonXDoNotKeepSpots);
			this.Controls.Add(this.buttonXKeepSpots);
			this.Controls.Add(this.pnBottomButtons);
			this.Controls.Add(this.pnTop);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormFlightDatesChangeWarning";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Schedule Update Warning";
			((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			this.pnTop.ResumeLayout(false);
			this.pnBottomButtons.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox pbLogo;
		private DevComponents.DotNetBar.ButtonX buttonXSave;
		private DevComponents.DotNetBar.ButtonX buttonXCancel;
		private DevExpress.XtraEditors.StyleController styleController;
		private System.Windows.Forms.Panel pnTop;
		private System.Windows.Forms.Panel pnBottomButtons;
		private DevExpress.XtraEditors.LabelControl labelControlTitle;
		private DevComponents.DotNetBar.ButtonX buttonXKeepSpots;
		private DevComponents.DotNetBar.ButtonX buttonXDoNotKeepSpots;
	}
}