using System;
using System.Collections.Generic;
using System.Linq;
using Asa.Business.Media.Entities.NonPersistent.Common;
using Asa.Business.Online.Entities.NonPersistent;

namespace Asa.Business.Media.Entities.NonPersistent.Section.Content
{
	public abstract class ProgramScheduleContent : MediaScheduleContent
	{
		public DateTime? SelectedQuarter { get; set; }
		public DigitalLegend DigitalLegend { get; set; }
		public bool ApplySettingsForAll { get; set; }
		public List<ScheduleSection> Sections { get; }

		public abstract int TotalPeriods { get; }

		#region Calculated Properies
		public int TotalActivePeriods
		{
			get
			{
				var defaultSection = Sections.FirstOrDefault();
				return defaultSection != null ? defaultSection.TotalActivePeriods : 0;
			}
		}
		public double TotalCPP
		{
			get { return Sections.Sum(s => s.TotalCPP); }
		}
		public double TotalGRP
		{
			get { return Sections.Sum(s => s.TotalGRP); }
		}
		public double AvgRate
		{
			get { return TotalSpots != 0 ? (TotalCost / TotalSpots) : 0; }
		}
		public double TotalCost
		{
			get { return Sections.Sum(s => s.TotalCost); }
		}
		public double NetRate
		{
			get { return TotalCost - Discount; }
		}
		public double Discount
		{
			get { return TotalCost * 0.15; }
		}
		public int TotalSpots
		{
			get { return Sections.Sum(s => s.TotalSpots); }
		}
		#endregion

		protected ProgramScheduleContent()
		{
			DigitalLegend = new DigitalLegend();
			Sections = new List<ScheduleSection>();
		}

		protected override void AfterCreate()
		{
			base.AfterCreate();

			foreach (var scheduleSection in Sections)
			{
				scheduleSection.Parent = this;
				scheduleSection.AfterCreate();
			}

			OnScheduleDatesChanged(this, EventArgs.Empty);
			OnMediaDataChangedChanged(this, EventArgs.Empty);

			Schedule.ScheduleDatesChanged += OnScheduleDatesChanged;
			Schedule.MediaDataChanged += OnMediaDataChangedChanged;
		}

		public override void Dispose()
		{
			if (Schedule != null)
			{
				Schedule.ScheduleDatesChanged -= OnScheduleDatesChanged;
				Schedule.MediaDataChanged -= OnMediaDataChangedChanged;
			}
			Sections.ForEach(s => s.Dispose());
			Sections.Clear();
			base.Dispose();
		}

		public abstract ScheduleSection CreateSection();

		public void ChangeSectionPosition(int position, int newPosition)
		{
			if (position < 0 || position >= Sections.Count) return;
			var section = Sections[position];
			section.Index = newPosition - 0.5;
			RebuildSectionIndexes();
		}

		public void RebuildSectionIndexes()
		{
			var i = 0;
			foreach (var snapshot in Sections.OrderBy(o => o.Index))
			{
				snapshot.Index = i;
				i++;
			}
			Sections.Sort((x, y) => x.Index.CompareTo(y.Index));
		}

		private void OnScheduleDatesChanged(object sender, EventArgs e)
		{
			Sections.ForEach(section => section.RebuildSpots());
		}

		private void OnMediaDataChangedChanged(object sender, EventArgs e)
		{
			foreach (var scheduleSection in Sections)
			{
				scheduleSection.ShowRating = scheduleSection.ShowRating & ScheduleSettings.UseDemo & !String.IsNullOrEmpty(ScheduleSettings.Demo);
				scheduleSection.ShowCPP = scheduleSection.ShowCPP & ScheduleSettings.UseDemo & !String.IsNullOrEmpty(ScheduleSettings.Demo);
				scheduleSection.ShowGRP = scheduleSection.ShowGRP & ScheduleSettings.UseDemo & !String.IsNullOrEmpty(ScheduleSettings.Demo);
				scheduleSection.ShowTotalCPP = scheduleSection.ShowTotalCPP & ScheduleSettings.UseDemo & !String.IsNullOrEmpty(ScheduleSettings.Demo);
				scheduleSection.ShowTotalGRP = scheduleSection.ShowTotalGRP & ScheduleSettings.UseDemo & !String.IsNullOrEmpty(ScheduleSettings.Demo);
			}
		}
	}
}
