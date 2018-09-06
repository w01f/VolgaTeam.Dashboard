using System.Drawing;
using System.Xml;
using Asa.Business.Solutions.Shift.Enums;

namespace Asa.Business.Solutions.Shift.Configuration.IntegratedSolution
{
	public class IntegratedSolutionTabAInfo : IntegratedSolutionSubTabInfo
	{
		public IntegratedSolutionTabAInfo() : base(ShiftChildTabType.A) { }

		public override void LoadData(XmlNode configNode, ResourceManager resourceManager)
		{
			base.LoadData(configNode, resourceManager);

			RightLogo = resourceManager.LogoTab9SubARightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab9SubARightFile.LocalPath)
				: null;
			FooterLogo = resourceManager.LogoTab9SubAFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab9SubAFooterFile.LocalPath)
				: null;
			BackgroundLogo = resourceManager.LogoTab9SubABackgroundFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab9SubABackgroundFile.LocalPath)
				: null;
		}
	}
}
