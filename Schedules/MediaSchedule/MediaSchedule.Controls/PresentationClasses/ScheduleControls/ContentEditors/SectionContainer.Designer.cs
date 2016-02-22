namespace Asa.Media.Controls.PresentationClasses.ScheduleControls.ContentEditors
{
	partial class SectionContainer
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
			this.labelControlWarnings = new DevExpress.XtraEditors.LabelControl();
			this.styleController = new DevExpress.XtraEditors.StyleController();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			this.SuspendLayout();
			// 
			// xtraTabControl
			// 
			this.xtraTabControl.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControl.Appearance.Options.UseFont = true;
			this.xtraTabControl.AppearancePage.Header.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControl.AppearancePage.Header.Options.UseFont = true;
			this.xtraTabControl.AppearancePage.HeaderActive.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControl.AppearancePage.HeaderActive.Options.UseFont = true;
			this.xtraTabControl.AppearancePage.HeaderDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControl.AppearancePage.HeaderDisabled.Options.UseFont = true;
			this.xtraTabControl.AppearancePage.HeaderHotTracked.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControl.AppearancePage.HeaderHotTracked.Options.UseFont = true;
			this.xtraTabControl.AppearancePage.PageClient.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControl.AppearancePage.PageClient.Options.UseFont = true;
			this.xtraTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.xtraTabControl.Location = new System.Drawing.Point(0, 27);
			this.xtraTabControl.Name = "xtraTabControl";
			this.xtraTabControl.Size = new System.Drawing.Size(712, 538);
			this.xtraTabControl.TabIndex = 0;
			// 
			// labelControlWarnings
			// 
			this.labelControlWarnings.AllowHtmlString = true;
			this.labelControlWarnings.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.labelControlWarnings.Appearance.ForeColor = System.Drawing.Color.Red;
			this.labelControlWarnings.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			this.labelControlWarnings.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.labelControlWarnings.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.labelControlWarnings.Cursor = System.Windows.Forms.Cursors.Hand;
			this.labelControlWarnings.Dock = System.Windows.Forms.DockStyle.Top;
			this.labelControlWarnings.Location = new System.Drawing.Point(0, 0);
			this.labelControlWarnings.Name = "labelControlWarnings";
			this.labelControlWarnings.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
			this.labelControlWarnings.Size = new System.Drawing.Size(712, 27);
			this.labelControlWarnings.StyleController = this.styleController;
			this.labelControlWarnings.TabIndex = 9;
			// 
			// styleController
			// 
			this.styleController.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.styleController.Appearance.Options.UseFont = true;
			this.styleController.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.styleController.AppearanceDisabled.Options.UseFont = true;
			this.styleController.AppearanceDropDown.Font = new System.Drawing.Font("Arial", 9.75F);
			this.styleController.AppearanceDropDown.Options.UseFont = true;
			this.styleController.AppearanceDropDownHeader.Font = new System.Drawing.Font("Arial", 9.75F);
			this.styleController.AppearanceDropDownHeader.Options.UseFont = true;
			this.styleController.AppearanceFocused.Font = new System.Drawing.Font("Arial", 9.75F);
			this.styleController.AppearanceFocused.Options.UseFont = true;
			this.styleController.AppearanceReadOnly.Font = new System.Drawing.Font("Arial", 9.75F);
			this.styleController.AppearanceReadOnly.Options.UseFont = true;
			// 
			// SectionContainer
			// 
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.xtraTabControl);
			this.Controls.Add(this.labelControlWarnings);
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "SectionContainer";
			this.Size = new System.Drawing.Size(712, 565);
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraTab.XtraTabControl xtraTabControl;
		private DevExpress.XtraEditors.LabelControl labelControlWarnings;
		private DevExpress.XtraEditors.StyleController styleController;
	}
}
