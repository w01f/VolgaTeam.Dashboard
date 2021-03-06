﻿using System;
using System.ComponentModel;
using System.Windows.Forms;
using Asa.Business.Media.Entities.NonPersistent.Digital;
using Asa.Business.Online.Configuration;
using Asa.Business.Online.Dictionaries;
using Asa.Common.Core.Helpers;
using DevExpress.Skins;
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

			var scaleFactor = Utilities.GetScaleFactor(CreateGraphics().DpiX);
			layoutControlItemCategory.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCategory.MaxSize, scaleFactor);
			layoutControlItemCategory.MinSize = RectangleHelper.ScaleSize(layoutControlItemCategory.MinSize, scaleFactor);
			layoutControlItemSubCategory.MaxSize = RectangleHelper.ScaleSize(layoutControlItemSubCategory.MaxSize, scaleFactor);
			layoutControlItemSubCategory.MinSize = RectangleHelper.ScaleSize(layoutControlItemSubCategory.MinSize, scaleFactor);
			layoutControlItemProduct.MaxSize = RectangleHelper.ScaleSize(layoutControlItemProduct.MaxSize, scaleFactor);
			layoutControlItemProduct.MinSize = RectangleHelper.ScaleSize(layoutControlItemProduct.MinSize, scaleFactor);
			layoutControlItemInfo.MaxSize = RectangleHelper.ScaleSize(layoutControlItemInfo.MaxSize, scaleFactor);
			layoutControlItemInfo.MinSize = RectangleHelper.ScaleSize(layoutControlItemInfo.MinSize, scaleFactor);
			layoutControlItemLogo.MaxSize = RectangleHelper.ScaleSize(layoutControlItemLogo.MaxSize, scaleFactor);
			layoutControlItemLogo.MinSize = RectangleHelper.ScaleSize(layoutControlItemLogo.MinSize, scaleFactor);
			layoutControlItemMonthlyInvestment.MaxSize = RectangleHelper.ScaleSize(layoutControlItemMonthlyInvestment.MaxSize, scaleFactor);
			layoutControlItemMonthlyInvestment.MinSize = RectangleHelper.ScaleSize(layoutControlItemMonthlyInvestment.MinSize, scaleFactor);
			layoutControlItemTotalInvestment.MaxSize = RectangleHelper.ScaleSize(layoutControlItemTotalInvestment.MaxSize, scaleFactor);
			layoutControlItemTotalInvestment.MinSize = RectangleHelper.ScaleSize(layoutControlItemTotalInvestment.MinSize, scaleFactor);
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
