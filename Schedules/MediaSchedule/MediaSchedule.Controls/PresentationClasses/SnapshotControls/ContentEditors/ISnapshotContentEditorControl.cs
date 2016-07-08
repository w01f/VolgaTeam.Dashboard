using Asa.Media.Controls.PresentationClasses.SnapshotControls.Settings;

namespace Asa.Media.Controls.PresentationClasses.SnapshotControls.ContentEditors
{
	public interface ISnapshotContentEditorControl
	{
		ISnapshotEditorControl ActiveEditor { get; }
		ISnapshotCollectionEditorControl ActiveItemCollection { get; }
		void Release();
		void UpdateAccordingSettings(SettingsChangedEventArgs eventArgs);
	}
}
