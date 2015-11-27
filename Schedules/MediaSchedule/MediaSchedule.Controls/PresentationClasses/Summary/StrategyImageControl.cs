using System.ComponentModel;
using System.Drawing;
using DevExpress.XtraTab;

namespace Asa.MediaSchedule.Controls.PresentationClasses.Summary
{
	[ToolboxItem(false)]
	//public partial class StrategyImageControl : UserControl
	public partial class StrategyImageControl : XtraTabPage
	{
		public StrategyImageControl()
		{
			InitializeComponent();
			Text = "Favorites";
			favoriteImagesControl.Init();
			favoriteImagesControl.ImageTooltip = "Drag and Drop this image to a program on the left";
		}

		public void AddLogoToFavorites(Image logo, string defaultName)
		{
			favoriteImagesControl.AddToFavorites(logo, defaultName);
		}
	}
}
