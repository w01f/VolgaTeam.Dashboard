using System;
using System.Collections.Generic;
using System.Linq;
using Asa.Business.Common.Entities.NonPersistent.Schedule;
using Asa.Business.Common.Interfaces;
using Asa.Business.Media.Configuration;
using Asa.Business.Media.Enums;
using Asa.Business.Online.Configuration;
using Asa.Business.Online.Interfaces;
using Newtonsoft.Json;

namespace Asa.Business.Media.Entities.NonPersistent.Schedule
{
	public class MediaScheduleSettings : BaseScheduleSettings,
		IDigitalScheduleSettings,
		IChangableScheduleSettings<MediaScheduleSettings, MediaScheduleChangeInfo>
	{
		public bool MondayBased { get; set; }
		public bool UseDemo { get; set; }
		public bool ImportDemo { get; set; }
		public string Demo { get; set; }
		public string Source { get; set; }
		public DemoType DemoType { get; set; }

		public SpotType SelectedSpotType { get; set; }
		public List<Daypart> Dayparts { get; private set; }
		public List<Station> Stations { get; private set; }

		public DigitalProductListViewSettings HomeViewSettings { get; private set; }
		public DigitalPackageSettings DigitalPackageSettings { get; private set; }

		[JsonIgnore]
		public List<Quarter> Quarters { get; private set; }

		[JsonIgnore]
		public DigitalProductListViewSettings DigitalProductListViewSettings
		{
			get { return HomeViewSettings; }
		}


		private DateTime? _userFlightDateStart;

		[JsonIgnore]
		public DateTime? UserFlightDateStart
		{
			get
			{
				return _userFlightDateStart;
			}
			set
			{
				_userFlightDateStart = value;
				FlightDateStart = value;
				if (!FlightDateStart.HasValue) return;
				while (FlightDateStart.Value.DayOfWeek != StartDayOfWeek)
					FlightDateStart = FlightDateStart.Value.AddDays(-1);
			}
		}

		private DateTime? _userFlightDateEnd;

		[JsonIgnore]
		public DateTime? UserFlightDateEnd
		{
			get
			{
				return _userFlightDateEnd;
			}
			set
			{
				_userFlightDateEnd = value;
				FlightDateEnd = value;
				if (!FlightDateEnd.HasValue) return;
				while (FlightDateEnd.Value.DayOfWeek != EndDayOfWeek)
					FlightDateEnd = FlightDateEnd.Value.AddDays(1);
			}
		}

		public override string FlightDates
		{
			get
			{
				if (UserFlightDateStart.HasValue && UserFlightDateEnd.HasValue)
					return UserFlightDateStart.Value.ToString("MM/dd/yy") + " - " + UserFlightDateEnd.Value.ToString("MM/dd/yy");
				return string.Empty;
			}
		}

		public DayOfWeek StartDayOfWeek
		{
			get { return MondayBased ? DayOfWeek.Monday : DayOfWeek.Sunday; }
		}

		public DayOfWeek EndDayOfWeek
		{
			get { return MondayBased ? DayOfWeek.Sunday : DayOfWeek.Saturday; }
		}

		public string StartWeekDays
		{
			get
			{
				if (!UserFlightDateStart.HasValue || UserFlightDateStart.Value.DayOfWeek == StartDayOfWeek) return String.Empty;
				var list = new List<string>();
				var date = UserFlightDateStart.Value;
				while (date.DayOfWeek != StartDayOfWeek)
				{
					list.Add(date.ToString("ddd"));
					date = date.AddDays(1);
				}
				return String.Join(", ", list);
			}
		}

		public string EndWeekDays
		{
			get
			{
				if (!UserFlightDateEnd.HasValue || UserFlightDateEnd.Value.DayOfWeek == EndDayOfWeek) return String.Empty;
				var list = new List<string>();
				var date = UserFlightDateEnd.Value;
				while (date.DayOfWeek != EndDayOfWeek)
				{
					list.Add(date.ToString("ddd"));
					date = date.AddDays(-1);
				}
				list.Reverse();
				return String.Join(", ", list);
			}
		}

		[JsonIgnore]
		public string DisplayedSpotType
		{
			get { return String.Format("{0}ly", SelectedSpotType); }
			set
			{
				SpotType temp;
				if (!String.IsNullOrEmpty(value) && Enum.TryParse(value.Replace("ly", ""), true, out temp))
					SelectedSpotType = temp;
				else
					SelectedSpotType = SpotType.Week;
			}
		}

		public MediaScheduleSettings()
		{
			Dayparts = new List<Daypart>();
			Stations = new List<Station>();

			HomeViewSettings = new DigitalProductListViewSettings();
			DigitalPackageSettings = new DigitalPackageSettings();
		}

		public override void Dispose()
		{
			Dayparts.ForEach(dp => dp.Dispose());
			Dayparts.Clear();

			Stations.ForEach(dp => dp.Dispose());
			Stations.Clear();

			base.Dispose();
		}

		protected override void AfterConstruction()
		{
			base.AfterConstruction();
			Status = MediaMetaData.Instance.ListManager.Statuses.FirstOrDefault();
			PresentationDate = DateTime.Now;
			DemoType = DemoType.Imp;
			MondayBased = true;

			DigitalProductListViewSettings.ResetToDefault();
			DigitalPackageSettings.ResetToDefault();
		}

		protected override void AfterCreate()
		{
			base.AfterCreate();

			Dayparts.AddRange(MediaMetaData.Instance.ListManager.Dayparts.Where(x => !Dayparts.Select(y => y.Name).Contains(x.Name)));
			Stations.AddRange(MediaMetaData.Instance.ListManager.Stations.Where(x => !Stations.Select(y => y.Name).Contains(x.Name)));

			Quarters = new List<Quarter>();
			LoadQuarters();
		}

		public void CompareChanges(MediaScheduleSettings changedInstance, MediaScheduleChangeInfo changeInfo)
		{
			changeInfo.SpotTypeChanged = changeInfo.SpotTypeChanged || SelectedSpotType != changedInstance.SelectedSpotType;
			changeInfo.CalendarTypeChanged = changeInfo.CalendarTypeChanged || MondayBased != changedInstance.MondayBased;
			changeInfo.ScheduleDatesChanged = changeInfo.ScheduleDatesChanged || (UserFlightDateStart.HasValue &&
				UserFlightDateEnd.HasValue &&
				(UserFlightDateStart != changedInstance.UserFlightDateStart || UserFlightDateEnd != changedInstance.UserFlightDateEnd));
		}

		public static Int32? CalcWeeksCount(DateTime? UserFlightDateStart, DateTime? UserFlightDateEnd, DayOfWeek startDayOfWeek, DayOfWeek endDayOfWeek)
		{
			if (!UserFlightDateStart.HasValue || !UserFlightDateEnd.HasValue)
				return null;
			var startDate = UserFlightDateStart.Value;
			while (startDate.DayOfWeek != startDayOfWeek)
				startDate = startDate.AddDays(-1);
			var endDate = UserFlightDateEnd.Value;
			while (endDate.DayOfWeek != endDayOfWeek)
				endDate = endDate.AddDays(1);
			var datesRange = endDate - startDate;
			return datesRange.Days / 7 + 1;
		}

		private void LoadQuarters()
		{
			if (!FlightDateStart.HasValue || !FlightDateEnd.HasValue) return;
			Quarters.Clear();
			var targetMonths = (MondayBased ?
				MediaMetaData.Instance.ListManager.MonthTemplatesMondayBased :
				MediaMetaData.Instance.ListManager.MonthTemplatesSundayBased)
					.Where(m => (m.StartDate <= FlightDateStart && m.EndDate >= FlightDateStart) ||
						(m.StartDate <= FlightDateEnd && m.EndDate >= FlightDateEnd) ||
						(m.StartDate >= FlightDateStart && m.EndDate <= FlightDateEnd)).ToList();
			if (!targetMonths.Any()) return;
			var startDate = FlightDateStart.Value;
			if (startDate.Month >= 1 && startDate.Month <= 3)
				startDate = new DateTime(startDate.Year, 1, 1);
			else if (startDate.Month >= 4 && startDate.Month <= 6)
				startDate = new DateTime(startDate.Year, 4, 1);
			else if (startDate.Month >= 7 && startDate.Month <= 9)
				startDate = new DateTime(startDate.Year, 7, 1);
			else if (startDate.Month >= 10 && startDate.Month <= 12)
				startDate = new DateTime(startDate.Year, 10, 1);
			while (startDate <= FlightDateEnd.Value)
			{
				var endDate = startDate.AddMonths(3);
				var quarter = new Quarter { DateAnchor = startDate };
				var quarterMonths = targetMonths.Where(m => (m.StartDate.Value.Day < 15 && m.StartDate.Value >= startDate && m.StartDate <= endDate) ||
					(m.StartDate.Value.Day > 15 && m.EndDate >= startDate && m.EndDate <= endDate)
					).OrderBy(m => m.Month).ToList();
				startDate = endDate;
				if (!quarterMonths.Any()) continue;
				quarter.DateStart = quarterMonths.First().StartDate.Value;
				quarter.DateEnd = quarterMonths.Last().EndDate.Value;
				Quarters.Add(quarter);
			}
		}
	}
}
