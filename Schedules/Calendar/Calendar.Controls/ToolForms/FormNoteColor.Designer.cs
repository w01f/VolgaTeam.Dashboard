namespace Asa.Calendar.Controls.ToolForms
{
    partial class FormNoteColor
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.buttonXShow = new DevComponents.DotNetBar.ButtonX();
			this.buttonXOK = new DevComponents.DotNetBar.ButtonX();
			this.pnSelectedColor = new System.Windows.Forms.Panel();
			this.layoutControl = new DevExpress.XtraLayout.LayoutControl();
			this.layoutControlGroupRoot = new DevExpress.XtraLayout.LayoutControlGroup();
			this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
			this.layoutControlItemCancel = new DevExpress.XtraLayout.LayoutControlItem();
			this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
			this.layoutControlItemOK = new DevExpress.XtraLayout.LayoutControlItem();
			this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
			this.layoutControlItemSelectedColor = new DevExpress.XtraLayout.LayoutControlItem();
			this.emptySpaceItem4 = new DevExpress.XtraLayout.EmptySpaceItem();
			this.checkEditApplyForAll = new DevExpress.XtraEditors.CheckEdit();
			this.layoutControlItemApplyForAll = new DevExpress.XtraLayout.LayoutControlItem();
			this.simpleLabelItemTitle = new DevExpress.XtraLayout.SimpleLabelItem();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl)).BeginInit();
			this.layoutControl.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupRoot)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCancel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemOK)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemSelectedColor)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditApplyForAll.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemApplyForAll)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.simpleLabelItemTitle)).BeginInit();
			this.SuspendLayout();
			// 
			// buttonXShow
			// 
			this.buttonXShow.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXShow.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXShow.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonXShow.Location = new System.Drawing.Point(12, 146);
			this.buttonXShow.Name = "buttonXShow";
			this.buttonXShow.Size = new System.Drawing.Size(116, 36);
			this.buttonXShow.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXShow.TabIndex = 9;
			this.buttonXShow.Text = "OK";
			this.buttonXShow.TextColor = System.Drawing.Color.Black;
			// 
			// buttonXOK
			// 
			this.buttonXOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonXOK.Location = new System.Drawing.Point(191, 146);
			this.buttonXOK.Name = "buttonXOK";
			this.buttonXOK.Size = new System.Drawing.Size(116, 36);
			this.buttonXOK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXOK.TabIndex = 10;
			this.buttonXOK.Text = "Cancel";
			this.buttonXOK.TextColor = System.Drawing.Color.Black;
			// 
			// pnSelectedColor
			// 
			this.pnSelectedColor.BackColor = System.Drawing.Color.Transparent;
			this.pnSelectedColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnSelectedColor.ForeColor = System.Drawing.Color.Black;
			this.pnSelectedColor.Location = new System.Drawing.Point(12, 42);
			this.pnSelectedColor.Name = "pnSelectedColor";
			this.pnSelectedColor.Size = new System.Drawing.Size(295, 56);
			this.pnSelectedColor.TabIndex = 11;
			this.pnSelectedColor.DoubleClick += new System.EventHandler(this.OnSelectedColorPanelDoubleClick);
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
			this.layoutControl.Controls.Add(this.pnSelectedColor);
			this.layoutControl.Controls.Add(this.buttonXShow);
			this.layoutControl.Controls.Add(this.buttonXOK);
			this.layoutControl.Controls.Add(this.checkEditApplyForAll);
			this.layoutControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutControl.ForeColor = System.Drawing.Color.Black;
			this.layoutControl.Location = new System.Drawing.Point(0, 0);
			this.layoutControl.Name = "layoutControl";
			this.layoutControl.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(802, 383, 250, 350);
			this.layoutControl.Root = this.layoutControlGroupRoot;
			this.layoutControl.Size = new System.Drawing.Size(319, 194);
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
            this.layoutControlItemCancel,
            this.emptySpaceItem2,
            this.layoutControlItemOK,
            this.emptySpaceItem3,
            this.layoutControlItemSelectedColor,
            this.emptySpaceItem4,
            this.layoutControlItemApplyForAll,
            this.simpleLabelItemTitle});
			this.layoutControlGroupRoot.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroupRoot.Name = "Root";
			this.layoutControlGroupRoot.Size = new System.Drawing.Size(319, 194);
			this.layoutControlGroupRoot.TextVisible = false;
			// 
			// emptySpaceItem1
			// 
			this.emptySpaceItem1.AllowHotTrack = false;
			this.emptySpaceItem1.Location = new System.Drawing.Point(0, 20);
			this.emptySpaceItem1.MaxSize = new System.Drawing.Size(0, 10);
			this.emptySpaceItem1.MinSize = new System.Drawing.Size(10, 10);
			this.emptySpaceItem1.Name = "emptySpaceItem1";
			this.emptySpaceItem1.Size = new System.Drawing.Size(299, 10);
			this.emptySpaceItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
			// 
			// layoutControlItemCancel
			// 
			this.layoutControlItemCancel.Control = this.buttonXOK;
			this.layoutControlItemCancel.ControlAlignment = System.Drawing.ContentAlignment.MiddleCenter;
			this.layoutControlItemCancel.FillControlToClientArea = false;
			this.layoutControlItemCancel.Location = new System.Drawing.Point(179, 134);
			this.layoutControlItemCancel.MaxSize = new System.Drawing.Size(120, 40);
			this.layoutControlItemCancel.MinSize = new System.Drawing.Size(120, 40);
			this.layoutControlItemCancel.Name = "layoutControlItemCancel";
			this.layoutControlItemCancel.Size = new System.Drawing.Size(120, 40);
			this.layoutControlItemCancel.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.layoutControlItemCancel.Text = "Cancel";
			this.layoutControlItemCancel.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItemCancel.TextVisible = false;
			this.layoutControlItemCancel.TrimClientAreaToControl = false;
			// 
			// emptySpaceItem2
			// 
			this.emptySpaceItem2.AllowHotTrack = false;
			this.emptySpaceItem2.Location = new System.Drawing.Point(120, 134);
			this.emptySpaceItem2.Name = "emptySpaceItem2";
			this.emptySpaceItem2.Size = new System.Drawing.Size(59, 40);
			this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
			// 
			// layoutControlItemOK
			// 
			this.layoutControlItemOK.Control = this.buttonXShow;
			this.layoutControlItemOK.ControlAlignment = System.Drawing.ContentAlignment.MiddleCenter;
			this.layoutControlItemOK.FillControlToClientArea = false;
			this.layoutControlItemOK.Location = new System.Drawing.Point(0, 134);
			this.layoutControlItemOK.MaxSize = new System.Drawing.Size(120, 40);
			this.layoutControlItemOK.MinSize = new System.Drawing.Size(120, 40);
			this.layoutControlItemOK.Name = "layoutControlItemOK";
			this.layoutControlItemOK.Size = new System.Drawing.Size(120, 40);
			this.layoutControlItemOK.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.layoutControlItemOK.Text = "OK";
			this.layoutControlItemOK.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItemOK.TextVisible = false;
			this.layoutControlItemOK.TrimClientAreaToControl = false;
			// 
			// emptySpaceItem3
			// 
			this.emptySpaceItem3.AllowHotTrack = false;
			this.emptySpaceItem3.Location = new System.Drawing.Point(0, 90);
			this.emptySpaceItem3.MaxSize = new System.Drawing.Size(0, 10);
			this.emptySpaceItem3.MinSize = new System.Drawing.Size(10, 10);
			this.emptySpaceItem3.Name = "emptySpaceItem3";
			this.emptySpaceItem3.Size = new System.Drawing.Size(299, 10);
			this.emptySpaceItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
			// 
			// layoutControlItemSelectedColor
			// 
			this.layoutControlItemSelectedColor.Control = this.pnSelectedColor;
			this.layoutControlItemSelectedColor.ControlAlignment = System.Drawing.ContentAlignment.MiddleCenter;
			this.layoutControlItemSelectedColor.FillControlToClientArea = false;
			this.layoutControlItemSelectedColor.Location = new System.Drawing.Point(0, 30);
			this.layoutControlItemSelectedColor.Name = "layoutControlItemSelectedColor";
			this.layoutControlItemSelectedColor.Size = new System.Drawing.Size(299, 60);
			this.layoutControlItemSelectedColor.Text = "Selected Color";
			this.layoutControlItemSelectedColor.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItemSelectedColor.TextVisible = false;
			this.layoutControlItemSelectedColor.TrimClientAreaToControl = false;
			// 
			// emptySpaceItem4
			// 
			this.emptySpaceItem4.AllowHotTrack = false;
			this.emptySpaceItem4.Location = new System.Drawing.Point(0, 124);
			this.emptySpaceItem4.MaxSize = new System.Drawing.Size(0, 10);
			this.emptySpaceItem4.MinSize = new System.Drawing.Size(10, 10);
			this.emptySpaceItem4.Name = "emptySpaceItem4";
			this.emptySpaceItem4.Size = new System.Drawing.Size(299, 10);
			this.emptySpaceItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.emptySpaceItem4.TextSize = new System.Drawing.Size(0, 0);
			// 
			// checkEditApplyForAll
			// 
			this.checkEditApplyForAll.Location = new System.Drawing.Point(12, 112);
			this.checkEditApplyForAll.Name = "checkEditApplyForAll";
			this.checkEditApplyForAll.Properties.Caption = "Apply for all notes";
			this.checkEditApplyForAll.Size = new System.Drawing.Size(295, 20);
			this.checkEditApplyForAll.StyleController = this.layoutControl;
			this.checkEditApplyForAll.TabIndex = 12;
			this.checkEditApplyForAll.CheckedChanged += new System.EventHandler(this.OnApplyForAllCheckedChanged);
			// 
			// layoutControlItemApplyForAll
			// 
			this.layoutControlItemApplyForAll.Control = this.checkEditApplyForAll;
			this.layoutControlItemApplyForAll.Location = new System.Drawing.Point(0, 100);
			this.layoutControlItemApplyForAll.Name = "layoutControlItemApplyForAll";
			this.layoutControlItemApplyForAll.Size = new System.Drawing.Size(299, 24);
			this.layoutControlItemApplyForAll.Text = "Apply For All";
			this.layoutControlItemApplyForAll.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItemApplyForAll.TextVisible = false;
			// 
			// simpleLabelItemTitle
			// 
			this.simpleLabelItemTitle.AllowHotTrack = false;
			this.simpleLabelItemTitle.AllowHtmlStringInCaption = true;
			this.simpleLabelItemTitle.AppearanceItemCaption.Options.UseTextOptions = true;
			this.simpleLabelItemTitle.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.simpleLabelItemTitle.AppearanceItemCaption.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.None;
			this.simpleLabelItemTitle.AppearanceItemCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.simpleLabelItemTitle.AppearanceItemCaption.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.simpleLabelItemTitle.Location = new System.Drawing.Point(0, 0);
			this.simpleLabelItemTitle.Name = "simpleLabelItemTitle";
			this.simpleLabelItemTitle.Size = new System.Drawing.Size(299, 20);
			this.simpleLabelItemTitle.Text = "Selected Color (Double-click to change):";
			this.simpleLabelItemTitle.TextSize = new System.Drawing.Size(233, 16);
			// 
			// FormNoteColor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(319, 194);
			this.Controls.Add(this.layoutControl);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormNoteColor";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Select Color";
			this.Load += new System.EventHandler(this.OnFormLoad);
			((System.ComponentModel.ISupportInitialize)(this.layoutControl)).EndInit();
			this.layoutControl.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupRoot)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCancel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemOK)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemSelectedColor)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditApplyForAll.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemApplyForAll)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.simpleLabelItemTitle)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX buttonXShow;
        private DevComponents.DotNetBar.ButtonX buttonXOK;
        private System.Windows.Forms.Panel pnSelectedColor;
		private DevExpress.XtraLayout.LayoutControl layoutControl;
		private DevExpress.XtraEditors.CheckEdit checkEditApplyForAll;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupRoot;
		private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItemCancel;
		private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItemOK;
		private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItemSelectedColor;
		private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem4;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItemApplyForAll;
		private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItemTitle;
	}
}