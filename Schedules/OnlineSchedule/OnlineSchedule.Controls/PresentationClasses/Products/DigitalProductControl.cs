using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Asa.Business.Online.Dictionaries;
using Asa.Business.Online.Entities.NonPersistent;
using Asa.Business.Online.Enums;
using Asa.Common.Core.Configuration;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;
using Asa.Common.GUI.Preview;
using Asa.Online.Controls.InteropClasses;
using Asa.Online.Controls.PresentationClasses.Summary;
using DevExpress.Skins;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraTab;

namespace Asa.Online.Controls.PresentationClasses.Products
{
	[ToolboxItem(false)]
	//public partial class DigitalProductControl : UserControl
	public partial class DigitalProductControl : XtraTabPage, IDigitalProductControl, IDigitalOutputItem
	{
		private readonly IDigitalProductsContainer _container;
		private bool _allowToSave;

		public DigitalProductControl(IDigitalProductsContainer container)
		{
			InitializeComponent();
			_container = container;
			Dock = DockStyle.Fill;

			layoutControlItemWebsiteLogo.Visibility =
				ListManager.Instance.DefaultControlsConfiguration.ProductEditorsSitesLogo != null
					? LayoutVisibility.Always
					: LayoutVisibility.Never;
			pictureEditWebsiteLogo.Image = ListManager.Instance.DefaultControlsConfiguration.ProductEditorsSitesLogo;
			layoutControlItemProductNameLogo.Visibility =
					ListManager.Instance.DefaultControlsConfiguration.ProductEditorsNameLogo != null
						? LayoutVisibility.Always
						: LayoutVisibility.Never;
			pictureEditProductNameLogo.Image = ListManager.Instance.DefaultControlsConfiguration.ProductEditorsNameLogo;
			layoutControlItemDescriptionLogo.Visibility =
					ListManager.Instance.DefaultControlsConfiguration.ProductEditorsDescriptionLogo != null
						? LayoutVisibility.Always
						: LayoutVisibility.Never;
			pictureEditDescriptionLogo.Image = ListManager.Instance.DefaultControlsConfiguration.ProductEditorsDescriptionLogo;
			layoutControlItemPriceLogo.Visibility =
					ListManager.Instance.DefaultControlsConfiguration.ProductEditorsPricingLogo != null
						? LayoutVisibility.Always
						: LayoutVisibility.Never;
			pictureEditPriceLogo.Image = ListManager.Instance.DefaultControlsConfiguration.ProductEditorsPricingLogo;
			layoutControlItemCommentsLogo.Visibility =
					ListManager.Instance.DefaultControlsConfiguration.ProductEditorsCommentsLogo != null
						? LayoutVisibility.Always
						: LayoutVisibility.Never;
			pictureEditCommentsLogo.Image = ListManager.Instance.DefaultControlsConfiguration.ProductEditorsCommentsLogo;

			simpleLabelItemWebsiteTitle.Text = String.Format("<image=WebsiteLogo><size=+2><color=#838383>{0}</color></size>", ListManager.Instance.DefaultControlsConfiguration.ProductEditorsSitesTitle ?? simpleLabelItemWebsiteTitle.Text);
			simpleLabelItemProductNameTitle.Text = String.Format("<image=ProductNameLogo><size=+2><color=#838383>{0}</color></size>", ListManager.Instance.DefaultControlsConfiguration.ProductEditorsNameTitle ?? simpleLabelItemProductNameTitle.Text);
			simpleLabelItemDescriptionTitle.Text = String.Format("<image=DescriptionLogo><size=+2><color=#838383>{0}</color></size>", ListManager.Instance.DefaultControlsConfiguration.ProductEditorsDescriptionTitle ?? simpleLabelItemDescriptionTitle.Text);
			simpleLabelItemPriceTitle.Text = String.Format("<image=PriceLogo><size=+2><color=#838383>{0}</color></size>", ListManager.Instance.DefaultControlsConfiguration.ProductEditorsPricingTitle ?? simpleLabelItemPriceTitle.Text);
			simpleLabelItemCommentsTitle.Text = String.Format("<image=CommentsLogo><size=+2><color=#838383>{0}</color></size>", ListManager.Instance.DefaultControlsConfiguration.ProductEditorsCommentsTitle ?? simpleLabelItemCommentsTitle.Text);

			spinEditImpressions.EnableSelectAll();
			spinEditImpressionsOnly.EnableSelectAll();
			spinEditInvestment.EnableSelectAll();
			spinEditInvestmentOnly.EnableSelectAll();
			spinEditCPM.EnableSelectAll();
			comboBoxEditStrengths1.DisableSelectAll();
			comboBoxEditStrengths2.DisableSelectAll();
			spinEditImpressions.EditValue = null;
			spinEditInvestment.EditValue = null;

			SummaryControl = new DigitalProductSummaryControl();

			var scaleFactor = Utilities.GetScaleFactor(CreateGraphics().DpiX);

			checkedListBoxControlWebsite.ItemHeight = (Int32)(checkedListBoxControlWebsite.ItemHeight * scaleFactor.Height);

			simpleLabelItemWebsiteTitle.MaxSize = RectangleHelper.ScaleSize(simpleLabelItemWebsiteTitle.MaxSize, scaleFactor);
			simpleLabelItemWebsiteTitle.MinSize = RectangleHelper.ScaleSize(simpleLabelItemWebsiteTitle.MinSize, scaleFactor);
			layoutControlItemWebsiteEditor.MaxSize = RectangleHelper.ScaleSize(layoutControlItemWebsiteEditor.MaxSize, scaleFactor);
			layoutControlItemWebsiteEditor.MinSize = RectangleHelper.ScaleSize(layoutControlItemWebsiteEditor.MinSize, scaleFactor);
			emptySpaceItemWebsites.MaxSize = RectangleHelper.ScaleSize(emptySpaceItemWebsites.MaxSize, scaleFactor);
			emptySpaceItemWebsites.MinSize = RectangleHelper.ScaleSize(emptySpaceItemWebsites.MinSize, scaleFactor);

			simpleLabelItemProductNameTitle.MaxSize = RectangleHelper.ScaleSize(simpleLabelItemProductNameTitle.MaxSize, scaleFactor);
			simpleLabelItemProductNameTitle.MinSize = RectangleHelper.ScaleSize(simpleLabelItemProductNameTitle.MinSize, scaleFactor);
			layoutControlItemProductNameReset.MaxSize = RectangleHelper.ScaleSize(layoutControlItemProductNameReset.MaxSize, scaleFactor);
			layoutControlItemProductNameReset.MinSize = RectangleHelper.ScaleSize(layoutControlItemProductNameReset.MinSize, scaleFactor);
			emptySpaceItemProductName.MaxSize = RectangleHelper.ScaleSize(emptySpaceItemProductName.MaxSize, scaleFactor);
			emptySpaceItemProductName.MinSize = RectangleHelper.ScaleSize(emptySpaceItemProductName.MinSize, scaleFactor);

			simpleLabelItemDescriptionTitle.MaxSize = RectangleHelper.ScaleSize(simpleLabelItemDescriptionTitle.MaxSize, scaleFactor);
			simpleLabelItemDescriptionTitle.MinSize = RectangleHelper.ScaleSize(simpleLabelItemDescriptionTitle.MinSize, scaleFactor);
			layoutControlItemDescriptionTargetingToggle.MaxSize = RectangleHelper.ScaleSize(layoutControlItemDescriptionTargetingToggle.MaxSize, scaleFactor);
			layoutControlItemDescriptionTargetingToggle.MinSize = RectangleHelper.ScaleSize(layoutControlItemDescriptionTargetingToggle.MinSize, scaleFactor);
			layoutControlItemDescriptionRichMediaToggle.MaxSize = RectangleHelper.ScaleSize(layoutControlItemDescriptionRichMediaToggle.MaxSize, scaleFactor);
			layoutControlItemDescriptionRichMediaToggle.MinSize = RectangleHelper.ScaleSize(layoutControlItemDescriptionRichMediaToggle.MinSize, scaleFactor);
			emptySpaceItemDescription.MaxSize = RectangleHelper.ScaleSize(emptySpaceItemDescription.MaxSize, scaleFactor);
			emptySpaceItemDescription.MinSize = RectangleHelper.ScaleSize(emptySpaceItemDescription.MinSize, scaleFactor);

			simpleLabelItemPriceTitle.MaxSize = RectangleHelper.ScaleSize(simpleLabelItemPriceTitle.MaxSize, scaleFactor);
			simpleLabelItemPriceTitle.MinSize = RectangleHelper.ScaleSize(simpleLabelItemPriceTitle.MinSize, scaleFactor);

			simpleLabelItemCommentsTitle.MaxSize = RectangleHelper.ScaleSize(simpleLabelItemCommentsTitle.MaxSize, scaleFactor);
			simpleLabelItemCommentsTitle.MinSize = RectangleHelper.ScaleSize(simpleLabelItemCommentsTitle.MinSize, scaleFactor);
			layoutControlItemCommentsStrengths1Toggle.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCommentsStrengths1Toggle.MaxSize, scaleFactor);
			layoutControlItemCommentsStrengths1Toggle.MinSize = RectangleHelper.ScaleSize(layoutControlItemCommentsStrengths1Toggle.MinSize, scaleFactor);
			layoutControlItemCommentsStrengths2Toggle.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCommentsStrengths2Toggle.MaxSize, scaleFactor);
			layoutControlItemCommentsStrengths2Toggle.MinSize = RectangleHelper.ScaleSize(layoutControlItemCommentsStrengths2Toggle.MinSize, scaleFactor);
			layoutControlItemCommentsToggle.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCommentsToggle.MaxSize, scaleFactor);
			layoutControlItemCommentsToggle.MinSize = RectangleHelper.ScaleSize(layoutControlItemCommentsToggle.MinSize, scaleFactor);
			layoutControlItemCommentsEditor.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCommentsEditor.MaxSize, scaleFactor);
			layoutControlItemCommentsEditor.MinSize = RectangleHelper.ScaleSize(layoutControlItemCommentsEditor.MinSize, scaleFactor);
			layoutControlItemCommentsTargetingToggle.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCommentsTargetingToggle.MaxSize, scaleFactor);
			layoutControlItemCommentsTargetingToggle.MinSize = RectangleHelper.ScaleSize(layoutControlItemCommentsTargetingToggle.MinSize, scaleFactor);
			layoutControlItemCommentsRichMediaToggle.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCommentsRichMediaToggle.MaxSize, scaleFactor);
			layoutControlItemCommentsRichMediaToggle.MinSize = RectangleHelper.ScaleSize(layoutControlItemCommentsRichMediaToggle.MinSize, scaleFactor);

			layoutControlItemReset.MaxSize = RectangleHelper.ScaleSize(layoutControlItemReset.MaxSize, scaleFactor);
			layoutControlItemReset.MinSize = RectangleHelper.ScaleSize(layoutControlItemReset.MinSize, scaleFactor);
		}

		public DigitalProduct Product { get; set; }
		public DigitalProductSummaryControl SummaryControl { get; private set; }

		private void UpdateFormula()
		{
			var imp = spinEditImpressions.EditValue as decimal?;
			var inv = spinEditInvestment.EditValue as decimal?;
			var cpm = spinEditCPM.EditValue as decimal?;

			switch (Product.Formula)
			{
				case FormulaType.CPM:
					if (Product.ShowAllPricingMonthly)
					{
						Product.MonthlyInvestment = inv;
						Product.MonthlyImpressions = imp;
						spinEditCPM.EditValue = Product.MonthlyCPMCalculated;
					}
					else if (Product.ShowAllPricingTotal)
					{
						Product.TotalInvestment = inv;
						Product.TotalImpressions = imp;
						spinEditCPM.EditValue = Product.TotalCPMCalculated;
					}
					break;
				case FormulaType.Investment:
					if (Product.ShowAllPricingMonthly)
					{
						Product.MonthlyImpressions = imp;
						Product.MonthlyCPM = cpm;
						spinEditInvestment.EditValue = Product.MonthlyInvestmentCalculated;
					}
					else if (Product.ShowAllPricingTotal)
					{
						Product.TotalImpressions = imp;
						Product.TotalCPM = cpm;
						spinEditInvestment.EditValue = Product.TotalInvestmentCalculated;
					}
					break;
				case FormulaType.Impressions:
					if (Product.ShowAllPricingMonthly)
					{
						Product.MonthlyInvestment = inv;
						Product.MonthlyCPM = cpm;
						spinEditImpressions.EditValue = Product.MonthlyImpressionsCalculated;
					}
					else if (Product.ShowAllPricingTotal)
					{
						Product.TotalInvestment = inv;
						Product.TotalCPM = cpm;
						spinEditImpressions.EditValue = Product.TotalImpressionsCalculated;
					}
					break;
			}
		}

		private void UpdateSinglePricing()
		{
			if (Product.ShowMonthlyInvestments || Product.ShowTotalInvestments)
			{
				Product.ShowMonthlyInvestments = comboBoxEditInvestmentType.SelectedIndex == 0;
				Product.ShowTotalInvestments = comboBoxEditInvestmentType.SelectedIndex == 1;
			}
			if (Product.ShowMonthlyImpressions || Product.ShowTotalImpressions)
			{
				Product.ShowMonthlyImpressions = comboBoxEditImpressionsType.SelectedIndex == 0;
				Product.ShowTotalImpressions = comboBoxEditImpressionsType.SelectedIndex == 1;
			}

			if (Product.ShowMonthlyInvestments)
			{
				Product.MonthlyInvestment = spinEditInvestmentOnly.EditValue as decimal?;
			}
			else if (Product.ShowTotalInvestments)
			{
				Product.TotalInvestment = spinEditInvestmentOnly.EditValue as decimal?;
			}
			else if (Product.ShowMonthlyImpressions)
			{
				Product.MonthlyImpressions = spinEditImpressionsOnly.EditValue as decimal?;
			}
			else if (Product.ShowTotalImpressions)
			{
				Product.TotalImpressions = spinEditImpressionsOnly.EditValue as decimal?;
			}
		}

		private void LoadLists()
		{
			checkedListBoxControlWebsite.Items.Clear();
			checkedListBoxControlWebsite.Items.AddRange(ListManager.Instance.Websites.ToArray());
			comboBoxEditStrengths1.Properties.Items.Clear();
			comboBoxEditStrengths1.Properties.Items.AddRange(ListManager.Instance.Strengths.ToArray());
			comboBoxEditStrengths2.Properties.Items.Clear();
			comboBoxEditStrengths2.Properties.Items.AddRange(ListManager.Instance.Strengths.ToArray());
		}

		public void LoadValues()
		{
			LoadLists();

			_allowToSave = false;

			Text = Product.Name;
			memoEditProductName.EditValue = !String.IsNullOrEmpty(Product.UserDefinedName) ? Product.UserDefinedName : Product.ExtendedName;

			switch (Product.Formula)
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

			comboBoxEditPriceType.SelectedIndex = -1;
			if (Product.ShowAllPricingMonthly)
				comboBoxEditPriceType.SelectedIndex = 0;
			else if (Product.ShowAllPricingTotal)
				comboBoxEditPriceType.SelectedIndex = 1;
			else if (Product.ShowMonthlyImpressions || Product.ShowTotalImpressions)
				comboBoxEditPriceType.SelectedIndex = 2;
			else if (Product.ShowMonthlyInvestments || Product.ShowTotalInvestments)
				comboBoxEditPriceType.SelectedIndex = 3;
			else
				comboBoxEditPriceType.SelectedIndex = 4;

			checkedListBoxControlWebsite.UnCheckAll();
			foreach (CheckedListBoxItem item in checkedListBoxControlWebsite.Items)
				if (Product.Websites.Contains(item.Value.ToString()))
					item.CheckState = CheckState.Checked;

			checkEditDescriptionManualEdit.Checked = Product.DescriptionManualEdit;
			buttonXShowDescriptionTargeting.Enabled = Product.TargetingAvailable;
			buttonXShowDescriptionTargeting.Checked = Product.ShowDescriptionTargeting && Product.TargetingAvailable;
			buttonXShowDescriptionRichMedia.Enabled = Product.RichMediaAvailable;
			buttonXShowDescriptionRichMedia.Checked = Product.ShowDescriptionRichMedia && Product.RichMediaAvailable;
			memoEditDescription.EditValue = Product.UserDescription;

			checkEditInvestmentDetails.Checked = !String.IsNullOrEmpty(Product.InvestmentDetails);
			textEditInvestmentDetails.EditValue = Product.InvestmentDetails;

			checkEditComments.Checked = Product.EnableComment;
			checkEditCommentManualEdit.Checked = Product.CommentManualEdit;
			buttonXShowCommentTargeting.Enabled = Product.EnableComment && Product.TargetingAvailable;
			buttonXShowCommentTargeting.Checked = Product.ShowCommentTargeting && Product.TargetingAvailable;
			buttonXShowCommentRichMedia.Enabled = Product.EnableComment && Product.RichMediaAvailable;
			buttonXShowCommentRichMedia.Checked = Product.ShowCommentRichMedia && Product.RichMediaAvailable;
			memoEditComments.EditValue = Product.Comment;
			checkEditStrengths1.Checked = !String.IsNullOrEmpty(Product.Strength1);
			comboBoxEditStrengths1.EditValue = Product.Strength1;
			checkEditStrengths2.Checked = !String.IsNullOrEmpty(Product.Strength2);
			comboBoxEditStrengths2.EditValue = Product.Strength2;

			layoutControlItemProductNameReset.Visibility = !String.Equals(memoEditProductName.EditValue as String, Product.ExtendedName, StringComparison.OrdinalIgnoreCase) ? LayoutVisibility.Always : LayoutVisibility.Never;
			_allowToSave = true;

			comboBoxEditPriceType_SelectedIndexChanged(comboBoxEditPriceType, EventArgs.Empty);

			SummaryControl.LoadData(Product);
		}

		private void UpdateFormulaComponents()
		{
			if (checkEditFormulaCPM.Checked)
			{
				Product.Formula = FormulaType.CPM;

				spinEditImpressions.Enabled = true;
				spinEditImpressions.Properties.AppearanceDisabled.Options.UseForeColor = false;
				spinEditInvestment.Enabled = true;
				spinEditInvestment.Properties.AppearanceDisabled.Options.UseForeColor = false;
				spinEditCPM.Enabled = false;
				spinEditCPM.Properties.AppearanceDisabled.ForeColor = Color.Black;
				spinEditCPM.Properties.AppearanceDisabled.Options.UseForeColor = true;
			}
			else if (checkEditFormulaInvestment.Checked)
			{
				Product.Formula = FormulaType.Investment;

				spinEditImpressions.Enabled = true;
				spinEditImpressions.Properties.AppearanceDisabled.Options.UseForeColor = false;
				spinEditInvestment.Properties.AppearanceDisabled.ForeColor = Color.Black;
				spinEditInvestment.Properties.AppearanceDisabled.Options.UseForeColor = true;
				spinEditInvestment.Enabled = false;
				spinEditCPM.Enabled = true;
				spinEditCPM.Properties.AppearanceDisabled.Options.UseForeColor = false;
			}
			else if (checkEditFormulaImpressions.Checked)
			{
				Product.Formula = FormulaType.Impressions;

				spinEditImpressions.Enabled = false;
				spinEditImpressions.Properties.AppearanceDisabled.ForeColor = Color.Black;
				spinEditImpressions.Properties.AppearanceDisabled.Options.UseForeColor = true;
				spinEditInvestment.Enabled = true;
				spinEditInvestment.Properties.AppearanceDisabled.Options.UseForeColor = false;
				spinEditCPM.Enabled = true;
				spinEditCPM.Properties.AppearanceDisabled.Options.UseForeColor = false;
			}
		}

		public void SaveValues()
		{
			if (!_allowToSave) return;
			Product.UserDefinedName = !String.Equals(memoEditProductName.EditValue as String, Product.ExtendedName, StringComparison.OrdinalIgnoreCase) ?
				memoEditProductName.EditValue as String :
				null;
			Product.Websites.Clear();
			foreach (CheckedListBoxItem item in checkedListBoxControlWebsite.Items)
				if (item.CheckState == CheckState.Checked)
					Product.Websites.Add(item.Value.ToString());
			Product.UserDescription = memoEditDescription.EditValue as String;
			Product.InvestmentDetails = checkEditInvestmentDetails.Checked ? textEditInvestmentDetails.EditValue as String : null;
			Product.EnableComment = checkEditComments.Checked;
			Product.Comment = checkEditComments.Checked && memoEditComments.EditValue != null ? memoEditComments.EditValue.ToString() : string.Empty;
			Product.Strength1 = checkEditStrengths1.Checked && comboBoxEditStrengths1.EditValue != null ? comboBoxEditStrengths1.EditValue.ToString() : string.Empty;
			Product.Strength2 = checkEditStrengths2.Checked && comboBoxEditStrengths2.EditValue != null ? comboBoxEditStrengths2.EditValue.ToString() : string.Empty;

			SummaryControl.LoadData(Product);
		}

		public void Release()
		{
			SummaryControl.Release();
			SummaryControl = null;

			Product = null;
		}

		private void comboBoxEditPriceType_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			_allowToSave = false;
			switch (comboBoxEditPriceType.SelectedIndex)
			{
				case 0:
					layoutControlGroupPriceTypeAll.Visibility = LayoutVisibility.Always;
					layoutControlGroupPriceTypeImpressions.Visibility = LayoutVisibility.Never;
					layoutControlGroupPriceTypeInvestment.Visibility = LayoutVisibility.Never;
					emptySpaceItemPriceType.Visibility = LayoutVisibility.Always;

					Product.ShowAllPricingMonthly = true;
					Product.ShowAllPricingTotal = false;
					Product.ShowMonthlyInvestments = false;
					Product.ShowMonthlyImpressions = false;
					Product.ShowTotalInvestments = false;
					Product.ShowTotalImpressions = false;

					spinEditImpressions.EditValue = Product.MonthlyImpressionsCalculated;
					spinEditInvestment.EditValue = Product.MonthlyInvestmentCalculated;
					spinEditCPM.EditValue = Product.MonthlyCPMCalculated;

					comboBoxEditInvestmentType.SelectedIndex = 0;
					spinEditInvestmentOnly.EditValue = null;
					comboBoxEditImpressionsType.SelectedIndex = 0;
					spinEditImpressionsOnly.EditValue = null;
					break;
				case 1:
					layoutControlGroupPriceTypeAll.Visibility = LayoutVisibility.Always;
					layoutControlGroupPriceTypeImpressions.Visibility = LayoutVisibility.Never;
					layoutControlGroupPriceTypeInvestment.Visibility = LayoutVisibility.Never;
					emptySpaceItemPriceType.Visibility = LayoutVisibility.Always;

					Product.ShowAllPricingMonthly = false;
					Product.ShowAllPricingTotal = true;
					Product.ShowMonthlyInvestments = false;
					Product.ShowMonthlyImpressions = false;
					Product.ShowTotalInvestments = false;
					Product.ShowTotalImpressions = false;

					spinEditImpressions.EditValue = Product.TotalImpressionsCalculated;
					spinEditInvestment.EditValue = Product.TotalInvestmentCalculated;
					spinEditCPM.EditValue = Product.TotalCPMCalculated;

					comboBoxEditInvestmentType.SelectedIndex = 0;
					spinEditInvestmentOnly.EditValue = null;
					comboBoxEditImpressionsType.SelectedIndex = 0;
					spinEditImpressionsOnly.EditValue = null;
					break;
				case 2:
					layoutControlGroupPriceTypeAll.Visibility = LayoutVisibility.Never;
					layoutControlGroupPriceTypeImpressions.Visibility = LayoutVisibility.Always;
					layoutControlGroupPriceTypeInvestment.Visibility = LayoutVisibility.Never;
					emptySpaceItemPriceType.Visibility = LayoutVisibility.Always;

					Product.ShowAllPricingMonthly = false;
					Product.ShowAllPricingTotal = false;
					Product.ShowMonthlyInvestments = false;
					Product.ShowTotalInvestments = false;
					if (!(Product.ShowMonthlyImpressions || Product.ShowTotalImpressions))
						Product.ShowMonthlyImpressions = true;

					spinEditImpressions.EditValue = null;
					spinEditInvestment.EditValue = null;
					spinEditCPM.EditValue = null;

					comboBoxEditInvestmentType.SelectedIndex = 0;
					spinEditInvestmentOnly.EditValue = null;
					comboBoxEditImpressionsType.SelectedIndex = Product.ShowTotalImpressions ? 1 : 0; ;
					spinEditImpressionsOnly.EditValue = Product.ShowTotalImpressions ? Product.TotalImpressions : Product.MonthlyImpressions;

					UpdateSinglePricing();
					break;
				case 3:
					layoutControlGroupPriceTypeAll.Visibility = LayoutVisibility.Never;
					layoutControlGroupPriceTypeImpressions.Visibility = LayoutVisibility.Never;
					layoutControlGroupPriceTypeInvestment.Visibility = LayoutVisibility.Always;
					emptySpaceItemPriceType.Visibility = LayoutVisibility.Always;

					Product.ShowAllPricingMonthly = false;
					Product.ShowAllPricingTotal = false;
					Product.ShowMonthlyImpressions = false;
					Product.ShowTotalImpressions = false;
					if (!(Product.ShowMonthlyInvestments || Product.ShowTotalInvestments))
						Product.ShowMonthlyInvestments = true;

					spinEditImpressions.EditValue = null;
					spinEditInvestment.EditValue = null;
					spinEditCPM.EditValue = null;

					comboBoxEditInvestmentType.SelectedIndex = Product.ShowTotalInvestments ? 1 : 0;
					spinEditInvestmentOnly.EditValue = Product.ShowTotalInvestments ? Product.TotalInvestment : Product.MonthlyInvestment;
					comboBoxEditImpressionsType.SelectedIndex = 0;
					spinEditImpressionsOnly.EditValue = null;

					UpdateSinglePricing();
					break;
				default:
					layoutControlGroupPriceTypeAll.Visibility = LayoutVisibility.Never;
					layoutControlGroupPriceTypeImpressions.Visibility = LayoutVisibility.Never;
					layoutControlGroupPriceTypeInvestment.Visibility = LayoutVisibility.Never;
					emptySpaceItemPriceType.Visibility = LayoutVisibility.Never;

					Product.ShowAllPricingMonthly = false;
					Product.ShowAllPricingTotal = false;
					Product.ShowMonthlyInvestments = false;
					Product.ShowMonthlyImpressions = false;
					Product.ShowTotalInvestments = false;
					Product.ShowTotalImpressions = false;

					spinEditImpressions.EditValue = null;
					spinEditInvestment.EditValue = null;
					spinEditCPM.EditValue = Product.Formula != FormulaType.CPM ? Product.DefaultRate : null;

					comboBoxEditInvestmentType.SelectedIndex = 0;
					spinEditInvestmentOnly.EditValue = null;
					comboBoxEditImpressionsType.SelectedIndex = 0;
					spinEditImpressionsOnly.EditValue = null;
					break;
			}
			_allowToSave = true;
			_container.RaiseDataChanged();
		}

		private void checkEditFormula_CheckedChanged(object sender, EventArgs e)
		{
			UpdateFormulaComponents();
			if (!_allowToSave) return;
			UpdateFormula();
			_container.RaiseDataChanged();
		}

		private void checkEditInvestmentDetails_CheckedChanged(object sender, EventArgs e)
		{
			textEditInvestmentDetails.Enabled = checkEditInvestmentDetails.Checked;
			if (!checkEditInvestmentDetails.Checked)
				textEditInvestmentDetails.EditValue = null;
			if (_allowToSave)
				_container.RaiseDataChanged();
		}

		private void spinEditPricing_EditValueChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			UpdateFormula();
			_container.RaiseDataChanged();
		}

		private void SinglePricing_EditValueChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			UpdateSinglePricing();
			_container.RaiseDataChanged();
		}

		private void checkEditStrengths1_CheckedChanged(object sender, EventArgs e)
		{
			comboBoxEditStrengths1.Enabled = checkEditStrengths1.Checked;
			_container.RaiseDataChanged();
		}

		private void checkEditStrengths2_CheckedChanged(object sender, EventArgs e)
		{
			comboBoxEditStrengths2.Enabled = checkEditStrengths2.Checked;
			_container.RaiseDataChanged();
		}

		private void checkEditComments_CheckedChanged(object sender, EventArgs e)
		{
			memoEditComments.Enabled = checkEditComments.Checked;
			checkEditCommentManualEdit.Enabled = checkEditComments.Checked;
			if (!checkEditComments.Checked)
				checkEditCommentManualEdit.Checked = false;
			buttonXShowCommentTargeting.Enabled = checkEditComments.Checked && Product.TargetingAvailable;
			buttonXShowCommentRichMedia.Enabled = checkEditComments.Checked && Product.RichMediaAvailable;
			if (!_allowToSave) return;
			_container.RaiseDataChanged();
		}

		private void Edit_EditValueChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			_container.RaiseDataChanged();
		}

		private void checkedListBoxControlWebsite_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
		{
			_container.RaiseDataChanged();
		}

		private void memoEditProductName_EditValueChanged(object sender, EventArgs e)
		{
			var userDefinedName = memoEditProductName.EditValue as String;
			if (String.Equals(userDefinedName, Product.ExtendedName, StringComparison.OrdinalIgnoreCase))
			{
				memoEditProductName.ForeColor = Color.Black;
				layoutControlItemProductNameReset.Visibility = LayoutVisibility.Never;
			}
			else
			{
				memoEditProductName.ForeColor = Color.Green;
				layoutControlItemProductNameReset.Visibility = LayoutVisibility.Always;
			}
			if (_allowToSave)
				_container.RaiseDataChanged();
		}

		private void checkEditDescriptionManualEdit_CheckedChanged(object sender, EventArgs e)
		{
			memoEditDescription.Properties.ReadOnly = !checkEditDescriptionManualEdit.Checked;
			buttonXShowDescriptionTargeting.Enabled = !checkEditDescriptionManualEdit.Checked && Product.TargetingAvailable;
			buttonXShowDescriptionRichMedia.Enabled = !checkEditDescriptionManualEdit.Checked && Product.RichMediaAvailable;
			if (!_allowToSave) return;
			Product.DescriptionManualEdit = checkEditDescriptionManualEdit.Checked;
			Product.UserDescription = null;
			memoEditDescription.EditValue = Product.UserDescription;
			_container.RaiseDataChanged();
		}

		private void buttonXShowDescriptionAdditionalInfo_CheckedChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			Product.ShowDescriptionTargeting = buttonXShowDescriptionTargeting.Checked;
			Product.ShowDescriptionRichMedia = buttonXShowDescriptionRichMedia.Checked;
			memoEditDescription.EditValue = Product.UserDescription;
			_container.RaiseDataChanged();
		}

		private void checkEditCommentManualEdit_CheckedChanged(object sender, EventArgs e)
		{
			memoEditComments.Properties.ReadOnly = !checkEditCommentManualEdit.Checked;
			buttonXShowCommentTargeting.Enabled = !checkEditCommentManualEdit.Checked && checkEditComments.Checked && Product.TargetingAvailable;
			buttonXShowCommentRichMedia.Enabled = !checkEditCommentManualEdit.Checked && checkEditComments.Checked && Product.RichMediaAvailable;
			if (!_allowToSave) return;
			Product.CommentManualEdit = checkEditCommentManualEdit.Checked;
			Product.Comment = null;
			memoEditComments.EditValue = Product.Comment;
			_container.RaiseDataChanged();
		}

		private void buttonXShowCommentAdditonalInfo_CheckedChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			Product.ShowCommentTargeting = buttonXShowCommentTargeting.Checked;
			Product.ShowCommentRichMedia = buttonXShowCommentRichMedia.Checked;
			memoEditComments.EditValue = Product.Comment;
			_container.RaiseDataChanged();
		}

		private void hyperLinkEditReset_OpenLink(object sender, OpenLinkEventArgs e)
		{
			if (PopupMessageHelper.Instance.ShowWarningQuestion(
				String.Format("Are you SURE you want to Wipe Everything{0}and reset the data for this Digital Product ? ", Environment.NewLine))
				!= DialogResult.Yes) return;
			Product.ApplyDefaultView();
			ResetProductName(this, new OpenLinkEventArgs(String.Empty));
			LoadValues();
			_container.LoadProduct(this);
			_container.RaiseDataChanged();
			e.Handled = true;
		}

		public void ResetProductName(object sender, OpenLinkEventArgs e)
		{
			e.Handled = true;
			if (PopupMessageHelper.Instance.ShowWarningQuestion(
				String.Format("Are you SURE you want to Wipe Everything{0}and reset the data for this Digital Product ? ", Environment.NewLine))
				!= DialogResult.Yes) return;
			Product.UserDefinedName = null;
			memoEditProductName.EditValue = Product.ExtendedName;
		}

		#region Output Staff
		public string SlideName => Product.Name;
		public int SlidesCount => 1;

		public void GenerateOutput()
		{
			_container.PowerPointProcessor.AppendOneSheets(new[] { Product }, _container.SelectedTheme);
		}

		public PreviewGroup GeneratePreview()
		{
			var previewGroup = new PreviewGroup
			{
				Name = Product.Name,
				PresentationSourcePath = Path.Combine(ResourceManager.Instance.TempFolder.LocalPath, Path.GetFileName(Path.GetTempFileName()))
			};
			_container.PowerPointProcessor.PrepareScheduleEmail(previewGroup.PresentationSourcePath, Product, _container.SelectedTheme);
			return previewGroup;
		}
		#endregion
	}
}
