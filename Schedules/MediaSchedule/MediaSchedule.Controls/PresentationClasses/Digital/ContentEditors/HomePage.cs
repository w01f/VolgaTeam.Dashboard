using System;
using System.ComponentModel;
using System.Drawing;
using Asa.Business.Media.Configuration;
using DevExpress.XtraTab;

namespace Asa.Media.Controls.PresentationClasses.Digital.ContentEditors
{
	[ToolboxItem(false)]
	//public partial class HomePage : UserControl
	public partial class HomePage : XtraTabPage, IDigitalSection
	{
		public DigitalSectionType SectionType=>DigitalSectionType.Home;
		public string HelpTag => "homedg";
		public HomePage()
		{
			InitializeComponent();
			Text = Business.Online.Dictionaries.ListManager.Instance.DefaultControlsConfiguration.SectionsHomeTitle ?? "Home";
			if (ResourceManager.Instance.DigitalProductsHomeLogoFile.ExistsLocal())
				pbLogo.Image = Image.FromFile(ResourceManager.Instance.DigitalProductsHomeLogoFile.LocalPath);
		}
	}
}
