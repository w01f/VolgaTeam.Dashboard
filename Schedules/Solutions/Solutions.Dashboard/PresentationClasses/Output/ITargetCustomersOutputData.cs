namespace Asa.Solutions.Dashboard.PresentationClasses.Output
{
	public interface ITargetCustomersOutputData : IDashboardOutputData
	{
		string Title { get; }
		string TargetDemo { get; }
		string HHI { get; }
		string Geography { get; }
	}
}
