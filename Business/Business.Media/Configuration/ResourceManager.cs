﻿using System;
using System.Threading.Tasks;
using Asa.Common.Core.Extensions;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.RemoteStorage;

namespace Asa.Business.Media.Configuration
{
	public class ResourceManager
	{
		public static ResourceManager Instance { get; } = new ResourceManager();

		public StorageFile TabsConfigFile { get; private set; }
		public StorageFile BrowserConfigFile { get; private set; }
		public StorageFile Gallery1ConfigFile { get; private set; }
		public StorageFile Gallery2ConfigFile { get; private set; }
		public StorageFile FormStyleConfigFile { get; private set; }

		public StorageFile MediaListsFile { get; private set; }

		public StorageFile SolutionsConfigFile { get; private set; }
		public StorageDirectory SolutionsDataFolder { get; private set; }

		public StorageFile MainAppTitleTextFile { get; private set; }
		public StorageFile TextResourcesFile { get; private set; }
		public ArchiveDirectory ImageResourcesFolder { get; private set; }

		private ResourceManager() { }

		public async Task Load()
		{
			await Asa.Common.Core.Configuration.ResourceManager.Instance.Load();

			await Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.Download();
			await Asa.Common.Core.Configuration.ResourceManager.Instance.ScheduleSlideTemplatesFolder.Download();
			await Asa.Common.Core.Configuration.ResourceManager.Instance.CalendarSlideTemplatesFolder.Download();
			await Asa.Common.Core.Configuration.ResourceManager.Instance.ArtworkFolder.Download();
			await Asa.Common.Core.Configuration.ResourceManager.Instance.RateCardFolder.Download();

			TabsConfigFile = new StorageFile(new[]
			{
				FileStorageManager.IncomingFolderName,
				AppProfileManager.Instance.AppName,
				"AppSettings",
				String.Format("{0}_tab_names.xml",MediaMetaData.Instance.DataTypeString.ToLower())
			});
			await TabsConfigFile.Download();

			BrowserConfigFile = new StorageFile(new[]
			{
				FileStorageManager.IncomingFolderName,
				AppProfileManager.Instance.AppName,
				"AppSettings",
				"eo.xml"
			});
			await BrowserConfigFile.Download();

			Gallery1ConfigFile = new StorageFile(new[]
			{
				FileStorageManager.IncomingFolderName,
				AppProfileManager.Instance.AppName,
				"AppSettings",
				"Gallery1.xml"
			});
			await Gallery1ConfigFile.Download();

			Gallery2ConfigFile = new StorageFile(new[]
			{
				FileStorageManager.IncomingFolderName,
				AppProfileManager.Instance.AppName,
				"AppSettings",
				"Gallery2.xml"
			});
			await Gallery2ConfigFile.Download();

			FormStyleConfigFile = new StorageFile(new[]
			{
				FileStorageManager.IncomingFolderName,
				AppProfileManager.Instance.AppName,
				"AppSettings",
				"style.xml"
			});
			await FormStyleConfigFile.Download();

			MediaListsFile = new StorageFile(
				Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge(
					String.Format("{0} Strategy.xml", MediaMetaData.Instance.DataTypeString)));

			SolutionsConfigFile = new StorageFile(new[]
{
				FileStorageManager.IncomingFolderName,
				AppProfileManager.Instance.AppName,
				"AppSettings",
				"solution_templates.xml"
			});
			await SolutionsConfigFile.Download();

			SolutionsDataFolder = new StorageDirectory(new[]
			{
				FileStorageManager.IncomingFolderName,
				AppProfileManager.Instance.AppName,
				"Solution Templates"
			});

			MainAppTitleTextFile = new StorageFile(new[]
			{
				FileStorageManager.IncomingFolderName,
				AppProfileManager.Instance.AppName,
				"AppSettings",
				"app_brand.txt"
			});
			if (await MainAppTitleTextFile.Exists(true))
				await MainAppTitleTextFile.Download();

			TextResourcesFile = new StorageFile(new[]
			{
				FileStorageManager.IncomingFolderName,
				AppProfileManager.Instance.AppName,
				"AppSettings",
				String.Format("{0}_subtab_names.xml",MediaMetaData.Instance.DataTypeString.ToLower())
			});
			if (await TextResourcesFile.Exists(true))
				await TextResourcesFile.Download();

			ImageResourcesFolder = new ArchiveDirectory(new[]
			{
				FileStorageManager.IncomingFolderName,
				AppProfileManager.Instance.AppName,
				"Resources"
			});
			if (await ImageResourcesFolder.Exists(true))
				await ImageResourcesFolder.Download();
		}
	}
}
