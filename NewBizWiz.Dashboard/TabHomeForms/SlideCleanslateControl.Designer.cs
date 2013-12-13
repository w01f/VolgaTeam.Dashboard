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
			this.laUserName = new System.Windows.Forms.Label();
			this.pbVersion = new System.Windows.Forms.PictureBox();
			this.pnMain.SuspendLayout();
			this.pnBottom.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditSlideHeader.Properties)).BeginInit();
			this.pnTop.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbVersion)).BeginInit();
			this.SuspendLayout();
			// 
			// pnMain
			// 
			this.pnMain.Size = new System.Drawing.Size(824, 437);
			// 
			// pnBottom
			// 
			this.pnBottom.Controls.Add(this.pbVersion);
			this.pnBottom.Controls.Add(this.laUserName);
			this.pnBottom.Location = new System.Drawing.Point(0, 437);
			this.pnBottom.Size = new System.Drawing.Size(919, 75);
			this.pnBottom.Controls.SetChildIndex(this.laUserName, 0);
			this.pnBottom.Controls.SetChildIndex(this.pbVersion, 0);
			this.pnBottom.Controls.SetChildIndex(this.buttonXSavedFiles, 0);
			// 
			// buttonXSavedFiles
			// 
			this.buttonXSavedFiles.Visible = false;
			// 
			// laSlideHeader
			// 
			this.laSlideHeader.Visible = false;
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
			// toolTip
			// 
			this.toolTip.IsBalloon = true;
			// 
			// laUserName
			// 
			this.laUserName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.laUserName.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laUserName.ForeColor = System.Drawing.Color.White;
			this.laUserName.Location = new System.Drawing.Point(556, 9);
			this.laUserName.Name = "laUserName";
			this.laUserName.Size = new System.Drawing.Size(342, 52);
			this.laUserName.TabIndex = 0;
			this.laUserName.Text = "label1";
			this.laUserName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// pbVersion
			// 
			this.pbVersion.Location = new System.Drawing.Point(3, 13);
			this.pbVersion.Name = "pbVersion";
			this.pbVersion.Size = new System.Drawing.Size(526, 45);
			this.pbVersion.TabIndex = 1;
			this.pbVersion.TabStop = false;
			// 
			// SlideCleanslateControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
			this.Name = "SlideCleanslateControl";
			this.pnMain.ResumeLayout(false);
			this.pnBottom.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditSlideHeader.Properties)).EndInit();
			this.pnTop.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pbVersion)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip;
		private System.Windows.Forms.Label laUserName;
        private System.Windows.Forms.PictureBox pbVersion;
    }
}
