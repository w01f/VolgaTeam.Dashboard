namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors
{
	public partial class StarAppControl
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
			this.labelFocusFake = new System.Windows.Forms.Label();
			this.barManager = new DevExpress.XtraBars.BarManager();
			this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
			this.barButtonItemImagePaste = new DevExpress.XtraBars.BarButtonItem();
			this.barButtonItemImageFavoritesAdd = new DevExpress.XtraBars.BarButtonItem();
			this.barButtonItemImageOpen = new DevExpress.XtraBars.BarButtonItem();
			this.barButtonItemImageReset = new DevExpress.XtraBars.BarButtonItem();
			this.barButtonItemImagePreview = new DevExpress.XtraBars.BarButtonItem();
			this.popupMenuImage = new DevExpress.XtraBars.PopupMenu();
			this.barButtonItemImageFavoritesOpen = new DevExpress.XtraBars.BarButtonItem();
			((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.popupMenuImage)).BeginInit();
			this.SuspendLayout();
			// 
			// labelFocusFake
			// 
			this.labelFocusFake.AutoSize = true;
			this.labelFocusFake.Location = new System.Drawing.Point(-100, -100);
			this.labelFocusFake.Name = "labelFocusFake";
			this.labelFocusFake.Size = new System.Drawing.Size(35, 13);
			this.labelFocusFake.TabIndex = 0;
			this.labelFocusFake.Text = "label1";
			// 
			// barManager
			// 
			this.barManager.DockControls.Add(this.barDockControlTop);
			this.barManager.DockControls.Add(this.barDockControlBottom);
			this.barManager.DockControls.Add(this.barDockControlLeft);
			this.barManager.DockControls.Add(this.barDockControlRight);
			this.barManager.Form = this;
			this.barManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barButtonItemImagePaste,
            this.barButtonItemImageFavoritesAdd,
            this.barButtonItemImageOpen,
            this.barButtonItemImageReset,
            this.barButtonItemImagePreview,
            this.barButtonItemImageFavoritesOpen});
			this.barManager.MaxItemId = 22;
			// 
			// barDockControlTop
			// 
			this.barDockControlTop.CausesValidation = false;
			this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.barDockControlTop.ForeColor = System.Drawing.Color.Black;
			this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
			this.barDockControlTop.Manager = this.barManager;
			this.barDockControlTop.Size = new System.Drawing.Size(997, 0);
			// 
			// barDockControlBottom
			// 
			this.barDockControlBottom.CausesValidation = false;
			this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.barDockControlBottom.ForeColor = System.Drawing.Color.Black;
			this.barDockControlBottom.Location = new System.Drawing.Point(0, 512);
			this.barDockControlBottom.Manager = this.barManager;
			this.barDockControlBottom.Size = new System.Drawing.Size(997, 0);
			// 
			// barDockControlLeft
			// 
			this.barDockControlLeft.CausesValidation = false;
			this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
			this.barDockControlLeft.ForeColor = System.Drawing.Color.Black;
			this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
			this.barDockControlLeft.Manager = this.barManager;
			this.barDockControlLeft.Size = new System.Drawing.Size(0, 512);
			// 
			// barDockControlRight
			// 
			this.barDockControlRight.CausesValidation = false;
			this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
			this.barDockControlRight.ForeColor = System.Drawing.Color.Black;
			this.barDockControlRight.Location = new System.Drawing.Point(997, 0);
			this.barDockControlRight.Manager = this.barManager;
			this.barDockControlRight.Size = new System.Drawing.Size(0, 512);
			// 
			// barButtonItemImagePaste
			// 
			this.barButtonItemImagePaste.Caption = "Paste Image here";
			this.barButtonItemImagePaste.CategoryGuid = new System.Guid("0399476a-ad4e-415d-bbb8-97162e27fa1a");
			this.barButtonItemImagePaste.Id = 0;
			this.barButtonItemImagePaste.Name = "barButtonItemImagePaste";
			// 
			// barButtonItemImageFavoritesAdd
			// 
			this.barButtonItemImageFavoritesAdd.Caption = "Save Image to Favorites...";
			this.barButtonItemImageFavoritesAdd.CategoryGuid = new System.Guid("0399476a-ad4e-415d-bbb8-97162e27fa1a");
			this.barButtonItemImageFavoritesAdd.Id = 2;
			this.barButtonItemImageFavoritesAdd.Name = "barButtonItemImageFavoritesAdd";
			// 
			// barButtonItemImageOpen
			// 
			this.barButtonItemImageOpen.Caption = "Browse and Insert an Image...";
			this.barButtonItemImageOpen.CategoryGuid = new System.Guid("0399476a-ad4e-415d-bbb8-97162e27fa1a");
			this.barButtonItemImageOpen.Id = 4;
			this.barButtonItemImageOpen.Name = "barButtonItemImageOpen";
			// 
			// barButtonItemImageReset
			// 
			this.barButtonItemImageReset.Caption = "Reset Image";
			this.barButtonItemImageReset.CategoryGuid = new System.Guid("0399476a-ad4e-415d-bbb8-97162e27fa1a");
			this.barButtonItemImageReset.Id = 6;
			this.barButtonItemImageReset.Name = "barButtonItemImageReset";
			// 
			// barButtonItemImagePreview
			// 
			this.barButtonItemImagePreview.Caption = "Preview Image...";
			this.barButtonItemImagePreview.CategoryGuid = new System.Guid("0399476a-ad4e-415d-bbb8-97162e27fa1a");
			this.barButtonItemImagePreview.Id = 35;
			this.barButtonItemImagePreview.Name = "barButtonItemImagePreview";
			// 
			// popupMenuImage
			// 
			this.popupMenuImage.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemImagePreview, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemImagePaste),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemImageFavoritesAdd, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemImageFavoritesOpen),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemImageOpen),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemImageReset, true)});
			this.popupMenuImage.Manager = this.barManager;
			this.popupMenuImage.Name = "popupMenuImage";
			// 
			// barButtonItemFavoritesOpen
			// 
			this.barButtonItemImageFavoritesOpen.Caption = "Insert Image from Favorites...";
			this.barButtonItemImageFavoritesOpen.Id = 21;
			this.barButtonItemImageFavoritesOpen.Name = "barButtonItemImageFavoritesOpen";
			// 
			// StarAppControl
			// 
			this.Controls.Add(this.labelFocusFake);
			this.Controls.Add(this.barDockControlLeft);
			this.Controls.Add(this.barDockControlRight);
			this.Controls.Add(this.barDockControlBottom);
			this.Controls.Add(this.barDockControlTop);
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "StarAppControl";
			this.Size = new System.Drawing.Size(997, 512);
			((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.popupMenuImage)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		public System.Windows.Forms.Label labelFocusFake;
		public DevExpress.XtraBars.BarManager barManager;
		private DevExpress.XtraBars.BarDockControl barDockControlTop;
		private DevExpress.XtraBars.BarDockControl barDockControlBottom;
		private DevExpress.XtraBars.BarDockControl barDockControlLeft;
		private DevExpress.XtraBars.BarDockControl barDockControlRight;
		public DevExpress.XtraBars.BarButtonItem barButtonItemImagePaste;
		public DevExpress.XtraBars.BarButtonItem barButtonItemImageFavoritesAdd;
		public DevExpress.XtraBars.BarButtonItem barButtonItemImageOpen;
		public DevExpress.XtraBars.BarButtonItem barButtonItemImageReset;
		public DevExpress.XtraBars.BarButtonItem barButtonItemImagePreview;
		public DevExpress.XtraBars.PopupMenu popupMenuImage;
		public DevExpress.XtraBars.BarButtonItem barButtonItemImageFavoritesOpen;
	}
}
