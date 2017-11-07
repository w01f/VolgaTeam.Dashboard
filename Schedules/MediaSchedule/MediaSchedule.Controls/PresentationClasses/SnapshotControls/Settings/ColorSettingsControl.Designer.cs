namespace Asa.Media.Controls.PresentationClasses.SnapshotControls.Settings
{
	partial class ColorSettingsControl
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
			this.layoutControl = new DevExpress.XtraLayout.LayoutControl();
			this.outputColorSelector = new Asa.Common.GUI.OutputColors.OutputColorSelector();
			this.layoutControlGroupRoot = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItemOutputColorSelector = new DevExpress.XtraLayout.LayoutControlItem();
			this.simpleLabelItemTitle = new DevExpress.XtraLayout.SimpleLabelItem();
			this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
			this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
			((System.ComponentModel.ISupportInitialize)(this.layoutControl)).BeginInit();
			this.layoutControl.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupRoot)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemOutputColorSelector)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.simpleLabelItemTitle)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			this.SuspendLayout();
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
			this.layoutControl.Controls.Add(this.outputColorSelector);
			this.layoutControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutControl.ForeColor = System.Drawing.Color.Black;
			this.layoutControl.Location = new System.Drawing.Point(0, 0);
			this.layoutControl.Name = "layoutControl";
			this.layoutControl.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(802, 383, 250, 350);
			this.layoutControl.Root = this.layoutControlGroupRoot;
			this.layoutControl.Size = new System.Drawing.Size(326, 510);
			this.layoutControl.TabIndex = 65;
			this.layoutControl.Text = "layoutControl1";
			// 
			// outputColorSelector
			// 
			this.outputColorSelector.BackColor = System.Drawing.Color.White;
			this.outputColorSelector.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.outputColorSelector.Location = new System.Drawing.Point(2, 42);
			this.outputColorSelector.Name = "outputColorSelector";
			this.outputColorSelector.Size = new System.Drawing.Size(322, 466);
			this.outputColorSelector.TabIndex = 50;
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
            this.layoutControlItemOutputColorSelector,
            this.simpleLabelItemTitle,
            this.emptySpaceItem2});
			this.layoutControlGroupRoot.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroupRoot.Name = "Root";
			this.layoutControlGroupRoot.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroupRoot.Size = new System.Drawing.Size(326, 510);
			this.layoutControlGroupRoot.TextVisible = false;
			// 
			// layoutControlItemOutputColorSelector
			// 
			this.layoutControlItemOutputColorSelector.Control = this.outputColorSelector;
			this.layoutControlItemOutputColorSelector.ControlAlignment = System.Drawing.ContentAlignment.MiddleCenter;
			this.layoutControlItemOutputColorSelector.FillControlToClientArea = false;
			this.layoutControlItemOutputColorSelector.Location = new System.Drawing.Point(0, 40);
			this.layoutControlItemOutputColorSelector.Name = "layoutControlItemOutputColorSelector";
			this.layoutControlItemOutputColorSelector.Size = new System.Drawing.Size(326, 470);
			this.layoutControlItemOutputColorSelector.Text = "Output Color Selector";
			this.layoutControlItemOutputColorSelector.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItemOutputColorSelector.TextVisible = false;
			this.layoutControlItemOutputColorSelector.TrimClientAreaToControl = false;
			// 
			// simpleLabelItemTitle
			// 
			this.simpleLabelItemTitle.AllowHotTrack = false;
			this.simpleLabelItemTitle.AllowHtmlStringInCaption = true;
			this.simpleLabelItemTitle.AppearanceItemCaption.Options.UseTextOptions = true;
			this.simpleLabelItemTitle.AppearanceItemCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.simpleLabelItemTitle.Location = new System.Drawing.Point(10, 0);
			this.simpleLabelItemTitle.MaxSize = new System.Drawing.Size(0, 40);
			this.simpleLabelItemTitle.MinSize = new System.Drawing.Size(1, 40);
			this.simpleLabelItemTitle.Name = "simpleLabelItemTitle";
			this.simpleLabelItemTitle.Size = new System.Drawing.Size(316, 40);
			this.simpleLabelItemTitle.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.simpleLabelItemTitle.Text = "<color=gray>Schedule Table Color:</color>";
			this.simpleLabelItemTitle.TextSize = new System.Drawing.Size(127, 16);
			// 
			// emptySpaceItem2
			// 
			this.emptySpaceItem2.AllowHotTrack = false;
			this.emptySpaceItem2.Location = new System.Drawing.Point(0, 0);
			this.emptySpaceItem2.MaxSize = new System.Drawing.Size(10, 0);
			this.emptySpaceItem2.MinSize = new System.Drawing.Size(10, 10);
			this.emptySpaceItem2.Name = "emptySpaceItem2";
			this.emptySpaceItem2.Size = new System.Drawing.Size(10, 40);
			this.emptySpaceItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
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
			// ColorSettingsControl
			// 
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.layoutControl);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "ColorSettingsControl";
			this.Size = new System.Drawing.Size(326, 510);
			((System.ComponentModel.ISupportInitialize)(this.layoutControl)).EndInit();
			this.layoutControl.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupRoot)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemOutputColorSelector)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.simpleLabelItemTitle)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion
		protected Common.GUI.OutputColors.OutputColorSelector outputColorSelector;
		private DevExpress.XtraEditors.StyleController styleController;
		private DevExpress.XtraLayout.LayoutControl layoutControl;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupRoot;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItemOutputColorSelector;
		private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItemTitle;
		private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
	}
}
