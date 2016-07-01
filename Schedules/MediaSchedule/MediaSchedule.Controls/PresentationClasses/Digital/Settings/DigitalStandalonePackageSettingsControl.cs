using System;
using System.Drawing;
using Asa.Business.Media.Configuration;
using Asa.Business.Online.Dictionaries;
using Asa.Business.Online.Entities.NonPersistent;
using Asa.Business.Online.Enums;
using Asa.Common.GUI.RetractableBar;
using Asa.Media.Controls.Properties;
using DevExpress.XtraTab;

namespace Asa.Media.Controls.PresentationClasses.Digital.Settings
{
	//public partial class DigitalPackageSettingsControl : UserControl, ISettingsControl
	public partial class DigitalStandalonePackageSettingsControl : XtraTabPage, ISettingsControl
	{
		private bool _allowToSave;
		private DigitalProductsContent _content;
		public int Order => 1;
		public ButtonInfo BarButton { get; }
		public DigitalSettingsType SettingsType => DigitalSettingsType.StandalonePackage;

		public event EventHandler<SettingsChangedEventArgs> DataChanged;

		public DigitalStandalonePackageSettingsControl()
		{
			InitializeComponent();
			Text = "Info";
			BarButton = new ButtonInfo
			{
				Logo = Resources.DigitalSettingsPackage,
				Tooltip = String.Format("Open {0} Package Settings",
					MediaMetaData.Instance.DataTypeString),
				Action = () => { TabControl.SelectedTabPage = this; }
			};

			buttonXCategory.Text = ListManager.Instance.DefaultControlsConfiguration.StandalonePackageSettingsCategoryTitle ?? buttonXCategory.Text;
			buttonXGroup.Text = ListManager.Instance.DefaultControlsConfiguration.StandalonePackageColumnsSubCategoryTitle ?? buttonXGroup.Text;
			buttonXProduct.Text = ListManager.Instance.DefaultControlsConfiguration.StandalonePackageSettingsProductTitle ?? buttonXProduct.Text;
			buttonXImpressions.Text = ListManager.Instance.DefaultControlsConfiguration.StandalonePackageSettingsImpressionsTitle ?? buttonXImpressions.Text;
			buttonXCPM.Text = ListManager.Instance.DefaultControlsConfiguration.StandalonePackageSettingsCPMTitle ?? buttonXCPM.Text;
			buttonXRate.Text = ListManager.Instance.DefaultControlsConfiguration.StandalonePackageSettingsRateTitle ?? buttonXRate.Text;
			buttonXInvestment.Text = ListManager.Instance.DefaultControlsConfiguration.StandalonePackageSettingsInvestmentTitle ?? buttonXInvestment.Text;
			buttonXInfo.Text = ListManager.Instance.DefaultControlsConfiguration.StandalonePackageSettingsInfoTitle ?? buttonXInfo.Text;
			buttonXLocation.Text = ListManager.Instance.DefaultControlsConfiguration.StandalonePackageSettingsLocationTitle ?? buttonXLocation.Text;
			buttonXScreenshot.Text = ListManager.Instance.DefaultControlsConfiguration.StandalonePackageSettingsScreenshotTitle ?? buttonXScreenshot.Text;
			labelControlFormula.Text = !String.IsNullOrEmpty(ListManager.Instance.DefaultControlsConfiguration.StandalonePackageSettingsFormulaTitle) ?
				String.Format("<b>{0}</b>", ListManager.Instance.DefaultControlsConfiguration.StandalonePackageSettingsFormulaTitle) :
				labelControlFormula.Text;

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
				buttonXGroup.Font = font;
				buttonXProduct.Font = font;
				buttonXImpressions.Font = font;
				buttonXCPM.Font = font;
				buttonXRate.Font = font;
				buttonXInvestment.Font = font;
				buttonXInfo.Font = font;
				buttonXLocation.Font = font;
				buttonXScreenshot.Font = font;
			}
		}

		public void LoadContentData(DigitalProductsContent content)
		{
			_content = content;

			_allowToSave = false;
			buttonXCategory.Checked = _content.StandalonePackage.DigitalPackageSettings.ShowCategory;
			buttonXGroup.Checked = _content.StandalonePackage.DigitalPackageSettings.ShowGroup;
			buttonXProduct.Checked = _content.StandalonePackage.DigitalPackageSettings.ShowProduct;
			buttonXImpressions.Checked = _content.StandalonePackage.DigitalPackageSettings.ShowImpressions;
			buttonXCPM.Checked = _content.StandalonePackage.DigitalPackageSettings.ShowCPM;
			buttonXRate.Checked = _content.StandalonePackage.DigitalPackageSettings.ShowRate;
			buttonXInvestment.Checked = _content.StandalonePackage.DigitalPackageSettings.ShowInvestment;
			buttonXInfo.Checked = _content.StandalonePackage.DigitalPackageSettings.ShowInfo;
			buttonXLocation.Checked = _content.StandalonePackage.DigitalPackageSettings.ShowLocation;
			buttonXScreenshot.Checked = _content.StandalonePackage.DigitalPackageSettings.ShowScreenshot;
			switch (_content.StandalonePackage.DigitalPackageSettings.Formula)
			{
				case FormulaType.CPM:
					checkEditFormulaCPM.Checked = true;
					checkEditFormulaInvestment.Checked = false;
					checkEditFormulaImpressions.Checked = false;
					break;
				case FormulaType.Investment:
					checkEditFormulaCPM.Checked = false;
					checkEditFormulaInvestment.Checked = true;
					checkEditFormulaImpressions.Checked = false;
					break;
				case FormulaType.Impressions:
					checkEditFormulaCPM.Checked = false;
					checkEditFormulaInvestment.Checked = false;
					checkEditFormulaImpressions.Checked = true;
					break;
			}
			_allowToSave = true;
		}

		private void OnSettingsChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;

			_content.StandalonePackage.DigitalPackageSettings.ShowCategory = buttonXCategory.Checked;
			_content.StandalonePackage.DigitalPackageSettings.ShowGroup = buttonXGroup.Checked;
			_content.StandalonePackage.DigitalPackageSettings.ShowProduct = buttonXProduct.Checked;
			_content.StandalonePackage.DigitalPackageSettings.ShowImpressions = buttonXImpressions.Checked;
			_content.StandalonePackage.DigitalPackageSettings.ShowCPM = buttonXCPM.Checked;
			_content.StandalonePackage.DigitalPackageSettings.ShowRate = buttonXRate.Checked;
			_content.StandalonePackage.DigitalPackageSettings.ShowInvestment = buttonXInvestment.Checked;
			_content.StandalonePackage.DigitalPackageSettings.ShowInfo = buttonXInfo.Checked;
			_content.StandalonePackage.DigitalPackageSettings.ShowLocation = buttonXLocation.Checked;
			_content.StandalonePackage.DigitalPackageSettings.ShowScreenshot = buttonXScreenshot.Checked;
			if (checkEditFormulaCPM.Checked)
				_content.StandalonePackage.DigitalPackageSettings.Formula = FormulaType.CPM;
			else if (checkEditFormulaInvestment.Checked)
				_content.StandalonePackage.DigitalPackageSettings.Formula = FormulaType.Investment;
			else if (checkEditFormulaImpressions.Checked)
				_content.StandalonePackage.DigitalPackageSettings.Formula = FormulaType.Impressions;

			DataChanged?.Invoke(this, new SettingsChangedEventArgs { ChangedSettingsType = SettingsType });
		}
	}
}
