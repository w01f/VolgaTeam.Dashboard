using System;
using System.Collections.Generic;
using System.Xml;

namespace Asa.Legacy.Common.Entities.Calendar
{
	public abstract class BaseCalendar
	{
		protected BaseCalendar()
		{
			Months = new List<CalendarMonth>();
			Days = new List<CalendarDay>();
			Notes = new List<CalendarNote>();
		}

		public List<CalendarMonth> Months { get; private set; }
		public List<CalendarDay> Days { get; private set; }
		public List<CalendarNote> Notes { get; private set; }

		public abstract void Deserialize(XmlNode node);

		protected void DeserializeInternal<TMonth, TDay, TNote>(XmlNode node)
			where TMonth : CalendarMonth
			where TDay : CalendarDay
			where TNote : CalendarNote
		{
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "Days":
						Days.Clear();
						foreach (XmlNode dayNode in childNode.ChildNodes)
						{
							var day = (TDay)Activator.CreateInstance(typeof(TDay));
							day.Deserialize(dayNode);
							Days.Add(day);
						}
						break;
					case "Months":
						Months.Clear();
						foreach (XmlNode monthNode in childNode.ChildNodes)
						{
							var month = (TMonth)Activator.CreateInstance(typeof(TMonth));
							month.Deserialize(monthNode);
							Months.Add(month);
						}
						break;
					case "CalendarNotes":
						Notes.Clear();
						foreach (XmlNode noteNode in childNode.ChildNodes)
						{
							var note = (TNote)Activator.CreateInstance(typeof(TNote));
							note.Deserialize(noteNode);
							if (note.StartDay != DateTime.MinValue && note.FinishDay != DateTime.MinValue)
								Notes.Add(note);
						}
						break;
				}
			}
		}
	}
}
