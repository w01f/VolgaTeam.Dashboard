
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
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
			this.labelItemSlidesLogo = new DevComponents.DotNetBar.LabelItem();
			this.ribbonTabItemSlides = new DevComponents.DotNetBar.RibbonTabItem();
			this.buttonItemSlideSettings = new DevComponents.DotNetBar.ButtonItem();
			this.superTooltip = new DevComponents.DotNetBar.SuperTooltip();
			this.styleManager = new DevComponents.DotNetBar.StyleManager(this.components);
			this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
			this.ribbonControl.SuspendLayout();
			this.ribbonPanelSlides.SuspendLayout();
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
			this.ribbonControl.CaptionVisible = true;
			this.ribbonControl.Controls.Add(this.ribbonPanelSlides);
			this.ribbonControl.Dock = System.Windows.Forms.DockStyle.Top;
			this.ribbonControl.ForeColor = System.Drawing.Color.Black;
			this.ribbonControl.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
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
			this.ribbonBarSlidesExit.TabIndex = 29;
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
			this.buttonItemSlidesExit.Image = global::Asa.SlideTemplateViewer.Properties.Resources.Exit;
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
			this.ribbonBarSlidesFloater.TabIndex = 32;
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
			this.buttonItemSlidesFloater.Image = global::Asa.SlideTemplateViewer.Properties.Resources.Floater;
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
			this.ribbonBarSlidesHelp.TabIndex = 31;
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
			this.buttonItemSlidesHelp.Image = global::Asa.SlideTemplateViewer.Properties.Resources.Help;
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
			this.ribbonBarSlidesPreview.Size = new System.Drawing.Size(94, 130);
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
			// labelItemSlidesLogo
			// 
			this.labelItemSlidesLogo.Image = global::Asa.SlideTemplateViewer.Properties.Resources.AddSlidesLogo;
			this.labelItemSlidesLogo.Name = "labelItemSlidesLogo";
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
			// FormMain
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(925, 699);
			this.Controls.Add(this.ribbonControl);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
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
			this.ResumeLayout(false);

        }

        #endregion
        public DevComponents.DotNetBar.SuperTooltip superTooltip;
		public DevComponents.DotNetBar.RibbonControl ribbonControl;
		private DevComponents.DotNetBar.RibbonPanel ribbonPanelSlides;
		private DevComponents.DotNetBar.RibbonBar ribbonBarSlidesLogo;
		public DevComponents.DotNetBar.RibbonTabItem ribbonTabItemSlides;
		private DevComponents.DotNetBar.ButtonItem buttonItemSlideSettings;
		private DevComponents.DotNetBar.StyleManager styleManager;
		private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
		private DevComponents.DotNetBar.LabelItem labelItemSlidesLogo;
		private DevComponents.DotNetBar.RibbonBar ribbonBarSlidesExit;
		private DevComponents.DotNetBar.ButtonItem buttonItemSlidesExit;
		private DevComponents.DotNetBar.RibbonBar ribbonBarSlidesFloater;
		private DevComponents.DotNetBar.ButtonItem buttonItemSlidesFloater;
		private DevComponents.DotNetBar.RibbonBar ribbonBarSlidesHelp;
		public DevComponents.DotNetBar.ButtonItem buttonItemSlidesHelp;
		public DevComponents.DotNetBar.RibbonBar ribbonBarSlidesPowerPoint;
		public DevComponents.DotNetBar.ButtonItem buttonItemSlidesPowerPoint;
		private DevComponents.DotNetBar.RibbonBar ribbonBarSlidesPreview;
		private DevComponents.DotNetBar.ButtonItem buttonItemSlidesPreview;
	}
}

