namespace Asa.Solutions.Dashboard.PresentationClasses.ContentEditors
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
			this.pbHeader = new System.Windows.Forms.PictureBox();
			this.pbCleanslateSplash = new System.Windows.Forms.PictureBox();
			this.pnMain.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditSlideHeader.Properties)).BeginInit();
			this.pnTop.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbSplash)).BeginInit();
			this.pnSplash.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbHeader)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbCleanslateSplash)).BeginInit();
			this.SuspendLayout();
			// 
			// pnMain
			// 
			this.pnMain.Controls.Add(this.pbCleanslateSplash);
			this.pnMain.Controls.Add(this.pbHeader);
			this.pnMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnMain.MaximumSize = new System.Drawing.Size(0, 0);
			this.pnMain.MinimumSize = new System.Drawing.Size(0, 0);
			this.pnMain.Size = new System.Drawing.Size(853, 461);
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
			// pbSplash
			// 
			this.pbSplash.Location = new System.Drawing.Point(-267, 151);
			// 
			// pnSplash
			// 
			this.pnSplash.Dock = System.Windows.Forms.DockStyle.Right;
			this.pnSplash.Location = new System.Drawing.Point(853, 51);
			this.pnSplash.Size = new System.Drawing.Size(144, 461);
			this.pnSplash.Visible = false;
			// 
			// toolTip
			// 
			this.toolTip.IsBalloon = true;
			// 
			// pbHeader
			// 
			this.pbHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pbHeader.Location = new System.Drawing.Point(11, 30);
			this.pbHeader.Name = "pbHeader";
			this.pbHeader.Size = new System.Drawing.Size(836, 91);
			this.pbHeader.TabIndex = 0;
			this.pbHeader.TabStop = false;
			// 
			// pbCleanslateSplash
			// 
			this.pbCleanslateSplash.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.pbCleanslateSplash.Location = new System.Drawing.Point(442, 151);
			this.pbCleanslateSplash.Name = "pbCleanslateSplash";
			this.pbCleanslateSplash.Size = new System.Drawing.Size(411, 310);
			this.pbCleanslateSplash.TabIndex = 1;
			this.pbCleanslateSplash.TabStop = false;
			// 
			// CleanslateControl
			// 
			this.BackColor = System.Drawing.Color.White;
			this.Name = "CleanslateControl";
			this.pnMain.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditSlideHeader.Properties)).EndInit();
			this.pnTop.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pbSplash)).EndInit();
			this.pnSplash.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pbHeader)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbCleanslateSplash)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

		private System.Windows.Forms.ToolTip toolTip;
		private System.Windows.Forms.PictureBox pbHeader;
		private System.Windows.Forms.PictureBox pbCleanslateSplash;
	}
}
