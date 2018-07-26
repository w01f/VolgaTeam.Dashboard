using System.Xml;

namespace Asa.Business.Solutions.Shift.Configuration
{
	public abstract class ShiftTabInfo
	{
		public string Title { get; protected set; }

		public virtual void LoadData(XmlNode configNode, ResourceManager resourceManager)
		{
			Title = configNode.SelectSingleNode("./Title")?.InnerText;
		}
	}
}
