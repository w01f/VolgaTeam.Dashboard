using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Asa.Business.Common.Entities.Helpers;
using Asa.Business.Common.Entities.NonPersistent.Schedule;
using Asa.Business.Common.Entities.Persistent;
using Asa.Business.Common.Interfaces;
using Asa.Common.Core.Configuration;
using Asa.Common.Core.Helpers;

namespace Asa.Business.Common.Contexts
{
	public abstract class ScheduleManager<TSchedule, TScheduleSettings, TSchedulesContainer, TContext>
		where TSchedule : BaseSchedule<TContext>, ISchedule<TScheduleSettings>
		where TScheduleSettings : IBaseScheduleSettings
		where TSchedulesContainer : IScheduleDBSetContainer<TSchedule, TContext>
		where TContext : ScheduleContext
	{
		public TContext Context { get; private set; }
		public TSchedule ActiveSchedule { get; private set; }
		public abstract TSchedulesContainer SchedulesContainer { get; }

		public event EventHandler<EventArgs> ScheduleOpened;
		public event EventHandler<EventArgs> ScheduleNameChanged;

		public void Init()
		{
			var storagePath = Path.Combine(
				AppProfileManager.Instance.AppSaveFolder.LocalPath,
				Constants.ScheduleStorageFileName);
			Context = (TContext)Activator.CreateInstance(typeof(TContext), storagePath);
		}

		public IEnumerable<TScheduleModel> GetScheduleList<TScheduleModel>()
			where TScheduleModel : ShortScheduleModel<TSchedule, TScheduleSettings>
		{
			return SchedulesContainer.Schedules
				.ToList()
				.Select(s => s.ToModel<TScheduleModel, TSchedule, TScheduleSettings>());
		}

		public void SaveScheduleModel<TScheduleModel>(TScheduleModel targetModel)
			where TScheduleModel : ShortScheduleModel<TSchedule, TScheduleSettings>
		{
			targetModel.Save<TScheduleModel, TSchedule, TScheduleSettings>();
		}

		public void OpenSchedule(TSchedule schedule)
		{
			ActiveSchedule = schedule;
			ScheduleOpened?.Invoke(this, EventArgs.Empty);
		}

		public void AddSchedule(string name)
		{
			var schedule = Activator.CreateInstance<TSchedule>();
			schedule.Context = Context;
			schedule.Name = name;
			SchedulesContainer.Schedules.Add(schedule);
			schedule.Add(Context);
			schedule.Save();

			OpenSchedule(schedule);
		}

		public void DeleteSchedule(TSchedule schedule)
		{
			schedule.Delete(Context);
			SchedulesContainer.Schedules.Remove(schedule);
			Context.SaveChanges();
		}

		public void SaveScheduleAs(string name)
		{
			var schedule = Activator.CreateInstance<TSchedule>();
			schedule.Context = Context;
			ActiveSchedule.CloneData(schedule);
			SchedulesContainer.Schedules.Add(schedule);
			schedule.Add(Context);
			schedule.Save();

			ActiveSchedule.Name = name;
			ActiveSchedule.Save();

			if (ScheduleNameChanged != null)
				ScheduleNameChanged(this, EventArgs.Empty);
		}
	}
}
