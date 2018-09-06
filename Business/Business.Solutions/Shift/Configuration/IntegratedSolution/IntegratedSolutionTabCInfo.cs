using System.Drawing;
using System.Xml;
using Asa.Business.Solutions.Shift.Enums;

namespace Asa.Business.Solutions.Shift.Configuration.IntegratedSolution
{
	public class IntegratedSolutionTabCInfo : IntegratedSolutionSubTabInfo
	{
		public IntegratedSolutionTabCInfo() : base(ShiftChildTabType.C) { }

		public override void LoadData(XmlNode configNode, ResourceManager resourceManager)
		{
			base.LoadData(configNode, resourceManager);

			RightLogo = resourceManager.LogoTab9SubCRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab9SubCRightFile.LocalPath)
				: null;
			FooterLogo = resourceManager.LogoTab9SubCFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab9SubCFooterFile.LocalPath)
				: null;
			BackgroundLogo = resourceManager.LogoTab9SubCBackgroundFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab9SubCBackgroundFile.LocalPath)
				: null;
		}
	}
}
