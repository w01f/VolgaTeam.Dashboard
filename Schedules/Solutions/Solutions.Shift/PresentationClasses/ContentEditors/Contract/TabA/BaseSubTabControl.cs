using DevExpress.XtraTab;

namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors.Contract.TabA
{
	//public class BaseSubTabControl : UserControl
	public class BaseSubTabControl : XtraTabPage
	{
		public ContractTabAControl Container { get; }
		public BaseShiftContainer SlideContainer => Container.SlideContainer;

		public BaseSubTabControl() { }

		public BaseSubTabControl(ContractTabAControl container)
		{
			Container = container;
		}
	}
}
