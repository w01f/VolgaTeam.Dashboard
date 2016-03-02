namespace AdSalesBrowser
{
	sealed partial class IEPage
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
			this.circularProgressWebpage = new DevComponents.DotNetBar.Controls.CircularProgress();
			this.SuspendLayout();
			// 
			// circularProgressWebpage
			// 
			this.circularProgressWebpage.BackColor = System.Drawing.Color.Transparent;
			// 
			// 
			// 
			this.circularProgressWebpage.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.circularProgressWebpage.Location = new System.Drawing.Point(23, 16);
			this.circularProgressWebpage.Name = "circularProgressWebpage";
			this.circularProgressWebpage.ProgressBarType = DevComponents.DotNetBar.eCircularProgressType.Dot;
			this.circularProgressWebpage.Size = new System.Drawing.Size(41, 44);
			this.circularProgressWebpage.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP;
			this.circularProgressWebpage.TabIndex = 2;
			this.circularProgressWebpage.TabStop = false;
			// 
			// WebPage
			// 
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.circularProgressWebpage);
			this.Name = "WebPage";
			this.Size = new System.Drawing.Size(572, 480);
			this.Resize += new System.EventHandler(this.WebPage_Resize);
			this.ResumeLayout(false);

		}

		#endregion

		private DevComponents.DotNetBar.Controls.CircularProgress circularProgressWebpage;
	}
}
