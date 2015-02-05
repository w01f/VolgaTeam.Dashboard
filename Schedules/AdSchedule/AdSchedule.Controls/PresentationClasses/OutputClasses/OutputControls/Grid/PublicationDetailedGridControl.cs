using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using NewBizWiz.AdSchedule.Controls.BusinessClasses;
using NewBizWiz.AdSchedule.Controls.InteropClasses;
using DevExpress.Utils;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraTab;
using NewBizWiz.Core.AdSchedule;
using NewBizWiz.Core.Common;
using ListManager = NewBizWiz.Core.AdSchedule.ListManager;

namespace NewBizWiz.AdSchedule.Controls.PresentationClasses.OutputClasses.OutputControls
{
	public partial class PublicationDetailedGridControl : XtraTabPage
	//public partial class PublicationDetailedGridControl : System.Windows.Forms.UserControl
	{
		private GridColumn _activeCol;
		private bool _allowToSave;

		public PublicationDetailedGridControl()
		{
			InitializeComponent();
			comboBoxEditSchedule.Properties.Items.Clear();
			comboBoxEditSchedule.Properties.Items.AddRange(ListManager.Instance.OutputHeaders.ToArray());
			if (comboBoxEditSchedule.Properties.Items.Count > 0)
				comboBoxEditSchedule.SelectedIndex = 0;

			textEditHeader.Hide();
			textEditHeader.Parent = gridControlPublication;
		}

		public PrintProduct PrintProduct { get; set; }

		public bool SettingsNotSaved
		{
			get { return Controller.Instance.Grids.DetailedGrid.SettingsNotSaved; }
			set { Controller.Instance.Grids.DetailedGrid.SettingsNotSaved = value; }
		}

		public void LoadPublication()
		{
			Text = PrintProduct.Name.Replace("&", "&&");
			pbLogo.Image = PrintProduct.SmallLogo;
			laPublicationName.Text = PrintProduct.Name.Replace("&", "&&");
			laDate.Text = PrintProduct.Parent.PresentationDate.HasValue ? PrintProduct.Parent.PresentationDate.Value.ToString("MM/dd/yy") : string.Empty;
			laBusinessName.Text = PrintProduct.Parent.BusinessName + (!string.IsNullOrEmpty(PrintProduct.Parent.AccountNumber) ? (" - " + PrintProduct.Parent.AccountNumber) : string.Empty);
			laDecisionMaker.Text = PrintProduct.Parent.DecisionMaker;
			laFlightDates.Text = PrintProduct.Parent.FlightDates;
			gridControlPublication.DataSource = new BindingList<Insert>(PrintProduct.Inserts);

			_allowToSave = false;
			comboBoxEditSchedule.Properties.Items.Clear();
			comboBoxEditSchedule.Properties.Items.AddRange(ListManager.Instance.OutputHeaders.ToArray());
			if (string.IsNullOrEmpty(PrintProduct.ViewSettings.DetailedGridSettings.SlideHeader))
			{
				if (comboBoxEditSchedule.Properties.Items.Count > 0)
					comboBoxEditSchedule.SelectedIndex = 0;
			}
			else
			{
				int index = comboBoxEditSchedule.Properties.Items.IndexOf(PrintProduct.ViewSettings.DetailedGridSettings.SlideHeader);
				if (index >= 0)
					comboBoxEditSchedule.SelectedIndex = index;
				else
					comboBoxEditSchedule.SelectedIndex = 0;
			}
			_allowToSave = true;
		}

		public void SetSlideHeader()
		{
			comboBoxEditSchedule.Enabled = Controller.Instance.Grids.DetailedGrid.SlideHeaderState.ShowSlideHeader;
			laBusinessName.Enabled = Controller.Instance.Grids.DetailedGrid.SlideHeaderState.ShowAdvertiser;
			laDate.Enabled = Controller.Instance.Grids.DetailedGrid.SlideHeaderState.ShowPresentationDate;
			laDecisionMaker.Enabled = Controller.Instance.Grids.DetailedGrid.SlideHeaderState.ShowDecisionMaker;
			laFlightDates.Enabled = Controller.Instance.Grids.DetailedGrid.SlideHeaderState.ShowFlightDates;
			laPublicationName.Enabled = Controller.Instance.Grids.DetailedGrid.SlideHeaderState.ShowName;
			pbLogo.Visible = Controller.Instance.Grids.DetailedGrid.SlideHeaderState.ShowLogo1;
		}

		#region Editor's Events
		private void textEditHeader_Leave(object sender, EventArgs e)
		{
			_activeCol.Caption = textEditHeader.Text;

			if (_activeCol == gridColumnADRate)
				Controller.Instance.Grids.DetailedGrid.CaptionCost = _activeCol.Caption;
			else if (_activeCol == gridColumnColorPricing)
				Controller.Instance.Grids.DetailedGrid.CaptionColor = _activeCol.Caption;
			else if (_activeCol == gridColumnColumnInches)
				Controller.Instance.Grids.DetailedGrid.CaptionSquare = _activeCol.Caption;
			else if (_activeCol == gridColumnDate)
				Controller.Instance.Grids.DetailedGrid.CaptionDate = _activeCol.Caption;
			else if (_activeCol == gridColumnDeadline)
				Controller.Instance.Grids.DetailedGrid.CaptionDeadline = _activeCol.Caption;
			else if (_activeCol == gridColumnDelivery)
				Controller.Instance.Grids.DetailedGrid.CaptionDelivery = _activeCol.Caption;
			else if (_activeCol == gridColumnDimensions)
				Controller.Instance.Grids.DetailedGrid.CaptionDimensions = _activeCol.Caption;
			else if (_activeCol == gridColumnDiscountRate)
				Controller.Instance.Grids.DetailedGrid.CaptionDiscount = _activeCol.Caption;
			else if (_activeCol == gridColumnFinalRate)
				Controller.Instance.Grids.DetailedGrid.CaptionFinalCost = _activeCol.Caption;
			else if (_activeCol == gridColumnID)
				Controller.Instance.Grids.DetailedGrid.CaptionID = _activeCol.Caption;
			else if (_activeCol == gridColumnIndex)
				Controller.Instance.Grids.DetailedGrid.CaptionIndex = _activeCol.Caption;
			else if (_activeCol == gridColumnMechanicals)
				Controller.Instance.Grids.DetailedGrid.CaptionMechanicals = _activeCol.Caption;
			else if (_activeCol == gridColumnPageSize)
				Controller.Instance.Grids.DetailedGrid.CaptionPageSize = _activeCol.Caption;
			else if (_activeCol == gridColumnPercentOfPage)
				Controller.Instance.Grids.DetailedGrid.CaptionPercentOfPage = _activeCol.Caption;
			else if (_activeCol == gridColumnPCIRate)
				Controller.Instance.Grids.DetailedGrid.CaptionPCI = _activeCol.Caption;
			else if (_activeCol == gridColumnPublication)
				Controller.Instance.Grids.DetailedGrid.CaptionPublication = _activeCol.Caption;
			else if (_activeCol == gridColumnReadership)
				Controller.Instance.Grids.DetailedGrid.CaptionReadership = _activeCol.Caption;
			else if (_activeCol == gridColumnSection)
				Controller.Instance.Grids.DetailedGrid.CaptionSection = _activeCol.Caption;

			Controller.Instance.Grids.DetailedGrid.SaveView();

			textEditHeader.Hide();
		}

		private void comboBoxEditSchedule_EditValueChanged(object sender, EventArgs e)
		{
			if (_allowToSave)
			{
				PrintProduct.ViewSettings.DetailedGridSettings.SlideHeader = comboBoxEditSchedule.EditValue != null ? comboBoxEditSchedule.EditValue.ToString() : string.Empty;
				SettingsNotSaved = true;
			}
		}
		#endregion

		#region Grid Events
		private void gridViewPublication_DoubleClick(object sender, EventArgs e)
		{
			var args = (e as DXMouseEventArgs);
			var view = sender as GridView;
			GridHitInfo hi = view.CalcHitInfo(args.Location);
			if (hi.InColumn)
			{
				var vi = view.GetViewInfo() as GridViewInfo;
				Rectangle bounds = vi.ColumnsInfo[hi.Column].Bounds;
				bounds.Width -= 10;
				bounds.Height -= 3;
				bounds.Y += 3;
				textEditHeader.SetBounds(bounds.X, bounds.Y, bounds.Width, bounds.Height);
				textEditHeader.EditValue = hi.Column.Caption;
				textEditHeader.Show();
				textEditHeader.Focus();
				_activeCol = hi.Column;
			}
			else
				Utilities.Instance.ShowWarning(" If you want to modify this Schedule Data,\ngo to the Schedules Tab and save changes...");
		}

		private void gridViewPublications_MouseUp(object sender, MouseEventArgs e)
		{
			var args = (e as DXMouseEventArgs);
			var view = sender as GridView;
			GridHitInfo hi = view.CalcHitInfo(args.Location);
			if (hi.InColumn && e.Clicks == 2)
			{
				var ViewInfo = view.GetViewInfo() as GridViewInfo;
				GridState prevState = view.State;
				if ((e.Button & MouseButtons.Left) != 0)
				{
					if (ViewInfo.ColumnsInfo[hi.Column].CaptionRect.Contains(new Point(e.X, e.Y)))
					{
						ViewInfo.SelectionInfo.ClearPressedInfo();
						args.Handled = true;
					}
				}
			}
		}

		private void gridViewPublications_CalcPreviewText(object sender, CalcPreviewTextEventArgs e)
		{
			var previewText = new SortedDictionary<int, string>();
			int maxNumber = 12;
			var insert = gridViewPublications.GetRow(e.RowHandle) as Insert;
			if (insert == null) return;
			if (Controller.Instance.Grids.DetailedGrid.ShowCommentsInPreview && !String.IsNullOrEmpty(e.PreviewText))
				previewText.Add(Controller.Instance.Grids.DetailedGrid.PositionCommentsInPreview > 0 && !previewText.Keys.Contains(Controller.Instance.Grids.DetailedGrid.PositionCommentsInPreview) ? Controller.Instance.Grids.DetailedGrid.PositionCommentsInPreview : ++maxNumber, e.PreviewText);
			if (Controller.Instance.Grids.DetailedGrid.ShowSectionInPreview && !String.IsNullOrEmpty(insert.FullSection))
				previewText.Add(Controller.Instance.Grids.DetailedGrid.PositionSectionInPreview > 0 && !previewText.Keys.Contains(Controller.Instance.Grids.DetailedGrid.PositionSectionInPreview) ? Controller.Instance.Grids.DetailedGrid.PositionSectionInPreview : ++maxNumber, "Section: " + insert.FullSection);
			if (Controller.Instance.Grids.DetailedGrid.ShowMechanicalsInPreview && !String.IsNullOrEmpty(insert.Mechanicals))
				previewText.Add(Controller.Instance.Grids.DetailedGrid.PositionMechanicalsInPreview > 0 && !previewText.Keys.Contains(Controller.Instance.Grids.DetailedGrid.PositionMechanicalsInPreview) ? Controller.Instance.Grids.DetailedGrid.PositionMechanicalsInPreview : ++maxNumber, "Mech: " + insert.Mechanicals);
			if (Controller.Instance.Grids.DetailedGrid.ShowDeliveryInPreview && !String.IsNullOrEmpty(insert.Delivery))
				previewText.Add(Controller.Instance.Grids.DetailedGrid.PositionDeliveryInPreview > 0 && !previewText.Keys.Contains(Controller.Instance.Grids.DetailedGrid.PositionDeliveryInPreview) ? Controller.Instance.Grids.DetailedGrid.PositionDeliveryInPreview : ++maxNumber, "Delivery: " + insert.Delivery);
			if (Controller.Instance.Grids.DetailedGrid.ShowPublicationInPreview && !String.IsNullOrEmpty(insert.Publication))
				previewText.Add(Controller.Instance.Grids.DetailedGrid.PositionPublicationInPreview > 0 && !previewText.Keys.Contains(Controller.Instance.Grids.DetailedGrid.PositionPublicationInPreview) ? Controller.Instance.Grids.DetailedGrid.PositionPublicationInPreview : ++maxNumber, "Publication: " + insert.Publication);
			if (Controller.Instance.Grids.DetailedGrid.ShowPageSizeInPreview && !String.IsNullOrEmpty(insert.PageSize))
				previewText.Add(Controller.Instance.Grids.DetailedGrid.PositionPageSizeInPreview > 0 && !previewText.Keys.Contains(Controller.Instance.Grids.DetailedGrid.PositionPageSizeInPreview) ? Controller.Instance.Grids.DetailedGrid.PositionPageSizeInPreview : ++maxNumber, "Page Size: " + insert.PageSize);
			if (Controller.Instance.Grids.DetailedGrid.ShowPercentOfPageInPreview && !String.IsNullOrEmpty(insert.PercentOfPage))
				previewText.Add(Controller.Instance.Grids.DetailedGrid.PositionPercentOfPageInPreview > 0 && !previewText.Keys.Contains(Controller.Instance.Grids.DetailedGrid.PositionPercentOfPageInPreview) ? Controller.Instance.Grids.DetailedGrid.PositionPercentOfPageInPreview : ++maxNumber, "% of Page: " + insert.PercentOfPage);
			if (Controller.Instance.Grids.DetailedGrid.ShowDimensionsInPreview && !String.IsNullOrEmpty(insert.Dimensions))
				previewText.Add(Controller.Instance.Grids.DetailedGrid.PositionDimensionsInPreview > 0 && !previewText.Keys.Contains(Controller.Instance.Grids.DetailedGrid.PositionDimensionsInPreview) ? Controller.Instance.Grids.DetailedGrid.PositionDimensionsInPreview : ++maxNumber, "Col. x Inches: " + insert.Dimensions);
			if (Controller.Instance.Grids.DetailedGrid.ShowColumnInchesInPreview && !String.IsNullOrEmpty(insert.SquareStringFormatted))
				previewText.Add(Controller.Instance.Grids.DetailedGrid.PositionColumnInchesInPreview > 0 && !previewText.Keys.Contains(Controller.Instance.Grids.DetailedGrid.PositionColumnInchesInPreview) ? Controller.Instance.Grids.DetailedGrid.PositionColumnInchesInPreview : ++maxNumber, "Total Col In: " + insert.SquareStringFormatted + " col. in.");
			if (Controller.Instance.Grids.DetailedGrid.ShowReadershipInPreview && !String.IsNullOrEmpty(insert.Readership))
				previewText.Add(Controller.Instance.Grids.DetailedGrid.PositionReadershipInPreview > 0 && !previewText.Keys.Contains(Controller.Instance.Grids.DetailedGrid.PositionReadershipInPreview) ? Controller.Instance.Grids.DetailedGrid.PositionReadershipInPreview : ++maxNumber, "Readership: " + insert.Readership);
			if (Controller.Instance.Grids.DetailedGrid.ShowDeadlineInPreview && !String.IsNullOrEmpty(insert.DeadlineForOutput))
				previewText.Add(Controller.Instance.Grids.DetailedGrid.PositionDeadlineInPreview > 0 && !previewText.Keys.Contains(Controller.Instance.Grids.DetailedGrid.PositionDeadlineInPreview) ? Controller.Instance.Grids.DetailedGrid.PositionDeadlineInPreview : ++maxNumber, "Deadline: " + insert.DeadlineForOutput);
			e.PreviewText = string.Join(",   ", previewText.Values.ToArray());
			if (string.IsNullOrEmpty(e.PreviewText))
				e.PreviewText = "            ";
		}

		private void gridViewPublications_MouseMove(object sender, MouseEventArgs e)
		{
			gridViewPublications.Focus();
		}

		private void gridViewPublications_ColumnPositionChanged(object sender, EventArgs e)
		{
			if (Controller.Instance.Grids.DetailedGrid.AllowToSave)
			{
				Controller.Instance.Grids.DetailedGrid.PositionCost = gridColumnADRate.VisibleIndex;
				Controller.Instance.Grids.DetailedGrid.PositionColor = gridColumnColorPricing.VisibleIndex;
				Controller.Instance.Grids.DetailedGrid.PositionSquare = gridColumnColumnInches.VisibleIndex;
				Controller.Instance.Grids.DetailedGrid.PositionDate = gridColumnDate.VisibleIndex;
				Controller.Instance.Grids.DetailedGrid.PositionDeadline = gridColumnDeadline.VisibleIndex;
				Controller.Instance.Grids.DetailedGrid.PositionDelivery = gridColumnDelivery.VisibleIndex;
				Controller.Instance.Grids.DetailedGrid.PositionDimensions = gridColumnDimensions.VisibleIndex;
				Controller.Instance.Grids.DetailedGrid.PositionDiscount = gridColumnDiscountRate.VisibleIndex;
				Controller.Instance.Grids.DetailedGrid.PositionFinalCost = gridColumnFinalRate.VisibleIndex;
				Controller.Instance.Grids.DetailedGrid.PositionID = gridColumnID.VisibleIndex;
				Controller.Instance.Grids.DetailedGrid.PositionIndex = gridColumnIndex.VisibleIndex;
				Controller.Instance.Grids.DetailedGrid.PositionMechanicals = gridColumnMechanicals.VisibleIndex;
				Controller.Instance.Grids.DetailedGrid.PositionPageSize = gridColumnPageSize.VisibleIndex;
				Controller.Instance.Grids.DetailedGrid.PositionPercentOfPage = gridColumnPercentOfPage.VisibleIndex;
				Controller.Instance.Grids.DetailedGrid.PositionPCI = gridColumnPCIRate.VisibleIndex;
				Controller.Instance.Grids.DetailedGrid.PositionPublication = gridColumnPublication.VisibleIndex;
				Controller.Instance.Grids.DetailedGrid.PositionReadership = gridColumnReadership.VisibleIndex;
				Controller.Instance.Grids.DetailedGrid.PositionSection = gridColumnSection.VisibleIndex;
				Controller.Instance.Grids.DetailedGrid.SaveView();
			}
		}

		private void gridViewPublications_ColumnWidthChanged(object sender, ColumnEventArgs e)
		{
			if (Controller.Instance.Grids.DetailedGrid.AllowToSave)
			{
				Controller.Instance.Grids.DetailedGrid.WidthCost = gridColumnADRate.Width;
				Controller.Instance.Grids.DetailedGrid.WidthColor = gridColumnColorPricing.Width;
				Controller.Instance.Grids.DetailedGrid.WidthSquare = gridColumnColumnInches.Width;
				Controller.Instance.Grids.DetailedGrid.WidthDate = gridColumnDate.Width;
				Controller.Instance.Grids.DetailedGrid.WidthDeadline = gridColumnDeadline.Width;
				Controller.Instance.Grids.DetailedGrid.WidthDelivery = gridColumnDelivery.Width;
				Controller.Instance.Grids.DetailedGrid.WidthDimensions = gridColumnDimensions.Width;
				Controller.Instance.Grids.DetailedGrid.WidthDiscount = gridColumnDiscountRate.Width;
				Controller.Instance.Grids.DetailedGrid.WidthFinalCost = gridColumnFinalRate.Width;
				Controller.Instance.Grids.DetailedGrid.WidthID = gridColumnID.Width;
				Controller.Instance.Grids.DetailedGrid.WidthIndex = gridColumnIndex.Width;
				Controller.Instance.Grids.DetailedGrid.WidthMechanicals = gridColumnMechanicals.Width;
				Controller.Instance.Grids.DetailedGrid.WidthPageSize = gridColumnPageSize.Width;
				Controller.Instance.Grids.DetailedGrid.WidthPercentOfPage = gridColumnPercentOfPage.Width;
				Controller.Instance.Grids.DetailedGrid.WidthPCI = gridColumnPCIRate.Width;
				Controller.Instance.Grids.DetailedGrid.WidthPublication = gridColumnPublication.Width;
				Controller.Instance.Grids.DetailedGrid.WidthReadership = gridColumnReadership.Width;
				Controller.Instance.Grids.DetailedGrid.WidthSection = gridColumnSection.Width;
				Controller.Instance.Grids.DetailedGrid.SaveView();
			}
		}
		#endregion

		#region Output Staff
		public List<Dictionary<string, string>> OutputReplacementsLists { get; set; }

		public Theme SelectedTheme
		{
			get { return BusinessWrapper.Instance.ThemeManager.GetThemes(SlideType.PrintDetailedGrid).FirstOrDefault(t => t.Name.Equals(BusinessWrapper.Instance.GetSelectedTheme(SlideType.PrintDetailedGrid)) || String.IsNullOrEmpty(BusinessWrapper.Instance.GetSelectedTheme(SlideType.PrintDetailedGrid))); }
		}

		public string Header
		{
			get
			{
				string result = string.Empty;
				if (comboBoxEditSchedule.EditValue != null && Controller.Instance.Grids.DetailedGrid.SlideHeaderState.ShowSlideHeader)
					result = comboBoxEditSchedule.EditValue.ToString();
				return result;
			}
		}

		public string LogoFile
		{
			get
			{
				string result = string.Empty;
				if (!Controller.Instance.Grids.DetailedGrid.SlideHeaderState.ShowLogo1 || PrintProduct.TinyLogo == null) return result;
				result = Path.GetTempFileName();
				PrintProduct.TinyLogo.Save(result);
				return result;
			}
		}

		public string PresentationName1
		{
			get
			{
				string result = string.Empty;
				if (Controller.Instance.Grids.DetailedGrid.SlideHeaderState.ShowName && Controller.Instance.Grids.DetailedGrid.SlideHeaderState.ShowLogo1)
					result = Text;
				return result.Replace("&&", "&");
			}
		}

		public string PresentationDate1
		{
			get
			{
				string result = string.Empty;
				if (Controller.Instance.Grids.DetailedGrid.SlideHeaderState.ShowPresentationDate && Controller.Instance.Grids.DetailedGrid.SlideHeaderState.ShowLogo1)
					result = laDate.Text;
				return result;
			}
		}

		public string PresentationName2
		{
			get
			{
				string result = string.Empty;
				if (Controller.Instance.Grids.DetailedGrid.SlideHeaderState.ShowName && !Controller.Instance.Grids.DetailedGrid.SlideHeaderState.ShowLogo1)
					result = Text;
				return result.Replace("&&", "&");
			}
		}

		public string PresentationDate2
		{
			get
			{
				string result = string.Empty;
				if (Controller.Instance.Grids.DetailedGrid.SlideHeaderState.ShowPresentationDate && !Controller.Instance.Grids.DetailedGrid.SlideHeaderState.ShowLogo1)
					result = laDate.Text;
				return result;
			}
		}

		public string BusinessName
		{
			get
			{
				string result = string.Empty;
				if (Controller.Instance.Grids.DetailedGrid.SlideHeaderState.ShowAdvertiser)
					result = laBusinessName.Text;
				return result;
			}
		}

		public string DecisionMaker
		{
			get
			{
				string result = string.Empty;
				if (Controller.Instance.Grids.DetailedGrid.SlideHeaderState.ShowDecisionMaker)
					result = laDecisionMaker.Text;
				return result;
			}
		}

		public string FlightDates
		{
			get
			{
				string result = string.Empty;
				if (Controller.Instance.Grids.DetailedGrid.SlideHeaderState.ShowFlightDates)
					result = laFlightDates.Text;
				return result;
			}
		}

		public bool ShowSignatureLine
		{
			get { return Controller.Instance.Grids.DetailedGrid.SlideBulletsState.ShowSignature; }
		}

		public bool ShowAdSpecsOnlyOnLastSlide
		{
			get
			{
				bool result = true;
				result = Controller.Instance.Grids.DetailedGrid.SlideBulletsState.ShowOnlyOnLastSlide;
				return result;
			}
		}

		public bool DoNotShowAdSpecs
		{
			get { return !Controller.Instance.Grids.DetailedGrid.SlideBulletsState.ShowSlideBullets; }
		}

		public bool ShowDigitalLegend
		{
			get
			{
				var lastProductIndex = PrintProduct.Parent.PrintProducts.Max(p => p.Index);
				return Controller.Instance.Grids.DetailedGrid.DigitalLegend.Enabled && (PrintProduct.Index == lastProductIndex || !Controller.Instance.Grids.DetailedGrid.DigitalLegend.OutputOnlyOnce);
			}
		}

		public string DigitalLegendInfo
		{
			get
			{
				if (!Controller.Instance.Grids.DetailedGrid.DigitalLegend.Enabled) return String.Empty;
				if (!Controller.Instance.Grids.DetailedGrid.DigitalLegend.AllowEdit)
					return String.Format("Digital Product Info: {0}", Controller.Instance.Grids.DetailedGrid.LocalSchedule.GetDigitalInfo(Controller.Instance.Grids.DetailedGrid.DigitalLegend.RequestOptions));
				if (!String.IsNullOrEmpty(Controller.Instance.Grids.DetailedGrid.DigitalLegend.CompiledInfo))
					return String.Format("Digital Product Info: {0}", Controller.Instance.Grids.DetailedGrid.DigitalLegend.CompiledInfo);
				return String.Empty;
			}
		}

		public string[] AdSpecs
		{
			get
			{
				var values = new List<string>();
				if (Controller.Instance.Grids.DetailedGrid.SlideBulletsState.ShowTotalInserts)
					values.Add("Total Ads: " + PrintProduct.TotalInserts.ToString("#,##0"));
				if (Controller.Instance.Grids.DetailedGrid.SlideBulletsState.ShowPageSize)
					values.Add("Page Size: " + PrintProduct.SizeOptions.PageSizeAndGroup);
				if (Controller.Instance.Grids.DetailedGrid.SlideBulletsState.ShowPercentOfPage)
					values.Add("% of Page: " + PrintProduct.SizeOptions.PercentOfPage);
				if (Controller.Instance.Grids.DetailedGrid.SlideBulletsState.ShowDimensions)
					values.Add("Col. x Inches: " + PrintProduct.SizeOptions.Dimensions);
				if (Controller.Instance.Grids.DetailedGrid.SlideBulletsState.ShowColumnInches && PrintProduct.SizeOptions.Square.HasValue)
					values.Add("Total Col. In.: " + PrintProduct.SizeOptions.Square.Value.ToString("#,###.00#"));
				if (Controller.Instance.Grids.DetailedGrid.SlideBulletsState.ShowAvgAdCost)
					values.Add("BW Ad Cost: " + PrintProduct.AvgADRate.ToString("$#,###.00"));
				if (Controller.Instance.Grids.DetailedGrid.SlideBulletsState.ShowAvgFinalCost)
					values.Add("Final Ad Cost: " + PrintProduct.AvgFinalRate.ToString("$#,###.00"));
				if (Controller.Instance.Grids.DetailedGrid.SlideBulletsState.ShowAvgPCI)
					values.Add("Avg PCI: " + PrintProduct.AvgPCIRate.ToString("$#,###.00"));
				if (Controller.Instance.Grids.DetailedGrid.SlideBulletsState.ShowDelivery && PrintProduct.DailyDelivery != null)
					values.Add("Delivery: " + PrintProduct.DailyDelivery.Value.ToString("#,##0"));
				if (Controller.Instance.Grids.DetailedGrid.SlideBulletsState.ShowReadership && PrintProduct.DailyReadership != null)
					values.Add("Readership: " + PrintProduct.DailyReadership.Value.ToString("#,##0"));
				if (Controller.Instance.Grids.DetailedGrid.SlideBulletsState.ShowTotalColor)
					values.Add("Total Color: " + PrintProduct.TotalColorPricingCalculated.ToString("$#,###.00"));
				if (Controller.Instance.Grids.DetailedGrid.SlideBulletsState.ShowDiscounts)
					values.Add("Discounts: " + PrintProduct.TotalDiscountRate.ToString("$#,###.00"));
				if (Controller.Instance.Grids.DetailedGrid.SlideBulletsState.ShowTotalFinalCost)
					values.Add("Investment: " + PrintProduct.TotalFinalRate.ToString("$#,###.00"));
				if (Controller.Instance.Grids.DetailedGrid.SlideBulletsState.ShowTotalSquare && PrintProduct.TotalSquare.HasValue)
					values.Add("Total Inches: " + PrintProduct.TotalSquare.Value.ToString("#,###.00#"));
				return values.ToArray();
			}
		}

		public string[] GridHeaders
		{
			get
			{
				var headers = new SortedDictionary<int, string>();
				if (Controller.Instance.Grids.DetailedGrid.PositionID != -1 && Controller.Instance.Grids.DetailedGrid.ShowIDHeader)
					headers.Add(Controller.Instance.Grids.DetailedGrid.PositionID, Controller.Instance.Grids.DetailedGrid.CaptionID);
				if (Controller.Instance.Grids.DetailedGrid.PositionDate != -1 && Controller.Instance.Grids.DetailedGrid.ShowDateHeader)
					headers.Add(Controller.Instance.Grids.DetailedGrid.PositionDate, Controller.Instance.Grids.DetailedGrid.CaptionDate);
				if (Controller.Instance.Grids.DetailedGrid.PositionPCI != -1 && Controller.Instance.Grids.DetailedGrid.ShowPCIHeader)
					headers.Add(Controller.Instance.Grids.DetailedGrid.PositionPCI, Controller.Instance.Grids.DetailedGrid.CaptionPCI);
				if (Controller.Instance.Grids.DetailedGrid.PositionCost != -1 && Controller.Instance.Grids.DetailedGrid.ShowCostHeader)
					headers.Add(Controller.Instance.Grids.DetailedGrid.PositionCost, Controller.Instance.Grids.DetailedGrid.CaptionCost.Replace("&&", "&"));
				if (Controller.Instance.Grids.DetailedGrid.PositionDiscount != -1 && Controller.Instance.Grids.DetailedGrid.ShowDiscountHeader)
					headers.Add(Controller.Instance.Grids.DetailedGrid.PositionDiscount, Controller.Instance.Grids.DetailedGrid.CaptionDiscount);
				if (Controller.Instance.Grids.DetailedGrid.PositionColor != -1 && Controller.Instance.Grids.DetailedGrid.ShowColorHeader)
					headers.Add(Controller.Instance.Grids.DetailedGrid.PositionColor, Controller.Instance.Grids.DetailedGrid.CaptionColor);
				if (Controller.Instance.Grids.DetailedGrid.PositionFinalCost != -1 && Controller.Instance.Grids.DetailedGrid.ShowFinalCostHeader)
					headers.Add(Controller.Instance.Grids.DetailedGrid.PositionFinalCost, Controller.Instance.Grids.DetailedGrid.CaptionFinalCost);
				if (Controller.Instance.Grids.DetailedGrid.PositionIndex != -1 && Controller.Instance.Grids.DetailedGrid.ShowIndexHeader)
					headers.Add(Controller.Instance.Grids.DetailedGrid.PositionIndex, Controller.Instance.Grids.DetailedGrid.CaptionIndex);
				if (Controller.Instance.Grids.DetailedGrid.PositionSquare != -1 && Controller.Instance.Grids.DetailedGrid.ShowSquareHeader)
					headers.Add(Controller.Instance.Grids.DetailedGrid.PositionSquare, Controller.Instance.Grids.DetailedGrid.CaptionSquare);
				if (Controller.Instance.Grids.DetailedGrid.PositionPageSize != -1 && Controller.Instance.Grids.DetailedGrid.ShowPageSizeHeader)
					headers.Add(Controller.Instance.Grids.DetailedGrid.PositionPageSize, Controller.Instance.Grids.DetailedGrid.CaptionPageSize);
				if (Controller.Instance.Grids.DetailedGrid.PositionPercentOfPage != -1 && Controller.Instance.Grids.DetailedGrid.ShowPercentOfPageHeader)
					headers.Add(Controller.Instance.Grids.DetailedGrid.PositionPercentOfPage, Controller.Instance.Grids.DetailedGrid.CaptionPercentOfPage);
				if (Controller.Instance.Grids.DetailedGrid.PositionMechanicals != -1 && Controller.Instance.Grids.DetailedGrid.ShowMechanicalsHeader)
					headers.Add(Controller.Instance.Grids.DetailedGrid.PositionMechanicals, Controller.Instance.Grids.DetailedGrid.CaptionMechanicals);
				if (Controller.Instance.Grids.DetailedGrid.PositionPublication != -1 && Controller.Instance.Grids.DetailedGrid.ShowPublicationHeader)
					headers.Add(Controller.Instance.Grids.DetailedGrid.PositionPublication, Controller.Instance.Grids.DetailedGrid.CaptionPublication);
				if (Controller.Instance.Grids.DetailedGrid.PositionDimensions != -1 && Controller.Instance.Grids.DetailedGrid.ShowDimensionsHeader)
					headers.Add(Controller.Instance.Grids.DetailedGrid.PositionDimensions, Controller.Instance.Grids.DetailedGrid.CaptionDimensions);
				if (Controller.Instance.Grids.DetailedGrid.PositionSection != -1 && Controller.Instance.Grids.DetailedGrid.ShowSectionHeader)
					headers.Add(Controller.Instance.Grids.DetailedGrid.PositionSection, Controller.Instance.Grids.DetailedGrid.CaptionSection);
				if (Controller.Instance.Grids.DetailedGrid.PositionReadership != -1 && Controller.Instance.Grids.DetailedGrid.ShowReadershipHeader)
					headers.Add(Controller.Instance.Grids.DetailedGrid.PositionReadership, Controller.Instance.Grids.DetailedGrid.CaptionReadership);
				if (Controller.Instance.Grids.DetailedGrid.PositionDelivery != -1 && Controller.Instance.Grids.DetailedGrid.ShowDeliveryHeader)
					headers.Add(Controller.Instance.Grids.DetailedGrid.PositionDelivery, Controller.Instance.Grids.DetailedGrid.CaptionDelivery);
				if (Controller.Instance.Grids.DetailedGrid.PositionDeadline != -1 && Controller.Instance.Grids.DetailedGrid.ShowDeadlineHeader)
					headers.Add(Controller.Instance.Grids.DetailedGrid.PositionDeadline, Controller.Instance.Grids.DetailedGrid.CaptionDeadline);
				return headers.Values.ToArray();
			}
		}

		public int[] GridHeaderSizes
		{
			get
			{
				var sizes = new SortedDictionary<int, int>();
				if (Controller.Instance.Grids.DetailedGrid.PositionID != -1 && Controller.Instance.Grids.DetailedGrid.ShowIDHeader)
					sizes.Add(Controller.Instance.Grids.DetailedGrid.PositionID, Controller.Instance.Grids.DetailedGrid.WidthID);
				if (Controller.Instance.Grids.DetailedGrid.PositionDate != -1 && Controller.Instance.Grids.DetailedGrid.ShowDateHeader)
					sizes.Add(Controller.Instance.Grids.DetailedGrid.PositionDate, Controller.Instance.Grids.DetailedGrid.WidthDate);
				if (Controller.Instance.Grids.DetailedGrid.PositionPCI != -1 && Controller.Instance.Grids.DetailedGrid.ShowPCIHeader)
					sizes.Add(Controller.Instance.Grids.DetailedGrid.PositionPCI, Controller.Instance.Grids.DetailedGrid.WidthPCI);
				if (Controller.Instance.Grids.DetailedGrid.PositionCost != -1 && Controller.Instance.Grids.DetailedGrid.ShowCostHeader)
					sizes.Add(Controller.Instance.Grids.DetailedGrid.PositionCost, Controller.Instance.Grids.DetailedGrid.WidthCost);
				if (Controller.Instance.Grids.DetailedGrid.PositionDiscount != -1 && Controller.Instance.Grids.DetailedGrid.ShowDiscountHeader)
					sizes.Add(Controller.Instance.Grids.DetailedGrid.PositionDiscount, Controller.Instance.Grids.DetailedGrid.WidthDiscount);
				if (Controller.Instance.Grids.DetailedGrid.PositionColor != -1 && Controller.Instance.Grids.DetailedGrid.ShowColorHeader)
					sizes.Add(Controller.Instance.Grids.DetailedGrid.PositionColor, Controller.Instance.Grids.DetailedGrid.WidthColor);
				if (Controller.Instance.Grids.DetailedGrid.PositionFinalCost != -1 && Controller.Instance.Grids.DetailedGrid.ShowFinalCostHeader)
					sizes.Add(Controller.Instance.Grids.DetailedGrid.PositionFinalCost, Controller.Instance.Grids.DetailedGrid.WidthFinalCost);
				if (Controller.Instance.Grids.DetailedGrid.PositionIndex != -1 && Controller.Instance.Grids.DetailedGrid.ShowIndexHeader)
					sizes.Add(Controller.Instance.Grids.DetailedGrid.PositionIndex, Controller.Instance.Grids.DetailedGrid.WidthIndex);
				if (Controller.Instance.Grids.DetailedGrid.PositionSquare != -1 && Controller.Instance.Grids.DetailedGrid.ShowSquareHeader)
					sizes.Add(Controller.Instance.Grids.DetailedGrid.PositionSquare, Controller.Instance.Grids.DetailedGrid.WidthSquare);
				if (Controller.Instance.Grids.DetailedGrid.PositionPageSize != -1 && Controller.Instance.Grids.DetailedGrid.ShowPageSizeHeader)
					sizes.Add(Controller.Instance.Grids.DetailedGrid.PositionPageSize, Controller.Instance.Grids.DetailedGrid.WidthPageSize);
				if (Controller.Instance.Grids.DetailedGrid.PositionPercentOfPage != -1 && Controller.Instance.Grids.DetailedGrid.ShowPercentOfPageHeader)
					sizes.Add(Controller.Instance.Grids.DetailedGrid.PositionPercentOfPage, Controller.Instance.Grids.DetailedGrid.WidthPercentOfPage);
				if (Controller.Instance.Grids.DetailedGrid.PositionMechanicals != -1 && Controller.Instance.Grids.DetailedGrid.ShowMechanicalsHeader)
					sizes.Add(Controller.Instance.Grids.DetailedGrid.PositionMechanicals, Controller.Instance.Grids.DetailedGrid.WidthMechanicals);
				if (Controller.Instance.Grids.DetailedGrid.PositionPublication != -1 && Controller.Instance.Grids.DetailedGrid.ShowPublicationHeader)
					sizes.Add(Controller.Instance.Grids.DetailedGrid.PositionPublication, Controller.Instance.Grids.DetailedGrid.WidthPublication);
				if (Controller.Instance.Grids.DetailedGrid.PositionDimensions != -1 && Controller.Instance.Grids.DetailedGrid.ShowDimensionsHeader)
					sizes.Add(Controller.Instance.Grids.DetailedGrid.PositionDimensions, Controller.Instance.Grids.DetailedGrid.WidthDimensions);
				if (Controller.Instance.Grids.DetailedGrid.PositionSection != -1 && Controller.Instance.Grids.DetailedGrid.ShowSectionHeader)
					sizes.Add(Controller.Instance.Grids.DetailedGrid.PositionSection, Controller.Instance.Grids.DetailedGrid.WidthSection);
				if (Controller.Instance.Grids.DetailedGrid.PositionReadership != -1 && Controller.Instance.Grids.DetailedGrid.ShowReadershipHeader)
					sizes.Add(Controller.Instance.Grids.DetailedGrid.PositionReadership, Controller.Instance.Grids.DetailedGrid.WidthReadership);
				if (Controller.Instance.Grids.DetailedGrid.PositionDelivery != -1 && Controller.Instance.Grids.DetailedGrid.ShowDeliveryHeader)
					sizes.Add(Controller.Instance.Grids.DetailedGrid.PositionDelivery, Controller.Instance.Grids.DetailedGrid.WidthDelivery);
				if (Controller.Instance.Grids.DetailedGrid.PositionDeadline != -1 && Controller.Instance.Grids.DetailedGrid.ShowDeadlineHeader)
					sizes.Add(Controller.Instance.Grids.DetailedGrid.PositionDeadline, Controller.Instance.Grids.DetailedGrid.WidthDeadline);
				return sizes.Values.ToArray();
			}
		}

		public string[][][] Grid { get; private set; }

		public void PrepareOutput()
		{
			Grid = GetGrid();
			PopulateReplacementsList();
		}

		public void PrintOutput()
		{
			PrepareOutput();
			AdSchedulePowerPointHelper.Instance.AppendDetailedGridGridBased(new[] { this });
		}

		public void PopulateReplacementsList()
		{
			var key = string.Empty;
			var value = string.Empty;
			OutputReplacementsLists = new List<Dictionary<string, string>>();
			var slideNumber = 0;
			var slidesCount = Grid.Length;
			foreach (var slideGrid in Grid)
			{
				var slideReplacementList = new Dictionary<string, string>();

				var hideAdSpecsOnSlide = ((slideNumber + 1) < slidesCount && ShowAdSpecsOnlyOnLastSlide) || DoNotShowAdSpecs;

				slideReplacementList.Add("BusinessName", BusinessName);
				slideReplacementList.Add("DecisionMaker", DecisionMaker);
				slideReplacementList.Add("Decisionmaker", DecisionMaker);
				slideReplacementList.Add("FlightDates", FlightDates);
				slideReplacementList.Add("DateTag", PresentationDate1);
				slideReplacementList.Add("Date", PresentationDate1);

				for (var j = 0; j < 6; j++)
				{
					if (j < AdSpecs.Length && !hideAdSpecsOnSlide)
						slideReplacementList.Add(string.Format("ADSPEC{0}", j + 1), AdSpecs[j]);
					else
						slideReplacementList.Add(string.Format("ADSPEC{0}", j + 1), String.Empty);
				}
				string[] gridHeaders = GridHeaders;
				for (int i = 0; i < gridHeaders.Length; i++)
				{
					key = string.Format("Header{0}", (i + 1));
					value = gridHeaders[i];
					if (!slideReplacementList.Keys.Contains(key))
						slideReplacementList.Add(key, value);
				}

				for (int i = 0; i < slideGrid.Length; i++)
				{
					var rowItemsNumber = slideGrid[i].Length;
					if (rowItemsNumber > 1)
					{
						for (int j = 0; j < rowItemsNumber; j++)
						{
							string columnPrefix = "a";
							if (Controller.Instance.Grids.DetailedGrid.ShowCommentsHeader)
							{
								switch (j)
								{
									case 0:
										columnPrefix = "k";
										break;
									case 1:
										columnPrefix = "a";
										break;
									case 2:
										columnPrefix = "b";
										break;
									case 3:
										columnPrefix = "c";
										break;
									case 4:
										columnPrefix = "d";
										break;
									case 5:
										columnPrefix = "e";
										break;
									case 6:
										columnPrefix = "f";
										break;
									case 7:
										columnPrefix = "g";
										break;
									case 8:
										columnPrefix = "h";
										break;
									case 9:
										columnPrefix = "i";
										break;
									case 10:
										columnPrefix = "j";
										break;
								}
							}
							else
							{
								switch (j)
								{
									case 0:
										columnPrefix = "a";
										break;
									case 1:
										columnPrefix = "b";
										break;
									case 2:
										columnPrefix = "c";
										break;
									case 3:
										columnPrefix = "d";
										break;
									case 4:
										columnPrefix = "e";
										break;
									case 5:
										columnPrefix = "f";
										break;
									case 6:
										columnPrefix = "g";
										break;
									case 7:
										columnPrefix = "h";
										break;
									case 8:
										columnPrefix = "i";
										break;
									case 9:
										columnPrefix = "j";
										break;
								}
							}
							key = string.Format("{0}{1}", new object[] { (i + 1).ToString(), columnPrefix });
							value = slideGrid[i][j];
							if (!slideReplacementList.Keys.Contains(key))
								slideReplacementList.Add(key, value);
						}
					}
					else
					{
						key = string.Format("{0}{1}", (i + 1).ToString(), "a");
						value = slideGrid[i][0];
						if (!slideReplacementList.Keys.Contains(key))
							slideReplacementList.Add(key, value);
						key = string.Format("{0}{1}", (i + 1).ToString(), "b");
						value = "Merge";
						if (!slideReplacementList.Keys.Contains(key))
							slideReplacementList.Add(key, value);
						key = string.Format("{0}{1}", (i + 1).ToString(), "c");
						if (!slideReplacementList.Keys.Contains(key))
							slideReplacementList.Add(key, value);
						key = string.Format("{0}{1}", (i + 1).ToString(), "d");
						if (!slideReplacementList.Keys.Contains(key))
							slideReplacementList.Add(key, value);
						key = string.Format("{0}{1}", (i + 1).ToString(), "e");
						if (!slideReplacementList.Keys.Contains(key))
							slideReplacementList.Add(key, value);
						key = string.Format("{0}{1}", (i + 1).ToString(), "f");
						if (!slideReplacementList.Keys.Contains(key))
							slideReplacementList.Add(key, value);
						key = string.Format("{0}{1}", (i + 1).ToString(), "g");
						if (!slideReplacementList.Keys.Contains(key))
							slideReplacementList.Add(key, value);
						key = string.Format("{0}{1}", (i + 1).ToString(), "h");
						if (!slideReplacementList.Keys.Contains(key))
							slideReplacementList.Add(key, value);
						key = string.Format("{0}{1}", (i + 1).ToString(), "i");
						if (!slideReplacementList.Keys.Contains(key))
							slideReplacementList.Add(key, value);
						key = string.Format("{0}{1}", (i + 1).ToString(), "j");
						if (!slideReplacementList.Keys.Contains(key))
							slideReplacementList.Add(key, value);
						value = "MergeComment";
						key = string.Format("{0}{1}", (i + 1).ToString(), "k");
						if (!slideReplacementList.Keys.Contains(key))
							slideReplacementList.Add(key, value);
					}
				}
				OutputReplacementsLists.Add(slideReplacementList);
				slideNumber++;
			}
		}

		private string[][][] GetGrid()
		{
			var result = new List<string[][]>();
			var slide = new List<string[]>();
			var row = new SortedDictionary<int, string>();
			var adNotes = new SortedDictionary<int, string>();
			var printProduct = PrintProduct;
			int rowCountPerSlide = Controller.Instance.Grids.DetailedGrid.ShowCommentsHeader ? OutputManager.DetailedGridGridBasedRowsCountWithNotes : OutputManager.DetailedGridGridBasedRowsCountWithoutNotes;
			int insertsCount = printProduct.Inserts.Count;
			var totalRowCount = insertsCount;
			if (ShowDigitalLegend && !String.IsNullOrEmpty(DigitalLegendInfo))
				totalRowCount++;
			for (int i = 0; i < totalRowCount; i += rowCountPerSlide)
			{
				slide.Clear();
				for (int j = 0; j < rowCountPerSlide; j++)
				{
					row.Clear();
					if ((i + j) < insertsCount)
					{
						if (Controller.Instance.Grids.DetailedGrid.ShowCommentsHeader)
						{
							adNotes.Clear();
							int maxNumber = 12;
							if (Controller.Instance.Grids.DetailedGrid.ShowCommentsInPreview && !string.IsNullOrEmpty(printProduct.Inserts[i + j].FullComment))
								adNotes.Add(Controller.Instance.Grids.DetailedGrid.PositionCommentsInPreview > 0 && !adNotes.Keys.Contains(Controller.Instance.Grids.DetailedGrid.PositionCommentsInPreview) ? Controller.Instance.Grids.DetailedGrid.PositionCommentsInPreview : ++maxNumber, printProduct.Inserts[i + j].FullComment);
							if (Controller.Instance.Grids.DetailedGrid.ShowSectionInPreview && !string.IsNullOrEmpty(printProduct.Inserts[i + j].FullSection))
								adNotes.Add(Controller.Instance.Grids.DetailedGrid.PositionSectionInPreview > 0 && !adNotes.Keys.Contains(Controller.Instance.Grids.DetailedGrid.PositionSectionInPreview) ? Controller.Instance.Grids.DetailedGrid.PositionSectionInPreview : ++maxNumber, "Section: " + printProduct.Inserts[i + j].FullSection);
							if (Controller.Instance.Grids.DetailedGrid.ShowMechanicalsInPreview && !string.IsNullOrEmpty(printProduct.Inserts[i + j].Mechanicals))
								adNotes.Add(Controller.Instance.Grids.DetailedGrid.PositionMechanicalsInPreview > 0 && !adNotes.Keys.Contains(Controller.Instance.Grids.DetailedGrid.PositionMechanicalsInPreview) ? Controller.Instance.Grids.DetailedGrid.PositionMechanicalsInPreview : ++maxNumber, "Mech: " + printProduct.Inserts[i + j].MechanicalsOutput);
							if (Controller.Instance.Grids.DetailedGrid.ShowDeliveryInPreview && !string.IsNullOrEmpty(printProduct.Inserts[i + j].Delivery))
								adNotes.Add(Controller.Instance.Grids.DetailedGrid.PositionDeliveryInPreview > 0 && !adNotes.Keys.Contains(Controller.Instance.Grids.DetailedGrid.PositionDeliveryInPreview) ? Controller.Instance.Grids.DetailedGrid.PositionDeliveryInPreview : ++maxNumber, "Delivery: " + printProduct.Inserts[i + j].Delivery);
							if (Controller.Instance.Grids.DetailedGrid.ShowPublicationInPreview && !string.IsNullOrEmpty(printProduct.Inserts[i + j].Publication))
								adNotes.Add(Controller.Instance.Grids.DetailedGrid.PositionPublicationInPreview > 0 && !adNotes.Keys.Contains(Controller.Instance.Grids.DetailedGrid.PositionPublicationInPreview) ? Controller.Instance.Grids.DetailedGrid.PositionPublicationInPreview : ++maxNumber, "Publication: " + printProduct.Inserts[i + j].Publication);
							if (Controller.Instance.Grids.DetailedGrid.ShowPageSizeInPreview && !string.IsNullOrEmpty(printProduct.Inserts[i + j].PageSize))
								adNotes.Add(Controller.Instance.Grids.DetailedGrid.PositionPageSizeInPreview > 0 && !adNotes.Keys.Contains(Controller.Instance.Grids.DetailedGrid.PositionPageSizeInPreview) ? Controller.Instance.Grids.DetailedGrid.PositionPageSizeInPreview : ++maxNumber, "Page Size: " + printProduct.Inserts[i + j].PageSizeOutput);
							if (Controller.Instance.Grids.DetailedGrid.ShowPercentOfPageInPreview && !string.IsNullOrEmpty(printProduct.Inserts[i + j].PercentOfPage))
								adNotes.Add(Controller.Instance.Grids.DetailedGrid.PositionPercentOfPageInPreview > 0 && !adNotes.Keys.Contains(Controller.Instance.Grids.DetailedGrid.PositionPercentOfPageInPreview) ? Controller.Instance.Grids.DetailedGrid.PositionPercentOfPageInPreview : ++maxNumber, "% of Page: " + printProduct.Inserts[i + j].PercentOfPageOutput);
							if (Controller.Instance.Grids.DetailedGrid.ShowDimensionsInPreview && !string.IsNullOrEmpty(printProduct.Inserts[i + j].Dimensions))
								adNotes.Add(Controller.Instance.Grids.DetailedGrid.PositionDimensionsInPreview > 0 && !adNotes.Keys.Contains(Controller.Instance.Grids.DetailedGrid.PositionDimensionsInPreview) ? Controller.Instance.Grids.DetailedGrid.PositionDimensionsInPreview : ++maxNumber, "Col. x Inches: " + printProduct.Inserts[i + j].Dimensions);
							if (Controller.Instance.Grids.DetailedGrid.ShowColumnInchesInPreview && !string.IsNullOrEmpty(printProduct.Inserts[i + j].SquareStringFormatted))
								adNotes.Add(Controller.Instance.Grids.DetailedGrid.PositionColumnInchesInPreview > 0 && !adNotes.Keys.Contains(Controller.Instance.Grids.DetailedGrid.PositionColumnInchesInPreview) ? Controller.Instance.Grids.DetailedGrid.PositionColumnInchesInPreview : ++maxNumber, "Total Col In: " + printProduct.Inserts[i + j].SquareStringFormatted + " col. in.");
							if (Controller.Instance.Grids.DetailedGrid.ShowReadershipInPreview && !string.IsNullOrEmpty(printProduct.Inserts[i + j].Readership))
								adNotes.Add(Controller.Instance.Grids.DetailedGrid.PositionReadershipInPreview > 0 && !adNotes.Keys.Contains(Controller.Instance.Grids.DetailedGrid.PositionReadershipInPreview) ? Controller.Instance.Grids.DetailedGrid.PositionReadershipInPreview : ++maxNumber, "Readership: " + printProduct.Inserts[i + j].Readership);
							if (Controller.Instance.Grids.DetailedGrid.ShowDeadlineInPreview && !string.IsNullOrEmpty(printProduct.Inserts[i + j].Deadline))
								adNotes.Add(Controller.Instance.Grids.DetailedGrid.PositionDeadlineInPreview > 0 && !adNotes.Keys.Contains(Controller.Instance.Grids.DetailedGrid.PositionDeadlineInPreview) ? Controller.Instance.Grids.DetailedGrid.PositionDeadlineInPreview : ++maxNumber, "Deadline: " + printProduct.Inserts[i + j].DeadlineForOutput);
							if (adNotes.Count > 0)
								row.Add(-1, string.Join(",   ", adNotes.Values.ToArray()));
							else
								row.Add(-1, "            ");
						}
						if (Controller.Instance.Grids.DetailedGrid.PositionID != -1 && Controller.Instance.Grids.DetailedGrid.ShowIDHeader)
							row.Add(Controller.Instance.Grids.DetailedGrid.PositionID, printProduct.Inserts[i + j].ID);
						if (Controller.Instance.Grids.DetailedGrid.PositionDate != -1 && Controller.Instance.Grids.DetailedGrid.ShowDateHeader)
							row.Add(Controller.Instance.Grids.DetailedGrid.PositionDate, printProduct.Inserts[i + j].Date.HasValue ? printProduct.Inserts[i + j].Date.Value.ToString("MM/dd/yy") : String.Empty);
						if (Controller.Instance.Grids.DetailedGrid.PositionPCI != -1 && Controller.Instance.Grids.DetailedGrid.ShowPCIHeader)
							row.Add(Controller.Instance.Grids.DetailedGrid.PositionPCI, printProduct.Inserts[i + j].PCIRate.HasValue ? (printProduct.Inserts[i + j].PCIRate.Value.ToString("$#,###.00")) : "N/A");
						if (Controller.Instance.Grids.DetailedGrid.PositionCost != -1 && Controller.Instance.Grids.DetailedGrid.ShowCostHeader)
							row.Add(Controller.Instance.Grids.DetailedGrid.PositionCost, printProduct.Inserts[i + j].ADRate.ToString("$#,###.00"));
						if (Controller.Instance.Grids.DetailedGrid.PositionDiscount != -1 && Controller.Instance.Grids.DetailedGrid.ShowDiscountHeader)
							row.Add(Controller.Instance.Grids.DetailedGrid.PositionDiscount, printProduct.Inserts[i + j].DiscountRate.ToString("$#,###.00"));
						if (Controller.Instance.Grids.DetailedGrid.PositionColor != -1 && Controller.Instance.Grids.DetailedGrid.ShowColorHeader)
							row.Add(Controller.Instance.Grids.DetailedGrid.PositionColor, printProduct.ColorOption != ColorOptions.BlackWhite && printProduct.ColorPricing != ColorPricingType.ColorIncluded ? printProduct.Inserts[i + j].ColorPricingCalculated.ToString("$#,###.00") : printProduct.Inserts[i + j].ColorPricingObject.ToString());
						if (Controller.Instance.Grids.DetailedGrid.PositionFinalCost != -1 && Controller.Instance.Grids.DetailedGrid.ShowFinalCostHeader)
							row.Add(Controller.Instance.Grids.DetailedGrid.PositionFinalCost, printProduct.Inserts[i + j].FinalRate.ToString("$#,###.00"));
						if (Controller.Instance.Grids.DetailedGrid.PositionIndex != -1 && Controller.Instance.Grids.DetailedGrid.ShowIndexHeader)
							row.Add(Controller.Instance.Grids.DetailedGrid.PositionIndex, printProduct.Inserts[i + j].Index.ToString("#,##0"));
						if (Controller.Instance.Grids.DetailedGrid.PositionSquare != -1 && Controller.Instance.Grids.DetailedGrid.ShowSquareHeader)
							row.Add(Controller.Instance.Grids.DetailedGrid.PositionSquare, "'" + printProduct.Inserts[i + j].SquareStringFormatted);
						if (Controller.Instance.Grids.DetailedGrid.PositionPageSize != -1 && Controller.Instance.Grids.DetailedGrid.ShowPageSizeHeader)
							row.Add(Controller.Instance.Grids.DetailedGrid.PositionPageSize, printProduct.Inserts[i + j].PageSizeOutput);
						if (Controller.Instance.Grids.DetailedGrid.PositionPercentOfPage != -1 && Controller.Instance.Grids.DetailedGrid.ShowPercentOfPageHeader)
							row.Add(Controller.Instance.Grids.DetailedGrid.PositionPercentOfPage, printProduct.Inserts[i + j].PercentOfPageOutput);
						if (Controller.Instance.Grids.DetailedGrid.PositionMechanicals != -1 && Controller.Instance.Grids.DetailedGrid.ShowMechanicalsHeader)
							row.Add(Controller.Instance.Grids.DetailedGrid.PositionMechanicals, printProduct.Inserts[i + j].MechanicalsOutput);
						if (Controller.Instance.Grids.DetailedGrid.PositionPublication != -1 && Controller.Instance.Grids.DetailedGrid.ShowPublicationHeader)
							row.Add(Controller.Instance.Grids.DetailedGrid.PositionPublication, printProduct.Name);
						if (Controller.Instance.Grids.DetailedGrid.PositionDimensions != -1 && Controller.Instance.Grids.DetailedGrid.ShowDimensionsHeader)
							row.Add(Controller.Instance.Grids.DetailedGrid.PositionDimensions, printProduct.Inserts[i + j].DimensionsOutput);
						if (Controller.Instance.Grids.DetailedGrid.PositionSection != -1 && Controller.Instance.Grids.DetailedGrid.ShowSectionHeader)
							row.Add(Controller.Instance.Grids.DetailedGrid.PositionSection, printProduct.Inserts[i + j].FullSection);
						if (Controller.Instance.Grids.DetailedGrid.PositionReadership != -1 && Controller.Instance.Grids.DetailedGrid.ShowReadershipHeader)
							row.Add(Controller.Instance.Grids.DetailedGrid.PositionReadership, printProduct.Inserts[i + j].Readership);
						if (Controller.Instance.Grids.DetailedGrid.PositionDelivery != -1 && Controller.Instance.Grids.DetailedGrid.ShowDeliveryHeader)
							row.Add(Controller.Instance.Grids.DetailedGrid.PositionDelivery, printProduct.Inserts[i + j].Delivery);
						if (Controller.Instance.Grids.DetailedGrid.PositionDeadline != -1 && Controller.Instance.Grids.DetailedGrid.ShowDeadlineHeader)
							row.Add(Controller.Instance.Grids.DetailedGrid.PositionDeadline, printProduct.Inserts[i + j].DeadlineForOutput);
						if (row.Values.Count > 0)
							slide.Add(row.Values.ToArray());
					}
				}
				if (i >= (totalRowCount - rowCountPerSlide))
				{
					if (ShowDigitalLegend && !String.IsNullOrEmpty(DigitalLegendInfo))
						slide.Add(new[] { DigitalLegendInfo });
				}
				result.Add(slide.ToArray());
			}
			return result.ToArray();
		}
		#endregion
	}
}