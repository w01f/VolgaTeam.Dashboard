namespace Asa.Media.Controls.PresentationClasses.ScheduleControls.ContentEditors
{
	partial class SectionContainer
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
			this.xtraTabControl = new DevExpress.XtraTab.XtraTabControl();
			this.labelControlWarnings = new DevExpress.XtraEditors.LabelControl();
			this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
			this.pnInfo = new System.Windows.Forms.Panel();
			this.labelControlCollectionItemsInfo = new DevExpress.XtraEditors.LabelControl();
			this.contextMenuStripSchedule = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripMenuItemScheduleConvertToOptionsSet = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuStripDigital = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripMenuItemDigitalToSnapshot = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemDigitalToOptions = new System.Windows.Forms.ToolStripMenuItem();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			this.pnInfo.SuspendLayout();
			this.contextMenuStripSchedule.SuspendLayout();
			this.contextMenuStripDigital.SuspendLayout();
			this.SuspendLayout();
			// 
			// xtraTabControl
			// 
			this.xtraTabControl.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControl.Appearance.Options.UseFont = true;
			this.xtraTabControl.AppearancePage.Header.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControl.AppearancePage.Header.Options.UseFont = true;
			this.xtraTabControl.AppearancePage.HeaderActive.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControl.AppearancePage.HeaderActive.Options.UseFont = true;
			this.xtraTabControl.AppearancePage.HeaderDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControl.AppearancePage.HeaderDisabled.Options.UseFont = true;
			this.xtraTabControl.AppearancePage.HeaderHotTracked.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControl.AppearancePage.HeaderHotTracked.Options.UseFont = true;
			this.xtraTabControl.AppearancePage.PageClient.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControl.AppearancePage.PageClient.Options.UseFont = true;
			this.xtraTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.xtraTabControl.Location = new System.Drawing.Point(0, 32);
			this.xtraTabControl.Name = "xtraTabControl";
			this.xtraTabControl.Size = new System.Drawing.Size(712, 533);
			this.xtraTabControl.TabIndex = 0;
			this.xtraTabControl.UseMnemonic = false;
			this.xtraTabControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnTabControlMouseDown);
			// 
			// labelControlWarnings
			// 
			this.labelControlWarnings.AllowHtmlString = true;
			this.labelControlWarnings.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.labelControlWarnings.Appearance.ForeColor = System.Drawing.Color.Red;
			this.labelControlWarnings.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			this.labelControlWarnings.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.labelControlWarnings.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.labelControlWarnings.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.labelControlWarnings.Cursor = System.Windows.Forms.Cursors.Hand;
			this.labelControlWarnings.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelControlWarnings.Location = new System.Drawing.Point(336, 0);
			this.labelControlWarnings.Name = "labelControlWarnings";
			this.labelControlWarnings.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
			this.labelControlWarnings.Size = new System.Drawing.Size(376, 32);
			this.labelControlWarnings.StyleController = this.styleController;
			this.labelControlWarnings.TabIndex = 9;
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
			// pnInfo
			// 
			this.pnInfo.Controls.Add(this.labelControlWarnings);
			this.pnInfo.Controls.Add(this.labelControlCollectionItemsInfo);
			this.pnInfo.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnInfo.Location = new System.Drawing.Point(0, 0);
			this.pnInfo.Name = "pnInfo";
			this.pnInfo.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
			this.pnInfo.Size = new System.Drawing.Size(712, 32);
			this.pnInfo.TabIndex = 10;
			// 
			// labelControlCollectionItemsInfo
			// 
			this.labelControlCollectionItemsInfo.AllowHtmlString = true;
			this.labelControlCollectionItemsInfo.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.labelControlCollectionItemsInfo.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.labelControlCollectionItemsInfo.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.labelControlCollectionItemsInfo.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.labelControlCollectionItemsInfo.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.labelControlCollectionItemsInfo.Cursor = System.Windows.Forms.Cursors.Hand;
			this.labelControlCollectionItemsInfo.Dock = System.Windows.Forms.DockStyle.Left;
			this.labelControlCollectionItemsInfo.Location = new System.Drawing.Point(10, 0);
			this.labelControlCollectionItemsInfo.Name = "labelControlCollectionItemsInfo";
			this.labelControlCollectionItemsInfo.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
			this.labelControlCollectionItemsInfo.Size = new System.Drawing.Size(326, 32);
			this.labelControlCollectionItemsInfo.StyleController = this.styleController;
			this.labelControlCollectionItemsInfo.TabIndex = 10;
			// 
			// contextMenuStripSchedule
			// 
			this.contextMenuStripSchedule.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemScheduleConvertToOptionsSet});
			this.contextMenuStripSchedule.Name = "contextMenuStripSchedule";
			this.contextMenuStripSchedule.ShowImageMargin = false;
			this.contextMenuStripSchedule.Size = new System.Drawing.Size(134, 26);
			// 
			// toolStripMenuItemScheduleConvertToOptionsSet
			// 
			this.toolStripMenuItemScheduleConvertToOptionsSet.Name = "toolStripMenuItemScheduleConvertToOptionsSet";
			this.toolStripMenuItemScheduleConvertToOptionsSet.Size = new System.Drawing.Size(133, 22);
			this.toolStripMenuItemScheduleConvertToOptionsSet.Text = "Create Flex-Grid";
			this.toolStripMenuItemScheduleConvertToOptionsSet.Click += new System.EventHandler(this.OnScheduleConvertToOptionsSetClick);
			// 
			// contextMenuStripDigital
			// 
			this.contextMenuStripDigital.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemDigitalToSnapshot,
            this.toolStripMenuItemDigitalToOptions});
			this.contextMenuStripDigital.Name = "contextMenuStripSchedule";
			this.contextMenuStripDigital.ShowImageMargin = false;
			this.contextMenuStripDigital.Size = new System.Drawing.Size(142, 48);
			// 
			// toolStripMenuItemDigitalToSnapshot
			// 
			this.toolStripMenuItemDigitalToSnapshot.Name = "toolStripMenuItemDigitalToSnapshot";
			this.toolStripMenuItemDigitalToSnapshot.Size = new System.Drawing.Size(141, 22);
			this.toolStripMenuItemDigitalToSnapshot.Text = "Send to Snapshot";
			// 
			// toolStripMenuItemDigitalToOptions
			// 
			this.toolStripMenuItemDigitalToOptions.Name = "toolStripMenuItemDigitalToOptions";
			this.toolStripMenuItemDigitalToOptions.Size = new System.Drawing.Size(141, 22);
			this.toolStripMenuItemDigitalToOptions.Text = "Send to Flex-Grid";
			// 
			// SectionContainer
			// 
			this.Controls.Add(this.xtraTabControl);
			this.Controls.Add(this.pnInfo);
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "SectionContainer";
			this.Size = new System.Drawing.Size(712, 565);
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			this.pnInfo.ResumeLayout(false);
			this.contextMenuStripSchedule.ResumeLayout(false);
			this.contextMenuStripDigital.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraTab.XtraTabControl xtraTabControl;
		private DevExpress.XtraEditors.LabelControl labelControlWarnings;
		private DevExpress.XtraEditors.StyleController styleController;
		private System.Windows.Forms.Panel pnInfo;
		private DevExpress.XtraEditors.LabelControl labelControlCollectionItemsInfo;
		private System.Windows.Forms.ContextMenuStrip contextMenuStripSchedule;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemScheduleConvertToOptionsSet;
		private System.Windows.Forms.ContextMenuStrip contextMenuStripDigital;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDigitalToSnapshot;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDigitalToOptions;
	}
}
