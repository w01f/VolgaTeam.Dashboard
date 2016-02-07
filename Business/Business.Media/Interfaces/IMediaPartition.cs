using Asa.Business.Common.Interfaces;

namespace Asa.Business.Media.Interfaces
{
	interface IMediaPartition<TPartitionContent> : ISchedulePartition<TPartitionContent>
		where TPartitionContent : ISchedulePartitionContent
	{
	}
}
