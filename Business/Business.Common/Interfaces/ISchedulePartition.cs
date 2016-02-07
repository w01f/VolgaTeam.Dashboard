using Asa.Business.Common.Enums;

namespace Asa.Business.Common.Interfaces
{
	public interface ISchedulePartition<TSchedulePartitionContent>
		where TSchedulePartitionContent : ISchedulePartitionContent
	{
		SchedulePartitionType Type { get; }
		TSchedulePartitionContent Content { get; set; }
	}
}
