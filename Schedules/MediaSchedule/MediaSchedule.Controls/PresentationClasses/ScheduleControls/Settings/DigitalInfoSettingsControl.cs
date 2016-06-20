using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Asa.Business.Media.Entities.NonPersistent.Section.Content;
using Asa.Business.Media.Entities.NonPersistent.Section.Digital;
using Asa.Business.Online.Dictionaries;
using Asa.Common.GUI.RetractableBar;
using Asa.Media.Controls.Properties;
using DevExpress.XtraTab;

namespace Asa.Media.Controls.PresentationClasses.ScheduleControls.Settings
{
	[ToolboxItem(false)]
	//public partial class SectionDigitalSettingsControl : UserControl
	public partial class DigitalInfoSettingsControl : XtraTabPage, ISectionSettingsControl
	{
		private bool _allowToSave;
		private ScheduleSection _sectionData;
		private SectionDigitalInfo _digitalInfo;

		public int Order => 0;
		public bool IsAvailable => true;
		public ButtonInfo BarButton { get; }
		public ScheduleSettingsType SettingsType => ScheduleSettingsType.DigitalInfo;

		public event EventHandler<SettingsChangedEventArgs> DataChanged;

		public DigitalInfoSettingsControl()
		{
			InitializeComponent();
			Text = "Info";
			BarButton = new ButtonInfo
			{
				Logo = Resources.SectionSettingsInfo,
				Tooltip = "Open Digital Settings",
				Action = () => { TabControl.SelectedTabPage = this; }
			};

			buttonXCategory.Text = ListManager.Instance.DefaultControlsConfiguration.MediaDigitalSettingsCategoryTitle ?? buttonXCategory.Text;
			buttonXSubCategory.Text = ListManager.Instance.DefaultControlsConfiguration.MediaDigitalSettingsSubCategoryTitle ?? buttonXSubCategory.Text;
			buttonXProduct.Text = ListManager.Instance.DefaultControlsConfiguration.MediaDigitalSettingsProductTitle ?? buttonXProduct.Text;
			buttonXInfo.Text = ListManager.Instance.DefaultControlsConfiguration.MediaDigitalSettingsInfoTitle ?? buttonXInfo.Text;
			buttonXLogo.Text = ListManager.Instance.DefaultControlsConfiguration.MediaDigitalSettingsLogosTitle ?? buttonXLogo.Text;
			buttonXMonthlyInvestment.Text = ListManager.Instance.DefaultControlsConfiguration.MediaDigitalSettingsMontlyInvestmentTitle ?? buttonXMonthlyInvestment.Text;
			buttonXTotalInvestment.Text = ListManager.Instance.DefaultControlsConfiguration.MediaDigitalSettingsTotalInvestmentTitle ?? buttonXTotalInvestment.Text;

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

				font = new Font(buttonXCategory.Font.FontFamily, buttonXCategory.Font.Size - 2, buttonXCategory.Font.Style);
				buttonXCategory.Font = font;
				buttonXLogo.Font = font;
				buttonXSubCategory.Font = font;
				buttonXProduct.Font = font;
				buttonXLogo.Font = font;
				buttonXMonthlyInvestment.Font = font;
				buttonXTotalInvestment.Font = font;
				buttonXInfo.Font = font;
			}
		}

		public void LoadSectionData(ScheduleSection sectionData)
		{
			_sectionData = sectionData;
			_digitalInfo = _sectionData.DigitalInfo;

			_allowToSave = false;
			buttonXCategory.Checked = _digitalInfo.ShowCategory;
			buttonXSubCategory.Checked = _digitalInfo.ShowSubCategory;
			buttonXProduct.Checked = _digitalInfo.ShowProduct;
			buttonXInfo.Checked = _digitalInfo.ShowInfo;
			buttonXLogo.Checked = _digitalInfo.ShowLogo;
			buttonXMonthlyInvestment.Checked = _digitalInfo.ShowMonthlyInvestemt;
			buttonXTotalInvestment.Checked = _digitalInfo.ShowTotalInvestemt;
			_allowToSave = true;
		}

		private void OnSettingsChanged(Object sender, EventArgs e)
		{
			if (!_allowToSave) return;

			_digitalInfo.ShowCategory = buttonXCategory.Checked;
			_digitalInfo.ShowSubCategory = buttonXSubCategory.Checked;
			_digitalInfo.ShowProduct = buttonXProduct.Checked;
			_digitalInfo.ShowInfo = buttonXInfo.Checked;
			_digitalInfo.ShowLogo = buttonXLogo.Checked;
			_digitalInfo.ShowMonthlyInvestemt = buttonXMonthlyInvestment.Checked;
			_digitalInfo.ShowTotalInvestemt = buttonXTotalInvestment.Checked;

			DataChanged?.Invoke(this, new SettingsChangedEventArgs { ChangedSettingsType = SettingsType });
		}
	}
}
