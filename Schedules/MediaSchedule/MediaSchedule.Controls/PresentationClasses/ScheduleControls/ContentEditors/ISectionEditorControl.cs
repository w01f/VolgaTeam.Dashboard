using Asa.Common.Core.Enums;

namespace Asa.Media.Controls.PresentationClasses.ScheduleControls.ContentEditors
{
	public interface ISectionEditorControl
	{
		SectionEditorType EditorType { get; }
		SlideType SlideType { get; }
		void InitControls();
		void Release();
		void SaveData();
	}
}
