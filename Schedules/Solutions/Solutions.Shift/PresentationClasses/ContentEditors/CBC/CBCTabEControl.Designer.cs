using Asa.Solutions.Common.PresentationClasses.ClipartEdit;

namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors.CBC
{
	sealed partial class CBCTabEControl
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
			DevExpress.XtraLayout.ColumnDefinition columnDefinition4 = new DevExpress.XtraLayout.ColumnDefinition();
			DevExpress.XtraLayout.ColumnDefinition columnDefinition5 = new DevExpress.XtraLayout.ColumnDefinition();
			DevExpress.XtraLayout.RowDefinition rowDefinition1 = new DevExpress.XtraLayout.RowDefinition();
			DevExpress.XtraLayout.RowDefinition rowDefinition2 = new DevExpress.XtraLayout.RowDefinition();
			DevExpress.XtraLayout.RowDefinition rowDefinition3 = new DevExpress.XtraLayout.RowDefinition();
			DevExpress.XtraLayout.RowDefinition rowDefinition4 = new DevExpress.XtraLayout.RowDefinition();
			this.xtraTabControl = new DevExpress.XtraTab.XtraTabControl();
			this.layoutControlItemTabControl = new DevExpress.XtraLayout.LayoutControlItem();
			this.comboBoxEditSlideHeader = new DevExpress.XtraEditors.ComboBoxEdit();
			this.layoutControlItemSlideHeader = new DevExpress.XtraLayout.LayoutControlItem();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl)).BeginInit();
			this.layoutControl.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupRoot)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemTabControl)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditSlideHeader.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemSlideHeader)).BeginInit();
			this.SuspendLayout();
			// 
			// layoutControl
			// 
			this.layoutControl.Appearance.Control.Font = new System.Drawing.Font("Arial", 9.75F);
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
			this.layoutControl.Controls.Add(this.comboBoxEditSlideHeader);
			this.layoutControl.Controls.Add(this.xtraTabControl);
			this.layoutControl.ForeColor = System.Drawing.Color.Black;
			this.layoutControl.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(553, 160, 595, 682);
			this.layoutControl.OptionsFocus.ActivateSelectedControlOnGotFocus = false;
			this.layoutControl.OptionsFocus.AllowFocusGroups = false;
			this.layoutControl.OptionsFocus.AllowFocusReadonlyEditors = false;
			this.layoutControl.OptionsFocus.AllowFocusTabbedGroups = false;
			this.layoutControl.Size = new System.Drawing.Size(981, 618);
			this.layoutControl.TabIndex = 69;
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
            this.layoutControlItemTabControl,
            this.layoutControlItemSlideHeader});
			this.layoutControlGroupRoot.LayoutMode = DevExpress.XtraLayout.Utils.LayoutMode.Table;
			this.layoutControlGroupRoot.Name = "Root";
			columnDefinition1.SizeType = System.Windows.Forms.SizeType.Absolute;
			columnDefinition1.Width = 20D;
			columnDefinition2.SizeType = System.Windows.Forms.SizeType.Percent;
			columnDefinition2.Width = 45D;
			columnDefinition3.SizeType = System.Windows.Forms.SizeType.Percent;
			columnDefinition3.Width = 10D;
			columnDefinition4.SizeType = System.Windows.Forms.SizeType.Percent;
			columnDefinition4.Width = 45D;
			columnDefinition5.SizeType = System.Windows.Forms.SizeType.Absolute;
			columnDefinition5.Width = 20D;
			this.layoutControlGroupRoot.OptionsTableLayoutGroup.ColumnDefinitions.AddRange(new DevExpress.XtraLayout.ColumnDefinition[] {
            columnDefinition1,
            columnDefinition2,
            columnDefinition3,
            columnDefinition4,
            columnDefinition5});
			rowDefinition1.Height = 20D;
			rowDefinition1.SizeType = System.Windows.Forms.SizeType.Absolute;
			rowDefinition2.Height = 15D;
			rowDefinition2.SizeType = System.Windows.Forms.SizeType.Percent;
			rowDefinition3.Height = 85D;
			rowDefinition3.SizeType = System.Windows.Forms.SizeType.Percent;
			rowDefinition4.Height = 20D;
			rowDefinition4.SizeType = System.Windows.Forms.SizeType.Absolute;
			this.layoutControlGroupRoot.OptionsTableLayoutGroup.RowDefinitions.AddRange(new DevExpress.XtraLayout.RowDefinition[] {
            rowDefinition1,
            rowDefinition2,
            rowDefinition3,
            rowDefinition4});
			this.layoutControlGroupRoot.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroupRoot.Size = new System.Drawing.Size(981, 618);
			// 
			// xtraTabControl
			// 
			this.xtraTabControl.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControl.Appearance.Options.UseFont = true;
			this.xtraTabControl.Appearance.Options.UseTextOptions = true;
			this.xtraTabControl.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.None;
			this.xtraTabControl.AppearancePage.Header.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControl.AppearancePage.Header.Options.UseFont = true;
			this.xtraTabControl.AppearancePage.Header.Options.UseTextOptions = true;
			this.xtraTabControl.AppearancePage.Header.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.None;
			this.xtraTabControl.AppearancePage.HeaderActive.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControl.AppearancePage.HeaderActive.Options.UseFont = true;
			this.xtraTabControl.AppearancePage.HeaderActive.Options.UseTextOptions = true;
			this.xtraTabControl.AppearancePage.HeaderActive.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.None;
			this.xtraTabControl.AppearancePage.HeaderDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControl.AppearancePage.HeaderDisabled.Options.UseFont = true;
			this.xtraTabControl.AppearancePage.HeaderDisabled.Options.UseTextOptions = true;
			this.xtraTabControl.AppearancePage.HeaderDisabled.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.None;
			this.xtraTabControl.AppearancePage.HeaderHotTracked.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControl.AppearancePage.HeaderHotTracked.Options.UseFont = true;
			this.xtraTabControl.AppearancePage.HeaderHotTracked.Options.UseTextOptions = true;
			this.xtraTabControl.AppearancePage.HeaderHotTracked.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.None;
			this.xtraTabControl.AppearancePage.PageClient.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControl.AppearancePage.PageClient.Options.UseFont = true;
			this.xtraTabControl.AppearancePage.PageClient.Options.UseTextOptions = true;
			this.xtraTabControl.AppearancePage.PageClient.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.None;
			this.xtraTabControl.Location = new System.Drawing.Point(22, 109);
			this.xtraTabControl.Name = "xtraTabControl";
			this.xtraTabControl.Size = new System.Drawing.Size(937, 487);
			this.xtraTabControl.TabIndex = 30;
			// 
			// layoutControlItemTabControl
			// 
			this.layoutControlItemTabControl.Control = this.xtraTabControl;
			this.layoutControlItemTabControl.ControlAlignment = System.Drawing.ContentAlignment.MiddleCenter;
			this.layoutControlItemTabControl.FillControlToClientArea = false;
			this.layoutControlItemTabControl.Location = new System.Drawing.Point(20, 107);
			this.layoutControlItemTabControl.Name = "layoutControlItemTabControl";
			this.layoutControlItemTabControl.OptionsTableLayoutItem.ColumnIndex = 1;
			this.layoutControlItemTabControl.OptionsTableLayoutItem.ColumnSpan = 3;
			this.layoutControlItemTabControl.OptionsTableLayoutItem.RowIndex = 2;
			this.layoutControlItemTabControl.Size = new System.Drawing.Size(941, 491);
			this.layoutControlItemTabControl.Text = "Tab Control";
			this.layoutControlItemTabControl.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItemTabControl.TextVisible = false;
			this.layoutControlItemTabControl.TrimClientAreaToControl = false;
			// 
			// comboBoxEditSlideHeader
			// 
			this.comboBoxEditSlideHeader.Location = new System.Drawing.Point(22, 52);
			this.comboBoxEditSlideHeader.Name = "comboBoxEditSlideHeader";
			this.comboBoxEditSlideHeader.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.comboBoxEditSlideHeader.Size = new System.Drawing.Size(419, 22);
			this.comboBoxEditSlideHeader.StyleController = this.layoutControl;
			this.comboBoxEditSlideHeader.TabIndex = 31;
			this.comboBoxEditSlideHeader.EditValueChanged += new System.EventHandler(this.OnEditValueChanged);
			// 
			// layoutControlItemSlideHeader
			// 
			this.layoutControlItemSlideHeader.Control = this.comboBoxEditSlideHeader;
			this.layoutControlItemSlideHeader.ControlAlignment = System.Drawing.ContentAlignment.MiddleCenter;
			this.layoutControlItemSlideHeader.FillControlToClientArea = false;
			this.layoutControlItemSlideHeader.Location = new System.Drawing.Point(20, 20);
			this.layoutControlItemSlideHeader.Name = "layoutControlItemSlideHeader";
			this.layoutControlItemSlideHeader.OptionsTableLayoutItem.ColumnIndex = 1;
			this.layoutControlItemSlideHeader.OptionsTableLayoutItem.RowIndex = 1;
			this.layoutControlItemSlideHeader.Size = new System.Drawing.Size(423, 87);
			this.layoutControlItemSlideHeader.Text = "Slide Header";
			this.layoutControlItemSlideHeader.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItemSlideHeader.TextVisible = false;
			this.layoutControlItemSlideHeader.TrimClientAreaToControl = false;
			// 
			// CBCTabEControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.Name = "CBCTabEControl";
			this.Size = new System.Drawing.Size(981, 618);
			((System.ComponentModel.ISupportInitialize)(this.layoutControl)).EndInit();
			this.layoutControl.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupRoot)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemTabControl)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditSlideHeader.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemSlideHeader)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraTab.XtraTabControl xtraTabControl;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItemTabControl;
		protected DevExpress.XtraEditors.ComboBoxEdit comboBoxEditSlideHeader;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItemSlideHeader;
	}
}
