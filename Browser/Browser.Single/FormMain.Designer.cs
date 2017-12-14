namespace Asa.Browser.Single
{
	partial class FormMain
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
			this.styleManager = new DevComponents.DotNetBar.StyleManager();
			this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel();
			this.barBottom = new DevComponents.DotNetBar.Bar();
			this.itemContainerStatusBarInfo = new DevComponents.DotNetBar.ItemContainer();
			this.labelItemAppTitle = new DevComponents.DotNetBar.LabelItem();
			this.labelItemUrl = new DevComponents.DotNetBar.LabelItem();
			this.labelItemStatusBarSeparator = new DevComponents.DotNetBar.LabelItem();
			this.itemContainerStatusBarActionButtons = new DevComponents.DotNetBar.ItemContainer();
			this.buttonItemUrlEmail = new DevComponents.DotNetBar.ButtonItem();
			this.buttonItemUrlCopy = new DevComponents.DotNetBar.ButtonItem();
			((System.ComponentModel.ISupportInitialize)(this.barBottom)).BeginInit();
			this.SuspendLayout();
			// 
			// styleManager
			// 
			this.styleManager.ManagerStyle = DevComponents.DotNetBar.eStyle.Metro;
			this.styleManager.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.White, System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(87)))), ((int)(((byte)(154))))));
			// 
			// defaultLookAndFeel
			// 
			this.defaultLookAndFeel.LookAndFeel.SkinName = "Office 2013";
			// 
			// barBottom
			// 
			this.barBottom.AntiAlias = true;
			this.barBottom.BarType = DevComponents.DotNetBar.eBarType.StatusBar;
			this.barBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.barBottom.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.barBottom.IsMaximized = false;
			this.barBottom.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.itemContainerStatusBarInfo,
            this.labelItemStatusBarSeparator,
            this.itemContainerStatusBarActionButtons});
			this.barBottom.Location = new System.Drawing.Point(0, 527);
			this.barBottom.Name = "barBottom";
			this.barBottom.PaddingBottom = 0;
			this.barBottom.PaddingTop = 0;
			this.barBottom.Size = new System.Drawing.Size(884, 28);
			this.barBottom.Stretch = true;
			this.barBottom.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.barBottom.TabIndex = 1;
			this.barBottom.TabStop = false;
			// 
			// itemContainerStatusBarInfo
			// 
			// 
			// 
			// 
			this.itemContainerStatusBarInfo.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.itemContainerStatusBarInfo.ItemSpacing = 20;
			this.itemContainerStatusBarInfo.MinimumSize = new System.Drawing.Size(0, 26);
			this.itemContainerStatusBarInfo.Name = "itemContainerStatusBarInfo";
			this.itemContainerStatusBarInfo.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.labelItemAppTitle,
            this.labelItemUrl});
			// 
			// 
			// 
			this.itemContainerStatusBarInfo.TitleMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.itemContainerStatusBarInfo.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.itemContainerStatusBarInfo.VerticalItemAlignment = DevComponents.DotNetBar.eVerticalItemsAlignment.Middle;
			// 
			// labelItemAppTitle
			// 
			this.labelItemAppTitle.Name = "labelItemAppTitle";
			this.labelItemAppTitle.Text = "Sales Browser";
			// 
			// labelItemUrl
			// 
			this.labelItemUrl.Name = "labelItemUrl";
			// 
			// labelItemStatusBarSeparator
			// 
			this.labelItemStatusBarSeparator.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far;
			this.labelItemStatusBarSeparator.Name = "labelItemStatusBarSeparator";
			// 
			// itemContainerStatusBarActionButtons
			// 
			// 
			// 
			// 
			this.itemContainerStatusBarActionButtons.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.itemContainerStatusBarActionButtons.ItemSpacing = 5;
			this.itemContainerStatusBarActionButtons.MinimumSize = new System.Drawing.Size(0, 26);
			this.itemContainerStatusBarActionButtons.Name = "itemContainerStatusBarActionButtons";
			this.itemContainerStatusBarActionButtons.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItemUrlEmail,
            this.buttonItemUrlCopy});
			// 
			// 
			// 
			this.itemContainerStatusBarActionButtons.TitleMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.itemContainerStatusBarActionButtons.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.itemContainerStatusBarActionButtons.VerticalItemAlignment = DevComponents.DotNetBar.eVerticalItemsAlignment.Middle;
			// 
			// buttonItemUrlEmail
			// 
			this.buttonItemUrlEmail.Name = "buttonItemUrlEmail";
			this.buttonItemUrlEmail.Click += new System.EventHandler(this.OnUrlEmailClick);
			// 
			// buttonItemUrlCopy
			// 
			this.buttonItemUrlCopy.Name = "buttonItemUrlCopy";
			this.buttonItemUrlCopy.Click += new System.EventHandler(this.OnUrlCopyClick);
			// 
			// FormMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.ClientSize = new System.Drawing.Size(884, 555);
			this.Controls.Add(this.barBottom);
			this.DoubleBuffered = true;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Browser";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OnFormClosed);
			((System.ComponentModel.ISupportInitialize)(this.barBottom)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevComponents.DotNetBar.StyleManager styleManager;
		private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
		private DevComponents.DotNetBar.Bar barBottom;
		private DevComponents.DotNetBar.ItemContainer itemContainerStatusBarInfo;
		private DevComponents.DotNetBar.LabelItem labelItemStatusBarSeparator;
		private DevComponents.DotNetBar.ItemContainer itemContainerStatusBarActionButtons;
		private DevComponents.DotNetBar.LabelItem labelItemAppTitle;
		private DevComponents.DotNetBar.LabelItem labelItemUrl;
		private DevComponents.DotNetBar.ButtonItem buttonItemUrlEmail;
		private DevComponents.DotNetBar.ButtonItem buttonItemUrlCopy;
	}
}

