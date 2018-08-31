using DevExpress.XtraTab;

namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors.IntegratedSolution.TabA
{
	//public class BaseTabASubControl : UserControl
	public class BaseTabASubControl : XtraTabPage
	{
		public IntegratedSolutionTabAControl Container { get; }
		public BaseShiftContainer SlideContainer => Container.SlideContainer;

		public BaseTabASubControl()
		{
		}

		public BaseTabASubControl(IntegratedSolutionTabAControl container)
		{
			Container = container;
		}
	}
}
