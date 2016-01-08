using System.Xml;
using Asa.Core.OnlineSchedule;

namespace Asa.Core.Common
{
	public interface IScheduleViewSettings
	{
		IHomeViewSettings SharedHomeViewSettings { get; }
		DigitalPackageSettings DigitalPackageSettings { get; }
		AdPlanViewSettings AdPlanViewSettings { get; }
	}

	public interface IHomeViewSettings
	{
		bool EnableAccountNumber { get; set; }
		bool EnableDigitalDimensions { get; set; }
		bool EnableDigitalStrategy { get; set; }
		bool EnableDigitalLocation { get; set; }
		bool EnableDigitalTargeting { get; set; }
		bool EnableDigitalRichMedia { get; set; }

		bool ShowAccountNumber { get; set; }
		bool ShowDigitalDimensions { get; set; }
		bool ShowDigitalStrategy { get; set; }
		bool ShowDigitalLocation { get; set; }
		bool ShowDigitalTargeting { get; set; }
		bool ShowDigitalRichMedia { get; set; }

		string Serialize();
		void Deserialize(XmlNode node);
	}
}
