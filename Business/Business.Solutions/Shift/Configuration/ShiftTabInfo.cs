using System.Xml;

namespace Asa.Business.Solutions.Shift.Configuration
{
	public abstract class ShiftTabInfo
	{
		protected ResourceManager _resourceManager;

		public string Title { get; protected set; }

		public virtual void LoadData(XmlNode configNode, ResourceManager resourceManager)
		{
			_resourceManager = resourceManager;
			Title = configNode.SelectSingleNode("./Title")?.InnerText;
		}
	}
}
