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
			this.components = new System.ComponentModel.Container();
			this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
			this.xtraTabControlPublications = new DevExpress.XtraTab.XtraTabControl();
			this.pnHeader = new System.Windows.Forms.Panel();
			this.checkEditProductName = new DevExpress.XtraEditors.CheckEdit();
			this.checkEditFlightDates = new DevExpress.XtraEditors.CheckEdit();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControlPublications)).BeginInit();
			this.pnHeader.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.checkEditProductName.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditFlightDates.Properties)).BeginInit();
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
			this.xtraTabControlPublications.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.xtraTabControlPublications_SelectedPageChanged);
			// 
			// pnHeader
			// 
			this.pnHeader.Controls.Add(this.checkEditProductName);
			this.pnHeader.Controls.Add(this.checkEditFlightDates);
			this.pnHeader.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnHeader.Location = new System.Drawing.Point(0, 0);
			this.pnHeader.Name = "pnHeader";
			this.pnHeader.Size = new System.Drawing.Size(737, 30);
			this.pnHeader.TabIndex = 4;
			// 
			// checkEditProductName
			// 
			this.checkEditProductName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.checkEditProductName.Location = new System.Drawing.Point(427, 5);
			this.checkEditProductName.Name = "checkEditProductName";
			this.checkEditProductName.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.checkEditProductName.Properties.Appearance.Options.UseFont = true;
			this.checkEditProductName.Properties.Appearance.Options.UseTextOptions = true;
			this.checkEditProductName.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			this.checkEditProductName.Properties.Caption = "Product Name";
			this.checkEditProductName.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Far;
			this.checkEditProductName.Size = new System.Drawing.Size(307, 21);
			this.checkEditProductName.TabIndex = 27;
			// 
			// checkEditFlightDates
			// 
			this.checkEditFlightDates.Location = new System.Drawing.Point(3, 5);
			this.checkEditFlightDates.Name = "checkEditFlightDates";
			this.checkEditFlightDates.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.checkEditFlightDates.Properties.Appearance.Options.UseFont = true;
			this.checkEditFlightDates.Properties.Caption = "Flight Dates";
			this.checkEditFlightDates.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Default;
			this.checkEditFlightDates.Size = new System.Drawing.Size(218, 21);
			this.checkEditFlightDates.TabIndex = 26;
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
			((System.ComponentModel.ISupportInitialize)(this.checkEditProductName.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditFlightDates.Properties)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
        private DevExpress.XtraTab.XtraTabControl xtraTabControlPublications;
		private System.Windows.Forms.Panel pnHeader;
		public DevExpress.XtraEditors.CheckEdit checkEditProductName;
		public DevExpress.XtraEditors.CheckEdit checkEditFlightDates;

    }
}
