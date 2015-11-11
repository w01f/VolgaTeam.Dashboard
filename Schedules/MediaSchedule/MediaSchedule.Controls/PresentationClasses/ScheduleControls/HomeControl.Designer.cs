using System.Windows.Forms;

namespace Asa.MediaSchedule.Controls.PresentationClasses.ScheduleControls
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HomeControl));
			this.stationsControl = new Asa.MediaSchedule.Controls.PresentationClasses.ScheduleControls.StationsControl();
			this.xtraTabControlMain = new DevExpress.XtraTab.XtraTabControl();
			this.xtraTabPageMedia = new DevExpress.XtraTab.XtraTabPage();
			this.pnMedia = new System.Windows.Forms.Panel();
			this.pbMediaLogo = new System.Windows.Forms.PictureBox();
			this.laFlexDateWarning = new System.Windows.Forms.Label();
			this.laMediaDescription = new System.Windows.Forms.Label();
			this.laMediaTitle = new System.Windows.Forms.Label();
			this.buttonXMonthlySchedule = new DevComponents.DotNetBar.ButtonX();
			this.buttonXWeeklySchedule = new DevComponents.DotNetBar.ButtonX();
			this.pbMediaDefault = new System.Windows.Forms.PictureBox();
			this.xtraTabPageDigital = new DevExpress.XtraTab.XtraTabPage();
			this.digitalProductListControl = new Asa.OnlineSchedule.Controls.PresentationClasses.DigitalProductListControl();
			this.daypartsControl = new Asa.MediaSchedule.Controls.PresentationClasses.ScheduleControls.DaypartsControl();
			this.xtraTabControlOptions = new DevExpress.XtraTab.XtraTabControl();
			this.xtraTabPageStations = new DevExpress.XtraTab.XtraTabPage();
			this.xtraTabPageDayparts = new DevExpress.XtraTab.XtraTabPage();
			this.xtraTabPageDemos = new DevExpress.XtraTab.XtraTabPage();
			this.pnDemos = new System.Windows.Forms.Panel();
			this.pnDemosInfo = new System.Windows.Forms.Panel();
			this.labelControlDemosInfo = new DevExpress.XtraEditors.LabelControl();
			this.pnSelectDemo = new System.Windows.Forms.Panel();
			this.comboBoxEditDemos = new DevExpress.XtraEditors.ComboBoxEdit();
			this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
			this.pnSelectSource = new System.Windows.Forms.Panel();
			this.comboBoxEditSource = new DevExpress.XtraEditors.ComboBoxEdit();
			this.pnDemosType = new System.Windows.Forms.Panel();
			this.buttonXDemosRtg = new DevComponents.DotNetBar.ButtonX();
			this.buttonXDemosImps = new DevComponents.DotNetBar.ButtonX();
			this.pnDemosImport = new System.Windows.Forms.Panel();
			this.buttonXDemosImport = new DevComponents.DotNetBar.ButtonX();
			this.pnDemosCustom = new System.Windows.Forms.Panel();
			this.buttonXDemosCustom = new DevComponents.DotNetBar.ButtonX();
			this.pnUseDemos = new System.Windows.Forms.Panel();
			this.labelControlDemoTitle = new DevExpress.XtraEditors.LabelControl();
			this.buttonXUseDemos = new DevComponents.DotNetBar.ButtonX();
			this.xtraTabPageCalendarType = new DevExpress.XtraTab.XtraTabPage();
			this.buttonXCalendarTypeSundayBased = new DevComponents.DotNetBar.ButtonX();
			this.buttonXCalendarTypeMondayBased = new DevComponents.DotNetBar.ButtonX();
			this.splitContainerControl = new DevExpress.XtraEditors.SplitContainerControl();
			this.superTooltip = new DevComponents.DotNetBar.SuperTooltip();
			this.pnMediaDefault = new System.Windows.Forms.Panel();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControlMain)).BeginInit();
			this.xtraTabControlMain.SuspendLayout();
			this.xtraTabPageMedia.SuspendLayout();
			this.pnMedia.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbMediaLogo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbMediaDefault)).BeginInit();
			this.xtraTabPageDigital.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControlOptions)).BeginInit();
			this.xtraTabControlOptions.SuspendLayout();
			this.xtraTabPageStations.SuspendLayout();
			this.xtraTabPageDayparts.SuspendLayout();
			this.xtraTabPageDemos.SuspendLayout();
			this.pnDemos.SuspendLayout();
			this.pnDemosInfo.SuspendLayout();
			this.pnSelectDemo.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditDemos.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			this.pnSelectSource.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditSource.Properties)).BeginInit();
			this.pnDemosType.SuspendLayout();
			this.pnDemosImport.SuspendLayout();
			this.pnDemosCustom.SuspendLayout();
			this.pnUseDemos.SuspendLayout();
			this.xtraTabPageCalendarType.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainerControl)).BeginInit();
			this.splitContainerControl.SuspendLayout();
			this.pnMediaDefault.SuspendLayout();
			this.SuspendLayout();
			// 
			// stationsControl
			// 
			this.stationsControl.BackColor = System.Drawing.Color.White;
			this.stationsControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.stationsControl.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.stationsControl.HasChanged = false;
			this.stationsControl.Location = new System.Drawing.Point(0, 0);
			this.stationsControl.Name = "stationsControl";
			this.stationsControl.Size = new System.Drawing.Size(344, 569);
			this.stationsControl.TabIndex = 0;
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
			this.xtraTabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.xtraTabControlMain.Location = new System.Drawing.Point(0, 0);
			this.xtraTabControlMain.Name = "xtraTabControlMain";
			this.xtraTabControlMain.SelectedTabPage = this.xtraTabPageMedia;
			this.xtraTabControlMain.Size = new System.Drawing.Size(665, 600);
			this.xtraTabControlMain.TabIndex = 18;
			this.xtraTabControlMain.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPageMedia,
            this.xtraTabPageDigital});
			this.xtraTabControlMain.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.xtraTabControlProducts_SelectedPageChanged);
			// 
			// xtraTabPageMedia
			// 
			this.xtraTabPageMedia.Appearance.PageClient.BackColor = System.Drawing.Color.DarkRed;
			this.xtraTabPageMedia.Appearance.PageClient.Options.UseBackColor = true;
			this.xtraTabPageMedia.Controls.Add(this.pnMediaDefault);
			this.xtraTabPageMedia.Controls.Add(this.pnMedia);
			this.xtraTabPageMedia.Name = "xtraTabPageMedia";
			this.xtraTabPageMedia.Padding = new System.Windows.Forms.Padding(10);
			this.xtraTabPageMedia.Size = new System.Drawing.Size(659, 569);
			this.xtraTabPageMedia.Text = "Television Strategy";
			// 
			// pnMedia
			// 
			this.pnMedia.BackColor = System.Drawing.Color.Transparent;
			this.pnMedia.Controls.Add(this.pbMediaLogo);
			this.pnMedia.Controls.Add(this.laFlexDateWarning);
			this.pnMedia.Controls.Add(this.laMediaDescription);
			this.pnMedia.Controls.Add(this.laMediaTitle);
			this.pnMedia.Controls.Add(this.buttonXMonthlySchedule);
			this.pnMedia.Controls.Add(this.buttonXWeeklySchedule);
			this.pnMedia.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnMedia.Location = new System.Drawing.Point(10, 10);
			this.pnMedia.Name = "pnMedia";
			this.pnMedia.Size = new System.Drawing.Size(639, 549);
			this.pnMedia.TabIndex = 0;
			// 
			// pbMediaLogo
			// 
			this.pbMediaLogo.Image = ((System.Drawing.Image)(resources.GetObject("pbMediaLogo.Image")));
			this.pbMediaLogo.Location = new System.Drawing.Point(21, 13);
			this.pbMediaLogo.Name = "pbMediaLogo";
			this.pbMediaLogo.Size = new System.Drawing.Size(468, 115);
			this.pbMediaLogo.TabIndex = 22;
			this.pbMediaLogo.TabStop = false;
			// 
			// laFlexDateWarning
			// 
			this.laFlexDateWarning.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.laFlexDateWarning.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laFlexDateWarning.ForeColor = System.Drawing.Color.Red;
			this.laFlexDateWarning.Location = new System.Drawing.Point(18, 372);
			this.laFlexDateWarning.Name = "laFlexDateWarning";
			this.laFlexDateWarning.Size = new System.Drawing.Size(606, 108);
			this.laFlexDateWarning.TabIndex = 21;
			this.laFlexDateWarning.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// laMediaDescription
			// 
			this.laMediaDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.laMediaDescription.Location = new System.Drawing.Point(18, 495);
			this.laMediaDescription.Name = "laMediaDescription";
			this.laMediaDescription.Size = new System.Drawing.Size(606, 44);
			this.laMediaDescription.TabIndex = 20;
			this.laMediaDescription.Text = "*Build a Monthly Schedule if you want the most flexibility to deal with preemptio" +
    "ns and makegoods\r\n*The Ad Calendar will only work if you build a Weekly Schedule" +
    " or a Snapshot Schedule…";
			// 
			// laMediaTitle
			// 
			this.laMediaTitle.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laMediaTitle.Location = new System.Drawing.Point(21, 174);
			this.laMediaTitle.Name = "laMediaTitle";
			this.laMediaTitle.Size = new System.Drawing.Size(623, 38);
			this.laMediaTitle.TabIndex = 19;
			this.laMediaTitle.Text = "What kind of schedule do you want to build?";
			// 
			// buttonXMonthlySchedule
			// 
			this.buttonXMonthlySchedule.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXMonthlySchedule.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXMonthlySchedule.Image = ((System.Drawing.Image)(resources.GetObject("buttonXMonthlySchedule.Image")));
			this.buttonXMonthlySchedule.Location = new System.Drawing.Point(288, 226);
			this.buttonXMonthlySchedule.Name = "buttonXMonthlySchedule";
			this.buttonXMonthlySchedule.Size = new System.Drawing.Size(201, 89);
			this.buttonXMonthlySchedule.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.superTooltip.SetSuperTooltip(this.buttonXMonthlySchedule, new DevComponents.DotNetBar.SuperTooltipInfo("", "", "Build a Monthly Schedule", null, null, DevComponents.DotNetBar.eTooltipColor.Gray));
			this.buttonXMonthlySchedule.TabIndex = 18;
			this.buttonXMonthlySchedule.TextColor = System.Drawing.Color.Black;
			this.buttonXMonthlySchedule.CheckedChanged += new System.EventHandler(this.buttonXScheduleType_CheckedChanged);
			this.buttonXMonthlySchedule.Click += new System.EventHandler(this.buttonXScheduleType_Click);
			// 
			// buttonXWeeklySchedule
			// 
			this.buttonXWeeklySchedule.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXWeeklySchedule.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXWeeklySchedule.Image = ((System.Drawing.Image)(resources.GetObject("buttonXWeeklySchedule.Image")));
			this.buttonXWeeklySchedule.Location = new System.Drawing.Point(21, 226);
			this.buttonXWeeklySchedule.Name = "buttonXWeeklySchedule";
			this.buttonXWeeklySchedule.Size = new System.Drawing.Size(201, 89);
			this.buttonXWeeklySchedule.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.superTooltip.SetSuperTooltip(this.buttonXWeeklySchedule, new DevComponents.DotNetBar.SuperTooltipInfo("", "", "Build a Weekly Schedule", null, null, DevComponents.DotNetBar.eTooltipColor.Gray));
			this.buttonXWeeklySchedule.TabIndex = 17;
			this.buttonXWeeklySchedule.TextColor = System.Drawing.Color.Black;
			this.buttonXWeeklySchedule.CheckedChanged += new System.EventHandler(this.buttonXScheduleType_CheckedChanged);
			this.buttonXWeeklySchedule.Click += new System.EventHandler(this.buttonXScheduleType_Click);
			// 
			// pbMediaDefault
			// 
			this.pbMediaDefault.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pbMediaDefault.Image = ((System.Drawing.Image)(resources.GetObject("pbMediaDefault.Image")));
			this.pbMediaDefault.Location = new System.Drawing.Point(0, 0);
			this.pbMediaDefault.Name = "pbMediaDefault";
			this.pbMediaDefault.Size = new System.Drawing.Size(639, 549);
			this.pbMediaDefault.TabIndex = 23;
			this.pbMediaDefault.TabStop = false;
			// 
			// xtraTabPageDigital
			// 
			this.xtraTabPageDigital.Controls.Add(this.digitalProductListControl);
			this.xtraTabPageDigital.Name = "xtraTabPageDigital";
			this.xtraTabPageDigital.Size = new System.Drawing.Size(659, 569);
			this.xtraTabPageDigital.Text = "Digital Strategy";
			// 
			// digitalProductListControl
			// 
			this.digitalProductListControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
			this.digitalProductListControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.digitalProductListControl.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.digitalProductListControl.Location = new System.Drawing.Point(0, 0);
			this.digitalProductListControl.Logo = null;
			this.digitalProductListControl.Name = "digitalProductListControl";
			this.digitalProductListControl.Size = new System.Drawing.Size(659, 569);
			this.digitalProductListControl.TabIndex = 0;
			// 
			// daypartsControl
			// 
			this.daypartsControl.BackColor = System.Drawing.Color.White;
			this.daypartsControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.daypartsControl.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.daypartsControl.HasChanged = false;
			this.daypartsControl.Location = new System.Drawing.Point(0, 0);
			this.daypartsControl.Name = "daypartsControl";
			this.daypartsControl.Size = new System.Drawing.Size(344, 569);
			this.daypartsControl.TabIndex = 17;
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
			this.xtraTabControlOptions.Dock = System.Windows.Forms.DockStyle.Fill;
			this.xtraTabControlOptions.Location = new System.Drawing.Point(0, 0);
			this.xtraTabControlOptions.Name = "xtraTabControlOptions";
			this.xtraTabControlOptions.SelectedTabPage = this.xtraTabPageStations;
			this.xtraTabControlOptions.Size = new System.Drawing.Size(350, 600);
			this.xtraTabControlOptions.TabIndex = 1;
			this.xtraTabControlOptions.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPageStations,
            this.xtraTabPageDayparts,
            this.xtraTabPageDemos,
            this.xtraTabPageCalendarType});
			// 
			// xtraTabPageStations
			// 
			this.xtraTabPageStations.Controls.Add(this.stationsControl);
			this.xtraTabPageStations.Name = "xtraTabPageStations";
			this.xtraTabPageStations.Size = new System.Drawing.Size(344, 569);
			this.xtraTabPageStations.Text = "Stations";
			// 
			// xtraTabPageDayparts
			// 
			this.xtraTabPageDayparts.Controls.Add(this.daypartsControl);
			this.xtraTabPageDayparts.Name = "xtraTabPageDayparts";
			this.xtraTabPageDayparts.Size = new System.Drawing.Size(344, 569);
			this.xtraTabPageDayparts.Text = "Dayparts";
			// 
			// xtraTabPageDemos
			// 
			this.xtraTabPageDemos.Controls.Add(this.pnDemos);
			this.xtraTabPageDemos.Name = "xtraTabPageDemos";
			this.xtraTabPageDemos.Size = new System.Drawing.Size(344, 569);
			this.xtraTabPageDemos.Text = "Demos";
			// 
			// pnDemos
			// 
			this.pnDemos.BackColor = System.Drawing.Color.Transparent;
			this.pnDemos.Controls.Add(this.pnDemosInfo);
			this.pnDemos.Controls.Add(this.pnSelectDemo);
			this.pnDemos.Controls.Add(this.pnSelectSource);
			this.pnDemos.Controls.Add(this.pnDemosType);
			this.pnDemos.Controls.Add(this.pnDemosImport);
			this.pnDemos.Controls.Add(this.pnDemosCustom);
			this.pnDemos.Controls.Add(this.pnUseDemos);
			this.pnDemos.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnDemos.Location = new System.Drawing.Point(0, 0);
			this.pnDemos.Name = "pnDemos";
			this.pnDemos.Size = new System.Drawing.Size(344, 569);
			this.pnDemos.TabIndex = 0;
			// 
			// pnDemosInfo
			// 
			this.pnDemosInfo.Controls.Add(this.labelControlDemosInfo);
			this.pnDemosInfo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnDemosInfo.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.pnDemosInfo.Location = new System.Drawing.Point(0, 336);
			this.pnDemosInfo.Name = "pnDemosInfo";
			this.pnDemosInfo.Size = new System.Drawing.Size(344, 233);
			this.pnDemosInfo.TabIndex = 12;
			// 
			// labelControlDemosInfo
			// 
			this.labelControlDemosInfo.AllowHtmlString = true;
			this.labelControlDemosInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelControlDemosInfo.Appearance.Font = new System.Drawing.Font("Arial", 11.25F);
			this.labelControlDemosInfo.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
			this.labelControlDemosInfo.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.labelControlDemosInfo.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.labelControlDemosInfo.Location = new System.Drawing.Point(13, 12);
			this.labelControlDemosInfo.Name = "labelControlDemosInfo";
			this.labelControlDemosInfo.Size = new System.Drawing.Size(319, 211);
			this.labelControlDemosInfo.TabIndex = 1;
			this.labelControlDemosInfo.Text = resources.GetString("labelControlDemosInfo.Text");
			// 
			// pnSelectDemo
			// 
			this.pnSelectDemo.Controls.Add(this.comboBoxEditDemos);
			this.pnSelectDemo.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnSelectDemo.Location = new System.Drawing.Point(0, 301);
			this.pnSelectDemo.Name = "pnSelectDemo";
			this.pnSelectDemo.Size = new System.Drawing.Size(344, 35);
			this.pnSelectDemo.TabIndex = 11;
			// 
			// comboBoxEditDemos
			// 
			this.comboBoxEditDemos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxEditDemos.Enabled = false;
			this.comboBoxEditDemos.Location = new System.Drawing.Point(13, 6);
			this.comboBoxEditDemos.Name = "comboBoxEditDemos";
			this.comboBoxEditDemos.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.comboBoxEditDemos.Properties.NullText = "Select Demo";
			this.comboBoxEditDemos.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
			this.comboBoxEditDemos.Size = new System.Drawing.Size(319, 22);
			this.comboBoxEditDemos.StyleController = this.styleController;
			this.comboBoxEditDemos.TabIndex = 1;
			this.comboBoxEditDemos.EditValueChanged += new System.EventHandler(this.comboBoxEditDemos_EditValueChanged);
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
			// pnSelectSource
			// 
			this.pnSelectSource.Controls.Add(this.comboBoxEditSource);
			this.pnSelectSource.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnSelectSource.Location = new System.Drawing.Point(0, 266);
			this.pnSelectSource.Name = "pnSelectSource";
			this.pnSelectSource.Size = new System.Drawing.Size(344, 35);
			this.pnSelectSource.TabIndex = 10;
			// 
			// comboBoxEditSource
			// 
			this.comboBoxEditSource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxEditSource.Enabled = false;
			this.comboBoxEditSource.Location = new System.Drawing.Point(13, 6);
			this.comboBoxEditSource.Name = "comboBoxEditSource";
			this.comboBoxEditSource.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.comboBoxEditSource.Properties.NullText = "Select Source";
			this.comboBoxEditSource.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
			this.comboBoxEditSource.Size = new System.Drawing.Size(319, 22);
			this.comboBoxEditSource.StyleController = this.styleController;
			this.comboBoxEditSource.TabIndex = 5;
			this.comboBoxEditSource.EditValueChanged += new System.EventHandler(this.comboBoxEditSource_EditValueChanged);
			// 
			// pnDemosType
			// 
			this.pnDemosType.Controls.Add(this.buttonXDemosRtg);
			this.pnDemosType.Controls.Add(this.buttonXDemosImps);
			this.pnDemosType.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnDemosType.Location = new System.Drawing.Point(0, 205);
			this.pnDemosType.Name = "pnDemosType";
			this.pnDemosType.Size = new System.Drawing.Size(344, 61);
			this.pnDemosType.TabIndex = 9;
			// 
			// buttonXDemosRtg
			// 
			this.buttonXDemosRtg.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXDemosRtg.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXDemosRtg.Enabled = false;
			this.buttonXDemosRtg.Location = new System.Drawing.Point(13, 13);
			this.buttonXDemosRtg.Name = "buttonXDemosRtg";
			this.buttonXDemosRtg.Size = new System.Drawing.Size(143, 35);
			this.buttonXDemosRtg.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.superTooltip.SetSuperTooltip(this.buttonXDemosRtg, new DevComponents.DotNetBar.SuperTooltipInfo("", "", "Show Ratings and CPPs", null, null, DevComponents.DotNetBar.eTooltipColor.Gray));
			this.buttonXDemosRtg.TabIndex = 1;
			this.buttonXDemosRtg.Text = "Ratings";
			this.buttonXDemosRtg.TextColor = System.Drawing.Color.Black;
			this.buttonXDemosRtg.CheckedChanged += new System.EventHandler(this.buttonXDemosType_CheckedChanged);
			this.buttonXDemosRtg.Click += new System.EventHandler(this.buttonXDemosType_Click);
			// 
			// buttonXDemosImps
			// 
			this.buttonXDemosImps.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXDemosImps.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXDemosImps.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXDemosImps.Enabled = false;
			this.buttonXDemosImps.Location = new System.Drawing.Point(189, 13);
			this.buttonXDemosImps.Name = "buttonXDemosImps";
			this.buttonXDemosImps.Size = new System.Drawing.Size(143, 35);
			this.buttonXDemosImps.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.superTooltip.SetSuperTooltip(this.buttonXDemosImps, new DevComponents.DotNetBar.SuperTooltipInfo("", "", "Show Impressions and CPMs", null, null, DevComponents.DotNetBar.eTooltipColor.Gray));
			this.buttonXDemosImps.TabIndex = 2;
			this.buttonXDemosImps.Text = "000s";
			this.buttonXDemosImps.TextColor = System.Drawing.Color.Black;
			this.buttonXDemosImps.CheckedChanged += new System.EventHandler(this.buttonXDemosType_CheckedChanged);
			this.buttonXDemosImps.Click += new System.EventHandler(this.buttonXDemosType_Click);
			// 
			// pnDemosImport
			// 
			this.pnDemosImport.Controls.Add(this.buttonXDemosImport);
			this.pnDemosImport.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnDemosImport.Location = new System.Drawing.Point(0, 146);
			this.pnDemosImport.Name = "pnDemosImport";
			this.pnDemosImport.Size = new System.Drawing.Size(344, 59);
			this.pnDemosImport.TabIndex = 8;
			// 
			// buttonXDemosImport
			// 
			this.buttonXDemosImport.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXDemosImport.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXDemosImport.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXDemosImport.Enabled = false;
			this.buttonXDemosImport.Location = new System.Drawing.Point(13, 13);
			this.buttonXDemosImport.Name = "buttonXDemosImport";
			this.buttonXDemosImport.Size = new System.Drawing.Size(319, 35);
			this.buttonXDemosImport.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXDemosImport.TabIndex = 2;
			this.buttonXDemosImport.Text = "Use Imported Estimates?";
			this.buttonXDemosImport.TextColor = System.Drawing.Color.Black;
			this.buttonXDemosImport.CheckedChanged += new System.EventHandler(this.buttonXDemos_CheckedChanged);
			this.buttonXDemosImport.Click += new System.EventHandler(this.buttonXDemos_Click);
			// 
			// pnDemosCustom
			// 
			this.pnDemosCustom.Controls.Add(this.buttonXDemosCustom);
			this.pnDemosCustom.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnDemosCustom.Location = new System.Drawing.Point(0, 87);
			this.pnDemosCustom.Name = "pnDemosCustom";
			this.pnDemosCustom.Size = new System.Drawing.Size(344, 59);
			this.pnDemosCustom.TabIndex = 7;
			// 
			// buttonXDemosCustom
			// 
			this.buttonXDemosCustom.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXDemosCustom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXDemosCustom.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXDemosCustom.Enabled = false;
			this.buttonXDemosCustom.Location = new System.Drawing.Point(13, 13);
			this.buttonXDemosCustom.Name = "buttonXDemosCustom";
			this.buttonXDemosCustom.Size = new System.Drawing.Size(319, 35);
			this.buttonXDemosCustom.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXDemosCustom.TabIndex = 1;
			this.buttonXDemosCustom.Text = "Use your Own Estimates?";
			this.buttonXDemosCustom.TextColor = System.Drawing.Color.Black;
			this.buttonXDemosCustom.CheckedChanged += new System.EventHandler(this.buttonXDemos_CheckedChanged);
			this.buttonXDemosCustom.Click += new System.EventHandler(this.buttonXDemos_Click);
			// 
			// pnUseDemos
			// 
			this.pnUseDemos.Controls.Add(this.labelControlDemoTitle);
			this.pnUseDemos.Controls.Add(this.buttonXUseDemos);
			this.pnUseDemos.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnUseDemos.Location = new System.Drawing.Point(0, 0);
			this.pnUseDemos.Name = "pnUseDemos";
			this.pnUseDemos.Size = new System.Drawing.Size(344, 87);
			this.pnUseDemos.TabIndex = 6;
			// 
			// labelControlDemoTitle
			// 
			this.labelControlDemoTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelControlDemoTitle.Appearance.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.labelControlDemoTitle.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.labelControlDemoTitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.labelControlDemoTitle.Location = new System.Drawing.Point(13, 3);
			this.labelControlDemoTitle.Name = "labelControlDemoTitle";
			this.labelControlDemoTitle.Size = new System.Drawing.Size(319, 38);
			this.labelControlDemoTitle.TabIndex = 1;
			this.labelControlDemoTitle.Text = "Are you working with an agency?";
			// 
			// buttonXUseDemos
			// 
			this.buttonXUseDemos.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXUseDemos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXUseDemos.AutoCheckOnClick = true;
			this.buttonXUseDemos.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXUseDemos.Location = new System.Drawing.Point(13, 47);
			this.buttonXUseDemos.Name = "buttonXUseDemos";
			this.buttonXUseDemos.Size = new System.Drawing.Size(319, 35);
			this.buttonXUseDemos.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.superTooltip.SetSuperTooltip(this.buttonXUseDemos, new DevComponents.DotNetBar.SuperTooltipInfo("", "", "Show Ratings or Impressions", null, null, DevComponents.DotNetBar.eTooltipColor.Gray));
			this.buttonXUseDemos.TabIndex = 0;
			this.buttonXUseDemos.Text = "Show Demo Estimates";
			this.buttonXUseDemos.TextColor = System.Drawing.Color.Black;
			this.buttonXUseDemos.CheckedChanged += new System.EventHandler(this.buttonXDemosNo_CheckedChanged);
			// 
			// xtraTabPageCalendarType
			// 
			this.xtraTabPageCalendarType.Controls.Add(this.buttonXCalendarTypeSundayBased);
			this.xtraTabPageCalendarType.Controls.Add(this.buttonXCalendarTypeMondayBased);
			this.xtraTabPageCalendarType.Name = "xtraTabPageCalendarType";
			this.xtraTabPageCalendarType.PageVisible = false;
			this.xtraTabPageCalendarType.Size = new System.Drawing.Size(344, 569);
			this.xtraTabPageCalendarType.Text = "Calendar";
			// 
			// buttonXCalendarTypeSundayBased
			// 
			this.buttonXCalendarTypeSundayBased.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXCalendarTypeSundayBased.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXCalendarTypeSundayBased.Location = new System.Drawing.Point(24, 131);
			this.buttonXCalendarTypeSundayBased.Name = "buttonXCalendarTypeSundayBased";
			this.buttonXCalendarTypeSundayBased.Size = new System.Drawing.Size(296, 55);
			this.buttonXCalendarTypeSundayBased.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXCalendarTypeSundayBased.TabIndex = 1;
			this.buttonXCalendarTypeSundayBased.Text = "Sunday - Saturday";
			this.buttonXCalendarTypeSundayBased.CheckedChanged += new System.EventHandler(this.buttonXCalendarType_CheckedChanged);
			this.buttonXCalendarTypeSundayBased.Click += new System.EventHandler(this.buttonXCalendarType_Click);
			// 
			// buttonXCalendarTypeMondayBased
			// 
			this.buttonXCalendarTypeMondayBased.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXCalendarTypeMondayBased.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXCalendarTypeMondayBased.Location = new System.Drawing.Point(24, 24);
			this.buttonXCalendarTypeMondayBased.Name = "buttonXCalendarTypeMondayBased";
			this.buttonXCalendarTypeMondayBased.Size = new System.Drawing.Size(296, 55);
			this.buttonXCalendarTypeMondayBased.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXCalendarTypeMondayBased.TabIndex = 0;
			this.buttonXCalendarTypeMondayBased.Text = "Monday - Sunday";
			this.buttonXCalendarTypeMondayBased.CheckedChanged += new System.EventHandler(this.buttonXCalendarType_CheckedChanged);
			this.buttonXCalendarTypeMondayBased.Click += new System.EventHandler(this.buttonXCalendarType_Click);
			// 
			// splitContainerControl
			// 
			this.splitContainerControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainerControl.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel2;
			this.splitContainerControl.Location = new System.Drawing.Point(0, 0);
			this.splitContainerControl.Name = "splitContainerControl";
			this.splitContainerControl.Panel1.Controls.Add(this.xtraTabControlMain);
			this.splitContainerControl.Panel1.Text = "Panel1";
			this.splitContainerControl.Panel2.Controls.Add(this.xtraTabControlOptions);
			this.splitContainerControl.Panel2.MinSize = 350;
			this.splitContainerControl.Panel2.Text = "Panel2";
			this.splitContainerControl.Size = new System.Drawing.Size(1020, 600);
			this.splitContainerControl.SplitterPosition = 350;
			this.splitContainerControl.TabIndex = 19;
			this.splitContainerControl.Text = "splitContainerControl1";
			// 
			// superTooltip
			// 
			this.superTooltip.DefaultTooltipSettings = new DevComponents.DotNetBar.SuperTooltipInfo("", "", "", null, null, DevComponents.DotNetBar.eTooltipColor.Gray);
			this.superTooltip.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			// 
			// pnMediaDefault
			// 
			this.pnMediaDefault.Controls.Add(this.pbMediaDefault);
			this.pnMediaDefault.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnMediaDefault.Location = new System.Drawing.Point(10, 10);
			this.pnMediaDefault.Name = "pnMediaDefault";
			this.pnMediaDefault.Size = new System.Drawing.Size(639, 549);
			this.pnMediaDefault.TabIndex = 24;
			this.pnMediaDefault.Resize += new System.EventHandler(this.pnMediaDefault_Resize);
			// 
			// HomeControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.splitContainerControl);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "HomeControl";
			this.Size = new System.Drawing.Size(1020, 600);
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControlMain)).EndInit();
			this.xtraTabControlMain.ResumeLayout(false);
			this.xtraTabPageMedia.ResumeLayout(false);
			this.pnMedia.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pbMediaLogo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbMediaDefault)).EndInit();
			this.xtraTabPageDigital.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControlOptions)).EndInit();
			this.xtraTabControlOptions.ResumeLayout(false);
			this.xtraTabPageStations.ResumeLayout(false);
			this.xtraTabPageDayparts.ResumeLayout(false);
			this.xtraTabPageDemos.ResumeLayout(false);
			this.pnDemos.ResumeLayout(false);
			this.pnDemosInfo.ResumeLayout(false);
			this.pnSelectDemo.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditDemos.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			this.pnSelectSource.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditSource.Properties)).EndInit();
			this.pnDemosType.ResumeLayout(false);
			this.pnDemosImport.ResumeLayout(false);
			this.pnDemosCustom.ResumeLayout(false);
			this.pnUseDemos.ResumeLayout(false);
			this.xtraTabPageCalendarType.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainerControl)).EndInit();
			this.splitContainerControl.ResumeLayout(false);
			this.pnMediaDefault.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

		private StationsControl stationsControl;
        private DevExpress.XtraTab.XtraTabControl xtraTabControlMain;
		private DevExpress.XtraTab.XtraTabPage xtraTabPageMedia;
		private System.Windows.Forms.Panel pnMedia;
		private DaypartsControl daypartsControl;
        private DevExpress.XtraTab.XtraTabControl xtraTabControlOptions;
        private DevExpress.XtraTab.XtraTabPage xtraTabPageStations;
        private DevExpress.XtraTab.XtraTabPage xtraTabPageDayparts;
		private DevExpress.XtraEditors.SplitContainerControl splitContainerControl;
        private DevExpress.XtraTab.XtraTabPage xtraTabPageDemos;
		private System.Windows.Forms.Panel pnDemos;
        private DevComponents.DotNetBar.ButtonX buttonXDemosImps;
		private DevComponents.DotNetBar.ButtonX buttonXDemosRtg;
        private DevComponents.DotNetBar.ButtonX buttonXDemosImport;
        private DevComponents.DotNetBar.ButtonX buttonXDemosCustom;
        private DevComponents.DotNetBar.ButtonX buttonXUseDemos;
        private DevExpress.XtraEditors.StyleController styleController;
		private DevExpress.XtraEditors.ComboBoxEdit comboBoxEditDemos;
		private DevExpress.XtraEditors.ComboBoxEdit comboBoxEditSource;
		private DevExpress.XtraTab.XtraTabPage xtraTabPageDigital;
		private System.Windows.Forms.Panel pnDemosImport;
		private System.Windows.Forms.Panel pnDemosCustom;
		private System.Windows.Forms.Panel pnUseDemos;
		private System.Windows.Forms.Panel pnDemosType;
		private System.Windows.Forms.Panel pnSelectDemo;
		private System.Windows.Forms.Panel pnSelectSource;
		private System.Windows.Forms.Panel pnDemosInfo;
		private OnlineSchedule.Controls.PresentationClasses.DigitalProductListControl digitalProductListControl;
		private DevComponents.DotNetBar.ButtonX buttonXMonthlySchedule;
		private DevComponents.DotNetBar.ButtonX buttonXWeeklySchedule;
		private DevExpress.XtraTab.XtraTabPage xtraTabPageCalendarType;
		private DevComponents.DotNetBar.ButtonX buttonXCalendarTypeSundayBased;
		private DevComponents.DotNetBar.ButtonX buttonXCalendarTypeMondayBased;
		private System.Windows.Forms.Label laMediaDescription;
		private System.Windows.Forms.Label laMediaTitle;
		private System.Windows.Forms.Label laFlexDateWarning;
		private System.Windows.Forms.PictureBox pbMediaLogo;
		private DevExpress.XtraEditors.LabelControl labelControlDemosInfo;
		private DevExpress.XtraEditors.LabelControl labelControlDemoTitle;
		private DevComponents.DotNetBar.SuperTooltip superTooltip;
		private System.Windows.Forms.PictureBox pbMediaDefault;
		private Panel pnMediaDefault;

    }
}
