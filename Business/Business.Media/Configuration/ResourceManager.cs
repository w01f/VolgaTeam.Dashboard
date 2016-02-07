using System;
using System.Threading.Tasks;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.RemoteStorage;

namespace Asa.Business.Media.Configuration
{
	public class ResourceManager
	{
		private static readonly ResourceManager _instance = new ResourceManager();

		public static ResourceManager Instance
		{
			get { return _instance; }
		}

		public StorageFile TabsConfigFile { get; private set; }
		public StorageFile Gallery1ConfigFile { get; private set; }
		public StorageFile Gallery2ConfigFile { get; private set; }

		public StorageFile MediaListsFile { get; private set; }

		public StorageFile DefaultStrategyLogoFile { get; private set; }

		private ResourceManager() { }

		public async Task Load()
		{
			await Asa.Common.Core.Configuration.ResourceManager.Instance.Load();

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

			MediaListsFile = new StorageFile(new[]
			{
				FileStorageManager.IncomingFolderName,
				AppProfileManager.Instance.AppName,
				"Data",
				String.Format("{0} Strategy.xml",MediaMetaData.Instance.DataTypeString)
			});
			await MediaListsFile.Download();

			DefaultStrategyLogoFile = new StorageFile(new[]
			{
				FileStorageManager.IncomingFolderName,
				FileStorageManager.CommonIncomingFolderName,
				"Artwork",
				MediaMetaData.Instance.DataTypeString.ToUpper(),
				"DefaultStrategyImage.png"
			});
			if (await DefaultStrategyLogoFile.Exists(true))
				await DefaultStrategyLogoFile.Download();
		}
	}
}
