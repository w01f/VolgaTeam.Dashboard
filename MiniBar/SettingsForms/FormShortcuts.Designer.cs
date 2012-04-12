namespace MiniBar.SettingsForms
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
            this.xtraScrollableControl = new DevExpress.XtraEditors.XtraScrollableControl();
            this.laSalesDepot = new System.Windows.Forms.Label();
            this.laDashboard = new System.Windows.Forms.Label();
            this.pbSalesDepot = new System.Windows.Forms.PictureBox();
            this.pbDashboard = new System.Windows.Forms.PictureBox();
            this.pnTop = new System.Windows.Forms.Panel();
            this.laHeader = new System.Windows.Forms.Label();
            this.pnMain = new System.Windows.Forms.Panel();
            this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel();
            this.pnStaticApplications = new System.Windows.Forms.Panel();
            this.xtraScrollableControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSalesDepot)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDashboard)).BeginInit();
            this.pnTop.SuspendLayout();
            this.pnMain.SuspendLayout();
            this.pnStaticApplications.SuspendLayout();
            this.SuspendLayout();
            // 
            // xtraScrollableControl
            // 
            this.xtraScrollableControl.AlwaysScrollActiveControlIntoView = false;
            this.xtraScrollableControl.Controls.Add(this.pnStaticApplications);
            this.xtraScrollableControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraScrollableControl.Location = new System.Drawing.Point(0, 0);
            this.xtraScrollableControl.Name = "xtraScrollableControl";
            this.xtraScrollableControl.Size = new System.Drawing.Size(353, 300);
            this.xtraScrollableControl.TabIndex = 0;
            // 
            // laSalesDepot
            // 
            this.laSalesDepot.AutoSize = true;
            this.laSalesDepot.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laSalesDepot.Location = new System.Drawing.Point(109, 131);
            this.laSalesDepot.Name = "laSalesDepot";
            this.laSalesDepot.Size = new System.Drawing.Size(50, 18);
            this.laSalesDepot.TabIndex = 3;
            this.laSalesDepot.Text = "label1";
            // 
            // laDashboard
            // 
            this.laDashboard.AutoSize = true;
            this.laDashboard.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laDashboard.Location = new System.Drawing.Point(107, 41);
            this.laDashboard.Name = "laDashboard";
            this.laDashboard.Size = new System.Drawing.Size(50, 18);
            this.laDashboard.TabIndex = 1;
            this.laDashboard.Text = "label1";
            // 
            // pbSalesDepot
            // 
            this.pbSalesDepot.Image = global::MiniBar.Properties.Resources.SalesDepot;
            this.pbSalesDepot.Location = new System.Drawing.Point(15, 104);
            this.pbSalesDepot.Name = "pbSalesDepot";
            this.pbSalesDepot.Size = new System.Drawing.Size(77, 72);
            this.pbSalesDepot.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbSalesDepot.TabIndex = 2;
            this.pbSalesDepot.TabStop = false;
            this.pbSalesDepot.Click += new System.EventHandler(this.pbSalesDepot_Click);
            this.pbSalesDepot.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
            this.pbSalesDepot.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseUp);
            // 
            // pbDashboard
            // 
            this.pbDashboard.Image = global::MiniBar.Properties.Resources.Dashboard;
            this.pbDashboard.Location = new System.Drawing.Point(13, 14);
            this.pbDashboard.Name = "pbDashboard";
            this.pbDashboard.Size = new System.Drawing.Size(78, 72);
            this.pbDashboard.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbDashboard.TabIndex = 0;
            this.pbDashboard.TabStop = false;
            this.pbDashboard.Click += new System.EventHandler(this.pbDashboard_Click);
            this.pbDashboard.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
            this.pbDashboard.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseUp);
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
            this.pnMain.Size = new System.Drawing.Size(357, 304);
            this.pnMain.TabIndex = 2;
            // 
            // defaultLookAndFeel
            // 
            this.defaultLookAndFeel.LookAndFeel.SkinName = "Money Twins";
            // 
            // pnStaticApplications
            // 
            this.pnStaticApplications.Controls.Add(this.pbSalesDepot);
            this.pnStaticApplications.Controls.Add(this.laSalesDepot);
            this.pnStaticApplications.Controls.Add(this.pbDashboard);
            this.pnStaticApplications.Controls.Add(this.laDashboard);
            this.pnStaticApplications.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnStaticApplications.Location = new System.Drawing.Point(0, 0);
            this.pnStaticApplications.Name = "pnStaticApplications";
            this.pnStaticApplications.Size = new System.Drawing.Size(353, 191);
            this.pnStaticApplications.TabIndex = 10;
            // 
            // FormShortcuts
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(357, 343);
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
            ((System.ComponentModel.ISupportInitialize)(this.pbSalesDepot)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDashboard)).EndInit();
            this.pnTop.ResumeLayout(false);
            this.pnMain.ResumeLayout(false);
            this.pnStaticApplications.ResumeLayout(false);
            this.pnStaticApplications.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.XtraScrollableControl xtraScrollableControl;
        private System.Windows.Forms.Panel pnTop;
        private System.Windows.Forms.Panel pnMain;
        private System.Windows.Forms.Label laHeader;
        private System.Windows.Forms.PictureBox pbDashboard;
        private System.Windows.Forms.Label laSalesDepot;
        private System.Windows.Forms.PictureBox pbSalesDepot;
        private System.Windows.Forms.Label laDashboard;
        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
        private System.Windows.Forms.Panel pnStaticApplications;
    }
}