namespace Asa.Browser.Controls.Controls.WebPage
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
			this.pnProgress.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbProgressLogo)).BeginInit();
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
			this.circularProgress.Size = new System.Drawing.Size(50, 315);
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
			this.pnProgress.Size = new System.Drawing.Size(800, 581);
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
			this.labelControlProgressText.Size = new System.Drawing.Size(370, 315);
			this.labelControlProgressText.TabIndex = 4;
			this.labelControlProgressText.Text = "<color=MediumSeaGreen>Chill out for just a few seconds…<br>Your app is connecting" +
    " to the server…</color>";
			// 
			// pbProgressLogo
			// 
			this.pbProgressLogo.Dock = System.Windows.Forms.DockStyle.Top;
			this.pbProgressLogo.Image = global::Asa.Browser.Controls.Properties.Resources.ProgressLogo;
			this.pbProgressLogo.Location = new System.Drawing.Point(180, 50);
			this.pbProgressLogo.Name = "pbProgressLogo";
			this.pbProgressLogo.Size = new System.Drawing.Size(420, 166);
			this.pbProgressLogo.TabIndex = 3;
			this.pbProgressLogo.TabStop = false;
			// 
			// WebKitPage
			// 
			this.Controls.Add(this.pnProgress);
			this.Size = new System.Drawing.Size(800, 581);
			this.Resize += new System.EventHandler(this.OnWebPageResize);
			this.pnProgress.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pbProgressLogo)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevComponents.DotNetBar.Controls.CircularProgress circularProgress;
		private System.Windows.Forms.Panel pnProgress;
		private DevExpress.XtraEditors.LabelControl labelControlProgressText;
		private System.Windows.Forms.PictureBox pbProgressLogo;
	}
}
