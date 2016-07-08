using Asa.Common.Core.Enums;

namespace Asa.Media.Controls.PresentationClasses.SnapshotControls.ContentEditors
{
	public interface ISnapshotEditorControl
	{
		SnapshotEditorType EditorType { get; }
		SlideType SlideType { get; }
		void InitControls();
		void Release();
		void SaveData();
	}
}
