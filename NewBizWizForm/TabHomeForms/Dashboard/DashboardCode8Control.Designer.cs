namespace NewBizWizForm.TabHomeForms.Dashboard
{
    partial class DashboardCode8Control
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
            this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel();
            this.pnMobileBig = new System.Windows.Forms.Panel();
            this.pbMobileBig = new System.Windows.Forms.PictureBox();
            this.pnOnlineBig = new System.Windows.Forms.Panel();
            this.pbOnlineBig = new System.Windows.Forms.PictureBox();
            this.pnMobileBig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbMobileBig)).BeginInit();
            this.pnOnlineBig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbOnlineBig)).BeginInit();
            this.SuspendLayout();
            // 
            // defaultLookAndFeel
            // 
            this.defaultLookAndFeel.LookAndFeel.SkinName = "Money Twins";
            // 
            // pnMobileBig
            // 
            this.pnMobileBig.Controls.Add(this.pbMobileBig);
            this.pnMobileBig.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnMobileBig.Location = new System.Drawing.Point(0, 230);
            this.pnMobileBig.Name = "pnMobileBig";
            this.pnMobileBig.Size = new System.Drawing.Size(731, 230);
            this.pnMobileBig.TabIndex = 17;
            // 
            // pbMobileBig
            // 
            this.pbMobileBig.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbMobileBig.Image = global::NewBizWizForm.Properties.Resources.HomeMobileBig;
            this.pbMobileBig.Location = new System.Drawing.Point(30, 17);
            this.pbMobileBig.Name = "pbMobileBig";
            this.pbMobileBig.Size = new System.Drawing.Size(771, 197);
            this.pbMobileBig.TabIndex = 4;
            this.pbMobileBig.TabStop = false;
            this.pbMobileBig.Click += new System.EventHandler(this.pbMobile_Click);
            this.pbMobileBig.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
            this.pbMobileBig.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseUp);
            // 
            // pnOnlineBig
            // 
            this.pnOnlineBig.Controls.Add(this.pbOnlineBig);
            this.pnOnlineBig.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnOnlineBig.Location = new System.Drawing.Point(0, 0);
            this.pnOnlineBig.Name = "pnOnlineBig";
            this.pnOnlineBig.Size = new System.Drawing.Size(731, 230);
            this.pnOnlineBig.TabIndex = 18;
            // 
            // pbOnlineBig
            // 
            this.pbOnlineBig.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbOnlineBig.Image = global::NewBizWizForm.Properties.Resources.HomeOnlineBig;
            this.pbOnlineBig.Location = new System.Drawing.Point(20, 17);
            this.pbOnlineBig.Name = "pbOnlineBig";
            this.pbOnlineBig.Size = new System.Drawing.Size(771, 197);
            this.pbOnlineBig.TabIndex = 3;
            this.pbOnlineBig.TabStop = false;
            this.pbOnlineBig.Click += new System.EventHandler(this.pbOnline_Click);
            this.pbOnlineBig.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
            this.pbOnlineBig.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseUp);
            // 
            // DashboardCode8Control
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.pnMobileBig);
            this.Controls.Add(this.pnOnlineBig);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Name = "DashboardCode8Control";
            this.Size = new System.Drawing.Size(731, 458);
            this.pnMobileBig.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbMobileBig)).EndInit();
            this.pnOnlineBig.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbOnlineBig)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
        private System.Windows.Forms.Panel pnMobileBig;
        private System.Windows.Forms.PictureBox pbMobileBig;
        private System.Windows.Forms.Panel pnOnlineBig;
        private System.Windows.Forms.PictureBox pbOnlineBig;
    }
}
