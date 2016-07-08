using Asa.Business.Media.Entities.NonPersistent.Snapshot;

namespace Asa.Media.Controls.PresentationClasses.SnapshotControls.Settings
{
	public interface ISnapshotSettingsControl : ISettingsDataControl
	{
		void LoadSnapshotData(Snapshot snapshotData);
	}
}
