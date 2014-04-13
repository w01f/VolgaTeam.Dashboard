using DevComponents.DotNetBar;
using DevExpress.XtraEditors;
using NewBizWiz.AdSchedule.Controls.BusinessClasses;
using NewBizWiz.CommonGUI.Gallery;
using NewBizWiz.Core.Common;

namespace NewBizWiz.AdSchedule.Controls.PresentationClasses
{
	public class PrintGallery2Control : GalleryControl
	{
		public override GalleryManager Manager
		{
			get { return BusinessWrapper.Instance.Gallery2Manager; }
		}

		public override RibbonPanel Panel
		{
			get { return Controller.Instance.Gallery2Panel; }
		}

		public override RibbonBar BrowseBar
		{
			get { return Controller.Instance.Gallery2BrowseBar; }
		}

		public override RibbonBar ImageBar
		{
			get { return Controller.Instance.Gallery2ImageBar; }
		}

		public override RibbonBar ZoomBar
		{
			get { return Controller.Instance.Gallery2ZoomBar; }
		}

		public override RibbonBar CopyBar
		{
			get { return Controller.Instance.Gallery2CopyBar; }
		}

		public override ItemContainer BrowseModeContainer
		{
			get { return Controller.Instance.Gallery2BrowseModeContainer; }
		}

		public override ButtonItem ViewMode
		{
			get { return Controller.Instance.Gallery2View; }
		}

		public override ButtonItem EditMode
		{
			get { return Controller.Instance.Gallery2Edit; }
		}

		public override ButtonItem ImageSelect
		{
			get { return Controller.Instance.Gallery2ImageSelect; }
		}

		public override ButtonItem ImageCrop
		{
			get { return Controller.Instance.Gallery2ImageCrop; }
		}

		public override ButtonItem ZoomIn
		{
			get { return Controller.Instance.Gallery2ZoomIn; }
		}

		public override ButtonItem ZoomOut
		{
			get { return Controller.Instance.Gallery2ZoomOut; }
		}

		public override ButtonItem Copy
		{
			get { return Controller.Instance.Gallery2Copy; }
		}

		public override ComboBoxEdit SectionsList
		{
			get { return Controller.Instance.Gallery2Sections; }
		}

		public override ComboBoxEdit GroupsList
		{
			get { return Controller.Instance.Gallery2Groups; }
		}
	}
}
