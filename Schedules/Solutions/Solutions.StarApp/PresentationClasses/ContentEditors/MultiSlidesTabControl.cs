using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Business.Solutions.StarApp.Configuration;
using Asa.Business.Solutions.StarApp.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Preview;
using DevExpress.XtraTab;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors
{
	public partial class MultiSlidesTabControl : ChildTabBaseControl
	{
		private SlideObject _sourceSlideObject;
		private SlidesChildTabInfo CustomTabInfo => (SlidesChildTabInfo)TabInfo;

		public MultiSlidesTabControl(IChildTabPageContainer slideContainer, StarChildTabInfo tabInfo) : base(slideContainer, tabInfo)
		{
			InitializeComponent();

			CustomTabInfo.LoadSlides();
			slidesEditContainer.SlideOutput += SlideContainer.OnCustomSlideOutput;
			slidesEditContainer.SlidePreview += SlideContainer.OnCustomSlidePreview;
			slidesEditContainer.Init(CustomTabInfo.Slides);
			slidesEditContainer.SelectionChanged += OnEditValueChanged;
		}

		public override void ApplyBackground()
		{
			if (TabInfo.BackgroundLogo != null)
				slidesEditContainer.SetBackground(TabInfo.BackgroundLogo);
		}

		public override void LoadData()
		{
			_allowToSave = false;

			switch (CustomTabInfo.TopTabType)
			{
				case StarTopTabType.Cover:
					switch (CustomTabInfo.TabType)
					{
						case StarChildTabType.U:
							_sourceSlideObject = SlideContainer.EditedContent.CoverState.TabU.Slide;
							break;
						case StarChildTabType.V:
							_sourceSlideObject = SlideContainer.EditedContent.CoverState.TabV.Slide;
							break;
						case StarChildTabType.W:
							_sourceSlideObject = SlideContainer.EditedContent.CoverState.TabW.Slide;
							break;
						default:
							throw new ArgumentOutOfRangeException("Star tab type is not defined");
					}
					break;
				case StarTopTabType.CNA:
					switch (CustomTabInfo.TabType)
					{
						case StarChildTabType.U:
							_sourceSlideObject = SlideContainer.EditedContent.CNAState.TabU.Slide;
							break;
						case StarChildTabType.V:
							_sourceSlideObject = SlideContainer.EditedContent.CNAState.TabV.Slide;
							break;
						case StarChildTabType.W:
							_sourceSlideObject = SlideContainer.EditedContent.CNAState.TabW.Slide;
							break;
						default:
							throw new ArgumentOutOfRangeException("Star tab type is not defined");
					}
					break;
				case StarTopTabType.Fishing:
					switch (CustomTabInfo.TabType)
					{
						case StarChildTabType.U:
							_sourceSlideObject = SlideContainer.EditedContent.FishingState.TabU.Slide;
							break;
						case StarChildTabType.V:
							_sourceSlideObject = SlideContainer.EditedContent.FishingState.TabV.Slide;
							break;
						case StarChildTabType.W:
							_sourceSlideObject = SlideContainer.EditedContent.FishingState.TabW.Slide;
							break;
						default:
							throw new ArgumentOutOfRangeException("Star tab type is not defined");
					}
					break;
				case StarTopTabType.Customer:
					switch (CustomTabInfo.TabType)
					{
						case StarChildTabType.U:
							_sourceSlideObject = SlideContainer.EditedContent.CustomerState.TabU.Slide;
							break;
						case StarChildTabType.V:
							_sourceSlideObject = SlideContainer.EditedContent.CustomerState.TabV.Slide;
							break;
						case StarChildTabType.W:
							_sourceSlideObject = SlideContainer.EditedContent.CustomerState.TabW.Slide;
							break;
						default:
							throw new ArgumentOutOfRangeException("Star tab type is not defined");
					}
					break;
				case StarTopTabType.Share:
					switch (CustomTabInfo.TabType)
					{
						case StarChildTabType.U:
							_sourceSlideObject = SlideContainer.EditedContent.ShareState.TabU.Slide;
							break;
						case StarChildTabType.V:
							_sourceSlideObject = SlideContainer.EditedContent.ShareState.TabV.Slide;
							break;
						case StarChildTabType.W:
							_sourceSlideObject = SlideContainer.EditedContent.ShareState.TabW.Slide;
							break;
						default:
							throw new ArgumentOutOfRangeException("Star tab type is not defined");
					}
					break;
				case StarTopTabType.ROI:
					switch (CustomTabInfo.TabType)
					{
						case StarChildTabType.U:
							_sourceSlideObject = SlideContainer.EditedContent.ROIState.TabU.Slide;
							break;
						case StarChildTabType.V:
							_sourceSlideObject = SlideContainer.EditedContent.ROIState.TabV.Slide;
							break;
						case StarChildTabType.W:
							_sourceSlideObject = SlideContainer.EditedContent.ROIState.TabW.Slide;
							break;
						default:
							throw new ArgumentOutOfRangeException("Star tab type is not defined");
					}
					break;
				case StarTopTabType.Market:
					switch (CustomTabInfo.TabType)
					{
						case StarChildTabType.U:
							_sourceSlideObject = SlideContainer.EditedContent.MarketState.TabU.Slide;
							break;
						case StarChildTabType.V:
							_sourceSlideObject = SlideContainer.EditedContent.MarketState.TabV.Slide;
							break;
						case StarChildTabType.W:
							_sourceSlideObject = SlideContainer.EditedContent.MarketState.TabW.Slide;
							break;
						default:
							throw new ArgumentOutOfRangeException("Star tab type is not defined");
					}
					break;
				case StarTopTabType.Video:
					switch (CustomTabInfo.TabType)
					{
						case StarChildTabType.U:
							_sourceSlideObject = SlideContainer.EditedContent.VideoState.TabU.Slide;
							break;
						case StarChildTabType.V:
							_sourceSlideObject = SlideContainer.EditedContent.VideoState.TabV.Slide;
							break;
						case StarChildTabType.W:
							_sourceSlideObject = SlideContainer.EditedContent.VideoState.TabW.Slide;
							break;
						default:
							throw new ArgumentOutOfRangeException("Star tab type is not defined");
					}
					break;
				case StarTopTabType.Audience:
					switch (CustomTabInfo.TabType)
					{
						case StarChildTabType.U:
							_sourceSlideObject = SlideContainer.EditedContent.AudienceState.TabU.Slide;
							break;
						case StarChildTabType.V:
							_sourceSlideObject = SlideContainer.EditedContent.AudienceState.TabV.Slide;
							break;
						case StarChildTabType.W:
							_sourceSlideObject = SlideContainer.EditedContent.AudienceState.TabW.Slide;
							break;
						default:
							throw new ArgumentOutOfRangeException("Star tab type is not defined");
					}
					break;
				case StarTopTabType.Solution:
					switch (CustomTabInfo.TabType)
					{
						case StarChildTabType.U:
							_sourceSlideObject = SlideContainer.EditedContent.SolutionState.TabU.Slide;
							break;
						case StarChildTabType.V:
							_sourceSlideObject = SlideContainer.EditedContent.SolutionState.TabV.Slide;
							break;
						case StarChildTabType.W:
							_sourceSlideObject = SlideContainer.EditedContent.SolutionState.TabW.Slide;
							break;
						default:
							throw new ArgumentOutOfRangeException("Star tab type is not defined");
					}
					break;
				case StarTopTabType.Closers:
					switch (CustomTabInfo.TabType)
					{
						case StarChildTabType.U:
							_sourceSlideObject = SlideContainer.EditedContent.ClosersState.TabU.Slide;
							break;
						case StarChildTabType.V:
							_sourceSlideObject = SlideContainer.EditedContent.ClosersState.TabV.Slide;
							break;
						case StarChildTabType.W:
							_sourceSlideObject = SlideContainer.EditedContent.ClosersState.TabW.Slide;
							break;
						default:
							throw new ArgumentOutOfRangeException("Star tab type is not defined");
					}
					break;
				default:
					throw new ArgumentOutOfRangeException("Star tab type is not defined");
			}

			slidesEditContainer.LoadData(_sourceSlideObject);
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

		public override Boolean GetOutputEnableState()
		{
			return true;
		}

		public override void ApplySlideHeaderValue(ListDataItem slideHeaderValue)
		{
		}

		public override void ApplyOutputEnableState(Boolean outputEnabled)
		{
		}

		private void OnEditValueChanged(object sender, EventArgs e)
		{
			RaiseEditValueChanged();
		}

		#region Output
		public override bool MultipleSlidesAllowed => false;
		public override bool ReadyForOutput => GetOutputItem() != null;

		public override OutputItem GetOutputItem()
		{
			if (_sourceSlideObject == null)
				return null;
			if (_sourceSlideObject.SourceSlideMasters.ContainsKey(SlideSettingsManager.Instance.SlideSettings.Format))
			{
				var slideMasterName = _sourceSlideObject.SourceSlideMasters[SlideSettingsManager.Instance.SlideSettings.Format];
				var targetSlideMaster = CustomTabInfo.Slides.Slides.FirstOrDefault(slideMaster =>
					String.Equals(slideMaster.Name, slideMasterName, StringComparison.OrdinalIgnoreCase));

				if (targetSlideMaster != null)
				{
					return new OutputItem
					{
						Name = slideMasterName,
						PresentationSourcePath = Path.Combine(Asa.Common.Core.Configuration.ResourceManager.Instance.TempFolder.LocalPath, Path.GetFileName(Path.GetTempFileName())),
						SlidesCount = 1,
						IsCurrent = ((XtraTabPage)TabPageContainer).TabControl?.SelectedTabPage == TabPageContainer,
						SlideGeneratingAction = (processor, destinationPresentation) =>
						{
							var templatePath = targetSlideMaster.GetMasterPath();
							processor.AppendSlideMaster(templatePath, destinationPresentation);
						},
						PreviewGeneratingAction = (processor, presentationSourcePath) =>
						{
							var templatePath = targetSlideMaster.GetMasterPath();
							processor.PreparePresentation(presentationSourcePath,
								presentation => processor.AppendSlideMaster(templatePath, presentation));
						}
					};
				}
			}

			return null;
		}
		#endregion
	}
}
