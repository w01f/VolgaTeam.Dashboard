namespace Asa.Solutions.Dashboard.PresentationClasses
{
	sealed partial class LeadoffStatementControl
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
			DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipTitleItem toolTipTitleItem1 = new DevExpress.Utils.ToolTipTitleItem();
			DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
			this.ckC = new System.Windows.Forms.CheckBox();
			this.ckB = new System.Windows.Forms.CheckBox();
			this.ckA = new System.Windows.Forms.CheckBox();
			this.memoEditC = new DevExpress.XtraEditors.MemoEdit();
			this.memoEditB = new DevExpress.XtraEditors.MemoEdit();
			this.memoEditA = new DevExpress.XtraEditors.MemoEdit();
			this.pnMain.SuspendLayout();
			this.pnBottom.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditSlideHeader.Properties)).BeginInit();
			this.pnTop.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.checkEditSolutionNew.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbDescription)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.memoEditC.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.memoEditB.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.memoEditA.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// pnMain
			// 
			this.pnMain.Controls.Add(this.ckC);
			this.pnMain.Controls.Add(this.ckA);
			this.pnMain.Controls.Add(this.ckB);
			this.pnMain.Controls.Add(this.memoEditA);
			this.pnMain.Controls.Add(this.memoEditB);
			this.pnMain.Controls.Add(this.memoEditC);
			// 
			// comboBoxEditSlideHeader
			// 
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
			this.comboBoxEditSlideHeader.EditValueChanged += new System.EventHandler(this.EditValueChanged);
			// 
			// checkEditSolutionNew
			// 
			this.checkEditSolutionNew.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.checkEditSolutionNew.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
			this.checkEditSolutionNew.Properties.Appearance.Options.UseFont = true;
			this.checkEditSolutionNew.Properties.Appearance.Options.UseForeColor = true;
			this.checkEditSolutionNew.Size = new System.Drawing.Size(157, 20);
			// 
			// pbDescription
			// 
			this.pbDescription.Image = global::Asa.Solutions.Dashboard.Properties.Resources.DescriptionLeadoff;
			// 
			// ckC
			// 
			this.ckC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.ckC.AutoSize = true;
			this.ckC.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.ckC.Location = new System.Drawing.Point(11, 298);
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
			this.ckB.Location = new System.Drawing.Point(11, 182);
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
			this.ckA.Location = new System.Drawing.Point(11, 63);
			this.ckA.Name = "ckA";
			this.ckA.Size = new System.Drawing.Size(44, 28);
			this.ckA.TabIndex = 13;
			this.ckA.TabStop = false;
			this.ckA.Text = "A";
			this.ckA.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.ckA.UseVisualStyleBackColor = true;
			this.ckA.CheckedChanged += new System.EventHandler(this.checkBoxes_CheckedChanged);
			// 
			// memoEditC
			// 
			this.memoEditC.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.memoEditC.Enabled = false;
			this.memoEditC.Location = new System.Drawing.Point(62, 269);
			this.memoEditC.Name = "memoEditC";
			this.memoEditC.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.memoEditC.Properties.Appearance.Options.UseFont = true;
			this.memoEditC.Size = new System.Drawing.Size(700, 80);
			this.memoEditC.TabIndex = 3;
			this.memoEditC.UseOptimizedRendering = true;
			this.memoEditC.EditValueChanged += new System.EventHandler(this.EditValueChanged);
			// 
			// memoEditB
			// 
			this.memoEditB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.memoEditB.Enabled = false;
			this.memoEditB.Location = new System.Drawing.Point(62, 153);
			this.memoEditB.Name = "memoEditB";
			this.memoEditB.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.memoEditB.Properties.Appearance.Options.UseFont = true;
			this.memoEditB.Size = new System.Drawing.Size(700, 81);
			this.memoEditB.TabIndex = 2;
			this.memoEditB.UseOptimizedRendering = true;
			this.memoEditB.EditValueChanged += new System.EventHandler(this.EditValueChanged);
			// 
			// memoEditA
			// 
			this.memoEditA.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.memoEditA.Enabled = false;
			this.memoEditA.Location = new System.Drawing.Point(62, 38);
			this.memoEditA.Name = "memoEditA";
			this.memoEditA.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.memoEditA.Properties.Appearance.Options.UseFont = true;
			this.memoEditA.Size = new System.Drawing.Size(700, 80);
			this.memoEditA.TabIndex = 1;
			this.memoEditA.UseOptimizedRendering = true;
			this.memoEditA.EditValueChanged += new System.EventHandler(this.EditValueChanged);
			// 
			// SlideLeadoffStatementControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.Name = "SlideLeadoffStatementControl";
			this.pnMain.ResumeLayout(false);
			this.pnMain.PerformLayout();
			this.pnBottom.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditSlideHeader.Properties)).EndInit();
			this.pnTop.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.checkEditSolutionNew.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbDescription)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.memoEditC.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.memoEditB.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.memoEditA.Properties)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

		private System.Windows.Forms.CheckBox ckA;
        private System.Windows.Forms.CheckBox ckC;
		private System.Windows.Forms.CheckBox ckB;
        private DevExpress.XtraEditors.MemoEdit memoEditC;
        private DevExpress.XtraEditors.MemoEdit memoEditB;
        private DevExpress.XtraEditors.MemoEdit memoEditA;
    }
}
