namespace NewBizWiz.Dashboard.TabHomeForms.Dashboard
{
    partial class DashboardCode11Control
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
            this.pnRadioBig = new System.Windows.Forms.Panel();
            this.pbRadioBig = new System.Windows.Forms.PictureBox();
            this.pnDigitalBig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbDigitalBig)).BeginInit();
            this.pnRadioBig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbRadioBig)).BeginInit();
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
            // pnRadioBig
            // 
            this.pnRadioBig.Controls.Add(this.pbRadioBig);
            this.pnRadioBig.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnRadioBig.Location = new System.Drawing.Point(0, 0);
            this.pnRadioBig.Name = "pnRadioBig";
            this.pnRadioBig.Size = new System.Drawing.Size(920, 230);
            this.pnRadioBig.TabIndex = 18;
            // 
            // pbRadioBig
            // 
            this.pbRadioBig.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbRadioBig.Image = global::NewBizWiz.Dashboard.Properties.Resources.HomeRadioBig;
            this.pbRadioBig.Location = new System.Drawing.Point(19, 17);
            this.pbRadioBig.Name = "pbRadioBig";
            this.pbRadioBig.Size = new System.Drawing.Size(886, 197);
            this.pbRadioBig.TabIndex = 3;
            this.pbRadioBig.TabStop = false;
            this.pbRadioBig.Click += new System.EventHandler(this.pbRadio_Click);
            this.pbRadioBig.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
            this.pbRadioBig.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseUp);
            // 
            // DashboardCode11Control
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.pnDigitalBig);
            this.Controls.Add(this.pnRadioBig);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Name = "DashboardCode11Control";
            this.Size = new System.Drawing.Size(920, 458);
            this.pnDigitalBig.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbDigitalBig)).EndInit();
            this.pnRadioBig.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbRadioBig)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
        private System.Windows.Forms.Panel pnDigitalBig;
        private System.Windows.Forms.PictureBox pbDigitalBig;
        private System.Windows.Forms.Panel pnRadioBig;
        private System.Windows.Forms.PictureBox pbRadioBig;
    }
}
