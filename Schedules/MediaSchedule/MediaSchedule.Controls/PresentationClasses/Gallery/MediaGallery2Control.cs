using Asa.Common.Core.Helpers;
using Asa.Common.GUI.ContentEditors.Events;
using Asa.Common.GUI.Gallery;
using DevComponents.DotNetBar;
using DevExpress.XtraEditors;
using Asa.Media.Controls.BusinessClasses.Managers;

namespace Asa.Media.Controls.PresentationClasses.Gallery
{
	public class MediaGallery2Control : GalleryControl
	{
		public override GalleryManager Manager => BusinessObjects.Instance.Gallery2Manager;

		public override RibbonPanel Panel => Controller.Instance.Gallery2Panel;

		public override RibbonBar BrowseBar => Controller.Instance.Gallery2BrowseBar;

		public override RibbonBar ImageBar => Controller.Instance.Gallery2ImageBar;

		public override RibbonBar ZoomBar => Controller.Instance.Gallery2ZoomBar;

		public override RibbonBar CopyBar => Controller.Instance.Gallery2CopyBar;

		public override ItemContainer BrowseModeContainer => Controller.Instance.Gallery2BrowseModeContainer;

		public override ButtonItem ViewMode => Controller.Instance.Gallery2View;

		public override ButtonItem EditMode => Controller.Instance.Gallery2Edit;

		public override ButtonItem ImageSelect => Controller.Instance.Gallery2ImageSelect;

		public override ButtonItem ImageCrop => Controller.Instance.Gallery2ImageCrop;

		public override ButtonItem ZoomIn => Controller.Instance.Gallery2ZoomIn;

		public override ButtonItem ZoomOut => Controller.Instance.Gallery2ZoomOut;

		public override ButtonItem Copy => Controller.Instance.Gallery2Copy;

		public override ComboBoxEdit SectionsList => Controller.Instance.Gallery2Sections;

		public override ComboBoxEdit GroupsList => Controller.Instance.Gallery2Groups;

		public override string Identifier => ContentIdentifiers.Gallery2;

		public override RibbonTabItem TabPage => Controller.Instance.TabGallery2;

		public override void ShowControl(ContentOpenEventArgs args = null)
		{
			Controller.Instance.MenuOutputPdfButton.Enabled = Controller.Instance.MenuEmailButton.Enabled = false;
			base.ShowControl(args);
		}

		public override void GetHelp()
		{
			BusinessObjects.Instance.HelpManager.OpenHelpLink("gallery2");
		}
	}
}
