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
		public Size ThumbnailSize { get; private set; }

		public void InitThumbnailSize(Size thumbnailSize)
		{
			ThumbnailSize = thumbnailSize;
		}

		protected override void ProcessSlideSizeFolder(StorageDirectory sizeFolder, SlideFormatEnum format)
		{
			var orderFile = Path.Combine(sizeFolder.LocalPath, "thumb_order.txt");
			if (!File.Exists(orderFile)) return;

			var folderNames = File.ReadAllLines(orderFile).Where(line => !String.IsNullOrWhiteSpace(line)).ToList();
			foreach (var folderName in folderNames)
			{
				var slideFolder = new StorageDirectory(sizeFolder.RelativePathParts.Merge(folderName));
				if (slideFolder.ExistsLocal())
				{
					var slideMaster = new SlideMaster(slideFolder)
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
