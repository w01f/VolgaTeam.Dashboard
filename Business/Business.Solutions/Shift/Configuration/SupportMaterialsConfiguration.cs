using Asa.Common.Core.Helpers;

namespace Asa.Business.Solutions.Shift.Configuration
{
	public class SupportMaterialsConfiguration
	{
		public SlideManager PartUSlides { get; }
		public SlideManager PartVSlides { get; }
		public SlideManager PartWSlides { get; }

		public SupportMaterialsConfiguration()
		{
			PartUSlides = new SlideManager();
			PartVSlides = new SlideManager();
			PartWSlides = new SlideManager();
		}

		public void Load(ResourceManager resourceManager)
		{
			if (resourceManager.Tab11PartUSlidesFolder.ExistsLocal())
			{
				PartUSlides.LoadSlides(resourceManager.Tab11PartUSlidesFolder);
			}

			if (resourceManager.Tab11PartVSlidesFolder.ExistsLocal())
			{
				PartVSlides.LoadSlides(resourceManager.Tab11PartVSlidesFolder);
			}

			if (resourceManager.Tab11PartWSlidesFolder.ExistsLocal())
			{
				PartWSlides.LoadSlides(resourceManager.Tab11PartWSlidesFolder);
			}
		}
	}
}
