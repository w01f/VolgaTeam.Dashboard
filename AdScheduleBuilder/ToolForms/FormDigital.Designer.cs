namespace AdScheduleBuilder.ToolForms
{
	partial class FormDigital
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
			this.buttonXCancel = new DevComponents.DotNetBar.ButtonX();
			this.buttonXOK = new DevComponents.DotNetBar.ButtonX();
			this.xtraTabControl = new DevExpress.XtraTab.XtraTabControl();
			this.xtraTabPageSimple = new DevExpress.XtraTab.XtraTabPage();
			this.xtraTabPageDetailed = new DevExpress.XtraTab.XtraTabPage();
			this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel();
			this.styleController = new DevExpress.XtraEditors.StyleController();
			this.labelControlSimpleTitle = new DevExpress.XtraEditors.LabelControl();
			this.labelControlSimpleWebsites = new DevExpress.XtraEditors.LabelControl();
			this.memoEditSimpleWebsite = new DevExpress.XtraEditors.MemoEdit();
			this.memoEditSimpleProductInfo = new DevExpress.XtraEditors.MemoEdit();
			this.labelControlSimpleProductInfo = new DevExpress.XtraEditors.LabelControl();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).BeginInit();
			this.xtraTabControl.SuspendLayout();
			this.xtraTabPageSimple.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.memoEditSimpleWebsite.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.memoEditSimpleProductInfo.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// buttonXCancel
			// 
			this.buttonXCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonXCancel.Location = new System.Drawing.Point(279, 322);
			this.buttonXCancel.Name = "buttonXCancel";
			this.buttonXCancel.Size = new System.Drawing.Size(107, 36);
			this.buttonXCancel.TabIndex = 8;
			this.buttonXCancel.Text = "Cancel";
			this.buttonXCancel.TextColor = System.Drawing.Color.Black;
			// 
			// buttonXOK
			// 
			this.buttonXOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonXOK.Location = new System.Drawing.Point(163, 322);
			this.buttonXOK.Name = "buttonXOK";
			this.buttonXOK.Size = new System.Drawing.Size(107, 36);
			this.buttonXOK.TabIndex = 7;
			this.buttonXOK.Text = "OK";
			this.buttonXOK.TextColor = System.Drawing.Color.Black;
			// 
			// xtraTabControl
			// 
			this.xtraTabControl.Appearance.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControl.Appearance.Options.UseFont = true;
			this.xtraTabControl.AppearancePage.Header.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControl.AppearancePage.Header.Options.UseFont = true;
			this.xtraTabControl.AppearancePage.HeaderActive.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControl.AppearancePage.HeaderActive.Options.UseFont = true;
			this.xtraTabControl.AppearancePage.HeaderDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControl.AppearancePage.HeaderDisabled.Options.UseFont = true;
			this.xtraTabControl.AppearancePage.HeaderHotTracked.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControl.AppearancePage.HeaderHotTracked.Options.UseFont = true;
			this.xtraTabControl.AppearancePage.PageClient.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControl.AppearancePage.PageClient.Options.UseFont = true;
			this.xtraTabControl.Dock = System.Windows.Forms.DockStyle.Top;
			this.xtraTabControl.Location = new System.Drawing.Point(0, 0);
			this.xtraTabControl.Name = "xtraTabControl";
			this.xtraTabControl.SelectedTabPage = this.xtraTabPageSimple;
			this.xtraTabControl.Size = new System.Drawing.Size(399, 310);
			this.xtraTabControl.TabIndex = 9;
			this.xtraTabControl.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPageSimple,
            this.xtraTabPageDetailed});
			// 
			// xtraTabPageSimple
			// 
			this.xtraTabPageSimple.Controls.Add(this.memoEditSimpleProductInfo);
			this.xtraTabPageSimple.Controls.Add(this.labelControlSimpleProductInfo);
			this.xtraTabPageSimple.Controls.Add(this.memoEditSimpleWebsite);
			this.xtraTabPageSimple.Controls.Add(this.labelControlSimpleWebsites);
			this.xtraTabPageSimple.Controls.Add(this.labelControlSimpleTitle);
			this.xtraTabPageSimple.Name = "xtraTabPageSimple";
			this.xtraTabPageSimple.Size = new System.Drawing.Size(397, 284);
			this.xtraTabPageSimple.Text = "Simple";
			// 
			// xtraTabPageDetailed
			// 
			this.xtraTabPageDetailed.Name = "xtraTabPageDetailed";
			this.xtraTabPageDetailed.Size = new System.Drawing.Size(298, 274);
			this.xtraTabPageDetailed.Text = "Detailed";
			// 
			// defaultLookAndFeel
			// 
			this.defaultLookAndFeel.LookAndFeel.SkinName = "Money Twins";
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
			// labelControlSimpleTitle
			// 
			this.labelControlSimpleTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelControlSimpleTitle.Appearance.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.labelControlSimpleTitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
			this.labelControlSimpleTitle.Location = new System.Drawing.Point(11, 14);
			this.labelControlSimpleTitle.Name = "labelControlSimpleTitle";
			this.labelControlSimpleTitle.Size = new System.Drawing.Size(374, 36);
			this.labelControlSimpleTitle.StyleController = this.styleController;
			this.labelControlSimpleTitle.TabIndex = 0;
			this.labelControlSimpleTitle.Text = "Do you want to show the simple Digital Product Info with your ad Schedule?";
			// 
			// labelControlSimpleWebsites
			// 
			this.labelControlSimpleWebsites.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelControlSimpleWebsites.Appearance.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.labelControlSimpleWebsites.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
			this.labelControlSimpleWebsites.Location = new System.Drawing.Point(11, 74);
			this.labelControlSimpleWebsites.Name = "labelControlSimpleWebsites";
			this.labelControlSimpleWebsites.Size = new System.Drawing.Size(374, 19);
			this.labelControlSimpleWebsites.StyleController = this.styleController;
			this.labelControlSimpleWebsites.TabIndex = 1;
			this.labelControlSimpleWebsites.Text = "Websites";
			// 
			// memoEditSimpleWebsite
			// 
			this.memoEditSimpleWebsite.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.memoEditSimpleWebsite.Location = new System.Drawing.Point(11, 98);
			this.memoEditSimpleWebsite.Name = "memoEditSimpleWebsite";
			this.memoEditSimpleWebsite.Size = new System.Drawing.Size(374, 64);
			this.memoEditSimpleWebsite.TabIndex = 2;
			// 
			// memoEditSimpleProductInfo
			// 
			this.memoEditSimpleProductInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.memoEditSimpleProductInfo.Location = new System.Drawing.Point(11, 206);
			this.memoEditSimpleProductInfo.Name = "memoEditSimpleProductInfo";
			this.memoEditSimpleProductInfo.Size = new System.Drawing.Size(374, 64);
			this.memoEditSimpleProductInfo.TabIndex = 4;
			// 
			// labelControlSimpleProductInfo
			// 
			this.labelControlSimpleProductInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelControlSimpleProductInfo.Appearance.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.labelControlSimpleProductInfo.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
			this.labelControlSimpleProductInfo.Location = new System.Drawing.Point(11, 182);
			this.labelControlSimpleProductInfo.Name = "labelControlSimpleProductInfo";
			this.labelControlSimpleProductInfo.Size = new System.Drawing.Size(374, 19);
			this.labelControlSimpleProductInfo.StyleController = this.styleController;
			this.labelControlSimpleProductInfo.TabIndex = 3;
			this.labelControlSimpleProductInfo.Text = "Digital Product Info:";
			// 
			// FormDigital
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
			this.ClientSize = new System.Drawing.Size(399, 370);
			this.Controls.Add(this.xtraTabControl);
			this.Controls.Add(this.buttonXCancel);
			this.Controls.Add(this.buttonXOK);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormDigital";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Show Digital Product Info";
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).EndInit();
			this.xtraTabControl.ResumeLayout(false);
			this.xtraTabPageSimple.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.memoEditSimpleWebsite.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.memoEditSimpleProductInfo.Properties)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevComponents.DotNetBar.ButtonX buttonXCancel;
		private DevComponents.DotNetBar.ButtonX buttonXOK;
		private DevExpress.XtraTab.XtraTabControl xtraTabControl;
		private DevExpress.XtraTab.XtraTabPage xtraTabPageSimple;
		private DevExpress.XtraTab.XtraTabPage xtraTabPageDetailed;
		private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
		private DevExpress.XtraEditors.StyleController styleController;
		private DevExpress.XtraEditors.MemoEdit memoEditSimpleProductInfo;
		private DevExpress.XtraEditors.LabelControl labelControlSimpleProductInfo;
		private DevExpress.XtraEditors.MemoEdit memoEditSimpleWebsite;
		private DevExpress.XtraEditors.LabelControl labelControlSimpleWebsites;
		private DevExpress.XtraEditors.LabelControl labelControlSimpleTitle;
	}
}