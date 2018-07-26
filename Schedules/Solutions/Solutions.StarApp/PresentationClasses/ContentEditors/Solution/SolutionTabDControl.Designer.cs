using Asa.Solutions.Common.PresentationClasses.ClipartEdit;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors.Solution
{
	sealed partial class SolutionTabDControl
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
			DevExpress.XtraLayout.ColumnDefinition columnDefinition1 = new DevExpress.XtraLayout.ColumnDefinition();
			DevExpress.XtraLayout.ColumnDefinition columnDefinition2 = new DevExpress.XtraLayout.ColumnDefinition();
			DevExpress.XtraLayout.ColumnDefinition columnDefinition3 = new DevExpress.XtraLayout.ColumnDefinition();
			DevExpress.XtraLayout.RowDefinition rowDefinition1 = new DevExpress.XtraLayout.RowDefinition();
			DevExpress.XtraLayout.RowDefinition rowDefinition2 = new DevExpress.XtraLayout.RowDefinition();
			DevExpress.XtraLayout.RowDefinition rowDefinition3 = new DevExpress.XtraLayout.RowDefinition();
			this.clipartEditContainerTabD1 = new Asa.Solutions.Common.PresentationClasses.ClipartEdit.ClipartEditContainer();
			this.memoEditTabDSubheader1 = new DevExpress.XtraEditors.MemoEdit();
			this.layoutControlGroupTabD = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItemTabDSubheader1 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItemTabDClipart1 = new DevExpress.XtraLayout.LayoutControlItem();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl)).BeginInit();
			this.layoutControl.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupRoot)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.memoEditTabDSubheader1.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupTabD)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemTabDSubheader1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemTabDClipart1)).BeginInit();
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
			this.layoutControl.Controls.Add(this.clipartEditContainerTabD1);
			this.layoutControl.Controls.Add(this.memoEditTabDSubheader1);
			this.layoutControl.ForeColor = System.Drawing.Color.Black;
			this.layoutControl.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(802, 383, 250, 350);
			this.layoutControl.OptionsFocus.ActivateSelectedControlOnGotFocus = false;
			this.layoutControl.OptionsFocus.AllowFocusGroups = false;
			this.layoutControl.OptionsFocus.AllowFocusReadonlyEditors = false;
			this.layoutControl.OptionsFocus.AllowFocusTabbedGroups = false;
			this.layoutControl.Size = new System.Drawing.Size(635, 479);
			this.layoutControl.TabIndex = 70;
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
			this.layoutControlGroupRoot.AppearanceTabPage.PageClient.Font = new System.Drawing.Font("Arial", 9.75F);
			this.layoutControlGroupRoot.AppearanceTabPage.PageClient.Options.UseFont = true;
			this.layoutControlGroupRoot.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroupTabD});
			this.layoutControlGroupRoot.Name = "Root";
			this.layoutControlGroupRoot.Padding = new DevExpress.XtraLayout.Utils.Padding(40, 10, 10, 10);
			this.layoutControlGroupRoot.Size = new System.Drawing.Size(635, 479);
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
			// clipartEditContainerTabD1
			// 
			this.clipartEditContainerTabD1.AllowDrop = true;
			this.clipartEditContainerTabD1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.clipartEditContainerTabD1.Location = new System.Drawing.Point(42, 126);
			this.clipartEditContainerTabD1.Name = "clipartEditContainerTabD1";
			this.clipartEditContainerTabD1.Size = new System.Drawing.Size(259, 203);
			this.clipartEditContainerTabD1.TabIndex = 1;
			// 
			// memoEditTabDSubheader1
			// 
			this.memoEditTabDSubheader1.Location = new System.Drawing.Point(334, 126);
			this.memoEditTabDSubheader1.Name = "memoEditTabDSubheader1";
			this.memoEditTabDSubheader1.Properties.NullText = "Type here";
			this.memoEditTabDSubheader1.Size = new System.Drawing.Size(289, 203);
			this.memoEditTabDSubheader1.StyleController = this.layoutControl;
			this.memoEditTabDSubheader1.TabIndex = 1;
			this.memoEditTabDSubheader1.EditValueChanged += new System.EventHandler(this.OnEditValueChanged);
			// 
			// layoutControlGroupTabD
			// 
			this.layoutControlGroupTabD.CustomizationFormText = "Tab D";
			this.layoutControlGroupTabD.GroupBordersVisible = false;
			this.layoutControlGroupTabD.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemTabDSubheader1,
            this.layoutControlItemTabDClipart1});
			this.layoutControlGroupTabD.LayoutMode = DevExpress.XtraLayout.Utils.LayoutMode.Table;
			this.layoutControlGroupTabD.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroupTabD.Name = "layoutControlGroupTabD";
			columnDefinition1.SizeType = System.Windows.Forms.SizeType.Percent;
			columnDefinition1.Width = 45D;
			columnDefinition2.SizeType = System.Windows.Forms.SizeType.Percent;
			columnDefinition2.Width = 5D;
			columnDefinition3.SizeType = System.Windows.Forms.SizeType.Percent;
			columnDefinition3.Width = 50D;
			this.layoutControlGroupTabD.OptionsTableLayoutGroup.ColumnDefinitions.AddRange(new DevExpress.XtraLayout.ColumnDefinition[] {
            columnDefinition1,
            columnDefinition2,
            columnDefinition3});
			rowDefinition1.Height = 25D;
			rowDefinition1.SizeType = System.Windows.Forms.SizeType.Percent;
			rowDefinition2.Height = 45D;
			rowDefinition2.SizeType = System.Windows.Forms.SizeType.Percent;
			rowDefinition3.Height = 30D;
			rowDefinition3.SizeType = System.Windows.Forms.SizeType.Percent;
			this.layoutControlGroupTabD.OptionsTableLayoutGroup.RowDefinitions.AddRange(new DevExpress.XtraLayout.RowDefinition[] {
            rowDefinition1,
            rowDefinition2,
            rowDefinition3});
			this.layoutControlGroupTabD.Size = new System.Drawing.Size(585, 459);
			this.layoutControlGroupTabD.Text = "Tab D";
			// 
			// layoutControlItemTabDSubheader1
			// 
			this.layoutControlItemTabDSubheader1.Control = this.memoEditTabDSubheader1;
			this.layoutControlItemTabDSubheader1.ControlAlignment = System.Drawing.ContentAlignment.MiddleCenter;
			this.layoutControlItemTabDSubheader1.CustomizationFormText = "Subheader 1";
			this.layoutControlItemTabDSubheader1.FillControlToClientArea = false;
			this.layoutControlItemTabDSubheader1.Location = new System.Drawing.Point(292, 114);
			this.layoutControlItemTabDSubheader1.Name = "layoutControlItemTabDSubheader1";
			this.layoutControlItemTabDSubheader1.OptionsTableLayoutItem.ColumnIndex = 2;
			this.layoutControlItemTabDSubheader1.OptionsTableLayoutItem.RowIndex = 1;
			this.layoutControlItemTabDSubheader1.Size = new System.Drawing.Size(293, 207);
			this.layoutControlItemTabDSubheader1.Text = "Subheader 1";
			this.layoutControlItemTabDSubheader1.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItemTabDSubheader1.TextVisible = false;
			this.layoutControlItemTabDSubheader1.TrimClientAreaToControl = false;
			// 
			// layoutControlItemTabDClipart1
			// 
			this.layoutControlItemTabDClipart1.Control = this.clipartEditContainerTabD1;
			this.layoutControlItemTabDClipart1.ControlAlignment = System.Drawing.ContentAlignment.MiddleCenter;
			this.layoutControlItemTabDClipart1.FillControlToClientArea = false;
			this.layoutControlItemTabDClipart1.Location = new System.Drawing.Point(0, 114);
			this.layoutControlItemTabDClipart1.Name = "layoutControlItemTabDClipart1";
			this.layoutControlItemTabDClipart1.OptionsTableLayoutItem.RowIndex = 1;
			this.layoutControlItemTabDClipart1.Size = new System.Drawing.Size(263, 207);
			this.layoutControlItemTabDClipart1.Text = "Clipart 1";
			this.layoutControlItemTabDClipart1.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItemTabDClipart1.TextVisible = false;
			this.layoutControlItemTabDClipart1.TrimClientAreaToControl = false;
			// 
			// SolutionTabDControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.BackColor = System.Drawing.Color.White;
			this.Name = "SolutionTabDControl";
			this.Size = new System.Drawing.Size(635, 479);
			((System.ComponentModel.ISupportInitialize)(this.layoutControl)).EndInit();
			this.layoutControl.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupRoot)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.memoEditTabDSubheader1.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupTabD)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemTabDSubheader1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemTabDClipart1)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion
		private DevExpress.XtraEditors.MemoEdit memoEditTabDSubheader1;
		private ClipartEditContainer clipartEditContainerTabD1;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupTabD;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItemTabDSubheader1;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItemTabDClipart1;
	}
}
