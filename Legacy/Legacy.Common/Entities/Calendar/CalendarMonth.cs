using System;
using System.Xml;

namespace Asa.Legacy.Common.Entities.Calendar
{
	public abstract class CalendarMonth
	{
		protected DateTime _date;

		public DateTime DaysRangeBegin { get; set; }
		public DateTime DaysRangeEnd { get; set; }
		public CalendarOutputData OutputData { get; protected set; }

		public abstract DateTime Date { get; set; }

		public void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "Date":
						DateTime tempDate;
						if (DateTime.TryParse(childNode.InnerText, out tempDate))
							Date = tempDate;
						break;
					case "OutputData":
						OutputData.Deserialize(childNode);
						break;
				}
			}
		}
	}
}
