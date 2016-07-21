namespace Asa.Solutions.Dashboard.PresentationClasses.Output
{
	public interface IClientGoalsOutputData : IDashboardOutputData
	{
		string Title { get; }
		int GoalsCount { get; }
		string[] SelectedGoals { get; }
	}
}
