using System.Xml;
using Asa.Business.Dashboard.Configuration;

namespace Asa.SlideTemplateViewer
{
	public class TextResourcesManager
	{
		public string FormText { get; private set; }
		public string RibbonTabTitle { get; private set; }
		public string StatusBarTitle { get; private set; }

		public void Load()
		{
			if (!ResourceManager.Instance.TextResourcesFile.ExistsLocal()) return;

			var document = new XmlDocument();
			document.Load(ResourceManager.Instance.TextResourcesFile.LocalPath);

			FormText = document.SelectSingleNode(@"//Settings/addslides/TitleBar")?.InnerText;
			RibbonTabTitle = document.SelectSingleNode(@"//Settings/addslides/RibbonTab")?.InnerText;
			StatusBarTitle = document.SelectSingleNode(@"//Settings/addslides/Footer")?.InnerText;
		}
	}
}
