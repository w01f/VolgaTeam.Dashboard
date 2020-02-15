using System;
using System.IO;
using System.Threading.Tasks;
using Asa.Common.Core.Extensions;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.RemoteStorage;

namespace Asa.Business.Media.Configuration
{
    public class ResourceManager
    {
        public static ResourceManager Instance { get; } = new ResourceManager();

        #region Station Independent
        public StorageFile MainAppTitleTextFile { get; private set; }
        public StorageFile FormStyleConfigFile { get; private set; }
        private StorageFile SyncFormColorConfigFile { get; set; }
        private StorageFile SyncFormTextConfigFile { get; set; }
        #endregion

        #region Station Dependent
        public StorageFile TabsConfigFile { get; private set; }
        public StorageFile SlideOutputConfigFile { get; private set; }
        public StorageFile BrowserConfigFile { get; private set; }
        public StorageFile Gallery1ConfigFile { get; private set; }
        public StorageFile Gallery2ConfigFile { get; private set; }
        public StorageFile MediaListsFile { get; private set; }
        public StorageFile SolutionsConfigFile { get; private set; }
        public StorageFile ConfigFile { get; private set; }
        public StorageFile TextResourcesFile { get; private set; }
        public StorageFile GraphicResourcesFile { get; private set; }
        public StorageFile AdditionalTextResourcesFile { get; private set; }
        public StorageFile IdleSettingsFile { get; private set; }

        public ArchiveDirectory ImageResourcesFolder { get; private set; }
        public StorageDirectory SolutionsDataFolder { get; private set; }
        #endregion

        private ResourceManager() { }

        public async Task LoadSubStorageIndependentResources()
        {
            await Asa.Common.Core.Configuration.ResourceManager.Instance.LoadSubStorageIndependentResources();

            await LoadSubStorageDependentResources();
        }

        private async Task LoadSubStorageDependentResources()
        {
            await Asa.Common.Core.Configuration.ResourceManager.Instance.LoadSubStorageDependentResources();

            await Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.Download();
            await Asa.Common.Core.Configuration.ResourceManager.Instance.CalendarSlideTemplatesFolder.Download();
            await Asa.Common.Core.Configuration.ResourceManager.Instance.ArtworkFolder.Download();
            await Asa.Common.Core.Configuration.ResourceManager.Instance.RateCardFolder.Download();

            var folderNameParts = !String.IsNullOrEmpty(AppProfileManager.Instance.SubStorageName)
                ? new[]
                {
                    FileStorageManager.IncomingFolderName,
                    AppProfileManager.Instance.AppSubStorageDependentFolderName,
                    AppProfileManager.Instance.SubStorageName,
                }
                : new[]
                {
                    FileStorageManager.IncomingFolderName,
                    AppProfileManager.Instance.AppSubStorageIndependentFolderName,
                };

            FormStyleConfigFile = new StorageFile(folderNameParts.Merge(new[]
                {
                    "AppSettings",
                    "style.xml"
                }));
            await FormStyleConfigFile.Download();

            MainAppTitleTextFile = new StorageFile(folderNameParts.Merge(new[]
            {
                "AppSettings",
                "app_brand.txt"
            }));
            if (await MainAppTitleTextFile.Exists(true))
                await MainAppTitleTextFile.Download();

            ConfigFile = new StorageFile(folderNameParts.Merge(new[]
            {
                "AppSettings",
                "app_common_config.xml"
            }));
            if (await ConfigFile.Exists(true))
                await ConfigFile.Download();

            TextResourcesFile = new StorageFile(folderNameParts.Merge(new[]
            {
                "AppSettings",
                "app_text_config.xml"
            }));
            if (await TextResourcesFile.Exists(true))
                await TextResourcesFile.Download();

            GraphicResourcesFile = new StorageFile(folderNameParts.Merge(new[]
            {
                "Asa.Media.Resources.dll"
            }));
            if (await GraphicResourcesFile.Exists(true, true))
                await GraphicResourcesFile.Download();

            AdditionalTextResourcesFile = new StorageFile(folderNameParts.Merge(new[]
            {
                "AppSettings",
                String.Format("{0}_subtab_names.xml",MediaMetaData.Instance.DataTypeString.ToLower())
            }));
            if (await AdditionalTextResourcesFile.Exists(true))
                await AdditionalTextResourcesFile.Download();

            SyncFormColorConfigFile = new StorageFile(folderNameParts.Merge(new[]
            {
                "AppSettings",
                "sync_color.xml"
            }));
            if (await SyncFormColorConfigFile.Exists(true))
                await SyncFormColorConfigFile.Download();

            SyncFormTextConfigFile = new StorageFile(folderNameParts.Merge(new[]
            {
                "AppSettings",
                "sync_text.xml"
            }));
            if (await SyncFormTextConfigFile.Exists(true))
                await SyncFormTextConfigFile.Download();

            TabsConfigFile = new StorageFile(folderNameParts.Merge(new[]
            {
                "AppSettings",
                $"{MediaMetaData.Instance.DataTypeString.ToLower()}_tab_names.xml"
            }));
            await TabsConfigFile.Download();

            SlideOutputConfigFile = new StorageFile(folderNameParts.Merge(new[]
            {
                "AppSettings",
                "OutputConfig.xml"
            }));
            await SlideOutputConfigFile.Download();

            BrowserConfigFile = new StorageFile(folderNameParts.Merge(new[]
            {
                "AppSettings",
                "eo.xml"
            }));
            await BrowserConfigFile.Download();

            Gallery1ConfigFile = new StorageFile(folderNameParts.Merge(new[]
            {
                "AppSettings",
                "Gallery1.xml"
            }));
            await Gallery1ConfigFile.Download();

            Gallery2ConfigFile = new StorageFile(folderNameParts.Merge(new[]
            {
                "AppSettings",
                "Gallery2.xml"
            }));
            await Gallery2ConfigFile.Download();

            SolutionsConfigFile = new StorageFile(folderNameParts.Merge(new[]
            {
                "AppSettings",
                "solution_templates.xml"
            }));
            await SolutionsConfigFile.Download();

            IdleSettingsFile = new StorageFile(folderNameParts.Merge(new[]
            {
                "AppSettings",
                "autoclose.xml"
            }));
            if (await IdleSettingsFile.Exists(true))
                await IdleSettingsFile.Download();

            ImageResourcesFolder = new ArchiveDirectory(folderNameParts.Merge(new[]
            {
                "Resources"
            }));
            if (await ImageResourcesFolder.Exists(true))
                await ImageResourcesFolder.Download();

            SolutionsDataFolder = new StorageDirectory(folderNameParts.Merge(new[]
            {
                "Solution Templates"
            }));

            MediaListsFile = new StorageFile(
                Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge(
                    String.Format("{0} Strategy.xml", MediaMetaData.Instance.DataTypeString)));

            #region Make local copy

            if (SyncFormColorConfigFile.ExistsLocal())
                File.Copy(SyncFormColorConfigFile.LocalPath, Path.Combine(Asa.Common.Core.Configuration.ResourceManager.Instance.AppRootFolderPath, Path.GetFileName(SyncFormColorConfigFile.LocalPath)), true);
            if (SyncFormTextConfigFile.ExistsLocal())
                File.Copy(SyncFormTextConfigFile.LocalPath, Path.Combine(Asa.Common.Core.Configuration.ResourceManager.Instance.AppRootFolderPath, Path.GetFileName(SyncFormTextConfigFile.LocalPath)), true);

            #endregion
        }
    }
}
