using System;
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
		public const int DefaultThumbnailHeight = 144;

		private readonly StorageDirectory _root;
		private StorageFile _masterFile;

		protected Size _thumbnailSize = Size.Empty;

		public Guid Identifier { get; set; }
		public string Name { get; private set; }
		public string ToolTipHeader { get; private set; }
		public string ToolTipBody { get; private set; }
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

			var titleFile = files.FirstOrDefault(file => file.Name == "title.txt");
			Name = titleFile != null ?
				File.ReadAllText(titleFile.LocalPath).Trim() :
				Path.GetFileName(_root.LocalPath);

			var toolTipFile = files.FirstOrDefault(file => file.Name == "tip.txt");
			if (toolTipFile != null)
			{
				var tooltipLines = File.ReadAllLines(toolTipFile.LocalPath);
				ToolTipHeader = tooltipLines.ElementAtOrDefault(0)?.Replace("*", "");
				ToolTipBody = tooltipLines.ElementAtOrDefault(1)?.Replace("*", "");
			}

			if (Int32.TryParse(Path.GetFileName(_root.LocalPath), out var tempInt))
				Order = tempInt;

			LogoFile = files.FirstOrDefault(file =>
				(String.Equals(file.Extension, ".PNG", StringComparison.OrdinalIgnoreCase) ||
				String.Equals(file.Extension, ".JPG", StringComparison.OrdinalIgnoreCase) ||
				String.Equals(file.Extension, ".JPEG", StringComparison.OrdinalIgnoreCase)) &&
				!file.Name.Contains("_rbn"));
			if (LogoFile != null)
			{
				Logo = new Bitmap(LogoFile.LocalPath);

				Int32 browseLogoWidth;
				Int32 browseLogoHeight;
				if (_thumbnailSize == Size.Empty)
				{
					browseLogoWidth = (Int32)(Logo.Width * DefaultThumbnailHeight) / Logo.Height;
					browseLogoHeight = DefaultThumbnailHeight;
				}
				else
				{
					var persentWidth = _thumbnailSize.Width > 0 ? (float)_thumbnailSize.Width / Logo.Width : 1;
					var persentHeight = _thumbnailSize.Height > 0 ? (float)_thumbnailSize.Height / Logo.Height : 1;
					var percentFinal = new[] { persentWidth, persentHeight }.Min();
					browseLogoWidth = (Int32)(Logo.Width * percentFinal);
					browseLogoHeight = (Int32)(Logo.Height * percentFinal);
				}
				BrowseLogo = Logo.GetThumbnailImage(browseLogoWidth, browseLogoHeight, null, IntPtr.Zero);

				var borderedLogo = Logo.DrawBorder();

				RibbonLogo = borderedLogo.GetThumbnailImage((borderedLogo.Width * 72) / borderedLogo.Height, 72, null, IntPtr.Zero);
				AdBarLogo = borderedLogo.GetThumbnailImage((borderedLogo.Width * 86) / borderedLogo.Height, 86, null, IntPtr.Zero);
			}
			_masterFile = files.FirstOrDefault(file => String.Equals(file.Extension, ".PPTX", StringComparison.OrdinalIgnoreCase));
		}

		public string GetMasterPath()
		{
			return _masterFile.LocalPath;
		}
	}
}
