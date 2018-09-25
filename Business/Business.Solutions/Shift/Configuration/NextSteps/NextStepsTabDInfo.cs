using System;
using System.Drawing;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Shift.Enums;

namespace Asa.Business.Solutions.Shift.Configuration.NextSteps
{
	public class NextStepsTabDInfo : ShiftTabWithHeaderInfo
	{
		public Image Clipart1Image => _resourceManager.GraphicResources?.Tab14_D_Clipart1;
		public ClipartConfiguration Clipart1Configuration { get; private set; }
		public Image Clipart2Image => _resourceManager.GraphicResources?.Tab14_D_Clipart2;
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

		public NextStepsTabDInfo() : base(ShiftChildTabType.D, ShiftTopTabType.NextSteps)
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

			if (!resourceManager.DataNextStepsPartDFile.ExistsLocal()) return;

			var document = new XmlDocument();
			document.Load(resourceManager.DataNextStepsPartDFile.LocalPath);

			var node = document.SelectSingleNode(@"/SHIFT14D");
			if (node == null) return;
			foreach (XmlNode childNode in node.ChildNodes)
			{
				var item = ListDataItem.FromXml(childNode);
				switch (childNode.Name)
				{
					case "SHIFT14DHeader":
						if (!String.IsNullOrEmpty(item.Value))
							HeadersItems.Add(item);
						break;
					case "SHIFT14DSubheader1":
						if (item.IsPlaceholder)
							SubHeader1Placeholder = item.Value;
						else
							SubHeader1DefaultValue = item.Value;
						break;
					case "SHIFT14DSubheader2":
						if (item.IsPlaceholder)
							SubHeader2Placeholder = item.Value;
						else
							SubHeader2DefaultValue = item.Value;
						break;
					case "SHIFT14DSubheader3":
						if (item.IsPlaceholder)
							SubHeader3Placeholder = item.Value;
						else
							SubHeader3DefaultValue = item.Value;
						break;
					case "SHIFT14DSubheader4":
						if (item.IsPlaceholder)
							SubHeader4Placeholder = item.Value;
						else
							SubHeader4DefaultValue = item.Value;
						break;
					case "SHIFT14DSubheader5":
						if (item.IsPlaceholder)
							SubHeader5Placeholder = item.Value;
						else
							SubHeader5DefaultValue = item.Value;
						break;
					case "SHIFT14DSubheader6":
						if (item.IsPlaceholder)
							SubHeader6Placeholder = item.Value;
						else
							SubHeader6DefaultValue = item.Value;
						break;
					case "SHIFT14DSubheader7":
						if (item.IsPlaceholder)
							SubHeader7Placeholder = item.Value;
						else
							SubHeader7DefaultValue = item.Value;
						break;
					case "SHIFT14DSubheader8":
						if (item.IsPlaceholder)
							SubHeader8Placeholder = item.Value;
						else
							SubHeader8DefaultValue = item.Value;
						break;
				}
			}

			Clipart1Configuration = ClipartConfiguration.FromXml(node, "SHIFT14DClipart1");
			Clipart2Configuration = ClipartConfiguration.FromXml(node, "SHIFT14DClipart2");

			CommonEditorConfiguration = TextEditorConfiguration.FromXml(node);
			HeadersEditorConfiguration = TextEditorConfiguration.FromXml(node, "SHIFT14DHeader");
			SubHeader1Configuration = TextEditorConfiguration.FromXml(node, "SHIFT14DSubheader1");
			SubHeader2Configuration = TextEditorConfiguration.FromXml(node, "SHIFT14DSubheader2");
			SubHeader3Configuration = TextEditorConfiguration.FromXml(node, "SHIFT14DSubheader3");
			SubHeader4Configuration = TextEditorConfiguration.FromXml(node, "SHIFT14DSubheader4");
			SubHeader5Configuration = TextEditorConfiguration.FromXml(node, "SHIFT14DSubheader5");
			SubHeader6Configuration = TextEditorConfiguration.FromXml(node, "SHIFT14DSubheader6");
			SubHeader7Configuration = TextEditorConfiguration.FromXml(node, "SHIFT14DSubheader7");
			SubHeader8Configuration = TextEditorConfiguration.FromXml(node, "SHIFT14DSubheader8");
		}
	}
}
