using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NewBizWiz.Core.Common
{
	public class ActivityManager
	{
		private XDocument _activityStorage;
		private readonly StorageDirectory _activityFolder;
		private readonly StorageFile _activityFile;

		private ActivityManager()
		{
			_activityFolder = AppProfileManager.Instance.UserDataFolder;
			_activityFile = new StorageFile(_activityFolder.RelativePathParts.Merge(String.Format("{0}.xml", DateTime.Now.ToString("MM-dd-yyyy"))));
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

		private async Task SaveStorage()
		{
			_activityStorage.Save(_activityFile.LocalPath);
			await _activityFile.Upload();
		}

		public async Task AddActivity(UserActivity activity)
		{
			_activityStorage.Root.Add(activity.Serialize());
			await SaveStorage();
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

	public class TabActivity : UserActivity
	{
		public string TabName { get; private set; }
		public string Advertiser { get; private set; }

		public TabActivity(string tabName, string advertiser = "")
			: base("Tab Selected")
		{
			TabName = tabName;
			Advertiser = advertiser;
		}

		public override XElement Serialize()
		{
			var element = base.Serialize();
			element.Add(new XAttribute("Tab", TabName));
			if (!String.IsNullOrEmpty(Advertiser))
				element.Add(new XAttribute("Advertiser", Advertiser));
			return element;
		}
	}

	public class PropertyEditActivity : UserActivity
	{
		public string Value { get; private set; }
		public string Advertiser { get; set; }

		public PropertyEditActivity(string propertName, string value, string editType = "", string advertiser = "")
			: base(String.Format("{0} {1}", propertName, !String.IsNullOrEmpty(editType) ? editType : "Changed"))
		{
			Value = value;
			Advertiser = advertiser;
		}

		public override XElement Serialize()
		{
			var element = base.Serialize();
			element.Add(new XAttribute("Value", Value));
			if (!String.IsNullOrEmpty(Advertiser))
				element.Add(new XAttribute("Advertiser", Advertiser));
			return element;
		}
	}

	public class OutputActivity : UserActivity
	{
		public string SlideName { get; private set; }
		public string Advertiser { get; private set; }
		public decimal? DollarValue { get; private set; }

		public OutputActivity(string slideName, string advertiser, decimal? dollarValue, Dictionary<string, object> otherOptions = null)
			: base("Output", otherOptions)
		{
			SlideName = slideName;
			Advertiser = advertiser;
			DollarValue = dollarValue;
		}

		public override XElement Serialize()
		{
			var element = base.Serialize();
			element.Add(new XAttribute("Slide", SlideName));
			if (!String.IsNullOrEmpty(Advertiser))
				element.Add(new XAttribute("Advertiser", Advertiser));
			if (DollarValue.HasValue)
				element.Add(new XAttribute("Dollars", DollarValue));
			return element;
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
