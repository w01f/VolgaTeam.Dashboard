namespace Asa.Bar.App.Forms
{
    partial class FormFloater
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormFloater));
			this.pnMain = new System.Windows.Forms.Panel();
			this.pictureBoxDock = new System.Windows.Forms.PictureBox();
			this.pictureBoxExpand = new System.Windows.Forms.PictureBox();
			this.pictureBoxExit = new System.Windows.Forms.PictureBox();
			this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
			this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripMenuItemCenterScreen = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItemDock = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItemExit = new System.Windows.Forms.ToolStripMenuItem();
			this.pnMain.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxDock)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxExpand)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxExit)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).BeginInit();
			this.contextMenuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnMain
			// 
			this.pnMain.BackColor = System.Drawing.Color.White;
			this.pnMain.Controls.Add(this.pictureBoxDock);
			this.pnMain.Controls.Add(this.pictureBoxExpand);
			this.pnMain.Controls.Add(this.pictureBoxExit);
			this.pnMain.Controls.Add(this.pictureBoxLogo);
			this.pnMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnMain.Location = new System.Drawing.Point(1, 1);
			this.pnMain.Name = "pnMain";
			this.pnMain.Size = new System.Drawing.Size(281, 118);
			this.pnMain.TabIndex = 2;
			// 
			// pictureBoxDock
			// 
			this.pictureBoxDock.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.pictureBoxDock.Location = new System.Drawing.Point(246, 83);
			this.pictureBoxDock.Name = "pictureBoxDock";
			this.pictureBoxDock.Size = new System.Drawing.Size(32, 32);
			this.pictureBoxDock.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.pictureBoxDock.TabIndex = 3;
			this.pictureBoxDock.TabStop = false;
			this.pictureBoxDock.Click += new System.EventHandler(this.OnDockButtonClick);
			// 
			// pictureBoxExpand
			// 
			this.pictureBoxExpand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.pictureBoxExpand.Location = new System.Drawing.Point(246, 43);
			this.pictureBoxExpand.Name = "pictureBoxExpand";
			this.pictureBoxExpand.Size = new System.Drawing.Size(32, 32);
			this.pictureBoxExpand.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.pictureBoxExpand.TabIndex = 2;
			this.pictureBoxExpand.TabStop = false;
			this.pictureBoxExpand.Click += new System.EventHandler(this.OnExpandButtonClick);
			this.pictureBoxExpand.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnButtonMouseDown);
			this.pictureBoxExpand.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnButtonMouseMove);
			this.pictureBoxExpand.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnButtonMouseUp);
			// 
			// pictureBoxExit
			// 
			this.pictureBoxExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.pictureBoxExit.Location = new System.Drawing.Point(246, 3);
			this.pictureBoxExit.Name = "pictureBoxExit";
			this.pictureBoxExit.Size = new System.Drawing.Size(32, 32);
			this.pictureBoxExit.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.pictureBoxExit.TabIndex = 1;
			this.pictureBoxExit.TabStop = false;
			this.pictureBoxExit.Click += new System.EventHandler(this.OnExitButtonClick);
			this.pictureBoxExit.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnButtonMouseDown);
			this.pictureBoxExit.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnButtonMouseMove);
			this.pictureBoxExit.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnButtonMouseUp);
			// 
			// pictureBoxLogo
			// 
			this.pictureBoxLogo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pictureBoxLogo.Location = new System.Drawing.Point(3, 3);
			this.pictureBoxLogo.Name = "pictureBoxLogo";
			this.pictureBoxLogo.Size = new System.Drawing.Size(240, 112);
			this.pictureBoxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.pictureBoxLogo.TabIndex = 0;
			this.pictureBoxLogo.TabStop = false;
			this.pictureBoxLogo.Click += new System.EventHandler(this.OnExpandButtonClick);
			this.pictureBoxLogo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnButtonMouseDown);
			this.pictureBoxLogo.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnButtonMouseMove);
			this.pictureBoxLogo.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnButtonMouseUp);
			// 
			// notifyIcon
			// 
			this.notifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
			this.notifyIcon.ContextMenuStrip = this.contextMenuStrip;
			this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
			this.notifyIcon.Text = "Minibar";
			this.notifyIcon.Visible = true;
			// 
			// contextMenuStrip
			// 
			this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemCenterScreen,
            this.toolStripSeparator1,
            this.toolStripMenuItemDock,
            this.toolStripSeparator2,
            this.toolStripMenuItemExit});
			this.contextMenuStrip.Name = "contextMenuStrip";
			this.contextMenuStrip.Size = new System.Drawing.Size(148, 82);
			// 
			// toolStripMenuItemCenterScreen
			// 
			this.toolStripMenuItemCenterScreen.Name = "toolStripMenuItemCenterScreen";
			this.toolStripMenuItemCenterScreen.Size = new System.Drawing.Size(147, 22);
			this.toolStripMenuItemCenterScreen.Text = "Center Screen";
			this.toolStripMenuItemCenterScreen.Click += new System.EventHandler(this.OnToolStripMenuItemCenterScreenClick);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(144, 6);
			// 
			// toolStripMenuItemDock
			// 
			this.toolStripMenuItemDock.Name = "toolStripMenuItemDock";
			this.toolStripMenuItemDock.Size = new System.Drawing.Size(147, 22);
			this.toolStripMenuItemDock.Text = "Taskbar Dock";
			this.toolStripMenuItemDock.Click += new System.EventHandler(this.OnToolStripMenuItemDockClick);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(144, 6);
			// 
			// toolStripMenuItemExit
			// 
			this.toolStripMenuItemExit.Name = "toolStripMenuItemExit";
			this.toolStripMenuItemExit.Size = new System.Drawing.Size(147, 22);
			this.toolStripMenuItemExit.Text = "Exit Minibar";
			this.toolStripMenuItemExit.Click += new System.EventHandler(this.OnExitButtonClick);
			// 
			// FormFloater
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(87)))), ((int)(((byte)(154)))));
			this.ClientSize = new System.Drawing.Size(283, 120);
			this.ControlBox = false;
			this.Controls.Add(this.pnMain);
			this.DoubleBuffered = true;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormFloater";
			this.Opacity = 0.85D;
			this.Padding = new System.Windows.Forms.Padding(1);
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "adSALESapps.com";
			this.TopMost = true;
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OnFormClosed);
			this.Shown += new System.EventHandler(this.OnFormShown);
			this.pnMain.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxDock)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxExpand)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxExit)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
			this.contextMenuStrip.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion
		private System.Windows.Forms.Panel pnMain;
		private System.Windows.Forms.PictureBox pictureBoxExpand;
		private System.Windows.Forms.PictureBox pictureBoxExit;
		private System.Windows.Forms.PictureBox pictureBoxLogo;
		private System.Windows.Forms.NotifyIcon notifyIcon;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemCenterScreen;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDock;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemExit;
		private System.Windows.Forms.PictureBox pictureBoxDock;
	}
}