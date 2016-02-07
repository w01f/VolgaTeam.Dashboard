namespace Asa.Common.GUI.Preview
{
    partial class FormPreview
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
			this.components = new System.ComponentModel.Container();
			DevExpress.Utils.SuperToolTip superToolTip4 = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipTitleItem toolTipTitleItem4 = new DevExpress.Utils.ToolTipTitleItem();
			DevExpress.Utils.ToolTipItem toolTipItem4 = new DevExpress.Utils.ToolTipItem();
			DevExpress.Utils.SuperToolTip superToolTip5 = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipTitleItem toolTipTitleItem5 = new DevExpress.Utils.ToolTipTitleItem();
			DevExpress.Utils.ToolTipItem toolTipItem5 = new DevExpress.Utils.ToolTipItem();
			DevExpress.Utils.SuperToolTip superToolTip6 = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipTitleItem toolTipTitleItem6 = new DevExpress.Utils.ToolTipTitleItem();
			DevExpress.Utils.ToolTipItem toolTipItem6 = new DevExpress.Utils.ToolTipItem();
			this.barManager = new DevExpress.XtraBars.BarManager(this.components);
			this.barOperations = new DevExpress.XtraBars.Bar();
			this.barLargeButtonItemOutput = new DevExpress.XtraBars.BarLargeButtonItem();
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
            this.barLargeButtonItemOutput,
            this.barLargeButtonItemHelp,
            this.barLargeButtonItemExit});
			this.barManager.MaxItemId = 16;
			// 
			// barOperations
			// 
			this.barOperations.BarName = "Tools";
			this.barOperations.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Top;
			this.barOperations.DockCol = 0;
			this.barOperations.DockRow = 0;
			this.barOperations.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
			this.barOperations.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barLargeButtonItemOutput, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barLargeButtonItemHelp, DevExpress.XtraBars.BarItemPaintStyle.Standard),
            new DevExpress.XtraBars.LinkPersistInfo(this.barLargeButtonItemExit)});
			this.barOperations.OptionsBar.AllowQuickCustomization = false;
			this.barOperations.OptionsBar.DisableClose = true;
			this.barOperations.OptionsBar.DisableCustomization = true;
			this.barOperations.OptionsBar.DrawDragBorder = false;
			this.barOperations.OptionsBar.UseWholeRow = true;
			this.barOperations.Text = "Tools";
			// 
			// barLargeButtonItemOutput
			// 
			this.barLargeButtonItemOutput.Border = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
			this.barLargeButtonItemOutput.Caption = "Send to\r\nPowerPoint";
			this.barLargeButtonItemOutput.CaptionAlignment = DevExpress.XtraBars.BarItemCaptionAlignment.Right;
			this.barLargeButtonItemOutput.Glyph = global::Asa.Common.GUI.Properties.Resources.PowerPoint;
			this.barLargeButtonItemOutput.Id = 4;
			this.barLargeButtonItemOutput.ItemAppearance.Normal.ForeColor = System.Drawing.Color.Black;
			this.barLargeButtonItemOutput.ItemAppearance.Normal.Options.UseForeColor = true;
			this.barLargeButtonItemOutput.Name = "barLargeButtonItemOutput";
			toolTipTitleItem4.Text = "Send to PowerPoint";
			toolTipItem4.LeftIndent = 6;
			toolTipItem4.Text = "Send this Schedule to PowerPoint";
			superToolTip4.Items.Add(toolTipTitleItem4);
			superToolTip4.Items.Add(toolTipItem4);
			this.barLargeButtonItemOutput.SuperTip = superToolTip4;
			this.barLargeButtonItemOutput.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemOutput_ItemClick);
			// 
			// barLargeButtonItemHelp
			// 
			this.barLargeButtonItemHelp.Border = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
			this.barLargeButtonItemHelp.CaptionAlignment = DevExpress.XtraBars.BarItemCaptionAlignment.Right;
			this.barLargeButtonItemHelp.Glyph = global::Asa.Common.GUI.Properties.Resources.Help;
			this.barLargeButtonItemHelp.Id = 6;
			this.barLargeButtonItemHelp.Name = "barLargeButtonItemHelp";
			toolTipTitleItem5.Text = "HELP";
			toolTipItem5.LeftIndent = 6;
			toolTipItem5.Text = "Learn more about how to preview your schedules";
			superToolTip5.Items.Add(toolTipTitleItem5);
			superToolTip5.Items.Add(toolTipItem5);
			this.barLargeButtonItemHelp.SuperTip = superToolTip5;
			this.barLargeButtonItemHelp.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barLargeButtonItemHelp_ItemClick);
			// 
			// barLargeButtonItemExit
			// 
			this.barLargeButtonItemExit.Border = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
			this.barLargeButtonItemExit.Caption = "Exit";
			this.barLargeButtonItemExit.Glyph = global::Asa.Common.GUI.Properties.Resources.Exit;
			this.barLargeButtonItemExit.Id = 15;
			this.barLargeButtonItemExit.Name = "barLargeButtonItemExit";
			toolTipTitleItem6.Text = "EXIT";
			toolTipItem6.LeftIndent = 6;
			toolTipItem6.Text = "Close this Window ";
			superToolTip6.Items.Add(toolTipTitleItem6);
			superToolTip6.Items.Add(toolTipItem6);
			this.barLargeButtonItemExit.SuperTip = superToolTip6;
			this.barLargeButtonItemExit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barLargeButtonItemExit_ItemClick);
			// 
			// barDockControlTop
			// 
			this.barDockControlTop.CausesValidation = false;
			this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.barDockControlTop.ForeColor = System.Drawing.Color.Black;
			this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
			this.barDockControlTop.Size = new System.Drawing.Size(934, 91);
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
			this.barDockControlLeft.Location = new System.Drawing.Point(0, 91);
			this.barDockControlLeft.Size = new System.Drawing.Size(0, 471);
			// 
			// barDockControlRight
			// 
			this.barDockControlRight.CausesValidation = false;
			this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
			this.barDockControlRight.ForeColor = System.Drawing.Color.Black;
			this.barDockControlRight.Location = new System.Drawing.Point(934, 91);
			this.barDockControlRight.Size = new System.Drawing.Size(0, 471);
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
			this.xtraTabControlGroups.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.xtraTabControlGroups.Dock = System.Windows.Forms.DockStyle.Fill;
			this.xtraTabControlGroups.Location = new System.Drawing.Point(0, 91);
			this.xtraTabControlGroups.Name = "xtraTabControlGroups";
			this.xtraTabControlGroups.Size = new System.Drawing.Size(934, 471);
			this.xtraTabControlGroups.TabIndex = 10;
			// 
			// FormPreview
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
			this.Name = "FormPreview";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Quick View";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormQuickView_FormClosed);
			this.Shown += new System.EventHandler(this.FormPreview_Shown);
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
        private DevExpress.XtraBars.BarLargeButtonItem barLargeButtonItemOutput;
		private DevExpress.XtraBars.BarLargeButtonItem barLargeButtonItemHelp;
		private DevExpress.XtraBars.BarLargeButtonItem barLargeButtonItemExit;
		private DevExpress.XtraTab.XtraTabControl xtraTabControlGroups;

    }
}