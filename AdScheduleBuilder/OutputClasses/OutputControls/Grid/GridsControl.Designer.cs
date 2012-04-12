namespace AdScheduleBuilder.OutputClasses.OutputControls
{
    partial class GridsControl
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
            this.pnEmpty = new System.Windows.Forms.Panel();
            this.pnMain = new System.Windows.Forms.Panel();
            this.splitContainerControl = new DevExpress.XtraEditors.SplitContainerControl();
            this.navBarControlDetails = new DevExpress.XtraNavBar.NavBarControl();
            this.navBarGroupAdNotes = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroupControlContainerAdNotes = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.navBarGroupControlContainerSlideBulltes = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.navBarGroupControlContainerSlideHeader = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.navBarGroupSlideHeader = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroupSlideBullets = new DevExpress.XtraNavBar.NavBarGroup();
            this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl)).BeginInit();
            this.splitContainerControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControlDetails)).BeginInit();
            this.navBarControlDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
            this.SuspendLayout();
            // 
            // pnEmpty
            // 
            this.pnEmpty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnEmpty.Location = new System.Drawing.Point(0, 0);
            this.pnEmpty.Name = "pnEmpty";
            this.pnEmpty.Size = new System.Drawing.Size(453, 490);
            this.pnEmpty.TabIndex = 0;
            // 
            // pnMain
            // 
            this.pnMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnMain.Location = new System.Drawing.Point(0, 0);
            this.pnMain.Name = "pnMain";
            this.pnMain.Size = new System.Drawing.Size(453, 490);
            this.pnMain.TabIndex = 1;
            // 
            // splitContainerControl
            // 
            this.splitContainerControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl.Name = "splitContainerControl";
            this.splitContainerControl.Panel1.Controls.Add(this.navBarControlDetails);
            this.splitContainerControl.Panel1.MinSize = 250;
            this.splitContainerControl.Panel1.Text = "Panel1";
            this.splitContainerControl.Panel2.Controls.Add(this.pnMain);
            this.splitContainerControl.Panel2.Controls.Add(this.pnEmpty);
            this.splitContainerControl.Panel2.Text = "Panel2";
            this.splitContainerControl.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel2;
            this.splitContainerControl.Size = new System.Drawing.Size(453, 490);
            this.splitContainerControl.SplitterPosition = 250;
            this.splitContainerControl.TabIndex = 2;
            this.splitContainerControl.Text = "splitContainerControl";
            // 
            // navBarControlDetails
            // 
            this.navBarControlDetails.ActiveGroup = this.navBarGroupAdNotes;
            this.navBarControlDetails.BackColor = System.Drawing.Color.Transparent;
            this.navBarControlDetails.Controls.Add(this.navBarGroupControlContainerAdNotes);
            this.navBarControlDetails.Controls.Add(this.navBarGroupControlContainerSlideBulltes);
            this.navBarControlDetails.Controls.Add(this.navBarGroupControlContainerSlideHeader);
            this.navBarControlDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navBarControlDetails.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.navBarGroupAdNotes,
            this.navBarGroupSlideHeader,
            this.navBarGroupSlideBullets});
            this.navBarControlDetails.Location = new System.Drawing.Point(0, 0);
            this.navBarControlDetails.Name = "navBarControlDetails";
            this.navBarControlDetails.OptionsNavPane.ExpandedWidth = 212;
            this.navBarControlDetails.OptionsNavPane.ShowExpandButton = false;
            this.navBarControlDetails.OptionsNavPane.ShowOverflowButton = false;
            this.navBarControlDetails.OptionsNavPane.ShowOverflowPanel = false;
            this.navBarControlDetails.OptionsNavPane.ShowSplitter = false;
            this.navBarControlDetails.PaintStyleKind = DevExpress.XtraNavBar.NavBarViewKind.ExplorerBar;
            this.navBarControlDetails.Size = new System.Drawing.Size(0, 0);
            this.navBarControlDetails.TabIndex = 0;
            this.navBarControlDetails.Text = "navBarControl1";
            this.navBarControlDetails.View = new DevExpress.XtraNavBar.ViewInfo.SkinNavigationPaneViewInfoRegistrator();
            // 
            // navBarGroupAdNotes
            // 
            this.navBarGroupAdNotes.Appearance.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.navBarGroupAdNotes.Appearance.Options.UseFont = true;
            this.navBarGroupAdNotes.Caption = "Ad Notes";
            this.navBarGroupAdNotes.ControlContainer = this.navBarGroupControlContainerAdNotes;
            this.navBarGroupAdNotes.Expanded = true;
            this.navBarGroupAdNotes.GroupCaptionUseImage = DevExpress.XtraNavBar.NavBarImage.Small;
            this.navBarGroupAdNotes.GroupClientHeight = 80;
            this.navBarGroupAdNotes.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navBarGroupAdNotes.Name = "navBarGroupAdNotes";
            this.navBarGroupAdNotes.SmallImage = global::AdScheduleBuilder.Properties.Resources.GridAdNotes;
            // 
            // navBarGroupControlContainerAdNotes
            // 
            this.navBarGroupControlContainerAdNotes.Name = "navBarGroupControlContainerAdNotes";
            this.navBarGroupControlContainerAdNotes.Size = new System.Drawing.Size(248, 330);
            this.navBarGroupControlContainerAdNotes.TabIndex = 1;
            // 
            // navBarGroupControlContainerSlideBulltes
            // 
            this.navBarGroupControlContainerSlideBulltes.Name = "navBarGroupControlContainerSlideBulltes";
            this.navBarGroupControlContainerSlideBulltes.Size = new System.Drawing.Size(210, 259);
            this.navBarGroupControlContainerSlideBulltes.TabIndex = 0;
            // 
            // navBarGroupControlContainerSlideHeader
            // 
            this.navBarGroupControlContainerSlideHeader.Name = "navBarGroupControlContainerSlideHeader";
            this.navBarGroupControlContainerSlideHeader.Size = new System.Drawing.Size(248, 330);
            this.navBarGroupControlContainerSlideHeader.TabIndex = 2;
            // 
            // navBarGroupSlideHeader
            // 
            this.navBarGroupSlideHeader.Appearance.Font = new System.Drawing.Font("Arial", 15.75F);
            this.navBarGroupSlideHeader.Appearance.Options.UseFont = true;
            this.navBarGroupSlideHeader.Caption = "Slide Headers";
            this.navBarGroupSlideHeader.ControlContainer = this.navBarGroupControlContainerSlideHeader;
            this.navBarGroupSlideHeader.GroupCaptionUseImage = DevExpress.XtraNavBar.NavBarImage.Small;
            this.navBarGroupSlideHeader.GroupClientHeight = 80;
            this.navBarGroupSlideHeader.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navBarGroupSlideHeader.Name = "navBarGroupSlideHeader";
            this.navBarGroupSlideHeader.SmallImage = global::AdScheduleBuilder.Properties.Resources.SlideHeader;
            // 
            // navBarGroupSlideBullets
            // 
            this.navBarGroupSlideBullets.Appearance.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.navBarGroupSlideBullets.Appearance.Options.UseFont = true;
            this.navBarGroupSlideBullets.Caption = "Schedule Totals";
            this.navBarGroupSlideBullets.ControlContainer = this.navBarGroupControlContainerSlideBulltes;
            this.navBarGroupSlideBullets.GroupCaptionUseImage = DevExpress.XtraNavBar.NavBarImage.Small;
            this.navBarGroupSlideBullets.GroupClientHeight = 80;
            this.navBarGroupSlideBullets.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navBarGroupSlideBullets.Name = "navBarGroupSlideBullets";
            this.navBarGroupSlideBullets.SmallImage = global::AdScheduleBuilder.Properties.Resources.SlideBullets;
            // 
            // defaultLookAndFeel
            // 
            this.defaultLookAndFeel.LookAndFeel.SkinName = "Money Twins";
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
            // GridsControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.splitContainerControl);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "GridsControl";
            this.Size = new System.Drawing.Size(453, 490);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl)).EndInit();
            this.splitContainerControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.navBarControlDetails)).EndInit();
            this.navBarControlDetails.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnEmpty;
        private System.Windows.Forms.Panel pnMain;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl;
        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
        private DevExpress.XtraNavBar.NavBarControl navBarControlDetails;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroupAdNotes;
        private DevExpress.XtraEditors.StyleController styleController;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainerAdNotes;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainerSlideBulltes;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroupSlideBullets;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroupSlideHeader;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainerSlideHeader;
    }
}
