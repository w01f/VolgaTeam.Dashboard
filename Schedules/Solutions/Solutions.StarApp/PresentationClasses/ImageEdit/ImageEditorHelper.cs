using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.Images;
using Asa.Common.GUI.FavoriteImages;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

namespace Asa.Solutions.StarApp.PresentationClasses.ImageEdit
{
	public abstract class ImageEditorHelper
	{
		protected PictureEdit _activeImageEditor;

		protected abstract PopupMenu Menu { get; }

		protected abstract BarButtonItem MenuItemPreview { get; }
		protected abstract BarButtonItem MenuItemPaste { get; }
		protected abstract BarButtonItem MenuItemFavoritesAdd { get; }
		protected abstract BarButtonItem MenuItemFavoritesOpen { get; }
		protected abstract BarButtonItem MenuItemBrowse { get; }
		protected abstract BarButtonItem MenuItemReset { get; }

		protected void InitMenu()
		{
			Menu.CloseUp += OnMenuClose;
			MenuItemPreview.ItemClick += OnPreviewItemClick;
			MenuItemPaste.ItemClick += OnPasteItemClick;
			MenuItemFavoritesAdd.ItemClick += OnFavoritesAddItemClick;
			MenuItemFavoritesOpen.ItemClick += OnFavoritesOpenItemClick;
			MenuItemBrowse.ItemClick += OnBrowseItemClick;
			MenuItemReset.ItemClick += OnResetItemClick;
		}

		public void AssignImageEditor(PictureEdit imageEditor)
		{
			imageEditor.Tag = new ImageEditorSettings { DefaultImage = imageEditor.Image };
			imageEditor.AllowDrop = true;
			imageEditor.Properties.SizeMode = PictureSizeMode.Squeeze;
			imageEditor.MouseClick += OnImageMouseClik;
			imageEditor.DragOver += OnImageDragOver;
			imageEditor.DragDrop += OnImageDragDrop;
			imageEditor.DoubleClick += OnImageDoubleClick;
		}

		public void AssignImageEditors(IList<PictureEdit> imageEditors)
		{
			foreach (var imageEditor in imageEditors)
				AssignImageEditor(imageEditor);
		}

		private void OnImageDoubleClick(Object sender, EventArgs e)
		{
			using (var form = new FormPreviewImage(((PictureEdit)sender).Image))
			{
				form.ShowDialog();
			}
		}

		private void OnImageMouseClik(object sender, MouseEventArgs e)
		{
			if (e.Button != MouseButtons.Right) return;
			_activeImageEditor = (PictureEdit)sender;
			var imageSettings = (ImageEditorSettings)_activeImageEditor.Tag;

			MenuItemPaste.Enabled = ClipboardHelper.GetImageFormClipboard() != null || ClipboardHelper.GetPngFormClipboard() != null || Clipboard.ContainsText(TextDataFormat.Html);
			MenuItemFavoritesAdd.Enabled = imageSettings.CurrentImage != null;
			MenuItemFavoritesOpen.Enabled = FavoriteImagesManager.Instance.Images.Any();
			MenuItemReset.Enabled = imageSettings.CurrentImage != null;

			Menu.ShowPopup(Cursor.Position);
		}

		private void OnImageDragDrop(object sender, DragEventArgs e)
		{
			var imageEditor = (PictureEdit)sender;
			if (e.Data != null && e.Data.GetDataPresent(DataFormats.FileDrop, true) &&
				e.Data.GetData(DataFormats.FileDrop, true) is String[])
			{
				var imageFilePath = (e.Data.GetData(DataFormats.FileDrop) as String[] ?? new string[] { }).FirstOrDefault();
				if (imageFilePath == null) return;
				var tempFile = Path.GetTempFileName();
				File.Copy(imageFilePath, tempFile, true);
				imageEditor.Image =
					((ImageEditorSettings)imageEditor.Tag).CurrentImage =
					Image.FromFile(tempFile);
				((ImageEditorSettings)imageEditor.Tag).CurrentImageName =
					Path.GetFileNameWithoutExtension(imageFilePath);
			}
		}

		private void OnImageDragOver(object sender, DragEventArgs e)
		{
			if (e.Data != null && e.Data.GetDataPresent(DataFormats.FileDrop, true) && e.Data.GetData(DataFormats.FileDrop, true) is String[])
				e.Effect = DragDropEffects.Copy;
			else
				e.Effect = DragDropEffects.None;
		}

		private void OnMenuClose(object sender, EventArgs e)
		{
			_activeImageEditor = null;
		}

		private void OnPreviewItemClick(object sender, ItemClickEventArgs e)
		{
			using (var form = new FormPreviewImage(_activeImageEditor.Image))
			{
				form.ShowDialog();
			}
		}

		private void OnPasteItemClick(object sender, ItemClickEventArgs e)
		{
			ImageSource imageSource = null;
			var clipboardImage = ClipboardHelper.GetPngFormClipboard() ?? ClipboardHelper.GetImageFormClipboard();
			if (clipboardImage != null)
				imageSource = ImageSource.FromImage(clipboardImage);
			else if (Clipboard.ContainsText(TextDataFormat.Html))
			{
				var textContent = Clipboard.GetText(TextDataFormat.Html);
				try
				{
					imageSource = ImageSource.FromString(textContent);
				}
				catch
				{
				}
			}

			if (imageSource != null)
			{
				_activeImageEditor.Image =
					((ImageEditorSettings)_activeImageEditor.Tag).CurrentImage =
					imageSource.OriginalImage ?? imageSource.BigImage;
				((ImageEditorSettings)_activeImageEditor.Tag).CurrentImageName = imageSource.Name;
			}
		}

		private void OnFavoritesAddItemClick(object sender, ItemClickEventArgs e)
		{
			var imageSettings = (ImageEditorSettings)_activeImageEditor.Tag;
			var imageName = imageSettings.CurrentImageName ?? "Clipart Image";
			using (var form = new FormAddFavoriteImage(imageSettings.CurrentImage, imageName, FavoriteImagesManager.Instance.Images.Select(i => i.Name.ToLower())))
			{
				form.Text = "Add Image to Favorites";
				form.simpleLabelItemTitle.Text = "Save this Image in your Favorites folder for future use";
				if (form.ShowDialog() != DialogResult.OK) return;
				imageName = form.ImageName;
			}
			FavoriteImagesManager.Instance.SaveImage(imageSettings.CurrentImage, imageName);
			PopupMessageHelper.Instance.ShowInformation("Image successfully added to Favorites");
		}

		private void OnFavoritesOpenItemClick(object sender, ItemClickEventArgs e)
		{
			using (var form = new FormFavoritesOpen())
			{
				if (form.ShowDialog() == DialogResult.OK && form.SelectedImageSource != null)
				{
					var imageSource = form.SelectedImageSource;
					_activeImageEditor.Image =
						((ImageEditorSettings)_activeImageEditor.Tag).CurrentImage =
						imageSource.OriginalImage ?? imageSource.BigImage;
					((ImageEditorSettings)_activeImageEditor.Tag).CurrentImageName = imageSource.Name;
				}
			}
		}

		private void OnBrowseItemClick(object sender, ItemClickEventArgs e)
		{
			using (var openFileDialog = new OpenFileDialog())
			{
				openFileDialog.DefaultExt = "png";
				openFileDialog.Filter = "Png Files|*.png|Bitmap|*.bmp|Jpeg Files|*.jpg, *.jpeg";
				openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
				if (openFileDialog.ShowDialog() == DialogResult.OK)
				{
					var tempFile = Path.GetTempFileName();
					File.Copy(openFileDialog.FileName, tempFile, true);
					_activeImageEditor.Image =
						((ImageEditorSettings)_activeImageEditor.Tag).CurrentImage =
						Image.FromFile(tempFile);
					((ImageEditorSettings)_activeImageEditor.Tag).CurrentImageName =
						Path.GetFileNameWithoutExtension(openFileDialog.FileName);
				}
			}
		}

		private void OnResetItemClick(object sender, ItemClickEventArgs e)
		{
			var imageSettings = (ImageEditorSettings)_activeImageEditor.Tag;
			imageSettings.ResetCurrent();
			_activeImageEditor.Image = imageSettings.DefaultImage;
		}
	}
}
