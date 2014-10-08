namespace NewBizWiz.QuickShare.Single
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
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
			this.ribbonControl = new DevComponents.DotNetBar.RibbonControl();
			this.ribbonPanelHome = new DevComponents.DotNetBar.RibbonPanel();
			this.ribbonBarHomeExit = new DevComponents.DotNetBar.RibbonBar();
			this.buttonItemHomeExit = new DevComponents.DotNetBar.ButtonItem();
			this.ribbonBarHomeOptions = new DevComponents.DotNetBar.RibbonBar();
			this.itemContainerHomeOptions1 = new DevComponents.DotNetBar.ItemContainer();
			this.buttonItemHomeSave = new DevComponents.DotNetBar.ButtonItem();
			this.buttonItemHomeHelp = new DevComponents.DotNetBar.ButtonItem();
			this.itemContainerHomeOptions2 = new DevComponents.DotNetBar.ItemContainer();
			this.buttonItemHomeSaveAs = new DevComponents.DotNetBar.ButtonItem();
			this.buttonItemHomeFloater = new DevComponents.DotNetBar.ButtonItem();
			this.ribbonBarHomeSchedule = new DevComponents.DotNetBar.RibbonBar();
			this.itemContainerHomeSchedule = new DevComponents.DotNetBar.ItemContainer();
			this.buttonItemHomeScheduleAdd = new DevComponents.DotNetBar.ButtonItem();
			this.buttonItemHomeScheduleClone = new DevComponents.DotNetBar.ButtonItem();
			this.ribbonBarHomeBasicInfo = new DevComponents.DotNetBar.RibbonBar();
			this.comboBoxEditBusinessName = new NewBizWiz.Core.Common.TabbedCombobox();
			this.styleController = new DevExpress.XtraEditors.StyleController();
			this.comboBoxEditDecisionMaker = new NewBizWiz.Core.Common.TabbedCombobox();
			this.comboBoxEditClientType = new NewBizWiz.Core.Common.TabbedCombobox();
			this.dateEditPresentationDate = new NewBizWiz.Core.Common.TabbedDateEdit();
			this.textEditAccountNumber = new DevExpress.XtraEditors.TextEdit();
			this.itemContainerHomeAdvertiser = new DevComponents.DotNetBar.ItemContainer();
			this.labelItemHomeAdvertiserTitle = new DevComponents.DotNetBar.LabelItem();
			this.controlContainerItemBusinessName = new DevComponents.DotNetBar.ControlContainerItem();
			this.controlContainerItemDecisionMaker = new DevComponents.DotNetBar.ControlContainerItem();
			this.controlContainerItemClientType = new DevComponents.DotNetBar.ControlContainerItem();
			this.itemContainerHomeSalesStrategy = new DevComponents.DotNetBar.ItemContainer();
			this.itemContainerHomePresentationDate = new DevComponents.DotNetBar.ItemContainer();
			this.labelItemHomePresentationDate = new DevComponents.DotNetBar.LabelItem();
			this.controlContainerItemPresentationDate = new DevComponents.DotNetBar.ControlContainerItem();
			this.itemContainerHomeAccountNumber = new DevComponents.DotNetBar.ItemContainer();
			this.checkBoxItemHomeAccountNumber = new DevComponents.DotNetBar.CheckBoxItem();
			this.controlContainerItemAccountNumber = new DevComponents.DotNetBar.ControlContainerItem();
			this.ribbonBarHomePackage = new DevComponents.DotNetBar.RibbonBar();
			this.itemContainerHomePackage = new DevComponents.DotNetBar.ItemContainer();
			this.buttonItemHomeNewPackage = new DevComponents.DotNetBar.ButtonItem();
			this.buttonItemHomeOpenPackage = new DevComponents.DotNetBar.ButtonItem();
			this.ribbonTabItemHome = new DevComponents.DotNetBar.RibbonTabItem();
			this.imageList = new System.Windows.Forms.ImageList();
			this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel();
			this.superTooltip = new DevComponents.DotNetBar.SuperTooltip();
			this.pnMain = new System.Windows.Forms.Panel();
			this.pnEmpty = new System.Windows.Forms.Panel();
			this.styleManager = new DevComponents.DotNetBar.StyleManager();
			this.ribbonControl.SuspendLayout();
			this.ribbonPanelHome.SuspendLayout();
			this.ribbonBarHomeBasicInfo.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditBusinessName.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditDecisionMaker.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditClientType.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dateEditPresentationDate.Properties.CalendarTimeProperties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dateEditPresentationDate.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditAccountNumber.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// ribbonControl
			// 
			this.ribbonControl.AutoExpand = false;
			this.ribbonControl.BackColor = System.Drawing.Color.White;
			// 
			// 
			// 
			this.ribbonControl.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonControl.CanCustomize = false;
			this.ribbonControl.CaptionVisible = true;
			this.ribbonControl.Controls.Add(this.ribbonPanelHome);
			this.ribbonControl.Dock = System.Windows.Forms.DockStyle.Top;
			this.ribbonControl.EnableQatPlacement = false;
			this.ribbonControl.ForeColor = System.Drawing.Color.Black;
			this.ribbonControl.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.ribbonTabItemHome});
			this.ribbonControl.KeyTipsFont = new System.Drawing.Font("Tahoma", 7F);
			this.ribbonControl.Location = new System.Drawing.Point(5, 1);
			this.ribbonControl.MdiSystemItemVisible = false;
			this.ribbonControl.Name = "ribbonControl";
			this.ribbonControl.Size = new System.Drawing.Size(990, 190);
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
			this.ribbonControl.TabIndex = 0;
			// 
			// ribbonPanelHome
			// 
			this.ribbonPanelHome.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonPanelHome.Controls.Add(this.ribbonBarHomeExit);
			this.ribbonPanelHome.Controls.Add(this.ribbonBarHomeOptions);
			this.ribbonPanelHome.Controls.Add(this.ribbonBarHomeSchedule);
			this.ribbonPanelHome.Controls.Add(this.ribbonBarHomeBasicInfo);
			this.ribbonPanelHome.Controls.Add(this.ribbonBarHomePackage);
			this.ribbonPanelHome.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ribbonPanelHome.Location = new System.Drawing.Point(0, 53);
			this.ribbonPanelHome.Name = "ribbonPanelHome";
			this.ribbonPanelHome.Padding = new System.Windows.Forms.Padding(3, 0, 3, 2);
			this.ribbonPanelHome.Size = new System.Drawing.Size(990, 137);
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
			this.ribbonPanelHome.TabIndex = 6;
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
			this.ribbonBarHomeExit.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItemHomeExit});
			this.ribbonBarHomeExit.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			this.ribbonBarHomeExit.Location = new System.Drawing.Point(586, 0);
			this.ribbonBarHomeExit.Name = "ribbonBarHomeExit";
			this.ribbonBarHomeExit.Size = new System.Drawing.Size(89, 135);
			this.ribbonBarHomeExit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonBarHomeExit.TabIndex = 27;
			this.ribbonBarHomeExit.Text = "EXIT";
			// 
			// 
			// 
			this.ribbonBarHomeExit.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarHomeExit.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// buttonItemHomeExit
			// 
			this.buttonItemHomeExit.Image = ((System.Drawing.Image)(resources.GetObject("buttonItemHomeExit.Image")));
			this.buttonItemHomeExit.Name = "buttonItemHomeExit";
			this.buttonItemHomeExit.SubItemsExpandWidth = 14;
			this.superTooltip.SetSuperTooltip(this.buttonItemHomeExit, new DevComponents.DotNetBar.SuperTooltipInfo("EXIT", "", "Close", null, null, DevComponents.DotNetBar.eTooltipColor.Gray));
			this.buttonItemHomeExit.Text = "buttonItem1";
			this.buttonItemHomeExit.Click += new System.EventHandler(this.buttonItemHomeExit_Click);
			// 
			// ribbonBarHomeOptions
			// 
			this.ribbonBarHomeOptions.AutoOverflowEnabled = true;
			// 
			// 
			// 
			this.ribbonBarHomeOptions.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarHomeOptions.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarHomeOptions.ContainerControlProcessDialogKey = true;
			this.ribbonBarHomeOptions.Dock = System.Windows.Forms.DockStyle.Left;
			this.ribbonBarHomeOptions.DragDropSupport = true;
			this.ribbonBarHomeOptions.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.itemContainerHomeOptions1,
            this.itemContainerHomeOptions2});
			this.ribbonBarHomeOptions.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			this.ribbonBarHomeOptions.Location = new System.Drawing.Point(445, 0);
			this.ribbonBarHomeOptions.Name = "ribbonBarHomeOptions";
			this.ribbonBarHomeOptions.Size = new System.Drawing.Size(141, 135);
			this.ribbonBarHomeOptions.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonBarHomeOptions.TabIndex = 62;
			this.ribbonBarHomeOptions.Text = "Options";
			// 
			// 
			// 
			this.ribbonBarHomeOptions.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarHomeOptions.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// itemContainerHomeOptions1
			// 
			// 
			// 
			// 
			this.itemContainerHomeOptions1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.itemContainerHomeOptions1.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
			this.itemContainerHomeOptions1.Name = "itemContainerHomeOptions1";
			this.itemContainerHomeOptions1.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItemHomeSave,
            this.buttonItemHomeHelp});
			// 
			// 
			// 
			this.itemContainerHomeOptions1.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// buttonItemHomeSave
			// 
			this.buttonItemHomeSave.Image = ((System.Drawing.Image)(resources.GetObject("buttonItemHomeSave.Image")));
			this.buttonItemHomeSave.Name = "buttonItemHomeSave";
			this.buttonItemHomeSave.SubItemsExpandWidth = 14;
			this.superTooltip.SetSuperTooltip(this.buttonItemHomeSave, new DevComponents.DotNetBar.SuperTooltipInfo("Save", "", "Save this schedule", null, null, DevComponents.DotNetBar.eTooltipColor.Gray));
			this.buttonItemHomeSave.Text = "Save";
			// 
			// buttonItemHomeHelp
			// 
			this.buttonItemHomeHelp.Image = ((System.Drawing.Image)(resources.GetObject("buttonItemHomeHelp.Image")));
			this.buttonItemHomeHelp.Name = "buttonItemHomeHelp";
			this.buttonItemHomeHelp.SubItemsExpandWidth = 14;
			this.superTooltip.SetSuperTooltip(this.buttonItemHomeHelp, new DevComponents.DotNetBar.SuperTooltipInfo("HELP", "", "Learn MORE about building schedules for PowerPoint", null, null, DevComponents.DotNetBar.eTooltipColor.Gray));
			// 
			// itemContainerHomeOptions2
			// 
			// 
			// 
			// 
			this.itemContainerHomeOptions2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.itemContainerHomeOptions2.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
			this.itemContainerHomeOptions2.Name = "itemContainerHomeOptions2";
			this.itemContainerHomeOptions2.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItemHomeSaveAs,
            this.buttonItemHomeFloater});
			// 
			// 
			// 
			this.itemContainerHomeOptions2.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// buttonItemHomeSaveAs
			// 
			this.buttonItemHomeSaveAs.Image = ((System.Drawing.Image)(resources.GetObject("buttonItemHomeSaveAs.Image")));
			this.buttonItemHomeSaveAs.Name = "buttonItemHomeSaveAs";
			this.buttonItemHomeSaveAs.SubItemsExpandWidth = 14;
			this.superTooltip.SetSuperTooltip(this.buttonItemHomeSaveAs, new DevComponents.DotNetBar.SuperTooltipInfo("Save As", "", "Save a copy of this schedule", null, null, DevComponents.DotNetBar.eTooltipColor.Gray));
			this.buttonItemHomeSaveAs.Text = "Save As";
			// 
			// buttonItemHomeFloater
			// 
			this.buttonItemHomeFloater.Image = ((System.Drawing.Image)(resources.GetObject("buttonItemHomeFloater.Image")));
			this.buttonItemHomeFloater.Name = "buttonItemHomeFloater";
			this.buttonItemHomeFloater.SubItemsExpandWidth = 14;
			this.superTooltip.SetSuperTooltip(this.buttonItemHomeFloater, new DevComponents.DotNetBar.SuperTooltipInfo("Floater", "", "Send to Floater", null, null, DevComponents.DotNetBar.eTooltipColor.Gray));
			this.buttonItemHomeFloater.Text = "Floater";
			this.buttonItemHomeFloater.Click += new System.EventHandler(this.buttonItemFloater_Click);
			// 
			// ribbonBarHomeSchedule
			// 
			this.ribbonBarHomeSchedule.AutoOverflowEnabled = true;
			// 
			// 
			// 
			this.ribbonBarHomeSchedule.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarHomeSchedule.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarHomeSchedule.ContainerControlProcessDialogKey = true;
			this.ribbonBarHomeSchedule.Dock = System.Windows.Forms.DockStyle.Left;
			this.ribbonBarHomeSchedule.DragDropSupport = true;
			this.ribbonBarHomeSchedule.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.itemContainerHomeSchedule});
			this.ribbonBarHomeSchedule.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			this.ribbonBarHomeSchedule.Location = new System.Drawing.Point(361, 0);
			this.ribbonBarHomeSchedule.Name = "ribbonBarHomeSchedule";
			this.ribbonBarHomeSchedule.Size = new System.Drawing.Size(84, 135);
			this.ribbonBarHomeSchedule.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonBarHomeSchedule.TabIndex = 61;
			this.ribbonBarHomeSchedule.Text = "Schedule";
			// 
			// 
			// 
			this.ribbonBarHomeSchedule.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarHomeSchedule.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// itemContainerHomeSchedule
			// 
			// 
			// 
			// 
			this.itemContainerHomeSchedule.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.itemContainerHomeSchedule.ItemSpacing = 20;
			this.itemContainerHomeSchedule.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
			this.itemContainerHomeSchedule.Name = "itemContainerHomeSchedule";
			this.itemContainerHomeSchedule.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItemHomeScheduleAdd,
            this.buttonItemHomeScheduleClone});
			// 
			// 
			// 
			this.itemContainerHomeSchedule.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// buttonItemHomeScheduleAdd
			// 
			this.buttonItemHomeScheduleAdd.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.buttonItemHomeScheduleAdd.ForeColor = System.Drawing.Color.Black;
			this.buttonItemHomeScheduleAdd.Image = ((System.Drawing.Image)(resources.GetObject("buttonItemHomeScheduleAdd.Image")));
			this.buttonItemHomeScheduleAdd.Name = "buttonItemHomeScheduleAdd";
			this.buttonItemHomeScheduleAdd.Text = "Add";
			// 
			// buttonItemHomeScheduleClone
			// 
			this.buttonItemHomeScheduleClone.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.buttonItemHomeScheduleClone.ForeColor = System.Drawing.Color.Black;
			this.buttonItemHomeScheduleClone.Image = ((System.Drawing.Image)(resources.GetObject("buttonItemHomeScheduleClone.Image")));
			this.buttonItemHomeScheduleClone.Name = "buttonItemHomeScheduleClone";
			this.buttonItemHomeScheduleClone.Text = "Clone";
			// 
			// ribbonBarHomeBasicInfo
			// 
			this.ribbonBarHomeBasicInfo.AutoOverflowEnabled = true;
			// 
			// 
			// 
			this.ribbonBarHomeBasicInfo.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarHomeBasicInfo.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarHomeBasicInfo.ContainerControlProcessDialogKey = true;
			this.ribbonBarHomeBasicInfo.Controls.Add(this.comboBoxEditBusinessName);
			this.ribbonBarHomeBasicInfo.Controls.Add(this.comboBoxEditDecisionMaker);
			this.ribbonBarHomeBasicInfo.Controls.Add(this.comboBoxEditClientType);
			this.ribbonBarHomeBasicInfo.Controls.Add(this.dateEditPresentationDate);
			this.ribbonBarHomeBasicInfo.Controls.Add(this.textEditAccountNumber);
			this.ribbonBarHomeBasicInfo.Dock = System.Windows.Forms.DockStyle.Left;
			this.ribbonBarHomeBasicInfo.DragDropSupport = true;
			this.ribbonBarHomeBasicInfo.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.itemContainerHomeAdvertiser,
            this.itemContainerHomeSalesStrategy});
			this.ribbonBarHomeBasicInfo.ItemSpacing = 7;
			this.ribbonBarHomeBasicInfo.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			this.ribbonBarHomeBasicInfo.Location = new System.Drawing.Point(96, 0);
			this.ribbonBarHomeBasicInfo.Name = "ribbonBarHomeBasicInfo";
			this.ribbonBarHomeBasicInfo.Size = new System.Drawing.Size(265, 135);
			this.ribbonBarHomeBasicInfo.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonBarHomeBasicInfo.TabIndex = 24;
			this.ribbonBarHomeBasicInfo.Text = "Basic Info";
			// 
			// 
			// 
			this.ribbonBarHomeBasicInfo.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarHomeBasicInfo.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// comboBoxEditBusinessName
			// 
			this.comboBoxEditBusinessName.Location = new System.Drawing.Point(4, 22);
			this.comboBoxEditBusinessName.Name = "comboBoxEditBusinessName";
			this.comboBoxEditBusinessName.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.comboBoxEditBusinessName.Properties.Appearance.Options.UseFont = true;
			this.comboBoxEditBusinessName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.comboBoxEditBusinessName.Properties.NullText = "Advertiser";
			this.comboBoxEditBusinessName.Size = new System.Drawing.Size(135, 20);
			this.comboBoxEditBusinessName.StyleController = this.styleController;
			this.comboBoxEditBusinessName.TabIndex = 0;
			// 
			// comboBoxEditDecisionMaker
			// 
			this.comboBoxEditDecisionMaker.Location = new System.Drawing.Point(4, 50);
			this.comboBoxEditDecisionMaker.Name = "comboBoxEditDecisionMaker";
			this.comboBoxEditDecisionMaker.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.comboBoxEditDecisionMaker.Properties.Appearance.Options.UseFont = true;
			this.comboBoxEditDecisionMaker.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.comboBoxEditDecisionMaker.Properties.NullText = "Contact";
			this.comboBoxEditDecisionMaker.Size = new System.Drawing.Size(135, 20);
			this.comboBoxEditDecisionMaker.StyleController = this.styleController;
			this.comboBoxEditDecisionMaker.TabIndex = 1;
			// 
			// comboBoxEditClientType
			// 
			this.comboBoxEditClientType.Location = new System.Drawing.Point(4, 78);
			this.comboBoxEditClientType.Name = "comboBoxEditClientType";
			this.comboBoxEditClientType.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.comboBoxEditClientType.Properties.Appearance.Options.UseFont = true;
			this.comboBoxEditClientType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.comboBoxEditClientType.Properties.NullText = "Select";
			this.comboBoxEditClientType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
			this.comboBoxEditClientType.Size = new System.Drawing.Size(135, 20);
			this.comboBoxEditClientType.StyleController = this.styleController;
			this.comboBoxEditClientType.TabIndex = 1;
			// 
			// dateEditPresentationDate
			// 
			this.dateEditPresentationDate.EditValue = null;
			this.dateEditPresentationDate.Location = new System.Drawing.Point(150, 22);
			this.dateEditPresentationDate.Name = "dateEditPresentationDate";
			this.dateEditPresentationDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
			this.dateEditPresentationDate.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.dateEditPresentationDate.Properties.Appearance.Options.UseFont = true;
			this.dateEditPresentationDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, ((System.Drawing.Image)(resources.GetObject("dateEditPresentationDate.Properties.Buttons"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true)});
			this.dateEditPresentationDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
			this.dateEditPresentationDate.Properties.DisplayFormat.FormatString = "MM/dd/yy";
			this.dateEditPresentationDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.dateEditPresentationDate.Properties.EditFormat.FormatString = "MM/dd/yy";
			this.dateEditPresentationDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.dateEditPresentationDate.Properties.NullDate = "";
			this.dateEditPresentationDate.Properties.NullText = "Select";
			this.dateEditPresentationDate.Properties.ShowPopupShadow = false;
			this.dateEditPresentationDate.Properties.ShowToday = false;
			this.dateEditPresentationDate.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
			this.dateEditPresentationDate.Size = new System.Drawing.Size(103, 20);
			this.dateEditPresentationDate.StyleController = this.styleController;
			this.dateEditPresentationDate.TabIndex = 0;
			// 
			// textEditAccountNumber
			// 
			this.textEditAccountNumber.Enabled = false;
			this.textEditAccountNumber.Location = new System.Drawing.Point(150, 78);
			this.textEditAccountNumber.Name = "textEditAccountNumber";
			this.textEditAccountNumber.Size = new System.Drawing.Size(103, 20);
			this.textEditAccountNumber.StyleController = this.styleController;
			this.textEditAccountNumber.TabIndex = 0;
			// 
			// itemContainerHomeAdvertiser
			// 
			// 
			// 
			// 
			this.itemContainerHomeAdvertiser.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.itemContainerHomeAdvertiser.ItemSpacing = 6;
			this.itemContainerHomeAdvertiser.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
			this.itemContainerHomeAdvertiser.Name = "itemContainerHomeAdvertiser";
			this.itemContainerHomeAdvertiser.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.labelItemHomeAdvertiserTitle,
            this.controlContainerItemBusinessName,
            this.controlContainerItemDecisionMaker,
            this.controlContainerItemClientType});
			// 
			// 
			// 
			this.itemContainerHomeAdvertiser.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// labelItemHomeAdvertiserTitle
			// 
			this.labelItemHomeAdvertiserTitle.ForeColor = System.Drawing.Color.Black;
			this.labelItemHomeAdvertiserTitle.Name = "labelItemHomeAdvertiserTitle";
			this.labelItemHomeAdvertiserTitle.SingleLineColor = System.Drawing.Color.Empty;
			this.labelItemHomeAdvertiserTitle.Text = " Advertiser:";
			// 
			// controlContainerItemBusinessName
			// 
			this.controlContainerItemBusinessName.AllowItemResize = false;
			this.controlContainerItemBusinessName.Control = this.comboBoxEditBusinessName;
			this.controlContainerItemBusinessName.MenuVisibility = DevComponents.DotNetBar.eMenuVisibility.VisibleAlways;
			this.controlContainerItemBusinessName.Name = "controlContainerItemBusinessName";
			// 
			// controlContainerItemDecisionMaker
			// 
			this.controlContainerItemDecisionMaker.AllowItemResize = false;
			this.controlContainerItemDecisionMaker.Control = this.comboBoxEditDecisionMaker;
			this.controlContainerItemDecisionMaker.MenuVisibility = DevComponents.DotNetBar.eMenuVisibility.VisibleAlways;
			this.controlContainerItemDecisionMaker.Name = "controlContainerItemDecisionMaker";
			// 
			// controlContainerItemClientType
			// 
			this.controlContainerItemClientType.AllowItemResize = false;
			this.controlContainerItemClientType.Control = this.comboBoxEditClientType;
			this.controlContainerItemClientType.MenuVisibility = DevComponents.DotNetBar.eMenuVisibility.VisibleAlways;
			this.controlContainerItemClientType.Name = "controlContainerItemClientType";
			// 
			// itemContainerHomeSalesStrategy
			// 
			// 
			// 
			// 
			this.itemContainerHomeSalesStrategy.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.itemContainerHomeSalesStrategy.ItemSpacing = 12;
			this.itemContainerHomeSalesStrategy.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
			this.itemContainerHomeSalesStrategy.Name = "itemContainerHomeSalesStrategy";
			this.itemContainerHomeSalesStrategy.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.itemContainerHomePresentationDate,
            this.itemContainerHomeAccountNumber});
			// 
			// 
			// 
			this.itemContainerHomeSalesStrategy.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// itemContainerHomePresentationDate
			// 
			// 
			// 
			// 
			this.itemContainerHomePresentationDate.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.itemContainerHomePresentationDate.ItemSpacing = 6;
			this.itemContainerHomePresentationDate.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
			this.itemContainerHomePresentationDate.Name = "itemContainerHomePresentationDate";
			this.itemContainerHomePresentationDate.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.labelItemHomePresentationDate,
            this.controlContainerItemPresentationDate});
			// 
			// 
			// 
			this.itemContainerHomePresentationDate.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// labelItemHomePresentationDate
			// 
			this.labelItemHomePresentationDate.ForeColor = System.Drawing.Color.Black;
			this.labelItemHomePresentationDate.Name = "labelItemHomePresentationDate";
			this.labelItemHomePresentationDate.Text = " Presentation Date:";
			// 
			// controlContainerItemPresentationDate
			// 
			this.controlContainerItemPresentationDate.AllowItemResize = false;
			this.controlContainerItemPresentationDate.Control = this.dateEditPresentationDate;
			this.controlContainerItemPresentationDate.MenuVisibility = DevComponents.DotNetBar.eMenuVisibility.VisibleAlways;
			this.controlContainerItemPresentationDate.Name = "controlContainerItemPresentationDate";
			// 
			// itemContainerHomeAccountNumber
			// 
			// 
			// 
			// 
			this.itemContainerHomeAccountNumber.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.itemContainerHomeAccountNumber.ItemSpacing = 3;
			this.itemContainerHomeAccountNumber.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
			this.itemContainerHomeAccountNumber.Name = "itemContainerHomeAccountNumber";
			this.itemContainerHomeAccountNumber.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.checkBoxItemHomeAccountNumber,
            this.controlContainerItemAccountNumber});
			// 
			// 
			// 
			this.itemContainerHomeAccountNumber.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// checkBoxItemHomeAccountNumber
			// 
			this.checkBoxItemHomeAccountNumber.Name = "checkBoxItemHomeAccountNumber";
			this.checkBoxItemHomeAccountNumber.Text = "Acct#: ";
			this.checkBoxItemHomeAccountNumber.TextColor = System.Drawing.Color.Black;
			// 
			// controlContainerItemAccountNumber
			// 
			this.controlContainerItemAccountNumber.AllowItemResize = false;
			this.controlContainerItemAccountNumber.Control = this.textEditAccountNumber;
			this.controlContainerItemAccountNumber.MenuVisibility = DevComponents.DotNetBar.eMenuVisibility.VisibleAlways;
			this.controlContainerItemAccountNumber.Name = "controlContainerItemAccountNumber";
			// 
			// ribbonBarHomePackage
			// 
			this.ribbonBarHomePackage.AutoOverflowEnabled = true;
			// 
			// 
			// 
			this.ribbonBarHomePackage.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarHomePackage.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarHomePackage.ContainerControlProcessDialogKey = true;
			this.ribbonBarHomePackage.Dock = System.Windows.Forms.DockStyle.Left;
			this.ribbonBarHomePackage.DragDropSupport = true;
			this.ribbonBarHomePackage.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.itemContainerHomePackage});
			this.ribbonBarHomePackage.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			this.ribbonBarHomePackage.Location = new System.Drawing.Point(3, 0);
			this.ribbonBarHomePackage.Name = "ribbonBarHomePackage";
			this.ribbonBarHomePackage.Size = new System.Drawing.Size(93, 135);
			this.ribbonBarHomePackage.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonBarHomePackage.TabIndex = 60;
			this.ribbonBarHomePackage.Text = "Package";
			// 
			// 
			// 
			this.ribbonBarHomePackage.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarHomePackage.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// itemContainerHomePackage
			// 
			// 
			// 
			// 
			this.itemContainerHomePackage.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.itemContainerHomePackage.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
			this.itemContainerHomePackage.Name = "itemContainerHomePackage";
			this.itemContainerHomePackage.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItemHomeNewPackage,
            this.buttonItemHomeOpenPackage});
			// 
			// 
			// 
			this.itemContainerHomePackage.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// buttonItemHomeNewPackage
			// 
			this.buttonItemHomeNewPackage.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.buttonItemHomeNewPackage.ForeColor = System.Drawing.Color.Black;
			this.buttonItemHomeNewPackage.Image = ((System.Drawing.Image)(resources.GetObject("buttonItemHomeNewPackage.Image")));
			this.buttonItemHomeNewPackage.Name = "buttonItemHomeNewPackage";
			this.buttonItemHomeNewPackage.SubItemsExpandWidth = 14;
			this.buttonItemHomeNewPackage.Text = "New";
			this.buttonItemHomeNewPackage.Click += new System.EventHandler(this.buttonItemHomeNewPackage_Click);
			// 
			// buttonItemHomeOpenPackage
			// 
			this.buttonItemHomeOpenPackage.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.buttonItemHomeOpenPackage.ForeColor = System.Drawing.Color.Black;
			this.buttonItemHomeOpenPackage.Image = ((System.Drawing.Image)(resources.GetObject("buttonItemHomeOpenPackage.Image")));
			this.buttonItemHomeOpenPackage.Name = "buttonItemHomeOpenPackage";
			this.buttonItemHomeOpenPackage.SubItemsExpandWidth = 14;
			this.buttonItemHomeOpenPackage.Text = "Open";
			this.buttonItemHomeOpenPackage.Click += new System.EventHandler(this.buttonItemHomeOpenPackage_Click);
			// 
			// ribbonTabItemHome
			// 
			this.ribbonTabItemHome.Checked = true;
			this.ribbonTabItemHome.Name = "ribbonTabItemHome";
			this.ribbonTabItemHome.Panel = this.ribbonPanelHome;
			this.ribbonTabItemHome.Text = "Home";
			// 
			// imageList
			// 
			this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
			this.imageList.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList.Images.SetKeyName(0, "MonthListIcon.png");
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
			this.pnMain.Location = new System.Drawing.Point(5, 191);
			this.pnMain.Name = "pnMain";
			this.pnMain.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
			this.pnMain.Size = new System.Drawing.Size(990, 557);
			this.pnMain.TabIndex = 1;
			// 
			// pnEmpty
			// 
			this.pnEmpty.BackColor = System.Drawing.Color.Transparent;
			this.pnEmpty.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnEmpty.Location = new System.Drawing.Point(5, 191);
			this.pnEmpty.Name = "pnEmpty";
			this.pnEmpty.Size = new System.Drawing.Size(990, 557);
			this.pnEmpty.TabIndex = 2;
			// 
			// styleManager
			// 
			this.styleManager.ManagerStyle = DevComponents.DotNetBar.eStyle.Metro;
			this.styleManager.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.White, System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(87)))), ((int)(((byte)(154))))));
			// 
			// FormMain
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(1000, 750);
			this.Controls.Add(this.pnEmpty);
			this.Controls.Add(this.pnMain);
			this.Controls.Add(this.ribbonControl);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(1000, 750);
			this.Name = "FormMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "QuickSHARE";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
			this.Shown += new System.EventHandler(this.FormMain_Shown);
			this.Resize += new System.EventHandler(this.FormMain_Resize);
			this.ribbonControl.ResumeLayout(false);
			this.ribbonControl.PerformLayout();
			this.ribbonPanelHome.ResumeLayout(false);
			this.ribbonBarHomeBasicInfo.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditBusinessName.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditDecisionMaker.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditClientType.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dateEditPresentationDate.Properties.CalendarTimeProperties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dateEditPresentationDate.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditAccountNumber.Properties)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

		private DevComponents.DotNetBar.RibbonPanel ribbonPanelHome;
        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
        public DevComponents.DotNetBar.RibbonControl ribbonControl;
		public DevComponents.DotNetBar.RibbonTabItem ribbonTabItemHome;
		public DevComponents.DotNetBar.SuperTooltip superTooltip;
		private DevExpress.XtraEditors.StyleController styleController;
		private DevComponents.DotNetBar.RibbonBar ribbonBarHomeExit;
		private DevComponents.DotNetBar.ButtonItem buttonItemHomeExit;
		private DevComponents.DotNetBar.RibbonBar ribbonBarHomeBasicInfo;
		public DevExpress.XtraEditors.TextEdit textEditAccountNumber;
		private DevComponents.DotNetBar.ItemContainer itemContainerHomeAdvertiser;
		private DevComponents.DotNetBar.LabelItem labelItemHomeAdvertiserTitle;
		private DevComponents.DotNetBar.ControlContainerItem controlContainerItemBusinessName;
		private DevComponents.DotNetBar.ControlContainerItem controlContainerItemDecisionMaker;
		private DevComponents.DotNetBar.ControlContainerItem controlContainerItemClientType;
		private DevComponents.DotNetBar.ItemContainer itemContainerHomeSalesStrategy;
		private DevComponents.DotNetBar.ItemContainer itemContainerHomePresentationDate;
		private DevComponents.DotNetBar.LabelItem labelItemHomePresentationDate;
		private DevComponents.DotNetBar.ControlContainerItem controlContainerItemPresentationDate;
		private DevComponents.DotNetBar.ItemContainer itemContainerHomeAccountNumber;
		public DevComponents.DotNetBar.CheckBoxItem checkBoxItemHomeAccountNumber;
		private DevComponents.DotNetBar.ControlContainerItem controlContainerItemAccountNumber;
		private System.Windows.Forms.Panel pnMain;
		private DevComponents.DotNetBar.RibbonBar ribbonBarHomePackage;
		private DevComponents.DotNetBar.ItemContainer itemContainerHomePackage;
		private DevComponents.DotNetBar.ButtonItem buttonItemHomeNewPackage;
		private DevComponents.DotNetBar.ButtonItem buttonItemHomeOpenPackage;
		private DevComponents.DotNetBar.RibbonBar ribbonBarHomeSchedule;
		private DevComponents.DotNetBar.ItemContainer itemContainerHomeSchedule;
		private DevComponents.DotNetBar.ButtonItem buttonItemHomeScheduleAdd;
		public DevComponents.DotNetBar.ButtonItem buttonItemHomeScheduleClone;
		private System.Windows.Forms.ImageList imageList;
		public Core.Common.TabbedCombobox comboBoxEditBusinessName;
		public Core.Common.TabbedCombobox comboBoxEditDecisionMaker;
		public Core.Common.TabbedCombobox comboBoxEditClientType;
		public Core.Common.TabbedDateEdit dateEditPresentationDate;
		private DevComponents.DotNetBar.RibbonBar ribbonBarHomeOptions;
		private DevComponents.DotNetBar.ItemContainer itemContainerHomeOptions1;
		private DevComponents.DotNetBar.ButtonItem buttonItemHomeSave;
		private DevComponents.DotNetBar.ButtonItem buttonItemHomeHelp;
		private DevComponents.DotNetBar.ItemContainer itemContainerHomeOptions2;
		private DevComponents.DotNetBar.ButtonItem buttonItemHomeSaveAs;
		private DevComponents.DotNetBar.ButtonItem buttonItemHomeFloater;
		private System.Windows.Forms.Panel pnEmpty;
		private DevComponents.DotNetBar.StyleManager styleManager;
    }
}

