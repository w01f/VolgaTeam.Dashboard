﻿namespace Asa.Bar.App.Forms
{
    partial class FormMain
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			DevComponents.DotNetBar.Rendering.SuperTabColorTable superTabColorTable1 = new DevComponents.DotNetBar.Rendering.SuperTabColorTable();
			DevComponents.DotNetBar.Rendering.SuperTabControlBoxStateColorTable superTabControlBoxStateColorTable1 = new DevComponents.DotNetBar.Rendering.SuperTabControlBoxStateColorTable();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
			DevComponents.DotNetBar.Rendering.SuperTabItemColorTable superTabItemColorTable1 = new DevComponents.DotNetBar.Rendering.SuperTabItemColorTable();
			DevComponents.DotNetBar.Rendering.SuperTabColorStates superTabColorStates1 = new DevComponents.DotNetBar.Rendering.SuperTabColorStates();
			DevComponents.DotNetBar.Rendering.SuperTabItemStateColorTable superTabItemStateColorTable1 = new DevComponents.DotNetBar.Rendering.SuperTabItemStateColorTable();
			this.superTabControlMain = new DevComponents.DotNetBar.SuperTabControl();
			this.superTabControlPanel2 = new DevComponents.DotNetBar.SuperTabControlPanel();
			this.ribbonBarSettings = new DevComponents.DotNetBar.RibbonBar();
			this.itemContainer6 = new DevComponents.DotNetBar.ItemContainer();
			this.labelItem1 = new DevComponents.DotNetBar.LabelItem();
			this.itemContainer3 = new DevComponents.DotNetBar.ItemContainer();
			this.itemContainer4 = new DevComponents.DotNetBar.ItemContainer();
			this.buttonItemColorSelector = new DevComponents.DotNetBar.ButtonItem();
			this.colorPickerDropDownAccent = new DevComponents.DotNetBar.ColorPickerDropDown();
			this.colorPickerDropDownText = new DevComponents.DotNetBar.ColorPickerDropDown();
			this.itemContainerMonitors = new DevComponents.DotNetBar.ItemContainer();
			this.buttonItemScreen1 = new DevComponents.DotNetBar.ButtonItem();
			this.buttonItemScreen2 = new DevComponents.DotNetBar.ButtonItem();
			this.buttonItemScreen3 = new DevComponents.DotNetBar.ButtonItem();
			this.buttonItemScreen4 = new DevComponents.DotNetBar.ButtonItem();
			this.buttonItemScreen5 = new DevComponents.DotNetBar.ButtonItem();
			this.buttonItemScreen6 = new DevComponents.DotNetBar.ButtonItem();
			this.itemContainer5 = new DevComponents.DotNetBar.ItemContainer();
			this.checkBoxItemLoadAtStartup = new DevComponents.DotNetBar.CheckBoxItem();
			this.checkBoxItemDocked = new DevComponents.DotNetBar.CheckBoxItem();
			this.itemContainer7 = new DevComponents.DotNetBar.ItemContainer();
			this.labelItem2 = new DevComponents.DotNetBar.LabelItem();
			this.ribbonBarBrowsers = new DevComponents.DotNetBar.RibbonBar();
			this.itemContainer1 = new DevComponents.DotNetBar.ItemContainer();
			this.buttonItemBrowserCh = new DevComponents.DotNetBar.ButtonItem();
			this.buttonItemBrowserEdge = new DevComponents.DotNetBar.ButtonItem();
			this.itemContainer2 = new DevComponents.DotNetBar.ItemContainer();
			this.buttonItemBrowserFF = new DevComponents.DotNetBar.ButtonItem();
			this.buttonItemBrowserIE = new DevComponents.DotNetBar.ButtonItem();
			this.superTabItemTemplates = new DevComponents.DotNetBar.SuperTabItem();
			this.superTabControlPanel1 = new DevComponents.DotNetBar.SuperTabControlPanel();
			this.styleManager = new DevComponents.DotNetBar.StyleManager(this.components);
			this.timerUpdateWindow = new System.Windows.Forms.Timer(this.components);
			this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripMenuItemCenterScreen = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItemDock = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItemExit = new System.Windows.Forms.ToolStripMenuItem();
			((System.ComponentModel.ISupportInitialize)(this.superTabControlMain)).BeginInit();
			this.superTabControlMain.SuspendLayout();
			this.superTabControlPanel2.SuspendLayout();
			this.contextMenuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// superTabControlMain
			// 
			this.superTabControlMain.BackColor = System.Drawing.Color.White;
			// 
			// 
			// 
			// 
			// 
			// 
			this.superTabControlMain.ControlBox.CloseBox.Name = "";
			this.superTabControlMain.ControlBox.CloseBox.Visible = true;
			this.superTabControlMain.ControlBox.CloseBox.Click += new System.EventHandler(this.OnTabControlCloseClick);
			// 
			// 
			// 
			this.superTabControlMain.ControlBox.MenuBox.Name = "";
			this.superTabControlMain.ControlBox.MenuBox.Visible = false;
			this.superTabControlMain.ControlBox.MenuBox.Click += new System.EventHandler(this.OnCollapseClick);
			this.superTabControlMain.ControlBox.Name = "";
			this.superTabControlMain.ControlBox.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.superTabControlMain.ControlBox.MenuBox,
            this.superTabControlMain.ControlBox.CloseBox});
			this.superTabControlMain.Controls.Add(this.superTabControlPanel2);
			this.superTabControlMain.Controls.Add(this.superTabControlPanel1);
			this.superTabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.superTabControlMain.ForeColor = System.Drawing.Color.Black;
			this.superTabControlMain.Location = new System.Drawing.Point(0, 0);
			this.superTabControlMain.Name = "superTabControlMain";
			this.superTabControlMain.ReorderTabsEnabled = false;
			this.superTabControlMain.SelectedTabFont = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.superTabControlMain.SelectedTabIndex = 0;
			this.superTabControlMain.Size = new System.Drawing.Size(600, 200);
			this.superTabControlMain.TabFont = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.superTabControlMain.TabIndex = 0;
			this.superTabControlMain.TabLayoutType = DevComponents.DotNetBar.eSuperTabLayoutType.SingleLineFit;
			this.superTabControlMain.Tabs.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.superTabItemTemplates});
			superTabControlBoxStateColorTable1.Image = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
			superTabColorTable1.ControlBoxDefault = superTabControlBoxStateColorTable1;
			this.superTabControlMain.TabStripColor = superTabColorTable1;
			this.superTabControlMain.TextAlignment = DevComponents.DotNetBar.eItemAlignment.Center;
			this.superTabControlMain.SelectedTabChanged += new System.EventHandler<DevComponents.DotNetBar.SuperTabStripSelectedTabChangedEventArgs>(this.OnTabControlPageChanged);
			this.superTabControlMain.TabStripMouseMove += new System.EventHandler<System.Windows.Forms.MouseEventArgs>(this.OnTabControlTabStripMouseMove);
			this.superTabControlMain.TabStripMouseDoubleClick += new System.EventHandler<System.Windows.Forms.MouseEventArgs>(this.OnTabControlDoubleClick);
			// 
			// superTabControlPanel2
			// 
			this.superTabControlPanel2.Controls.Add(this.ribbonBarSettings);
			this.superTabControlPanel2.Controls.Add(this.ribbonBarBrowsers);
			this.superTabControlPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.superTabControlPanel2.Location = new System.Drawing.Point(0, 30);
			this.superTabControlPanel2.Name = "superTabControlPanel2";
			this.superTabControlPanel2.Size = new System.Drawing.Size(600, 170);
			this.superTabControlPanel2.TabIndex = 2;
			this.superTabControlPanel2.TabItem = this.superTabItemTemplates;
			// 
			// ribbonBarSettings
			// 
			this.ribbonBarSettings.AutoOverflowEnabled = false;
			// 
			// 
			// 
			this.ribbonBarSettings.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarSettings.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarSettings.ContainerControlProcessDialogKey = true;
			this.ribbonBarSettings.DragDropSupport = true;
			this.ribbonBarSettings.HorizontalItemAlignment = DevComponents.DotNetBar.eHorizontalItemsAlignment.Center;
			this.ribbonBarSettings.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.itemContainer6,
            this.itemContainer3,
            this.itemContainer7});
			this.ribbonBarSettings.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			this.ribbonBarSettings.Location = new System.Drawing.Point(172, 13);
			this.ribbonBarSettings.Name = "ribbonBarSettings";
			this.ribbonBarSettings.Padding = new System.Windows.Forms.Padding(20, 0, 20, 0);
			this.ribbonBarSettings.Size = new System.Drawing.Size(193, 145);
			this.ribbonBarSettings.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonBarSettings.TabIndex = 2;
			this.ribbonBarSettings.Text = "Extras";
			// 
			// 
			// 
			this.ribbonBarSettings.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarSettings.TitleStyle.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
			this.ribbonBarSettings.TitleStyle.TextColor = System.Drawing.Color.Red;
			// 
			// 
			// 
			this.ribbonBarSettings.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarSettings.VerticalItemAlignment = DevComponents.DotNetBar.eVerticalItemsAlignment.Middle;
			// 
			// itemContainer6
			// 
			// 
			// 
			// 
			this.itemContainer6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.itemContainer6.FixedSize = new System.Drawing.Size(10, 0);
			this.itemContainer6.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
			this.itemContainer6.Name = "itemContainer6";
			this.itemContainer6.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.labelItem1});
			// 
			// 
			// 
			this.itemContainer6.TitleMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.itemContainer6.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// labelItem1
			// 
			this.labelItem1.Name = "labelItem1";
			this.labelItem1.Text = "  ";
			// 
			// itemContainer3
			// 
			// 
			// 
			// 
			this.itemContainer3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.itemContainer3.HorizontalItemAlignment = DevComponents.DotNetBar.eHorizontalItemsAlignment.Center;
			this.itemContainer3.ItemSpacing = 30;
			this.itemContainer3.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
			this.itemContainer3.Name = "itemContainer3";
			this.itemContainer3.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.itemContainer4,
            this.itemContainer5});
			// 
			// 
			// 
			this.itemContainer3.TitleMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.itemContainer3.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// itemContainer4
			// 
			// 
			// 
			// 
			this.itemContainer4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.itemContainer4.ItemSpacing = 20;
			this.itemContainer4.Name = "itemContainer4";
			this.itemContainer4.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItemColorSelector,
            this.itemContainerMonitors});
			// 
			// 
			// 
			this.itemContainer4.TitleMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.itemContainer4.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// buttonItemColorSelector
			// 
			this.buttonItemColorSelector.Name = "buttonItemColorSelector";
			this.buttonItemColorSelector.SplitButton = true;
			this.buttonItemColorSelector.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.colorPickerDropDownAccent,
            this.colorPickerDropDownText});
			this.buttonItemColorSelector.Text = "color";
			// 
			// colorPickerDropDownAccent
			// 
			this.colorPickerDropDownAccent.Name = "colorPickerDropDownAccent";
			this.colorPickerDropDownAccent.SplitButton = true;
			this.colorPickerDropDownAccent.Text = "Ribbon Color";
			this.colorPickerDropDownAccent.SelectedColorChanged += new System.EventHandler(this.OnSelectedAccentColorChanged);
			this.colorPickerDropDownAccent.ColorPreview += new DevComponents.DotNetBar.ColorPreviewEventHandler(this.OnAccentColorPreview);
			this.colorPickerDropDownAccent.PopupClose += new System.EventHandler(this.OnAccentColorPopupClose);
			// 
			// colorPickerDropDownText
			// 
			this.colorPickerDropDownText.BeginGroup = true;
			this.colorPickerDropDownText.Name = "colorPickerDropDownText";
			this.colorPickerDropDownText.SplitButton = true;
			this.colorPickerDropDownText.Text = "Text Color";
			this.colorPickerDropDownText.SelectedColorChanged += new System.EventHandler(this.OnSelectedTextColorChanged);
			this.colorPickerDropDownText.ColorPreview += new DevComponents.DotNetBar.ColorPreviewEventHandler(this.OnTextColorPreview);
			this.colorPickerDropDownText.PopupClose += new System.EventHandler(this.OnTextColorPopupClose);
			// 
			// itemContainerMonitors
			// 
			// 
			// 
			// 
			this.itemContainerMonitors.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.itemContainerMonitors.Name = "itemContainerMonitors";
			this.itemContainerMonitors.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItemScreen1,
            this.buttonItemScreen2,
            this.buttonItemScreen3,
            this.buttonItemScreen4,
            this.buttonItemScreen5,
            this.buttonItemScreen6});
			// 
			// 
			// 
			this.itemContainerMonitors.TitleMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.itemContainerMonitors.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// buttonItemScreen1
			// 
			this.buttonItemScreen1.Name = "buttonItemScreen1";
			this.buttonItemScreen1.Tag = "0";
			this.buttonItemScreen1.Text = "1";
			this.buttonItemScreen1.Click += new System.EventHandler(this.OnMonitorSelectorClick);
			// 
			// buttonItemScreen2
			// 
			this.buttonItemScreen2.Name = "buttonItemScreen2";
			this.buttonItemScreen2.Tag = "1";
			this.buttonItemScreen2.Text = "2";
			this.buttonItemScreen2.Click += new System.EventHandler(this.OnMonitorSelectorClick);
			// 
			// buttonItemScreen3
			// 
			this.buttonItemScreen3.Name = "buttonItemScreen3";
			this.buttonItemScreen3.Tag = "2";
			this.buttonItemScreen3.Text = "3";
			this.buttonItemScreen3.Click += new System.EventHandler(this.OnMonitorSelectorClick);
			// 
			// buttonItemScreen4
			// 
			this.buttonItemScreen4.Name = "buttonItemScreen4";
			this.buttonItemScreen4.Tag = "3";
			this.buttonItemScreen4.Text = "4";
			this.buttonItemScreen4.Click += new System.EventHandler(this.OnMonitorSelectorClick);
			// 
			// buttonItemScreen5
			// 
			this.buttonItemScreen5.Name = "buttonItemScreen5";
			this.buttonItemScreen5.Tag = "4";
			this.buttonItemScreen5.Text = "5";
			this.buttonItemScreen5.Click += new System.EventHandler(this.OnMonitorSelectorClick);
			// 
			// buttonItemScreen6
			// 
			this.buttonItemScreen6.Name = "buttonItemScreen6";
			this.buttonItemScreen6.Tag = "5";
			this.buttonItemScreen6.Text = "6";
			this.buttonItemScreen6.Click += new System.EventHandler(this.OnMonitorSelectorClick);
			// 
			// itemContainer5
			// 
			// 
			// 
			// 
			this.itemContainer5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.itemContainer5.ItemSpacing = 20;
			this.itemContainer5.Name = "itemContainer5";
			this.itemContainer5.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.checkBoxItemLoadAtStartup,
            this.checkBoxItemDocked});
			// 
			// 
			// 
			this.itemContainer5.TitleMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.itemContainer5.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// checkBoxItemLoadAtStartup
			// 
			this.checkBoxItemLoadAtStartup.Name = "checkBoxItemLoadAtStartup";
			this.checkBoxItemLoadAtStartup.Text = "start";
			this.checkBoxItemLoadAtStartup.CheckedChanged += new DevComponents.DotNetBar.CheckBoxChangeEventHandler(this.OnLoadAtStartupCheckedChanged);
			// 
			// checkBoxItemDocked
			// 
			this.checkBoxItemDocked.Name = "checkBoxItemDocked";
			this.checkBoxItemDocked.Text = "dock";
			// 
			// itemContainer7
			// 
			// 
			// 
			// 
			this.itemContainer7.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.itemContainer7.FixedSize = new System.Drawing.Size(10, 0);
			this.itemContainer7.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
			this.itemContainer7.Name = "itemContainer7";
			this.itemContainer7.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.labelItem2});
			// 
			// 
			// 
			this.itemContainer7.TitleMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.itemContainer7.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// labelItem2
			// 
			this.labelItem2.Name = "labelItem2";
			this.labelItem2.Text = "  ";
			// 
			// ribbonBarBrowsers
			// 
			this.ribbonBarBrowsers.AutoOverflowEnabled = true;
			// 
			// 
			// 
			this.ribbonBarBrowsers.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarBrowsers.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarBrowsers.ContainerControlProcessDialogKey = true;
			this.ribbonBarBrowsers.DragDropSupport = true;
			this.ribbonBarBrowsers.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.itemContainer1,
            this.itemContainer2});
			this.ribbonBarBrowsers.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			this.ribbonBarBrowsers.Location = new System.Drawing.Point(12, 16);
			this.ribbonBarBrowsers.Name = "ribbonBarBrowsers";
			this.ribbonBarBrowsers.Size = new System.Drawing.Size(154, 145);
			this.ribbonBarBrowsers.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonBarBrowsers.TabIndex = 1;
			this.ribbonBarBrowsers.Text = "Browser";
			// 
			// 
			// 
			this.ribbonBarBrowsers.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBarBrowsers.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// itemContainer1
			// 
			// 
			// 
			// 
			this.itemContainer1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.itemContainer1.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
			this.itemContainer1.Name = "itemContainer1";
			this.itemContainer1.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItemBrowserCh,
            this.buttonItemBrowserEdge});
			// 
			// 
			// 
			this.itemContainer1.TitleMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.itemContainer1.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// buttonItemBrowserCh
			// 
			this.buttonItemBrowserCh.Enabled = false;
			this.buttonItemBrowserCh.Image = ((System.Drawing.Image)(resources.GetObject("buttonItemBrowserCh.Image")));
			this.buttonItemBrowserCh.Name = "buttonItemBrowserCh";
			this.buttonItemBrowserCh.Tag = "chrome";
			this.buttonItemBrowserCh.Text = "buttonItem1";
			this.buttonItemBrowserCh.Tooltip = "Google Chrome";
			this.buttonItemBrowserCh.Click += new System.EventHandler(this.OnBrowserSelectorClick);
			// 
			// buttonItemBrowserEdge
			// 
			this.buttonItemBrowserEdge.Enabled = false;
			this.buttonItemBrowserEdge.Image = global::Asa.Bar.App.Properties.Resources.edge;
			this.buttonItemBrowserEdge.Name = "buttonItemBrowserEdge";
			this.buttonItemBrowserEdge.Tag = "edge";
			this.buttonItemBrowserEdge.Text = "buttonItem3";
			this.buttonItemBrowserEdge.Tooltip = "Edge";
			this.buttonItemBrowserEdge.Click += new System.EventHandler(this.OnBrowserSelectorClick);
			// 
			// itemContainer2
			// 
			// 
			// 
			// 
			this.itemContainer2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.itemContainer2.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
			this.itemContainer2.Name = "itemContainer2";
			this.itemContainer2.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItemBrowserFF,
            this.buttonItemBrowserIE});
			// 
			// 
			// 
			this.itemContainer2.TitleMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.itemContainer2.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// buttonItemBrowserFF
			// 
			this.buttonItemBrowserFF.Enabled = false;
			this.buttonItemBrowserFF.Image = ((System.Drawing.Image)(resources.GetObject("buttonItemBrowserFF.Image")));
			this.buttonItemBrowserFF.Name = "buttonItemBrowserFF";
			this.buttonItemBrowserFF.Tag = "firefox";
			this.buttonItemBrowserFF.Text = "buttonItem2";
			this.buttonItemBrowserFF.Tooltip = "Mozilla Firefox";
			this.buttonItemBrowserFF.Click += new System.EventHandler(this.OnBrowserSelectorClick);
			// 
			// buttonItemBrowserIE
			// 
			this.buttonItemBrowserIE.Enabled = false;
			this.buttonItemBrowserIE.Image = ((System.Drawing.Image)(resources.GetObject("buttonItemBrowserIE.Image")));
			this.buttonItemBrowserIE.Name = "buttonItemBrowserIE";
			this.buttonItemBrowserIE.Tag = "iexplore";
			this.buttonItemBrowserIE.Text = "buttonItem4";
			this.buttonItemBrowserIE.Tooltip = "Internet Explorer";
			this.buttonItemBrowserIE.Click += new System.EventHandler(this.OnBrowserSelectorClick);
			// 
			// superTabItemTemplates
			// 
			this.superTabItemTemplates.AttachedControl = this.superTabControlPanel2;
			this.superTabItemTemplates.GlobalItem = false;
			this.superTabItemTemplates.Name = "superTabItemTemplates";
			superTabItemStateColorTable1.Text = System.Drawing.Color.Red;
			superTabColorStates1.Normal = superTabItemStateColorTable1;
			superTabItemColorTable1.Default = superTabColorStates1;
			this.superTabItemTemplates.TabColor = superTabItemColorTable1;
			this.superTabItemTemplates.Text = "Custom ribbonBar templates to use";
			this.superTabItemTemplates.TextAlignment = DevComponents.DotNetBar.eItemAlignment.Center;
			// 
			// superTabControlPanel1
			// 
			this.superTabControlPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.superTabControlPanel1.Location = new System.Drawing.Point(0, 0);
			this.superTabControlPanel1.Name = "superTabControlPanel1";
			this.superTabControlPanel1.Size = new System.Drawing.Size(600, 200);
			this.superTabControlPanel1.TabIndex = 1;
			// 
			// styleManager
			// 
			this.styleManager.ManagerStyle = DevComponents.DotNetBar.eStyle.Metro;
			this.styleManager.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255))))), System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(115)))), ((int)(((byte)(199))))));
			// 
			// timerUpdateWindow
			// 
			this.timerUpdateWindow.Interval = 5000;
			this.timerUpdateWindow.Tick += new System.EventHandler(this.OnUpdateWindowTimerTick);
			// 
			// notifyIcon
			// 
			this.notifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
			this.notifyIcon.ContextMenuStrip = this.contextMenuStrip;
			this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
			this.notifyIcon.Text = "Minibar";
			// 
			// contextMenuStrip
			// 
			this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemCenterScreen,
            this.toolStripSeparator1,
            this.toolStripMenuItemDock,
            this.toolStripSeparator2,
            this.toolStripMenuItemExit});
			this.contextMenuStrip.Name = "contextMenuStrip";
			this.contextMenuStrip.Size = new System.Drawing.Size(148, 82);
			// 
			// toolStripMenuItemCenterScreen
			// 
			this.toolStripMenuItemCenterScreen.Name = "toolStripMenuItemCenterScreen";
			this.toolStripMenuItemCenterScreen.Size = new System.Drawing.Size(147, 22);
			this.toolStripMenuItemCenterScreen.Text = "Center Screen";
			this.toolStripMenuItemCenterScreen.Click += new System.EventHandler(this.OnToolStripMenuItemCenterScreenClick);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(144, 6);
			// 
			// toolStripMenuItemDock
			// 
			this.toolStripMenuItemDock.Name = "toolStripMenuItemDock";
			this.toolStripMenuItemDock.Size = new System.Drawing.Size(147, 22);
			this.toolStripMenuItemDock.Text = "Taskbar Dock";
			this.toolStripMenuItemDock.Click += new System.EventHandler(this.OnToolStripMenuItemDockClick);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(144, 6);
			// 
			// toolStripMenuItemExit
			// 
			this.toolStripMenuItemExit.Name = "toolStripMenuItemExit";
			this.toolStripMenuItemExit.Size = new System.Drawing.Size(147, 22);
			this.toolStripMenuItemExit.Text = "Exit Minibar";
			this.toolStripMenuItemExit.Click += new System.EventHandler(this.OnTabControlCloseClick);
			// 
			// FormMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.ClientSize = new System.Drawing.Size(600, 200);
			this.ControlBox = false;
			this.Controls.Add(this.superTabControlMain);
			this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormMain";
			this.Opacity = 0D;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "adsalesapps";
			this.TopMost = true;
			this.Deactivate += new System.EventHandler(this.OnFormDeactivate);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OnFormClosed);
			this.Load += new System.EventHandler(this.OnFormLoad);
			this.Shown += new System.EventHandler(this.OnFormShown);
			((System.ComponentModel.ISupportInitialize)(this.superTabControlMain)).EndInit();
			this.superTabControlMain.ResumeLayout(false);
			this.superTabControlPanel2.ResumeLayout(false);
			this.contextMenuStrip.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.SuperTabControl superTabControlMain;
        private DevComponents.DotNetBar.SuperTabControlPanel superTabControlPanel1;
        private DevComponents.DotNetBar.SuperTabControlPanel superTabControlPanel2;
        private DevComponents.DotNetBar.SuperTabItem superTabItemTemplates;
        private DevComponents.DotNetBar.StyleManager styleManager;
        private DevComponents.DotNetBar.RibbonBar ribbonBarBrowsers;
        private DevComponents.DotNetBar.ItemContainer itemContainer1;
        private DevComponents.DotNetBar.ButtonItem buttonItemBrowserCh;
        private DevComponents.DotNetBar.ButtonItem buttonItemBrowserEdge;
        private DevComponents.DotNetBar.ItemContainer itemContainer2;
        private DevComponents.DotNetBar.ButtonItem buttonItemBrowserFF;
        private DevComponents.DotNetBar.ButtonItem buttonItemBrowserIE;
		private DevComponents.DotNetBar.RibbonBar ribbonBarSettings;
		private DevComponents.DotNetBar.ItemContainer itemContainer3;
        private System.Windows.Forms.Timer timerUpdateWindow;
		private DevComponents.DotNetBar.ItemContainer itemContainerMonitors;
        private DevComponents.DotNetBar.ButtonItem buttonItemScreen1;
        private DevComponents.DotNetBar.ButtonItem buttonItemScreen2;
        private DevComponents.DotNetBar.ButtonItem buttonItemScreen3;
        private DevComponents.DotNetBar.ButtonItem buttonItemScreen4;
        private DevComponents.DotNetBar.ButtonItem buttonItemScreen5;
        private DevComponents.DotNetBar.ButtonItem buttonItemScreen6;
        private DevComponents.DotNetBar.ItemContainer itemContainer4;
		private DevComponents.DotNetBar.ItemContainer itemContainer5;
		private DevComponents.DotNetBar.CheckBoxItem checkBoxItemLoadAtStartup;
		private DevComponents.DotNetBar.CheckBoxItem checkBoxItemDocked;
		private DevComponents.DotNetBar.ItemContainer itemContainer6;
		private DevComponents.DotNetBar.LabelItem labelItem1;
		private DevComponents.DotNetBar.ItemContainer itemContainer7;
		private DevComponents.DotNetBar.LabelItem labelItem2;
		private DevComponents.DotNetBar.ButtonItem buttonItemColorSelector;
		private DevComponents.DotNetBar.ColorPickerDropDown colorPickerDropDownAccent;
		private DevComponents.DotNetBar.ColorPickerDropDown colorPickerDropDownText;
		private System.Windows.Forms.NotifyIcon notifyIcon;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemCenterScreen;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDock;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemExit;
	}
}

