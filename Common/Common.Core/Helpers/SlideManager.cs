using System.Collections.Generic;
using System.IO;
using Asa.Common.Core.Configuration;
using Asa.Common.Core.Objects.Slides;

namespace Asa.Common.Core.Helpers
{
	public class SlideManager
	{
		public List<SlideMaster> Slides { get; private set; }

		public SlideManager()
		{
			Slides = new List<SlideMaster>();
		}

		public void Load()
		{
			var storageDirectory = ResourceManager.Instance.SlideMastersFolder;
			if (!storageDirectory.ExistsLocal()) return;
			foreach (var sizeFolder in storageDirectory.GetLocalFolders())
			{
				double width = 0;
				double height = 0;
				switch (Path.GetFileName(sizeFolder.LocalPath))
				{
					case "4x3":
						width = 10;
						height = 7.5;
						break;
					case "5x4":
						width = 10.75;
						height = 8.25;
						break;
					case "16x9":
						width = 13.333333333333334;
						height = 7.5;
						break;
					case "3x4":
						width = 7.5;
						height = 10;
						break;
					case "4x5":
						width = 8.25;
						height = 10.75;
						break;
				}
				foreach (var groupFolder in sizeFolder.GetLocalFolders())
					foreach (var slideFolder in groupFolder.GetLocalFolders())
					{
						var slideMaster = new SlideMaster(slideFolder) { Group = groupFolder.Name, SizeWidth = width, SizeHeght = height };
						slideMaster.Load();
						Slides.Add(slideMaster);
					}
			}
			Slides.Sort((x, y) => x.Group.Equals(y.Group) ? x.Order.CompareTo(y.Order) : WinAPIHelper.StrCmpLogicalW(x.Group, y.Group));
		}
	}
}
