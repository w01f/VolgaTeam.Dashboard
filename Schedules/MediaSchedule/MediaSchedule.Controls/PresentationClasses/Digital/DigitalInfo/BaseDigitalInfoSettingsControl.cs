using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Asa.Business.Media.Entities.NonPersistent.Digital;
using Asa.Business.Online.Configuration;
using Asa.Business.Online.Dictionaries;
using DevExpress.XtraTab;

namespace Asa.Media.Controls.PresentationClasses.Digital.DigitalInfo
{
	[ToolboxItem(false)]
	//public partial class BaseDigitalInfoSettingsControl : UserControl
	public abstract partial class BaseDigitalInfoSettingsControl : XtraTabPage
	{
		private bool _allowToSave;
		protected MediaDigitalInfo _digitalInfo;

		protected BaseDigitalInfoSettingsControl()
		{
			InitializeComponent();
			Text = "Info";

			buttonXCategory.Text = DigitalControlsConfiguration.WrapTitle(ListManager.Instance.DefaultControlsConfiguration.DigitalInfoSettingsCategoryTitle ?? buttonXCategory.Text);
			buttonXSubCategory.Text = DigitalControlsConfiguration.WrapTitle(ListManager.Instance.DefaultControlsConfiguration.DigitalInfoSettingsSubCategoryTitle ?? buttonXSubCategory.Text);
			buttonXProduct.Text = DigitalControlsConfiguration.WrapTitle(ListManager.Instance.DefaultControlsConfiguration.DigitalInfoSettingsProductTitle ?? buttonXProduct.Text);
			buttonXInfo.Text = DigitalControlsConfiguration.WrapTitle(ListManager.Instance.DefaultControlsConfiguration.DigitalInfoSettingsInfoTitle ?? buttonXInfo.Text);
			buttonXLogo.Text = DigitalControlsConfiguration.WrapTitle(ListManager.Instance.DefaultControlsConfiguration.DigitalInfoSettingsLogosTitle ?? buttonXLogo.Text);
			buttonXMonthlyInvestment.Text = DigitalControlsConfiguration.WrapTitle(ListManager.Instance.DefaultControlsConfiguration.DigitalInfoSettingsMontlyInvestmentTitle ?? buttonXMonthlyInvestment.Text);
			buttonXTotalInvestment.Text = DigitalControlsConfiguration.WrapTitle(ListManager.Instance.DefaultControlsConfiguration.DigitalInfoSettingsTotalInvestmentTitle ?? buttonXTotalInvestment.Text);

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

		protected abstract void RaiseDataChanged();

		protected void LoadData()
		{
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

			RaiseDataChanged();
		}
	}
}
