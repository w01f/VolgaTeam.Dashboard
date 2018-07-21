using System;
using System.Collections.Generic;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Common.Core.Helpers;

namespace Asa.Business.Solutions.StarApp.Configuration
{
	public class CoverConfiguration
	{
		public List<ListDataItem> HeaderPartAItems { get; }
		public ClipartConfiguration PartAClipart1Configuration { get; private set; }
		public string PartASubHeader1DefaultValue { get; private set; }
		public string PartASubHeader1Placeholder { get; private set; }
		public SlideManager PartUSlides { get; }
		public SlideManager PartVSlides { get; }
		public SlideManager PartWSlides { get; }

		public CoverConfiguration()
		{
			HeaderPartAItems = new List<ListDataItem>();
			PartAClipart1Configuration = new ClipartConfiguration();

			PartUSlides = new SlideManager();
			PartVSlides = new SlideManager();
			PartWSlides = new SlideManager();
		}

		public void Load(ResourceManager resourceManager)
		{
			if (resourceManager.DataCoverPartAFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataCoverPartAFile.LocalPath);

				var node = document.SelectSingleNode(@"/CP01A");
				if (node == null) return;
				foreach (XmlNode childNode in node.ChildNodes)
				{
					var item = ListDataItem.FromXml(childNode);
					switch (childNode.Name)
					{
						case "CP01AHeader":
							if (!String.IsNullOrEmpty(item.Value))
								HeaderPartAItems.Add(item);
							break;
						case "CP01ASubheader1":
							if (item.IsPlaceholder)
								PartASubHeader1Placeholder = item.Value;
							else
								PartASubHeader1DefaultValue = item.Value;
							break;
					}
				}

				PartAClipart1Configuration = ClipartConfiguration.FromXml(node, "CP01AClipart1");
			}

			if (resourceManager.Tab1PartUSlidesFolder.ExistsLocal())
			{
				PartUSlides.LoadSlides(resourceManager.Tab1PartUSlidesFolder);
			}

			if (resourceManager.Tab1PartVSlidesFolder.ExistsLocal())
			{
				PartVSlides.LoadSlides(resourceManager.Tab1PartVSlidesFolder);
			}

			if (resourceManager.Tab1PartWSlidesFolder.ExistsLocal())
			{
				PartWSlides.LoadSlides(resourceManager.Tab1PartWSlidesFolder);
			}
		}
	}
}
