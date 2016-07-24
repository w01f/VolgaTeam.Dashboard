using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Asa.Common.Core.Extensions;
using Asa.Common.Core.Objects.RemoteStorage;

namespace Asa.Common.Core.Objects.Images
{
	public class ImageSourceGroup
	{
		private readonly StorageDirectory _root;

		public string Name { get; set; }
		public int Order { get; set; }

		public bool IsDefault => Order < 0;

		public List<ImageSource> Images { get; private set; }

		public EventHandler<EventArgs> OnDataChanged;

		public ImageSourceGroup(StorageDirectory root)
		{
			_root = root;
			Images = new List<ImageSource>();
		}

		public void LoadImages()
		{
			Images.Clear();

			var bigImageFolder = new StorageDirectory(_root.RelativePathParts.Merge(ImageSource.BigImageFolderName));
			if (!bigImageFolder.ExistsLocal()) return;


			foreach (var file in bigImageFolder.GetLocalFiles().Where(file => file.Extension == ImageSource.ImageFileExtension))
			{
				var imageSource = ImageSource.FromFolder(_root, Path.GetFileNameWithoutExtension(file.LocalPath));
				if (imageSource != null)
					Images.Add(imageSource);
			}
		}
	}
}
