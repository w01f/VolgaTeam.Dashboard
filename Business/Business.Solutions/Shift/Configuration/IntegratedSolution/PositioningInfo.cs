using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;

namespace Asa.Business.Solutions.Shift.Configuration.IntegratedSolution
{
	public class PositioningInfo : IProductToggleContainerInfo
	{
		public string Title { get; private set; }

		public Tab1Info Tab1 { get; private set; }
		public Tab2Info Tab2 { get; private set; }

		private PositioningInfo()
		{
			Title = "positioning";
			Tab1 = Tab1Info.Empty();
			Tab2 = Tab2Info.Empty();
		}

		public IList<IProductSubTabInfo> GetSubTabInfoList()
		{
			return new IProductSubTabInfo[] { Tab1, Tab2 };
		}

		public static PositioningInfo FromXml(XmlNode configNode)
		{
			var positioningInfo = Empty();

			positioningInfo.Title = configNode.SelectSingleNode("./Title")?.InnerText?? positioningInfo.Title;

			foreach (var node in configNode.SelectNodes("./Tabs/Tab")?.OfType<XmlNode>().ToArray() ?? new XmlNode[] { })
			{
				var id = node.Attributes?.OfType<XmlAttribute>()
					.FirstOrDefault(a => String.Equals(a.Name, "Id", StringComparison.OrdinalIgnoreCase))?.Value?.ToUpper();
				switch (id)
				{
					case "STATEMENTS":
						positioningInfo.Tab1 = Tab1Info.FromXml(node);
						break;
					case "BULLETPOINTS":
						positioningInfo.Tab2 = Tab2Info.FromXml(node);
						break;
				}
			}
			return positioningInfo;
		}

		public static PositioningInfo Empty()
		{
			return new PositioningInfo();
		}

		public class Tab1Info : IProductSubTabInfo
		{
			public string Title { get; private set; }

			public CheckboxInfo ComboCheckbox1 { get; private set; }
			public List<ListDataItem> Combo1Items { get; }
			public CheckboxInfo MemoPopupCheckbox1 { get; private set; }
			public List<ListDataItem> MemoPopup1Items { get; }
			public TextEditorConfiguration Combo1Configuration { get; set; }
			public TextEditorConfiguration MemoPopup1Configuration { get; set; }

			public CheckboxInfo ComboCheckbox2 { get; private set; }
			public List<ListDataItem> Combo2Items { get; }
			public CheckboxInfo MemoPopupCheckbox2 { get; private set; }
			public List<ListDataItem> MemoPopup2Items { get; }
			public TextEditorConfiguration Combo2Configuration { get; set; }
			public TextEditorConfiguration MemoPopup2Configuration { get; set; }

			private Tab1Info()
			{
				Title = "statements";
				ComboCheckbox1 = CheckboxInfo.Empty();
				Combo1Items = new List<ListDataItem>();
				MemoPopupCheckbox1 = CheckboxInfo.Empty();
				MemoPopup1Items = new List<ListDataItem>();
				Combo1Configuration = TextEditorConfiguration.Empty();
				MemoPopup1Configuration = TextEditorConfiguration.Empty();

				ComboCheckbox2 = CheckboxInfo.Empty();
				Combo2Items = new List<ListDataItem>();
				MemoPopupCheckbox2 = CheckboxInfo.Empty();
				MemoPopup2Items = new List<ListDataItem>();
				Combo2Configuration = TextEditorConfiguration.Empty();
				MemoPopup2Configuration = TextEditorConfiguration.Empty();
			}

			public static Tab1Info FromXml(XmlNode configNode)
			{
				var tabInfo = Empty();
				if (configNode != null)
				{
					tabInfo.Title = configNode.SelectSingleNode("./Title")?.InnerText?? tabInfo.Title;

					tabInfo.ComboCheckbox1 = CheckboxInfo.FromXml(configNode.SelectSingleNode("./Checkbox1"));

					foreach (var node in configNode.SelectNodes("./Combo1")?.OfType<XmlNode>().ToArray() ?? new XmlNode[] { })
						tabInfo.Combo1Items.Add(ListDataItem.FromXml(node));

					tabInfo.MemoPopupCheckbox1 = CheckboxInfo.FromXml(configNode.SelectSingleNode("./MultiBox1Checkbox1"));

					foreach (var node in configNode.SelectNodes("./MultiBox1")?.OfType<XmlNode>().ToArray() ?? new XmlNode[] { })
						tabInfo.MemoPopup1Items.Add(ListDataItem.FromXml(node));

					tabInfo.ComboCheckbox2 = CheckboxInfo.FromXml(configNode.SelectSingleNode("./Checkbox2"));

					foreach (var node in configNode.SelectNodes("./Combo2")?.OfType<XmlNode>().ToArray() ?? new XmlNode[] { })
						tabInfo.Combo2Items.Add(ListDataItem.FromXml(node));

					tabInfo.MemoPopupCheckbox2 = CheckboxInfo.FromXml(configNode.SelectSingleNode("./MultiBox2Checkbox2"));

					foreach (var node in configNode.SelectNodes("./MultiBox2")?.OfType<XmlNode>().ToArray() ?? new XmlNode[] { })
						tabInfo.MemoPopup2Items.Add(ListDataItem.FromXml(node));

					tabInfo.Combo1Configuration = TextEditorConfiguration.FromXml(configNode, "Combo1");
					tabInfo.MemoPopup1Configuration = TextEditorConfiguration.FromXml(configNode, "Multibox1");

					tabInfo.Combo2Configuration = TextEditorConfiguration.FromXml(configNode, "Combo2");
					tabInfo.MemoPopup2Configuration = TextEditorConfiguration.FromXml(configNode, "Multibox2");
				}
				return tabInfo;
			}

			public static Tab1Info Empty()
			{
				return new Tab1Info();
			}
		}

		public class Tab2Info : IProductSubTabInfo
		{
			public string Title { get; private set; }

			public CheckboxInfo Checkbox1 { get; private set; }
			public List<ListDataItem> Combo1Items { get; }
			public TextEditorConfiguration Combo1Configuration { get; set; }

			public List<ListDataItem> BulletCombo1Items { get; }
			public List<ListDataItem> BulletCombo2Items { get; }
			public List<ListDataItem> BulletCombo3Items { get; }
			public List<ListDataItem> BulletCombo4Items { get; }
			public List<ListDataItem> BulletCombo5Items { get; }
			public List<ListDataItem> BulletCombo6Items { get; }
			public List<ListDataItem> BulletCombo7Items { get; }
			public List<ListDataItem> BulletCombo8Items { get; }
			public TextEditorConfiguration BulletComboConfiguration { get; set; }

			private Tab2Info()
			{
				Title = "bulletpoints";
				Checkbox1 = CheckboxInfo.Empty();
				Combo1Items = new List<ListDataItem>();
				Combo1Configuration = TextEditorConfiguration.Empty();

				BulletCombo1Items = new List<ListDataItem>();
				BulletCombo2Items = new List<ListDataItem>();
				BulletCombo3Items = new List<ListDataItem>();
				BulletCombo4Items = new List<ListDataItem>();
				BulletCombo5Items = new List<ListDataItem>();
				BulletCombo6Items = new List<ListDataItem>();
				BulletCombo7Items = new List<ListDataItem>();
				BulletCombo8Items = new List<ListDataItem>();
				BulletComboConfiguration = TextEditorConfiguration.Empty();
			}

			public static Tab2Info FromXml(XmlNode configNode)
			{
				var tabInfo = Empty();
				if (configNode != null)
				{
					tabInfo.Title = configNode.SelectSingleNode("./Title")?.InnerText?? tabInfo.Title;

					tabInfo.Checkbox1 = CheckboxInfo.FromXml(configNode.SelectSingleNode("./Checkbox1"));

					foreach (var node in configNode.SelectNodes("./Combo1")?.OfType<XmlNode>().ToArray() ?? new XmlNode[] { })
						tabInfo.Combo1Items.Add(ListDataItem.FromXml(node));

					var bulletComboLists = new[]
					{
						tabInfo.BulletCombo1Items,
						tabInfo.BulletCombo2Items,
						tabInfo.BulletCombo3Items,
						tabInfo.BulletCombo4Items,
						tabInfo.BulletCombo5Items,
						tabInfo.BulletCombo6Items,
						tabInfo.BulletCombo7Items,
						tabInfo.BulletCombo8Items
					};

					for (var i = 0; i < bulletComboLists.Length; i++)
					{
						var defaultItemNode = configNode.SelectSingleNode(String.Format("./BulletCombo{0}", i + 1));
						if (defaultItemNode == null)
							continue;
						bulletComboLists[i].Add(ListDataItem.FromXml(defaultItemNode));
					}

					foreach (var node in configNode.SelectNodes("./BulletList/Item")?.OfType<XmlNode>().ToArray() ?? new XmlNode[] { })
					{
						var nodeValue = node.InnerText;
						foreach (var bulletComboList in bulletComboLists)
							if (!bulletComboList.Any(item => String.Equals(nodeValue, item.Value, StringComparison.OrdinalIgnoreCase)))
								bulletComboList.Add(ListDataItem.FromString(nodeValue));
					}

					tabInfo.Combo1Configuration = TextEditorConfiguration.FromXml(configNode, "Combo1");
					tabInfo.BulletComboConfiguration = TextEditorConfiguration.FromXml(configNode, "BulletCombos");
				}
				return tabInfo;
			}

			public static Tab2Info Empty()
			{
				return new Tab2Info();
			}
		}
	}
}
