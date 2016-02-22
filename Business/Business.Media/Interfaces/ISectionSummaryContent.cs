using Asa.Business.Media.Entities.NonPersistent.Section.Summary;
using Asa.Business.Media.Enums;

namespace Asa.Business.Media.Interfaces
{
	public interface ISectionSummaryContent
	{
		SectionSummary Parent { get; }
		SectionSummaryTypeEnum SummaryType { get; }
		void SynchronizeSectionContent();
		void Dispose();
	}
}
