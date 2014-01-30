﻿using System;
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
			spinEditInvestment.Enter += Utilities.Instance.Editor_Enter;
			spinEditInvestment.MouseDown += Utilities.Instance.Editor_MouseDown;
			spinEditInvestment.MouseUp += Utilities.Instance.Editor_MouseUp;
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
					if (Product.ShowMonthly)
					{
						Product.MonthlyInvestment = inv;
						Product.MonthlyImpressions = imp;
						spinEditCPM.EditValue = Product.MonthlyCPMCalculated;
					}
					else if (Product.ShowTotal)
					{
						Product.TotalInvestment = inv;
						Product.TotalImpressions = imp;
						spinEditCPM.EditValue = Product.TotalCPMCalculated;
					}
					break;
				case FormulaType.Investment:
					if (Product.ShowMonthly)
					{
						Product.MonthlyImpressions = imp;
						Product.MonthlyCPM = cpm;
						spinEditInvestment.EditValue = Product.MonthlyInvestmentCalculated;
					}
					else if (Product.ShowTotal)
					{
						Product.TotalImpressions = imp;
						Product.TotalCPM = cpm;
						spinEditInvestment.EditValue = Product.TotalInvestmentCalculated;
					}
					break;
				case FormulaType.Impressions:
					if (Product.ShowMonthly)
					{
						Product.MonthlyInvestment = inv;
						Product.MonthlyCPM = cpm;
						spinEditImpressions.EditValue = Product.MonthlyImpressionsCalculated;
					}
					else if (Product.ShowTotal)
					{
						Product.TotalInvestment = inv;
						Product.TotalCPM = cpm;
						spinEditImpressions.EditValue = Product.TotalImpressionsCalculated;
					}
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
			if (Product.ShowMonthly)
				comboBoxEditPriceType.SelectedIndex = 0;
			else if (Product.ShowTotal)
				comboBoxEditPriceType.SelectedIndex = 1;
			else
				comboBoxEditPriceType.SelectedIndex = 2;

			checkedListBoxControlWebsite.UnCheckAll();
			foreach (CheckedListBoxItem item in checkedListBoxControlWebsite.Items)
				if (Product.Websites.Contains(item.Value.ToString()))
					item.CheckState = CheckState.Checked;

			if (Product.Width.HasValue && Product.Height.HasValue && Product.ShowDimensions)
				memoEditDescription.EditValue = String.Format("(Ad Dimensions: {0}){1}{1}{2}", Product.Dimensions, Environment.NewLine, Product.Description);
			else
				memoEditDescription.EditValue = Product.Description;

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
			Product.Comment = checkEditComments.Checked && memoEditComments.EditValue != null ? memoEditComments.EditValue.ToString() : string.Empty;
			Product.Strength1 = checkEditStrengths1.Checked && comboBoxEditStrengths1.EditValue != null ? comboBoxEditStrengths1.EditValue.ToString() : string.Empty;
			Product.Strength2 = checkEditStrengths2.Checked && comboBoxEditStrengths2.EditValue != null ? comboBoxEditStrengths2.EditValue.ToString() : string.Empty;
		}

		private void comboBoxEditPriceType_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			_allowToSave = false;
			if (comboBoxEditPriceType.SelectedIndex == 0)
			{
				checkEditFormulaImpressions.Visible = true;
				checkEditFormulaInvestment.Visible = true;
				checkEditFormulaCPM.Visible = true;
				spinEditImpressions.Visible = true;
				spinEditInvestment.Visible = true;
				spinEditCPM.Visible = true;

				Product.ShowMonthly = true;
				Product.ShowTotal = false;

				spinEditImpressions.EditValue = Product.MonthlyImpressionsCalculated;
				spinEditInvestment.EditValue = Product.MonthlyInvestmentCalculated;
				spinEditCPM.EditValue = Product.MonthlyCPMCalculated;
			}
			else if (comboBoxEditPriceType.SelectedIndex == 1)
			{
				checkEditFormulaImpressions.Visible = true;
				checkEditFormulaInvestment.Visible = true;
				checkEditFormulaCPM.Visible = true;
				spinEditImpressions.Visible = true;
				spinEditInvestment.Visible = true;
				spinEditCPM.Visible = true;

				Product.ShowMonthly = false;
				Product.ShowTotal = true;

				spinEditImpressions.EditValue = Product.TotalImpressionsCalculated;
				spinEditInvestment.EditValue = Product.TotalInvestmentCalculated;
				spinEditCPM.EditValue = Product.TotalCPMCalculated;
			}
			else
			{
				checkEditFormulaImpressions.Visible = false;
				checkEditFormulaInvestment.Visible = false;
				checkEditFormulaCPM.Visible = false;
				spinEditImpressions.Visible = false;
				spinEditInvestment.Visible = false;
				spinEditCPM.Visible = false;

				Product.ShowMonthly = false;
				Product.ShowTotal = false;

				spinEditImpressions.EditValue = null;
				spinEditInvestment.EditValue = null;
				spinEditCPM.EditValue = Product.Formula != FormulaType.CPM ? Product.DefaultRate : null; ;
			}
			_allowToSave = true;
			_container.SettingsNotSaved = true;
		}

		private void checkEditFormula_CheckedChanged(object sender, EventArgs e)
		{
			UpdateFormulaComponents();
			if (_allowToSave)
			{
				UpdateFormula();
				_container.SettingsNotSaved = true;
			}
		}

		private void spinEditPricing_EditValueChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			UpdateFormula();
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
