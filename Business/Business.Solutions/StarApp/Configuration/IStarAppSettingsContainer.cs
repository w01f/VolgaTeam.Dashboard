using Asa.Business.Common.Interfaces;

namespace Asa.Business.Solutions.StarApp.Configuration
{
	public interface IStarAppSettingsContainer : IBaseSettingsContainer
	{
		string SelectedStarOutputItemsEncoded { get; set; }
	}
}
