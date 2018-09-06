using Asa.Business.Solutions.Shift.Configuration;
using Asa.Business.Solutions.Shift.Entities.NonPersistent;

namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors.IntegratedSolution
{
	class IntegratedSolutionTabBControl: IntegratedSolutionSubTabControl
	{
		protected override IntegratedSolutionState.SubTabState TabState =>
			SlideContainer.EditedContent.IntegratedSolutionState.TabB;
		
		public IntegratedSolutionTabBControl(IChildTabPageContainer slideContainer, ShiftChildTabInfo tabInfo) : base(slideContainer, tabInfo){}
	}
}
