using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Layout;
using DevExpress.XtraGrid.Views.Layout.ViewInfo;
using NewBizWiz.CommonGUI.Common;
using NewBizWiz.Core.Common;

namespace NewBizWiz.CommonGUI.FavoriteImages
{
	public partial class FavoriteImagesControl : UserControl
	{
		private FavoriteImagesManager _manager;
		private Cursor _dragRowCursor;
		private LayoutViewHitInfo _downHitInfo;
		private LayoutViewHitInfo _menuHitInfo;

		public string ImageTooltip { get; set; }

		public FavoriteImagesControl()
		{
			InitializeComponent();
		}

		public void Init()
		{
			_manager = FavoriteImagesManager.Instance;
			_manager.CollectionChanged += (o, e) =>
			{
				gridControlLogoGallery.DataSource = _manager.Images;
				layoutViewLogoGallery.Refresh();
			};
			gridControlLogoGallery.DataSource = _manager.Images;
		}

		public void AddToFavorites(Image image, string defaultName)
		{
			using (var form = new FormAddFavoriteImage(image, defaultName, _manager.Images.Select(i => i.Name.ToLower())))
			{
				form.Text = "Add Image to Favorites";
				form.laTitle.Text = "Save this Image in your Favorites folder for future presentations";
				if (form.ShowDialog() != DialogResult.OK) return;
				_manager.SaveImage(image, form.ImageName);
			}
		}

		private Cursor GetDragCursor(Image image)
		{
			if (image == null) return Cursors.Default;
			var view = layoutViewLogoGallery;
			image = ResizeImage(view.CardMinSize.Width, image);
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

		private Image ResizeImage(int newSize, Image originalImage)
		{
			if (originalImage.Width <= newSize)
				newSize = originalImage.Width;

			var newHeight = originalImage.Height * newSize / originalImage.Width;

			if (newHeight > newSize)
			{
				newSize = originalImage.Width * newSize / originalImage.Height;
				newHeight = newSize;
			}
			return originalImage.GetThumbnailImage(newSize, newHeight, null, IntPtr.Zero);
		}

		private void layoutViewLogoGallery_MouseDown(object sender, MouseEventArgs e)
		{
			var view = sender as LayoutView;
			_downHitInfo = null;
			if (view == null) return;
			var hitInfo = view.CalcHitInfo(new Point(e.X, e.Y));
			if (ModifierKeys != Keys.None)
				return;
			if (!hitInfo.InCard) return;
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

		private void layoutViewLogoGallery_MouseMove(object sender, MouseEventArgs e)
		{
			var view = sender as LayoutView;
			if (view == null) return;
			view.Focus();
			if (e.Button != MouseButtons.Left || _downHitInfo == null) return;
			var sourceItem = view.GetRow(_downHitInfo.RowHandle) as ImageSource;
			if (sourceItem == null) return;
			var dragSize = SystemInformation.DragSize;
			var dragRect = new Rectangle(new Point(_downHitInfo.HitPoint.X - dragSize.Width / 2,
				_downHitInfo.HitPoint.Y - dragSize.Height / 2), dragSize);
			if (dragRect.Contains(new Point(e.X, e.Y))) return;
			_dragRowCursor = GetDragCursor(sourceItem.BigImage);
			view.GridControl.DoDragDrop(sourceItem, DragDropEffects.All);
			_downHitInfo = null;
		}

		private void layoutViewLogoGallery_MouseUp(object sender, MouseEventArgs e)
		{
			_downHitInfo = null;
		}

		private void gridControlLogoGallery_GiveFeedback(object sender, GiveFeedbackEventArgs e)
		{
			if (_downHitInfo == null) return;
			e.UseDefaultCursors = false;
			Cursor.Current = _dragRowCursor;
		}

		private void toolStripMenuItemCopy_Click(object sender, EventArgs e)
		{
			var imageSource = layoutViewLogoGallery.GetRow(_menuHitInfo.RowHandle) as ImageSource;
			if (imageSource == null || !imageSource.ContainsData) return;
			Clipboard.SetText(String.Format("<ImageSource>{0}</ImageSource>", imageSource.Serialize()), TextDataFormat.Html);
		}

		private void toolStripMenuItemRename_Click(object sender, EventArgs e)
		{
			var imageSource = layoutViewLogoGallery.GetRow(_menuHitInfo.RowHandle) as ImageSource;
			if (imageSource == null) return;
			var image = imageSource.BigImage.Clone() as Image;
			using (var form = new FormAddFavoriteImage(image, null, _manager.Images.Select(i => i.Name.ToLower())))
			{
				form.Text = "Rename Favorite Image";
				form.laTitle.Text = "Set new name for your Favorite Image";
				if (form.ShowDialog() != DialogResult.OK) return;
				_manager.DeleteImage(imageSource);
				_manager.SaveImage(image, form.ImageName);
			}
			_menuHitInfo = null;
		}

		private void toolStripMenuItemDelete_Click(object sender, EventArgs e)
		{
			var imageSource = layoutViewLogoGallery.GetRow(_menuHitInfo.RowHandle) as ImageSource;
			if (imageSource == null) return;
			if (Utilities.Instance.ShowWarningQuestion("Are you sure you want to delete image?") != DialogResult.Yes) return;
			_manager.DeleteImage(imageSource);
			_menuHitInfo = null;
		}

		private void toolTipController_GetActiveObjectInfo(object sender, ToolTipControllerGetActiveObjectInfoEventArgs e)
		{
			var view = gridControlLogoGallery.GetViewAt(e.ControlMousePosition) as LayoutView;
			if (view == null) return;
			var hi = view.CalcHitInfo(e.ControlMousePosition);
			if (!hi.InFieldValue) return;
			var imageSource = view.GetRow(hi.RowHandle) as ImageSource;
			e.Info = new ToolTipControlInfo(new CellToolTipInfo(hi.RowHandle, hi.Column, "cell"), !String.IsNullOrEmpty(ImageTooltip) ? ImageTooltip : Path.GetFileName(imageSource.FileName));
		}
	}
}
