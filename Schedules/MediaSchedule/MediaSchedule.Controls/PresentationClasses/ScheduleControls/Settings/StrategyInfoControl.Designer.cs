namespace Asa.Media.Controls.PresentationClasses.ScheduleControls.Settings
{
	partial class StrategyInfoControl
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
			this.checkEditTotalsSpots = new DevExpress.XtraEditors.CheckEdit();
			this.checkEditStation = new DevExpress.XtraEditors.CheckEdit();
			this.buttonXResetLogos = new DevComponents.DotNetBar.ButtonX();
			((System.ComponentModel.ISupportInitialize)(this.checkEditTotalsSpots.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditStation.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// checkEditTotalsSpots
			// 
			this.checkEditTotalsSpots.Location = new System.Drawing.Point(15, 18);
			this.checkEditTotalsSpots.Name = "checkEditTotalsSpots";
			this.checkEditTotalsSpots.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.checkEditTotalsSpots.Properties.Appearance.Options.UseFont = true;
			this.checkEditTotalsSpots.Properties.AutoWidth = true;
			this.checkEditTotalsSpots.Properties.Caption = "Total Spots";
			this.checkEditTotalsSpots.Size = new System.Drawing.Size(88, 20);
			this.checkEditTotalsSpots.TabIndex = 8;
			this.checkEditTotalsSpots.CheckedChanged += new System.EventHandler(this.OnSettingChanged);
			// 
			// checkEditStation
			// 
			this.checkEditStation.Location = new System.Drawing.Point(15, 67);
			this.checkEditStation.Name = "checkEditStation";
			this.checkEditStation.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.checkEditStation.Properties.Appearance.Options.UseFont = true;
			this.checkEditStation.Properties.AutoWidth = true;
			this.checkEditStation.Properties.Caption = "Station";
			this.checkEditStation.Size = new System.Drawing.Size(63, 20);
			this.checkEditStation.TabIndex = 9;
			this.checkEditStation.CheckedChanged += new System.EventHandler(this.OnSettingChanged);
			// 
			// buttonXResetLogos
			// 
			this.buttonXResetLogos.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXResetLogos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXResetLogos.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXResetLogos.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonXResetLogos.Location = new System.Drawing.Point(15, 411);
			this.buttonXResetLogos.Name = "buttonXResetLogos";
			this.buttonXResetLogos.Size = new System.Drawing.Size(267, 34);
			this.buttonXResetLogos.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXResetLogos.TabIndex = 10;
			this.buttonXResetLogos.Text = "Refresh Logos";
			this.buttonXResetLogos.Click += new System.EventHandler(this.OnResetLogosClick);
			// 
			// StrategyInfoControl
			// 
			this.Appearance.PageClient.BackColor = System.Drawing.Color.White;
			this.Appearance.PageClient.Options.UseBackColor = true;
			this.Controls.Add(this.buttonXResetLogos);
			this.Controls.Add(this.checkEditStation);
			this.Controls.Add(this.checkEditTotalsSpots);
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Size = new System.Drawing.Size(296, 457);
			((System.ComponentModel.ISupportInitialize)(this.checkEditTotalsSpots.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditStation.Properties)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		protected DevExpress.XtraEditors.CheckEdit checkEditTotalsSpots;
		protected DevExpress.XtraEditors.CheckEdit checkEditStation;
		private DevComponents.DotNetBar.ButtonX buttonXResetLogos;
	}
}
