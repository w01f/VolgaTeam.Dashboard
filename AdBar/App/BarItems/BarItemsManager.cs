using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Asa.Bar.App.Configuration;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Extensions;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.RemoteStorage;

namespace Asa.Bar.App.BarItems
{
	class BarItemsManager
	{
		public List<TabPage> Tabs { get; set; }

		public BarItemsManager()
		{
			Tabs = new List<TabPage>();
		}

		public async Task Load()
		{
			await ExtractTabContent();

			var tabsConfigContent = ConfigHelper.GetTextFromFile(ResourceManager.Instance.TabsConfigFile.LocalPath);
			foreach (Match m in Regex.Matches(tabsConfigContent, "<Tab>(.+?)</Tab>", RegexOptions.IgnoreCase | RegexOptions.Singleline))
			{
				var pageConfigContent = m.Groups[1].Value;
				var tab = new TabPage(pageConfigContent);
				Tabs.Add(tab);
			}
		}

		private async Task ExtractTabContent()
		{
			if (FileStorageManager.Instance.DataState == DataActualityState.Updated) return;
			var tabConfigFiles = await ResourceManager.Instance.DataFolder.GetRemoteFiles(itemName => itemName.Contains("tab_"));
			foreach (var storageFile in tabConfigFiles)
			{
				var archiveFolder = new ArchiveDirectory(ResourceManager.Instance.DataFolder.RelativePathParts.Merge(Path.GetFileNameWithoutExtension(storageFile.Name)));
				await archiveFolder.Download();
			}
		}
	}
}
