using System;
using Asa.Business.Common.Entities.NonPersistent.Schedule;
using Asa.Business.Common.Interfaces;

namespace Asa.Business.Common.Helpers
{
	public static class ShortScheduleHelper
	{
		public static TScheduleModel ToModel<TScheduleModel, TSchedule, TScheduleSettings>(this TSchedule source)
			where TScheduleModel : ShortScheduleModel<TSchedule, TScheduleSettings>
			where TSchedule : ISchedule<TScheduleSettings>
			where TScheduleSettings : IBaseScheduleSettings
		{
			var model = Activator.CreateInstance<TScheduleModel>();
			model.Parent = source;
			model.EditMode = source.Settings.EditMode;
			model.Name = source.Name;
			model.Advertiser = source.Settings.BusinessName;
			model.LastModified = source.LastModified;
			model.Status = source.Settings.Status;
			return model;

		}

		public static void Save<TScheduleModel, TSchedule, TScheduleSettings>(this TScheduleModel source)
			where TScheduleModel : ShortScheduleModel<TSchedule, TScheduleSettings>
			where TSchedule : ISchedule<TScheduleSettings>
			where TScheduleSettings : IBaseScheduleSettings
		{
			source.Parent.Settings.Status = source.Status;
			source.Parent.Save();
		}
	}
}
