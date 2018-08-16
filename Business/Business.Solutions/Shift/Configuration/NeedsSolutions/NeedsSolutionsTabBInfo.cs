using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Shift.Enums;

namespace Asa.Business.Solutions.Shift.Configuration.NeedsSolutions
{
	public class NeedsSolutionsTabBInfo : ShiftTabWithHeaderInfo
	{
		public Image Clipart1Image { get; private set; }
		public ClipartConfiguration Clipart1Configuration { get; private set; }
		public Image Clipart2Image { get; private set; }
		public ClipartConfiguration Clipart2Configuration { get; private set; }
		public Image Clipart3Image { get; private set; }
		public ClipartConfiguration Clipart3Configuration { get; private set; }

		public List<NeedsItemInfo> NeedsList { get; }

		public string Combo1Placeholder { get; private set; }
		public TextEditorConfiguration Combo1Configuration { get; set; }
		public string Combo2Placeholder { get; private set; }
		public TextEditorConfiguration Combo2Configuration { get; set; }
		public string Combo3Placeholder { get; private set; }
		public TextEditorConfiguration Combo3Configuration { get; set; }
		public string Combo4Placeholder { get; private set; }
		public TextEditorConfiguration Combo4Configuration { get; set; }

		public TextEditorConfiguration SubHeader1Configuration { get; set; }
		public TextEditorConfiguration SubHeader2Configuration { get; set; }
		public TextEditorConfiguration SubHeader3Configuration { get; set; }
		public TextEditorConfiguration SubHeader4Configuration { get; set; }

		public NeedsSolutionsTabBInfo() : base(ShiftChildTabType.B)
		{
			Clipart1Configuration = new ClipartConfiguration();
			Clipart2Configuration = new ClipartConfiguration();
			Clipart3Configuration = new ClipartConfiguration();

			NeedsList = new List<NeedsItemInfo>();

			Combo1Configuration = TextEditorConfiguration.Empty();
			Combo2Configuration = TextEditorConfiguration.Empty();
			Combo3Configuration = TextEditorConfiguration.Empty();
			Combo4Configuration = TextEditorConfiguration.Empty();

			SubHeader1Configuration = TextEditorConfiguration.Empty();
			SubHeader2Configuration = TextEditorConfiguration.Empty();
			SubHeader3Configuration = TextEditorConfiguration.Empty();
			SubHeader4Configuration = TextEditorConfiguration.Empty();
		}

		public override void LoadData(XmlNode configNode, ResourceManager resourceManager)
		{
			base.LoadData(configNode, resourceManager);

			RightLogo = resourceManager.LogoTab7SubBRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab7SubBRightFile.LocalPath)
				: null;
			FooterLogo = resourceManager.LogoTab7SubBFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab7SubBFooterFile.LocalPath)
				: null;
			BackgroundLogo = resourceManager.LogoTab7SubBBackgroundFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab7SubBBackgroundFile.LocalPath)
				: null;

			Clipart1Image = resourceManager.ClipartTab7SubB1File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab7SubB1File.LocalPath)
				: null;
			Clipart2Image = resourceManager.ClipartTab7SubB2File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab7SubB2File.LocalPath)
				: null;
			Clipart3Image = resourceManager.ClipartTab7SubB3File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab7SubB3File.LocalPath)
				: null;

			if (resourceManager.DataNeedsCommonFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataNeedsCommonFile.LocalPath);

				var itemInfoNodes = document.SelectNodes("//SHIFTNeeds/Need")?.OfType<XmlNode>().ToArray() ?? new XmlNode[] { };
				foreach (var itemInfoNode in itemInfoNodes)
					NeedsList.Add(NeedsItemInfo.FromXml(itemInfoNode, resourceManager.ClipartTab7SubAFolder));
			}

			if (resourceManager.DataNeedsSolutionsPartBFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataNeedsSolutionsPartBFile.LocalPath);

				var node = document.SelectSingleNode(@"/SHIFT07B");
				if (node == null) return;
				foreach (XmlNode childNode in node.ChildNodes)
				{
					var item = ListDataItem.FromXml(childNode);
					switch (childNode.Name)
					{
						case "SHIFT07BHeader":
							if (!String.IsNullOrEmpty(item.Value))
								HeadersItems.Add(item);
							break;
						case "SHIFT07BCombo1":
							if (item.IsPlaceholder)
								Combo1Placeholder = item.Value;
							break;
						case "SHIFT07BCombo2":
							if (item.IsPlaceholder)
								Combo2Placeholder = item.Value;
							break;
						case "SHIFT07BCombo3":
							if (item.IsPlaceholder)
								Combo3Placeholder = item.Value;
							break;
						case "SHIFT07BCombo4":
							if (item.IsPlaceholder)
								Combo4Placeholder = item.Value;
							break;
					}
				}

				Clipart1Configuration = ClipartConfiguration.FromXml(node, "SHIFT07BClipart1");
				Clipart2Configuration = ClipartConfiguration.FromXml(node, "SHIFT07BClipart2");
				Clipart3Configuration = ClipartConfiguration.FromXml(node, "SHIFT07BClipart3");

				CommonEditorConfiguration = TextEditorConfiguration.FromXml(node);
				HeadersEditorConfiguration = TextEditorConfiguration.FromXml(node, "SHIFT07BHeader");

				Combo1Configuration = TextEditorConfiguration.FromXml(node, "SHIFT07BCombo1");
				Combo2Configuration = TextEditorConfiguration.FromXml(node, "SHIFT07BCombo2");
				Combo3Configuration = TextEditorConfiguration.FromXml(node, "SHIFT07BCombo3");
				Combo4Configuration = TextEditorConfiguration.FromXml(node, "SHIFT07BCombo4");

				SubHeader1Configuration = TextEditorConfiguration.FromXml(node, "SHIFT07BLinkText1");
				SubHeader2Configuration = TextEditorConfiguration.FromXml(node, "SHIFT07BLinkText2");
				SubHeader3Configuration = TextEditorConfiguration.FromXml(node, "SHIFT07BLinkText3");
				SubHeader4Configuration = TextEditorConfiguration.FromXml(node, "SHIFT07BLinkText4");
			}
		}
	}
}
