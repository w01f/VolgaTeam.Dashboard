namespace Asa.AdSchedule.Controls.PresentationClasses.OutputClasses.OutputControls
{
    partial class GridsControl
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
			this.pnMain = new System.Windows.Forms.Panel();
			this.xtraTabControlOptions = new DevExpress.XtraTab.XtraTabControl();
			this.xtraTabPagePrint = new DevExpress.XtraTab.XtraTabPage();
			this.xtraTabPageAdNotes = new DevExpress.XtraTab.XtraTabPage();
			this.xtraTabPageSlideHeaders = new DevExpress.XtraTab.XtraTabPage();
			this.xtraTabPageSlideBullets = new DevExpress.XtraTab.XtraTabPage();
			this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
			this.superTooltip = new DevComponents.DotNetBar.SuperTooltip();
			this.retractableBar = new Asa.CommonGUI.RetractableBar.RetractableBarLeft();
			this.pnEmpty = new System.Windows.Forms.Panel();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControlOptions)).BeginInit();
			this.xtraTabControlOptions.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			this.retractableBar.Content.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnMain
			// 
			this.pnMain.Location = new System.Drawing.Point(351, 42);
			this.pnMain.Name = "pnMain";
			this.pnMain.Size = new System.Drawing.Size(117, 106);
			this.pnMain.TabIndex = 1;
			// 
			// xtraTabControlOptions
			// 
			this.xtraTabControlOptions.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControlOptions.Appearance.Options.UseFont = true;
			this.xtraTabControlOptions.AppearancePage.Header.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlOptions.AppearancePage.Header.Options.UseFont = true;
			this.xtraTabControlOptions.AppearancePage.HeaderActive.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControlOptions.AppearancePage.HeaderActive.Options.UseFont = true;
			this.xtraTabControlOptions.AppearancePage.HeaderDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlOptions.AppearancePage.HeaderDisabled.Options.UseFont = true;
			this.xtraTabControlOptions.AppearancePage.HeaderHotTracked.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlOptions.AppearancePage.HeaderHotTracked.Options.UseFont = true;
			this.xtraTabControlOptions.AppearancePage.PageClient.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlOptions.AppearancePage.PageClient.Options.UseFont = true;
			this.xtraTabControlOptions.Dock = System.Windows.Forms.DockStyle.Fill;
			this.xtraTabControlOptions.Location = new System.Drawing.Point(0, 0);
			this.xtraTabControlOptions.Name = "xtraTabControlOptions";
			this.xtraTabControlOptions.SelectedTabPage = this.xtraTabPagePrint;
			this.xtraTabControlOptions.Size = new System.Drawing.Size(296, 468);
			this.xtraTabControlOptions.TabIndex = 0;
			this.xtraTabControlOptions.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPagePrint,
            this.xtraTabPageAdNotes,
            this.xtraTabPageSlideHeaders,
            this.xtraTabPageSlideBullets});
			this.xtraTabControlOptions.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.xtraTabControlDetails_SelectedPageChanged);
			// 
			// xtraTabPagePrint
			// 
			this.xtraTabPagePrint.Name = "xtraTabPagePrint";
			this.xtraTabPagePrint.Size = new System.Drawing.Size(290, 437);
			this.xtraTabPagePrint.Text = "Columns";
			// 
			// xtraTabPageAdNotes
			// 
			this.xtraTabPageAdNotes.Name = "xtraTabPageAdNotes";
			this.xtraTabPageAdNotes.Size = new System.Drawing.Size(290, 437);
			this.xtraTabPageAdNotes.Text = "Notes";
			// 
			// xtraTabPageSlideHeaders
			// 
			this.xtraTabPageSlideHeaders.Name = "xtraTabPageSlideHeaders";
			this.xtraTabPageSlideHeaders.Size = new System.Drawing.Size(290, 437);
			this.xtraTabPageSlideHeaders.Text = "Slide Info";
			// 
			// xtraTabPageSlideBullets
			// 
			this.xtraTabPageSlideBullets.Name = "xtraTabPageSlideBullets";
			this.xtraTabPageSlideBullets.Size = new System.Drawing.Size(290, 437);
			this.xtraTabPageSlideBullets.Text = "Totals";
			// 
			// styleController
			// 
			this.styleController.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.styleController.Appearance.Options.UseFont = true;
			this.styleController.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.styleController.AppearanceDisabled.Options.UseFont = true;
			this.styleController.AppearanceDropDown.Font = new System.Drawing.Font("Arial", 9.75F);
			this.styleController.AppearanceDropDown.Options.UseFont = true;
			this.styleController.AppearanceDropDownHeader.Font = new System.Drawing.Font("Arial", 9.75F);
			this.styleController.AppearanceDropDownHeader.Options.UseFont = true;
			this.styleController.AppearanceFocused.Font = new System.Drawing.Font("Arial", 9.75F);
			this.styleController.AppearanceFocused.Options.UseFont = true;
			this.styleController.AppearanceReadOnly.Font = new System.Drawing.Font("Arial", 9.75F);
			this.styleController.AppearanceReadOnly.Options.UseFont = true;
			// 
			// superTooltip
			// 
			this.superTooltip.DefaultTooltipSettings = new DevComponents.DotNetBar.SuperTooltipInfo("", "", "", null, null, DevComponents.DotNetBar.eTooltipColor.Gray);
			this.superTooltip.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			// 
			// retractableBar
			// 
			this.retractableBar.AnimationDelay = 0;
			this.retractableBar.BackColor = System.Drawing.Color.Transparent;
			// 
			// retractableBar.Content
			// 
			this.retractableBar.Content.Controls.Add(this.xtraTabControlOptions);
			this.retractableBar.Content.Dock = System.Windows.Forms.DockStyle.Fill;
			this.retractableBar.Content.Location = new System.Drawing.Point(2, 42);
			this.retractableBar.Content.Name = "Content";
			this.retractableBar.Content.Size = new System.Drawing.Size(296, 468);
			this.retractableBar.Content.TabIndex = 1;
			this.retractableBar.ContentSize = 300;
			this.retractableBar.Dock = System.Windows.Forms.DockStyle.Left;
			this.retractableBar.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.retractableBar.Location = new System.Drawing.Point(0, 0);
			this.retractableBar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.retractableBar.Name = "retractableBar";
			this.retractableBar.Size = new System.Drawing.Size(300, 512);
			this.retractableBar.TabIndex = 3;
			// 
			// pnEmpty
			// 
			this.pnEmpty.Location = new System.Drawing.Point(515, 42);
			this.pnEmpty.Name = "pnEmpty";
			this.pnEmpty.Size = new System.Drawing.Size(117, 106);
			this.pnEmpty.TabIndex = 4;
			// 
			// GridsControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.pnEmpty);
			this.Controls.Add(this.pnMain);
			this.Controls.Add(this.retractableBar);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "GridsControl";
			this.Size = new System.Drawing.Size(759, 512);
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControlOptions)).EndInit();
			this.xtraTabControlOptions.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			this.retractableBar.Content.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

		private System.Windows.Forms.Panel pnMain;
        private DevExpress.XtraEditors.StyleController styleController;
        private DevExpress.XtraTab.XtraTabControl xtraTabControlOptions;
        private DevExpress.XtraTab.XtraTabPage xtraTabPageAdNotes;
        private DevExpress.XtraTab.XtraTabPage xtraTabPageSlideHeaders;
        private DevComponents.DotNetBar.SuperTooltip superTooltip;
		private DevExpress.XtraTab.XtraTabPage xtraTabPagePrint;
		private DevExpress.XtraTab.XtraTabPage xtraTabPageSlideBullets;
		private CommonGUI.RetractableBar.RetractableBarLeft retractableBar;
		private System.Windows.Forms.Panel pnEmpty;
    }
}
