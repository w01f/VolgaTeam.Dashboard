using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Asa.Business.Common.Entities.NonPersistent.Common;
using Asa.Business.Common.Entities.NonPersistent.ScheduleTemplates;
using Asa.Business.Common.Entities.Persistent;
using Asa.Business.Common.Enums;
using Asa.Business.Common.Helpers;
using Asa.Business.Common.Interfaces;
using Asa.Business.Media.Contexts;
using Asa.Business.Media.Entities.NonPersistent.Option;
using Asa.Business.Media.Entities.NonPersistent.Schedule;
using Asa.Business.Media.Entities.NonPersistent.Section.Content;
using Asa.Business.Media.Entities.NonPersistent.Snapshot;
using Asa.Business.Media.Enums;
using Asa.Business.Media.Interfaces;
using Asa.Business.Online.Entities.NonPersistent;
using Asa.Business.Online.Interfaces;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Business.Solutions.Common.Enums;
using Asa.Business.Solutions.Common.Interfaces;
using Newtonsoft.Json;

namespace Asa.Business.Media.Entities.Persistent
{
	public class MediaSchedule : BaseSchedule<MediaContext>, IDigitalSchedule<MediaScheduleSettings>
	{
		#region Persistent Properties
		public virtual ICollection<MediaPartition> Partitions { get; set; }
		public virtual ICollection<MediaSolution> Solutions { get; set; }
		#endregion

		#region Nonpersistent Properties
		private MediaScheduleSettings _settings;

		[NotMapped, JsonIgnore]
		public MediaScheduleSettings Settings
		{
			get
			{
				if (_settings == null)
					_settings = SettingsContainer.CreateInstance<MediaScheduleSettings>(this, SettingsEncoded);
				return _settings;
			}
			set
			{
				if (_settings == null || _settings != value)
				{
					if (_settings != null)
						_settings.Dispose();
					_settings = value;
				}
			}
		}

		[NotMapped, JsonIgnore]
		public ProgramScheduleContent ProgramSchedule
		{
			get
			{
				return Settings.SelectedSpotType == SpotType.Week ?
						(ProgramScheduleContent)GetSchedulePartitionContent<WeeklyScheduleContent>(SchedulePartitionType.WeeklySchedule) :
						GetSchedulePartitionContent<MonthlyScheduleContent>(SchedulePartitionType.MonthlySchedule);
			}
			set
			{
				if (Settings.SelectedSpotType == SpotType.Week)
					ApplySchedulePartitionContent(SchedulePartitionType.WeeklySchedule, (WeeklyScheduleContent)value);
				else
					ApplySchedulePartitionContent(SchedulePartitionType.MonthlySchedule, (MonthlyScheduleContent)value);
			}
		}

		[NotMapped, JsonIgnore]
		public SnapshotContent SnapshotContent
		{
			get { return GetSchedulePartitionContent<SnapshotContent>(SchedulePartitionType.Snapshots); }
			set { ApplySchedulePartitionContent(SchedulePartitionType.Snapshots, value); }
		}

		[NotMapped, JsonIgnore]
		public OptionsContent OptionsContent
		{
			get { return GetSchedulePartitionContent<OptionsContent>(SchedulePartitionType.Options); }
			set { ApplySchedulePartitionContent(SchedulePartitionType.Options, value); }
		}
		#endregion

		#region IDigitalSchedule Properties
		[NotMapped, JsonIgnore]
		public DigitalProductsContent DigitalProductsContent
		{
			get { return GetSchedulePartitionContent<DigitalProductsContent>(SchedulePartitionType.DigitalProducts); }
			set { ApplySchedulePartitionContent(SchedulePartitionType.DigitalProducts, value); }
		}
		#endregion

		public event EventHandler<EventArgs> MediaDataChanged;
		public event EventHandler<EventArgs> DigitalDataChanged;
		public event EventHandler<EventArgs> CalendarTypeChanged;

		public MediaSchedule()
		{
			Partitions = new List<MediaPartition>();
			Solutions = new List<MediaSolution>();
		}

		#region Base Schedule Processing
		public override void Save(MediaContext context, IDbEntity<MediaContext> current, bool withCommit = true)
		{
			foreach (var mediaPartition in Partitions.ToList())
				mediaPartition.Save(context, mediaPartition, false);
			base.Save(context, current, withCommit);
		}

		public override void Delete(MediaContext context)
		{
			foreach (var schedulePartition in Partitions.ToList())
				schedulePartition.Delete(context);
			base.Delete(context);
		}

		public override void CloneData(BaseSchedule<MediaContext> target)
		{
			var mediaTarget = (MediaSchedule)target;
			base.CloneData(mediaTarget);
			foreach (var partition in Partitions.ToList())
			{
				var targetPartition = (MediaPartition)Activator.CreateInstance(partition.GetType());
				partition.CloneData(targetPartition);
				targetPartition.Schedule = mediaTarget;
				mediaTarget.Partitions.Add(targetPartition);
			}
		}

		public override void ResetParent() { }

		public override void BeforeSave()
		{
			SettingsEncoded = Settings.Serialize();
			foreach (var schedulePartition in Partitions.ToList())
				schedulePartition.BeforeSave();
			foreach (var scheduleSolution in Solutions.ToList())
				scheduleSolution.BeforeSave();
		}
		#endregion

		#region Partition Processing
		private MediaPartition CreatePartition(SchedulePartitionType partitionType)
		{
			MediaPartition schedulePartition;
			switch (partitionType)
			{
				case SchedulePartitionType.WeeklySchedule:
					schedulePartition = new WeeklySchedulePartition();
					break;
				case SchedulePartitionType.MonthlySchedule:
					schedulePartition = new MonthlySchedulePartition();
					break;
				case SchedulePartitionType.DigitalProducts:
					schedulePartition = new DigitalProductsPartition();
					break;
				case SchedulePartitionType.Snapshots:
					schedulePartition = new SnapshotPartition();
					break;
				case SchedulePartitionType.Options:
					schedulePartition = new OptionsPartition();
					break;
				case SchedulePartitionType.BroadcastCalendar:
					schedulePartition = new BroadcastCalendarPartition();
					break;
				case SchedulePartitionType.CustomCalendar:
					schedulePartition = new CustomCalendarPartition();
					break;
				default:
					throw new ArgumentOutOfRangeException("Undefined schedule partition type");
			}
			schedulePartition.Add(Context);
			schedulePartition.Schedule = this;
			Partitions.Add(schedulePartition);
			MarkAsModified();
			return schedulePartition;
		}

		public TSchedulePartitionContent GetSchedulePartitionContent<TSchedulePartitionContent>(SchedulePartitionType partitionType)
			where TSchedulePartitionContent : ISchedulePartitionContent
		{
			var schedulePartition = Partitions.FirstOrDefault(partition => partition.Type == partitionType);
			if (schedulePartition == null)
				schedulePartition = CreatePartition(partitionType);
			return ((ISchedulePartition<TSchedulePartitionContent>)schedulePartition).Content;
		}

		public void ApplySchedulePartitionContent<TSchedulePartitionContent>(SchedulePartitionType partitionType, TSchedulePartitionContent content)
			where TSchedulePartitionContent : class, ISchedulePartitionContent
		{
			var schedulePartition = Partitions.FirstOrDefault(partition => partition.Type == partitionType) ??
				CreatePartition(partitionType);
			var oldContent = ((ISchedulePartition<TSchedulePartitionContent>)schedulePartition).Content;
			if (oldContent != null)
			{
				oldContent.Dispose();
				((ISchedulePartition<TSchedulePartitionContent>)schedulePartition).Content = null;
				GC.Collect();
				GC.WaitForPendingFinalizers();
			}
			((ISchedulePartition<TSchedulePartitionContent>)schedulePartition).Content = content;
			OnPartitionContentChanged(new PartitionContentChangedEventArgs(partitionType));
		}

		public override void ApplySettingsChanges<TChangeInfo>(TChangeInfo changeInfo)
		{
			var mediaChangeInfo = changeInfo as MediaScheduleChangeInfo;
			if (changeInfo.ScheduleDatesChanged)
				OnScheduleDatesChanged(new ScheduleDatesChangedEventArgs { KeepDatesRelatedData = mediaChangeInfo.KeepSpotsWhenDatesChanged});
			if (mediaChangeInfo == null) return;
			if (mediaChangeInfo.DigitalContentChanged)
				DigitalDataChanged?.Invoke(this, EventArgs.Empty);
			if (mediaChangeInfo.CalendarTypeChanged)
				CalendarTypeChanged?.Invoke(this, EventArgs.Empty);
			MediaDataChanged?.Invoke(this, EventArgs.Empty);
		}
		#endregion

		#region Templates Processing
		public override ScheduleTemplate GetTemplate(string name)
		{
			var template = base.GetTemplate(name);
			template.Advertiser = Settings.BusinessName;
			return template;
		}

		public override void LoadFromTemplate(ScheduleTemplate sourceTemplate)
		{
			base.LoadFromTemplate(sourceTemplate);
			foreach (var partitionTemplate in sourceTemplate.PartitionTemplates)
				ApplySchedulePartitionContentFromTemplate(partitionTemplate);
		}

		private void ApplySchedulePartitionContentFromTemplate(SchedulePartitionTemplate template)
		{
			var schedulePartition = Partitions.FirstOrDefault(partition => partition.Type == template.PartitionType) ??
				CreatePartition(template.PartitionType);
			schedulePartition.ContentEncoded = template.Content;
		}

		protected override IEnumerable<SchedulePartitionTemplate> GetPartitionTemplates()
		{
			return Partitions.Select(partition => new SchedulePartitionTemplate
			{
				PartitionType = partition.Type,
				Content = partition.ContentEncoded
			});
		}
		#endregion

		#region Solution Processing
		private MediaSolution CreateScheduleSolution(SolutionType solutionType)
		{
			MediaSolution scheduleSolution;
			switch (solutionType)
			{
				case SolutionType.Dashboard:
					scheduleSolution = new MediaDashboardSolution();
					break;
				default:
					throw new ArgumentOutOfRangeException("Undefined schedule solution type");
			}
			scheduleSolution.Add(Context);
			scheduleSolution.Schedule = this;
			Solutions.Add(scheduleSolution);
			MarkAsModified();
			return scheduleSolution;
		}

		public TScheduleSolutionContent GetScheduleSolutionContent<TScheduleSolutionContent>(SolutionType solutionType)
			where TScheduleSolutionContent : BaseSolutionContent
		{
			var scheduleSolution = Solutions.OfType<IScheduleSolution<TScheduleSolutionContent>>().FirstOrDefault(solution => solution.Type == (Int32)solutionType) ??
				(IScheduleSolution<TScheduleSolutionContent>)CreateScheduleSolution(solutionType);
			return scheduleSolution.Content;
		}

		public void ApplyScheduleSolutionContent<TScheduleSolutionContent>(SolutionType solutionType, TScheduleSolutionContent content)
			where TScheduleSolutionContent : BaseSolutionContent
		{
			var scheduleSolution = Solutions.OfType<IScheduleSolution<TScheduleSolutionContent>>().FirstOrDefault(solution => solution.Type == (Int32)solutionType) ??
				(IScheduleSolution<TScheduleSolutionContent>)CreateScheduleSolution(solutionType);
			var oldContent = scheduleSolution.Content;
			if (oldContent != null)
			{
				oldContent.Dispose();
				scheduleSolution.Content = null;
				GC.Collect();
				GC.WaitForPendingFinalizers();
			}
			scheduleSolution.Content = content;
		}
		#endregion
	}
}
