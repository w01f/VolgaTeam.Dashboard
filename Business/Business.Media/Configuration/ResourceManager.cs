using System;
using System.Threading.Tasks;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.RemoteStorage;

namespace Asa.Business.Media.Configuration
{
	public class ResourceManager
	{
		public static ResourceManager Instance { get; } = new ResourceManager();

		public StorageFile TabsConfigFile { get; private set; }
		public StorageFile Gallery1ConfigFile { get; private set; }
		public StorageFile Gallery2ConfigFile { get; private set; }

		public StorageFile MediaListsFile { get; private set; }

		public StorageFile MainAppTitleTextFile { get; private set; }
		public StorageFile MainAppIconFile { get; private set; }
		public StorageFile MainAppRibbonLogoFile { get; private set; }
		public StorageFile DigitalProductsRibbonLogoFile { get; private set; }
		public StorageFile DigitalProductsHomeMainLogoFile { get; private set; }
		public StorageFile DigitalProductsHomeRightLogoFile { get; private set; }
		public StorageFile DigitalProductsHomeBottomLogoFile { get; private set; }

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

			MainAppTitleTextFile = new StorageFile(new[]
{
				FileStorageManager.IncomingFolderName,
				AppProfileManager.Instance.AppName,
				"AppSettings",
				"app_brand.txt"
			});
			if (await MainAppTitleTextFile.Exists(true))
				await MainAppTitleTextFile.Download();

			MainAppIconFile = new StorageFile(new[]
{
				FileStorageManager.IncomingFolderName,
				AppProfileManager.Instance.AppName,
				"AppSettings",
				"form_icon.ico"
			});
			if (await MainAppIconFile.Exists(true))
				await MainAppIconFile.Download();

			MainAppRibbonLogoFile = new StorageFile(new[]
			{
				FileStorageManager.IncomingFolderName,
				AppProfileManager.Instance.AppName,
				"AppSettings",
				"branding_image.png"
			});
			if (await MainAppRibbonLogoFile.Exists(true))
				await MainAppRibbonLogoFile.Download();

			DigitalProductsRibbonLogoFile = new StorageFile(new[]
			{
				FileStorageManager.IncomingFolderName,
				AppProfileManager.Instance.AppName,
				"AppSettings",
				"digital_ribbon_group_1.png"
			});
			if (await DigitalProductsRibbonLogoFile.Exists(true))
				await DigitalProductsRibbonLogoFile.Download();

			DigitalProductsHomeMainLogoFile = new StorageFile(new[]
			{
				FileStorageManager.IncomingFolderName,
				AppProfileManager.Instance.AppName,
				"AppSettings",
				"digital_home.png"
			});
			if (await DigitalProductsHomeMainLogoFile.Exists(true))
				await DigitalProductsHomeMainLogoFile.Download();

			DigitalProductsHomeRightLogoFile = new StorageFile(new[]
			{
				FileStorageManager.IncomingFolderName,
				AppProfileManager.Instance.AppName,
				"AppSettings",
				"digital_home_right.png"
			});
			if (await DigitalProductsHomeRightLogoFile.Exists(true))
				await DigitalProductsHomeRightLogoFile.Download();

			DigitalProductsHomeBottomLogoFile = new StorageFile(new[]
			{
				FileStorageManager.IncomingFolderName,
				AppProfileManager.Instance.AppName,
				"AppSettings",
				"digital_home_bottom.png"
			});
			if (await DigitalProductsHomeBottomLogoFile.Exists(true))
				await DigitalProductsHomeBottomLogoFile.Download();
		}
	}
}
