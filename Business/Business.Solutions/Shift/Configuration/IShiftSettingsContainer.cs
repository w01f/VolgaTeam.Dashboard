using Asa.Business.Common.Interfaces;

namespace Asa.Business.Solutions.Shift.Configuration
{
	public interface IShiftSettingsContainer : IBaseSettingsContainer
	{
		string SelectedShiftOutputItemsEncoded { get; set; }
	}
}
