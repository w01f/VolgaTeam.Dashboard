using System;
using System.Collections.Generic;
using System.Xml;
using Asa.Legacy.Common.Entities.Digital;
using Asa.Legacy.Media.Entities.Schedule;

namespace Asa.Legacy.Media.Entities.Section
{
	public abstract class ProgramSchedule
	{
		public RegularSchedule Parent { get; private set; }
		public DateTime? SelectedQuarter { get; set; }
		public DigitalLegend DigitalLegend { get; set; }
		public bool ApplySettingsForAll { get; set; }
		public List<ScheduleSection> Sections { get; private set; }

		public abstract int TotalPeriods { get; }

		protected ProgramSchedule(RegularSchedule parent)
		{
			Parent = parent;
			DigitalLegend = new DigitalLegend();
			Sections = new List<ScheduleSection>();
		}

		public abstract ScheduleSection CreateSection();

		public static ProgramSchedule Create(RegularSchedule parent)
		{
			switch (parent.SelectedSpotType)
			{
				case SpotType.Week:
					return new WeekSchedule(parent);
				case SpotType.Month:
					return new MonthSchedule(parent);
				default:
					return null;
			}
		}

		public void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
				switch (childNode.Name)
				{
					case "SelectedQuarter":
						{
							DateTime temp;
							if (DateTime.TryParse(childNode.InnerText, out temp))
								SelectedQuarter = temp;
						}
						break;
					case "DigitalLegend":
						DigitalLegend.Deserialize(childNode);
						break;
					case "ApplySettingsForAll":
						{
							bool temp;
							if (bool.TryParse(childNode.InnerText, out temp))
								ApplySettingsForAll = temp;
						}
						break;
					case "Section":
						var section = CreateSection();
						section.Deserialize(childNode);
						Sections.Add(section);
						break;
				}
		}

		public void DeserializeSection(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
				switch (childNode.Name)
				{
					case "DigitalLegend":
						DigitalLegend.Deserialize(childNode);
						break;
				}
			var section = CreateSection();
			section.Name = Parent.Name;
			section.Deserialize(node);
			Sections.Add(section);
		}
	}
}
