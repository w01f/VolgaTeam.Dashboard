namespace Asa.Common.GUI.Preview
{
    partial class FormEmail
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipTitleItem toolTipTitleItem1 = new DevExpress.Utils.ToolTipTitleItem();
			DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
			DevExpress.Utils.SuperToolTip superToolTip2 = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipTitleItem toolTipTitleItem2 = new DevExpress.Utils.ToolTipTitleItem();
			DevExpress.Utils.ToolTipItem toolTipItem2 = new DevExpress.Utils.ToolTipItem();
			DevExpress.Utils.SuperToolTip superToolTip3 = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipTitleItem toolTipTitleItem3 = new DevExpress.Utils.ToolTipTitleItem();
			DevExpress.Utils.ToolTipItem toolTipItem3 = new DevExpress.Utils.ToolTipItem();
			DevExpress.Utils.SuperToolTip superToolTip4 = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipTitleItem toolTipTitleItem4 = new DevExpress.Utils.ToolTipTitleItem();
			DevExpress.Utils.ToolTipItem toolTipItem4 = new DevExpress.Utils.ToolTipItem();
			this.barManager = new DevExpress.XtraBars.BarManager();
			this.barOperations = new DevExpress.XtraBars.Bar();
			this.barLargeButtonItemRegularEmail = new DevExpress.XtraBars.BarLargeButtonItem();
			this.barLargeButtonItemPDFEmail = new DevExpress.XtraBars.BarLargeButtonItem();
			this.barLargeButtonItemHelp = new DevExpress.XtraBars.BarLargeButtonItem();
			this.barLargeButtonItemExit = new DevExpress.XtraBars.BarLargeButtonItem();
			this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
			this.xtraTabControlGroups = new DevExpress.XtraTab.XtraTabControl();
			((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControlGroups)).BeginInit();
			this.SuspendLayout();
			// 
			// barManager
			// 
			this.barManager.AllowCustomization = false;
			this.barManager.AllowItemAnimatedHighlighting = false;
			this.barManager.AllowMoveBarOnToolbar = false;
			this.barManager.AllowQuickCustomization = false;
			this.barManager.AllowShowToolbarsPopup = false;
			this.barManager.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.barOperations});
			this.barManager.DockControls.Add(this.barDockControlTop);
			this.barManager.DockControls.Add(this.barDockControlBottom);
			this.barManager.DockControls.Add(this.barDockControlLeft);
			this.barManager.DockControls.Add(this.barDockControlRight);
			this.barManager.Form = this;
			this.barManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barLargeButtonItemRegularEmail,
            this.barLargeButtonItemHelp,
            this.barLargeButtonItemExit,
            this.barLargeButtonItemPDFEmail});
			this.barManager.MaxItemId = 15;
			// 
			// barOperations
			// 
			this.barOperations.BarName = "Tools";
			this.barOperations.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Top;
			this.barOperations.DockCol = 0;
			this.barOperations.DockRow = 0;
			this.barOperations.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
			this.barOperations.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barLargeButtonItemRegularEmail, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barLargeButtonItemPDFEmail, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barLargeButtonItemHelp, DevExpress.XtraBars.BarItemPaintStyle.Standard),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barLargeButtonItemExit, DevExpress.XtraBars.BarItemPaintStyle.Standard)});
			this.barOperations.OptionsBar.AllowQuickCustomization = false;
			this.barOperations.OptionsBar.DisableClose = true;
			this.barOperations.OptionsBar.DisableCustomization = true;
			this.barOperations.OptionsBar.DrawDragBorder = false;
			this.barOperations.OptionsBar.UseWholeRow = true;
			this.barOperations.Text = "Tools";
			// 
			// barLargeButtonItemRegularEmail
			// 
			this.barLargeButtonItemRegularEmail.Border = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
			this.barLargeButtonItemRegularEmail.Caption = "Send\r\nPowerPoint";
			this.barLargeButtonItemRegularEmail.CaptionAlignment = DevExpress.XtraBars.BarItemCaptionAlignment.Right;
			this.barLargeButtonItemRegularEmail.Glyph = global::Asa.Common.GUI.Properties.Resources.RegularEmail;
			this.barLargeButtonItemRegularEmail.Id = 4;
			this.barLargeButtonItemRegularEmail.Name = "barLargeButtonItemRegularEmail";
			toolTipTitleItem1.Text = "Email PowerPoint file";
			toolTipItem1.LeftIndent = 6;
			toolTipItem1.Text = "Send this file as a PowerPoint email attachment";
			superToolTip1.Items.Add(toolTipTitleItem1);
			superToolTip1.Items.Add(toolTipItem1);
			this.barLargeButtonItemRegularEmail.SuperTip = superToolTip1;
			this.barLargeButtonItemRegularEmail.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemRegularEmail_ItemClick);
			// 
			// barLargeButtonItemPDFEmail
			// 
			this.barLargeButtonItemPDFEmail.Border = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
			this.barLargeButtonItemPDFEmail.Caption = "Send\r\nAdobe PDF";
			this.barLargeButtonItemPDFEmail.CaptionAlignment = DevExpress.XtraBars.BarItemCaptionAlignment.Right;
			this.barLargeButtonItemPDFEmail.Glyph = global::Asa.Common.GUI.Properties.Resources.PDFEmail;
			this.barLargeButtonItemPDFEmail.Id = 14;
			this.barLargeButtonItemPDFEmail.Name = "barLargeButtonItemPDFEmail";
			toolTipTitleItem2.Text = "Email as PDF";
			toolTipItem2.LeftIndent = 6;
			toolTipItem2.Text = "Send this file as a PDF file email attachment";
			superToolTip2.Items.Add(toolTipTitleItem2);
			superToolTip2.Items.Add(toolTipItem2);
			this.barLargeButtonItemPDFEmail.SuperTip = superToolTip2;
			this.barLargeButtonItemPDFEmail.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barLargeButtonItemPDFEmail_ItemClick);
			// 
			// barLargeButtonItemHelp
			// 
			this.barLargeButtonItemHelp.Border = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
			this.barLargeButtonItemHelp.CaptionAlignment = DevExpress.XtraBars.BarItemCaptionAlignment.Right;
			this.barLargeButtonItemHelp.Glyph = global::Asa.Common.GUI.Properties.Resources.Help;
			this.barLargeButtonItemHelp.Id = 6;
			this.barLargeButtonItemHelp.Name = "barLargeButtonItemHelp";
			toolTipTitleItem3.Text = "HELP";
			toolTipItem3.LeftIndent = 6;
			toolTipItem3.Text = "Help me understand how to Email my Schedules";
			superToolTip3.Items.Add(toolTipTitleItem3);
			superToolTip3.Items.Add(toolTipItem3);
			this.barLargeButtonItemHelp.SuperTip = superToolTip3;
			this.barLargeButtonItemHelp.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barLargeButtonItemHelp_ItemClick);
			// 
			// barLargeButtonItemExit
			// 
			this.barLargeButtonItemExit.Border = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
			this.barLargeButtonItemExit.CaptionAlignment = DevExpress.XtraBars.BarItemCaptionAlignment.Right;
			this.barLargeButtonItemExit.Glyph = global::Asa.Common.GUI.Properties.Resources.Exit;
			this.barLargeButtonItemExit.Id = 7;
			this.barLargeButtonItemExit.Name = "barLargeButtonItemExit";
			toolTipTitleItem4.Text = "EXIT";
			toolTipItem4.LeftIndent = 6;
			toolTipItem4.Text = "Close this Window ";
			superToolTip4.Items.Add(toolTipTitleItem4);
			superToolTip4.Items.Add(toolTipItem4);
			this.barLargeButtonItemExit.SuperTip = superToolTip4;
			this.barLargeButtonItemExit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barLargeButtonItemExit_ItemClick);
			// 
			// barDockControlTop
			// 
			this.barDockControlTop.CausesValidation = false;
			this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.barDockControlTop.ForeColor = System.Drawing.Color.Black;
			this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
			this.barDockControlTop.Size = new System.Drawing.Size(934, 88);
			// 
			// barDockControlBottom
			// 
			this.barDockControlBottom.CausesValidation = false;
			this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.barDockControlBottom.ForeColor = System.Drawing.Color.Black;
			this.barDockControlBottom.Location = new System.Drawing.Point(0, 562);
			this.barDockControlBottom.Size = new System.Drawing.Size(934, 0);
			// 
			// barDockControlLeft
			// 
			this.barDockControlLeft.CausesValidation = false;
			this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
			this.barDockControlLeft.ForeColor = System.Drawing.Color.Black;
			this.barDockControlLeft.Location = new System.Drawing.Point(0, 88);
			this.barDockControlLeft.Size = new System.Drawing.Size(0, 474);
			// 
			// barDockControlRight
			// 
			this.barDockControlRight.CausesValidation = false;
			this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
			this.barDockControlRight.ForeColor = System.Drawing.Color.Black;
			this.barDockControlRight.Location = new System.Drawing.Point(934, 88);
			this.barDockControlRight.Size = new System.Drawing.Size(0, 474);
			// 
			// xtraTabControlGroups
			// 
			this.xtraTabControlGroups.Appearance.BackColor = System.Drawing.Color.White;
			this.xtraTabControlGroups.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControlGroups.Appearance.ForeColor = System.Drawing.Color.Black;
			this.xtraTabControlGroups.Appearance.Options.UseBackColor = true;
			this.xtraTabControlGroups.Appearance.Options.UseFont = true;
			this.xtraTabControlGroups.Appearance.Options.UseForeColor = true;
			this.xtraTabControlGroups.AppearancePage.Header.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlGroups.AppearancePage.Header.Options.UseFont = true;
			this.xtraTabControlGroups.AppearancePage.HeaderActive.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControlGroups.AppearancePage.HeaderActive.Options.UseFont = true;
			this.xtraTabControlGroups.AppearancePage.HeaderDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlGroups.AppearancePage.HeaderDisabled.Options.UseFont = true;
			this.xtraTabControlGroups.AppearancePage.HeaderHotTracked.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlGroups.AppearancePage.HeaderHotTracked.Options.UseFont = true;
			this.xtraTabControlGroups.AppearancePage.PageClient.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlGroups.AppearancePage.PageClient.Options.UseFont = true;
			this.xtraTabControlGroups.Dock = System.Windows.Forms.DockStyle.Fill;
			this.xtraTabControlGroups.Location = new System.Drawing.Point(0, 88);
			this.xtraTabControlGroups.Name = "xtraTabControlGroups";
			this.xtraTabControlGroups.Size = new System.Drawing.Size(934, 474);
			this.xtraTabControlGroups.TabIndex = 11;
			// 
			// FormEmail
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(934, 562);
			this.Controls.Add(this.xtraTabControlGroups);
			this.Controls.Add(this.barDockControlLeft);
			this.Controls.Add(this.barDockControlRight);
			this.Controls.Add(this.barDockControlBottom);
			this.Controls.Add(this.barDockControlTop);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "FormEmail";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Email this Basic Overview";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormQuickView_FormClosed);
			this.Shown += new System.EventHandler(this.FormEmail_Shown);
			((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControlGroups)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager;
        private DevExpress.XtraBars.Bar barOperations;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarLargeButtonItem barLargeButtonItemRegularEmail;
        private DevExpress.XtraBars.BarLargeButtonItem barLargeButtonItemHelp;
		private DevExpress.XtraBars.BarLargeButtonItem barLargeButtonItemExit;
        private DevExpress.XtraBars.BarLargeButtonItem barLargeButtonItemPDFEmail;
		private DevExpress.XtraTab.XtraTabControl xtraTabControlGroups;

    }
}