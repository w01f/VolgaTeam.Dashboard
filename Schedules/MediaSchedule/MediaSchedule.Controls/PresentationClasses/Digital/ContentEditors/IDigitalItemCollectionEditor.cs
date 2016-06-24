namespace Asa.Media.Controls.PresentationClasses.Digital.ContentEditors
{
	public interface IDigitalItemCollectionEditor
	{
		bool HasItems { get; }
		void AddItem(object sender);
		void CloneItem();
		void DeleteItem();
	}
}
