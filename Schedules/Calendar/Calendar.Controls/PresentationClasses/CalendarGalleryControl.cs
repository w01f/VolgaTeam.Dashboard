using DevComponents.DotNetBar;
using DevExpress.XtraEditors;
using NewBizWiz.Calendar.Controls.BusinessClasses;
using NewBizWiz.CommonGUI.Gallery;
using NewBizWiz.Core.Common;

namespace NewBizWiz.Calendar.Controls.PresentationClasses
{
	public class CalendarGalleryControl : GalleryControl
	{
		public override GalleryManager Manager
		{
			get { return BusinessWrapper.Instance.GalleryManager; }
		}

		public override RibbonBar BrowseBar
		{
			get { return Controller.Instance.GalleryBrowseBar; }
		}

		public override RibbonBar ImageBar
		{
			get { return Controller.Instance.GalleryImageBar; }
		}

		public override RibbonBar ZoomBar
		{
			get { return Controller.Instance.GalleryZoomBar; }
		}

		public override RibbonBar CopyBar
		{
			get { return Controller.Instance.GalleryCopyBar; }
		}

		public override ButtonItem ScreenshotsMode
		{
			get { return Controller.Instance.GalleryScreenshots; }
		}

		public override ButtonItem AdSpecsMode
		{
			get { return Controller.Instance.GalleryAdSpecs; }
		}

		public override ButtonItem ViewMode
		{
			get { return Controller.Instance.GalleryView; }
		}

		public override ButtonItem EditMode
		{
			get { return Controller.Instance.GalleryEdit; }
		}

		public override ButtonItem ImageSelect
		{
			get { return Controller.Instance.GalleryImageSelect; }
		}

		public override ButtonItem ImageCrop
		{
			get { return Controller.Instance.GalleryImageCrop; }
		}

		public override ButtonItem ZoomIn
		{
			get { return Controller.Instance.GalleryZoomIn; }
		}

		public override ButtonItem ZoomOut
		{
			get { return Controller.Instance.GalleryZoomOut; }
		}

		public override ButtonItem Copy
		{
			get { return Controller.Instance.GalleryCopy; }
		}

		public override ComboBoxEdit SectionsList
		{
			get { return Controller.Instance.GallerySections; }
		}

		public override ComboBoxEdit GroupsList
		{
			get { return Controller.Instance.GalleryGroups; }
		}
	}
}
