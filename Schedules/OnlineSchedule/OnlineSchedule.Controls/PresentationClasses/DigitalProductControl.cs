using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraTab;
using NewBizWiz.CommonGUI.Preview;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.OnlineSchedule;
using NewBizWiz.OnlineSchedule.Controls.InteropClasses;
using ItemCheckEventArgs = DevExpress.XtraEditors.Controls.ItemCheckEventArgs;
using ListManager = NewBizWiz.Core.OnlineSchedule.ListManager;
using SettingsManager = NewBizWiz.Core.Common.SettingsManager;

namespace NewBizWiz.OnlineSchedule.Controls.PresentationClasses
{
	[ToolboxItem(false)]
	//public partial class DigitalProductControl : UserControl
	public partial class DigitalProductControl : XtraTabPage
	{
		private readonly DigitalProductContainer _container;
		private bool _allowToSave;

		public DigitalProductControl(DigitalProductContainer container)
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
			spinEditImpressions.Enter += Utilities.Instance.Editor_Enter;
			spinEditImpressions.MouseDown += Utilities.Instance.Editor_MouseDown;
			spinEditImpressions.MouseUp += Utilities.Instance.Editor_MouseUp;
			spinEditImpressionsOnly.Enter += Utilities.Instance.Editor_Enter;
			spinEditImpressionsOnly.MouseDown += Utilities.Instance.Editor_MouseDown;
			spinEditImpressionsOnly.MouseUp += Utilities.Instance.Editor_MouseUp;
			spinEditInvestment.Enter += Utilities.Instance.Editor_Enter;
			spinEditInvestment.MouseDown += Utilities.Instance.Editor_MouseDown;
			spinEditInvestment.MouseUp += Utilities.Instance.Editor_MouseUp;
			spinEditInvestmentOnly.Enter += Utilities.Instance.Editor_Enter;
			spinEditInvestmentOnly.MouseDown += Utilities.Instance.Editor_MouseDown;
			spinEditInvestmentOnly.MouseUp += Utilities.Instance.Editor_MouseUp;
			spinEditCPM.Enter += Utilities.Instance.Editor_Enter;
			spinEditCPM.MouseDown += Utilities.Instance.Editor_MouseDown;
			spinEditCPM.MouseUp += Utilities.Instance.Editor_MouseUp;
			memoEditProductName.Enter += Utilities.Instance.Editor_Enter;
			memoEditProductName.MouseDown += Utilities.Instance.Editor_MouseDown;
			memoEditProductName.MouseUp += Utilities.Instance.Editor_MouseUp;
			memoEditComments.Enter += Utilities.Instance.Editor_Enter;
			memoEditComments.MouseDown += Utilities.Instance.Editor_MouseDown;
			memoEditComments.MouseUp += Utilities.Instance.Editor_MouseUp;
			memoEditDescription.Enter += Utilities.Instance.Editor_Enter;
			memoEditDescription.MouseDown += Utilities.Instance.Editor_MouseDown;
			memoEditDescription.MouseUp += Utilities.Instance.Editor_MouseUp;

			spinEditImpressions.EditValue = null;
			spinEditInvestment.EditValue = null;

			AssignCloseActiveEditorsonOutSideClick(this);
		}

		public DigitalProduct Product { get; set; }

		private void AssignCloseActiveEditorsonOutSideClick(Control control)
		{
			if (control.GetType() != typeof(TextEdit) && control.GetType() != typeof(MemoEdit) && control.GetType() != typeof(ComboBoxEdit) && control.GetType() != typeof(LookUpEdit) && control.GetType() != typeof(DateEdit) && control.GetType() != typeof(CheckedListBoxControl) && control.GetType() != typeof(SpinEdit) && control.GetType() != typeof(CheckEdit))
			{
				foreach (Control childControl in control.Controls)
					AssignCloseActiveEditorsonOutSideClick(childControl);
			}
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

			Text = Product.Name.Replace("&", "&&");
			memoEditProductName.EditValue = Product.UserDefinedName;

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

			if (Product.Width.HasValue && Product.Height.HasValue && Product.ShowDimensions)
				memoEditDescription.EditValue = String.Format("(Ad Dimensions: {0}{3}) {1}{1}{2}", Product.Dimensions, Environment.NewLine, Product.Description, !String.IsNullOrEmpty(Product.Location) && !Product.Location.Equals("N/A") ? String.Format(" Location - {0}", Product.Location) : String.Empty);
			else
				memoEditDescription.EditValue = Product.Description;

			checkEditInvestmentDetails.Checked = !String.IsNullOrEmpty(Product.InvestmentDetails);
			textEditInvestmentDetails.EditValue = Product.InvestmentDetails;

			checkEditComments.Checked = !String.IsNullOrEmpty(Product.Comment);
			memoEditComments.EditValue = Product.Comment;
			checkEditStrengths1.Checked = !String.IsNullOrEmpty(Product.Strength1);
			comboBoxEditStrengths1.EditValue = Product.Strength1;
			checkEditStrengths2.Checked = !String.IsNullOrEmpty(Product.Strength2);
			comboBoxEditStrengths2.EditValue = Product.Strength2;

			hyperLinkEditResetProductName.Visible = !(memoEditProductName.EditValue != null && memoEditProductName.EditValue.ToString().Equals(Product.ExtendedName));
			_allowToSave = true;

			comboBoxEditPriceType_SelectedIndexChanged(comboBoxEditPriceType, EventArgs.Empty);
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
			Product.UserDefinedName = memoEditProductName.EditValue != null ? memoEditProductName.EditValue.ToString() : Product.ExtendedName;
			Product.Websites.Clear();
			foreach (CheckedListBoxItem item in checkedListBoxControlWebsite.Items)
				if (item.CheckState == CheckState.Checked)
					Product.Websites.Add(item.Value.ToString());

			var dimensionsMatch = new Regex(@"\(Ad Dimensions:[\s\w]*\)");
			var description = memoEditDescription.EditValue != null ? memoEditDescription.EditValue.ToString() : String.Empty;
			Product.ShowDimensions = dimensionsMatch.IsMatch(description);
			Product.Description = dimensionsMatch.IsMatch(description) ?
				dimensionsMatch.Replace(description, String.Empty).Trim() :
				description.Trim();
			Product.InvestmentDetails = checkEditInvestmentDetails.Checked ? textEditInvestmentDetails.EditValue as String : null;
			Product.Comment = checkEditComments.Checked && memoEditComments.EditValue != null ? memoEditComments.EditValue.ToString() : string.Empty;
			Product.Strength1 = checkEditStrengths1.Checked && comboBoxEditStrengths1.EditValue != null ? comboBoxEditStrengths1.EditValue.ToString() : string.Empty;
			Product.Strength2 = checkEditStrengths2.Checked && comboBoxEditStrengths2.EditValue != null ? comboBoxEditStrengths2.EditValue.ToString() : string.Empty;
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
			_container.SettingsNotSaved = true;
		}

		private void checkEditFormula_CheckedChanged(object sender, EventArgs e)
		{
			UpdateFormulaComponents();
			if (!_allowToSave) return;
			UpdateFormula();
			_container.SettingsNotSaved = true;
		}

		private void checkEditInvestmentDetails_CheckedChanged(object sender, EventArgs e)
		{
			textEditInvestmentDetails.Enabled = checkEditInvestmentDetails.Checked;
			if (!checkEditInvestmentDetails.Checked)
				textEditInvestmentDetails.EditValue = null;
			if (_allowToSave)
				_container.SettingsNotSaved = true;
		}

		private void spinEditPricing_EditValueChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			UpdateFormula();
			_container.SettingsNotSaved = true;
		}

		private void SinglePricing_EditValueChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			UpdateSinglePricing();
			_container.SettingsNotSaved = true;
		}

		private void checkEditStrengths1_CheckedChanged(object sender, EventArgs e)
		{
			comboBoxEditStrengths1.Enabled = checkEditStrengths1.Checked;
			_container.SettingsNotSaved = true;
		}

		private void checkEditStrengths2_CheckedChanged(object sender, EventArgs e)
		{
			comboBoxEditStrengths2.Enabled = checkEditStrengths2.Checked;
			_container.SettingsNotSaved = true;
		}

		private void checkEditComments_CheckedChanged(object sender, EventArgs e)
		{
			memoEditComments.Enabled = checkEditComments.Checked;
			_container.SettingsNotSaved = true;
		}

		private void Edit_EditValueChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			_container.SettingsNotSaved = true;
		}

		private void checkedListBoxControlWebsite_ItemCheck(object sender, ItemCheckEventArgs e)
		{
			_container.SettingsNotSaved = true;
		}

		private void memoEditProductName_EditValueChanged(object sender, EventArgs e)
		{
			var userDefinedName = memoEditProductName.EditValue != null ? memoEditProductName.EditValue.ToString() : null;
			if (Product.ExtendedName.Equals(userDefinedName))
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
				_container.SettingsNotSaved = true;
		}

		private void hyperLinkEditReset_OpenLink(object sender, OpenLinkEventArgs e)
		{
			Product.ApplyDefaultView();
			ResetProductName(this, new OpenLinkEventArgs(String.Empty));
			LoadValues();
			_container.LoadProduct(this);
			_container.SettingsNotSaved = true;
			e.Handled = true;

		}

		public void ResetProductName(object sender, OpenLinkEventArgs e)
		{
			e.Handled = true;
			memoEditProductName.EditValue = Product.ExtendedName;
		}

		#region Output Staff
		public PreviewGroup GetPreviewGroup()
		{
			return new PreviewGroup
			{
				Name = Product.Name,
				PresentationSourcePath = Path.Combine(SettingsManager.Instance.TempPath, Path.GetFileName(Path.GetTempFileName()))
			};
		}

		public void Output()
		{
			OnlineSchedulePowerPointHelper.Instance.AppendOneSheet(new[] { Product }, _container.SelectedTheme);
		}
		#endregion
	}
}
