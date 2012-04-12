﻿namespace NewBizWizForm.TabHomeForms
{
    partial class SimpleSummaryItemControl
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
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            this.laNumber = new System.Windows.Forms.Label();
            this.panelExMain = new DevComponents.DotNetBar.PanelEx();
            this.ckItem = new System.Windows.Forms.CheckBox();
            this.laTotal = new System.Windows.Forms.Label();
            this.ckDetails = new System.Windows.Forms.CheckBox();
            this.ckTotal = new System.Windows.Forms.CheckBox();
            this.laMonthly = new System.Windows.Forms.Label();
            this.ckMonthly = new System.Windows.Forms.CheckBox();
            this.comboBoxEditItem = new DevExpress.XtraEditors.ComboBoxEdit();
            this.spinEditMonthly = new DevExpress.XtraEditors.SpinEdit();
            this.spinEditTotal = new DevExpress.XtraEditors.SpinEdit();
            this.memoEditDetails = new DevExpress.XtraEditors.MemoEdit();
            this.pbUp = new System.Windows.Forms.PictureBox();
            this.pbDown = new System.Windows.Forms.PictureBox();
            this.pbDelete = new System.Windows.Forms.PictureBox();
            this.panelExMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditItem.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditMonthly.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditTotal.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEditDetails.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbUp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDelete)).BeginInit();
            this.SuspendLayout();
            // 
            // laNumber
            // 
            this.laNumber.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laNumber.Location = new System.Drawing.Point(2, 2);
            this.laNumber.Name = "laNumber";
            this.laNumber.Size = new System.Drawing.Size(42, 42);
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
            this.panelExMain.Controls.Add(this.ckItem);
            this.panelExMain.Controls.Add(this.laTotal);
            this.panelExMain.Controls.Add(this.ckDetails);
            this.panelExMain.Controls.Add(this.ckTotal);
            this.panelExMain.Controls.Add(this.laMonthly);
            this.panelExMain.Controls.Add(this.ckMonthly);
            this.panelExMain.Controls.Add(this.comboBoxEditItem);
            this.panelExMain.Controls.Add(this.memoEditDetails);
            this.panelExMain.Controls.Add(this.spinEditMonthly);
            this.panelExMain.Controls.Add(this.spinEditTotal);
            this.panelExMain.Location = new System.Drawing.Point(45, 12);
            this.panelExMain.Name = "panelExMain";
            this.panelExMain.Size = new System.Drawing.Size(509, 99);
            this.panelExMain.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelExMain.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelExMain.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelExMain.Style.Border = DevComponents.DotNetBar.eBorderType.RaisedInner;
            this.panelExMain.Style.BorderColor.Color = System.Drawing.Color.White;
            this.panelExMain.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelExMain.Style.GradientAngle = 90;
            this.panelExMain.TabIndex = 30;
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
            this.ckDetails.Checked = true;
            this.ckDetails.CheckState = System.Windows.Forms.CheckState.Checked;
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
            this.ckTotal.Checked = true;
            this.ckTotal.CheckState = System.Windows.Forms.CheckState.Checked;
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
            this.ckMonthly.Checked = true;
            this.ckMonthly.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckMonthly.Location = new System.Drawing.Point(9, 56);
            this.ckMonthly.Name = "ckMonthly";
            this.ckMonthly.Size = new System.Drawing.Size(15, 14);
            this.ckMonthly.TabIndex = 30;
            this.ckMonthly.TabStop = false;
            this.ckMonthly.UseVisualStyleBackColor = true;
            this.ckMonthly.CheckedChanged += new System.EventHandler(this.ckMonthly_CheckedChanged);
            // 
            // comboBoxEditItem
            // 
            this.comboBoxEditItem.Location = new System.Drawing.Point(30, 8);
            this.comboBoxEditItem.Name = "comboBoxEditItem";
            this.comboBoxEditItem.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9F);
            this.comboBoxEditItem.Properties.Appearance.Options.UseFont = true;
            this.comboBoxEditItem.Properties.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 9F);
            this.comboBoxEditItem.Properties.AppearanceDisabled.Options.UseFont = true;
            this.comboBoxEditItem.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Arial", 9F);
            this.comboBoxEditItem.Properties.AppearanceDropDown.Options.UseFont = true;
            this.comboBoxEditItem.Properties.AppearanceFocused.Font = new System.Drawing.Font("Arial", 9F);
            this.comboBoxEditItem.Properties.AppearanceFocused.Options.UseFont = true;
            this.comboBoxEditItem.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("Arial", 9F);
            this.comboBoxEditItem.Properties.AppearanceReadOnly.Options.UseFont = true;
            this.comboBoxEditItem.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEditItem.Properties.NullText = "Type or Select";
            this.comboBoxEditItem.Size = new System.Drawing.Size(264, 21);
            this.comboBoxEditItem.TabIndex = 0;
            this.comboBoxEditItem.EditValueChanged += new System.EventHandler(this.comboBoxEditItem_EditValueChanged);
            // 
            // spinEditMonthly
            // 
            this.spinEditMonthly.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinEditMonthly.Location = new System.Drawing.Point(30, 52);
            this.spinEditMonthly.Name = "spinEditMonthly";
            this.spinEditMonthly.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9F);
            this.spinEditMonthly.Properties.Appearance.Options.UseFont = true;
            this.spinEditMonthly.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, true, false, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true)});
            this.spinEditMonthly.Properties.DisplayFormat.FormatString = "$#,###.00";
            this.spinEditMonthly.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.spinEditMonthly.Properties.EditFormat.FormatString = "$#,###.00";
            this.spinEditMonthly.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.spinEditMonthly.Size = new System.Drawing.Size(106, 21);
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
            this.spinEditTotal.Location = new System.Drawing.Point(188, 52);
            this.spinEditTotal.Name = "spinEditTotal";
            this.spinEditTotal.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9F);
            this.spinEditTotal.Properties.Appearance.Options.UseFont = true;
            this.spinEditTotal.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, true, false, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject2, "", null, null, true)});
            this.spinEditTotal.Properties.DisplayFormat.FormatString = "$#,###.00";
            this.spinEditTotal.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.spinEditTotal.Properties.EditFormat.FormatString = "$#,###.00";
            this.spinEditTotal.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.spinEditTotal.Size = new System.Drawing.Size(106, 21);
            this.spinEditTotal.TabIndex = 2;
            this.spinEditTotal.EditValueChanged += new System.EventHandler(this.spinEditTotal_EditValueChanged);
            // 
            // memoEditDetails
            // 
            this.memoEditDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.memoEditDetails.Location = new System.Drawing.Point(321, 8);
            this.memoEditDetails.Name = "memoEditDetails";
            this.memoEditDetails.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9F);
            this.memoEditDetails.Properties.Appearance.Options.UseFont = true;
            this.memoEditDetails.Properties.NullText = "Brief Overview...";
            this.memoEditDetails.Size = new System.Drawing.Size(182, 84);
            this.memoEditDetails.TabIndex = 3;
            this.memoEditDetails.EditValueChanged += new System.EventHandler(this.memoEditDetails_EditValueChanged);
            // 
            // pbUp
            // 
            this.pbUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbUp.Image = global::NewBizWizForm.Properties.Resources.ArrowUpGreenSmall;
            this.pbUp.Location = new System.Drawing.Point(560, 45);
            this.pbUp.Name = "pbUp";
            this.pbUp.Size = new System.Drawing.Size(32, 32);
            this.pbUp.TabIndex = 36;
            this.pbUp.TabStop = false;
            this.pbUp.Click += new System.EventHandler(this.pbUp_Click);
            this.pbUp.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
            this.pbUp.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseUp);
            // 
            // pbDown
            // 
            this.pbDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbDown.Image = global::NewBizWizForm.Properties.Resources.ArrowDownGreenSmall;
            this.pbDown.Location = new System.Drawing.Point(560, 79);
            this.pbDown.Name = "pbDown";
            this.pbDown.Size = new System.Drawing.Size(32, 32);
            this.pbDown.TabIndex = 35;
            this.pbDown.TabStop = false;
            this.pbDown.Click += new System.EventHandler(this.pbDown_Click);
            this.pbDown.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
            this.pbDown.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseUp);
            // 
            // pbDelete
            // 
            this.pbDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbDelete.Image = global::NewBizWizForm.Properties.Resources.DeleteTiny;
            this.pbDelete.Location = new System.Drawing.Point(560, 11);
            this.pbDelete.Name = "pbDelete";
            this.pbDelete.Size = new System.Drawing.Size(32, 32);
            this.pbDelete.TabIndex = 34;
            this.pbDelete.TabStop = false;
            this.pbDelete.Click += new System.EventHandler(this.pbDelete_Click);
            this.pbDelete.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
            this.pbDelete.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseUp);
            // 
            // SimpleSummaryItemControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.pbUp);
            this.Controls.Add(this.pbDown);
            this.Controls.Add(this.pbDelete);
            this.Controls.Add(this.panelExMain);
            this.Controls.Add(this.laNumber);
            this.Name = "SimpleSummaryItemControl";
            this.Size = new System.Drawing.Size(595, 123);
            this.Load += new System.EventHandler(this.SimpleSummaryItemControl_Load);
            this.panelExMain.ResumeLayout(false);
            this.panelExMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditItem.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditMonthly.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditTotal.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEditDetails.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbUp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDelete)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label laNumber;
        private DevComponents.DotNetBar.PanelEx panelExMain;
        private System.Windows.Forms.PictureBox pbUp;
        private System.Windows.Forms.PictureBox pbDown;
        private System.Windows.Forms.PictureBox pbDelete;
        private System.Windows.Forms.Label laTotal;
        private System.Windows.Forms.CheckBox ckTotal;
        private System.Windows.Forms.Label laMonthly;
        private System.Windows.Forms.CheckBox ckMonthly;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEditItem;
        private DevExpress.XtraEditors.SpinEdit spinEditMonthly;
        private DevExpress.XtraEditors.SpinEdit spinEditTotal;
        private DevExpress.XtraEditors.MemoEdit memoEditDetails;
        private System.Windows.Forms.CheckBox ckItem;
        public System.Windows.Forms.CheckBox ckDetails;
    }
}
