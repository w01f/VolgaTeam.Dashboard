using Asa.Business.Media.Entities.NonPersistent.Snapshot;

namespace Asa.Media.Controls.PresentationClasses.SnapshotControls.Settings
{
	public interface IContentSettingsControl : ISettingsDataControl
	{
		void LoadContentData(SnapshotContent content);
	}
}
