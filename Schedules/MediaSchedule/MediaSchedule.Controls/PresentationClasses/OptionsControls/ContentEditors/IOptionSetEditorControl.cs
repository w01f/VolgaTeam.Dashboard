namespace Asa.Media.Controls.PresentationClasses.OptionsControls.ContentEditors
{
	public interface IOptionSetEditorControl
	{
		OptionEditorType EditorType { get; }
		void InitControls();
		void Release();
		void SaveData();
	}
}
