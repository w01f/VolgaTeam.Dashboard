using System;
using System.Xml.Linq;
using Asa.Core.Common;

namespace Asa.Bar.App.Common
{
	public enum AdBarActivityType
	{
		ApplicationOpen,
		ApplicationClose,
	}

	class AdBarActivity : UserActivity
	{
		public string ActivityData { get; private set; }

		public AdBarActivity(AdBarActivityType activityType, string activityData = "")
			: base(activityType.ToString())
		{
			ActivityData = activityData;
		}

		public override XElement Serialize()
		{
			var element = base.Serialize();
			if (!String.IsNullOrEmpty(ActivityData))
				element.Add(new XAttribute("ActivityData", ActivityData));
			return element;
		}
	}
}
