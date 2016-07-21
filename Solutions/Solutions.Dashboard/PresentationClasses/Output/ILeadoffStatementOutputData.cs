namespace Asa.Solutions.Dashboard.PresentationClasses.Output
{
	public interface ILeadoffStatementOutputData : IDashboardOutputData
	{
		string Title { get; }
		int StatementsCount { get; }
		string[] SelectedStatements { get; }
	}
}
