using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.Utils.Menu;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.BandedGrid.ViewInfo;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using NewBizWiz.CommonGUI.Common;
using NewBizWiz.CommonGUI.ImageGallery;
using NewBizWiz.CommonGUI.ToolForms;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.MediaSchedule;
using NewBizWiz.MediaSchedule.Controls.BusinessClasses;
using Application = System.Windows.Forms.Application;
using Font = System.Drawing.Font;
using Point = System.Drawing.Point;
using Schedule = NewBizWiz.Core.MediaSchedule.Schedule;

namespace NewBizWiz.MediaSchedule.Controls.PresentationClasses.ScheduleControls
{
	[ToolboxItem(false)]
	public partial class ScheduleSectionControl : UserControl
	{
		private readonly List<BandedGridColumn> _spotColumns = new List<BandedGridColumn>();
		private bool _allowToSave;
		private readonly Font _spotColumnFont = new Font("Arial", 14);
		protected GridDragDropHelper _dragDropHelper;
		protected Schedule _localSchedule;
		protected string _helpKey;

		public ScheduleSectionControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			xtraTabPageOptionsLine.Text = MediaMetaData.Instance.DataTypeString;
			if ((base.CreateGraphics()).DpiX > 96)
			{
				var font = new Font(laTotalPeriodsTitle.Font.FontFamily, laTotalPeriodsTitle.Font.Size - 2, laTotalPeriodsTitle.Font.Style);
				laTotalPeriodsTitle.Font = font;
				laTotalGRPTitle.Font = font;
				laTotalCPPTitle.Font = font;
				laAvgRateTitle.Font = font;
				laTotalCostTitle.Font = font;
				laNetRateTitle.Font = font;
				laAgencyDiscountTitle.Font = font;
				font = new Font(laTotalPeriodsValue.Font.FontFamily, laTotalPeriodsValue.Font.Size - 2, laTotalPeriodsValue.Font.Style);
				laTotalPeriodsValue.Font = font;
				laTotalGRPValue.Font = font;
				laTotalCPPValue.Font = font;
				laAvgRateValue.Font = font;
				laTotalCostValue.Font = font;
				laNetRateValue.Font = font;
				laAgencyDiscountValue.Font = font;
			}
			repositoryItemComboBoxDays.Enter += Utilities.Instance.Editor_Enter;
			repositoryItemComboBoxDays.MouseDown += Utilities.Instance.Editor_MouseDown;
			repositoryItemComboBoxDays.MouseUp += Utilities.Instance.Editor_MouseUp;
			repositoryItemComboBoxDayparts.Enter += Utilities.Instance.Editor_Enter;
			repositoryItemComboBoxDayparts.MouseDown += Utilities.Instance.Editor_MouseDown;
			repositoryItemComboBoxDayparts.MouseUp += Utilities.Instance.Editor_MouseUp;
			repositoryItemComboBoxLengths.Enter += Utilities.Instance.Editor_Enter;
			repositoryItemComboBoxLengths.MouseDown += Utilities.Instance.Editor_MouseDown;
			repositoryItemComboBoxLengths.MouseUp += Utilities.Instance.Editor_MouseUp;
			repositoryItemPopupContainerEditProgram.Enter += Utilities.Instance.Editor_Enter;
			repositoryItemPopupContainerEditProgram.MouseDown += Utilities.Instance.Editor_MouseDown;
			repositoryItemPopupContainerEditProgram.MouseUp += Utilities.Instance.Editor_MouseUp;
			repositoryItemTextEditProgram.Enter += Utilities.Instance.Editor_Enter;
			repositoryItemTextEditProgram.MouseDown += Utilities.Instance.Editor_MouseDown;
			repositoryItemTextEditProgram.MouseUp += Utilities.Instance.Editor_MouseUp;
			repositoryItemComboBoxStations.Enter += Utilities.Instance.Editor_Enter;
			repositoryItemComboBoxStations.MouseDown += Utilities.Instance.Editor_MouseDown;
			repositoryItemComboBoxStations.MouseUp += Utilities.Instance.Editor_MouseUp;
			repositoryItemComboBoxTimes.Enter += Utilities.Instance.Editor_Enter;
			repositoryItemComboBoxTimes.MouseDown += Utilities.Instance.Editor_MouseDown;
			repositoryItemComboBoxTimes.MouseUp += Utilities.Instance.Editor_MouseUp;
			repositoryItemSpinEdit000s.Enter += Utilities.Instance.Editor_Enter;
			repositoryItemSpinEdit000s.MouseDown += Utilities.Instance.Editor_MouseDown;
			repositoryItemSpinEdit000s.MouseUp += Utilities.Instance.Editor_MouseUp;
			repositoryItemSpinEditRate.Enter += Utilities.Instance.Editor_Enter;
			repositoryItemSpinEditRate.MouseDown += Utilities.Instance.Editor_MouseDown;
			repositoryItemSpinEditRate.MouseUp += Utilities.Instance.Editor_MouseUp;
			repositoryItemSpinEditRating.Enter += Utilities.Instance.Editor_Enter;
			repositoryItemSpinEditRating.MouseDown += Utilities.Instance.Editor_MouseDown;
			repositoryItemSpinEditRating.MouseUp += Utilities.Instance.Editor_MouseUp;
			repositoryItemSpinEditSpot.Enter += Utilities.Instance.Editor_Enter;
			repositoryItemSpinEditSpot.MouseDown += Utilities.Instance.Editor_MouseDown;
			repositoryItemSpinEditSpot.MouseUp += Utilities.Instance.Editor_MouseUp;
			quarterSelectorControl.QuarterSelected += QuarterCheckedChanged;
			retractableBarControl.Collapse(true);
		}
		public bool AllowToLeaveControl
		{
			get
			{
				if (SettingsNotSaved)
					SaveSchedule();
				return true;
			}
		}
		protected string SpotTitle
		{
			get { return _localSchedule.SelectedSpotType.ToString(); }
		}

		public bool SettingsNotSaved { get; set; }

		public ScheduleSection ScheduleSection
		{
			get { return _localSchedule.Section; }
		}
		public virtual ButtonItem ThemeButton
		{
			get { return null; }
		}
		public virtual ButtonItem PowerPointButton
		{
			get { return null; }
		}
		public virtual ButtonItem PdfButton
		{
			get { return null; }
		}
		public virtual ButtonItem EmailButton
		{
			get { return null; }
		}
		public virtual ButtonItem PreviewButton
		{
			get { return null; }
		}
		public virtual RibbonBar QuarterBar
		{
			get { return null; }
		}
		public virtual ButtonItem QuarterButton
		{
			get { return null; }
		}
		public SlideType SlideType
		{
			get
			{
				if (_localSchedule == null) return SlideType.None;
				switch (_localSchedule.SelectedSpotType)
				{
					case SpotType.Week:
						return MediaMetaData.Instance.DataType == MediaDataType.TV ? SlideType.TVWeeklySchedule : SlideType.RadioWeeklySchedule;
					case SpotType.Month:
						return MediaMetaData.Instance.DataType == MediaDataType.TV ? SlideType.TVMonthlySchedule : SlideType.RadioMonthlySchedule;
					default:
						return SlideType.None;
				}
			}
		}
		#region Methods
		private void CloseActiveEditorsonOutSideClick(object sender, EventArgs e)
		{
			labelControlScheduleInfo.Focus();
			advBandedGridViewSchedule.CloseEditor();
			advBandedGridViewSchedule.FocusedColumn = null;
		}

		private void UpdateGrid(bool quickLoad)
		{
			int focussedRow = advBandedGridViewSchedule.FocusedRowHandle;
			advBandedGridViewSchedule.BeginUpdate();
			gridControlSchedule.DataSource = null;
			gridControlSchedule.DataMember = string.Empty;
			bandedGridColumnDay.FieldName = ScheduleSection.ProgramDayDataColumnName;
			bandedGridColumnDaypart.FieldName = ScheduleSection.ProgramDaypartDataColumnName;
			bandedGridColumnCPP.FieldName = ScheduleSection.ProgramCPPDataColumnName;
			bandedGridColumnCPP.SummaryItem.FieldName = ScheduleSection.ProgramTotalCPPDataColumnName;
			bandedGridColumnGRP.FieldName = ScheduleSection.ProgramGRPDataColumnName;
			bandedGridColumnGRP.SummaryItem.FieldName = ScheduleSection.ProgramGRPDataColumnName;
			bandedGridColumnIndex.FieldName = ScheduleSection.ProgramIndexDataColumnName;
			bandedGridColumnLength.FieldName = ScheduleSection.ProgramLengthDataColumnName;
			bandedGridColumnLogoImage.FieldName = ScheduleSection.ProgramLogoImageDataColumnName;
			bandedGridColumnLogoSource.FieldName = ScheduleSection.ProgramLogoSourceDataColumnName;
			bandedGridColumnName.FieldName = ScheduleSection.ProgramNameDataColumnName;
			bandedGridColumnRate.FieldName = ScheduleSection.ProgramRateDataColumnName;
			bandedGridColumnRating.FieldName = ScheduleSection.ProgramRatingDataColumnName;
			bandedGridColumnStation.FieldName = ScheduleSection.ProgramStationDataColumnName;
			bandedGridColumnTime.FieldName = ScheduleSection.ProgramTimeDataColumnName;
			bandedGridColumnTotalSpots.FieldName = ScheduleSection.ProgramTotalSpotDataColumnName;
			bandedGridColumnTotalSpots.SummaryItem.FieldName = ScheduleSection.ProgramTotalSpotDataColumnName;
			bandedGridColumnCost.FieldName = ScheduleSection.ProgramCostDataColumnName;
			bandedGridColumnCost.SummaryItem.FieldName = ScheduleSection.ProgramCostDataColumnName;
			ScheduleSection.GenerateDataSource();
			if (!quickLoad)
				BuildSpotColumns();
			if (ScheduleSection.Programs.Count > 0)
			{
				pbNoPrograms.Visible = false;
				gridControlSchedule.Visible = true;
				gridControlSchedule.BringToFront();
				gridControlSchedule.DataSource = ScheduleSection.DataSource;
			}
			else
			{
				gridControlSchedule.Visible = false;
				pbNoPrograms.Visible = true;
				pbNoPrograms.BringToFront();
			}
			advBandedGridViewSchedule.EndUpdate();
			if (_dragDropHelper == null && ScheduleSection.Programs.Count > 0)
			{
				_dragDropHelper = new GridDragDropHelper(advBandedGridViewSchedule, true, 40);
				_dragDropHelper.AfterDrop += gridControlSchedule_AfterDrop;
			}
			if (focussedRow >= 0 && focussedRow < advBandedGridViewSchedule.RowCount)
				advBandedGridViewSchedule.FocusedRowHandle = focussedRow;
		}

		private void UpdateTotalsVisibility()
		{
			pnTotalCPP.Visible = ScheduleSection.ShowTotalCPP;
			pnTotalCPP.SendToBack();
			pnTotalGRP.Visible = ScheduleSection.ShowTotalGRP;
			pnTotalGRP.SendToBack();
			pnTotalSpots.Visible = ScheduleSection.ShowTotalSpots;
			pnTotalSpots.SendToBack();
			pnTotalPeriods.Visible = ScheduleSection.ShowTotalPeriods;
			pnTotalPeriods.SendToBack();
			pnAvgRate.Visible = ScheduleSection.ShowAverageRate;
			pnAvgRate.BringToFront();
			pnTotalCost.Visible = ScheduleSection.ShowTotalRate;
			pnTotalCost.BringToFront();
			pnNetRate.Visible = ScheduleSection.ShowNetRate;
			pnNetRate.BringToFront();
			pnAgencyDiscount.Visible = ScheduleSection.ShowDiscount;
			pnAgencyDiscount.BringToFront();
		}

		private void UpdateColumnsAccordingScreenSize()
		{
			gridBandStation.Visible = ScheduleSection.ShowStation || ScheduleSection.ShowDaypart;
			if (ScheduleSection.ShowStation)
				advBandedGridViewSchedule.SetColumnPosition(bandedGridColumnStation, 0, 0);
			if (ScheduleSection.ShowDaypart)
				advBandedGridViewSchedule.SetColumnPosition(bandedGridColumnDaypart, 1, 0);
			gridBandTotals.Visible = ScheduleSection.ShowTotalSpots || ScheduleSection.ShowCost || ScheduleSection.ShowGRP || ScheduleSection.ShowCPP;
			if (ScheduleSection.ShowTotalSpots)
				advBandedGridViewSchedule.SetColumnPosition(bandedGridColumnTotalSpots, 0, 0);
			if (ScheduleSection.ShowGRP)
				advBandedGridViewSchedule.SetColumnPosition(bandedGridColumnGRP, 1, 0);
			var secondColumnIndex = ScheduleSection.ShowTotalSpots || ScheduleSection.ShowGRP ? 1 : 0;
			if (ScheduleSection.ShowCost)
				advBandedGridViewSchedule.SetColumnPosition(bandedGridColumnCost, 0, secondColumnIndex);
			if (ScheduleSection.ShowCPP)
				advBandedGridViewSchedule.SetColumnPosition(bandedGridColumnCPP, 1, secondColumnIndex);
		}

		private void UpdateSpotsStatus()
		{
			if (ScheduleSection.ShowSpots)
			{
				gridBandSpots.Visible = true;
				QuarterButton.Enabled = true;
				quarterSelectorControl.Enabled = QuarterButton.Checked;
				var selectedQuarter = quarterSelectorControl.SelectedQuarter;
				foreach (var column in _spotColumns)
				{
					var quarter = ((object[])column.Tag)[0] as Quarter;
					column.Visible = (quarter != null && selectedQuarter != null && quarter.ToString() == selectedQuarter.ToString()) || !QuarterButton.Checked;
				}
			}
			else
			{
				gridBandSpots.Visible = false;
				QuarterButton.Enabled = false;
				quarterSelectorControl.Enabled = false;
			}
		}

		private void UpdateTotalsValues()
		{
			if (ScheduleSection.Programs.Any())
			{
				pnBottom.Visible = true;
				laTotalPeriodsValue.Text = ScheduleSection.TotalActivePeriods.ToString("#,##0");
				laTotalSpotsValue.Text = ScheduleSection.TotalSpots.ToString("#,##0");
				laTotalGRPValue.Text = ScheduleSection.TotalGRP.ToString(_localSchedule.DemoType == DemoType.Rtg ? "#,###.0" : "#,##0");
				laTotalCPPValue.Text = ScheduleSection.TotalCPP.ToString("$#,###.00");
				laAvgRateValue.Text = ScheduleSection.AvgRate.ToString("$#,###.00");
				laTotalCostValue.Text = ScheduleSection.TotalCost.ToString(ScheduleSection.UseDecimalRates ? "$#,##0.00" : "$#,##0");
				laNetRateValue.Text = ScheduleSection.NetRate.ToString("$#,###.00");
				laAgencyDiscountValue.Text = ScheduleSection.Discount.ToString("$#,###.00");
			}
			else
			{
				pnBottom.Visible = false;
				laTotalPeriodsValue.Text =
				laTotalSpotsValue.Text =
				laTotalGRPValue.Text =
				laTotalCPPValue.Text =
				laAvgRateValue.Text =
				laTotalCostValue.Text =
				laNetRateValue.Text =
				laAgencyDiscountValue.Text = String.Empty;
			}
		}

		private void UpdateOutputStatus(bool enabled)
		{
			PowerPointButton.Enabled = enabled;
			ThemeButton.Enabled = enabled;
			EmailButton.Enabled = enabled;
			PreviewButton.Enabled = enabled;
			if (PdfButton != null)
				PdfButton.Enabled = enabled;
		}

		private void UpdateRateFormat()
		{
			if (ScheduleSection.UseDecimalRates)
			{
				bandedGridColumnCost.DisplayFormat.FormatString = "$#,##0.00";
				bandedGridColumnCost.SummaryItem.DisplayFormat = "{0:c2}";
				repositoryItemSpinEditRate.DisplayFormat.FormatString = "$#,##0.00";
				repositoryItemSpinEditRate.EditFormat.FormatString = "$#,##0.00";
				repositoryItemSpinEditRate.IsFloatValue = true;
			}
			else
			{
				bandedGridColumnCost.DisplayFormat.FormatString = "$#,##0";
				bandedGridColumnCost.SummaryItem.DisplayFormat = "{0:c0}";
				repositoryItemSpinEditRate.DisplayFormat.FormatString = "$#,##0";
				repositoryItemSpinEditRate.EditFormat.FormatString = "$#,##0";
				repositoryItemSpinEditRate.IsFloatValue = false;
			}
		}

		private void CloneSpots(int rowHandle, object valueToClone, int startIndex, bool everyOthere)
		{
			if (everyOthere)
				startIndex++;
			var i = 0;
			foreach (var column in _spotColumns.Where(c => c.Visible))
			{
				if (!(column.VisibleIndex > startIndex || startIndex == -1)) continue;
				if (i == 0)
					advBandedGridViewSchedule.SetRowCellValue(rowHandle, column, valueToClone);
				i++;
				if (!everyOthere || i > 1)
					i = 0;
			}
		}

		private void BuildSpotColumns()
		{
			foreach (var column in _spotColumns)
			{
				gridBandSpots.Columns.Remove(column);
				advBandedGridViewSchedule.Columns.Remove(column);
			}

			_spotColumns.Clear();

			gridBandSpots.Visible = false;
			foreach (DataColumn column in ScheduleSection.DataSource.Columns)
			{
				if (!column.ColumnName.Contains(ScheduleSection.ProgramSpotDataColumnNamePrefix)) continue;
				var bandedGridColumn = new BandedGridColumn();
				bandedGridColumn.AppearanceCell.Font = _spotColumnFont;
				bandedGridColumn.AppearanceCell.Options.UseTextOptions = true;
				bandedGridColumn.AppearanceCell.Options.UseFont = true;
				bandedGridColumn.AppearanceCell.Options.HighPriority = true;
				bandedGridColumn.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
				bandedGridColumn.AutoFillDown = true;
				bandedGridColumn.Caption = column.Caption;
				bandedGridColumn.ColumnEdit = repositoryItemSpinEditSpot;
				bandedGridColumn.FieldName = column.ColumnName;
				bandedGridColumn.ToolTip = column.ExtendedProperties["Tooltip"] as String;
				bandedGridColumn.Tag = column.ExtendedProperties["SpotSettings"];
				bandedGridColumn.OptionsColumn.FixedWidth = true;
				bandedGridColumn.RowCount = 2;
				bandedGridColumn.Width = 45;
				bandedGridColumn.Visible = true;
				bandedGridColumn.SummaryItem.FieldName = column.ColumnName;
				bandedGridColumn.SummaryItem.SummaryType = SummaryItemType.Sum;
				var isFullSpot = (Boolean)((object[])column.ExtendedProperties["SpotSettings"])[2];
				if (!isFullSpot)
				{
					bandedGridColumn.AppearanceHeader.BackColor = Color.Red;
					bandedGridColumn.AppearanceHeader.ForeColor = Color.White;
					bandedGridColumn.AppearanceHeader.Options.UseBackColor = true;
					bandedGridColumn.AppearanceHeader.Options.UseForeColor = true;
				}
				_spotColumns.Add(bandedGridColumn);
				advBandedGridViewSchedule.Columns.Add(bandedGridColumn);
				gridBandSpots.Columns.Add(bandedGridColumn);
			}
			gridBandSpots.Visible = _spotColumns.Count > 0 && ScheduleSection.ShowSpots;
		}

		private IEnumerable<DXMenuItem> GetContextMenuItems(ColumnView targetView, GridColumn targetColumn, int targetRowHandle)
		{
			var items = new List<DXMenuItem>();
			if (_spotColumns.Any(sc => sc.Visible && sc == targetColumn))
			{
				var columnName = ((object[])targetColumn.Tag)[1];
				items.Add(new DXMenuItem(String.Format("Clone {1} to all {0}s", SpotTitle, columnName), (o, args) =>
				{
					targetView.CloseEditor();
					var valueToClone = targetView.GetRowCellValue(targetRowHandle, targetColumn);
					CloneSpots(targetRowHandle, valueToClone, -1, false);
				}));
				items.Add(new DXMenuItem(String.Format("Clone {1} to all Remaining {0}s", SpotTitle, columnName), (o, args) =>
				{
					targetView.CloseEditor();
					var valueToClone = targetView.GetRowCellValue(targetRowHandle, targetColumn);
					CloneSpots(targetRowHandle, valueToClone, targetColumn.VisibleIndex, false);
				}));
				items.Add(new DXMenuItem(String.Format("Clone {1} to every other Remaining {0}s", SpotTitle, columnName), (o, args) =>
				{
					targetView.CloseEditor();
					var valueToClone = targetView.GetRowCellValue(targetRowHandle, targetColumn);
					CloneSpots(targetRowHandle, valueToClone, targetColumn.VisibleIndex, true);
				}));
				items.Add(new DXMenuItem("Wipe all Spots on this line", (o, args) => CloneSpots(targetRowHandle, null, -1, false)));
			}
			else if (targetColumn == bandedGridColumnIndex ||
					 targetColumn == bandedGridColumnLogoImage ||
					 targetColumn == bandedGridColumnStation ||
					 targetColumn == bandedGridColumnDaypart ||
					 targetColumn == bandedGridColumnName)
			{
				var sourceIndex = targetView.GetDataSourceRowIndex(targetRowHandle);
				items.Add(new DXMenuItem("Clone this Entire Line", (o, args) => CloneProgram(sourceIndex, true)));
				items.Add(new DXMenuItem("Clone Just this Program", (o, args) => CloneProgram(sourceIndex, false)));
				items.Add(new DXMenuItem("Delete this Line", DeleteProgram_Click));
			}
			return items;
		}

		public virtual void LoadSchedule(bool quickLoad)
		{
			_allowToSave = false;
			_helpKey = String.Format("{0}ly", SpotTitle.ToLower());
			laTotalPeriodsTitle.Text = String.Format("Total {0}s:", SpotTitle);
			ScheduleSection.DataChanged += ScheduleSection_DataChanged;
			if (MediaMetaData.Instance.ListManager.FlexFlightDatesAllowed &&
				(_localSchedule.FlightDateStart != _localSchedule.UserFlightDateStart || _localSchedule.FlightDateEnd != _localSchedule.UserFlightDateEnd))
				labelControlFlexFlightDatesWarning.Visible = true;
			else
				labelControlFlexFlightDatesWarning.Visible = false;
			buttonXRating.Enabled = _localSchedule.UseDemo & !String.IsNullOrEmpty(_localSchedule.Demo);
			buttonXCPP.Enabled = _localSchedule.UseDemo & !String.IsNullOrEmpty(_localSchedule.Demo);
			buttonXGRP.Enabled = _localSchedule.UseDemo & !String.IsNullOrEmpty(_localSchedule.Demo);
			buttonXTotalCPP.Enabled = _localSchedule.UseDemo & !String.IsNullOrEmpty(_localSchedule.Demo);
			buttonXTotalGRP.Enabled = _localSchedule.UseDemo & !String.IsNullOrEmpty(_localSchedule.Demo);
			buttonXRating.Text = _localSchedule.DemoType == DemoType.Rtg ? "Rtg" : "(000s)";
			bandedGridColumnRating.Caption = String.Format("{0}{1}",
				(!String.IsNullOrEmpty(_localSchedule.Demo) ? String.Format("{0} ", _localSchedule.Demo) : String.Empty),
				_localSchedule.DemoType == DemoType.Rtg ? "Rtg" : "(000)");
			bandedGridColumnRating.ColumnEdit = _localSchedule.DemoType == DemoType.Rtg ? repositoryItemSpinEditRating : repositoryItemSpinEdit000s;
			buttonXCPP.Text = _localSchedule.DemoType == DemoType.Rtg ? "CPP" : "CPM";
			buttonXTotalCPP.Text = _localSchedule.DemoType == DemoType.Rtg ? "Overall CPP" : "Overall CPM";
			laTotalCPPTitle.Text = _localSchedule.DemoType == DemoType.Rtg ? "Overall CPP:" : "Overall CPM:";
			bandedGridColumnCPP.Caption = _localSchedule.DemoType == DemoType.Rtg ? "CPP" : "CPM";
			buttonXGRP.Text = _localSchedule.DemoType == DemoType.Rtg ? "GRPs" : "Impr";
			buttonXTotalGRP.Text = _localSchedule.DemoType == DemoType.Rtg ? "Total GRPs" : "Total Impr";
			laTotalGRPTitle.Text = _localSchedule.DemoType == DemoType.Rtg ? "Total GRPs:" : "Total Impr:";
			bandedGridColumnGRP.Caption = _localSchedule.DemoType == DemoType.Rtg ? "GRPs" : "Impr";
			bandedGridColumnGRP.ColumnEdit = _localSchedule.DemoType == DemoType.Rtg ? repositoryItemSpinEditRating : repositoryItemSpinEdit000s;
			bandedGridColumnGRP.SummaryItem.DisplayFormat = _localSchedule.DemoType == DemoType.Rtg ? "{0:n1}" : "{0:n0}";
			buttonXRate.Checked = ScheduleSection.ShowRate;
			buttonXRating.Checked = ScheduleSection.ShowRating;
			buttonXCost.Checked = ScheduleSection.ShowCost;
			buttonXCPP.Checked = ScheduleSection.ShowCPP;
			buttonXDay.Checked = ScheduleSection.ShowDay;
			buttonXDaypart.Checked = ScheduleSection.ShowDaypart;
			buttonXGRP.Checked = ScheduleSection.ShowGRP;
			buttonXLength.Checked = ScheduleSection.ShowLenght;
			buttonXLogo.Checked = ScheduleSection.ShowLogo;
			buttonXStation.Checked = ScheduleSection.ShowStation;
			buttonXTime.Checked = ScheduleSection.ShowTime;
			buttonXSpots.Text = String.Format("{0}s", SpotTitle);
			buttonXSpots.Checked = ScheduleSection.ShowSpots;

			QuarterButton.Checked = ScheduleSection.ShowSelectedQuarter;
			quarterSelectorControl.InitControls(ScheduleSection.Parent.Quarters, ScheduleSection.Parent.Quarters.FirstOrDefault(q => !ScheduleSection.SelectedQuarter.HasValue || q.DateAnchor == ScheduleSection.SelectedQuarter.Value));
			QuarterBar.Enabled =
			quarterSelectorControl.Visible = ScheduleSection.Parent.Quarters.Count > 1;


			buttonXTotalPeriods.Checked = ScheduleSection.ShowTotalPeriods;
			buttonXTotalPeriods.Text = String.Format("Total {0}s", SpotTitle);
			buttonXTotalSpots.Checked = ScheduleSection.ShowTotalSpots;
			buttonXTotalGRP.Checked = ScheduleSection.ShowTotalGRP;
			buttonXTotalCPP.Checked = ScheduleSection.ShowTotalCPP;
			buttonXAvgRate.Checked = ScheduleSection.ShowAverageRate;
			buttonXTotalCost.Checked = ScheduleSection.ShowTotalRate;
			buttonXNetRate.Checked = ScheduleSection.ShowNetRate;
			buttonXDiscount.Checked = ScheduleSection.ShowDiscount;

			bandedGridColumnRate.Visible = ScheduleSection.ShowRate;
			bandedGridColumnRating.Visible = ScheduleSection.ShowRating;
			gridBandRate.Visible = ScheduleSection.ShowRate | ScheduleSection.ShowRating;
			if (ScheduleSection.ShowRate)
				advBandedGridViewSchedule.SetColumnPosition(bandedGridColumnRate, 0, 0);
			if (ScheduleSection.ShowRating)
				advBandedGridViewSchedule.SetColumnPosition(bandedGridColumnRating, 1, 0);
			bandedGridColumnCPP.Visible = ScheduleSection.ShowCPP;
			bandedGridColumnGRP.Visible = ScheduleSection.ShowGRP;
			bandedGridColumnCost.Visible = ScheduleSection.ShowCost;
			bandedGridColumnLength.Visible = ScheduleSection.ShowLenght;
			bandedGridColumnDay.Visible = ScheduleSection.ShowDay;
			bandedGridColumnTime.Visible = ScheduleSection.ShowTime;
			gridBandLogo.Visible = ScheduleSection.ShowLogo;

			bandedGridColumnStation.Visible = ScheduleSection.ShowStation;
			bandedGridColumnDaypart.Visible = ScheduleSection.ShowDaypart;

			UpdateColumnsAccordingScreenSize();

			repositoryItemComboBoxStations.Items.Clear();
			repositoryItemComboBoxStations.Items.AddRange(_localSchedule.Stations.Where(x => x.Available).Select(x => x.Name).ToArray());
			repositoryItemComboBoxDayparts.Items.Clear();
			repositoryItemComboBoxDayparts.Items.AddRange(_localSchedule.Dayparts.Where(x => x.Available).Select(x => x.Code).ToArray());

			hyperLinkEditInfoContract.Enabled = Directory.Exists(BusinessWrapper.Instance.OutputManager.ContractTemplatesFolderPath);

			if (!quickLoad)
			{
				repositoryItemComboBoxLengths.Items.Clear();
				repositoryItemComboBoxLengths.Items.AddRange(MediaMetaData.Instance.ListManager.Lengths);
				repositoryItemComboBoxDays.Items.Clear();
				repositoryItemComboBoxDays.Items.AddRange(MediaMetaData.Instance.ListManager.Days);
				repositoryItemComboBoxTimes.Items.Clear();
				repositoryItemComboBoxTimes.Items.AddRange(MediaMetaData.Instance.ListManager.Times);

				UpdateTotalsVisibility();
			}

			UpdateGrid(quickLoad);
			UpdateSpotsStatus();
			UpdateTotalsValues();
			UpdateOutputStatus(ScheduleSection.Programs.Any());
			UpdateRateFormat();



			_allowToSave = true;
			SettingsNotSaved = false;
		}

		protected virtual bool SaveSchedule(string scheduleName = "")
		{
			advBandedGridViewSchedule.CloseEditor();
			MediaMetaData.Instance.SettingsManager.SelectedColor = outputColorSelector.SelectedColor ?? String.Empty;
			MediaMetaData.Instance.SettingsManager.SaveSettings();
			SettingsNotSaved = false;
			return true;
		}

		public virtual void CloneProgram(int sourceIndex, bool fullClone)
		{
			ScheduleSection.CloneProgram(sourceIndex, fullClone);
			UpdateGrid(false);
			UpdateSpotsStatus();
			UpdateTotalsValues();
			UpdateOutputStatus(ScheduleSection.Programs.Any());
			if (advBandedGridViewSchedule.RowCount > 0)
				advBandedGridViewSchedule.FocusedRowHandle = advBandedGridViewSchedule.RowCount - 1;
			var options = new Dictionary<string, object>();
			options.Add("Advertiser", ScheduleSection.Parent.BusinessName);
			options.Add(String.Format("{0}lyTotalSpots", SpotTitle), ScheduleSection.TotalSpots);
			options.Add(String.Format("{0}lyAverageRate", SpotTitle), ScheduleSection.AvgRate);
			options.Add(String.Format("{0}lyGrossInvestment", SpotTitle), ScheduleSection.TotalCost);
			AddActivity(new UserActivity("New Program Added", options));
			SettingsNotSaved = true;
		}

		protected virtual void ScheduleSection_DataChanged(object sender, EventArgs e)
		{
			UpdateTotalsValues();
		}

		protected virtual void AddActivity(UserActivity activity) { }

		#endregion

		#region Ribbon Operations Events

		public virtual void Help_Click(object sender, EventArgs e) { }

		public virtual void Save_Click(object sender, EventArgs e) { }

		public virtual void SaveAs_Click(object sender, EventArgs e) { }

		public virtual void AddProgram_Click(object sender, EventArgs e)
		{
			ScheduleSection.AddProgram();
			UpdateGrid(false);
			UpdateSpotsStatus();
			UpdateTotalsValues();
			UpdateOutputStatus(ScheduleSection.Programs.Any());
			if (advBandedGridViewSchedule.RowCount > 0)
				advBandedGridViewSchedule.FocusedRowHandle = advBandedGridViewSchedule.RowCount - 1;
			var options = new Dictionary<string, object>();
			options.Add("Advertiser", ScheduleSection.Parent.BusinessName);
			options.Add(String.Format("{0}lyTotalSpots", SpotTitle), ScheduleSection.TotalSpots);
			options.Add(String.Format("{0}lyAverageRate", SpotTitle), ScheduleSection.AvgRate);
			options.Add(String.Format("{0}lyGrossInvestment", SpotTitle), ScheduleSection.TotalCost);
			AddActivity(new UserActivity("New Program Added", options));
			SettingsNotSaved = true;
		}

		public virtual void DeleteProgram_Click(object sender, EventArgs e)
		{
			var selectedProgramRow = advBandedGridViewSchedule.GetFocusedDataRow();
			if (selectedProgramRow == null) return;
			var selectedProgramIndex = selectedProgramRow[0].ToString();
			if (Utilities.Instance.ShowWarningQuestion(String.Format("Delete Line ID {0}?", selectedProgramIndex)) != DialogResult.Yes) return;
			ScheduleSection.DeleteProgram(advBandedGridViewSchedule.GetDataSourceRowIndex(advBandedGridViewSchedule.FocusedRowHandle));
			UpdateGrid(false);
			UpdateSpotsStatus();
			UpdateTotalsValues();
			UpdateOutputStatus(ScheduleSection.Programs.Any());
			SettingsNotSaved = true;
		}
		#endregion

		#region Options Events
		protected void TrackOptionChanged()
		{
			var options = new Dictionary<string, object>();
			options.Add("Advertiser", ScheduleSection.Parent.BusinessName);
			AddActivity(new UserActivity("Navbar Schedule Cleanup", options));
		}

		private void button_CheckedChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			ScheduleSection.ShowRate = buttonXRate.Checked;
			ScheduleSection.ShowRating = buttonXRating.Checked;
			ScheduleSection.ShowCost = buttonXCost.Checked;
			ScheduleSection.ShowCPP = buttonXCPP.Checked;
			ScheduleSection.ShowDay = buttonXDay.Checked;
			ScheduleSection.ShowDaypart = buttonXDaypart.Checked;
			ScheduleSection.ShowGRP = buttonXGRP.Checked;
			ScheduleSection.ShowLenght = buttonXLength.Checked;
			ScheduleSection.ShowLogo = buttonXLogo.Checked;
			ScheduleSection.ShowStation = buttonXStation.Checked;
			ScheduleSection.ShowTime = buttonXTime.Checked;
			ScheduleSection.ShowSpots = buttonXSpots.Checked;

			ScheduleSection.ShowTotalPeriods = buttonXTotalPeriods.Checked;
			ScheduleSection.ShowTotalSpots = buttonXTotalSpots.Checked;
			ScheduleSection.ShowTotalGRP = buttonXTotalGRP.Checked;
			ScheduleSection.ShowTotalCPP = buttonXTotalCPP.Checked;
			ScheduleSection.ShowAverageRate = buttonXAvgRate.Checked;
			ScheduleSection.ShowTotalRate = buttonXTotalCost.Checked;
			ScheduleSection.ShowNetRate = buttonXNetRate.Checked;
			ScheduleSection.ShowDiscount = buttonXDiscount.Checked;

			ScheduleSection.ShowSelectedQuarter = QuarterButton.Checked && buttonXSpots.Checked;
			var selectedQuarter = quarterSelectorControl.SelectedQuarter;
			ScheduleSection.SelectedQuarter = ScheduleSection.ShowSelectedQuarter && selectedQuarter != null ? selectedQuarter.DateAnchor : (DateTime?)null;

			bandedGridColumnRate.Visible = ScheduleSection.ShowRate;
			bandedGridColumnRating.Visible = ScheduleSection.ShowRating;
			gridBandRate.Visible = ScheduleSection.ShowRate | ScheduleSection.ShowRating;
			if (ScheduleSection.ShowRate)
				advBandedGridViewSchedule.SetColumnPosition(bandedGridColumnRate, 0, 0);
			if (ScheduleSection.ShowRating)
				advBandedGridViewSchedule.SetColumnPosition(bandedGridColumnRating, 1, 0);

			bandedGridColumnTotalSpots.Visible = ScheduleSection.ShowTotalSpots;
			bandedGridColumnGRP.Visible = ScheduleSection.ShowGRP;
			bandedGridColumnCost.Visible = ScheduleSection.ShowCost;
			bandedGridColumnCPP.Visible = ScheduleSection.ShowCPP;
			bandedGridColumnLength.Visible = ScheduleSection.ShowLenght;
			bandedGridColumnDay.Visible = ScheduleSection.ShowDay;
			bandedGridColumnTime.Visible = ScheduleSection.ShowTime;
			gridBandLogo.Visible = ScheduleSection.ShowLogo;

			bandedGridColumnStation.Visible = ScheduleSection.ShowStation;
			bandedGridColumnDaypart.Visible = ScheduleSection.ShowDaypart;
			gridBandSpots.Visible = _spotColumns.Count > 0 && ScheduleSection.ShowSpots;

			UpdateTotalsVisibility();
			UpdateTotalsValues();

			UpdateColumnsAccordingScreenSize();

			UpdateSpotsStatus();

			TrackOptionChanged();

			UpdateRateFormat();

			SettingsNotSaved = true;
		}

		public void QuarterCheckedChanged(object sender, EventArgs e)
		{
			quarterSelectorControl.Enabled = QuarterButton.Checked;
			button_CheckedChanged(sender, e);
		}

		private void labelControlFlexFlightDatesWarning_Click(object sender, EventArgs e)
		{
			using (var form = new FormFlexFlightDatesWarning())
			{
				form.ShowDialog();
			}
		}

		private void hyperLinkEditLineAdvanced_OpenLink(object sender, OpenLinkEventArgs e)
		{
			e.Handled = true;
			using (var form = new FormOutputSettings())
			{
				form.checkEditEmptySports.Text = String.Format(form.checkEditEmptySports.Text, String.Format("{0}s:", SpotTitle));
				form.checkEditEmptySports.Enabled = ScheduleSection.ShowSpots;
				form.checkEditEmptySports.Checked = !ScheduleSection.ShowEmptySpots;
				form.checkEditOutputNoBrackets.Checked = ScheduleSection.OutputNoBrackets;
				form.checkEditUseGenericDates.Checked = ScheduleSection.UseGenericDateColumns;
				form.checkEditUseDecimalRate.Checked = ScheduleSection.UseDecimalRates;
				form.checkEditOutputLimitQuarters.Visible = ScheduleSection.Parent.Quarters.Count > 1;
				form.checkEditOutputLimitQuarters.Checked = ScheduleSection.OutputPerQuater;
				form.checkEditOutputLimitPeriods.Checked = ScheduleSection.OutputMaxPeriods.HasValue;
				form.spinEditOutputLimitPeriods.EditValue = ScheduleSection.OutputMaxPeriods;
				form.checkEditOutputLimitPeriods.Text = String.Format("Max {0}s Per PPT Slide", SpotTitle);
				form.checkEditEmptySports.Enabled = ScheduleSection.ShowSpots;
				form.checkEditLockToMaster.Checked = MediaMetaData.Instance.SettingsManager.UseSlideMaster;
				if (form.ShowDialog() != DialogResult.OK) return;
				ScheduleSection.ShowEmptySpots = !form.checkEditEmptySports.Checked;
				ScheduleSection.OutputNoBrackets = form.checkEditOutputNoBrackets.Checked;
				ScheduleSection.UseGenericDateColumns = form.checkEditUseGenericDates.Checked;
				ScheduleSection.UseDecimalRates = form.checkEditUseDecimalRate.Checked;
				ScheduleSection.OutputPerQuater = form.checkEditOutputLimitQuarters.Checked;
				ScheduleSection.OutputMaxPeriods = form.spinEditOutputLimitPeriods.EditValue != null ? (Int32?)form.spinEditOutputLimitPeriods.Value : null;
				MediaMetaData.Instance.SettingsManager.UseSlideMaster = form.checkEditLockToMaster.Checked;
				UpdateGrid(false);
				UpdateRateFormat();
				TrackOptionChanged();
				SettingsNotSaved = true;
			}
		}

		private void hyperLinkEditInfoContract_OpenLink(object sender, OpenLinkEventArgs e)
		{
			e.Handled = true;
			using (var form = new FormContractSettings())
			{
				form.checkEditShowSignatureLine.Checked = ScheduleSection.ContractSettings.ShowSignatureLine;
				form.checkEditShowRatesExpiration.Checked = ScheduleSection.ContractSettings.RateExpirationDate.HasValue;
				form.checkEditShowDisclaimer.Checked = ScheduleSection.ContractSettings.ShowDisclaimer;
				form.dateEditRatesExpirationDate.EditValue = ScheduleSection.ContractSettings.RateExpirationDate;
				if (form.ShowDialog() != DialogResult.OK) return;
				ScheduleSection.ContractSettings.ShowSignatureLine = form.checkEditShowSignatureLine.Checked;
				ScheduleSection.ContractSettings.ShowDisclaimer = form.checkEditShowDisclaimer.Checked;
				ScheduleSection.ContractSettings.RateExpirationDate = (DateTime?)form.dateEditRatesExpirationDate.EditValue;
				SettingsNotSaved = true;
			}
		}

		protected void OnColorChanged(object sender, EventArgs e)
		{
			TrackOptionChanged();
			SettingsNotSaved = true;
		}
		#endregion

		#region Grid Events

		private void advBandedGridViewSchedule_CellValueChanged(object sender, CellValueChangedEventArgs e)
		{
			advBandedGridViewSchedule.CloseEditor();
			advBandedGridViewSchedule.UpdateCurrentRow();
			var options = new Dictionary<string, object>();
			options.Add("Advertiser", ScheduleSection.Parent.BusinessName);
			options.Add("Program", advBandedGridViewSchedule.GetRowCellValue(e.RowHandle, bandedGridColumnName));
			options.Add(String.Format("{0}lyTotalSpots", SpotTitle), ScheduleSection.TotalSpots);
			options.Add(String.Format("{0}lyAverageRate", SpotTitle), ScheduleSection.AvgRate);
			options.Add(String.Format("{0}lyGrossInvestment", SpotTitle), ScheduleSection.TotalCost);
			AddActivity(new UserActivity("Program Line Updated", options));
			SettingsNotSaved = true;
		}

		private void advBandedGridViewSchedule_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
		{
			if (e.Column == null) return;
			if (!e.Column.AppearanceHeader.Options.UseBackColor) return;
			var rect = e.Bounds;
			ControlPaint.DrawBorder3D(e.Graphics, e.Bounds);
			var brush =
				e.Cache.GetGradientBrush(rect, e.Column.AppearanceHeader.BackColor,
				e.Column.AppearanceHeader.BackColor2, e.Column.AppearanceHeader.GradientMode);
			rect.Inflate(-1, -1);
			// Fill column headers with the specified colors.
			e.Graphics.FillRectangle(brush, rect);
			e.Appearance.DrawString(e.Cache, e.Info.Caption, e.Info.CaptionRect);
			// Draw the filter and sort buttons.
			foreach (DevExpress.Utils.Drawing.DrawElementInfo info in e.Info.InnerElements)
			{
				if (!info.Visible) continue;
				DevExpress.Utils.Drawing.ObjectPainter.DrawObject(e.Cache, info.ElementPainter,
					info.ElementInfo);
			}
			e.Handled = true;
		}

		private void advBandedGridViewSchedule_CustomDrawFooter(object sender, RowObjectCustomDrawEventArgs e)
		{
			if (_spotColumns.Count > 0)
			{
				e.Painter.DrawObject(e.Info);
				var view = sender as AdvBandedGridView;
				var viewInfo = view.GetViewInfo() as AdvBandedGridViewInfo;
				var column = bandedGridColumnName;
				if (ScheduleSection.ShowRate)
					column = bandedGridColumnRate;
				else if (ScheduleSection.ShowRating)
					column = bandedGridColumnRating;
				else if (ScheduleSection.ShowLenght)
					column = bandedGridColumnLength;
				else if (ScheduleSection.ShowDay)
					column = bandedGridColumnDay;
				else if (ScheduleSection.ShowTime)
					column = bandedGridColumnTime;
				var x = viewInfo.ColumnsInfo[column].Bounds.X;
				var width = viewInfo.ColumnsInfo[column].Bounds.Width;
				const string spotTotalTitle = "Totals: ";
				var size = e.Appearance.CalcTextSize(e.Cache, spotTotalTitle, 50);
				var textWidth = Convert.ToInt32(size.Width) + 1;
				var textRect = new Rectangle(x + width - 50, e.Bounds.Y, textWidth, e.Bounds.Height);
				e.Appearance.DrawString(e.Cache, spotTotalTitle, textRect);
				e.Handled = true;
			}
		}

		private void advBandedGridViewSchedule_CustomRowCellEditForEditing(object sender, CustomRowCellEditEventArgs e)
		{
			if (e.Column != bandedGridColumnName || advBandedGridViewSchedule.FocusedRowHandle == GridControl.InvalidRowHandle) return;
			var station = advBandedGridViewSchedule.GetRowCellValue(advBandedGridViewSchedule.FocusedRowHandle, bandedGridColumnStation) as String;
			var daypart = advBandedGridViewSchedule.GetRowCellValue(advBandedGridViewSchedule.FocusedRowHandle, bandedGridColumnDaypart) as String;
			gridColumnProgramSourceStation.Visible = String.IsNullOrEmpty(station);
			gridColumnProgramSourceDaypart.Visible = String.IsNullOrEmpty(daypart);
			var daypartFromList = MediaMetaData.Instance.ListManager.Dayparts.FirstOrDefault(x => x.Code.Equals(daypart));
			gridColumnProgramSourceName.Caption = String.Format("Program ({0})", daypartFromList != null ? daypartFromList.Name : "All Programming");
			var dataSource = new List<SourceProgram>();
			if (!String.IsNullOrEmpty(station) && (!String.IsNullOrEmpty(daypart) || !ScheduleSection.ShowDaypart))
				dataSource.AddRange(MediaMetaData.Instance.ListManager.SourcePrograms.Where(x => (x.Station.Equals(station) || String.IsNullOrEmpty(station)) && (!ScheduleSection.ShowDaypart || (x.Daypart.Equals(daypart) || String.IsNullOrEmpty(daypart)))));
			else
				dataSource.AddRange(MediaMetaData.Instance.ListManager.SourcePrograms.OrderBy(sp => sp.Daypart));
			if (dataSource.Any())
			{
				gridViewProgramSource.DoubleClick -= gridViewProgramSource_DoubleClick;
				gridControlProgramSource.DataSource = dataSource;
				gridViewProgramSource.DoubleClick += gridViewProgramSource_DoubleClick;
				e.RepositoryItem = repositoryItemPopupContainerEditProgram;
			}
			else
				e.RepositoryItem = repositoryItemTextEditProgram;
		}

		private void gridViewProgramSource_DoubleClick(object sender, EventArgs e)
		{
			popupContainerControlProgramSource.OwnerEdit.ClosePopup();
		}

		private void repositoryItemPopupContainerEditProgram_CloseUp(object sender, CloseUpEventArgs e)
		{
			if (e.CloseMode != PopupCloseMode.Normal) return;
			if (gridViewProgramSource.FocusedRowHandle != GridControl.InvalidRowHandle)
			{
				var program = MediaMetaData.Instance.ListManager.SourcePrograms.FirstOrDefault(x => x.Id.Equals(gridViewProgramSource.GetRowCellValue(gridViewProgramSource.FocusedRowHandle, gridColumnProgramSourceId).ToString()));
				if (program != null)
				{
					advBandedGridViewSchedule.CellValueChanged -= advBandedGridViewSchedule_CellValueChanged;
					e.Value = program.Name;
					if (advBandedGridViewSchedule.GetRowCellValue(advBandedGridViewSchedule.FocusedRowHandle, bandedGridColumnDaypart) == null || string.IsNullOrEmpty(advBandedGridViewSchedule.GetRowCellValue(advBandedGridViewSchedule.FocusedRowHandle, bandedGridColumnDaypart).ToString()))
						advBandedGridViewSchedule.SetRowCellValue(advBandedGridViewSchedule.FocusedRowHandle, bandedGridColumnDaypart, program.Daypart);
					advBandedGridViewSchedule.SetRowCellValue(advBandedGridViewSchedule.FocusedRowHandle, bandedGridColumnDay, program.Day);
					advBandedGridViewSchedule.SetRowCellValue(advBandedGridViewSchedule.FocusedRowHandle, bandedGridColumnTime, program.Time);
					if (string.IsNullOrEmpty(advBandedGridViewSchedule.GetRowCellValue(advBandedGridViewSchedule.FocusedRowHandle, bandedGridColumnLength).ToString()))
						advBandedGridViewSchedule.SetRowCellValue(advBandedGridViewSchedule.FocusedRowHandle, bandedGridColumnLength, MediaMetaData.Instance.ListManager.Lengths.FirstOrDefault());
					if (_localSchedule.ImportDemo && _localSchedule.UseDemo)
					{
						var demo = program.Demos.FirstOrDefault(d => d.Name.Equals(_localSchedule.Demo) && d.Source.Equals(_localSchedule.Source) && d.DemoType == _localSchedule.DemoType);
						if (demo != null)
							advBandedGridViewSchedule.SetRowCellValue(advBandedGridViewSchedule.FocusedRowHandle, bandedGridColumnRating, demo.Value);
					}
					advBandedGridViewSchedule.CellValueChanged += advBandedGridViewSchedule_CellValueChanged;
				}
			}
			e.AcceptValue = true;
		}

		private void repositoryItemPopupContainerEditProgram_Closed(object sender, ClosedEventArgs e)
		{
			advBandedGridViewSchedule.CloseEditor();
		}

		private void advBandedGridViewSchedule_MouseDown(object sender, MouseEventArgs e)
		{
			var view = sender as AdvBandedGridView;
			if (view == null) return;
			var hInfo = view.CalcHitInfo(e.Location);
			if (hInfo.HitTest != BandedGridHitTest.RowCell)
				CloseActiveEditorsonOutSideClick(null, null);
		}

		private void advBandedGridViewSchedule_ShownEditor(object sender, EventArgs e)
		{
			var view = sender as AdvBandedGridView;
			var edit = view.ActiveEditor as TextEdit;
			if (edit == null) return;
			edit.Properties.BeforeShowMenu += Properties_BeforeShowMenu;
		}

		private void Properties_BeforeShowMenu(object sender, BeforeShowMenuEventArgs e)
		{
			var items = GetContextMenuItems(advBandedGridViewSchedule, advBandedGridViewSchedule.FocusedColumn, advBandedGridViewSchedule.FocusedRowHandle);
			if (!items.Any()) return;
			e.Menu.Items.Clear();
			foreach (var menuItem in items)
				e.Menu.Items.Add(menuItem);
		}

		private void advBandedGridViewSchedule_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
		{
			if (!e.HitInfo.InRowCell) return;
			foreach (var menuItem in GetContextMenuItems(advBandedGridViewSchedule, e.HitInfo.Column, e.HitInfo.RowHandle))
				e.Menu.Items.Add(menuItem);
		}

		private void advBandedGridViewSchedule_RowCellClick(object sender, RowCellClickEventArgs e)
		{
			if (e.Column != bandedGridColumnLogoImage) return;
			if (advBandedGridViewSchedule.FocusedRowHandle == GridControl.InvalidRowHandle) return;
			var logo = ImageSource.FromString(advBandedGridViewSchedule.GetRowCellValue(advBandedGridViewSchedule.FocusedRowHandle, bandedGridColumnLogoSource) as String);
			using (var form = new FormImageGallery(MediaMetaData.Instance.ListManager.Images))
			{
				if (form.ShowDialog() != DialogResult.OK) return;
				if (form.SelectedImageSource == null) return;
				advBandedGridViewSchedule.SetRowCellValue(advBandedGridViewSchedule.FocusedRowHandle, bandedGridColumnLogoSource, form.SelectedImageSource.Serialize());
				advBandedGridViewSchedule.SetRowCellValue(advBandedGridViewSchedule.FocusedRowHandle, bandedGridColumnLogoImage, form.SelectedImageSource.SmallImage);
			}
		}

		private void gridControlSchedule_AfterDrop(object sender, DragEventArgs e)
		{
			var grid = sender as GridControl;
			var view = grid.MainView as GridView;
			var hitInfo = view.CalcHitInfo(grid.PointToClient(new Point(e.X, e.Y)));
			var downHitInfo = e.Data.GetData(typeof(BandedGridHitInfo)) as BandedGridHitInfo;
			if (downHitInfo == null) return;
			var sourceRow = downHitInfo.RowHandle;
			var targetRow = hitInfo.HitTest == GridHitTest.EmptyRow ? view.DataRowCount : hitInfo.RowHandle;
			ScheduleSection.ChangeProgramPosition(sourceRow, targetRow);
			UpdateGrid(false);
			UpdateSpotsStatus();
			UpdateTotalsValues();
			UpdateOutputStatus(ScheduleSection.Programs.Any());
			if (advBandedGridViewSchedule.RowCount > 0)
				advBandedGridViewSchedule.FocusedRowHandle = targetRow;
			var options = new Dictionary<string, object>();
			options.Add("Advertiser", ScheduleSection.Parent.BusinessName);
			options.Add(String.Format("{0}lyTotalSpots", SpotTitle), ScheduleSection.TotalSpots);
			options.Add(String.Format("{0}lyAverageRate", SpotTitle), ScheduleSection.AvgRate);
			options.Add(String.Format("{0}lyGrossInvestment", SpotTitle), ScheduleSection.TotalCost);
			AddActivity(new UserActivity("Change Program Position", options));
			SettingsNotSaved = true;
		}
		#endregion

		#region Output Staff

		public bool ShowDigitalLegendOnlyFirstSlide
		{
			get { return ScheduleSection.DigitalLegend.OutputOnlyOnce; }
		}

		public virtual string DigitalLegend
		{
			get { return String.Empty; }
		}

		public virtual Theme SelectedTheme
		{
			get { return null; }
		}

		private IEnumerable<OutputSchedule> PrepareOutput()
		{
			var outputPages = new List<OutputSchedule>();
			if (_localSchedule == null) return outputPages;
			var defaultProgram = ScheduleSection.Programs.FirstOrDefault();
			if (defaultProgram == null) return outputPages;
			var defaultSpotsNotEmpy = defaultProgram.SpotsNotEmpty;
			var programsPerSlide = 12;
			programsPerSlide = ScheduleSection.Programs.Count > programsPerSlide ? programsPerSlide : ScheduleSection.Programs.Count;
			var totalSpotsCount = 0;
			if (buttonXSpots.Checked)
				totalSpotsCount = ScheduleSection.ShowEmptySpots ? defaultProgram.Spots.Count : defaultSpotsNotEmpy.Length;
			var spotsIteratorLimit = totalSpotsCount;
			var spotsPerSlide = ScheduleSection.OutputMaxPeriods.HasValue ? ScheduleSection.OutputMaxPeriods.Value : 26;
			spotsPerSlide = totalSpotsCount == 0 || totalSpotsCount > spotsPerSlide ? spotsPerSlide : totalSpotsCount;
			var spotIntervals = new List<SpotInterval>();
			if (ScheduleSection.OutputPerQuater)
			{
				var spots = ScheduleSection.ShowEmptySpots ? defaultProgram.Spots.ToArray() : defaultSpotsNotEmpy;
				foreach (var quarter in _localSchedule.Quarters)
				{
					var spotInterval = new SpotInterval();
					spotInterval.Start = spotInterval.End = spotIntervals.Any() ? spotIntervals.Last().End : 0;
					spotInterval.End += spots.Count(s => s.Quarter == quarter);
					spotInterval.Name = String.Format("Q{0}", quarter.QuarterNumber);
					if (spotInterval.Start == spotInterval.End) continue;
					spotIntervals.Add(spotInterval);
				}
			}
			else
				spotIntervals.Add(new SpotInterval { Start = 0, End = spotsIteratorLimit });
			foreach (var spotInterval in spotIntervals)
			{
				for (var i = 0; i < ScheduleSection.Programs.Count; i += programsPerSlide)
				{
					for (var k = spotInterval.Start; k < (spotInterval.End == 0 ? 1 : spotInterval.End); k += spotsPerSlide)
					{
						var outputPage = new OutputSchedule(ScheduleSection);

						outputPage.Advertiser = _localSchedule.BusinessName;
						outputPage.DecisionMaker = _localSchedule.DecisionMaker;
						outputPage.Demo = _localSchedule.Demo + (!string.IsNullOrEmpty(_localSchedule.Source) ? (" (" + _localSchedule.Source + ")") : string.Empty);
						outputPage.DigitalInfo = !ScheduleSection.DigitalLegend.OutputOnlyOnce ||
												 ((i + programsPerSlide) >= ScheduleSection.Programs.Count &&
												  (k + spotsPerSlide) >= totalSpotsCount) ?
							DigitalLegend :
							String.Empty;
						outputPage.Color = MediaMetaData.Instance.SettingsManager.SelectedColor;
						outputPage.Quarter = spotInterval.Name;

						outputPage.ProgramsPerSlide = programsPerSlide;
						outputPage.SpotsPerSlide = totalSpotsCount > 0 ? spotsPerSlide : 0;
						outputPage.ShowRates = buttonXRate.Checked;
						outputPage.ShowRating = buttonXRating.Checked;
						outputPage.ShowCPP = buttonXCPP.Checked;
						outputPage.ShowGRP = buttonXGRP.Checked;
						outputPage.ShowCost = buttonXCost.Checked;
						outputPage.ShowStation = buttonXStation.Checked;
						outputPage.ShowStationInBrackets = !ScheduleSection.OutputNoBrackets;
						outputPage.ShowDay = buttonXDay.Checked;
						outputPage.ShowTime = buttonXTime.Checked;
						outputPage.ShowLength = buttonXLength.Checked;
						outputPage.ShowTotalSpots = buttonXTotalSpots.Checked;
						outputPage.ShowSpots = buttonXSpots.Checked;
						outputPage.ShowLogo = buttonXLogo.Checked;

						#region Set Totals

						if (buttonXTotalPeriods.Checked)
							outputPage.Totals.Add(laTotalPeriodsTitle.Text, laTotalPeriodsValue.Text);
						if (buttonXTotalSpots.Checked)
							outputPage.Totals.Add(laTotalSpotsTitle.Text, laTotalSpotsValue.Text);
						if (buttonXTotalGRP.Checked)
							outputPage.Totals.Add(laTotalGRPTitle.Text, laTotalGRPValue.Text);
						if (buttonXTotalCPP.Checked)
							outputPage.Totals.Add(laTotalCPPTitle.Text, laTotalCPPValue.Text);
						if (buttonXAvgRate.Checked)
							outputPage.Totals.Add(laAvgRateTitle.Text, laAvgRateValue.Text);
						if (buttonXTotalCost.Checked)
							outputPage.Totals.Add(laTotalCostTitle.Text, laTotalCostValue.Text);
						if (buttonXNetRate.Checked)
							outputPage.Totals.Add(laNetRateTitle.Text, laNetRateValue.Text);
						if (buttonXDiscount.Checked)
							outputPage.Totals.Add(laAgencyDiscountTitle.Text, laAgencyDiscountValue.Text);

						#endregion

						#region Set OutputProgram Values

						for (var j = 0; j < programsPerSlide; j++)
						{
							if ((i + j) < ScheduleSection.Programs.Count)
							{
								var program = ScheduleSection.Programs[i + j];
								var outputProgram = new OutputProgram(outputPage);
								outputProgram.Name = program.Name + (buttonXDaypart.Checked ? ("-" + program.Daypart) : string.Empty);
								outputProgram.LineID = program.Index.ToString("00");
								outputProgram.Station = program.Station;
								outputProgram.Days = program.Day;
								outputProgram.Time = program.Time;
								outputProgram.Length = program.Length;
								outputProgram.Rate = buttonXRate.Checked && program.Rate.HasValue ? program.Rate.Value.ToString(ScheduleSection.UseDecimalRates ? "$#,##0.00" : "$#,##0") : string.Empty;
								outputProgram.Rating = buttonXRating.Checked && program.Rating.HasValue ? program.Rating.Value.ToString(_localSchedule.DemoType == DemoType.Rtg ? "#,###.0" : "#,##0") : string.Empty;
								outputProgram.CPP = buttonXCPP.Checked ? program.CPP.ToString("$#,###.00") : String.Empty;
								outputProgram.GRP = buttonXGRP.Checked ? program.GRP.ToString(_localSchedule.DemoType == DemoType.Rtg ? "#,###.0" : "#,##0") : String.Empty;
								outputProgram.TotalRate = buttonXCost.Checked ? program.Cost.ToString(ScheduleSection.UseDecimalRates ? "$#,##0.00" : "$#,##0") : String.Empty;
								outputProgram.TotalSpots = program.TotalSpots.ToString("#,##0");
								outputProgram.Logo = program.Logo != null ? program.Logo.Clone() : null;

								#region Set Spots Values

								var spotsNotEmpy = program.SpotsNotEmpty;
								for (var l = 0; l < spotsPerSlide; l++)
								{
									if ((k + l) < spotInterval.End)
									{
										var value = !program.Parent.ShowEmptySpots ?
											(spotsNotEmpy[k + l].Count > 0 ? spotsNotEmpy[k + l].Count.ToString() : "-") :
											(program.Spots[k + l].Count > 0 ? program.Spots[k + l].Count.ToString() : "-");
										outputProgram.Spots.Add(value);
									}
									else
										break;
									Application.DoEvents();
								}

								#endregion

								outputPage.Programs.Add(outputProgram);
								Application.DoEvents();
							}
							else
								break;
						}

						#endregion

						#region Set Total Values
						for (var l = 0; l < spotsPerSlide; l++)
						{
							if ((k + l) < spotInterval.End)
							{
								var outputTotalSpot = new OutputTotalSpot();
								outputTotalSpot.Day = !defaultProgram.Parent.ShowEmptySpots ? (defaultSpotsNotEmpy[k + l].Date.Day.ToString()) : (defaultProgram.Spots[k + l].Date.Day.ToString());
								outputTotalSpot.Month = Spot.GetMonthAbbreviation(!defaultProgram.Parent.ShowEmptySpots ? defaultSpotsNotEmpy[k + l].Date.Month : defaultProgram.Spots[k + l].Date.Month);

								int sum;
								if (!defaultProgram.Parent.ShowEmptySpots)
									sum = defaultProgram.Parent.Programs.Select(x => x.SpotsNotEmpty.FirstOrDefault(y => y.Date.Equals(defaultSpotsNotEmpy[k + l].Date))).Where(z => z.Count.HasValue).Select(z => z.Count.Value).Sum();
								else
									sum = defaultProgram.Parent.Programs.Select(x => x.Spots.FirstOrDefault(y => y.Date.Equals(defaultProgram.Spots[k + l].Date))).Where(z => z.Count.HasValue).Select(z => z.Count.Value).Sum();
								outputTotalSpot.Value = sum > 0 ? sum.ToString() : "-";
								outputPage.TotalSpots.Add(outputTotalSpot);
							}
							else
								break;
							Application.DoEvents();
						}

						outputPage.TotalCost = ScheduleSection.TotalCost.ToString(ScheduleSection.UseDecimalRates ? "$#,##0.00" : "$#,##0");
						outputPage.TotalSpot = ScheduleSection.TotalSpots.ToString("#,##0");
						outputPage.TotalCPP = ScheduleSection.TotalCPP.ToString("$#,###.00");
						outputPage.TotalGRP = ScheduleSection.TotalGRP.ToString(_localSchedule.DemoType == DemoType.Rtg ? "#,###.0" : "#,##0");

						#endregion

						outputPage.GetLogos();
						outputPage.PopulateScheduleReplacementsList();

						outputPages.Add(outputPage);
					}
				}
			}
			return outputPages;
		}

		protected void TrackOutput()
		{
			var options = new Dictionary<string, object>();
			options.Add("Slide", String.Format("{0}ly Schedule", SpotTitle));
			options.Add("Advertiser", ScheduleSection.Parent.BusinessName);
			options.Add(String.Format("{0}lyTotalSpots", SpotTitle), ScheduleSection.TotalSpots);
			options.Add(String.Format("{0}lyAverageRate", SpotTitle), ScheduleSection.AvgRate);
			options.Add(String.Format("{0}lyGrossInvestment", SpotTitle), ScheduleSection.TotalCost);
			AddActivity(new UserActivity("Output", options));
		}

		protected void TrackPreview()
		{
			var options = new Dictionary<string, object>();
			options.Add("Slide", String.Format("{0}ly Schedule", SlideType));
			options.Add("Advertiser", ScheduleSection.Parent.BusinessName);
			options.Add(String.Format("{0}lyTotalSpots", SpotTitle), ScheduleSection.TotalSpots);
			options.Add(String.Format("{0}lyAverageRate", SpotTitle), ScheduleSection.AvgRate);
			options.Add(String.Format("{0}lyGrossInvestment", SpotTitle), ScheduleSection.TotalCost);
			AddActivity(new UserActivity("Preview", options));
		}

		protected virtual void PowerPointInternal(IEnumerable<OutputSchedule> outputPages) { throw new NotImplementedException(); }
		protected virtual void EmailInternal(IEnumerable<OutputSchedule> outputPages) { throw new NotImplementedException(); }
		protected virtual void PreviewInternal(IEnumerable<OutputSchedule> outputPages) { throw new NotImplementedException(); }
		protected virtual void PdfInternal(IEnumerable<OutputSchedule> outputPages) { throw new NotImplementedException(); }

		public void PowerPoint_Click(object sender, EventArgs e)
		{
			SaveSchedule();
			if (_localSchedule == null) return;
			if (!ScheduleSection.Programs.Any()) return;
			IEnumerable<OutputSchedule> outputPages = null;
			using (var formProgress = new FormProgress())
			{
				formProgress.laProgress.Text = "Chill-Out for a few seconds...\nPreparing slides so your presentation can look AWESOME!";
				formProgress.TopMost = true;
				formProgress.Show();
				var thread = new Thread(() => Invoke((MethodInvoker)delegate { outputPages = PrepareOutput(); }));
				thread.Start();
				while (thread.IsAlive)
					Application.DoEvents();
				formProgress.Close();
			}
			PowerPointInternal(outputPages);
		}

		public void Preview_Click(object sender, EventArgs e)
		{
			SaveSchedule();
			if (_localSchedule == null) return;
			if (!ScheduleSection.Programs.Any()) return;
			IEnumerable<OutputSchedule> outputPages = null;
			using (var formProgress = new FormProgress())
			{
				formProgress.laProgress.Text = "Chill-Out for a few seconds...\nPreparing Preview...";
				formProgress.TopMost = true;
				formProgress.Show();
				var thread = new Thread(() => Invoke((MethodInvoker)delegate { outputPages = PrepareOutput(); }));
				thread.Start();
				while (thread.IsAlive)
					Application.DoEvents();
				formProgress.Close();
			}
			PreviewInternal(outputPages);
		}

		public void Email_Click(object sender, EventArgs e)
		{
			SaveSchedule();
			if (_localSchedule == null) return;
			if (!ScheduleSection.Programs.Any()) return;
			IEnumerable<OutputSchedule> outputPages = null;
			using (var formProgress = new FormProgress())
			{
				formProgress.laProgress.Text = "Chill-Out for a few seconds...\nPreparing Email...";
				formProgress.TopMost = true;
				formProgress.Show();
				var thread = new Thread(() => Invoke((MethodInvoker)delegate { outputPages = PrepareOutput(); }));
				thread.Start();
				while (thread.IsAlive)
					Application.DoEvents();
				formProgress.Close();
			}
			EmailInternal(outputPages);
		}

		public void Pdf_Click(object sender, EventArgs e)
		{
			SaveSchedule();
			if (_localSchedule == null) return;
			if (!ScheduleSection.Programs.Any()) return;
			IEnumerable<OutputSchedule> outputPages = null;
			using (var formProgress = new FormProgress())
			{
				formProgress.laProgress.Text = "Chill-Out for a few seconds...\nPreparing slides so your presentation can look AWESOME!";
				formProgress.TopMost = true;
				formProgress.Show();
				var thread = new Thread(() => Invoke((MethodInvoker)delegate { outputPages = PrepareOutput(); }));
				thread.Start();
				while (thread.IsAlive)
					Application.DoEvents();
				formProgress.Close();
			}
			PdfInternal(outputPages);
		}
		#endregion
	}
}