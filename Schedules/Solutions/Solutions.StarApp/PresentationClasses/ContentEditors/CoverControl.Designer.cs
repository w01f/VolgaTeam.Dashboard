namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors
{
	sealed partial class CoverControl
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
			this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
			this.checkEditAddAsPageOne = new DevExpress.XtraEditors.CheckEdit();
			this.layoutControl = new DevExpress.XtraLayout.LayoutControl();
			this.comboBoxEditSlideHeader = new DevExpress.XtraEditors.ComboBoxEdit();
			this.pictureEditLogoRight = new DevExpress.XtraEditors.PictureEdit();
			this.pictureEditLogoFooter = new DevExpress.XtraEditors.PictureEdit();
			this.layoutControlGroupRoot = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItemSlideHeader = new DevExpress.XtraLayout.LayoutControlItem();
			this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
			this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
			this.layoutControlItemLogoRight = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItemLogoFooter = new DevExpress.XtraLayout.LayoutControlItem();
			this.tabbedControlGroupData = new DevExpress.XtraLayout.TabbedControlGroup();
			this.layoutControlGroupTabA = new DevExpress.XtraLayout.LayoutControlGroup();
			this.emptySpaceItemMain = new DevExpress.XtraLayout.EmptySpaceItem();
			this.layoutControlItemAddAsPageOne = new DevExpress.XtraLayout.LayoutControlItem();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditAddAsPageOne.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl)).BeginInit();
			this.layoutControl.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditSlideHeader.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureEditLogoRight.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureEditLogoFooter.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupRoot)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemSlideHeader)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemLogoRight)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemLogoFooter)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.tabbedControlGroupData)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupTabA)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemMain)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemAddAsPageOne)).BeginInit();
			this.SuspendLayout();
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
			// checkEditAddAsPageOne
			// 
			this.checkEditAddAsPageOne.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.checkEditAddAsPageOne.Location = new System.Drawing.Point(468, 15);
			this.checkEditAddAsPageOne.Name = "checkEditAddAsPageOne";
			this.checkEditAddAsPageOne.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
			this.checkEditAddAsPageOne.Properties.Caption = "<color=gray>Always Output to Page 1</color>";
			this.checkEditAddAsPageOne.Size = new System.Drawing.Size(187, 20);
			this.checkEditAddAsPageOne.StyleController = this.layoutControl;
			this.checkEditAddAsPageOne.TabIndex = 29;
			this.checkEditAddAsPageOne.CheckedChanged += new System.EventHandler(this.OnEditValueChanged);
			// 
			// layoutControl
			// 
			this.layoutControl.Appearance.Control.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.layoutControl.Appearance.Control.Options.UseFont = true;
			this.layoutControl.Appearance.ControlDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.layoutControl.Appearance.ControlDisabled.Options.UseFont = true;
			this.layoutControl.Appearance.ControlDropDown.Font = new System.Drawing.Font("Arial", 9.75F);
			this.layoutControl.Appearance.ControlDropDown.Options.UseFont = true;
			this.layoutControl.Appearance.ControlDropDownHeader.Font = new System.Drawing.Font("Arial", 9.75F);
			this.layoutControl.Appearance.ControlDropDownHeader.Options.UseFont = true;
			this.layoutControl.Appearance.ControlFocused.Font = new System.Drawing.Font("Arial", 9.75F);
			this.layoutControl.Appearance.ControlFocused.Options.UseFont = true;
			this.layoutControl.Appearance.ControlReadOnly.Font = new System.Drawing.Font("Arial", 9.75F);
			this.layoutControl.Appearance.ControlReadOnly.Options.UseFont = true;
			this.layoutControl.BackColor = System.Drawing.Color.White;
			this.layoutControl.Controls.Add(this.checkEditAddAsPageOne);
			this.layoutControl.Controls.Add(this.comboBoxEditSlideHeader);
			this.layoutControl.Controls.Add(this.pictureEditLogoRight);
			this.layoutControl.Controls.Add(this.pictureEditLogoFooter);
			this.layoutControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutControl.ForeColor = System.Drawing.Color.Black;
			this.layoutControl.Location = new System.Drawing.Point(0, 0);
			this.layoutControl.Name = "layoutControl";
			this.layoutControl.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(802, 383, 250, 350);
			this.layoutControl.OptionsFocus.AllowFocusGroups = false;
			this.layoutControl.OptionsFocus.AllowFocusReadonlyEditors = false;
			this.layoutControl.OptionsFocus.AllowFocusTabbedGroups = false;
			this.layoutControl.Root = this.layoutControlGroupRoot;
			this.layoutControl.Size = new System.Drawing.Size(977, 512);
			this.layoutControl.StyleController = this.styleController;
			this.layoutControl.TabIndex = 69;
			this.layoutControl.Text = "layoutControl1";
			// 
			// comboBoxEditSlideHeader
			// 
			this.comboBoxEditSlideHeader.Location = new System.Drawing.Point(12, 14);
			this.comboBoxEditSlideHeader.Name = "comboBoxEditSlideHeader";
			this.comboBoxEditSlideHeader.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.comboBoxEditSlideHeader.Properties.Appearance.Options.UseFont = true;
			this.comboBoxEditSlideHeader.Properties.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.comboBoxEditSlideHeader.Properties.AppearanceDisabled.Options.UseFont = true;
			this.comboBoxEditSlideHeader.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Arial", 9.75F);
			this.comboBoxEditSlideHeader.Properties.AppearanceDropDown.Options.UseFont = true;
			this.comboBoxEditSlideHeader.Properties.AppearanceFocused.Font = new System.Drawing.Font("Arial", 9.75F);
			this.comboBoxEditSlideHeader.Properties.AppearanceFocused.Options.UseFont = true;
			this.comboBoxEditSlideHeader.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("Arial", 9.75F);
			this.comboBoxEditSlideHeader.Properties.AppearanceReadOnly.Options.UseFont = true;
			this.comboBoxEditSlideHeader.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.comboBoxEditSlideHeader.Size = new System.Drawing.Size(356, 22);
			this.comboBoxEditSlideHeader.StyleController = this.layoutControl;
			this.comboBoxEditSlideHeader.TabIndex = 28;
			this.comboBoxEditSlideHeader.EditValueChanged += new System.EventHandler(this.OnEditValueChanged);
			// 
			// pictureEditLogoRight
			// 
			this.pictureEditLogoRight.Cursor = System.Windows.Forms.Cursors.Default;
			this.pictureEditLogoRight.Location = new System.Drawing.Point(667, 10);
			this.pictureEditLogoRight.Name = "pictureEditLogoRight";
	        this.pictureEditLogoRight.Properties.AllowFocused = false;
			this.pictureEditLogoRight.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.pictureEditLogoRight.Properties.NullText = " ";
			this.pictureEditLogoRight.Properties.PictureAlignment = System.Drawing.ContentAlignment.TopRight;
			this.pictureEditLogoRight.Properties.ReadOnly = true;
			this.pictureEditLogoRight.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
			this.pictureEditLogoRight.Properties.ShowMenu = false;
			this.pictureEditLogoRight.Properties.ZoomAccelerationFactor = 1D;
			this.pictureEditLogoRight.Size = new System.Drawing.Size(300, 492);
			this.pictureEditLogoRight.StyleController = this.layoutControl;
			this.pictureEditLogoRight.TabIndex = 4;
			// 
			// pictureEditLogoFooter
			// 
			this.pictureEditLogoFooter.Cursor = System.Windows.Forms.Cursors.Default;
			this.pictureEditLogoFooter.Location = new System.Drawing.Point(10, 422);
			this.pictureEditLogoFooter.Name = "pictureEditLogoFooter";
	        this.pictureEditLogoFooter.Properties.AllowFocused = false;
			this.pictureEditLogoFooter.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.pictureEditLogoFooter.Properties.NullText = " ";
			this.pictureEditLogoFooter.Properties.PictureAlignment = System.Drawing.ContentAlignment.BottomLeft;
			this.pictureEditLogoFooter.Properties.ReadOnly = true;
			this.pictureEditLogoFooter.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
			this.pictureEditLogoFooter.Properties.ShowMenu = false;
			this.pictureEditLogoFooter.Properties.ZoomAccelerationFactor = 1D;
			this.pictureEditLogoFooter.Size = new System.Drawing.Size(637, 80);
			this.pictureEditLogoFooter.StyleController = this.layoutControl;
			this.pictureEditLogoFooter.TabIndex = 29;
			// 
			// layoutControlGroupRoot
			// 
			this.layoutControlGroupRoot.AllowHtmlStringInCaption = true;
			this.layoutControlGroupRoot.AppearanceGroup.Font = new System.Drawing.Font("Arial", 9.75F);
			this.layoutControlGroupRoot.AppearanceGroup.Options.UseFont = true;
			this.layoutControlGroupRoot.AppearanceItemCaption.Font = new System.Drawing.Font("Arial", 9.75F);
			this.layoutControlGroupRoot.AppearanceItemCaption.Options.UseFont = true;
			this.layoutControlGroupRoot.AppearanceTabPage.Header.Font = new System.Drawing.Font("Arial", 9.75F);
			this.layoutControlGroupRoot.AppearanceTabPage.Header.Options.UseFont = true;
			this.layoutControlGroupRoot.AppearanceTabPage.HeaderActive.Font = new System.Drawing.Font("Arial", 9.75F);
			this.layoutControlGroupRoot.AppearanceTabPage.HeaderActive.Options.UseFont = true;
			this.layoutControlGroupRoot.AppearanceTabPage.HeaderDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.layoutControlGroupRoot.AppearanceTabPage.HeaderDisabled.Options.UseFont = true;
			this.layoutControlGroupRoot.AppearanceTabPage.HeaderHotTracked.Font = new System.Drawing.Font("Arial", 9.75F);
			this.layoutControlGroupRoot.AppearanceTabPage.HeaderHotTracked.Options.UseFont = true;
			this.layoutControlGroupRoot.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			this.layoutControlGroupRoot.GroupBordersVisible = false;
			this.layoutControlGroupRoot.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemSlideHeader,
            this.emptySpaceItem1,
            this.emptySpaceItem2,
            this.layoutControlItemLogoRight,
            this.layoutControlItemLogoFooter,
            this.tabbedControlGroupData,
            this.layoutControlItemAddAsPageOne});
			this.layoutControlGroupRoot.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroupRoot.Name = "Root";
			this.layoutControlGroupRoot.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroupRoot.Size = new System.Drawing.Size(977, 512);
			this.layoutControlGroupRoot.TextVisible = false;
			// 
			// layoutControlItemSlideHeader
			// 
			this.layoutControlItemSlideHeader.Control = this.comboBoxEditSlideHeader;
			this.layoutControlItemSlideHeader.ControlAlignment = System.Drawing.ContentAlignment.MiddleLeft;
			this.layoutControlItemSlideHeader.FillControlToClientArea = false;
			this.layoutControlItemSlideHeader.Location = new System.Drawing.Point(10, 0);
			this.layoutControlItemSlideHeader.MaxSize = new System.Drawing.Size(360, 50);
			this.layoutControlItemSlideHeader.MinSize = new System.Drawing.Size(360, 50);
			this.layoutControlItemSlideHeader.Name = "layoutControlItemSlideHeader";
			this.layoutControlItemSlideHeader.Size = new System.Drawing.Size(360, 50);
			this.layoutControlItemSlideHeader.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.layoutControlItemSlideHeader.Text = "Slide Header";
			this.layoutControlItemSlideHeader.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItemSlideHeader.TextVisible = false;
			this.layoutControlItemSlideHeader.TrimClientAreaToControl = false;
			// 
			// emptySpaceItem1
			// 
			this.emptySpaceItem1.AllowHotTrack = false;
			this.emptySpaceItem1.Location = new System.Drawing.Point(370, 0);
			this.emptySpaceItem1.Name = "emptySpaceItem1";
			this.emptySpaceItem1.Size = new System.Drawing.Size(96, 50);
			this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
			// 
			// emptySpaceItem2
			// 
			this.emptySpaceItem2.AllowHotTrack = false;
			this.emptySpaceItem2.Location = new System.Drawing.Point(0, 0);
			this.emptySpaceItem2.MaxSize = new System.Drawing.Size(10, 0);
			this.emptySpaceItem2.MinSize = new System.Drawing.Size(10, 10);
			this.emptySpaceItem2.Name = "emptySpaceItem2";
			this.emptySpaceItem2.Size = new System.Drawing.Size(10, 50);
			this.emptySpaceItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
			// 
			// layoutControlItemLogoRight
			// 
			this.layoutControlItemLogoRight.Control = this.pictureEditLogoRight;
			this.layoutControlItemLogoRight.ControlAlignment = System.Drawing.ContentAlignment.TopRight;
			this.layoutControlItemLogoRight.FillControlToClientArea = false;
			this.layoutControlItemLogoRight.Location = new System.Drawing.Point(657, 0);
			this.layoutControlItemLogoRight.MaxSize = new System.Drawing.Size(320, 0);
			this.layoutControlItemLogoRight.MinSize = new System.Drawing.Size(320, 1);
			this.layoutControlItemLogoRight.Name = "layoutControlItemLogoRight";
			this.layoutControlItemLogoRight.Padding = new DevExpress.XtraLayout.Utils.Padding(10, 10, 10, 10);
			this.layoutControlItemLogoRight.Size = new System.Drawing.Size(320, 512);
			this.layoutControlItemLogoRight.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.layoutControlItemLogoRight.Text = "Logo Right";
			this.layoutControlItemLogoRight.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItemLogoRight.TextVisible = false;
			this.layoutControlItemLogoRight.TrimClientAreaToControl = false;
			// 
			// layoutControlItemLogoFooter
			// 
			this.layoutControlItemLogoFooter.Control = this.pictureEditLogoFooter;
			this.layoutControlItemLogoFooter.ControlAlignment = System.Drawing.ContentAlignment.BottomLeft;
			this.layoutControlItemLogoFooter.FillControlToClientArea = false;
			this.layoutControlItemLogoFooter.Location = new System.Drawing.Point(0, 412);
			this.layoutControlItemLogoFooter.MaxSize = new System.Drawing.Size(0, 100);
			this.layoutControlItemLogoFooter.MinSize = new System.Drawing.Size(1, 100);
			this.layoutControlItemLogoFooter.Name = "layoutControlItemLogoFooter";
			this.layoutControlItemLogoFooter.Padding = new DevExpress.XtraLayout.Utils.Padding(10, 10, 10, 10);
			this.layoutControlItemLogoFooter.Size = new System.Drawing.Size(657, 100);
			this.layoutControlItemLogoFooter.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.layoutControlItemLogoFooter.Text = "Logo Footer";
			this.layoutControlItemLogoFooter.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItemLogoFooter.TextVisible = false;
			this.layoutControlItemLogoFooter.TrimClientAreaToControl = false;
			// 
			// tabbedControlGroupData
			// 
			this.tabbedControlGroupData.AppearanceTabPage.Header.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.tabbedControlGroupData.AppearanceTabPage.Header.Options.UseFont = true;
			this.tabbedControlGroupData.AppearanceTabPage.HeaderActive.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.tabbedControlGroupData.AppearanceTabPage.HeaderActive.Options.UseFont = true;
			this.tabbedControlGroupData.AppearanceTabPage.HeaderDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.tabbedControlGroupData.AppearanceTabPage.HeaderDisabled.Options.UseFont = true;
			this.tabbedControlGroupData.AppearanceTabPage.HeaderHotTracked.Font = new System.Drawing.Font("Arial", 9.75F);
			this.tabbedControlGroupData.AppearanceTabPage.HeaderHotTracked.Options.UseFont = true;
			this.tabbedControlGroupData.AppearanceTabPage.PageClient.Font = new System.Drawing.Font("Arial", 9.75F);
			this.tabbedControlGroupData.AppearanceTabPage.PageClient.Options.UseFont = true;
			this.tabbedControlGroupData.Location = new System.Drawing.Point(0, 50);
			this.tabbedControlGroupData.Name = "tabbedControlGroupData";
			this.tabbedControlGroupData.SelectedTabPage = this.layoutControlGroupTabA;
			this.tabbedControlGroupData.SelectedTabPageIndex = 0;
			this.tabbedControlGroupData.Size = new System.Drawing.Size(657, 362);
			this.tabbedControlGroupData.TabPages.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroupTabA});
			this.tabbedControlGroupData.SelectedPageChanged += new DevExpress.XtraLayout.LayoutTabPageChangedEventHandler(this.OnSelectedPageChanged);
			// 
			// layoutControlGroupTabA
			// 
			this.layoutControlGroupTabA.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItemMain});
			this.layoutControlGroupTabA.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroupTabA.Name = "layoutControlGroupTabA";
			this.layoutControlGroupTabA.Size = new System.Drawing.Size(633, 313);
			this.layoutControlGroupTabA.Text = "Tab A";
			// 
			// emptySpaceItemMain
			// 
			this.emptySpaceItemMain.AllowHotTrack = false;
			this.emptySpaceItemMain.Location = new System.Drawing.Point(0, 0);
			this.emptySpaceItemMain.Name = "emptySpaceItemMain";
			this.emptySpaceItemMain.Size = new System.Drawing.Size(633, 313);
			this.emptySpaceItemMain.TextSize = new System.Drawing.Size(0, 0);
			// 
			// layoutControlItemAddAsPageOne
			// 
			this.layoutControlItemAddAsPageOne.Control = this.checkEditAddAsPageOne;
			this.layoutControlItemAddAsPageOne.ControlAlignment = System.Drawing.ContentAlignment.MiddleRight;
			this.layoutControlItemAddAsPageOne.FillControlToClientArea = false;
			this.layoutControlItemAddAsPageOne.Location = new System.Drawing.Point(466, 0);
			this.layoutControlItemAddAsPageOne.Name = "layoutControlItemAddAsPageOne";
			this.layoutControlItemAddAsPageOne.Size = new System.Drawing.Size(191, 50);
			this.layoutControlItemAddAsPageOne.Text = "Add As Page One";
			this.layoutControlItemAddAsPageOne.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItemAddAsPageOne.TextVisible = false;
			this.layoutControlItemAddAsPageOne.TrimClientAreaToControl = false;
			// 
			// CoverControl
			// 
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.layoutControl);
			this.Name = "CoverControl";
			this.Size = new System.Drawing.Size(977, 512);
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditAddAsPageOne.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl)).EndInit();
			this.layoutControl.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditSlideHeader.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureEditLogoRight.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureEditLogoFooter.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupRoot)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemSlideHeader)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemLogoRight)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemLogoFooter)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.tabbedControlGroupData)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupTabA)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemMain)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemAddAsPageOne)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion
		private DevExpress.XtraEditors.StyleController styleController;
		private DevExpress.XtraEditors.CheckEdit checkEditAddAsPageOne;
		protected DevExpress.XtraLayout.LayoutControl layoutControl;
		protected DevExpress.XtraEditors.ComboBoxEdit comboBoxEditSlideHeader;
		protected DevExpress.XtraEditors.PictureEdit pictureEditLogoRight;
		private DevExpress.XtraEditors.PictureEdit pictureEditLogoFooter;
		protected DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupRoot;
		protected DevExpress.XtraLayout.LayoutControlItem layoutControlItemSlideHeader;
		protected DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
		private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
		protected DevExpress.XtraLayout.LayoutControlItem layoutControlItemLogoRight;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItemLogoFooter;
		private DevExpress.XtraLayout.TabbedControlGroup tabbedControlGroupData;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupTabA;
		private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItemMain;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItemAddAsPageOne;
	}
}
