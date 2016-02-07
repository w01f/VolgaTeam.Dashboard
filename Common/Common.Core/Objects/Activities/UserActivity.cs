using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Asa.Common.Core.Objects.Activities
{
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
}
