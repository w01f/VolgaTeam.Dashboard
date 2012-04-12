namespace NewBizWizForm.TabHomeForms
{
    partial class LeadoffStatementControl
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
            this.panelExMain = new DevComponents.DotNetBar.PanelEx();
            this.comboBoxEditSlideHeader = new DevExpress.XtraEditors.ComboBoxEdit();
            this.laDetail = new System.Windows.Forms.Label();
            this.ckC = new System.Windows.Forms.CheckBox();
            this.ckB = new System.Windows.Forms.CheckBox();
            this.ckA = new System.Windows.Forms.CheckBox();
            this.laSlideHeader = new System.Windows.Forms.Label();
            this.laTitle = new System.Windows.Forms.Label();
            this.memoEditC = new DevExpress.XtraEditors.MemoEdit();
            this.memoEditB = new DevExpress.XtraEditors.MemoEdit();
            this.memoEditA = new DevExpress.XtraEditors.MemoEdit();
            this.panelExMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditSlideHeader.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEditC.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEditB.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEditA.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelExMain
            // 
            this.panelExMain.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelExMain.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.panelExMain.Controls.Add(this.comboBoxEditSlideHeader);
            this.panelExMain.Controls.Add(this.laDetail);
            this.panelExMain.Controls.Add(this.ckC);
            this.panelExMain.Controls.Add(this.ckB);
            this.panelExMain.Controls.Add(this.ckA);
            this.panelExMain.Controls.Add(this.laSlideHeader);
            this.panelExMain.Controls.Add(this.laTitle);
            this.panelExMain.Controls.Add(this.memoEditC);
            this.panelExMain.Controls.Add(this.memoEditB);
            this.panelExMain.Controls.Add(this.memoEditA);
            this.panelExMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelExMain.Location = new System.Drawing.Point(0, 0);
            this.panelExMain.Name = "panelExMain";
            this.panelExMain.Size = new System.Drawing.Size(854, 381);
            this.panelExMain.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelExMain.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelExMain.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelExMain.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelExMain.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelExMain.Style.GradientAngle = 90;
            this.panelExMain.TabIndex = 1;
            // 
            // comboBoxEditSlideHeader
            // 
            this.comboBoxEditSlideHeader.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxEditSlideHeader.Location = new System.Drawing.Point(536, 33);
            this.comboBoxEditSlideHeader.Name = "comboBoxEditSlideHeader";
            this.comboBoxEditSlideHeader.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxEditSlideHeader.Properties.Appearance.Options.UseFont = true;
            this.comboBoxEditSlideHeader.Properties.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
            this.comboBoxEditSlideHeader.Properties.AppearanceDisabled.Options.UseFont = true;
            this.comboBoxEditSlideHeader.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Arial", 9.75F);
            this.comboBoxEditSlideHeader.Properties.AppearanceDropDown.Options.UseFont = true;
            this.comboBoxEditSlideHeader.Properties.AppearanceFocused.Font = new System.Drawing.Font("Arial", 9.75F);
            this.comboBoxEditSlideHeader.Properties.AppearanceFocused.Options.UseFont = true;
            this.comboBoxEditSlideHeader.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("Arial", 9.75F);
            this.comboBoxEditSlideHeader.Properties.AppearanceReadOnly.Options.UseFont = true;
            this.comboBoxEditSlideHeader.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEditSlideHeader.Size = new System.Drawing.Size(311, 22);
            this.comboBoxEditSlideHeader.TabIndex = 0;
            this.comboBoxEditSlideHeader.EditValueChanged += new System.EventHandler(this.memoEditC_EditValueChanged);
            // 
            // laDetail
            // 
            this.laDetail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.laDetail.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laDetail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(147)))), ((int)(((byte)(207)))));
            this.laDetail.Location = new System.Drawing.Point(583, 94);
            this.laDetail.Name = "laDetail";
            this.laDetail.Size = new System.Drawing.Size(264, 271);
            this.laDetail.TabIndex = 21;
            this.laDetail.Text = "This simple Intro slide sets the meeting agenda, and the expectations for the mee" +
                "ting…";
            this.laDetail.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // ckC
            // 
            this.ckC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ckC.AutoSize = true;
            this.ckC.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ckC.Location = new System.Drawing.Point(48, 314);
            this.ckC.Name = "ckC";
            this.ckC.Size = new System.Drawing.Size(44, 28);
            this.ckC.TabIndex = 17;
            this.ckC.TabStop = false;
            this.ckC.Text = "C";
            this.ckC.UseVisualStyleBackColor = true;
            this.ckC.CheckedChanged += new System.EventHandler(this.checkBoxes_CheckedChanged);
            // 
            // ckB
            // 
            this.ckB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ckB.AutoSize = true;
            this.ckB.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ckB.Location = new System.Drawing.Point(48, 218);
            this.ckB.Name = "ckB";
            this.ckB.Size = new System.Drawing.Size(44, 28);
            this.ckB.TabIndex = 15;
            this.ckB.TabStop = false;
            this.ckB.Text = "B";
            this.ckB.UseVisualStyleBackColor = true;
            this.ckB.CheckedChanged += new System.EventHandler(this.checkBoxes_CheckedChanged);
            // 
            // ckA
            // 
            this.ckA.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ckA.AutoSize = true;
            this.ckA.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ckA.Location = new System.Drawing.Point(48, 120);
            this.ckA.Name = "ckA";
            this.ckA.Size = new System.Drawing.Size(44, 28);
            this.ckA.TabIndex = 13;
            this.ckA.TabStop = false;
            this.ckA.Text = "A";
            this.ckA.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ckA.UseVisualStyleBackColor = true;
            this.ckA.CheckedChanged += new System.EventHandler(this.checkBoxes_CheckedChanged);
            // 
            // laSlideHeader
            // 
            this.laSlideHeader.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.laSlideHeader.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.laSlideHeader.Location = new System.Drawing.Point(537, 10);
            this.laSlideHeader.Name = "laSlideHeader";
            this.laSlideHeader.Size = new System.Drawing.Size(310, 22);
            this.laSlideHeader.TabIndex = 11;
            this.laSlideHeader.Text = "Select or type your Slide Header";
            this.laSlideHeader.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // laTitle
            // 
            this.laTitle.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laTitle.ForeColor = System.Drawing.Color.Black;
            this.laTitle.Location = new System.Drawing.Point(3, 2);
            this.laTitle.Name = "laTitle";
            this.laTitle.Size = new System.Drawing.Size(527, 66);
            this.laTitle.TabIndex = 8;
            this.laTitle.Text = "Set the Tone with an\r\nIntroduction Slide…";
            // 
            // memoEditC
            // 
            this.memoEditC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.memoEditC.Enabled = false;
            this.memoEditC.Location = new System.Drawing.Point(99, 285);
            this.memoEditC.Name = "memoEditC";
            this.memoEditC.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.memoEditC.Properties.Appearance.Options.UseFont = true;
            this.memoEditC.Size = new System.Drawing.Size(443, 80);
            this.memoEditC.TabIndex = 3;
            this.memoEditC.EditValueChanged += new System.EventHandler(this.memoEditC_EditValueChanged);
            // 
            // memoEditB
            // 
            this.memoEditB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.memoEditB.Enabled = false;
            this.memoEditB.Location = new System.Drawing.Point(99, 189);
            this.memoEditB.Name = "memoEditB";
            this.memoEditB.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.memoEditB.Properties.Appearance.Options.UseFont = true;
            this.memoEditB.Size = new System.Drawing.Size(443, 81);
            this.memoEditB.TabIndex = 2;
            this.memoEditB.EditValueChanged += new System.EventHandler(this.memoEditC_EditValueChanged);
            // 
            // memoEditA
            // 
            this.memoEditA.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.memoEditA.Enabled = false;
            this.memoEditA.Location = new System.Drawing.Point(99, 95);
            this.memoEditA.Name = "memoEditA";
            this.memoEditA.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.memoEditA.Properties.Appearance.Options.UseFont = true;
            this.memoEditA.Size = new System.Drawing.Size(443, 80);
            this.memoEditA.TabIndex = 1;
            this.memoEditA.EditValueChanged += new System.EventHandler(this.memoEditC_EditValueChanged);
            // 
            // LeadoffStatementControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.panelExMain);
            this.Name = "LeadoffStatementControl";
            this.Size = new System.Drawing.Size(854, 381);
            this.Load += new System.EventHandler(this.LeadoffStatementControl_Load);
            this.panelExMain.ResumeLayout(false);
            this.panelExMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditSlideHeader.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEditC.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEditB.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEditA.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelExMain;
        private System.Windows.Forms.Label laTitle;
        private System.Windows.Forms.Label laSlideHeader;
        private System.Windows.Forms.CheckBox ckA;
        private System.Windows.Forms.CheckBox ckC;
        private System.Windows.Forms.CheckBox ckB;
        private System.Windows.Forms.Label laDetail;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEditSlideHeader;
        private DevExpress.XtraEditors.MemoEdit memoEditC;
        private DevExpress.XtraEditors.MemoEdit memoEditB;
        private DevExpress.XtraEditors.MemoEdit memoEditA;
    }
}
