using System;

namespace AdSalesBrowser.SalesLibraryExtensions.LinkViewContent
{
	class PowerPointContent : PageContent
	{
		private double _slideWidth;
		private double _slideHeight;
		
		public override LinkContentType ContentType => LinkContentType.PowerPoint;
		
		public override void Load(object[] data)
		{
			base.Load(data);

			double temp;
			if (double.TryParse(data[4].ToString(), out temp))
				_slideWidth = temp;
			if (double.TryParse(data[5].ToString(), out temp))
				_slideHeight = temp;
		}

		
		public bool IsFitToInsert(SlideSettings currentSlideSettings)
		{
			var currentWidth = (Int32)Math.Round(currentSlideSettings.SizeWidth);
			var slideWidth = (Int32)Math.Round(_slideWidth);
			return slideWidth == 0 || currentWidth == slideWidth;
		}
	}
}
