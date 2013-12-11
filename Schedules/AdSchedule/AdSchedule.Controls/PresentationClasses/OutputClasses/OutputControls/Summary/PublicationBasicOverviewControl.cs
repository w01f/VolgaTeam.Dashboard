using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraTab;
using NewBizWiz.AdSchedule.Controls.BusinessClasses;
using NewBizWiz.AdSchedule.Controls.InteropClasses;
using NewBizWiz.Core.AdSchedule;
using NewBizWiz.Core.Common;
using ListManager = NewBizWiz.Core.AdSchedule.ListManager;

namespace NewBizWiz.AdSchedule.Controls.PresentationClasses.OutputClasses.OutputControls
{
	public partial class PublicationBasicOverviewControl : XtraTabPage
	//public partial class PublicationBasicOverviewControl : System.Windows.Forms.UserControl
	{
		private bool _allowToSave;

		public PublicationBasicOverviewControl()
		{
			InitializeComponent();
			if ((base.CreateGraphics()).DpiX > 96)
			{
				laAdSize.Font = new Font(laAdSize.Font.FontFamily, laAdSize.Font.Size - 3, laAdSize.Font.Style);
				laDates.Font = new Font(laDates.Font.FontFamily, laDates.Font.Size - 3, laDates.Font.Style);
				laInvestmentDetails.Font = new Font(laInvestmentDetails.Font.FontFamily, laInvestmentDetails.Font.Size - 3, laInvestmentDetails.Font.Style);
				laTotalAds.Font = new Font(laTotalAds.Font.FontFamily, laTotalAds.Font.Size - 3, laTotalAds.Font.Style);
				checkEditAvgADRate.Font = new Font(checkEditAvgADRate.Font.FontFamily, checkEditAvgADRate.Font.Size - 2, checkEditAvgADRate.Font.Style);
				checkEditAvgPCIRate.Font = new Font(checkEditAvgPCIRate.Font.FontFamily, checkEditAvgPCIRate.Font.Size - 2, checkEditAvgPCIRate.Font.Style);
				checkEditBusinessName.Font = new Font(checkEditBusinessName.Font.FontFamily, checkEditBusinessName.Font.Size - 2, checkEditBusinessName.Font.Style);
				checkEditStandartColor.Font = new Font(checkEditStandartColor.Font.FontFamily, checkEditStandartColor.Font.Size - 2, checkEditStandartColor.Font.Style);
				checkEditStandartDimensions.Font = new Font(checkEditStandartDimensions.Font.FontFamily, checkEditStandartDimensions.Font.Size - 2, checkEditStandartDimensions.Font.Style);
				checkEditDate.Font = new Font(checkEditDate.Font.FontFamily, checkEditDate.Font.Size - 2, checkEditDate.Font.Style);
				checkEditDecisionMaker.Font = new Font(checkEditDecisionMaker.Font.FontFamily, checkEditDecisionMaker.Font.Size - 2, checkEditDecisionMaker.Font.Style);
				checkEditFlightDates.Font = new Font(checkEditFlightDates.Font.FontFamily, checkEditFlightDates.Font.Size - 2, checkEditFlightDates.Font.Style);
				checkEditFlightDates2.Font = new Font(checkEditFlightDates2.Font.FontFamily, checkEditFlightDates2.Font.Size - 2, checkEditFlightDates2.Font.Style);
				checkEditStandartMechanicals.Font = new Font(checkEditStandartMechanicals.Font.FontFamily, checkEditStandartMechanicals.Font.Size - 2, checkEditStandartMechanicals.Font.Style);
				checkEditName.Font = new Font(checkEditName.Font.FontFamily, checkEditName.Font.Size - 3, checkEditName.Font.Style);
				checkEditStandartPageSize.Font = new Font(checkEditStandartPageSize.Font.FontFamily, checkEditStandartPageSize.Font.Size - 2, checkEditStandartPageSize.Font.Style);
				checkEditTotalAds.Font = new Font(checkEditTotalAds.Font.FontFamily, checkEditTotalAds.Font.Size - 2, checkEditTotalAds.Font.Style);
				checkEditStandartSquare.Font = new Font(checkEditStandartSquare.Font.FontFamily, checkEditStandartSquare.Font.Size - 2, checkEditStandartSquare.Font.Style);
				checkEditTotalCost.Font = new Font(checkEditTotalCost.Font.FontFamily, checkEditTotalCost.Font.Size - 2, checkEditTotalCost.Font.Style);
				checkEditTotalDiscounts.Font = new Font(checkEditTotalDiscounts.Font.FontFamily, checkEditTotalDiscounts.Font.Size - 2, checkEditTotalDiscounts.Font.Style);
				checkEditTotalSquare.Font = new Font(checkEditTotalSquare.Font.FontFamily, checkEditTotalSquare.Font.Size - 2, checkEditTotalSquare.Font.Style);
			}
		}

		public PrintProduct PrintProduct { get; set; }

		public bool SettingsNotSaved
		{
			get { return Controller.Instance.Summaries.BasicOverview.SettingsNotSaved; }
			set { Controller.Instance.Summaries.BasicOverview.SettingsNotSaved = value; }
		}

		public void LoadPublication()
		{
			Text = PrintProduct.Name.Replace("&", "&&");
			pbLogo.Image = PrintProduct.SmallLogo != null ? new Bitmap(PrintProduct.SmallLogo) : null;
			checkEditName.Text = PrintProduct.Name.Replace("&", "&&");

			checkEditStandartDimensions.Text = !string.IsNullOrEmpty(PrintProduct.SizeOptions.Dimensions) ? PrintProduct.SizeOptions.Dimensions : string.Empty;
			checkEditStandartDimensions.Visible = !string.IsNullOrEmpty(PrintProduct.SizeOptions.Dimensions);
			checkEditSharePageDimensions.Text = !string.IsNullOrEmpty(PrintProduct.SizeOptions.Dimensions) ? PrintProduct.SizeOptions.Dimensions : string.Empty;
			checkEditSharePageDimensions.Visible = !string.IsNullOrEmpty(PrintProduct.SizeOptions.Dimensions);
			checkEditStandartSquare.Text = PrintProduct.SizeOptions.Square.HasValue ? (PrintProduct.SizeOptions.Square.Value.ToString("#,##0.00#") + " col. in.") : string.Empty;
			checkEditStandartSquare.Visible = PrintProduct.SizeOptions.Square.HasValue;
			checkEditTotalSquare.Text = PrintProduct.TotalSquare.HasValue && PrintProduct.AdPricingStrategy != AdPricingStrategies.SharePage ? ("Total Inches: " + PrintProduct.TotalSquare.Value.ToString("#,##0.00#")) : string.Empty;
			checkEditTotalSquare.Visible = PrintProduct.TotalSquare.HasValue && PrintProduct.AdPricingStrategy != AdPricingStrategies.SharePage;
			checkEditStandartPageSize.Visible = !string.IsNullOrEmpty(PrintProduct.SizeOptions.PageSize);
			checkEditStandartPageSize.Text = !string.IsNullOrEmpty(PrintProduct.SizeOptions.PageSize) ? PrintProduct.SizeOptions.PageSize : string.Empty;
			checkEditSharePagePageSize.Visible = !string.IsNullOrEmpty(PrintProduct.SizeOptions.PageSize);
			checkEditSharePagePageSize.Text = !string.IsNullOrEmpty(PrintProduct.SizeOptions.PageSize) ? PrintProduct.SizeOptions.PageSize : string.Empty;
			checkEditStandartMechanicals.Visible = false; // !string.IsNullOrEmpty(this.Publication.SizeOptions.Mechanicals);
			checkEditStandartMechanicals.Text = string.Empty; // !string.IsNullOrEmpty(this.Publication.SizeOptions.Mechanicals) ? ("Mechanicals: " + this.Publication.SizeOptions.Mechanicals) : string.Empty;
			checkEditSharePagePercentOfPage.Visible = !string.IsNullOrEmpty(PrintProduct.SizeOptions.PercentOfPage);
			checkEditSharePagePercentOfPage.Text = !string.IsNullOrEmpty(PrintProduct.SizeOptions.PercentOfPage) ? (PrintProduct.SizeOptions.PercentOfPage + " Share of Page") : string.Empty;
			pnAdSizeStandart.Visible = PrintProduct.AdPricingStrategy != AdPricingStrategies.SharePage;
			pnAdSizeSharePage.Visible = PrintProduct.AdPricingStrategy == AdPricingStrategies.SharePage;

			checkEditAvgADRate.Text = "Avg Ad Rate: " + PrintProduct.AvgADRate.ToString("$#,##0.00");
			checkEditAvgPCIRate.Text = PrintProduct.AvgPCIRate > 0 ? ("Avg PCI: " + PrintProduct.AvgPCIRate.ToString("$#,##0.00")) : string.Empty;
			checkEditAvgPCIRate.Visible = PrintProduct.AvgPCIRate > 0;
			checkEditBusinessName.Text = PrintProduct.Parent.BusinessName + (!string.IsNullOrEmpty(PrintProduct.Parent.AccountNumber) ? (" - " + PrintProduct.Parent.AccountNumber) : string.Empty);

			switch (PrintProduct.ColorOption)
			{
				case ColorOptions.BlackWhite:
					checkEditStandartColor.Text = "Black && White";
					checkEditSharePageColor.Text = "Black && White";
					break;
				case ColorOptions.SpotColor:
					checkEditStandartColor.Text = "Spot Color";
					checkEditSharePageColor.Text = "Spot Color";
					break;
				case ColorOptions.FullColor:
					checkEditStandartColor.Text = "Full Color";
					checkEditSharePageColor.Text = "Full Color";
					break;
			}

			checkEditDate.Text = PrintProduct.Parent.PresentationDate.HasValue ? PrintProduct.Parent.PresentationDate.Value.ToString("MM/dd/yy") : string.Empty;

			var dates = new List<string>();
			foreach (var insert in PrintProduct.Inserts)
			{
				if (insert.Date.HasValue)
					dates.Add(insert.Date.Value.ToString("MM/dd/yy"));
			}
			memoEditDates.EditValue = string.Join(", ", dates.ToArray());

			checkEditDecisionMaker.Text = PrintProduct.Parent.DecisionMaker;
			checkEditFlightDates.Text = "   " + PrintProduct.Parent.FlightDates;
			checkEditFlightDates2.Text = PrintProduct.Parent.FlightDates;
			checkEditTotalAds.Text = "Total Ads: " + PrintProduct.TotalInserts.ToString("#,##0");
			checkEditTotalCost.Text = "Total Cost: " + PrintProduct.TotalFinalRate.ToString("$#,##0.00");
			checkEditTotalDiscounts.Text = "Total Discounts: " + PrintProduct.TotalDiscountRate.ToString("$#,##0.00");

			_allowToSave = false;
			checkEditTotalDiscounts.Checked = PrintProduct.TotalDiscountRate > 0;


			comboBoxEditSchedule.Properties.Items.Clear();
			comboBoxEditSchedule.Properties.Items.AddRange(ListManager.Instance.OutputHeaders.ToArray());
			if (string.IsNullOrEmpty(PrintProduct.ViewSettings.BasicOverviewSettings.SlideHeader))
			{
				if (comboBoxEditSchedule.Properties.Items.Count > 0)
					comboBoxEditSchedule.SelectedIndex = 0;
			}
			else
			{
				int index = comboBoxEditSchedule.Properties.Items.IndexOf(PrintProduct.ViewSettings.BasicOverviewSettings.SlideHeader);
				if (index >= 0)
					comboBoxEditSchedule.SelectedIndex = index;
				else
					comboBoxEditSchedule.SelectedIndex = 0;
			}
			textEditRunDatesComment.EditValue = PrintProduct.ViewSettings.BasicOverviewSettings.Comments;

			checkEditName.Checked = PrintProduct.ViewSettings.BasicOverviewSettings.ShowName;
			checkEditBusinessName.Checked = PrintProduct.ViewSettings.BasicOverviewSettings.ShowAdvertiser;
			checkEditDecisionMaker.Checked = PrintProduct.ViewSettings.BasicOverviewSettings.ShowDecisionMaker;
			checkEditDate.Checked = PrintProduct.ViewSettings.BasicOverviewSettings.ShowPresentationDate;
			checkEditSchedule.Checked = PrintProduct.ViewSettings.BasicOverviewSettings.ShowSlideHeader;
			checkEditFlightDates.Checked = PrintProduct.ViewSettings.BasicOverviewSettings.ShowFlightDates;
			checkEditLogo.Checked = PrintProduct.ViewSettings.BasicOverviewSettings.ShowLogo;

			checkEditStandartPageSize.Enabled = PrintProduct.ViewSettings.BasicOverviewSettings.EnablePageSize;
			checkEditSharePagePageSize.Enabled = PrintProduct.ViewSettings.BasicOverviewSettings.EnablePageSize;
			checkEditSharePagePercentOfPage.Enabled = PrintProduct.ViewSettings.BasicOverviewSettings.EnablePercentOfPage;
			checkEditStandartColor.Enabled = PrintProduct.ViewSettings.BasicOverviewSettings.EnableColor;
			checkEditSharePageColor.Enabled = PrintProduct.ViewSettings.BasicOverviewSettings.EnableColor;
			checkEditStandartDimensions.Enabled = PrintProduct.ViewSettings.BasicOverviewSettings.EnableDimensions;
			checkEditSharePageDimensions.Enabled = PrintProduct.ViewSettings.BasicOverviewSettings.EnableDimensions;
			checkEditStandartSquare.Enabled = PrintProduct.ViewSettings.BasicOverviewSettings.EnableSquare;
			checkEditAdSizePicture.Checked = PrintProduct.ViewSettings.BasicOverviewSettings.ShowAdSizeDetails;
			checkEditStandartPageSize.Checked = PrintProduct.ViewSettings.BasicOverviewSettings.ShowPageSize;
			checkEditSharePagePageSize.Checked = PrintProduct.ViewSettings.BasicOverviewSettings.ShowPageSize;
			checkEditSharePagePercentOfPage.Checked = PrintProduct.ViewSettings.BasicOverviewSettings.ShowPercentOfPage;
			checkEditStandartColor.Checked = PrintProduct.ViewSettings.BasicOverviewSettings.ShowColor;
			checkEditSharePageColor.Checked = PrintProduct.ViewSettings.BasicOverviewSettings.ShowColor;
			checkEditStandartDimensions.Checked = PrintProduct.ViewSettings.BasicOverviewSettings.ShowDimensions;
			checkEditSharePageDimensions.Checked = PrintProduct.ViewSettings.BasicOverviewSettings.ShowDimensions;
			checkEditStandartSquare.Checked = PrintProduct.ViewSettings.BasicOverviewSettings.ShowSquare;
			checkEditStandartMechanicals.Checked = PrintProduct.ViewSettings.BasicOverviewSettings.ShowMechanicals;

			checkEditTotalAds.Enabled = PrintProduct.ViewSettings.BasicOverviewSettings.EnableTotalInserts;
			checkEditTotalSquare.Enabled = PrintProduct.ViewSettings.BasicOverviewSettings.EnableTotalSquare;
			checkEditTotalAdsPicture.Checked = PrintProduct.ViewSettings.BasicOverviewSettings.ShowTotalDetails;
			checkEditTotalAds.Checked = PrintProduct.ViewSettings.BasicOverviewSettings.ShowTotalInserts;
			checkEditTotalSquare.Checked = PrintProduct.ViewSettings.BasicOverviewSettings.ShowTotalSquare;

			checkEditAvgADRate.Enabled = PrintProduct.ViewSettings.BasicOverviewSettings.EnableAvgAdCost;
			checkEditAvgPCIRate.Enabled = PrintProduct.ViewSettings.BasicOverviewSettings.EnableAvgPCI;
			checkEditTotalCost.Enabled = PrintProduct.ViewSettings.BasicOverviewSettings.EnableInvestment;
			checkEditTotalDiscounts.Enabled = PrintProduct.ViewSettings.BasicOverviewSettings.EnableDiscounts;
			checkEditInvestmentDetailsPicture.Checked = PrintProduct.ViewSettings.BasicOverviewSettings.ShowInvestmentDetails;
			checkEditAvgADRate.Checked = PrintProduct.ViewSettings.BasicOverviewSettings.ShowAvgAdCost;
			checkEditAvgPCIRate.Checked = PrintProduct.ViewSettings.BasicOverviewSettings.ShowAvgPCI;
			checkEditTotalCost.Checked = PrintProduct.ViewSettings.BasicOverviewSettings.ShowInvestment;
			checkEditTotalDiscounts.Checked = PrintProduct.ViewSettings.BasicOverviewSettings.ShowDiscounts && PrintProduct.TotalDiscountRate > 0;


			checkEditRunDatesComment.Enabled = PrintProduct.ViewSettings.BasicOverviewSettings.EnableComments;
			checkEditDates.Enabled = PrintProduct.ViewSettings.BasicOverviewSettings.EnableDates;
			checkEditFlightDates2.Enabled = PrintProduct.ViewSettings.BasicOverviewSettings.EnableFlightDates2;
			checkEditDatesPicture.Checked = PrintProduct.ViewSettings.BasicOverviewSettings.ShowDateDetails;
			checkEditRunDatesComment.Checked = PrintProduct.ViewSettings.BasicOverviewSettings.ShowComments;
			checkEditDates.Checked = PrintProduct.ViewSettings.BasicOverviewSettings.ShowDates;
			checkEditFlightDates2.Checked = PrintProduct.ViewSettings.BasicOverviewSettings.ShowFlightDates2;

			_allowToSave = true;
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

		#region Check Event Handlers
		private void checkEditTextEdit_CheckedChanged(object sender, EventArgs e)
		{
			textEditRunDatesComment.Enabled = checkEditRunDatesComment.Checked;
			checkEdit_CheckedChanged(null, null);
		}

		private void checkEditSchedule_CheckedChanged(object sender, EventArgs e)
		{
			comboBoxEditSchedule.Enabled = checkEditSchedule.Checked;
			checkEdit_CheckedChanged(null, null);
		}

		private void checkEditDates_CheckedChanged(object sender, EventArgs e)
		{
			memoEditDates.Enabled = checkEditDates.Checked;
			checkEdit_CheckedChanged(null, null);
		}

		private void checkEditAdSizePicture_CheckedChanged(object sender, EventArgs e) { }

		private void checkEditTotalAdsPicture_CheckedChanged(object sender, EventArgs e) { }

		private void checkEditInvestmentDetailsPicture_CheckedChanged(object sender, EventArgs e) { }

		private void checkEditDatesPicture_CheckedChanged(object sender, EventArgs e) { }

		private void checkEdit_CheckedChanged(object sender, EventArgs e)
		{
			if (_allowToSave)
			{
				PrintProduct.ViewSettings.BasicOverviewSettings.ShowPageSize = PrintProduct.AdPricingStrategy == AdPricingStrategies.SharePage ? checkEditSharePagePageSize.Checked : checkEditStandartPageSize.Checked;
				PrintProduct.ViewSettings.BasicOverviewSettings.ShowPercentOfPage = checkEditSharePagePercentOfPage.Checked;
				PrintProduct.ViewSettings.BasicOverviewSettings.ShowAdSizeDetails = checkEditAdSizePicture.Checked;
				PrintProduct.ViewSettings.BasicOverviewSettings.ShowAdvertiser = checkEditBusinessName.Checked;
				PrintProduct.ViewSettings.BasicOverviewSettings.ShowAvgAdCost = checkEditAvgADRate.Checked;
				PrintProduct.ViewSettings.BasicOverviewSettings.ShowAvgPCI = checkEditAvgPCIRate.Checked;
				PrintProduct.ViewSettings.BasicOverviewSettings.ShowColor = PrintProduct.AdPricingStrategy == AdPricingStrategies.SharePage ? checkEditSharePageColor.Checked : checkEditStandartColor.Checked;
				PrintProduct.ViewSettings.BasicOverviewSettings.ShowDimensions = PrintProduct.AdPricingStrategy == AdPricingStrategies.SharePage ? checkEditSharePageDimensions.Checked : checkEditStandartDimensions.Checked;
				PrintProduct.ViewSettings.BasicOverviewSettings.ShowComments = checkEditRunDatesComment.Checked;
				PrintProduct.ViewSettings.BasicOverviewSettings.ShowDateDetails = checkEditDatesPicture.Checked;
				PrintProduct.ViewSettings.BasicOverviewSettings.ShowDates = checkEditDates.Checked;
				PrintProduct.ViewSettings.BasicOverviewSettings.ShowDecisionMaker = checkEditDecisionMaker.Checked;
				PrintProduct.ViewSettings.BasicOverviewSettings.ShowDiscounts = checkEditTotalDiscounts.Checked;
				PrintProduct.ViewSettings.BasicOverviewSettings.ShowFlightDates = checkEditFlightDates.Checked;
				PrintProduct.ViewSettings.BasicOverviewSettings.ShowFlightDates2 = checkEditFlightDates2.Checked;
				PrintProduct.ViewSettings.BasicOverviewSettings.ShowInvestment = checkEditTotalCost.Checked;
				PrintProduct.ViewSettings.BasicOverviewSettings.ShowInvestmentDetails = checkEditInvestmentDetailsPicture.Checked;
				PrintProduct.ViewSettings.BasicOverviewSettings.ShowLogo = checkEditLogo.Checked;
				PrintProduct.ViewSettings.BasicOverviewSettings.ShowMechanicals = checkEditStandartMechanicals.Checked;
				PrintProduct.ViewSettings.BasicOverviewSettings.ShowName = checkEditName.Checked;
				PrintProduct.ViewSettings.BasicOverviewSettings.ShowPresentationDate = checkEditDate.Checked;
				PrintProduct.ViewSettings.BasicOverviewSettings.ShowSlideHeader = checkEditSchedule.Checked;
				PrintProduct.ViewSettings.BasicOverviewSettings.ShowSquare = checkEditStandartSquare.Checked;
				PrintProduct.ViewSettings.BasicOverviewSettings.ShowTotalDetails = checkEditTotalAdsPicture.Checked;
				PrintProduct.ViewSettings.BasicOverviewSettings.ShowTotalInserts = checkEditTotalAds.Checked;
				PrintProduct.ViewSettings.BasicOverviewSettings.ShowTotalSquare = checkEditTotalSquare.Checked;
				SettingsNotSaved = true;
			}
		}

		private void comboBoxEditSchedule_EditValueChanged(object sender, EventArgs e)
		{
			if (_allowToSave)
			{
				PrintProduct.ViewSettings.BasicOverviewSettings.SlideHeader = comboBoxEditSchedule.EditValue != null ? comboBoxEditSchedule.EditValue.ToString() : string.Empty;
				SettingsNotSaved = true;
			}
		}

		private void textEditRunDatesComment_EditValueChanged(object sender, EventArgs e)
		{
			if (_allowToSave)
			{
				PrintProduct.ViewSettings.BasicOverviewSettings.Comments = textEditRunDatesComment.EditValue != null ? textEditRunDatesComment.EditValue.ToString() : string.Empty;
				SettingsNotSaved = true;
			}
		}
		#endregion

		#region Output Staff
		public string Header
		{
			get
			{
				string result = string.Empty;
				if (checkEditSchedule.Checked)
					result = comboBoxEditSchedule.EditValue.ToString();
				return result;
			}
		}

		public string LogoFile
		{
			get
			{
				string result = string.Empty;
				if (checkEditLogo.Checked)
				{
					result = Path.GetTempFileName();
					pbLogo.Image.Save(result);
				}
				return result;
			}
		}

		public string PresentationName1
		{
			get
			{
				string result = string.Empty;
				if (checkEditName.Checked && checkEditLogo.Checked)
					result = Text;
				return result.Replace("&&", "&");
			}
		}

		public string PresentationDate1
		{
			get
			{
				string result = string.Empty;
				if (checkEditDate.Checked && checkEditLogo.Checked)
					result = checkEditDate.Text;
				return result;
			}
		}

		public string PresentationName2
		{
			get
			{
				string result = string.Empty;
				if (checkEditName.Checked && !checkEditLogo.Checked)
					result = Text;
				return result.Replace("&&", "&");
			}
		}

		public string PresentationDate2
		{
			get
			{
				string result = string.Empty;
				if (checkEditDate.Checked && !checkEditLogo.Checked)
					result = checkEditDate.Text;
				return result;
			}
		}

		public string BusinessName
		{
			get
			{
				string result = string.Empty;
				if (checkEditBusinessName.Checked)
					result = checkEditBusinessName.Text;
				return result;
			}
		}

		public string DecisionMaker
		{
			get
			{
				string result = string.Empty;
				if (checkEditDecisionMaker.Checked)
					result = checkEditDecisionMaker.Text;
				return result;
			}
		}

		public string FlightDates1
		{
			get
			{
				string result = string.Empty;
				if (checkEditFlightDates.Checked)
					result = checkEditFlightDates.Text;
				return result;
			}
		}

		public string FlightDates2
		{
			get
			{
				string result = string.Empty;
				if (checkEditFlightDates2.Checked)
					result = checkEditFlightDates2.Text;
				return result;
			}
		}

		public string[] AdSpecs
		{
			get
			{
				var values = new List<string>();
				if (PrintProduct.AdPricingStrategy == AdPricingStrategies.SharePage)
				{
					if (checkEditSharePagePercentOfPage.Checked && !string.IsNullOrEmpty(checkEditSharePagePercentOfPage.Text))
						values.Add(checkEditSharePagePercentOfPage.Text);
					if (checkEditSharePageColor.Checked)
						values.Add(checkEditSharePageColor.Text.Replace("&&", "&"));
					if (checkEditSharePagePageSize.Checked && !string.IsNullOrEmpty(PrintProduct.SizeOptions.PageSize))
						values.Add(checkEditSharePagePageSize.Text);
					if (checkEditSharePageDimensions.Checked && !string.IsNullOrEmpty(checkEditSharePageDimensions.Text))
						values.Add(checkEditSharePageDimensions.Text);
				}
				else
				{
					if (checkEditStandartSquare.Checked && !string.IsNullOrEmpty(checkEditStandartSquare.Text))
						values.Add(checkEditStandartSquare.Text);
					if (checkEditStandartPageSize.Checked && !string.IsNullOrEmpty(PrintProduct.SizeOptions.PageSize))
						values.Add(checkEditStandartPageSize.Text);
					if (checkEditStandartDimensions.Checked && !string.IsNullOrEmpty(checkEditStandartDimensions.Text))
						values.Add(checkEditStandartDimensions.Text);
					//if (checkEditStandartMechanicals.Checked && !string.IsNullOrEmpty(this.Publication.SizeOptions.Mechanicals))
					//    values.Add(checkEditStandartMechanicals.Text);
					if (checkEditStandartColor.Checked)
						values.Add(checkEditStandartColor.Text.Replace("&&", "&"));
				}
				return values.ToArray();
			}
		}

		public string[] AdSummaries
		{
			get
			{
				var values = new List<string>();
				if (checkEditTotalAds.Checked)
					values.Add(checkEditTotalAds.Text);
				if (checkEditTotalSquare.Checked && !string.IsNullOrEmpty(checkEditTotalSquare.Text))
					values.Add(checkEditTotalSquare.Text);
				return values.ToArray();
			}
		}

		public string[] InvestmentDetails
		{
			get
			{
				var values = new List<string>();
				if (checkEditAvgADRate.Checked)
					values.Add(checkEditAvgADRate.Text);
				if (checkEditAvgPCIRate.Checked && !string.IsNullOrEmpty(checkEditAvgPCIRate.Text))
					values.Add(checkEditAvgPCIRate.Text);
				if (checkEditTotalDiscounts.Checked)
					values.Add(checkEditTotalDiscounts.Text);
				if (checkEditTotalCost.Checked)
					values.Add(checkEditTotalCost.Text);
				return values.ToArray();
			}
		}

		public string RunDates
		{
			get
			{
				string result = string.Empty;
				if (checkEditDates.Checked)
					result = memoEditDates.EditValue.ToString();
				if (checkEditRunDatesComment.Checked && textEditRunDatesComment.EditValue != null && !string.IsNullOrEmpty(textEditRunDatesComment.EditValue.ToString()))
				{
					if (!string.IsNullOrEmpty(result))
						result += " - ";
					result += textEditRunDatesComment.Text;
				}
				return result;
			}
		}

		public string DigitalLegend
		{
			get
			{
				if (!PrintProduct.Parent.ViewSettings.BasicOverviewViewSettings.DigitalLegend.Enabled) return String.Empty;
				if (!(PrintProduct.Index == 1 || !PrintProduct.Parent.ViewSettings.BasicOverviewViewSettings.DigitalLegend.OutputOnlyOnce)) return String.Empty;
				if (!PrintProduct.Parent.ViewSettings.BasicOverviewViewSettings.DigitalLegend.AllowEdit)
					return String.Format("Digital Product Info: {0}", PrintProduct.Parent.GetDigitalInfo(PrintProduct.Parent.ViewSettings.BasicOverviewViewSettings.DigitalLegend.RequestOptions));
				if (!String.IsNullOrEmpty(PrintProduct.Parent.ViewSettings.BasicOverviewViewSettings.DigitalLegend.Info))
					return String.Format("Digital Product Info: {0}", PrintProduct.Parent.ViewSettings.BasicOverviewViewSettings.DigitalLegend.Info);
				return String.Empty;
			}
		}

		public string OutputFileIndex
		{
			get
			{
				int index = 0;

				if (checkEditAdSizePicture.Checked &&
					checkEditTotalAdsPicture.Checked &&
					checkEditInvestmentDetailsPicture.Checked &&
					checkEditDatesPicture.Checked)
					index = 1;
				else if (checkEditAdSizePicture.Checked &&
						 checkEditTotalAdsPicture.Checked &&
						 checkEditInvestmentDetailsPicture.Checked &&
						 !checkEditDatesPicture.Checked)
					index = 2;
				else if (checkEditAdSizePicture.Checked &&
						 !checkEditTotalAdsPicture.Checked &&
						 checkEditInvestmentDetailsPicture.Checked &&
						 checkEditDatesPicture.Checked)
					index = 3;
				else if (checkEditAdSizePicture.Checked &&
						 checkEditTotalAdsPicture.Checked &&
						 !checkEditInvestmentDetailsPicture.Checked &&
						 checkEditDatesPicture.Checked)
					index = 4;
				else if (checkEditAdSizePicture.Checked &&
						 checkEditTotalAdsPicture.Checked &&
						 !checkEditInvestmentDetailsPicture.Checked &&
						 !checkEditDatesPicture.Checked)
					index = 5;
				else if (checkEditAdSizePicture.Checked &&
						 !checkEditTotalAdsPicture.Checked &&
						 checkEditInvestmentDetailsPicture.Checked &&
						 !checkEditDatesPicture.Checked)
					index = 6;
				else if (checkEditAdSizePicture.Checked &&
						 !checkEditTotalAdsPicture.Checked &&
						 !checkEditInvestmentDetailsPicture.Checked &&
						 checkEditDatesPicture.Checked)
					index = 7;
				else if (!checkEditAdSizePicture.Checked &&
						 checkEditTotalAdsPicture.Checked &&
						 checkEditInvestmentDetailsPicture.Checked &&
						 checkEditDatesPicture.Checked)
					index = 8;
				else if (!checkEditAdSizePicture.Checked &&
						 checkEditTotalAdsPicture.Checked &&
						 checkEditInvestmentDetailsPicture.Checked &&
						 !checkEditDatesPicture.Checked)
					index = 9;
				else if (!checkEditAdSizePicture.Checked &&
						 checkEditTotalAdsPicture.Checked &&
						 !checkEditInvestmentDetailsPicture.Checked &&
						 checkEditDatesPicture.Checked)
					index = 10;
				else if (!checkEditAdSizePicture.Checked &&
						 !checkEditTotalAdsPicture.Checked &&
						 checkEditInvestmentDetailsPicture.Checked &&
						 checkEditDatesPicture.Checked)
					index = 11;
				else if (checkEditAdSizePicture.Checked &&
						 !checkEditTotalAdsPicture.Checked &&
						 !checkEditInvestmentDetailsPicture.Checked &&
						 !checkEditDatesPicture.Checked)
					index = 12;
				else if (!checkEditAdSizePicture.Checked &&
						 checkEditTotalAdsPicture.Checked &&
						 !checkEditInvestmentDetailsPicture.Checked &&
						 !checkEditDatesPicture.Checked)
					index = 13;
				else if (!checkEditAdSizePicture.Checked &&
						 !checkEditTotalAdsPicture.Checked &&
						 checkEditInvestmentDetailsPicture.Checked &&
						 !checkEditDatesPicture.Checked)
					index = 14;
				else if (!checkEditAdSizePicture.Checked &&
						 !checkEditTotalAdsPicture.Checked &&
						 !checkEditInvestmentDetailsPicture.Checked &&
						 checkEditDatesPicture.Checked)
					index = 15;

				return String.Format("{0}{1}", index, PrintProduct.Parent.ViewSettings.BasicOverviewViewSettings.DigitalLegend.Enabled && (PrintProduct.Index == 1 || !PrintProduct.Parent.ViewSettings.BasicOverviewViewSettings.DigitalLegend.OutputOnlyOnce) ? "d" : String.Empty);
			}
		}

		public Theme SelectedTheme
		{
			get { return BusinessWrapper.Instance.ThemeManager.Themes.FirstOrDefault(t => t.Name.Equals(PrintProduct.Parent.ThemeName) || String.IsNullOrEmpty(PrintProduct.Parent.ThemeName)); }
		}

		public void PrintOutput()
		{
			AdSchedulePowerPointHelper.Instance.AppendBasicOverview(new[] { this });
		}
		#endregion
	}
}