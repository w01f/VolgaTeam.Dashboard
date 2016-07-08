namespace Asa.Media.Controls.PresentationClasses.SnapshotControls.ContentEditors
{
	partial class SnapshotEditorsContainer
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
			this.components = new System.ComponentModel.Container();
			this.xtraTabControl = new DevExpress.XtraTab.XtraTabControl();
			this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
			this.pnInfo = new System.Windows.Forms.Panel();
			this.labelControlCollectionItemsInfo = new DevExpress.XtraEditors.LabelControl();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			this.pnInfo.SuspendLayout();
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
			this.xtraTabControl.Location = new System.Drawing.Point(0, 32);
			this.xtraTabControl.Name = "xtraTabControl";
			this.xtraTabControl.Size = new System.Drawing.Size(712, 533);
			this.xtraTabControl.TabIndex = 0;
			this.xtraTabControl.UseMnemonic = false;
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
			// pnInfo
			// 
			this.pnInfo.Controls.Add(this.labelControlCollectionItemsInfo);
			this.pnInfo.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnInfo.Location = new System.Drawing.Point(0, 0);
			this.pnInfo.Name = "pnInfo";
			this.pnInfo.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
			this.pnInfo.Size = new System.Drawing.Size(712, 32);
			this.pnInfo.TabIndex = 11;
			// 
			// labelControlCollectionItemsInfo
			// 
			this.labelControlCollectionItemsInfo.AllowHtmlString = true;
			this.labelControlCollectionItemsInfo.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.labelControlCollectionItemsInfo.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.labelControlCollectionItemsInfo.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.labelControlCollectionItemsInfo.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.labelControlCollectionItemsInfo.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.labelControlCollectionItemsInfo.Cursor = System.Windows.Forms.Cursors.Hand;
			this.labelControlCollectionItemsInfo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelControlCollectionItemsInfo.Location = new System.Drawing.Point(10, 0);
			this.labelControlCollectionItemsInfo.Name = "labelControlCollectionItemsInfo";
			this.labelControlCollectionItemsInfo.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
			this.labelControlCollectionItemsInfo.Size = new System.Drawing.Size(702, 32);
			this.labelControlCollectionItemsInfo.StyleController = this.styleController;
			this.labelControlCollectionItemsInfo.TabIndex = 10;
			// 
			// OptionSetEditorsContainer
			// 
			this.Controls.Add(this.xtraTabControl);
			this.Controls.Add(this.pnInfo);
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Size = new System.Drawing.Size(712, 565);
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			this.pnInfo.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraTab.XtraTabControl xtraTabControl;
		private DevExpress.XtraEditors.StyleController styleController;
		private System.Windows.Forms.Panel pnInfo;
		private DevExpress.XtraEditors.LabelControl labelControlCollectionItemsInfo;
	}
}
