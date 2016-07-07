using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Asa.Business.Media.Configuration;
using Asa.Common.Core.Enums;
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
			if (ResourceManager.Instance.DigitalProductsHomeMainLogoFile.ExistsLocal())
				pbMainLogo.Image = Image.FromFile(ResourceManager.Instance.DigitalProductsHomeMainLogoFile.LocalPath);
			if (ResourceManager.Instance.DigitalProductsHomeRightLogoFile.ExistsLocal())
			{
				pbRightLogo.Visible = true;
				pbRightLogo.Image = Image.FromFile(ResourceManager.Instance.DigitalProductsHomeRightLogoFile.LocalPath);
			}
			else
				pbRightLogo.Visible = false;
			if (ResourceManager.Instance.DigitalProductsHomeBottomLogoFile.ExistsLocal())
			{
				pnBottomLogo.Visible = true;
				pbBottomLogo.Image = Image.FromFile(ResourceManager.Instance.DigitalProductsHomeBottomLogoFile.LocalPath);
			}
			else
				pnBottomLogo.Visible = false;
		}
	}
}
