using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Asa.Business.Common.Contexts;
using Asa.Business.Common.Entities.NonPersistent.Schedule;
using Asa.Business.Common.Entities.NonPersistent.ScheduleTemplates;
using Asa.Business.Common.Helpers;
using Asa.Business.Common.Interfaces;
using Newtonsoft.Json;

namespace Asa.Business.Common.Entities.Persistent
{
	public abstract class BaseSchedule<TContext> : ChangeTrackedEntity<TContext>, ITemplatedSchedule
		where TContext : ScheduleContext
	{
		#region Persistent Properties
		[Required]
		public string Name { get; set; }
		public string SettingsEncoded { get; set; }
		#endregion

		#region Nonpersistent Properties
		[NotMapped, JsonIgnore]
		public TContext Context { get; set; }
		#endregion

		public event EventHandler<ScheduleDatesChangedEventArgs> ScheduleDatesChanged;
		public event EventHandler<PartitionContentChangedEventArgs> PartitionContentChanged;

		public void Save()
		{
			LastModified = DateTime.Now;
			Save(Context, this);
		}

		public virtual void CloneData(BaseSchedule<TContext> target)
		{
			target.Name = Name;
			target.SettingsEncoded = SettingsEncoded;
		}

		public abstract void ApplySettingsChanges<TChangeInfo>(TChangeInfo changeInfo)
			where TChangeInfo : BaseScheduleChangeInfo;
		
		protected virtual void OnScheduleDatesChanged(ScheduleDatesChangedEventArgs e)
		{
			var handler = ScheduleDatesChanged;
			handler?.Invoke(this, e);
		}

		protected virtual void OnPartitionContentChanged(PartitionContentChangedEventArgs e)
		{
			var handler = PartitionContentChanged;
			handler?.Invoke(this, e);
		}

		public virtual ScheduleTemplate GetTemplate(string name)
		{
			var template = new ScheduleTemplate();

			template.Name = name;
			template.Date = DateTime.Now;
			template.ScheduleSettingsContent = SettingsEncoded;
			template.PartitionTemplates.AddRange(GetPartitionTemplates());

			return template;
		}

		public virtual void LoadFromTemplate(ScheduleTemplate sourceTemplate)
		{
			Name = sourceTemplate.Name;
			SettingsEncoded = sourceTemplate.ScheduleSettingsContent;
		}

		protected abstract IEnumerable<SchedulePartitionTemplate> GetPartitionTemplates();
	}
}
