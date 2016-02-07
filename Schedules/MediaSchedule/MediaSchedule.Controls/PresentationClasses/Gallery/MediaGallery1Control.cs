using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Gallery;
using DevComponents.DotNetBar;
using DevExpress.XtraEditors;
using Asa.Media.Controls.BusinessClasses;

namespace Asa.Media.Controls.PresentationClasses.Gallery
{
	public class MediaGallery1Control : GalleryControl
	{
		public override GalleryManager Manager
		{
			get { return BusinessObjects.Instance.Gallery1Manager; }
		}

		public override RibbonPanel Panel
		{
			get { return Controller.Instance.Gallery1Panel; }
		}

		public override RibbonBar BrowseBar
		{
			get { return Controller.Instance.Gallery1BrowseBar; }
		}

		public override RibbonBar ImageBar
		{
			get { return Controller.Instance.Gallery1ImageBar; }
		}

		public override RibbonBar ZoomBar
		{
			get { return Controller.Instance.Gallery1ZoomBar; }
		}

		public override RibbonBar CopyBar
		{
			get { return Controller.Instance.Gallery1CopyBar; }
		}

		public override ItemContainer BrowseModeContainer
		{
			get { return Controller.Instance.Gallery1BrowseModeContainer; }
		}

		public override ButtonItem ViewMode
		{
			get { return Controller.Instance.Gallery1View; }
		}

		public override ButtonItem EditMode
		{
			get { return Controller.Instance.Gallery1Edit; }
		}

		public override ButtonItem ImageSelect
		{
			get { return Controller.Instance.Gallery1ImageSelect; }
		}

		public override ButtonItem ImageCrop
		{
			get { return Controller.Instance.Gallery1ImageCrop; }
		}

		public override ButtonItem ZoomIn
		{
			get { return Controller.Instance.Gallery1ZoomIn; }
		}

		public override ButtonItem ZoomOut
		{
			get { return Controller.Instance.Gallery1ZoomOut; }
		}

		public override ButtonItem Copy
		{
			get { return Controller.Instance.Gallery1Copy; }
		}

		public override ComboBoxEdit SectionsList
		{
			get { return Controller.Instance.Gallery1Sections; }
		}

		public override ComboBoxEdit GroupsList
		{
			get { return Controller.Instance.Gallery1Groups; }
		}

		public override string Identifier
		{
			get { return ContentIdentifiers.Gallery1; }
		}

		public override RibbonTabItem TabPage
		{
			get { return Controller.Instance.TabGallery1; }
		}

		public override void GetHelp()
		{
			BusinessObjects.Instance.HelpManager.OpenHelpLink("gallery1");
		}
	}
}
