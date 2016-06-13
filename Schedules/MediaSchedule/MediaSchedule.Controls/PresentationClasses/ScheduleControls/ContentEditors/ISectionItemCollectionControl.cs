namespace Asa.Media.Controls.PresentationClasses.ScheduleControls.ContentEditors
{
	public interface ISectionItemCollectionControl
	{
		string CollectionTitle { get; }
		string CollectionItemTitle { get; }
		bool AllowToAddItem { get; }
		bool AllowToDeleteItem { get; }
		void AddItem();
		void DeleteItem();
	}
}
