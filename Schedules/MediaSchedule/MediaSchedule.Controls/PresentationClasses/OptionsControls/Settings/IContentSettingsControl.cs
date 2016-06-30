using Asa.Business.Media.Entities.NonPersistent.Option;

namespace Asa.Media.Controls.PresentationClasses.OptionsControls.Settings
{
	public interface IContentSettingsControl : ISettingsDataControl
	{
		void LoadContentData(OptionsContent content);
	}
}
