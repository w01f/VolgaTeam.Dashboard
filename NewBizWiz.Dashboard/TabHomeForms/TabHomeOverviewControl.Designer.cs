namespace NewBizWiz.Dashboard.TabHomeForms
{
    partial class TabHomeOverviewControl
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
			this.pnMain = new System.Windows.Forms.Panel();
			this.pbWatermark = new System.Windows.Forms.PictureBox();
			this.pnBottom = new System.Windows.Forms.Panel();
			this.pbVersion = new System.Windows.Forms.PictureBox();
			this.laUserName = new System.Windows.Forms.Label();
			this.pnMain.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbWatermark)).BeginInit();
			this.pnBottom.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbVersion)).BeginInit();
			this.SuspendLayout();
			// 
			// toolTip
			// 
			this.toolTip.IsBalloon = true;
			// 
			// pnMain
			// 
			this.pnMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pnMain.Controls.Add(this.pbWatermark);
			this.pnMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnMain.Location = new System.Drawing.Point(0, 0);
			this.pnMain.Name = "pnMain";
			this.pnMain.Size = new System.Drawing.Size(898, 519);
			this.pnMain.TabIndex = 0;
			// 
			// pbWatermark
			// 
			this.pbWatermark.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.pbWatermark.Location = new System.Drawing.Point(726, 438);
			this.pbWatermark.Name = "pbWatermark";
			this.pbWatermark.Size = new System.Drawing.Size(146, 71);
			this.pbWatermark.TabIndex = 14;
			this.pbWatermark.TabStop = false;
			// 
			// pnBottom
			// 
			this.pnBottom.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pnBottom.Controls.Add(this.pbVersion);
			this.pnBottom.Controls.Add(this.laUserName);
			this.pnBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnBottom.Location = new System.Drawing.Point(0, 519);
			this.pnBottom.Name = "pnBottom";
			this.pnBottom.Size = new System.Drawing.Size(898, 56);
			this.pnBottom.TabIndex = 1;
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
			this.laUserName.Location = new System.Drawing.Point(535, 6);
			this.laUserName.Name = "laUserName";
			this.laUserName.Size = new System.Drawing.Size(342, 41);
			this.laUserName.TabIndex = 0;
			this.laUserName.Text = "label1";
			this.laUserName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// TabHomeOverviewControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
			this.Controls.Add(this.pnMain);
			this.Controls.Add(this.pnBottom);
			this.Name = "TabHomeOverviewControl";
			this.Size = new System.Drawing.Size(898, 575);
			this.pnMain.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pbWatermark)).EndInit();
			this.pnBottom.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pbVersion)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Panel pnMain;
        private System.Windows.Forms.Panel pnBottom;
        private System.Windows.Forms.Label laUserName;
        private System.Windows.Forms.PictureBox pbWatermark;
        private System.Windows.Forms.PictureBox pbVersion;
    }
}
