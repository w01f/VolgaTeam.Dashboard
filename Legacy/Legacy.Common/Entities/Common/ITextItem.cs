using System.Xml;

namespace Asa.Legacy.Common.Entities.Common
{
	public interface ITextItem
	{
		void Deserialize(XmlNode node);
	}
}
