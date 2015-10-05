﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NewBizWiz.Core.Common
{
	public class SlideManager
	{
		public List<SlideMaster> Slides { get; private set; }

		public SlideManager()
		{
			Slides = new List<SlideMaster>();
		}

		public async Task Load()
		{
			var storageDirectory = ResourceManager.Instance.SlideMastersFolder;
			if (!await storageDirectory.Exists(true)) return;
			foreach (var sizeFolder in await storageDirectory.GetFolders())
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
				foreach (var groupFolder in await sizeFolder.GetFolders())
					foreach (var slideFolder in await groupFolder.GetFolders())
					{
						var slideMaster = new SlideMaster(slideFolder) { Group = groupFolder.Name, SizeWidth = width, SizeHeght = height };
						await slideMaster.Load();
						Slides.Add(slideMaster);
					}
			}
			Slides.Sort((x, y) => x.Group.Equals(y.Group) ? x.Order.CompareTo(y.Order) : Interop.WinAPIHelper.StrCmpLogicalW(x.Group, y.Group));
		}
	}

	public class SlideMaster
	{
		private readonly StorageDirectory _root;
		private StorageFile _masterFile;

		public string Name { get; private set; }
		public string Group { get; set; }
		public double SizeHeght { get; set; }
		public double SizeWidth { get; set; }
		public int Order { get; private set; }
		public Image Logo { get; private set; }
		public Image BrowseLogo { get; private set; }
		public Image RibbonLogo { get; private set; }
		public Image AdBarLogo { get; private set; }

		public SlideMaster(StorageDirectory root)
		{
			_root = root;
		}

		public async Task Load()
		{
			var files = (await _root.GetFiles()).ToList();

			foreach (var file in files.Where(file => new[] { ".txt", ".png" }.Contains(file.Extension)))
				await file.Download();

			var titleFile = files.First(file => file.Name == "title.txt");
			Name = File.ReadAllText(titleFile.LocalPath).Trim();

			int tempInt;
			if (Int32.TryParse(Path.GetFileName(_root.LocalPath), out tempInt))
				Order = tempInt;

			var logoFile = files.FirstOrDefault(file => file.Extension == ".png" && !file.Name.Contains("_rbn"));
			if (logoFile != null)
			{
				Logo = new Bitmap(logoFile.LocalPath);
				BrowseLogo = Logo.GetThumbnailImage((Logo.Width * 144) / Logo.Height, 144, null, IntPtr.Zero);

				var borderedLogo = Logo.DrawBorder();

				RibbonLogo = borderedLogo.GetThumbnailImage((borderedLogo.Width * 72) / borderedLogo.Height, 72, null, IntPtr.Zero);
				AdBarLogo = borderedLogo.GetThumbnailImage((borderedLogo.Width * 86) / borderedLogo.Height, 86, null, IntPtr.Zero);
			}
			_masterFile = files.FirstOrDefault(file => file.Extension == ".pptx");
		}

		public async Task<string> GetMasterPath()
		{
			if (_masterFile == null)
				_masterFile = (await _root.GetRemoteFiles()).FirstOrDefault(file => file.Extension == ".pptx");
			await _masterFile.Download();
			return _masterFile.LocalPath;
		}
	}

	public class SlideMasterEventArgs : EventArgs
	{
		public SlideMaster SelectedSlide { get; set; }
	}
}
