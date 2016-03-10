using System;
using System.Xml;

namespace Asa.Legacy.Media.Entities.Section
{
	public class Spot
	{
		private readonly Program _parent;

		public Spot(Program parent)
		{
			_parent = parent;
		}

		public DateTime Date { get; set; }
		public int? Count { get; set; }

		public void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "Date":
						DateTime tempDateTime;
						if (DateTime.TryParse(childNode.InnerText, out tempDateTime))
							Date = tempDateTime;
						break;
					case "Count":
						int tempInt;
						if (int.TryParse(childNode.InnerText, out tempInt))
							Count = tempInt;
						break;
				}
			}
		}
	}
}
