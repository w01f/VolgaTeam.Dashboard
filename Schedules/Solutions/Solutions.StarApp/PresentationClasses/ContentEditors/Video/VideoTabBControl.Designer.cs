using Asa.Solutions.Common.PresentationClasses.ClipartEdit;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors.Video
{
	sealed partial class VideoTabBControl
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
			this.clipartEditContainerTabB1 = new Asa.Solutions.Common.PresentationClasses.ClipartEdit.ClipartEditContainer();
			this.memoEditTabBSubheader1 = new DevExpress.XtraEditors.MemoEdit();
			this.layoutControlGroupTabB = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItemTabBSubheader1 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItemTabBClipart1 = new DevExpress.XtraLayout.LayoutControlItem();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl)).BeginInit();
			this.layoutControl.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.memoEditTabBSubheader1.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupRoot)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupTabB)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemTabBSubheader1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemTabBClipart1)).BeginInit();
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
			this.layoutControl.Controls.Add(this.clipartEditContainerTabB1);
			this.layoutControl.Controls.Add(this.memoEditTabBSubheader1);
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
			// clipartEditContainerTabB1
			// 
			this.clipartEditContainerTabB1.AllowDrop = true;
			this.clipartEditContainerTabB1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.clipartEditContainerTabB1.Location = new System.Drawing.Point(246, 389);
			this.clipartEditContainerTabB1.Name = "clipartEditContainerTabB1";
			this.clipartEditContainerTabB1.Size = new System.Drawing.Size(516, 420);
			this.clipartEditContainerTabB1.TabIndex = 1;
			// 
			// memoEditTabBSubheader1
			// 
			this.memoEditTabBSubheader1.Location = new System.Drawing.Point(42, 42);
			this.memoEditTabBSubheader1.Name = "memoEditTabBSubheader1";
			this.memoEditTabBSubheader1.Properties.NullText = "Type here";
			this.memoEditTabBSubheader1.Size = new System.Drawing.Size(924, 266);
			this.memoEditTabBSubheader1.StyleController = this.layoutControl;
			this.memoEditTabBSubheader1.TabIndex = 1;
			this.memoEditTabBSubheader1.EditValueChanged += new System.EventHandler(this.OnEditValueChanged);
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
            this.layoutControlGroupTabB});
			this.layoutControlGroupRoot.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroupRoot.Name = "Root";
			this.layoutControlGroupRoot.Padding = new DevExpress.XtraLayout.Utils.Padding(40, 10, 10, 10);
			this.layoutControlGroupRoot.Size = new System.Drawing.Size(978, 821);
			this.layoutControlGroupRoot.TextVisible = false;
			// 
			// layoutControlGroupTabB
			// 
			this.layoutControlGroupTabB.CustomizationFormText = "Tab B";
			this.layoutControlGroupTabB.GroupBordersVisible = false;
			this.layoutControlGroupTabB.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemTabBSubheader1,
            this.layoutControlItemTabBClipart1});
			this.layoutControlGroupTabB.LayoutMode = DevExpress.XtraLayout.Utils.LayoutMode.Table;
			this.layoutControlGroupTabB.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroupTabB.Name = "layoutControlGroupTabB";
			columnDefinition1.SizeType = System.Windows.Forms.SizeType.Percent;
			columnDefinition1.Width = 22D;
			columnDefinition2.SizeType = System.Windows.Forms.SizeType.Percent;
			columnDefinition2.Width = 56D;
			columnDefinition3.SizeType = System.Windows.Forms.SizeType.Percent;
			columnDefinition3.Width = 22D;
			this.layoutControlGroupTabB.OptionsTableLayoutGroup.ColumnDefinitions.AddRange(new DevExpress.XtraLayout.ColumnDefinition[] {
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
			this.layoutControlGroupTabB.OptionsTableLayoutGroup.RowDefinitions.AddRange(new DevExpress.XtraLayout.RowDefinition[] {
            rowDefinition1,
            rowDefinition2,
            rowDefinition3,
            rowDefinition4});
			this.layoutControlGroupTabB.Size = new System.Drawing.Size(928, 801);
			this.layoutControlGroupTabB.Text = "Tab B";
			// 
			// layoutControlItemTabBSubheader1
			// 
			this.layoutControlItemTabBSubheader1.Control = this.memoEditTabBSubheader1;
			this.layoutControlItemTabBSubheader1.ControlAlignment = System.Drawing.ContentAlignment.MiddleCenter;
			this.layoutControlItemTabBSubheader1.CustomizationFormText = "Subheader 1";
			this.layoutControlItemTabBSubheader1.Location = new System.Drawing.Point(0, 30);
			this.layoutControlItemTabBSubheader1.Name = "layoutControlItemTabBSubheader1";
			this.layoutControlItemTabBSubheader1.OptionsTableLayoutItem.ColumnSpan = 3;
			this.layoutControlItemTabBSubheader1.OptionsTableLayoutItem.RowIndex = 1;
			this.layoutControlItemTabBSubheader1.Size = new System.Drawing.Size(928, 270);
			this.layoutControlItemTabBSubheader1.Text = "Subheader 1";
			this.layoutControlItemTabBSubheader1.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItemTabBSubheader1.TextVisible = false;
			this.layoutControlItemTabBSubheader1.TrimClientAreaToControl = false;
			// 
			// layoutControlItemTabBClipart1
			// 
			this.layoutControlItemTabBClipart1.Control = this.clipartEditContainerTabB1;
			this.layoutControlItemTabBClipart1.ControlAlignment = System.Drawing.ContentAlignment.MiddleCenter;
			this.layoutControlItemTabBClipart1.FillControlToClientArea = false;
			this.layoutControlItemTabBClipart1.Location = new System.Drawing.Point(204, 377);
			this.layoutControlItemTabBClipart1.Name = "layoutControlItemTabBClipart1";
			this.layoutControlItemTabBClipart1.OptionsTableLayoutItem.ColumnIndex = 1;
			this.layoutControlItemTabBClipart1.OptionsTableLayoutItem.RowIndex = 3;
			this.layoutControlItemTabBClipart1.Size = new System.Drawing.Size(520, 424);
			this.layoutControlItemTabBClipart1.Text = "Clipart 1";
			this.layoutControlItemTabBClipart1.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItemTabBClipart1.TextVisible = false;
			this.layoutControlItemTabBClipart1.TrimClientAreaToControl = false;
			// 
			// VideoTabBControl
			// 
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.layoutControl);
			this.Name = "VideoTabBControl";
			this.Size = new System.Drawing.Size(978, 821);
			this.Controls.SetChildIndex(this.layoutControl, 0);
			((System.ComponentModel.ISupportInitialize)(this.layoutControl)).EndInit();
			this.layoutControl.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.memoEditTabBSubheader1.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupRoot)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupTabB)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemTabBSubheader1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemTabBClipart1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion
		private DevExpress.XtraEditors.MemoEdit memoEditTabBSubheader1;
		private ClipartEditContainer clipartEditContainerTabB1;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupTabB;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItemTabBSubheader1;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItemTabBClipart1;
	}
}
