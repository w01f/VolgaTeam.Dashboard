using System;
using System.Drawing;
using System.Windows.Forms;
using Asa.Business.Solutions.Shift.Configuration;
using Asa.Business.Solutions.Shift.Enums;
using DevExpress.XtraTab;

namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors
{
	//public partial class ChildTabPageContainerControl<TChildTabControl> : UserControl, IChildTabPageContainer where TChildTabControl : ChildTabBaseControl
	public partial class ChildTabPageContainerControl<TChildTabControl> : XtraTabPage, IChildTabPageContainer where TChildTabControl : ChildTabBaseControl
	{
		private TChildTabControl _contentControl;
		public ShiftChildTabInfo TabInfo { get; }
		public MultiTabControl ParentControl { get; }
		public ChildTabBaseControl ContentControl => _contentControl;
		public bool OutputEnabled => TabInfo.IsRegularChildTab && (_contentControl?.GetOutputEnableState() ?? GetOutputEnabledStateIfControlNotLoaded());

		public ChildTabPageContainerControl(MultiTabControl multiTabControl, ShiftChildTabInfo tabInfo)
		{
			ParentControl = multiTabControl;
			TabInfo = tabInfo;
			InitializeComponent();

			Text = TabInfo.Title;

			FormatSlideHeader();
		}

		public void LoadContent()
		{
			if (_contentControl != null) return;
			_contentControl = (TChildTabControl)Activator.CreateInstance(typeof(TChildTabControl), this, TabInfo);
			_contentControl.Dock = DockStyle.Fill;
			ParentControl.SlideContainer.AssignCloseActiveEditorsOnOutsideClick(ContentControl);
			Controls.Add(_contentControl);
			_contentControl.ApplyBackground();
		}

		public void FormatSlideHeader()
		{
			if (TabInfo.IsRegularChildTab)
			{
				if (!OutputEnabled)
					Appearance.Header.ForeColor =
						Appearance.HeaderActive.ForeColor =
							Appearance.HeaderHotTracked.ForeColor = Color.Gray;
				else
					Appearance.Reset();
			}
		}

		private bool GetOutputEnabledStateIfControlNotLoaded()
		{
			switch (ParentControl.TabInfo.TabType)
			{
				case ShiftTopTabType.Cover:
					switch (TabInfo.TabType)
					{
						case ShiftChildTabType.A:
							return ParentControl.SlideContainer.EditedContent.CoverState.TabA.EnableOutput ?? TabInfo.EnableOutput;
						case ShiftChildTabType.B:
							return ParentControl.SlideContainer.EditedContent.CoverState.TabB.EnableOutput ?? TabInfo.EnableOutput;
						case ShiftChildTabType.C:
							return ParentControl.SlideContainer.EditedContent.CoverState.TabC.EnableOutput ?? TabInfo.EnableOutput;
						case ShiftChildTabType.D:
							return ParentControl.SlideContainer.EditedContent.CoverState.TabD.EnableOutput ?? TabInfo.EnableOutput;
						default:
							return TabInfo.EnableOutput;
					}
				case ShiftTopTabType.Intro:
					switch (TabInfo.TabType)
					{
						case ShiftChildTabType.A:
							return ParentControl.SlideContainer.EditedContent.IntroState.TabA.EnableOutput ?? TabInfo.EnableOutput;
						case ShiftChildTabType.B:
							return ParentControl.SlideContainer.EditedContent.IntroState.TabB.EnableOutput ?? TabInfo.EnableOutput;
						case ShiftChildTabType.C:
							return ParentControl.SlideContainer.EditedContent.IntroState.TabC.EnableOutput ?? TabInfo.EnableOutput;
						case ShiftChildTabType.D:
							return ParentControl.SlideContainer.EditedContent.IntroState.TabD.EnableOutput ?? TabInfo.EnableOutput;
						default:
							return TabInfo.EnableOutput;
					}
				case ShiftTopTabType.Agenda:
					switch (TabInfo.TabType)
					{
						case ShiftChildTabType.A:
							return ParentControl.SlideContainer.EditedContent.AgendaState.TabA.EnableOutput ?? TabInfo.EnableOutput;
						case ShiftChildTabType.B:
							return ParentControl.SlideContainer.EditedContent.AgendaState.TabB.EnableOutput ?? TabInfo.EnableOutput;
						case ShiftChildTabType.C:
							return ParentControl.SlideContainer.EditedContent.AgendaState.TabC.EnableOutput ?? TabInfo.EnableOutput;
						case ShiftChildTabType.D:
							return ParentControl.SlideContainer.EditedContent.AgendaState.TabD.EnableOutput ?? TabInfo.EnableOutput;
						case ShiftChildTabType.E:
							return ParentControl.SlideContainer.EditedContent.AgendaState.TabE.EnableOutput ?? TabInfo.EnableOutput;
						default:
							return TabInfo.EnableOutput;
					}
				case ShiftTopTabType.Goals:
					switch (TabInfo.TabType)
					{
						case ShiftChildTabType.A:
							return ParentControl.SlideContainer.EditedContent.GoalsState.TabA.EnableOutput ?? TabInfo.EnableOutput;
						case ShiftChildTabType.B:
							return ParentControl.SlideContainer.EditedContent.GoalsState.TabB.EnableOutput ?? TabInfo.EnableOutput;
						case ShiftChildTabType.C:
							return ParentControl.SlideContainer.EditedContent.GoalsState.TabC.EnableOutput ?? TabInfo.EnableOutput;
						case ShiftChildTabType.D:
							return ParentControl.SlideContainer.EditedContent.GoalsState.TabD.EnableOutput ?? TabInfo.EnableOutput;
						default:
							return TabInfo.EnableOutput;
					}
				case ShiftTopTabType.Market:
					switch (TabInfo.TabType)
					{
						case ShiftChildTabType.A:
							return ParentControl.SlideContainer.EditedContent.MarketState.TabA.EnableOutput ?? TabInfo.EnableOutput;
						case ShiftChildTabType.B:
							return ParentControl.SlideContainer.EditedContent.MarketState.TabB.EnableOutput ?? TabInfo.EnableOutput;
						case ShiftChildTabType.C:
							return ParentControl.SlideContainer.EditedContent.MarketState.TabC.EnableOutput ?? TabInfo.EnableOutput;
						case ShiftChildTabType.D:
							return ParentControl.SlideContainer.EditedContent.MarketState.TabD.EnableOutput ?? TabInfo.EnableOutput;
						case ShiftChildTabType.E:
							return ParentControl.SlideContainer.EditedContent.MarketState.TabE.EnableOutput ?? TabInfo.EnableOutput;
						default:
							return TabInfo.EnableOutput;
					}
				case ShiftTopTabType.Partnership:
					switch (TabInfo.TabType)
					{
						case ShiftChildTabType.A:
							return ParentControl.SlideContainer.EditedContent.PartnershipState.TabA.EnableOutput ?? TabInfo.EnableOutput;
						case ShiftChildTabType.B:
							return ParentControl.SlideContainer.EditedContent.PartnershipState.TabB.EnableOutput ?? TabInfo.EnableOutput;
						case ShiftChildTabType.C:
							return ParentControl.SlideContainer.EditedContent.PartnershipState.TabC.EnableOutput ?? TabInfo.EnableOutput;
						case ShiftChildTabType.D:
							return ParentControl.SlideContainer.EditedContent.PartnershipState.TabD.EnableOutput ?? TabInfo.EnableOutput;
						default:
							return TabInfo.EnableOutput;
					}
				case ShiftTopTabType.NeedsSolutions:
					switch (TabInfo.TabType)
					{
						case ShiftChildTabType.A:
							return ParentControl.SlideContainer.EditedContent.NeedsSolutionsState.TabA.EnableOutput ?? TabInfo.EnableOutput;
						case ShiftChildTabType.B:
							return ParentControl.SlideContainer.EditedContent.NeedsSolutionsState.TabB.EnableOutput ?? TabInfo.EnableOutput;
						case ShiftChildTabType.C:
							return ParentControl.SlideContainer.EditedContent.NeedsSolutionsState.TabC.EnableOutput ?? TabInfo.EnableOutput;
						case ShiftChildTabType.D:
							return ParentControl.SlideContainer.EditedContent.NeedsSolutionsState.TabD.EnableOutput ?? TabInfo.EnableOutput;
						case ShiftChildTabType.E:
							return ParentControl.SlideContainer.EditedContent.NeedsSolutionsState.TabE.EnableOutput ?? TabInfo.EnableOutput;
						case ShiftChildTabType.F:
							return ParentControl.SlideContainer.EditedContent.NeedsSolutionsState.TabF.EnableOutput ?? TabInfo.EnableOutput;
						default:
							return TabInfo.EnableOutput;
					}
				case ShiftTopTabType.CBC:
					switch (TabInfo.TabType)
					{
						case ShiftChildTabType.A:
							return ParentControl.SlideContainer.EditedContent.CBCState.TabA.EnableOutput ?? TabInfo.EnableOutput;
						case ShiftChildTabType.B:
							return ParentControl.SlideContainer.EditedContent.CBCState.TabB.EnableOutput ?? TabInfo.EnableOutput;
						case ShiftChildTabType.C:
							return ParentControl.SlideContainer.EditedContent.CBCState.TabC.EnableOutput ?? TabInfo.EnableOutput;
						case ShiftChildTabType.D:
							return ParentControl.SlideContainer.EditedContent.CBCState.TabD.EnableOutput ?? TabInfo.EnableOutput;
						case ShiftChildTabType.E:
							return ParentControl.SlideContainer.EditedContent.CBCState.TabE.EnableOutput ?? TabInfo.EnableOutput;
						case ShiftChildTabType.F:
							return ParentControl.SlideContainer.EditedContent.CBCState.TabF.EnableOutput ?? TabInfo.EnableOutput;
						default:
							return TabInfo.EnableOutput;
					}
				case ShiftTopTabType.IntegratedSolution:
					switch (TabInfo.TabType)
					{
						case ShiftChildTabType.A:
							return ParentControl.SlideContainer.EditedContent.IntegratedSolutionState.TabA.EnableOutput ?? TabInfo.EnableOutput;
						case ShiftChildTabType.B:
							return ParentControl.SlideContainer.EditedContent.IntegratedSolutionState.TabB.EnableOutput ?? TabInfo.EnableOutput;
						case ShiftChildTabType.C:
							return ParentControl.SlideContainer.EditedContent.IntegratedSolutionState.TabC.EnableOutput ?? TabInfo.EnableOutput;
						case ShiftChildTabType.D:
							return ParentControl.SlideContainer.EditedContent.IntegratedSolutionState.TabD.EnableOutput ?? TabInfo.EnableOutput;
						case ShiftChildTabType.E:
							return ParentControl.SlideContainer.EditedContent.IntegratedSolutionState.TabE.EnableOutput ?? TabInfo.EnableOutput;
						default:
							return TabInfo.EnableOutput;
					}
				case ShiftTopTabType.Investment:
					switch (TabInfo.TabType)
					{
						default:
							return TabInfo.EnableOutput;
					}
				case ShiftTopTabType.NextSteps:
					switch (TabInfo.TabType)
					{
						default:
							return TabInfo.EnableOutput;
					}
				case ShiftTopTabType.Contract:
					switch (TabInfo.TabType)
					{
						default:
							return TabInfo.EnableOutput;
					}
				case ShiftTopTabType.SupportMaterials:
					switch (TabInfo.TabType)
					{
						default:
							return TabInfo.EnableOutput;
					}
				case ShiftTopTabType.SpecBuilder:
					switch (TabInfo.TabType)
					{
						default:
							return TabInfo.EnableOutput;
					}
				case ShiftTopTabType.Approach:
					switch (TabInfo.TabType)
					{
						case ShiftChildTabType.A:
							return ParentControl.SlideContainer.EditedContent.ApproachState.TabA.EnableOutput ?? TabInfo.EnableOutput;
						case ShiftChildTabType.B:
							return ParentControl.SlideContainer.EditedContent.ApproachState.TabB.EnableOutput ?? TabInfo.EnableOutput;
						case ShiftChildTabType.C:
							return ParentControl.SlideContainer.EditedContent.ApproachState.TabC.EnableOutput ?? TabInfo.EnableOutput;
						default:
							return TabInfo.EnableOutput;
					}
				default:
					return TabInfo.EnableOutput;
			}
		}
	}
}
