using Asa.Business.Media.Entities.Persistent;
using Asa.Business.Online.Entities.NonPersistent;
using Asa.Business.Online.Interfaces;

namespace Asa.Business.Media.Entities.NonPersistent.Digital
{
	class DigitalMediaProductsContent : DigitalProductsContent
	{
		public override IDigitalSchedule<IDigitalScheduleSettings> Schedule => ((DigitalProductsPartition)Parent).Schedule;
	}
}
