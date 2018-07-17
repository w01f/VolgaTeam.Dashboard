namespace Asa.Common.GUI.Preview
{
	partial class PreviewGroupControl
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
			this.pnContainer = new System.Windows.Forms.Panel();
			this.xtraTabControlItems = new DevExpress.XtraTab.XtraTabControl();
			this.pnContainer.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControlItems)).BeginInit();
			this.SuspendLayout();
			// 
			// pnContainer
			// 
			this.pnContainer.Controls.Add(this.xtraTabControlItems);
			this.pnContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnContainer.Location = new System.Drawing.Point(0, 0);
			this.pnContainer.Name = "pnContainer";
			this.pnContainer.Padding = new System.Windows.Forms.Padding(0, 30, 0, 0);
			this.pnContainer.Size = new System.Drawing.Size(561, 430);
			this.pnContainer.TabIndex = 0;
			// 
			// xtraTabControlItems
			// 
			this.xtraTabControlItems.Appearance.BackColor = System.Drawing.Color.White;
			this.xtraTabControlItems.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControlItems.Appearance.ForeColor = System.Drawing.Color.Black;
			this.xtraTabControlItems.Appearance.Options.UseBackColor = true;
			this.xtraTabControlItems.Appearance.Options.UseFont = true;
			this.xtraTabControlItems.Appearance.Options.UseForeColor = true;
			this.xtraTabControlItems.AppearancePage.Header.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlItems.AppearancePage.Header.Options.UseFont = true;
			this.xtraTabControlItems.AppearancePage.Header.Options.UseTextOptions = true;
			this.xtraTabControlItems.AppearancePage.Header.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.None;
			this.xtraTabControlItems.AppearancePage.HeaderActive.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControlItems.AppearancePage.HeaderActive.Options.UseFont = true;
			this.xtraTabControlItems.AppearancePage.HeaderActive.Options.UseTextOptions = true;
			this.xtraTabControlItems.AppearancePage.HeaderActive.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.None;
			this.xtraTabControlItems.AppearancePage.HeaderDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlItems.AppearancePage.HeaderDisabled.Options.UseFont = true;
			this.xtraTabControlItems.AppearancePage.HeaderHotTracked.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlItems.AppearancePage.HeaderHotTracked.Options.UseFont = true;
			this.xtraTabControlItems.AppearancePage.PageClient.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlItems.AppearancePage.PageClient.Options.UseFont = true;
			this.xtraTabControlItems.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.xtraTabControlItems.Dock = System.Windows.Forms.DockStyle.Fill;
			this.xtraTabControlItems.Location = new System.Drawing.Point(5, 30);
			this.xtraTabControlItems.Name = "xtraTabControlItems";
			this.xtraTabControlItems.Size = new System.Drawing.Size(551, 395);
			this.xtraTabControlItems.TabIndex = 11;
			this.xtraTabControlItems.UseMnemonic = false;
			// 
			// PreviewGroupControl
			// 
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.pnContainer);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "PreviewGroupControl";
			this.Size = new System.Drawing.Size(561, 430);
			this.pnContainer.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControlItems)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel pnContainer;
		private DevExpress.XtraTab.XtraTabControl xtraTabControlItems;
	}
}
