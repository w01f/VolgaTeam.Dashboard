namespace NewBizWiz.MiniBar.SettingsForms
{
    partial class FormMinibarOptions
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMinibarOptions));
            this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel();
            this.laHeader = new System.Windows.Forms.Label();
            this.pnMain = new System.Windows.Forms.Panel();
            this.xtraTabControl = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPageControl = new DevExpress.XtraTab.XtraTabPage();
            this.checkEditQuickRetraction = new DevExpress.XtraEditors.CheckEdit();
            this.styleController = new DevExpress.XtraEditors.StyleController();
            this.laControlOwnDescription = new System.Windows.Forms.Label();
            this.rbControlOwn = new System.Windows.Forms.RadioButton();
            this.laControlImportDescription = new System.Windows.Forms.Label();
            this.rbControlImport = new System.Windows.Forms.RadioButton();
            this.laControlHeader = new System.Windows.Forms.Label();
            this.xtraTabPageAutorun = new DevExpress.XtraTab.XtraTabPage();
            this.laAutorunFloatDescription = new System.Windows.Forms.Label();
            this.rbAutorunFloat = new System.Windows.Forms.RadioButton();
            this.laAutorunNoneDescription = new System.Windows.Forms.Label();
            this.rbAutorunNone = new System.Windows.Forms.RadioButton();
            this.laAutorunHiddenDescription = new System.Windows.Forms.Label();
            this.rbAutorunHidden = new System.Windows.Forms.RadioButton();
            this.laAutorunNormalDescription = new System.Windows.Forms.Label();
            this.rbAutorunNormal = new System.Windows.Forms.RadioButton();
            this.laAutorunHeader = new System.Windows.Forms.Label();
            this.xtraTabPageVisibility = new DevExpress.XtraTab.XtraTabPage();
            this.laVisiblePowerPointMaximizedDescription = new System.Windows.Forms.Label();
            this.rbVisiblePowerPointMaximized = new System.Windows.Forms.RadioButton();
            this.laVisiblePowerPointDescription = new System.Windows.Forms.Label();
            this.rbVisiblePowerPoint = new System.Windows.Forms.RadioButton();
            this.laHideSpecificProgramMaximizedDescription = new System.Windows.Forms.Label();
            this.rbHideSpecificProgramMaximized = new System.Windows.Forms.RadioButton();
            this.laHideSpecificProgramDescription = new System.Windows.Forms.Label();
            this.rbHideSpecificProgram = new System.Windows.Forms.RadioButton();
            this.laHideAllDescription = new System.Windows.Forms.Label();
            this.rbHideAll = new System.Windows.Forms.RadioButton();
            this.laVisibility = new System.Windows.Forms.Label();
            this.xtraTabPagePrograms = new DevExpress.XtraTab.XtraTabPage();
            this.laPrograms = new System.Windows.Forms.Label();
            this.memoEditPrograms = new DevExpress.XtraEditors.MemoEdit();
            this.xtraTabPageClose = new DevExpress.XtraTab.XtraTabPage();
            this.laCloseFloatDescription = new System.Windows.Forms.Label();
            this.rbCloseFloat = new System.Windows.Forms.RadioButton();
            this.laCloseShutdownDescription = new System.Windows.Forms.Label();
            this.rbCloseShutdown = new System.Windows.Forms.RadioButton();
            this.laCloseHideDescription = new System.Windows.Forms.Label();
            this.rbCloseHide = new System.Windows.Forms.RadioButton();
            this.laClose = new System.Windows.Forms.Label();
            this.buttonXCancel = new DevComponents.DotNetBar.ButtonX();
            this.buttonXSave = new DevComponents.DotNetBar.ButtonX();
            this.pnTop = new System.Windows.Forms.Panel();
            this.pnMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).BeginInit();
            this.xtraTabControl.SuspendLayout();
            this.xtraTabPageControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditQuickRetraction.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
            this.xtraTabPageAutorun.SuspendLayout();
            this.xtraTabPageVisibility.SuspendLayout();
            this.xtraTabPagePrograms.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoEditPrograms.Properties)).BeginInit();
            this.xtraTabPageClose.SuspendLayout();
            this.pnTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // defaultLookAndFeel
            // 
            this.defaultLookAndFeel.LookAndFeel.SkinName = "Money Twins";
            // 
            // laHeader
            // 
            this.laHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.laHeader.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laHeader.Location = new System.Drawing.Point(0, 0);
            this.laHeader.Name = "laHeader";
            this.laHeader.Size = new System.Drawing.Size(529, 58);
            this.laHeader.TabIndex = 0;
            this.laHeader.Text = "The Minibar...";
            this.laHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnMain
            // 
            this.pnMain.Controls.Add(this.xtraTabControl);
            this.pnMain.Controls.Add(this.buttonXCancel);
            this.pnMain.Controls.Add(this.buttonXSave);
            this.pnMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnMain.Location = new System.Drawing.Point(0, 58);
            this.pnMain.Name = "pnMain";
            this.pnMain.Size = new System.Drawing.Size(529, 449);
            this.pnMain.TabIndex = 4;
            // 
            // xtraTabControl
            // 
            this.xtraTabControl.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.xtraTabControl.Appearance.Options.UseFont = true;
            this.xtraTabControl.AppearancePage.Header.Font = new System.Drawing.Font("Arial", 9.75F);
            this.xtraTabControl.AppearancePage.Header.Options.UseFont = true;
            this.xtraTabControl.AppearancePage.HeaderActive.Font = new System.Drawing.Font("Arial", 9.75F);
            this.xtraTabControl.AppearancePage.HeaderActive.Options.UseFont = true;
            this.xtraTabControl.AppearancePage.HeaderDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
            this.xtraTabControl.AppearancePage.HeaderDisabled.Options.UseFont = true;
            this.xtraTabControl.AppearancePage.HeaderHotTracked.Font = new System.Drawing.Font("Arial", 9.75F);
            this.xtraTabControl.AppearancePage.HeaderHotTracked.Options.UseFont = true;
            this.xtraTabControl.AppearancePage.PageClient.Font = new System.Drawing.Font("Arial", 9.75F);
            this.xtraTabControl.AppearancePage.PageClient.Options.UseFont = true;
            this.xtraTabControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.xtraTabControl.Location = new System.Drawing.Point(0, 0);
            this.xtraTabControl.Name = "xtraTabControl";
            this.xtraTabControl.SelectedTabPage = this.xtraTabPageControl;
            this.xtraTabControl.Size = new System.Drawing.Size(529, 401);
            this.xtraTabControl.TabIndex = 14;
            this.xtraTabControl.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPageControl,
            this.xtraTabPageAutorun,
            this.xtraTabPageVisibility,
            this.xtraTabPagePrograms,
            this.xtraTabPageClose});
            // 
            // xtraTabPageControl
            // 
            this.xtraTabPageControl.Controls.Add(this.checkEditQuickRetraction);
            this.xtraTabPageControl.Controls.Add(this.laControlOwnDescription);
            this.xtraTabPageControl.Controls.Add(this.rbControlOwn);
            this.xtraTabPageControl.Controls.Add(this.laControlImportDescription);
            this.xtraTabPageControl.Controls.Add(this.rbControlImport);
            this.xtraTabPageControl.Controls.Add(this.laControlHeader);
            this.xtraTabPageControl.Name = "xtraTabPageControl";
            this.xtraTabPageControl.Size = new System.Drawing.Size(527, 375);
            this.xtraTabPageControl.Text = "1. Control";
            // 
            // checkEditQuickRetraction
            // 
            this.checkEditQuickRetraction.Location = new System.Drawing.Point(9, 342);
            this.checkEditQuickRetraction.Name = "checkEditQuickRetraction";
            this.checkEditQuickRetraction.Properties.Caption = "Disable minibar Fade animation";
            this.checkEditQuickRetraction.Size = new System.Drawing.Size(507, 21);
            this.checkEditQuickRetraction.StyleController = this.styleController;
            this.checkEditQuickRetraction.TabIndex = 6;
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
            // laControlOwnDescription
            // 
            this.laControlOwnDescription.AutoSize = true;
            this.laControlOwnDescription.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laControlOwnDescription.Location = new System.Drawing.Point(29, 137);
            this.laControlOwnDescription.Name = "laControlOwnDescription";
            this.laControlOwnDescription.Size = new System.Drawing.Size(413, 15);
            this.laControlOwnDescription.TabIndex = 5;
            this.laControlOwnDescription.Text = "I need to make some changes to how the minibar works on my computer...";
            // 
            // rbControlOwn
            // 
            this.rbControlOwn.AutoSize = true;
            this.rbControlOwn.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rbControlOwn.Location = new System.Drawing.Point(11, 114);
            this.rbControlOwn.Name = "rbControlOwn";
            this.rbControlOwn.Size = new System.Drawing.Size(356, 21);
            this.rbControlOwn.TabIndex = 4;
            this.rbControlOwn.TabStop = true;
            this.rbControlOwn.Text = "B. I want to Control my OWN Minibar View Settings";
            this.rbControlOwn.UseVisualStyleBackColor = true;
            this.rbControlOwn.CheckedChanged += new System.EventHandler(this.rbControl_CheckedChanged);
            // 
            // laControlImportDescription
            // 
            this.laControlImportDescription.AutoSize = true;
            this.laControlImportDescription.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laControlImportDescription.Location = new System.Drawing.Point(29, 70);
            this.laControlImportDescription.Name = "laControlImportDescription";
            this.laControlImportDescription.Size = new System.Drawing.Size(405, 15);
            this.laControlImportDescription.TabIndex = 3;
            this.laControlImportDescription.Text = "I\'ll just use the default minibar view settings that come with the software...";
            // 
            // rbControlImport
            // 
            this.rbControlImport.AutoSize = true;
            this.rbControlImport.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rbControlImport.Location = new System.Drawing.Point(11, 47);
            this.rbControlImport.Name = "rbControlImport";
            this.rbControlImport.Size = new System.Drawing.Size(354, 21);
            this.rbControlImport.TabIndex = 2;
            this.rbControlImport.TabStop = true;
            this.rbControlImport.Text = "A. Import Standard View Settings from the Network";
            this.rbControlImport.UseVisualStyleBackColor = true;
            this.rbControlImport.CheckedChanged += new System.EventHandler(this.rbControl_CheckedChanged);
            // 
            // laControlHeader
            // 
            this.laControlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.laControlHeader.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laControlHeader.Location = new System.Drawing.Point(0, 0);
            this.laControlHeader.Name = "laControlHeader";
            this.laControlHeader.Size = new System.Drawing.Size(527, 44);
            this.laControlHeader.TabIndex = 1;
            this.laControlHeader.Text = "Who should CONTROL your Minibar View Settings?";
            this.laControlHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // xtraTabPageAutorun
            // 
            this.xtraTabPageAutorun.Controls.Add(this.laAutorunFloatDescription);
            this.xtraTabPageAutorun.Controls.Add(this.rbAutorunFloat);
            this.xtraTabPageAutorun.Controls.Add(this.laAutorunNoneDescription);
            this.xtraTabPageAutorun.Controls.Add(this.rbAutorunNone);
            this.xtraTabPageAutorun.Controls.Add(this.laAutorunHiddenDescription);
            this.xtraTabPageAutorun.Controls.Add(this.rbAutorunHidden);
            this.xtraTabPageAutorun.Controls.Add(this.laAutorunNormalDescription);
            this.xtraTabPageAutorun.Controls.Add(this.rbAutorunNormal);
            this.xtraTabPageAutorun.Controls.Add(this.laAutorunHeader);
            this.xtraTabPageAutorun.Name = "xtraTabPageAutorun";
            this.xtraTabPageAutorun.Size = new System.Drawing.Size(527, 375);
            this.xtraTabPageAutorun.Text = "2. Startup";
            // 
            // laAutorunFloatDescription
            // 
            this.laAutorunFloatDescription.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laAutorunFloatDescription.ForeColor = System.Drawing.Color.Black;
            this.laAutorunFloatDescription.Location = new System.Drawing.Point(29, 288);
            this.laAutorunFloatDescription.Name = "laAutorunFloatDescription";
            this.laAutorunFloatDescription.Size = new System.Drawing.Size(487, 77);
            this.laAutorunFloatDescription.TabIndex = 16;
            this.laAutorunFloatDescription.Text = resources.GetString("laAutorunFloatDescription.Text");
            // 
            // rbAutorunFloat
            // 
            this.rbAutorunFloat.AutoSize = true;
            this.rbAutorunFloat.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rbAutorunFloat.ForeColor = System.Drawing.Color.Black;
            this.rbAutorunFloat.Location = new System.Drawing.Point(11, 265);
            this.rbAutorunFloat.Name = "rbAutorunFloat";
            this.rbAutorunFloat.Size = new System.Drawing.Size(352, 21);
            this.rbAutorunFloat.TabIndex = 15;
            this.rbAutorunFloat.TabStop = true;
            this.rbAutorunFloat.Text = "D. Minibar Hidden - Mini-Floater Visible on Desktop";
            this.rbAutorunFloat.UseVisualStyleBackColor = true;
            // 
            // laAutorunNoneDescription
            // 
            this.laAutorunNoneDescription.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laAutorunNoneDescription.Location = new System.Drawing.Point(29, 219);
            this.laAutorunNoneDescription.Name = "laAutorunNoneDescription";
            this.laAutorunNoneDescription.Size = new System.Drawing.Size(487, 46);
            this.laAutorunNoneDescription.TabIndex = 14;
            this.laAutorunNoneDescription.Text = "Minibar process does not startup AT ALL. Your software will not Synchronize updat" +
                "es and new files...";
            // 
            // rbAutorunNone
            // 
            this.rbAutorunNone.AutoSize = true;
            this.rbAutorunNone.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rbAutorunNone.Location = new System.Drawing.Point(11, 196);
            this.rbAutorunNone.Name = "rbAutorunNone";
            this.rbAutorunNone.Size = new System.Drawing.Size(270, 21);
            this.rbAutorunNone.TabIndex = 13;
            this.rbAutorunNone.TabStop = true;
            this.rbAutorunNone.Text = "C. DO NOT Launch Minibar Program.";
            this.rbAutorunNone.UseVisualStyleBackColor = true;
            // 
            // laAutorunHiddenDescription
            // 
            this.laAutorunHiddenDescription.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laAutorunHiddenDescription.Location = new System.Drawing.Point(29, 137);
            this.laAutorunHiddenDescription.Name = "laAutorunHiddenDescription";
            this.laAutorunHiddenDescription.Size = new System.Drawing.Size(487, 52);
            this.laAutorunHiddenDescription.TabIndex = 10;
            this.laAutorunHiddenDescription.Text = resources.GetString("laAutorunHiddenDescription.Text");
            // 
            // rbAutorunHidden
            // 
            this.rbAutorunHidden.AutoSize = true;
            this.rbAutorunHidden.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rbAutorunHidden.Location = new System.Drawing.Point(11, 114);
            this.rbAutorunHidden.Name = "rbAutorunHidden";
            this.rbAutorunHidden.Size = new System.Drawing.Size(361, 21);
            this.rbAutorunHidden.TabIndex = 9;
            this.rbAutorunHidden.TabStop = true;
            this.rbAutorunHidden.Text = "B. Minibar Hidden - Sync just running in Background";
            this.rbAutorunHidden.UseVisualStyleBackColor = true;
            // 
            // laAutorunNormalDescription
            // 
            this.laAutorunNormalDescription.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laAutorunNormalDescription.Location = new System.Drawing.Point(29, 70);
            this.laAutorunNormalDescription.Name = "laAutorunNormalDescription";
            this.laAutorunNormalDescription.Size = new System.Drawing.Size(487, 41);
            this.laAutorunNormalDescription.TabIndex = 8;
            this.laAutorunNormalDescription.Text = "Traditional Default startup for the minibar so you can have immediate access to s" +
                "everal sales applications and resources...";
            // 
            // rbAutorunNormal
            // 
            this.rbAutorunNormal.AutoSize = true;
            this.rbAutorunNormal.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rbAutorunNormal.Location = new System.Drawing.Point(11, 47);
            this.rbAutorunNormal.Name = "rbAutorunNormal";
            this.rbAutorunNormal.Size = new System.Drawing.Size(259, 21);
            this.rbAutorunNormal.TabIndex = 7;
            this.rbAutorunNormal.TabStop = true;
            this.rbAutorunNormal.Text = "A. Docked Minibar Bottom of screen";
            this.rbAutorunNormal.UseVisualStyleBackColor = true;
            // 
            // laAutorunHeader
            // 
            this.laAutorunHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.laAutorunHeader.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laAutorunHeader.Location = new System.Drawing.Point(0, 0);
            this.laAutorunHeader.Name = "laAutorunHeader";
            this.laAutorunHeader.Size = new System.Drawing.Size(527, 44);
            this.laAutorunHeader.TabIndex = 6;
            this.laAutorunHeader.Text = "How Should the Minibar load when your Computer boots up?";
            this.laAutorunHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // xtraTabPageVisibility
            // 
            this.xtraTabPageVisibility.Controls.Add(this.laVisiblePowerPointMaximizedDescription);
            this.xtraTabPageVisibility.Controls.Add(this.rbVisiblePowerPointMaximized);
            this.xtraTabPageVisibility.Controls.Add(this.laVisiblePowerPointDescription);
            this.xtraTabPageVisibility.Controls.Add(this.rbVisiblePowerPoint);
            this.xtraTabPageVisibility.Controls.Add(this.laHideSpecificProgramMaximizedDescription);
            this.xtraTabPageVisibility.Controls.Add(this.rbHideSpecificProgramMaximized);
            this.xtraTabPageVisibility.Controls.Add(this.laHideSpecificProgramDescription);
            this.xtraTabPageVisibility.Controls.Add(this.rbHideSpecificProgram);
            this.xtraTabPageVisibility.Controls.Add(this.laHideAllDescription);
            this.xtraTabPageVisibility.Controls.Add(this.rbHideAll);
            this.xtraTabPageVisibility.Controls.Add(this.laVisibility);
            this.xtraTabPageVisibility.Name = "xtraTabPageVisibility";
            this.xtraTabPageVisibility.Size = new System.Drawing.Size(527, 375);
            this.xtraTabPageVisibility.Text = "3. Visibility";
            // 
            // laVisiblePowerPointMaximizedDescription
            // 
            this.laVisiblePowerPointMaximizedDescription.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laVisiblePowerPointMaximizedDescription.Location = new System.Drawing.Point(29, 316);
            this.laVisiblePowerPointMaximizedDescription.Name = "laVisiblePowerPointMaximizedDescription";
            this.laVisiblePowerPointMaximizedDescription.Size = new System.Drawing.Size(487, 41);
            this.laVisiblePowerPointMaximizedDescription.TabIndex = 19;
            this.laVisiblePowerPointMaximizedDescription.Text = "The Minibar is by Default, ALWAYS HIDDEN. The only way the Minibar is visible is " +
                "IF PowerPoint is Maximized on your Desktop.";
            // 
            // rbVisiblePowerPointMaximized
            // 
            this.rbVisiblePowerPointMaximized.AutoSize = true;
            this.rbVisiblePowerPointMaximized.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rbVisiblePowerPointMaximized.Location = new System.Drawing.Point(11, 293);
            this.rbVisiblePowerPointMaximized.Name = "rbVisiblePowerPointMaximized";
            this.rbVisiblePowerPointMaximized.Size = new System.Drawing.Size(439, 21);
            this.rbVisiblePowerPointMaximized.TabIndex = 18;
            this.rbVisiblePowerPointMaximized.TabStop = true;
            this.rbVisiblePowerPointMaximized.Text = "E. Only Visible WHEN PowerPoint is MAXIMIZED on the screen.";
            this.rbVisiblePowerPointMaximized.UseVisualStyleBackColor = true;
            this.rbVisiblePowerPointMaximized.CheckedChanged += new System.EventHandler(this.rbHide_CheckedChanged);
            // 
            // laVisiblePowerPointDescription
            // 
            this.laVisiblePowerPointDescription.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laVisiblePowerPointDescription.Location = new System.Drawing.Point(29, 256);
            this.laVisiblePowerPointDescription.Name = "laVisiblePowerPointDescription";
            this.laVisiblePowerPointDescription.Size = new System.Drawing.Size(487, 41);
            this.laVisiblePowerPointDescription.TabIndex = 17;
            this.laVisiblePowerPointDescription.Text = "The Minibar is by Default, ALWAYS HIDDEN. The only way the Minibar is visible is " +
                "IF PowerPoint is Active or Maximized on your Desktop.";
            // 
            // rbVisiblePowerPoint
            // 
            this.rbVisiblePowerPoint.AutoSize = true;
            this.rbVisiblePowerPoint.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rbVisiblePowerPoint.Location = new System.Drawing.Point(11, 233);
            this.rbVisiblePowerPoint.Name = "rbVisiblePowerPoint";
            this.rbVisiblePowerPoint.Size = new System.Drawing.Size(403, 21);
            this.rbVisiblePowerPoint.TabIndex = 16;
            this.rbVisiblePowerPoint.TabStop = true;
            this.rbVisiblePowerPoint.Text = "D. Only Visible WHEN PowerPoint is Active on the screen.";
            this.rbVisiblePowerPoint.UseVisualStyleBackColor = true;
            this.rbVisiblePowerPoint.CheckedChanged += new System.EventHandler(this.rbHide_CheckedChanged);
            // 
            // laHideSpecificProgramMaximizedDescription
            // 
            this.laHideSpecificProgramMaximizedDescription.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laHideSpecificProgramMaximizedDescription.Location = new System.Drawing.Point(29, 194);
            this.laHideSpecificProgramMaximizedDescription.Name = "laHideSpecificProgramMaximizedDescription";
            this.laHideSpecificProgramMaximizedDescription.Size = new System.Drawing.Size(487, 41);
            this.laHideSpecificProgramMaximizedDescription.TabIndex = 15;
            this.laHideSpecificProgramMaximizedDescription.Text = "The Minibar is visible when PowerPoint is active OR Maximized. It is Hidden when " +
                "Specific Programs are only Maximized on my screen.";
            // 
            // rbHideSpecificProgramMaximized
            // 
            this.rbHideSpecificProgramMaximized.AutoSize = true;
            this.rbHideSpecificProgramMaximized.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rbHideSpecificProgramMaximized.Location = new System.Drawing.Point(11, 171);
            this.rbHideSpecificProgramMaximized.Name = "rbHideSpecificProgramMaximized";
            this.rbHideSpecificProgramMaximized.Size = new System.Drawing.Size(497, 21);
            this.rbHideSpecificProgramMaximized.TabIndex = 14;
            this.rbHideSpecificProgramMaximized.TabStop = true;
            this.rbHideSpecificProgramMaximized.Text = "C. Auto Hide only when Specific Programs are Maximized on my Screen.";
            this.rbHideSpecificProgramMaximized.UseVisualStyleBackColor = true;
            this.rbHideSpecificProgramMaximized.CheckedChanged += new System.EventHandler(this.rbHide_CheckedChanged);
            // 
            // laHideSpecificProgramDescription
            // 
            this.laHideSpecificProgramDescription.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laHideSpecificProgramDescription.Location = new System.Drawing.Point(29, 131);
            this.laHideSpecificProgramDescription.Name = "laHideSpecificProgramDescription";
            this.laHideSpecificProgramDescription.Size = new System.Drawing.Size(487, 41);
            this.laHideSpecificProgramDescription.TabIndex = 13;
            this.laHideSpecificProgramDescription.Text = "The Minibar is visible when PowerPoint is active OR Maximized. It is Hidden when " +
                "Specific Programs are Active on my screen.";
            // 
            // rbHideSpecificProgram
            // 
            this.rbHideSpecificProgram.AutoSize = true;
            this.rbHideSpecificProgram.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rbHideSpecificProgram.Location = new System.Drawing.Point(11, 108);
            this.rbHideSpecificProgram.Name = "rbHideSpecificProgram";
            this.rbHideSpecificProgram.Size = new System.Drawing.Size(463, 21);
            this.rbHideSpecificProgram.TabIndex = 12;
            this.rbHideSpecificProgram.TabStop = true;
            this.rbHideSpecificProgram.Text = "B. Auto Hide only when Specific Programs are Active on my screen.";
            this.rbHideSpecificProgram.UseVisualStyleBackColor = true;
            this.rbHideSpecificProgram.CheckedChanged += new System.EventHandler(this.rbHide_CheckedChanged);
            // 
            // laHideAllDescription
            // 
            this.laHideAllDescription.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laHideAllDescription.Location = new System.Drawing.Point(29, 70);
            this.laHideAllDescription.Name = "laHideAllDescription";
            this.laHideAllDescription.Size = new System.Drawing.Size(487, 41);
            this.laHideAllDescription.TabIndex = 11;
            this.laHideAllDescription.Text = "The Minibar is visible only when PowerPoint is Active or Maximized, and when NO o" +
                "ther applications are Maximized on the screen.";
            // 
            // rbHideAll
            // 
            this.rbHideAll.AutoSize = true;
            this.rbHideAll.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rbHideAll.Location = new System.Drawing.Point(11, 47);
            this.rbHideAll.Name = "rbHideAll";
            this.rbHideAll.Size = new System.Drawing.Size(436, 21);
            this.rbHideAll.TabIndex = 10;
            this.rbHideAll.TabStop = true;
            this.rbHideAll.Text = "A. Auto Hide for ALL Maximized Windows EXCEPT PowerPoint.";
            this.rbHideAll.UseVisualStyleBackColor = true;
            this.rbHideAll.CheckedChanged += new System.EventHandler(this.rbHide_CheckedChanged);
            // 
            // laVisibility
            // 
            this.laVisibility.Dock = System.Windows.Forms.DockStyle.Top;
            this.laVisibility.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laVisibility.Location = new System.Drawing.Point(0, 0);
            this.laVisibility.Name = "laVisibility";
            this.laVisibility.Size = new System.Drawing.Size(527, 44);
            this.laVisibility.TabIndex = 9;
            this.laVisibility.Text = "How do you want your Minibar to be Visible on your screen?";
            this.laVisibility.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // xtraTabPagePrograms
            // 
            this.xtraTabPagePrograms.Controls.Add(this.laPrograms);
            this.xtraTabPagePrograms.Controls.Add(this.memoEditPrograms);
            this.xtraTabPagePrograms.Name = "xtraTabPagePrograms";
            this.xtraTabPagePrograms.Size = new System.Drawing.Size(527, 375);
            this.xtraTabPagePrograms.Text = "4. Programs";
            // 
            // laPrograms
            // 
            this.laPrograms.Dock = System.Windows.Forms.DockStyle.Top;
            this.laPrograms.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laPrograms.Location = new System.Drawing.Point(0, 0);
            this.laPrograms.Name = "laPrograms";
            this.laPrograms.Size = new System.Drawing.Size(527, 44);
            this.laPrograms.TabIndex = 11;
            this.laPrograms.Text = "The Minibar is Hidden when the programs below are Active or Maximized:";
            this.laPrograms.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // memoEditPrograms
            // 
            this.memoEditPrograms.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.memoEditPrograms.Location = new System.Drawing.Point(11, 46);
            this.memoEditPrograms.Name = "memoEditPrograms";
            this.memoEditPrograms.Size = new System.Drawing.Size(505, 318);
            this.memoEditPrograms.StyleController = this.styleController;
            this.memoEditPrograms.TabIndex = 10;
            // 
            // xtraTabPageClose
            // 
            this.xtraTabPageClose.Controls.Add(this.laCloseFloatDescription);
            this.xtraTabPageClose.Controls.Add(this.rbCloseFloat);
            this.xtraTabPageClose.Controls.Add(this.laCloseShutdownDescription);
            this.xtraTabPageClose.Controls.Add(this.rbCloseShutdown);
            this.xtraTabPageClose.Controls.Add(this.laCloseHideDescription);
            this.xtraTabPageClose.Controls.Add(this.rbCloseHide);
            this.xtraTabPageClose.Controls.Add(this.laClose);
            this.xtraTabPageClose.Name = "xtraTabPageClose";
            this.xtraTabPageClose.Size = new System.Drawing.Size(527, 375);
            this.xtraTabPageClose.Text = "5. ShutDown";
            // 
            // laCloseFloatDescription
            // 
            this.laCloseFloatDescription.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laCloseFloatDescription.ForeColor = System.Drawing.Color.Black;
            this.laCloseFloatDescription.Location = new System.Drawing.Point(29, 190);
            this.laCloseFloatDescription.Name = "laCloseFloatDescription";
            this.laCloseFloatDescription.Size = new System.Drawing.Size(487, 77);
            this.laCloseFloatDescription.TabIndex = 25;
            this.laCloseFloatDescription.Text = "The Minibar still runs only in the background so your software can still sync upd" +
                "ates and new files. The Mini-Floater is displayed on the desktop.";
            // 
            // rbCloseFloat
            // 
            this.rbCloseFloat.AutoSize = true;
            this.rbCloseFloat.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rbCloseFloat.ForeColor = System.Drawing.Color.Black;
            this.rbCloseFloat.Location = new System.Drawing.Point(11, 167);
            this.rbCloseFloat.Name = "rbCloseFloat";
            this.rbCloseFloat.Size = new System.Drawing.Size(492, 21);
            this.rbCloseFloat.TabIndex = 24;
            this.rbCloseFloat.TabStop = true;
            this.rbCloseFloat.Text = "C. Hide Immediately, SYNC Enabled,  and show Mini-Floater on Desktop";
            this.rbCloseFloat.UseVisualStyleBackColor = true;
            // 
            // laCloseShutdownDescription
            // 
            this.laCloseShutdownDescription.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laCloseShutdownDescription.Location = new System.Drawing.Point(29, 137);
            this.laCloseShutdownDescription.Name = "laCloseShutdownDescription";
            this.laCloseShutdownDescription.Size = new System.Drawing.Size(487, 27);
            this.laCloseShutdownDescription.TabIndex = 21;
            this.laCloseShutdownDescription.Text = "The Minibar closes. Your software will not Sync if the Minibar is Shut Down.";
            // 
            // rbCloseShutdown
            // 
            this.rbCloseShutdown.AutoSize = true;
            this.rbCloseShutdown.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rbCloseShutdown.Location = new System.Drawing.Point(11, 114);
            this.rbCloseShutdown.Name = "rbCloseShutdown";
            this.rbCloseShutdown.Size = new System.Drawing.Size(284, 21);
            this.rbCloseShutdown.TabIndex = 20;
            this.rbCloseShutdown.TabStop = true;
            this.rbCloseShutdown.Text = "B. Just Totally SHUT DOWN the Minibar";
            this.rbCloseShutdown.UseVisualStyleBackColor = true;
            // 
            // laCloseHideDescription
            // 
            this.laCloseHideDescription.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laCloseHideDescription.Location = new System.Drawing.Point(29, 70);
            this.laCloseHideDescription.Name = "laCloseHideDescription";
            this.laCloseHideDescription.Size = new System.Drawing.Size(487, 41);
            this.laCloseHideDescription.TabIndex = 19;
            this.laCloseHideDescription.Text = "The Minibar still runs only in the background so your software can still sync upd" +
                "ates and new files.";
            // 
            // rbCloseHide
            // 
            this.rbCloseHide.AutoSize = true;
            this.rbCloseHide.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rbCloseHide.Location = new System.Drawing.Point(11, 47);
            this.rbCloseHide.Name = "rbCloseHide";
            this.rbCloseHide.Size = new System.Drawing.Size(436, 21);
            this.rbCloseHide.TabIndex = 18;
            this.rbCloseHide.TabStop = true;
            this.rbCloseHide.Text = "A. Hide Immediately but leave SYNC Enabled in the background";
            this.rbCloseHide.UseVisualStyleBackColor = true;
            // 
            // laClose
            // 
            this.laClose.Dock = System.Windows.Forms.DockStyle.Top;
            this.laClose.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laClose.Location = new System.Drawing.Point(0, 0);
            this.laClose.Name = "laClose";
            this.laClose.Size = new System.Drawing.Size(527, 44);
            this.laClose.TabIndex = 17;
            this.laClose.Text = "When you CLOSE the Minibar, what should happen?";
            this.laClose.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonXCancel
            // 
            this.buttonXCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonXCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonXCancel.ForeColor = System.Drawing.Color.Black;
            this.buttonXCancel.Location = new System.Drawing.Point(374, 407);
            this.buttonXCancel.Name = "buttonXCancel";
            this.buttonXCancel.Size = new System.Drawing.Size(122, 32);
            this.buttonXCancel.TabIndex = 12;
            this.buttonXCancel.Text = "Cancel";
            // 
            // buttonXSave
            // 
            this.buttonXSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonXSave.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonXSave.ForeColor = System.Drawing.Color.Black;
            this.buttonXSave.Location = new System.Drawing.Point(222, 407);
            this.buttonXSave.Name = "buttonXSave";
            this.buttonXSave.Size = new System.Drawing.Size(122, 32);
            this.buttonXSave.TabIndex = 11;
            this.buttonXSave.Text = "Save and Close";
            // 
            // pnTop
            // 
            this.pnTop.Controls.Add(this.laHeader);
            this.pnTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnTop.Location = new System.Drawing.Point(0, 0);
            this.pnTop.Name = "pnTop";
            this.pnTop.Size = new System.Drawing.Size(529, 58);
            this.pnTop.TabIndex = 3;
            // 
            // FormMinibarOptions
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(529, 507);
            this.Controls.Add(this.pnMain);
            this.Controls.Add(this.pnTop);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMinibarOptions";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Minibar Options";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMinibarOptions_FormClosed);
            this.Load += new System.EventHandler(this.FormMinibarOptions_Load);
            this.pnMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).EndInit();
            this.xtraTabControl.ResumeLayout(false);
            this.xtraTabPageControl.ResumeLayout(false);
            this.xtraTabPageControl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditQuickRetraction.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
            this.xtraTabPageAutorun.ResumeLayout(false);
            this.xtraTabPageAutorun.PerformLayout();
            this.xtraTabPageVisibility.ResumeLayout(false);
            this.xtraTabPageVisibility.PerformLayout();
            this.xtraTabPagePrograms.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.memoEditPrograms.Properties)).EndInit();
            this.xtraTabPageClose.ResumeLayout(false);
            this.xtraTabPageClose.PerformLayout();
            this.pnTop.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
        private System.Windows.Forms.Label laHeader;
        private System.Windows.Forms.Panel pnMain;
        private System.Windows.Forms.Panel pnTop;
        private DevExpress.XtraEditors.StyleController styleController;
        private DevExpress.XtraEditors.MemoEdit memoEditPrograms;
        private DevComponents.DotNetBar.ButtonX buttonXCancel;
        private DevComponents.DotNetBar.ButtonX buttonXSave;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl;
        private DevExpress.XtraTab.XtraTabPage xtraTabPageControl;
        private System.Windows.Forms.Label laControlHeader;
        private DevExpress.XtraTab.XtraTabPage xtraTabPageAutorun;
        private DevExpress.XtraTab.XtraTabPage xtraTabPageVisibility;
        private DevExpress.XtraTab.XtraTabPage xtraTabPagePrograms;
        private DevExpress.XtraTab.XtraTabPage xtraTabPageClose;
        private System.Windows.Forms.Label laControlOwnDescription;
        private System.Windows.Forms.Label laControlImportDescription;
        private System.Windows.Forms.RadioButton rbControlImport;
        private System.Windows.Forms.RadioButton rbControlOwn;
        private System.Windows.Forms.Label laAutorunFloatDescription;
        private System.Windows.Forms.RadioButton rbAutorunFloat;
        private System.Windows.Forms.Label laAutorunNoneDescription;
        private System.Windows.Forms.RadioButton rbAutorunNone;
        private System.Windows.Forms.Label laAutorunHiddenDescription;
        private System.Windows.Forms.RadioButton rbAutorunHidden;
        private System.Windows.Forms.Label laAutorunNormalDescription;
        private System.Windows.Forms.RadioButton rbAutorunNormal;
        private System.Windows.Forms.Label laAutorunHeader;
        private System.Windows.Forms.Label laVisiblePowerPointMaximizedDescription;
        private System.Windows.Forms.RadioButton rbVisiblePowerPointMaximized;
        private System.Windows.Forms.Label laVisiblePowerPointDescription;
        private System.Windows.Forms.RadioButton rbVisiblePowerPoint;
        private System.Windows.Forms.Label laHideSpecificProgramMaximizedDescription;
        private System.Windows.Forms.RadioButton rbHideSpecificProgramMaximized;
        private System.Windows.Forms.Label laHideSpecificProgramDescription;
        private System.Windows.Forms.RadioButton rbHideSpecificProgram;
        private System.Windows.Forms.Label laHideAllDescription;
        private System.Windows.Forms.RadioButton rbHideAll;
        private System.Windows.Forms.Label laVisibility;
        private System.Windows.Forms.Label laPrograms;
        private System.Windows.Forms.Label laCloseFloatDescription;
        private System.Windows.Forms.RadioButton rbCloseFloat;
        private System.Windows.Forms.Label laCloseShutdownDescription;
        private System.Windows.Forms.RadioButton rbCloseShutdown;
        private System.Windows.Forms.Label laCloseHideDescription;
        private System.Windows.Forms.RadioButton rbCloseHide;
        private System.Windows.Forms.Label laClose;
        private DevExpress.XtraEditors.CheckEdit checkEditQuickRetraction;
    }
}