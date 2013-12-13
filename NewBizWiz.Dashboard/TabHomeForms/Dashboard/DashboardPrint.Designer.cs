﻿namespace NewBizWiz.Dashboard.TabHomeForms.Dashboard
{
    partial class DashboardPrint
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
			this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
			this.pbOnline = new System.Windows.Forms.PictureBox();
			this.pbSellerPoint = new System.Windows.Forms.PictureBox();
			this.pbCalendar = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.pbOnline)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbSellerPoint)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbCalendar)).BeginInit();
			this.SuspendLayout();
			// 
			// defaultLookAndFeel
			// 
			this.defaultLookAndFeel.LookAndFeel.SkinName = "Money Twins";
			// 
			// pbOnline
			// 
			this.pbOnline.Cursor = System.Windows.Forms.Cursors.Hand;
			this.pbOnline.Image = global::NewBizWiz.Dashboard.Properties.Resources.HomeOnline;
			this.pbOnline.Location = new System.Drawing.Point(57, 182);
			this.pbOnline.Name = "pbOnline";
			this.pbOnline.Size = new System.Drawing.Size(452, 90);
			this.pbOnline.TabIndex = 2;
			this.pbOnline.TabStop = false;
			this.pbOnline.Click += new System.EventHandler(this.pbOnline_Click);
			this.pbOnline.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
			this.pbOnline.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseUp);
			// 
			// pbSellerPoint
			// 
			this.pbSellerPoint.Cursor = System.Windows.Forms.Cursors.Hand;
			this.pbSellerPoint.Image = global::NewBizWiz.Dashboard.Properties.Resources.HomeNewspaper;
			this.pbSellerPoint.Location = new System.Drawing.Point(57, 39);
			this.pbSellerPoint.Name = "pbSellerPoint";
			this.pbSellerPoint.Size = new System.Drawing.Size(452, 90);
			this.pbSellerPoint.TabIndex = 0;
			this.pbSellerPoint.TabStop = false;
			this.pbSellerPoint.Click += new System.EventHandler(this.pbSellerPoint_Click);
			this.pbSellerPoint.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
			this.pbSellerPoint.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseUp);
			// 
			// pbCalendar
			// 
			this.pbCalendar.Cursor = System.Windows.Forms.Cursors.Hand;
			this.pbCalendar.Image = global::NewBizWiz.Dashboard.Properties.Resources.HomeCalendar;
			this.pbCalendar.Location = new System.Drawing.Point(57, 325);
			this.pbCalendar.Name = "pbCalendar";
			this.pbCalendar.Size = new System.Drawing.Size(452, 90);
			this.pbCalendar.TabIndex = 1;
			this.pbCalendar.TabStop = false;
			this.pbCalendar.Click += new System.EventHandler(this.pbCalendar_Click_1);
			this.pbCalendar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
			this.pbCalendar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseUp);
			// 
			// DashboardPrint
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
			this.Controls.Add(this.pbOnline);
			this.Controls.Add(this.pbSellerPoint);
			this.Controls.Add(this.pbCalendar);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "DashboardPrint";
			this.Size = new System.Drawing.Size(731, 458);
			((System.ComponentModel.ISupportInitialize)(this.pbOnline)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbSellerPoint)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbCalendar)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

		private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
		private System.Windows.Forms.PictureBox pbSellerPoint;
		private System.Windows.Forms.PictureBox pbCalendar;
		private System.Windows.Forms.PictureBox pbOnline;
    }
}
