namespace Asa.Solutions.Common.PresentationClasses.ClipartEdit
{
	partial class ClipartEditContainer
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
			this.popupMenuImage = new DevExpress.XtraBars.PopupMenu();
			this.barButtonItemPreview = new DevExpress.XtraBars.BarButtonItem();
			this.barButtonItemImagePaste = new DevExpress.XtraBars.BarButtonItem();
			this.barButtonItemImageFavoritesAdd = new DevExpress.XtraBars.BarButtonItem();
			this.barButtonItemImageFavoritesOpen = new DevExpress.XtraBars.BarButtonItem();
			this.barButtonItemInsertFile = new DevExpress.XtraBars.BarButtonItem();
			this.barButtonItemInsertYouTube = new DevExpress.XtraBars.BarButtonItem();
			this.barButtonItemReset = new DevExpress.XtraBars.BarButtonItem();
			this.barManager = new DevExpress.XtraBars.BarManager();
			this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
			this.pictureEdit = new DevExpress.XtraEditors.PictureEdit();
			this.barButtonItemImageOpen = new DevExpress.XtraBars.BarButtonItem();
			((System.ComponentModel.ISupportInitialize)(this.popupMenuImage)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureEdit.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// popupMenuImage
			// 
			this.popupMenuImage.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemPreview, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemImagePaste, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemImageFavoritesAdd, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemImageFavoritesOpen),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemInsertFile, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemInsertYouTube),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemReset, true)});
			this.popupMenuImage.Manager = this.barManager;
			this.popupMenuImage.Name = "popupMenuImage";
			// 
			// barButtonItemPreview
			// 
			this.barButtonItemPreview.Caption = "Preview...";
			this.barButtonItemPreview.CategoryGuid = new System.Guid("0399476a-ad4e-415d-bbb8-97162e27fa1a");
			this.barButtonItemPreview.Id = 35;
			this.barButtonItemPreview.Name = "barButtonItemPreview";
			this.barButtonItemPreview.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnPreviewItemClick);
			// 
			// barButtonItemImagePaste
			// 
			this.barButtonItemImagePaste.Caption = "Paste Image here";
			this.barButtonItemImagePaste.CategoryGuid = new System.Guid("0399476a-ad4e-415d-bbb8-97162e27fa1a");
			this.barButtonItemImagePaste.Id = 0;
			this.barButtonItemImagePaste.Name = "barButtonItemImagePaste";
			this.barButtonItemImagePaste.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnPasteItemClick);
			// 
			// barButtonItemImageFavoritesAdd
			// 
			this.barButtonItemImageFavoritesAdd.Caption = "Save Image to Favorites...";
			this.barButtonItemImageFavoritesAdd.CategoryGuid = new System.Guid("0399476a-ad4e-415d-bbb8-97162e27fa1a");
			this.barButtonItemImageFavoritesAdd.Id = 2;
			this.barButtonItemImageFavoritesAdd.Name = "barButtonItemImageFavoritesAdd";
			this.barButtonItemImageFavoritesAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnFavoritesAddItemClick);
			// 
			// barButtonItemImageFavoritesOpen
			// 
			this.barButtonItemImageFavoritesOpen.Caption = "Insert Image from Favorites...";
			this.barButtonItemImageFavoritesOpen.Id = 21;
			this.barButtonItemImageFavoritesOpen.Name = "barButtonItemImageFavoritesOpen";
			this.barButtonItemImageFavoritesOpen.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnFavoritesOpenItemClick);
			// 
			// barButtonItemInsertFile
			// 
			this.barButtonItemInsertFile.Caption = "Insert Image or Video file...";
			this.barButtonItemInsertFile.CategoryGuid = new System.Guid("0399476a-ad4e-415d-bbb8-97162e27fa1a");
			this.barButtonItemInsertFile.Id = 4;
			this.barButtonItemInsertFile.Name = "barButtonItemInsertFile";
			this.barButtonItemInsertFile.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnInsertFileItemClick);
			// 
			// barButtonItemInsertYouTube
			// 
			this.barButtonItemInsertYouTube.Caption = "Insert YouTube Video...";
			this.barButtonItemInsertYouTube.Id = 22;
			this.barButtonItemInsertYouTube.Name = "barButtonItemInsertYouTube";
			this.barButtonItemInsertYouTube.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnInsertYouTubeItemClick);
			// 
			// barButtonItemReset
			// 
			this.barButtonItemReset.Caption = "Reset to default";
			this.barButtonItemReset.CategoryGuid = new System.Guid("0399476a-ad4e-415d-bbb8-97162e27fa1a");
			this.barButtonItemReset.Id = 6;
			this.barButtonItemReset.Name = "barButtonItemReset";
			this.barButtonItemReset.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnResetItemClick);
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
            this.barButtonItemInsertFile,
            this.barButtonItemReset,
            this.barButtonItemPreview,
            this.barButtonItemImageFavoritesOpen,
            this.barButtonItemInsertYouTube});
			this.barManager.MaxItemId = 23;
			// 
			// barDockControlTop
			// 
			this.barDockControlTop.CausesValidation = false;
			this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.barDockControlTop.ForeColor = System.Drawing.Color.Black;
			this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
			this.barDockControlTop.Manager = this.barManager;
			this.barDockControlTop.Size = new System.Drawing.Size(355, 0);
			// 
			// barDockControlBottom
			// 
			this.barDockControlBottom.CausesValidation = false;
			this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.barDockControlBottom.ForeColor = System.Drawing.Color.Black;
			this.barDockControlBottom.Location = new System.Drawing.Point(0, 379);
			this.barDockControlBottom.Manager = this.barManager;
			this.barDockControlBottom.Size = new System.Drawing.Size(355, 0);
			// 
			// barDockControlLeft
			// 
			this.barDockControlLeft.CausesValidation = false;
			this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
			this.barDockControlLeft.ForeColor = System.Drawing.Color.Black;
			this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
			this.barDockControlLeft.Manager = this.barManager;
			this.barDockControlLeft.Size = new System.Drawing.Size(0, 379);
			// 
			// barDockControlRight
			// 
			this.barDockControlRight.CausesValidation = false;
			this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
			this.barDockControlRight.ForeColor = System.Drawing.Color.Black;
			this.barDockControlRight.Location = new System.Drawing.Point(355, 0);
			this.barDockControlRight.Manager = this.barManager;
			this.barDockControlRight.Size = new System.Drawing.Size(0, 379);
			// 
			// pictureEdit
			// 
			this.pictureEdit.AllowDrop = true;
			this.pictureEdit.Cursor = System.Windows.Forms.Cursors.Default;
			this.pictureEdit.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pictureEdit.Location = new System.Drawing.Point(0, 0);
			this.pictureEdit.Name = "pictureEdit";
			this.pictureEdit.Properties.AllowFocused = false;
			this.pictureEdit.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.pictureEdit.Properties.Appearance.Options.UseBackColor = true;
			this.pictureEdit.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.pictureEdit.Properties.NullText = " ";
			this.pictureEdit.Properties.PictureAlignment = System.Drawing.ContentAlignment.TopLeft;
			this.pictureEdit.Properties.ReadOnly = true;
			this.pictureEdit.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
			this.pictureEdit.Properties.ShowMenu = false;
			this.pictureEdit.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
			this.pictureEdit.Properties.ZoomAccelerationFactor = 1D;
			this.pictureEdit.Size = new System.Drawing.Size(355, 379);
			this.pictureEdit.TabIndex = 4;
			this.pictureEdit.DragDrop += new System.Windows.Forms.DragEventHandler(this.OnImageDragDrop);
			this.pictureEdit.DragOver += new System.Windows.Forms.DragEventHandler(this.OnImageDragOver);
			this.pictureEdit.DoubleClick += new System.EventHandler(this.OnImageDoubleClick);
			this.pictureEdit.MouseClick += new System.Windows.Forms.MouseEventHandler(this.OnImageMouseClik);
			// 
			// barButtonItemImageOpen
			// 
			this.barButtonItemImageOpen.Caption = "Insert Image or Video file...";
			this.barButtonItemImageOpen.CategoryGuid = new System.Guid("0399476a-ad4e-415d-bbb8-97162e27fa1a");
			this.barButtonItemImageOpen.Id = 4;
			this.barButtonItemImageOpen.Name = "barButtonItemImageOpen";
			this.barButtonItemImageOpen.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnInsertFileItemClick);
			// 
			// ClipartEditContainer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.pictureEdit);
			this.Controls.Add(this.barDockControlLeft);
			this.Controls.Add(this.barDockControlRight);
			this.Controls.Add(this.barDockControlBottom);
			this.Controls.Add(this.barDockControlTop);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "ClipartEditContainer";
			this.Size = new System.Drawing.Size(355, 379);
			((System.ComponentModel.ISupportInitialize)(this.popupMenuImage)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureEdit.Properties)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		public DevExpress.XtraBars.PopupMenu popupMenuImage;
		public DevExpress.XtraBars.BarButtonItem barButtonItemPreview;
		public DevExpress.XtraBars.BarButtonItem barButtonItemImagePaste;
		public DevExpress.XtraBars.BarButtonItem barButtonItemImageFavoritesAdd;
		public DevExpress.XtraBars.BarButtonItem barButtonItemImageFavoritesOpen;
		public DevExpress.XtraBars.BarButtonItem barButtonItemInsertFile;
		public DevExpress.XtraBars.BarButtonItem barButtonItemReset;
		public DevExpress.XtraBars.BarManager barManager;
		private DevExpress.XtraBars.BarDockControl barDockControlTop;
		private DevExpress.XtraBars.BarDockControl barDockControlBottom;
		private DevExpress.XtraBars.BarDockControl barDockControlLeft;
		private DevExpress.XtraBars.BarDockControl barDockControlRight;
		private DevExpress.XtraEditors.PictureEdit pictureEdit;
		private DevExpress.XtraBars.BarButtonItem barButtonItemInsertYouTube;
		public DevExpress.XtraBars.BarButtonItem barButtonItemImageOpen;
	}
}
