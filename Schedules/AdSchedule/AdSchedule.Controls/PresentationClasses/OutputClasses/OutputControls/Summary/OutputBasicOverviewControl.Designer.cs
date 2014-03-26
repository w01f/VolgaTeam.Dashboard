namespace NewBizWiz.AdSchedule.Controls.PresentationClasses.OutputClasses.OutputControls
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
			this.laAdvertiser = new System.Windows.Forms.Label();
			this.laFlightDates = new System.Windows.Forms.Label();
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
			this.pnHeader.Controls.Add(this.laFlightDates);
			this.pnHeader.Controls.Add(this.laAdvertiser);
			this.pnHeader.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnHeader.Location = new System.Drawing.Point(0, 0);
			this.pnHeader.Name = "pnHeader";
			this.pnHeader.Size = new System.Drawing.Size(737, 30);
			this.pnHeader.TabIndex = 4;
			// 
			// laAdvertiser
			// 
			this.laAdvertiser.Location = new System.Drawing.Point(3, 5);
			this.laAdvertiser.Name = "laAdvertiser";
			this.laAdvertiser.Size = new System.Drawing.Size(315, 20);
			this.laAdvertiser.TabIndex = 0;
			this.laAdvertiser.Text = "label1";
			this.laAdvertiser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// laFlightDates
			// 
			this.laFlightDates.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.laFlightDates.Location = new System.Drawing.Point(419, 5);
			this.laFlightDates.Name = "laFlightDates";
			this.laFlightDates.Size = new System.Drawing.Size(315, 20);
			this.laFlightDates.TabIndex = 1;
			this.laFlightDates.Text = "label1";
			this.laFlightDates.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
	    public System.Windows.Forms.Label laFlightDates;
		private System.Windows.Forms.Label laAdvertiser;

    }
}
