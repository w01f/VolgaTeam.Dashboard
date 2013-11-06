using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace NewBizWiz.Core.Common
{
	public class SlideManager
	{
		public List<SlideMaster> Slides { get; private set; }

		public SlideManager(string rootPath)
		{
			Slides = new List<SlideMaster>();
			if (!Directory.Exists(rootPath)) return;
			foreach (var sizeFolder in Directory.GetDirectories(rootPath))
			{
				double width = 0;
				double height = 0;
				switch (Path.GetFileName(sizeFolder))
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
						width = 13;
						height = 7.32;
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
				foreach (var groupFolder in Directory.GetDirectories(sizeFolder))
					foreach (var slideFolder in Directory.GetDirectories(groupFolder))
						Slides.Add(new SlideMaster(slideFolder) { Group = Path.GetFileName(groupFolder), SizeWidth = width, SizeHeght = height });
			}
			Slides.Sort((x, y) => x.Group.Equals(y.Group) ? x.Order.CompareTo(y.Order) : Interop.WinAPIHelper.StrCmpLogicalW(x.Group, y.Group));
		}
	}

	public class SlideMaster
	{
		public string Name { get; private set; }
		public string Group { get; set; }
		public double SizeHeght { get; set; }
		public double SizeWidth { get; set; }
		public int Order { get; private set; }
		public Image Logo { get; private set; }
		public Image BrowseLogo { get; private set; }
		public Image RibbonLogo { get; private set; }
		public Image AdBarLogo { get; private set; }
		public string MasterPath { get; private set; }

		public SlideMaster(string rootPath)
		{
			var titlePath = Path.Combine(rootPath, "title.txt");
			if (File.Exists(titlePath))
				Name = File.ReadAllText(titlePath).Trim();
			int tempInt;
			if (Int32.TryParse(Path.GetFileName(rootPath), out tempInt))
				Order = tempInt;
			var logoPath = Directory.GetFiles(rootPath, "*.png").FirstOrDefault(f => !f.Contains("_rbn"));
			if (logoPath != null)
			{
				Logo = new Bitmap(logoPath);
				BrowseLogo = Logo.GetThumbnailImage((Logo.Width * 144) / Logo.Height, 144, null, IntPtr.Zero);
				RibbonLogo = Logo.GetThumbnailImage((Logo.Width * 72) / Logo.Height, 72, null, IntPtr.Zero);
				AdBarLogo = Logo.GetThumbnailImage((Logo.Width * 86) / Logo.Height, 86, null, IntPtr.Zero);
			}
			MasterPath = Directory.GetFiles(rootPath, "*.ppt").FirstOrDefault();
		}
	}

	public class SlideMasterEventArgs : EventArgs
	{
		public SlideMaster SelectedSlide { get; set; }
	}
}
