using Asa.Business.Solutions.Shift.Configuration;
using Asa.Business.Solutions.Shift.Entities.NonPersistent;

namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors.IntegratedSolution
{
	class IntegratedSolutionTabEControl: IntegratedSolutionSubTabControl
	{
		protected override IntegratedSolutionState.SubTabState TabState =>
			SlideContainer.EditedContent.IntegratedSolutionState.TabE;
		
		public IntegratedSolutionTabEControl(IChildTabPageContainer slideContainer, ShiftChildTabInfo tabInfo) : base(slideContainer, tabInfo){}
	}
}
