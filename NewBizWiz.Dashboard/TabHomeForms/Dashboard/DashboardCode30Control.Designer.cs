namespace NewBizWiz.Dashboard.TabHomeForms.Dashboard
{
    partial class DashboardCode30Control
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
            this.pnTV = new System.Windows.Forms.Panel();
            this.pnDigital = new System.Windows.Forms.Panel();
            this.pnCalendar = new System.Windows.Forms.Panel();
            this.pbDigital = new System.Windows.Forms.PictureBox();
            this.pbTV = new System.Windows.Forms.PictureBox();
            this.pbCalendar = new System.Windows.Forms.PictureBox();
            this.pnTV.SuspendLayout();
            this.pnDigital.SuspendLayout();
            this.pnCalendar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbDigital)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbTV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCalendar)).BeginInit();
            this.SuspendLayout();
            // 
            // defaultLookAndFeel
            // 
            this.defaultLookAndFeel.LookAndFeel.SkinName = "Money Twins";
            // 
            // pnTV
            // 
            this.pnTV.Controls.Add(this.pbTV);
            this.pnTV.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnTV.Location = new System.Drawing.Point(0, 151);
            this.pnTV.Name = "pnTV";
            this.pnTV.Size = new System.Drawing.Size(831, 151);
            this.pnTV.TabIndex = 10;
            // 
            // pnDigital
            // 
            this.pnDigital.Controls.Add(this.pbDigital);
            this.pnDigital.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnDigital.Location = new System.Drawing.Point(0, 302);
            this.pnDigital.Name = "pnDigital";
            this.pnDigital.Size = new System.Drawing.Size(831, 151);
            this.pnDigital.TabIndex = 11;
            // 
            // pnCalendar
            // 
            this.pnCalendar.Controls.Add(this.pbCalendar);
            this.pnCalendar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnCalendar.Location = new System.Drawing.Point(0, 0);
            this.pnCalendar.Name = "pnCalendar";
            this.pnCalendar.Size = new System.Drawing.Size(831, 151);
            this.pnCalendar.TabIndex = 12;
            // 
            // pbDigital
            // 
            this.pbDigital.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbDigital.Image = global::NewBizWiz.Dashboard.Properties.Resources.HomeDigital;
            this.pbDigital.Location = new System.Drawing.Point(37, 12);
            this.pbDigital.Name = "pbDigital";
            this.pbDigital.Size = new System.Drawing.Size(703, 127);
            this.pbDigital.TabIndex = 3;
            this.pbDigital.TabStop = false;
            this.pbDigital.Click += new System.EventHandler(this.pbDigital_Click);
            this.pbDigital.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
            this.pbDigital.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseUp);
            // 
            // pbTV
            // 
            this.pbTV.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbTV.Image = global::NewBizWiz.Dashboard.Properties.Resources.HomeTV;
            this.pbTV.Location = new System.Drawing.Point(37, 12);
            this.pbTV.Name = "pbTV";
            this.pbTV.Size = new System.Drawing.Size(703, 127);
            this.pbTV.TabIndex = 2;
            this.pbTV.TabStop = false;
            this.pbTV.Click += new System.EventHandler(this.pbTV_Click);
            this.pbTV.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
            this.pbTV.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseUp);
            // 
            // pbCalendar
            // 
            this.pbCalendar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbCalendar.Image = global::NewBizWiz.Dashboard.Properties.Resources.HomeStar;
            this.pbCalendar.Location = new System.Drawing.Point(37, 12);
            this.pbCalendar.Name = "pbCalendar";
            this.pbCalendar.Size = new System.Drawing.Size(703, 127);
            this.pbCalendar.TabIndex = 4;
            this.pbCalendar.TabStop = false;
            this.pbCalendar.Click += new System.EventHandler(this.pbStar_Click);
            this.pbCalendar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
            this.pbCalendar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseUp);
            // 
            // DashboardCode18Control
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.pnDigital);
            this.Controls.Add(this.pnTV);
            this.Controls.Add(this.pnCalendar);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Name = "DashboardCode18Control";
            this.Size = new System.Drawing.Size(831, 458);
            this.pnTV.ResumeLayout(false);
            this.pnDigital.ResumeLayout(false);
            this.pnCalendar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbDigital)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbTV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCalendar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
        private System.Windows.Forms.Panel pnTV;
        private System.Windows.Forms.PictureBox pbTV;
        private System.Windows.Forms.Panel pnDigital;
        private System.Windows.Forms.PictureBox pbDigital;
        private System.Windows.Forms.Panel pnCalendar;
        private System.Windows.Forms.PictureBox pbCalendar;
    }
}
