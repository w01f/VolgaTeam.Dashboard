namespace AdSalesAddIn
{
    partial class ribbonAdSales : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public ribbonAdSales()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
            LoadNBWApplications();
        }

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
            this.tabAdSales = this.Factory.CreateRibbonTab();
            this.groupSlideFormat = this.Factory.CreateRibbonGroup();
            this.dropDownWizards = this.Factory.CreateRibbonDropDown();
            this.buttonGroupSlideFormatLandscape = this.Factory.CreateRibbonButtonGroup();
            this.buttonGroupSlideFormatPortrait = this.Factory.CreateRibbonButtonGroup();
            this.groupStarterSlides = this.Factory.CreateRibbonGroup();
            this.groupDashboard = this.Factory.CreateRibbonGroup();
            this.groupSalesDepot = this.Factory.CreateRibbonGroup();
            this.groupClipart = this.Factory.CreateRibbonGroup();
            this.groupApplications = this.Factory.CreateRibbonGroup();
            this.groupMinibar = this.Factory.CreateRibbonGroup();
            this.toggleButtonSlideFormat43 = this.Factory.CreateRibbonToggleButton();
            this.toggleButtonSlideFormat54 = this.Factory.CreateRibbonToggleButton();
            this.toggleButtonSlideFormat169 = this.Factory.CreateRibbonToggleButton();
            this.toggleButtonSlideFormat34 = this.Factory.CreateRibbonToggleButton();
            this.toggleButtonSlideFormat45 = this.Factory.CreateRibbonToggleButton();
            this.buttonAddCover = this.Factory.CreateRibbonButton();
            this.buttonAddSlide = this.Factory.CreateRibbonButton();
            this.buttonDashboard = this.Factory.CreateRibbonButton();
            this.buttonSalesDepot = this.Factory.CreateRibbonButton();
            this.buttonSalesGallery = this.Factory.CreateRibbonButton();
            this.buttonClientLogos = this.Factory.CreateRibbonButton();
            this.buttonWebArt = this.Factory.CreateRibbonButton();
            this.buttonMinibarLoad = this.Factory.CreateRibbonButton();
            this.buttonMinibarKill = this.Factory.CreateRibbonButton();
            this.tabAdSales.SuspendLayout();
            this.groupSlideFormat.SuspendLayout();
            this.buttonGroupSlideFormatLandscape.SuspendLayout();
            this.buttonGroupSlideFormatPortrait.SuspendLayout();
            this.groupStarterSlides.SuspendLayout();
            this.groupDashboard.SuspendLayout();
            this.groupSalesDepot.SuspendLayout();
            this.groupClipart.SuspendLayout();
            this.groupMinibar.SuspendLayout();
            // 
            // tabAdSales
            // 
            this.tabAdSales.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.tabAdSales.Groups.Add(this.groupSlideFormat);
            this.tabAdSales.Groups.Add(this.groupStarterSlides);
            this.tabAdSales.Groups.Add(this.groupDashboard);
            this.tabAdSales.Groups.Add(this.groupSalesDepot);
            this.tabAdSales.Groups.Add(this.groupClipart);
            this.tabAdSales.Groups.Add(this.groupApplications);
            this.tabAdSales.Groups.Add(this.groupMinibar);
            this.tabAdSales.Label = "adSALESapps";
            this.tabAdSales.Name = "tabAdSales";
            // 
            // groupSlideFormat
            // 
            this.groupSlideFormat.Items.Add(this.dropDownWizards);
            this.groupSlideFormat.Items.Add(this.buttonGroupSlideFormatLandscape);
            this.groupSlideFormat.Items.Add(this.buttonGroupSlideFormatPortrait);
            this.groupSlideFormat.Label = "Slide Format";
            this.groupSlideFormat.Name = "groupSlideFormat";
            // 
            // dropDownWizards
            // 
            this.dropDownWizards.Label = "Wizards";
            this.dropDownWizards.Name = "dropDownWizards";
            this.dropDownWizards.ShowLabel = false;
            this.dropDownWizards.SelectionChanged += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.dropDownWizards_SelectionChanged);
            // 
            // buttonGroupSlideFormatLandscape
            // 
            this.buttonGroupSlideFormatLandscape.Items.Add(this.toggleButtonSlideFormat43);
            this.buttonGroupSlideFormatLandscape.Items.Add(this.toggleButtonSlideFormat54);
            this.buttonGroupSlideFormatLandscape.Items.Add(this.toggleButtonSlideFormat169);
            this.buttonGroupSlideFormatLandscape.Name = "buttonGroupSlideFormatLandscape";
            // 
            // buttonGroupSlideFormatPortrait
            // 
            this.buttonGroupSlideFormatPortrait.Items.Add(this.toggleButtonSlideFormat34);
            this.buttonGroupSlideFormatPortrait.Items.Add(this.toggleButtonSlideFormat45);
            this.buttonGroupSlideFormatPortrait.Name = "buttonGroupSlideFormatPortrait";
            // 
            // groupStarterSlides
            // 
            this.groupStarterSlides.Items.Add(this.buttonAddCover);
            this.groupStarterSlides.Items.Add(this.buttonAddSlide);
            this.groupStarterSlides.Label = "Starter Slides";
            this.groupStarterSlides.Name = "groupStarterSlides";
            // 
            // groupDashboard
            // 
            this.groupDashboard.Items.Add(this.buttonDashboard);
            this.groupDashboard.Label = "Dashboard";
            this.groupDashboard.Name = "groupDashboard";
            // 
            // groupSalesDepot
            // 
            this.groupSalesDepot.Items.Add(this.buttonSalesDepot);
            this.groupSalesDepot.Label = "Sales Library";
            this.groupSalesDepot.Name = "groupSalesDepot";
            // 
            // groupClipart
            // 
            this.groupClipart.Items.Add(this.buttonSalesGallery);
            this.groupClipart.Items.Add(this.buttonClientLogos);
            this.groupClipart.Items.Add(this.buttonWebArt);
            this.groupClipart.Label = "Clipart";
            this.groupClipart.Name = "groupClipart";
            // 
            // groupApplications
            // 
            this.groupApplications.Label = "Apps";
            this.groupApplications.Name = "groupApplications";
            // 
            // groupMinibar
            // 
            this.groupMinibar.Items.Add(this.buttonMinibarLoad);
            this.groupMinibar.Items.Add(this.buttonMinibarKill);
            this.groupMinibar.Label = "Minibar";
            this.groupMinibar.Name = "groupMinibar";
            // 
            // toggleButtonSlideFormat43
            // 
            this.toggleButtonSlideFormat43.Label = "4 x 3";
            this.toggleButtonSlideFormat43.Name = "toggleButtonSlideFormat43";
            this.toggleButtonSlideFormat43.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.toggleButtonSlideFormat45_Click);
            // 
            // toggleButtonSlideFormat54
            // 
            this.toggleButtonSlideFormat54.Label = "5 x 4";
            this.toggleButtonSlideFormat54.Name = "toggleButtonSlideFormat54";
            this.toggleButtonSlideFormat54.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.toggleButtonSlideFormat45_Click);
            // 
            // toggleButtonSlideFormat169
            // 
            this.toggleButtonSlideFormat169.Label = "16 x 9";
            this.toggleButtonSlideFormat169.Name = "toggleButtonSlideFormat169";
            this.toggleButtonSlideFormat169.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.toggleButtonSlideFormat45_Click);
            // 
            // toggleButtonSlideFormat34
            // 
            this.toggleButtonSlideFormat34.Label = "3 x 4";
            this.toggleButtonSlideFormat34.Name = "toggleButtonSlideFormat34";
            this.toggleButtonSlideFormat34.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.toggleButtonSlideFormat45_Click);
            // 
            // toggleButtonSlideFormat45
            // 
            this.toggleButtonSlideFormat45.Label = "4 x 5";
            this.toggleButtonSlideFormat45.Name = "toggleButtonSlideFormat45";
            this.toggleButtonSlideFormat45.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.toggleButtonSlideFormat45_Click);
            // 
            // buttonAddCover
            // 
            this.buttonAddCover.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.buttonAddCover.Label = "Add Cover";
            this.buttonAddCover.Name = "buttonAddCover";
            this.buttonAddCover.ScreenTip = "Add a Cover Slide";
            this.buttonAddCover.ShowImage = true;
            this.buttonAddCover.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonAddCover_Click);
            // 
            // buttonAddSlide
            // 
            this.buttonAddSlide.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.buttonAddSlide.Label = "Add Slide";
            this.buttonAddSlide.Name = "buttonAddSlide";
            this.buttonAddSlide.ScreenTip = "Add an Empty Slide";
            this.buttonAddSlide.ShowImage = true;
            this.buttonAddSlide.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonAddSlide_Click);
            // 
            // buttonDashboard
            // 
            this.buttonDashboard.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.buttonDashboard.Image = global::AdSalesAddIn.Properties.Resources.Dashboard;
            this.buttonDashboard.Label = "Sales Dashboard";
            this.buttonDashboard.Name = "buttonDashboard";
            this.buttonDashboard.ScreenTip = "Launch Dashboard";
            this.buttonDashboard.ShowImage = true;
            this.buttonDashboard.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonDashboard_Click);
            // 
            // buttonSalesDepot
            // 
            this.buttonSalesDepot.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.buttonSalesDepot.Image = global::AdSalesAddIn.Properties.Resources.SalesDepot;
            this.buttonSalesDepot.Label = "View Library";
            this.buttonSalesDepot.Name = "buttonSalesDepot";
            this.buttonSalesDepot.ScreenTip = "Access Sales Library";
            this.buttonSalesDepot.ShowImage = true;
            this.buttonSalesDepot.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonSalesDepot_Click);
            // 
            // buttonSalesGallery
            // 
            this.buttonSalesGallery.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.buttonSalesGallery.Label = "Sales Gallery";
            this.buttonSalesGallery.Name = "buttonSalesGallery";
            this.buttonSalesGallery.ScreenTip = "Open Sales Clipart Gallery";
            this.buttonSalesGallery.ShowImage = true;
            this.buttonSalesGallery.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonSalesGallery_Click);
            // 
            // buttonClientLogos
            // 
            this.buttonClientLogos.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.buttonClientLogos.Label = "Client Logos";
            this.buttonClientLogos.Name = "buttonClientLogos";
            this.buttonClientLogos.ScreenTip = "Add Client Logos";
            this.buttonClientLogos.ShowImage = true;
            this.buttonClientLogos.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonClientLogos_Click);
            // 
            // buttonWebArt
            // 
            this.buttonWebArt.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.buttonWebArt.Label = "Web Art";
            this.buttonWebArt.Name = "buttonWebArt";
            this.buttonWebArt.ScreenTip = "Site Screenshots and Ad Samples";
            this.buttonWebArt.ShowImage = true;
            this.buttonWebArt.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonWebArt_Click);
            // 
            // buttonMinibarLoad
            // 
            this.buttonMinibarLoad.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.buttonMinibarLoad.Label = "Load";
            this.buttonMinibarLoad.Name = "buttonMinibarLoad";
            this.buttonMinibarLoad.ScreenTip = "Open Minibar";
            this.buttonMinibarLoad.ShowImage = true;
            this.buttonMinibarLoad.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonMinibar_Click);
            // 
            // buttonMinibarKill
            // 
            this.buttonMinibarKill.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.buttonMinibarKill.Label = "Kill";
            this.buttonMinibarKill.Name = "buttonMinibarKill";
            this.buttonMinibarKill.ScreenTip = "Kill Minibar";
            this.buttonMinibarKill.ShowImage = true;
            this.buttonMinibarKill.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonMinibarKill_Click);
            // 
            // ribbonAdSales
            // 
            this.Name = "ribbonAdSales";
            this.RibbonType = "Microsoft.PowerPoint.Presentation";
            this.Tabs.Add(this.tabAdSales);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.Ribbon_Load);
            this.tabAdSales.ResumeLayout(false);
            this.tabAdSales.PerformLayout();
            this.groupSlideFormat.ResumeLayout(false);
            this.groupSlideFormat.PerformLayout();
            this.buttonGroupSlideFormatLandscape.ResumeLayout(false);
            this.buttonGroupSlideFormatLandscape.PerformLayout();
            this.buttonGroupSlideFormatPortrait.ResumeLayout(false);
            this.buttonGroupSlideFormatPortrait.PerformLayout();
            this.groupStarterSlides.ResumeLayout(false);
            this.groupStarterSlides.PerformLayout();
            this.groupDashboard.ResumeLayout(false);
            this.groupDashboard.PerformLayout();
            this.groupSalesDepot.ResumeLayout(false);
            this.groupSalesDepot.PerformLayout();
            this.groupClipart.ResumeLayout(false);
            this.groupClipart.PerformLayout();
            this.groupMinibar.ResumeLayout(false);
            this.groupMinibar.PerformLayout();

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tabAdSales;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup groupDashboard;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonDashboard;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup groupSlideFormat;
        internal Microsoft.Office.Tools.Ribbon.RibbonButtonGroup buttonGroupSlideFormatLandscape;
        internal Microsoft.Office.Tools.Ribbon.RibbonToggleButton toggleButtonSlideFormat43;
        internal Microsoft.Office.Tools.Ribbon.RibbonToggleButton toggleButtonSlideFormat54;
        internal Microsoft.Office.Tools.Ribbon.RibbonToggleButton toggleButtonSlideFormat169;
        internal Microsoft.Office.Tools.Ribbon.RibbonButtonGroup buttonGroupSlideFormatPortrait;
        internal Microsoft.Office.Tools.Ribbon.RibbonToggleButton toggleButtonSlideFormat34;
        internal Microsoft.Office.Tools.Ribbon.RibbonToggleButton toggleButtonSlideFormat45;
        internal Microsoft.Office.Tools.Ribbon.RibbonDropDown dropDownWizards;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup groupStarterSlides;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonAddCover;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonAddSlide;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup groupSalesDepot;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonSalesDepot;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup groupClipart;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonSalesGallery;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonClientLogos;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonWebArt;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup groupApplications;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup groupMinibar;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonMinibarLoad;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonMinibarKill;
    }

    partial class ThisRibbonCollection
    {
        internal ribbonAdSales Ribbon1
        {
            get { return this.GetRibbon<ribbonAdSales>(); }
        }
    }
}
