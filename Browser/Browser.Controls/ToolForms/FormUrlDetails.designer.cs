namespace Asa.Browser.Controls.ToolForms
{
	partial class FormUrlDetails
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
			this.buttonXCancel = new DevComponents.DotNetBar.ButtonX();
			this.buttonXCopy = new DevComponents.DotNetBar.ButtonX();
			this.buttonXEmail = new DevComponents.DotNetBar.ButtonX();
			this.layoutControl = new DevExpress.XtraLayout.LayoutControl();
			this.layoutControlGroupRoot = new DevExpress.XtraLayout.LayoutControlGroup();
			this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
			this.pictureEditTitleLogo = new DevExpress.XtraEditors.PictureEdit();
			this.layoutControlItemLogo = new DevExpress.XtraLayout.LayoutControlItem();
			this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
			this.simpleLabelItemTitle = new DevExpress.XtraLayout.SimpleLabelItem();
			this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
			this.simpleLabelItemWebAddressTitle = new DevExpress.XtraLayout.SimpleLabelItem();
			this.simpleLabelItemWebAddressValue = new DevExpress.XtraLayout.SimpleLabelItem();
			this.emptySpaceItem4 = new DevExpress.XtraLayout.EmptySpaceItem();
			this.layoutControlItemCancel = new DevExpress.XtraLayout.LayoutControlItem();
			this.emptySpaceItem5 = new DevExpress.XtraLayout.EmptySpaceItem();
			this.layoutControlItemCopy = new DevExpress.XtraLayout.LayoutControlItem();
			this.emptySpaceItem6 = new DevExpress.XtraLayout.EmptySpaceItem();
			this.layoutControlItemEmail = new DevExpress.XtraLayout.LayoutControlItem();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl)).BeginInit();
			this.layoutControl.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupRoot)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureEditTitleLogo.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemLogo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.simpleLabelItemTitle)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.simpleLabelItemWebAddressTitle)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.simpleLabelItemWebAddressValue)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCancel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCopy)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem6)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemEmail)).BeginInit();
			this.SuspendLayout();
			// 
			// buttonXCancel
			// 
			this.buttonXCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonXCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonXCancel.Location = new System.Drawing.Point(289, 182);
			this.buttonXCancel.Name = "buttonXCancel";
			this.buttonXCancel.Size = new System.Drawing.Size(94, 36);
			this.buttonXCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXCancel.TabIndex = 5;
			this.buttonXCancel.Text = "Cancel";
			this.buttonXCancel.TextColor = System.Drawing.Color.Green;
			// 
			// buttonXCopy
			// 
			this.buttonXCopy.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonXCopy.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXCopy.Location = new System.Drawing.Point(152, 182);
			this.buttonXCopy.Name = "buttonXCopy";
			this.buttonXCopy.Size = new System.Drawing.Size(94, 36);
			this.buttonXCopy.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXCopy.TabIndex = 4;
			this.buttonXCopy.Text = "Copy URL";
			this.buttonXCopy.TextColor = System.Drawing.Color.Green;
			this.buttonXCopy.Click += new System.EventHandler(this.OnCopyClick);
			// 
			// buttonXEmail
			// 
			this.buttonXEmail.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXEmail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonXEmail.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXEmail.Location = new System.Drawing.Point(12, 182);
			this.buttonXEmail.Name = "buttonXEmail";
			this.buttonXEmail.Size = new System.Drawing.Size(94, 36);
			this.buttonXEmail.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXEmail.TabIndex = 3;
			this.buttonXEmail.Text = "Email URL";
			this.buttonXEmail.TextColor = System.Drawing.Color.Green;
			this.buttonXEmail.Click += new System.EventHandler(this.OnEmailClick);
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
			this.layoutControl.Controls.Add(this.pictureEditTitleLogo);
			this.layoutControl.Controls.Add(this.buttonXEmail);
			this.layoutControl.Controls.Add(this.buttonXCopy);
			this.layoutControl.Controls.Add(this.buttonXCancel);
			this.layoutControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutControl.ForeColor = System.Drawing.Color.Black;
			this.layoutControl.Location = new System.Drawing.Point(0, 0);
			this.layoutControl.Name = "layoutControl";
			this.layoutControl.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(802, 383, 250, 350);
			this.layoutControl.Root = this.layoutControlGroupRoot;
			this.layoutControl.Size = new System.Drawing.Size(395, 230);
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
            this.layoutControlItemLogo,
            this.emptySpaceItem2,
            this.simpleLabelItemTitle,
            this.simpleLabelItemWebAddressTitle,
            this.simpleLabelItemWebAddressValue,
            this.emptySpaceItem4,
            this.emptySpaceItem3,
            this.layoutControlItemCancel,
            this.emptySpaceItem5,
            this.layoutControlItemCopy,
            this.emptySpaceItem6,
            this.layoutControlItemEmail});
			this.layoutControlGroupRoot.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroupRoot.Name = "Root";
			this.layoutControlGroupRoot.Size = new System.Drawing.Size(395, 230);
			this.layoutControlGroupRoot.TextVisible = false;
			// 
			// emptySpaceItem1
			// 
			this.emptySpaceItem1.AllowHotTrack = false;
			this.emptySpaceItem1.Location = new System.Drawing.Point(0, 48);
			this.emptySpaceItem1.MaxSize = new System.Drawing.Size(0, 30);
			this.emptySpaceItem1.MinSize = new System.Drawing.Size(10, 30);
			this.emptySpaceItem1.Name = "emptySpaceItem1";
			this.emptySpaceItem1.Size = new System.Drawing.Size(375, 30);
			this.emptySpaceItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
			// 
			// pictureEditTitleLogo
			// 
			this.pictureEditTitleLogo.Cursor = System.Windows.Forms.Cursors.Default;
			this.pictureEditTitleLogo.EditValue = global::Asa.Browser.Controls.Properties.Resources.UrlDetailsForm;
			this.pictureEditTitleLogo.Location = new System.Drawing.Point(10, 10);
			this.pictureEditTitleLogo.Name = "pictureEditTitleLogo";
			this.pictureEditTitleLogo.Properties.AllowFocused = false;
			this.pictureEditTitleLogo.Properties.Appearance.BackColor = System.Drawing.Color.White;
			this.pictureEditTitleLogo.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
			this.pictureEditTitleLogo.Properties.Appearance.Options.UseBackColor = true;
			this.pictureEditTitleLogo.Properties.Appearance.Options.UseForeColor = true;
			this.pictureEditTitleLogo.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.pictureEditTitleLogo.Properties.PictureAlignment = System.Drawing.ContentAlignment.TopLeft;
			this.pictureEditTitleLogo.Properties.ReadOnly = true;
			this.pictureEditTitleLogo.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
			this.pictureEditTitleLogo.Properties.ShowMenu = false;
			this.pictureEditTitleLogo.Properties.ZoomAccelerationFactor = 1D;
			this.pictureEditTitleLogo.Size = new System.Drawing.Size(48, 48);
			this.pictureEditTitleLogo.StyleController = this.layoutControl;
			this.pictureEditTitleLogo.TabIndex = 5;
			// 
			// layoutControlItemLogo
			// 
			this.layoutControlItemLogo.Control = this.pictureEditTitleLogo;
			this.layoutControlItemLogo.FillControlToClientArea = false;
			this.layoutControlItemLogo.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItemLogo.MaxSize = new System.Drawing.Size(48, 48);
			this.layoutControlItemLogo.MinSize = new System.Drawing.Size(48, 48);
			this.layoutControlItemLogo.Name = "layoutControlItemLogo";
			this.layoutControlItemLogo.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItemLogo.Size = new System.Drawing.Size(48, 48);
			this.layoutControlItemLogo.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.layoutControlItemLogo.Text = "Logo";
			this.layoutControlItemLogo.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItemLogo.TextVisible = false;
			this.layoutControlItemLogo.TrimClientAreaToControl = false;
			// 
			// emptySpaceItem2
			// 
			this.emptySpaceItem2.AllowHotTrack = false;
			this.emptySpaceItem2.Location = new System.Drawing.Point(48, 0);
			this.emptySpaceItem2.MaxSize = new System.Drawing.Size(30, 0);
			this.emptySpaceItem2.MinSize = new System.Drawing.Size(30, 10);
			this.emptySpaceItem2.Name = "emptySpaceItem2";
			this.emptySpaceItem2.Size = new System.Drawing.Size(30, 48);
			this.emptySpaceItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
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
			this.simpleLabelItemTitle.Location = new System.Drawing.Point(78, 0);
			this.simpleLabelItemTitle.Name = "simpleLabelItemTitle";
			this.simpleLabelItemTitle.Size = new System.Drawing.Size(297, 48);
			this.simpleLabelItemTitle.Text = "<color=green><size=+8>URL Details</size></color>";
			this.simpleLabelItemTitle.TextSize = new System.Drawing.Size(127, 27);
			// 
			// emptySpaceItem3
			// 
			this.emptySpaceItem3.AllowHotTrack = false;
			this.emptySpaceItem3.Location = new System.Drawing.Point(0, 98);
			this.emptySpaceItem3.MaxSize = new System.Drawing.Size(0, 10);
			this.emptySpaceItem3.MinSize = new System.Drawing.Size(10, 10);
			this.emptySpaceItem3.Name = "emptySpaceItem3";
			this.emptySpaceItem3.Size = new System.Drawing.Size(375, 10);
			this.emptySpaceItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
			// 
			// simpleLabelItemWebAddressTitle
			// 
			this.simpleLabelItemWebAddressTitle.AllowHotTrack = false;
			this.simpleLabelItemWebAddressTitle.Location = new System.Drawing.Point(0, 78);
			this.simpleLabelItemWebAddressTitle.Name = "simpleLabelItemWebAddressTitle";
			this.simpleLabelItemWebAddressTitle.Size = new System.Drawing.Size(375, 20);
			this.simpleLabelItemWebAddressTitle.Text = "Web Address:";
			this.simpleLabelItemWebAddressTitle.TextSize = new System.Drawing.Size(127, 16);
			// 
			// simpleLabelItemWebAddressValue
			// 
			this.simpleLabelItemWebAddressValue.AllowHotTrack = false;
			this.simpleLabelItemWebAddressValue.AllowHtmlStringInCaption = true;
			this.simpleLabelItemWebAddressValue.AppearanceItemCaption.Options.UseTextOptions = true;
			this.simpleLabelItemWebAddressValue.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.simpleLabelItemWebAddressValue.AppearanceItemCaption.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.None;
			this.simpleLabelItemWebAddressValue.AppearanceItemCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
			this.simpleLabelItemWebAddressValue.AppearanceItemCaption.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.simpleLabelItemWebAddressValue.Location = new System.Drawing.Point(0, 108);
			this.simpleLabelItemWebAddressValue.MaxSize = new System.Drawing.Size(375, 50);
			this.simpleLabelItemWebAddressValue.MinSize = new System.Drawing.Size(375, 50);
			this.simpleLabelItemWebAddressValue.Name = "simpleLabelItemWebAddressValue";
			this.simpleLabelItemWebAddressValue.Size = new System.Drawing.Size(375, 50);
			this.simpleLabelItemWebAddressValue.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.simpleLabelItemWebAddressValue.Text = " ";
			this.simpleLabelItemWebAddressValue.TextSize = new System.Drawing.Size(127, 16);
			// 
			// emptySpaceItem4
			// 
			this.emptySpaceItem4.AllowHotTrack = false;
			this.emptySpaceItem4.Location = new System.Drawing.Point(0, 158);
			this.emptySpaceItem4.Name = "emptySpaceItem4";
			this.emptySpaceItem4.Size = new System.Drawing.Size(375, 12);
			this.emptySpaceItem4.TextSize = new System.Drawing.Size(0, 0);
			// 
			// layoutControlItemCancel
			// 
			this.layoutControlItemCancel.Control = this.buttonXCancel;
			this.layoutControlItemCancel.ControlAlignment = System.Drawing.ContentAlignment.MiddleCenter;
			this.layoutControlItemCancel.FillControlToClientArea = false;
			this.layoutControlItemCancel.Location = new System.Drawing.Point(277, 170);
			this.layoutControlItemCancel.MaxSize = new System.Drawing.Size(98, 40);
			this.layoutControlItemCancel.MinSize = new System.Drawing.Size(98, 40);
			this.layoutControlItemCancel.Name = "layoutControlItemCancel";
			this.layoutControlItemCancel.Size = new System.Drawing.Size(98, 40);
			this.layoutControlItemCancel.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.layoutControlItemCancel.Text = "Cancel";
			this.layoutControlItemCancel.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItemCancel.TextVisible = false;
			this.layoutControlItemCancel.TrimClientAreaToControl = false;
			// 
			// emptySpaceItem5
			// 
			this.emptySpaceItem5.AllowHotTrack = false;
			this.emptySpaceItem5.Location = new System.Drawing.Point(238, 170);
			this.emptySpaceItem5.Name = "emptySpaceItem5";
			this.emptySpaceItem5.Size = new System.Drawing.Size(39, 40);
			this.emptySpaceItem5.TextSize = new System.Drawing.Size(0, 0);
			// 
			// layoutControlItemCopy
			// 
			this.layoutControlItemCopy.Control = this.buttonXCopy;
			this.layoutControlItemCopy.ControlAlignment = System.Drawing.ContentAlignment.MiddleCenter;
			this.layoutControlItemCopy.FillControlToClientArea = false;
			this.layoutControlItemCopy.Location = new System.Drawing.Point(140, 170);
			this.layoutControlItemCopy.MaxSize = new System.Drawing.Size(98, 40);
			this.layoutControlItemCopy.MinSize = new System.Drawing.Size(98, 40);
			this.layoutControlItemCopy.Name = "layoutControlItemCopy";
			this.layoutControlItemCopy.Size = new System.Drawing.Size(98, 40);
			this.layoutControlItemCopy.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.layoutControlItemCopy.Text = "Copy";
			this.layoutControlItemCopy.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItemCopy.TextVisible = false;
			this.layoutControlItemCopy.TrimClientAreaToControl = false;
			// 
			// emptySpaceItem6
			// 
			this.emptySpaceItem6.AllowHotTrack = false;
			this.emptySpaceItem6.Location = new System.Drawing.Point(98, 170);
			this.emptySpaceItem6.Name = "emptySpaceItem6";
			this.emptySpaceItem6.Size = new System.Drawing.Size(42, 40);
			this.emptySpaceItem6.TextSize = new System.Drawing.Size(0, 0);
			// 
			// layoutControlItemEmail
			// 
			this.layoutControlItemEmail.Control = this.buttonXEmail;
			this.layoutControlItemEmail.ControlAlignment = System.Drawing.ContentAlignment.MiddleCenter;
			this.layoutControlItemEmail.FillControlToClientArea = false;
			this.layoutControlItemEmail.Location = new System.Drawing.Point(0, 170);
			this.layoutControlItemEmail.MaxSize = new System.Drawing.Size(98, 40);
			this.layoutControlItemEmail.MinSize = new System.Drawing.Size(98, 40);
			this.layoutControlItemEmail.Name = "layoutControlItemEmail";
			this.layoutControlItemEmail.Size = new System.Drawing.Size(98, 40);
			this.layoutControlItemEmail.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.layoutControlItemEmail.Text = "Email";
			this.layoutControlItemEmail.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItemEmail.TextVisible = false;
			this.layoutControlItemEmail.TrimClientAreaToControl = false;
			// 
			// FormUrlDetails
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.ClientSize = new System.Drawing.Size(395, 230);
			this.Controls.Add(this.layoutControl);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormUrlDetails";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "URL Details";
			((System.ComponentModel.ISupportInitialize)(this.layoutControl)).EndInit();
			this.layoutControl.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupRoot)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureEditTitleLogo.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemLogo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.simpleLabelItemTitle)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.simpleLabelItemWebAddressTitle)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.simpleLabelItemWebAddressValue)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCancel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCopy)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem6)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemEmail)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevComponents.DotNetBar.ButtonX buttonXCancel;
		private DevComponents.DotNetBar.ButtonX buttonXCopy;
		private DevComponents.DotNetBar.ButtonX buttonXEmail;
		private DevExpress.XtraLayout.LayoutControl layoutControl;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupRoot;
		private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
		private DevExpress.XtraEditors.PictureEdit pictureEditTitleLogo;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItemLogo;
		private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
		private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItemTitle;
		private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItemWebAddressTitle;
		private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItemWebAddressValue;
		private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem4;
		private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItemCancel;
		private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem5;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItemCopy;
		private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem6;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItemEmail;
	}
}