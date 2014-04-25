namespace NewBizWiz.Core.Common
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
		PrintSimpleSummary = 109,
		#endregion

		#region Online Schedule
		OnlineWebPackage = 201,
		OnlineDigitalProduct = 202,
		OnlineAdPlan = 203,
		DigitalSummary = 204,
		#endregion

		#region TV Schedule
		TVWebPackage = 301,
		TVDigitalProduct = 302,
		TVMonthlySchedule = 303,
		TVWeeklySchedule = 304,
		TVSummary = 305,
		#endregion

		#region Radio Schedule
		RadioWebPackage = 401,
		RadioDigitalProduct = 402,
		RadioMonthlySchedule = 403,
		RadioWeeklySchedule = 404,
		RadioSummary = 405,
		#endregion

		WebQuick = 501
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

	public enum NBWLinkType
	{
		None = 0,
		App,
		Url,
		SyncedFile,
		SimpleFile,
	}
}
