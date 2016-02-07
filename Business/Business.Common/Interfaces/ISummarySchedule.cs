using System.Collections.Generic;
using Asa.Business.Common.Entities.NonPersistent.Schedule;
using Asa.Business.Common.Entities.NonPersistent.Summary;

namespace Asa.Business.Common.Interfaces
{
	public interface ISummarySchedule
	{
		BaseScheduleSettings BaseSettings { get; }
		BaseSummarySettings ProductSummary { get; }
		CustomSummarySettings CustomSummary { get; }
		IEnumerable<ISummaryProduct> ProductSummaries { get; }
	}
}
