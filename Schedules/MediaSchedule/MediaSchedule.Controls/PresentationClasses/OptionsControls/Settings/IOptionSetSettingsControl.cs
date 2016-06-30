using Asa.Business.Media.Entities.NonPersistent.Option;

namespace Asa.Media.Controls.PresentationClasses.OptionsControls.Settings
{
	public interface IOptionSetSettingsControl : ISettingsDataControl
	{
		void LoadOptionsSetData(OptionSet optionSetData);
	}
}
