using System;
using System.IO;
using System.Text;
using System.Xml;

namespace NewBizWiz.Core.Common
{
	public class AppProfileManager
	{
		private static readonly AppProfileManager _instance = new AppProfileManager();

		private Guid _appID;
		private readonly StorageFile _localAppIdFile;

		public static AppProfileManager Instance
		{
			get { return _instance; }
		}

		public string ProfilePath
		{
			get { return String.Format("AppID-{0}", _appID); }
		}

		private AppProfileManager()
		{
			_localAppIdFile = new StorageFile(new[] { "AppID.xml" });
		}

		public async void LoadProfile()
		{
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

			var ifExisted = await StorageDirectory.Exists(new[] { FileStorageManager.OutgoingFolderName, ProfilePath }, true);
			if (!ifExisted)
				await StorageDirectory.CreateSubFolder(new[] { FileStorageManager.OutgoingFolderName }, ProfilePath);
		}

		private void CreateProfile()
		{
			_appID = Guid.NewGuid();
			var xml = new StringBuilder();

			xml.AppendLine(@"<AppID>" + _appID + @"</AppID>");
			using (var sw = new StreamWriter(_localAppIdFile.LocalPath, false))
			{
				sw.Write(xml);
				sw.Flush();
			}
		}
	}
}
