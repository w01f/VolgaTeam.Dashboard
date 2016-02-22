using System;
using System.Drawing;
using System.Windows.Forms;
using Asa.Business.Media.Configuration;
using Asa.Common.GUI.RetractableBar;
using Asa.Media.Controls.BusinessClasses;
using Asa.Media.Controls.Properties;
using DevExpress.XtraTab;

namespace Asa.Media.Controls.PresentationClasses.ScheduleControls.Settings
{
	//public partial class ColorSettingsControl : UserControl, ISettingsControl
	public partial class ColorSettingsControl : XtraTabPage, ISettingsControl
	{
		public int Order => 4;
		public bool IsAvailable => BusinessObjects.Instance.OutputManager.ScheduleColors.Items.Count > 1;
		public ButtonInfo BarButton { get; }
		public ScheduleSettingsType SettingsType => ScheduleSettingsType.Colors;

		public ColorSettingsControl()
		{
			InitializeComponent();
			Text = "Slide Style";
			BusinessObjects.Instance.OutputManager.ColorsChanged += (o, e) =>
				{
					InitColorControls();
				};
			BarButton = new ButtonInfo
			{
				Logo = Resources.SectionSettingsOptions,
				Tooltip = "Open Slide Style",
				Action = () => { TabControl.SelectedTabPage = this; }
			};
			if (CreateGraphics().DpiX > 96)
			{
				laColorsTitle.Font = new Font(laColorsTitle.Font.FontFamily, laColorsTitle.Font.Size - 2, laColorsTitle.Font.Style);
			}
			InitColorControls();
		}

		private void InitColorControls()
		{
			outputColorSelector.InitData(BusinessObjects.Instance.OutputManager.ScheduleColors, MediaMetaData.Instance.SettingsManager.SelectedColor);
			outputColorSelector.ColorChanged += OnColorChanged;
		}

		private void OnColorChanged(object sender, EventArgs e)
		{
			MediaMetaData.Instance.SettingsManager.SelectedColor = outputColorSelector.SelectedColor ?? String.Empty;
			MediaMetaData.Instance.SettingsManager.SaveSettings();
		}
	}
}
