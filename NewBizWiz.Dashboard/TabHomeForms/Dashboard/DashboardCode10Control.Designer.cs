namespace NewBizWiz.Dashboard.TabHomeForms.Dashboard
{
    partial class DashboardCode10Control
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
            this.pnDigitalBig = new System.Windows.Forms.Panel();
            this.pnTVBig = new System.Windows.Forms.Panel();
            this.pbDigitalBig = new System.Windows.Forms.PictureBox();
            this.pbTVBig = new System.Windows.Forms.PictureBox();
            this.pnDigitalBig.SuspendLayout();
            this.pnTVBig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbDigitalBig)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbTVBig)).BeginInit();
            this.SuspendLayout();
            // 
            // defaultLookAndFeel
            // 
            this.defaultLookAndFeel.LookAndFeel.SkinName = "Money Twins";
            // 
            // pnDigitalBig
            // 
            this.pnDigitalBig.Controls.Add(this.pbDigitalBig);
            this.pnDigitalBig.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnDigitalBig.Location = new System.Drawing.Point(0, 230);
            this.pnDigitalBig.Name = "pnDigitalBig";
            this.pnDigitalBig.Size = new System.Drawing.Size(920, 230);
            this.pnDigitalBig.TabIndex = 17;
            // 
            // pnTVBig
            // 
            this.pnTVBig.Controls.Add(this.pbTVBig);
            this.pnTVBig.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnTVBig.Location = new System.Drawing.Point(0, 0);
            this.pnTVBig.Name = "pnTVBig";
            this.pnTVBig.Size = new System.Drawing.Size(920, 230);
            this.pnTVBig.TabIndex = 18;
            // 
            // pbDigitalBig
            // 
            this.pbDigitalBig.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbDigitalBig.Image = global::NewBizWiz.Dashboard.Properties.Resources.HomeDigitalBig;
            this.pbDigitalBig.Location = new System.Drawing.Point(19, 17);
            this.pbDigitalBig.Name = "pbDigitalBig";
            this.pbDigitalBig.Size = new System.Drawing.Size(886, 197);
            this.pbDigitalBig.TabIndex = 4;
            this.pbDigitalBig.TabStop = false;
            this.pbDigitalBig.Click += new System.EventHandler(this.pbDigitalBig_Click);
            this.pbDigitalBig.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
            this.pbDigitalBig.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseUp);
            // 
            // pbTVBig
            // 
            this.pbTVBig.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbTVBig.Image = global::NewBizWiz.Dashboard.Properties.Resources.HomeTVBig;
            this.pbTVBig.Location = new System.Drawing.Point(19, 17);
            this.pbTVBig.Name = "pbTVBig";
            this.pbTVBig.Size = new System.Drawing.Size(886, 197);
            this.pbTVBig.TabIndex = 3;
            this.pbTVBig.TabStop = false;
            this.pbTVBig.Click += new System.EventHandler(this.pbTV_Click);
            this.pbTVBig.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
            this.pbTVBig.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseUp);
            // 
            // DashboardCode10Control
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.pnDigitalBig);
            this.Controls.Add(this.pnTVBig);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Name = "DashboardCode10Control";
            this.Size = new System.Drawing.Size(920, 458);
            this.pnDigitalBig.ResumeLayout(false);
            this.pnTVBig.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbDigitalBig)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbTVBig)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
        private System.Windows.Forms.Panel pnDigitalBig;
        private System.Windows.Forms.PictureBox pbDigitalBig;
        private System.Windows.Forms.Panel pnTVBig;
        private System.Windows.Forms.PictureBox pbTVBig;
    }
}
