﻿using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.Output;
using Asa.Common.Core.Objects.Slides;
using Asa.Common.GUI.Slides;

namespace Asa.Solutions.Common.PresentationClasses.SlidesEdit
{
	public class SlidesEditContainer : SlidesContainerControl
	{
		private SlideObject _slideObject;
		private SolutionSlideManager _slideManager;

		public void Init(SolutionSlideManager slideManager)
		{
			_slideManager = slideManager;

			InitSlides(_slideManager, _slideManager.ThumbnailSize);
			SlideSettingsManager.Instance.SettingsChanging += OnSlideFormatSettingsChanging;
			SlideSettingsManager.Instance.SettingsChanged += OnSlideFormatSettingsChanged;
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
			if (selectedSlideMaster == null) return;
			if (_slideObject.SourceSlideMasters.ContainsKey(SlideSettingsManager.Instance.SlideSettings.Format))
				_slideObject.SourceSlideMasters[SlideSettingsManager.Instance.SlideSettings.Format] = selectedSlideMaster.Name;
			else
				_slideObject.SourceSlideMasters.Add(SlideSettingsManager.Instance.SlideSettings.Format, selectedSlideMaster.Name);
		}

		public void SetBackground(Image image)
		{
			if (image == null) return;
			foreach (var slideGroupPage in xtraTabControlSlides.TabPages.OfType<SlideGroupPage>().ToList())
			{
				slideGroupPage.slidesListView.Colors.BackColor = Color.Transparent;
				slideGroupPage.slidesListView.BackgroundImage = image;
				slideGroupPage.slidesListView.BackgroundImageLayout = ImageLayout.Stretch;
			}
		}

		private void ApplySavedState()
		{
			SlideMaster targetSlideMaster = null;
			if (_slideObject != null && _slideObject.SourceSlideMasters.ContainsKey(SlideSettingsManager.Instance.SlideSettings.Format))
			{
				var slideMasterName = _slideObject.SourceSlideMasters[SlideSettingsManager.Instance.SlideSettings.Format];
				targetSlideMaster =
					_slideManager.Slides.FirstOrDefault(slideMaster =>
						String.Equals(slideMaster.Name, slideMasterName, StringComparison.OrdinalIgnoreCase));
			}
			if (targetSlideMaster == null)
			{
				targetSlideMaster = _slideManager.Slides.FirstOrDefault(slideMaster =>
					slideMaster.Format == SlideSettingsManager.Instance.SlideSettings.Format);
				SelectSlide(targetSlideMaster);
				SaveData();
			}
			else
				SelectSlide(targetSlideMaster);
		}

		private void OnSlideFormatSettingsChanging(object sender, SlideSettingsChangingEventArgs e)
		{
			SaveData();
		}

		private void OnSlideFormatSettingsChanged(object sender, EventArgs e)
		{
			InitSlides(_slideManager, _slideManager.ThumbnailSize);
			ApplySavedState();
		}
	}
}
