using System;
using System.Drawing;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.StarApp.Enums;

namespace Asa.Business.Solutions.StarApp.Configuration.Audience
{
	public class AudienceTabAInfo : StarTabWithHeaderInfo
	{
		public override StarChildTabType TabType => StarChildTabType.A;

		public Image Clipart1Image => _resourceManager.GraphicResources?.Tab9_A_Clipart1;
		public Image Clipart2Image => _resourceManager.GraphicResources?.Tab9_A_Clipart2;

		public string SubHeader1DefaultValue { get; private set; }
		public string SubHeader2DefaultValue { get; private set; }
		public string SubHeader1Placeholder { get; private set; }
		public string SubHeader2Placeholder { get; private set; }
		public ClipartConfiguration Clipart1Configuration { get; private set; }
		public ClipartConfiguration Clipart2Configuration { get; private set; }

		public AudienceTabAInfo() : base(StarTopTabType.Audience)
		{
			Clipart1Configuration = new ClipartConfiguration();
			Clipart2Configuration = new ClipartConfiguration();
		}

		public override void LoadData(XmlNode configNode, ResourceManager resourceManager)
		{
			base.LoadData(configNode, resourceManager);

			if (!resourceManager.DataAudiencePartAFile.ExistsLocal()) return;
			var document = new XmlDocument();
			document.Load(resourceManager.DataAudiencePartAFile.LocalPath);

			var node = document.SelectSingleNode(@"/CP09A");
			if (node == null) return;
			foreach (XmlNode childNode in node.ChildNodes)
			{
				var item = ListDataItem.FromXml(childNode);
				switch (childNode.Name)
				{
					case "CP09AHeader":
						if (!String.IsNullOrEmpty(item.Value))
							HeadersItems.Add(item);
						break;
					case "CP09ASubheader1":
						if (item.IsPlaceholder)
							SubHeader1Placeholder = item.Value;
						else
							SubHeader1DefaultValue = item.Value;
						break;
					case "CP09ASubheader2":
						if (item.IsPlaceholder)
							SubHeader2Placeholder = item.Value;
						else
							SubHeader2DefaultValue = item.Value;
						break;
				}
			}

			Clipart1Configuration = ClipartConfiguration.FromXml(node, "CP09AClipart1");
			Clipart2Configuration = ClipartConfiguration.FromXml(node, "CP09AClipart2");
		}
	}
}
