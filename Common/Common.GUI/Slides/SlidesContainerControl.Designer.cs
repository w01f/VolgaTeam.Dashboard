namespace Asa.Common.GUI.Slides
{
	partial class SlidesContainerControl
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
			this.xtraTabControlSlides = new DevExpress.XtraTab.XtraTabControl();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControlSlides)).BeginInit();
			this.SuspendLayout();
			// 
			// xtraTabControlSlides
			// 
			this.xtraTabControlSlides.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControlSlides.Appearance.Options.UseFont = true;
			this.xtraTabControlSlides.AppearancePage.Header.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlSlides.AppearancePage.Header.Options.UseFont = true;
			this.xtraTabControlSlides.AppearancePage.HeaderActive.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControlSlides.AppearancePage.HeaderActive.Options.UseFont = true;
			this.xtraTabControlSlides.AppearancePage.HeaderDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlSlides.AppearancePage.HeaderDisabled.Options.UseFont = true;
			this.xtraTabControlSlides.AppearancePage.HeaderHotTracked.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlSlides.AppearancePage.HeaderHotTracked.Options.UseFont = true;
			this.xtraTabControlSlides.AppearancePage.PageClient.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlSlides.AppearancePage.PageClient.Options.UseFont = true;
			this.xtraTabControlSlides.Dock = System.Windows.Forms.DockStyle.Fill;
			this.xtraTabControlSlides.Location = new System.Drawing.Point(0, 0);
			this.xtraTabControlSlides.Name = "xtraTabControlSlides";
			this.xtraTabControlSlides.Size = new System.Drawing.Size(637, 497);
			this.xtraTabControlSlides.TabIndex = 0;
			// 
			// SlidesContainerControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.Controls.Add(this.xtraTabControlSlides);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "SlidesContainerControl";
			this.Size = new System.Drawing.Size(637, 497);
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControlSlides)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		protected DevExpress.XtraTab.XtraTabControl xtraTabControlSlides;
	}
}
