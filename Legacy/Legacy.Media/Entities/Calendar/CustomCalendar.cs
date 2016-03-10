using System.Xml;
using Asa.Legacy.Common.Entities.Calendar;

namespace Asa.Legacy.Media.Entities.Calendar
{
	public class CustomCalendar : MediaCalendar
	{
		public override void Deserialize(XmlNode node)
		{
			DeserializeInternal<CalendarMonthMediaMondayBased, CalendarDayMondayBased, CommonCalendarNote>(node);
		}
	}
}
