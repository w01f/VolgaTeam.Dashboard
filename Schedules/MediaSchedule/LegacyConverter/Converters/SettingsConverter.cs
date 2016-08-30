using System;
using Asa.Business.Media.Entities.NonPersistent.Schedule;
using Asa.Business.Online.Configuration;
using Asa.Legacy.Media.Entities.Schedule;
using Asa.Legacy.Media.Entities.Settings;

namespace Asa.Media.LegacyConverter.Converters
{
	public static class SettingsConverter
	{
		public static void ImportData(this MediaScheduleSettings target, RegularSchedule source)
		{
			target.BusinessName = source.BusinessName;
			target.DecisionMaker = source.DecisionMaker;
			target.Status = source.Status;
			target.PresentationDate = source.PresentationDate;
			target.UserFlightDateStart = source.UserFlightDateStart;
			target.UserFlightDateEnd = source.UserFlightDateEnd;
			target.MondayBased = source.MondayBased;
			target.UseDemo = source.UseDemo;
			target.ImportDemo = source.ImportDemo;
			target.Demo = source.Demo;
			target.Source = source.Source;
			target.DemoType = (Business.Media.Enums.DemoType)(Int32)source.DemoType;
			target.SelectedSpotType = (Business.Media.Enums.SpotType)(Int32)source.SelectedSpotType;
			target.Source = source.Source;

			foreach (var oldDaypart in source.Dayparts)
			{
				var daypart = new Business.Media.Entities.NonPersistent.Schedule.Daypart();
				daypart.ImportData(oldDaypart);
				target.Dayparts.Add(daypart);
			}

			foreach (var oldStation in source.Stations)
			{
				var station = new Business.Media.Entities.NonPersistent.Schedule.Station();
				station.ImportData(oldStation);
				target.Stations.Add(station);
			}

			target.DigitalProductListViewSettings.ImportData(source.ViewSettings.HomeViewSettings);
			target.DigitalPackageSettings.ImportData(source.ViewSettings.DigitalPackageSettings);
		}

		private static void ImportData(
			this Business.Media.Entities.NonPersistent.Schedule.Daypart target, 
			Legacy.Media.Entities.Schedule.Daypart source)
		{
			target.Name = source.Name;
			target.Code = source.Code;
			target.Available = source.Available;
		}

		private static void ImportData(
			this Business.Media.Entities.NonPersistent.Schedule.Station target, 
			Legacy.Media.Entities.Schedule.Station source)
		{
			target.Name = source.Name;
			target.Logo = source.Logo;
			target.Available = source.Available;
		}

		private static void ImportData(this DigitalProductListViewSettings target, HomeViewSettings source)
		{
			target.EnableDigitalDimensions = source.EnableDigitalDimensions;
			target.EnableDigitalLocation = source.EnableDigitalLocation;
			target.EnableDigitalRichMedia = source.EnableDigitalRichMedia;
			target.EnableDigitalStrategy = source.EnableDigitalStrategy;
			target.EnableDigitalTargeting = source.EnableDigitalTargeting;

			target.ShowDigitalDimensions = source.ShowDigitalDimensions;
			target.ShowDigitalLocation = source.ShowDigitalLocation;
			target.ShowDigitalRichMedia = source.ShowDigitalRichMedia;
			target.ShowDigitalStrategy = source.ShowDigitalStrategy;
			target.ShowDigitalTargeting = source.ShowDigitalTargeting;
		}

		private static void ImportData(
			this Business.Online.Configuration.DigitalPackageSettings target, 
			Legacy.Common.Entities.Digital.DigitalPackageSettings source)
		{
			target.EnableCategory = source.EnableCategory;
			target.EnableGroup = source.EnableGroup;
			target.EnableProduct = source.EnableProduct;
			target.EnableImpressions = source.EnableImpressions;
			target.EnableCPM = source.EnableCPM;
			target.EnableRate = source.EnableRate;
			target.EnableInvestment = source.EnableInvestment;
			target.EnableInfo = source.EnableInfo;
			target.EnableScreenshot = source.EnableScreenshot;

			target.ShowCategory = source.ShowCategory;
			target.ShowGroup = source.ShowGroup;
			target.ShowProduct = source.ShowProduct;
			target.ShowImpressions = source.ShowImpressions;
			target.ShowCPM = source.ShowCPM;
			target.ShowRate = source.ShowRate;
			target.ShowInvestment = source.ShowInvestment;
			target.ShowInfo = source.ShowInfo;
			target.ShowScreenshot = source.ShowScreenshot;
		}
	}
}
