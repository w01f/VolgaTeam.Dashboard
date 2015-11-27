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
		TVProgramSchedule = 303,
		TVSnapshot = 304,
		TVOptions = 305,
		#endregion

		#region Radio Schedule
		RadioWebPackage = 401,
		RadioDigitalProduct = 402,
		RadioProgramSchedule = 403,
		RadioSnapshot = 404,
		RadioOptions = 405,
		#endregion

		WebQuick = 501,

		#region Common
		Summaries = 901,
		Summary1 = 902,
		Summary2 = 903,
		Strategy = 904,
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
