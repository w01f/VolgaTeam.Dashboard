using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Asa.Business.Common.Entities.Helpers;
using Asa.Business.Common.Entities.NonPersistent.Common;
using Asa.Business.Common.Entities.Persistent;
using Asa.Business.Common.Enums;
using Asa.Business.Common.Interfaces;
using Asa.Business.Media.Contexts;
using Asa.Business.Media.Entities.NonPersistent.Schedule;
using Asa.Business.Media.Entities.NonPersistent.Section.Content;
using Asa.Business.Media.Entities.NonPersistent.Snapshot;
using Asa.Business.Media.Enums;
using Asa.Business.Online.Entities.NonPersistent;
using Asa.Business.Online.Interfaces;
using Newtonsoft.Json;

namespace Asa.Business.Media.Entities.Persistent
{
	public class MediaSchedule : BaseSchedule<MediaContext>, IDigitalSchedule<MediaScheduleSettings>
	{
		#region Persistent Properties
		public virtual ICollection<MediaPartition> Partitions { get; set; }
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
		}
		#endregion

		public TSchedulePartitionContent GetSchedulePartitionContent<TSchedulePartitionContent>(SchedulePartitionType partitionType)
			where TSchedulePartitionContent : ISchedulePartitionContent
		{
			var schedulePartition = Partitions.FirstOrDefault(partition => partition.Type == partitionType);
			if (schedulePartition == null)
			{
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
			}
			return ((ISchedulePartition<TSchedulePartitionContent>)schedulePartition).Content;
		}

		public void ApplySchedulePartitionContent<TSchedulePartitionContent>(SchedulePartitionType partitionType, TSchedulePartitionContent content)
			where TSchedulePartitionContent : class, ISchedulePartitionContent
		{
			var schedulePartition = Partitions.FirstOrDefault(partition => partition.Type == partitionType);
			if (schedulePartition == null)
				throw new ArgumentException("Target partition not found");
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
			base.ApplySettingsChanges(changeInfo);
			var mediaChangeInfo = changeInfo as MediaScheduleChangeInfo;
			if (mediaChangeInfo == null) return;
			if (mediaChangeInfo.DigitalContentChanged && DigitalDataChanged != null)
				DigitalDataChanged(this, EventArgs.Empty);
			if (mediaChangeInfo.CalendarTypeChanged && CalendarTypeChanged != null)
				CalendarTypeChanged(this, EventArgs.Empty);
			if (MediaDataChanged != null)
				MediaDataChanged(this, EventArgs.Empty);
		}
	}
}
