using System.Drawing;
using System.Xml;
using Asa.Business.Solutions.Shift.Enums;

namespace Asa.Business.Solutions.Shift.Configuration.IntegratedSolution
{
	public class IntegratedSolutionTabBInfo : IntegratedSolutionSubTabInfo
	{
		public IntegratedSolutionTabBInfo() : base(ShiftChildTabType.B) { }

		public override void LoadData(XmlNode configNode, ResourceManager resourceManager)
		{
			base.LoadData(configNode, resourceManager);

			RightLogo = resourceManager.LogoTab9SubBRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab9SubBRightFile.LocalPath)
				: null;
			FooterLogo = resourceManager.LogoTab9SubBFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab9SubBFooterFile.LocalPath)
				: null;
			BackgroundLogo = resourceManager.LogoTab9SubBBackgroundFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab9SubBBackgroundFile.LocalPath)
				: null;
		}
	}
}
