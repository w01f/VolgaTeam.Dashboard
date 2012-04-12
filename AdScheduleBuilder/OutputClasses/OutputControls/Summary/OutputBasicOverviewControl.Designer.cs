namespace AdScheduleBuilder.OutputClasses.OutputControls
{
    partial class OutputBasicOverviewControl
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
            this.xtraTabControlPublications = new DevExpress.XtraTab.XtraTabControl();
            this.pnHeader = new System.Windows.Forms.Panel();
            this.laScheduleWindow = new System.Windows.Forms.Label();
            this.laAdvertiser = new System.Windows.Forms.Label();
            this.laScheduleName = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControlPublications)).BeginInit();
            this.pnHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // defaultLookAndFeel
            // 
            this.defaultLookAndFeel.LookAndFeel.SkinName = "Money Twins";
            // 
            // xtraTabControlPublications
            // 
            this.xtraTabControlPublications.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.xtraTabControlPublications.Appearance.Options.UseFont = true;
            this.xtraTabControlPublications.AppearancePage.Header.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.xtraTabControlPublications.AppearancePage.Header.Options.UseFont = true;
            this.xtraTabControlPublications.AppearancePage.HeaderActive.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.xtraTabControlPublications.AppearancePage.HeaderActive.Options.UseFont = true;
            this.xtraTabControlPublications.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControlPublications.Location = new System.Drawing.Point(0, 30);
            this.xtraTabControlPublications.Name = "xtraTabControlPublications";
            this.xtraTabControlPublications.Size = new System.Drawing.Size(737, 400);
            this.xtraTabControlPublications.TabIndex = 3;
            // 
            // pnHeader
            // 
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
            this.laScheduleWindow.Size = new System.Drawing.Size(137, 30);
            this.laScheduleWindow.TabIndex = 1;
            this.laScheduleWindow.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // laAdvertiser
            // 
            this.laAdvertiser.Dock = System.Windows.Forms.DockStyle.Left;
            this.laAdvertiser.Location = new System.Drawing.Point(0, 0);
            this.laAdvertiser.Name = "laAdvertiser";
            this.laAdvertiser.Size = new System.Drawing.Size(300, 30);
            this.laAdvertiser.TabIndex = 2;
            this.laAdvertiser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // laScheduleName
            // 
            this.laScheduleName.Dock = System.Windows.Forms.DockStyle.Right;
            this.laScheduleName.Location = new System.Drawing.Point(437, 0);
            this.laScheduleName.Name = "laScheduleName";
            this.laScheduleName.Size = new System.Drawing.Size(300, 30);
            this.laScheduleName.TabIndex = 0;
            this.laScheduleName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // OutputBasicOverviewControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.xtraTabControlPublications);
            this.Controls.Add(this.pnHeader);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Name = "OutputBasicOverviewControl";
            this.Size = new System.Drawing.Size(737, 430);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControlPublications)).EndInit();
            this.pnHeader.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
        private DevExpress.XtraTab.XtraTabControl xtraTabControlPublications;
        private System.Windows.Forms.Panel pnHeader;
        private System.Windows.Forms.Label laScheduleWindow;
        private System.Windows.Forms.Label laAdvertiser;
        private System.Windows.Forms.Label laScheduleName;

    }
}
