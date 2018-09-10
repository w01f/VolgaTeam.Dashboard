using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Shift.Enums;

namespace Asa.Business.Solutions.Shift.Configuration.NeedsSolutions
{
	public class NeedsSolutionsTabFInfo : ShiftTabWithHeaderInfo
	{
		public Image ListUpImage => _resourceManager.GraphicResources?.Tab7ListUp;
		public Image ListDownImage => _resourceManager.GraphicResources?.Tab7ListDown;
		public Image ListPopupImage => _resourceManager.GraphicResources?.Tab7ListPopup;
		public Image ListWipeImage => _resourceManager.GraphicResources?.Tab7ListWipe;

		public List<SolutionsItemInfo> SolutionsList { get; }

		public List<ListDataItem> Combo1Items { get; }
		public TextEditorConfiguration Combo1Configuration { get; set; }

		public SolutionsItemInfo Tab1DefaultItem { get; set; }
		public SolutionsItemInfo Tab2DefaultItem { get; set; }
		public SolutionsItemInfo Tab3DefaultItem { get; set; }
		public SolutionsItemInfo Tab4DefaultItem { get; set; }

		public string Tab1DefaultName { get; set; }
		public string Tab2DefaultName { get; set; }
		public string Tab3DefaultName { get; set; }
		public string Tab4DefaultName { get; set; }

		public TextEditorConfiguration SubHeader1Configuration { get; set; }
		public TextEditorConfiguration SubHeader2Configuration { get; set; }
		public TextEditorConfiguration SubHeader3Configuration { get; set; }
		public TextEditorConfiguration SubHeader4Configuration { get; set; }

		public FormListConfiguration FormListConfiguration { get; set; }

		public NeedsSolutionsTabFInfo() : base(ShiftChildTabType.F, ShiftTopTabType.IntegratedSolution)
		{
			SolutionsList = new List<SolutionsItemInfo>();

			Combo1Items = new List<ListDataItem>();
			Combo1Configuration = TextEditorConfiguration.Empty();

			SubHeader1Configuration = TextEditorConfiguration.Empty();
			SubHeader2Configuration = TextEditorConfiguration.Empty();
			SubHeader3Configuration = TextEditorConfiguration.Empty();
			SubHeader4Configuration = TextEditorConfiguration.Empty();

			FormListConfiguration = FormListConfiguration.Empty();
		}

		public override void LoadData(XmlNode configNode, ResourceManager resourceManager)
		{
			base.LoadData(configNode, resourceManager);

			if (resourceManager.DataSolutionsCommonFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataSolutionsCommonFile.LocalPath);

				var itemInfoNodes = document.SelectNodes("//Products/Product")?.OfType<XmlNode>().ToArray() ?? new XmlNode[] { };
				foreach (var itemInfoNode in itemInfoNodes)
					SolutionsList.Add(SolutionsItemInfo.FromXml(itemInfoNode, resourceManager.ClipartTab7SubCFolder));
			}

			if (resourceManager.DataNeedsSolutionsPartFFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataNeedsSolutionsPartFFile.LocalPath);

				var node = document.SelectSingleNode(@"/SHIFT07F");
				if (node == null) return;
				foreach (XmlNode childNode in node.ChildNodes)
				{
					var item = ListDataItem.FromXml(childNode);
					switch (childNode.Name)
					{
						case "SHIFT07FHeader":
							if (!String.IsNullOrEmpty(item.Value))
								HeadersItems.Add(item);
							break;
						case "SHIFT07FCombo1":
							if (!String.IsNullOrEmpty(item.Value))
								Combo1Items.Add(item);
							break;
						case "SHIFT07FTab1":
							Tab1DefaultItem = SolutionsList.FirstOrDefault(needsItem => String.Equals(
								needsItem.SubHeaderDefaultValue,
								childNode.Attributes?
									.OfType<XmlAttribute>()
									.FirstOrDefault(a => String.Equals(a.Name, "MultiBoxValue", StringComparison.OrdinalIgnoreCase))?.Value, StringComparison.OrdinalIgnoreCase));
							Tab1DefaultName = childNode.Attributes?
								.OfType<XmlAttribute>()
								.FirstOrDefault(a => String.Equals(a.Name, "TabName", StringComparison.OrdinalIgnoreCase))?.Value;
							break;
						case "SHIFT07FTab2":
							Tab2DefaultItem = SolutionsList.FirstOrDefault(needsItem => String.Equals(
								needsItem.SubHeaderDefaultValue,
								childNode.Attributes?
									.OfType<XmlAttribute>()
									.FirstOrDefault(a => String.Equals(a.Name, "MultiBoxValue", StringComparison.OrdinalIgnoreCase))?.Value, StringComparison.OrdinalIgnoreCase));
							Tab2DefaultName = childNode.Attributes?
								.OfType<XmlAttribute>()
								.FirstOrDefault(a => String.Equals(a.Name, "TabName", StringComparison.OrdinalIgnoreCase))?.Value;
							break;
						case "SHIFT07FTab3":
							Tab3DefaultItem = SolutionsList.FirstOrDefault(needsItem => String.Equals(
								needsItem.SubHeaderDefaultValue,
								childNode.Attributes?
									.OfType<XmlAttribute>()
									.FirstOrDefault(a => String.Equals(a.Name, "MultiBoxValue", StringComparison.OrdinalIgnoreCase))?.Value, StringComparison.OrdinalIgnoreCase));
							Tab3DefaultName = childNode.Attributes?
								.OfType<XmlAttribute>()
								.FirstOrDefault(a => String.Equals(a.Name, "TabName", StringComparison.OrdinalIgnoreCase))?.Value;
							break;
						case "SHIFT07FTab4":
							Tab4DefaultItem = SolutionsList.FirstOrDefault(needsItem => String.Equals(
								needsItem.SubHeaderDefaultValue,
								childNode.Attributes?
									.OfType<XmlAttribute>()
									.FirstOrDefault(a => String.Equals(a.Name, "MultiBoxValue", StringComparison.OrdinalIgnoreCase))?.Value, StringComparison.OrdinalIgnoreCase));
							Tab4DefaultName = childNode.Attributes?
								.OfType<XmlAttribute>()
								.FirstOrDefault(a => String.Equals(a.Name, "TabName", StringComparison.OrdinalIgnoreCase))?.Value;
							break;
					}
				}

				CommonEditorConfiguration = TextEditorConfiguration.FromXml(node);
				HeadersEditorConfiguration = TextEditorConfiguration.FromXml(node, "SHIFT07FHeader");

				Combo1Configuration = TextEditorConfiguration.FromXml(node, "SHIFT07FCombo1");

				SubHeader1Configuration = TextEditorConfiguration.FromXml(node, "Tab1MultiBox");
				SubHeader2Configuration = TextEditorConfiguration.FromXml(node, "Tab2MultiBox");
				SubHeader3Configuration = TextEditorConfiguration.FromXml(node, "Tab3MultiBox");
				SubHeader4Configuration = TextEditorConfiguration.FromXml(node, "Tab4MultiBox");

				FormListConfiguration = FormListConfiguration.FromXml(node);
			}
		}
	}
}
