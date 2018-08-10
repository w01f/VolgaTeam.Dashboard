using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Business.Solutions.Common.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.Images;
using Asa.Common.GUI.FavoriteImages;
using Asa.Common.GUI.ToolForms;
using Asa.Solutions.Common.Helpers;
using DevExpress.XtraBars;

namespace Asa.Solutions.Common.PresentationClasses.ClipartEdit
{
	public partial class ClipartEditContainer : UserControl
	{
		private ClipartObject _defaultClipartObject;
		private ClipartObject _currentClipartObject;
		private IScheduleResourceHolder _resourceHolder;

		public event EventHandler<EventArgs> EditValueChanged;

		public ClipartEditContainer()
		{
			InitializeComponent();
			AllowDrop = true;
		}

		public void Init(ClipartObject defaultClipartObject, ClipartConfiguration configuration, IScheduleResourceHolder resourceHolder)
		{
			_defaultClipartObject = defaultClipartObject;
			_resourceHolder = resourceHolder;
			ResetToDefault();

			pictureEdit.Properties.PictureAlignment = configuration.Alignment;
			pictureEdit.Properties.SizeMode = configuration.SizeMode;
		}

		public void LoadData(ClipartObject clipartObject)
		{
			if (_currentClipartObject != null && _currentClipartObject != _defaultClipartObject && _currentClipartObject.Type == ClipartObjectType.Video)
			{
				var videoCipartObject = (VideoClipartObject)_currentClipartObject;
				var videoResource = _resourceHolder.ResourceContainer.Items.FirstOrDefault(item => item.Id == videoCipartObject.ResourceId);
				if (videoResource != null)
					_resourceHolder.ResourceContainer.RemoveResource(videoResource);
			}
			_currentClipartObject = clipartObject ?? _defaultClipartObject;
			pictureEdit.Tag = _currentClipartObject;
			switch (_currentClipartObject.Type)
			{
				case ClipartObjectType.Image:
					var imageObject = (ImageClipartObject)_currentClipartObject;
					pictureEdit.Image = imageObject.Image;
					break;
				case ClipartObjectType.Video:
					var videoClipartObject = (VideoClipartObject)_currentClipartObject;
					pictureEdit.Image = videoClipartObject.Thumbnail;
					break;
				case ClipartObjectType.YouTube:
					var youtubeObject = (YouTubeClipartObject)_currentClipartObject;
					pictureEdit.Image = youtubeObject.Thumbnail;
					break;
				default:
					throw new ArgumentOutOfRangeException("Undefined clipart type found");
			}
		}

		public ClipartObject GetActiveClipartObject()
		{
			if (_currentClipartObject == _defaultClipartObject)
				return null;
			return _currentClipartObject;
		}

		private void ResetToDefault()
		{
			LoadData(_defaultClipartObject);
		}

		private VideoClipartObject GenererateVideoClipart(string filePath)
		{
			VideoResourceItem videoResourceItem = null;
			FormProgress.ShowProgress("Saving Video Content...", () =>
			{
				AsyncHelper.RunSync(async () =>
				{
					await Task.Run(() =>
					{
						videoResourceItem = VideoResourceHelper.AddVideoResource(filePath, _resourceHolder.ResourceContainer);
					});
				});
			}, false);
			if (videoResourceItem != null)
			{
				var clipartObject = VideoClipartObject.FromVideoResource(videoResourceItem);

				clipartObject.SourceFilePath = videoResourceItem.GetSourceFile();

				var thumbnailFiles = videoResourceItem.GetThumbnailFies();
				if (thumbnailFiles.Any())
					clipartObject.Thumbnail = Image.FromFile(thumbnailFiles.First());

				return clipartObject;
			}
			return null;
		}

		#region Common Event Handlers
		private void OnImageDoubleClick(Object sender, EventArgs e)
		{
			switch (_currentClipartObject.Type)
			{
				case ClipartObjectType.Image:
					var imageObject = (ImageClipartObject)_currentClipartObject;
					using (var form = new FormPreviewImage(imageObject))
					{
						form.ShowDialog();
					}
					break;
				case ClipartObjectType.YouTube:
					var youtubeObject = (YouTubeClipartObject)_currentClipartObject;
					using (var form = new FormPreviewYouTube(youtubeObject))
					{
						form.ShowDialog();
					}
					break;
				case ClipartObjectType.Video:
					var videoObject = (VideoClipartObject)_currentClipartObject;
					try
					{
						Process.Start(videoObject.SourceFilePath);
					}
					catch { }
					break;
				default:
					throw new ArgumentOutOfRangeException("Undefined clipart type found");
			}
		}

		private void OnImageMouseClik(object sender, MouseEventArgs e)
		{
			if (e.Button != MouseButtons.Right) return;

			barButtonItemImagePaste.Enabled = ClipboardHelper.GetImageFormClipboard() != null || ClipboardHelper.GetPngFormClipboard() != null || Clipboard.ContainsText(TextDataFormat.Html);
			barButtonItemImageFavoritesAdd.Enabled = _currentClipartObject != _defaultClipartObject;
			barButtonItemImageFavoritesOpen.Enabled = FavoriteImagesManager.Instance.Images.Any();
			barButtonItemReset.Enabled = _currentClipartObject != _defaultClipartObject;

			popupMenuImage.ShowPopup(Cursor.Position);
		}

		private void OnImageDragDrop(object sender, DragEventArgs e)
		{
			if (e.Data != null && e.Data.GetDataPresent(DataFormats.FileDrop, true) &&
				e.Data.GetData(DataFormats.FileDrop, true) is String[])
			{
				var filePath = (e.Data.GetData(DataFormats.FileDrop) as String[] ?? new string[] { }).FirstOrDefault();
				if (filePath == null) return;

				ClipartObject clipartObject = null;
				if (FileFormatHelper.IsImageFile(filePath))
					clipartObject = ImageClipartObject.FromFile(filePath);
				else if (FileFormatHelper.IsVideoFile(filePath))
					clipartObject = GenererateVideoClipart(filePath);
				if (clipartObject != null)
				{
					LoadData(clipartObject);
					EditValueChanged?.Invoke(sender, e);
				}
			}
		}

		private void OnImageDragOver(object sender, DragEventArgs e)
		{
			if (e.Data != null && e.Data.GetDataPresent(DataFormats.FileDrop, true) && e.Data.GetData(DataFormats.FileDrop, true) is String[])
				e.Effect = DragDropEffects.Copy;
			else
				e.Effect = DragDropEffects.None;
		}

		private void OnPreviewItemClick(object sender, ItemClickEventArgs e)
		{
			OnImageDoubleClick(sender, e);
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
				LoadData(ImageClipartObject.FromImageSource(imageSource));
				EditValueChanged?.Invoke(sender, e);
			}
		}

		private void OnFavoritesAddItemClick(object sender, ItemClickEventArgs e)
		{
			var imageName = _currentClipartObject.Name ?? "Clipart Image";

			Image favoritesImage;
			switch (_currentClipartObject.Type)
			{
				case ClipartObjectType.Image:
					var imageObject = (ImageClipartObject)_currentClipartObject;
					favoritesImage = imageObject.Image;
					break;
				case ClipartObjectType.Video:
					var videoClipartObject = (VideoClipartObject)_currentClipartObject;
					favoritesImage = videoClipartObject.Thumbnail;
					break;
				case ClipartObjectType.YouTube:
					var youtubeObject = (YouTubeClipartObject)_currentClipartObject;
					favoritesImage = youtubeObject.Thumbnail;
					break;
				default:
					throw new ArgumentOutOfRangeException("Undefined clipart type found");
			}

			using (var form = new FormAddFavoriteImage(favoritesImage, imageName, FavoriteImagesManager.Instance.Images.Select(i => i.Name.ToLower())))
			{
				form.Text = "Add Image to Favorites";
				form.simpleLabelItemTitle.Text = "Save this Image in your Favorites folder for future use";
				if (form.ShowDialog() != DialogResult.OK) return;
				imageName = form.ImageName;
			}

			FavoriteImagesManager.Instance.SaveImage(favoritesImage, imageName);
			PopupMessageHelper.Instance.ShowInformation("Image successfully added to Favorites");
		}

		private void OnFavoritesOpenItemClick(object sender, ItemClickEventArgs e)
		{
			using (var form = new FormFavoritesOpen())
			{
				if (form.ShowDialog() == DialogResult.OK && form.SelectedImageSource != null)
				{
					var imageSource = form.SelectedImageSource;
					LoadData(ImageClipartObject.FromImageSource(imageSource));
					EditValueChanged?.Invoke(sender, e);
				}
			}
		}

		private void OnInsertFileItemClick(object sender, ItemClickEventArgs e)
		{
			using (var openFileDialog = new OpenFileDialog())
			{
				openFileDialog.Title = "Inser Image or Video file";
				openFileDialog.DefaultExt = "*.png;*.bmp;*.jpg;*.jpeg;*.mp4;*.avi;*.wmv";
				openFileDialog.Filter = "Image or Video files|*.png;*.bmp;*.jpg;*.jpeg;*.mp4;*.avi;*.wmv";
				openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
				if (openFileDialog.ShowDialog() == DialogResult.OK)
				{
					var filePath = openFileDialog.FileName;
					ClipartObject clipartObject = null;
					if (FileFormatHelper.IsImageFile(filePath))
						clipartObject = ImageClipartObject.FromFile(filePath);
					else if (FileFormatHelper.IsVideoFile(filePath))
						clipartObject = GenererateVideoClipart(filePath);
					if (clipartObject != null)
					{
						LoadData(clipartObject);
						EditValueChanged?.Invoke(sender, e);
					}
				}
			}
		}

		private void OnInsertYouTubeItemClick(object sender, ItemClickEventArgs e)
		{
			using (var form = new FormYouTubeInsert())
			{
				if (form.ShowDialog() == DialogResult.OK)
				{
					LoadData(form.YouTubeInfo);
					EditValueChanged?.Invoke(sender, e);
				}
			}
		}

		private void OnResetItemClick(object sender, ItemClickEventArgs e)
		{
			ResetToDefault();
			EditValueChanged?.Invoke(sender, e);
		}
		#endregion
	}
}
