using System.Xml;

namespace Asa.Legacy.Media.Entities.Summary
{
	public interface ISectionSummaryContent
	{
		SectionSummary Parent { get; }
		void Deserialize(XmlNode node);
	}
}
