using Asa.Common.Core.Helpers;

namespace Asa.Business.Solutions.Shift.Configuration
{
	public class InvestmentConfiguration
	{
		public SlideManager PartUSlides { get; }
		public SlideManager PartVSlides { get; }
		public SlideManager PartWSlides { get; }

		public InvestmentConfiguration()
		{
			PartUSlides = new SlideManager();
			PartVSlides = new SlideManager();
			PartWSlides = new SlideManager();
		}

		public void Load(ResourceManager resourceManager)
		{
			if (resourceManager.Tab7PartUSlidesFolder.ExistsLocal())
			{
				PartUSlides.LoadSlides(resourceManager.Tab7PartUSlidesFolder);
			}

			if (resourceManager.Tab7PartVSlidesFolder.ExistsLocal())
			{
				PartVSlides.LoadSlides(resourceManager.Tab7PartVSlidesFolder);
			}

			if (resourceManager.Tab7PartWSlidesFolder.ExistsLocal())
			{
				PartWSlides.LoadSlides(resourceManager.Tab7PartWSlidesFolder);
			}
		}
	}
}
