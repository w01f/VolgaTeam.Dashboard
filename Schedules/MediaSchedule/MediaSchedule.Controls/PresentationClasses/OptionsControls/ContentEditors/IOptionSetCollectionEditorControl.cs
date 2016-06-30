namespace Asa.Media.Controls.PresentationClasses.OptionsControls.ContentEditors
{
	public interface IOptionSetCollectionEditorControl
	{
		string CollectionTitle { get; }
		string CollectionItemTitle { get; }
		bool AllowToAddItem { get; }
		bool AllowToDeleteItem { get; }
		void AddItem();
		void DeleteItem();
	}
}
