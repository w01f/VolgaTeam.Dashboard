namespace TVScheduleBuilder.CustomControls
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
            this.pbMonthlySchedule = new System.Windows.Forms.PictureBox();
            this.pbWeeklySchedule = new System.Windows.Forms.PictureBox();
            this.pnRight = new System.Windows.Forms.Panel();
            this.pnDayparts = new System.Windows.Forms.Panel();
            this.pnStations = new System.Windows.Forms.Panel();
            this.daypartsControl = new TVScheduleBuilder.CustomControls.DaypartsControl();
            this.stationsControl = new TVScheduleBuilder.CustomControls.StationsControl();
            ((System.ComponentModel.ISupportInitialize)(this.pbMonthlySchedule)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbWeeklySchedule)).BeginInit();
            this.pnRight.SuspendLayout();
            this.pnDayparts.SuspendLayout();
            this.pnStations.SuspendLayout();
            this.SuspendLayout();
            // 
            // pbMonthlySchedule
            // 
            this.pbMonthlySchedule.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pbMonthlySchedule.Image = global::TVScheduleBuilder.Properties.Resources.MonthlyScheduleButton;
            this.pbMonthlySchedule.Location = new System.Drawing.Point(19, 285);
            this.pbMonthlySchedule.Name = "pbMonthlySchedule";
            this.pbMonthlySchedule.Size = new System.Drawing.Size(525, 149);
            this.pbMonthlySchedule.TabIndex = 16;
            this.pbMonthlySchedule.TabStop = false;
            this.pbMonthlySchedule.Click += new System.EventHandler(this.pbMonthlySchedule_Click);
            this.pbMonthlySchedule.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
            this.pbMonthlySchedule.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseUp);
            // 
            // pbWeeklySchedule
            // 
            this.pbWeeklySchedule.Image = global::TVScheduleBuilder.Properties.Resources.WeeklyScheduleButton;
            this.pbWeeklySchedule.Location = new System.Drawing.Point(19, 104);
            this.pbWeeklySchedule.Name = "pbWeeklySchedule";
            this.pbWeeklySchedule.Size = new System.Drawing.Size(525, 156);
            this.pbWeeklySchedule.TabIndex = 15;
            this.pbWeeklySchedule.TabStop = false;
            this.pbWeeklySchedule.Click += new System.EventHandler(this.pbWeeklySchedule_Click);
            this.pbWeeklySchedule.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
            this.pbWeeklySchedule.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseUp);
            // 
            // pnRight
            // 
            this.pnRight.Controls.Add(this.pnDayparts);
            this.pnRight.Controls.Add(this.pnStations);
            this.pnRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnRight.Location = new System.Drawing.Point(574, 0);
            this.pnRight.Name = "pnRight";
            this.pnRight.Size = new System.Drawing.Size(320, 539);
            this.pnRight.TabIndex = 17;
            this.pnRight.Resize += new System.EventHandler(this.pnRight_Resize);
            // 
            // pnDayparts
            // 
            this.pnDayparts.Controls.Add(this.daypartsControl);
            this.pnDayparts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnDayparts.Location = new System.Drawing.Point(0, 289);
            this.pnDayparts.Name = "pnDayparts";
            this.pnDayparts.Padding = new System.Windows.Forms.Padding(10);
            this.pnDayparts.Size = new System.Drawing.Size(320, 250);
            this.pnDayparts.TabIndex = 1;
            // 
            // pnStations
            // 
            this.pnStations.Controls.Add(this.stationsControl);
            this.pnStations.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnStations.Location = new System.Drawing.Point(0, 0);
            this.pnStations.Name = "pnStations";
            this.pnStations.Padding = new System.Windows.Forms.Padding(10);
            this.pnStations.Size = new System.Drawing.Size(320, 289);
            this.pnStations.TabIndex = 0;
            // 
            // daypartsControl
            // 
            this.daypartsControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.daypartsControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.daypartsControl.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.daypartsControl.HasChanged = false;
            this.daypartsControl.Location = new System.Drawing.Point(10, 10);
            this.daypartsControl.Name = "daypartsControl";
            this.daypartsControl.Padding = new System.Windows.Forms.Padding(3);
            this.daypartsControl.Size = new System.Drawing.Size(300, 230);
            this.daypartsControl.TabIndex = 0;
            // 
            // stationsControl
            // 
            this.stationsControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.stationsControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stationsControl.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.stationsControl.HasChanged = false;
            this.stationsControl.Location = new System.Drawing.Point(10, 10);
            this.stationsControl.Name = "stationsControl";
            this.stationsControl.Padding = new System.Windows.Forms.Padding(3);
            this.stationsControl.Size = new System.Drawing.Size(300, 269);
            this.stationsControl.TabIndex = 0;
            // 
            // HomeControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.pnRight);
            this.Controls.Add(this.pbMonthlySchedule);
            this.Controls.Add(this.pbWeeklySchedule);
            this.Name = "HomeControl";
            this.Size = new System.Drawing.Size(894, 539);
            ((System.ComponentModel.ISupportInitialize)(this.pbMonthlySchedule)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbWeeklySchedule)).EndInit();
            this.pnRight.ResumeLayout(false);
            this.pnDayparts.ResumeLayout(false);
            this.pnStations.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbWeeklySchedule;
        private System.Windows.Forms.PictureBox pbMonthlySchedule;
        private System.Windows.Forms.Panel pnRight;
        private System.Windows.Forms.Panel pnDayparts;
        private System.Windows.Forms.Panel pnStations;
        private StationsControl stationsControl;
        private DaypartsControl daypartsControl;

    }
}
