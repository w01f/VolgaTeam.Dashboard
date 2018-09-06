using System.Drawing;
using System.Xml;
using Asa.Business.Solutions.Shift.Enums;

namespace Asa.Business.Solutions.Shift.Configuration.IntegratedSolution
{
	public class IntegratedSolutionTabEInfo : IntegratedSolutionSubTabInfo
	{
		public IntegratedSolutionTabEInfo() : base(ShiftChildTabType.E) { }

		public override void LoadData(XmlNode configNode, ResourceManager resourceManager)
		{
			base.LoadData(configNode, resourceManager);

			RightLogo = resourceManager.LogoTab9SubERightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab9SubERightFile.LocalPath)
				: null;
			FooterLogo = resourceManager.LogoTab9SubEFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab9SubEFooterFile.LocalPath)
				: null;
			BackgroundLogo = resourceManager.LogoTab9SubEBackgroundFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab9SubEBackgroundFile.LocalPath)
				: null;
		}
	}
}
