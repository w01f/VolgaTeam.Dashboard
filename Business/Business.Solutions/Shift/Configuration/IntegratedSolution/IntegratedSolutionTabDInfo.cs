using System.Drawing;
using System.Xml;
using Asa.Business.Solutions.Shift.Enums;

namespace Asa.Business.Solutions.Shift.Configuration.IntegratedSolution
{
	public class IntegratedSolutionTabDInfo : IntegratedSolutionSubTabInfo
	{
		public IntegratedSolutionTabDInfo() : base(ShiftChildTabType.D) { }

		public override void LoadData(XmlNode configNode, ResourceManager resourceManager)
		{
			base.LoadData(configNode, resourceManager);

			RightLogo = resourceManager.LogoTab9SubDRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab9SubDRightFile.LocalPath)
				: null;
			FooterLogo = resourceManager.LogoTab9SubDFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab9SubDFooterFile.LocalPath)
				: null;
			BackgroundLogo = resourceManager.LogoTab9SubDBackgroundFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab9SubDBackgroundFile.LocalPath)
				: null;
		}
	}
}
