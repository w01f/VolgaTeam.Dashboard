namespace RadioScheduleBuilder.CustomControls
{
    partial class HomeControl
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
            this.pnBottom = new System.Windows.Forms.Panel();
            this.pnStations = new System.Windows.Forms.Panel();
            this.stationsControl = new RadioScheduleBuilder.CustomControls.StationsControl();
            this.pnDayparts = new System.Windows.Forms.Panel();
            this.daypartsControl = new RadioScheduleBuilder.CustomControls.DaypartsControl();
            this.pbBottomDescription = new System.Windows.Forms.PictureBox();
            this.pbMonthlySchedule = new System.Windows.Forms.PictureBox();
            this.pbWeeklySchedule = new System.Windows.Forms.PictureBox();
            this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel();
            this.xtraTabControl = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPageRadio = new DevExpress.XtraTab.XtraTabPage();
            this.pnRadio = new System.Windows.Forms.Panel();
            this.xtraTabPageDigital = new DevExpress.XtraTab.XtraTabPage();
            this.pnDigital = new System.Windows.Forms.Panel();
            this.pnBottom.SuspendLayout();
            this.pnStations.SuspendLayout();
            this.pnDayparts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbBottomDescription)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMonthlySchedule)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbWeeklySchedule)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).BeginInit();
            this.xtraTabControl.SuspendLayout();
            this.xtraTabPageRadio.SuspendLayout();
            this.pnRadio.SuspendLayout();
            this.xtraTabPageDigital.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnBottom
            // 
            this.pnBottom.Controls.Add(this.pnStations);
            this.pnBottom.Controls.Add(this.pnDayparts);
            this.pnBottom.Controls.Add(this.pbBottomDescription);
            this.pnBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnBottom.Location = new System.Drawing.Point(0, 315);
            this.pnBottom.Name = "pnBottom";
            this.pnBottom.Size = new System.Drawing.Size(1018, 325);
            this.pnBottom.TabIndex = 17;
            // 
            // pnStations
            // 
            this.pnStations.Controls.Add(this.stationsControl);
            this.pnStations.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnStations.Location = new System.Drawing.Point(714, 0);
            this.pnStations.Name = "pnStations";
            this.pnStations.Padding = new System.Windows.Forms.Padding(5);
            this.pnStations.Size = new System.Drawing.Size(304, 325);
            this.pnStations.TabIndex = 0;
            // 
            // stationsControl
            // 
            this.stationsControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.stationsControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stationsControl.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.stationsControl.HasChanged = false;
            this.stationsControl.Location = new System.Drawing.Point(5, 5);
            this.stationsControl.Name = "stationsControl";
            this.stationsControl.Padding = new System.Windows.Forms.Padding(3);
            this.stationsControl.Size = new System.Drawing.Size(294, 315);
            this.stationsControl.TabIndex = 0;
            // 
            // pnDayparts
            // 
            this.pnDayparts.Controls.Add(this.daypartsControl);
            this.pnDayparts.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnDayparts.Location = new System.Drawing.Point(410, 0);
            this.pnDayparts.Name = "pnDayparts";
            this.pnDayparts.Padding = new System.Windows.Forms.Padding(5);
            this.pnDayparts.Size = new System.Drawing.Size(304, 325);
            this.pnDayparts.TabIndex = 1;
            // 
            // daypartsControl
            // 
            this.daypartsControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.daypartsControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.daypartsControl.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.daypartsControl.HasChanged = false;
            this.daypartsControl.Location = new System.Drawing.Point(5, 5);
            this.daypartsControl.Name = "daypartsControl";
            this.daypartsControl.Padding = new System.Windows.Forms.Padding(3);
            this.daypartsControl.Size = new System.Drawing.Size(294, 315);
            this.daypartsControl.TabIndex = 0;
            // 
            // pbBottomDescription
            // 
            this.pbBottomDescription.Dock = System.Windows.Forms.DockStyle.Left;
            this.pbBottomDescription.Image = global::RadioScheduleBuilder.Properties.Resources.HomeBottomDescription;
            this.pbBottomDescription.Location = new System.Drawing.Point(0, 0);
            this.pbBottomDescription.Name = "pbBottomDescription";
            this.pbBottomDescription.Size = new System.Drawing.Size(410, 325);
            this.pbBottomDescription.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbBottomDescription.TabIndex = 2;
            this.pbBottomDescription.TabStop = false;
            // 
            // pbMonthlySchedule
            // 
            this.pbMonthlySchedule.Image = global::RadioScheduleBuilder.Properties.Resources.MonthlyScheduleButton;
            this.pbMonthlySchedule.Location = new System.Drawing.Point(562, 32);
            this.pbMonthlySchedule.Name = "pbMonthlySchedule";
            this.pbMonthlySchedule.Size = new System.Drawing.Size(438, 156);
            this.pbMonthlySchedule.TabIndex = 16;
            this.pbMonthlySchedule.TabStop = false;
            this.pbMonthlySchedule.Click += new System.EventHandler(this.pbMonthlySchedule_Click);
            this.pbMonthlySchedule.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
            this.pbMonthlySchedule.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseUp);
            // 
            // pbWeeklySchedule
            // 
            this.pbWeeklySchedule.Image = global::RadioScheduleBuilder.Properties.Resources.WeeklyScheduleButton;
            this.pbWeeklySchedule.Location = new System.Drawing.Point(19, 32);
            this.pbWeeklySchedule.Name = "pbWeeklySchedule";
            this.pbWeeklySchedule.Size = new System.Drawing.Size(439, 156);
            this.pbWeeklySchedule.TabIndex = 15;
            this.pbWeeklySchedule.TabStop = false;
            this.pbWeeklySchedule.Click += new System.EventHandler(this.pbWeeklySchedule_Click);
            this.pbWeeklySchedule.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
            this.pbWeeklySchedule.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseUp);
            // 
            // defaultLookAndFeel
            // 
            this.defaultLookAndFeel.LookAndFeel.SkinName = "Money Twins";
            // 
            // xtraTabControl
            // 
            this.xtraTabControl.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.xtraTabControl.Appearance.Options.UseFont = true;
            this.xtraTabControl.AppearancePage.Header.Font = new System.Drawing.Font("Arial", 9.75F);
            this.xtraTabControl.AppearancePage.Header.Options.UseFont = true;
            this.xtraTabControl.AppearancePage.HeaderActive.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.xtraTabControl.AppearancePage.HeaderActive.Options.UseFont = true;
            this.xtraTabControl.AppearancePage.HeaderDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
            this.xtraTabControl.AppearancePage.HeaderDisabled.Options.UseFont = true;
            this.xtraTabControl.AppearancePage.HeaderHotTracked.Font = new System.Drawing.Font("Arial", 9.75F);
            this.xtraTabControl.AppearancePage.HeaderHotTracked.Options.UseFont = true;
            this.xtraTabControl.AppearancePage.PageClient.Font = new System.Drawing.Font("Arial", 9.75F);
            this.xtraTabControl.AppearancePage.PageClient.Options.UseFont = true;
            this.xtraTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl.Location = new System.Drawing.Point(0, 0);
            this.xtraTabControl.Name = "xtraTabControl";
            this.xtraTabControl.SelectedTabPage = this.xtraTabPageRadio;
            this.xtraTabControl.Size = new System.Drawing.Size(1020, 666);
            this.xtraTabControl.TabIndex = 18;
            this.xtraTabControl.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPageRadio,
            this.xtraTabPageDigital});
            // 
            // xtraTabPageRadio
            // 
            this.xtraTabPageRadio.Controls.Add(this.pnRadio);
            this.xtraTabPageRadio.Name = "xtraTabPageRadio";
            this.xtraTabPageRadio.Size = new System.Drawing.Size(1018, 640);
            this.xtraTabPageRadio.Text = "Radio Strategy";
            // 
            // pnRadio
            // 
            this.pnRadio.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.pnRadio.Controls.Add(this.pnBottom);
            this.pnRadio.Controls.Add(this.pbMonthlySchedule);
            this.pnRadio.Controls.Add(this.pbWeeklySchedule);
            this.pnRadio.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnRadio.Location = new System.Drawing.Point(0, 0);
            this.pnRadio.Name = "pnRadio";
            this.pnRadio.Size = new System.Drawing.Size(1018, 640);
            this.pnRadio.TabIndex = 0;
            // 
            // xtraTabPageDigital
            // 
            this.xtraTabPageDigital.Controls.Add(this.pnDigital);
            this.xtraTabPageDigital.Name = "xtraTabPageDigital";
            this.xtraTabPageDigital.PageEnabled = false;
            this.xtraTabPageDigital.Size = new System.Drawing.Size(1018, 640);
            this.xtraTabPageDigital.Text = "Digital Strategy";
            // 
            // pnDigital
            // 
            this.pnDigital.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.pnDigital.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnDigital.Location = new System.Drawing.Point(0, 0);
            this.pnDigital.Name = "pnDigital";
            this.pnDigital.Size = new System.Drawing.Size(1018, 640);
            this.pnDigital.TabIndex = 0;
            // 
            // HomeControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.xtraTabControl);
            this.Name = "HomeControl";
            this.Size = new System.Drawing.Size(1020, 666);
            this.pnBottom.ResumeLayout(false);
            this.pnStations.ResumeLayout(false);
            this.pnDayparts.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbBottomDescription)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMonthlySchedule)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbWeeklySchedule)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).EndInit();
            this.xtraTabControl.ResumeLayout(false);
            this.xtraTabPageRadio.ResumeLayout(false);
            this.pnRadio.ResumeLayout(false);
            this.xtraTabPageDigital.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbWeeklySchedule;
        private System.Windows.Forms.PictureBox pbMonthlySchedule;
        private System.Windows.Forms.Panel pnBottom;
        private System.Windows.Forms.Panel pnDayparts;
        private System.Windows.Forms.Panel pnStations;
        private StationsControl stationsControl;
        private DaypartsControl daypartsControl;
        private System.Windows.Forms.PictureBox pbBottomDescription;
        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl;
        private DevExpress.XtraTab.XtraTabPage xtraTabPageRadio;
        private DevExpress.XtraTab.XtraTabPage xtraTabPageDigital;
        private System.Windows.Forms.Panel pnRadio;
        private System.Windows.Forms.Panel pnDigital;

    }
}
