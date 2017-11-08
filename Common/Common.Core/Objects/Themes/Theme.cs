﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Extensions;
using Asa.Common.Core.Objects.RemoteStorage;

namespace Asa.Common.Core.Objects.Themes
{
	public class Theme
	{
		private readonly StorageDirectory _root;
		private StorageFile _themeFile;

		public string Name { get; private set; }
		public int Order { get; private set; }
		public Image Logo { get; private set; }
		public Image BrowseLogo { get; private set; }
		public Image RibbonLogo { get; private set; }
		public List<SlideType> ApprovedSlides { get; private set; }

		public Theme(StorageDirectory root)
		{
			_root = root;
		}

		public override string ToString()
		{
			return Name;
		}

		public void Load()
		{
			var files = _root.GetLocalFiles().ToList();

			var titleFile = files.First(file => file.Name == "title.txt");
			Name = File.ReadAllText(titleFile.LocalPath).Trim();

			int tempInt;
			if (Int32.TryParse(Path.GetFileName(_root.LocalPath), out tempInt))
				Order = tempInt;

			var bigLogoFile = files.FirstOrDefault(file => file.Name == "preview.png");
			if (bigLogoFile != null)
			{
				Logo = new Bitmap(bigLogoFile.LocalPath);
				BrowseLogo = Logo.GetThumbnailImage(Logo.Width * 144 / Logo.Height, 144, null, IntPtr.Zero);
				var ribbonLogo = Logo.GetThumbnailImage(Logo.Width * 64 / Logo.Height, 64, null, IntPtr.Zero);
				RibbonLogo = ribbonLogo.DrawBorder();
			}

			_themeFile = files.FirstOrDefault(file => file.Extension == ".thmx");

			ApprovedSlides = new List<SlideType>();
		}

		public string GetThemePath()
		{
			return _themeFile.LocalPath;
		}
	}
}
