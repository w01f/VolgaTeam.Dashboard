namespace Asa.Bar.App.Forms
{
	partial class FormStart
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormStart));
			this.laTitle = new System.Windows.Forms.Label();
			this.laDetails = new System.Windows.Forms.Label();
			this.circularProgress = new DevComponents.DotNetBar.Controls.CircularProgress();
			this.pbCancel = new System.Windows.Forms.PictureBox();
			this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripMenuItemShowProgress = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItemKillApp = new System.Windows.Forms.ToolStripMenuItem();
			this.panelMain = new System.Windows.Forms.Panel();
			this.panelCancel = new System.Windows.Forms.Panel();
			this.panelCancelInner = new System.Windows.Forms.Panel();
			((System.ComponentModel.ISupportInitialize)(this.pbCancel)).BeginInit();
			this.contextMenuStrip.SuspendLayout();
			this.panelMain.SuspendLayout();
			this.panelCancel.SuspendLayout();
			this.panelCancelInner.SuspendLayout();
			this.SuspendLayout();
			// 
			// laTitle
			// 
			this.laTitle.BackColor = System.Drawing.Color.Transparent;
			this.laTitle.Cursor = System.Windows.Forms.Cursors.Arrow;
			this.laTitle.Dock = System.Windows.Forms.DockStyle.Fill;
			this.laTitle.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laTitle.ForeColor = System.Drawing.Color.White;
			this.laTitle.Location = new System.Drawing.Point(31, 0);
			this.laTitle.Name = "laTitle";
			this.laTitle.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
			this.laTitle.Size = new System.Drawing.Size(243, 32);
			this.laTitle.TabIndex = 2;
			this.laTitle.Text = "Test...";
			this.laTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.laTitle.UseMnemonic = false;
			// 
			// laDetails
			// 
			this.laDetails.BackColor = System.Drawing.Color.Transparent;
			this.laDetails.Cursor = System.Windows.Forms.Cursors.Arrow;
			this.laDetails.Dock = System.Windows.Forms.DockStyle.Fill;
			this.laDetails.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laDetails.ForeColor = System.Drawing.Color.White;
			this.laDetails.Location = new System.Drawing.Point(31, 0);
			this.laDetails.Name = "laDetails";
			this.laDetails.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
			this.laDetails.Size = new System.Drawing.Size(243, 32);
			this.laDetails.TabIndex = 13;
			this.laDetails.Text = "Test...";
			this.laDetails.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.laDetails.UseMnemonic = false;
			// 
			// circularProgress
			// 
			this.circularProgress.AnimationSpeed = 50;
			this.circularProgress.BackColor = System.Drawing.Color.Transparent;
			// 
			// 
			// 
			this.circularProgress.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.circularProgress.Dock = System.Windows.Forms.DockStyle.Left;
			this.circularProgress.Enabled = false;
			this.circularProgress.Location = new System.Drawing.Point(10, 0);
			this.circularProgress.Name = "circularProgress";
			this.circularProgress.ProgressBarType = DevComponents.DotNetBar.eCircularProgressType.Dot;
			this.circularProgress.ProgressColor = System.Drawing.Color.White;
			this.circularProgress.ProgressTextFormat = "";
			this.circularProgress.Size = new System.Drawing.Size(21, 32);
			this.circularProgress.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP;
			this.circularProgress.TabIndex = 17;
			// 
			// pbCancel
			// 
			this.pbCancel.BackColor = System.Drawing.Color.Transparent;
			this.pbCancel.Cursor = System.Windows.Forms.Cursors.Hand;
			this.pbCancel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pbCancel.Image = global::Asa.Bar.App.Properties.Resources.ProgressCancel;
			this.pbCancel.Location = new System.Drawing.Point(4, 4);
			this.pbCancel.Name = "pbCancel";
			this.pbCancel.Size = new System.Drawing.Size(25, 24);
			this.pbCancel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pbCancel.TabIndex = 0;
			this.pbCancel.TabStop = false;
			this.pbCancel.Click += new System.EventHandler(this.pbCancel_Click);
			// 
			// notifyIcon
			// 
			this.notifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
			this.notifyIcon.ContextMenuStrip = this.contextMenuStrip;
			this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
			// 
			// contextMenuStrip
			// 
			this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemShowProgress,
            this.toolStripSeparator1,
            this.toolStripMenuItemKillApp});
			this.contextMenuStrip.Name = "contextMenuStrip";
			this.contextMenuStrip.Size = new System.Drawing.Size(170, 54);
			// 
			// toolStripMenuItemShowProgress
			// 
			this.toolStripMenuItemShowProgress.Name = "toolStripMenuItemShowProgress";
			this.toolStripMenuItemShowProgress.Size = new System.Drawing.Size(169, 22);
			this.toolStripMenuItemShowProgress.Text = "Show Sync Details";
			this.toolStripMenuItemShowProgress.Click += new System.EventHandler(this.toolStripMenuItemShowProgress_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(166, 6);
			// 
			// toolStripMenuItemKillApp
			// 
			this.toolStripMenuItemKillApp.Name = "toolStripMenuItemKillApp";
			this.toolStripMenuItemKillApp.Size = new System.Drawing.Size(169, 22);
			this.toolStripMenuItemKillApp.Text = "Kill {0}";
			this.toolStripMenuItemKillApp.Click += new System.EventHandler(this.toolStripMenuItemKillApp_Click);
			// 
			// panelMain
			// 
			this.panelMain.BackColor = System.Drawing.Color.ForestGreen;
			this.panelMain.Controls.Add(this.laTitle);
			this.panelMain.Controls.Add(this.laDetails);
			this.panelMain.Controls.Add(this.circularProgress);
			this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelMain.Location = new System.Drawing.Point(1, 1);
			this.panelMain.Name = "panelMain";
			this.panelMain.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
			this.panelMain.Size = new System.Drawing.Size(274, 32);
			this.panelMain.TabIndex = 8;
			// 
			// panelCancel
			// 
			this.panelCancel.Controls.Add(this.panelCancelInner);
			this.panelCancel.Dock = System.Windows.Forms.DockStyle.Right;
			this.panelCancel.Location = new System.Drawing.Point(275, 1);
			this.panelCancel.Name = "panelCancel";
			this.panelCancel.Padding = new System.Windows.Forms.Padding(1, 0, 0, 0);
			this.panelCancel.Size = new System.Drawing.Size(34, 32);
			this.panelCancel.TabIndex = 9;
			// 
			// panelCancelInner
			// 
			this.panelCancelInner.BackColor = System.Drawing.Color.ForestGreen;
			this.panelCancelInner.Controls.Add(this.pbCancel);
			this.panelCancelInner.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelCancelInner.Location = new System.Drawing.Point(1, 0);
			this.panelCancelInner.Name = "panelCancelInner";
			this.panelCancelInner.Padding = new System.Windows.Forms.Padding(4);
			this.panelCancelInner.Size = new System.Drawing.Size(33, 32);
			this.panelCancelInner.TabIndex = 0;
			// 
			// FormStart
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(310, 34);
			this.ControlBox = false;
			this.Controls.Add(this.panelMain);
			this.Controls.Add(this.panelCancel);
			this.Cursor = System.Windows.Forms.Cursors.Arrow;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "FormStart";
			this.Padding = new System.Windows.Forms.Padding(1);
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "ProgressForm";
			this.TopMost = true;
			this.Shown += new System.EventHandler(this.FormProgress_Shown);
			((System.ComponentModel.ISupportInitialize)(this.pbCancel)).EndInit();
			this.contextMenuStrip.ResumeLayout(false);
			this.panelMain.ResumeLayout(false);
			this.panelCancel.ResumeLayout(false);
			this.panelCancelInner.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

		public System.Windows.Forms.Label laTitle;
        private System.Windows.Forms.PictureBox pbCancel;
		public System.Windows.Forms.Label laDetails;
		private System.Windows.Forms.NotifyIcon notifyIcon;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemShowProgress;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemKillApp;
		private DevComponents.DotNetBar.Controls.CircularProgress circularProgress;
		private System.Windows.Forms.Panel panelMain;
		private System.Windows.Forms.Panel panelCancel;
		private System.Windows.Forms.Panel panelCancelInner;
	}
}