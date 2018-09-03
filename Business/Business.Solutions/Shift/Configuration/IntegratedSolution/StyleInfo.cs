using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using Asa.Common.Core.Extensions;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.RemoteStorage;

namespace Asa.Business.Solutions.Shift.Configuration.IntegratedSolution
{
	public class StyleInfo : IProductToggleContainerInfo
	{
		public string Title { get; private set; }

		public ImageTabInfo Tab1 { get; private set; }
		public LayoutTabInfo Tab2 { get; private set; }

		private StyleInfo()
		{
			Title = "style";
			Tab1 = ImageTabInfo.Empty();
			Tab2 = LayoutTabInfo.Empty();
		}

		public IList<IProductSubTabInfo> GetSubTabInfoList()
		{
			return new IProductSubTabInfo[] { Tab1 };
		}

		public static StyleInfo FromXml(XmlNode configNode, StorageDirectory sharedImagesFolder, string productId)
		{
			var styleInfo = Empty();

			styleInfo.Title = configNode.SelectSingleNode("./Title")?.InnerText ?? styleInfo.Title;

			foreach (var node in configNode.SelectNodes("./Tabs/Tab")?.OfType<XmlNode>().ToArray() ?? new XmlNode[] { })
			{
				var id = node.Attributes?.OfType<XmlAttribute>()
					.FirstOrDefault(a => String.Equals(a.Name, "Id", StringComparison.OrdinalIgnoreCase))?.Value?.ToUpper();
				switch (id)
				{
					case "IMAGE":
						styleInfo.Tab1 = ImageTabInfo.FromXml(node,
							new StorageDirectory(sharedImagesFolder.RelativePathParts.Merge(new[] { "product_images", productId })));
						break;
					case "LAYOUT":
						styleInfo.Tab2 = LayoutTabInfo.FromXml(node,
							new StorageDirectory(sharedImagesFolder.RelativePathParts.Merge("layout_images")));
						break;
				}
			}
			return styleInfo;
		}

		public static StyleInfo Empty()
		{
			return new StyleInfo();
		}

		public class ImageTabInfo : IProductSubTabInfo
		{
			public string Title { get; private set; }

			public List<string> ImageFiles { get; }

			public string DefaultImagePath { get; private set; }

			private ImageTabInfo()
			{
				ImageFiles = new List<String>();
			}

			public static ImageTabInfo FromXml(XmlNode configNode, StorageDirectory imageFolder)
			{
				var tabInfo = Empty();

				if (imageFolder.ExistsLocal())
					tabInfo.ImageFiles.AddRange(Directory.GetFiles(imageFolder.LocalPath, "*.png"));

				tabInfo.ImageFiles.Sort(WinAPIHelper.StrCmpLogicalW);

				if (configNode != null)
				{
					tabInfo.Title = configNode.SelectSingleNode("./Title")?.InnerText ?? "image";
					tabInfo.DefaultImagePath = tabInfo.ImageFiles.FirstOrDefault(imageFile =>
												   String.Equals(configNode.SelectSingleNode("./Image")?.InnerText,
													   Path.GetFileName(imageFile), StringComparison.OrdinalIgnoreCase)) ??
											   tabInfo.ImageFiles.FirstOrDefault();
				}

				return tabInfo;
			}

			public static ImageTabInfo Empty()
			{
				return new ImageTabInfo();
			}
		}

		public class LayoutTabInfo : IProductSubTabInfo
		{
			public string Title { get; private set; }

			public List<LayoutItem> LayoutItems { get; }

			public LayoutItem DefaultItem { get; private set; }

			private LayoutTabInfo()
			{
				LayoutItems = new List<LayoutItem>();
			}

			public static LayoutTabInfo FromXml(XmlNode configNode, StorageDirectory imageFolder)
			{
				var tabInfo = Empty();

				if (imageFolder.ExistsLocal())
				{
					var layoutFiles = Directory.GetFiles(imageFolder.LocalPath, "*.png").ToList();
					layoutFiles.Sort(WinAPIHelper.StrCmpLogicalW);
					tabInfo.LayoutItems.AddRange(layoutFiles.Select(file => LayoutItem.FromFile(file)));
				}

				if (configNode != null)
				{
					tabInfo.Title = configNode.SelectSingleNode("./Title")?.InnerText ?? "layout";
					tabInfo.DefaultItem = tabInfo.LayoutItems.FirstOrDefault(layoutItem =>
												   String.Equals(configNode.SelectSingleNode("./Image")?.InnerText,
													   Path.GetFileName(layoutItem.FilePath), StringComparison.OrdinalIgnoreCase)) ??
											   tabInfo.LayoutItems.FirstOrDefault();
				}

				return tabInfo;
			}

			public static LayoutTabInfo Empty()
			{
				return new LayoutTabInfo();
			}
		}
	}
}
