using Asa.Common.Core.Helpers;

namespace Asa.Business.Solutions.Shift.Configuration
{
	public class ClosersConfiguration
	{
		public SlideManager PartUSlides { get; }
		public SlideManager PartVSlides { get; }
		public SlideManager PartWSlides { get; }

		public ClosersConfiguration()
		{
			PartUSlides = new SlideManager();
			PartVSlides = new SlideManager();
			PartWSlides = new SlideManager();
		}

		public void Load(ResourceManager resourceManager)
		{
			if (resourceManager.Tab8PartUSlidesFolder.ExistsLocal())
			{
				PartUSlides.LoadSlides(resourceManager.Tab8PartUSlidesFolder);
			}

			if (resourceManager.Tab8PartVSlidesFolder.ExistsLocal())
			{
				PartVSlides.LoadSlides(resourceManager.Tab8PartVSlidesFolder);
			}

			if (resourceManager.Tab8PartWSlidesFolder.ExistsLocal())
			{
				PartWSlides.LoadSlides(resourceManager.Tab8PartWSlidesFolder);
			}
		}
	}
}
