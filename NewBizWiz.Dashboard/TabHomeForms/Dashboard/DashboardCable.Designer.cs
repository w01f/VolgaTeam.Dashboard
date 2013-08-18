namespace NewBizWiz.Dashboard.TabHomeForms.Dashboard
{
    partial class DashboardCable
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
			this.pbCalendar = new System.Windows.Forms.PictureBox();
			this.pbSellerPoint = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.pbOnline)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbCalendar)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbSellerPoint)).BeginInit();
			this.SuspendLayout();
			// 
			// defaultLookAndFeel
			// 
			this.defaultLookAndFeel.LookAndFeel.SkinName = "Money Twins";
			// 
			// pbOnline
			// 
			this.pbOnline.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.pbOnline.Cursor = System.Windows.Forms.Cursors.Hand;
			this.pbOnline.Image = global::NewBizWiz.Dashboard.Properties.Resources.HomeOnline;
			this.pbOnline.Location = new System.Drawing.Point(331, 71);
			this.pbOnline.Name = "pbOnline";
			this.pbOnline.Size = new System.Drawing.Size(330, 90);
			this.pbOnline.TabIndex = 2;
			this.pbOnline.TabStop = false;
			this.pbOnline.Click += new System.EventHandler(this.pbOnline_Click);
			this.pbOnline.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
			this.pbOnline.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseUp);
			// 
			// pbCalendar
			// 
			this.pbCalendar.Enabled = false;
			this.pbCalendar.Image = global::NewBizWiz.Dashboard.Properties.Resources.HomeCalendarDisabled;
			this.pbCalendar.Location = new System.Drawing.Point(55, 281);
			this.pbCalendar.Name = "pbCalendar";
			this.pbCalendar.Size = new System.Drawing.Size(420, 90);
			this.pbCalendar.TabIndex = 1;
			this.pbCalendar.TabStop = false;
			this.pbCalendar.Click += new System.EventHandler(this.pbCalendar_Click);
			// 
			// pbSellerPoint
			// 
			this.pbSellerPoint.Cursor = System.Windows.Forms.Cursors.Hand;
			this.pbSellerPoint.Image = global::NewBizWiz.Dashboard.Properties.Resources.HomeCable;
			this.pbSellerPoint.Location = new System.Drawing.Point(55, 71);
			this.pbSellerPoint.Name = "pbSellerPoint";
			this.pbSellerPoint.Size = new System.Drawing.Size(330, 90);
			this.pbSellerPoint.TabIndex = 0;
			this.pbSellerPoint.TabStop = false;
			this.pbSellerPoint.Click += new System.EventHandler(this.pbSellerPoint_Click);
			this.pbSellerPoint.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
			this.pbSellerPoint.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseUp);
			// 
			// DashboardCable
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
			this.Controls.Add(this.pbOnline);
			this.Controls.Add(this.pbCalendar);
			this.Controls.Add(this.pbSellerPoint);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "DashboardCable";
			this.Size = new System.Drawing.Size(731, 458);
			((System.ComponentModel.ISupportInitialize)(this.pbOnline)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbCalendar)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbSellerPoint)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

		private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
		private System.Windows.Forms.PictureBox pbSellerPoint;
		private System.Windows.Forms.PictureBox pbCalendar;
		private System.Windows.Forms.PictureBox pbOnline;
    }
}
