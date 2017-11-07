using System.ComponentModel;
using System.Windows.Forms;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using Asa.Media.Controls.BusinessClasses.Managers;
using DevExpress.Skins;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraTab;

namespace Asa.Media.Controls.PresentationClasses.Digital.ContentEditors
{
	[ToolboxItem(false)]
	//public partial class HomePage : UserControl
	public partial class HomePage : XtraTabPage, IDigitalSection
	{
		public DigitalSectionType SectionType => DigitalSectionType.Home;
		public SlideType SlideType => SlideType.DigitalProducts;
		public string HelpTag => "homedg";
		public HomePage()
		{
			InitializeComponent();
			Text = Business.Online.Dictionaries.ListManager.Instance.DefaultControlsConfiguration.SectionsHomeTitle ?? "Home";
			pictureEditMainLogo.Image = BusinessObjects.Instance.ImageResourcesManager.DigitalProductsHomeMainLogo ?? pictureEditMainLogo.Image;
			layoutControlItemRightLogo.Visibility = BusinessObjects.Instance.ImageResourcesManager.DigitalProductsHomeRightLogo != null?LayoutVisibility.Always : LayoutVisibility.Never;
			pictureEditRightLogo.Image = BusinessObjects.Instance.ImageResourcesManager.DigitalProductsHomeRightLogo;
			layoutControlItemBotomLogo.Visibility = BusinessObjects.Instance.ImageResourcesManager.DigitalProductsHomeBottomLogo != null ? LayoutVisibility.Always : LayoutVisibility.Never;
			pictureEditBottomLogo.Image = BusinessObjects.Instance.ImageResourcesManager.DigitalProductsHomeBottomLogo;

			var scaleFactor = Utilities.GetScaleFactor(CreateGraphics().DpiX);
			layoutControlItemRightLogo.MaxSize = RectangleHelper.ScaleSize(layoutControlItemRightLogo.MaxSize, scaleFactor);
			layoutControlItemRightLogo.MinSize = RectangleHelper.ScaleSize(layoutControlItemRightLogo.MinSize, scaleFactor);
			layoutControlItemBotomLogo.MaxSize = RectangleHelper.ScaleSize(layoutControlItemBotomLogo.MaxSize, scaleFactor);
			layoutControlItemBotomLogo.MinSize = RectangleHelper.ScaleSize(layoutControlItemBotomLogo.MinSize, scaleFactor);
		}
	}
}
