using Asa.Solutions.Common.PresentationClasses.ClipartEdit;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors.Video
{
	sealed partial class VideoTabAControl
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
			DevExpress.XtraLayout.ColumnDefinition columnDefinition1 = new DevExpress.XtraLayout.ColumnDefinition();
			DevExpress.XtraLayout.ColumnDefinition columnDefinition2 = new DevExpress.XtraLayout.ColumnDefinition();
			DevExpress.XtraLayout.ColumnDefinition columnDefinition3 = new DevExpress.XtraLayout.ColumnDefinition();
			DevExpress.XtraLayout.RowDefinition rowDefinition1 = new DevExpress.XtraLayout.RowDefinition();
			DevExpress.XtraLayout.RowDefinition rowDefinition2 = new DevExpress.XtraLayout.RowDefinition();
			DevExpress.XtraLayout.RowDefinition rowDefinition3 = new DevExpress.XtraLayout.RowDefinition();
			DevExpress.XtraLayout.RowDefinition rowDefinition4 = new DevExpress.XtraLayout.RowDefinition();
			this.clipartEditContainerTabA1 = new Asa.Solutions.Common.PresentationClasses.ClipartEdit.ClipartEditContainer();
			this.memoEditTabASubheader1 = new DevExpress.XtraEditors.MemoEdit();
			this.layoutControlGroupTabA = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItemTabASubheader1 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItemTabAClipart1 = new DevExpress.XtraLayout.LayoutControlItem();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl)).BeginInit();
			this.layoutControl.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.memoEditTabASubheader1.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupRoot)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupTabA)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemTabASubheader1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemTabAClipart1)).BeginInit();
			this.SuspendLayout();
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
			this.layoutControl.Controls.Add(this.clipartEditContainerTabA1);
			this.layoutControl.Controls.Add(this.memoEditTabASubheader1);
			this.layoutControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutControl.ForeColor = System.Drawing.Color.Black;
			this.layoutControl.Location = new System.Drawing.Point(0, 0);
			this.layoutControl.Name = "layoutControl";
			this.layoutControl.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(802, 383, 250, 350);
			this.layoutControl.OptionsFocus.ActivateSelectedControlOnGotFocus = false;
			this.layoutControl.OptionsFocus.AllowFocusGroups = false;
			this.layoutControl.OptionsFocus.AllowFocusReadonlyEditors = false;
			this.layoutControl.OptionsFocus.AllowFocusTabbedGroups = false;
			this.layoutControl.Root = this.layoutControlGroupRoot;
			this.layoutControl.Size = new System.Drawing.Size(978, 821);
			this.layoutControl.TabIndex = 71;
			this.layoutControl.Text = "layoutControl1";
			// 
			// clipartEditContainerTabA1
			// 
			this.clipartEditContainerTabA1.AllowDrop = true;
			this.clipartEditContainerTabA1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.clipartEditContainerTabA1.Location = new System.Drawing.Point(246, 389);
			this.clipartEditContainerTabA1.Name = "clipartEditContainerTabA1";
			this.clipartEditContainerTabA1.Size = new System.Drawing.Size(516, 420);
			this.clipartEditContainerTabA1.TabIndex = 1;
			// 
			// memoEditTabASubheader1
			// 
			this.memoEditTabASubheader1.Location = new System.Drawing.Point(42, 42);
			this.memoEditTabASubheader1.Name = "memoEditTabASubheader1";
			this.memoEditTabASubheader1.Properties.NullText = "Type here";
			this.memoEditTabASubheader1.Size = new System.Drawing.Size(924, 266);
			this.memoEditTabASubheader1.StyleController = this.layoutControl;
			this.memoEditTabASubheader1.TabIndex = 1;
			this.memoEditTabASubheader1.EditValueChanged += new System.EventHandler(this.OnEditValueChanged);
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
            this.layoutControlGroupTabA});
			this.layoutControlGroupRoot.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroupRoot.Name = "Root";
			this.layoutControlGroupRoot.Padding = new DevExpress.XtraLayout.Utils.Padding(40, 10, 10, 10);
			this.layoutControlGroupRoot.Size = new System.Drawing.Size(978, 821);
			this.layoutControlGroupRoot.TextVisible = false;
			// 
			// layoutControlGroupTabA
			// 
			this.layoutControlGroupTabA.GroupBordersVisible = false;
			this.layoutControlGroupTabA.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemTabASubheader1,
            this.layoutControlItemTabAClipart1});
			this.layoutControlGroupTabA.LayoutMode = DevExpress.XtraLayout.Utils.LayoutMode.Table;
			this.layoutControlGroupTabA.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroupTabA.Name = "layoutControlGroupTabA";
			columnDefinition1.SizeType = System.Windows.Forms.SizeType.Percent;
			columnDefinition1.Width = 22D;
			columnDefinition2.SizeType = System.Windows.Forms.SizeType.Percent;
			columnDefinition2.Width = 56D;
			columnDefinition3.SizeType = System.Windows.Forms.SizeType.Percent;
			columnDefinition3.Width = 22D;
			this.layoutControlGroupTabA.OptionsTableLayoutGroup.ColumnDefinitions.AddRange(new DevExpress.XtraLayout.ColumnDefinition[] {
            columnDefinition1,
            columnDefinition2,
            columnDefinition3});
			rowDefinition1.Height = 30D;
			rowDefinition1.SizeType = System.Windows.Forms.SizeType.Absolute;
			rowDefinition2.Height = 35D;
			rowDefinition2.SizeType = System.Windows.Forms.SizeType.Percent;
			rowDefinition3.Height = 10D;
			rowDefinition3.SizeType = System.Windows.Forms.SizeType.Percent;
			rowDefinition4.Height = 55D;
			rowDefinition4.SizeType = System.Windows.Forms.SizeType.Percent;
			this.layoutControlGroupTabA.OptionsTableLayoutGroup.RowDefinitions.AddRange(new DevExpress.XtraLayout.RowDefinition[] {
            rowDefinition1,
            rowDefinition2,
            rowDefinition3,
            rowDefinition4});
			this.layoutControlGroupTabA.Size = new System.Drawing.Size(928, 801);
			this.layoutControlGroupTabA.Text = "Tab A";
			// 
			// layoutControlItemTabASubheader1
			// 
			this.layoutControlItemTabASubheader1.Control = this.memoEditTabASubheader1;
			this.layoutControlItemTabASubheader1.ControlAlignment = System.Drawing.ContentAlignment.MiddleCenter;
			this.layoutControlItemTabASubheader1.FillControlToClientArea = false;
			this.layoutControlItemTabASubheader1.Location = new System.Drawing.Point(0, 30);
			this.layoutControlItemTabASubheader1.Name = "layoutControlItemTabASubheader1";
			this.layoutControlItemTabASubheader1.OptionsTableLayoutItem.ColumnSpan = 3;
			this.layoutControlItemTabASubheader1.OptionsTableLayoutItem.RowIndex = 1;
			this.layoutControlItemTabASubheader1.Size = new System.Drawing.Size(928, 270);
			this.layoutControlItemTabASubheader1.Text = "Subheader 1";
			this.layoutControlItemTabASubheader1.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItemTabASubheader1.TextVisible = false;
			this.layoutControlItemTabASubheader1.TrimClientAreaToControl = false;
			// 
			// layoutControlItemTabAClipart1
			// 
			this.layoutControlItemTabAClipart1.Control = this.clipartEditContainerTabA1;
			this.layoutControlItemTabAClipart1.ControlAlignment = System.Drawing.ContentAlignment.MiddleCenter;
			this.layoutControlItemTabAClipart1.FillControlToClientArea = false;
			this.layoutControlItemTabAClipart1.Location = new System.Drawing.Point(204, 377);
			this.layoutControlItemTabAClipart1.Name = "layoutControlItemTabAClipart1";
			this.layoutControlItemTabAClipart1.OptionsTableLayoutItem.ColumnIndex = 1;
			this.layoutControlItemTabAClipart1.OptionsTableLayoutItem.RowIndex = 3;
			this.layoutControlItemTabAClipart1.Size = new System.Drawing.Size(520, 424);
			this.layoutControlItemTabAClipart1.Text = "Clipart 1";
			this.layoutControlItemTabAClipart1.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItemTabAClipart1.TextVisible = false;
			this.layoutControlItemTabAClipart1.TrimClientAreaToControl = false;
			// 
			// VideoTabAControl
			// 
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.layoutControl);
			this.Name = "VideoTabAControl";
			this.Size = new System.Drawing.Size(978, 821);
			this.Controls.SetChildIndex(this.layoutControl, 0);
			((System.ComponentModel.ISupportInitialize)(this.layoutControl)).EndInit();
			this.layoutControl.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.memoEditTabASubheader1.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupRoot)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupTabA)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemTabASubheader1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemTabAClipart1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion
		private DevExpress.XtraEditors.MemoEdit memoEditTabASubheader1;
		private ClipartEditContainer clipartEditContainerTabA1;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupTabA;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItemTabASubheader1;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItemTabAClipart1;
	}
}
