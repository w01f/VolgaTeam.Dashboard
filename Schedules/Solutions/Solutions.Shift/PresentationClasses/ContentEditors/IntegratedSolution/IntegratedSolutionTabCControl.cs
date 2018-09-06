using Asa.Business.Solutions.Shift.Configuration;
using Asa.Business.Solutions.Shift.Entities.NonPersistent;

namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors.IntegratedSolution
{
	class IntegratedSolutionTabCControl: IntegratedSolutionSubTabControl
	{
		protected override IntegratedSolutionState.SubTabState TabState =>
			SlideContainer.EditedContent.IntegratedSolutionState.TabC;
		
		public IntegratedSolutionTabCControl(IChildTabPageContainer slideContainer, ShiftChildTabInfo tabInfo) : base(slideContainer, tabInfo){}
	}
}
