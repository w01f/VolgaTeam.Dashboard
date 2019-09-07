using System;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Extensions;
using Asa.Common.Core.Objects.RemoteStorage;

namespace Asa.Common.Core.Helpers
{
    public class AppProfileManager
    {
        public const string UserDataFolderName = "user_data";
        public const string SavedFilesFolderName = "saved_files";
        public const string SharedFolderName = "Shared";

        public AppTypeEnum AppType { get; private set; }
        private Guid _appID;
        private StorageFile _localAppIdFile;

        public static AppProfileManager Instance { get; } = new AppProfileManager();

        private string ProfileName => String.Format("AppID-{0}", _appID);

        private StorageDirectory ProfilesRootFolder { get; set; }
        private StorageDirectory ProfileFolder { get; set; }
        private StorageDirectory AppDataFolder { get; set; }

        public StorageDirectory SharedFolder { get; private set; }
        public StorageDirectory UserDataFolder { get; private set; }
        public StorageDirectory AppSaveFolder { get; private set; }

        private string AppName
        {
            get
            {
                switch (AppType)
                {
                    case AppTypeEnum.AdBar:
                        return "app_adsalesapps";
                    case AppTypeEnum.Dashboard:
                        return "app_6ms";
                    case AppTypeEnum.TVSchedule:
                        return "app_sellerpoint_tv";
                    case AppTypeEnum.RadioSchedule:
                        return "app_sellerpoint_radio";
                    case AppTypeEnum.PrintSchedule:
                        return "app_sellerpoint_np";
                }
                throw new InvalidEnumArgumentException("Storage Type Undefined");
            }
        }

        public string AppSubStorageIndependentFolderName => AppName;

        public string AppSubStorageDependentFolderName
        {
            get
            {
                var suffix = !string.IsNullOrEmpty(SubStorageName) ? "_clients" : string.Empty;

                switch (AppType)
                {
                    case AppTypeEnum.AdBar:
                        return $"app_adsalesapps{suffix}";
                    case AppTypeEnum.Dashboard:
                        return $"app_6ms{suffix}";
                    case AppTypeEnum.TVSchedule:
                        return $"app_sellerpoint_tv{suffix}";
                    case AppTypeEnum.RadioSchedule:
                        return $"app_sellerpoint_radio{suffix}";
                    case AppTypeEnum.PrintSchedule:
                        return $"app_sellerpoint_np{suffix}";
                }
                throw new InvalidEnumArgumentException("Storage Type Undefined");
            }
        }

        public string SubStorageName { get; set; }

        private AppProfileManager() { }

        public void InitApplication(AppTypeEnum appType)
        {
            AppType = appType;
            _localAppIdFile = new StorageFile(new[] { String.Format("{0}_app_id.xml", AppName) });
        }

        public async Task LoadProfile(bool useRemoteConnection = true)
        {
            _appID = Guid.Empty;
            if (File.Exists(_localAppIdFile.LocalPath))
            {
                var document = new XmlDocument();
                document.Load(_localAppIdFile.LocalPath);

                var node = document.SelectSingleNode(@"//Config/AppID");
                if (!string.IsNullOrEmpty(node?.InnerText))
                    _appID = new Guid(node.InnerText);

                SubStorageName = document.SelectSingleNode(@"//Config/SubStorageName")?.InnerText;
            }

            if (_appID.Equals(Guid.Empty))
                SaveProfile();

            ProfilesRootFolder = new StorageDirectory(new[] { FileStorageManager.OutgoingFolderName, AppName });
            if (!await ProfilesRootFolder.Exists(useRemoteConnection))
                await StorageDirectory.CreateSubFolder(new[] { FileStorageManager.OutgoingFolderName }, AppName, useRemoteConnection);

            ProfileFolder = new StorageDirectory(ProfilesRootFolder.RelativePathParts.Merge(ProfileName));
            if (!await ProfileFolder.Exists(useRemoteConnection))
                await StorageDirectory.CreateSubFolder(ProfilesRootFolder.RelativePathParts, ProfileName, useRemoteConnection);

            SharedFolder = new StorageDirectory(ProfilesRootFolder.RelativePathParts.Merge(SharedFolderName));
            if (!await ProfileFolder.Exists(useRemoteConnection))
                await StorageDirectory.CreateSubFolder(ProfilesRootFolder.RelativePathParts, SharedFolderName, useRemoteConnection);

            UserDataFolder = new StorageDirectory(ProfileFolder.RelativePathParts.Merge(new[] { UserDataFolderName }));
            if (!await UserDataFolder.Exists(useRemoteConnection))
                await StorageDirectory.CreateSubFolder(ProfileFolder.RelativePathParts, UserDataFolderName, useRemoteConnection);

            AppSaveFolder = new StorageDirectory(ProfileFolder.RelativePathParts.Merge(SavedFilesFolderName));
            if (!await AppSaveFolder.Exists(useRemoteConnection))
                await StorageDirectory.CreateSubFolder(ProfileFolder.RelativePathParts, SavedFilesFolderName, useRemoteConnection);

            AppDataFolder = new StorageDirectory(new[] { FileStorageManager.IncomingFolderName, AppName, "Data" });
        }

        public void SaveProfile()
        {
            if (_appID == Guid.Empty)
                _appID = Guid.NewGuid();
            
            var xml = new StringBuilder();

            xml.AppendLine(@"<Config>");

            xml.AppendLine(@"<AppID>" + _appID + @"</AppID>");

            if (!String.IsNullOrEmpty(SubStorageName))
                xml.AppendLine(@"<SubStorageName>" + SubStorageName + @"</SubStorageName>");

            xml.AppendLine(@"</Config>");

            _localAppIdFile.AllocateParentFolder();

            using (var sw = new StreamWriter(_localAppIdFile.LocalPath, false))
            {
                sw.Write(xml);
                sw.Flush();
            }
        }
    }
}
