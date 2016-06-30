using Asa.Media.Controls.PresentationClasses.OptionsControls.Settings;

namespace Asa.Media.Controls.PresentationClasses.OptionsControls.ContentEditors
{
	public interface IOptionContentEditorControl
	{
		IOptionSetEditorControl ActiveEditor { get; }
		IOptionSetCollectionEditorControl ActiveItemCollection { get; }
		void Release();
		void UpdateAccordingSettings(SettingsChangedEventArgs eventArgs);
	}
}
