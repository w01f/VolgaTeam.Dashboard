using Asa.Business.Common.Interfaces;

namespace Asa.Business.Solutions.Dashboard.Configuration
{
	public interface IDashboardSettingsContainer : IBaseSettingsContainer
	{
		string SalesRep { get; set; }
	}
}
