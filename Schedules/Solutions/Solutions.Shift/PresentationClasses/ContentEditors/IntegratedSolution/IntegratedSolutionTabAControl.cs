﻿using Asa.Business.Solutions.Shift.Configuration;
using Asa.Business.Solutions.Shift.Entities.NonPersistent;

namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors.IntegratedSolution
{
	class IntegratedSolutionTabAControl: IntegratedSolutionSubTabControl
	{
		protected override IntegratedSolutionState.SubTabState TabState =>
			SlideContainer.EditedContent.IntegratedSolutionState.TabA;
		
		public IntegratedSolutionTabAControl(IChildTabPageContainer slideContainer, ShiftChildTabInfo tabInfo) : base(slideContainer, tabInfo){}
	}
}
