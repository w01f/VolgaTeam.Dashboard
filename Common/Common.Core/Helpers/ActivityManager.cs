using System;
using System.Xml.Linq;
using Asa.Common.Core.Extensions;
using Asa.Common.Core.Objects.Activities;
using Asa.Common.Core.Objects.RemoteStorage;

namespace Asa.Common.Core.Helpers
{
	public class ActivityManager
	{
		private XDocument _activityStorage;
		private readonly StorageFile _activityFile;

		private ActivityManager()
		{
			var activityFolder = AppProfileManager.Instance.UserDataFolder;
			_activityFile = new StorageFile(activityFolder.RelativePathParts.Merge(String.Format("{0}.xml", DateTime.Now.ToString("MM-dd-yyyy"))));
		}

		public static ActivityManager OpenStorage()
		{
			var instance = new ActivityManager();
			instance.Init();
			return instance;
		}

		private void Init()
		{
			if (_activityFile.ExistsLocal())
				_activityStorage = XDocument.Load(_activityFile.LocalPath);
			else
			{
				_activityStorage = new XDocument();
				var root = new XElement("UserActivities");
				_activityStorage.Add(root);
			}
		}

		private void SaveStorage()
		{
			_activityStorage.Save(_activityFile.LocalPath);
			//_activityFile.Upload();
		}

		public void AddActivity(UserActivity activity)
		{
			_activityStorage.Root.Add(activity.Serialize());
			SaveStorage();
		}
	}
}
