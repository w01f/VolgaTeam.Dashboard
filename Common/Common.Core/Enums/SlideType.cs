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

		#region TV Schedule
		TVSchedulePrograms = 1011,
		TVScheduleDigital = 1012,
		TVScheduleSummary = 1013,

		TVSnapshotPrograms = 1021,
		TVSnapshotDigital = 1022,
		TVSnapshotSummary = 1023,

		TVOptionsPrograms = 1031,
		TVOptionsDigital = 1032,
		TVOptionstSummary = 1034,
		#endregion

		#region Radio Schedule
		RadioSchedulePrograms = 2011,
		RadioScheduleDigital = 2012,
		RadioScheduleSummary = 2013,

		RadioSnapshotPrograms = 2021,
		RadioSnapshotDigital = 2022,
		RadioSnapshotSummary = 2023,

		RadioOptionsPrograms = 2031,
		RadioOptionsDigital = 2032,
		RadioOptionstSummary = 2034,
		#endregion

		#region Digital
		DigitalProducts = 301,
		DigitalSummary = 302,
		DigitalProductPackage = 303,
		DigitalStandalonePackage = 304,
		#endregion

		#region StarApp
		StarAppCleanslate = 401,
		StarAppCover = 402,
		StarAppCNA = 403,
		StarAppFishing = 404,
		StarAppCustomer = 405,
		StarAppShare = 406,
		StarAppROI = 407,
		StarAppMarket = 408,
		StarAppVideo = 409,
		StarAppAudience = 410,
		StarAppSolution = 411,
		StarAppClosers = 412,
		#endregion

		#region Shift
		ShiftCleanslate = 5010,
		ShiftCoverA = 5021,
		ShiftCoverB = 5022,
		ShiftCoverC = 5023,
		ShiftCoverD = 5024,
		ShiftIntroA = 5031,
		ShiftIntroB = 5032,
		ShiftIntroC = 5033,
		ShiftIntroD = 5034,
		ShiftAgendaA = 5041,
		ShiftAgendaB = 5042,
		ShiftAgendaC = 5043,
		ShiftAgendaD = 5044,
		ShiftGoals = 5050,
		ShiftMarket = 5060,
		ShiftPartnership = 5070,
		ShiftNeedsSolutions = 5080,
		ShiftCBC = 5090,
		ShiftIntegratedSolution = 5100,
		ShiftInvestment = 5110,
		ShiftClosers = 5120,
		ShiftNextSteps = 5130,
		ShiftContract = 5140,
		ShiftSupportMaterials = 5150,
		#endregion
	}
}
