namespace AdScheduleBuilder.OutputClasses.OutputControls
{
    partial class OutputCalendarControl
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
            this.pnHeader = new System.Windows.Forms.Panel();
            this.laScheduleWindow = new System.Windows.Forms.Label();
            this.laAdvertiser = new System.Windows.Forms.Label();
            this.laScheduleName = new System.Windows.Forms.Label();
            this.pnMain = new System.Windows.Forms.Panel();
            this.pnEmpty = new System.Windows.Forms.Panel();
            this.pnCalendarView = new System.Windows.Forms.Panel();
            this.pnHeader.SuspendLayout();
            this.pnMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // defaultLookAndFeel
            // 
            this.defaultLookAndFeel.LookAndFeel.SkinName = "Money Twins";
            // 
            // pnHeader
            // 
            this.pnHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.pnHeader.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnHeader.Controls.Add(this.laScheduleWindow);
            this.pnHeader.Controls.Add(this.laAdvertiser);
            this.pnHeader.Controls.Add(this.laScheduleName);
            this.pnHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnHeader.Location = new System.Drawing.Point(0, 0);
            this.pnHeader.Name = "pnHeader";
            this.pnHeader.Size = new System.Drawing.Size(737, 30);
            this.pnHeader.TabIndex = 4;
            // 
            // laScheduleWindow
            // 
            this.laScheduleWindow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.laScheduleWindow.Location = new System.Drawing.Point(300, 0);
            this.laScheduleWindow.Name = "laScheduleWindow";
            this.laScheduleWindow.Size = new System.Drawing.Size(133, 26);
            this.laScheduleWindow.TabIndex = 1;
            this.laScheduleWindow.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // laAdvertiser
            // 
            this.laAdvertiser.Dock = System.Windows.Forms.DockStyle.Left;
            this.laAdvertiser.Location = new System.Drawing.Point(0, 0);
            this.laAdvertiser.Name = "laAdvertiser";
            this.laAdvertiser.Size = new System.Drawing.Size(300, 26);
            this.laAdvertiser.TabIndex = 2;
            this.laAdvertiser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // laScheduleName
            // 
            this.laScheduleName.Dock = System.Windows.Forms.DockStyle.Right;
            this.laScheduleName.Location = new System.Drawing.Point(433, 0);
            this.laScheduleName.Name = "laScheduleName";
            this.laScheduleName.Size = new System.Drawing.Size(300, 26);
            this.laScheduleName.TabIndex = 0;
            this.laScheduleName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnMain
            // 
            this.pnMain.Controls.Add(this.pnCalendarView);
            this.pnMain.Controls.Add(this.pnEmpty);
            this.pnMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnMain.Location = new System.Drawing.Point(0, 30);
            this.pnMain.Name = "pnMain";
            this.pnMain.Size = new System.Drawing.Size(737, 400);
            this.pnMain.TabIndex = 7;
            // 
            // pnEmpty
            // 
            this.pnEmpty.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnEmpty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnEmpty.Location = new System.Drawing.Point(0, 0);
            this.pnEmpty.Name = "pnEmpty";
            this.pnEmpty.Size = new System.Drawing.Size(737, 400);
            this.pnEmpty.TabIndex = 7;
            // 
            // pnCalendarView
            // 
            this.pnCalendarView.BackColor = System.Drawing.Color.AliceBlue;
            this.pnCalendarView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnCalendarView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnCalendarView.Location = new System.Drawing.Point(0, 0);
            this.pnCalendarView.Name = "pnCalendarView";
            this.pnCalendarView.Size = new System.Drawing.Size(737, 400);
            this.pnCalendarView.TabIndex = 6;
            // 
            // OutputCalendarControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.Controls.Add(this.pnMain);
            this.Controls.Add(this.pnHeader);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Name = "OutputCalendarControl";
            this.Size = new System.Drawing.Size(737, 430);
            this.pnHeader.ResumeLayout(false);
            this.pnMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
        private System.Windows.Forms.Panel pnHeader;
        private System.Windows.Forms.Label laScheduleWindow;
        private System.Windows.Forms.Label laAdvertiser;
        private System.Windows.Forms.Label laScheduleName;
        private System.Windows.Forms.Panel pnMain;
        private System.Windows.Forms.Panel pnEmpty;
        private System.Windows.Forms.Panel pnCalendarView;

    }
}
