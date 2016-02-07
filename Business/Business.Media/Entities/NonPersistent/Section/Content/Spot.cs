using System;
using System.Collections.Generic;
using System.Linq;
using Asa.Business.Media.Entities.NonPersistent.Schedule;
using Asa.Business.Media.Enums;
using Asa.Common.Core.Interfaces;
using Asa.Common.Core.Objects.Output;
using Newtonsoft.Json;

namespace Asa.Business.Media.Entities.NonPersistent.Section.Content
{
	public class Spot : IJsonCloneable<Spot>
	{
		public Program Parent { get; set; }

		public DateTime Date { get; set; }
		public int? Count { get; set; }

		private IEnumerable<DateTime> DateRange
		{
			get
			{
				var dateRange = new List<DateTime>();
				foreach (var dayOfWeek in Parent.WeekDays)
				{
					var date = Date;
					while (date.DayOfWeek != dayOfWeek)
						date = date.AddDays(1);
					dateRange.Add(date);
				}
				dateRange.Sort();
				return dateRange;
			}
		}

		public DateTime? StartDate
		{
			get
			{
				return DateRange.FirstOrDefault();
			}
		}

		public DateTime? EndDate
		{
			get
			{
				return DateRange.LastOrDefault();
			}
		}

		public string Name
		{
			get
			{
				switch (Parent.Parent.SpotType)
				{
					case SpotType.Month:
						return Date.ToString("MMMM");
					case SpotType.Week:
						return String.Format("{0} {1}", GetMonthAbbreviation(Date.Month), Date.Day.ToString("00"));
					default:
						return String.Empty;
				}
			}
		}

		public string ColumnName
		{
			get
			{
				if (Parent.Parent.UseGenericDateColumns)
					return String.Format("{0}{2}{1}",
						Parent.Parent.SpotType == SpotType.Week ? "wk" : "mo",
						Parent.Spots.IndexOf(this) + 1,
						Environment.NewLine);
				switch (Parent.Parent.SpotType)
				{
					case SpotType.Month:
						return GetMonthAbbreviation(Date.Month);
					case SpotType.Week:
						return String.Format("{0}\n{1}", GetMonthAbbreviation(Date.Month), Date.Day.ToString("00"));
					default:
						return String.Empty;
				}
			}
		}

		public string FullColumnName
		{
			get
			{
				switch (Parent.Parent.SpotType)
				{
					case SpotType.Week:
						return String.Format("Week {0}", Name);
					default:
						return Name;
				}
			}
		}

		public TextGroup FormattedString
		{
			get
			{
				var textGroup = new TextGroup(", ", "[", "]");
				if (!Count.HasValue) return textGroup;

				var programNameGroup = new TextGroup("  -  ");
				programNameGroup.Items.Add(new TextItem(Parent.Station, true));
				programNameGroup.Items.Add(new TextItem(Parent.Name, false));
				textGroup.Items.Add(programNameGroup);

				if (!String.IsNullOrEmpty(Parent.Daypart))
					textGroup.Items.Add(new TextItem(Parent.Daypart, false));

				if (!String.IsNullOrEmpty(Parent.Time))
					textGroup.Items.Add(new TextItem(Parent.Time, false));

				textGroup.Items.Add(new TextItem(String.Format("{0}x {1}", Count.Value, Parent.Day), false));

				return textGroup;
			}
		}

		public Quarter Quarter
		{
			get { return Parent.Parent.ParentScheduleSettings.Quarters.FirstOrDefault(q => q.DateStart <= Date && q.DateEnd >= Date); }
		}

		[JsonConstructor]
		private Spot() { }

		public Spot(Program parent)
		{
			Parent = parent;
		}

		public static string GetMonthAbbreviation(int monthNumber)
		{
			var result = string.Empty;
			switch (monthNumber)
			{
				case 1:
					result = "JA";
					break;
				case 2:
					result = "FE";
					break;
				case 3:
					result = "MR";
					break;
				case 4:
					result = "AP";
					break;
				case 5:
					result = "MY";
					break;
				case 6:
					result = "JN";
					break;
				case 7:
					result = "JL";
					break;
				case 8:
					result = "AU";
					break;
				case 9:
					result = "SE";
					break;
				case 10:
					result = "OC";
					break;
				case 11:
					result = "NV";
					break;
				case 12:
					result = "DE";
					break;
			}
			return result;
		}

		public void AfterClone(Spot source, bool fullClone)
		{
			Parent = source.Parent;
			if (!fullClone)
				Count = null;
		}

		public void Dispose()
		{
			Parent = null;
		}
	}
}
