using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Asa.Common.Core.Objects.RemoteStorage;
using Asa.Common.Core.Objects.TabPages;

namespace Asa.Media.Controls.BusinessClasses.Managers
{
	public class TextResourcesManager
	{
		public const string HomeMainTab1Id = "BuildSchedule";
		public const string HomeAdditionalTab1Id = "MediaProperties";

		public const string SolutionsAdditionalTab1Id = "SolutionTemplates";
		public const string SolutionsAdditionalTab2Id = "Resources";

		public List<TabPageConfig> TabPageSettings { get; } = new List<TabPageConfig>();

		public void LoadTabPageSettings(StorageFile contentFile)
		{
			if (!contentFile.ExistsLocal()) return;
			var document = new XmlDocument();
			document.Load(contentFile.LocalPath);
			foreach (var node in document.SelectNodes(@"//Root/SubTab").OfType<XmlNode>().ToList())
			{
				var tabPageConfig = new TabPageConfig();
				tabPageConfig.Deserialize(node);
				TabPageSettings.Add(tabPageConfig);
			}
			TabPageSettings.Sort((x, y) => x.Order.CompareTo(y.Order));
		}
	}
}