﻿using System;
using System.Drawing;
using System.IO;
using System.Linq;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Extensions;
using Asa.Common.Core.Objects.RemoteStorage;

namespace Asa.Common.Core.Objects.Slides
{
	public class SlideMaster
	{
		private readonly StorageDirectory _root;
		private StorageFile _masterFile;

		public Guid Identifier { get; set; }
		public string Name { get; private set; }
		public string Group { get; set; }
		public SlideFormatEnum Format { get; set; }
		public int Order { get; private set; }
		public StorageFile LogoFile { get; private set; }
		public Image Logo { get; private set; }
		public Image BrowseLogo { get; private set; }
		public Image RibbonLogo { get; private set; }
		public Image AdBarLogo { get; private set; }

		public SlideMaster(StorageDirectory root)
		{
			_root = root;
			Identifier = Guid.NewGuid();
		}

		public void Load()
		{
			var files = _root.GetLocalFiles().ToList();

			var titleFile = files.First(file => file.Name == "title.txt");
			Name = File.ReadAllText(titleFile.LocalPath).Trim();

			int tempInt;
			if (Int32.TryParse(Path.GetFileName(_root.LocalPath), out tempInt))
				Order = tempInt;

			LogoFile = files.FirstOrDefault(file => file.Extension == ".png" && !file.Name.Contains("_rbn"));
			if (LogoFile != null)
			{
				Logo = new Bitmap(LogoFile.LocalPath);
				BrowseLogo = Logo.GetThumbnailImage((Logo.Width * 144) / Logo.Height, 144, null, IntPtr.Zero);

				var borderedLogo = Logo.DrawBorder();

				RibbonLogo = borderedLogo.GetThumbnailImage((borderedLogo.Width * 72) / borderedLogo.Height, 72, null, IntPtr.Zero);
				AdBarLogo = borderedLogo.GetThumbnailImage((borderedLogo.Width * 86) / borderedLogo.Height, 86, null, IntPtr.Zero);
			}
			_masterFile = files.FirstOrDefault(file => file.Extension == ".pptx");
		}

		public string GetMasterPath()
		{
			return _masterFile.LocalPath;
		}
	}
}
