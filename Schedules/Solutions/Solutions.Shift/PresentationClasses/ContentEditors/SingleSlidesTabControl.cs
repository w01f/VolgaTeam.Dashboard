using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Business.Solutions.Shift.Configuration;
using Asa.Business.Solutions.Shift.Enums;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.Output;
using Asa.Common.Core.Objects.Slides;
using Asa.Common.GUI.Preview;
using Asa.Solutions.Common.PresentationClasses.SlidesEdit;
using DevExpress.Skins;
using DevExpress.XtraEditors;
using DevExpress.XtraTab;
using SlideMasterEventArgs = Asa.Common.GUI.Slides.SlideMasterEventArgs;

namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors
{
	//public partial class SingleSlidesTabControl : UserControl
	public partial class SingleSlidesTabControl : ChildTabBaseControl
	{
		private SlideObject _savedSlideObject;
		private SlidesChildTabInfo CustomTabInfo => (SlidesChildTabInfo)TabInfo;
		private SlideMaster _currentSlide;

		public SingleSlidesTabControl(IChildTabPageContainer slideContainer, ShiftChildTabInfo tabInfo) : base(slideContainer, tabInfo)
		{
			InitializeComponent();

			CustomTabInfo.LoadSlides();

			SlideSettingsManager.Instance.SettingsChanging += OnSlideFormatSettingsChanging;
			SlideSettingsManager.Instance.SettingsChanged += OnSlideFormatSettingsChanged;

			pictureEditClipart.DoubleClick += OnOutputClick;
			pictureEditClipart.MouseWheel += OnClipartMouseWheel;

			pictureEditUp.Image = CustomTabInfo.ListUpImage;
			pictureEditDown.Image = CustomTabInfo.ListDownImage;
			pictureEditList.Image = CustomTabInfo.ListPopupImage;

			var scaleFactor = Utilities.GetScaleFactor(CreateGraphics().DpiX);
			layoutControlItemUp.MaxSize = RectangleHelper.ScaleSize(layoutControlItemUp.MaxSize, scaleFactor);
			layoutControlItemUp.MinSize = RectangleHelper.ScaleSize(layoutControlItemUp.MinSize, scaleFactor);
			layoutControlItemList.MaxSize = RectangleHelper.ScaleSize(layoutControlItemList.MaxSize, scaleFactor);
			layoutControlItemList.MinSize = RectangleHelper.ScaleSize(layoutControlItemList.MinSize, scaleFactor);
			layoutControlItemDown.MaxSize = RectangleHelper.ScaleSize(layoutControlItemDown.MaxSize, scaleFactor);
			layoutControlItemDown.MinSize = RectangleHelper.ScaleSize(layoutControlItemDown.MinSize, scaleFactor);
		}

		public override void LoadData()
		{
			_allowToSave = false;

			_savedSlideObject = null;

			switch (CustomTabInfo.TopTabType)
			{
				case ShiftTopTabType.Cover:
					switch (CustomTabInfo.TabType)
					{
						case ShiftChildTabType.K:
							_savedSlideObject = SlideContainer.EditedContent.CoverState.TabK.Slide;
							break;
						case ShiftChildTabType.L:
							_savedSlideObject = SlideContainer.EditedContent.CoverState.TabL.Slide;
							break;
						case ShiftChildTabType.M:
							_savedSlideObject = SlideContainer.EditedContent.CoverState.TabM.Slide;
							break;
						case ShiftChildTabType.N:
							_savedSlideObject = SlideContainer.EditedContent.CoverState.TabN.Slide;
							break;
						case ShiftChildTabType.O:
							_savedSlideObject = SlideContainer.EditedContent.CoverState.TabO.Slide;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				case ShiftTopTabType.Intro:
					switch (CustomTabInfo.TabType)
					{
						case ShiftChildTabType.K:
							_savedSlideObject = SlideContainer.EditedContent.IntroState.TabK.Slide;
							break;
						case ShiftChildTabType.L:
							_savedSlideObject = SlideContainer.EditedContent.IntroState.TabL.Slide;
							break;
						case ShiftChildTabType.M:
							_savedSlideObject = SlideContainer.EditedContent.IntroState.TabM.Slide;
							break;
						case ShiftChildTabType.N:
							_savedSlideObject = SlideContainer.EditedContent.IntroState.TabN.Slide;
							break;
						case ShiftChildTabType.O:
							_savedSlideObject = SlideContainer.EditedContent.IntroState.TabO.Slide;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				case ShiftTopTabType.Agenda:
					switch (CustomTabInfo.TabType)
					{
						case ShiftChildTabType.K:
							_savedSlideObject = SlideContainer.EditedContent.AgendaState.TabK.Slide;
							break;
						case ShiftChildTabType.L:
							_savedSlideObject = SlideContainer.EditedContent.AgendaState.TabL.Slide;
							break;
						case ShiftChildTabType.M:
							_savedSlideObject = SlideContainer.EditedContent.AgendaState.TabM.Slide;
							break;
						case ShiftChildTabType.N:
							_savedSlideObject = SlideContainer.EditedContent.AgendaState.TabN.Slide;
							break;
						case ShiftChildTabType.O:
							_savedSlideObject = SlideContainer.EditedContent.AgendaState.TabO.Slide;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				case ShiftTopTabType.Goals:
					switch (CustomTabInfo.TabType)
					{
						case ShiftChildTabType.K:
							_savedSlideObject = SlideContainer.EditedContent.GoalsState.TabK.Slide;
							break;
						case ShiftChildTabType.L:
							_savedSlideObject = SlideContainer.EditedContent.GoalsState.TabL.Slide;
							break;
						case ShiftChildTabType.M:
							_savedSlideObject = SlideContainer.EditedContent.GoalsState.TabM.Slide;
							break;
						case ShiftChildTabType.N:
							_savedSlideObject = SlideContainer.EditedContent.GoalsState.TabN.Slide;
							break;
						case ShiftChildTabType.O:
							_savedSlideObject = SlideContainer.EditedContent.GoalsState.TabO.Slide;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				case ShiftTopTabType.Market:
					switch (CustomTabInfo.TabType)
					{
						case ShiftChildTabType.K:
							_savedSlideObject = SlideContainer.EditedContent.MarketState.TabK.Slide;
							break;
						case ShiftChildTabType.L:
							_savedSlideObject = SlideContainer.EditedContent.MarketState.TabL.Slide;
							break;
						case ShiftChildTabType.M:
							_savedSlideObject = SlideContainer.EditedContent.MarketState.TabM.Slide;
							break;
						case ShiftChildTabType.N:
							_savedSlideObject = SlideContainer.EditedContent.MarketState.TabN.Slide;
							break;
						case ShiftChildTabType.O:
							_savedSlideObject = SlideContainer.EditedContent.MarketState.TabO.Slide;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				case ShiftTopTabType.Partnership:
					switch (CustomTabInfo.TabType)
					{
						case ShiftChildTabType.K:
							_savedSlideObject = SlideContainer.EditedContent.PartnershipState.TabK.Slide;
							break;
						case ShiftChildTabType.L:
							_savedSlideObject = SlideContainer.EditedContent.PartnershipState.TabL.Slide;
							break;
						case ShiftChildTabType.M:
							_savedSlideObject = SlideContainer.EditedContent.PartnershipState.TabM.Slide;
							break;
						case ShiftChildTabType.N:
							_savedSlideObject = SlideContainer.EditedContent.PartnershipState.TabN.Slide;
							break;
						case ShiftChildTabType.O:
							_savedSlideObject = SlideContainer.EditedContent.PartnershipState.TabO.Slide;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				case ShiftTopTabType.NeedsSolutions:
					switch (CustomTabInfo.TabType)
					{
						case ShiftChildTabType.K:
							_savedSlideObject = SlideContainer.EditedContent.NeedsSolutionsState.TabK.Slide;
							break;
						case ShiftChildTabType.L:
							_savedSlideObject = SlideContainer.EditedContent.NeedsSolutionsState.TabL.Slide;
							break;
						case ShiftChildTabType.M:
							_savedSlideObject = SlideContainer.EditedContent.NeedsSolutionsState.TabM.Slide;
							break;
						case ShiftChildTabType.N:
							_savedSlideObject = SlideContainer.EditedContent.NeedsSolutionsState.TabN.Slide;
							break;
						case ShiftChildTabType.O:
							_savedSlideObject = SlideContainer.EditedContent.NeedsSolutionsState.TabO.Slide;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				case ShiftTopTabType.CBC:
					switch (CustomTabInfo.TabType)
					{
						case ShiftChildTabType.K:
							_savedSlideObject = SlideContainer.EditedContent.CBCState.TabK.Slide;
							break;
						case ShiftChildTabType.L:
							_savedSlideObject = SlideContainer.EditedContent.CBCState.TabL.Slide;
							break;
						case ShiftChildTabType.M:
							_savedSlideObject = SlideContainer.EditedContent.CBCState.TabM.Slide;
							break;
						case ShiftChildTabType.N:
							_savedSlideObject = SlideContainer.EditedContent.CBCState.TabN.Slide;
							break;
						case ShiftChildTabType.O:
							_savedSlideObject = SlideContainer.EditedContent.CBCState.TabO.Slide;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				case ShiftTopTabType.IntegratedSolution:
					switch (CustomTabInfo.TabType)
					{
						case ShiftChildTabType.K:
							_savedSlideObject = SlideContainer.EditedContent.IntegratedSolutionState.TabK.Slide;
							break;
						case ShiftChildTabType.L:
							_savedSlideObject = SlideContainer.EditedContent.IntegratedSolutionState.TabL.Slide;
							break;
						case ShiftChildTabType.M:
							_savedSlideObject = SlideContainer.EditedContent.IntegratedSolutionState.TabM.Slide;
							break;
						case ShiftChildTabType.N:
							_savedSlideObject = SlideContainer.EditedContent.IntegratedSolutionState.TabN.Slide;
							break;
						case ShiftChildTabType.O:
							_savedSlideObject = SlideContainer.EditedContent.IntegratedSolutionState.TabO.Slide;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				case ShiftTopTabType.Approach:
					switch (CustomTabInfo.TabType)
					{
						case ShiftChildTabType.K:
							_savedSlideObject = SlideContainer.EditedContent.ApproachState.TabK.Slide;
							break;
						case ShiftChildTabType.L:
							_savedSlideObject = SlideContainer.EditedContent.ApproachState.TabL.Slide;
							break;
						case ShiftChildTabType.M:
							_savedSlideObject = SlideContainer.EditedContent.ApproachState.TabM.Slide;
							break;
						case ShiftChildTabType.N:
							_savedSlideObject = SlideContainer.EditedContent.ApproachState.TabN.Slide;
							break;
						case ShiftChildTabType.O:
							_savedSlideObject = SlideContainer.EditedContent.ApproachState.TabO.Slide;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				case ShiftTopTabType.ROI:
					switch (CustomTabInfo.TabType)
					{
						case ShiftChildTabType.K:
							_savedSlideObject = SlideContainer.EditedContent.ROIState.TabK.Slide;
							break;
						case ShiftChildTabType.L:
							_savedSlideObject = SlideContainer.EditedContent.ROIState.TabL.Slide;
							break;
						case ShiftChildTabType.M:
							_savedSlideObject = SlideContainer.EditedContent.ROIState.TabM.Slide;
							break;
						case ShiftChildTabType.N:
							_savedSlideObject = SlideContainer.EditedContent.ROIState.TabN.Slide;
							break;
						case ShiftChildTabType.O:
							_savedSlideObject = SlideContainer.EditedContent.ROIState.TabO.Slide;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
			}

			if (_savedSlideObject != null && _savedSlideObject.SourceSlideMasters.ContainsKey(SlideSettingsManager.Instance.SlideSettings.Format))
			{
				var slideMasterName = _savedSlideObject.SourceSlideMasters[SlideSettingsManager.Instance.SlideSettings.Format];
				_currentSlide = CustomTabInfo.Slides.Slides.FirstOrDefault(slideMaster =>
						 String.Equals(slideMaster.Name, slideMasterName, StringComparison.OrdinalIgnoreCase));
			}
			else
				_currentSlide = CustomTabInfo.Slides.Slides.FirstOrDefault(slideMaster =>
					slideMaster.Format == SlideSettingsManager.Instance.SlideSettings.Format);

			SaveSlideObject();

			UpdateDataControl();

			_allowToSave = true;
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;

			SaveSlideObject();

			_dataChanged = false;
		}

		public override bool GetOutputEnableState()
		{
			return true;
		}

		public override void ApplyOutputEnableState(bool outputEnabled)
		{
		}

		protected override SlideDescription GetSlideDescription()
		{
			if (_currentSlide == null)
				return base.GetSlideDescription();
			return new SlideDescription
			{
				LeftText = _currentSlide.GetMasterName(),
				RightText = String.Format("{0} of {1}", CustomTabInfo.Slides.Slides.IndexOf(_currentSlide) + 1, CustomTabInfo.Slides.Slides.Count)
			};
		}

		private void SaveSlideObject()
		{
			if (_savedSlideObject == null)
				_savedSlideObject = new SlideObject();
			if (_currentSlide == null) return;
			if (_savedSlideObject.SourceSlideMasters.ContainsKey(SlideSettingsManager.Instance.SlideSettings.Format))
				_savedSlideObject.SourceSlideMasters[SlideSettingsManager.Instance.SlideSettings.Format] = _currentSlide.Name;
			else
				_savedSlideObject.SourceSlideMasters.Add(SlideSettingsManager.Instance.SlideSettings.Format, _currentSlide.Name);
		}

		private void UpdateDataControl()
		{
			pictureEditClipart.Image = _currentSlide != null ?
				Image.FromFile(_currentSlide.LogoFile.LocalPath) :
				null;
			RaiseSlideDescriptionChanged();
		}

		private void OnSlideFormatSettingsChanging(object sender, SlideSettingsChangingEventArgs e)
		{
			SaveSlideObject();
		}

		private void OnSlideFormatSettingsChanged(object sender, EventArgs e)
		{
			LoadData();
		}

		private void OnUpButtonClick(object sender, EventArgs e)
		{
			if (!CustomTabInfo.Slides.Slides.Any()) return;
			if (_currentSlide == null) return;

			var currentItemIndex = CustomTabInfo.Slides.Slides.IndexOf(_currentSlide);
			var nextIndex = currentItemIndex > 0 ? currentItemIndex - 1 : CustomTabInfo.Slides.Slides.Count - 1;
			_currentSlide = CustomTabInfo.Slides.Slides[nextIndex];

			UpdateDataControl();
			RaiseEditValueChanged();
		}

		private void OnDownButtonClick(object sender, EventArgs e)
		{
			if (!CustomTabInfo.Slides.Slides.Any()) return;
			if (_currentSlide == null) return;

			var currentItemIndex = CustomTabInfo.Slides.Slides.IndexOf(_currentSlide);
			var nextIndex = currentItemIndex < CustomTabInfo.Slides.Slides.Count - 1 ? currentItemIndex + 1 : 0;
			_currentSlide = CustomTabInfo.Slides.Slides[nextIndex];

			UpdateDataControl();
			RaiseEditValueChanged();
		}

		private void OnClipartMouseWheel(object sender, MouseEventArgs e)
		{
			if (e.Delta < 0)
				OnUpButtonClick(pictureEditUp, EventArgs.Empty);
			else
				OnDownButtonClick(pictureEditDown, EventArgs.Empty);
		}

		private void OnListButtonClick(object sender, EventArgs e)
		{
			if (!CustomTabInfo.Slides.Slides.Any()) return;
			if (_currentSlide == null) return;

			using (var form = new FormSlides(CustomTabInfo.Slides, _currentSlide))
			{
				if (form.ShowDialog() == DialogResult.OK)
				{
					_currentSlide = form.SelectedSlide;
					UpdateDataControl();
					RaiseEditValueChanged();
				}
			}
		}

		private void OnPreviewClick(object sender, EventArgs e)
		{
			if (_currentSlide == null) return;
			SaveSlideObject();
			SlideContainer.OnCustomSlidePreview(sender, new SlideMasterEventArgs
			{
				SlideMaster = _currentSlide
			});
		}

		private void OnOutputClick(object sender, EventArgs e)
		{
			if (_currentSlide == null) return;
			SaveSlideObject();
			SlideContainer.OnCustomSlideOutput(sender, new SlideMasterEventArgs
			{
				SlideMaster = _currentSlide
			});
		}

		private void OnPictureEditMouseHover(object sender, EventArgs e)
		{
			var pictureEdit = (PictureEdit)sender;
			pictureEdit.Properties.Appearance.BackColor =
				pictureEdit.Properties.AppearanceFocused.BackColor =
					SlideContainer.StyleConfiguration.ToggleHoverColor ?? pictureEdit.BackColor;
		}

		private void OnPictureEditMouseMove(object sender, MouseEventArgs e)
		{
			OnPictureEditMouseHover(sender, e);
		}

		private void OnPictureEditMouseLeave(object sender, EventArgs e)
		{
			var pictureEdit = (PictureEdit)sender;
			pictureEdit.Properties.Appearance.BackColor = Color.Transparent;
			pictureEdit.Properties.AppearanceFocused.BackColor = Color.Transparent;
		}

		#region Output
		public override bool MultipleSlidesAllowed => false;
		public override bool ReadyForOutput => GetOutputItems() != null;
		public override SlideType SlideType => SlideType.CustomSlide;

		public override IList<OutputItem> GetOutputItems()
		{
			if (_savedSlideObject == null)
				return null;
			if (_savedSlideObject.SourceSlideMasters.ContainsKey(SlideSettingsManager.Instance.SlideSettings.Format))
			{
				var slideMasterName = _savedSlideObject.SourceSlideMasters[SlideSettingsManager.Instance.SlideSettings.Format];
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
							processor.AppendSlideMaster(targetSlideMaster.GetMasterPath(), destinationPresentation);
						},
						PreviewGeneratingAction = (processor, presentationSourcePath) =>
						{
							processor.PreparePresentation(presentationSourcePath,
								presentation => processor.AppendSlideMaster(targetSlideMaster.GetMasterPath(), presentation));
						}
					}};
				}
			}

			return new List<OutputItem>();
		}
		#endregion
	}
}
