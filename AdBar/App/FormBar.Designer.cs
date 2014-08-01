namespace AdBAR
{
    partial class FormBar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormBar));
            this.superTabControlMain = new DevComponents.DotNetBar.SuperTabControl();
            this.superTabControlPanel2 = new DevComponents.DotNetBar.SuperTabControlPanel();
            this.ribbonBarSync = new DevComponents.DotNetBar.RibbonBar();
            this.itemContainer3 = new DevComponents.DotNetBar.ItemContainer();
            this.itemContainer4 = new DevComponents.DotNetBar.ItemContainer();
            this.checkBoxItemSyncEnable = new DevComponents.DotNetBar.CheckBoxItem();
            this.colorPickerDropDownInterface = new DevComponents.DotNetBar.ColorPickerDropDown();
            this.checkBoxItemSyncHourly = new DevComponents.DotNetBar.CheckBoxItem();
            this.itemContainerMonitors = new DevComponents.DotNetBar.ItemContainer();
            this.labelItem2 = new DevComponents.DotNetBar.LabelItem();
            this.buttonItemScreen1 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItemScreen2 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItemScreen3 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItemScreen4 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItemScreen5 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItemScreen6 = new DevComponents.DotNetBar.ButtonItem();
            this.itemContainer7 = new DevComponents.DotNetBar.ItemContainer();
            this.itemContainer5 = new DevComponents.DotNetBar.ItemContainer();
            this.labelItem1 = new DevComponents.DotNetBar.LabelItem();
            this.labelItemSyncLast = new DevComponents.DotNetBar.LabelItem();
            this.itemContainer6 = new DevComponents.DotNetBar.ItemContainer();
            this.labelItemSyncLabelNext = new DevComponents.DotNetBar.LabelItem();
            this.labelItemSyncNext = new DevComponents.DotNetBar.LabelItem();
            this.ribbonBarBrowsers = new DevComponents.DotNetBar.RibbonBar();
            this.itemContainer1 = new DevComponents.DotNetBar.ItemContainer();
            this.buttonItemBrowserCh = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItemBrowserOp = new DevComponents.DotNetBar.ButtonItem();
            this.itemContainer2 = new DevComponents.DotNetBar.ItemContainer();
            this.buttonItemBrowserFF = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItemBrowserIE = new DevComponents.DotNetBar.ButtonItem();
            this.superTabItemTemplates = new DevComponents.DotNetBar.SuperTabItem();
            this.superTabControlPanel1 = new DevComponents.DotNetBar.SuperTabControlPanel();
            this.backgroundWorkerChecker = new System.ComponentModel.BackgroundWorker();
            this.styleManagerMain = new DevComponents.DotNetBar.StyleManager(this.components);
            this.timerChecker = new System.Windows.Forms.Timer(this.components);
            this.timerSyncronization = new System.Windows.Forms.Timer(this.components);
            this.timerUpdate = new System.Windows.Forms.Timer(this.components);
            this.timerMonitorWatcher = new System.Windows.Forms.Timer(this.components);
            this.labelItem3 = new DevComponents.DotNetBar.LabelItem();
            ((System.ComponentModel.ISupportInitialize)(this.superTabControlMain)).BeginInit();
            this.superTabControlMain.SuspendLayout();
            this.superTabControlPanel2.SuspendLayout();
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
            this.superTabControlMain.ControlBox.CloseBox.Click += new System.EventHandler(this.superTabControlMain_ControlBox_CloseBox_Click);
            // 
            // 
            // 
            this.superTabControlMain.ControlBox.MenuBox.Name = "";
            this.superTabControlMain.ControlBox.MenuBox.Visible = false;
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
            this.superTabControlMain.SelectedTabFont = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.superTabControlMain.SelectedTabIndex = 0;
            this.superTabControlMain.Size = new System.Drawing.Size(600, 200);
            this.superTabControlMain.TabFont = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.superTabControlMain.TabIndex = 0;
            this.superTabControlMain.TabLayoutType = DevComponents.DotNetBar.eSuperTabLayoutType.SingleLineFit;
            this.superTabControlMain.Tabs.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.superTabItemTemplates});
            this.superTabControlMain.Text = "superTabControl1";
            this.superTabControlMain.TextAlignment = DevComponents.DotNetBar.eItemAlignment.Center;
            this.superTabControlMain.SelectedTabChanged += new System.EventHandler<DevComponents.DotNetBar.SuperTabStripSelectedTabChangedEventArgs>(this.superTabControlMain_SelectedTabChanged);
            this.superTabControlMain.TabStripMouseMove += new System.EventHandler<System.Windows.Forms.MouseEventArgs>(this.superTabControlMain_TabStripMouseMove);
            // 
            // superTabControlPanel2
            // 
            this.superTabControlPanel2.Controls.Add(this.ribbonBarSync);
            this.superTabControlPanel2.Controls.Add(this.ribbonBarBrowsers);
            this.superTabControlPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.superTabControlPanel2.Location = new System.Drawing.Point(0, 27);
            this.superTabControlPanel2.Name = "superTabControlPanel2";
            this.superTabControlPanel2.Size = new System.Drawing.Size(600, 173);
            this.superTabControlPanel2.TabIndex = 2;
            this.superTabControlPanel2.TabItem = this.superTabItemTemplates;
            // 
            // ribbonBarSync
            // 
            this.ribbonBarSync.AutoOverflowEnabled = false;
            // 
            // 
            // 
            this.ribbonBarSync.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonBarSync.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ribbonBarSync.ContainerControlProcessDialogKey = true;
            this.ribbonBarSync.DragDropSupport = true;
            this.ribbonBarSync.HorizontalItemAlignment = DevComponents.DotNetBar.eHorizontalItemsAlignment.Center;
            this.ribbonBarSync.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.itemContainer3});
            this.ribbonBarSync.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.ribbonBarSync.Location = new System.Drawing.Point(172, 16);
            this.ribbonBarSync.Name = "ribbonBarSync";
            this.ribbonBarSync.Size = new System.Drawing.Size(237, 135);
            this.ribbonBarSync.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ribbonBarSync.TabIndex = 2;
            this.ribbonBarSync.Text = "Extras";
            // 
            // 
            // 
            this.ribbonBarSync.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonBarSync.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ribbonBarSync.VerticalItemAlignment = DevComponents.DotNetBar.eVerticalItemsAlignment.Middle;
            // 
            // itemContainer3
            // 
            // 
            // 
            // 
            this.itemContainer3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.itemContainer3.HorizontalItemAlignment = DevComponents.DotNetBar.eHorizontalItemsAlignment.Center;
            this.itemContainer3.ItemSpacing = 5;
            this.itemContainer3.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
            this.itemContainer3.Name = "itemContainer3";
            this.itemContainer3.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.itemContainer4,
            this.checkBoxItemSyncHourly,
            this.itemContainerMonitors,
            this.itemContainer7});
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
            this.itemContainer4.Name = "itemContainer4";
            this.itemContainer4.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.checkBoxItemSyncEnable,
            this.labelItem3,
            this.colorPickerDropDownInterface});
            // 
            // 
            // 
            this.itemContainer4.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // checkBoxItemSyncEnable
            // 
            this.checkBoxItemSyncEnable.Name = "checkBoxItemSyncEnable";
            this.checkBoxItemSyncEnable.Text = "Sync Enabled";
            this.checkBoxItemSyncEnable.Click += new System.EventHandler(this.checkBoxItemSyncEnable_Click);
            // 
            // colorPickerDropDownInterface
            // 
            this.colorPickerDropDownInterface.AutoExpandOnClick = true;
            this.colorPickerDropDownInterface.Name = "colorPickerDropDownInterface";
            this.colorPickerDropDownInterface.PopupAnimation = DevComponents.DotNetBar.ePopupAnimation.None;
            this.colorPickerDropDownInterface.SplitButton = true;
            this.colorPickerDropDownInterface.Text = "AdBar Color";
            this.colorPickerDropDownInterface.SelectedColorChanged += new System.EventHandler(this.colorPickerDropDownInterface_SelectedColorChanged);
            this.colorPickerDropDownInterface.ColorPreview += new DevComponents.DotNetBar.ColorPreviewEventHandler(this.colorPickerDropDownInterface_ColorPreview);
            this.colorPickerDropDownInterface.PopupClose += new System.EventHandler(this.colorPickerDropDownInterface_PopupClose);
            // 
            // checkBoxItemSyncHourly
            // 
            this.checkBoxItemSyncHourly.Name = "checkBoxItemSyncHourly";
            this.checkBoxItemSyncHourly.Text = "Sync Hourly";
            this.checkBoxItemSyncHourly.Click += new System.EventHandler(this.checkBoxItemSyncHourly_Click);
            // 
            // itemContainerMonitors
            // 
            // 
            // 
            // 
            this.itemContainerMonitors.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.itemContainerMonitors.Name = "itemContainerMonitors";
            this.itemContainerMonitors.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.labelItem2,
            this.buttonItemScreen1,
            this.buttonItemScreen2,
            this.buttonItemScreen3,
            this.buttonItemScreen4,
            this.buttonItemScreen5,
            this.buttonItemScreen6});
            // 
            // 
            // 
            this.itemContainerMonitors.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // labelItem2
            // 
            this.labelItem2.Name = "labelItem2";
            this.labelItem2.Text = "Screen:";
            // 
            // buttonItemScreen1
            // 
            this.buttonItemScreen1.Name = "buttonItemScreen1";
            this.buttonItemScreen1.Text = "1";
            this.buttonItemScreen1.Click += new System.EventHandler(this.buttonItemScreen_Click);
            // 
            // buttonItemScreen2
            // 
            this.buttonItemScreen2.Name = "buttonItemScreen2";
            this.buttonItemScreen2.Text = "2";
            this.buttonItemScreen2.Click += new System.EventHandler(this.buttonItemScreen_Click);
            // 
            // buttonItemScreen3
            // 
            this.buttonItemScreen3.Name = "buttonItemScreen3";
            this.buttonItemScreen3.Text = "3";
            this.buttonItemScreen3.Click += new System.EventHandler(this.buttonItemScreen_Click);
            // 
            // buttonItemScreen4
            // 
            this.buttonItemScreen4.Name = "buttonItemScreen4";
            this.buttonItemScreen4.Text = "4";
            this.buttonItemScreen4.Click += new System.EventHandler(this.buttonItemScreen_Click);
            // 
            // buttonItemScreen5
            // 
            this.buttonItemScreen5.Name = "buttonItemScreen5";
            this.buttonItemScreen5.Text = "5";
            this.buttonItemScreen5.Click += new System.EventHandler(this.buttonItemScreen_Click);
            // 
            // buttonItemScreen6
            // 
            this.buttonItemScreen6.Name = "buttonItemScreen6";
            this.buttonItemScreen6.Text = "6";
            this.buttonItemScreen6.Click += new System.EventHandler(this.buttonItemScreen_Click);
            // 
            // itemContainer7
            // 
            // 
            // 
            // 
            this.itemContainer7.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.itemContainer7.HorizontalItemAlignment = DevComponents.DotNetBar.eHorizontalItemsAlignment.Center;
            this.itemContainer7.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
            this.itemContainer7.Name = "itemContainer7";
            this.itemContainer7.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.itemContainer5,
            this.itemContainer6});
            // 
            // 
            // 
            this.itemContainer7.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // itemContainer5
            // 
            // 
            // 
            // 
            this.itemContainer5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.itemContainer5.Name = "itemContainer5";
            this.itemContainer5.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.labelItem1,
            this.labelItemSyncLast});
            // 
            // 
            // 
            this.itemContainer5.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // labelItem1
            // 
            this.labelItem1.Name = "labelItem1";
            this.labelItem1.Text = "Last sync:";
            // 
            // labelItemSyncLast
            // 
            this.labelItemSyncLast.Name = "labelItemSyncLast";
            this.labelItemSyncLast.Text = "---";
            // 
            // itemContainer6
            // 
            // 
            // 
            // 
            this.itemContainer6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.itemContainer6.Name = "itemContainer6";
            this.itemContainer6.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.labelItemSyncLabelNext,
            this.labelItemSyncNext});
            // 
            // 
            // 
            this.itemContainer6.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // labelItemSyncLabelNext
            // 
            this.labelItemSyncLabelNext.BeginGroup = true;
            this.labelItemSyncLabelNext.Name = "labelItemSyncLabelNext";
            this.labelItemSyncLabelNext.Text = "Next sync:";
            // 
            // labelItemSyncNext
            // 
            this.labelItemSyncNext.Name = "labelItemSyncNext";
            this.labelItemSyncNext.Text = "---";
            this.labelItemSyncNext.Click += new System.EventHandler(this.labelItemSyncNext_Click);
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
            this.ribbonBarBrowsers.Size = new System.Drawing.Size(154, 135);
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
            this.buttonItemBrowserOp});
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
            this.buttonItemBrowserCh.Click += new System.EventHandler(this.buttonItemBrowserSwitch_Click);
            // 
            // buttonItemBrowserOp
            // 
            this.buttonItemBrowserOp.Enabled = false;
            this.buttonItemBrowserOp.Image = ((System.Drawing.Image)(resources.GetObject("buttonItemBrowserOp.Image")));
            this.buttonItemBrowserOp.Name = "buttonItemBrowserOp";
            this.buttonItemBrowserOp.Tag = "opera";
            this.buttonItemBrowserOp.Text = "buttonItem3";
            this.buttonItemBrowserOp.Tooltip = "Opera Browser";
            this.buttonItemBrowserOp.Click += new System.EventHandler(this.buttonItemBrowserSwitch_Click);
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
            this.buttonItemBrowserFF.Click += new System.EventHandler(this.buttonItemBrowserSwitch_Click);
            // 
            // buttonItemBrowserIE
            // 
            this.buttonItemBrowserIE.Enabled = false;
            this.buttonItemBrowserIE.Image = ((System.Drawing.Image)(resources.GetObject("buttonItemBrowserIE.Image")));
            this.buttonItemBrowserIE.Name = "buttonItemBrowserIE";
            this.buttonItemBrowserIE.Tag = "iexplore";
            this.buttonItemBrowserIE.Text = "buttonItem4";
            this.buttonItemBrowserIE.Tooltip = "Internet Explorer";
            this.buttonItemBrowserIE.Click += new System.EventHandler(this.buttonItemBrowserSwitch_Click);
            // 
            // superTabItemTemplates
            // 
            this.superTabItemTemplates.AttachedControl = this.superTabControlPanel2;
            this.superTabItemTemplates.GlobalItem = false;
            this.superTabItemTemplates.Name = "superTabItemTemplates";
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
            // backgroundWorkerChecker
            // 
            this.backgroundWorkerChecker.WorkerReportsProgress = true;
            this.backgroundWorkerChecker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerChecker_DoWork);
            this.backgroundWorkerChecker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorkerChecker_ProgressChanged);
            this.backgroundWorkerChecker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerChecker_RunWorkerCompleted);
            // 
            // styleManagerMain
            // 
            this.styleManagerMain.ManagerStyle = DevComponents.DotNetBar.eStyle.Office2007Blue;
            this.styleManagerMain.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.White, System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(87)))), ((int)(((byte)(154))))));
            // 
            // timerChecker
            // 
            this.timerChecker.Interval = 5000;
            this.timerChecker.Tick += new System.EventHandler(this.timerChecker_Tick);
            // 
            // timerSyncronization
            // 
            this.timerSyncronization.Interval = 1;
            this.timerSyncronization.Tick += new System.EventHandler(this.timerSyncronization_Tick);
            // 
            // timerUpdate
            // 
            this.timerUpdate.Enabled = true;
            this.timerUpdate.Interval = 5000;
            this.timerUpdate.Tick += new System.EventHandler(this.timerUpdate_Tick);
            // 
            // timerMonitorWatcher
            // 
            this.timerMonitorWatcher.Enabled = true;
            this.timerMonitorWatcher.Interval = 2500;
            this.timerMonitorWatcher.Tick += new System.EventHandler(this.timerMonitorWatcher_Tick);
            // 
            // labelItem3
            // 
            this.labelItem3.Name = "labelItem3";
            this.labelItem3.Text = "     ";
            // 
            // FormBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 200);
            this.ControlBox = false;
            this.Controls.Add(this.superTabControlMain);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormBar";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "adBAR";
            this.TopMost = true;
            this.Deactivate += new System.EventHandler(this.FormBar_Deactivate);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormBar_FormClosed);
            this.Load += new System.EventHandler(this.FormBar_Load);
            this.Shown += new System.EventHandler(this.FormBar_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.superTabControlMain)).EndInit();
            this.superTabControlMain.ResumeLayout(false);
            this.superTabControlPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.SuperTabControl superTabControlMain;
        private DevComponents.DotNetBar.SuperTabControlPanel superTabControlPanel1;
        private System.ComponentModel.BackgroundWorker backgroundWorkerChecker;
        private DevComponents.DotNetBar.SuperTabControlPanel superTabControlPanel2;
        private DevComponents.DotNetBar.SuperTabItem superTabItemTemplates;
        private DevComponents.DotNetBar.StyleManager styleManagerMain;
        private DevComponents.DotNetBar.RibbonBar ribbonBarBrowsers;
        private DevComponents.DotNetBar.ItemContainer itemContainer1;
        private DevComponents.DotNetBar.ButtonItem buttonItemBrowserCh;
        private DevComponents.DotNetBar.ButtonItem buttonItemBrowserOp;
        private DevComponents.DotNetBar.ItemContainer itemContainer2;
        private DevComponents.DotNetBar.ButtonItem buttonItemBrowserFF;
        private DevComponents.DotNetBar.ButtonItem buttonItemBrowserIE;
        private System.Windows.Forms.Timer timerChecker;
        private DevComponents.DotNetBar.RibbonBar ribbonBarSync;
        private System.Windows.Forms.Timer timerSyncronization;
        private DevComponents.DotNetBar.ItemContainer itemContainer3;
        private DevComponents.DotNetBar.ItemContainer itemContainer5;
        private DevComponents.DotNetBar.LabelItem labelItem1;
        private DevComponents.DotNetBar.LabelItem labelItemSyncLast;
        private DevComponents.DotNetBar.LabelItem labelItemSyncLabelNext;
        private DevComponents.DotNetBar.LabelItem labelItemSyncNext;
        private DevComponents.DotNetBar.ItemContainer itemContainer7;
        private DevComponents.DotNetBar.ItemContainer itemContainer6;
        private DevComponents.DotNetBar.CheckBoxItem checkBoxItemSyncEnable;
        private DevComponents.DotNetBar.CheckBoxItem checkBoxItemSyncHourly;
        private System.Windows.Forms.Timer timerUpdate;
        private System.Windows.Forms.Timer timerMonitorWatcher;
        private DevComponents.DotNetBar.ItemContainer itemContainerMonitors;
        private DevComponents.DotNetBar.LabelItem labelItem2;
        private DevComponents.DotNetBar.ButtonItem buttonItemScreen1;
        private DevComponents.DotNetBar.ButtonItem buttonItemScreen2;
        private DevComponents.DotNetBar.ButtonItem buttonItemScreen3;
        private DevComponents.DotNetBar.ButtonItem buttonItemScreen4;
        private DevComponents.DotNetBar.ButtonItem buttonItemScreen5;
        private DevComponents.DotNetBar.ButtonItem buttonItemScreen6;
        private DevComponents.DotNetBar.ItemContainer itemContainer4;
        private DevComponents.DotNetBar.ColorPickerDropDown colorPickerDropDownInterface;
        private DevComponents.DotNetBar.LabelItem labelItem3;
    }
}

