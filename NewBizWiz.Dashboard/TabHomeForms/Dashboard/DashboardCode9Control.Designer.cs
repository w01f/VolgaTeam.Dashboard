namespace NewBizWiz.Dashboard.TabHomeForms.Dashboard
{
    partial class DashboardCode9Control
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
            this.pbDigitalBig = new System.Windows.Forms.PictureBox();
            this.pnNewspaperBig = new System.Windows.Forms.Panel();
            this.pbNewspaperBig = new System.Windows.Forms.PictureBox();
            this.pnDigitalBig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbDigitalBig)).BeginInit();
            this.pnNewspaperBig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbNewspaperBig)).BeginInit();
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
            // pnNewspaperBig
            // 
            this.pnNewspaperBig.Controls.Add(this.pbNewspaperBig);
            this.pnNewspaperBig.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnNewspaperBig.Location = new System.Drawing.Point(0, 0);
            this.pnNewspaperBig.Name = "pnNewspaperBig";
            this.pnNewspaperBig.Size = new System.Drawing.Size(920, 230);
            this.pnNewspaperBig.TabIndex = 18;
            // 
            // pbNewspaperBig
            // 
            this.pbNewspaperBig.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbNewspaperBig.Image = global::NewBizWiz.Dashboard.Properties.Resources.HomeNewspaperBig;
            this.pbNewspaperBig.Location = new System.Drawing.Point(19, 17);
            this.pbNewspaperBig.Name = "pbNewspaperBig";
            this.pbNewspaperBig.Size = new System.Drawing.Size(886, 197);
            this.pbNewspaperBig.TabIndex = 3;
            this.pbNewspaperBig.TabStop = false;
            this.pbNewspaperBig.Click += new System.EventHandler(this.pbNewspaper_Click);
            this.pbNewspaperBig.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
            this.pbNewspaperBig.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseUp);
            // 
            // DashboardCode9Control
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.pnDigitalBig);
            this.Controls.Add(this.pnNewspaperBig);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Name = "DashboardCode9Control";
            this.Size = new System.Drawing.Size(920, 458);
            this.pnDigitalBig.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbDigitalBig)).EndInit();
            this.pnNewspaperBig.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbNewspaperBig)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
        private System.Windows.Forms.Panel pnDigitalBig;
        private System.Windows.Forms.PictureBox pbDigitalBig;
        private System.Windows.Forms.Panel pnNewspaperBig;
        private System.Windows.Forms.PictureBox pbNewspaperBig;
    }
}
