namespace CustomSlidesAddIn
{
    partial class Ribbon : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public Ribbon()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
            LoadCustomSlides();
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
            this.tabHome = this.Factory.CreateRibbonTab();
            this.groupCustomSlides = this.Factory.CreateRibbonGroup();
            this.boxLandscape = this.Factory.CreateRibbonBox();
            this.toggleButtonSlideFormat43 = this.Factory.CreateRibbonToggleButton();
            this.toggleButtonSlideFormat54 = this.Factory.CreateRibbonToggleButton();
            this.toggleButtonSlideFormat169 = this.Factory.CreateRibbonToggleButton();
            this.boxPortrait = this.Factory.CreateRibbonBox();
            this.toggleButtonSlideFormat34 = this.Factory.CreateRibbonToggleButton();
            this.toggleButtonSlideFormat45 = this.Factory.CreateRibbonToggleButton();
            this.tabHome.SuspendLayout();
            this.boxLandscape.SuspendLayout();
            this.boxPortrait.SuspendLayout();
            // 
            // tabHome
            // 
            this.tabHome.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.tabHome.ControlId.OfficeId = "TabHome";
            this.tabHome.Groups.Add(this.groupCustomSlides);
            this.tabHome.Label = "TabHome";
            this.tabHome.Name = "tabHome";
            // 
            // groupCustomSlides
            // 
            this.groupCustomSlides.Label = "Custom Slides";
            this.groupCustomSlides.Name = "groupCustomSlides";
            this.groupCustomSlides.Position = this.Factory.RibbonPosition.BeforeOfficeId("GroupFont");
            // 
            // boxLandscape
            // 
            this.boxLandscape.BoxStyle = Microsoft.Office.Tools.Ribbon.RibbonBoxStyle.Vertical;
            this.boxLandscape.Items.Add(this.toggleButtonSlideFormat43);
            this.boxLandscape.Items.Add(this.toggleButtonSlideFormat54);
            this.boxLandscape.Items.Add(this.toggleButtonSlideFormat169);
            this.boxLandscape.Name = "boxLandscape";
            // 
            // toggleButtonSlideFormat43
            // 
            this.toggleButtonSlideFormat43.Label = "4 x 3";
            this.toggleButtonSlideFormat43.Name = "toggleButtonSlideFormat43";
            this.toggleButtonSlideFormat43.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.toggleButtonSlideFormat_Click);
            // 
            // toggleButtonSlideFormat54
            // 
            this.toggleButtonSlideFormat54.Label = "5 x 4";
            this.toggleButtonSlideFormat54.Name = "toggleButtonSlideFormat54";
            this.toggleButtonSlideFormat54.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.toggleButtonSlideFormat_Click);
            // 
            // toggleButtonSlideFormat169
            // 
            this.toggleButtonSlideFormat169.Label = "16 x 9";
            this.toggleButtonSlideFormat169.Name = "toggleButtonSlideFormat169";
            this.toggleButtonSlideFormat169.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.toggleButtonSlideFormat_Click);
            // 
            // boxPortrait
            // 
            this.boxPortrait.BoxStyle = Microsoft.Office.Tools.Ribbon.RibbonBoxStyle.Vertical;
            this.boxPortrait.Items.Add(this.toggleButtonSlideFormat34);
            this.boxPortrait.Items.Add(this.toggleButtonSlideFormat45);
            this.boxPortrait.Name = "boxPortrait";
            // 
            // toggleButtonSlideFormat34
            // 
            this.toggleButtonSlideFormat34.Label = "3 x 4";
            this.toggleButtonSlideFormat34.Name = "toggleButtonSlideFormat34";
            this.toggleButtonSlideFormat34.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.toggleButtonSlideFormat_Click);
            // 
            // toggleButtonSlideFormat45
            // 
            this.toggleButtonSlideFormat45.Label = "4 x 5";
            this.toggleButtonSlideFormat45.Name = "toggleButtonSlideFormat45";
            this.toggleButtonSlideFormat45.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.toggleButtonSlideFormat_Click);
            // 
            // Ribbon
            // 
            this.Name = "Ribbon";
            this.RibbonType = "Microsoft.PowerPoint.Presentation";
            this.Tabs.Add(this.tabHome);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.Ribbon_Load);
            this.tabHome.ResumeLayout(false);
            this.tabHome.PerformLayout();
            this.boxLandscape.ResumeLayout(false);
            this.boxLandscape.PerformLayout();
            this.boxPortrait.ResumeLayout(false);
            this.boxPortrait.PerformLayout();

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tabHome;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup groupCustomSlides;
        internal Microsoft.Office.Tools.Ribbon.RibbonToggleButton toggleButtonSlideFormat43;
        internal Microsoft.Office.Tools.Ribbon.RibbonToggleButton toggleButtonSlideFormat54;
        internal Microsoft.Office.Tools.Ribbon.RibbonToggleButton toggleButtonSlideFormat169;
        internal Microsoft.Office.Tools.Ribbon.RibbonToggleButton toggleButtonSlideFormat34;
        internal Microsoft.Office.Tools.Ribbon.RibbonToggleButton toggleButtonSlideFormat45;
        internal Microsoft.Office.Tools.Ribbon.RibbonBox boxPortrait;
        internal Microsoft.Office.Tools.Ribbon.RibbonBox boxLandscape;
    }

    partial class ThisRibbonCollection
    {
        internal Ribbon Ribbon1
        {
            get { return this.GetRibbon<Ribbon>(); }
        }
    }
}
