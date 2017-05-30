namespace Asa.Dashboard.TabHomeForms.Dashboard
{
	partial class DashboardDefault
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
			this.pbSellerPoint = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.pbSellerPoint)).BeginInit();
			this.SuspendLayout();
			// 
			// pbSellerPoint
			// 
			this.pbSellerPoint.Cursor = System.Windows.Forms.Cursors.Default;
			this.pbSellerPoint.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pbSellerPoint.Image = global::Asa.Dashboard.Properties.Resources.HomeDefault;
			this.pbSellerPoint.Location = new System.Drawing.Point(10, 10);
			this.pbSellerPoint.Name = "pbSellerPoint";
			this.pbSellerPoint.Size = new System.Drawing.Size(711, 438);
			this.pbSellerPoint.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pbSellerPoint.TabIndex = 0;
			this.pbSellerPoint.TabStop = false;
			// 
			// DashboardDefault
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.pbSellerPoint);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "DashboardDefault";
			this.Padding = new System.Windows.Forms.Padding(10);
			this.Size = new System.Drawing.Size(731, 458);
			((System.ComponentModel.ISupportInitialize)(this.pbSellerPoint)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

		private System.Windows.Forms.PictureBox pbSellerPoint;
    }
}
