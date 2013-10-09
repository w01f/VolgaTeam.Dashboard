﻿namespace NewBizWiz.Dashboard.TabSlides
{
    partial class TabSlidesMainPage
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
			this.pnBottom = new System.Windows.Forms.Panel();
			this.pbVersion = new System.Windows.Forms.PictureBox();
			this.laUserName = new System.Windows.Forms.Label();
			this.pnMain = new System.Windows.Forms.Panel();
			this.laSlideSize = new System.Windows.Forms.Label();
			this.pnBottom.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbVersion)).BeginInit();
			this.pnMain.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnBottom
			// 
			this.pnBottom.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pnBottom.Controls.Add(this.pbVersion);
			this.pnBottom.Controls.Add(this.laUserName);
			this.pnBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnBottom.Location = new System.Drawing.Point(0, 504);
			this.pnBottom.Name = "pnBottom";
			this.pnBottom.Size = new System.Drawing.Size(894, 56);
			this.pnBottom.TabIndex = 2;
			// 
			// pbVersion
			// 
			this.pbVersion.Location = new System.Drawing.Point(3, 4);
			this.pbVersion.Name = "pbVersion";
			this.pbVersion.Size = new System.Drawing.Size(526, 45);
			this.pbVersion.TabIndex = 1;
			this.pbVersion.TabStop = false;
			// 
			// laUserName
			// 
			this.laUserName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.laUserName.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laUserName.ForeColor = System.Drawing.Color.White;
			this.laUserName.Location = new System.Drawing.Point(531, 6);
			this.laUserName.Name = "laUserName";
			this.laUserName.Size = new System.Drawing.Size(342, 41);
			this.laUserName.TabIndex = 0;
			this.laUserName.Text = "laSlideSize";
			this.laUserName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// pnMain
			// 
			this.pnMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pnMain.Controls.Add(this.laSlideSize);
			this.pnMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnMain.Location = new System.Drawing.Point(0, 0);
			this.pnMain.Name = "pnMain";
			this.pnMain.Size = new System.Drawing.Size(894, 504);
			this.pnMain.TabIndex = 3;
			// 
			// laSlideSize
			// 
			this.laSlideSize.Dock = System.Windows.Forms.DockStyle.Top;
			this.laSlideSize.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laSlideSize.ForeColor = System.Drawing.Color.White;
			this.laSlideSize.Location = new System.Drawing.Point(0, 0);
			this.laSlideSize.Name = "laSlideSize";
			this.laSlideSize.Size = new System.Drawing.Size(890, 41);
			this.laSlideSize.TabIndex = 1;
			this.laSlideSize.Text = "Slide Size: {0}";
			this.laSlideSize.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// TabSlidesMainPage
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
			this.Controls.Add(this.pnMain);
			this.Controls.Add(this.pnBottom);
			this.Name = "TabSlidesMainPage";
			this.Size = new System.Drawing.Size(894, 560);
			this.pnBottom.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pbVersion)).EndInit();
			this.pnMain.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

		private System.Windows.Forms.Panel pnBottom;
		private System.Windows.Forms.PictureBox pbVersion;
		private System.Windows.Forms.Label laUserName;
		private System.Windows.Forms.Panel pnMain;
		private System.Windows.Forms.Label laSlideSize;


	}
}