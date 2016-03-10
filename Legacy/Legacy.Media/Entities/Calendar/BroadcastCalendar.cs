using System;
using System.Xml;
using Asa.Legacy.Common.Entities.Calendar;

namespace Asa.Legacy.Media.Entities.Calendar
{
	public class BroadcastCalendar : MediaCalendar
	{
		public BroadcastDataTypeEnum DataSourceType { get; set; }

		public override void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "DataSourceType":
						BroadcastDataTypeEnum temp;
						if (Enum.TryParse(childNode.InnerText, true, out temp))
							DataSourceType = temp;
						break;
				}
			}
			DeserializeInternal<CalendarMonthMediaMondayBased, CalendarDayMondayBased, MediaDataNote>(node);
		}
	}
}
