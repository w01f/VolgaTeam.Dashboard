using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Shift.Enums;

namespace Asa.Business.Solutions.Shift.Configuration.CBC
{
	public class CBCTabFInfo : ShiftTabWithHeaderInfo
	{
		public Image Clipart1Image { get; private set; }
		public ClipartConfiguration Clipart1Configuration { get; private set; }
		public Image Clipart2Image { get; private set; }
		public ClipartConfiguration Clipart2Configuration { get; private set; }

		public List<ListDataItem> Combo1Items { get; }
		public TextEditorConfiguration Combo1Configuration { get; set; }

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

		public CBCTabFInfo() : base(ShiftChildTabType.F)
		{
			Clipart1Configuration = new ClipartConfiguration();
			Clipart2Configuration = new ClipartConfiguration();

			Combo1Items = new List<ListDataItem>();
			Combo1Configuration = TextEditorConfiguration.Empty();

			SubHeader1Configuration = TextEditorConfiguration.Empty();
			SubHeader2Configuration = TextEditorConfiguration.Empty();
			SubHeader3Configuration = TextEditorConfiguration.Empty();
			SubHeader4Configuration = TextEditorConfiguration.Empty();
			SubHeader5Configuration = TextEditorConfiguration.Empty();
		}

		public override void LoadData(XmlNode configNode, ResourceManager resourceManager)
		{
			base.LoadData(configNode, resourceManager);

			RightLogo = resourceManager.LogoTab8SubFRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab8SubFRightFile.LocalPath)
				: null;
			FooterLogo = resourceManager.LogoTab8SubFFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab8SubFFooterFile.LocalPath)
				: null;
			BackgroundLogo = resourceManager.LogoTab8SubFBackgroundFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab8SubFBackgroundFile.LocalPath)
				: null;

			Clipart1Image = resourceManager.ClipartTab8SubF1File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab8SubF1File.LocalPath)
				: null;
			Clipart2Image = resourceManager.ClipartTab8SubF2File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab8SubF2File.LocalPath)
				: null;

			if (resourceManager.DataCBCPartFFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataCBCPartFFile.LocalPath);

				var node = document.SelectSingleNode(@"/SHIFT08F");
				if (node == null) return;
				foreach (XmlNode childNode in node.ChildNodes)
				{
					var item = ListDataItem.FromXml(childNode);
					switch (childNode.Name)
					{
						case "SHIFT08FHeader":
							if (!String.IsNullOrEmpty(item.Value))
								HeadersItems.Add(item);
							break;
						case "SHIFT08FCombo1":
							if (!String.IsNullOrEmpty(item.Value))
								Combo1Items.Add(item);
							break;
						case "SHIFT08FSubHeader1":
							if (item.IsPlaceholder)
								SubHeader1Placeholder = item.Value;
							else
								SubHeader1DefaultValue = item.Value;
							break;
						case "SHIFT08FSubHeader2":
							if (item.IsPlaceholder)
								SubHeader2Placeholder = item.Value;
							else
								SubHeader2DefaultValue = item.Value;
							break;
						case "SHIFT08FSubHeader3":
							if (item.IsPlaceholder)
								SubHeader3Placeholder = item.Value;
							else
								SubHeader3DefaultValue = item.Value;
							break;
						case "SHIFT08FSubHeader4":
							if (item.IsPlaceholder)
								SubHeader4Placeholder = item.Value;
							else
								SubHeader4DefaultValue = item.Value;
							break;
						case "SHIFT08FSubHeader5":
							if (item.IsPlaceholder)
								SubHeader5Placeholder = item.Value;
							else
								SubHeader5DefaultValue = item.Value;
							break;
					}
				}

				Clipart1Configuration = ClipartConfiguration.FromXml(node, "SHIFT08FClipart1");
				Clipart2Configuration = ClipartConfiguration.FromXml(node, "SHIFT08FClipart2");

				CommonEditorConfiguration = TextEditorConfiguration.FromXml(node);
				HeadersEditorConfiguration = TextEditorConfiguration.FromXml(node, "SHIFT08FHeader");
				Combo1Configuration = TextEditorConfiguration.FromXml(node, "SHIFT08FCombo1");
				SubHeader1Configuration = TextEditorConfiguration.FromXml(node, "SHIFT08FSubheader1");
				SubHeader2Configuration = TextEditorConfiguration.FromXml(node, "SHIFT08FSubheader2");
				SubHeader3Configuration = TextEditorConfiguration.FromXml(node, "SHIFT08FSubheader3");
				SubHeader4Configuration = TextEditorConfiguration.FromXml(node, "SHIFT08FSubheader4");
				SubHeader5Configuration = TextEditorConfiguration.FromXml(node, "SHIFT08FSubheader5");
			}
		}
	}
}
