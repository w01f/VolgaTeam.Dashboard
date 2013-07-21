using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.OnlineSchedule;
using NewBizWiz.OnlineSchedule.Controls.InteropClasses;
using NewBizWiz.OnlineSchedule.Controls.PresentationClasses.ToolForms;
using NewBizWiz.OnlineSchedule.Controls.Properties;
using ItemCheckEventArgs = DevExpress.XtraEditors.Controls.ItemCheckEventArgs;
using ListManager = NewBizWiz.Core.OnlineSchedule.ListManager;
using SettingsManager = NewBizWiz.Core.Common.SettingsManager;

namespace NewBizWiz.OnlineSchedule.Controls.PresentationClasses
{
	[ToolboxItem(false)]
	public partial class ProductPackageControl : UserControl
	{
		private bool _allowToSave;

		public ProductPackageControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			if ((base.CreateGraphics()).DpiX > 96)
			{
				var font = new Font(labelControlProduct.Font.FontFamily, labelControlProduct.Font.Size - 4, labelControlProduct.Font.Style);
				labelControlProduct.Font = font;
				labelControlWebsitelogo.Font = font;
				labelControlComments.Font = font;
				labelControlAdCampaign.Font = font;
				labelControlImpressions.Font = font;
				labelControlInvestment.Font = font;
				font = new Font(labelControlWebsiteDetails.Font.FontFamily, labelControlWebsiteDetails.Font.Size - 2, labelControlWebsiteDetails.Font.Style);
				labelControlActiveDays.Font = font;
				labelControlAdRate.Font = font;
				labelControlFlightDates.Font = font;
				labelControlMonthlyCPM.Font = font;
				labelControlMonthlyImpressions.Font = font;
				labelControlMonthlyInvestment.Font = font;
				labelControlTotalAds.Font = font;
				labelControlTotalCPM.Font = font;
				labelControlTotalImpressions.Font = font;
				labelControlTotalInvestment.Font = font;
				labelControlWebsiteDetails.Font = font;
				checkEditComments.Font = font;
				checkEditDuration.Font = font;
				checkEditMonths.Font = font;
				checkEditWeeks.Font = font;
			}
			spinEditActiveDays.Enter += Utilities.Instance.Editor_Enter;
			spinEditActiveDays.MouseDown += Utilities.Instance.Editor_MouseDown;
			spinEditActiveDays.MouseUp += Utilities.Instance.Editor_MouseUp;
			spinEditAdRate.Enter += Utilities.Instance.Editor_Enter;
			spinEditAdRate.MouseDown += Utilities.Instance.Editor_MouseDown;
			spinEditAdRate.MouseUp += Utilities.Instance.Editor_MouseUp;
			spinEditDuration.Enter += Utilities.Instance.Editor_Enter;
			spinEditDuration.MouseDown += Utilities.Instance.Editor_MouseDown;
			spinEditDuration.MouseUp += Utilities.Instance.Editor_MouseUp;
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
			textEditCustomWebsite1.Enter += Utilities.Instance.Editor_Enter;
			textEditCustomWebsite1.MouseDown += Utilities.Instance.Editor_MouseDown;
			textEditCustomWebsite1.MouseUp += Utilities.Instance.Editor_MouseUp;
			textEditCustomWebsite2.Enter += Utilities.Instance.Editor_Enter;
			textEditCustomWebsite2.MouseDown += Utilities.Instance.Editor_MouseDown;
			textEditCustomWebsite2.MouseUp += Utilities.Instance.Editor_MouseUp;
			memoEditComments.Enter += Utilities.Instance.Editor_Enter;
			memoEditComments.MouseDown += Utilities.Instance.Editor_MouseDown;
			memoEditComments.MouseUp += Utilities.Instance.Editor_MouseUp;
			memoEditDescription.Enter += Utilities.Instance.Editor_Enter;
			memoEditDescription.MouseDown += Utilities.Instance.Editor_MouseDown;
			memoEditDescription.MouseUp += Utilities.Instance.Editor_MouseUp;

			AssignCloseActiveEditorsonOutSideClick(this);
		}

		public ProductPackage ProductPackage { get; set; }

		private void AssignCloseActiveEditorsonOutSideClick(Control control)
		{
			if (control.GetType() != typeof(TextEdit) && control.GetType() != typeof(MemoEdit) && control.GetType() != typeof(ComboBoxEdit) && control.GetType() != typeof(LookUpEdit) && control.GetType() != typeof(DateEdit) && control.GetType() != typeof(CheckedListBoxControl) && control.GetType() != typeof(SpinEdit) && control.GetType() != typeof(CheckEdit))
			{
				control.Click += CloseActiveEditorsonOutSideClick;
				foreach (Control childControl in control.Controls)
					AssignCloseActiveEditorsonOutSideClick(childControl);
			}
		}

		private void CloseActiveEditorsonOutSideClick(object sender, EventArgs e)
		{
			xtraScrollableControlRight.Focus();
		}

		private void UpdateMonthlyFormula()
		{
			switch (ProductPackage.Formula)
			{
				case FormulaType.CPM:
					ProductPackage.MonthlyInvestment = spinEditMonthlyInvestment.EditValue != null ? (double?)spinEditMonthlyInvestment.Value : null;
					ProductPackage.MonthlyImpressions = spinEditMonthlyImpressions.EditValue != null ? (double?)spinEditMonthlyImpressions.Value : null;

					spinEditMonthlyCPM.EditValue = ProductPackage.MonthlyCPMCalculated;
					break;
				case FormulaType.Investment:
					ProductPackage.MonthlyImpressions = spinEditMonthlyImpressions.EditValue != null ? (double?)spinEditMonthlyImpressions.Value : null;
					ProductPackage.MonthlyCPM = spinEditMonthlyCPM.EditValue != null ? (double?)spinEditMonthlyCPM.Value : null;

					spinEditMonthlyInvestment.EditValue = ProductPackage.MonthlyInvestmentCalculated;
					break;
				case FormulaType.Impressions:
					ProductPackage.MonthlyInvestment = spinEditMonthlyInvestment.EditValue != null ? (double?)spinEditMonthlyInvestment.Value : null;
					ProductPackage.MonthlyCPM = spinEditMonthlyCPM.EditValue != null ? (double?)spinEditMonthlyCPM.Value : null;

					spinEditMonthlyImpressions.EditValue = ProductPackage.MonthlyImpressionsCalculated;
					break;
			}
		}

		private void UpdateTotalFormula()
		{
			switch (ProductPackage.Formula)
			{
				case FormulaType.CPM:
					ProductPackage.TotalInvestment = spinEditTotalInvestment.EditValue != null ? (double?)spinEditTotalInvestment.Value : null;
					ProductPackage.TotalImpressions = spinEditTotalImpressions.EditValue != null ? (double?)spinEditTotalImpressions.Value : null;

					spinEditTotalCPM.EditValue = ProductPackage.TotalCPMCalculated;
					break;
				case FormulaType.Investment:
					ProductPackage.TotalImpressions = spinEditTotalImpressions.EditValue != null ? (double?)spinEditTotalImpressions.Value : null;
					ProductPackage.TotalCPM = spinEditTotalCPM.EditValue != null ? (double?)spinEditTotalCPM.Value : null;

					spinEditTotalInvestment.EditValue = ProductPackage.TotalInvestmentCalculated;
					break;
				case FormulaType.Impressions:
					ProductPackage.TotalInvestment = spinEditTotalInvestment.EditValue != null ? (double?)spinEditTotalInvestment.Value : null;
					ProductPackage.TotalCPM = spinEditTotalCPM.EditValue != null ? (double?)spinEditTotalCPM.Value : null;

					spinEditTotalImpressions.EditValue = ProductPackage.TotalImpressionsCalculated;
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

			Text = ProductPackage.Name.Replace("&", "&&");

			if (!string.IsNullOrEmpty(ProductPackage.SlideHeader))
				Controller.Instance.WebPackage.comboBoxEditSlideHeader.EditValue = ProductPackage.SlideHeader;
			else
				Controller.Instance.WebPackage.comboBoxEditSlideHeader.EditValue = null;
			Controller.Instance.WebPackage.labelControlPresentationDate.Text = "Presentation Date: " + ProductPackage.Parent.PresentationDate.ToString("MM/dd/yy");

			checkedListBoxControlWebsite.UnCheckAll();
			foreach (CheckedListBoxItem item in checkedListBoxControlWebsite.Items)
				if (ProductPackage.Websites.Contains(item.Value.ToString()))
					item.CheckState = CheckState.Checked;
			if (checkedListBoxControlWebsite.CheckedItems.Count == 0)
				if (checkedListBoxControlWebsite.Items.Count > 0)
					checkedListBoxControlWebsite.Items[0].CheckState = CheckState.Checked;
			checkEditCustomWebsite1.Checked = ProductPackage.ShowCustomWebsite1;
			textEditCustomWebsite1.EditValue = ProductPackage.CustomWebsite1;
			checkEditCustomWebsite2.Checked = ProductPackage.ShowCustomWebsite2;
			textEditCustomWebsite2.EditValue = ProductPackage.CustomWebsite2;
			labelControlProduct.Text = ProductPackage.Name;
			memoEditDescription.EditValue = ProductPackage.Description;
			spinEditAdRate.EditValue = ProductPackage.AdRate;
			spinEditMonthlyInvestment.EditValue = ProductPackage.MonthlyInvestmentCalculated;
			spinEditTotalInvestment.EditValue = ProductPackage.TotalInvestmentCalculated;
			spinEditMonthlyImpressions.EditValue = ProductPackage.MonthlyImpressionsCalculated;
			spinEditTotalImpressions.EditValue = ProductPackage.TotalImpressionsCalculated;
			spinEditMonthlyCPM.EditValue = ProductPackage.MonthlyCPMCalculated;
			spinEditTotalCPM.EditValue = ProductPackage.TotalCPMCalculated;
			labelControlFlightDates.Text = ProductPackage.Parent.FlightDates;
			checkEditDuration.Checked = ProductPackage.ShowDuration;
			switch (ProductPackage.DurationType)
			{
				case "Months":
					checkEditMonths.Checked = true;
					checkEditWeeks.Checked = false;
					break;
				case "Weeks":
					checkEditWeeks.Checked = true;
					checkEditMonths.Checked = false;
					break;
			}
			if (ProductPackage.DurationValue.HasValue)
			{
				spinEditDuration.EditValue = ProductPackage.DurationValue;
			}
			else
			{
				if (checkEditMonths.Checked)
					spinEditDuration.EditValue = ProductPackage.MonthDuraton;
				else if (checkEditWeeks.Checked)
					spinEditDuration.EditValue = ProductPackage.WeeksDuration;
			}
			spinEditActiveDays.EditValue = ProductPackage.ActiveDays;
			spinEditTotalAds.EditValue = ProductPackage.TotalAds;
			checkEditComments.Checked = ProductPackage.ShowCommentText;
			memoEditComments.EditValue = ProductPackage.Comment;
			checkEditStrengths1.Checked = ProductPackage.ShowStrength1;
			comboBoxEditStrengths1.EditValue = ProductPackage.Strength1;
			checkEditStrengths2.Checked = ProductPackage.ShowStrength2;
			comboBoxEditStrengths2.EditValue = ProductPackage.Strength2;

			UpdateView();
			_allowToSave = true;
		}

		public void UpdateView()
		{
			Controller.Instance.WebPackage.labelControlPresentationDate.Visible = ProductPackage.ShowPresentationDate;
			if (ProductPackage.ShowBusinessName && ProductPackage.ShowDecisionMaker)
			{
				Controller.Instance.WebPackage.labelControlAdvertiser.Visible = true;
				Controller.Instance.WebPackage.labelControlAdvertiser.Text = "Prepared For: " + ProductPackage.Parent.BusinessName + "\n\n" + ProductPackage.Parent.DecisionMaker;
			}
			else if (!ProductPackage.ShowBusinessName && ProductPackage.ShowDecisionMaker)
			{
				Controller.Instance.WebPackage.labelControlAdvertiser.Visible = true;
				Controller.Instance.WebPackage.labelControlAdvertiser.Text = ProductPackage.Parent.DecisionMaker;
			}
			else if (ProductPackage.ShowBusinessName && !ProductPackage.ShowDecisionMaker)
			{
				Controller.Instance.WebPackage.labelControlAdvertiser.Visible = true;
				Controller.Instance.WebPackage.labelControlAdvertiser.Text = "Prepared For: " + ProductPackage.Parent.BusinessName;
			}
			else
			{
				Controller.Instance.WebPackage.labelControlAdvertiser.Visible = false;
			}

			labelControlProduct.Visible = true;
			pbProductLogo.Visible = true;
			memoEditDescription.Visible = true;
			pnProduct.Visible = true;
			pnWebsites.Dock = DockStyle.Bottom;

			labelControlFlightDates.Visible = ProductPackage.ShowFlightDates;

			int heightImpressions = pnImpressionsLogo.Height + 25;
			pnImpressions.Visible = ProductPackage.ShowMonthlyImpressions | ProductPackage.ShowTotalImpressions;
			pnMonthlyImpressions.Visible = ProductPackage.ShowMonthlyImpressions;
			labelControlMonthlyCPM.Visible = ProductPackage.ShowMonthlyCPM;
			spinEditMonthlyCPM.Visible = ProductPackage.ShowMonthlyCPM;
			if (ProductPackage.ShowMonthlyImpressions)
			{
				pnMonthlyImpressions.SendToBack();
				heightImpressions += pnMonthlyImpressions.Height;
			}
			pnTotalImpressions.Visible = ProductPackage.ShowTotalImpressions;
			labelControlTotalCPM.Visible = ProductPackage.ShowTotalCPM;
			spinEditTotalCPM.Visible = ProductPackage.ShowTotalCPM;
			if (ProductPackage.ShowTotalImpressions)
			{
				pnTotalImpressions.BringToFront();
				heightImpressions += pnTotalImpressions.Height;
			}
			pnImpressionsLogo.SendToBack();
			if (ProductPackage.ShowMonthlyImpressions | ProductPackage.ShowTotalImpressions)
			{
				pnImpressions.Height = heightImpressions;
				pnImpressions.SendToBack();
			}

			int heightInvestment = pnInvestmentLogo.Height + 25;
			pnInvestment.Visible = ProductPackage.ShowAdRate | ProductPackage.ShowMonthlyInvestment | ProductPackage.ShowTotalInvestment;
			pnAdRate.Visible = ProductPackage.ShowAdRate;
			if (ProductPackage.ShowAdRate)
			{
				pnAdRate.SendToBack();
				heightInvestment += pnAdRate.Height;
			}
			pnMonthlyInvestment.Visible = ProductPackage.ShowMonthlyInvestment;
			if (ProductPackage.ShowMonthlyInvestment)
				heightInvestment += pnMonthlyInvestment.Height;
			pnTotalInvestment.Visible = ProductPackage.ShowTotalInvestment;
			if (ProductPackage.ShowTotalInvestment)
			{
				pnTotalInvestment.BringToFront();
				heightInvestment += pnTotalInvestment.Height;
			}
			pnInvestmentLogo.SendToBack();
			if (ProductPackage.ShowAdRate | ProductPackage.ShowMonthlyInvestment | ProductPackage.ShowTotalInvestment)
			{
				pnInvestment.Height = heightInvestment;
				pnInvestment.SendToBack();
			}

			pnTotalMonth.Visible = ProductPackage.ShowFlightDates;
			if (ProductPackage.ShowFlightDates)
				pnTotalMonth.SendToBack();
			pnActiveDays.Visible = ProductPackage.ShowActiveDays;
			pnTotalAds.Visible = ProductPackage.ShowTotalAds;
			if (ProductPackage.ShowTotalAds)
				pnTotalAds.BringToFront();
			pnTotals.Visible = ProductPackage.ShowFlightDates | ProductPackage.ShowActiveDays | ProductPackage.ShowTotalAds;
			if (ProductPackage.ShowFlightDates | ProductPackage.ShowActiveDays | ProductPackage.ShowTotalAds)
				pnTotals.SendToBack();

			pnAdCampaignLogo.Visible = ProductPackage.ShowFlightDates | ProductPackage.ShowActiveDays | ProductPackage.ShowTotalAds;
			if (ProductPackage.ShowFlightDates | ProductPackage.ShowActiveDays | ProductPackage.ShowTotalAds)
				pnAdCampaignLogo.SendToBack();

			pnComments.Visible = ProductPackage.ShowComments;
			if (ProductPackage.ShowComments)
				pnComments.BringToFront();

			switch (ProductPackage.Formula)
			{
				case FormulaType.CPM:
					spinEditMonthlyCPM.Enabled = false;
					spinEditTotalCPM.Enabled = false;
					spinEditMonthlyInvestment.Enabled = true;
					spinEditTotalInvestment.Enabled = true;
					spinEditMonthlyImpressions.Enabled = true;
					spinEditTotalImpressions.Enabled = true;
					break;
				case FormulaType.Investment:
					spinEditMonthlyInvestment.Enabled = false;
					spinEditTotalInvestment.Enabled = false;
					spinEditMonthlyCPM.Enabled = true;
					spinEditTotalCPM.Enabled = true;
					spinEditMonthlyImpressions.Enabled = true;
					spinEditTotalImpressions.Enabled = true;
					break;
				case FormulaType.Impressions:
					spinEditMonthlyImpressions.Enabled = false;
					spinEditTotalImpressions.Enabled = false;
					spinEditMonthlyCPM.Enabled = true;
					spinEditTotalCPM.Enabled = true;
					spinEditMonthlyInvestment.Enabled = true;
					spinEditTotalInvestment.Enabled = true;
					break;
			}
			Controller.Instance.WebPackage.pictureBoxFormula.Image = ProductPackage.ShowCPMButton ? Resources.InvestmentLogo : Resources.InvestmentLogo;
			Controller.Instance.WebPackage.labelControlFormula.Enabled = ProductPackage.ShowCPMButton;
			Controller.Instance.WebPackage.checkEditFormulaCPM.Enabled = ProductPackage.ShowCPMButton;
			Controller.Instance.WebPackage.checkEditFormulaInvestment.Enabled = ProductPackage.ShowCPMButton;
			Controller.Instance.WebPackage.checkEditFormulaImpressions.Enabled = ProductPackage.ShowCPMButton;
		}

		public void SaveValues()
		{
			if (_allowToSave)
			{
				ProductPackage.SlideHeader = Controller.Instance.WebPackage.comboBoxEditSlideHeader.EditValue != null ? Controller.Instance.WebPackage.comboBoxEditSlideHeader.EditValue.ToString() : (Controller.Instance.WebPackage.comboBoxEditSlideHeader.Properties.Items.Count > 0 ? Controller.Instance.WebPackage.comboBoxEditSlideHeader.Properties.Items[0].ToString() : string.Empty);
				ProductPackage.Websites.Clear();
				if (Controller.Instance.WebPackage.buttonXWebsites.Checked)
					foreach (CheckedListBoxItem item in checkedListBoxControlWebsite.Items)
						if (item.CheckState == CheckState.Checked)
							ProductPackage.Websites.Add(item.Value.ToString());
				ProductPackage.CustomWebsite1 = textEditCustomWebsite1.EditValue != null ? textEditCustomWebsite1.EditValue.ToString() : string.Empty;
				ProductPackage.CustomWebsite2 = textEditCustomWebsite2.EditValue != null ? textEditCustomWebsite2.EditValue.ToString() : string.Empty;
				ProductPackage.Description = memoEditDescription.EditValue != null ? memoEditDescription.EditValue.ToString() : string.Empty;
				ProductPackage.AdRate = spinEditAdRate.EditValue != null ? (double?)spinEditAdRate.Value : null;
				ProductPackage.MonthlyInvestment = spinEditMonthlyInvestment.EditValue != null ? (double?)spinEditMonthlyInvestment.Value : null;
				ProductPackage.TotalInvestment = spinEditTotalInvestment.EditValue != null ? (double?)spinEditTotalInvestment.Value : null;
				ProductPackage.MonthlyImpressions = spinEditMonthlyImpressions.EditValue != null ? (double?)spinEditMonthlyImpressions.Value : null;
				ProductPackage.TotalImpressions = spinEditTotalImpressions.EditValue != null ? (double?)spinEditTotalImpressions.Value : null;
				ProductPackage.MonthlyCPM = spinEditMonthlyCPM.EditValue != null ? (double?)spinEditMonthlyCPM.Value : null;
				ProductPackage.TotalCPM = spinEditTotalCPM.EditValue != null ? (double?)spinEditTotalCPM.Value : null;
				ProductPackage.DurationValue = spinEditDuration.EditValue != null ? (int?)spinEditDuration.Value : null;
				ProductPackage.ActiveDays = spinEditActiveDays.EditValue != null ? (int?)spinEditActiveDays.Value : null;
				ProductPackage.TotalAds = spinEditTotalAds.EditValue != null ? (int?)spinEditTotalAds.Value : null;
				ProductPackage.Comment = memoEditComments.EditValue != null ? memoEditComments.EditValue.ToString() : string.Empty;
				ProductPackage.Strength1 = comboBoxEditStrengths1.EditValue != null ? comboBoxEditStrengths1.EditValue.ToString() : string.Empty;
				ProductPackage.Strength2 = comboBoxEditStrengths2.EditValue != null ? comboBoxEditStrengths2.EditValue.ToString() : string.Empty;
				SaveCheckboxValues();
			}
		}

		public void SaveCheckboxValues()
		{
			if (_allowToSave)
			{
				ProductPackage.ShowCustomWebsite1 = checkEditCustomWebsite1.Checked;
				ProductPackage.ShowCustomWebsite2 = checkEditCustomWebsite2.Checked;
				ProductPackage.ShowDuration = checkEditDuration.Checked;
				if (checkEditMonths.Checked)
					ProductPackage.DurationType = "Months";
				else if (checkEditWeeks.Checked)
					ProductPackage.DurationType = "Weeks";
				ProductPackage.ShowCommentText = checkEditComments.Checked;
				ProductPackage.ShowStrength1 = checkEditStrengths1.Checked;
				ProductPackage.ShowStrength2 = checkEditStrengths2.Checked;
			}
		}


		public void HideDefaultPanel()
		{
			pnMain.BringToFront();
		}

		private void ckTotalMonth_CheckedChanged(object sender, EventArgs e)
		{
			spinEditDuration.Enabled = checkEditDuration.Checked;
			checkEditMonths.Enabled = checkEditDuration.Checked;
			checkEditWeeks.Enabled = checkEditDuration.Checked;
			SaveCheckboxValues();
			Controller.Instance.WebPackage.UpdateOutputStatus();
			Controller.Instance.WebPackage.SettingsNotSaved = true;
		}

		private void checkEditStrengths1_CheckedChanged(object sender, EventArgs e)
		{
			comboBoxEditStrengths1.Enabled = checkEditStrengths1.Checked;
			SaveCheckboxValues();
			Controller.Instance.WebPackage.SettingsNotSaved = true;
		}

		private void checkEditStrengths2_CheckedChanged(object sender, EventArgs e)
		{
			comboBoxEditStrengths2.Enabled = checkEditStrengths2.Checked;
			SaveCheckboxValues();
			Controller.Instance.WebPackage.SettingsNotSaved = true;
		}

		private void checkEditComments_CheckedChanged(object sender, EventArgs e)
		{
			memoEditComments.Enabled = checkEditComments.Checked;
			SaveCheckboxValues();
			Controller.Instance.WebPackage.SettingsNotSaved = true;
		}

		public void WebsiteCheckedChanged()
		{
			checkedListBoxControlWebsite.Enabled = Controller.Instance.WebPackage.buttonXWebsites.Checked;
			if (!Controller.Instance.WebPackage.buttonXWebsites.Checked)
			{
				checkedListBoxControlWebsite.UnCheckAll();
				checkEditCustomWebsite1.Checked = Controller.Instance.WebPackage.buttonXWebsites.Checked;
				checkEditCustomWebsite2.Checked = Controller.Instance.WebPackage.buttonXWebsites.Checked;
				checkEditCustomWebsite1.Enabled = Controller.Instance.WebPackage.buttonXWebsites.Checked;
				checkEditCustomWebsite2.Enabled = Controller.Instance.WebPackage.buttonXWebsites.Checked;
			}
			else
			{
				if (checkedListBoxControlWebsite.Items.Count > 0)
					checkedListBoxControlWebsite.Items[0].CheckState = CheckState.Checked;
				checkEditCustomWebsite1.Enabled = Controller.Instance.WebPackage.buttonXWebsites.Checked;
				checkEditCustomWebsite2.Enabled = Controller.Instance.WebPackage.buttonXWebsites.Checked;
			}
			Controller.Instance.WebPackage.UpdateOutputStatus();
		}

		private void checkEditCustomWebsite1_CheckedChanged(object sender, EventArgs e)
		{
			textEditCustomWebsite1.Enabled = checkEditCustomWebsite1.Checked;
			SaveCheckboxValues();
		}

		private void checkEditCustomWebsite2_CheckedChanged(object sender, EventArgs e)
		{
			textEditCustomWebsite2.Enabled = checkEditCustomWebsite2.Checked;
			SaveCheckboxValues();
		}

		private void Edit_EditValueChanged(object sender, EventArgs e)
		{
			Controller.Instance.WebPackage.SettingsNotSaved = true;
		}

		private void spinEditMonthly_EditValueChanged(object sender, EventArgs e)
		{
			if (_allowToSave)
			{
				UpdateMonthlyFormula();
				Controller.Instance.WebPackage.SettingsNotSaved = true;
			}
		}

		private void spinEditTotal_EditValueChanged(object sender, EventArgs e)
		{
			if (_allowToSave)
			{
				UpdateTotalFormula();
				Controller.Instance.WebPackage.SettingsNotSaved = true;
			}
		}

		private void checkedListBoxControlWebsite_ItemCheck(object sender, ItemCheckEventArgs e)
		{
			Controller.Instance.WebPackage.SettingsNotSaved = true;
		}

		private void checkEditMonths_CheckedChanged(object sender, EventArgs e)
		{
			checkEditWeeks.CheckedChanged -= checkEditWeeks_CheckedChanged;
			checkEditWeeks.Checked = !checkEditMonths.Checked;
			if (!ProductPackage.DurationValue.HasValue)
			{
				if (checkEditMonths.Checked)
					spinEditDuration.EditValue = ProductPackage.MonthDuraton;
				else if (checkEditWeeks.Checked)
					spinEditDuration.EditValue = ProductPackage.WeeksDuration;
			}
			SaveCheckboxValues();
			checkEditWeeks.CheckedChanged += checkEditWeeks_CheckedChanged;
		}

		private void checkEditWeeks_CheckedChanged(object sender, EventArgs e)
		{
			checkEditMonths.CheckedChanged -= checkEditMonths_CheckedChanged;
			checkEditMonths.Checked = !checkEditWeeks.Checked;
			if (!ProductPackage.DurationValue.HasValue)
			{
				if (checkEditMonths.Checked)
					spinEditDuration.EditValue = ProductPackage.MonthDuraton;
				else if (checkEditWeeks.Checked)
					spinEditDuration.EditValue = ProductPackage.WeeksDuration;
			}
			SaveCheckboxValues();
			checkEditMonths.CheckedChanged += checkEditMonths_CheckedChanged;
		}

		#region Output Staff
		public void Output()
		{
			SaveValues();
			using (var formProgress = new FormProgress())
			{
				formProgress.laProgress.Text = "Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!";
				formProgress.TopMost = true;
				formProgress.Show();
				OnlineSchedulePowerPointHelper.Instance.AppendOneSheetPackage(ProductPackage);
				formProgress.Close();
			}
			using (var formOutput = new FormSlideOutput())
			{
				if (formOutput.ShowDialog() != DialogResult.OK)
					Utilities.Instance.ActivateForm(Controller.Instance.FormMain.Handle, true, false);
			}
		}

		public void Email()
		{
			SaveValues();
			using (var formProgress = new FormProgress())
			{
				formProgress.laProgress.Text = "Chill-Out for a few seconds...\nPreparing Presentation for Email...";
				formProgress.TopMost = true;
				formProgress.Show();
				string tempFileName = Path.Combine(SettingsManager.Instance.TempPath, Path.GetFileName(Path.GetTempFileName()));
				OnlineSchedulePowerPointHelper.Instance.PreparePackageEmail(tempFileName, ProductPackage);
				formProgress.Close();
				if (File.Exists(tempFileName))
					using (var formEmail = new FormEmail())
					{
						formEmail.Text = "Email this Online Schedule";
						formEmail.PresentationFile = tempFileName;
						RegistryHelper.MainFormHandle = formEmail.Handle;
						RegistryHelper.MaximizeMainForm = false;
						formEmail.ShowDialog();
						RegistryHelper.MaximizeMainForm = true;
						RegistryHelper.MainFormHandle = Controller.Instance.FormMain.Handle;
					}
			}
		}
		#endregion
	}
}