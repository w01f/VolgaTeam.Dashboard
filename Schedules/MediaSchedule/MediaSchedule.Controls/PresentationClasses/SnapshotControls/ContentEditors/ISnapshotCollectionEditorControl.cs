namespace Asa.Media.Controls.PresentationClasses.SnapshotControls.ContentEditors
{
	public interface ISnapshotCollectionEditorControl
	{
		string CollectionTitle { get; }
		string CollectionItemTitle { get; }
		bool AllowToAddItem { get; }
		bool AllowToDeleteItem { get; }
		void AddItem();
		void DeleteItem();
	}
}
