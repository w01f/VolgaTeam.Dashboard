using System;
using System.Drawing;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Shift.Enums;

namespace Asa.Business.Solutions.Shift.Configuration.Partnership
{
	public class PartnershipTabCInfo : ShiftTabWithHeaderInfo
	{
		public Image Clipart1Image => _resourceManager.GraphicResources?.Tab6_C_Clipart1;
		public ClipartConfiguration Clipart1Configuration { get; private set; }
		public Image Clipart2Image => _resourceManager.GraphicResources?.Tab6_C_Clipart2;
		public ClipartConfiguration Clipart2Configuration { get; private set; }

		public string SubHeader1DefaultValue { get; private set; }
		public string SubHeader1Placeholder { get; private set; }
		public TextEditorConfiguration SubHeader1Configuration { get; set; }

		public string SubHeader2DefaultValue { get; private set; }
		public string SubHeader2Placeholder { get; private set; }
		public TextEditorConfiguration SubHeader2Configuration { get; set; }

		public string SubHeader3DefaultValue { get; private set; }
		public string SubHeader3Placeholder { get; private set; }
		public TextEditorConfiguration SubHeader3Configuration { get; set; }

		public string SubHeader4DefaultValue { get; private set; }
		public string SubHeader4Placeholder { get; private set; }
		public TextEditorConfiguration SubHeader4Configuration { get; set; }

		public string SubHeader5DefaultValue { get; private set; }
		public string SubHeader5Placeholder { get; private set; }
		public TextEditorConfiguration SubHeader5Configuration { get; set; }

		public string SubHeader6DefaultValue { get; private set; }
		public string SubHeader6Placeholder { get; private set; }
		public TextEditorConfiguration SubHeader6Configuration { get; set; }

		public string SubHeader7DefaultValue { get; private set; }
		public string SubHeader7Placeholder { get; private set; }
		public TextEditorConfiguration SubHeader7Configuration { get; set; }

		public string SubHeader8DefaultValue { get; private set; }
		public string SubHeader8Placeholder { get; private set; }
		public TextEditorConfiguration SubHeader8Configuration { get; set; }

		public PartnershipTabCInfo() : base(ShiftChildTabType.C, ShiftTopTabType.Partnership)
		{
			Clipart1Configuration = new ClipartConfiguration();
			Clipart2Configuration = new ClipartConfiguration();

			SubHeader1Configuration = TextEditorConfiguration.Empty();
			SubHeader2Configuration = TextEditorConfiguration.Empty();
			SubHeader3Configuration = TextEditorConfiguration.Empty();
			SubHeader4Configuration = TextEditorConfiguration.Empty();
			SubHeader5Configuration = TextEditorConfiguration.Empty();
			SubHeader6Configuration = TextEditorConfiguration.Empty();
			SubHeader7Configuration = TextEditorConfiguration.Empty();
			SubHeader8Configuration = TextEditorConfiguration.Empty();
		}

		public override void LoadData(XmlNode configNode, ResourceManager resourceManager)
		{
			base.LoadData(configNode, resourceManager);

			if (!resourceManager.DataPartnershipPartCFile.ExistsLocal()) return;

			var document = new XmlDocument();
			document.Load(resourceManager.DataPartnershipPartCFile.LocalPath);

			var node = document.SelectSingleNode(@"/SHIFT06C");
			if (node == null) return;
			foreach (XmlNode childNode in node.ChildNodes)
			{
				var item = ListDataItem.FromXml(childNode);
				switch (childNode.Name)
				{
					case "SHIFT06CHeader":
						if (!String.IsNullOrEmpty(item.Value))
							HeadersItems.Add(item);
						break;
					case "SHIFT06CSubheader1":
						if (item.IsPlaceholder)
							SubHeader1Placeholder = item.Value;
						else
							SubHeader1DefaultValue = item.Value;
						break;
					case "SHIFT06CSubheader2":
						if (item.IsPlaceholder)
							SubHeader2Placeholder = item.Value;
						else
							SubHeader2DefaultValue = item.Value;
						break;
					case "SHIFT06CSubheader3":
						if (item.IsPlaceholder)
							SubHeader3Placeholder = item.Value;
						else
							SubHeader3DefaultValue = item.Value;
						break;
					case "SHIFT06CSubheader4":
						if (item.IsPlaceholder)
							SubHeader4Placeholder = item.Value;
						else
							SubHeader4DefaultValue = item.Value;
						break;
					case "SHIFT06CSubheader5":
						if (item.IsPlaceholder)
							SubHeader5Placeholder = item.Value;
						else
							SubHeader5DefaultValue = item.Value;
						break;
					case "SHIFT06CSubheader6":
						if (item.IsPlaceholder)
							SubHeader6Placeholder = item.Value;
						else
							SubHeader6DefaultValue = item.Value;
						break;
					case "SHIFT06CSubheader7":
						if (item.IsPlaceholder)
							SubHeader7Placeholder = item.Value;
						else
							SubHeader7DefaultValue = item.Value;
						break;
					case "SHIFT06CSubheader8":
						if (item.IsPlaceholder)
							SubHeader8Placeholder = item.Value;
						else
							SubHeader8DefaultValue = item.Value;
						break;
				}
			}

			Clipart1Configuration = ClipartConfiguration.FromXml(node, "SHIFT06CClipart1");
			Clipart2Configuration = ClipartConfiguration.FromXml(node, "SHIFT06CClipart2");

			CommonEditorConfiguration = TextEditorConfiguration.FromXml(node);
			HeadersEditorConfiguration = TextEditorConfiguration.FromXml(node, "SHIFT06CHeader");
			SubHeader1Configuration = TextEditorConfiguration.FromXml(node, "SHIFT06CSubheader1");
			SubHeader2Configuration = TextEditorConfiguration.FromXml(node, "SHIFT06CSubheader2");
			SubHeader3Configuration = TextEditorConfiguration.FromXml(node, "SHIFT06CSubheader3");
			SubHeader4Configuration = TextEditorConfiguration.FromXml(node, "SHIFT06CSubheader4");
			SubHeader5Configuration = TextEditorConfiguration.FromXml(node, "SHIFT06CSubheader5");
			SubHeader6Configuration = TextEditorConfiguration.FromXml(node, "SHIFT06CSubheader6");
			SubHeader7Configuration = TextEditorConfiguration.FromXml(node, "SHIFT06CSubheader7");
			SubHeader8Configuration = TextEditorConfiguration.FromXml(node, "SHIFT06CSubheader8");
		}
	}
}
