using Asa.Business.Common.Interfaces;

namespace Asa.Calendar.Controls.ToolForms
{
	partial class FormDayProperties
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
			this.components = new System.ComponentModel.Container();
			this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
			this.checkEditComment = new DevExpress.XtraEditors.CheckEdit();
			this.memoEditComment = new DevExpress.XtraEditors.MemoEdit();
			this.buttonXOK = new DevComponents.DotNetBar.ButtonX();
			this.buttonXCancel = new DevComponents.DotNetBar.ButtonX();
			this.layoutControl = new DevExpress.XtraLayout.LayoutControl();
			this.layoutControlGroupRoot = new DevExpress.XtraLayout.LayoutControlGroup();
			this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
			this.layoutControlItemCommentValue = new DevExpress.XtraLayout.LayoutControlItem();
			this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
			this.layoutControlItemCommentToggle = new DevExpress.XtraLayout.LayoutControlItem();
			this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
			this.simpleLabelItemDisclaimer = new DevExpress.XtraLayout.SimpleLabelItem();
			this.layoutControlItemOK = new DevExpress.XtraLayout.LayoutControlItem();
			this.emptySpaceItem4 = new DevExpress.XtraLayout.EmptySpaceItem();
			this.layoutControlItemCancel = new DevExpress.XtraLayout.LayoutControlItem();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditComment.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.memoEditComment.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl)).BeginInit();
			this.layoutControl.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupRoot)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCommentValue)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCommentToggle)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.simpleLabelItemDisclaimer)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemOK)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCancel)).BeginInit();
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
			// checkEditComment
			// 
			this.checkEditComment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.checkEditComment.Location = new System.Drawing.Point(12, 12);
			this.checkEditComment.Name = "checkEditComment";
			this.checkEditComment.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
			this.checkEditComment.Properties.Appearance.Options.UseForeColor = true;
			this.checkEditComment.Properties.Caption = "Show Comment";
			this.checkEditComment.Size = new System.Drawing.Size(325, 20);
			this.checkEditComment.StyleController = this.layoutControl;
			this.checkEditComment.TabIndex = 36;
			this.checkEditComment.CheckedChanged += new System.EventHandler(this.OnCommentCheckedChanged);
			// 
			// memoEditComment
			// 
			this.memoEditComment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.memoEditComment.Enabled = false;
			this.memoEditComment.Location = new System.Drawing.Point(12, 46);
			this.memoEditComment.Name = "memoEditComment";
			this.memoEditComment.Properties.Appearance.BackColor = System.Drawing.Color.White;
			this.memoEditComment.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
			this.memoEditComment.Properties.Appearance.Options.UseBackColor = true;
			this.memoEditComment.Properties.Appearance.Options.UseForeColor = true;
			this.memoEditComment.Properties.NullText = "Type Here";
			this.memoEditComment.Size = new System.Drawing.Size(325, 77);
			this.memoEditComment.StyleController = this.layoutControl;
			this.memoEditComment.TabIndex = 37;
			// 
			// buttonXOK
			// 
			this.buttonXOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonXOK.Location = new System.Drawing.Point(351, 12);
			this.buttonXOK.Name = "buttonXOK";
			this.buttonXOK.Size = new System.Drawing.Size(116, 36);
			this.buttonXOK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXOK.TabIndex = 39;
			this.buttonXOK.Text = "OK";
			this.buttonXOK.TextColor = System.Drawing.Color.Black;
			// 
			// buttonXCancel
			// 
			this.buttonXCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonXCancel.Location = new System.Drawing.Point(351, 114);
			this.buttonXCancel.Name = "buttonXCancel";
			this.buttonXCancel.Size = new System.Drawing.Size(116, 36);
			this.buttonXCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXCancel.TabIndex = 40;
			this.buttonXCancel.Text = "Cancel";
			this.buttonXCancel.TextColor = System.Drawing.Color.Black;
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
			this.layoutControl.Controls.Add(this.buttonXCancel);
			this.layoutControl.Controls.Add(this.buttonXOK);
			this.layoutControl.Controls.Add(this.checkEditComment);
			this.layoutControl.Controls.Add(this.memoEditComment);
			this.layoutControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutControl.ForeColor = System.Drawing.Color.Black;
			this.layoutControl.Location = new System.Drawing.Point(0, 0);
			this.layoutControl.Name = "layoutControl";
			this.layoutControl.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(802, 383, 250, 350);
			this.layoutControl.Root = this.layoutControlGroupRoot;
			this.layoutControl.Size = new System.Drawing.Size(479, 162);
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
            this.layoutControlItemCommentValue,
            this.emptySpaceItem2,
            this.layoutControlItemCommentToggle,
            this.emptySpaceItem3,
            this.simpleLabelItemDisclaimer,
            this.layoutControlItemOK,
            this.emptySpaceItem4,
            this.layoutControlItemCancel});
			this.layoutControlGroupRoot.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroupRoot.Name = "Root";
			this.layoutControlGroupRoot.Size = new System.Drawing.Size(479, 162);
			this.layoutControlGroupRoot.TextVisible = false;
			// 
			// emptySpaceItem1
			// 
			this.emptySpaceItem1.AllowHotTrack = false;
			this.emptySpaceItem1.Location = new System.Drawing.Point(329, 0);
			this.emptySpaceItem1.MaxSize = new System.Drawing.Size(10, 0);
			this.emptySpaceItem1.MinSize = new System.Drawing.Size(10, 10);
			this.emptySpaceItem1.Name = "emptySpaceItem1";
			this.emptySpaceItem1.Size = new System.Drawing.Size(10, 142);
			this.emptySpaceItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
			// 
			// layoutControlItemCommentValue
			// 
			this.layoutControlItemCommentValue.Control = this.memoEditComment;
			this.layoutControlItemCommentValue.ControlAlignment = System.Drawing.ContentAlignment.MiddleCenter;
			this.layoutControlItemCommentValue.FillControlToClientArea = false;
			this.layoutControlItemCommentValue.Location = new System.Drawing.Point(0, 34);
			this.layoutControlItemCommentValue.Name = "layoutControlItemCommentValue";
			this.layoutControlItemCommentValue.Size = new System.Drawing.Size(329, 81);
			this.layoutControlItemCommentValue.Text = "Comment Value";
			this.layoutControlItemCommentValue.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItemCommentValue.TextVisible = false;
			this.layoutControlItemCommentValue.TrimClientAreaToControl = false;
			// 
			// emptySpaceItem2
			// 
			this.emptySpaceItem2.AllowHotTrack = false;
			this.emptySpaceItem2.Location = new System.Drawing.Point(0, 24);
			this.emptySpaceItem2.MaxSize = new System.Drawing.Size(0, 10);
			this.emptySpaceItem2.MinSize = new System.Drawing.Size(10, 10);
			this.emptySpaceItem2.Name = "emptySpaceItem2";
			this.emptySpaceItem2.Size = new System.Drawing.Size(329, 10);
			this.emptySpaceItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
			// 
			// layoutControlItemCommentToggle
			// 
			this.layoutControlItemCommentToggle.Control = this.checkEditComment;
			this.layoutControlItemCommentToggle.ControlAlignment = System.Drawing.ContentAlignment.MiddleLeft;
			this.layoutControlItemCommentToggle.FillControlToClientArea = false;
			this.layoutControlItemCommentToggle.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItemCommentToggle.Name = "layoutControlItemCommentToggle";
			this.layoutControlItemCommentToggle.Size = new System.Drawing.Size(329, 24);
			this.layoutControlItemCommentToggle.Text = "Comment Toggle";
			this.layoutControlItemCommentToggle.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItemCommentToggle.TextVisible = false;
			this.layoutControlItemCommentToggle.TrimClientAreaToControl = false;
			// 
			// emptySpaceItem3
			// 
			this.emptySpaceItem3.AllowHotTrack = false;
			this.emptySpaceItem3.Location = new System.Drawing.Point(0, 115);
			this.emptySpaceItem3.MaxSize = new System.Drawing.Size(0, 10);
			this.emptySpaceItem3.MinSize = new System.Drawing.Size(10, 10);
			this.emptySpaceItem3.Name = "emptySpaceItem3";
			this.emptySpaceItem3.Size = new System.Drawing.Size(329, 10);
			this.emptySpaceItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
			// 
			// simpleLabelItemDisclaimer
			// 
			this.simpleLabelItemDisclaimer.AllowHotTrack = false;
			this.simpleLabelItemDisclaimer.AllowHtmlStringInCaption = true;
			this.simpleLabelItemDisclaimer.AppearanceItemCaption.Options.UseTextOptions = true;
			this.simpleLabelItemDisclaimer.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.simpleLabelItemDisclaimer.AppearanceItemCaption.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.None;
			this.simpleLabelItemDisclaimer.AppearanceItemCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.simpleLabelItemDisclaimer.AppearanceItemCaption.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.simpleLabelItemDisclaimer.Location = new System.Drawing.Point(0, 125);
			this.simpleLabelItemDisclaimer.Name = "simpleLabelItemDisclaimer";
			this.simpleLabelItemDisclaimer.Size = new System.Drawing.Size(329, 17);
			this.simpleLabelItemDisclaimer.Text = "<size=-2>*Keep Comments Short & Sweet</size>";
			this.simpleLabelItemDisclaimer.TextSize = new System.Drawing.Size(152, 13);
			// 
			// layoutControlItemOK
			// 
			this.layoutControlItemOK.Control = this.buttonXOK;
			this.layoutControlItemOK.ControlAlignment = System.Drawing.ContentAlignment.MiddleCenter;
			this.layoutControlItemOK.FillControlToClientArea = false;
			this.layoutControlItemOK.Location = new System.Drawing.Point(339, 0);
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
			// emptySpaceItem4
			// 
			this.emptySpaceItem4.AllowHotTrack = false;
			this.emptySpaceItem4.Location = new System.Drawing.Point(339, 40);
			this.emptySpaceItem4.Name = "emptySpaceItem4";
			this.emptySpaceItem4.Size = new System.Drawing.Size(120, 62);
			this.emptySpaceItem4.TextSize = new System.Drawing.Size(0, 0);
			// 
			// layoutControlItemCancel
			// 
			this.layoutControlItemCancel.Control = this.buttonXCancel;
			this.layoutControlItemCancel.ControlAlignment = System.Drawing.ContentAlignment.MiddleCenter;
			this.layoutControlItemCancel.FillControlToClientArea = false;
			this.layoutControlItemCancel.Location = new System.Drawing.Point(339, 102);
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
			// FormDayProperties
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(479, 162);
			this.Controls.Add(this.layoutControl);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormDayProperties";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Day Properties:";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormClosing);
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditComment.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.memoEditComment.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl)).EndInit();
			this.layoutControl.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupRoot)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCommentValue)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCommentToggle)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.simpleLabelItemDisclaimer)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemOK)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCancel)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraEditors.StyleController styleController;
		private DevExpress.XtraEditors.CheckEdit checkEditComment;
		public DevExpress.XtraEditors.MemoEdit memoEditComment;
		private DevComponents.DotNetBar.ButtonX buttonXOK;
		private DevComponents.DotNetBar.ButtonX buttonXCancel;
		private DevExpress.XtraLayout.LayoutControl layoutControl;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupRoot;
		private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItemCommentValue;
		private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItemCommentToggle;
		private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
		private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItemDisclaimer;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItemOK;
		private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem4;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItemCancel;
	}
}