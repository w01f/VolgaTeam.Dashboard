using Asa.Common.Core.Helpers;

namespace Asa.Business.Solutions.Shift.Configuration
{
	public class ContractConfiguration
	{
		public SlideManager PartUSlides { get; }
		public SlideManager PartVSlides { get; }
		public SlideManager PartWSlides { get; }

		public ContractConfiguration()
		{
			PartUSlides = new SlideManager();
			PartVSlides = new SlideManager();
			PartWSlides = new SlideManager();
		}

		public void Load(ResourceManager resourceManager)
		{
			if (resourceManager.Tab10PartUSlidesFolder.ExistsLocal())
			{
				PartUSlides.LoadSlides(resourceManager.Tab10PartUSlidesFolder);
			}

			if (resourceManager.Tab10PartVSlidesFolder.ExistsLocal())
			{
				PartVSlides.LoadSlides(resourceManager.Tab10PartVSlidesFolder);
			}

			if (resourceManager.Tab10PartWSlidesFolder.ExistsLocal())
			{
				PartWSlides.LoadSlides(resourceManager.Tab10PartWSlidesFolder);
			}
		}
	}
}
