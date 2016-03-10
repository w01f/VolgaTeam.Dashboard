using System.Xml;

namespace Asa.Legacy.Common.Entities.Calendar
{
	public class CalendarMondayBased : BaseCalendar
	{
		public override void Deserialize(XmlNode node)
		{
			DeserializeInternal<CalendarMonthMondayBased, CalendarDayMondayBased, CalendarNote>(node);
		}
	}
}
