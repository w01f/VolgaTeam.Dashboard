namespace AdSalesBrowser.WebPage
{
	sealed partial class WebKitPage
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
			this.circularProgress = new DevComponents.DotNetBar.Controls.CircularProgress();
			this.pnProgress = new System.Windows.Forms.Panel();
			this.labelControlProgressText = new DevExpress.XtraEditors.LabelControl();
			this.pbProgressLogo = new System.Windows.Forms.PictureBox();
			this.pbIExplorer = new System.Windows.Forms.PictureBox();
			this.pbFirefox = new System.Windows.Forms.PictureBox();
			this.pbChrome = new System.Windows.Forms.PictureBox();
			this.superTooltip = new DevComponents.DotNetBar.SuperTooltip();
			this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
			this.pnProgress.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbProgressLogo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbIExplorer)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbFirefox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbChrome)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
			this.panelControl1.SuspendLayout();
			this.SuspendLayout();
			// 
			// circularProgress
			// 
			this.circularProgress.BackColor = System.Drawing.Color.Transparent;
			// 
			// 
			// 
			this.circularProgress.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.circularProgress.Dock = System.Windows.Forms.DockStyle.Left;
			this.circularProgress.Location = new System.Drawing.Point(180, 216);
			this.circularProgress.Name = "circularProgress";
			this.circularProgress.ProgressBarType = DevComponents.DotNetBar.eCircularProgressType.Dot;
			this.circularProgress.ProgressColor = System.Drawing.Color.MediumSeaGreen;
			this.circularProgress.Size = new System.Drawing.Size(50, 289);
			this.circularProgress.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP;
			this.circularProgress.TabIndex = 2;
			this.circularProgress.TabStop = false;
			// 
			// pnProgress
			// 
			this.pnProgress.Controls.Add(this.labelControlProgressText);
			this.pnProgress.Controls.Add(this.circularProgress);
			this.pnProgress.Controls.Add(this.pbProgressLogo);
			this.pnProgress.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnProgress.Location = new System.Drawing.Point(0, 0);
			this.pnProgress.Name = "pnProgress";
			this.pnProgress.Padding = new System.Windows.Forms.Padding(180, 50, 200, 50);
			this.pnProgress.Size = new System.Drawing.Size(800, 555);
			this.pnProgress.TabIndex = 3;
			// 
			// labelControlProgressText
			// 
			this.labelControlProgressText.AllowHtmlString = true;
			this.labelControlProgressText.Appearance.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.labelControlProgressText.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.labelControlProgressText.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.labelControlProgressText.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.labelControlProgressText.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelControlProgressText.Location = new System.Drawing.Point(230, 216);
			this.labelControlProgressText.Name = "labelControlProgressText";
			this.labelControlProgressText.Padding = new System.Windows.Forms.Padding(30, 0, 0, 0);
			this.labelControlProgressText.Size = new System.Drawing.Size(370, 289);
			this.labelControlProgressText.TabIndex = 4;
			this.labelControlProgressText.Text = "<color=MediumSeaGreen>Chill out for just a few seconds…<br>Your app is connecting" +
    " to the server…</color>";
			// 
			// pbProgressLogo
			// 
			this.pbProgressLogo.Dock = System.Windows.Forms.DockStyle.Top;
			this.pbProgressLogo.Image = global::AdSalesBrowser.Properties.Resources.ProgressLogo;
			this.pbProgressLogo.Location = new System.Drawing.Point(180, 50);
			this.pbProgressLogo.Name = "pbProgressLogo";
			this.pbProgressLogo.Size = new System.Drawing.Size(420, 166);
			this.pbProgressLogo.TabIndex = 3;
			this.pbProgressLogo.TabStop = false;
			// 
			// pbIExplorer
			// 
			this.pbIExplorer.Enabled = false;
			this.pbIExplorer.Image = global::AdSalesBrowser.Properties.Resources.ie;
			this.pbIExplorer.Location = new System.Drawing.Point(91, 1);
			this.pbIExplorer.Name = "pbIExplorer";
			this.pbIExplorer.Size = new System.Drawing.Size(24, 24);
			this.superTooltip.SetSuperTooltip(this.pbIExplorer, new DevComponents.DotNetBar.SuperTooltipInfo("", "", "Open in Internet Explorer", null, null, DevComponents.DotNetBar.eTooltipColor.Gray, false, false, new System.Drawing.Size(0, 0)));
			this.pbIExplorer.TabIndex = 2;
			this.pbIExplorer.TabStop = false;
			this.pbIExplorer.Tag = "iexplore";
			this.pbIExplorer.Click += new System.EventHandler(this.OnExternalBrowserOpenClick);
			// 
			// pbFirefox
			// 
			this.pbFirefox.Enabled = false;
			this.pbFirefox.Image = global::AdSalesBrowser.Properties.Resources.ff;
			this.pbFirefox.Location = new System.Drawing.Point(50, 1);
			this.pbFirefox.Name = "pbFirefox";
			this.pbFirefox.Size = new System.Drawing.Size(24, 24);
			this.superTooltip.SetSuperTooltip(this.pbFirefox, new DevComponents.DotNetBar.SuperTooltipInfo("", "", "Open in Firefox", null, null, DevComponents.DotNetBar.eTooltipColor.Gray, false, false, new System.Drawing.Size(0, 0)));
			this.pbFirefox.TabIndex = 1;
			this.pbFirefox.TabStop = false;
			this.pbFirefox.Tag = "firefox";
			this.pbFirefox.Click += new System.EventHandler(this.OnExternalBrowserOpenClick);
			// 
			// pbChrome
			// 
			this.pbChrome.Enabled = false;
			this.pbChrome.Image = global::AdSalesBrowser.Properties.Resources.ch;
			this.pbChrome.Location = new System.Drawing.Point(9, 1);
			this.pbChrome.Name = "pbChrome";
			this.pbChrome.Size = new System.Drawing.Size(24, 24);
			this.superTooltip.SetSuperTooltip(this.pbChrome, new DevComponents.DotNetBar.SuperTooltipInfo("", "", "Open in Chrome", null, null, DevComponents.DotNetBar.eTooltipColor.Gray, false, false, new System.Drawing.Size(0, 0)));
			this.pbChrome.TabIndex = 0;
			this.pbChrome.TabStop = false;
			this.pbChrome.Tag = "chrome";
			this.pbChrome.Click += new System.EventHandler(this.OnExternalBrowserOpenClick);
			// 
			// superTooltip
			// 
			this.superTooltip.DefaultTooltipSettings = new DevComponents.DotNetBar.SuperTooltipInfo("", "", "", null, null, DevComponents.DotNetBar.eTooltipColor.Gray);
			this.superTooltip.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			// 
			// panelControl1
			// 
			this.panelControl1.Controls.Add(this.pbIExplorer);
			this.panelControl1.Controls.Add(this.pbChrome);
			this.panelControl1.Controls.Add(this.pbFirefox);
			this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panelControl1.Location = new System.Drawing.Point(0, 555);
			this.panelControl1.Name = "panelControl1";
			this.panelControl1.Size = new System.Drawing.Size(800, 26);
			this.panelControl1.TabIndex = 5;
			// 
			// WebKitPage
			// 
			this.Controls.Add(this.pnProgress);
			this.Controls.Add(this.panelControl1);
			this.Name = "WebKitPage";
			this.Size = new System.Drawing.Size(800, 581);
			this.Resize += new System.EventHandler(this.OnWebPageResize);
			this.pnProgress.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pbProgressLogo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbIExplorer)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbFirefox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbChrome)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
			this.panelControl1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private DevComponents.DotNetBar.Controls.CircularProgress circularProgress;
		private System.Windows.Forms.Panel pnProgress;
		private DevExpress.XtraEditors.LabelControl labelControlProgressText;
		private System.Windows.Forms.PictureBox pbProgressLogo;
		private System.Windows.Forms.PictureBox pbIExplorer;
		private System.Windows.Forms.PictureBox pbFirefox;
		private System.Windows.Forms.PictureBox pbChrome;
		private DevComponents.DotNetBar.SuperTooltip superTooltip;
		private DevExpress.XtraEditors.PanelControl panelControl1;
	}
}
