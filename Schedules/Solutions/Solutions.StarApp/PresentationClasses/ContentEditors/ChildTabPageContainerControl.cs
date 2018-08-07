using System;
using System.Drawing;
using System.Windows.Forms;
using Asa.Business.Solutions.StarApp.Configuration;
using Asa.Business.Solutions.StarApp.Enums;
using DevExpress.XtraTab;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors
{
	//public partial class ChildTabPageContainerControl<TChildTabControl> : UserControl, IChildTabPageContainer where TChildTabControl : ChildTabBaseControl
	public partial class ChildTabPageContainerControl<TChildTabControl> : XtraTabPage, IChildTabPageContainer where TChildTabControl : ChildTabBaseControl
	{
		private TChildTabControl _contentControl;
		public StarChildTabInfo TabInfo { get; }
		public MultiTabControl ParentControl { get; }
		public ChildTabBaseControl ContentControl => _contentControl;
		public bool OutputEnabled => TabInfo.IsRegularChildTab && (_contentControl?.GetOutputEnableState() ?? GetOutputEnabledStateIfControlNotLoaded());

		public ChildTabPageContainerControl(MultiTabControl multiTabControl, StarChildTabInfo tabInfo)
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
				case StarTopTabType.Cover:
					switch (TabInfo.TabType)
					{
						case StarChildTabType.A:
							return ParentControl.SlideContainer.EditedContent.CoverState.TabA.EnableOutput ?? TabInfo.EnableOutput;
						default:
							return TabInfo.EnableOutput;
					}
				case StarTopTabType.CNA:
					switch (TabInfo.TabType)
					{
						case StarChildTabType.A:
							return ParentControl.SlideContainer.EditedContent.CNAState.TabA.EnableOutput ?? TabInfo.EnableOutput;
						case StarChildTabType.B:
							return ParentControl.SlideContainer.EditedContent.CNAState.TabB.EnableOutput ?? TabInfo.EnableOutput;
						default:
							return TabInfo.EnableOutput;
					}
				case StarTopTabType.Fishing:
					switch (TabInfo.TabType)
					{
						case StarChildTabType.A:
							return ParentControl.SlideContainer.EditedContent.FishingState.TabA.EnableOutput ?? TabInfo.EnableOutput;
						case StarChildTabType.B:
							return ParentControl.SlideContainer.EditedContent.FishingState.TabB.EnableOutput ?? TabInfo.EnableOutput;
						case StarChildTabType.C:
							return ParentControl.SlideContainer.EditedContent.FishingState.TabC.EnableOutput ?? TabInfo.EnableOutput;
						default:
							return TabInfo.EnableOutput;
					}
				case StarTopTabType.Customer:
					switch (TabInfo.TabType)
					{
						case StarChildTabType.A:
							return ParentControl.SlideContainer.EditedContent.CustomerState.TabA.EnableOutput ?? TabInfo.EnableOutput;
						case StarChildTabType.B:
							return ParentControl.SlideContainer.EditedContent.CustomerState.TabB.EnableOutput ?? TabInfo.EnableOutput;
						case StarChildTabType.C:
							return ParentControl.SlideContainer.EditedContent.CustomerState.TabC.EnableOutput ?? TabInfo.EnableOutput;
						default:
							return TabInfo.EnableOutput;
					}
				case StarTopTabType.Share:
					switch (TabInfo.TabType)
					{
						case StarChildTabType.A:
							return ParentControl.SlideContainer.EditedContent.ShareState.TabA.EnableOutput ?? TabInfo.EnableOutput;
						case StarChildTabType.B:
							return ParentControl.SlideContainer.EditedContent.ShareState.TabB.EnableOutput ?? TabInfo.EnableOutput;
						case StarChildTabType.C:
							return ParentControl.SlideContainer.EditedContent.ShareState.TabC.EnableOutput ?? TabInfo.EnableOutput;
						case StarChildTabType.D:
							return ParentControl.SlideContainer.EditedContent.ShareState.TabD.EnableOutput ?? TabInfo.EnableOutput;
						case StarChildTabType.E:
							return ParentControl.SlideContainer.EditedContent.ShareState.TabE.EnableOutput ?? TabInfo.EnableOutput;
						default:
							return TabInfo.EnableOutput;
					}
				case StarTopTabType.ROI:
					switch (TabInfo.TabType)
					{
						case StarChildTabType.A:
							return ParentControl.SlideContainer.EditedContent.ROIState.TabA.EnableOutput ?? TabInfo.EnableOutput;
						case StarChildTabType.B:
							return ParentControl.SlideContainer.EditedContent.ROIState.TabB.EnableOutput ?? TabInfo.EnableOutput;
						case StarChildTabType.C:
							return ParentControl.SlideContainer.EditedContent.ROIState.TabC.EnableOutput ?? TabInfo.EnableOutput;
						case StarChildTabType.D:
							return ParentControl.SlideContainer.EditedContent.ROIState.TabD.EnableOutput ?? TabInfo.EnableOutput;
						default:
							return TabInfo.EnableOutput;
					}
				case StarTopTabType.Market:
					switch (TabInfo.TabType)
					{
						case StarChildTabType.A:
							return ParentControl.SlideContainer.EditedContent.MarketState.TabA.EnableOutput ?? TabInfo.EnableOutput;
						case StarChildTabType.B:
							return ParentControl.SlideContainer.EditedContent.MarketState.TabB.EnableOutput ?? TabInfo.EnableOutput;
						case StarChildTabType.C:
							return ParentControl.SlideContainer.EditedContent.MarketState.TabC.EnableOutput ?? TabInfo.EnableOutput;
						default:
							return TabInfo.EnableOutput;
					}
				case StarTopTabType.Video:
					switch (TabInfo.TabType)
					{
						case StarChildTabType.A:
							return ParentControl.SlideContainer.EditedContent.VideoState.TabA.EnableOutput ?? TabInfo.EnableOutput;
						case StarChildTabType.B:
							return ParentControl.SlideContainer.EditedContent.VideoState.TabB.EnableOutput ?? TabInfo.EnableOutput;
						case StarChildTabType.C:
							return ParentControl.SlideContainer.EditedContent.VideoState.TabC.EnableOutput ?? TabInfo.EnableOutput;
						case StarChildTabType.D:
							return ParentControl.SlideContainer.EditedContent.VideoState.TabD.EnableOutput ?? TabInfo.EnableOutput;
						default:
							return TabInfo.EnableOutput;
					}
				case StarTopTabType.Audience:
					switch (TabInfo.TabType)
					{
						case StarChildTabType.A:
							return ParentControl.SlideContainer.EditedContent.AudienceState.TabA.EnableOutput ?? TabInfo.EnableOutput;
						case StarChildTabType.B:
							return ParentControl.SlideContainer.EditedContent.AudienceState.TabB.EnableOutput ?? TabInfo.EnableOutput;
						case StarChildTabType.C:
							return ParentControl.SlideContainer.EditedContent.AudienceState.TabC.EnableOutput ?? TabInfo.EnableOutput;
						default:
							return TabInfo.EnableOutput;
					}
				case StarTopTabType.Solution:
					switch (TabInfo.TabType)
					{
						case StarChildTabType.A:
							return ParentControl.SlideContainer.EditedContent.SolutionState.TabA.EnableOutput ?? TabInfo.EnableOutput;
						case StarChildTabType.B:
							return ParentControl.SlideContainer.EditedContent.SolutionState.TabB.EnableOutput ?? TabInfo.EnableOutput;
						case StarChildTabType.C:
							return ParentControl.SlideContainer.EditedContent.SolutionState.TabC.EnableOutput ?? TabInfo.EnableOutput;
						case StarChildTabType.D:
							return ParentControl.SlideContainer.EditedContent.SolutionState.TabD.EnableOutput ?? TabInfo.EnableOutput;
						default:
							return TabInfo.EnableOutput;
					}
				case StarTopTabType.Closers:
					switch (TabInfo.TabType)
					{
						case StarChildTabType.A:
							return ParentControl.SlideContainer.EditedContent.ClosersState.TabA.EnableOutput ?? TabInfo.EnableOutput;
						case StarChildTabType.B:
							return ParentControl.SlideContainer.EditedContent.ClosersState.TabB.EnableOutput ?? TabInfo.EnableOutput;
						case StarChildTabType.C:
							return ParentControl.SlideContainer.EditedContent.ClosersState.TabC.EnableOutput ?? TabInfo.EnableOutput;
						default:
							return TabInfo.EnableOutput;
					}
				default:
					return TabInfo.EnableOutput;
			}
		}
	}
}
