using System;
using Asa.Business.Common.Enums;

namespace Asa.Business.Common.Entities.Helpers
{
	public class PartitionContentChangedEventArgs:EventArgs
	{
		public SchedulePartitionType PartitionType { get; private set; }

		public PartitionContentChangedEventArgs(SchedulePartitionType partitionType)
		{
			PartitionType = partitionType;
		}
	}
}
