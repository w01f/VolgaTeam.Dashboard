using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace NewBizWiz.Core.Common
{
	public class ThemeManager
	{
		public List<Theme> Themes { get; private set; }

		public ThemeManager(string rootPath)
		{
			Themes = new List<Theme>();
			if (!Directory.Exists(rootPath)) return;
			foreach (var themeFolder in Directory.GetDirectories(rootPath))
				Themes.Add(new Theme(themeFolder));
			Themes.Sort((x, y) => x.Order.CompareTo(y.Order));
		}
	}

	public class Theme
	{
		public string Name { get; private set; }
		public int Order { get; private set; }
		public Image Logo { get; private set; }
		public Image BrowseLogo { get; private set; }
		public Image RibbonLogo { get; private set; }
		public string PotFilePath { get; private set; }
		public string ThemeFilePath { get; private set; }

		public Theme(string rootPath)
		{
			var titlePath = Path.Combine(rootPath, "title.txt");
			if (File.Exists(titlePath))
				Name = File.ReadAllText(titlePath).Trim();

			int tempInt;
			if (Int32.TryParse(Path.GetFileName(rootPath), out tempInt))
				Order = tempInt;

			var bigLogoPath = Path.Combine(rootPath, "preview.png");
			if (File.Exists(bigLogoPath))
			{
				Logo = new Bitmap(bigLogoPath);
				BrowseLogo = Logo.GetThumbnailImage((Logo.Width * 144) / Logo.Height, 144, null, IntPtr.Zero);
				RibbonLogo = Logo.GetThumbnailImage((Logo.Width * 72) / Logo.Height, 72, null, IntPtr.Zero);
			}
			PotFilePath = Directory.GetFiles(rootPath, "*.pot").FirstOrDefault();
			ThemeFilePath = Directory.GetFiles(rootPath, "*.thmx").FirstOrDefault();
		}
	}

	public class ThemeEventArgs : EventArgs
	{
		public Theme SelectedTheme { get; set; }
	}
}
