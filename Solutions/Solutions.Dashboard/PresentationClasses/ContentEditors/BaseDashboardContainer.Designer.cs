namespace Asa.Solutions.Dashboard.PresentationClasses.ContentEditors
{
	partial class BaseDashboardContainer
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
			this.xtraTabControl = new DevExpress.XtraTab.XtraTabControl();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).BeginInit();
			this.SuspendLayout();
			// 
			// xtraTabControl
			// 
			this.xtraTabControl.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControl.Appearance.Options.UseFont = true;
			this.xtraTabControl.Appearance.Options.UseTextOptions = true;
			this.xtraTabControl.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.None;
			this.xtraTabControl.AppearancePage.Header.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControl.AppearancePage.Header.Options.UseFont = true;
			this.xtraTabControl.AppearancePage.Header.Options.UseTextOptions = true;
			this.xtraTabControl.AppearancePage.Header.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.None;
			this.xtraTabControl.AppearancePage.HeaderActive.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControl.AppearancePage.HeaderActive.Options.UseFont = true;
			this.xtraTabControl.AppearancePage.HeaderActive.Options.UseTextOptions = true;
			this.xtraTabControl.AppearancePage.HeaderActive.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.None;
			this.xtraTabControl.AppearancePage.HeaderDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControl.AppearancePage.HeaderDisabled.Options.UseFont = true;
			this.xtraTabControl.AppearancePage.HeaderDisabled.Options.UseTextOptions = true;
			this.xtraTabControl.AppearancePage.HeaderDisabled.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.None;
			this.xtraTabControl.AppearancePage.HeaderHotTracked.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControl.AppearancePage.HeaderHotTracked.Options.UseFont = true;
			this.xtraTabControl.AppearancePage.HeaderHotTracked.Options.UseTextOptions = true;
			this.xtraTabControl.AppearancePage.HeaderHotTracked.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.None;
			this.xtraTabControl.AppearancePage.PageClient.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControl.AppearancePage.PageClient.Options.UseFont = true;
			this.xtraTabControl.AppearancePage.PageClient.Options.UseTextOptions = true;
			this.xtraTabControl.AppearancePage.PageClient.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.None;
			this.xtraTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.xtraTabControl.Location = new System.Drawing.Point(0, 0);
			this.xtraTabControl.Name = "xtraTabControl";
			this.xtraTabControl.Size = new System.Drawing.Size(589, 460);
			this.xtraTabControl.TabIndex = 0;
			// 
			// BaseDashboardContainer
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.Controls.Add(this.xtraTabControl);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "BaseDashboardContainer";
			this.Size = new System.Drawing.Size(589, 460);
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraTab.XtraTabControl xtraTabControl;
	}
}
