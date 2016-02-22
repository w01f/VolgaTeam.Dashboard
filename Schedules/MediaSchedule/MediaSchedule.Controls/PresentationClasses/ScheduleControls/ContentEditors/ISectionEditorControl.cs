namespace Asa.Media.Controls.PresentationClasses.ScheduleControls.ContentEditors
{
	public interface ISectionEditorControl
	{
		SectionEditorType EditorType { get; }
		void InitControls();
		void Release();
		void SaveData();
	}
}
