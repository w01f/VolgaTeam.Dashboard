namespace Asa.Media.Controls.PresentationClasses.ScheduleControls.ContentEditors
{
	partial class SectionSummaryProductItemControl
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
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
			this.laNumber = new System.Windows.Forms.Label();
			this.panelExMain = new DevComponents.DotNetBar.PanelEx();
			this.textEditItem = new DevExpress.XtraEditors.TextEdit();
			this.ckItem = new System.Windows.Forms.CheckBox();
			this.laTotal = new System.Windows.Forms.Label();
			this.ckDetails = new System.Windows.Forms.CheckBox();
			this.ckTotal = new System.Windows.Forms.CheckBox();
			this.laMonthly = new System.Windows.Forms.Label();
			this.ckMonthly = new System.Windows.Forms.CheckBox();
			this.memoEditDetails = new DevExpress.XtraEditors.MemoEdit();
			this.spinEditMonthly = new DevExpress.XtraEditors.SpinEdit();
			this.spinEditTotal = new DevExpress.XtraEditors.SpinEdit();
			this.buttonXUp = new DevComponents.DotNetBar.ButtonX();
			this.buttonXDown = new DevComponents.DotNetBar.ButtonX();
			this.buttonXReset = new DevComponents.DotNetBar.ButtonX();
			this.buttonXImportDigital = new DevComponents.DotNetBar.ButtonX();
			this.buttonXDelete = new DevComponents.DotNetBar.ButtonX();
			this.buttonXImportMedia = new DevComponents.DotNetBar.ButtonX();
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.panelExMain.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.textEditItem.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.memoEditDetails.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.spinEditMonthly.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.spinEditTotal.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// laNumber
			// 
			this.laNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.laNumber.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laNumber.Location = new System.Drawing.Point(19, 29);
			this.laNumber.Name = "laNumber";
			this.laNumber.Size = new System.Drawing.Size(32, 105);
			this.laNumber.TabIndex = 27;
			this.laNumber.Text = "1";
			this.laNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// panelExMain
			// 
			this.panelExMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panelExMain.CanvasColor = System.Drawing.SystemColors.Control;
			this.panelExMain.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
			this.panelExMain.Controls.Add(this.textEditItem);
			this.panelExMain.Controls.Add(this.ckItem);
			this.panelExMain.Controls.Add(this.laTotal);
			this.panelExMain.Controls.Add(this.ckDetails);
			this.panelExMain.Controls.Add(this.ckTotal);
			this.panelExMain.Controls.Add(this.laMonthly);
			this.panelExMain.Controls.Add(this.ckMonthly);
			this.panelExMain.Controls.Add(this.memoEditDetails);
			this.panelExMain.Controls.Add(this.spinEditMonthly);
			this.panelExMain.Controls.Add(this.spinEditTotal);
			this.panelExMain.DisabledBackColor = System.Drawing.Color.Empty;
			this.panelExMain.Location = new System.Drawing.Point(95, 29);
			this.panelExMain.Name = "panelExMain";
			this.panelExMain.Size = new System.Drawing.Size(383, 105);
			this.panelExMain.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.panelExMain.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.panelExMain.Style.BorderColor.Color = System.Drawing.Color.LightGray;
			this.panelExMain.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.panelExMain.Style.GradientAngle = 90;
			this.panelExMain.TabIndex = 30;
			// 
			// textEditItem
			// 
			this.textEditItem.Location = new System.Drawing.Point(30, 7);
			this.textEditItem.Name = "textEditItem";
			this.textEditItem.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.textEditItem.Properties.Appearance.Options.UseFont = true;
			this.textEditItem.Size = new System.Drawing.Size(264, 22);
			this.textEditItem.TabIndex = 38;
			this.textEditItem.EditValueChanged += new System.EventHandler(this.textEditItem_EditValueChanged);
			// 
			// ckItem
			// 
			this.ckItem.AutoSize = true;
			this.ckItem.Checked = true;
			this.ckItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.ckItem.Location = new System.Drawing.Point(9, 11);
			this.ckItem.Name = "ckItem";
			this.ckItem.Size = new System.Drawing.Size(15, 14);
			this.ckItem.TabIndex = 37;
			this.ckItem.UseVisualStyleBackColor = true;
			this.ckItem.CheckedChanged += new System.EventHandler(this.ckItem_CheckedChanged);
			// 
			// laTotal
			// 
			this.laTotal.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laTotal.Location = new System.Drawing.Point(188, 77);
			this.laTotal.Name = "laTotal";
			this.laTotal.Size = new System.Drawing.Size(106, 19);
			this.laTotal.TabIndex = 35;
			this.laTotal.Text = "Total";
			// 
			// ckDetails
			// 
			this.ckDetails.AutoSize = true;
			this.ckDetails.Location = new System.Drawing.Point(300, 11);
			this.ckDetails.Name = "ckDetails";
			this.ckDetails.Size = new System.Drawing.Size(15, 14);
			this.ckDetails.TabIndex = 36;
			this.ckDetails.UseVisualStyleBackColor = true;
			this.ckDetails.CheckedChanged += new System.EventHandler(this.ckDetails_CheckedChanged);
			// 
			// ckTotal
			// 
			this.ckTotal.AutoSize = true;
			this.ckTotal.Location = new System.Drawing.Point(167, 56);
			this.ckTotal.Name = "ckTotal";
			this.ckTotal.Size = new System.Drawing.Size(15, 14);
			this.ckTotal.TabIndex = 33;
			this.ckTotal.TabStop = false;
			this.ckTotal.UseVisualStyleBackColor = true;
			this.ckTotal.CheckedChanged += new System.EventHandler(this.ckTotal_CheckedChanged);
			// 
			// laMonthly
			// 
			this.laMonthly.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laMonthly.Location = new System.Drawing.Point(30, 77);
			this.laMonthly.Name = "laMonthly";
			this.laMonthly.Size = new System.Drawing.Size(106, 19);
			this.laMonthly.TabIndex = 32;
			this.laMonthly.Text = "Monthly";
			// 
			// ckMonthly
			// 
			this.ckMonthly.AutoSize = true;
			this.ckMonthly.Location = new System.Drawing.Point(9, 56);
			this.ckMonthly.Name = "ckMonthly";
			this.ckMonthly.Size = new System.Drawing.Size(15, 14);
			this.ckMonthly.TabIndex = 30;
			this.ckMonthly.TabStop = false;
			this.ckMonthly.UseVisualStyleBackColor = true;
			this.ckMonthly.CheckedChanged += new System.EventHandler(this.ckMonthly_CheckedChanged);
			// 
			// memoEditDetails
			// 
			this.memoEditDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.memoEditDetails.Enabled = false;
			this.memoEditDetails.Location = new System.Drawing.Point(321, 8);
			this.memoEditDetails.Name = "memoEditDetails";
			this.memoEditDetails.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9F);
			this.memoEditDetails.Properties.Appearance.Options.UseFont = true;
			this.memoEditDetails.Properties.NullText = "Brief Overview...";
			this.memoEditDetails.Size = new System.Drawing.Size(56, 90);
			this.memoEditDetails.TabIndex = 3;
			this.memoEditDetails.EditValueChanged += new System.EventHandler(this.memoEditDetails_EditValueChanged);
			// 
			// spinEditMonthly
			// 
			this.spinEditMonthly.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.spinEditMonthly.Enabled = false;
			this.spinEditMonthly.Location = new System.Drawing.Point(30, 52);
			this.spinEditMonthly.Name = "spinEditMonthly";
			this.spinEditMonthly.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9F);
			this.spinEditMonthly.Properties.Appearance.Options.UseFont = true;
			this.spinEditMonthly.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, true, false, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject3, "", null, null, true)});
			this.spinEditMonthly.Properties.DisplayFormat.FormatString = "$#,###.00";
			this.spinEditMonthly.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.spinEditMonthly.Properties.EditFormat.FormatString = "$#,###.00";
			this.spinEditMonthly.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.spinEditMonthly.Size = new System.Drawing.Size(106, 22);
			this.spinEditMonthly.TabIndex = 1;
			this.spinEditMonthly.EditValueChanged += new System.EventHandler(this.spinEditMonthly_EditValueChanged);
			// 
			// spinEditTotal
			// 
			this.spinEditTotal.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.spinEditTotal.Enabled = false;
			this.spinEditTotal.Location = new System.Drawing.Point(188, 52);
			this.spinEditTotal.Name = "spinEditTotal";
			this.spinEditTotal.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9F);
			this.spinEditTotal.Properties.Appearance.Options.UseFont = true;
			this.spinEditTotal.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, true, false, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true)});
			this.spinEditTotal.Properties.DisplayFormat.FormatString = "$#,###.00";
			this.spinEditTotal.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.spinEditTotal.Properties.EditFormat.FormatString = "$#,###.00";
			this.spinEditTotal.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.spinEditTotal.Size = new System.Drawing.Size(106, 22);
			this.spinEditTotal.TabIndex = 2;
			this.spinEditTotal.EditValueChanged += new System.EventHandler(this.spinEditTotal_EditValueChanged);
			// 
			// buttonXUp
			// 
			this.buttonXUp.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXUp.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXUp.Image = global::Asa.Media.Controls.Properties.Resources.SummaryItemUp;
			this.buttonXUp.Location = new System.Drawing.Point(57, 29);
			this.buttonXUp.Name = "buttonXUp";
			this.buttonXUp.Size = new System.Drawing.Size(32, 32);
			this.buttonXUp.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXUp.TabIndex = 40;
			this.toolTip.SetToolTip(this.buttonXUp, "Nudge Up");
			this.buttonXUp.Click += new System.EventHandler(this.OnUpItemClick);
			// 
			// buttonXDown
			// 
			this.buttonXDown.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonXDown.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXDown.Image = global::Asa.Media.Controls.Properties.Resources.SummaryItemDown;
			this.buttonXDown.Location = new System.Drawing.Point(57, 102);
			this.buttonXDown.Name = "buttonXDown";
			this.buttonXDown.Size = new System.Drawing.Size(32, 32);
			this.buttonXDown.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXDown.TabIndex = 41;
			this.toolTip.SetToolTip(this.buttonXDown, "Nudge Down");
			this.buttonXDown.Click += new System.EventHandler(this.OnDownItemClick);
			// 
			// buttonXReset
			// 
			this.buttonXReset.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXReset.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXReset.Image = global::Asa.Media.Controls.Properties.Resources.SummaryItemReset;
			this.buttonXReset.Location = new System.Drawing.Point(545, 29);
			this.buttonXReset.Name = "buttonXReset";
			this.buttonXReset.Size = new System.Drawing.Size(32, 32);
			this.buttonXReset.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXReset.TabIndex = 43;
			this.toolTip.SetToolTip(this.buttonXReset, "Wipe this Info");
			this.buttonXReset.Click += new System.EventHandler(this.OnResetItemClick);
			// 
			// buttonXImportDigital
			// 
			this.buttonXImportDigital.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXImportDigital.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXImportDigital.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXImportDigital.Image = global::Asa.Media.Controls.Properties.Resources.SummaryItemImportDigital;
			this.buttonXImportDigital.Location = new System.Drawing.Point(492, 102);
			this.buttonXImportDigital.Name = "buttonXImportDigital";
			this.buttonXImportDigital.Size = new System.Drawing.Size(32, 32);
			this.buttonXImportDigital.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXImportDigital.TabIndex = 44;
			this.toolTip.SetToolTip(this.buttonXImportDigital, "Import Digital Info");
			this.buttonXImportDigital.Click += new System.EventHandler(this.OnImportDigitalClick);
			// 
			// buttonXDelete
			// 
			this.buttonXDelete.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXDelete.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXDelete.Image = global::Asa.Media.Controls.Properties.Resources.SummaryItemDelete;
			this.buttonXDelete.Location = new System.Drawing.Point(545, 102);
			this.buttonXDelete.Name = "buttonXDelete";
			this.buttonXDelete.Size = new System.Drawing.Size(32, 32);
			this.buttonXDelete.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXDelete.TabIndex = 45;
			this.toolTip.SetToolTip(this.buttonXDelete, "Delete this line");
			this.buttonXDelete.Click += new System.EventHandler(this.OnDeleteItemClick);
			// 
			// buttonXImportMedia
			// 
			this.buttonXImportMedia.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXImportMedia.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXImportMedia.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXImportMedia.Image = global::Asa.Media.Controls.Properties.Resources.SummaryItemImportMedia;
			this.buttonXImportMedia.Location = new System.Drawing.Point(492, 29);
			this.buttonXImportMedia.Name = "buttonXImportMedia";
			this.buttonXImportMedia.Size = new System.Drawing.Size(32, 32);
			this.buttonXImportMedia.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXImportMedia.TabIndex = 42;
			this.buttonXImportMedia.Click += new System.EventHandler(this.OnImportMediaClick);
			// 
			// toolTip
			// 
			this.toolTip.BackColor = System.Drawing.Color.White;
			// 
			// SectionSummaryProductItemControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.buttonXDelete);
			this.Controls.Add(this.buttonXImportDigital);
			this.Controls.Add(this.buttonXReset);
			this.Controls.Add(this.buttonXImportMedia);
			this.Controls.Add(this.buttonXDown);
			this.Controls.Add(this.buttonXUp);
			this.Controls.Add(this.panelExMain);
			this.Controls.Add(this.laNumber);
			this.Name = "SectionSummaryProductItemControl";
			this.Size = new System.Drawing.Size(595, 160);
			this.panelExMain.ResumeLayout(false);
			this.panelExMain.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.textEditItem.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.memoEditDetails.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.spinEditMonthly.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.spinEditTotal.Properties)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label laNumber;
        private DevComponents.DotNetBar.PanelEx panelExMain;
        private System.Windows.Forms.Label laTotal;
        private System.Windows.Forms.CheckBox ckTotal;
        private System.Windows.Forms.Label laMonthly;
		private System.Windows.Forms.CheckBox ckMonthly;
        private DevExpress.XtraEditors.SpinEdit spinEditMonthly;
        private DevExpress.XtraEditors.SpinEdit spinEditTotal;
        private DevExpress.XtraEditors.MemoEdit memoEditDetails;
        private System.Windows.Forms.CheckBox ckItem;
        public System.Windows.Forms.CheckBox ckDetails;
		private DevExpress.XtraEditors.TextEdit textEditItem;
		private DevComponents.DotNetBar.ButtonX buttonXUp;
		private DevComponents.DotNetBar.ButtonX buttonXDown;
		private DevComponents.DotNetBar.ButtonX buttonXImportMedia;
		private DevComponents.DotNetBar.ButtonX buttonXReset;
		private DevComponents.DotNetBar.ButtonX buttonXImportDigital;
		private DevComponents.DotNetBar.ButtonX buttonXDelete;
		private System.Windows.Forms.ToolTip toolTip;
	}
}
