namespace Asa.Common.Core.Enums
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
		TVDigitalProduct = 301,
		TVProgramSchedule = 302,
		TVSnapshot = 303,
		TVOptions = 304,
		#endregion

		#region Radio Schedule
		RadioDigitalProduct = 401,
		RadioProgramSchedule = 402,
		RadioSnapshot = 403,
		RadioOptions = 404,
		#endregion

		WebQuick = 501,

		#region Common
		Summary1 = 901,
		Summary2 = 902,
		#endregion
	}
}
