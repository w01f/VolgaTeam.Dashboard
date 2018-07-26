using System.Xml;

namespace Asa.Business.Solutions.StarApp.Configuration
{
	public abstract class StarTabInfo
	{
		public string Title { get; protected set; }

		public virtual void LoadData(XmlNode configNode, ResourceManager resourceManager)
		{
			Title = configNode.SelectSingleNode("./Title")?.InnerText;
		}
	}
}
