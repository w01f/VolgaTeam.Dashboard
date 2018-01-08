using System;
using System.Xml;

namespace Asa.Common.Core.Objects.TabPages
{
	public class RibbonTabPageConfig : TabPageConfig
	{
		public int Order { get; set; }
		public bool DefaultInQuickMode { get; set; }

		public override void Deserialize(XmlNode node)
		{
			base.Deserialize(node);
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "Order":
						{
							if (Int32.TryParse(childNode.InnerText, out var temp))
								Order = temp;
							break;
						}
					case "GitrdunDefault":
					{
						if (Boolean.TryParse(childNode.InnerText, out var temp))
							DefaultInQuickMode = temp;
						break;
					}
				}
			}
		}
	}
}
