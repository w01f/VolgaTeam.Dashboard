using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Asa.Common.Core.Configuration;
using Asa.Common.Core.Objects.Images;

namespace Asa.Common.Core.Helpers
{
	public class FavoriteImagesManager
	{
		public List<ImageSource> Images { get; }
		public event EventHandler<EventArgs> CollectionChanged;

		public static FavoriteImagesManager Instance { get; } = new FavoriteImagesManager();

		private FavoriteImagesManager()
		{
			Images = new List<ImageSource>();
			LoadImages();
		}

		protected void OnCollectionChanged()
		{
			CollectionChanged?.Invoke(this, EventArgs.Empty);
		}

		private void LoadImages()
		{
			Images.Clear();
			foreach (var file in Directory.GetFiles(ResourceManager.Instance.FavoriteImagesFolder.LocalPath, String.Format("*{0}", ImageSource.ImageFileExtension)))
			{
				using (var fs = new FileStream(file, FileMode.Open, FileAccess.Read))
				{
					var imageSource = ImageSource.FromImage(Image.FromStream(fs));
					imageSource.Name = Path.GetFileNameWithoutExtension(file);
					imageSource.FileName = file;
					Images.Add(imageSource);
					fs.Close();
				}
			}
		}

		public void DeleteImage(ImageSource image)
		{
			if (image == null) return;
			if (!File.Exists(image.FileName)) return;
			try
			{
				image.Dispose();
				File.Delete(image.FileName);
				LoadImages();
				OnCollectionChanged();
			}
			catch { }

		}

		public void SaveImage(Image image, string fileName)
		{
			if (image == null) return;
			image.Save(Path.Combine(ResourceManager.Instance.FavoriteImagesFolder.LocalPath, String.Format("{0}.png", fileName)));
			LoadImages();
			OnCollectionChanged();
		}

		public void SaveImages(Dictionary<string, Image> images)
		{
			foreach (var image in images)
				image.Value.Save(Path.Combine(ResourceManager.Instance.FavoriteImagesFolder.LocalPath, String.Format("{0}.png", image.Key)));
			LoadImages();
			OnCollectionChanged();
		}
	}
}
