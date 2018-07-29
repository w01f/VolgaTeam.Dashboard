using System;
using System.Windows.Forms;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Business.Solutions.StarApp.Configuration;
using Asa.Business.Solutions.StarApp.Enums;
using Asa.Common.GUI.Preview;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors
{
	public partial class SlidesTabControl : ChildTabBaseControl
	{
		private SlidesChildTabInfo CustomTabInfo => (SlidesChildTabInfo)TabInfo;

		public SlidesTabControl(IChildTabPageContainer slideContainer, StarChildTabInfo tabInfo) : base(slideContainer, tabInfo)
		{
			InitializeComponent();

			slidesEditContainer.Init(CustomTabInfo.Slides);
			slidesEditContainer.SelectionChanged += OnEditValueChanged;
			slidesEditContainer.SlideOutput += SlideContainer.OnCustomSlideOutput;

			if (TabInfo.BackgroundLogo != null)
				slidesEditContainer.SetBackground(TabInfo.BackgroundLogo);
		}

		public override void LoadData()
		{
			_allowToSave = false;

			SlideObject sourceSlideObject;
			switch (CustomTabInfo.TopTabType)
			{
				case StarTopTabType.Cover:
					switch (CustomTabInfo.TabType)
					{
						case StarChildTabType.U:
							sourceSlideObject = SlideContainer.EditedContent.CoverState.TabU.Slide;
							break;
						case StarChildTabType.V:
							sourceSlideObject = SlideContainer.EditedContent.CoverState.TabV.Slide;
							break;
						case StarChildTabType.W:
							sourceSlideObject = SlideContainer.EditedContent.CoverState.TabW.Slide;
							break;
						default:
							throw new ArgumentOutOfRangeException("Star tab type is not defined");
					}
					break;
				case StarTopTabType.CNA:
					switch (CustomTabInfo.TabType)
					{
						case StarChildTabType.U:
							sourceSlideObject = SlideContainer.EditedContent.CNAState.TabU.Slide;
							break;
						case StarChildTabType.V:
							sourceSlideObject = SlideContainer.EditedContent.CNAState.TabV.Slide;
							break;
						case StarChildTabType.W:
							sourceSlideObject = SlideContainer.EditedContent.CNAState.TabW.Slide;
							break;
						default:
							throw new ArgumentOutOfRangeException("Star tab type is not defined");
					}
					break;
				case StarTopTabType.Fishing:
					switch (CustomTabInfo.TabType)
					{
						case StarChildTabType.U:
							sourceSlideObject = SlideContainer.EditedContent.FishingState.TabU.Slide;
							break;
						case StarChildTabType.V:
							sourceSlideObject = SlideContainer.EditedContent.FishingState.TabV.Slide;
							break;
						case StarChildTabType.W:
							sourceSlideObject = SlideContainer.EditedContent.FishingState.TabW.Slide;
							break;
						default:
							throw new ArgumentOutOfRangeException("Star tab type is not defined");
					}
					break;
				case StarTopTabType.Customer:
					switch (CustomTabInfo.TabType)
					{
						case StarChildTabType.U:
							sourceSlideObject = SlideContainer.EditedContent.CustomerState.TabU.Slide;
							break;
						case StarChildTabType.V:
							sourceSlideObject = SlideContainer.EditedContent.CustomerState.TabV.Slide;
							break;
						case StarChildTabType.W:
							sourceSlideObject = SlideContainer.EditedContent.CustomerState.TabW.Slide;
							break;
						default:
							throw new ArgumentOutOfRangeException("Star tab type is not defined");
					}
					break;
				case StarTopTabType.Share:
					switch (CustomTabInfo.TabType)
					{
						case StarChildTabType.U:
							sourceSlideObject = SlideContainer.EditedContent.ShareState.TabU.Slide;
							break;
						case StarChildTabType.V:
							sourceSlideObject = SlideContainer.EditedContent.ShareState.TabV.Slide;
							break;
						case StarChildTabType.W:
							sourceSlideObject = SlideContainer.EditedContent.ShareState.TabW.Slide;
							break;
						default:
							throw new ArgumentOutOfRangeException("Star tab type is not defined");
					}
					break;
				case StarTopTabType.ROI:
					switch (CustomTabInfo.TabType)
					{
						case StarChildTabType.U:
							sourceSlideObject = SlideContainer.EditedContent.ROIState.TabU.Slide;
							break;
						case StarChildTabType.V:
							sourceSlideObject = SlideContainer.EditedContent.ROIState.TabV.Slide;
							break;
						case StarChildTabType.W:
							sourceSlideObject = SlideContainer.EditedContent.ROIState.TabW.Slide;
							break;
						default:
							throw new ArgumentOutOfRangeException("Star tab type is not defined");
					}
					break;
				case StarTopTabType.Market:
					switch (CustomTabInfo.TabType)
					{
						case StarChildTabType.U:
							sourceSlideObject = SlideContainer.EditedContent.MarketState.TabU.Slide;
							break;
						case StarChildTabType.V:
							sourceSlideObject = SlideContainer.EditedContent.MarketState.TabV.Slide;
							break;
						case StarChildTabType.W:
							sourceSlideObject = SlideContainer.EditedContent.MarketState.TabW.Slide;
							break;
						default:
							throw new ArgumentOutOfRangeException("Star tab type is not defined");
					}
					break;
				case StarTopTabType.Video:
					switch (CustomTabInfo.TabType)
					{
						case StarChildTabType.U:
							sourceSlideObject = SlideContainer.EditedContent.VideoState.TabU.Slide;
							break;
						case StarChildTabType.V:
							sourceSlideObject = SlideContainer.EditedContent.VideoState.TabV.Slide;
							break;
						case StarChildTabType.W:
							sourceSlideObject = SlideContainer.EditedContent.VideoState.TabW.Slide;
							break;
						default:
							throw new ArgumentOutOfRangeException("Star tab type is not defined");
					}
					break;
				case StarTopTabType.Audience:
					switch (CustomTabInfo.TabType)
					{
						case StarChildTabType.U:
							sourceSlideObject = SlideContainer.EditedContent.AudienceState.TabU.Slide;
							break;
						case StarChildTabType.V:
							sourceSlideObject = SlideContainer.EditedContent.AudienceState.TabV.Slide;
							break;
						case StarChildTabType.W:
							sourceSlideObject = SlideContainer.EditedContent.AudienceState.TabW.Slide;
							break;
						default:
							throw new ArgumentOutOfRangeException("Star tab type is not defined");
					}
					break;
				case StarTopTabType.Solution:
					switch (CustomTabInfo.TabType)
					{
						case StarChildTabType.U:
							sourceSlideObject = SlideContainer.EditedContent.SolutionState.TabU.Slide;
							break;
						case StarChildTabType.V:
							sourceSlideObject = SlideContainer.EditedContent.SolutionState.TabV.Slide;
							break;
						case StarChildTabType.W:
							sourceSlideObject = SlideContainer.EditedContent.SolutionState.TabW.Slide;
							break;
						default:
							throw new ArgumentOutOfRangeException("Star tab type is not defined");
					}
					break;
				case StarTopTabType.Closers:
					switch (CustomTabInfo.TabType)
					{
						case StarChildTabType.U:
							sourceSlideObject = SlideContainer.EditedContent.ClosersState.TabU.Slide;
							break;
						case StarChildTabType.V:
							sourceSlideObject = SlideContainer.EditedContent.ClosersState.TabV.Slide;
							break;
						case StarChildTabType.W:
							sourceSlideObject = SlideContainer.EditedContent.ClosersState.TabW.Slide;
							break;
						default:
							throw new ArgumentOutOfRangeException("Star tab type is not defined");
					}
					break;
				default:
					throw new ArgumentOutOfRangeException("Star tab type is not defined");
			}

			slidesEditContainer.LoadData(sourceSlideObject);
			Application.DoEvents();

			_allowToSave = true;
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;

			slidesEditContainer.SaveData();

			_dataChanged = false;
		}

		public override ListDataItem GetSlideHeaderValue()
		{
			return null;
		}

		public override void ApplySlideHeaderValue(ListDataItem slideHeaderValue)
		{
		}

		private void OnEditValueChanged(object sender, EventArgs e)
		{
			RaiseEditValueChanged();
		}

		#region Output
		public override bool ReadyForOutput => false;

		public override OutputItem GetOutputItem()
		{
			return null;
		}
		#endregion
	}
}
