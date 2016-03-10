using System;
using System.Xml;

namespace Asa.Legacy.Common.Entities.Calendar
{
	public class CalendarSundayBased : BaseCalendar
	{
		public override void Deserialize(XmlNode node)
		{
			DeserializeInternal<CalendarMonthSundayBased, CalendarDaySundayBased, CommonCalendarNote>(node);
		}
	}
}
