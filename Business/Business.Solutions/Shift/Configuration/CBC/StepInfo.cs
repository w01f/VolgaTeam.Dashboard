using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;

namespace Asa.Business.Solutions.Shift.Configuration.CBC
{
	public class StepInfo
	{
		public int Index { get; private set; }
		public string Title { get; private set; }

		public List<ListDataItem> Products { get; }

		private StepInfo()
		{
			Products = new List<ListDataItem>();
		}

		public static StepInfo FromXml(XmlNode configNode)
		{
			var stepInfo = new StepInfo();

			stepInfo.Index = Int32.Parse(configNode.SelectSingleNode("./Tab")?.InnerText ?? "0");
			stepInfo.Title = configNode.SelectSingleNode("./Name")?.InnerText;

			foreach (var productNode in (configNode.SelectNodes("./Product")?.OfType<XmlNode>() ?? new XmlNode[] { }).ToList())
				stepInfo.Products.Add(ListDataItem.FromString(productNode.InnerText));

			return stepInfo;
		}
	}
}
