using System;
using System.ComponentModel;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraTab;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.OnlineSchedule;
using NewBizWiz.OnlineSchedule.Controls.InteropClasses;
using ItemCheckEventArgs = DevExpress.XtraEditors.Controls.ItemCheckEventArgs;
using ListManager = NewBizWiz.Core.OnlineSchedule.ListManager;

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
				labelControlMonthlyImpressions.Font = font;
				labelControlTotalImpressions.Font = font;
				labelControlComments.Font = font;
				font = new Font(labelControlTotalAds.Font.FontFamily, labelControlTotalAds.Font.Size - 2, labelControlTotalAds.Font.Style);
				labelControlActiveDays.Font = font;
				labelControlAdRate.Font = font;
				labelControlTotalAds.Font = font;
				labelControlOther.Font = font;
				checkEditMonthlyFormulaCPM.Font = new Font(checkEditMonthlyFormulaCPM.Font.FontFamily, checkEditMonthlyFormulaCPM.Font.Size - 2, checkEditMonthlyFormulaCPM.Font.Style);
				checkEditMonthlyFormulaImpressions.Font = new Font(checkEditMonthlyFormulaImpressions.Font.FontFamily, checkEditMonthlyFormulaImpressions.Font.Size - 2, checkEditMonthlyFormulaImpressions.Font.Style);
				checkEditMonthlyFormulaInvestment.Font = new Font(checkEditMonthlyFormulaInvestment.Font.FontFamily, checkEditMonthlyFormulaInvestment.Font.Size - 2, checkEditMonthlyFormulaInvestment.Font.Style);
				checkEditTotalFormulaCPM.Font = new Font(checkEditTotalFormulaCPM.Font.FontFamily, checkEditTotalFormulaCPM.Font.Size - 2, checkEditTotalFormulaCPM.Font.Style);
				checkEditTotalFormulaImpressions.Font = new Font(checkEditTotalFormulaImpressions.Font.FontFamily, checkEditTotalFormulaImpressions.Font.Size - 2, checkEditTotalFormulaImpressions.Font.Style);
				checkEditTotalFormulaInvestment.Font = new Font(checkEditTotalFormulaInvestment.Font.FontFamily, checkEditTotalFormulaInvestment.Font.Size - 2, checkEditTotalFormulaInvestment.Font.Style);
				hyperLinkEditResetProductName.Font = new Font(hyperLinkEditResetProductName.Font.FontFamily, hyperLinkEditResetProductName.Font.Size - 2, hyperLinkEditResetProductName.Font.Style);
				hyperLinkEditReset.Font = new Font(hyperLinkEditReset.Font.FontFamily, hyperLinkEditReset.Font.Size - 2, hyperLinkEditReset.Font.Style);
			}
			spinEditActiveDays.Enter += Utilities.Instance.Editor_Enter;
			spinEditActiveDays.MouseDown += Utilities.Instance.Editor_MouseDown;
			spinEditActiveDays.MouseUp += Utilities.Instance.Editor_MouseUp;
			spinEditAdRate.Enter += Utilities.Instance.Editor_Enter;
			spinEditAdRate.MouseDown += Utilities.Instance.Editor_MouseDown;
			spinEditAdRate.MouseUp += Utilities.Instance.Editor_MouseUp;

			spinEditMonthlyImpressions.Enter += Utilities.Instance.Editor_Enter;
			spinEditMonthlyImpressions.MouseDown += Utilities.Instance.Editor_MouseDown;
			spinEditMonthlyImpressions.MouseUp += Utilities.Instance.Editor_MouseUp;
			spinEditMonthlyInvestment.Enter += Utilities.Instance.Editor_Enter;
			spinEditMonthlyInvestment.MouseDown += Utilities.Instance.Editor_MouseDown;
			spinEditMonthlyInvestment.MouseUp += Utilities.Instance.Editor_MouseUp;
			spinEditMonthlyCPM.Enter += Utilities.Instance.Editor_Enter;
			spinEditMonthlyCPM.MouseDown += Utilities.Instance.Editor_MouseDown;
			spinEditMonthlyCPM.MouseUp += Utilities.Instance.Editor_MouseUp;
			spinEditTotalAds.Enter += Utilities.Instance.Editor_Enter;
			spinEditTotalAds.MouseDown += Utilities.Instance.Editor_MouseDown;
			spinEditTotalAds.MouseUp += Utilities.Instance.Editor_MouseUp;
			spinEditTotalImpressions.Enter += Utilities.Instance.Editor_Enter;
			spinEditTotalImpressions.MouseDown += Utilities.Instance.Editor_MouseDown;
			spinEditTotalImpressions.MouseUp += Utilities.Instance.Editor_MouseUp;
			spinEditTotalInvestment.Enter += Utilities.Instance.Editor_Enter;
			spinEditTotalInvestment.MouseDown += Utilities.Instance.Editor_MouseDown;
			spinEditTotalInvestment.MouseUp += Utilities.Instance.Editor_MouseUp;
			spinEditTotalCPM.Enter += Utilities.Instance.Editor_Enter;
			spinEditTotalCPM.MouseDown += Utilities.Instance.Editor_MouseDown;
			spinEditTotalCPM.MouseUp += Utilities.Instance.Editor_MouseUp;
			memoEditProductName.Enter += Utilities.Instance.Editor_Enter;
			memoEditProductName.MouseDown += Utilities.Instance.Editor_MouseDown;
			memoEditProductName.MouseUp += Utilities.Instance.Editor_MouseUp;
			memoEditComments.Enter += Utilities.Instance.Editor_Enter;
			memoEditComments.MouseDown += Utilities.Instance.Editor_MouseDown;
			memoEditComments.MouseUp += Utilities.Instance.Editor_MouseUp;
			memoEditDescription.Enter += Utilities.Instance.Editor_Enter;
			memoEditDescription.MouseDown += Utilities.Instance.Editor_MouseDown;
			memoEditDescription.MouseUp += Utilities.Instance.Editor_MouseUp;

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

		private void UpdateMonthlyFormula()
		{
			switch (Product.Formula)
			{
				case FormulaType.CPM:
					Product.MonthlyInvestment = spinEditMonthlyInvestment.EditValue != null ? (double?)spinEditMonthlyInvestment.Value : null;
					Product.MonthlyImpressions = spinEditMonthlyImpressions.EditValue != null ? (double?)spinEditMonthlyImpressions.Value : null;

					spinEditMonthlyCPM.EditValue = Product.MonthlyCPMCalculated;
					break;
				case FormulaType.Investment:
					Product.MonthlyImpressions = spinEditMonthlyImpressions.EditValue != null ? (double?)spinEditMonthlyImpressions.Value : null;
					Product.MonthlyCPM = spinEditMonthlyCPM.EditValue != null ? (double?)spinEditMonthlyCPM.Value : null;

					spinEditMonthlyInvestment.EditValue = Product.MonthlyInvestmentCalculated;
					break;
				case FormulaType.Impressions:
					Product.MonthlyInvestment = spinEditMonthlyInvestment.EditValue != null ? (double?)spinEditMonthlyInvestment.Value : null;
					Product.MonthlyCPM = spinEditMonthlyCPM.EditValue != null ? (double?)spinEditMonthlyCPM.Value : null;

					spinEditMonthlyImpressions.EditValue = Product.MonthlyImpressionsCalculated;
					break;
			}
		}

		private void UpdateTotalFormula()
		{
			switch (Product.Formula)
			{
				case FormulaType.CPM:
					Product.TotalInvestment = spinEditTotalInvestment.EditValue != null ? (double?)spinEditTotalInvestment.Value : null;
					Product.TotalImpressions = spinEditTotalImpressions.EditValue != null ? (double?)spinEditTotalImpressions.Value : null;

					spinEditTotalCPM.EditValue = Product.TotalCPMCalculated;
					break;
				case FormulaType.Investment:
					Product.TotalImpressions = spinEditTotalImpressions.EditValue != null ? (double?)spinEditTotalImpressions.Value : null;
					Product.TotalCPM = spinEditTotalCPM.EditValue != null ? (double?)spinEditTotalCPM.Value : null;

					spinEditTotalInvestment.EditValue = Product.TotalInvestmentCalculated;
					break;
				case FormulaType.Impressions:
					Product.TotalInvestment = spinEditTotalInvestment.EditValue != null ? (double?)spinEditTotalInvestment.Value : null;
					Product.TotalCPM = spinEditTotalCPM.EditValue != null ? (double?)spinEditTotalCPM.Value : null;

					spinEditTotalImpressions.EditValue = Product.TotalImpressionsCalculated;
					break;
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
					checkEditMonthlyFormulaCPM.Checked = true;
					checkEditMonthlyFormulaInvestment.Checked = false;
					checkEditMonthlyFormulaImpressions.Checked = false;
					checkEditTotalFormulaCPM.Checked = true;
					checkEditTotalFormulaInvestment.Checked = false;
					checkEditTotalFormulaImpressions.Checked = false;
					break;
				case FormulaType.Investment:
					checkEditMonthlyFormulaCPM.Checked = false;
					checkEditMonthlyFormulaInvestment.Checked = true;
					checkEditMonthlyFormulaImpressions.Checked = false;
					checkEditTotalFormulaCPM.Checked = false;
					checkEditTotalFormulaInvestment.Checked = true;
					checkEditTotalFormulaImpressions.Checked = false;
					break;
				case FormulaType.Impressions:
					checkEditMonthlyFormulaCPM.Checked = false;
					checkEditMonthlyFormulaInvestment.Checked = false;
					checkEditMonthlyFormulaImpressions.Checked = true;
					checkEditTotalFormulaCPM.Checked = false;
					checkEditTotalFormulaInvestment.Checked = false;
					checkEditTotalFormulaImpressions.Checked = true;
					break;
			}

			spinEditMonthlyImpressions.EditValue = Product.MonthlyImpressionsCalculated;
			spinEditMonthlyInvestment.EditValue = Product.MonthlyInvestmentCalculated;
			spinEditMonthlyCPM.EditValue = Product.MonthlyCPMCalculated;
			spinEditTotalImpressions.EditValue = Product.TotalImpressionsCalculated;
			spinEditTotalInvestment.EditValue = Product.TotalInvestmentCalculated;
			spinEditTotalCPM.EditValue = Product.TotalCPMCalculated;

			checkEditMonthly.Checked = Product.ShowMonthly;
			checkEditTotal.Checked = Product.ShowTotal;

			checkedListBoxControlWebsite.UnCheckAll();
			foreach (CheckedListBoxItem item in checkedListBoxControlWebsite.Items)
				if (Product.Websites.Contains(item.Value.ToString()))
					item.CheckState = CheckState.Checked;

			if (Product.Width.HasValue && Product.Height.HasValue && Product.ShowDimensions)
				memoEditDescription.EditValue = String.Format("(Ad Dimensions: {0}){1}{1}{2}", Product.Dimensions, Environment.NewLine, Product.Description);
			else
				memoEditDescription.EditValue = Product.Description;

			spinEditAdRate.EditValue = Product.AdRate;
			spinEditActiveDays.EditValue = Product.ActiveDays;
			spinEditTotalAds.EditValue = Product.TotalAds;
			checkEditComments.Checked = !String.IsNullOrEmpty(Product.Comment);
			memoEditComments.EditValue = Product.Comment;
			checkEditStrengths1.Checked = !String.IsNullOrEmpty(Product.Strength1);
			comboBoxEditStrengths1.EditValue = Product.Strength1;
			checkEditStrengths2.Checked = !String.IsNullOrEmpty(Product.Strength2);
			comboBoxEditStrengths2.EditValue = Product.Strength2;

			hyperLinkEditResetProductName.Visible = !(memoEditProductName.EditValue != null && memoEditProductName.EditValue.ToString().Equals(Product.ExtendedName));
			_allowToSave = true;
		}

		private void UpdateFormulaComponents()
		{
			if (checkEditMonthlyFormulaCPM.Checked && checkEditTotalFormulaCPM.Checked)
			{
				Product.Formula = FormulaType.CPM;

				if (checkEditMonthly.Checked)
				{
					spinEditMonthlyImpressions.Enabled = true;
					spinEditMonthlyImpressions.Properties.AppearanceDisabled.Options.UseForeColor = false;
					spinEditMonthlyInvestment.Enabled = true;
					spinEditMonthlyInvestment.Properties.AppearanceDisabled.Options.UseForeColor = false;
					spinEditMonthlyCPM.Enabled = false;
					spinEditMonthlyCPM.Properties.AppearanceDisabled.ForeColor = Color.Black;
					spinEditMonthlyCPM.Properties.AppearanceDisabled.Options.UseForeColor = true;
				}

				if (checkEditTotal.Checked)
				{
					spinEditTotalImpressions.Enabled = true;
					spinEditTotalImpressions.Properties.AppearanceDisabled.Options.UseForeColor = false;
					spinEditTotalInvestment.Enabled = true;
					spinEditTotalInvestment.Properties.AppearanceDisabled.Options.UseForeColor = false;
					spinEditTotalCPM.Enabled = false;
					spinEditTotalCPM.Properties.AppearanceDisabled.ForeColor = Color.Black;
					spinEditTotalCPM.Properties.AppearanceDisabled.Options.UseForeColor = true;
				}
			}
			else if (checkEditMonthlyFormulaInvestment.Checked && checkEditTotalFormulaInvestment.Checked)
			{
				Product.Formula = FormulaType.Investment;

				if (checkEditMonthly.Checked)
				{
					spinEditMonthlyImpressions.Enabled = true;
					spinEditMonthlyImpressions.Properties.AppearanceDisabled.Options.UseForeColor = false;
					spinEditMonthlyInvestment.Properties.AppearanceDisabled.ForeColor = Color.Black;
					spinEditMonthlyInvestment.Properties.AppearanceDisabled.Options.UseForeColor = true;
					spinEditMonthlyInvestment.Enabled = false;
					spinEditMonthlyCPM.Enabled = true;
					spinEditMonthlyCPM.Properties.AppearanceDisabled.Options.UseForeColor = false;
				}

				if (checkEditTotal.Checked)
				{
					spinEditTotalImpressions.Enabled = true;
					spinEditTotalImpressions.Properties.AppearanceDisabled.Options.UseForeColor = false;
					spinEditTotalInvestment.Enabled = false;
					spinEditTotalInvestment.Properties.AppearanceDisabled.ForeColor = Color.Black;
					spinEditTotalInvestment.Properties.AppearanceDisabled.Options.UseForeColor = true;
					spinEditTotalCPM.Enabled = true;
					spinEditTotalCPM.Properties.AppearanceDisabled.Options.UseForeColor = false;
				}
			}
			else if (checkEditMonthlyFormulaImpressions.Checked && checkEditTotalFormulaImpressions.Checked)
			{
				Product.Formula = FormulaType.Impressions;

				if (checkEditMonthly.Checked)
				{
					spinEditMonthlyImpressions.Enabled = false;
					spinEditMonthlyImpressions.Properties.AppearanceDisabled.ForeColor = Color.Black;
					spinEditMonthlyImpressions.Properties.AppearanceDisabled.Options.UseForeColor = true;
					spinEditMonthlyInvestment.Enabled = true;
					spinEditMonthlyInvestment.Properties.AppearanceDisabled.Options.UseForeColor = false;
					spinEditMonthlyCPM.Enabled = true;
					spinEditMonthlyCPM.Properties.AppearanceDisabled.Options.UseForeColor = false;
				}

				if (checkEditTotal.Checked)
				{
					spinEditTotalImpressions.Enabled = false;
					spinEditTotalImpressions.Properties.AppearanceDisabled.ForeColor = Color.Black;
					spinEditTotalImpressions.Properties.AppearanceDisabled.Options.UseForeColor = true;
					spinEditTotalInvestment.Enabled = true;
					spinEditTotalInvestment.Properties.AppearanceDisabled.Options.UseForeColor = false;
					spinEditTotalCPM.Enabled = true;
					spinEditTotalCPM.Properties.AppearanceDisabled.Options.UseForeColor = false;
				}
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

			Product.AdRate = spinEditAdRate.EditValue != null ? (double?)spinEditAdRate.Value : null;
			Product.MonthlyInvestment = spinEditMonthlyInvestment.EditValue != null ? (double?)spinEditMonthlyInvestment.Value : null;
			Product.TotalInvestment = spinEditTotalInvestment.EditValue != null ? (double?)spinEditTotalInvestment.Value : null;
			Product.MonthlyImpressions = spinEditMonthlyImpressions.EditValue != null ? (double?)spinEditMonthlyImpressions.Value : null;
			Product.TotalImpressions = spinEditTotalImpressions.EditValue != null ? (double?)spinEditTotalImpressions.Value : null;
			Product.MonthlyCPM = spinEditMonthlyCPM.EditValue != null ? (double?)spinEditMonthlyCPM.Value : null;
			Product.TotalCPM = spinEditTotalCPM.EditValue != null ? (double?)spinEditTotalCPM.Value : null;
			Product.ShowMonthly = checkEditMonthly.Checked;
			Product.ShowTotal = checkEditTotal.Checked;
			Product.ActiveDays = spinEditActiveDays.EditValue != null ? (int?)spinEditActiveDays.Value : null;
			Product.TotalAds = spinEditTotalAds.EditValue != null ? (int?)spinEditTotalAds.Value : null;
			Product.Comment = checkEditComments.Checked && memoEditComments.EditValue != null ? memoEditComments.EditValue.ToString() : string.Empty;
			Product.Strength1 = checkEditStrengths1.Checked && comboBoxEditStrengths1.EditValue != null ? comboBoxEditStrengths1.EditValue.ToString() : string.Empty;
			Product.Strength2 = checkEditStrengths2.Checked && comboBoxEditStrengths2.EditValue != null ? comboBoxEditStrengths2.EditValue.ToString() : string.Empty;
		}

		private void checkEditMonthly_CheckedChanged(object sender, EventArgs e)
		{
			checkEditMonthlyFormulaImpressions.Enabled = checkEditMonthly.Checked;
			spinEditMonthlyImpressions.Enabled = checkEditMonthly.Checked && !checkEditMonthlyFormulaImpressions.Checked;
			checkEditMonthlyFormulaInvestment.Enabled = checkEditMonthly.Checked;
			spinEditMonthlyInvestment.Enabled = checkEditMonthly.Checked && !checkEditMonthlyFormulaInvestment.Checked;
			checkEditMonthlyFormulaCPM.Enabled = checkEditMonthly.Checked;
			spinEditMonthlyCPM.Enabled = checkEditMonthly.Checked && !checkEditMonthlyFormulaCPM.Checked;
			if (!_allowToSave) return;
			_container.SettingsNotSaved = true;
			if (checkEditMonthly.Checked) return;
			spinEditMonthlyImpressions.EditValue = null;
			spinEditMonthlyInvestment.EditValue = null;
			spinEditMonthlyCPM.EditValue = Product.Formula != FormulaType.CPM ? Product.DefaultRate : null;
		}

		private void checkEditTotal_CheckedChanged(object sender, EventArgs e)
		{
			checkEditTotalFormulaImpressions.Enabled = checkEditTotal.Checked;
			spinEditTotalImpressions.Enabled = checkEditTotal.Checked && !checkEditTotalFormulaImpressions.Checked;
			checkEditTotalFormulaInvestment.Enabled = checkEditTotal.Checked;
			spinEditTotalInvestment.Enabled = checkEditTotal.Checked && !checkEditTotalFormulaInvestment.Checked;
			checkEditTotalFormulaCPM.Enabled = checkEditTotal.Checked;
			spinEditTotalCPM.Enabled = checkEditTotal.Checked && !checkEditTotalFormulaCPM.Checked;
			if (!_allowToSave) return;
			_container.SettingsNotSaved = true;
			if (checkEditTotal.Checked) return;
			spinEditTotalImpressions.EditValue = null;
			spinEditTotalInvestment.EditValue = null;
			spinEditTotalCPM.EditValue = Product.Formula != FormulaType.CPM ? Product.DefaultRate : null;
		}

		private void checkEditFormula_CheckedChanged(object sender, EventArgs e)
		{
			if (sender == checkEditMonthlyFormulaImpressions)
				checkEditTotalFormulaImpressions.Checked = checkEditMonthlyFormulaImpressions.Checked;
			else if (sender == checkEditTotalFormulaImpressions)
				checkEditMonthlyFormulaImpressions.Checked = checkEditTotalFormulaImpressions.Checked;
			else if (sender == checkEditMonthlyFormulaInvestment)
				checkEditTotalFormulaInvestment.Checked = checkEditMonthlyFormulaInvestment.Checked;
			else if (sender == checkEditTotalFormulaInvestment)
				checkEditMonthlyFormulaInvestment.Checked = checkEditTotalFormulaInvestment.Checked;
			else if (sender == checkEditMonthlyFormulaCPM)
				checkEditTotalFormulaCPM.Checked = checkEditMonthlyFormulaCPM.Checked;
			else if (sender == checkEditTotalFormulaCPM)
				checkEditMonthlyFormulaCPM.Checked = checkEditTotalFormulaCPM.Checked;

			UpdateFormulaComponents();

			if (_allowToSave)
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
			_container.SettingsNotSaved = true;
		}

		private void spinEditMonthly_EditValueChanged(object sender, EventArgs e)
		{
			if (_allowToSave)
			{
				UpdateMonthlyFormula();
				_container.SettingsNotSaved = true;
			}
		}

		private void spinEditTotal_EditValueChanged(object sender, EventArgs e)
		{
			if (_allowToSave)
			{
				UpdateTotalFormula();
				_container.SettingsNotSaved = true;
			}
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
		public void Output()
		{
			OnlineSchedulePowerPointHelper.Instance.AppendOneSheet(new[] { Product }, _container.SelectedTheme);
		}
		#endregion
	}
}
