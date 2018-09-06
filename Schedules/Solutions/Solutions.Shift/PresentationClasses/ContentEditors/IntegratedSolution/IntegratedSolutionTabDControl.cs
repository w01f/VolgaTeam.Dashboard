using Asa.Business.Solutions.Shift.Configuration;
using Asa.Business.Solutions.Shift.Entities.NonPersistent;

namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors.IntegratedSolution
{
	class IntegratedSolutionTabDControl: IntegratedSolutionSubTabControl
	{
		protected override IntegratedSolutionState.SubTabState TabState =>
			SlideContainer.EditedContent.IntegratedSolutionState.TabD;
		
		public IntegratedSolutionTabDControl(IChildTabPageContainer slideContainer, ShiftChildTabInfo tabInfo) : base(slideContainer, tabInfo){}
	}
}
