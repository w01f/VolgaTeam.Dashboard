using System.Threading.Tasks;

namespace NewBizWiz.Core.Common
{
	public class ResourceManager
	{
		private static readonly ResourceManager _instance = new ResourceManager();

		public static ResourceManager Instance
		{
			get { return _instance; }
		}

		#region Local
		public StorageDirectory AppSettingsFolder { get; private set; }
		public StorageDirectory TempFolder { get; private set; }

		public StorageFile SharedSettingsFile { get; private set; }
		public StorageFile AppSettingsFile { get; private set; }
		#endregion

		#region Remote
		public StorageDirectory RateCardFolder { get; private set; }
		public StorageDirectory SlideTemplatesFolder { get; private set; }
		public StorageDirectory SlideMastersFolder { get; private set; }
		public StorageDirectory ThemesFolder { get; private set; }

		public StorageFile DashboardCodeFile { get; private set; }
		public StorageFile HelpFile { get; private set; }
		public StorageFile HelpBrowserFile { get; private set; }
		#endregion

		private ResourceManager() { }

		public async Task Load()
		{
			#region Local
			TempFolder = new StorageDirectory(new[]
			{
				"Temp"
			});
			if (!await TempFolder.Exists())
				await StorageDirectory.CreateSubFolder(new string[] { }, "Temp");

			AppSettingsFolder = new StorageDirectory(new[]
			{
				FileStorageManager.LocalFilesFolderName,
				AppProfileManager.Instance.AppName,
			});
			if (!await AppSettingsFolder.Exists())
				await StorageDirectory.CreateSubFolder(new[] { FileStorageManager.LocalFilesFolderName }, AppProfileManager.Instance.AppName);

			SharedSettingsFile = new StorageFile(new[]
			{
				FileStorageManager.LocalFilesFolderName,
				FileStorageManager.CommonIncomingFolderName,
				"Settings.xml"
			});
			SharedSettingsFile.AllocateParentFolder();

			AppSettingsFile = new StorageFile(new[]
			{
				FileStorageManager.LocalFilesFolderName,
				AppProfileManager.Instance.AppName,
				"Settings.xml"
			});
			AppSettingsFile.AllocateParentFolder();
			#endregion

			#region Remote
			RateCardFolder = new StorageDirectory(new[]
			{
				FileStorageManager.IncomingFolderName,
				FileStorageManager.CommonIncomingFolderName,
				"RateCard"
			});

			SlideTemplatesFolder = new StorageDirectory(new[]
			{
				FileStorageManager.IncomingFolderName,
				FileStorageManager.CommonIncomingFolderName,
				"Slides"
			});

			SlideMastersFolder = new StorageDirectory(new[]
			{
				FileStorageManager.IncomingFolderName,
				FileStorageManager.CommonIncomingFolderName,
				"SlidesTab"
			});

			ThemesFolder = new StorageDirectory(new[]
			{
				FileStorageManager.IncomingFolderName,
				FileStorageManager.CommonIncomingFolderName,
				"SellerPointThemes"
			});

			DashboardCodeFile = new StorageFile(new[]
			{
				FileStorageManager.IncomingFolderName,
				FileStorageManager.CommonIncomingFolderName,
				"AppSettings",
				"dashboard.xml"
			});
			await DashboardCodeFile.Download();

			HelpFile = new StorageFile(new[]
			{
				FileStorageManager.IncomingFolderName,
				FileStorageManager.CommonIncomingFolderName,
				"HelpUrls",
				HelpManager.GetFileName()
			});
			await HelpFile.Download();

			HelpBrowserFile = new StorageFile(new[]
			{
				FileStorageManager.IncomingFolderName,
				FileStorageManager.CommonIncomingFolderName,
				"HelpUrls",
				"!Help_Browser.xml"
			});
			await HelpBrowserFile.Download();
			#endregion
		}
	}
}
