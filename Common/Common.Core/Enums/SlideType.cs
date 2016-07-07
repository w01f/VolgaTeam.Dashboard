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
	}
}
