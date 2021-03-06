﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.Images;
using Asa.Common.GUI.Common;
using Asa.Common.GUI.FavoriteImages;
using Asa.Common.GUI.ImageGallery;
using Manina.Windows.Forms;

namespace Asa.Calendar.Controls.PresentationClasses.SlideInfo
{
	public partial class CalendarHeaderSelector : UserControl
	{
		private bool _isLoading;
		private readonly List<ImageSource> _dataSource = new List<ImageSource>();
		private ImageSourceAdaptor _listViewAdaptor;
		private Cursor _dragRowCursor;
		private Point _hitPoint;
		private ImageListView.HitInfo _downHitInfo;
		private ImageListView.HitInfo _menuHitInfo;

		public event EventHandler<EventArgs> SelectionChanged;

		public CalendarHeaderSelector()
		{
			InitializeComponent();
		}

		public ImageSource SelectedImageSource
		{
			get
			{
				return imageListView.SelectedItems.Count > 0 ?
					imageListView.SelectedItems.Select(item => item.Tag as ImageSource).FirstOrDefault() :
					null;
			}
			set
			{
				_isLoading = true;
				imageListView.ClearSelection();
				if (value != null && _dataSource.Contains(value))
				{
					var index = _dataSource.IndexOf(value);
					imageListView.Items[index].Selected = true;
				}
				else if (imageListView.Items.Count > 0)
					imageListView.Items[0].Selected = true;
				_isLoading = false;
			}
		}

		public void LoadData(IEnumerable<ImageSource> dataSource)
		{
			_isLoading = true;
			_dataSource.Clear();
			_dataSource.AddRange(dataSource);
			_listViewAdaptor = new ImageSourceAdaptor(_dataSource);
			imageListView.Items.Clear();
			imageListView.Items.AddRange(_dataSource.Select(ims => new ImageListViewItem(ims.Identifier) { Tag = ims }).ToArray(), _listViewAdaptor);
			_isLoading = false;
		}

		private Cursor GetDragCursor(Image image)
		{
			if (image == null) return Cursors.Default;
			image = ResizeImage(imageListView.ThumbnailSize.Width, image);
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

		private void OnImageListViewSelectionChanged(object sender, EventArgs e)
		{
			if (_isLoading) return;
			SelectionChanged?.Invoke(sender, e);
		}

		private void OnImageListViewItemHover(object sender, ItemHoverEventArgs e)
		{
			toolTip.RemoveAll();
			if (!(e.Item?.Tag is ImageSource sourceItem)) return;
			var toolTipText = Path.GetFileName(sourceItem.FileName);
			toolTip.SetToolTip(imageListView, toolTipText);
		}

		private void OnImageListViewMouseMove(object sender, MouseEventArgs e)
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

		private void imageListView_MouseDown(object sender, MouseEventArgs e)
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

		private void OnImageListViewMouseUp(object sender, MouseEventArgs e)
		{
			_downHitInfo = null;
		}

		private void OnImageListViewGiveFeedback(object sender, GiveFeedbackEventArgs e)
		{
			if (_downHitInfo == null) return;
			e.UseDefaultCursors = false;
			Cursor.Current = _dragRowCursor;
		}

		private void OnMenuOpening(object sender, CancelEventArgs e)
		{
			e.Cancel = !(_menuHitInfo != null && _menuHitInfo.InItemArea && _menuHitInfo.ItemIndex >= 0);
		}

		private void OnMenuItemCopyClick(object sender, EventArgs e)
		{
			var imageSource = imageListView.Items[_menuHitInfo.ItemIndex].Tag as ImageSource;
			if (imageSource == null || !imageSource.ContainsData) return;
			Clipboard.SetText(imageSource.Serialize(), TextDataFormat.Html);
			_menuHitInfo = null;
		}

		private void OnMenuItemFavoritesClick(object sender, EventArgs e)
		{
			var imageSource = imageListView.Items[_menuHitInfo.ItemIndex].Tag as ImageSource;
			if (imageSource == null) return;
			var imageName = imageSource.Name;
			using (var form = new FormAddFavoriteImage(imageSource.BigImage, imageName, FavoriteImagesManager.Instance.Images.Select(i => i.Name.ToLower())))
			{
				form.Text = "Add Image to Favorites";
				form.simpleLabelItemTitle.Text = "Save this Image in your Favorites folder for future<br>presentations";
				if (form.ShowDialog() != DialogResult.OK) return;
				imageName = form.ImageName;
			}
			FavoriteImagesManager.Instance.SaveImage(imageSource.BigImage, imageName);
			PopupMessageHelper.Instance.ShowInformation("Image successfully added to Favorites");
			_menuHitInfo = null;
		}
	}
}
