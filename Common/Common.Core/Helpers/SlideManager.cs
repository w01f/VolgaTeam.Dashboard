﻿using System.Collections.Generic;
using System.IO;
using Asa.Common.Core.Configuration;
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

		public void Load()
		{
			var storageDirectory = ResourceManager.Instance.SlideMastersFolder;
			if (!storageDirectory.ExistsLocal()) return;
			LoadSlides(storageDirectory);
		}

		private void LoadSlides(StorageDirectory storageDirectory)
		{
			foreach (var sizeFolder in storageDirectory.GetLocalFolders())
			{
				var format = SlideFormatEnum.Undefined;
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
				}
				foreach (var groupFolder in sizeFolder.GetLocalFolders())
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
