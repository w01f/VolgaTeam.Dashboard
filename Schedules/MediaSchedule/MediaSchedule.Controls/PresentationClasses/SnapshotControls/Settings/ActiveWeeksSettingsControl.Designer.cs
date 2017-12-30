namespace Asa.Media.Controls.PresentationClasses.SnapshotControls.Settings
{
	partial class ActiveWeeksSettingsControl
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
			this.checkedListBoxActiveWeeks = new DevExpress.XtraEditors.CheckedListBoxControl();
			this.buttonXClearAll = new DevComponents.DotNetBar.ButtonX();
			this.buttonXSelectAll = new DevComponents.DotNetBar.ButtonX();
			this.layoutControl = new DevExpress.XtraLayout.LayoutControl();
			this.layoutControlGroupRoot = new DevExpress.XtraLayout.LayoutControlGroup();
			this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
			this.layoutControlItemActiveWeeks = new DevExpress.XtraLayout.LayoutControlItem();
			this.simpleLabelItemWarning = new DevExpress.XtraLayout.SimpleLabelItem();
			this.simpleLabelItemTitle = new DevExpress.XtraLayout.SimpleLabelItem();
			this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
			this.layoutControlItemSelectAll = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItemClearAll = new DevExpress.XtraLayout.LayoutControlItem();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkedListBoxActiveWeeks)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl)).BeginInit();
			this.layoutControl.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupRoot)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemActiveWeeks)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.simpleLabelItemWarning)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.simpleLabelItemTitle)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemSelectAll)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemClearAll)).BeginInit();
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
			// checkedListBoxActiveWeeks
			// 
			this.checkedListBoxActiveWeeks.Appearance.BackColor = System.Drawing.Color.White;
			this.checkedListBoxActiveWeeks.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.checkedListBoxActiveWeeks.Appearance.Options.UseBackColor = true;
			this.checkedListBoxActiveWeeks.Appearance.Options.UseFont = true;
			this.checkedListBoxActiveWeeks.CheckOnClick = true;
			this.checkedListBoxActiveWeeks.Cursor = System.Windows.Forms.Cursors.Default;
			this.checkedListBoxActiveWeeks.ItemHeight = 40;
			this.checkedListBoxActiveWeeks.Location = new System.Drawing.Point(12, 107);
			this.checkedListBoxActiveWeeks.Name = "checkedListBoxActiveWeeks";
			this.checkedListBoxActiveWeeks.SelectionMode = System.Windows.Forms.SelectionMode.None;
			this.checkedListBoxActiveWeeks.Size = new System.Drawing.Size(255, 235);
			this.checkedListBoxActiveWeeks.StyleController = this.layoutControl;
			this.checkedListBoxActiveWeeks.TabIndex = 51;
			this.checkedListBoxActiveWeeks.ItemCheck += new DevExpress.XtraEditors.Controls.ItemCheckEventHandler(this.OnItemCheck);
			// 
			// buttonXClearAll
			// 
			this.buttonXClearAll.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXClearAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXClearAll.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXClearAll.Font = new System.Drawing.Font("Arial", 9.75F);
			this.buttonXClearAll.Location = new System.Drawing.Point(151, 57);
			this.buttonXClearAll.Name = "buttonXClearAll";
			this.buttonXClearAll.Size = new System.Drawing.Size(116, 36);
			this.buttonXClearAll.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXClearAll.TabIndex = 11;
			this.buttonXClearAll.Text = "Clear All";
			this.buttonXClearAll.TextColor = System.Drawing.Color.Black;
			this.buttonXClearAll.Click += new System.EventHandler(this.OnClearAll_Click);
			// 
			// buttonXSelectAll
			// 
			this.buttonXSelectAll.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXSelectAll.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXSelectAll.Font = new System.Drawing.Font("Arial", 9.75F);
			this.buttonXSelectAll.Location = new System.Drawing.Point(12, 57);
			this.buttonXSelectAll.Name = "buttonXSelectAll";
			this.buttonXSelectAll.Size = new System.Drawing.Size(116, 36);
			this.buttonXSelectAll.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXSelectAll.TabIndex = 10;
			this.buttonXSelectAll.Text = "Select All";
			this.buttonXSelectAll.TextColor = System.Drawing.Color.Black;
			this.buttonXSelectAll.Click += new System.EventHandler(this.OnSelectAll_Click);
			// 
			// layoutControl
			// 
			this.layoutControl.AllowCustomization = false;
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
			this.layoutControl.Controls.Add(this.buttonXClearAll);
			this.layoutControl.Controls.Add(this.buttonXSelectAll);
			this.layoutControl.Controls.Add(this.checkedListBoxActiveWeeks);
			this.layoutControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutControl.ForeColor = System.Drawing.Color.Black;
			this.layoutControl.Location = new System.Drawing.Point(0, 0);
			this.layoutControl.Name = "layoutControl";
			this.layoutControl.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(802, 383, 250, 350);
			this.layoutControl.Root = this.layoutControlGroupRoot;
			this.layoutControl.Size = new System.Drawing.Size(279, 394);
			this.layoutControl.TabIndex = 65;
			this.layoutControl.Text = "layoutControl1";
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
            this.emptySpaceItem1,
            this.layoutControlItemActiveWeeks,
            this.simpleLabelItemWarning,
            this.simpleLabelItemTitle,
            this.emptySpaceItem2,
            this.layoutControlItemSelectAll,
            this.layoutControlItemClearAll});
			this.layoutControlGroupRoot.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroupRoot.Name = "Root";
			this.layoutControlGroupRoot.Size = new System.Drawing.Size(279, 394);
			this.layoutControlGroupRoot.TextVisible = false;
			// 
			// emptySpaceItem1
			// 
			this.emptySpaceItem1.AllowHotTrack = false;
			this.emptySpaceItem1.Location = new System.Drawing.Point(120, 45);
			this.emptySpaceItem1.Name = "emptySpaceItem1";
			this.emptySpaceItem1.Size = new System.Drawing.Size(19, 40);
			this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
			// 
			// layoutControlItemActiveWeeks
			// 
			this.layoutControlItemActiveWeeks.Control = this.checkedListBoxActiveWeeks;
			this.layoutControlItemActiveWeeks.ControlAlignment = System.Drawing.ContentAlignment.MiddleCenter;
			this.layoutControlItemActiveWeeks.FillControlToClientArea = false;
			this.layoutControlItemActiveWeeks.Location = new System.Drawing.Point(0, 95);
			this.layoutControlItemActiveWeeks.Name = "layoutControlItemActiveWeeks";
			this.layoutControlItemActiveWeeks.Size = new System.Drawing.Size(259, 239);
			this.layoutControlItemActiveWeeks.Text = "Active Weeks";
			this.layoutControlItemActiveWeeks.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItemActiveWeeks.TextVisible = false;
			this.layoutControlItemActiveWeeks.TrimClientAreaToControl = false;
			// 
			// simpleLabelItemWarning
			// 
			this.simpleLabelItemWarning.AllowHotTrack = false;
			this.simpleLabelItemWarning.AllowHtmlStringInCaption = true;
			this.simpleLabelItemWarning.AppearanceItemCaption.Options.UseTextOptions = true;
			this.simpleLabelItemWarning.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.simpleLabelItemWarning.AppearanceItemCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.simpleLabelItemWarning.AppearanceItemCaption.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.simpleLabelItemWarning.Location = new System.Drawing.Point(0, 334);
			this.simpleLabelItemWarning.MaxSize = new System.Drawing.Size(0, 40);
			this.simpleLabelItemWarning.MinSize = new System.Drawing.Size(1, 40);
			this.simpleLabelItemWarning.Name = "simpleLabelItemWarning";
			this.simpleLabelItemWarning.Size = new System.Drawing.Size(259, 40);
			this.simpleLabelItemWarning.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.simpleLabelItemWarning.Text = "<color=red>You should select at least 1 week</color>";
			this.simpleLabelItemWarning.TextSize = new System.Drawing.Size(218, 16);
			// 
			// simpleLabelItemTitle
			// 
			this.simpleLabelItemTitle.AllowHotTrack = false;
			this.simpleLabelItemTitle.AllowHtmlStringInCaption = true;
			this.simpleLabelItemTitle.AppearanceItemCaption.Options.UseTextOptions = true;
			this.simpleLabelItemTitle.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.simpleLabelItemTitle.AppearanceItemCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
			this.simpleLabelItemTitle.AppearanceItemCaption.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.simpleLabelItemTitle.Location = new System.Drawing.Point(0, 0);
			this.simpleLabelItemTitle.MaxSize = new System.Drawing.Size(0, 45);
			this.simpleLabelItemTitle.MinSize = new System.Drawing.Size(1, 45);
			this.simpleLabelItemTitle.Name = "simpleLabelItemTitle";
			this.simpleLabelItemTitle.Size = new System.Drawing.Size(259, 45);
			this.simpleLabelItemTitle.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.simpleLabelItemTitle.Text = "Do you want to apply this schedule to<br>specific weeks on the calendar?";
			this.simpleLabelItemTitle.TextSize = new System.Drawing.Size(218, 32);
			// 
			// emptySpaceItem2
			// 
			this.emptySpaceItem2.AllowHotTrack = false;
			this.emptySpaceItem2.Location = new System.Drawing.Point(0, 85);
			this.emptySpaceItem2.MaxSize = new System.Drawing.Size(0, 10);
			this.emptySpaceItem2.MinSize = new System.Drawing.Size(10, 10);
			this.emptySpaceItem2.Name = "emptySpaceItem2";
			this.emptySpaceItem2.Size = new System.Drawing.Size(259, 10);
			this.emptySpaceItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
			// 
			// layoutControlItemSelectAll
			// 
			this.layoutControlItemSelectAll.Control = this.buttonXSelectAll;
			this.layoutControlItemSelectAll.ControlAlignment = System.Drawing.ContentAlignment.MiddleCenter;
			this.layoutControlItemSelectAll.FillControlToClientArea = false;
			this.layoutControlItemSelectAll.Location = new System.Drawing.Point(0, 45);
			this.layoutControlItemSelectAll.MaxSize = new System.Drawing.Size(120, 40);
			this.layoutControlItemSelectAll.MinSize = new System.Drawing.Size(120, 40);
			this.layoutControlItemSelectAll.Name = "layoutControlItemSelectAll";
			this.layoutControlItemSelectAll.Size = new System.Drawing.Size(120, 40);
			this.layoutControlItemSelectAll.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.layoutControlItemSelectAll.Text = "Select All";
			this.layoutControlItemSelectAll.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
			this.layoutControlItemSelectAll.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItemSelectAll.TextToControlDistance = 0;
			this.layoutControlItemSelectAll.TextVisible = false;
			this.layoutControlItemSelectAll.TrimClientAreaToControl = false;
			// 
			// layoutControlItemClearAll
			// 
			this.layoutControlItemClearAll.Control = this.buttonXClearAll;
			this.layoutControlItemClearAll.ControlAlignment = System.Drawing.ContentAlignment.MiddleCenter;
			this.layoutControlItemClearAll.FillControlToClientArea = false;
			this.layoutControlItemClearAll.Location = new System.Drawing.Point(139, 45);
			this.layoutControlItemClearAll.MaxSize = new System.Drawing.Size(120, 40);
			this.layoutControlItemClearAll.MinSize = new System.Drawing.Size(120, 40);
			this.layoutControlItemClearAll.Name = "layoutControlItemClearAll";
			this.layoutControlItemClearAll.Size = new System.Drawing.Size(120, 40);
			this.layoutControlItemClearAll.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.layoutControlItemClearAll.Text = "Clear All";
			this.layoutControlItemClearAll.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
			this.layoutControlItemClearAll.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItemClearAll.TextToControlDistance = 0;
			this.layoutControlItemClearAll.TextVisible = false;
			this.layoutControlItemClearAll.TrimClientAreaToControl = false;
			// 
			// ActiveWeeksSettingsControl
			// 
			this.Controls.Add(this.layoutControl);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "ActiveWeeksSettingsControl";
			this.Size = new System.Drawing.Size(279, 394);
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkedListBoxActiveWeeks)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl)).EndInit();
			this.layoutControl.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupRoot)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemActiveWeeks)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.simpleLabelItemWarning)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.simpleLabelItemTitle)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemSelectAll)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemClearAll)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraEditors.StyleController styleController;
		public DevExpress.XtraEditors.CheckedListBoxControl checkedListBoxActiveWeeks;
		private DevComponents.DotNetBar.ButtonX buttonXClearAll;
		private DevComponents.DotNetBar.ButtonX buttonXSelectAll;
		private DevExpress.XtraLayout.LayoutControl layoutControl;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupRoot;
		private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItemActiveWeeks;
		private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItemWarning;
		private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItemTitle;
		private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItemSelectAll;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItemClearAll;
	}
}
