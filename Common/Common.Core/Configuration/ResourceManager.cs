using System;
using System.IO;
using System.Threading.Tasks;
using Asa.Common.Core.Extensions;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.RemoteStorage;

namespace Asa.Common.Core.Configuration
{
    public class ResourceManager
    {
        public static ResourceManager Instance { get; } = new ResourceManager();

        #region Local
        public string AppRootFolderPath { get; }
        public StorageDirectory AppSettingsFolder { get; private set; }
        public StorageDirectory TempFolder { get; private set; }
        public StorageDirectory FavoriteImagesFolder { get; private set; }
        public StorageDirectory UserListsFolder { get; private set; }

        public StorageFile SharedSettingsFile { get; private set; }
        public StorageFile AppSettingsFile { get; private set; }
        #endregion

        #region Remote

        #region Station Independent
        public ArchiveDirectory LauncherTemplatesFolder { get; private set; }
        public ArchiveDirectory SpecialAppsFolder { get; private set; }
        #endregion

        #region Station Dependent
        public ArchiveDirectory DictionariesFolder { get; private set; }
        public ArchiveDirectory RateCardFolder { get; private set; }
        public ArchiveDirectory MasterWizardsFolder { get; private set; }
        public ArchiveDirectory ScheduleSlideTemplatesFolder { get; private set; }
        public ArchiveDirectory CalendarSlideTemplatesFolder { get; private set; }
        public ArchiveDirectory SlideMastersFolder { get; private set; }
        public ArchiveDirectory ThemesFolder { get; private set; }
        public ArchiveDirectory ArtworkFolder { get; private set; }
        
        public StorageFile DefaultSlideSettingsFile { get; private set; }
        public StorageFile SlideSizeSettingsFile { get; private set; }
        public StorageFile HelpFile { get; private set; }
        public StorageFile HelpBrowserFile { get; private set; }
        public StorageFile OnlineListsFile { get; private set; }
        public StorageFile DataSimpleSummaryFile { get; private set; }
        #endregion

        #endregion

        private ResourceManager()
        {
            AppRootFolderPath = Path.GetDirectoryName(typeof(ResourceManager).Assembly.Location);
        }

        public async Task LoadSubStorageIndependentResources()
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
                AppProfileManager.Instance.AppSubStorageIndependentFolderName,
            });
            if (!await AppSettingsFolder.Exists())
                await StorageDirectory.CreateSubFolder(new[] { FileStorageManager.LocalFilesFolderName }, AppProfileManager.Instance.AppSubStorageIndependentFolderName);

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
                AppProfileManager.Instance.AppSubStorageIndependentFolderName,
                "Settings.xml"
            });
            AppSettingsFile.AllocateParentFolder();
            #endregion

            #region Remote
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
            #endregion
        }

        public async Task LoadSubStorageDependentResources()
        {
            var folderNameParts = !String.IsNullOrEmpty(AppProfileManager.Instance.SubStorageName)
                ? new[]
                {
                    FileStorageManager.IncomingFolderName,
                    $"{FileStorageManager.CommonIncomingFolderName}_clients",
                    AppProfileManager.Instance.SubStorageName,
                }
                : new[]
                {
                    FileStorageManager.IncomingFolderName,
                    FileStorageManager.CommonIncomingFolderName,
                };

            #region Remote
            DictionariesFolder = new ArchiveDirectory(folderNameParts.Merge(new[]
            {
                "ad_sales_data"
            }));

            MasterWizardsFolder = new ArchiveDirectory(folderNameParts.Merge(new[]
            {
                "Slides"
            }));
            await MasterWizardsFolder.Download();

            ThemesFolder = new ArchiveDirectory(folderNameParts.Merge(new[]
            {
                "SellerPointThemes"
            }));
            await ThemesFolder.Download();

            SlideMastersFolder = new ArchiveDirectory(folderNameParts.Merge(new[]
            {
                "SlidesTab"
            }));
            if (await SlideMastersFolder.Exists(true))
                await SlideMastersFolder.Download();

            ScheduleSlideTemplatesFolder = new ArchiveDirectory(folderNameParts.Merge(new[]
            {
                "ScheduleBuilders"
            }));

            CalendarSlideTemplatesFolder = new ArchiveDirectory(folderNameParts.Merge(new[]
            {
                "Calendar"
            }));

            ArtworkFolder = new ArchiveDirectory(folderNameParts.Merge(new[]
            {
                "Artwork"
            }));

            RateCardFolder = new ArchiveDirectory(folderNameParts.Merge(new[]
            {
                "RateCard"
            }));

            DefaultSlideSettingsFile = new StorageFile(folderNameParts.Merge(new[]
            {
                "AppSettings",
                "DefaultSlideSettings.xml"
            }));
            await DefaultSlideSettingsFile.Download();

            SlideSizeSettingsFile = new StorageFile(folderNameParts.Merge(new[]
            {
                "AppSettings",
                "SlideSizeSettings.xml"
            }));
            await SlideSizeSettingsFile.Download();

            HelpFile = new StorageFile(folderNameParts.Merge(new[]
            {
                "HelpUrls",
                HelpManager.GetFileName()
            }));
            await HelpFile.Download();

            HelpBrowserFile = new StorageFile(folderNameParts.Merge(new[]
            {
                "HelpUrls",
                "!Help_Browser.xml"
            }));
            await HelpBrowserFile.Download();

            OnlineListsFile = new StorageFile(
                DictionariesFolder.RelativePathParts.Merge("Online Strategy.xml"));

            DataSimpleSummaryFile = new StorageFile(
                DictionariesFolder.RelativePathParts.Merge("Closing Summary.xml"));

            #endregion
        }
    }
}
