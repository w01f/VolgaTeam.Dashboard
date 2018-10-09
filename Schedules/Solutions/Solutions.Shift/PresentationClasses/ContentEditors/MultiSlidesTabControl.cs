using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Business.Solutions.Shift.Configuration;
using Asa.Business.Solutions.Shift.Enums;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Preview;
using DevExpress.XtraTab;

namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors
{
	public partial class MultiSlidesTabControl : ChildTabBaseControl
	{
		private SlideObject _sourceSlideObject;
		private SlidesChildTabInfo CustomTabInfo => (SlidesChildTabInfo)TabInfo;

		public MultiSlidesTabControl(IChildTabPageContainer slideContainer, ShiftChildTabInfo tabInfo) : base(slideContainer, tabInfo)
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
				case ShiftTopTabType.Cover:
					switch (CustomTabInfo.TabType)
					{
						case ShiftChildTabType.U:
							_sourceSlideObject = SlideContainer.EditedContent.CoverState.TabU.Slide;
							break;
						case ShiftChildTabType.V:
							_sourceSlideObject = SlideContainer.EditedContent.CoverState.TabV.Slide;
							break;
						case ShiftChildTabType.W:
							_sourceSlideObject = SlideContainer.EditedContent.CoverState.TabW.Slide;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					slidesEditContainer.LoadData(_sourceSlideObject);
					break;
				case ShiftTopTabType.Intro:
					switch (CustomTabInfo.TabType)
					{
						case ShiftChildTabType.U:
							_sourceSlideObject = SlideContainer.EditedContent.IntroState.TabU.Slide;
							break;
						case ShiftChildTabType.V:
							_sourceSlideObject = SlideContainer.EditedContent.IntroState.TabV.Slide;
							break;
						case ShiftChildTabType.W:
							_sourceSlideObject = SlideContainer.EditedContent.IntroState.TabW.Slide;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					slidesEditContainer.LoadData(_sourceSlideObject);
					break;
				case ShiftTopTabType.Agenda:
					switch (CustomTabInfo.TabType)
					{
						case ShiftChildTabType.U:
							_sourceSlideObject = SlideContainer.EditedContent.AgendaState.TabU.Slide;
							break;
						case ShiftChildTabType.V:
							_sourceSlideObject = SlideContainer.EditedContent.AgendaState.TabV.Slide;
							break;
						case ShiftChildTabType.W:
							_sourceSlideObject = SlideContainer.EditedContent.AgendaState.TabW.Slide;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					slidesEditContainer.LoadData(_sourceSlideObject);
					break;
				case ShiftTopTabType.Goals:
					switch (CustomTabInfo.TabType)
					{
						case ShiftChildTabType.U:
							_sourceSlideObject = SlideContainer.EditedContent.GoalsState.TabU.Slide;
							break;
						case ShiftChildTabType.V:
							_sourceSlideObject = SlideContainer.EditedContent.GoalsState.TabV.Slide;
							break;
						case ShiftChildTabType.W:
							_sourceSlideObject = SlideContainer.EditedContent.GoalsState.TabW.Slide;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					slidesEditContainer.LoadData(_sourceSlideObject);
					break;
				case ShiftTopTabType.Market:
					switch (CustomTabInfo.TabType)
					{
						case ShiftChildTabType.U:
							_sourceSlideObject = SlideContainer.EditedContent.MarketState.TabU.Slide;
							break;
						case ShiftChildTabType.V:
							_sourceSlideObject = SlideContainer.EditedContent.MarketState.TabV.Slide;
							break;
						case ShiftChildTabType.W:
							_sourceSlideObject = SlideContainer.EditedContent.MarketState.TabW.Slide;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					slidesEditContainer.LoadData(_sourceSlideObject);
					break;
				case ShiftTopTabType.Partnership:
					switch (CustomTabInfo.TabType)
					{
						case ShiftChildTabType.U:
							_sourceSlideObject = SlideContainer.EditedContent.PartnershipState.TabU.Slide;
							break;
						case ShiftChildTabType.V:
							_sourceSlideObject = SlideContainer.EditedContent.PartnershipState.TabV.Slide;
							break;
						case ShiftChildTabType.W:
							_sourceSlideObject = SlideContainer.EditedContent.PartnershipState.TabW.Slide;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					slidesEditContainer.LoadData(_sourceSlideObject);
					break;
				case ShiftTopTabType.NeedsSolutions:
					switch (CustomTabInfo.TabType)
					{
						case ShiftChildTabType.U:
							_sourceSlideObject = SlideContainer.EditedContent.NeedsSolutionsState.TabU.Slide;
							break;
						case ShiftChildTabType.V:
							_sourceSlideObject = SlideContainer.EditedContent.NeedsSolutionsState.TabV.Slide;
							break;
						case ShiftChildTabType.W:
							_sourceSlideObject = SlideContainer.EditedContent.NeedsSolutionsState.TabW.Slide;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					slidesEditContainer.LoadData(_sourceSlideObject);
					break;
				case ShiftTopTabType.CBC:
					switch (CustomTabInfo.TabType)
					{
						case ShiftChildTabType.U:
							_sourceSlideObject = SlideContainer.EditedContent.CBCState.TabU.Slide;
							break;
						case ShiftChildTabType.V:
							_sourceSlideObject = SlideContainer.EditedContent.CBCState.TabV.Slide;
							break;
						case ShiftChildTabType.W:
							_sourceSlideObject = SlideContainer.EditedContent.CBCState.TabW.Slide;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					slidesEditContainer.LoadData(_sourceSlideObject);
					break;
				case ShiftTopTabType.IntegratedSolution:
					switch (CustomTabInfo.TabType)
					{
						case ShiftChildTabType.U:
							_sourceSlideObject = SlideContainer.EditedContent.IntegratedSolutionState.TabU.Slide;
							break;
						case ShiftChildTabType.V:
							_sourceSlideObject = SlideContainer.EditedContent.IntegratedSolutionState.TabV.Slide;
							break;
						case ShiftChildTabType.W:
							_sourceSlideObject = SlideContainer.EditedContent.IntegratedSolutionState.TabW.Slide;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					slidesEditContainer.LoadData(_sourceSlideObject);
					break;
				case ShiftTopTabType.Approach:
					switch (CustomTabInfo.TabType)
					{
						case ShiftChildTabType.U:
							_sourceSlideObject = SlideContainer.EditedContent.ApproachState.TabU.Slide;
							break;
						case ShiftChildTabType.V:
							_sourceSlideObject = SlideContainer.EditedContent.ApproachState.TabV.Slide;
							break;
						case ShiftChildTabType.W:
							_sourceSlideObject = SlideContainer.EditedContent.ApproachState.TabW.Slide;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					slidesEditContainer.LoadData(_sourceSlideObject);
					break;
				default:
					_sourceSlideObject = new SlideObject();
					slidesEditContainer.LoadData(_sourceSlideObject);
					slidesEditContainer.SaveData();
					break;
			}

			_allowToSave = true;
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;

			slidesEditContainer.SaveData();

			_dataChanged = false;
		}

		public override Boolean GetOutputEnableState()
		{
			return true;
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
		public override bool ReadyForOutput => GetOutputItems() != null;
		public override SlideType SlideType => SlideType.CustomSlide;

		public override IList<OutputItem> GetOutputItems()
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
					return new[]{ new OutputItem
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
					}};
				}
			}

			return new List<OutputItem>();
		}
		#endregion
	}
}
