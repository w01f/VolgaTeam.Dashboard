using System.Threading.Tasks;
using Asa.Common.Core.Extensions;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.RemoteStorage;

namespace Asa.Common.Core.Configuration
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
		public StorageDirectory FavoriteImagesFolder { get; private set; }
		public StorageDirectory UserListsFolder { get; private set; }

		public StorageFile SharedSettingsFile { get; private set; }
		public StorageFile AppSettingsFile { get; private set; }
		#endregion

		#region Remote
		public ArchiveDirectory RateCardFolder { get; private set; }
		public ArchiveDirectory MasterWizardsFolder { get; private set; }
		public ArchiveDirectory ScheduleSlideTemplatesFolder { get; private set; }
		public ArchiveDirectory CalendarSlideTemplatesFolder { get; private set; }
		public ArchiveDirectory SlideMastersFolder { get; private set; }
		public ArchiveDirectory ThemesFolder { get; private set; }
		public ArchiveDirectory ArtworkFolder { get; private set; }
		public ArchiveDirectory LauncherTemplatesFolder { get; private set; }
		public ArchiveDirectory SpecialAppsFolder { get; private set; }

		public StorageFile DashboardCodeFile { get; private set; }
		public StorageFile DefaultSlideSettingsFile { get; private set; }
		public StorageFile SlideSizeSettingsFile { get; private set; }
		public StorageFile HelpFile { get; private set; }
		public StorageFile HelpBrowserFile { get; private set; }
		public StorageFile OnlineListsFile { get; private set; }
		public StorageFile DataSimpleSummaryFile { get; private set; }
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

			FavoriteImagesFolder = new StorageDirectory(new[]
			{
				FileStorageManager.LocalFilesFolderName,
				"image_favorites"
			});
			if (!await FavoriteImagesFolder.Exists())
				await StorageDirectory.CreateSubFolder(new[] { FileStorageManager.LocalFilesFolderName }, "image_favorites");

			UserListsFolder = new StorageDirectory(new[]
			{
				FileStorageManager.LocalFilesFolderName,
				"user_lists"
			});
			if (!await UserListsFolder.Exists())
				await StorageDirectory.CreateSubFolder(new[] { FileStorageManager.LocalFilesFolderName }, "user_lists");

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
			MasterWizardsFolder = new ArchiveDirectory(new[]
			{
				FileStorageManager.IncomingFolderName,
				FileStorageManager.CommonIncomingFolderName,
				"Slides"
			});
			await MasterWizardsFolder.Download();

			ThemesFolder = new ArchiveDirectory(new[]
			{
				FileStorageManager.IncomingFolderName,
				FileStorageManager.CommonIncomingFolderName,
				"SellerPointThemes"
			});
			await ThemesFolder.Download();

			LauncherTemplatesFolder = new ArchiveDirectory(new[]
			{
				FileStorageManager.IncomingFolderName,
				FileStorageManager.CommonIncomingFolderName,
				"LauncherTemplates"
			});
			await LauncherTemplatesFolder.Download();

			SpecialAppsFolder = new ArchiveDirectory(new[]
			{
				FileStorageManager.IncomingFolderName,
				FileStorageManager.CommonIncomingFolderName,
				"SpecialApps"
			});
			if (await SpecialAppsFolder.Exists(true))
				await SpecialAppsFolder.Download();


			SlideMastersFolder = new ArchiveDirectory(new[]
			{
				FileStorageManager.IncomingFolderName,
				FileStorageManager.CommonIncomingFolderName,
				"SlidesTab"
			});


			ScheduleSlideTemplatesFolder = new ArchiveDirectory(new[]
			{
				FileStorageManager.IncomingFolderName,
				FileStorageManager.CommonIncomingFolderName,
				"ScheduleBuilders"
			});

			CalendarSlideTemplatesFolder = new ArchiveDirectory(new[]
			{
				FileStorageManager.IncomingFolderName,
				FileStorageManager.CommonIncomingFolderName,
				"Calendar"
			});

			ArtworkFolder = new ArchiveDirectory(new[]
			{
				FileStorageManager.IncomingFolderName,
				FileStorageManager.CommonIncomingFolderName,
				"Artwork"
			});

			RateCardFolder = new ArchiveDirectory(new[]
			{
				FileStorageManager.IncomingFolderName,
				FileStorageManager.CommonIncomingFolderName,
				"RateCard"
			});

			DashboardCodeFile = new StorageFile(new[]
			{
				FileStorageManager.IncomingFolderName,
				FileStorageManager.CommonIncomingFolderName,
				"AppSettings",
				"dashboard.xml"
			});
			await DashboardCodeFile.Download();

			DefaultSlideSettingsFile = new StorageFile(new[]
			{
				FileStorageManager.IncomingFolderName,
				FileStorageManager.CommonIncomingFolderName,
				"AppSettings",
				"DefaultSlideSettings.xml"
			});
			await DefaultSlideSettingsFile.Download();

			SlideSizeSettingsFile = new StorageFile(new[]
			{
				FileStorageManager.IncomingFolderName,
				FileStorageManager.CommonIncomingFolderName,
				"AppSettings",
				"SlideSizeSettings.xml"
			});
			await SlideSizeSettingsFile.Download();

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

			OnlineListsFile = new StorageFile(
				AppProfileManager.Instance.AppDataFolder.RelativePathParts.Merge("Online Strategy.xml"));
			if (await OnlineListsFile.Exists(true))
				await OnlineListsFile.Download();

			DataSimpleSummaryFile = new StorageFile(
				AppProfileManager.Instance.AppDataFolder.RelativePathParts.Merge("Closing Summary.xml"));
			if (await DataSimpleSummaryFile.Exists(true))
				await DataSimpleSummaryFile.Download();
			#endregion
		}
	}
}
