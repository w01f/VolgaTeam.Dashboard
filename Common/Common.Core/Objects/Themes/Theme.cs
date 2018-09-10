using System;
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
		private Image _ribbonLogo;

		public Guid Identifier { get; set; }
		public string Name { get; private set; }
		public int Order { get; private set; }
		public string LocalPath { get; private set; }
		public List<SlideType> ApprovedSlides { get; private set; }

		public Theme(StorageDirectory root)
		{
			_root = root;
			Identifier = Guid.NewGuid();
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

			if (Int32.TryParse(Path.GetFileName(_root.LocalPath), out var tempInt))
				Order = tempInt;

			var bigLogoFile = files.FirstOrDefault(file => file.Name == "preview.png");
			if (bigLogoFile != null)
				LocalPath = bigLogoFile.LocalPath;

			_themeFile = files.FirstOrDefault(file => file.Extension == ".thmx");

			ApprovedSlides = new List<SlideType>();
		}

		public Image GetRibbonLogo()
		{
			if (_ribbonLogo != null)
				return _ribbonLogo;

			if (!String.IsNullOrWhiteSpace(LocalPath))
			{
				using (var originalImage = Image.FromFile(LocalPath))
				{
					using (var ribbonLogo =
						originalImage.GetThumbnailImage(originalImage.Width * 64 / originalImage.Height, 64, null, IntPtr.Zero))
					{
						_ribbonLogo = ribbonLogo.DrawBorder();
					}
				}
			}

			return _ribbonLogo;
		}

		public string GetThemePath()
		{
			return _themeFile.LocalPath;
		}
	}
}
