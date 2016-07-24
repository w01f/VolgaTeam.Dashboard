using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Media.Configuration;
using Asa.Common.GUI.RetractableBar;
using Asa.Media.Controls.BusinessClasses.Managers;
using Asa.Media.Controls.Properties;
using DevExpress.XtraTab;

namespace Asa.Media.Controls.PresentationClasses.SnapshotControls.Settings
{
	//public partial class ColorSettingsControl : UserControl, ISettingsControl
	public partial class ColorSettingsControl : XtraTabPage, ISettingsControl
	{
		public int Order => 3;
		public bool IsAvailable => BusinessObjects.Instance.OutputManager.SnapshotColors.Items.Count > 1;
		public ButtonInfo BarButton { get; }
		public SnapshotSettingsType SettingsType => SnapshotSettingsType.Colors;

		public ColorSettingsControl()
		{
			InitializeComponent();
			Text = "Slide Style";
			BusinessObjects.Instance.OutputManager.ColorCollectionChanged += (o, e) =>
				{
					LoadData();
				};
			BusinessObjects.Instance.OutputManager.SelectedColorChanged += (o, e) =>
			{
				LoadData();
			};
			BarButton = new ButtonInfo
			{
				Logo = Resources.SectionSettingsStyle,
				Tooltip = "Open Slide Style",
				Action = () => { TabControl.SelectedTabPage = this; }
			};
			if (CreateGraphics().DpiX > 96)
			{
				var font = new Font(styleController.Appearance.Font.FontFamily, styleController.Appearance.Font.Size - 2,
					styleController.Appearance.Font.Style);
				styleController.Appearance.Font = font;
				styleController.AppearanceDisabled.Font = font;
				styleController.AppearanceDropDown.Font = font;
				styleController.AppearanceDropDownHeader.Font = font;
				styleController.AppearanceFocused.Font = font;
				styleController.AppearanceReadOnly.Font = font;
			}
			LoadData();
		}

		private void LoadData()
		{
			outputColorSelector.InitData(BusinessObjects.Instance.OutputManager.SnapshotColors, MediaMetaData.Instance.SettingsManager.SelectedColor);
			outputColorSelector.ColorChanged += OnColorChanged;
		}

		private void OnColorChanged(object sender, EventArgs e)
		{
			MediaMetaData.Instance.SettingsManager.SelectedColor = outputColorSelector.SelectedColor ?? 
				BusinessObjects.Instance.OutputManager.SnapshotColors.Items.Select(ci => ci.Name).FirstOrDefault();
			MediaMetaData.Instance.SettingsManager.SaveSettings();
			BusinessObjects.Instance.OutputManager.RaiseSelectedColorChanged();
		}
	}
}
