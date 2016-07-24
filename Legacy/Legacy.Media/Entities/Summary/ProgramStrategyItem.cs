using System;
using System.Xml;
using Asa.Legacy.Common.Entities.Common;

namespace Asa.Legacy.Media.Entities.Summary
{
	public class ProgramStrategyItem
	{
		private readonly StrategySummaryContent _parent;

		public bool Enabled { get; set; }
		public string Name { get; set; }
		public string Station { get; set; }
		public string Description { get; set; }
		public decimal Order { get; set; }

		private ImageSource _logo;
		public ImageSource Logo => _logo;

		public ProgramStrategyItem(StrategySummaryContent programStrategy)
		{
			_parent = programStrategy;
			_logo = new ImageSource();
		}

		public void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "Enabled":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								Enabled = temp;
							break;
						}
					case "Name":
						Name = childNode.InnerText;
						break;
					case "Station":
						Station = childNode.InnerText;
						break;
					case "Description":
						Description = childNode.InnerText;
						break;
					case "Order":
						{
							decimal temp;
							if (Decimal.TryParse(childNode.InnerText, out temp))
								Order = temp;
							break;
						}
					case "Logo":
						_logo.Deserialize(childNode);
						break;
				}
			}
		}
	}
}
