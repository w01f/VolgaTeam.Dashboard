namespace Asa.Common.GUI.ToolForms
{
    partial class FormEmailFileName
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
			this.layoutControl = new DevExpress.XtraLayout.LayoutControl();
			this.buttonXOK = new DevComponents.DotNetBar.ButtonX();
			this.buttonXCancel = new DevComponents.DotNetBar.ButtonX();
			this.textEditFileName = new DevExpress.XtraEditors.TextEdit();
			this.pictureEditLogo = new DevExpress.XtraEditors.PictureEdit();
			this.layoutControlGroupRoot = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItemLogo = new DevExpress.XtraLayout.LayoutControlItem();
			this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
			this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
			this.layoutControlItemScheduleName = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItemCancel = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItemOK = new DevExpress.XtraLayout.LayoutControlItem();
			this.emptySpaceItem5 = new DevExpress.XtraLayout.EmptySpaceItem();
			this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl)).BeginInit();
			this.layoutControl.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.textEditFileName.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureEditLogo.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupRoot)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemLogo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemScheduleName)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCancel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemOK)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
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
			this.layoutControl.Controls.Add(this.buttonXOK);
			this.layoutControl.Controls.Add(this.buttonXCancel);
			this.layoutControl.Controls.Add(this.textEditFileName);
			this.layoutControl.Controls.Add(this.pictureEditLogo);
			this.layoutControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutControl.ForeColor = System.Drawing.Color.Black;
			this.layoutControl.Location = new System.Drawing.Point(0, 0);
			this.layoutControl.Name = "layoutControl";
			this.layoutControl.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(802, 383, 250, 350);
			this.layoutControl.Root = this.layoutControlGroupRoot;
			this.layoutControl.Size = new System.Drawing.Size(384, 137);
			this.layoutControl.TabIndex = 66;
			this.layoutControl.Text = "layoutControl1";
			// 
			// buttonXOK
			// 
			this.buttonXOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonXOK.Location = new System.Drawing.Point(12, 89);
			this.buttonXOK.Name = "buttonXOK";
			this.buttonXOK.Size = new System.Drawing.Size(116, 36);
			this.buttonXOK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXOK.TabIndex = 1;
			this.buttonXOK.Text = "Generate Email";
			this.buttonXOK.TextColor = System.Drawing.Color.Black;
			// 
			// buttonXCancel
			// 
			this.buttonXCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonXCancel.Location = new System.Drawing.Point(256, 89);
			this.buttonXCancel.Name = "buttonXCancel";
			this.buttonXCancel.Size = new System.Drawing.Size(116, 36);
			this.buttonXCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXCancel.TabIndex = 2;
			this.buttonXCancel.Text = "Cancel";
			this.buttonXCancel.TextColor = System.Drawing.Color.Black;
			// 
			// textEditScheduleName
			// 
			this.textEditFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textEditFileName.Location = new System.Drawing.Point(86, 43);
			this.textEditFileName.Name = "textEditFileName";
			this.textEditFileName.Properties.Appearance.BackColor = System.Drawing.Color.White;
			this.textEditFileName.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.textEditFileName.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
			this.textEditFileName.Properties.Appearance.Options.UseBackColor = true;
			this.textEditFileName.Properties.Appearance.Options.UseFont = true;
			this.textEditFileName.Properties.Appearance.Options.UseForeColor = true;
			this.textEditFileName.Properties.NullText = "Type here";
			this.textEditFileName.Size = new System.Drawing.Size(286, 22);
			this.textEditFileName.StyleController = this.layoutControl;
			this.textEditFileName.TabIndex = 0;
			// 
			// pictureEditLogo
			// 
			this.pictureEditLogo.Cursor = System.Windows.Forms.Cursors.Default;
			this.pictureEditLogo.EditValue = global::Asa.Common.GUI.Properties.Resources.Email;
			this.pictureEditLogo.Location = new System.Drawing.Point(12, 12);
			this.pictureEditLogo.Name = "pictureEditLogo";
			this.pictureEditLogo.Properties.Appearance.BackColor = System.Drawing.Color.White;
			this.pictureEditLogo.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
			this.pictureEditLogo.Properties.Appearance.Options.UseBackColor = true;
			this.pictureEditLogo.Properties.Appearance.Options.UseForeColor = true;
			this.pictureEditLogo.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.pictureEditLogo.Properties.ReadOnly = true;
			this.pictureEditLogo.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
			this.pictureEditLogo.Properties.ShowMenu = false;
			this.pictureEditLogo.Properties.ZoomAccelerationFactor = 1D;
			this.pictureEditLogo.Size = new System.Drawing.Size(60, 60);
			this.pictureEditLogo.StyleController = this.layoutControl;
			this.pictureEditLogo.TabIndex = 4;
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
            this.layoutControlItemLogo,
            this.emptySpaceItem1,
            this.emptySpaceItem2,
            this.layoutControlItemScheduleName,
            this.layoutControlItemCancel,
            this.layoutControlItemOK,
            this.emptySpaceItem5,
            this.emptySpaceItem3});
			this.layoutControlGroupRoot.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroupRoot.Name = "Root";
			this.layoutControlGroupRoot.Size = new System.Drawing.Size(384, 137);
			this.layoutControlGroupRoot.TextVisible = false;
			// 
			// layoutControlItemLogo
			// 
			this.layoutControlItemLogo.Control = this.pictureEditLogo;
			this.layoutControlItemLogo.ControlAlignment = System.Drawing.ContentAlignment.TopCenter;
			this.layoutControlItemLogo.FillControlToClientArea = false;
			this.layoutControlItemLogo.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItemLogo.MaxSize = new System.Drawing.Size(64, 64);
			this.layoutControlItemLogo.MinSize = new System.Drawing.Size(64, 64);
			this.layoutControlItemLogo.Name = "layoutControlItemLogo";
			this.layoutControlItemLogo.Size = new System.Drawing.Size(64, 67);
			this.layoutControlItemLogo.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.layoutControlItemLogo.Text = "Logo";
			this.layoutControlItemLogo.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItemLogo.TextVisible = false;
			this.layoutControlItemLogo.TrimClientAreaToControl = false;
			// 
			// emptySpaceItem1
			// 
			this.emptySpaceItem1.AllowHotTrack = false;
			this.emptySpaceItem1.Location = new System.Drawing.Point(0, 67);
			this.emptySpaceItem1.Name = "emptySpaceItem1";
			this.emptySpaceItem1.Size = new System.Drawing.Size(364, 10);
			this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
			// 
			// emptySpaceItem2
			// 
			this.emptySpaceItem2.AllowHotTrack = false;
			this.emptySpaceItem2.Location = new System.Drawing.Point(64, 0);
			this.emptySpaceItem2.MaxSize = new System.Drawing.Size(10, 0);
			this.emptySpaceItem2.MinSize = new System.Drawing.Size(10, 10);
			this.emptySpaceItem2.Name = "emptySpaceItem2";
			this.emptySpaceItem2.Size = new System.Drawing.Size(10, 67);
			this.emptySpaceItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
			// 
			// layoutControlItemScheduleName
			// 
			this.layoutControlItemScheduleName.AllowHtmlStringInCaption = true;
			this.layoutControlItemScheduleName.Control = this.textEditFileName;
			this.layoutControlItemScheduleName.Location = new System.Drawing.Point(74, 0);
			this.layoutControlItemScheduleName.Name = "layoutControlItemScheduleName";
			this.layoutControlItemScheduleName.Size = new System.Drawing.Size(290, 57);
			this.layoutControlItemScheduleName.Text = "File Name:";
			this.layoutControlItemScheduleName.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
			this.layoutControlItemScheduleName.TextLocation = DevExpress.Utils.Locations.Top;
			this.layoutControlItemScheduleName.TextSize = new System.Drawing.Size(63, 16);
			this.layoutControlItemScheduleName.TextToControlDistance = 15;
			// 
			// layoutControlItemCancel
			// 
			this.layoutControlItemCancel.Control = this.buttonXCancel;
			this.layoutControlItemCancel.ControlAlignment = System.Drawing.ContentAlignment.MiddleCenter;
			this.layoutControlItemCancel.FillControlToClientArea = false;
			this.layoutControlItemCancel.Location = new System.Drawing.Point(244, 77);
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
			// layoutControlItemOK
			// 
			this.layoutControlItemOK.Control = this.buttonXOK;
			this.layoutControlItemOK.ControlAlignment = System.Drawing.ContentAlignment.MiddleCenter;
			this.layoutControlItemOK.FillControlToClientArea = false;
			this.layoutControlItemOK.Location = new System.Drawing.Point(0, 77);
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
			// emptySpaceItem5
			// 
			this.emptySpaceItem5.AllowHotTrack = false;
			this.emptySpaceItem5.Location = new System.Drawing.Point(120, 77);
			this.emptySpaceItem5.Name = "emptySpaceItem5";
			this.emptySpaceItem5.Size = new System.Drawing.Size(124, 40);
			this.emptySpaceItem5.TextSize = new System.Drawing.Size(0, 0);
			// 
			// emptySpaceItem3
			// 
			this.emptySpaceItem3.AllowHotTrack = false;
			this.emptySpaceItem3.Location = new System.Drawing.Point(74, 57);
			this.emptySpaceItem3.Name = "emptySpaceItem3";
			this.emptySpaceItem3.Size = new System.Drawing.Size(290, 10);
			this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
			// 
			// FormEmailFileName
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(384, 137);
			this.Controls.Add(this.layoutControl);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormEmailFileName";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Name the Schedule File Before you Send it:";
			((System.ComponentModel.ISupportInitialize)(this.layoutControl)).EndInit();
			this.layoutControl.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.textEditFileName.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureEditLogo.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupRoot)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemLogo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemScheduleName)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCancel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemOK)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
			this.ResumeLayout(false);

        }

		#endregion

		private DevExpress.XtraLayout.LayoutControl layoutControl;
		private DevComponents.DotNetBar.ButtonX buttonXOK;
		private DevComponents.DotNetBar.ButtonX buttonXCancel;
		private DevExpress.XtraEditors.TextEdit textEditFileName;
		private DevExpress.XtraEditors.PictureEdit pictureEditLogo;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupRoot;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItemLogo;
		private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
		private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItemScheduleName;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItemCancel;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItemOK;
		private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem5;
		private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
	}
}