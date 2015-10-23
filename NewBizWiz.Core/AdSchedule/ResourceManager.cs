using System.Threading.Tasks;
using Asa.Core.Common;

namespace Asa.Core.AdSchedule
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

		public StorageFile PrintListsFile { get; private set; }
		public StorageFile CalendarLegendFile { get; private set; }

		public StorageFile ViewSettingsFile { get; private set; }

		private ResourceManager() { }

		public async Task Load()
		{
			await Common.ResourceManager.Instance.Load();

			await Common.ResourceManager.Instance.CalendarSlideTemplatesFolder.Download();
			await Common.ResourceManager.Instance.ArtworkFolder.Download();
			await Common.ResourceManager.Instance.RateCardFolder.Download();

			TabsConfigFile = new StorageFile(new[]
			{
				FileStorageManager.IncomingFolderName,
				AppProfileManager.Instance.AppName,
				"AppSettings",
				"adsched_tab_names.xml"
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

			PrintListsFile = new StorageFile(new[]
			{
				FileStorageManager.IncomingFolderName,
				AppProfileManager.Instance.AppName,
				"Data",
				"Print Strategy.xml"
			});
			await PrintListsFile.Download();

			CalendarLegendFile = new StorageFile(new[]
			{
				FileStorageManager.IncomingFolderName,
				FileStorageManager.CommonIncomingFolderName,
				"Calendar",
				"FileLegend.xls"
			});

			ViewSettingsFile = new StorageFile(new[]
			{
				FileStorageManager.LocalFilesFolderName,
				AppProfileManager.Instance.AppName,
				"ViewSetings.xml"
			});
			ViewSettingsFile.AllocateParentFolder();
		}
	}
}
