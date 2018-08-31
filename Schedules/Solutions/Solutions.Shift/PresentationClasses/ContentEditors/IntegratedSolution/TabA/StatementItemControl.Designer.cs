namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors.IntegratedSolution.TabA
{
	partial class StatementItemControl
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
			this.layoutControl = new DevExpress.XtraLayout.LayoutControl();
			this.memoPopupEdit = new Asa.Solutions.Common.PresentationClasses.MemoPopupEdit.MemoPopupEdit();
			this.comboBoxEditCombo = new DevExpress.XtraEditors.ComboBoxEdit();
			this.checkEditCombo = new DevExpress.XtraEditors.CheckEdit();
			this.checkEditMemoPopup = new DevExpress.XtraEditors.CheckEdit();
			this.layoutControlGroupRoot = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItemCombo = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItemMemoPopup = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItemToggleCombo = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItemToggleMemoPopup = new DevExpress.XtraLayout.LayoutControlItem();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl)).BeginInit();
			this.layoutControl.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditCombo.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditCombo.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditMemoPopup.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupRoot)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCombo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemMemoPopup)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemToggleCombo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemToggleMemoPopup)).BeginInit();
			this.SuspendLayout();
			// 
			// layoutControl
			// 
			this.layoutControl.AllowCustomization = false;
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
			this.layoutControl.Controls.Add(this.memoPopupEdit);
			this.layoutControl.Controls.Add(this.comboBoxEditCombo);
			this.layoutControl.Controls.Add(this.checkEditCombo);
			this.layoutControl.Controls.Add(this.checkEditMemoPopup);
			this.layoutControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutControl.Location = new System.Drawing.Point(0, 0);
			this.layoutControl.Name = "layoutControl";
			this.layoutControl.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(729, 206, 450, 400);
			this.layoutControl.OptionsFocus.ActivateSelectedControlOnGotFocus = false;
			this.layoutControl.OptionsFocus.AllowFocusGroups = false;
			this.layoutControl.OptionsFocus.AllowFocusReadonlyEditors = false;
			this.layoutControl.OptionsFocus.AllowFocusTabbedGroups = false;
			this.layoutControl.Root = this.layoutControlGroupRoot;
			this.layoutControl.Size = new System.Drawing.Size(396, 134);
			this.layoutControl.TabIndex = 0;
			// 
			// memoPopupEdit
			// 
			this.memoPopupEdit.BackColor = System.Drawing.Color.Transparent;
			this.memoPopupEdit.Location = new System.Drawing.Point(49, 32);
			this.memoPopupEdit.Margin = new System.Windows.Forms.Padding(3, 9, 3, 9);
			this.memoPopupEdit.Name = "memoPopupEdit";
			this.memoPopupEdit.Size = new System.Drawing.Size(347, 102);
			this.memoPopupEdit.TabIndex = 35;
			// 
			// comboBoxEditCombo
			// 
			this.comboBoxEditCombo.Location = new System.Drawing.Point(49, 0);
			this.comboBoxEditCombo.Name = "comboBoxEditCombo";
			this.comboBoxEditCombo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.comboBoxEditCombo.Properties.NullText = "Select or type";
			this.comboBoxEditCombo.Size = new System.Drawing.Size(318, 22);
			this.comboBoxEditCombo.StyleController = this.layoutControl;
			this.comboBoxEditCombo.TabIndex = 11;
			this.comboBoxEditCombo.EditValueChanged += new System.EventHandler(this.OnEditValueChanged);
			// 
			// checkEditCombo
			// 
			this.checkEditCombo.Location = new System.Drawing.Point(0, 1);
			this.checkEditCombo.Name = "checkEditCombo";
			this.checkEditCombo.Properties.AllowFocused = false;
			this.checkEditCombo.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
			this.checkEditCombo.Properties.Appearance.Options.UseTextOptions = true;
			this.checkEditCombo.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			this.checkEditCombo.Properties.AppearanceDisabled.Options.UseTextOptions = true;
			this.checkEditCombo.Properties.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			this.checkEditCombo.Properties.AppearanceFocused.Options.UseTextOptions = true;
			this.checkEditCombo.Properties.AppearanceFocused.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			this.checkEditCombo.Properties.AppearanceReadOnly.Options.UseTextOptions = true;
			this.checkEditCombo.Properties.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			this.checkEditCombo.Properties.AutoWidth = true;
			this.checkEditCombo.Properties.Caption = "<color=gray>test</color>";
			this.checkEditCombo.Size = new System.Drawing.Size(44, 20);
			this.checkEditCombo.StyleController = this.layoutControl;
			this.checkEditCombo.TabIndex = 36;
			this.checkEditCombo.CheckedChanged += new System.EventHandler(this.OnToggleComboCheckedChanged);
			// 
			// checkEditMemoPopup
			// 
			this.checkEditMemoPopup.Location = new System.Drawing.Point(0, 32);
			this.checkEditMemoPopup.Name = "checkEditMemoPopup";
			this.checkEditMemoPopup.Properties.Caption = "";
			this.checkEditMemoPopup.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Far;
			this.checkEditMemoPopup.Size = new System.Drawing.Size(44, 19);
			this.checkEditMemoPopup.StyleController = this.layoutControl;
			this.checkEditMemoPopup.TabIndex = 37;
			this.checkEditMemoPopup.CheckedChanged += new System.EventHandler(this.OnToggleMemoPopupCheckedChanged);
			// 
			// layoutControlGroupRoot
			// 
			this.layoutControlGroupRoot.AppearanceGroup.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.layoutControlGroupRoot.AppearanceGroup.Options.UseFont = true;
			this.layoutControlGroupRoot.AppearanceItemCaption.Font = new System.Drawing.Font("Arial", 9.75F);
			this.layoutControlGroupRoot.AppearanceItemCaption.Options.UseFont = true;
			this.layoutControlGroupRoot.AppearanceTabPage.Header.Font = new System.Drawing.Font("Arial", 9.75F);
			this.layoutControlGroupRoot.AppearanceTabPage.Header.Options.UseFont = true;
			this.layoutControlGroupRoot.AppearanceTabPage.HeaderActive.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.layoutControlGroupRoot.AppearanceTabPage.HeaderActive.Options.UseFont = true;
			this.layoutControlGroupRoot.AppearanceTabPage.HeaderDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.layoutControlGroupRoot.AppearanceTabPage.HeaderDisabled.Options.UseFont = true;
			this.layoutControlGroupRoot.AppearanceTabPage.HeaderHotTracked.Font = new System.Drawing.Font("Arial", 9.75F);
			this.layoutControlGroupRoot.AppearanceTabPage.HeaderHotTracked.Options.UseFont = true;
			this.layoutControlGroupRoot.AppearanceTabPage.PageClient.Font = new System.Drawing.Font("Arial", 9.75F);
			this.layoutControlGroupRoot.AppearanceTabPage.PageClient.Options.UseFont = true;
			this.layoutControlGroupRoot.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			this.layoutControlGroupRoot.GroupBordersVisible = false;
			this.layoutControlGroupRoot.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemCombo,
            this.layoutControlItemMemoPopup,
            this.layoutControlItemToggleCombo,
            this.layoutControlItemToggleMemoPopup});
			this.layoutControlGroupRoot.LayoutMode = DevExpress.XtraLayout.Utils.LayoutMode.Table;
			this.layoutControlGroupRoot.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroupRoot.Name = "Root";
			columnDefinition1.SizeType = System.Windows.Forms.SizeType.AutoSize;
			columnDefinition1.Width = 44D;
			columnDefinition2.SizeType = System.Windows.Forms.SizeType.Absolute;
			columnDefinition2.Width = 5D;
			columnDefinition3.SizeType = System.Windows.Forms.SizeType.Percent;
			columnDefinition3.Width = 100D;
			this.layoutControlGroupRoot.OptionsTableLayoutGroup.ColumnDefinitions.AddRange(new DevExpress.XtraLayout.ColumnDefinition[] {
            columnDefinition1,
            columnDefinition2,
            columnDefinition3});
			rowDefinition1.Height = 22D;
			rowDefinition1.SizeType = System.Windows.Forms.SizeType.AutoSize;
			rowDefinition2.Height = 10D;
			rowDefinition2.SizeType = System.Windows.Forms.SizeType.Absolute;
			rowDefinition3.Height = 100D;
			rowDefinition3.SizeType = System.Windows.Forms.SizeType.Percent;
			this.layoutControlGroupRoot.OptionsTableLayoutGroup.RowDefinitions.AddRange(new DevExpress.XtraLayout.RowDefinition[] {
            rowDefinition1,
            rowDefinition2,
            rowDefinition3});
			this.layoutControlGroupRoot.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroupRoot.Size = new System.Drawing.Size(396, 134);
			this.layoutControlGroupRoot.TextVisible = false;
			// 
			// layoutControlItemCombo
			// 
			this.layoutControlItemCombo.AllowHtmlStringInCaption = true;
			this.layoutControlItemCombo.Control = this.comboBoxEditCombo;
			this.layoutControlItemCombo.ControlAlignment = System.Drawing.ContentAlignment.MiddleCenter;
			this.layoutControlItemCombo.Enabled = false;
			this.layoutControlItemCombo.FillControlToClientArea = false;
			this.layoutControlItemCombo.Location = new System.Drawing.Point(49, 0);
			this.layoutControlItemCombo.Name = "layoutControlItemCombo";
			this.layoutControlItemCombo.OptionsTableLayoutItem.ColumnIndex = 2;
			this.layoutControlItemCombo.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 29, 0, 0);
			this.layoutControlItemCombo.Size = new System.Drawing.Size(347, 22);
			this.layoutControlItemCombo.Text = "Combo";
			this.layoutControlItemCombo.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItemCombo.TextVisible = false;
			this.layoutControlItemCombo.TrimClientAreaToControl = false;
			// 
			// layoutControlItemMemoPopup
			// 
			this.layoutControlItemMemoPopup.Control = this.memoPopupEdit;
			this.layoutControlItemMemoPopup.ControlAlignment = System.Drawing.ContentAlignment.MiddleCenter;
			this.layoutControlItemMemoPopup.Enabled = false;
			this.layoutControlItemMemoPopup.Location = new System.Drawing.Point(49, 32);
			this.layoutControlItemMemoPopup.MinSize = new System.Drawing.Size(10, 20);
			this.layoutControlItemMemoPopup.Name = "layoutControlItemMemoPopup";
			this.layoutControlItemMemoPopup.OptionsTableLayoutItem.ColumnIndex = 2;
			this.layoutControlItemMemoPopup.OptionsTableLayoutItem.RowIndex = 2;
			this.layoutControlItemMemoPopup.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItemMemoPopup.Size = new System.Drawing.Size(347, 102);
			this.layoutControlItemMemoPopup.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.layoutControlItemMemoPopup.Text = "Memo Popup";
			this.layoutControlItemMemoPopup.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItemMemoPopup.TextVisible = false;
			// 
			// layoutControlItemToggleCombo
			// 
			this.layoutControlItemToggleCombo.Control = this.checkEditCombo;
			this.layoutControlItemToggleCombo.ControlAlignment = System.Drawing.ContentAlignment.MiddleLeft;
			this.layoutControlItemToggleCombo.FillControlToClientArea = false;
			this.layoutControlItemToggleCombo.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItemToggleCombo.MaxSize = new System.Drawing.Size(44, 20);
			this.layoutControlItemToggleCombo.MinSize = new System.Drawing.Size(44, 20);
			this.layoutControlItemToggleCombo.Name = "layoutControlItemToggleCombo";
			this.layoutControlItemToggleCombo.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItemToggleCombo.Size = new System.Drawing.Size(44, 22);
			this.layoutControlItemToggleCombo.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.layoutControlItemToggleCombo.Text = "Toggle Combo";
			this.layoutControlItemToggleCombo.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItemToggleCombo.TextVisible = false;
			this.layoutControlItemToggleCombo.TrimClientAreaToControl = false;
			// 
			// layoutControlItemToggleMemoPopup
			// 
			this.layoutControlItemToggleMemoPopup.Control = this.checkEditMemoPopup;
			this.layoutControlItemToggleMemoPopup.ControlAlignment = System.Drawing.ContentAlignment.TopRight;
			this.layoutControlItemToggleMemoPopup.FillControlToClientArea = false;
			this.layoutControlItemToggleMemoPopup.Location = new System.Drawing.Point(0, 32);
			this.layoutControlItemToggleMemoPopup.MaxSize = new System.Drawing.Size(0, 19);
			this.layoutControlItemToggleMemoPopup.MinSize = new System.Drawing.Size(10, 19);
			this.layoutControlItemToggleMemoPopup.Name = "layoutControlItemToggleMemoPopup";
			this.layoutControlItemToggleMemoPopup.OptionsTableLayoutItem.RowIndex = 2;
			this.layoutControlItemToggleMemoPopup.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItemToggleMemoPopup.Size = new System.Drawing.Size(44, 102);
			this.layoutControlItemToggleMemoPopup.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.layoutControlItemToggleMemoPopup.Text = "Toggle MemoPopup";
			this.layoutControlItemToggleMemoPopup.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItemToggleMemoPopup.TextVisible = false;
			this.layoutControlItemToggleMemoPopup.TrimClientAreaToControl = false;
			// 
			// StatementItemControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.layoutControl);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "StatementItemControl";
			this.Size = new System.Drawing.Size(396, 134);
			((System.ComponentModel.ISupportInitialize)(this.layoutControl)).EndInit();
			this.layoutControl.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditCombo.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditCombo.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditMemoPopup.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupRoot)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCombo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemMemoPopup)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemToggleCombo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemToggleMemoPopup)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion
		public DevExpress.XtraLayout.LayoutControl layoutControl;
		public DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupRoot;
		protected DevExpress.XtraEditors.ComboBoxEdit comboBoxEditCombo;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItemCombo;
		private Common.PresentationClasses.MemoPopupEdit.MemoPopupEdit memoPopupEdit;
		private DevExpress.XtraEditors.CheckEdit checkEditCombo;
		private DevExpress.XtraEditors.CheckEdit checkEditMemoPopup;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItemMemoPopup;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItemToggleCombo;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItemToggleMemoPopup;
	}
}
