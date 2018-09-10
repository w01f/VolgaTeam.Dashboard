using System;
using System.Windows.Forms;
using Asa.Business.Media.Configuration;
using Asa.Business.Online.Dictionaries;
using Asa.Business.Online.Entities.NonPersistent;
using Asa.Business.Online.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.RetractableBar;
using Asa.Media.Controls.BusinessClasses.Managers;
using DevExpress.Skins;
using DevExpress.XtraTab;

namespace Asa.Media.Controls.PresentationClasses.Digital.Settings
{
	//public partial class DigitalProductPackageSettingsControl : UserControl, ISettingsControl
	public partial class DigitalProductPackageSettingsControl : XtraTabPage, ISettingsControl
	{
		private bool _allowToSave;
		private DigitalProductsContent _content;
		public int Order => 1;
		public ButtonInfo BarButton { get; }
		public DigitalSettingsType SettingsType => DigitalSettingsType.ProductPackage;

		public event EventHandler<SettingsChangedEventArgs> DataChanged;

		public DigitalProductPackageSettingsControl()
		{
			InitializeComponent();
			Text = "Info";
			BarButton = new ButtonInfo
			{
				Logo = BusinessObjects.Instance.ImageResourcesManager.DigitalRetractableBarProductPackageImage ?? Properties.Resources.DigitalSettingsPackage,
				Tooltip = String.Format("Open {0} Schedule Settings",
					MediaMetaData.Instance.DataTypeString),
				Action = () => { TabControl.SelectedTabPage = this; }
			};

			pictureEditFormulaLogo.Image =
				BusinessObjects.Instance.ImageResourcesManager.DigitalRetractableBarProductPackageFormulaImage ??
				pictureEditFormulaLogo.Image;

			buttonXCategory.Text = ListManager.Instance.DefaultControlsConfiguration.ProductPackageSettingsCategoryTitle ?? buttonXCategory.Text;
			buttonXGroup.Text = ListManager.Instance.DefaultControlsConfiguration.ProductPackageColumnsSubCategoryTitle ?? buttonXGroup.Text;
			buttonXProduct.Text = ListManager.Instance.DefaultControlsConfiguration.ProductPackageSettingsProductTitle ?? buttonXProduct.Text;
			buttonXImpressions.Text = ListManager.Instance.DefaultControlsConfiguration.ProductPackageSettingsImpressionsTitle ?? buttonXImpressions.Text;
			buttonXCPM.Text = ListManager.Instance.DefaultControlsConfiguration.ProductPackageSettingsCPMTitle ?? buttonXCPM.Text;
			buttonXRate.Text = ListManager.Instance.DefaultControlsConfiguration.ProductPackageSettingsRateTitle ?? buttonXRate.Text;
			buttonXInvestment.Text = ListManager.Instance.DefaultControlsConfiguration.ProductPackageSettingsInvestmentTitle ?? buttonXInvestment.Text;
			buttonXInfo.Text = ListManager.Instance.DefaultControlsConfiguration.ProductPackageSettingsInfoTitle ?? buttonXInfo.Text;
			buttonXLocation.Text = ListManager.Instance.DefaultControlsConfiguration.ProductPackageSettingsLocationTitle ?? buttonXLocation.Text;
			buttonXScreenshot.Text = ListManager.Instance.DefaultControlsConfiguration.ProductPackageSettingsScreenshotTitle ?? buttonXScreenshot.Text;
			simpleLabelItemFormulaTitle.Text = !String.IsNullOrEmpty(ListManager.Instance.DefaultControlsConfiguration.ProductPackageSettingsFormulaTitle) ?
				String.Format("<b>{0}</b>", ListManager.Instance.DefaultControlsConfiguration.ProductPackageSettingsFormulaTitle) :
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
			buttonXCategory.Checked = _content.ScheduleSettings.DigitalPackageSettings.ShowCategory;
			buttonXGroup.Checked = _content.ScheduleSettings.DigitalPackageSettings.ShowGroup;
			buttonXProduct.Checked = _content.ScheduleSettings.DigitalPackageSettings.ShowProduct;
			buttonXImpressions.Checked = _content.ScheduleSettings.DigitalPackageSettings.ShowImpressions;
			buttonXCPM.Checked = _content.ScheduleSettings.DigitalPackageSettings.ShowCPM;
			buttonXRate.Checked = _content.ScheduleSettings.DigitalPackageSettings.ShowRate;
			buttonXInvestment.Checked = _content.ScheduleSettings.DigitalPackageSettings.ShowInvestment;
			buttonXInfo.Checked = _content.ScheduleSettings.DigitalPackageSettings.ShowInfo;
			buttonXLocation.Checked = _content.ScheduleSettings.DigitalPackageSettings.ShowLocation;
			buttonXScreenshot.Checked = _content.ScheduleSettings.DigitalPackageSettings.ShowScreenshot;
			switch (_content.ScheduleSettings.DigitalPackageSettings.Formula)
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

			_content.ScheduleSettings.DigitalPackageSettings.ShowCategory = buttonXCategory.Checked;
			_content.ScheduleSettings.DigitalPackageSettings.ShowGroup = buttonXGroup.Checked;
			_content.ScheduleSettings.DigitalPackageSettings.ShowProduct = buttonXProduct.Checked;
			_content.ScheduleSettings.DigitalPackageSettings.ShowImpressions = buttonXImpressions.Checked;
			_content.ScheduleSettings.DigitalPackageSettings.ShowCPM = buttonXCPM.Checked;
			_content.ScheduleSettings.DigitalPackageSettings.ShowRate = buttonXRate.Checked;
			_content.ScheduleSettings.DigitalPackageSettings.ShowInvestment = buttonXInvestment.Checked;
			_content.ScheduleSettings.DigitalPackageSettings.ShowInfo = buttonXInfo.Checked;
			_content.ScheduleSettings.DigitalPackageSettings.ShowLocation = buttonXLocation.Checked;
			_content.ScheduleSettings.DigitalPackageSettings.ShowScreenshot = buttonXScreenshot.Checked;
			if (checkEditFormulaCPM.Checked)
				_content.ScheduleSettings.DigitalPackageSettings.Formula = FormulaType.CPM;
			else if (checkEditFormulaInvestment.Checked)
				_content.ScheduleSettings.DigitalPackageSettings.Formula = FormulaType.Investment;
			else if (checkEditFormulaImpressions.Checked)
				_content.ScheduleSettings.DigitalPackageSettings.Formula = FormulaType.Impressions;

			DataChanged?.Invoke(this, new SettingsChangedEventArgs { ChangedSettingsType = SettingsType });
		}
	}
}
