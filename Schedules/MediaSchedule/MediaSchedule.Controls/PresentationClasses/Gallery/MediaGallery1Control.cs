using Asa.Common.Core.Helpers;
using DevComponents.DotNetBar;
using DevExpress.XtraEditors;
using Asa.Media.Controls.BusinessClasses.Managers;
using Asa.Schedules.Common.Controls.ContentEditors.Events;
using Asa.Schedules.Common.Controls.Gallery;

namespace Asa.Media.Controls.PresentationClasses.Gallery
{
	public class MediaGallery1Control : GalleryControl
	{
		public override GalleryManager Manager => BusinessObjects.Instance.Gallery1Manager;

		public override RibbonPanel Panel => Controller.Instance.Gallery1Panel;

		public override RibbonBar BrowseBar => Controller.Instance.Gallery1BrowseBar;

		public override RibbonBar ImageBar => Controller.Instance.Gallery1ImageBar;

		public override RibbonBar ZoomBar => Controller.Instance.Gallery1ZoomBar;

		public override RibbonBar CopyBar => Controller.Instance.Gallery1CopyBar;

		public override ItemContainer BrowseModeContainer => Controller.Instance.Gallery1BrowseModeContainer;

		public override ButtonItem ViewMode => Controller.Instance.Gallery1View;

		public override ButtonItem EditMode => Controller.Instance.Gallery1Edit;

		public override ButtonItem ImageSelect => Controller.Instance.Gallery1ImageSelect;

		public override ButtonItem ImageCrop => Controller.Instance.Gallery1ImageCrop;

		public override ButtonItem ZoomIn => Controller.Instance.Gallery1ZoomIn;

		public override ButtonItem ZoomOut => Controller.Instance.Gallery1ZoomOut;

		public override ButtonItem Copy => Controller.Instance.Gallery1Copy;

		public override ComboBoxEdit SectionsList => Controller.Instance.Gallery1Sections;

		public override ComboBoxEdit GroupsList => Controller.Instance.Gallery1Groups;

		public override string Identifier => ContentIdentifiers.Gallery1;

		public override RibbonTabItem TabPage => Controller.Instance.TabGallery1;

		public override void ShowControl(ContentOpenEventArgs args = null)
		{
			Controller.Instance.MenuOutputPdfButton.Enabled = Controller.Instance.MenuEmailButton.Enabled = false;
			base.ShowControl(args);
		}

		public override void GetHelp()
		{
			BusinessObjects.Instance.HelpManager.OpenHelpLink("gallery1");
		}
	}
}
