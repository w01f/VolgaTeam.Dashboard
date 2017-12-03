
namespace Asa.Calendar.Controls.PresentationClasses.Calendars
{
	public abstract partial class BaseCalendarControl<TPartitionContet, TSchedule, TScheduleSettings, TChangeInfo>
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
			this.styleController = new DevExpress.XtraEditors.StyleController();
			this.pnMain = new System.Windows.Forms.Panel();
			this.retractableBarControl = new Asa.Common.GUI.RetractableBar.RetractableBarLeft();
			this.pnData = new System.Windows.Forms.Panel();
			this.layoutControlData = new DevExpress.XtraLayout.LayoutControl();
			this.layoutControlGroupRootData = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItemContainer = new DevExpress.XtraLayout.LayoutControlItem();
			this.emptySpaceItemSplash = new DevExpress.XtraLayout.EmptySpaceItem();
			this.layoutControlMain = new DevExpress.XtraLayout.LayoutControl();
			this.pictureEditDefaultLogo = new DevExpress.XtraEditors.PictureEdit();
			this.layoutControlGroupRootMain = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItemDefaultLogo = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItemData = new DevExpress.XtraLayout.LayoutControlItem();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			this.pnData.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlData)).BeginInit();
			this.layoutControlData.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupRootData)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemContainer)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemSplash)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlMain)).BeginInit();
			this.layoutControlMain.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureEditDefaultLogo.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupRootMain)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDefaultLogo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemData)).BeginInit();
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
			// pnMain
			// 
			this.pnMain.Location = new System.Drawing.Point(0, 0);
			this.pnMain.Name = "pnMain";
			this.pnMain.Size = new System.Drawing.Size(136, 499);
			this.pnMain.TabIndex = 4;
			// 
			// retractableBarControl
			// 
			this.retractableBarControl.AnimationDelay = 0;
			this.retractableBarControl.BackColor = System.Drawing.Color.Transparent;
			// 
			// retractableBarControl.Content
			// 
			this.retractableBarControl.Content.Dock = System.Windows.Forms.DockStyle.Fill;
			this.retractableBarControl.Content.Location = new System.Drawing.Point(2, 42);
			this.retractableBarControl.Content.Name = "Content";
			this.retractableBarControl.Content.Size = new System.Drawing.Size(296, 455);
			this.retractableBarControl.Content.TabIndex = 1;
			this.retractableBarControl.ContentSize = 300;
			this.retractableBarControl.Dock = System.Windows.Forms.DockStyle.Left;
			this.retractableBarControl.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			// 
			// retractableBarControl.Header
			// 
			this.retractableBarControl.Header.Dock = System.Windows.Forms.DockStyle.Fill;
			this.retractableBarControl.Header.Location = new System.Drawing.Point(49, 2);
			this.retractableBarControl.Header.Name = "Header";
			this.retractableBarControl.Header.Size = new System.Drawing.Size(245, 36);
			this.retractableBarControl.Header.TabIndex = 2;
			this.retractableBarControl.Location = new System.Drawing.Point(0, 0);
			this.retractableBarControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.retractableBarControl.Name = "retractableBarControl";
			this.retractableBarControl.Size = new System.Drawing.Size(300, 499);
			this.retractableBarControl.TabIndex = 6;
			// 
			// pnData
			// 
			this.pnData.Controls.Add(this.layoutControlData);
			this.pnData.Controls.Add(this.retractableBarControl);
			this.pnData.Location = new System.Drawing.Point(80, 20);
			this.pnData.Name = "pnData";
			this.pnData.Size = new System.Drawing.Size(573, 499);
			this.pnData.TabIndex = 7;
			// 
			// layoutControlData
			// 
			this.layoutControlData.Appearance.Control.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.layoutControlData.Appearance.Control.Options.UseFont = true;
			this.layoutControlData.Appearance.ControlDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.layoutControlData.Appearance.ControlDisabled.Options.UseFont = true;
			this.layoutControlData.Appearance.ControlDropDown.Font = new System.Drawing.Font("Arial", 9.75F);
			this.layoutControlData.Appearance.ControlDropDown.Options.UseFont = true;
			this.layoutControlData.Appearance.ControlDropDownHeader.Font = new System.Drawing.Font("Arial", 9.75F);
			this.layoutControlData.Appearance.ControlDropDownHeader.Options.UseFont = true;
			this.layoutControlData.Appearance.ControlFocused.Font = new System.Drawing.Font("Arial", 9.75F);
			this.layoutControlData.Appearance.ControlFocused.Options.UseFont = true;
			this.layoutControlData.Appearance.ControlReadOnly.Font = new System.Drawing.Font("Arial", 9.75F);
			this.layoutControlData.Appearance.ControlReadOnly.Options.UseFont = true;
			this.layoutControlData.BackColor = System.Drawing.Color.White;
			this.layoutControlData.Controls.Add(this.pnMain);
			this.layoutControlData.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutControlData.ForeColor = System.Drawing.Color.Black;
			this.layoutControlData.Location = new System.Drawing.Point(300, 0);
			this.layoutControlData.Name = "layoutControlData";
			this.layoutControlData.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(802, 383, 250, 350);
			this.layoutControlData.Root = this.layoutControlGroupRootData;
			this.layoutControlData.Size = new System.Drawing.Size(273, 499);
			this.layoutControlData.StyleController = this.styleController;
			this.layoutControlData.TabIndex = 65;
			this.layoutControlData.Text = "layoutControl1";
			// 
			// layoutControlGroupRootData
			// 
			this.layoutControlGroupRootData.AllowHtmlStringInCaption = true;
			this.layoutControlGroupRootData.AppearanceGroup.Font = new System.Drawing.Font("Arial", 9.75F);
			this.layoutControlGroupRootData.AppearanceGroup.Options.UseFont = true;
			this.layoutControlGroupRootData.AppearanceItemCaption.Font = new System.Drawing.Font("Arial", 9.75F);
			this.layoutControlGroupRootData.AppearanceItemCaption.Options.UseFont = true;
			this.layoutControlGroupRootData.AppearanceTabPage.Header.Font = new System.Drawing.Font("Arial", 9.75F);
			this.layoutControlGroupRootData.AppearanceTabPage.Header.Options.UseFont = true;
			this.layoutControlGroupRootData.AppearanceTabPage.HeaderActive.Font = new System.Drawing.Font("Arial", 9.75F);
			this.layoutControlGroupRootData.AppearanceTabPage.HeaderActive.Options.UseFont = true;
			this.layoutControlGroupRootData.AppearanceTabPage.HeaderDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.layoutControlGroupRootData.AppearanceTabPage.HeaderDisabled.Options.UseFont = true;
			this.layoutControlGroupRootData.AppearanceTabPage.HeaderHotTracked.Font = new System.Drawing.Font("Arial", 9.75F);
			this.layoutControlGroupRootData.AppearanceTabPage.HeaderHotTracked.Options.UseFont = true;
			this.layoutControlGroupRootData.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			this.layoutControlGroupRootData.GroupBordersVisible = false;
			this.layoutControlGroupRootData.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemContainer,
            this.emptySpaceItemSplash});
			this.layoutControlGroupRootData.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroupRootData.Name = "Root";
			this.layoutControlGroupRootData.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroupRootData.Size = new System.Drawing.Size(273, 499);
			this.layoutControlGroupRootData.TextVisible = false;
			// 
			// layoutControlItemContainer
			// 
			this.layoutControlItemContainer.Control = this.pnMain;
			this.layoutControlItemContainer.ControlAlignment = System.Drawing.ContentAlignment.MiddleCenter;
			this.layoutControlItemContainer.FillControlToClientArea = false;
			this.layoutControlItemContainer.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItemContainer.Name = "layoutControlItemContainer";
			this.layoutControlItemContainer.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItemContainer.Size = new System.Drawing.Size(136, 499);
			this.layoutControlItemContainer.Text = "Container";
			this.layoutControlItemContainer.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItemContainer.TextVisible = false;
			this.layoutControlItemContainer.TrimClientAreaToControl = false;
			// 
			// emptySpaceItemSplash
			// 
			this.emptySpaceItemSplash.AllowHotTrack = false;
			this.emptySpaceItemSplash.Location = new System.Drawing.Point(136, 0);
			this.emptySpaceItemSplash.Name = "emptySpaceItemSplash";
			this.emptySpaceItemSplash.Size = new System.Drawing.Size(137, 499);
			this.emptySpaceItemSplash.TextSize = new System.Drawing.Size(0, 0);
			this.emptySpaceItemSplash.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
			// 
			// layoutControlMain
			// 
			this.layoutControlMain.Appearance.Control.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.layoutControlMain.Appearance.Control.Options.UseFont = true;
			this.layoutControlMain.Appearance.ControlDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.layoutControlMain.Appearance.ControlDisabled.Options.UseFont = true;
			this.layoutControlMain.Appearance.ControlDropDown.Font = new System.Drawing.Font("Arial", 9.75F);
			this.layoutControlMain.Appearance.ControlDropDown.Options.UseFont = true;
			this.layoutControlMain.Appearance.ControlDropDownHeader.Font = new System.Drawing.Font("Arial", 9.75F);
			this.layoutControlMain.Appearance.ControlDropDownHeader.Options.UseFont = true;
			this.layoutControlMain.Appearance.ControlFocused.Font = new System.Drawing.Font("Arial", 9.75F);
			this.layoutControlMain.Appearance.ControlFocused.Options.UseFont = true;
			this.layoutControlMain.Appearance.ControlReadOnly.Font = new System.Drawing.Font("Arial", 9.75F);
			this.layoutControlMain.Appearance.ControlReadOnly.Options.UseFont = true;
			this.layoutControlMain.BackColor = System.Drawing.Color.White;
			this.layoutControlMain.Controls.Add(this.pnData);
			this.layoutControlMain.Controls.Add(this.pictureEditDefaultLogo);
			this.layoutControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutControlMain.ForeColor = System.Drawing.Color.Black;
			this.layoutControlMain.Location = new System.Drawing.Point(0, 0);
			this.layoutControlMain.Name = "layoutControlMain";
			this.layoutControlMain.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(802, 383, 250, 350);
			this.layoutControlMain.Root = this.layoutControlGroupRootMain;
			this.layoutControlMain.Size = new System.Drawing.Size(653, 519);
			this.layoutControlMain.StyleController = this.styleController;
			this.layoutControlMain.TabIndex = 65;
			this.layoutControlMain.Text = "layoutControl1";
			// 
			// pictureEditDefaultLogo
			// 
			this.pictureEditDefaultLogo.Cursor = System.Windows.Forms.Cursors.Default;
			this.pictureEditDefaultLogo.Location = new System.Drawing.Point(40, 40);
			this.pictureEditDefaultLogo.Name = "pictureEditDefaultLogo";
			this.pictureEditDefaultLogo.Properties.AllowFocused = false;
			this.pictureEditDefaultLogo.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.pictureEditDefaultLogo.Properties.PictureAlignment = System.Drawing.ContentAlignment.TopLeft;
			this.pictureEditDefaultLogo.Properties.ReadOnly = true;
			this.pictureEditDefaultLogo.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
			this.pictureEditDefaultLogo.Properties.ShowMenu = false;
			this.pictureEditDefaultLogo.Properties.ZoomAccelerationFactor = 1D;
			this.pictureEditDefaultLogo.Size = new System.Drawing.Size(20, 459);
			this.pictureEditDefaultLogo.StyleController = this.layoutControlMain;
			this.pictureEditDefaultLogo.TabIndex = 4;
			// 
			// layoutControlGroupRootMain
			// 
			this.layoutControlGroupRootMain.AllowHtmlStringInCaption = true;
			this.layoutControlGroupRootMain.AppearanceGroup.Font = new System.Drawing.Font("Arial", 9.75F);
			this.layoutControlGroupRootMain.AppearanceGroup.Options.UseFont = true;
			this.layoutControlGroupRootMain.AppearanceItemCaption.Font = new System.Drawing.Font("Arial", 9.75F);
			this.layoutControlGroupRootMain.AppearanceItemCaption.Options.UseFont = true;
			this.layoutControlGroupRootMain.AppearanceTabPage.Header.Font = new System.Drawing.Font("Arial", 9.75F);
			this.layoutControlGroupRootMain.AppearanceTabPage.Header.Options.UseFont = true;
			this.layoutControlGroupRootMain.AppearanceTabPage.HeaderActive.Font = new System.Drawing.Font("Arial", 9.75F);
			this.layoutControlGroupRootMain.AppearanceTabPage.HeaderActive.Options.UseFont = true;
			this.layoutControlGroupRootMain.AppearanceTabPage.HeaderDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.layoutControlGroupRootMain.AppearanceTabPage.HeaderDisabled.Options.UseFont = true;
			this.layoutControlGroupRootMain.AppearanceTabPage.HeaderHotTracked.Font = new System.Drawing.Font("Arial", 9.75F);
			this.layoutControlGroupRootMain.AppearanceTabPage.HeaderHotTracked.Options.UseFont = true;
			this.layoutControlGroupRootMain.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			this.layoutControlGroupRootMain.GroupBordersVisible = false;
			this.layoutControlGroupRootMain.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemDefaultLogo,
            this.layoutControlItemData});
			this.layoutControlGroupRootMain.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroupRootMain.Name = "Root";
			this.layoutControlGroupRootMain.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 20, 0);
			this.layoutControlGroupRootMain.Size = new System.Drawing.Size(653, 519);
			this.layoutControlGroupRootMain.TextVisible = false;
			// 
			// layoutControlItemDefaultLogo
			// 
			this.layoutControlItemDefaultLogo.Control = this.pictureEditDefaultLogo;
			this.layoutControlItemDefaultLogo.FillControlToClientArea = false;
			this.layoutControlItemDefaultLogo.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItemDefaultLogo.Name = "layoutControlItemDefaultLogo";
			this.layoutControlItemDefaultLogo.Padding = new DevExpress.XtraLayout.Utils.Padding(40, 20, 20, 20);
			this.layoutControlItemDefaultLogo.Size = new System.Drawing.Size(80, 499);
			this.layoutControlItemDefaultLogo.Text = "Default Logo";
			this.layoutControlItemDefaultLogo.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItemDefaultLogo.TextVisible = false;
			this.layoutControlItemDefaultLogo.TrimClientAreaToControl = false;
			this.layoutControlItemDefaultLogo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
			// 
			// layoutControlItemData
			// 
			this.layoutControlItemData.Control = this.pnData;
			this.layoutControlItemData.FillControlToClientArea = false;
			this.layoutControlItemData.Location = new System.Drawing.Point(80, 0);
			this.layoutControlItemData.Name = "layoutControlItemData";
			this.layoutControlItemData.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItemData.Size = new System.Drawing.Size(573, 499);
			this.layoutControlItemData.Text = "Data";
			this.layoutControlItemData.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItemData.TextVisible = false;
			this.layoutControlItemData.TrimClientAreaToControl = false;
			this.layoutControlItemData.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
			// 
			// BaseCalendarControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.layoutControlMain);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "BaseCalendarControl";
			this.Size = new System.Drawing.Size(653, 519);
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			this.pnData.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.layoutControlData)).EndInit();
			this.layoutControlData.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupRootData)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemContainer)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemSplash)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlMain)).EndInit();
			this.layoutControlMain.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictureEditDefaultLogo.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupRootMain)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDefaultLogo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemData)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion
		private DevExpress.XtraEditors.StyleController styleController;
		protected System.Windows.Forms.Panel pnMain;
		protected Asa.Common.GUI.RetractableBar.RetractableBarLeft retractableBarControl;
		private System.Windows.Forms.Panel pnData;
		private DevExpress.XtraLayout.LayoutControl layoutControlData;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupRootData;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItemContainer;
		private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItemSplash;
		private DevExpress.XtraLayout.LayoutControl layoutControlMain;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupRootMain;
	    protected DevExpress.XtraLayout.LayoutControlItem layoutControlItemDefaultLogo;
		protected DevExpress.XtraEditors.PictureEdit pictureEditDefaultLogo;
	    protected DevExpress.XtraLayout.LayoutControlItem layoutControlItemData;
	}
}
