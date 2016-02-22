using System;
using System.Drawing;
using System.Linq;
using Asa.Business.Media.Configuration;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Interfaces;
using Asa.Common.Core.Objects.Images;
using Newtonsoft.Json;

namespace Asa.Business.Media.Entities.NonPersistent.Snapshot
{
	public class SnapshotProgram : IJsonCloneable<SnapshotProgram>
	{
		private string _name;
		public Snapshot Parent { get; set; }
		public Guid UniqueID { get; set; }
		public decimal Index { get; set; }
		public string Station { get; set; }
		public ImageSource Logo { get; set; }
		public string Daypart { get; set; }
		public string Length { get; set; }
		public string Time { get; set; }
		public decimal? Rate { get; set; }

		public int? MondaySpot { get; set; }
		public int? TuesdaySpot { get; set; }
		public int? WednesdaySpot { get; set; }
		public int? ThursdaySpot { get; set; }
		public int? FridaySpot { get; set; }
		public int? SaturdaySpot { get; set; }
		public int? SundaySpot { get; set; }

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

		public Image SmallLogo
		{
			get { return Logo != null ? Logo.TinyImage : null; }
		}

		public decimal TotalCost
		{
			get { return (Rate.HasValue ? Rate.Value : 0) * TotalSpots; }
		}

		public int TotalSpots
		{
			get
			{
				return new[] 
				{ 
					MondaySpot, 
					TuesdaySpot, 
					WednesdaySpot, 
					ThursdaySpot, 
					FridaySpot, 
					SaturdaySpot, 
					SundaySpot 
				}
				.Select(v => v ?? 0)
				.Sum();
			}
		}

		public string StartDayLetter
		{
			get
			{
				if (Parent.Parent.ScheduleSettings.StartDayOfWeek == DayOfWeek.Sunday && SundaySpot.HasValue)
					return "Su";
				if (MondaySpot.HasValue)
					return "M";
				if (TuesdaySpot.HasValue)
					return "T";
				if (WednesdaySpot.HasValue)
					return "W";
				if (ThursdaySpot.HasValue)
					return "Th";
				if (FridaySpot.HasValue)
					return "F";
				if (SaturdaySpot.HasValue)
					return "Sa";
				if (SundaySpot.HasValue)
					return "Su";
				return "M";
			}
		}

		public string EndDayLetter
		{
			get
			{
				if (Parent.Parent.ScheduleSettings.EndDayOfWeek == DayOfWeek.Sunday && SundaySpot.HasValue)
					return "Su";
				if (SaturdaySpot.HasValue)
					return "Sa";
				if (FridaySpot.HasValue)
					return "F";
				if (ThursdaySpot.HasValue)
					return "Th";
				if (WednesdaySpot.HasValue)
					return "W";
				if (TuesdaySpot.HasValue)
					return "T";
				if (MondaySpot.HasValue)
					return "M";
				if (SundaySpot.HasValue)
					return "Su";
				return "Su";
			}
		}
		#endregion

		[JsonConstructor]
		private SnapshotProgram() { }

		public SnapshotProgram(Snapshot parent)
		{
			Parent = parent;
			UniqueID = Guid.NewGuid();
			Index = Parent.Programs.Count + 1;
			Station = Parent.Parent.ScheduleSettings.Stations.Count(x => x.Available) == 1 ?
				Parent.Parent.ScheduleSettings.Stations.Where(x => x.Available).Select(x => x.Name).FirstOrDefault() :
				null;
			Logo = MediaMetaData.Instance.ListManager.Images.Where(g => g.IsDefault).Select(g => g.Images.FirstOrDefault(i => i.IsDefault)).FirstOrDefault()?.Clone<ImageSource, ImageSource>();
		}

		public void Dispose()
		{
			Logo.Dispose();
			Logo = null;

			Parent = null;
		}

		public void ApplyDefaultValues()
		{
			var source = MediaMetaData.Instance.ListManager.SourcePrograms.FirstOrDefault(x => x.Name.Equals(_name));
			if (source == null) return;
			Daypart = source.Daypart;
			Time = source.Time;
		}

		public void AfterClone(SnapshotProgram source, bool fullClone = true)
		{
			Parent = source.Parent;
			if (!fullClone)
			{
				MondaySpot = null;
				TuesdaySpot = null;
				WednesdaySpot = null;
				ThursdaySpot = null;
				FridaySpot = null;
				SaturdaySpot = null;
				SundaySpot = null;
			}
		}

		public DateTime GetStartWeekDayByDate(DateTime startDate)
		{
			var initialIncrement = 0;
			if (startDate.DayOfWeek == DayOfWeek.Sunday)
				initialIncrement = 1;

			if (startDate.DayOfWeek == DayOfWeek.Sunday && SundaySpot.HasValue)
				return startDate;
			if (MondaySpot.HasValue)
				return startDate.AddDays(initialIncrement);
			if (TuesdaySpot.HasValue)
				return startDate.AddDays(initialIncrement + 1);
			if (WednesdaySpot.HasValue)
				return startDate.AddDays(initialIncrement + 2);
			if (ThursdaySpot.HasValue)
				return startDate.AddDays(initialIncrement + 3);
			if (FridaySpot.HasValue)
				return startDate.AddDays(initialIncrement + 4);
			if (SaturdaySpot.HasValue)
				return startDate.AddDays(initialIncrement + 5);
			if (SundaySpot.HasValue)
				return startDate.AddDays(initialIncrement + 6);

			return startDate;
		}

		public DateTime GetEndWeekDayByDate(DateTime endDate)
		{
			var initialDecrement = 0;
			if (endDate.DayOfWeek == DayOfWeek.Sunday)
				initialDecrement = -1;

			if (endDate.DayOfWeek == DayOfWeek.Sunday && SundaySpot.HasValue)
				return endDate;
			if (SaturdaySpot.HasValue)
				return endDate.AddDays(initialDecrement);
			if (FridaySpot.HasValue)
				return endDate.AddDays(initialDecrement - 1);
			if (ThursdaySpot.HasValue)
				return endDate.AddDays(initialDecrement - 2);
			if (WednesdaySpot.HasValue)
				return endDate.AddDays(initialDecrement - 3);
			if (TuesdaySpot.HasValue)
				return endDate.AddDays(initialDecrement - 4);
			if (MondaySpot.HasValue)
				return endDate.AddDays(initialDecrement - 5);
			if (SundaySpot.HasValue)
				return endDate.AddDays(initialDecrement - 6);

			return endDate;
		}
	}
}
