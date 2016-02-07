using Asa.Business.Common.Interfaces;
using Asa.Business.Online.Entities.NonPersistent;

namespace Asa.Business.Online.Interfaces
{
	public interface IDigitalSchedule<out TScheduleSettings> : ISchedule<TScheduleSettings> where TScheduleSettings: IDigitalScheduleSettings
	{
		DigitalProductsContent DigitalProductsContent { get; set; }
	}
}
