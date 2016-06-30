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

		#region Online
		OnlineDigitalProduct = 201,
		OnlineWebPackage = 202,
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
	}
}
