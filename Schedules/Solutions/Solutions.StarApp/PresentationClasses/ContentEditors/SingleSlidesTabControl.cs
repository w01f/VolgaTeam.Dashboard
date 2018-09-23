using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Business.Solutions.StarApp.Configuration;
using Asa.Business.Solutions.StarApp.Enums;
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

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors
{
	//public partial class SingleSlidesTabControl : UserControl
	public partial class SingleSlidesTabControl : ChildTabBaseControl
	{
		private SlideObject _savedSlideObject;
		private SlidesChildTabInfo CustomTabInfo => (SlidesChildTabInfo)TabInfo;
		private SlideMaster _currentSlide;

		public SingleSlidesTabControl(IChildTabPageContainer slideContainer, StarChildTabInfo tabInfo) : base(slideContainer, tabInfo)
		{
			InitializeComponent();

			CustomTabInfo.LoadSlides();

			SlideSettingsManager.Instance.SettingsChanging += OnSlideFormatSettingsChanging;
			SlideSettingsManager.Instance.SettingsChanged += OnSlideFormatSettingsChanged;

			pictureEditClipart.DoubleClick += OnClipartDoubleClick;
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
				case StarTopTabType.Cover:
					switch (CustomTabInfo.TabType)
					{
						case StarChildTabType.K:
							_savedSlideObject = SlideContainer.EditedContent.CoverState.TabK.Slide;
							break;
						case StarChildTabType.L:
							_savedSlideObject = SlideContainer.EditedContent.CoverState.TabL.Slide;
							break;
						case StarChildTabType.M:
							_savedSlideObject = SlideContainer.EditedContent.CoverState.TabM.Slide;
							break;
						case StarChildTabType.N:
							_savedSlideObject = SlideContainer.EditedContent.CoverState.TabN.Slide;
							break;
						case StarChildTabType.O:
							_savedSlideObject = SlideContainer.EditedContent.CoverState.TabO.Slide;
							break;
						default:
							throw new ArgumentOutOfRangeException("StarApp tab type is not defined");
					}
					break;
				case StarTopTabType.CNA:
					switch (CustomTabInfo.TabType)
					{
						case StarChildTabType.K:
							_savedSlideObject = SlideContainer.EditedContent.CNAState.TabK.Slide;
							break;
						case StarChildTabType.L:
							_savedSlideObject = SlideContainer.EditedContent.CNAState.TabL.Slide;
							break;
						case StarChildTabType.M:
							_savedSlideObject = SlideContainer.EditedContent.CNAState.TabM.Slide;
							break;
						case StarChildTabType.N:
							_savedSlideObject = SlideContainer.EditedContent.CNAState.TabN.Slide;
							break;
						case StarChildTabType.O:
							_savedSlideObject = SlideContainer.EditedContent.CNAState.TabO.Slide;
							break;
						default:
							throw new ArgumentOutOfRangeException("StarApp tab type is not defined");
					}
					break;
				case StarTopTabType.Fishing:
					switch (CustomTabInfo.TabType)
					{
						case StarChildTabType.K:
							_savedSlideObject = SlideContainer.EditedContent.FishingState.TabK.Slide;
							break;
						case StarChildTabType.L:
							_savedSlideObject = SlideContainer.EditedContent.FishingState.TabL.Slide;
							break;
						case StarChildTabType.M:
							_savedSlideObject = SlideContainer.EditedContent.FishingState.TabM.Slide;
							break;
						case StarChildTabType.N:
							_savedSlideObject = SlideContainer.EditedContent.FishingState.TabN.Slide;
							break;
						case StarChildTabType.O:
							_savedSlideObject = SlideContainer.EditedContent.FishingState.TabO.Slide;
							break;
						default:
							throw new ArgumentOutOfRangeException("StarApp tab type is not defined");
					}
					break;
				case StarTopTabType.Customer:
					switch (CustomTabInfo.TabType)
					{
						case StarChildTabType.K:
							_savedSlideObject = SlideContainer.EditedContent.CustomerState.TabK.Slide;
							break;
						case StarChildTabType.L:
							_savedSlideObject = SlideContainer.EditedContent.CustomerState.TabL.Slide;
							break;
						case StarChildTabType.M:
							_savedSlideObject = SlideContainer.EditedContent.CustomerState.TabM.Slide;
							break;
						case StarChildTabType.N:
							_savedSlideObject = SlideContainer.EditedContent.CustomerState.TabN.Slide;
							break;
						case StarChildTabType.O:
							_savedSlideObject = SlideContainer.EditedContent.CustomerState.TabO.Slide;
							break;
						default:
							throw new ArgumentOutOfRangeException("StarApp tab type is not defined");
					}
					break;
				case StarTopTabType.Share:
					switch (CustomTabInfo.TabType)
					{
						case StarChildTabType.K:
							_savedSlideObject = SlideContainer.EditedContent.ShareState.TabK.Slide;
							break;
						case StarChildTabType.L:
							_savedSlideObject = SlideContainer.EditedContent.ShareState.TabL.Slide;
							break;
						case StarChildTabType.M:
							_savedSlideObject = SlideContainer.EditedContent.ShareState.TabM.Slide;
							break;
						case StarChildTabType.N:
							_savedSlideObject = SlideContainer.EditedContent.ShareState.TabN.Slide;
							break;
						case StarChildTabType.O:
							_savedSlideObject = SlideContainer.EditedContent.ShareState.TabO.Slide;
							break;
						default:
							throw new ArgumentOutOfRangeException("StarApp tab type is not defined");
					}
					break;
				case StarTopTabType.ROI:
					switch (CustomTabInfo.TabType)
					{
						case StarChildTabType.K:
							_savedSlideObject = SlideContainer.EditedContent.ROIState.TabK.Slide;
							break;
						case StarChildTabType.L:
							_savedSlideObject = SlideContainer.EditedContent.ROIState.TabL.Slide;
							break;
						case StarChildTabType.M:
							_savedSlideObject = SlideContainer.EditedContent.ROIState.TabM.Slide;
							break;
						case StarChildTabType.N:
							_savedSlideObject = SlideContainer.EditedContent.ROIState.TabN.Slide;
							break;
						case StarChildTabType.O:
							_savedSlideObject = SlideContainer.EditedContent.ROIState.TabO.Slide;
							break;
						default:
							throw new ArgumentOutOfRangeException("StarApp tab type is not defined");
					}
					break;
				case StarTopTabType.Market:
					switch (CustomTabInfo.TabType)
					{
						case StarChildTabType.K:
							_savedSlideObject = SlideContainer.EditedContent.MarketState.TabK.Slide;
							break;
						case StarChildTabType.L:
							_savedSlideObject = SlideContainer.EditedContent.MarketState.TabL.Slide;
							break;
						case StarChildTabType.M:
							_savedSlideObject = SlideContainer.EditedContent.MarketState.TabM.Slide;
							break;
						case StarChildTabType.N:
							_savedSlideObject = SlideContainer.EditedContent.MarketState.TabN.Slide;
							break;
						case StarChildTabType.O:
							_savedSlideObject = SlideContainer.EditedContent.MarketState.TabO.Slide;
							break;
						default:
							throw new ArgumentOutOfRangeException("StarApp tab type is not defined");
					}
					break;
				case StarTopTabType.Video:
					switch (CustomTabInfo.TabType)
					{
						case StarChildTabType.K:
							_savedSlideObject = SlideContainer.EditedContent.VideoState.TabK.Slide;
							break;
						case StarChildTabType.L:
							_savedSlideObject = SlideContainer.EditedContent.VideoState.TabL.Slide;
							break;
						case StarChildTabType.M:
							_savedSlideObject = SlideContainer.EditedContent.VideoState.TabM.Slide;
							break;
						case StarChildTabType.N:
							_savedSlideObject = SlideContainer.EditedContent.VideoState.TabN.Slide;
							break;
						case StarChildTabType.O:
							_savedSlideObject = SlideContainer.EditedContent.VideoState.TabO.Slide;
							break;
						default:
							throw new ArgumentOutOfRangeException("StarApp tab type is not defined");
					}
					break;
				case StarTopTabType.Audience:
					switch (CustomTabInfo.TabType)
					{
						case StarChildTabType.K:
							_savedSlideObject = SlideContainer.EditedContent.AudienceState.TabK.Slide;
							break;
						case StarChildTabType.L:
							_savedSlideObject = SlideContainer.EditedContent.AudienceState.TabL.Slide;
							break;
						case StarChildTabType.M:
							_savedSlideObject = SlideContainer.EditedContent.AudienceState.TabM.Slide;
							break;
						case StarChildTabType.N:
							_savedSlideObject = SlideContainer.EditedContent.AudienceState.TabN.Slide;
							break;
						case StarChildTabType.O:
							_savedSlideObject = SlideContainer.EditedContent.AudienceState.TabO.Slide;
							break;
						default:
							throw new ArgumentOutOfRangeException("StarApp tab type is not defined");
					}
					break;
				case StarTopTabType.Solution:
					switch (CustomTabInfo.TabType)
					{
						case StarChildTabType.K:
							_savedSlideObject = SlideContainer.EditedContent.SolutionState.TabK.Slide;
							break;
						case StarChildTabType.L:
							_savedSlideObject = SlideContainer.EditedContent.SolutionState.TabL.Slide;
							break;
						case StarChildTabType.M:
							_savedSlideObject = SlideContainer.EditedContent.SolutionState.TabM.Slide;
							break;
						case StarChildTabType.N:
							_savedSlideObject = SlideContainer.EditedContent.SolutionState.TabN.Slide;
							break;
						case StarChildTabType.O:
							_savedSlideObject = SlideContainer.EditedContent.SolutionState.TabO.Slide;
							break;
						default:
							throw new ArgumentOutOfRangeException("StarApp tab type is not defined");
					}
					break;
				case StarTopTabType.Closers:
					switch (CustomTabInfo.TabType)
					{
						case StarChildTabType.K:
							_savedSlideObject = SlideContainer.EditedContent.ClosersState.TabK.Slide;
							break;
						case StarChildTabType.L:
							_savedSlideObject = SlideContainer.EditedContent.ClosersState.TabL.Slide;
							break;
						case StarChildTabType.M:
							_savedSlideObject = SlideContainer.EditedContent.ClosersState.TabM.Slide;
							break;
						case StarChildTabType.N:
							_savedSlideObject = SlideContainer.EditedContent.ClosersState.TabN.Slide;
							break;
						case StarChildTabType.O:
							_savedSlideObject = SlideContainer.EditedContent.ClosersState.TabO.Slide;
							break;
						default:
							throw new ArgumentOutOfRangeException("StarApp tab type is not defined");
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

		private void OnClipartDoubleClick(object sender, EventArgs e)
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
		public override bool ReadyForOutput => GetOutputItem() != null;

		public override OutputItem GetOutputItem()
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
					return new OutputItem
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
					};
				}
			}

			return null;
		}
		#endregion
	}
}
