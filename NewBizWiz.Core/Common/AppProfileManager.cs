using System;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Asa.Core.Common
{
	public enum AppTypeEnum
	{
		None = 0,
		AdBar,
		Dashboard,
		TVSchedule,
		RadioSchedule,
		PrintSchedule
	}

	public class AppProfileManager
	{
		public const string UserDataFolderName = "user_data";
		public const string SavedFilesFolderName = "saved_files";

		private static readonly AppProfileManager _instance = new AppProfileManager();

		public AppTypeEnum AppType { get; private set; }
		private Guid _appID;
		private StorageFile _localAppIdFile;

		public static AppProfileManager Instance
		{
			get { return _instance; }
		}

		public string AppName
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

		private string ProfileName
		{
			get { return String.Format("AppID-{0}", _appID); }
		}

		public StorageDirectory ProfilesRootFolder { get; private set; }
		public StorageDirectory ProfileFolder { get; private set; }
		public StorageDirectory UserDataFolder { get; private set; }
		public StorageDirectory AppSaveFolder { get; private set; }

		private AppProfileManager() {}

		public void InitApplication(AppTypeEnum appType)
		{
			AppType = appType;
		}

		public async Task LoadProfile()
		{
			_localAppIdFile = new StorageFile(new[] { String.Format("{0}_app_id.xml", AppName) });

			_appID = Guid.Empty;
			if (File.Exists(_localAppIdFile.LocalPath))
			{
				var document = new XmlDocument();
				document.Load(_localAppIdFile.LocalPath);

				var node = document.SelectSingleNode(@"/AppID");
				if (node != null)
					if (!string.IsNullOrEmpty(node.InnerText))
						_appID = new Guid(node.InnerText);
			}

			if (_appID.Equals(Guid.Empty))
				CreateProfile();

			ProfilesRootFolder = new StorageDirectory(new[] { FileStorageManager.OutgoingFolderName, AppName });
			if (!await ProfilesRootFolder.Exists(true))
				await StorageDirectory.CreateSubFolder(new[] { FileStorageManager.OutgoingFolderName }, AppName, true);

			ProfileFolder = new StorageDirectory(ProfilesRootFolder.RelativePathParts.Merge(ProfileName));
			if (!await ProfileFolder.Exists(true))
				await StorageDirectory.CreateSubFolder(ProfilesRootFolder.RelativePathParts, ProfileName, true);

			UserDataFolder = new StorageDirectory(ProfileFolder.RelativePathParts.Merge(new[] { UserDataFolderName }));
			if (!await UserDataFolder.Exists(true))
				await StorageDirectory.CreateSubFolder(ProfileFolder.RelativePathParts, UserDataFolderName, true);

			AppSaveFolder = new StorageDirectory(ProfileFolder.RelativePathParts.Merge(SavedFilesFolderName));
			if (!await AppSaveFolder.Exists(true))
				await StorageDirectory.CreateSubFolder(ProfileFolder.RelativePathParts, SavedFilesFolderName, true);
		}

		private void CreateProfile()
		{
			_appID = Guid.NewGuid();
			var xml = new StringBuilder();

			xml.AppendLine(@"<AppID>" + _appID + @"</AppID>");

			_localAppIdFile.AllocateParentFolder();
			using (var sw = new StreamWriter(_localAppIdFile.LocalPath, false))
			{
				sw.Write(xml);
				sw.Flush();
			}
		}
	}
}
