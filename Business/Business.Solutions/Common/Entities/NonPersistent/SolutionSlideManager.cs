using System;
using System.Drawing;
using System.IO;
using System.Linq;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Extensions;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.RemoteStorage;
using Asa.Common.Core.Objects.Slides;

namespace Asa.Business.Solutions.Common.Entities.NonPersistent
{
	public class SolutionSlideManager : SlideManager
	{
		private StorageDirectory _slideTemplatesDirectory;
		public Size ThumbnailSize { get; private set; }

		public void InitThumbnailSize(Size thumbnailSize)
		{
			ThumbnailSize = thumbnailSize;
		}

		public void LoadSlides(StorageDirectory slideContentsDirectory, StorageDirectory slideTemplatesDirectory)
		{
			_slideTemplatesDirectory = slideTemplatesDirectory;
			base.LoadSlides(slideContentsDirectory);
		}

		protected override void ProcessSlideSizeFolder(StorageDirectory sizeFolder, SlideFormatEnum format)
		{
			var orderFiles = new[]
			{
				Path.Combine(sizeFolder.LocalPath, "thumb_order.txt"),
				Path.Combine(sizeFolder.LocalPath, "slide_order.txt")
			};

			var orderFile = orderFiles.FirstOrDefault(File.Exists);
			if (orderFile == null) return;

			var folderNames = File.ReadAllLines(orderFile).Where(line => !String.IsNullOrWhiteSpace(line)).ToList();
			foreach (var folderName in folderNames)
			{
				var slideContentsFolder = new StorageDirectory(sizeFolder.RelativePathParts.Merge(folderName));
				var slideTemplatesFolder = new StorageDirectory(_slideTemplatesDirectory.RelativePathParts.Merge(
					new[] { sizeFolder.RelativePathParts.Last(), folderName }));
				if (slideContentsFolder.ExistsLocal())
				{
					var slideMaster = new SlideMaster(slideContentsFolder, slideTemplatesFolder)
					{
						Group = "Default",
						Format = format
					};
					slideMaster.Load();
					Slides.Add(slideMaster);
				}
			}
		}
	}
}
