using System;
using System.Drawing;
using System.Windows.Forms;
using Asa.Business.Media.Configuration;
using Asa.Business.Online.Entities.NonPersistent;
using Asa.Common.GUI.RetractableBar;
using Asa.Media.Controls.Properties;
using DevExpress.XtraTab;

namespace Asa.Media.Controls.PresentationClasses.Digital.Settings
{
	//public partial class DigitalListSettingsControl : UserControl, ISettingsControl
	public partial class DigitalListSettingsControl : XtraTabPage, ISettingsControl
	{
		private bool _allowToSave;
		private DigitalProductsContent _content;
		public int Order => 1;
		public ButtonInfo BarButton { get; }
		public DigitalSettingsType SettingsType => DigitalSettingsType.ProductList;

		public event EventHandler<SettingsChangedEventArgs> DataChanged;

		public DigitalListSettingsControl()
		{
			InitializeComponent();
			Text = "Info";
			BarButton = new ButtonInfo
			{
				Logo = Resources.DigitalSettingsList,
				Tooltip = String.Format("Open {0} Schedule Settings",
					MediaMetaData.Instance.DataTypeString),
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

				font = new Font(buttonXDimensions.Font.FontFamily, buttonXDimensions.Font.Size - 2, buttonXDimensions.Font.Style);
				buttonXDimensions.Font = font;
				buttonXRichMedia.Font = font;
				buttonXStrategy.Font = font;
				buttonXLocation.Font = font;
				buttonXTargeting.Font = font;
			}
		}

		public void LoadContentData(DigitalProductsContent content)
		{
			_content = content;

			_allowToSave = false;
			buttonXDimensions.Enabled = _content.ScheduleSettings.DigitalProductListViewSettings.EnableDigitalDimensions;
			buttonXLocation.Enabled = _content.ScheduleSettings.DigitalProductListViewSettings.EnableDigitalLocation;
			buttonXStrategy.Enabled = _content.ScheduleSettings.DigitalProductListViewSettings.EnableDigitalStrategy;
			buttonXTargeting.Enabled = _content.ScheduleSettings.DigitalProductListViewSettings.EnableDigitalTargeting;
			buttonXRichMedia.Enabled = _content.ScheduleSettings.DigitalProductListViewSettings.EnableDigitalRichMedia;

			buttonXDimensions.Checked = _content.ScheduleSettings.DigitalProductListViewSettings.ShowDigitalDimensions;
			buttonXLocation.Checked = _content.ScheduleSettings.DigitalProductListViewSettings.ShowDigitalLocation;
			buttonXStrategy.Checked = _content.ScheduleSettings.DigitalProductListViewSettings.ShowDigitalStrategy;
			buttonXTargeting.Checked = _content.ScheduleSettings.DigitalProductListViewSettings.ShowDigitalTargeting;
			buttonXRichMedia.Checked = _content.ScheduleSettings.DigitalProductListViewSettings.ShowDigitalRichMedia;
			_allowToSave = true;
		}

		private void OnSettingsChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;

			_content.ScheduleSettings.DigitalProductListViewSettings.ShowDigitalDimensions = buttonXDimensions.Checked;
			_content.ScheduleSettings.DigitalProductListViewSettings.ShowDigitalLocation = buttonXLocation.Checked;
			_content.ScheduleSettings.DigitalProductListViewSettings.ShowDigitalStrategy = buttonXStrategy.Checked;
			_content.ScheduleSettings.DigitalProductListViewSettings.ShowDigitalTargeting = buttonXTargeting.Checked;
			_content.ScheduleSettings.DigitalProductListViewSettings.ShowDigitalRichMedia = buttonXRichMedia.Checked;

			DataChanged?.Invoke(this, new SettingsChangedEventArgs { ChangedSettingsType = SettingsType });
		}
	}
}
