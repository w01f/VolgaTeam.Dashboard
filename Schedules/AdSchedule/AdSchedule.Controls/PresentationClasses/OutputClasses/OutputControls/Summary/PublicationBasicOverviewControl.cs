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
using NewBizWiz.CommonGUI.Preview;
using NewBizWiz.Core.AdSchedule;
using NewBizWiz.Core.Common;
using SettingsManager = NewBizWiz.Core.Common.SettingsManager;

namespace NewBizWiz.AdSchedule.Controls.PresentationClasses.OutputClasses.OutputControls
{
	public partial class PublicationBasicOverviewControl : XtraTabPage, IBasicOverviewOutputControl
	//public partial class PublicationBasicOverviewControl : System.Windows.Forms.UserControl
	{
		private bool _allowToSave;
		private readonly OutputBasicOverviewControl _parent;

		public PublicationBasicOverviewControl(OutputBasicOverviewControl parent)
		{
			InitializeComponent();
			_parent = parent;
			if ((base.CreateGraphics()).DpiX > 96)
			{
				laAdSize.Font = new Font(laAdSize.Font.FontFamily, laAdSize.Font.Size - 3, laAdSize.Font.Style);
				laDates.Font = new Font(laDates.Font.FontFamily, laDates.Font.Size - 3, laDates.Font.Style);
				laInvestmentDetails.Font = new Font(laInvestmentDetails.Font.FontFamily, laInvestmentDetails.Font.Size - 3, laInvestmentDetails.Font.Style);
				laTotalAds.Font = new Font(laTotalAds.Font.FontFamily, laTotalAds.Font.Size - 3, laTotalAds.Font.Style);
				checkEditAvgADRate.Font = new Font(checkEditAvgADRate.Font.FontFamily, checkEditAvgADRate.Font.Size - 2, checkEditAvgADRate.Font.Style);
				checkEditAvgPCIRate.Font = new Font(checkEditAvgPCIRate.Font.FontFamily, checkEditAvgPCIRate.Font.Size - 2, checkEditAvgPCIRate.Font.Style);
				checkEditColor.Font = new Font(checkEditColor.Font.FontFamily, checkEditColor.Font.Size - 2, checkEditColor.Font.Style);
				checkEditDimensions.Font = new Font(checkEditDimensions.Font.FontFamily, checkEditDimensions.Font.Size - 2, checkEditDimensions.Font.Style);
				checkEditFlightDates2.Font = new Font(checkEditFlightDates2.Font.FontFamily, checkEditFlightDates2.Font.Size - 2, checkEditFlightDates2.Font.Style);
				checkEditPageSize.Font = new Font(checkEditPageSize.Font.FontFamily, checkEditPageSize.Font.Size - 2, checkEditPageSize.Font.Style);
				checkEditTotalAds.Font = new Font(checkEditTotalAds.Font.FontFamily, checkEditTotalAds.Font.Size - 2, checkEditTotalAds.Font.Style);
				checkEditSquare.Font = new Font(checkEditSquare.Font.FontFamily, checkEditSquare.Font.Size - 2, checkEditSquare.Font.Style);
				checkEditTotalCost.Font = new Font(checkEditTotalCost.Font.FontFamily, checkEditTotalCost.Font.Size - 2, checkEditTotalCost.Font.Style);
				checkEditTotalDiscounts.Font = new Font(checkEditTotalDiscounts.Font.FontFamily, checkEditTotalDiscounts.Font.Size - 2, checkEditTotalDiscounts.Font.Style);
				checkEditTotalSquare.Font = new Font(checkEditTotalSquare.Font.FontFamily, checkEditTotalSquare.Font.Size - 2, checkEditTotalSquare.Font.Style);
			}

			SummaryControl = new PublicationSummaryControl(this);
		}

		public PrintProduct PrintProduct { get; set; }
		public PublicationSummaryControl SummaryControl { get; private set; }

		public bool SettingsNotSaved
		{
			get { return _parent.SettingsNotSaved; }
			set { _parent.SettingsNotSaved = value; }
		}

		public void LoadPublication()
		{
			Text = PrintProduct.Name.Replace("&", "&&");
			pbLogo.Image = PrintProduct.TinyLogo != null ? new Bitmap(PrintProduct.TinyLogo) : null;
			checkEditDimensions.Text = !string.IsNullOrEmpty(PrintProduct.SizeOptions.Dimensions) ? PrintProduct.SizeOptions.Dimensions : string.Empty;
			checkEditDimensions.Visible = !string.IsNullOrEmpty(PrintProduct.SizeOptions.Dimensions);
			checkEditSquare.Text = PrintProduct.SizeOptions.Square.HasValue ? (PrintProduct.SizeOptions.Square.Value.ToString("#,##0.00#") + " col. in.") : string.Empty;
			checkEditSquare.Visible = PrintProduct.SizeOptions.Square.HasValue && PrintProduct.AdPricingStrategy != AdPricingStrategies.SharePage;
			checkEditTotalSquare.Text = PrintProduct.TotalSquare.HasValue && PrintProduct.AdPricingStrategy != AdPricingStrategies.SharePage ? ("Total Inches: " + PrintProduct.TotalSquare.Value.ToString("#,##0.00#")) : string.Empty;
			checkEditTotalSquare.Visible = PrintProduct.TotalSquare.HasValue && PrintProduct.AdPricingStrategy != AdPricingStrategies.SharePage;
			checkEditPageSize.Visible = !String.IsNullOrEmpty(PrintProduct.SizeOptions.PageSizeAndGroup);
			checkEditPageSize.Text = !String.IsNullOrEmpty(PrintProduct.SizeOptions.PageSizeAndGroup) ? PrintProduct.SizeOptions.PageSizeAndGroup : String.Empty;
			checkEditPercentOfPage.Visible = !String.IsNullOrEmpty(PrintProduct.SizeOptions.PercentOfPage) && PrintProduct.AdPricingStrategy == AdPricingStrategies.SharePage;
			checkEditPercentOfPage.Text = !String.IsNullOrEmpty(PrintProduct.SizeOptions.PercentOfPage) ? (PrintProduct.SizeOptions.PercentOfPage + " Share of Page") : string.Empty;
			checkEditAvgADRate.Text = "Avg Ad Rate: " + PrintProduct.AvgADRate.ToString("$#,##0.00");
			checkEditAvgPCIRate.Text = PrintProduct.AvgPCIRate > 0 ? ("Avg PCI: " + PrintProduct.AvgPCIRate.ToString("$#,##0.00")) : string.Empty;
			checkEditAvgPCIRate.Visible = PrintProduct.AvgPCIRate > 0;
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

			memoEditDates.EditValue = String.Join(", ", (PrintProduct.Inserts.Where(insert => insert.Date.HasValue).Select(insert => insert.Date.Value.ToString("MM/dd/yy"))));
			checkEditFlightDates2.Text = PrintProduct.Parent.FlightDates;
			checkEditTotalAds.Text = "Total Ads: " + PrintProduct.TotalInserts.ToString("#,##0");
			checkEditTotalCost.Text = "Total Cost: " + PrintProduct.TotalFinalRate.ToString("$#,##0.00");
			checkEditTotalDiscounts.Text = "Total Discounts: " + PrintProduct.TotalDiscountRate.ToString("$#,##0.00");

			_allowToSave = false;
			checkEditTotalDiscounts.Checked = PrintProduct.TotalDiscountRate > 0;
			textEditRunDatesComment.EditValue = PrintProduct.ViewSettings.BasicOverviewSettings.Comments;
			checkEditLogo.Checked = PrintProduct.ViewSettings.BasicOverviewSettings.ShowLogo;
			checkEditPageSize.Enabled = PrintProduct.ViewSettings.BasicOverviewSettings.EnablePageSize;
			checkEditPercentOfPage.Enabled = PrintProduct.ViewSettings.BasicOverviewSettings.EnablePercentOfPage;
			checkEditColor.Enabled = PrintProduct.ViewSettings.BasicOverviewSettings.EnableColor;
			checkEditDimensions.Enabled = PrintProduct.ViewSettings.BasicOverviewSettings.EnableDimensions;
			checkEditSquare.Enabled = PrintProduct.ViewSettings.BasicOverviewSettings.EnableSquare;
			checkEditAdSizePicture.Checked = PrintProduct.ViewSettings.BasicOverviewSettings.ShowAdSizeDetails;
			checkEditPageSize.Checked = PrintProduct.ViewSettings.BasicOverviewSettings.ShowPageSize;
			checkEditPercentOfPage.Checked = PrintProduct.ViewSettings.BasicOverviewSettings.ShowPercentOfPage;
			checkEditColor.Checked = PrintProduct.ViewSettings.BasicOverviewSettings.ShowColor;
			checkEditDimensions.Checked = PrintProduct.ViewSettings.BasicOverviewSettings.ShowDimensions;
			checkEditSquare.Checked = PrintProduct.ViewSettings.BasicOverviewSettings.ShowSquare;

			checkEditTotalAds.Enabled = PrintProduct.ViewSettings.BasicOverviewSettings.EnableTotalInserts;
			checkEditTotalSquare.Enabled = PrintProduct.ViewSettings.BasicOverviewSettings.EnableTotalSquare;
			checkEditTotalAdsPicture.Checked = PrintProduct.ViewSettings.BasicOverviewSettings.ShowTotalDetails;
			checkEditTotalAds.Checked = PrintProduct.ViewSettings.BasicOverviewSettings.ShowTotalInserts;
			checkEditTotalSquare.Checked = PrintProduct.ViewSettings.BasicOverviewSettings.ShowTotalSquare;

			checkEditAvgADRate.Enabled = PrintProduct.ViewSettings.BasicOverviewSettings.EnableAvgAdCost;
			checkEditAvgPCIRate.Enabled = PrintProduct.ViewSettings.BasicOverviewSettings.EnableAvgPCI;
			checkEditTotalCost.Enabled = PrintProduct.ViewSettings.BasicOverviewSettings.EnableInvestment;
			checkEditTotalDiscounts.Enabled = PrintProduct.ViewSettings.BasicOverviewSettings.EnableDiscounts;
			checkEditAvgADRate.Checked = PrintProduct.ViewSettings.BasicOverviewSettings.ShowAvgAdCost;
			checkEditAvgPCIRate.Checked = PrintProduct.ViewSettings.BasicOverviewSettings.ShowAvgPCI;
			checkEditTotalCost.Checked = PrintProduct.ViewSettings.BasicOverviewSettings.ShowInvestment;
			checkEditTotalDiscounts.Checked = PrintProduct.ViewSettings.BasicOverviewSettings.ShowDiscounts && PrintProduct.TotalDiscountRate > 0;
			checkEditInvestmentDetailsPicture.Checked = PrintProduct.ViewSettings.BasicOverviewSettings.ShowInvestmentDetails &&
				(PrintProduct.ViewSettings.BasicOverviewSettings.ShowAvgAdCost ||
				PrintProduct.ViewSettings.BasicOverviewSettings.ShowAvgPCI ||
				PrintProduct.ViewSettings.BasicOverviewSettings.ShowInvestment ||
				(PrintProduct.ViewSettings.BasicOverviewSettings.ShowDiscounts && PrintProduct.TotalDiscountRate > 0));

			checkEditRunDatesComment.Enabled = PrintProduct.ViewSettings.BasicOverviewSettings.EnableComments;
			checkEditDates.Enabled = PrintProduct.ViewSettings.BasicOverviewSettings.EnableDates;
			checkEditFlightDates2.Enabled = PrintProduct.ViewSettings.BasicOverviewSettings.EnableFlightDates2;
			checkEditDatesPicture.Checked = PrintProduct.ViewSettings.BasicOverviewSettings.ShowDateDetails;
			checkEditRunDatesComment.Checked = PrintProduct.ViewSettings.BasicOverviewSettings.ShowComments;
			checkEditDates.Checked = PrintProduct.ViewSettings.BasicOverviewSettings.ShowDates;
			checkEditFlightDates2.Checked = PrintProduct.ViewSettings.BasicOverviewSettings.ShowFlightDates2;

			_allowToSave = true;

			SummaryControl.UpdateControls();
		}

		private void checkEdit_MouseDown(object sender, MouseEventArgs e)
		{
			var cEdit = (CheckEdit)sender;
			var cInfo = (CheckEditViewInfo)cEdit.GetViewInfo();
			var r = cInfo.CheckInfo.GlyphRect;
			var editorRect = new Rectangle(new Point(0, 0), cEdit.Size);
			if (!r.Contains(e.Location) && editorRect.Contains(e.Location))
				((DXMouseEventArgs)e).Handled = true;
		}

		#region Check Event Handlers
		public void OnOptionChanged(object sender)
		{
			checkEdit_CheckedChanged(sender, EventArgs.Empty);
		}

		private void checkEditTextEdit_CheckedChanged(object sender, EventArgs e)
		{
			textEditRunDatesComment.Enabled = checkEditRunDatesComment.Checked;
			checkEdit_CheckedChanged(null, null);
		}

		private void checkEditDates_CheckedChanged(object sender, EventArgs e)
		{
			memoEditDates.Enabled = checkEditDates.Checked;
			checkEdit_CheckedChanged(null, null);
		}

		private void checkEdit_CheckedChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			PrintProduct.ViewSettings.BasicOverviewSettings.ShowPageSize = checkEditPageSize.Checked;
			PrintProduct.ViewSettings.BasicOverviewSettings.ShowPercentOfPage = checkEditPercentOfPage.Checked;
			PrintProduct.ViewSettings.BasicOverviewSettings.ShowAdSizeDetails = checkEditAdSizePicture.Checked;
			PrintProduct.ViewSettings.BasicOverviewSettings.ShowAvgAdCost = checkEditAvgADRate.Checked;
			PrintProduct.ViewSettings.BasicOverviewSettings.ShowAvgPCI = checkEditAvgPCIRate.Checked;
			PrintProduct.ViewSettings.BasicOverviewSettings.ShowColor = checkEditColor.Checked;
			PrintProduct.ViewSettings.BasicOverviewSettings.ShowDimensions = checkEditDimensions.Checked;
			PrintProduct.ViewSettings.BasicOverviewSettings.ShowComments = checkEditRunDatesComment.Checked;
			PrintProduct.ViewSettings.BasicOverviewSettings.ShowDateDetails = checkEditDatesPicture.Checked;
			PrintProduct.ViewSettings.BasicOverviewSettings.ShowDates = checkEditDates.Checked;
			PrintProduct.ViewSettings.BasicOverviewSettings.ShowDiscounts = checkEditTotalDiscounts.Checked;
			PrintProduct.ViewSettings.BasicOverviewSettings.ShowFlightDates2 = checkEditFlightDates2.Checked;
			PrintProduct.ViewSettings.BasicOverviewSettings.ShowInvestment = checkEditTotalCost.Checked;
			PrintProduct.ViewSettings.BasicOverviewSettings.ShowInvestmentDetails = checkEditInvestmentDetailsPicture.Checked;
			PrintProduct.ViewSettings.BasicOverviewSettings.ShowLogo = checkEditLogo.Checked;
			PrintProduct.ViewSettings.BasicOverviewSettings.ShowSquare = checkEditSquare.Checked;
			PrintProduct.ViewSettings.BasicOverviewSettings.ShowTotalDetails = checkEditTotalAdsPicture.Checked;
			PrintProduct.ViewSettings.BasicOverviewSettings.ShowTotalInserts = checkEditTotalAds.Checked;
			PrintProduct.ViewSettings.BasicOverviewSettings.ShowTotalSquare = checkEditTotalSquare.Checked;

			if (!(checkEditAvgADRate.Checked || checkEditTotalDiscounts.Checked || checkEditAvgPCIRate.Checked || checkEditTotalCost.Checked))
				checkEditInvestmentDetailsPicture.Checked = false;

			SettingsNotSaved = true;

			SummaryControl.UpdateControls();
		}

		private void textEditRunDatesComment_EditValueChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			PrintProduct.ViewSettings.BasicOverviewSettings.Comments = textEditRunDatesComment.EditValue != null ? textEditRunDatesComment.EditValue.ToString() : string.Empty;
			SettingsNotSaved = true;
		}
		#endregion

		#region Output Staff
		public string LogoFile
		{
			get
			{
				var result = string.Empty;
				if (!checkEditLogo.Checked) return result;
				result = Path.GetTempFileName();
				pbLogo.Image.Save(result);
				return result;
			}
		}

		public string PresentationName1
		{
			get
			{
				var result = string.Empty;
				if (checkEditLogo.Checked)
					result = Text;
				return result.Replace("&&", "&");
			}
		}

		public string PresentationDate1
		{
			get
			{
				var result = string.Empty;
				if (checkEditLogo.Checked && _parent.LocalSchedule.PresentationDate.HasValue)
					result = _parent.LocalSchedule.PresentationDate.Value.ToString("MM/dd/yy");
				return result;
			}
		}

		public string PresentationName2
		{
			get
			{
				var result = string.Empty;
				if (!checkEditLogo.Checked)
					result = Text;
				return result.Replace("&&", "&");
			}
		}

		public string PresentationDate2
		{
			get
			{
				var result = string.Empty;
				if (_parent.LocalSchedule.PresentationDate.HasValue)
					result = _parent.LocalSchedule.PresentationDate.Value.ToString("MM/dd/yy");
				return result;
			}
		}

		public string BusinessName
		{
			get { return _parent.LocalSchedule.BusinessName; }
		}

		public string DecisionMaker
		{
			get { return _parent.LocalSchedule.DecisionMaker; }
		}

		public string FlightDates1
		{
			get { return _parent.LocalSchedule.FlightDates; }
		}

		public string FlightDates2
		{
			get
			{
				var result = string.Empty;
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
				if (checkEditPercentOfPage.Checked && !string.IsNullOrEmpty(checkEditPercentOfPage.Text) && PrintProduct.AdPricingStrategy == AdPricingStrategies.SharePage)
					values.Add(checkEditPercentOfPage.Text);
				if (checkEditSquare.Checked && !string.IsNullOrEmpty(checkEditSquare.Text) && PrintProduct.AdPricingStrategy != AdPricingStrategies.SharePage)
					values.Add(checkEditSquare.Text);
				if (checkEditPageSize.Checked && !String.IsNullOrEmpty(PrintProduct.SizeOptions.PageSizeAndGroup))
					values.Add(checkEditPageSize.Text);
				if (checkEditDimensions.Checked && !string.IsNullOrEmpty(checkEditDimensions.Text))
					values.Add(checkEditDimensions.Text);
				if (checkEditColor.Checked)
					values.Add(checkEditColor.Text.Replace("&&", "&"));
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
				if (!String.IsNullOrEmpty(PrintProduct.Parent.ViewSettings.BasicOverviewViewSettings.DigitalLegend.CompiledInfo))
					return String.Format("Digital Product Info: {0}", PrintProduct.Parent.ViewSettings.BasicOverviewViewSettings.DigitalLegend.CompiledInfo);
				return String.Empty;
			}
		}

		public string SummaryDescription
		{
			get
			{
				var result = new List<string>();
				result.AddRange(AdSpecs);
				result.AddRange(AdSummaries);
				return String.Join(", ", result);
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
			get { return BusinessWrapper.Instance.ThemeManager.GetThemes(SlideType.PrintBasicOverview).FirstOrDefault(t => t.Name.Equals(BusinessWrapper.Instance.GetSelectedTheme(SlideType.PrintBasicOverview)) || String.IsNullOrEmpty(BusinessWrapper.Instance.GetSelectedTheme(SlideType.PrintBasicOverview))); }
		}

		public string SlideName
		{
			get { return PrintProduct.Name; }
		}

		public PreviewGroup GetPreviewGroup()
		{
			return new PreviewGroup
			{
				Name = PrintProduct.Name,
				PresentationSourcePath = Path.Combine(ResourceManager.Instance.TempFolder.LocalPath, Path.GetFileName(Path.GetTempFileName()))
			};
		}

		public void Output()
		{
			AdSchedulePowerPointHelper.Instance.AppendBasicOverview(new[] { this });
		}
		#endregion
	}
}