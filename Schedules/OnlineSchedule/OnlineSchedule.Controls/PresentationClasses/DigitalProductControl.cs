using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraTab;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.OnlineSchedule;
using NewBizWiz.OnlineSchedule.Controls.BusinessClasses;
using NewBizWiz.OnlineSchedule.Controls.InteropClasses;
using NewBizWiz.OnlineSchedule.Controls.Properties;
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
				var font = new Font(labelControlWebsiteLogo.Font.FontFamily, labelControlWebsiteLogo.Font.Size - 4, labelControlWebsiteLogo.Font.Style);
				labelControlWebsiteLogo.Font = font;
				labelControlComments.Font = font;
				labelControlAdCampaign.Font = font;
				labelControlImpressions.Font = font;
				labelControlInvestment.Font = font;
				labelControlDimensions.Font = new Font(labelControlDimensions.Font.FontFamily, labelControlDimensions.Font.Size - 3, labelControlDimensions.Font.Style);
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
				labelControlBusinessName.Font = new Font(labelControlBusinessName.Font.FontFamily, labelControlBusinessName.Font.Size - 3, labelControlBusinessName.Font.Style);
				labelControlPresentationDate.Font = new Font(labelControlPresentationDate.Font.FontFamily, labelControlPresentationDate.Font.Size - 3, labelControlPresentationDate.Font.Style);
				labelControlFormula.Font = new Font(labelControlFormula.Font.FontFamily, labelControlFormula.Font.Size - 2, labelControlFormula.Font.Style);
				checkEditFormulaCPM.Font = new Font(checkEditFormulaCPM.Font.FontFamily, checkEditFormulaCPM.Font.Size - 2, checkEditFormulaCPM.Font.Style);
				checkEditFormulaImpressions.Font = new Font(checkEditFormulaImpressions.Font.FontFamily, checkEditFormulaImpressions.Font.Size - 2, checkEditFormulaImpressions.Font.Style);
				checkEditFormulaInvestment.Font = new Font(checkEditFormulaInvestment.Font.FontFamily, checkEditFormulaInvestment.Font.Size - 2, checkEditFormulaInvestment.Font.Style);
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
			textEditCustomWebsite3.Enter += Utilities.Instance.Editor_Enter;
			textEditCustomWebsite3.MouseDown += Utilities.Instance.Editor_MouseDown;
			textEditCustomWebsite3.MouseUp += Utilities.Instance.Editor_MouseUp;
			textEditCustomWebsite4.Enter += Utilities.Instance.Editor_Enter;
			textEditCustomWebsite4.MouseDown += Utilities.Instance.Editor_MouseDown;
			textEditCustomWebsite4.MouseUp += Utilities.Instance.Editor_MouseUp;
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
					Product.MonthlyCPM = spinEditMonthlyCPM.EditValue != null && !(Product.RateType == RateType.CPM && Product.DefaultRate.HasValue && (double)spinEditMonthlyCPM.Value == Product.DefaultRate) ? (double?)spinEditMonthlyCPM.Value : null;

					spinEditMonthlyInvestment.EditValue = Product.MonthlyInvestmentCalculated;
					break;
				case FormulaType.Impressions:
					Product.MonthlyInvestment = spinEditMonthlyInvestment.EditValue != null ? (double?)spinEditMonthlyInvestment.Value : null;
					Product.MonthlyCPM = spinEditMonthlyCPM.EditValue != null && !(Product.RateType == RateType.CPM && Product.DefaultRate.HasValue && (double)spinEditMonthlyCPM.Value == Product.DefaultRate) ? (double?)spinEditMonthlyCPM.Value : null;

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
					Product.TotalCPM = spinEditTotalCPM.EditValue != null && !(Product.RateType == RateType.CPM && Product.DefaultRate.HasValue && (double)spinEditTotalCPM.Value == Product.DefaultRate) ? (double?)spinEditTotalCPM.Value : null;

					spinEditTotalInvestment.EditValue = Product.TotalInvestmentCalculated;
					break;
				case FormulaType.Impressions:
					Product.TotalInvestment = spinEditTotalInvestment.EditValue != null ? (double?)spinEditTotalInvestment.Value : null;
					Product.TotalCPM = spinEditTotalCPM.EditValue != null && !(Product.RateType == RateType.CPM && Product.DefaultRate.HasValue && (double)spinEditTotalCPM.Value == Product.DefaultRate) ? (double?)spinEditTotalCPM.Value : null;

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
			if (!string.IsNullOrEmpty(Product.SlideHeader))
				_container.comboBoxEditSlideHeader.EditValue = Product.SlideHeader;
			else
				_container.comboBoxEditSlideHeader.EditValue = null;
			labelControlPresentationDate.Text = "Presentation Date:\n" + Product.Parent.PresentationDate.ToString("MM/dd/yy");

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

			checkedListBoxControlWebsite.UnCheckAll();
			foreach (CheckedListBoxItem item in checkedListBoxControlWebsite.Items)
				if (Product.Websites.Contains(item.Value.ToString()))
					item.CheckState = CheckState.Checked;
			checkEditCustomWebsite1.Checked = Product.ShowCustomWebsite1;
			textEditCustomWebsite1.EditValue = Product.CustomWebsite1;
			checkEditCustomWebsite2.Checked = Product.ShowCustomWebsite2;
			textEditCustomWebsite2.EditValue = Product.CustomWebsite2;
			checkEditCustomWebsite3.Checked = Product.ShowCustomWebsite3;
			checkEditCustomWebsite4.Checked = Product.ShowCustomWebsite4;
			textEditCustomWebsite4.EditValue = Product.CustomWebsite4;
			textEditCustomWebsite3.EditValue = Product.CustomWebsite3;
			labelControlDimensions.Text = "Ad Dimensions: " + Product.Dimensions;
			memoEditDescription.EditValue = Product.Description;
			spinEditAdRate.EditValue = Product.AdRate;
			spinEditMonthlyInvestment.EditValue = Product.MonthlyInvestmentCalculated;
			spinEditTotalInvestment.EditValue = Product.TotalInvestmentCalculated;
			spinEditMonthlyImpressions.EditValue = Product.MonthlyImpressionsCalculated;
			spinEditTotalImpressions.EditValue = Product.TotalImpressionsCalculated;
			spinEditMonthlyCPM.EditValue = Product.MonthlyCPMCalculated;
			spinEditTotalCPM.EditValue = Product.TotalCPMCalculated;
			labelControlFlightDates.Text = Product.Parent.FlightDates;
			checkEditDuration.Checked = Product.ShowDuration;
			switch (Product.DurationType)
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
			if (Product.DurationValue.HasValue)
			{
				spinEditDuration.EditValue = Product.DurationValue;
			}
			else
			{
				if (checkEditMonths.Checked)
					spinEditDuration.EditValue = Product.MonthDuraton;
				else if (checkEditWeeks.Checked)
					spinEditDuration.EditValue = Product.WeeksDuration;
			}
			spinEditActiveDays.EditValue = Product.ActiveDays;
			spinEditTotalAds.EditValue = Product.TotalAds;
			checkEditComments.Checked = Product.ShowCommentText;
			memoEditComments.EditValue = Product.Comment;
			checkEditStrengths1.Checked = Product.ShowStrength1;
			comboBoxEditStrengths1.EditValue = Product.Strength1;
			checkEditStrengths2.Checked = Product.ShowStrength2;
			comboBoxEditStrengths2.EditValue = Product.Strength2;

			UpdateView();
			UpdateDefaultCPM();
			_allowToSave = true;
		}

		public void UpdateDefaultCPM()
		{
			if (Product.RateType == RateType.CPM && Product.Formula != FormulaType.CPM)
			{
				spinEditMonthlyCPM.EditValue = !Product.MonthlyCPM.HasValue ? Product.DefaultRate : Product.MonthlyCPM;
				spinEditTotalCPM.EditValue = !Product.TotalCPM.HasValue ? Product.DefaultRate : Product.TotalCPM;
			}
		}

		public void UpdateView()
		{
			labelControlPresentationDate.Visible = Product.ShowPresentationDate;
			if (Product.ShowBusinessName && Product.ShowDecisionMaker)
			{
				labelControlBusinessName.Visible = true;
				labelControlBusinessName.Text = "Prepared For: " + Product.Parent.BusinessName + "\n" + Product.Parent.DecisionMaker;
			}
			else if (!Product.ShowBusinessName && Product.ShowDecisionMaker)
			{
				labelControlBusinessName.Visible = true;
				labelControlBusinessName.Text = Product.Parent.DecisionMaker;
			}
			else if (Product.ShowBusinessName && !Product.ShowDecisionMaker)
			{
				labelControlBusinessName.Visible = true;
				labelControlBusinessName.Text = "Prepared For: " + Product.Parent.BusinessName;
			}
			else
			{
				labelControlBusinessName.Visible = false;
			}

			pbProductLogo.Enabled = Product.ShowProduct;
			pbProductLogo.Image = pbProductLogo.Enabled ? Resources.ProductLogo : Resources.ProductLogoDisabled;
			memoEditProductName.Enabled = Product.ShowProduct;
			hyperLinkEditResetProductName.Visible = !(memoEditProductName.EditValue != null && memoEditProductName.EditValue.ToString().Equals(Product.ExtendedName));

			pbDimensions.Enabled = Product.ShowDimensions | Product.ShowDescription;
			pbDimensions.Image = pbDimensions.Enabled ? Resources.Dimensions : Resources.DimensionsDisabled;
			labelControlDimensions.Enabled = Product.ShowDimensions;
			memoEditDescription.Enabled = Product.ShowDescription;

			if (!Product.ShowCPMButton)
			{
				Product.Formula = FormulaType.CPM;
				_allowToSave = false;
				checkEditFormulaCPM.Checked = true;
				checkEditFormulaInvestment.Checked = false;
				checkEditFormulaImpressions.Checked = false;
				_allowToSave = true;
			}
			else
			{
				if (checkEditFormulaCPM.Checked)
					Product.Formula = FormulaType.CPM;
				else if (checkEditFormulaInvestment.Checked)
					Product.Formula = FormulaType.Investment;
				else if (checkEditFormulaImpressions.Checked)
					Product.Formula = FormulaType.Impressions;
			}

			pbAdCampaign.Enabled = Product.ShowFlightDates | Product.ShowActiveDays | Product.ShowTotalAds;
			pbAdCampaign.Image = pbAdCampaign.Enabled ? Resources.AdCampaignLogo : Resources.AdCampaignLogoDisabled;
			labelControlAdCampaign.Enabled = Product.ShowFlightDates | Product.ShowActiveDays | Product.ShowTotalAds;
			labelControlFlightDates.Enabled = Product.ShowFlightDates;
			checkEditDuration.Enabled = Product.ShowFlightDates;
			spinEditDuration.Enabled = Product.ShowFlightDates;
			checkEditMonths.Enabled = Product.ShowFlightDates;
			checkEditWeeks.Enabled = Product.ShowFlightDates;
			labelControlAdRate.Enabled = Product.ShowAdRate;
			spinEditAdRate.Enabled = Product.ShowAdRate;
			labelControlTotalAds.Enabled = Product.ShowTotalAds;
			spinEditTotalAds.Enabled = Product.ShowTotalAds;
			labelControlActiveDays.Enabled = Product.ShowActiveDays;
			spinEditActiveDays.Enabled = Product.ShowActiveDays;

			pbCommentsLogo.Enabled = Product.ShowComments;
			pbCommentsLogo.Image = pbCommentsLogo.Enabled ? Resources.CommentsLogo : Resources.CommentsLogoDisabled;
			labelControlComments.Enabled = Product.ShowComments;
			checkEditStrengths1.Enabled = Product.ShowComments;
			comboBoxEditStrengths1.Enabled = checkEditStrengths1.Checked && Product.ShowComments;
			checkEditStrengths2.Enabled = Product.ShowComments;
			comboBoxEditStrengths2.Enabled = checkEditStrengths2.Checked && Product.ShowComments;
			checkEditComments.Enabled = Product.ShowComments;
			memoEditComments.Enabled = checkEditComments.Checked && Product.ShowComments;

			pbWebsites.Enabled = pbOtherWebsites.Enabled = Product.ShowWebsite;
			pbWebsites.Image = pbOtherWebsites.Image = pbWebsites.Enabled ? Resources.Websites : Resources.WebsitesDisabled;
			labelControlWebsiteDetails.Enabled = Product.ShowWebsite;
			labelControlWebsiteLogo.Enabled = Product.ShowWebsite;
			labelControlOtherWebsitesLogo.Enabled = Product.ShowWebsite;
			checkedListBoxControlWebsite.Enabled = Product.ShowWebsite;
			checkEditCustomWebsite1.Enabled = Product.ShowWebsite;
			textEditCustomWebsite1.Enabled = checkEditCustomWebsite1.Checked && Product.ShowWebsite;
			checkEditCustomWebsite2.Enabled = Product.ShowWebsite;
			textEditCustomWebsite2.Enabled = checkEditCustomWebsite2.Checked && Product.ShowWebsite;
			checkEditCustomWebsite3.Enabled = Product.ShowWebsite;
			textEditCustomWebsite3.Enabled = checkEditCustomWebsite3.Checked && Product.ShowWebsite;
			checkEditCustomWebsite4.Enabled = Product.ShowWebsite;
			textEditCustomWebsite4.Enabled = checkEditCustomWebsite4.Checked && Product.ShowWebsite;

			pbFormula.Enabled = Product.ShowCPMButton;
			pbFormula.Image = pbFormula.Enabled ? Resources.FormulaLogo : Resources.FormulaLogoDisabled;
			labelControlFormula.Enabled = Product.ShowCPMButton;
			checkEditFormulaCPM.Enabled = Product.ShowCPMButton;
			checkEditFormulaInvestment.Enabled = Product.ShowCPMButton;
			checkEditFormulaImpressions.Enabled = Product.ShowCPMButton;

			UpdateFormulaComponents();
		}

		private void UpdateFormulaComponents()
		{
			if (checkEditFormulaCPM.Checked)
			{
				Product.Formula = FormulaType.CPM;
				labelControlMonthlyCPM.Enabled = false;
				spinEditMonthlyCPM.Enabled = false;
				labelControlTotalCPM.Enabled = false;
				spinEditTotalCPM.Enabled = false;
				if (Product.ShowCPMButton)
				{
					labelControlMonthlyCPM.Appearance.ForeColor = Color.Black;
					labelControlMonthlyCPM.Appearance.Options.UseForeColor = true;

					spinEditMonthlyCPM.Properties.AppearanceDisabled.ForeColor = Color.Black;
					spinEditMonthlyCPM.Properties.AppearanceDisabled.Options.UseForeColor = true;

					labelControlTotalCPM.Appearance.ForeColor = Color.Black;
					labelControlTotalCPM.Appearance.Options.UseForeColor = true;

					spinEditTotalCPM.Properties.AppearanceDisabled.ForeColor = Color.Black;
					spinEditTotalCPM.Properties.AppearanceDisabled.Options.UseForeColor = true;
				}
				else
				{
					labelControlMonthlyCPM.Appearance.Options.UseForeColor = false;
					spinEditMonthlyCPM.Properties.AppearanceDisabled.Options.UseForeColor = false;
					labelControlTotalCPM.Appearance.Options.UseForeColor = false;
					spinEditTotalCPM.Properties.AppearanceDisabled.Options.UseForeColor = false;
				}

				labelControlMonthlyInvestment.Enabled = Product.ShowMonthlyInvestment;
				labelControlMonthlyInvestment.Appearance.Options.UseForeColor = false;
				spinEditMonthlyInvestment.Enabled = Product.ShowMonthlyInvestment;
				spinEditMonthlyInvestment.Properties.AppearanceDisabled.Options.UseForeColor = false;
				labelControlTotalInvestment.Enabled = Product.ShowTotalInvestment;
				labelControlTotalInvestment.Appearance.Options.UseForeColor = false;
				spinEditTotalInvestment.Enabled = Product.ShowTotalInvestment;
				spinEditTotalInvestment.Properties.AppearanceDisabled.Options.UseForeColor = false;

				labelControlMonthlyImpressions.Enabled = Product.ShowMonthlyImpressions;
				labelControlMonthlyImpressions.Appearance.Options.UseForeColor = false;
				spinEditMonthlyImpressions.Enabled = Product.ShowMonthlyImpressions;
				spinEditMonthlyImpressions.Properties.AppearanceDisabled.Options.UseForeColor = false;
				labelControlTotalImpressions.Enabled = Product.ShowTotalImpressions;
				labelControlTotalImpressions.Appearance.Options.UseForeColor = false;
				spinEditTotalImpressions.Enabled = Product.ShowTotalImpressions;
				spinEditTotalImpressions.Properties.AppearanceDisabled.Options.UseForeColor = false;
			}
			else if (checkEditFormulaInvestment.Checked)
			{
				Product.Formula = FormulaType.Investment;

				labelControlMonthlyCPM.Enabled = Product.ShowMonthlyCPM;
				labelControlMonthlyCPM.Appearance.Options.UseForeColor = false;
				spinEditMonthlyCPM.Enabled = Product.ShowMonthlyCPM;
				spinEditMonthlyCPM.Properties.AppearanceDisabled.Options.UseForeColor = false;
				labelControlTotalCPM.Enabled = Product.ShowTotalCPM;
				labelControlTotalCPM.Appearance.Options.UseForeColor = false;
				spinEditTotalCPM.Enabled = Product.ShowTotalCPM;
				spinEditTotalCPM.Properties.AppearanceDisabled.Options.UseForeColor = false;

				labelControlMonthlyInvestment.Enabled = false;
				spinEditMonthlyInvestment.Enabled = false;
				labelControlTotalInvestment.Enabled = false;
				spinEditTotalInvestment.Enabled = false;
				if (Product.ShowMonthlyInvestment)
				{
					labelControlMonthlyInvestment.Appearance.ForeColor = Color.Black;
					labelControlMonthlyInvestment.Appearance.Options.UseForeColor = true;

					spinEditMonthlyInvestment.Properties.AppearanceDisabled.ForeColor = Color.Black;
					spinEditMonthlyInvestment.Properties.AppearanceDisabled.Options.UseForeColor = true;
				}
				else
				{
					labelControlMonthlyInvestment.Appearance.Options.UseForeColor = false;
					spinEditMonthlyInvestment.Properties.AppearanceDisabled.Options.UseForeColor = false;
				}

				if (Product.ShowTotalInvestment)
				{
					labelControlTotalInvestment.Appearance.ForeColor = Color.Black;
					labelControlTotalInvestment.Appearance.Options.UseForeColor = true;

					spinEditTotalInvestment.Properties.AppearanceDisabled.ForeColor = Color.Black;
					spinEditTotalInvestment.Properties.AppearanceDisabled.Options.UseForeColor = true;
				}
				else
				{
					labelControlTotalInvestment.Appearance.Options.UseForeColor = false;
					spinEditTotalInvestment.Properties.AppearanceDisabled.Options.UseForeColor = false;
				}

				labelControlMonthlyImpressions.Enabled = Product.ShowMonthlyImpressions;
				labelControlMonthlyImpressions.Appearance.Options.UseForeColor = false;
				spinEditMonthlyImpressions.Enabled = Product.ShowMonthlyImpressions;
				spinEditMonthlyImpressions.Properties.AppearanceDisabled.Options.UseForeColor = false;
				labelControlTotalImpressions.Enabled = Product.ShowTotalImpressions;
				labelControlTotalImpressions.Appearance.Options.UseForeColor = false;
				spinEditTotalImpressions.Enabled = Product.ShowTotalImpressions;
				spinEditTotalImpressions.Properties.AppearanceDisabled.Options.UseForeColor = false;
			}
			else if (checkEditFormulaImpressions.Checked)
			{
				Product.Formula = FormulaType.Impressions;

				labelControlMonthlyCPM.Enabled = Product.ShowMonthlyCPM;
				labelControlMonthlyCPM.Appearance.Options.UseForeColor = false;
				spinEditMonthlyCPM.Enabled = Product.ShowMonthlyCPM;
				spinEditMonthlyCPM.Properties.AppearanceDisabled.Options.UseForeColor = false;
				labelControlTotalCPM.Enabled = Product.ShowTotalCPM;
				labelControlTotalCPM.Appearance.Options.UseForeColor = false;
				spinEditTotalCPM.Enabled = Product.ShowTotalCPM;
				spinEditTotalCPM.Properties.AppearanceDisabled.Options.UseForeColor = false;

				labelControlMonthlyInvestment.Enabled = Product.ShowMonthlyInvestment;
				labelControlMonthlyInvestment.Appearance.Options.UseForeColor = false;
				spinEditMonthlyInvestment.Enabled = Product.ShowMonthlyInvestment;
				spinEditMonthlyInvestment.Properties.AppearanceDisabled.Options.UseForeColor = false;
				labelControlTotalInvestment.Enabled = Product.ShowTotalInvestment;
				labelControlTotalInvestment.Appearance.Options.UseForeColor = false;
				spinEditTotalInvestment.Enabled = Product.ShowTotalInvestment;
				spinEditTotalInvestment.Properties.AppearanceDisabled.Options.UseForeColor = false;

				labelControlMonthlyImpressions.Enabled = false;
				spinEditMonthlyImpressions.Enabled = false;
				labelControlTotalImpressions.Enabled = false;
				spinEditTotalImpressions.Enabled = false;
				if (Product.ShowMonthlyImpressions)
				{
					labelControlMonthlyImpressions.Appearance.ForeColor = Color.Black;
					labelControlMonthlyImpressions.Appearance.Options.UseForeColor = true;

					spinEditMonthlyImpressions.Properties.AppearanceDisabled.ForeColor = Color.Black;
					spinEditMonthlyImpressions.Properties.AppearanceDisabled.Options.UseForeColor = true;
				}
				else
				{
					labelControlMonthlyImpressions.Appearance.Options.UseForeColor = false;
					spinEditMonthlyImpressions.Properties.AppearanceDisabled.Options.UseForeColor = false;
				}

				if (Product.ShowTotalImpressions)
				{
					labelControlTotalImpressions.Appearance.ForeColor = Color.Black;
					labelControlTotalImpressions.Appearance.Options.UseForeColor = true;

					spinEditTotalImpressions.Properties.AppearanceDisabled.ForeColor = Color.Black;
					spinEditTotalImpressions.Properties.AppearanceDisabled.Options.UseForeColor = true;
				}
				else
				{
					labelControlTotalImpressions.Appearance.Options.UseForeColor = false;
					spinEditTotalImpressions.Properties.AppearanceDisabled.Options.UseForeColor = false;
				}
			}
			pbInvestmentLogo.Enabled = Product.ShowAdRate | Product.ShowMonthlyInvestment | Product.ShowTotalInvestment;
			pbInvestmentLogo.Image = pbInvestmentLogo.Enabled ? Resources.InvestmentLogo : Resources.InvestmentLogoDisabled;
			labelControlInvestment.Enabled = Product.ShowAdRate | Product.ShowMonthlyInvestment | Product.ShowTotalInvestment;

			pbImpressionsLogo.Enabled = Product.ShowCPMButton | Product.ShowMonthlyImpressions | Product.ShowTotalImpressions;
			pbImpressionsLogo.Image = pbImpressionsLogo.Enabled ? Resources.ImpressionsLogo : Resources.ImpressionsLogoDisabled;
			labelControlImpressions.Enabled = Product.ShowCPMButton | Product.ShowMonthlyImpressions | Product.ShowTotalImpressions;
		}

		public void SaveValues()
		{
			if (_allowToSave)
			{
				Product.UserDefinedName = memoEditProductName.EditValue != null ? memoEditProductName.EditValue.ToString() : Product.ExtendedName;
				Product.SlideHeader = _container.comboBoxEditSlideHeader.EditValue != null ? _container.comboBoxEditSlideHeader.EditValue.ToString() : (_container.comboBoxEditSlideHeader.Properties.Items.Count > 0 ? _container.comboBoxEditSlideHeader.Properties.Items[0].ToString() : string.Empty);
				Product.Websites.Clear();
				if (_container.Websites.Checked)
					foreach (CheckedListBoxItem item in checkedListBoxControlWebsite.Items)
						if (item.CheckState == CheckState.Checked)
							Product.Websites.Add(item.Value.ToString());
				Product.CustomWebsite1 = textEditCustomWebsite1.EditValue != null ? textEditCustomWebsite1.EditValue.ToString() : string.Empty;
				Product.CustomWebsite2 = textEditCustomWebsite2.EditValue != null ? textEditCustomWebsite2.EditValue.ToString() : string.Empty;
				Product.CustomWebsite3 = textEditCustomWebsite3.EditValue != null ? textEditCustomWebsite3.EditValue.ToString() : string.Empty;
				Product.CustomWebsite4 = textEditCustomWebsite2.EditValue != null ? textEditCustomWebsite4.EditValue.ToString() : string.Empty;
				Product.Description = memoEditDescription.EditValue != null ? memoEditDescription.EditValue.ToString() : string.Empty;
				Product.AdRate = spinEditAdRate.EditValue != null ? (double?)spinEditAdRate.Value : null;
				Product.MonthlyInvestment = spinEditMonthlyInvestment.EditValue != null ? (double?)spinEditMonthlyInvestment.Value : null;
				Product.TotalInvestment = spinEditTotalInvestment.EditValue != null ? (double?)spinEditTotalInvestment.Value : null;
				Product.MonthlyImpressions = spinEditMonthlyImpressions.EditValue != null ? (double?)spinEditMonthlyImpressions.Value : null;
				Product.TotalImpressions = spinEditTotalImpressions.EditValue != null ? (double?)spinEditTotalImpressions.Value : null;
				Product.MonthlyCPM = spinEditMonthlyCPM.EditValue != null && !(Product.RateType == RateType.CPM && Product.DefaultRate.HasValue && (double)spinEditMonthlyCPM.Value == Product.DefaultRate) ? (double?)spinEditMonthlyCPM.Value : null;
				Product.TotalCPM = spinEditTotalCPM.EditValue != null && !(Product.RateType == RateType.CPM && Product.DefaultRate.HasValue && (double)spinEditTotalCPM.Value == Product.DefaultRate) ? (double?)spinEditTotalCPM.Value : null;
				Product.DurationValue = spinEditDuration.EditValue != null ? (int?)spinEditDuration.Value : null;
				Product.ActiveDays = spinEditActiveDays.EditValue != null ? (int?)spinEditActiveDays.Value : null;
				Product.TotalAds = spinEditTotalAds.EditValue != null ? (int?)spinEditTotalAds.Value : null;
				Product.Comment = memoEditComments.EditValue != null ? memoEditComments.EditValue.ToString() : string.Empty;
				Product.Strength1 = comboBoxEditStrengths1.EditValue != null ? comboBoxEditStrengths1.EditValue.ToString() : string.Empty;
				Product.Strength2 = comboBoxEditStrengths2.EditValue != null ? comboBoxEditStrengths2.EditValue.ToString() : string.Empty;
				SaveCheckboxValues();
			}
		}

		public void SaveCheckboxValues()
		{
			if (_allowToSave)
			{
				Product.ShowCustomWebsite1 = checkEditCustomWebsite1.Checked;
				Product.ShowCustomWebsite2 = checkEditCustomWebsite2.Checked;
				Product.ShowCustomWebsite3 = checkEditCustomWebsite3.Checked;
				Product.ShowCustomWebsite4 = checkEditCustomWebsite4.Checked;
				Product.ShowDuration = checkEditDuration.Checked;
				if (checkEditMonths.Checked)
					Product.DurationType = "Months";
				else if (checkEditWeeks.Checked)
					Product.DurationType = "Weeks";
				Product.ShowCommentText = checkEditComments.Checked;
				Product.ShowStrength1 = checkEditStrengths1.Checked;
				Product.ShowStrength2 = checkEditStrengths2.Checked;
			}
		}

		private void checkEditFormula_CheckedChanged(object sender, EventArgs e)
		{
			UpdateFormulaComponents();
			if (_allowToSave)
				_container.SettingsNotSaved = true;
		}

		private void ckTotalMonth_CheckedChanged(object sender, EventArgs e)
		{
			spinEditDuration.Enabled = checkEditDuration.Checked;
			checkEditMonths.Enabled = checkEditDuration.Checked;
			checkEditWeeks.Enabled = checkEditDuration.Checked;
			SaveCheckboxValues();
			UpdateOutputStatus();
			_container.SettingsNotSaved = true;
		}

		private void checkEditStrengths1_CheckedChanged(object sender, EventArgs e)
		{
			comboBoxEditStrengths1.Enabled = checkEditStrengths1.Checked;
			SaveCheckboxValues();
			_container.SettingsNotSaved = true;
		}

		private void checkEditStrengths2_CheckedChanged(object sender, EventArgs e)
		{
			comboBoxEditStrengths2.Enabled = checkEditStrengths2.Checked;
			SaveCheckboxValues();
			_container.SettingsNotSaved = true;
		}

		private void checkEditComments_CheckedChanged(object sender, EventArgs e)
		{
			memoEditComments.Enabled = checkEditComments.Checked;
			SaveCheckboxValues();
			_container.SettingsNotSaved = true;
		}

		public void WebsiteCheckedChanged()
		{
			checkedListBoxControlWebsite.Enabled = _container.Websites.Checked;
			if (!_container.Websites.Checked)
			{
				checkedListBoxControlWebsite.UnCheckAll();
				checkEditCustomWebsite1.Checked = _container.Websites.Checked;
				checkEditCustomWebsite2.Checked = _container.Websites.Checked;
				checkEditCustomWebsite3.Enabled = _container.Websites.Checked;
				checkEditCustomWebsite4.Enabled = _container.Websites.Checked;
			}
			UpdateOutputStatus();
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

		private void checkEditCustomWebsite3_CheckedChanged(object sender, EventArgs e)
		{
			textEditCustomWebsite3.Enabled = checkEditCustomWebsite3.Checked;
			SaveCheckboxValues();
		}

		private void checkEditCustomWebsite4_CheckedChanged(object sender, EventArgs e)
		{
			textEditCustomWebsite4.Enabled = checkEditCustomWebsite4.Checked;
			SaveCheckboxValues();
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

		private void checkEditMonths_CheckedChanged(object sender, EventArgs e)
		{
			checkEditWeeks.CheckedChanged -= checkEditWeeks_CheckedChanged;
			checkEditWeeks.Checked = !checkEditMonths.Checked;
			if (!Product.DurationValue.HasValue)
			{
				if (checkEditMonths.Checked)
					spinEditDuration.EditValue = Product.MonthDuraton;
				else if (checkEditWeeks.Checked)
					spinEditDuration.EditValue = Product.WeeksDuration;
			}
			SaveCheckboxValues();
			checkEditWeeks.CheckedChanged += checkEditWeeks_CheckedChanged;
		}

		private void checkEditWeeks_CheckedChanged(object sender, EventArgs e)
		{
			checkEditMonths.CheckedChanged -= checkEditMonths_CheckedChanged;
			checkEditMonths.Checked = !checkEditWeeks.Checked;
			if (!Product.DurationValue.HasValue)
			{
				if (checkEditMonths.Checked)
					spinEditDuration.EditValue = Product.MonthDuraton;
				else if (checkEditWeeks.Checked)
					spinEditDuration.EditValue = Product.WeeksDuration;
			}
			SaveCheckboxValues();
			checkEditMonths.CheckedChanged += checkEditMonths_CheckedChanged;
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
				hyperLinkEditResetProductName.Visible = Product.ShowProduct;
			}
			if (_allowToSave)
				_container.SettingsNotSaved = true;
		}

		public void ResetProductName(object sender, OpenLinkEventArgs e)
		{
			e.Handled = true;
			memoEditProductName.EditValue = Product.ExtendedName;
		}

		#region Output Staff
		public void UpdateOutputStatus()
		{
			string templateName = Product.GetSlideSource(BusinessWrapper.Instance.OutputManager.OneSheetsTemplatesFolderPath);
			if (_container.Preview != null)
				_container.Preview.Enabled = !string.IsNullOrEmpty(templateName);
			_container.PowerPoint.Enabled = !string.IsNullOrEmpty(templateName);
			_container.Email.Enabled = !string.IsNullOrEmpty(templateName);
			_container.Theme.Enabled = !string.IsNullOrEmpty(templateName);
			if (!string.IsNullOrEmpty(templateName))
			{
				_container.labelControlOutputStatus.ForeColor = Color.Green;
				_container.labelControlOutputStatus.Text = "Output enabled";
				_container.pbOutputHelp.Image = Resources.HelpSmallGreen;
				_container.pbOutputHelp.Text = "enabled";
			}
			else
			{
				_container.labelControlOutputStatus.ForeColor = Color.DarkRed;
				_container.labelControlOutputStatus.Text = "Output  disabled";
				_container.pbOutputHelp.Image = Resources.HelpSmallRed;
				_container.pbOutputHelp.Text = "disabled";
			}
		}

		public void Output()
		{
			SaveValues();
			OnlineSchedulePowerPointHelper.Instance.AppendOneSheet(new[] { Product }, _container.SelectedTheme);
		}
		#endregion
	}
}
