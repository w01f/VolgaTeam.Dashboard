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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormBar));
			this.superTabControlMain = new DevComponents.DotNetBar.SuperTabControl();
			this.superTabControlPanel2 = new DevComponents.DotNetBar.SuperTabControlPanel();
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
			this.styleManagerMain = new DevComponents.DotNetBar.StyleManager();
			this.timerUpdate = new System.Windows.Forms.Timer();
			this.timerChecker = new System.Windows.Forms.Timer();
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
			this.superTabControlPanel2.Controls.Add(this.ribbonBarBrowsers);
			this.superTabControlPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.superTabControlPanel2.Location = new System.Drawing.Point(0, 27);
			this.superTabControlPanel2.Name = "superTabControlPanel2";
			this.superTabControlPanel2.Size = new System.Drawing.Size(600, 173);
			this.superTabControlPanel2.TabIndex = 2;
			this.superTabControlPanel2.TabItem = this.superTabItemTemplates;
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
			this.styleManagerMain.ManagerStyle = DevComponents.DotNetBar.eStyle.Office2010Blue;
			this.styleManagerMain.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.White, System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(87)))), ((int)(((byte)(154))))));
			// 
			// timerUpdate
			// 
			this.timerUpdate.Enabled = true;
			this.timerUpdate.Interval = 5000;
			this.timerUpdate.Tick += new System.EventHandler(this.timerUpdate_Tick);
			// 
			// timerChecker
			// 
			this.timerChecker.Interval = 5000;
			this.timerChecker.Tick += new System.EventHandler(this.timerChecker_Tick);
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
			this.Load += new System.EventHandler(this.FormBar_Load);
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
        private System.Windows.Forms.Timer timerUpdate;
        private DevComponents.DotNetBar.RibbonBar ribbonBarBrowsers;
        private DevComponents.DotNetBar.ItemContainer itemContainer1;
        private DevComponents.DotNetBar.ButtonItem buttonItemBrowserCh;
        private DevComponents.DotNetBar.ButtonItem buttonItemBrowserOp;
        private DevComponents.DotNetBar.ItemContainer itemContainer2;
        private DevComponents.DotNetBar.ButtonItem buttonItemBrowserFF;
        private DevComponents.DotNetBar.ButtonItem buttonItemBrowserIE;
        private System.Windows.Forms.Timer timerChecker;
    }
}

