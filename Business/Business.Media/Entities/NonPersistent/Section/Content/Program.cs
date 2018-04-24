using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using Asa.Business.Media.Configuration;
using Asa.Business.Media.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Interfaces;
using Asa.Common.Core.Objects.Images;
using Newtonsoft.Json;

namespace Asa.Business.Media.Entities.NonPersistent.Section.Content
{
	public class Program : IJsonCloneable<Program>
	{
		private string _name;
		private string _day;

		#region Basic Properties

		public ScheduleSection Parent { get; set; }
		public Guid UniqueID { get; set; }
		public decimal Index { get; set; }
		public ImageSource Logo { get; set; }
		public string Station { get; set; }
		public string Daypart { get; set; }
		public string Time { get; set; }
		public string Length { get; set; }
		public double? Rate { get; set; }
		public double? Rating { get; set; }
		public List<Spot> Spots { get; set; }
		#endregion

		#region Calculated Properties
		[JsonIgnore]
		public string Name
		{
			get { return _name; }
			set
			{
				string oldValue = _name;
				_name = value;
				if (string.IsNullOrEmpty(oldValue))
					ApplyDefaultValues();
			}
		}

		[JsonIgnore]
		public string Day
		{
			get { return _day; }
			set
			{
				_day = value;
				_weekDays = null;
			}
		}

		public Image SmallLogo => Logo?.TinyImage;

		public double CPP
		{
			get
			{
				double result = 0;
				if (Rate.HasValue && Rating.HasValue)
					if (Rating.Value != 0)
						result = Rate.Value / Rating.Value;
				return result;
			}
		}

		public double GRP => (Rating ?? 0) * TotalSpots * (Parent.ParentScheduleSettings.DemoType == DemoType.Rtg ? 1 : 1000);

		public double Cost => (Rate ?? 0) * TotalSpots;

		public int TotalSpots
		{
			get { return Spots.Select(x => x.Count ?? 0).Sum(); }
		}

		public Spot[] SpotsNotEmpty
		{
			get { return Spots.Where(x => Parent.Programs.Select(z => z.Spots.FirstOrDefault(y => y.Date.Equals(x.Date))).Sum(w => w.Count) > 0).ToArray(); }
		}

		private IEnumerable<DayOfWeek> _weekDays;
		public IEnumerable<DayOfWeek> WeekDays
		{
			get
			{
				if (_weekDays != null) return _weekDays;
				var weekdays = new List<DayOfWeek>();
				if (String.IsNullOrEmpty(Day))
				{
					_weekDays = weekdays;
					return weekdays;
				}
				var regexp = new Regex("(?<=[A-Z])(?=[A-Z][a-z])|(?=[A-Z])|(?<=[A-Za-z])(?=[^A-Za-z])");
				foreach (var weekPart in regexp.Split(Day))
				{
					if (new[] { "M", "Mo", "Mon", "Mn" }.Any(d => weekPart.Equals(d)))
						weekdays.Add(DayOfWeek.Monday);
					else if (new[] { "T", "Tu", "Tue", "Tues" }.Any(d => weekPart.Equals(d)))
						weekdays.Add(DayOfWeek.Tuesday);
					else if (new[] { "W", "We", "Wed", "Wd" }.Any(d => weekPart.Equals(d)))
						weekdays.Add(DayOfWeek.Wednesday);
					else if (new[] { "Th", "Thu", "Thur", "Tr", "Thurs" }.Any(d => weekPart.Equals(d)))
						weekdays.Add(DayOfWeek.Thursday);
					else if (new[] { "F", "Fr", "Fri" }.Any(d => weekPart.Equals(d)))
						weekdays.Add(DayOfWeek.Friday);
					else if (new[] { "Sa", "Sat", "St" }.Any(d => weekPart.Equals(d)))
						weekdays.Add(DayOfWeek.Saturday);
					else if (new[] { "Su", "Sun", "Sn" }.Any(d => weekPart.Equals(d)))
						weekdays.Add(DayOfWeek.Sunday);
				}
				_weekDays = weekdays;
				return _weekDays;
			}
		}

		public decimal SummaryOrder => Index;

		public string SummaryTitle
		{
			get
			{
				var result = new List<string>();
				if (Parent.ShowStation && !String.IsNullOrEmpty(Station))
					result.Add(Station);
				if (Parent.ShowProgram && !String.IsNullOrEmpty(Name))
					result.Add(Name);
				return String.Join("  -  ", result);
			}
		}

		public string SummaryInfo
		{
			get
			{
				var result = new List<string>();
				if (Parent.ShowDaypart && !String.IsNullOrEmpty(Daypart))
					result.Add(Daypart);
				if (Parent.ShowTime && !String.IsNullOrEmpty(Time))
					result.Add(Time);
				result.Add(String.Format("{0}x", Spots.Sum(sp => sp.Count)));
				return String.Join(", ", result);
			}
		}
		#endregion

		[JsonConstructor]
		private Program() { }

		public Program(ScheduleSection parent)
		{
			Parent = parent;
			UniqueID = Guid.NewGuid();
			Index = Parent.Programs.Count + 1;
			Logo = MediaMetaData.Instance.ListManager.Images.Where(g => g.IsDefault).Select(g => g.Images.FirstOrDefault(i => i.IsDefault)).FirstOrDefault()?.Clone<ImageSource, ImageSource>() ?? new ImageSource();
			Station = Parent.ParentScheduleSettings.Stations.Count(x => x.Available) == 1 ? Parent.ParentScheduleSettings.Stations.Where(x => x.Available).Select(x => x.Name).FirstOrDefault() : string.Empty;
			Daypart = string.Empty;
			Day = string.Empty;
			Time = string.Empty;
			Length = string.Empty;
			Spots = new List<Spot>();
		}

		public void Dispose()
		{
			Spots.ForEach(s => s.Dispose());
			Spots.Clear();

			Logo?.Dispose();
			Logo = null;

			Parent = null;
		}

		public void ApplyDefaultValues()
		{
			var source = MediaMetaData.Instance.ListManager.SourcePrograms.Where(x => x.Name.Equals(_name)).ToArray();
			if (source.Length <= 0) return;
			Daypart = source[0].Daypart;
			Day = source[0].Day;
			Time = source[0].Time;
		}

		public void RebuildSpots(bool keepExistedData = false)
		{
			if (!Parent.ParentScheduleSettings.FlightDateStart.HasValue || !Parent.ParentScheduleSettings.FlightDateEnd.HasValue) return;

			var startDate = Parent.ParentScheduleSettings.FlightDateStart.Value;
			var endDate = Parent.ParentScheduleSettings.FlightDateEnd.Value;
			var spotDate = startDate;

			var newSpots = new List<Spot>();
			while (spotDate < endDate)
			{
				var spot = new Spot(this) { Date = spotDate };
				spotDate = Parent.SpotType == SpotType.Week ? spotDate.AddDays(7) : new DateTime(spotDate.AddMonths(1).Year, spotDate.AddMonths(1).Month, 1);
				newSpots.Add(spot);
			}

			if (!(Spots.Any() && Spots.First().Date == startDate && Spots.Count == newSpots.Count))
			{
				if (keepExistedData)
				{
					if (Spots.Count >= newSpots.Count)
						for (int i = 0; i < newSpots.Count; i++)
							newSpots[i].Count = Spots[i].Count;
					else
						for (int i = 0; i < Spots.Count; i++)
							newSpots[i].Count = Spots[i].Count;
				}

				Spots.Clear();
				Spots.AddRange(newSpots);
			}
		}

		public void AfterClone(Program source, bool fullClone = true)
		{
			Parent = source.Parent;
			UniqueID = Guid.NewGuid();
			Spots.ForEach(spot =>
			{
				spot.AfterClone(source.Spots.First(sourceSpot => sourceSpot.Date == spot.Date), fullClone);
				spot.Parent = this;
			});
		}
	}
}
