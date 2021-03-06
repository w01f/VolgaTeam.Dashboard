﻿using Asa.Business.Common.Entities.NonPersistent.Schedule;
using Asa.Business.Common.Entities.NonPersistent.ScheduleResources;
using Asa.Business.Solutions.Common.Entities.NonPersistent;

namespace Asa.Business.Solutions.Shift.Entities.NonPersistent
{
	public abstract class ShiftContent : BaseSolutionContent
	{
		public abstract BaseScheduleSettings ScheduleSettings { get; }
		public abstract BaseScheduleResourceContainer ScheduleResources { get; }

		public CoverState CoverState { get; }
		public IntroState IntroState { get; }
		public AgendaState AgendaState { get; }
		public GoalsState GoalsState { get; }
		public MarketState MarketState { get; }
		public PartnershipState PartnershipState { get; }
		public NeedsSolutionsState NeedsSolutionsState { get; }
		public CBCState CBCState { get; }
		public IntegratedSolutionState IntegratedSolutionState { get; }
		public InvestmentState InvestmentState { get; }
		public NextStepsState NextStepsState { get; }
		public ContractState ContractState { get; }
		public ApproachState ApproachState { get; }
		public ROIState ROIState { get; }

		protected ShiftContent()
		{
			CoverState = new CoverState();
			IntroState = new IntroState();
			AgendaState = new AgendaState();
			GoalsState = new GoalsState();
			MarketState = new MarketState();
			PartnershipState = new PartnershipState();
			NeedsSolutionsState = new NeedsSolutionsState();
			CBCState = new CBCState();
			IntegratedSolutionState = new IntegratedSolutionState();
			InvestmentState = new InvestmentState();
			NextStepsState = new NextStepsState();
			ContractState = new ContractState();
			ApproachState = new ApproachState();
			ROIState = new ROIState();
		}

		protected override void AfterCreate()
		{
			base.AfterCreate();
		}
	}
}
