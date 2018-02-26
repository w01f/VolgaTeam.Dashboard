using Asa.Solutions.StarApp.PresentationClasses.ContentEditors;
using DevExpress.XtraBars;

namespace Asa.Solutions.StarApp.PresentationClasses.ImageEdit
{
	public class ROIControlImageEditorHelper : ImageEditorHelper
	{
		private readonly ROITabBaseControl _imageEditorContainer;

		protected override PopupMenu Menu => _imageEditorContainer.popupMenuImage;
		protected override BarButtonItem MenuItemPreview => _imageEditorContainer.barButtonItemImagePreview;
		protected override BarButtonItem MenuItemPaste => _imageEditorContainer.barButtonItemImagePaste;
		protected override BarButtonItem MenuItemFavoritesAdd => _imageEditorContainer.barButtonItemImageFavoritesAdd;
		protected override BarButtonItem MenuItemFavoritesOpen => _imageEditorContainer.barButtonItemImageFavoritesOpen;
		protected override BarButtonItem MenuItemBrowse => _imageEditorContainer.barButtonItemImageOpen;
		protected override BarButtonItem MenuItemReset => _imageEditorContainer.barButtonItemImageReset;

		public ROIControlImageEditorHelper(ROITabBaseControl imageEditorContainer)
		{
			_imageEditorContainer = imageEditorContainer;
			InitMenu();
		}
	}
}
