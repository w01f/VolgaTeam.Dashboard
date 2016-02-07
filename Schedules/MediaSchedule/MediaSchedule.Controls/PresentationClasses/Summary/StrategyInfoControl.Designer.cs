﻿namespace Asa.Media.Controls.PresentationClasses.Summary
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
			// StrategyInfoControl
			// 
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.checkEditStation);
			this.Controls.Add(this.checkEditTotalsSpots);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "StrategyInfoControl";
			this.Size = new System.Drawing.Size(296, 457);
			((System.ComponentModel.ISupportInitialize)(this.checkEditTotalsSpots.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditStation.Properties)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		protected DevExpress.XtraEditors.CheckEdit checkEditTotalsSpots;
		protected DevExpress.XtraEditors.CheckEdit checkEditStation;


	}
}
