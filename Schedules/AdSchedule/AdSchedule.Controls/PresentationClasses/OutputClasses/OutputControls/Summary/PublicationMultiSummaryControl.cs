﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraTab;
using NewBizWiz.Core.AdSchedule;
using NewBizWiz.Core.Common;

namespace NewBizWiz.AdSchedule.Controls.PresentationClasses.OutputClasses.OutputControls
{
	[ToolboxItem(false)]
	public partial class PublicationMultiSummaryControl : XtraTabPage
	//public partial class PublicationMultiSummaryControl : System.Windows.Forms.UserControl
	{
		private bool _allowToSave;

		public PublicationMultiSummaryControl()
		{
			InitializeComponent();
		}

		public PrintProduct PrintProduct { get; set; }

		public bool SettingsNotSaved
		{
			get { return Controller.Instance.Summaries.MultiSummary.SettingsNotSaved; }
			set { Controller.Instance.Summaries.MultiSummary.SettingsNotSaved = value; }
		}

		private bool AllowToCheck()
		{
			int checkedNumber = 0;
			if (checkEditPageSize.Checked)
				checkedNumber++;
			if (checkEditPercentOfPage.Checked)
				checkedNumber++;
			if (checkEditAvgAdCost.Checked)
				checkedNumber++;
			if (checkEditAvgFinalCost.Checked)
				checkedNumber++;
			if (checkEditAvgPCI.Checked)
				checkedNumber++;
			if (checkEditColor.Checked)
				checkedNumber++;
			if (checkEditSquare.Checked)
				checkedNumber++;
			if (checkEditDimensions.Checked)
				checkedNumber++;
			if (checkEditDiscounts.Checked)
				checkedNumber++;
			if (checkEditMechanicals.Checked)
				checkedNumber++;
			if (checkEditSections.Checked)
				checkedNumber++;
			if (checkEditTotalAds.Checked)
				checkedNumber++;
			if (checkEditTotalSquare.Checked)
				checkedNumber++;
			return checkedNumber < 6;
		}

		public void LoadPublication()
		{
			Text = PrintProduct.Name.Replace("&", "&&");
			pbLogo.Image = PrintProduct.SmallLogo != null ? new Bitmap(PrintProduct.SmallLogo) : null;
			checkEditFlightDates.Text = PrintProduct.Parent.FlightDateStart.ToString("MM/dd/yy") + " - " + PrintProduct.Parent.FlightDateEnd.ToString("MM/dd/yy");
			checkEditName.Text = PrintProduct.Name.Replace("&", "&&");
			checkEditTotalAds.Text = "Total Ads: " + PrintProduct.TotalInserts.ToString("#,##0");
			checkEditTotalSquare.Text = PrintProduct.TotalSquare.HasValue && PrintProduct.AdPricingStrategy != AdPricingStrategies.SharePage ? ("Total Column Inches: " + PrintProduct.TotalSquare.Value.ToString("#,##0.00#")) : string.Empty;
			checkEditTotalSquare.Visible = PrintProduct.TotalSquare.HasValue && PrintProduct.AdPricingStrategy != AdPricingStrategies.SharePage;
			checkEditPageSize.Text = !string.IsNullOrEmpty(PrintProduct.SizeOptions.PageSize) ? ("Page Size: " + PrintProduct.SizeOptions.PageSize) : string.Empty;
			checkEditPageSize.Visible = !string.IsNullOrEmpty(PrintProduct.SizeOptions.PageSize);
			checkEditPercentOfPage.Text = !string.IsNullOrEmpty(PrintProduct.SizeOptions.PercentOfPage) ? (PrintProduct.SizeOptions.PercentOfPage + " Share of Page") : string.Empty;
			checkEditPercentOfPage.Visible = !string.IsNullOrEmpty(PrintProduct.SizeOptions.PercentOfPage);
			checkEditAvgAdCost.Text = "BW Avg Ad Cost: " + PrintProduct.AvgADRate.ToString("$#,##0.00");
			checkEditAvgFinalCost.Text = "Final Avg Ad Cost: " + PrintProduct.AvgFinalRate.ToString("$#,##0.00");
			checkEditAvgPCI.Text = PrintProduct.AvgPCIRate > 0 ? ("Avg PCI: " + PrintProduct.AvgPCIRate.ToString("$#,##0.00")) : string.Empty;
			checkEditAvgPCI.Visible = PrintProduct.AvgPCIRate > 0;
			checkEditSquare.Text = PrintProduct.SizeOptions.Square.HasValue && PrintProduct.AdPricingStrategy != AdPricingStrategies.SharePage ? ("Column Inches: " + PrintProduct.SizeOptions.Square.Value.ToString("#,##0.00#") + " col. in.") : string.Empty;
			checkEditSquare.Visible = PrintProduct.SizeOptions.Square.HasValue && PrintProduct.AdPricingStrategy != AdPricingStrategies.SharePage;
			checkEditDimensions.Text = !string.IsNullOrEmpty(PrintProduct.SizeOptions.Dimensions) ? ("Dimensions: " + PrintProduct.SizeOptions.Dimensions) : string.Empty;
			checkEditDimensions.Visible = !string.IsNullOrEmpty(PrintProduct.SizeOptions.Dimensions);
			checkEditDiscounts.Text = "Discounts: " + PrintProduct.TotalDiscountRate.ToString("$#,##0.00");
			labelControlSections.Text = "Sections: " + string.Join(", ", PrintProduct.Inserts.Where(x => !string.IsNullOrEmpty(x.FullSection)).Select(x => x.FullSection).Distinct());

			switch (PrintProduct.ColorOption)
			{
				case ColorOptions.BlackWhite:
					checkEditColor.Text = "Black && White";
					break;
				case ColorOptions.SpotColor:
					checkEditColor.Text = "Spot Color";
					break;
				case ColorOptions.FullColor:
					checkEditColor.Text = "Full Color";
					break;
			}
			var dates = new List<string>();
			foreach (Insert insert in PrintProduct.Inserts)
			{
				if (insert.DateObject != null)
					dates.Add(insert.Date.ToString("MM/dd/yy"));
			}
			memoEditDates.EditValue = string.Join(", ", dates.ToArray());
			laInvestment.Text = "Investment: " + PrintProduct.TotalFinalRate.ToString("$#,##0.00");

			_allowToSave = false;
			checkEditName.Checked = PrintProduct.ViewSettings.MultiSummarySettings.ShowName;
			checkEditLogo.Checked = PrintProduct.ViewSettings.MultiSummarySettings.ShowLogo;
			checkEditInvestment.Checked = PrintProduct.ViewSettings.MultiSummarySettings.ShowInvestment;

			checkEditFlightDates.Enabled = PrintProduct.ViewSettings.MultiSummarySettings.EnableFlightDates;
			checkEditComments.Enabled = PrintProduct.ViewSettings.MultiSummarySettings.EnableComments;
			checkEditDates.Enabled = PrintProduct.ViewSettings.MultiSummarySettings.EnableDates;
			checkEditFlightDates.Checked = PrintProduct.ViewSettings.MultiSummarySettings.ShowFlightDates;
			checkEditComments.Checked = PrintProduct.ViewSettings.MultiSummarySettings.ShowComments;
			checkEditDates.Checked = PrintProduct.ViewSettings.MultiSummarySettings.ShowDates;

			checkEditTotalAds.Enabled = PrintProduct.ViewSettings.MultiSummarySettings.EnableTotalInserts;
			checkEditDimensions.Enabled = PrintProduct.ViewSettings.MultiSummarySettings.EnableDimensions;
			checkEditPageSize.Enabled = PrintProduct.ViewSettings.MultiSummarySettings.EnablePageSize;
			checkEditPercentOfPage.Enabled = PrintProduct.ViewSettings.MultiSummarySettings.EnablePercentOfPage;
			checkEditColor.Enabled = PrintProduct.ViewSettings.MultiSummarySettings.EnableTotalColor;
			checkEditAvgAdCost.Enabled = PrintProduct.ViewSettings.MultiSummarySettings.EnableAvgAdCost;
			checkEditAvgFinalCost.Enabled = PrintProduct.ViewSettings.MultiSummarySettings.EnableAvgFinalCost;
			checkEditDiscounts.Enabled = PrintProduct.ViewSettings.MultiSummarySettings.EnableDiscounts;
			checkEditSections.Enabled = PrintProduct.ViewSettings.MultiSummarySettings.EnableSection;
			checkEditTotalAds.Checked = PrintProduct.ViewSettings.MultiSummarySettings.ShowTotalInserts;
			checkEditDimensions.Checked = PrintProduct.ViewSettings.MultiSummarySettings.ShowDimensions & !string.IsNullOrEmpty(PrintProduct.SizeOptions.Dimensions);
			checkEditPageSize.Checked = PrintProduct.ViewSettings.MultiSummarySettings.ShowPageSize & !string.IsNullOrEmpty(PrintProduct.SizeOptions.PageSize);
			checkEditPercentOfPage.Checked = PrintProduct.ViewSettings.MultiSummarySettings.ShowPercentOfPage & !string.IsNullOrEmpty(PrintProduct.SizeOptions.PercentOfPage);
			checkEditColor.Checked = PrintProduct.ViewSettings.MultiSummarySettings.ShowTotalColor;
			checkEditAvgAdCost.Checked = PrintProduct.ViewSettings.MultiSummarySettings.ShowAvgAdCost;
			checkEditAvgFinalCost.Checked = PrintProduct.ViewSettings.MultiSummarySettings.ShowAvgFinalCost;
			checkEditDiscounts.Checked = PrintProduct.ViewSettings.MultiSummarySettings.ShowDiscounts;
			checkEditSections.Checked = PrintProduct.ViewSettings.MultiSummarySettings.ShowSection;

			checkEditAvgPCI.Checked = PrintProduct.ViewSettings.MultiSummarySettings.ShowAvgPCI & PrintProduct.AvgPCIRate > 0;
			checkEditTotalSquare.Checked = PrintProduct.ViewSettings.MultiSummarySettings.ShowTotalSquare & PrintProduct.TotalSquare.HasValue && PrintProduct.AdPricingStrategy != AdPricingStrategies.SharePage;
			checkEditSquare.Checked = PrintProduct.ViewSettings.MultiSummarySettings.ShowSquare & PrintProduct.SizeOptions.Square.HasValue && PrintProduct.AdPricingStrategy != AdPricingStrategies.SharePage;
			checkEditMechanicals.Checked = PrintProduct.ViewSettings.MultiSummarySettings.ShowMechanicals;

			comboBoxEditInvestment.EditValue = PrintProduct.ViewSettings.MultiSummarySettings.InvestmentType;
			memoEditComments.EditValue = PrintProduct.ViewSettings.MultiSummarySettings.Comments;
			_allowToSave = true;
		}

		private void checkEditInvestment_CheckedChanged(object sender, EventArgs e)
		{
			comboBoxEditInvestment.Enabled = checkEditInvestment.Checked;
			checkEdit_CheckedChanged(null, null);
		}

		private void checkEditDates_CheckedChanged(object sender, EventArgs e)
		{
			memoEditDates.Enabled = checkEditDates.Checked;
			checkEdit_CheckedChanged(null, null);
		}

		private void checkEditComments_CheckedChanged(object sender, EventArgs e)
		{
			memoEditComments.Enabled = checkEditComments.Checked;
			checkEdit_CheckedChanged(null, null);
		}

		private void checkEditAdItems_EditValueChanging(object sender, ChangingEventArgs e)
		{
			if (_allowToSave)
			{
				if ((bool)e.NewValue)
				{
					if (!AllowToCheck())
					{
						Utilities.Instance.ShowWarning("You may select only up to 6 Ad-Items");
						e.Cancel = true;
					}
				}
			}
		}

		private void comboBoxEditInvestment_EditValueChanged(object sender, EventArgs e)
		{
			if (_allowToSave)
			{
				PrintProduct.ViewSettings.MultiSummarySettings.InvestmentType = comboBoxEditInvestment.EditValue != null ? comboBoxEditInvestment.EditValue.ToString() : string.Empty;
				SettingsNotSaved = true;
			}
		}

		private void memoEditComments_EditValueChanged(object sender, EventArgs e)
		{
			if (_allowToSave)
			{
				PrintProduct.ViewSettings.MultiSummarySettings.Comments = memoEditComments.EditValue != null ? memoEditComments.EditValue.ToString() : string.Empty;
				SettingsNotSaved = true;
			}
		}

		private void checkEdit_CheckedChanged(object sender, EventArgs e)
		{
			if (_allowToSave)
			{
				PrintProduct.ViewSettings.MultiSummarySettings.ShowPageSize = checkEditPageSize.Checked;
				PrintProduct.ViewSettings.MultiSummarySettings.ShowPercentOfPage = checkEditPercentOfPage.Checked;
				PrintProduct.ViewSettings.MultiSummarySettings.ShowAvgAdCost = checkEditAvgAdCost.Checked;
				PrintProduct.ViewSettings.MultiSummarySettings.ShowAvgFinalCost = checkEditAvgFinalCost.Checked;
				PrintProduct.ViewSettings.MultiSummarySettings.ShowAvgPCI = checkEditAvgPCI.Checked;
				PrintProduct.ViewSettings.MultiSummarySettings.ShowSquare = checkEditSquare.Checked;
				PrintProduct.ViewSettings.MultiSummarySettings.ShowComments = checkEditComments.Checked;
				PrintProduct.ViewSettings.MultiSummarySettings.ShowDates = checkEditDates.Checked;
				PrintProduct.ViewSettings.MultiSummarySettings.ShowDimensions = checkEditDimensions.Checked;
				PrintProduct.ViewSettings.MultiSummarySettings.ShowDiscounts = checkEditDiscounts.Checked;
				PrintProduct.ViewSettings.MultiSummarySettings.ShowFlightDates = checkEditFlightDates.Checked;
				PrintProduct.ViewSettings.MultiSummarySettings.ShowInvestment = checkEditInvestment.Checked;
				PrintProduct.ViewSettings.MultiSummarySettings.ShowLogo = checkEditLogo.Checked;
				PrintProduct.ViewSettings.MultiSummarySettings.ShowMechanicals = checkEditMechanicals.Checked;
				PrintProduct.ViewSettings.MultiSummarySettings.ShowName = checkEditName.Checked;
				PrintProduct.ViewSettings.MultiSummarySettings.ShowSection = checkEditSections.Checked;
				PrintProduct.ViewSettings.MultiSummarySettings.ShowTotalColor = checkEditColor.Checked;
				PrintProduct.ViewSettings.MultiSummarySettings.ShowTotalInserts = checkEditTotalAds.Checked;
				PrintProduct.ViewSettings.MultiSummarySettings.ShowTotalSquare = checkEditTotalSquare.Checked;
				SettingsNotSaved = true;
			}
		}

		private void checkEdit_MouseDown(object sender, MouseEventArgs e)
		{
			var cEdit = (CheckEdit)sender;
			var cInfo = (CheckEditViewInfo)cEdit.GetViewInfo();
			Rectangle r = cInfo.CheckInfo.GlyphRect;
			var editorRect = new Rectangle(new Point(0, 0), cEdit.Size);
			if (!r.Contains(e.Location) && editorRect.Contains(e.Location))
				((DXMouseEventArgs)e).Handled = true;
		}
	}
}