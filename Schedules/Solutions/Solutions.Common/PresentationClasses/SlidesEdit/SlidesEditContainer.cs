using System;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Slides;

namespace Asa.Solutions.Common.PresentationClasses.SlidesEdit
{
	public class SlidesEditContainer : SlidesContainerControl
	{
		private SlideManager _slideManager;

		public void Init(SlideManager slideManager)
		{
			_slideManager = slideManager;

			InitSlides(_slideManager);
			SlideSettingsManager.Instance.SettingsChanged += OnSlideFormatSettingsChanged;
		}

		private void OnSlideFormatSettingsChanged(object sender, EventArgs e)
		{
			InitSlides(_slideManager);
		}
	}
}
