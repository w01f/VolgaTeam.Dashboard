using System.Collections.Generic;
using System.IO;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Objects.RemoteStorage;
using Asa.Common.Core.Objects.Slides;

namespace Asa.Common.Core.Helpers
{
	public class SlideManager
	{
		public List<SlideMaster> Slides { get; }

		public SlideManager()
		{
			Slides = new List<SlideMaster>();
		}

		public virtual void LoadSlides(StorageDirectory slideContentsDirectory)
		{
			if (!slideContentsDirectory.ExistsLocal()) return;
			foreach (var sizeContentsFolder in slideContentsDirectory.GetLocalFolders())
			{
				SlideFormatEnum format;
				switch (Path.GetFileName(sizeContentsFolder.LocalPath))
				{
					case "4x3":
						format = SlideFormatEnum.Format4x3;
						break;
					case "16x9":
						format = SlideFormatEnum.Format16x9;
						break;
					case "3x4":
						format = SlideFormatEnum.Format3x4;
						break;
					default:
						continue;
				}

				ProcessSlideSizeFolder(sizeContentsFolder, format);
			}
		}

		protected virtual void ProcessSlideSizeFolder(StorageDirectory sizeFolder, SlideFormatEnum format)
		{
			foreach (var groupFolder in sizeFolder.GetLocalFolders())
			{
				foreach (var slideContentsFolder in groupFolder.GetLocalFolders())
				{
					var slideMaster = new SlideMaster(slideContentsFolder, slideContentsFolder)
					{
						Group = groupFolder.Name,
						Format = format
					};
					slideMaster.Load();
					Slides.Add(slideMaster);
				}
			}
			Slides.Sort(
				(x, y) => x.Group.Equals(y.Group) ? x.Order.CompareTo(y.Order) : WinAPIHelper.StrCmpLogicalW(x.Group, y.Group));
		}
	}
}
