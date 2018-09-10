using System;
using System.Drawing;
using System.IO;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Common.Core.Objects.RemoteStorage;

namespace Asa.Business.Solutions.Shift.Configuration.Approach
{
	public class ApproachItemInfo
	{
		public string Id { get; private set; }
		public string Title { get; private set; }

		public string ImagePath { get; private set; }
		public ClipartConfiguration ClipartConfiguration { get; }

		public string SubHeaderDefaultValue { get; private set; }
		public string SubHeaderPlaceholder { get; private set; }

		public TextEditorConfiguration SubheaderConfiguration { get; set; }

		private ApproachItemInfo()
		{
			ClipartConfiguration = new ClipartConfiguration();
			SubheaderConfiguration = TextEditorConfiguration.Empty();
		}

		public override string ToString()
		{
			return Title;
		}

		public static ApproachItemInfo FromXml(XmlNode configNode, StorageDirectory imageFolder)
		{
			var itemInfo = new ApproachItemInfo();

			itemInfo.Id = configNode.SelectSingleNode("./Button")?.InnerText;
			itemInfo.Title = configNode.SelectSingleNode("./Name")?.InnerText;

			var imageFileName = configNode.SelectSingleNode("./StaticImage")?.InnerText;
			if (!String.IsNullOrEmpty(imageFileName))
				itemInfo.ImagePath = Path.Combine(imageFolder.LocalPath, imageFileName);

			itemInfo.SubHeaderDefaultValue = configNode.SelectSingleNode("./DefaultStatement")?.InnerText;

			return itemInfo;
		}
	}
}
