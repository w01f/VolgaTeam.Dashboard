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

		public virtual void LoadSlides(StorageDirectory storageDirectory)
		{
			if (!storageDirectory.ExistsLocal()) return;
			foreach (var sizeFolder in storageDirectory.GetLocalFolders())
			{
				SlideFormatEnum format;
				switch (Path.GetFileName(sizeFolder.LocalPath))
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

				ProcessSlideSizeFolder(sizeFolder, format);
			}
		}

		protected virtual void ProcessSlideSizeFolder(StorageDirectory sizeFolder, SlideFormatEnum format)
		{
			foreach (var groupFolder in sizeFolder.GetLocalFolders())
			{
				foreach (var slideFolder in groupFolder.GetLocalFolders())
				{
					var slideMaster = new SlideMaster(slideFolder)
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
