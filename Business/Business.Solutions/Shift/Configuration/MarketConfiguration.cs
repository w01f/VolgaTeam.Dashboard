using Asa.Common.Core.Helpers;

namespace Asa.Business.Solutions.Shift.Configuration
{
	public class MarketConfiguration
	{
		public SlideManager PartUSlides { get; }
		public SlideManager PartVSlides { get; }
		public SlideManager PartWSlides { get; }

		public MarketConfiguration()
		{
			PartUSlides = new SlideManager();
			PartVSlides = new SlideManager();
			PartWSlides = new SlideManager();
		}

		public void Load(ResourceManager resourceManager)
		{
			if (resourceManager.Tab3PartUSlidesFolder.ExistsLocal())
			{
				PartUSlides.LoadSlides(resourceManager.Tab3PartUSlidesFolder);
			}

			if (resourceManager.Tab3PartVSlidesFolder.ExistsLocal())
			{
				PartVSlides.LoadSlides(resourceManager.Tab3PartVSlidesFolder);
			}

			if (resourceManager.Tab3PartWSlidesFolder.ExistsLocal())
			{
				PartWSlides.LoadSlides(resourceManager.Tab3PartWSlidesFolder);
			}
		}
	}
}
