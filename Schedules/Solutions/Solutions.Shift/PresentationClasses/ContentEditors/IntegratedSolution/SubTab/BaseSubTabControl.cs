using DevExpress.XtraTab;

namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors.IntegratedSolution.SubTab
{
	//public class BaseSubTabControl : UserControl
	public class BaseSubTabControl : XtraTabPage
	{
		public IntegratedSolutionSubTabControl Container { get; }
		public BaseShiftContainer SlideContainer => Container.SlideContainer;

		public BaseSubTabControl()
		{
		}

		public BaseSubTabControl(IntegratedSolutionSubTabControl container)
		{
			Container = container;
		}
	}
}