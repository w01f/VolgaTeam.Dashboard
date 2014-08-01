namespace NewBizWiz.Dashboard.TabHomeForms.Dashboard
{
    partial class DashboardTV
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
			this.pbOnline = new System.Windows.Forms.PictureBox();
			this.pbSellerPoint = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.pbOnline)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbSellerPoint)).BeginInit();
			this.SuspendLayout();
			// 
			// pbOnline
			// 
			this.pbOnline.Cursor = System.Windows.Forms.Cursors.Hand;
			this.pbOnline.Image = global::NewBizWiz.Dashboard.Properties.Resources.HomeOnline;
			this.pbOnline.Location = new System.Drawing.Point(55, 271);
			this.pbOnline.Name = "pbOnline";
			this.pbOnline.Size = new System.Drawing.Size(493, 90);
			this.pbOnline.TabIndex = 2;
			this.pbOnline.TabStop = false;
			this.pbOnline.Click += new System.EventHandler(this.pbOnline_Click);
			this.pbOnline.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
			this.pbOnline.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseUp);
			// 
			// pbSellerPoint
			// 
			this.pbSellerPoint.Cursor = System.Windows.Forms.Cursors.Hand;
			this.pbSellerPoint.Image = global::NewBizWiz.Dashboard.Properties.Resources.HomeTV;
			this.pbSellerPoint.Location = new System.Drawing.Point(55, 71);
			this.pbSellerPoint.Name = "pbSellerPoint";
			this.pbSellerPoint.Size = new System.Drawing.Size(494, 90);
			this.pbSellerPoint.TabIndex = 0;
			this.pbSellerPoint.TabStop = false;
			this.pbSellerPoint.Click += new System.EventHandler(this.pbSellerPoint_Click);
			this.pbSellerPoint.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
			this.pbSellerPoint.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseUp);
			// 
			// DashboardTV
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.pbOnline);
			this.Controls.Add(this.pbSellerPoint);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "DashboardTV";
			this.Size = new System.Drawing.Size(731, 458);
			((System.ComponentModel.ISupportInitialize)(this.pbOnline)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbSellerPoint)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

		private System.Windows.Forms.PictureBox pbSellerPoint;
		private System.Windows.Forms.PictureBox pbOnline;
    }
}
