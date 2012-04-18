namespace AdScheduleBuilder.OutputClasses.OutputControls
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
            this.pnEmpty = new System.Windows.Forms.Panel();
            this.pnMain = new System.Windows.Forms.Panel();
            this.splitContainerControl = new DevExpress.XtraEditors.SplitContainerControl();
            this.xtraTabControlDetails = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPageAdNotes = new DevExpress.XtraTab.XtraTabPage();
            this.xtraTabPageSlideHeaders = new DevExpress.XtraTab.XtraTabPage();
            this.xtraTabPageSlideBullets = new DevExpress.XtraTab.XtraTabPage();
            this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl)).BeginInit();
            this.splitContainerControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControlDetails)).BeginInit();
            this.xtraTabControlDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
            this.SuspendLayout();
            // 
            // pnEmpty
            // 
            this.pnEmpty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnEmpty.Location = new System.Drawing.Point(0, 0);
            this.pnEmpty.Name = "pnEmpty";
            this.pnEmpty.Size = new System.Drawing.Size(453, 490);
            this.pnEmpty.TabIndex = 0;
            // 
            // pnMain
            // 
            this.pnMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnMain.Location = new System.Drawing.Point(0, 0);
            this.pnMain.Name = "pnMain";
            this.pnMain.Size = new System.Drawing.Size(453, 490);
            this.pnMain.TabIndex = 1;
            // 
            // splitContainerControl
            // 
            this.splitContainerControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl.Name = "splitContainerControl";
            this.splitContainerControl.Panel1.Controls.Add(this.xtraTabControlDetails);
            this.splitContainerControl.Panel1.MinSize = 250;
            this.splitContainerControl.Panel1.Text = "Panel1";
            this.splitContainerControl.Panel2.Controls.Add(this.pnMain);
            this.splitContainerControl.Panel2.Controls.Add(this.pnEmpty);
            this.splitContainerControl.Panel2.Text = "Panel2";
            this.splitContainerControl.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel2;
            this.splitContainerControl.Size = new System.Drawing.Size(453, 490);
            this.splitContainerControl.SplitterPosition = 250;
            this.splitContainerControl.TabIndex = 2;
            this.splitContainerControl.Text = "splitContainerControl";
            // 
            // xtraTabControlDetails
            // 
            this.xtraTabControlDetails.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.xtraTabControlDetails.Appearance.Options.UseFont = true;
            this.xtraTabControlDetails.AppearancePage.Header.Font = new System.Drawing.Font("Arial", 9.75F);
            this.xtraTabControlDetails.AppearancePage.Header.Options.UseFont = true;
            this.xtraTabControlDetails.AppearancePage.HeaderActive.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.xtraTabControlDetails.AppearancePage.HeaderActive.Options.UseFont = true;
            this.xtraTabControlDetails.AppearancePage.HeaderDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
            this.xtraTabControlDetails.AppearancePage.HeaderDisabled.Options.UseFont = true;
            this.xtraTabControlDetails.AppearancePage.HeaderHotTracked.Font = new System.Drawing.Font("Arial", 9.75F);
            this.xtraTabControlDetails.AppearancePage.HeaderHotTracked.Options.UseFont = true;
            this.xtraTabControlDetails.AppearancePage.PageClient.Font = new System.Drawing.Font("Arial", 9.75F);
            this.xtraTabControlDetails.AppearancePage.PageClient.Options.UseFont = true;
            this.xtraTabControlDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControlDetails.Location = new System.Drawing.Point(0, 0);
            this.xtraTabControlDetails.Name = "xtraTabControlDetails";
            this.xtraTabControlDetails.SelectedTabPage = this.xtraTabPageAdNotes;
            this.xtraTabControlDetails.Size = new System.Drawing.Size(0, 0);
            this.xtraTabControlDetails.TabIndex = 0;
            this.xtraTabControlDetails.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPageAdNotes,
            this.xtraTabPageSlideHeaders,
            this.xtraTabPageSlideBullets});
            // 
            // xtraTabPageAdNotes
            // 
            this.xtraTabPageAdNotes.Name = "xtraTabPageAdNotes";
            this.xtraTabPageAdNotes.Size = new System.Drawing.Size(0, 0);
            this.xtraTabPageAdNotes.Text = "AdNotes";
            // 
            // xtraTabPageSlideHeaders
            // 
            this.xtraTabPageSlideHeaders.Name = "xtraTabPageSlideHeaders";
            this.xtraTabPageSlideHeaders.Size = new System.Drawing.Size(0, 0);
            this.xtraTabPageSlideHeaders.Text = "Headers";
            // 
            // xtraTabPageSlideBullets
            // 
            this.xtraTabPageSlideBullets.Name = "xtraTabPageSlideBullets";
            this.xtraTabPageSlideBullets.Size = new System.Drawing.Size(0, 0);
            this.xtraTabPageSlideBullets.Text = "Totals";
            // 
            // defaultLookAndFeel
            // 
            this.defaultLookAndFeel.LookAndFeel.SkinName = "Money Twins";
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
            // GridsControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.splitContainerControl);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "GridsControl";
            this.Size = new System.Drawing.Size(453, 490);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl)).EndInit();
            this.splitContainerControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControlDetails)).EndInit();
            this.xtraTabControlDetails.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnEmpty;
        private System.Windows.Forms.Panel pnMain;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl;
        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
        private DevExpress.XtraEditors.StyleController styleController;
        private DevExpress.XtraTab.XtraTabControl xtraTabControlDetails;
        private DevExpress.XtraTab.XtraTabPage xtraTabPageAdNotes;
        private DevExpress.XtraTab.XtraTabPage xtraTabPageSlideHeaders;
        private DevExpress.XtraTab.XtraTabPage xtraTabPageSlideBullets;
    }
}
