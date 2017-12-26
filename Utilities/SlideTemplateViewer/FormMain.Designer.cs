
namespace Asa.SlideTemplateViewer
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
			this.ribbonControl = new DevComponents.DotNetBar.RibbonControl();
			this.ribbonPanelSlides = new DevComponents.DotNetBar.RibbonPanel();
			this.ribbonBarSlidesPowerPoint = new DevComponents.DotNetBar.RibbonBar();
			this.buttonItemSlidesPowerPoint = new DevComponents.DotNetBar.ButtonItem();
			this.ribbonBarSlidesPreview = new DevComponents.DotNetBar.RibbonBar();
			this.buttonItemSlidesPreview = new DevComponents.DotNetBar.ButtonItem();
			this.ribbonBarSlidesLogo = new DevComponents.DotNetBar.RibbonBar();
			this.labelItemSlidesLogo = new DevComponents.DotNetBar.LabelItem();
			this.applicationButtonApplicationMenu = new DevComponents.DotNetBar.ApplicationButton();
			this.buttonItemApplicationMenuSlideSettings = new DevComponents.DotNetBar.ButtonItem();
			this.buttonItemApplicationMenuHelp = new DevComponents.DotNetBar.ButtonItem();
			this.buttonItemApplicationMenuExit = new DevComponents.DotNetBar.ButtonItem();
			this.ribbonTabItemSlides = new DevComponents.DotNetBar.RibbonTabItem();
			this.buttonItemCollapse = new DevComponents.DotNetBar.ButtonItem();
			this.buttonItemExpand = new DevComponents.DotNetBar.ButtonItem();
			this.buttonItemPin = new DevComponents.DotNetBar.ButtonItem();
			this.buttonItemQatFloater = new DevComponents.DotNetBar.ButtonItem();
			this.buttonItemQatHelp = new DevComponents.DotNetBar.ButtonItem();
			this.superTooltip = new DevComponents.DotNetBar.SuperTooltip();
			this.styleManager = new DevComponents.DotNetBar.StyleManager();
			this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel();
			this.barBottom = new DevComponents.DotNetBar.Bar();
			this.itemContainerStatusBarMainInfo = new DevComponents.DotNetBar.ItemContainer();
			this.labelItemAppTitle = new DevComponents.DotNetBar.LabelItem();
			this.labelItemStatusBarSeparator = new DevComponents.DotNetBar.LabelItem();
			this.itemContainerStatusBarAdditionalInfo = new DevComponents.DotNetBar.ItemContainer();
			this.labelItemSlideSize = new DevComponents.DotNetBar.LabelItem();
			this.ribbonControl.SuspendLayout();
			this.ribbonPanelSlides.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.barBottom)).BeginInit();
			this.SuspendLayout();
			// 
			// ribbonControl
			// 
			this.ribbonControl.BackColor = System.Drawing.Color.White;
			// 
			// 
			// 
			this.ribbonControl.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonControl.CaptionVisible = true;
			this.ribbonControl.Controls.Add(this.ribbonPanelSlides);
			this.ribbonControl.Dock = System.Windows.Forms.DockStyle.Top;
			this.ribbonControl.ForeColor = System.Drawing.Color.Black;
			this.ribbonControl.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.applicationButtonApplicationMenu,
            this.ribbonTabItemSlides,
            this.buttonItemCollapse,
            this.buttonItemExpand,
            this.buttonItemPin});
			this.ribbonControl.KeyTipsFont = new System.Drawing.Font("Tahoma", 7F);
			this.ribbonControl.Location = new System.Drawing.Point(5, 1);
			this.ribbonControl.MdiSystemItemVisible = false;
			this.ribbonControl.Name = "ribbonControl";
			this.ribbonControl.QuickToolbarItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItemQatFloater,
            this.buttonItemQatHelp});
			this.ribbonControl.Size = new System.Drawing.Size(915, 156);
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
			this.ribbonControl.TabIndex = 5;
			this.ribbonControl.AfterRibbonPanelPopup += new System.EventHandler(this.OnRibbonAfterPanelPopup);
			this.ribbonControl.AfterRibbonPanelPopupClose += new System.EventHandler(this.OnRibbonAfterPanelPopupClose);
			this.ribbonControl.ExpandedChanged += new System.EventHandler(this.OnRibbonExpandedChanged);
			// 
			// ribbonPanelSlides
			// 
			this.ribbonPanelSlides.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonPanelSlides.Controls.Add(this.ribbonBarSlidesPowerPoint);
			this.ribbonPanelSlides.Controls.Add(this.ribbonBarSlidesPreview);
			this.ribbonPanelSlides.Controls.Add(this.ribbonBarSlidesLogo);
			this.ribbonPanelSlides.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ribbonPanelSlides.Location = new System.Drawing.Point(0, 54);
			this.ribbonPanelSlides.Name = "ribbonPanelSlides";
			this.ribbonPanelSlides.Padding = new System.Windows.Forms.Padding(3, 0, 3, 2);
			this.ribbonPanelSlides.Size = new System.Drawing.Size(915, 102);
			// 
			// 
			// 
			this.ribbonPanelSlides.Style.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonPanelSlides.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonPanelSlides.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonPanelSlides.TabIndex = 23;
			// 
			// ribbonBarSlidesPowerPoint
			// 
			this.ribbonBarSlidesPowerPoint.AutoOverflowEnabled = true;
			// 
			// 
			// 
			this.ribbonBarSlidesPowerPoint.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarSlidesPowerPoint.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarSlidesPowerPoint.ContainerControlProcessDialogKey = true;
			this.ribbonBarSlidesPowerPoint.Dock = System.Windows.Forms.DockStyle.Left;
			this.ribbonBarSlidesPowerPoint.DragDropSupport = true;
			this.ribbonBarSlidesPowerPoint.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItemSlidesPowerPoint});
			this.ribbonBarSlidesPowerPoint.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			this.ribbonBarSlidesPowerPoint.Location = new System.Drawing.Point(298, 0);
			this.ribbonBarSlidesPowerPoint.Name = "ribbonBarSlidesPowerPoint";
			this.ribbonBarSlidesPowerPoint.Size = new System.Drawing.Size(88, 100);
			this.ribbonBarSlidesPowerPoint.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonBarSlidesPowerPoint.TabIndex = 30;
			this.ribbonBarSlidesPowerPoint.Text = "PowerPoint";
			// 
			// 
			// 
			this.ribbonBarSlidesPowerPoint.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarSlidesPowerPoint.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// buttonItemSlidesPowerPoint
			// 
			this.buttonItemSlidesPowerPoint.Image = global::Asa.SlideTemplateViewer.Properties.Resources.Output;
			this.buttonItemSlidesPowerPoint.Name = "buttonItemSlidesPowerPoint";
			this.buttonItemSlidesPowerPoint.SubItemsExpandWidth = 14;
			this.superTooltip.SetSuperTooltip(this.buttonItemSlidesPowerPoint, new DevComponents.DotNetBar.SuperTooltipInfo("Insert Slide", "", "Add this slide to my Presentation…", null, null, DevComponents.DotNetBar.eTooltipColor.Gray));
			// 
			// ribbonBarSlidesPreview
			// 
			this.ribbonBarSlidesPreview.AutoOverflowEnabled = true;
			// 
			// 
			// 
			this.ribbonBarSlidesPreview.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarSlidesPreview.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarSlidesPreview.ContainerControlProcessDialogKey = true;
			this.ribbonBarSlidesPreview.Dock = System.Windows.Forms.DockStyle.Left;
			this.ribbonBarSlidesPreview.DragDropSupport = true;
			this.ribbonBarSlidesPreview.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItemSlidesPreview});
			this.ribbonBarSlidesPreview.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			this.ribbonBarSlidesPreview.Location = new System.Drawing.Point(204, 0);
			this.ribbonBarSlidesPreview.Name = "ribbonBarSlidesPreview";
			this.ribbonBarSlidesPreview.Size = new System.Drawing.Size(94, 100);
			this.ribbonBarSlidesPreview.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonBarSlidesPreview.TabIndex = 33;
			this.ribbonBarSlidesPreview.Text = "Preview";
			// 
			// 
			// 
			this.ribbonBarSlidesPreview.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarSlidesPreview.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// buttonItemSlidesPreview
			// 
			this.buttonItemSlidesPreview.Image = global::Asa.SlideTemplateViewer.Properties.Resources.Preview;
			this.buttonItemSlidesPreview.Name = "buttonItemSlidesPreview";
			this.buttonItemSlidesPreview.SubItemsExpandWidth = 14;
			this.superTooltip.SetSuperTooltip(this.buttonItemSlidesPreview, new DevComponents.DotNetBar.SuperTooltipInfo("Slide Preview", "", "View this slide before you add it to your presentation…", null, null, DevComponents.DotNetBar.eTooltipColor.Gray));
			this.buttonItemSlidesPreview.Text = "buttonItem1";
			// 
			// ribbonBarSlidesLogo
			// 
			this.ribbonBarSlidesLogo.AutoOverflowEnabled = true;
			// 
			// 
			// 
			this.ribbonBarSlidesLogo.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarSlidesLogo.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarSlidesLogo.ContainerControlProcessDialogKey = true;
			this.ribbonBarSlidesLogo.Dock = System.Windows.Forms.DockStyle.Left;
			this.ribbonBarSlidesLogo.DragDropSupport = true;
			this.ribbonBarSlidesLogo.HorizontalItemAlignment = DevComponents.DotNetBar.eHorizontalItemsAlignment.Center;
			this.ribbonBarSlidesLogo.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.labelItemSlidesLogo});
			this.ribbonBarSlidesLogo.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			this.ribbonBarSlidesLogo.Location = new System.Drawing.Point(3, 0);
			this.ribbonBarSlidesLogo.Name = "ribbonBarSlidesLogo";
			this.ribbonBarSlidesLogo.Size = new System.Drawing.Size(201, 100);
			this.ribbonBarSlidesLogo.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonBarSlidesLogo.TabIndex = 23;
			this.ribbonBarSlidesLogo.Text = "GO GET YOUR BIZ!";
			// 
			// 
			// 
			this.ribbonBarSlidesLogo.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarSlidesLogo.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarSlidesLogo.VerticalItemAlignment = DevComponents.DotNetBar.eVerticalItemsAlignment.Middle;
			// 
			// labelItemSlidesLogo
			// 
			this.labelItemSlidesLogo.Image = global::Asa.SlideTemplateViewer.Properties.Resources.AddSlidesLogo;
			this.labelItemSlidesLogo.Name = "labelItemSlidesLogo";
			// 
			// applicationButtonApplicationMenu
			// 
			this.applicationButtonApplicationMenu.AutoExpandOnClick = true;
			this.applicationButtonApplicationMenu.CanCustomize = false;
			this.applicationButtonApplicationMenu.HotTrackingStyle = DevComponents.DotNetBar.eHotTrackingStyle.Image;
			this.applicationButtonApplicationMenu.Image = ((System.Drawing.Image)(resources.GetObject("applicationButtonApplicationMenu.Image")));
			this.applicationButtonApplicationMenu.ImageFixedSize = new System.Drawing.Size(16, 16);
			this.applicationButtonApplicationMenu.ImagePaddingHorizontal = 0;
			this.applicationButtonApplicationMenu.ImagePaddingVertical = 1;
			this.applicationButtonApplicationMenu.Name = "applicationButtonApplicationMenu";
			this.applicationButtonApplicationMenu.ShowSubItems = false;
			this.applicationButtonApplicationMenu.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItemApplicationMenuSlideSettings,
            this.buttonItemApplicationMenuHelp,
            this.buttonItemApplicationMenuExit});
			this.applicationButtonApplicationMenu.Text = "File";
			// 
			// buttonItemApplicationMenuSlideSettings
			// 
			this.buttonItemApplicationMenuSlideSettings.BeginGroup = true;
			this.buttonItemApplicationMenuSlideSettings.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.buttonItemApplicationMenuSlideSettings.Image = global::Asa.SlideTemplateViewer.Properties.Resources.ApplicationMenuSettings;
			this.buttonItemApplicationMenuSlideSettings.Name = "buttonItemApplicationMenuSlideSettings";
			this.buttonItemApplicationMenuSlideSettings.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.F2);
			this.buttonItemApplicationMenuSlideSettings.SubItemsExpandWidth = 24;
			this.buttonItemApplicationMenuSlideSettings.Text = "Slide Settings...";
			this.buttonItemApplicationMenuSlideSettings.Click += new System.EventHandler(this.OnSlideSettingsClick);
			// 
			// buttonItemApplicationMenuHelp
			// 
			this.buttonItemApplicationMenuHelp.BeginGroup = true;
			this.buttonItemApplicationMenuHelp.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.buttonItemApplicationMenuHelp.Image = global::Asa.SlideTemplateViewer.Properties.Resources.ApplicationMenuHelp;
			this.buttonItemApplicationMenuHelp.Name = "buttonItemApplicationMenuHelp";
			this.buttonItemApplicationMenuHelp.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.F1);
			this.buttonItemApplicationMenuHelp.SubItemsExpandWidth = 24;
			this.buttonItemApplicationMenuHelp.Text = "Help";
			this.buttonItemApplicationMenuHelp.Click += new System.EventHandler(this.OnHelpClick);
			// 
			// buttonItemApplicationMenuExit
			// 
			this.buttonItemApplicationMenuExit.BeginGroup = true;
			this.buttonItemApplicationMenuExit.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.buttonItemApplicationMenuExit.Image = global::Asa.SlideTemplateViewer.Properties.Resources.ApplicationMenuExit;
			this.buttonItemApplicationMenuExit.Name = "buttonItemApplicationMenuExit";
			this.buttonItemApplicationMenuExit.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.AltF4);
			this.buttonItemApplicationMenuExit.Text = "Exit";
			this.buttonItemApplicationMenuExit.Click += new System.EventHandler(this.OnExitClick);
			// 
			// ribbonTabItemSlides
			// 
			this.ribbonTabItemSlides.Checked = true;
			this.ribbonTabItemSlides.Name = "ribbonTabItemSlides";
			this.ribbonTabItemSlides.Panel = this.ribbonPanelSlides;
			this.ribbonTabItemSlides.Text = "Slides";
			// 
			// buttonItemCollapse
			// 
			this.buttonItemCollapse.Image = global::Asa.SlideTemplateViewer.Properties.Resources.RibbonCollapse;
			this.buttonItemCollapse.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far;
			this.buttonItemCollapse.Name = "buttonItemCollapse";
			this.superTooltip.SetSuperTooltip(this.buttonItemCollapse, new DevComponents.DotNetBar.SuperTooltipInfo("", "", "Collpase", null, null, DevComponents.DotNetBar.eTooltipColor.Gray, false, false, new System.Drawing.Size(60, 18)));
			this.buttonItemCollapse.Text = "buttonItem2";
			this.buttonItemCollapse.Click += new System.EventHandler(this.OnRibbonCollapseClick);
			// 
			// buttonItemExpand
			// 
			this.buttonItemExpand.Image = global::Asa.SlideTemplateViewer.Properties.Resources.RibbonExpand;
			this.buttonItemExpand.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far;
			this.buttonItemExpand.Name = "buttonItemExpand";
			this.superTooltip.SetSuperTooltip(this.buttonItemExpand, new DevComponents.DotNetBar.SuperTooltipInfo("", "", "Expand", null, null, DevComponents.DotNetBar.eTooltipColor.Gray, false, true, new System.Drawing.Size(60, 18)));
			this.buttonItemExpand.Text = "buttonItem1";
			this.buttonItemExpand.Visible = false;
			this.buttonItemExpand.Click += new System.EventHandler(this.OnRibbonExpandClick);
			// 
			// buttonItemPin
			// 
			this.buttonItemPin.Image = global::Asa.SlideTemplateViewer.Properties.Resources.RibbonPin;
			this.buttonItemPin.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far;
			this.buttonItemPin.Name = "buttonItemPin";
			this.superTooltip.SetSuperTooltip(this.buttonItemPin, new DevComponents.DotNetBar.SuperTooltipInfo("", "", "Lock", null, null, DevComponents.DotNetBar.eTooltipColor.Gray, false, true, new System.Drawing.Size(40, 18)));
			this.buttonItemPin.Text = "buttonItem3";
			this.buttonItemPin.Visible = false;
			this.buttonItemPin.Click += new System.EventHandler(this.OnRibbonPinClick);
			// 
			// buttonItemQatFloater
			// 
			this.buttonItemQatFloater.Image = global::Asa.SlideTemplateViewer.Properties.Resources.QatFloater;
			this.buttonItemQatFloater.Name = "buttonItemQatFloater";
			this.superTooltip.SetSuperTooltip(this.buttonItemQatFloater, new DevComponents.DotNetBar.SuperTooltipInfo("Floater", "", "Send to Floater", null, null, DevComponents.DotNetBar.eTooltipColor.Gray, true, false, new System.Drawing.Size(0, 0)));
			this.buttonItemQatFloater.Text = "buttonItem1";
			this.buttonItemQatFloater.Click += new System.EventHandler(this.OnFloaterClick);
			// 
			// buttonItemQatHelp
			// 
			this.buttonItemQatHelp.Image = global::Asa.SlideTemplateViewer.Properties.Resources.QatHelp;
			this.buttonItemQatHelp.Name = "buttonItemQatHelp";
			this.superTooltip.SetSuperTooltip(this.buttonItemQatHelp, new DevComponents.DotNetBar.SuperTooltipInfo("HELP", "", "Learn MORE about building schedules for PowerPoint", null, null, DevComponents.DotNetBar.eTooltipColor.Gray, true, false, new System.Drawing.Size(0, 0)));
			this.buttonItemQatHelp.Text = "buttonItem1";
			this.buttonItemQatHelp.Click += new System.EventHandler(this.OnHelpClick);
			// 
			// superTooltip
			// 
			this.superTooltip.DefaultTooltipSettings = new DevComponents.DotNetBar.SuperTooltipInfo("", "", "", null, null, DevComponents.DotNetBar.eTooltipColor.Gray);
			this.superTooltip.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
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
            this.itemContainerStatusBarMainInfo,
            this.labelItemStatusBarSeparator,
            this.itemContainerStatusBarAdditionalInfo});
			this.barBottom.Location = new System.Drawing.Point(5, 672);
			this.barBottom.Name = "barBottom";
			this.barBottom.PaddingBottom = 0;
			this.barBottom.PaddingTop = 0;
			this.barBottom.Size = new System.Drawing.Size(915, 26);
			this.barBottom.Stretch = true;
			this.barBottom.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.barBottom.TabIndex = 6;
			this.barBottom.TabStop = false;
			// 
			// itemContainerStatusBarMainInfo
			// 
			// 
			// 
			// 
			this.itemContainerStatusBarMainInfo.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.itemContainerStatusBarMainInfo.ItemSpacing = 20;
			this.itemContainerStatusBarMainInfo.MinimumSize = new System.Drawing.Size(0, 24);
			this.itemContainerStatusBarMainInfo.Name = "itemContainerStatusBarMainInfo";
			this.itemContainerStatusBarMainInfo.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.labelItemAppTitle});
			// 
			// 
			// 
			this.itemContainerStatusBarMainInfo.TitleMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.itemContainerStatusBarMainInfo.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.itemContainerStatusBarMainInfo.VerticalItemAlignment = DevComponents.DotNetBar.eVerticalItemsAlignment.Middle;
			// 
			// labelItemAppTitle
			// 
			this.labelItemAppTitle.Name = "labelItemAppTitle";
			this.labelItemAppTitle.Text = "Slide Gallery";
			// 
			// labelItemStatusBarSeparator
			// 
			this.labelItemStatusBarSeparator.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far;
			this.labelItemStatusBarSeparator.Name = "labelItemStatusBarSeparator";
			// 
			// itemContainerStatusBarAdditionalInfo
			// 
			// 
			// 
			// 
			this.itemContainerStatusBarAdditionalInfo.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.itemContainerStatusBarAdditionalInfo.ItemSpacing = 5;
			this.itemContainerStatusBarAdditionalInfo.MinimumSize = new System.Drawing.Size(0, 24);
			this.itemContainerStatusBarAdditionalInfo.Name = "itemContainerStatusBarAdditionalInfo";
			this.itemContainerStatusBarAdditionalInfo.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.labelItemSlideSize});
			// 
			// 
			// 
			this.itemContainerStatusBarAdditionalInfo.TitleMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.itemContainerStatusBarAdditionalInfo.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.itemContainerStatusBarAdditionalInfo.VerticalItemAlignment = DevComponents.DotNetBar.eVerticalItemsAlignment.Middle;
			// 
			// labelItemSlideSize
			// 
			this.labelItemSlideSize.Name = "labelItemSlideSize";
			// 
			// FormMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(925, 700);
			this.Controls.Add(this.barBottom);
			this.Controls.Add(this.ribbonControl);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(925, 700);
			this.Name = "FormMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Add Slides";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMain_FormClosed);
			this.Shown += new System.EventHandler(this.FormMain_Shown);
			this.Resize += new System.EventHandler(this.FormMain_Resize);
			this.ribbonControl.ResumeLayout(false);
			this.ribbonControl.PerformLayout();
			this.ribbonPanelSlides.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.barBottom)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion
        public DevComponents.DotNetBar.SuperTooltip superTooltip;
		public DevComponents.DotNetBar.RibbonControl ribbonControl;
		private DevComponents.DotNetBar.RibbonPanel ribbonPanelSlides;
		private DevComponents.DotNetBar.RibbonBar ribbonBarSlidesLogo;
		public DevComponents.DotNetBar.RibbonTabItem ribbonTabItemSlides;
		private DevComponents.DotNetBar.StyleManager styleManager;
		private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
		private DevComponents.DotNetBar.LabelItem labelItemSlidesLogo;
		public DevComponents.DotNetBar.RibbonBar ribbonBarSlidesPowerPoint;
		public DevComponents.DotNetBar.ButtonItem buttonItemSlidesPowerPoint;
		private DevComponents.DotNetBar.RibbonBar ribbonBarSlidesPreview;
		private DevComponents.DotNetBar.ButtonItem buttonItemSlidesPreview;
		private DevComponents.DotNetBar.ButtonItem buttonItemQatFloater;
		private DevComponents.DotNetBar.ButtonItem buttonItemQatHelp;
		private DevComponents.DotNetBar.ApplicationButton applicationButtonApplicationMenu;
		private DevComponents.DotNetBar.ButtonItem buttonItemApplicationMenuSlideSettings;
		private DevComponents.DotNetBar.ButtonItem buttonItemApplicationMenuHelp;
		private DevComponents.DotNetBar.ButtonItem buttonItemApplicationMenuExit;
		private DevComponents.DotNetBar.ButtonItem buttonItemCollapse;
		private DevComponents.DotNetBar.ButtonItem buttonItemExpand;
		private DevComponents.DotNetBar.ButtonItem buttonItemPin;
		private DevComponents.DotNetBar.Bar barBottom;
		private DevComponents.DotNetBar.ItemContainer itemContainerStatusBarMainInfo;
		private DevComponents.DotNetBar.LabelItem labelItemAppTitle;
		private DevComponents.DotNetBar.LabelItem labelItemStatusBarSeparator;
		private DevComponents.DotNetBar.ItemContainer itemContainerStatusBarAdditionalInfo;
		private DevComponents.DotNetBar.LabelItem labelItemSlideSize;
	}
}

