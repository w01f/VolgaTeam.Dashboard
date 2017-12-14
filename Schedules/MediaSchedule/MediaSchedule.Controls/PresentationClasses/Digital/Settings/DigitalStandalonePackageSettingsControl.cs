using System;
using System.Windows.Forms;
using Asa.Business.Media.Configuration;
using Asa.Business.Online.Dictionaries;
using Asa.Business.Online.Entities.NonPersistent;
using Asa.Business.Online.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.RetractableBar;
using Asa.Media.Controls.BusinessClasses.Managers;
using Asa.Media.Controls.Properties;
using DevExpress.Skins;
using DevExpress.XtraTab;

namespace Asa.Media.Controls.PresentationClasses.Digital.Settings
{
	//public partial class DigitalStandalonePackageSettingsControl : UserControl, ISettingsControl
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
				Logo = BusinessObjects.Instance.ImageResourcesManager.DigitalRetractableBarStandalonePackageImage ?? Resources.DigitalSettingsPackage,
				Tooltip = String.Format("Open {0} Package Settings",
					MediaMetaData.Instance.DataTypeString),
				Action = () => { TabControl.SelectedTabPage = this; }
			};

			pictureEditFormulaLogo.Image =
				BusinessObjects.Instance.ImageResourcesManager.DigitalRetractableBarStandalonePackageFormulaImage ??
				pictureEditFormulaLogo.Image;

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
			simpleLabelItemFormulaTitle.Text = !String.IsNullOrEmpty(ListManager.Instance.DefaultControlsConfiguration.StandalonePackageSettingsFormulaTitle) ?
				String.Format("<b>{0}</b>", ListManager.Instance.DefaultControlsConfiguration.StandalonePackageSettingsFormulaTitle) :
				simpleLabelItemFormulaTitle.Text;

			var scaleFactor = Utilities.GetScaleFactor(CreateGraphics().DpiX);
			layoutControlItemCategory.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCategory.MaxSize, scaleFactor);
			layoutControlItemCategory.MinSize = RectangleHelper.ScaleSize(layoutControlItemCategory.MinSize, scaleFactor);
			layoutControlItemGroup.MaxSize = RectangleHelper.ScaleSize(layoutControlItemGroup.MaxSize, scaleFactor);
			layoutControlItemGroup.MinSize = RectangleHelper.ScaleSize(layoutControlItemGroup.MinSize, scaleFactor);
			layoutControlItemProduct.MaxSize = RectangleHelper.ScaleSize(layoutControlItemProduct.MaxSize, scaleFactor);
			layoutControlItemProduct.MinSize = RectangleHelper.ScaleSize(layoutControlItemProduct.MinSize, scaleFactor);
			layoutControlItemImpressions.MaxSize = RectangleHelper.ScaleSize(layoutControlItemImpressions.MaxSize, scaleFactor);
			layoutControlItemImpressions.MinSize = RectangleHelper.ScaleSize(layoutControlItemImpressions.MinSize, scaleFactor);
			layoutControlItemCPM.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCPM.MaxSize, scaleFactor);
			layoutControlItemCPM.MinSize = RectangleHelper.ScaleSize(layoutControlItemCPM.MinSize, scaleFactor);
			layoutControlItemRate.MaxSize = RectangleHelper.ScaleSize(layoutControlItemRate.MaxSize, scaleFactor);
			layoutControlItemRate.MinSize = RectangleHelper.ScaleSize(layoutControlItemRate.MinSize, scaleFactor);
			layoutControlItemInvestment.MaxSize = RectangleHelper.ScaleSize(layoutControlItemInvestment.MaxSize, scaleFactor);
			layoutControlItemInvestment.MinSize = RectangleHelper.ScaleSize(layoutControlItemInvestment.MinSize, scaleFactor);
			layoutControlItemInfo.MaxSize = RectangleHelper.ScaleSize(layoutControlItemInfo.MaxSize, scaleFactor);
			layoutControlItemInfo.MinSize = RectangleHelper.ScaleSize(layoutControlItemInfo.MinSize, scaleFactor);
			layoutControlItemLocation.MaxSize = RectangleHelper.ScaleSize(layoutControlItemLocation.MaxSize, scaleFactor);
			layoutControlItemLocation.MinSize = RectangleHelper.ScaleSize(layoutControlItemLocation.MinSize, scaleFactor);
			layoutControlItemScreenshot.MaxSize = RectangleHelper.ScaleSize(layoutControlItemScreenshot.MaxSize, scaleFactor);
			layoutControlItemScreenshot.MinSize = RectangleHelper.ScaleSize(layoutControlItemScreenshot.MinSize, scaleFactor);
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
