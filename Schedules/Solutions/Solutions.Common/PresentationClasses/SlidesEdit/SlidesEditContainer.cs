using System;
using System.Linq;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.Output;
using Asa.Common.Core.Objects.Slides;
using Asa.Common.Core.OfficeInterops;
using Asa.Common.GUI.Slides;
using SlideMasterEventArgs = Asa.Common.GUI.Slides.SlideMasterEventArgs;

namespace Asa.Solutions.Common.PresentationClasses.SlidesEdit
{
	public class SlidesEditContainer : SlidesContainerControl
	{
		private SlideManager _slideManager;
		private SlideObject _slideObject;
		private PowerPointProcessor _powerPointProcessor;

		public void Init(SlideManager slideManager, PowerPointProcessor powerPointProcessor)
		{
			_slideManager = slideManager;
			_powerPointProcessor = powerPointProcessor;
			InitSlides(_slideManager);
			SlideSettingsManager.Instance.SettingsChanging += OnSlideFormatSettingsChanging;
			SlideSettingsManager.Instance.SettingsChanged += OnSlideFormatSettingsChanged;

			SlideOutput += OnSlideOutput;
		}

		public void LoadData(SlideObject slideObject)
		{
			_slideObject = slideObject;
			ApplySavedState();
		}

		public void SaveData()
		{
			if (_slideObject == null) return;
			var selectedSlideMaster = SelectedSlide;
			if (_slideObject.SourceSlideMasters.ContainsKey(SlideSettingsManager.Instance.SlideSettings.Format))
				_slideObject.SourceSlideMasters[SlideSettingsManager.Instance.SlideSettings.Format] = selectedSlideMaster.Name;
			else
				_slideObject.SourceSlideMasters.Add(SlideSettingsManager.Instance.SlideSettings.Format, selectedSlideMaster.Name);
		}

		private void ApplySavedState()
		{
			SlideMaster targetSlideMaster = null;
			if (_slideObject.SourceSlideMasters.ContainsKey(SlideSettingsManager.Instance.SlideSettings.Format))
			{
				var slideMasterName = _slideObject.SourceSlideMasters[SlideSettingsManager.Instance.SlideSettings.Format];
				targetSlideMaster =
					_slideManager.Slides.FirstOrDefault(slideMaster =>
						String.Equals(slideMaster.Name, slideMasterName, StringComparison.OrdinalIgnoreCase));
			}
			if (targetSlideMaster == null)
				targetSlideMaster = _slideManager.Slides.FirstOrDefault(slideMaster =>
					slideMaster.Format == SlideSettingsManager.Instance.SlideSettings.Format);

			SelectSlide(targetSlideMaster); ;
		}

		private void OnSlideFormatSettingsChanging(object sender, SlideSettingsChangingEventArgs e)
		{
			SaveData();
		}

		private void OnSlideFormatSettingsChanged(object sender, EventArgs e)
		{
			InitSlides(_slideManager);
			ApplySavedState();
		}

		private void OnSlideOutput(Object sender, SlideMasterEventArgs e)
		{
			//var previewGroup = 
		}
	}
}
