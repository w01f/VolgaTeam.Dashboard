namespace Asa.AdSchedule.Controls.PresentationClasses.OutputClasses.OutputControls
{
    partial class OutputMultiSummaryControl
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
			this.pnTopHeader = new System.Windows.Forms.Panel();
			this.checkEditProductName = new DevExpress.XtraEditors.CheckEdit();
			this.checkEditFlightDates = new DevExpress.XtraEditors.CheckEdit();
			this.xtraTabControlPublications = new DevExpress.XtraTab.XtraTabControl();
			this.pnTopHeader.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.checkEditProductName.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditFlightDates.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControlPublications)).BeginInit();
			this.SuspendLayout();
			// 
			// pnTopHeader
			// 
			this.pnTopHeader.BackColor = System.Drawing.Color.White;
			this.pnTopHeader.Controls.Add(this.checkEditProductName);
			this.pnTopHeader.Controls.Add(this.checkEditFlightDates);
			this.pnTopHeader.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnTopHeader.Location = new System.Drawing.Point(0, 0);
			this.pnTopHeader.Name = "pnTopHeader";
			this.pnTopHeader.Size = new System.Drawing.Size(796, 30);
			this.pnTopHeader.TabIndex = 3;
			// 
			// checkEditProductName
			// 
			this.checkEditProductName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.checkEditProductName.Location = new System.Drawing.Point(483, 5);
			this.checkEditProductName.Name = "checkEditProductName";
			this.checkEditProductName.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.checkEditProductName.Properties.Appearance.Options.UseFont = true;
			this.checkEditProductName.Properties.Appearance.Options.UseTextOptions = true;
			this.checkEditProductName.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			this.checkEditProductName.Properties.Caption = "Product Name";
			this.checkEditProductName.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Far;
			this.checkEditProductName.Size = new System.Drawing.Size(310, 19);
			this.checkEditProductName.TabIndex = 29;
			this.checkEditProductName.CheckedChanged += new System.EventHandler(this.checkEdit_CheckedChanged);
			this.checkEditProductName.MouseDown += new System.Windows.Forms.MouseEventHandler(this.checkEdit_MouseDown);
			// 
			// checkEditFlightDates
			// 
			this.checkEditFlightDates.Location = new System.Drawing.Point(3, 5);
			this.checkEditFlightDates.Name = "checkEditFlightDates";
			this.checkEditFlightDates.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.checkEditFlightDates.Properties.Appearance.Options.UseFont = true;
			this.checkEditFlightDates.Properties.Caption = "Flight Dates";
			this.checkEditFlightDates.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Default;
			this.checkEditFlightDates.Size = new System.Drawing.Size(228, 20);
			this.checkEditFlightDates.TabIndex = 28;
			this.checkEditFlightDates.CheckedChanged += new System.EventHandler(this.checkEdit_CheckedChanged);
			this.checkEditFlightDates.MouseDown += new System.Windows.Forms.MouseEventHandler(this.checkEdit_MouseDown);
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
			this.xtraTabControlPublications.Size = new System.Drawing.Size(796, 400);
			this.xtraTabControlPublications.TabIndex = 4;
			this.xtraTabControlPublications.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.xtraTabControlPublications_SelectedPageChanged);
			// 
			// OutputMultiSummaryControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.xtraTabControlPublications);
			this.Controls.Add(this.pnTopHeader);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "OutputMultiSummaryControl";
			this.Size = new System.Drawing.Size(796, 430);
			this.pnTopHeader.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.checkEditProductName.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditFlightDates.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControlPublications)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

		private System.Windows.Forms.Panel pnTopHeader;
		private DevExpress.XtraTab.XtraTabControl xtraTabControlPublications;
		public DevExpress.XtraEditors.CheckEdit checkEditProductName;
		public DevExpress.XtraEditors.CheckEdit checkEditFlightDates;

    }
}
