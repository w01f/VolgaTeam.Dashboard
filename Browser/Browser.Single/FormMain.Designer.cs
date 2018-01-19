namespace Asa.Browser.Single
{
	partial class FormMain
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
			this.styleManager = new DevComponents.DotNetBar.StyleManager();
			this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel();
			this.barBottom = new DevComponents.DotNetBar.Bar();
			this.itemContainerStatusBarInfo = new DevComponents.DotNetBar.ItemContainer();
			this.labelItemAppTitle = new DevComponents.DotNetBar.LabelItem();
			this.labelItemUrl = new DevComponents.DotNetBar.LabelItem();
			this.labelItemStatusBarSeparator = new DevComponents.DotNetBar.LabelItem();
			this.itemContainerStatusBarActionButtons = new DevComponents.DotNetBar.ItemContainer();
			this.ribbonControl = new DevComponents.DotNetBar.RibbonControl();
			this.panelMain = new System.Windows.Forms.Panel();
			this.panelEmpty = new System.Windows.Forms.Panel();
			((System.ComponentModel.ISupportInitialize)(this.barBottom)).BeginInit();
			this.SuspendLayout();
			// 
			// styleManager
			// 
			this.styleManager.ManagerStyle = DevComponents.DotNetBar.eStyle.Metro;
			this.styleManager.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.White, System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(87)))), ((int)(((byte)(154))))));
			// 
			// defaultLookAndFeel
			// 
			this.defaultLookAndFeel.LookAndFeel.SkinName = "Office 2013";
			// 
			// barBottom
			// 
			this.barBottom.AntiAlias = true;
			this.barBottom.BarType = DevComponents.DotNetBar.eBarType.StatusBar;
			this.barBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.barBottom.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.barBottom.IsMaximized = false;
			this.barBottom.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.itemContainerStatusBarInfo,
            this.labelItemStatusBarSeparator,
            this.itemContainerStatusBarActionButtons});
			this.barBottom.Location = new System.Drawing.Point(5, 527);
			this.barBottom.Name = "barBottom";
			this.barBottom.PaddingBottom = 0;
			this.barBottom.PaddingTop = 0;
			this.barBottom.Size = new System.Drawing.Size(874, 26);
			this.barBottom.Stretch = true;
			this.barBottom.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.barBottom.TabIndex = 1;
			this.barBottom.TabStop = false;
			// 
			// itemContainerStatusBarInfo
			// 
			// 
			// 
			// 
			this.itemContainerStatusBarInfo.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.itemContainerStatusBarInfo.ItemSpacing = 20;
			this.itemContainerStatusBarInfo.MinimumSize = new System.Drawing.Size(0, 24);
			this.itemContainerStatusBarInfo.Name = "itemContainerStatusBarInfo";
			this.itemContainerStatusBarInfo.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.labelItemAppTitle,
            this.labelItemUrl});
			// 
			// 
			// 
			this.itemContainerStatusBarInfo.TitleMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.itemContainerStatusBarInfo.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.itemContainerStatusBarInfo.VerticalItemAlignment = DevComponents.DotNetBar.eVerticalItemsAlignment.Middle;
			// 
			// labelItemAppTitle
			// 
			this.labelItemAppTitle.Name = "labelItemAppTitle";
			this.labelItemAppTitle.Text = "Sales Browser";
			// 
			// labelItemUrl
			// 
			this.labelItemUrl.Name = "labelItemUrl";
			// 
			// labelItemStatusBarSeparator
			// 
			this.labelItemStatusBarSeparator.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far;
			this.labelItemStatusBarSeparator.Name = "labelItemStatusBarSeparator";
			// 
			// itemContainerStatusBarActionButtons
			// 
			// 
			// 
			// 
			this.itemContainerStatusBarActionButtons.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.itemContainerStatusBarActionButtons.ItemSpacing = 5;
			this.itemContainerStatusBarActionButtons.MinimumSize = new System.Drawing.Size(0, 24);
			this.itemContainerStatusBarActionButtons.Name = "itemContainerStatusBarActionButtons";
			// 
			// 
			// 
			this.itemContainerStatusBarActionButtons.TitleMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.itemContainerStatusBarActionButtons.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.itemContainerStatusBarActionButtons.VerticalItemAlignment = DevComponents.DotNetBar.eVerticalItemsAlignment.Middle;
			// 
			// ribbonControl
			// 
			this.ribbonControl.BackColor = System.Drawing.Color.White;
			// 
			// 
			// 
			this.ribbonControl.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonControl.CanCustomize = false;
			this.ribbonControl.CaptionVisible = true;
			this.ribbonControl.Dock = System.Windows.Forms.DockStyle.Top;
			this.ribbonControl.EnableQatPlacement = false;
			this.ribbonControl.ForeColor = System.Drawing.Color.Black;
			this.ribbonControl.KeyTipsFont = new System.Drawing.Font("Tahoma", 7F);
			this.ribbonControl.Location = new System.Drawing.Point(5, 1);
			this.ribbonControl.MdiSystemItemVisible = false;
			this.ribbonControl.Name = "ribbonControl";
			this.ribbonControl.Size = new System.Drawing.Size(874, 28);
			this.ribbonControl.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonControl.SystemText.MaximizeRibbonText = "&Maximize the Ribbon";
			this.ribbonControl.SystemText.MinimizeRibbonText = "Mi&nimize the Ribbon";
			this.ribbonControl.SystemText.QatAddItemText = "&Add to Quick Access Toolbar";
			this.ribbonControl.SystemText.QatCustomizeMenuLabel = "<b>Customize Quick Access Toolbar</b>";
			this.ribbonControl.SystemText.QatCustomizeText = "&Customize Quick Access Toolbar...";
			this.ribbonControl.SystemText.QatDialogAddButton = "&Add >>";
			this.ribbonControl.SystemText.QatDialogCancelButton = "Cancel";
			this.ribbonControl.SystemText.QatDialogCaption = "Customize Quick Access Toolbar";
			this.ribbonControl.SystemText.QatDialogCategoriesLabel = "&Choose commands from:";
			this.ribbonControl.SystemText.QatDialogOkButton = "OK";
			this.ribbonControl.SystemText.QatDialogPlacementCheckbox = "&Place Quick Access Toolbar below the Ribbon";
			this.ribbonControl.SystemText.QatDialogRemoveButton = "&Remove";
			this.ribbonControl.SystemText.QatPlaceAboveRibbonText = "&Place Quick Access Toolbar above the Ribbon";
			this.ribbonControl.SystemText.QatPlaceBelowRibbonText = "&Place Quick Access Toolbar below the Ribbon";
			this.ribbonControl.SystemText.QatRemoveItemText = "&Remove from Quick Access Toolbar";
			this.ribbonControl.TabGroupHeight = 14;
			this.ribbonControl.TabIndex = 2;
			this.ribbonControl.UseCustomizeDialog = false;
			// 
			// panelMain
			// 
			this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelMain.Location = new System.Drawing.Point(5, 29);
			this.panelMain.Name = "panelMain";
			this.panelMain.Size = new System.Drawing.Size(874, 498);
			this.panelMain.TabIndex = 3;
			// 
			// panelEmpty
			// 
			this.panelEmpty.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelEmpty.Location = new System.Drawing.Point(5, 1);
			this.panelEmpty.Name = "panelEmpty";
			this.panelEmpty.Size = new System.Drawing.Size(874, 552);
			this.panelEmpty.TabIndex = 4;
			// 
			// FormMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(884, 555);
			this.Controls.Add(this.panelMain);
			this.Controls.Add(this.ribbonControl);
			this.Controls.Add(this.barBottom);
			this.Controls.Add(this.panelEmpty);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Browser";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OnFormClosed);
			((System.ComponentModel.ISupportInitialize)(this.barBottom)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevComponents.DotNetBar.StyleManager styleManager;
		private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
		private DevComponents.DotNetBar.ItemContainer itemContainerStatusBarInfo;
		private DevComponents.DotNetBar.LabelItem labelItemStatusBarSeparator;
		private DevComponents.DotNetBar.LabelItem labelItemAppTitle;
		private DevComponents.DotNetBar.LabelItem labelItemUrl;
		private DevComponents.DotNetBar.RibbonControl ribbonControl;
		private System.Windows.Forms.Panel panelMain;
		private System.Windows.Forms.Panel panelEmpty;
		public DevComponents.DotNetBar.Bar barBottom;
		public DevComponents.DotNetBar.ItemContainer itemContainerStatusBarActionButtons;
	}
}

