using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Asa.Common.Core.Extensions;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.Images;
using Manina.Windows.Forms;
using Asa.Common.GUI.Common;
using Asa.Common.GUI.ImageGallery;

namespace Asa.Common.GUI.FavoriteImages
{
	public partial class FavoriteImagesControl : UserControl
	{
		private FavoriteImagesManager _manager;
		private ImageSourceAdaptor _listViewAdaptor;
		private Cursor _dragRowCursor;
		private Point _hitPoint;
		private ImageListView.HitInfo _downHitInfo;
		private ImageListView.HitInfo _menuHitInfo;

		public event EventHandler<EventArgs> OnImageDoubleClick;

		public string ImageTooltip { get; set; }

		public ImageSource SelectedImageSource
		{
			get
			{
				return imageListView.SelectedItems.Count > 0 ?
					imageListView.SelectedItems.Select(item => (ImageSource)item.Tag).FirstOrDefault() :
					null;
			}
		}

		public FavoriteImagesControl()
		{
			InitializeComponent();
		}

		public void Init()
		{
			_manager = FavoriteImagesManager.Instance;
			_manager.CollectionChanged += OnFavoritesCollectionChanged;
			_listViewAdaptor = new ImageSourceAdaptor(_manager.Images);
			LoadImages();
		}

		public void Release()
		{
			_manager.CollectionChanged -= OnFavoritesCollectionChanged;
			imageListView.Items.Clear();
		}

		private void LoadImages()
		{
			imageListView.Items.Clear();
			imageListView.Items.AddRange(_manager.Images.Select(ims => new ImageListViewItem(ims.Identifier) { Tag = ims }).ToArray(), _listViewAdaptor);
		}

		private void OnFavoritesCollectionChanged(object sender, EventArgs e)
		{
			_listViewAdaptor = new ImageSourceAdaptor(_manager.Images);
			LoadImages();
		}

		public void AddToFavorites(Image image, string defaultName)
		{
			using (var form = new FormAddFavoriteImage(image, defaultName, _manager.Images.Select(i => i.Name.ToLower())))
			{
				form.Text = "Add Image to Favorites";
				form.simpleLabelItemTitle.Text = "Save this Image in your Favorites folder for future<br>presentations";
				if (form.ShowDialog() != DialogResult.OK) return;
				_manager.SaveImage(image, form.ImageName);
			}
		}

		private Cursor GetDragCursor(Image image)
		{
			if (image == null) return Cursors.Default;
			image = GraphicExtensions.ResizeImage(imageListView.ThumbnailSize.Width, image);
			var imageBounds = new Rectangle(new Point(0, 0), image.Size);
			var result = new Bitmap(imageBounds.Width, imageBounds.Height);
			var resultGraphics = Graphics.FromImage(result);
			float[][] matrixItems =
			{
				new float[] { 1, 0, 0, 0, 0 },
				new float[] { 0, 1, 0, 0, 0 },
				new float[] { 0, 0, 1, 0, 0 },
				new[] { 0, 0, 0, 0.7f, 0 },
				new float[] { 0, 0, 0, 0, 1 }
			};
			var colorMatrix = new ColorMatrix(matrixItems);
			var imageAttributes = new ImageAttributes();
			imageAttributes.SetColorMatrix(
				colorMatrix,
				ColorMatrixFlag.Default,
				ColorAdjustType.Bitmap);
			resultGraphics.DrawImage(image, imageBounds, imageBounds.X, imageBounds.Y, imageBounds.Width, imageBounds.Height, GraphicsUnit.Pixel, imageAttributes);
			var offset = new Point(imageBounds.Width / 2, imageBounds.Height / 2);
			return GridDragDropHelper.CreateCursor(result, offset);
		}

		private void OnGalleryItemClick(object sender, ItemClickEventArgs e)
		{
			imageListView.ClearSelection();
			e.Item.Selected = true;
		}

		private void OnGalleryItemDoubleClick(object sender, ItemClickEventArgs e)
		{
			OnGalleryItemClick(sender, e);
			OnImageDoubleClick?.Invoke(this, EventArgs.Empty);
		}

		private void OnGalleryMouseDown(object sender, MouseEventArgs e)
		{
			_downHitInfo = null;
			_menuHitInfo = null;
			_hitPoint = new Point(e.X, e.Y);
			imageListView.HitTest(_hitPoint, out var hitInfo);
			if (ModifierKeys != Keys.None)
				return;
			if (!hitInfo.InItemArea) return;
			switch (e.Button)
			{
				case MouseButtons.Left:
					_downHitInfo = hitInfo;
					break;
				case MouseButtons.Right:
					_menuHitInfo = hitInfo;
					contextMenuStrip.Show(MousePosition);
					break;
			}
		}

		private void OnGalleryMouseMove(object sender, MouseEventArgs e)
		{
			imageListView.Focus();
			if (e.Button != MouseButtons.Left || _downHitInfo == null || _downHitInfo.ItemIndex < 0) return;
			if (!(imageListView.Items[_downHitInfo.ItemIndex].Tag is ImageSource sourceItem)) return;
			var dragSize = SystemInformation.DragSize;
			var dragRect = new Rectangle(new Point(_hitPoint.X - dragSize.Width / 2,
				_hitPoint.Y - dragSize.Height / 2), dragSize);
			if (dragRect.Contains(new Point(e.X, e.Y))) return;
			_dragRowCursor = GetDragCursor(sourceItem.BigImage);
			imageListView.DoDragDrop(sourceItem, DragDropEffects.All);
			_downHitInfo = null;
		}

		private void OnGalleryMouseUp(object sender, MouseEventArgs e)
		{
			_downHitInfo = null;
		}

		private void OnGalleryItemHover(object sender, ItemHoverEventArgs e)
		{
			toolTip.RemoveAll();
			if (!(e.Item?.Tag is ImageSource sourceItem)) return;
			var toolTipText = !String.IsNullOrEmpty(ImageTooltip) ? ImageTooltip : Path.GetFileName(sourceItem.FileName);
			toolTip.SetToolTip(imageListView, toolTipText);
		}

		private void OnGalleryGiveFeedback(object sender, GiveFeedbackEventArgs e)
		{
			if (_downHitInfo == null) return;
			e.UseDefaultCursors = false;
			Cursor.Current = _dragRowCursor;
		}

		private void OnMenuStripOpening(object sender, System.ComponentModel.CancelEventArgs e)
		{
			e.Cancel = !(_menuHitInfo != null && _menuHitInfo.InItemArea && _menuHitInfo.ItemIndex >= 0);
		}

		private void OnMenuItemCopyClick(object sender, EventArgs e)
		{
			if (!(imageListView.Items[_menuHitInfo.ItemIndex].Tag is ImageSource imageSource) || !imageSource.ContainsData) return;
			Clipboard.SetText(String.Format("<ImageSource>{0}</ImageSource>", imageSource.Serialize()), TextDataFormat.Html);
			_menuHitInfo = null;
		}

		private void OnMenuItemRenameClick(object sender, EventArgs e)
		{
			if (!(imageListView.Items[_menuHitInfo.ItemIndex].Tag is ImageSource imageSource)) return;
			var image = imageSource.BigImage.Clone() as Image;
			using (var form = new FormAddFavoriteImage(image, null, _manager.Images.Select(i => i.Name.ToLower())))
			{
				form.Text = "Rename Favorite Image";
				form.simpleLabelItemTitle.Text = "Set new name for your Favorite Image";
				if (form.ShowDialog() != DialogResult.OK) return;
				_manager.DeleteImage(imageSource);
				_manager.SaveImage(image, form.ImageName);
			}
			_menuHitInfo = null;
		}

		private void OnMenuItemDeleteClick(object sender, EventArgs e)
		{
			if (!(imageListView.Items[_menuHitInfo.ItemIndex].Tag is ImageSource imageSource)) return;
			if (PopupMessageHelper.Instance.ShowWarningQuestion("Are you sure you want to delete image?") != DialogResult.Yes) return;
			_manager.DeleteImage(imageSource);
			_menuHitInfo = null;
		}
	}
}
