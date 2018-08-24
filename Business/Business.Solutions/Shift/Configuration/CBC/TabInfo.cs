using System;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;

namespace Asa.Business.Solutions.Shift.Configuration.CBC
{
	public class TabInfo
	{
		public StepInfo StepInfo { get; }
		public string Title { get; private set; }
		public ListDataItem[] ComboDefaultItems { get; }

		public TextEditorConfiguration ComboConfiguration { get; set; }

		private TabInfo(StepInfo stepInfo)
		{
			StepInfo = stepInfo;
			ComboDefaultItems = new ListDataItem[12];
			ComboConfiguration = TextEditorConfiguration.Empty();
		}

		public static TabInfo FromXml(
			XmlNode configNode,
			string parentConfigPrefix,
			StepInfo stepInfo)
		{
			var tabInfo = new TabInfo(stepInfo);

			tabInfo.Title = configNode.SelectSingleNode(String.Format("./{0}TabNames/Tab{1}", parentConfigPrefix, tabInfo.StepInfo.Index))?.InnerText;

			for (var i = 0; i < tabInfo.ComboDefaultItems.Length; i++)
			{
				var listDataItemNode = configNode.SelectSingleNode(String.Format("./{0}TAB{1}COMBO{2}",
					parentConfigPrefix,
					tabInfo.StepInfo.Index,
					i + 1));
				if (listDataItemNode != null)
					tabInfo.ComboDefaultItems[i] = ListDataItem.FromXml(listDataItemNode);
			}

			tabInfo.ComboConfiguration = TextEditorConfiguration.FromXml(configNode, String.Format("Tab{0}Combos", tabInfo.StepInfo.Index));

			return tabInfo;
		}
	}
}
