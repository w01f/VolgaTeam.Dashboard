using Asa.Common.Core.Enums;

namespace AdSalesBrowser.SalesLibraryExtensions
{
	public class SlideSettings
	{
		public double SizeHeght { get; set; }
		public double SizeWidth { get; set; }
		public SlideOrientationEnum Orientation { get; set; }

		public SlideSettings()
		{
			Orientation = SlideOrientationEnum.Landscape;
			SizeWidth = 10;
			SizeHeght = 7.5;
		}
	}
}
