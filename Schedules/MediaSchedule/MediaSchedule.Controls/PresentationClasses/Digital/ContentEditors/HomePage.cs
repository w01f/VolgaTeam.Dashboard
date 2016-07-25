using System.ComponentModel;
using System.Windows.Forms;
using Asa.Common.Core.Enums;
using Asa.Media.Controls.BusinessClasses.Managers;
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
			pbMainLogo.Image = BusinessObjects.Instance.ImageResourcesManager.DigitalProductsHomeMainLogo ?? pbMainLogo.Image;
			if (BusinessObjects.Instance.ImageResourcesManager.DigitalProductsHomeRightLogo != null)
			{
				pbRightLogo.Visible = true;
				pbRightLogo.Image = BusinessObjects.Instance.ImageResourcesManager.DigitalProductsHomeRightLogo;
			}
			else
				pbRightLogo.Visible = false;
			if (BusinessObjects.Instance.ImageResourcesManager.DigitalProductsHomeBottomLogo != null)
			{
				pnBottomLogo.Visible = true;
				pbBottomLogo.Image = BusinessObjects.Instance.ImageResourcesManager.DigitalProductsHomeBottomLogo;
			}
			else
				pnBottomLogo.Visible = false;
		}
	}
}
