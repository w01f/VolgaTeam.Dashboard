using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Shift.Enums;

namespace Asa.Business.Solutions.Shift.Configuration.NeedsSolutions
{
	public class NeedsSolutionsTabDInfo : ShiftTabWithHeaderInfo
	{
		public Image Clipart1Image { get; private set; }
		public ClipartConfiguration Clipart1Configuration { get; private set; }
		public Image Clipart2Image { get; private set; }
		public ClipartConfiguration Clipart2Configuration { get; private set; }
		public Image Clipart3Image { get; private set; }
		public ClipartConfiguration Clipart3Configuration { get; private set; }

		public List<SolutionsItemInfo> SolutionsList { get; }

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

		public NeedsSolutionsTabDInfo() : base(ShiftChildTabType.D)
		{
			Clipart1Configuration = new ClipartConfiguration();
			Clipart2Configuration = new ClipartConfiguration();
			Clipart3Configuration = new ClipartConfiguration();

			SolutionsList = new List<SolutionsItemInfo>();

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

			RightLogo = resourceManager.LogoTab7SubDRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab7SubDRightFile.LocalPath)
				: null;
			FooterLogo = resourceManager.LogoTab7SubDFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab7SubDFooterFile.LocalPath)
				: null;
			BackgroundLogo = resourceManager.LogoTab7SubDBackgroundFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab7SubDBackgroundFile.LocalPath)
				: null;

			Clipart1Image = resourceManager.ClipartTab7SubD1File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab7SubD1File.LocalPath)
				: null;
			Clipart2Image = resourceManager.ClipartTab7SubD2File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab7SubD2File.LocalPath)
				: null;
			Clipart3Image = resourceManager.ClipartTab7SubD3File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab7SubD3File.LocalPath)
				: null;

			if (resourceManager.DataSolutionsCommonFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataSolutionsCommonFile.LocalPath);

				var itemInfoNodes = document.SelectNodes("//Products/Product")?.OfType<XmlNode>().ToArray() ?? new XmlNode[] { };
				foreach (var itemInfoNode in itemInfoNodes)
					SolutionsList.Add(SolutionsItemInfo.FromXml(itemInfoNode, resourceManager.ClipartTab7SubCFolder));
			}

			if (resourceManager.DataNeedsSolutionsPartDFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataNeedsSolutionsPartDFile.LocalPath);

				var node = document.SelectSingleNode(@"/SHIFT07D");
				if (node == null) return;
				foreach (XmlNode childNode in node.ChildNodes)
				{
					var item = ListDataItem.FromXml(childNode);
					switch (childNode.Name)
					{
						case "SHIFT07DHeader":
							if (!String.IsNullOrEmpty(item.Value))
								HeadersItems.Add(item);
							break;
						case "SHIFT07DCombo1":
							if (item.IsPlaceholder)
								Combo1Placeholder = item.Value;
							break;
						case "SHIFT07DCombo2":
							if (item.IsPlaceholder)
								Combo2Placeholder = item.Value;
							break;
						case "SHIFT07DCombo3":
							if (item.IsPlaceholder)
								Combo3Placeholder = item.Value;
							break;
						case "SHIFT07DCombo4":
							if (item.IsPlaceholder)
								Combo4Placeholder = item.Value;
							break;
					}
				}

				Clipart1Configuration = ClipartConfiguration.FromXml(node, "SHIFT07DClipart1");
				Clipart2Configuration = ClipartConfiguration.FromXml(node, "SHIFT07DClipart2");
				Clipart3Configuration = ClipartConfiguration.FromXml(node, "SHIFT07DClipart3");

				CommonEditorConfiguration = TextEditorConfiguration.FromXml(node);
				HeadersEditorConfiguration = TextEditorConfiguration.FromXml(node, "SHIFT07DHeader");

				Combo1Configuration = TextEditorConfiguration.FromXml(node, "SHIFT07DCombo1");
				Combo2Configuration = TextEditorConfiguration.FromXml(node, "SHIFT07DCombo2");
				Combo3Configuration = TextEditorConfiguration.FromXml(node, "SHIFT07DCombo3");
				Combo4Configuration = TextEditorConfiguration.FromXml(node, "SHIFT07DCombo4");

				SubHeader1Configuration = TextEditorConfiguration.FromXml(node, "SHIFT07DLinkText1");
				SubHeader2Configuration = TextEditorConfiguration.FromXml(node, "SHIFT07DLinkText2");
				SubHeader3Configuration = TextEditorConfiguration.FromXml(node, "SHIFT07DLinkText3");
				SubHeader4Configuration = TextEditorConfiguration.FromXml(node, "SHIFT07DLinkText4");
			}
		}
	}
}
