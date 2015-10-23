namespace Asa.Core.Common
{
	public enum SlideType
	{
		None = 0,

		#region Dashboard
		Cleanslate = 1,
		Cover = 2,
		LeadoffStatement = 3,
		ClientGoals = 4,
		TargetCustomers = 5,
		SimpleSummary = 6,
		#endregion

		#region Ad Schedule
		PrintWebPackage = 101,
		PrintDigitalProduct = 102,
		PrintBasicOverview = 103,
		PrintMultiSummary = 104,
		PrintSnapshot = 105,
		PrintDetailedGrid = 106,
		PrintMultiGrid = 107,
		PrintAdPlan = 108,
		#endregion

		#region Online Schedule
		OnlineWebPackage = 201,
		OnlineDigitalProduct = 202,
		OnlineAdPlan = 203,
		#endregion

		#region TV Schedule
		TVWebPackage = 301,
		TVDigitalProduct = 302,
		TVMonthlySchedule = 303,
		TVWeeklySchedule = 304,
		TVSnapshot = 305,
		TVOptions = 306,
		#endregion

		#region Radio Schedule
		RadioWebPackage = 401,
		RadioDigitalProduct = 402,
		RadioMonthlySchedule = 403,
		RadioWeeklySchedule = 404,
		RadioSnapshot = 405,
		RadioOptions = 406,
		#endregion

		WebQuick = 501,

		#region Common
		Summary1 = 901,
		Summary2 = 902,
		Strategy = 903,
		#endregion

	}

	public enum BrowserType
	{
		Default = 0,
		Chrome,
		Firefox,
		Safari,
		Opera,
		IE
	}
}
