using System.Xml;

namespace Asa.Business.Solutions.StarApp.Configuration
{
	public abstract class StarTabInfo
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
