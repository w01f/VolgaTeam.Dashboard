using Asa.Business.Media.Entities.NonPersistent.Section.Summary;

namespace Asa.Business.Media.Interfaces
{
	public interface ISectionSummaryContent
	{
		SectionSummary Parent { get; }
		void SynchronizeSectionContent();
		void Dispose();
	}
}
