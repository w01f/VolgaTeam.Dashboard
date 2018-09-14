using System.Drawing;
using Asa.Common.Resources.Solutions.Dashboard;

namespace Asa.Solutions.Dashboard.Resources
{
	public class ResourceContainer : IDashboardGraphicResources
	{
		public Image CleanslateHeaderLogo => Resources.Main.Resource.Header;
		public Image CleanslateSplashLogo => Resources.Main.Resource.Logo;
		public Image CoverSplashLogo => Resources.Cover.Resource.Logo;
		public Image LeadoffStatementSplashLogo => Resources.Intro.Resource.Logo;
		public Image ClientGoalsSplashLogo => Resources.CNA.Resource.Logo;
		public Image TargeCustomersSplashLogo => Resources.TargetCustomers.Resource.Logo;
		public Image SimpleSummarySplashLogo => Resources.ClosingSummary.Resource.Logo;
	}
}
