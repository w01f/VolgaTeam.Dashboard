using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Asa.Core.Common
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

	public class UserActivity
	{
		public string UserName { get; private set; }
		public string ActivityType { get; private set; }
		public DateTime ActivityTime { get; private set; }
		public Dictionary<string, object> OtherOptions { get; private set; }

		public UserActivity(string activityType, Dictionary<string, object> otherOptions = null)
		{
			ActivityTime = DateTime.Now;
			UserName = Environment.UserName;
			ActivityType = activityType;
			OtherOptions = new Dictionary<string, object>();
			if (otherOptions != null)
				foreach (var option in otherOptions)
					OtherOptions.Add(option.Key, option.Value);
		}

		public virtual XElement Serialize()
		{
			var activityElement = new XElement("UserActivity");
			activityElement.Add(new XAttribute("UserName", UserName));
			activityElement.Add(new XAttribute("ActivityType", ActivityType));
			activityElement.Add(new XAttribute("ActivityTime", ActivityTime));
			foreach (var otherOption in OtherOptions)
			{
				var list = otherOption.Value as IEnumerable;
				if (list != null && !(otherOption.Value is String))
				{
					foreach (var item in list)
						activityElement.Add(new XElement(otherOption.Key, item));
				}
				else
					activityElement.Add(new XAttribute(otherOption.Key, otherOption.Value));
			}
			return activityElement;
		}
	}

	public class ScheduleActivity : UserActivity
	{
		public string ActionName { get; private set; }
		public string ScheduleName { get; private set; }

		public ScheduleActivity(string actionName, string scheduleName, Dictionary<string, object> otherOptions = null)
			: base("Schedule", otherOptions)
		{
			ActionName = actionName;
			ScheduleName = scheduleName;
		}

		public override XElement Serialize()
		{
			var element = base.Serialize();
			element.Add(new XAttribute("Action", ActionName));
			element.Add(new XAttribute("ScheduleName", ScheduleName));
			return element;
		}
	}
}
