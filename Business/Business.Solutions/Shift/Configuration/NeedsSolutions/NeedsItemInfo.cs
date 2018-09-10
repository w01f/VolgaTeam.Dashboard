using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Common.Core.Objects.RemoteStorage;

namespace Asa.Business.Solutions.Shift.Configuration.NeedsSolutions
{
	public class NeedsItemInfo
	{
		public string Id { get; private set; }
		public string Title { get; private set; }

		public string ImagePath { get; private set; }
		public ClipartConfiguration ClipartConfiguration { get; }

		public string SubHeaderDefaultValue { get; private set; }
		public string SubHeaderPlaceholder { get; private set; }

		public TextEditorConfiguration SubheaderConfiguration { get; set; }

		private NeedsItemInfo()
		{
			ClipartConfiguration = new ClipartConfiguration();
			SubheaderConfiguration = TextEditorConfiguration.Empty();
		}

		public override String ToString()
		{
			return Title;
		}

		public static NeedsItemInfo FromXml(XmlNode configNode, StorageDirectory imageFolder)
		{
			var itemInfo = new NeedsItemInfo();

			itemInfo.Id = configNode.Attributes?.OfType<XmlAttribute>()
				.FirstOrDefault(a => String.Equals(a.Name, "Button", StringComparison.OrdinalIgnoreCase))?.Value;

			itemInfo.Title = configNode.Attributes?.OfType<XmlAttribute>()
				.FirstOrDefault(a => String.Equals(a.Name, "Value", StringComparison.OrdinalIgnoreCase))?.Value;

			var imageFileName = configNode.Attributes?.OfType<XmlAttribute>()
				.FirstOrDefault(a => String.Equals(a.Name, "StaticImage", StringComparison.OrdinalIgnoreCase))?.Value;
			if (!String.IsNullOrEmpty(imageFileName))
			{
				itemInfo.ImagePath = Path.Combine(imageFolder.LocalPath, imageFileName);
			}

			itemInfo.SubHeaderDefaultValue = configNode.Attributes?.OfType<XmlAttribute>()
				.FirstOrDefault(a => String.Equals(a.Name, "Description", StringComparison.OrdinalIgnoreCase))?.Value;

			return itemInfo;
		}
	}
}
