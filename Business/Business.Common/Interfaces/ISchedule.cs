namespace Asa.Business.Common.Interfaces
{
	public interface ISchedule<out TScheduleSettings>: IChangeTracked where TScheduleSettings : IBaseScheduleSettings
	{
		string Name { get; set; }
		TScheduleSettings Settings { get; }
		void Save();
	}
}