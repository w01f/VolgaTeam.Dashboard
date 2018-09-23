using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Shift.Enums;

namespace Asa.Business.Solutions.Shift.Configuration.Approach
{
	public class ApproachTabCInfo : ShiftTabWithHeaderInfo
	{
		public List<ApproachItemInfo> ApproachItems { get; }

		public List<ListDataItem> Combo1Items { get; }
		public TextEditorConfiguration Combo1Configuration { get; set; }

		public ApproachItemInfo Tab1DefaultItem { get; set; }
		public ApproachItemInfo Tab2DefaultItem { get; set; }
		public ApproachItemInfo Tab3DefaultItem { get; set; }
		public ApproachItemInfo Tab4DefaultItem { get; set; }

		public string Tab1DefaultName { get; set; }
		public string Tab2DefaultName { get; set; }
		public string Tab3DefaultName { get; set; }
		public string Tab4DefaultName { get; set; }

		public TextEditorConfiguration SubHeader1Configuration { get; set; }
		public TextEditorConfiguration SubHeader2Configuration { get; set; }
		public TextEditorConfiguration SubHeader3Configuration { get; set; }
		public TextEditorConfiguration SubHeader4Configuration { get; set; }

		public FormListConfiguration FormListConfiguration { get; set; }

		public ApproachTabCInfo() : base(ShiftChildTabType.C, ShiftTopTabType.Approach)
		{
			ApproachItems = new List<ApproachItemInfo>();

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

			if (resourceManager.DataApproachesCommonFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataApproachesCommonFile.LocalPath);

				var itemInfoNodes = document.SelectNodes("//OurApproach/Approach")?.OfType<XmlNode>().ToArray() ?? new XmlNode[] { };
				foreach (var itemInfoNode in itemInfoNodes)
					ApproachItems.Add(ApproachItemInfo.FromXml(itemInfoNode, resourceManager.ClipartTab15SubAFolder));
			}

			if (resourceManager.DataApproachPartCFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataApproachPartCFile.LocalPath);

				var node = document.SelectSingleNode(@"/SHIFT09C");
				if (node == null) return;
				foreach (XmlNode childNode in node.ChildNodes)
				{
					var item = ListDataItem.FromXml(childNode);
					switch (childNode.Name)
					{
						case "SHIFT09CHeader":
							if (!String.IsNullOrEmpty(item.Value))
								HeadersItems.Add(item);
							break;
						case "SHIFT09CCombo1":
							if (!String.IsNullOrEmpty(item.Value))
								Combo1Items.Add(item);
							break;
						case "SHIFT09CTab1":
							Tab1DefaultItem = ApproachItems.FirstOrDefault(needsItem => String.Equals(
								needsItem.SubHeaderDefaultValue,
								childNode.Attributes?
									.OfType<XmlAttribute>()
									.FirstOrDefault(a => String.Equals(a.Name, "MultiBoxValue", StringComparison.OrdinalIgnoreCase))?.Value, StringComparison.OrdinalIgnoreCase));
							Tab1DefaultName = childNode.Attributes?
								.OfType<XmlAttribute>()
								.FirstOrDefault(a => String.Equals(a.Name, "TabName", StringComparison.OrdinalIgnoreCase))?.Value;
							break;
						case "SHIFT09CTab2":
							Tab2DefaultItem = ApproachItems.FirstOrDefault(needsItem => String.Equals(
								needsItem.SubHeaderDefaultValue,
								childNode.Attributes?
									.OfType<XmlAttribute>()
									.FirstOrDefault(a => String.Equals(a.Name, "MultiBoxValue", StringComparison.OrdinalIgnoreCase))?.Value, StringComparison.OrdinalIgnoreCase));
							Tab2DefaultName = childNode.Attributes?
								.OfType<XmlAttribute>()
								.FirstOrDefault(a => String.Equals(a.Name, "TabName", StringComparison.OrdinalIgnoreCase))?.Value;
							break;
						case "SHIFT09CTab3":
							Tab3DefaultItem = ApproachItems.FirstOrDefault(needsItem => String.Equals(
								needsItem.SubHeaderDefaultValue,
								childNode.Attributes?
									.OfType<XmlAttribute>()
									.FirstOrDefault(a => String.Equals(a.Name, "MultiBoxValue", StringComparison.OrdinalIgnoreCase))?.Value, StringComparison.OrdinalIgnoreCase));
							Tab3DefaultName = childNode.Attributes?
								.OfType<XmlAttribute>()
								.FirstOrDefault(a => String.Equals(a.Name, "TabName", StringComparison.OrdinalIgnoreCase))?.Value;
							break;
						case "SHIFT09CTab4":
							Tab4DefaultItem = ApproachItems.FirstOrDefault(needsItem => String.Equals(
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
				HeadersEditorConfiguration = TextEditorConfiguration.FromXml(node, "SHIFT09CHeader");

				Combo1Configuration = TextEditorConfiguration.FromXml(node, "SHIFT09CCombo1");

				SubHeader1Configuration = TextEditorConfiguration.FromXml(node, "Tab1MultiBox");
				SubHeader2Configuration = TextEditorConfiguration.FromXml(node, "Tab2MultiBox");
				SubHeader3Configuration = TextEditorConfiguration.FromXml(node, "Tab3MultiBox");
				SubHeader4Configuration = TextEditorConfiguration.FromXml(node, "Tab4MultiBox");

				FormListConfiguration = FormListConfiguration.FromXml(node);
			}
		}
	}
}
