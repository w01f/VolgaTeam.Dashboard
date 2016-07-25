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
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
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
			if ((base.CreateGraphics()).DpiX > 96)
			{
				var font = new Font(styleController.Appearance.Font.FontFamily, styleController.Appearance.Font.Size - 2, styleController.Appearance.Font.Style);
				styleController.Appearance.Font = font;
				styleController.AppearanceDisabled.Font = font;
				styleController.AppearanceDropDown.Font = font;
				styleController.AppearanceDropDownHeader.Font = font;
				styleController.AppearanceFocused.Font = font;
				styleController.AppearanceReadOnly.Font = font;
				font = new Font(labelControlWebsite.Font.FontFamily, labelControlWebsite.Font.Size - 3, labelControlWebsite.Font.Style);
				labelControlWebsite.Font = font;
				labelControlProduct.Font = font;
				labelControlDescription.Font = font;
				labelControlPriceType.Font = font;
				labelControlComments.Font = font;
				checkEditFormulaCPM.Font = new Font(checkEditFormulaCPM.Font.FontFamily, checkEditFormulaCPM.Font.Size - 2, checkEditFormulaCPM.Font.Style);
				checkEditFormulaImpressions.Font = new Font(checkEditFormulaImpressions.Font.FontFamily, checkEditFormulaImpressions.Font.Size - 2, checkEditFormulaImpressions.Font.Style);
				checkEditFormulaInvestment.Font = new Font(checkEditFormulaInvestment.Font.FontFamily, checkEditFormulaInvestment.Font.Size - 2, checkEditFormulaInvestment.Font.Style);
				hyperLinkEditResetProductName.Font = new Font(hyperLinkEditResetProductName.Font.FontFamily, hyperLinkEditResetProductName.Font.Size - 2, hyperLinkEditResetProductName.Font.Style);
				hyperLinkEditReset.Font = new Font(hyperLinkEditReset.Font.FontFamily, hyperLinkEditReset.Font.Size - 2, hyperLinkEditReset.Font.Style);
			}

			labelControlWebsite.Appearance.Image = ListManager.Instance.DefaultControlsConfiguration.ProductEditorsSitesLogo;
			labelControlWebsite.Text = String.Format("<color=#838383>{0}</color>", ListManager.Instance.DefaultControlsConfiguration.ProductEditorsSitesTitle ?? labelControlWebsite.Text);
			labelControlProduct.Appearance.Image = ListManager.Instance.DefaultControlsConfiguration.ProductEditorsNameLogo;
			labelControlProduct.Text = String.Format("<color=#838383>{0}</color>", ListManager.Instance.DefaultControlsConfiguration.ProductEditorsNameTitle ?? labelControlProduct.Text);
			labelControlDescription.Appearance.Image = ListManager.Instance.DefaultControlsConfiguration.ProductEditorsDescriptionLogo;
			labelControlDescription.Text = String.Format("<color=#838383>{0}</color>", ListManager.Instance.DefaultControlsConfiguration.ProductEditorsDescriptionTitle ?? labelControlDescription.Text);
			labelControlPriceType.Appearance.Image = ListManager.Instance.DefaultControlsConfiguration.ProductEditorsPricingLogo;
			labelControlPriceType.Text = String.Format("<color=#838383>{0}</color>", ListManager.Instance.DefaultControlsConfiguration.ProductEditorsPricingTitle ?? labelControlPriceType.Text);
			labelControlComments.Appearance.Image = ListManager.Instance.DefaultControlsConfiguration.ProductEditorsCommentsLogo;
			labelControlComments.Text = String.Format("<color=#838383>{0}</color>", ListManager.Instance.DefaultControlsConfiguration.ProductEditorsCommentsTitle ?? labelControlComments.Text);

			spinEditImpressions.EnableSelectAll();
			spinEditImpressionsOnly.EnableSelectAll();
			spinEditInvestment.EnableSelectAll();
			spinEditInvestmentOnly.EnableSelectAll();
			spinEditCPM.EnableSelectAll();
			comboBoxEditStrengths1.DisableSelectAll();
			comboBoxEditStrengths2.DisableSelectAll();
			spinEditImpressions.EditValue = null;
			spinEditInvestment.EditValue = null;

			AssignCloseActiveEditorsonOutSideClick(this);

			SummaryControl = new DigitalProductSummaryControl();
		}

		public DigitalProduct Product { get; set; }
		public DigitalProductSummaryControl SummaryControl { get; private set; }

		private void AssignCloseActiveEditorsonOutSideClick(Control control)
		{
			if (control.GetType() != typeof(TextEdit) &&
				control.GetType() != typeof(MemoEdit) &&
				control.GetType() != typeof(ComboBoxEdit) &&
				control.GetType() != typeof(LookUpEdit) &&
				control.GetType() != typeof(DateEdit) &&
				control.GetType() != typeof(CheckedListBoxControl) &&
				control.GetType() != typeof(SpinEdit) &&
				control.GetType() != typeof(CheckEdit))
			{
				control.Click += CloseActiveEditorsOnOutSideClick;
				foreach (Control childControl in control.Controls)
					AssignCloseActiveEditorsonOutSideClick(childControl);
			}
		}

		private void CloseActiveEditorsOnOutSideClick(object sender, EventArgs e)
		{
			labelControlWebsite.Focus();
		}

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

			hyperLinkEditResetProductName.Visible = !String.Equals(memoEditProductName.EditValue as String, Product.ExtendedName, StringComparison.OrdinalIgnoreCase);
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
					pnPricyTypeAll.Visible = true;
					checkEditFormulaImpressions.Visible = true;
					checkEditFormulaInvestment.Visible = true;
					checkEditFormulaCPM.Visible = true;
					spinEditImpressions.Visible = true;
					spinEditInvestment.Visible = true;
					spinEditCPM.Visible = true;

					pnPriceTypeImpressions.Visible = false;
					pnPriceTypeInvestment.Visible = false;

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
					pnPricyTypeAll.Visible = true;
					checkEditFormulaImpressions.Visible = true;
					checkEditFormulaInvestment.Visible = true;
					checkEditFormulaCPM.Visible = true;
					spinEditImpressions.Visible = true;
					spinEditInvestment.Visible = true;
					spinEditCPM.Visible = true;

					pnPriceTypeImpressions.Visible = false;
					pnPriceTypeInvestment.Visible = false;

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
					pnPricyTypeAll.Visible = false;
					pnPriceTypeImpressions.Visible = true;
					pnPriceTypeInvestment.Visible = false;

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
					pnPricyTypeAll.Visible = false;
					pnPriceTypeImpressions.Visible = false;
					pnPriceTypeInvestment.Visible = true;

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
					pnPricyTypeAll.Visible = false;
					pnPriceTypeImpressions.Visible = false;
					pnPriceTypeInvestment.Visible = false;

					checkEditFormulaImpressions.Visible = false;
					checkEditFormulaInvestment.Visible = false;
					checkEditFormulaCPM.Visible = false;
					spinEditImpressions.Visible = false;
					spinEditInvestment.Visible = false;
					spinEditCPM.Visible = false;

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
				hyperLinkEditResetProductName.Visible = false;
			}
			else
			{
				memoEditProductName.ForeColor = Color.Green;
				hyperLinkEditResetProductName.Visible = true;
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

		public void GenerateOutput()
		{
			OnlineSchedulePowerPointHelper.Instance.AppendOneSheets(new[] { Product }, _container.SelectedTheme);
		}

		public PreviewGroup GeneratePreview()
		{
			var previewGroup = new PreviewGroup
			{
				Name = Product.Name,
				PresentationSourcePath = Path.Combine(ResourceManager.Instance.TempFolder.LocalPath, Path.GetFileName(Path.GetTempFileName()))
			};
			OnlineSchedulePowerPointHelper.Instance.PrepareScheduleEmail(previewGroup.PresentationSourcePath, Product, _container.SelectedTheme);
			return previewGroup;
		}
		#endregion
	}
}
