using System;
using System.Drawing;
using System.IO;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Common.Core.Objects.RemoteStorage;

namespace Asa.Business.Solutions.Shift.Configuration.NeedsSolutions
{
	public class SolutionsItemInfo
	{
		public string Id { get; private set; }
		public string Title { get; private set; }
		public string ButtonHeader { get; private set; }

		public Image ClipartImage { get; private set; }
		public ClipartConfiguration ClipartConfiguration { get; }

		public string SubHeaderDefaultValue { get; private set; }
		public string SubHeaderPlaceholder { get; private set; }

		public SolutionButtonConfiguration ButtonConfiguration { get; set; }
		public TextEditorConfiguration SubheaderConfiguration { get; set; }

		private SolutionsItemInfo()
		{
			ClipartConfiguration = new ClipartConfiguration();
			ButtonConfiguration = SolutionButtonConfiguration.Empty();
			SubheaderConfiguration = TextEditorConfiguration.Empty();
		}

		public override String ToString()
		{
			return ButtonHeader;
		}

		public static SolutionsItemInfo FromXml(XmlNode configNode, StorageDirectory imageFolder)
		{
			var itemInfo = new SolutionsItemInfo();

			itemInfo.Id = configNode.SelectSingleNode("./ProductButton")?.InnerText;
			itemInfo.Title = configNode.SelectSingleNode("./Name")?.InnerText;
			itemInfo.ButtonHeader = configNode.SelectSingleNode("./BenefitHeader")?.InnerText;
			
			var imageFileName = configNode.SelectSingleNode("./StaticImage")?.InnerText;
			if (!String.IsNullOrEmpty(imageFileName))
			{
				var imageFilePath = Path.Combine(imageFolder.LocalPath, imageFileName);
				itemInfo.ClipartImage = File.Exists(imageFilePath)
					? Image.FromFile(imageFilePath)
					: null;
			}

			itemInfo.SubHeaderDefaultValue = configNode.SelectSingleNode("./BenefitDetails")?.InnerText;

			return itemInfo;
		}
	}
}
