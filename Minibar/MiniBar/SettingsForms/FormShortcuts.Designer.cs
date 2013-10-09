namespace NewBizWiz.MiniBar.SettingsForms
{
    partial class FormShortcuts
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
			this.xtraScrollableControl = new DevExpress.XtraEditors.XtraScrollableControl();
			this.pnWebSalesDepot = new System.Windows.Forms.Panel();
			this.pbWebSalesDepot = new System.Windows.Forms.PictureBox();
			this.pnLocalSalesDepot = new System.Windows.Forms.Panel();
			this.pbLocalSalesDepot = new System.Windows.Forms.PictureBox();
			this.pnTop = new System.Windows.Forms.Panel();
			this.laHeader = new System.Windows.Forms.Label();
			this.pnMain = new System.Windows.Forms.Panel();
			this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
			this.xtraScrollableControl.SuspendLayout();
			this.pnWebSalesDepot.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbWebSalesDepot)).BeginInit();
			this.pnLocalSalesDepot.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbLocalSalesDepot)).BeginInit();
			this.pnTop.SuspendLayout();
			this.pnMain.SuspendLayout();
			this.SuspendLayout();
			// 
			// xtraScrollableControl
			// 
			this.xtraScrollableControl.AlwaysScrollActiveControlIntoView = false;
			this.xtraScrollableControl.Controls.Add(this.pnWebSalesDepot);
			this.xtraScrollableControl.Controls.Add(this.pnLocalSalesDepot);
			this.xtraScrollableControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.xtraScrollableControl.Location = new System.Drawing.Point(0, 0);
			this.xtraScrollableControl.Name = "xtraScrollableControl";
			this.xtraScrollableControl.Size = new System.Drawing.Size(353, 526);
			this.xtraScrollableControl.TabIndex = 0;
			// 
			// pnWebSalesDepot
			// 
			this.pnWebSalesDepot.Controls.Add(this.pbWebSalesDepot);
			this.pnWebSalesDepot.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnWebSalesDepot.Location = new System.Drawing.Point(0, 91);
			this.pnWebSalesDepot.Name = "pnWebSalesDepot";
			this.pnWebSalesDepot.Size = new System.Drawing.Size(353, 91);
			this.pnWebSalesDepot.TabIndex = 12;
			// 
			// pbWebSalesDepot
			// 
			this.pbWebSalesDepot.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pbWebSalesDepot.Image = global::NewBizWiz.MiniBar.Properties.Resources.SalesDepot;
			this.pbWebSalesDepot.Location = new System.Drawing.Point(11, 10);
			this.pbWebSalesDepot.Name = "pbWebSalesDepot";
			this.pbWebSalesDepot.Size = new System.Drawing.Size(330, 72);
			this.pbWebSalesDepot.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.pbWebSalesDepot.TabIndex = 2;
			this.pbWebSalesDepot.TabStop = false;
			this.pbWebSalesDepot.Click += new System.EventHandler(this.pbWebSalesDepot_Click);
			this.pbWebSalesDepot.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
			this.pbWebSalesDepot.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseUp);
			// 
			// pnLocalSalesDepot
			// 
			this.pnLocalSalesDepot.Controls.Add(this.pbLocalSalesDepot);
			this.pnLocalSalesDepot.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnLocalSalesDepot.Location = new System.Drawing.Point(0, 0);
			this.pnLocalSalesDepot.Name = "pnLocalSalesDepot";
			this.pnLocalSalesDepot.Size = new System.Drawing.Size(353, 91);
			this.pnLocalSalesDepot.TabIndex = 11;
			// 
			// pbLocalSalesDepot
			// 
			this.pbLocalSalesDepot.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pbLocalSalesDepot.Image = global::NewBizWiz.MiniBar.Properties.Resources.SalesDepot;
			this.pbLocalSalesDepot.Location = new System.Drawing.Point(11, 9);
			this.pbLocalSalesDepot.Name = "pbLocalSalesDepot";
			this.pbLocalSalesDepot.Size = new System.Drawing.Size(330, 72);
			this.pbLocalSalesDepot.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.pbLocalSalesDepot.TabIndex = 4;
			this.pbLocalSalesDepot.TabStop = false;
			this.pbLocalSalesDepot.Click += new System.EventHandler(this.pbLocalSalesDepot_Click);
			this.pbLocalSalesDepot.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
			this.pbLocalSalesDepot.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseUp);
			// 
			// pnTop
			// 
			this.pnTop.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pnTop.Controls.Add(this.laHeader);
			this.pnTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnTop.Location = new System.Drawing.Point(0, 0);
			this.pnTop.Name = "pnTop";
			this.pnTop.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
			this.pnTop.Size = new System.Drawing.Size(357, 39);
			this.pnTop.TabIndex = 1;
			// 
			// laHeader
			// 
			this.laHeader.Dock = System.Windows.Forms.DockStyle.Fill;
			this.laHeader.Location = new System.Drawing.Point(10, 0);
			this.laHeader.Name = "laHeader";
			this.laHeader.Size = new System.Drawing.Size(343, 35);
			this.laHeader.TabIndex = 0;
			this.laHeader.Text = "Click to add Desktop Shortcuts:";
			this.laHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pnMain
			// 
			this.pnMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pnMain.Controls.Add(this.xtraScrollableControl);
			this.pnMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnMain.Location = new System.Drawing.Point(0, 39);
			this.pnMain.Name = "pnMain";
			this.pnMain.Size = new System.Drawing.Size(357, 530);
			this.pnMain.TabIndex = 2;
			// 
			// defaultLookAndFeel
			// 
			this.defaultLookAndFeel.LookAndFeel.SkinName = "Money Twins";
			// 
			// FormShortcuts
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
			this.ClientSize = new System.Drawing.Size(357, 569);
			this.Controls.Add(this.pnMain);
			this.Controls.Add(this.pnTop);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormShortcuts";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Manage Desktop Shortcuts";
			this.xtraScrollableControl.ResumeLayout(false);
			this.pnWebSalesDepot.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pbWebSalesDepot)).EndInit();
			this.pnLocalSalesDepot.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pbLocalSalesDepot)).EndInit();
			this.pnTop.ResumeLayout(false);
			this.pnMain.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.XtraScrollableControl xtraScrollableControl;
        private System.Windows.Forms.Panel pnTop;
        private System.Windows.Forms.Panel pnMain;
		private System.Windows.Forms.Label laHeader;
		private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
		private System.Windows.Forms.Panel pnWebSalesDepot;
		private System.Windows.Forms.PictureBox pbWebSalesDepot;
		private System.Windows.Forms.Panel pnLocalSalesDepot;
		private System.Windows.Forms.PictureBox pbLocalSalesDepot;
    }
}