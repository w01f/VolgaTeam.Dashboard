using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;

namespace Asa.Business.Solutions.Shift.Configuration.IntegratedSolution
{
	public class ResearchInfo : IProductToggleContainerInfo
	{
		public string Title { get; private set; }

		public Tab1Info Tab1 { get; private set; }

		private ResearchInfo()
		{
			Title = "research";
			Tab1 = Tab1Info.Empty();
		}

		public IList<IProductSubTabInfo> GetSubTabInfoList()
		{
			return new IProductSubTabInfo[] { Tab1 };
		}

		public static ResearchInfo FromXml(XmlNode configNode)
		{
			var researchInfo = Empty();

			researchInfo.Title = configNode.SelectSingleNode("./Title")?.InnerText ?? researchInfo.Title;

			foreach (var node in configNode.SelectNodes("./Tabs/Tab")?.OfType<XmlNode>().ToArray() ?? new XmlNode[] { })
			{
				var id = node.Attributes?.OfType<XmlAttribute>()
					.FirstOrDefault(a => String.Equals(a.Name, "Id", StringComparison.OrdinalIgnoreCase))?.Value?.ToUpper();
				switch (id)
				{
					case "DATA":
						researchInfo.Tab1 = Tab1Info.FromXml(node);
						break;
				}
			}
			return researchInfo;
		}

		public static ResearchInfo Empty()
		{
			return new ResearchInfo();
		}

		public class Tab1Info : IProductSubTabInfo
		{
			public string Title { get; private set; }

			public CheckboxInfo ToggleSwitch { get; private set; }
			public BundleInfo BundleInfo { get; private set; }

			public string Placeholder1 { get; set; }
			public string Placeholder2 { get; set; }
			public string Placeholder3 { get; set; }

			public TextEditorConfiguration Item1Configuration { get; set; }
			public TextEditorConfiguration Item2Configuration { get; set; }
			public TextEditorConfiguration Item3Configuration { get; set; }

			private Tab1Info()
			{
				Title = "data";
				ToggleSwitch = CheckboxInfo.Empty();
				BundleInfo = BundleInfo.Empty();

				Item1Configuration = TextEditorConfiguration.Empty();
				Item2Configuration = TextEditorConfiguration.Empty();
				Item3Configuration = TextEditorConfiguration.Empty();
			}

			public static Tab1Info FromXml(XmlNode configNode)
			{
				var tabInfo = Empty();
				if (configNode != null)
				{
					tabInfo.Title = configNode.SelectSingleNode("./Title")?.InnerText ?? tabInfo.Title;

					tabInfo.ToggleSwitch = CheckboxInfo.FromXml(configNode.SelectSingleNode("./ToggleSwitch"));

					tabInfo.BundleInfo = BundleInfo.FromXml(configNode);

					tabInfo.Placeholder1 = configNode.SelectSingleNode("./Item1Placeholder")?.InnerText;
					tabInfo.Placeholder2 = configNode.SelectSingleNode("./Item2Placeholder")?.InnerText;
					tabInfo.Placeholder3 = configNode.SelectSingleNode("./Item3Placeholder")?.InnerText;

					tabInfo.Item1Configuration = TextEditorConfiguration.FromXml(configNode, "BundleItem1");
					tabInfo.Item2Configuration = TextEditorConfiguration.FromXml(configNode, "BundleItem2");
					tabInfo.Item3Configuration = TextEditorConfiguration.FromXml(configNode, "BundleItem3");
				}
				return tabInfo;
			}

			public static Tab1Info Empty()
			{
				return new Tab1Info();
			}
		}

		public class BundleInfo
		{
			public List<BundleListItem> Items { get; }

			private BundleInfo()
			{
				Items = new List<BundleListItem>();
			}

			public static BundleInfo FromXml(XmlNode configNode)
			{
				var bundleInfo = Empty();
				if (configNode != null)
				{
					foreach (var node in configNode.SelectNodes("./BundleList/Bundle")?.OfType<XmlNode>().ToArray() ?? new XmlNode[] { })
						bundleInfo.Items.Add(BundleListItem.FromXml(node));
				}
				return bundleInfo;
			}

			public static BundleInfo Empty()
			{
				return new BundleInfo();
			}
		}

		public class BundleListItem
		{
			public string Value1 { get; set; }
			public string Value2 { get; set; }
			public string Value3 { get; set; }
			public bool IsDefault { get; set; }

			public static BundleListItem FromXml(XmlNode node)
			{
				var listDataItem = new BundleListItem();
				if (node != null)
				{
					listDataItem.IsDefault = Boolean.Parse(node.Attributes?.OfType<XmlAttribute>()
						.FirstOrDefault(a => String.Equals(a.Name, "IsDefault", StringComparison.OrdinalIgnoreCase))?.Value ?? "false");

					listDataItem.Value1 = node.SelectSingleNode("./Item1")?.InnerText;
					listDataItem.Value2 = node.SelectSingleNode("./Item2")?.InnerText;
					listDataItem.Value3 = node.SelectSingleNode("./Item3")?.InnerText;
				}
				return listDataItem;
			}

			public static bool Equals(BundleListItem item1, BundleListItem item2)
			{
				return String.Equals(item1?.Value1, item2?.Value1, StringComparison.OrdinalIgnoreCase) &&
					   String.Equals(item1?.Value2, item2?.Value2, StringComparison.OrdinalIgnoreCase) &&
					   String.Equals(item1?.Value3, item2?.Value3, StringComparison.OrdinalIgnoreCase);
			}

			public bool IsEmpty()
			{
				return String.IsNullOrWhiteSpace(Value1) &&
					   String.IsNullOrWhiteSpace(Value2) &&
					   String.IsNullOrWhiteSpace(Value3);
			}
		}
	}
}