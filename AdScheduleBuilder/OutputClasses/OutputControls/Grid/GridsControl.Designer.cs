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
			this.pnEmpty = new System.Windows.Forms.Panel();
			this.pnMain = new System.Windows.Forms.Panel();
			this.splitContainerControl = new DevExpress.XtraEditors.SplitContainerControl();
			this.xtraTabControlOptions = new DevExpress.XtraTab.XtraTabControl();
			this.xtraTabPagePrint = new DevExpress.XtraTab.XtraTabPage();
			this.xtraTabPageAdNotes = new DevExpress.XtraTab.XtraTabPage();
			this.xtraTabPageSlideInfo = new DevExpress.XtraTab.XtraTabPage();
			this.pnSlideInfoBody = new System.Windows.Forms.Panel();
			this.pnSlideInfoHeader = new System.Windows.Forms.Panel();
			this.pbInfoHelp = new System.Windows.Forms.PictureBox();
			this.buttonXSlideBullets = new DevComponents.DotNetBar.ButtonX();
			this.buttonXSlideHeaders = new DevComponents.DotNetBar.ButtonX();
			this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel();
			this.styleController = new DevExpress.XtraEditors.StyleController();
			this.superTooltip = new DevComponents.DotNetBar.SuperTooltip();
			((System.ComponentModel.ISupportInitialize)(this.splitContainerControl)).BeginInit();
			this.splitContainerControl.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControlOptions)).BeginInit();
			this.xtraTabControlOptions.SuspendLayout();
			this.xtraTabPageSlideInfo.SuspendLayout();
			this.pnSlideInfoHeader.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbInfoHelp)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			this.SuspendLayout();
			// 
			// pnEmpty
			// 
			this.pnEmpty.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnEmpty.Location = new System.Drawing.Point(0, 0);
			this.pnEmpty.Name = "pnEmpty";
			this.pnEmpty.Size = new System.Drawing.Size(177, 490);
			this.pnEmpty.TabIndex = 0;
			// 
			// pnMain
			// 
			this.pnMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnMain.Location = new System.Drawing.Point(0, 0);
			this.pnMain.Name = "pnMain";
			this.pnMain.Size = new System.Drawing.Size(177, 490);
			this.pnMain.TabIndex = 1;
			// 
			// splitContainerControl
			// 
			this.splitContainerControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainerControl.Location = new System.Drawing.Point(0, 0);
			this.splitContainerControl.Name = "splitContainerControl";
			this.splitContainerControl.Panel1.Controls.Add(this.xtraTabControlOptions);
			this.splitContainerControl.Panel1.MinSize = 270;
			this.splitContainerControl.Panel1.Text = "Panel1";
			this.splitContainerControl.Panel2.Controls.Add(this.pnMain);
			this.splitContainerControl.Panel2.Controls.Add(this.pnEmpty);
			this.splitContainerControl.Panel2.Text = "Panel2";
			this.splitContainerControl.Size = new System.Drawing.Size(453, 490);
			this.splitContainerControl.SplitterPosition = 250;
			this.splitContainerControl.TabIndex = 2;
			this.splitContainerControl.Text = "splitContainerControl";
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
			this.xtraTabControlOptions.Size = new System.Drawing.Size(270, 490);
			this.xtraTabControlOptions.TabIndex = 0;
			this.xtraTabControlOptions.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPagePrint,
            this.xtraTabPageAdNotes,
            this.xtraTabPageSlideInfo});
			this.xtraTabControlOptions.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.xtraTabControlDetails_SelectedPageChanged);
			// 
			// xtraTabPagePrint
			// 
			this.xtraTabPagePrint.Name = "xtraTabPagePrint";
			this.xtraTabPagePrint.Size = new System.Drawing.Size(268, 464);
			this.xtraTabPagePrint.Text = "Print";
			// 
			// xtraTabPageAdNotes
			// 
			this.xtraTabPageAdNotes.Name = "xtraTabPageAdNotes";
			this.xtraTabPageAdNotes.Size = new System.Drawing.Size(268, 464);
			this.xtraTabPageAdNotes.Text = "Notes";
			// 
			// xtraTabPageSlideInfo
			// 
			this.xtraTabPageSlideInfo.Controls.Add(this.pnSlideInfoBody);
			this.xtraTabPageSlideInfo.Controls.Add(this.pnSlideInfoHeader);
			this.xtraTabPageSlideInfo.Name = "xtraTabPageSlideInfo";
			this.xtraTabPageSlideInfo.Size = new System.Drawing.Size(268, 464);
			this.xtraTabPageSlideInfo.Text = "Info";
			// 
			// pnSlideInfoBody
			// 
			this.pnSlideInfoBody.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
			this.pnSlideInfoBody.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pnSlideInfoBody.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnSlideInfoBody.Location = new System.Drawing.Point(0, 56);
			this.pnSlideInfoBody.Name = "pnSlideInfoBody";
			this.pnSlideInfoBody.Size = new System.Drawing.Size(268, 408);
			this.pnSlideInfoBody.TabIndex = 0;
			// 
			// pnSlideInfoHeader
			// 
			this.pnSlideInfoHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
			this.pnSlideInfoHeader.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pnSlideInfoHeader.Controls.Add(this.pbInfoHelp);
			this.pnSlideInfoHeader.Controls.Add(this.buttonXSlideBullets);
			this.pnSlideInfoHeader.Controls.Add(this.buttonXSlideHeaders);
			this.pnSlideInfoHeader.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnSlideInfoHeader.Location = new System.Drawing.Point(0, 0);
			this.pnSlideInfoHeader.Name = "pnSlideInfoHeader";
			this.pnSlideInfoHeader.Size = new System.Drawing.Size(268, 56);
			this.pnSlideInfoHeader.TabIndex = 1;
			// 
			// pbInfoHelp
			// 
			this.pbInfoHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.pbInfoHelp.Image = global::AdScheduleBuilder.Properties.Resources.Help;
			this.pbInfoHelp.Location = new System.Drawing.Point(213, 1);
			this.pbInfoHelp.Name = "pbInfoHelp";
			this.pbInfoHelp.Size = new System.Drawing.Size(48, 48);
			this.pbInfoHelp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pbInfoHelp.TabIndex = 38;
			this.pbInfoHelp.TabStop = false;
			this.pbInfoHelp.Click += new System.EventHandler(this.InfoHelp_Click);
			this.pbInfoHelp.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
			this.pbInfoHelp.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseUp);
			// 
			// buttonXSlideBullets
			// 
			this.buttonXSlideBullets.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXSlideBullets.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXSlideBullets.Location = new System.Drawing.Point(110, 6);
			this.buttonXSlideBullets.Name = "buttonXSlideBullets";
			this.buttonXSlideBullets.Size = new System.Drawing.Size(91, 40);
			this.buttonXSlideBullets.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXSlideBullets.TabIndex = 1;
			this.buttonXSlideBullets.Text = "Totals";
			this.buttonXSlideBullets.TextColor = System.Drawing.Color.Black;
			this.buttonXSlideBullets.CheckedChanged += new System.EventHandler(this.buttonXSlideHeaders_CheckedChanged);
			this.buttonXSlideBullets.Click += new System.EventHandler(this.buttonXSlideInfoSelector_Click);
			// 
			// buttonXSlideHeaders
			// 
			this.buttonXSlideHeaders.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXSlideHeaders.Checked = true;
			this.buttonXSlideHeaders.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXSlideHeaders.Location = new System.Drawing.Point(3, 6);
			this.buttonXSlideHeaders.Name = "buttonXSlideHeaders";
			this.buttonXSlideHeaders.Size = new System.Drawing.Size(91, 40);
			this.buttonXSlideHeaders.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXSlideHeaders.TabIndex = 0;
			this.buttonXSlideHeaders.Text = "Headers";
			this.buttonXSlideHeaders.TextColor = System.Drawing.Color.Black;
			this.buttonXSlideHeaders.CheckedChanged += new System.EventHandler(this.buttonXSlideHeaders_CheckedChanged);
			this.buttonXSlideHeaders.Click += new System.EventHandler(this.buttonXSlideInfoSelector_Click);
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
			// superTooltip
			// 
			this.superTooltip.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
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
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControlOptions)).EndInit();
			this.xtraTabControlOptions.ResumeLayout(false);
			this.xtraTabPageSlideInfo.ResumeLayout(false);
			this.pnSlideInfoHeader.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pbInfoHelp)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnEmpty;
        private System.Windows.Forms.Panel pnMain;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl;
        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
        private DevExpress.XtraEditors.StyleController styleController;
        private DevExpress.XtraTab.XtraTabControl xtraTabControlOptions;
        private DevExpress.XtraTab.XtraTabPage xtraTabPageAdNotes;
        private DevExpress.XtraTab.XtraTabPage xtraTabPageSlideInfo;
        private DevComponents.DotNetBar.SuperTooltip superTooltip;
        private DevExpress.XtraTab.XtraTabPage xtraTabPagePrint;
        private System.Windows.Forms.Panel pnSlideInfoBody;
        private System.Windows.Forms.Panel pnSlideInfoHeader;
        private DevComponents.DotNetBar.ButtonX buttonXSlideBullets;
		private DevComponents.DotNetBar.ButtonX buttonXSlideHeaders;
        private System.Windows.Forms.PictureBox pbInfoHelp;
    }
}
