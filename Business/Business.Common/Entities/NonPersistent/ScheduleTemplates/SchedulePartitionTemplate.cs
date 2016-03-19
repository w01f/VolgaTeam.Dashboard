using Asa.Business.Common.Enums;

namespace Asa.Business.Common.Entities.NonPersistent.ScheduleTemplates
{
	public class SchedulePartitionTemplate
	{
		public SchedulePartitionType PartitionType { get; set; }
		public string Content { get; set; }
	}
}
