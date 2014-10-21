namespace NewBizWiz.Dashboard.TabHomeForms
{
    partial class SlideCleanslateControl
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
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.pbVersion = new System.Windows.Forms.PictureBox();
			this.pnBottom.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditSlideHeader.Properties)).BeginInit();
			this.pnTop.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.checkEditSolutionNew.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbDescription)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbVersion)).BeginInit();
			this.SuspendLayout();
			// 
			// pnMain
			// 
			this.pnMain.Size = new System.Drawing.Size(789, 386);
			// 
			// pnBottom
			// 
			this.pnBottom.Controls.Add(this.pbVersion);
			this.pnBottom.Location = new System.Drawing.Point(0, 437);
			this.pnBottom.Size = new System.Drawing.Size(789, 75);
			this.pnBottom.Controls.SetChildIndex(this.simpleButtonSaveTemplate, 0);
			this.pnBottom.Controls.SetChildIndex(this.pbDescription, 0);
			this.pnBottom.Controls.SetChildIndex(this.pbVersion, 0);
			this.pnBottom.Controls.SetChildIndex(this.checkEditSolutionNew, 0);
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
			this.comboBoxEditSlideHeader.Visible = false;
			// 
			// pnTop
			// 
			this.pnTop.Visible = false;
			// 
			// checkEditSolutionNew
			// 
			this.checkEditSolutionNew.Location = new System.Drawing.Point(575, 27);
			this.checkEditSolutionNew.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.checkEditSolutionNew.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
			this.checkEditSolutionNew.Properties.Appearance.Options.UseFont = true;
			this.checkEditSolutionNew.Properties.Appearance.Options.UseForeColor = true;
			this.checkEditSolutionNew.Visible = false;
			// 
			// pbDescription
			// 
			this.pbDescription.Visible = false;
			// 
			// simpleButtonSaveTemplate
			// 
			this.simpleButtonSaveTemplate.Visible = false;
			// 
			// toolTip
			// 
			this.toolTip.IsBalloon = true;
			// 
			// pbVersion
			// 
			this.pbVersion.Location = new System.Drawing.Point(3, 15);
			this.pbVersion.Name = "pbVersion";
			this.pbVersion.Size = new System.Drawing.Size(526, 45);
			this.pbVersion.TabIndex = 1;
			this.pbVersion.TabStop = false;
			// 
			// SlideCleanslateControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.Name = "SlideCleanslateControl";
			this.pnBottom.ResumeLayout(false);
			this.pnBottom.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditSlideHeader.Properties)).EndInit();
			this.pnTop.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.checkEditSolutionNew.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbDescription)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbVersion)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

		private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.PictureBox pbVersion;
    }
}
