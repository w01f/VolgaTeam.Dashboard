using System;
using System.Xml;

namespace Asa.Legacy.Common.Entities.Common
{
	public class DateRange
	{
		public DateTime? StartDate { get; set; }
		public DateTime? FinishDate { get; set; }

		public void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
				switch (childNode.Name)
				{
					case "StartDate":
						{
							DateTime temp;
							if (DateTime.TryParse(childNode.InnerText, out temp))
								StartDate = temp;
						}
						break;
					case "FinishDate":
						{
							DateTime temp;
							if (DateTime.TryParse(childNode.InnerText, out temp))
								FinishDate = temp;
						}
						break;
				}
		}
	}
}
