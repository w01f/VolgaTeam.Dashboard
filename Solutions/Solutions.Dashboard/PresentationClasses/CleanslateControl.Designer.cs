namespace Asa.Solutions.Dashboard.PresentationClasses
{
    partial class CleanslateControl
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
			this.pbSellerPoint = new System.Windows.Forms.PictureBox();
			this.pnMain.SuspendLayout();
			this.pnBottom.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditSlideHeader.Properties)).BeginInit();
			this.pnTop.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.checkEditSolutionNew.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbDescription)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbSellerPoint)).BeginInit();
			this.SuspendLayout();
			// 
			// pnMain
			// 
			this.pnMain.Controls.Add(this.pbSellerPoint);
			this.pnMain.Padding = new System.Windows.Forms.Padding(10);
			this.pnMain.Size = new System.Drawing.Size(789, 386);
			// 
			// pnBottom
			// 
			this.pnBottom.Location = new System.Drawing.Point(0, 437);
			this.pnBottom.Size = new System.Drawing.Size(789, 75);
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
			// toolTip
			// 
			this.toolTip.IsBalloon = true;
			// 
			// pbSellerPoint
			// 
			this.pbSellerPoint.Cursor = System.Windows.Forms.Cursors.Default;
			this.pbSellerPoint.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pbSellerPoint.Image = global::Asa.Solutions.Dashboard.Properties.Resources.HomeDefault;
			this.pbSellerPoint.Location = new System.Drawing.Point(10, 10);
			this.pbSellerPoint.Name = "pbSellerPoint";
			this.pbSellerPoint.Size = new System.Drawing.Size(769, 366);
			this.pbSellerPoint.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pbSellerPoint.TabIndex = 1;
			this.pbSellerPoint.TabStop = false;
			// 
			// CleanslateControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.Name = "CleanslateControl";
			this.pnMain.ResumeLayout(false);
			this.pnMain.PerformLayout();
			this.pnBottom.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditSlideHeader.Properties)).EndInit();
			this.pnTop.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.checkEditSolutionNew.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbDescription)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbSellerPoint)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

		private System.Windows.Forms.ToolTip toolTip;
		private System.Windows.Forms.PictureBox pbSellerPoint;
	}
}
