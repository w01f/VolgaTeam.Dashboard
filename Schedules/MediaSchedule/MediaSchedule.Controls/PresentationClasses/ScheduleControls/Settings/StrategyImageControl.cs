using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Asa.Common.GUI.RetractableBar;
using Asa.Media.Controls.Properties;
using DevExpress.XtraTab;

namespace Asa.Media.Controls.PresentationClasses.ScheduleControls.Settings
{
	[ToolboxItem(false)]
	//public partial class StrategyImageControl : UserControl
	public partial class StrategyImageControl : XtraTabPage, ISettingsControl
	{
		public Int32 Order => 2;
		public bool IsAvailable => true;
		public ButtonInfo BarButton { get; }
		public ScheduleSettingsType SettingsType=>ScheduleSettingsType.FavoriteImages;

		public StrategyImageControl()
		{
			InitializeComponent();
			Text = "My Gallery";
			BarButton = new ButtonInfo
			{
				Tooltip = "Open My Gallery",
				Logo = Resources.SummaryOptionsFavorites,
				Action = () => { TabControl.SelectedTabPage = this; }
			};
			favoriteImagesControl.Init();
			favoriteImagesControl.ImageTooltip = "Drag and Drop this image to a program on the left";
		}

		public void AddLogoToFavorites(Image logo, string defaultName)
		{
			favoriteImagesControl.AddToFavorites(logo, defaultName);
		}

		public void Release()
		{
			favoriteImagesControl.Release();
			favoriteImagesControl.Dispose();
		}
	}
}
