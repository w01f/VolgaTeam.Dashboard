
namespace Asa.Dashboard
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
            this.IsDead = true;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.ribbonControl = new DevComponents.DotNetBar.RibbonControl();
			this.ribbonPanelSlides = new DevComponents.DotNetBar.RibbonPanel();
			this.ribbonBarSlidesExit = new DevComponents.DotNetBar.RibbonBar();
			this.buttonItemSlidesExit = new DevComponents.DotNetBar.ButtonItem();
			this.ribbonBarSlidesFloater = new DevComponents.DotNetBar.RibbonBar();
			this.buttonItemSlidesFloater = new DevComponents.DotNetBar.ButtonItem();
			this.ribbonBarSlidesHelp = new DevComponents.DotNetBar.RibbonBar();
			this.buttonItemSlidesHelp = new DevComponents.DotNetBar.ButtonItem();
			this.ribbonBarSlidesPowerPoint = new DevComponents.DotNetBar.RibbonBar();
			this.buttonItemSlidesPowerPoint = new DevComponents.DotNetBar.ButtonItem();
			this.ribbonBarSlidesPreview = new DevComponents.DotNetBar.RibbonBar();
			this.buttonItemSlidesPreview = new DevComponents.DotNetBar.ButtonItem();
			this.ribbonBarSlidesLogo = new DevComponents.DotNetBar.RibbonBar();
			this.buttonItemSlidesLogo = new DevComponents.DotNetBar.ButtonItem();
			this.ribbonPanelHome = new DevComponents.DotNetBar.RibbonPanel();
			this.ribbonBarHomeExit = new DevComponents.DotNetBar.RibbonBar();
			this.buttonItemHomeExit = new DevComponents.DotNetBar.ButtonItem();
			this.ribbonBarHomeFloater = new DevComponents.DotNetBar.RibbonBar();
			this.buttonItemHomeFloater = new DevComponents.DotNetBar.ButtonItem();
			this.ribbonBarHomeHelp = new DevComponents.DotNetBar.RibbonBar();
			this.buttonItemHomeHelp = new DevComponents.DotNetBar.ButtonItem();
			this.ribbonBarHomeLoad = new DevComponents.DotNetBar.RibbonBar();
			this.buttonItemHomeLoad = new DevComponents.DotNetBar.ButtonItem();
			this.ribbonBarPowerPoint = new DevComponents.DotNetBar.RibbonBar();
			this.buttonItemPowerPoint = new DevComponents.DotNetBar.ButtonItem();
			this.buttonItemHomeThemeCleanslate = new DevComponents.DotNetBar.ButtonItem();
			this.buttonItemHomeThemeCover = new DevComponents.DotNetBar.ButtonItem();
			this.buttonItemHomeThemeLeadoff = new DevComponents.DotNetBar.ButtonItem();
			this.buttonItemHomeThemeClientGoals = new DevComponents.DotNetBar.ButtonItem();
			this.buttonItemHomeThemeTargetCustomers = new DevComponents.DotNetBar.ButtonItem();
			this.buttonItemHomeThemeSimpleSummary = new DevComponents.DotNetBar.ButtonItem();
			this.ribbonBarPreview = new DevComponents.DotNetBar.RibbonBar();
			this.buttonItemPreview = new DevComponents.DotNetBar.ButtonItem();
			this.ribbonBarHomeOverview = new DevComponents.DotNetBar.RibbonBar();
			this.buttonItemHomeOverview = new DevComponents.DotNetBar.ButtonItem();
			this.ribbonTabItemHome = new DevComponents.DotNetBar.RibbonTabItem();
			this.ribbonTabItemSlides = new DevComponents.DotNetBar.RibbonTabItem();
			this.buttonItemSlideSettings = new DevComponents.DotNetBar.ButtonItem();
			this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
			this.superTooltip = new DevComponents.DotNetBar.SuperTooltip();
			this.pnMain = new System.Windows.Forms.Panel();
			this.styleManager = new DevComponents.DotNetBar.StyleManager(this.components);
			this.ribbonControl.SuspendLayout();
			this.ribbonPanelSlides.SuspendLayout();
			this.ribbonPanelHome.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolTip
			// 
			this.toolTip.IsBalloon = true;
			this.toolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
			// 
			// ribbonControl
			// 
			this.ribbonControl.AutoExpand = false;
			this.ribbonControl.BackColor = System.Drawing.Color.White;
			// 
			// 
			// 
			this.ribbonControl.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonControl.CaptionVisible = true;
			this.ribbonControl.Controls.Add(this.ribbonPanelSlides);
			this.ribbonControl.Controls.Add(this.ribbonPanelHome);
			this.ribbonControl.Dock = System.Windows.Forms.DockStyle.Top;
			this.ribbonControl.ForeColor = System.Drawing.Color.Black;
			this.ribbonControl.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.ribbonTabItemHome,
            this.ribbonTabItemSlides,
            this.buttonItemSlideSettings});
			this.ribbonControl.KeyTipsFont = new System.Drawing.Font("Tahoma", 7F);
			this.ribbonControl.Location = new System.Drawing.Point(5, 1);
			this.ribbonControl.MdiSystemItemVisible = false;
			this.ribbonControl.Name = "ribbonControl";
			this.ribbonControl.Size = new System.Drawing.Size(915, 185);
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
			// 
			// ribbonPanelSlides
			// 
			this.ribbonPanelSlides.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonPanelSlides.Controls.Add(this.ribbonBarSlidesExit);
			this.ribbonPanelSlides.Controls.Add(this.ribbonBarSlidesFloater);
			this.ribbonPanelSlides.Controls.Add(this.ribbonBarSlidesHelp);
			this.ribbonPanelSlides.Controls.Add(this.ribbonBarSlidesPowerPoint);
			this.ribbonPanelSlides.Controls.Add(this.ribbonBarSlidesPreview);
			this.ribbonPanelSlides.Controls.Add(this.ribbonBarSlidesLogo);
			this.ribbonPanelSlides.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ribbonPanelSlides.Location = new System.Drawing.Point(0, 53);
			this.ribbonPanelSlides.Name = "ribbonPanelSlides";
			this.ribbonPanelSlides.Padding = new System.Windows.Forms.Padding(3, 0, 3, 2);
			this.ribbonPanelSlides.Size = new System.Drawing.Size(915, 132);
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
			// ribbonBarSlidesExit
			// 
			this.ribbonBarSlidesExit.AutoOverflowEnabled = true;
			// 
			// 
			// 
			this.ribbonBarSlidesExit.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarSlidesExit.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarSlidesExit.ContainerControlProcessDialogKey = true;
			this.ribbonBarSlidesExit.Dock = System.Windows.Forms.DockStyle.Left;
			this.ribbonBarSlidesExit.DragDropSupport = true;
			this.ribbonBarSlidesExit.HorizontalItemAlignment = DevComponents.DotNetBar.eHorizontalItemsAlignment.Center;
			this.ribbonBarSlidesExit.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItemSlidesExit});
			this.ribbonBarSlidesExit.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			this.ribbonBarSlidesExit.Location = new System.Drawing.Point(558, 0);
			this.ribbonBarSlidesExit.Name = "ribbonBarSlidesExit";
			this.ribbonBarSlidesExit.Size = new System.Drawing.Size(85, 130);
			this.ribbonBarSlidesExit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonBarSlidesExit.TabIndex = 24;
			this.ribbonBarSlidesExit.Text = "EXIT";
			// 
			// 
			// 
			this.ribbonBarSlidesExit.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarSlidesExit.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarSlidesExit.VerticalItemAlignment = DevComponents.DotNetBar.eVerticalItemsAlignment.Middle;
			// 
			// buttonItemSlidesExit
			// 
			this.buttonItemSlidesExit.Image = global::Asa.Dashboard.Properties.Resources.Exit;
			this.buttonItemSlidesExit.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
			this.buttonItemSlidesExit.Name = "buttonItemSlidesExit";
			this.buttonItemSlidesExit.SubItemsExpandWidth = 14;
			this.superTooltip.SetSuperTooltip(this.buttonItemSlidesExit, new DevComponents.DotNetBar.SuperTooltipInfo("Exit", "", "Close this app…", null, null, DevComponents.DotNetBar.eTooltipColor.Gray, true, false, new System.Drawing.Size(0, 0)));
			this.buttonItemSlidesExit.Click += new System.EventHandler(this.buttonItemExit_Click);
			// 
			// ribbonBarSlidesFloater
			// 
			this.ribbonBarSlidesFloater.AutoOverflowEnabled = true;
			// 
			// 
			// 
			this.ribbonBarSlidesFloater.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarSlidesFloater.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarSlidesFloater.ContainerControlProcessDialogKey = true;
			this.ribbonBarSlidesFloater.Dock = System.Windows.Forms.DockStyle.Left;
			this.ribbonBarSlidesFloater.DragDropSupport = true;
			this.ribbonBarSlidesFloater.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItemSlidesFloater});
			this.ribbonBarSlidesFloater.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			this.ribbonBarSlidesFloater.Location = new System.Drawing.Point(470, 0);
			this.ribbonBarSlidesFloater.Name = "ribbonBarSlidesFloater";
			this.ribbonBarSlidesFloater.Size = new System.Drawing.Size(88, 130);
			this.ribbonBarSlidesFloater.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonBarSlidesFloater.TabIndex = 27;
			this.ribbonBarSlidesFloater.Text = "Floater";
			// 
			// 
			// 
			this.ribbonBarSlidesFloater.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarSlidesFloater.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// buttonItemSlidesFloater
			// 
			this.buttonItemSlidesFloater.Image = global::Asa.Dashboard.Properties.Resources.Floater;
			this.buttonItemSlidesFloater.Name = "buttonItemSlidesFloater";
			this.buttonItemSlidesFloater.SubItemsExpandWidth = 14;
			this.superTooltip.SetSuperTooltip(this.buttonItemSlidesFloater, new DevComponents.DotNetBar.SuperTooltipInfo("Floater", "", "Minimize this app to the floater bar…", null, null, DevComponents.DotNetBar.eTooltipColor.Gray));
			this.buttonItemSlidesFloater.Text = "Floater";
			this.buttonItemSlidesFloater.Click += new System.EventHandler(this.buttonItemFloater_Click);
			// 
			// ribbonBarSlidesHelp
			// 
			this.ribbonBarSlidesHelp.AutoOverflowEnabled = true;
			// 
			// 
			// 
			this.ribbonBarSlidesHelp.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarSlidesHelp.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarSlidesHelp.ContainerControlProcessDialogKey = true;
			this.ribbonBarSlidesHelp.Dock = System.Windows.Forms.DockStyle.Left;
			this.ribbonBarSlidesHelp.DragDropSupport = true;
			this.ribbonBarSlidesHelp.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItemSlidesHelp});
			this.ribbonBarSlidesHelp.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			this.ribbonBarSlidesHelp.Location = new System.Drawing.Point(386, 0);
			this.ribbonBarSlidesHelp.Name = "ribbonBarSlidesHelp";
			this.ribbonBarSlidesHelp.Size = new System.Drawing.Size(84, 130);
			this.ribbonBarSlidesHelp.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonBarSlidesHelp.TabIndex = 26;
			this.ribbonBarSlidesHelp.Text = "HELP";
			// 
			// 
			// 
			this.ribbonBarSlidesHelp.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarSlidesHelp.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// buttonItemSlidesHelp
			// 
			this.buttonItemSlidesHelp.Image = global::Asa.Dashboard.Properties.Resources.Help;
			this.buttonItemSlidesHelp.Name = "buttonItemSlidesHelp";
			this.buttonItemSlidesHelp.SubItemsExpandWidth = 14;
			this.superTooltip.SetSuperTooltip(this.buttonItemSlidesHelp, new DevComponents.DotNetBar.SuperTooltipInfo("Help", "", "Learn more about this app…", null, null, DevComponents.DotNetBar.eTooltipColor.Gray));
			this.buttonItemSlidesHelp.Text = "buttonItem1";
			this.buttonItemSlidesHelp.Click += new System.EventHandler(this.buttonItemHelp_Click);
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
			this.ribbonBarSlidesPowerPoint.Size = new System.Drawing.Size(88, 130);
			this.ribbonBarSlidesPowerPoint.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonBarSlidesPowerPoint.TabIndex = 25;
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
			this.buttonItemSlidesPowerPoint.Image = global::Asa.Dashboard.Properties.Resources.Output;
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
			this.ribbonBarSlidesPreview.Size = new System.Drawing.Size(94, 130);
			this.ribbonBarSlidesPreview.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonBarSlidesPreview.TabIndex = 28;
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
			this.buttonItemSlidesPreview.Image = global::Asa.Dashboard.Properties.Resources.Preview;
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
            this.buttonItemSlidesLogo});
			this.ribbonBarSlidesLogo.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			this.ribbonBarSlidesLogo.Location = new System.Drawing.Point(3, 0);
			this.ribbonBarSlidesLogo.Name = "ribbonBarSlidesLogo";
			this.ribbonBarSlidesLogo.Size = new System.Drawing.Size(201, 130);
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
			// buttonItemSlidesLogo
			// 
			this.buttonItemSlidesLogo.Image = global::Asa.Dashboard.Properties.Resources.MasterWizardLogo;
			this.buttonItemSlidesLogo.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
			this.buttonItemSlidesLogo.Name = "buttonItemSlidesLogo";
			this.buttonItemSlidesLogo.SubItemsExpandWidth = 14;
			this.buttonItemSlidesLogo.Click += new System.EventHandler(this.labelItemLogo_Click);
			// 
			// ribbonPanelHome
			// 
			this.ribbonPanelHome.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonPanelHome.Controls.Add(this.ribbonBarHomeExit);
			this.ribbonPanelHome.Controls.Add(this.ribbonBarHomeFloater);
			this.ribbonPanelHome.Controls.Add(this.ribbonBarHomeHelp);
			this.ribbonPanelHome.Controls.Add(this.ribbonBarHomeLoad);
			this.ribbonPanelHome.Controls.Add(this.ribbonBarPowerPoint);
			this.ribbonPanelHome.Controls.Add(this.ribbonBarPreview);
			this.ribbonPanelHome.Controls.Add(this.ribbonBarHomeOverview);
			this.ribbonPanelHome.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ribbonPanelHome.Location = new System.Drawing.Point(0, 53);
			this.ribbonPanelHome.Name = "ribbonPanelHome";
			this.ribbonPanelHome.Padding = new System.Windows.Forms.Padding(3, 0, 3, 2);
			this.ribbonPanelHome.Size = new System.Drawing.Size(915, 132);
			// 
			// 
			// 
			this.ribbonPanelHome.Style.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonPanelHome.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonPanelHome.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonPanelHome.TabIndex = 4;
			this.ribbonPanelHome.Visible = false;
			// 
			// ribbonBarHomeExit
			// 
			this.ribbonBarHomeExit.AutoOverflowEnabled = true;
			// 
			// 
			// 
			this.ribbonBarHomeExit.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarHomeExit.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarHomeExit.ContainerControlProcessDialogKey = true;
			this.ribbonBarHomeExit.Dock = System.Windows.Forms.DockStyle.Left;
			this.ribbonBarHomeExit.DragDropSupport = true;
			this.ribbonBarHomeExit.HorizontalItemAlignment = DevComponents.DotNetBar.eHorizontalItemsAlignment.Center;
			this.ribbonBarHomeExit.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItemHomeExit});
			this.ribbonBarHomeExit.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			this.ribbonBarHomeExit.Location = new System.Drawing.Point(682, 0);
			this.ribbonBarHomeExit.Name = "ribbonBarHomeExit";
			this.ribbonBarHomeExit.Size = new System.Drawing.Size(94, 130);
			this.ribbonBarHomeExit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonBarHomeExit.TabIndex = 12;
			this.ribbonBarHomeExit.Text = "EXIT";
			// 
			// 
			// 
			this.ribbonBarHomeExit.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarHomeExit.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarHomeExit.VerticalItemAlignment = DevComponents.DotNetBar.eVerticalItemsAlignment.Middle;
			// 
			// buttonItemHomeExit
			// 
			this.buttonItemHomeExit.Image = global::Asa.Dashboard.Properties.Resources.Exit;
			this.buttonItemHomeExit.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
			this.buttonItemHomeExit.Name = "buttonItemHomeExit";
			this.buttonItemHomeExit.SubItemsExpandWidth = 14;
			this.superTooltip.SetSuperTooltip(this.buttonItemHomeExit, new DevComponents.DotNetBar.SuperTooltipInfo("Exit", "", "Close this app…", null, null, DevComponents.DotNetBar.eTooltipColor.Gray, true, false, new System.Drawing.Size(0, 0)));
			this.buttonItemHomeExit.Click += new System.EventHandler(this.buttonItemExit_Click);
			// 
			// ribbonBarHomeFloater
			// 
			this.ribbonBarHomeFloater.AutoOverflowEnabled = true;
			// 
			// 
			// 
			this.ribbonBarHomeFloater.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarHomeFloater.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarHomeFloater.ContainerControlProcessDialogKey = true;
			this.ribbonBarHomeFloater.Dock = System.Windows.Forms.DockStyle.Left;
			this.ribbonBarHomeFloater.DragDropSupport = true;
			this.ribbonBarHomeFloater.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItemHomeFloater});
			this.ribbonBarHomeFloater.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			this.ribbonBarHomeFloater.Location = new System.Drawing.Point(594, 0);
			this.ribbonBarHomeFloater.Name = "ribbonBarHomeFloater";
			this.ribbonBarHomeFloater.Size = new System.Drawing.Size(88, 130);
			this.ribbonBarHomeFloater.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonBarHomeFloater.TabIndex = 22;
			this.ribbonBarHomeFloater.Text = "Floater";
			// 
			// 
			// 
			this.ribbonBarHomeFloater.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarHomeFloater.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// buttonItemHomeFloater
			// 
			this.buttonItemHomeFloater.Image = global::Asa.Dashboard.Properties.Resources.Floater;
			this.buttonItemHomeFloater.Name = "buttonItemHomeFloater";
			this.buttonItemHomeFloater.SubItemsExpandWidth = 14;
			this.superTooltip.SetSuperTooltip(this.buttonItemHomeFloater, new DevComponents.DotNetBar.SuperTooltipInfo("Floater", "", "Minimize this app to the floater bar…", null, null, DevComponents.DotNetBar.eTooltipColor.Gray));
			this.buttonItemHomeFloater.Text = "Floater";
			this.buttonItemHomeFloater.Click += new System.EventHandler(this.buttonItemFloater_Click);
			// 
			// ribbonBarHomeHelp
			// 
			this.ribbonBarHomeHelp.AutoOverflowEnabled = true;
			// 
			// 
			// 
			this.ribbonBarHomeHelp.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarHomeHelp.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarHomeHelp.ContainerControlProcessDialogKey = true;
			this.ribbonBarHomeHelp.Dock = System.Windows.Forms.DockStyle.Left;
			this.ribbonBarHomeHelp.DragDropSupport = true;
			this.ribbonBarHomeHelp.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItemHomeHelp});
			this.ribbonBarHomeHelp.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			this.ribbonBarHomeHelp.Location = new System.Drawing.Point(518, 0);
			this.ribbonBarHomeHelp.Name = "ribbonBarHomeHelp";
			this.ribbonBarHomeHelp.Size = new System.Drawing.Size(76, 130);
			this.ribbonBarHomeHelp.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonBarHomeHelp.TabIndex = 21;
			this.ribbonBarHomeHelp.Text = "HELP";
			// 
			// 
			// 
			this.ribbonBarHomeHelp.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarHomeHelp.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// buttonItemHomeHelp
			// 
			this.buttonItemHomeHelp.Image = global::Asa.Dashboard.Properties.Resources.Help;
			this.buttonItemHomeHelp.Name = "buttonItemHomeHelp";
			this.buttonItemHomeHelp.SubItemsExpandWidth = 14;
			this.superTooltip.SetSuperTooltip(this.buttonItemHomeHelp, new DevComponents.DotNetBar.SuperTooltipInfo("Help", "", "Learn more about this app…", null, null, DevComponents.DotNetBar.eTooltipColor.Gray));
			this.buttonItemHomeHelp.Text = "buttonItem1";
			this.buttonItemHomeHelp.Click += new System.EventHandler(this.buttonItemHelp_Click);
			// 
			// ribbonBarHomeLoad
			// 
			this.ribbonBarHomeLoad.AutoOverflowEnabled = true;
			// 
			// 
			// 
			this.ribbonBarHomeLoad.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarHomeLoad.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarHomeLoad.ContainerControlProcessDialogKey = true;
			this.ribbonBarHomeLoad.Dock = System.Windows.Forms.DockStyle.Left;
			this.ribbonBarHomeLoad.DragDropSupport = true;
			this.ribbonBarHomeLoad.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItemHomeLoad});
			this.ribbonBarHomeLoad.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			this.ribbonBarHomeLoad.Location = new System.Drawing.Point(424, 0);
			this.ribbonBarHomeLoad.Name = "ribbonBarHomeLoad";
			this.ribbonBarHomeLoad.Size = new System.Drawing.Size(94, 130);
			this.ribbonBarHomeLoad.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonBarHomeLoad.TabIndex = 24;
			this.ribbonBarHomeLoad.Text = "My Slides";
			// 
			// 
			// 
			this.ribbonBarHomeLoad.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarHomeLoad.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// buttonItemHomeLoad
			// 
			this.buttonItemHomeLoad.Image = global::Asa.Dashboard.Properties.Resources.Load;
			this.buttonItemHomeLoad.Name = "buttonItemHomeLoad";
			this.buttonItemHomeLoad.SubItemsExpandWidth = 14;
			this.buttonItemHomeLoad.Text = "buttonItem1";
			this.buttonItemHomeLoad.Click += new System.EventHandler(this.buttonItemHomeLoad_Click);
			// 
			// ribbonBarPowerPoint
			// 
			this.ribbonBarPowerPoint.AutoOverflowEnabled = true;
			// 
			// 
			// 
			this.ribbonBarPowerPoint.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarPowerPoint.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarPowerPoint.ContainerControlProcessDialogKey = true;
			this.ribbonBarPowerPoint.Dock = System.Windows.Forms.DockStyle.Left;
			this.ribbonBarPowerPoint.DragDropSupport = true;
			this.ribbonBarPowerPoint.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItemPowerPoint,
            this.buttonItemHomeThemeCleanslate,
            this.buttonItemHomeThemeCover,
            this.buttonItemHomeThemeLeadoff,
            this.buttonItemHomeThemeClientGoals,
            this.buttonItemHomeThemeTargetCustomers,
            this.buttonItemHomeThemeSimpleSummary});
			this.ribbonBarPowerPoint.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			this.ribbonBarPowerPoint.Location = new System.Drawing.Point(291, 0);
			this.ribbonBarPowerPoint.Name = "ribbonBarPowerPoint";
			this.ribbonBarPowerPoint.Size = new System.Drawing.Size(133, 130);
			this.ribbonBarPowerPoint.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonBarPowerPoint.TabIndex = 20;
			this.ribbonBarPowerPoint.Text = "PowerPoint";
			// 
			// 
			// 
			this.ribbonBarPowerPoint.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarPowerPoint.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// buttonItemPowerPoint
			// 
			this.buttonItemPowerPoint.Image = global::Asa.Dashboard.Properties.Resources.Output;
			this.buttonItemPowerPoint.Name = "buttonItemPowerPoint";
			this.buttonItemPowerPoint.SubItemsExpandWidth = 14;
			this.superTooltip.SetSuperTooltip(this.buttonItemPowerPoint, new DevComponents.DotNetBar.SuperTooltipInfo("Insert Slide", "", "Add this slide to my Presentation…", null, null, DevComponents.DotNetBar.eTooltipColor.Gray));
			this.buttonItemPowerPoint.Click += new System.EventHandler(this.buttonItemPowerPoint_Click);
			// 
			// buttonItemHomeThemeCleanslate
			// 
			this.buttonItemHomeThemeCleanslate.Name = "buttonItemHomeThemeCleanslate";
			this.buttonItemHomeThemeCleanslate.SubItemsExpandWidth = 14;
			this.buttonItemHomeThemeCleanslate.Text = "Theme:\r\nDefault";
			this.buttonItemHomeThemeCleanslate.Click += new System.EventHandler(this.buttonItemHomeTheme_Click);
			// 
			// buttonItemHomeThemeCover
			// 
			this.buttonItemHomeThemeCover.Name = "buttonItemHomeThemeCover";
			this.buttonItemHomeThemeCover.SubItemsExpandWidth = 14;
			this.buttonItemHomeThemeCover.Text = "Theme:\r\nDefault";
			this.buttonItemHomeThemeCover.Visible = false;
			// 
			// buttonItemHomeThemeLeadoff
			// 
			this.buttonItemHomeThemeLeadoff.Name = "buttonItemHomeThemeLeadoff";
			this.buttonItemHomeThemeLeadoff.SubItemsExpandWidth = 14;
			this.buttonItemHomeThemeLeadoff.Text = "Theme:\r\nDefault";
			this.buttonItemHomeThemeLeadoff.Visible = false;
			// 
			// buttonItemHomeThemeClientGoals
			// 
			this.buttonItemHomeThemeClientGoals.Name = "buttonItemHomeThemeClientGoals";
			this.buttonItemHomeThemeClientGoals.SubItemsExpandWidth = 14;
			this.buttonItemHomeThemeClientGoals.Text = "Theme:\r\nDefault";
			this.buttonItemHomeThemeClientGoals.Visible = false;
			// 
			// buttonItemHomeThemeTargetCustomers
			// 
			this.buttonItemHomeThemeTargetCustomers.Name = "buttonItemHomeThemeTargetCustomers";
			this.buttonItemHomeThemeTargetCustomers.SubItemsExpandWidth = 14;
			this.buttonItemHomeThemeTargetCustomers.Text = "Theme:\r\nDefault";
			this.buttonItemHomeThemeTargetCustomers.Visible = false;
			// 
			// buttonItemHomeThemeSimpleSummary
			// 
			this.buttonItemHomeThemeSimpleSummary.Name = "buttonItemHomeThemeSimpleSummary";
			this.buttonItemHomeThemeSimpleSummary.SubItemsExpandWidth = 14;
			this.buttonItemHomeThemeSimpleSummary.Text = "Theme:\r\nDefault";
			this.buttonItemHomeThemeSimpleSummary.Visible = false;
			// 
			// ribbonBarPreview
			// 
			this.ribbonBarPreview.AutoOverflowEnabled = true;
			// 
			// 
			// 
			this.ribbonBarPreview.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarPreview.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarPreview.ContainerControlProcessDialogKey = true;
			this.ribbonBarPreview.Dock = System.Windows.Forms.DockStyle.Left;
			this.ribbonBarPreview.DragDropSupport = true;
			this.ribbonBarPreview.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItemPreview});
			this.ribbonBarPreview.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			this.ribbonBarPreview.Location = new System.Drawing.Point(204, 0);
			this.ribbonBarPreview.Name = "ribbonBarPreview";
			this.ribbonBarPreview.Size = new System.Drawing.Size(87, 130);
			this.ribbonBarPreview.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonBarPreview.TabIndex = 23;
			this.ribbonBarPreview.Text = "Preview";
			// 
			// 
			// 
			this.ribbonBarPreview.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarPreview.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// buttonItemPreview
			// 
			this.buttonItemPreview.Image = global::Asa.Dashboard.Properties.Resources.Preview;
			this.buttonItemPreview.Name = "buttonItemPreview";
			this.buttonItemPreview.SubItemsExpandWidth = 14;
			this.superTooltip.SetSuperTooltip(this.buttonItemPreview, new DevComponents.DotNetBar.SuperTooltipInfo("Slide Preview", "", "View this slide before you add it to your presentation…", null, null, DevComponents.DotNetBar.eTooltipColor.Gray));
			this.buttonItemPreview.Text = "buttonItem1";
			this.buttonItemPreview.Click += new System.EventHandler(this.buttonItemPreview_Click);
			// 
			// ribbonBarHomeOverview
			// 
			this.ribbonBarHomeOverview.AutoOverflowEnabled = true;
			// 
			// 
			// 
			this.ribbonBarHomeOverview.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarHomeOverview.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarHomeOverview.ContainerControlProcessDialogKey = true;
			this.ribbonBarHomeOverview.Dock = System.Windows.Forms.DockStyle.Left;
			this.ribbonBarHomeOverview.DragDropSupport = true;
			this.ribbonBarHomeOverview.HorizontalItemAlignment = DevComponents.DotNetBar.eHorizontalItemsAlignment.Center;
			this.ribbonBarHomeOverview.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItemHomeOverview});
			this.ribbonBarHomeOverview.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			this.ribbonBarHomeOverview.Location = new System.Drawing.Point(3, 0);
			this.ribbonBarHomeOverview.Name = "ribbonBarHomeOverview";
			this.ribbonBarHomeOverview.Size = new System.Drawing.Size(201, 130);
			this.ribbonBarHomeOverview.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonBarHomeOverview.TabIndex = 5;
			this.ribbonBarHomeOverview.Text = "GO GET YOUR BIZ!";
			// 
			// 
			// 
			this.ribbonBarHomeOverview.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarHomeOverview.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarHomeOverview.VerticalItemAlignment = DevComponents.DotNetBar.eVerticalItemsAlignment.Middle;
			// 
			// buttonItemHomeOverview
			// 
			this.buttonItemHomeOverview.Checked = true;
			this.buttonItemHomeOverview.Image = global::Asa.Dashboard.Properties.Resources.MasterWizardLogo;
			this.buttonItemHomeOverview.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
			this.buttonItemHomeOverview.Name = "buttonItemHomeOverview";
			this.buttonItemHomeOverview.SubItemsExpandWidth = 14;
			this.buttonItemHomeOverview.Click += new System.EventHandler(this.buttonItemHomeOverview_Click);
			// 
			// ribbonTabItemHome
			// 
			this.ribbonTabItemHome.Name = "ribbonTabItemHome";
			this.ribbonTabItemHome.Panel = this.ribbonPanelHome;
			this.ribbonTabItemHome.Text = "HOME";
			this.ribbonTabItemHome.Click += new System.EventHandler(this.Outside_Click);
			// 
			// ribbonTabItemSlides
			// 
			this.ribbonTabItemSlides.Checked = true;
			this.ribbonTabItemSlides.Name = "ribbonTabItemSlides";
			this.ribbonTabItemSlides.Panel = this.ribbonPanelSlides;
			this.ribbonTabItemSlides.Text = "Slides";
			// 
			// buttonItemSlideSettings
			// 
			this.buttonItemSlideSettings.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far;
			this.buttonItemSlideSettings.Name = "buttonItemSlideSettings";
			this.buttonItemSlideSettings.Text = "Slide Settings";
			this.buttonItemSlideSettings.Click += new System.EventHandler(this.buttonItemSlideSettings_Click);
			// 
			// defaultLookAndFeel
			// 
			this.defaultLookAndFeel.LookAndFeel.SkinName = "Office 2013";
			// 
			// superTooltip
			// 
			this.superTooltip.DefaultTooltipSettings = new DevComponents.DotNetBar.SuperTooltipInfo("", "", "", null, null, DevComponents.DotNetBar.eTooltipColor.Gray);
			this.superTooltip.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			// 
			// pnMain
			// 
			this.pnMain.BackColor = System.Drawing.Color.Transparent;
			this.pnMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnMain.Location = new System.Drawing.Point(5, 186);
			this.pnMain.Name = "pnMain";
			this.pnMain.Size = new System.Drawing.Size(915, 511);
			this.pnMain.TabIndex = 6;
			// 
			// styleManager
			// 
			this.styleManager.ManagerStyle = DevComponents.DotNetBar.eStyle.Metro;
			this.styleManager.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.White, System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(87)))), ((int)(((byte)(154))))));
			// 
			// FormMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(925, 699);
			this.Controls.Add(this.pnMain);
			this.Controls.Add(this.ribbonControl);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimumSize = new System.Drawing.Size(925, 700);
			this.Name = "FormMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "6 Minute Seller";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMain_FormClosed);
			this.Shown += new System.EventHandler(this.FormMain_Shown);
			this.Click += new System.EventHandler(this.Outside_Click);
			this.Resize += new System.EventHandler(this.FormMain_Resize);
			this.ribbonControl.ResumeLayout(false);
			this.ribbonControl.PerformLayout();
			this.ribbonPanelSlides.ResumeLayout(false);
			this.ribbonPanelHome.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

		private System.Windows.Forms.ToolTip toolTip;
		private DevComponents.DotNetBar.RibbonTabItem ribbonTabItemHome;
		private DevComponents.DotNetBar.RibbonBar ribbonBarHomeOverview;
        private DevComponents.DotNetBar.RibbonBar ribbonBarHomeExit;
		private DevComponents.DotNetBar.ButtonItem buttonItemHomeExit;
		private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
		public DevComponents.DotNetBar.ButtonItem buttonItemHomeOverview;
        public DevComponents.DotNetBar.ButtonItem buttonItemPowerPoint;
		public DevComponents.DotNetBar.RibbonBar ribbonBarPowerPoint;
		private DevComponents.DotNetBar.RibbonBar ribbonBarHomeHelp;
        public DevComponents.DotNetBar.SuperTooltip superTooltip;
		public DevComponents.DotNetBar.ButtonItem buttonItemHomeHelp;
		public DevComponents.DotNetBar.RibbonControl ribbonControl;
        private DevComponents.DotNetBar.RibbonBar ribbonBarHomeFloater;
		private DevComponents.DotNetBar.ButtonItem buttonItemHomeFloater;
		public DevComponents.DotNetBar.ButtonItem buttonItemHomeThemeCleanslate;
		private DevComponents.DotNetBar.RibbonPanel ribbonPanelSlides;
		private DevComponents.DotNetBar.RibbonBar ribbonBarSlidesExit;
		private DevComponents.DotNetBar.ButtonItem buttonItemSlidesExit;
		private DevComponents.DotNetBar.RibbonBar ribbonBarSlidesFloater;
		private DevComponents.DotNetBar.ButtonItem buttonItemSlidesFloater;
		private DevComponents.DotNetBar.RibbonBar ribbonBarSlidesHelp;
		public DevComponents.DotNetBar.ButtonItem buttonItemSlidesHelp;
		public DevComponents.DotNetBar.RibbonBar ribbonBarSlidesPowerPoint;
		public DevComponents.DotNetBar.ButtonItem buttonItemSlidesPowerPoint;
		private DevComponents.DotNetBar.RibbonBar ribbonBarSlidesLogo;
		public DevComponents.DotNetBar.ButtonItem buttonItemSlidesLogo;
		public DevComponents.DotNetBar.RibbonTabItem ribbonTabItemSlides;
		private DevComponents.DotNetBar.RibbonBar ribbonBarPreview;
		private DevComponents.DotNetBar.RibbonBar ribbonBarSlidesPreview;
		private DevComponents.DotNetBar.ButtonItem buttonItemSlidesPreview;
		public DevComponents.DotNetBar.ButtonItem buttonItemPreview;
		private System.Windows.Forms.Panel pnMain;
		public DevComponents.DotNetBar.ButtonItem buttonItemHomeThemeCover;
		public DevComponents.DotNetBar.ButtonItem buttonItemHomeThemeLeadoff;
		public DevComponents.DotNetBar.ButtonItem buttonItemHomeThemeClientGoals;
		public DevComponents.DotNetBar.ButtonItem buttonItemHomeThemeTargetCustomers;
		public DevComponents.DotNetBar.ButtonItem buttonItemHomeThemeSimpleSummary;
		private DevComponents.DotNetBar.RibbonBar ribbonBarHomeLoad;
		public DevComponents.DotNetBar.ButtonItem buttonItemHomeLoad;
		private DevComponents.DotNetBar.StyleManager styleManager;
		private DevComponents.DotNetBar.ButtonItem buttonItemSlideSettings;
		public DevComponents.DotNetBar.RibbonPanel ribbonPanelHome;
    }
}

