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

		public string FormOpenScheduleMainTitle { get; private set; }
		public string FormOpenScheduleTab1Title { get; private set; }
		public string FormOpenScheduleTab2Title { get; private set; }
		public string FormOpenScheduleTab3Title { get; private set; }

		public List<ContentTabPageConfig> TabPageSettings { get; } = new List<ContentTabPageConfig>();

		public void LoadTextResources(StorageFile resourceFile)
		{
			if (!resourceFile.ExistsLocal()) return;
			var document = new XmlDocument();
			document.Load(resourceFile.LocalPath);

			FormOpenScheduleMainTitle = document.SelectSingleNode(@"//Config/OpenPopupLabels/WindowTitle")?.InnerText ?? "Open Previous Solutions";
			FormOpenScheduleTab1Title = document.SelectSingleNode(@"//Config/OpenPopupLabels/Tab1")?.InnerText ?? "My Solutions";
			FormOpenScheduleTab2Title = document.SelectSingleNode(@"//Config/OpenPopupLabels/Tab2")?.InnerText ?? "GitRDun";
			FormOpenScheduleTab3Title = document.SelectSingleNode(@"//Config/OpenPopupLabels/Tab3")?.InnerText ?? "Public Cloud";
		}

		public void LoadTabPageSettings(StorageFile contentFile)
		{
			if (!contentFile.ExistsLocal()) return;
			var document = new XmlDocument();
			document.Load(contentFile.LocalPath);
			foreach (var node in document.SelectNodes(@"//Root/SubTab").OfType<XmlNode>().ToList())
			{
				var tabPageConfig = new ContentTabPageConfig();
				tabPageConfig.Deserialize(node);
				TabPageSettings.Add(tabPageConfig);
			}
		}
	}
}