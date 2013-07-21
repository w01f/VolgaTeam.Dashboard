namespace TVScheduleBuilder.CustomControls
{
    partial class HomeControl
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
			this.stationsControl = new TVScheduleBuilder.CustomControls.StationsControl();
			this.pbMonthlySchedule = new System.Windows.Forms.PictureBox();
			this.pbWeeklySchedule = new System.Windows.Forms.PictureBox();
			this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
			this.xtraTabControlMain = new DevExpress.XtraTab.XtraTabControl();
			this.xtraTabPageTV = new DevExpress.XtraTab.XtraTabPage();
			this.pnTV = new System.Windows.Forms.Panel();
			this.daypartsControl = new TVScheduleBuilder.CustomControls.DaypartsControl();
			this.pnRightTop = new System.Windows.Forms.Panel();
			this.laRightTopTitle = new System.Windows.Forms.Label();
			this.pbOptionsHelp = new System.Windows.Forms.PictureBox();
			this.xtraTabControlOptions = new DevExpress.XtraTab.XtraTabControl();
			this.xtraTabPageStations = new DevExpress.XtraTab.XtraTabPage();
			this.xtraTabPageDayparts = new DevExpress.XtraTab.XtraTabPage();
			this.xtraTabPageDemos = new DevExpress.XtraTab.XtraTabPage();
			this.pnDemos = new System.Windows.Forms.Panel();
			this.pnDemosSource = new System.Windows.Forms.Panel();
			this.comboBoxEditSource = new DevExpress.XtraEditors.ComboBoxEdit();
			this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
			this.buttonXDemosSourceDisable = new DevComponents.DotNetBar.ButtonX();
			this.buttonXDemosSourceEnable = new DevComponents.DotNetBar.ButtonX();
			this.laDemosSource = new System.Windows.Forms.Label();
			this.pnDemosSelect = new System.Windows.Forms.Panel();
			this.comboBoxEditDemos = new DevExpress.XtraEditors.ComboBoxEdit();
			this.laDemosSelect = new System.Windows.Forms.Label();
			this.pnDemosType = new System.Windows.Forms.Panel();
			this.buttonXDemosImps = new DevComponents.DotNetBar.ButtonX();
			this.buttonXDemosRtg = new DevComponents.DotNetBar.ButtonX();
			this.laDemosType = new System.Windows.Forms.Label();
			this.pnDemosButtons = new System.Windows.Forms.Panel();
			this.buttonXDemosCustom = new DevComponents.DotNetBar.ButtonX();
			this.buttonXDemosImport = new DevComponents.DotNetBar.ButtonX();
			this.buttonXDemosNo = new DevComponents.DotNetBar.ButtonX();
			this.splitContainerControl = new DevExpress.XtraEditors.SplitContainerControl();
			this.pnTop = new System.Windows.Forms.Panel();
			this.laTopTitle = new System.Windows.Forms.Label();
			this.xtraTabPageDigital = new DevExpress.XtraTab.XtraTabPage();
			this.pnOptionsDigital = new System.Windows.Forms.Panel();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)(this.pbMonthlySchedule)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbWeeklySchedule)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControlMain)).BeginInit();
			this.xtraTabControlMain.SuspendLayout();
			this.xtraTabPageTV.SuspendLayout();
			this.pnTV.SuspendLayout();
			this.pnRightTop.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbOptionsHelp)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControlOptions)).BeginInit();
			this.xtraTabControlOptions.SuspendLayout();
			this.xtraTabPageStations.SuspendLayout();
			this.xtraTabPageDayparts.SuspendLayout();
			this.xtraTabPageDemos.SuspendLayout();
			this.pnDemos.SuspendLayout();
			this.pnDemosSource.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditSource.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			this.pnDemosSelect.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditDemos.Properties)).BeginInit();
			this.pnDemosType.SuspendLayout();
			this.pnDemosButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainerControl)).BeginInit();
			this.splitContainerControl.SuspendLayout();
			this.pnTop.SuspendLayout();
			this.xtraTabPageDigital.SuspendLayout();
			this.pnOptionsDigital.SuspendLayout();
			this.SuspendLayout();
			// 
			// stationsControl
			// 
			this.stationsControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
			this.stationsControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.stationsControl.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.stationsControl.HasChanged = false;
			this.stationsControl.Location = new System.Drawing.Point(0, 0);
			this.stationsControl.Name = "stationsControl";
			this.stationsControl.Padding = new System.Windows.Forms.Padding(3);
			this.stationsControl.Size = new System.Drawing.Size(344, 580);
			this.stationsControl.TabIndex = 0;
			// 
			// pbMonthlySchedule
			// 
			this.pbMonthlySchedule.Image = global::TVScheduleBuilder.Properties.Resources.MonthlyScheduleButton;
			this.pbMonthlySchedule.Location = new System.Drawing.Point(27, 310);
			this.pbMonthlySchedule.Name = "pbMonthlySchedule";
			this.pbMonthlySchedule.Size = new System.Drawing.Size(595, 156);
			this.pbMonthlySchedule.TabIndex = 16;
			this.pbMonthlySchedule.TabStop = false;
			this.pbMonthlySchedule.Click += new System.EventHandler(this.pbMonthlySchedule_Click);
			this.pbMonthlySchedule.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
			this.pbMonthlySchedule.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseUp);
			// 
			// pbWeeklySchedule
			// 
			this.pbWeeklySchedule.Image = global::TVScheduleBuilder.Properties.Resources.WeeklyScheduleButton;
			this.pbWeeklySchedule.Location = new System.Drawing.Point(27, 56);
			this.pbWeeklySchedule.Name = "pbWeeklySchedule";
			this.pbWeeklySchedule.Size = new System.Drawing.Size(595, 156);
			this.pbWeeklySchedule.TabIndex = 15;
			this.pbWeeklySchedule.TabStop = false;
			this.pbWeeklySchedule.Click += new System.EventHandler(this.pbWeeklySchedule_Click);
			this.pbWeeklySchedule.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
			this.pbWeeklySchedule.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseUp);
			// 
			// defaultLookAndFeel
			// 
			this.defaultLookAndFeel.LookAndFeel.SkinName = "Money Twins";
			// 
			// xtraTabControlMain
			// 
			this.xtraTabControlMain.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControlMain.Appearance.Options.UseFont = true;
			this.xtraTabControlMain.AppearancePage.Header.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlMain.AppearancePage.Header.Options.UseFont = true;
			this.xtraTabControlMain.AppearancePage.HeaderActive.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControlMain.AppearancePage.HeaderActive.Options.UseFont = true;
			this.xtraTabControlMain.AppearancePage.HeaderDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlMain.AppearancePage.HeaderDisabled.Options.UseFont = true;
			this.xtraTabControlMain.AppearancePage.HeaderHotTracked.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlMain.AppearancePage.HeaderHotTracked.Options.UseFont = true;
			this.xtraTabControlMain.AppearancePage.PageClient.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlMain.AppearancePage.PageClient.Options.UseFont = true;
			this.xtraTabControlMain.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Style3D;
			this.xtraTabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.xtraTabControlMain.Location = new System.Drawing.Point(0, 56);
			this.xtraTabControlMain.Name = "xtraTabControlMain";
			this.xtraTabControlMain.SelectedTabPage = this.xtraTabPageTV;
			this.xtraTabControlMain.Size = new System.Drawing.Size(664, 610);
			this.xtraTabControlMain.TabIndex = 18;
			this.xtraTabControlMain.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPageTV});
			// 
			// xtraTabPageTV
			// 
			this.xtraTabPageTV.Appearance.PageClient.BackColor = System.Drawing.Color.DarkRed;
			this.xtraTabPageTV.Appearance.PageClient.Options.UseBackColor = true;
			this.xtraTabPageTV.Controls.Add(this.pnTV);
			this.xtraTabPageTV.Name = "xtraTabPageTV";
			this.xtraTabPageTV.Size = new System.Drawing.Size(658, 580);
			this.xtraTabPageTV.Text = "Television Strategy";
			// 
			// pnTV
			// 
			this.pnTV.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
			this.pnTV.Controls.Add(this.pbMonthlySchedule);
			this.pnTV.Controls.Add(this.pbWeeklySchedule);
			this.pnTV.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnTV.Location = new System.Drawing.Point(0, 0);
			this.pnTV.Name = "pnTV";
			this.pnTV.Size = new System.Drawing.Size(658, 580);
			this.pnTV.TabIndex = 0;
			// 
			// daypartsControl
			// 
			this.daypartsControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
			this.daypartsControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.daypartsControl.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.daypartsControl.HasChanged = false;
			this.daypartsControl.Location = new System.Drawing.Point(0, 0);
			this.daypartsControl.Name = "daypartsControl";
			this.daypartsControl.Padding = new System.Windows.Forms.Padding(3);
			this.daypartsControl.Size = new System.Drawing.Size(344, 580);
			this.daypartsControl.TabIndex = 17;
			// 
			// pnRightTop
			// 
			this.pnRightTop.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pnRightTop.Controls.Add(this.laRightTopTitle);
			this.pnRightTop.Controls.Add(this.pbOptionsHelp);
			this.pnRightTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnRightTop.Location = new System.Drawing.Point(0, 0);
			this.pnRightTop.Name = "pnRightTop";
			this.pnRightTop.Size = new System.Drawing.Size(350, 56);
			this.pnRightTop.TabIndex = 0;
			// 
			// laRightTopTitle
			// 
			this.laRightTopTitle.AutoSize = true;
			this.laRightTopTitle.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laRightTopTitle.ForeColor = System.Drawing.Color.White;
			this.laRightTopTitle.Location = new System.Drawing.Point(3, 13);
			this.laRightTopTitle.Name = "laRightTopTitle";
			this.laRightTopTitle.Size = new System.Drawing.Size(168, 27);
			this.laRightTopTitle.TabIndex = 27;
			this.laRightTopTitle.Text = "Schedule Info:";
			// 
			// pbOptionsHelp
			// 
			this.pbOptionsHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.pbOptionsHelp.Image = global::TVScheduleBuilder.Properties.Resources.Help;
			this.pbOptionsHelp.Location = new System.Drawing.Point(292, 2);
			this.pbOptionsHelp.Name = "pbOptionsHelp";
			this.pbOptionsHelp.Size = new System.Drawing.Size(49, 49);
			this.pbOptionsHelp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pbOptionsHelp.TabIndex = 26;
			this.pbOptionsHelp.TabStop = false;
			this.pbOptionsHelp.Click += new System.EventHandler(this.pbOptionsHelp_Click);
			this.pbOptionsHelp.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
			this.pbOptionsHelp.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseUp);
			// 
			// xtraTabControlOptions
			// 
			this.xtraTabControlOptions.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControlOptions.Appearance.Options.UseFont = true;
			this.xtraTabControlOptions.AppearancePage.Header.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlOptions.AppearancePage.Header.Options.UseFont = true;
			this.xtraTabControlOptions.AppearancePage.HeaderActive.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControlOptions.AppearancePage.HeaderActive.Options.UseFont = true;
			this.xtraTabControlOptions.AppearancePage.HeaderDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlOptions.AppearancePage.HeaderDisabled.Options.UseFont = true;
			this.xtraTabControlOptions.AppearancePage.HeaderHotTracked.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlOptions.AppearancePage.HeaderHotTracked.Options.UseFont = true;
			this.xtraTabControlOptions.AppearancePage.PageClient.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlOptions.AppearancePage.PageClient.Options.UseFont = true;
			this.xtraTabControlOptions.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Style3D;
			this.xtraTabControlOptions.Dock = System.Windows.Forms.DockStyle.Fill;
			this.xtraTabControlOptions.Location = new System.Drawing.Point(0, 56);
			this.xtraTabControlOptions.Name = "xtraTabControlOptions";
			this.xtraTabControlOptions.SelectedTabPage = this.xtraTabPageStations;
			this.xtraTabControlOptions.Size = new System.Drawing.Size(350, 610);
			this.xtraTabControlOptions.TabIndex = 1;
			this.xtraTabControlOptions.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPageStations,
            this.xtraTabPageDayparts,
            this.xtraTabPageDemos,
            this.xtraTabPageDigital});
			// 
			// xtraTabPageStations
			// 
			this.xtraTabPageStations.Controls.Add(this.stationsControl);
			this.xtraTabPageStations.Name = "xtraTabPageStations";
			this.xtraTabPageStations.Size = new System.Drawing.Size(344, 580);
			this.xtraTabPageStations.Text = "Stations";
			// 
			// xtraTabPageDayparts
			// 
			this.xtraTabPageDayparts.Controls.Add(this.daypartsControl);
			this.xtraTabPageDayparts.Name = "xtraTabPageDayparts";
			this.xtraTabPageDayparts.Size = new System.Drawing.Size(344, 580);
			this.xtraTabPageDayparts.Text = "Dayparts";
			// 
			// xtraTabPageDemos
			// 
			this.xtraTabPageDemos.Controls.Add(this.pnDemos);
			this.xtraTabPageDemos.Name = "xtraTabPageDemos";
			this.xtraTabPageDemos.Size = new System.Drawing.Size(344, 580);
			this.xtraTabPageDemos.Text = "Demos";
			// 
			// pnDemos
			// 
			this.pnDemos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
			this.pnDemos.Controls.Add(this.pnDemosSource);
			this.pnDemos.Controls.Add(this.pnDemosSelect);
			this.pnDemos.Controls.Add(this.pnDemosType);
			this.pnDemos.Controls.Add(this.pnDemosButtons);
			this.pnDemos.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnDemos.Location = new System.Drawing.Point(0, 0);
			this.pnDemos.Name = "pnDemos";
			this.pnDemos.Size = new System.Drawing.Size(344, 580);
			this.pnDemos.TabIndex = 0;
			// 
			// pnDemosSource
			// 
			this.pnDemosSource.Controls.Add(this.comboBoxEditSource);
			this.pnDemosSource.Controls.Add(this.buttonXDemosSourceDisable);
			this.pnDemosSource.Controls.Add(this.buttonXDemosSourceEnable);
			this.pnDemosSource.Controls.Add(this.laDemosSource);
			this.pnDemosSource.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnDemosSource.Location = new System.Drawing.Point(0, 298);
			this.pnDemosSource.Name = "pnDemosSource";
			this.pnDemosSource.Size = new System.Drawing.Size(344, 139);
			this.pnDemosSource.TabIndex = 3;
			// 
			// comboBoxEditSource
			// 
			this.comboBoxEditSource.Location = new System.Drawing.Point(13, 98);
			this.comboBoxEditSource.Name = "comboBoxEditSource";
			this.comboBoxEditSource.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.comboBoxEditSource.Properties.NullText = "Select Source";
			this.comboBoxEditSource.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
			this.comboBoxEditSource.Size = new System.Drawing.Size(209, 22);
			this.comboBoxEditSource.StyleController = this.styleController;
			this.comboBoxEditSource.TabIndex = 5;
			this.comboBoxEditSource.EditValueChanged += new System.EventHandler(this.comboBoxEditSource_EditValueChanged);
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
			// buttonXDemosSourceDisable
			// 
			this.buttonXDemosSourceDisable.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXDemosSourceDisable.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXDemosSourceDisable.Location = new System.Drawing.Point(13, 38);
			this.buttonXDemosSourceDisable.Name = "buttonXDemosSourceDisable";
			this.buttonXDemosSourceDisable.Size = new System.Drawing.Size(99, 45);
			this.buttonXDemosSourceDisable.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXDemosSourceDisable.TabIndex = 3;
			this.buttonXDemosSourceDisable.Text = "Disable\r\nSource";
			this.buttonXDemosSourceDisable.TextColor = System.Drawing.Color.Black;
			this.buttonXDemosSourceDisable.CheckedChanged += new System.EventHandler(this.buttonXDemosSource_CheckedChanged);
			this.buttonXDemosSourceDisable.Click += new System.EventHandler(this.buttonXDemosSource_Click);
			// 
			// buttonXDemosSourceEnable
			// 
			this.buttonXDemosSourceEnable.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXDemosSourceEnable.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXDemosSourceEnable.Location = new System.Drawing.Point(123, 38);
			this.buttonXDemosSourceEnable.Name = "buttonXDemosSourceEnable";
			this.buttonXDemosSourceEnable.Size = new System.Drawing.Size(99, 45);
			this.buttonXDemosSourceEnable.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXDemosSourceEnable.TabIndex = 4;
			this.buttonXDemosSourceEnable.Text = "Enable\r\nSource";
			this.buttonXDemosSourceEnable.TextColor = System.Drawing.Color.Black;
			this.buttonXDemosSourceEnable.CheckedChanged += new System.EventHandler(this.buttonXDemosSource_CheckedChanged);
			this.buttonXDemosSourceEnable.Click += new System.EventHandler(this.buttonXDemosSource_Click);
			// 
			// laDemosSource
			// 
			this.laDemosSource.AutoSize = true;
			this.laDemosSource.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laDemosSource.Location = new System.Drawing.Point(10, 12);
			this.laDemosSource.Name = "laDemosSource";
			this.laDemosSource.Size = new System.Drawing.Size(62, 18);
			this.laDemosSource.TabIndex = 0;
			this.laDemosSource.Text = "Source:";
			// 
			// pnDemosSelect
			// 
			this.pnDemosSelect.Controls.Add(this.comboBoxEditDemos);
			this.pnDemosSelect.Controls.Add(this.laDemosSelect);
			this.pnDemosSelect.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnDemosSelect.Location = new System.Drawing.Point(0, 200);
			this.pnDemosSelect.Name = "pnDemosSelect";
			this.pnDemosSelect.Size = new System.Drawing.Size(344, 98);
			this.pnDemosSelect.TabIndex = 2;
			// 
			// comboBoxEditDemos
			// 
			this.comboBoxEditDemos.Location = new System.Drawing.Point(13, 43);
			this.comboBoxEditDemos.Name = "comboBoxEditDemos";
			this.comboBoxEditDemos.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.comboBoxEditDemos.Properties.NullText = "Select Demo";
			this.comboBoxEditDemos.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
			this.comboBoxEditDemos.Size = new System.Drawing.Size(209, 22);
			this.comboBoxEditDemos.StyleController = this.styleController;
			this.comboBoxEditDemos.TabIndex = 1;
			this.comboBoxEditDemos.EditValueChanged += new System.EventHandler(this.comboBoxEditDemos_EditValueChanged);
			// 
			// laDemosSelect
			// 
			this.laDemosSelect.AutoSize = true;
			this.laDemosSelect.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laDemosSelect.Location = new System.Drawing.Point(10, 12);
			this.laDemosSelect.Name = "laDemosSelect";
			this.laDemosSelect.Size = new System.Drawing.Size(136, 18);
			this.laDemosSelect.TabIndex = 0;
			this.laDemosSelect.Text = "Select your Demo:";
			// 
			// pnDemosType
			// 
			this.pnDemosType.Controls.Add(this.buttonXDemosImps);
			this.pnDemosType.Controls.Add(this.buttonXDemosRtg);
			this.pnDemosType.Controls.Add(this.laDemosType);
			this.pnDemosType.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnDemosType.Location = new System.Drawing.Point(0, 81);
			this.pnDemosType.Name = "pnDemosType";
			this.pnDemosType.Size = new System.Drawing.Size(344, 119);
			this.pnDemosType.TabIndex = 1;
			// 
			// buttonXDemosImps
			// 
			this.buttonXDemosImps.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXDemosImps.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXDemosImps.Location = new System.Drawing.Point(123, 42);
			this.buttonXDemosImps.Name = "buttonXDemosImps";
			this.buttonXDemosImps.Size = new System.Drawing.Size(99, 45);
			this.buttonXDemosImps.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXDemosImps.TabIndex = 2;
			this.buttonXDemosImps.Text = "000s";
			this.buttonXDemosImps.TextColor = System.Drawing.Color.Black;
			this.buttonXDemosImps.CheckedChanged += new System.EventHandler(this.buttonXDemosType_CheckedChanged);
			this.buttonXDemosImps.Click += new System.EventHandler(this.buttonXDemosType_Click);
			// 
			// buttonXDemosRtg
			// 
			this.buttonXDemosRtg.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXDemosRtg.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXDemosRtg.Location = new System.Drawing.Point(13, 42);
			this.buttonXDemosRtg.Name = "buttonXDemosRtg";
			this.buttonXDemosRtg.Size = new System.Drawing.Size(99, 45);
			this.buttonXDemosRtg.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXDemosRtg.TabIndex = 1;
			this.buttonXDemosRtg.Text = "Ratings";
			this.buttonXDemosRtg.TextColor = System.Drawing.Color.Black;
			this.buttonXDemosRtg.CheckedChanged += new System.EventHandler(this.buttonXDemosType_CheckedChanged);
			this.buttonXDemosRtg.Click += new System.EventHandler(this.buttonXDemosType_Click);
			// 
			// laDemosType
			// 
			this.laDemosType.AutoSize = true;
			this.laDemosType.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laDemosType.Location = new System.Drawing.Point(10, 12);
			this.laDemosType.Name = "laDemosType";
			this.laDemosType.Size = new System.Drawing.Size(276, 18);
			this.laDemosType.TabIndex = 0;
			this.laDemosType.Text = "Build Schedule with Ratings or (000s)?";
			// 
			// pnDemosButtons
			// 
			this.pnDemosButtons.Controls.Add(this.buttonXDemosCustom);
			this.pnDemosButtons.Controls.Add(this.buttonXDemosImport);
			this.pnDemosButtons.Controls.Add(this.buttonXDemosNo);
			this.pnDemosButtons.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnDemosButtons.Location = new System.Drawing.Point(0, 0);
			this.pnDemosButtons.Name = "pnDemosButtons";
			this.pnDemosButtons.Size = new System.Drawing.Size(344, 81);
			this.pnDemosButtons.TabIndex = 0;
			// 
			// buttonXDemosCustom
			// 
			this.buttonXDemosCustom.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXDemosCustom.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXDemosCustom.Location = new System.Drawing.Point(123, 19);
			this.buttonXDemosCustom.Name = "buttonXDemosCustom";
			this.buttonXDemosCustom.Size = new System.Drawing.Size(99, 45);
			this.buttonXDemosCustom.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXDemosCustom.TabIndex = 1;
			this.buttonXDemosCustom.Text = "Custom\r\nEstimates";
			this.buttonXDemosCustom.TextColor = System.Drawing.Color.Black;
			this.buttonXDemosCustom.CheckedChanged += new System.EventHandler(this.buttonXDemos_CheckedChanged);
			this.buttonXDemosCustom.Click += new System.EventHandler(this.buttonXDemos_Click);
			// 
			// buttonXDemosImport
			// 
			this.buttonXDemosImport.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXDemosImport.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXDemosImport.Location = new System.Drawing.Point(233, 19);
			this.buttonXDemosImport.Name = "buttonXDemosImport";
			this.buttonXDemosImport.Size = new System.Drawing.Size(99, 45);
			this.buttonXDemosImport.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXDemosImport.TabIndex = 2;
			this.buttonXDemosImport.Text = "Import\r\nEstimates";
			this.buttonXDemosImport.TextColor = System.Drawing.Color.Black;
			this.buttonXDemosImport.CheckedChanged += new System.EventHandler(this.buttonXDemos_CheckedChanged);
			this.buttonXDemosImport.Click += new System.EventHandler(this.buttonXDemos_Click);
			// 
			// buttonXDemosNo
			// 
			this.buttonXDemosNo.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXDemosNo.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXDemosNo.Location = new System.Drawing.Point(13, 19);
			this.buttonXDemosNo.Name = "buttonXDemosNo";
			this.buttonXDemosNo.Size = new System.Drawing.Size(99, 45);
			this.buttonXDemosNo.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXDemosNo.TabIndex = 0;
			this.buttonXDemosNo.Text = "No\r\nDemos";
			this.buttonXDemosNo.TextColor = System.Drawing.Color.Black;
			this.buttonXDemosNo.CheckedChanged += new System.EventHandler(this.buttonXDemos_CheckedChanged);
			this.buttonXDemosNo.Click += new System.EventHandler(this.buttonXDemos_Click);
			// 
			// splitContainerControl
			// 
			this.splitContainerControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainerControl.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel2;
			this.splitContainerControl.Location = new System.Drawing.Point(0, 0);
			this.splitContainerControl.Name = "splitContainerControl";
			this.splitContainerControl.Panel1.Controls.Add(this.xtraTabControlMain);
			this.splitContainerControl.Panel1.Controls.Add(this.pnTop);
			this.splitContainerControl.Panel1.Text = "Panel1";
			this.splitContainerControl.Panel2.Controls.Add(this.xtraTabControlOptions);
			this.splitContainerControl.Panel2.Controls.Add(this.pnRightTop);
			this.splitContainerControl.Panel2.MinSize = 350;
			this.splitContainerControl.Panel2.Text = "Panel2";
			this.splitContainerControl.Size = new System.Drawing.Size(1020, 666);
			this.splitContainerControl.TabIndex = 19;
			this.splitContainerControl.Text = "splitContainerControl1";
			// 
			// pnTop
			// 
			this.pnTop.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pnTop.Controls.Add(this.laTopTitle);
			this.pnTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnTop.Location = new System.Drawing.Point(0, 0);
			this.pnTop.Name = "pnTop";
			this.pnTop.Size = new System.Drawing.Size(664, 56);
			this.pnTop.TabIndex = 19;
			// 
			// laTopTitle
			// 
			this.laTopTitle.AutoSize = true;
			this.laTopTitle.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laTopTitle.ForeColor = System.Drawing.Color.White;
			this.laTopTitle.Location = new System.Drawing.Point(3, 13);
			this.laTopTitle.Name = "laTopTitle";
			this.laTopTitle.Size = new System.Drawing.Size(168, 27);
			this.laTopTitle.TabIndex = 27;
			this.laTopTitle.Text = "Schedule Info:";
			this.laTopTitle.UseMnemonic = false;
			// 
			// xtraTabPageDigital
			// 
			this.xtraTabPageDigital.Controls.Add(this.pnOptionsDigital);
			this.xtraTabPageDigital.Name = "xtraTabPageDigital";
			this.xtraTabPageDigital.Size = new System.Drawing.Size(344, 580);
			this.xtraTabPageDigital.Text = "Digital";
			// 
			// pnOptionsDigital
			// 
			this.pnOptionsDigital.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
			this.pnOptionsDigital.Controls.Add(this.checkBox1);
			this.pnOptionsDigital.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnOptionsDigital.Location = new System.Drawing.Point(0, 0);
			this.pnOptionsDigital.Name = "pnOptionsDigital";
			this.pnOptionsDigital.Size = new System.Drawing.Size(344, 580);
			this.pnOptionsDigital.TabIndex = 0;
			// 
			// checkBox1
			// 
			this.checkBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.checkBox1.AutoSize = true;
			this.checkBox1.Location = new System.Drawing.Point(224, 544);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(98, 20);
			this.checkBox1.TabIndex = 1;
			this.checkBox1.Text = "test Activate";
			this.checkBox1.UseVisualStyleBackColor = true;
			// 
			// HomeControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
			this.Controls.Add(this.splitContainerControl);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "HomeControl";
			this.Size = new System.Drawing.Size(1020, 666);
			((System.ComponentModel.ISupportInitialize)(this.pbMonthlySchedule)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbWeeklySchedule)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControlMain)).EndInit();
			this.xtraTabControlMain.ResumeLayout(false);
			this.xtraTabPageTV.ResumeLayout(false);
			this.pnTV.ResumeLayout(false);
			this.pnRightTop.ResumeLayout(false);
			this.pnRightTop.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbOptionsHelp)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControlOptions)).EndInit();
			this.xtraTabControlOptions.ResumeLayout(false);
			this.xtraTabPageStations.ResumeLayout(false);
			this.xtraTabPageDayparts.ResumeLayout(false);
			this.xtraTabPageDemos.ResumeLayout(false);
			this.pnDemos.ResumeLayout(false);
			this.pnDemosSource.ResumeLayout(false);
			this.pnDemosSource.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditSource.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			this.pnDemosSelect.ResumeLayout(false);
			this.pnDemosSelect.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditDemos.Properties)).EndInit();
			this.pnDemosType.ResumeLayout(false);
			this.pnDemosType.PerformLayout();
			this.pnDemosButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainerControl)).EndInit();
			this.splitContainerControl.ResumeLayout(false);
			this.pnTop.ResumeLayout(false);
			this.pnTop.PerformLayout();
			this.xtraTabPageDigital.ResumeLayout(false);
			this.pnOptionsDigital.ResumeLayout(false);
			this.pnOptionsDigital.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbWeeklySchedule;
        private System.Windows.Forms.PictureBox pbMonthlySchedule;
        private StationsControl stationsControl;
        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
        private DevExpress.XtraTab.XtraTabControl xtraTabControlMain;
		private DevExpress.XtraTab.XtraTabPage xtraTabPageTV;
		private System.Windows.Forms.Panel pnTV;
        private DaypartsControl daypartsControl;
        private System.Windows.Forms.Panel pnRightTop;
        private DevExpress.XtraTab.XtraTabControl xtraTabControlOptions;
        private DevExpress.XtraTab.XtraTabPage xtraTabPageStations;
        private DevExpress.XtraTab.XtraTabPage xtraTabPageDayparts;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl;
        private System.Windows.Forms.PictureBox pbOptionsHelp;
        private System.Windows.Forms.Label laRightTopTitle;
        private DevExpress.XtraTab.XtraTabPage xtraTabPageDemos;
        private System.Windows.Forms.Panel pnDemos;
        private System.Windows.Forms.Panel pnDemosSelect;
        private System.Windows.Forms.Label laDemosSelect;
        private System.Windows.Forms.Panel pnDemosType;
        private DevComponents.DotNetBar.ButtonX buttonXDemosImps;
        private DevComponents.DotNetBar.ButtonX buttonXDemosRtg;
        private System.Windows.Forms.Label laDemosType;
        private System.Windows.Forms.Panel pnDemosButtons;
        private DevComponents.DotNetBar.ButtonX buttonXDemosImport;
        private DevComponents.DotNetBar.ButtonX buttonXDemosCustom;
        private DevComponents.DotNetBar.ButtonX buttonXDemosNo;
        private DevExpress.XtraEditors.StyleController styleController;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEditDemos;
        private System.Windows.Forms.Panel pnDemosSource;
        private System.Windows.Forms.Label laDemosSource;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEditSource;
        private DevComponents.DotNetBar.ButtonX buttonXDemosSourceEnable;
        private DevComponents.DotNetBar.ButtonX buttonXDemosSourceDisable;
        private System.Windows.Forms.Panel pnTop;
        private System.Windows.Forms.Label laTopTitle;
		private DevExpress.XtraTab.XtraTabPage xtraTabPageDigital;
		private System.Windows.Forms.Panel pnOptionsDigital;
		private System.Windows.Forms.CheckBox checkBox1;

    }
}
