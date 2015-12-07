using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Asa.CommonGUI.Preview;
using Asa.MediaSchedule.Controls.InteropClasses;
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
using Asa.CommonGUI.Common;
using Asa.CommonGUI.ImageGallery;
using Asa.Core.Common;
using Asa.Core.MediaSchedule;
using Asa.MediaSchedule.Controls.BusinessClasses;
using DevExpress.XtraTab;

namespace Asa.MediaSchedule.Controls.PresentationClasses.ScheduleControls
{
	[ToolboxItem(false)]
	//public partial class SectionControl : UserControl
	public partial class SectionControl : XtraTabPage
	{
		private readonly List<BandedGridColumn> _spotColumns = new List<BandedGridColumn>();
		private readonly Font _spotColumnFont = new Font("Arial", 14);
		protected GridDragDropHelper _dragDropHelper;

		private Quarter _selectedQuarter;

		public ScheduleSection SectionData { get; private set; }

		#region Totals Calculation
		public string TotalPeriodsTag
		{
			get { return "Total Weeks"; }
		}
		public string TotalPeriodsValueFormatted
		{
			get { return SectionData.TotalActivePeriods.ToString("#,##0"); }
		}
		public string TotalSpotsTag
		{
			get { return "Total Spots"; }
		}
		public string TotalSpotsValueFormatted
		{
			get { return SectionData.TotalSpots.ToString("#,##0"); }
		}
		public string TotalGRPTag
		{
			get { return "Total GRPs"; }
		}
		public string TotalGRPValueFormatted
		{
			get { return SectionData.TotalGRP.ToString(SectionData.ParentSchedule.DemoType == DemoType.Rtg ? "#,###.0" : "#,##0"); }
		}
		public string TotalCPPTag
		{
			get { return "Overall CPP"; }
		}
		public string TotalCPPValueFormatted
		{
			get { return SectionData.TotalCPP.ToString("$#,###.00"); }
		}
		public string AvgRateTag
		{
			get { return "Average Rate"; }
		}
		public string AvgRateValueFormatted
		{
			get { return SectionData.AvgRate.ToString("$#,###.00"); }
		}
		public string TotalCostTag
		{
			get { return "Gross Investment"; }
		}
		public string TotalCostValuesFormatted
		{
			get { return SectionData.TotalCost.ToString(SectionData.UseDecimalRates ? "$#,##0.00" : "$#,##0"); }
		}
		public string NetRateTag
		{
			get { return "Net Investment"; }
		}
		public string NetRateValueFormatted
		{
			get { return SectionData.NetRate.ToString("$#,###.00"); }
		}
		public string TotalDiscountTag
		{
			get { return "Agency Discount"; }
		}
		public string TotalDiscountValueFormatted
		{
			get { return SectionData.Discount.ToString("$#,###.00"); }
		}
		#endregion

		public event EventHandler<EventArgs> DataChanged;

		public SectionControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			repositoryItemComboBoxDays.Enter += TextEditorsHelper.Editor_Enter;
			repositoryItemComboBoxDays.MouseDown += TextEditorsHelper.Editor_MouseDown;
			repositoryItemComboBoxDays.MouseUp += TextEditorsHelper.Editor_MouseUp;
			repositoryItemComboBoxDayparts.Enter += TextEditorsHelper.Editor_Enter;
			repositoryItemComboBoxDayparts.MouseDown += TextEditorsHelper.Editor_MouseDown;
			repositoryItemComboBoxDayparts.MouseUp += TextEditorsHelper.Editor_MouseUp;
			repositoryItemComboBoxLengths.Enter += TextEditorsHelper.Editor_Enter;
			repositoryItemComboBoxLengths.MouseDown += TextEditorsHelper.Editor_MouseDown;
			repositoryItemComboBoxLengths.MouseUp += TextEditorsHelper.Editor_MouseUp;
			repositoryItemPopupContainerEditProgram.Enter += TextEditorsHelper.Editor_Enter;
			repositoryItemPopupContainerEditProgram.MouseDown += TextEditorsHelper.Editor_MouseDown;
			repositoryItemPopupContainerEditProgram.MouseUp += TextEditorsHelper.Editor_MouseUp;
			repositoryItemTextEditProgram.Enter += TextEditorsHelper.Editor_Enter;
			repositoryItemTextEditProgram.MouseDown += TextEditorsHelper.Editor_MouseDown;
			repositoryItemTextEditProgram.MouseUp += TextEditorsHelper.Editor_MouseUp;
			repositoryItemComboBoxStations.Enter += TextEditorsHelper.Editor_Enter;
			repositoryItemComboBoxStations.MouseDown += TextEditorsHelper.Editor_MouseDown;
			repositoryItemComboBoxStations.MouseUp += TextEditorsHelper.Editor_MouseUp;
			repositoryItemComboBoxTimes.Enter += TextEditorsHelper.Editor_Enter;
			repositoryItemComboBoxTimes.MouseDown += TextEditorsHelper.Editor_MouseDown;
			repositoryItemComboBoxTimes.MouseUp += TextEditorsHelper.Editor_MouseUp;
			repositoryItemSpinEdit000s.Enter += TextEditorsHelper.Editor_Enter;
			repositoryItemSpinEdit000s.MouseDown += TextEditorsHelper.Editor_MouseDown;
			repositoryItemSpinEdit000s.MouseUp += TextEditorsHelper.Editor_MouseUp;
			repositoryItemSpinEditRate.Enter += TextEditorsHelper.Editor_Enter;
			repositoryItemSpinEditRate.MouseDown += TextEditorsHelper.Editor_MouseDown;
			repositoryItemSpinEditRate.MouseUp += TextEditorsHelper.Editor_MouseUp;
			repositoryItemSpinEditRating.Enter += TextEditorsHelper.Editor_Enter;
			repositoryItemSpinEditRating.MouseDown += TextEditorsHelper.Editor_MouseDown;
			repositoryItemSpinEditRating.MouseUp += TextEditorsHelper.Editor_MouseUp;
			repositoryItemSpinEditSpot.Enter += TextEditorsHelper.Editor_Enter;
			repositoryItemSpinEditSpot.MouseDown += TextEditorsHelper.Editor_MouseDown;
			repositoryItemSpinEditSpot.MouseUp += TextEditorsHelper.Editor_MouseUp;
		}
		protected string SpotTitle
		{
			get { return SectionData.ParentSchedule.SelectedSpotType.ToString(); }
		}

		#region Methods
		public void LoadData(ScheduleSection sectionData)
		{
			SectionData = sectionData;
			Text = SectionData.Name.Replace("&", "&&");
			SectionData.DataChanged += OnScheduleSectionDataChanged;
			bandedGridColumnRating.Caption = String.Format("{0}{1}",
				(!String.IsNullOrEmpty(SectionData.ParentSchedule.Demo) ? String.Format("{0} ", SectionData.ParentSchedule.Demo) : String.Empty),
				SectionData.ParentSchedule.DemoType == DemoType.Rtg ? "Rtg" : "(000)");
			bandedGridColumnRating.ColumnEdit = SectionData.ParentSchedule.DemoType == DemoType.Rtg ? repositoryItemSpinEditRating : repositoryItemSpinEdit000s;
			bandedGridColumnCPP.Caption = SectionData.ParentSchedule.DemoType == DemoType.Rtg ? "CPP" : "CPM";
			bandedGridColumnGRP.Caption = SectionData.ParentSchedule.DemoType == DemoType.Rtg ? "GRPs" : "Impr";
			bandedGridColumnGRP.ColumnEdit = SectionData.ParentSchedule.DemoType == DemoType.Rtg ? repositoryItemSpinEditRating : repositoryItemSpinEdit000s;
			bandedGridColumnGRP.SummaryItem.DisplayFormat = SectionData.ParentSchedule.DemoType == DemoType.Rtg ? "{0:n1}" : "{0:n0}";

			repositoryItemComboBoxStations.Items.Clear();
			repositoryItemComboBoxStations.Items.AddRange(SectionData.ParentSchedule.Stations.Where(x => x.Available).OfType<object>().ToArray());
			repositoryItemComboBoxDayparts.Items.Clear();
			repositoryItemComboBoxDayparts.Items.AddRange(SectionData.ParentSchedule.Dayparts.Where(x => x.Available).OfType<object>().ToArray());
			repositoryItemComboBoxLengths.Items.Clear();
			repositoryItemComboBoxLengths.Items.AddRange(MediaMetaData.Instance.ListManager.Lengths);
			repositoryItemComboBoxDays.Items.Clear();
			repositoryItemComboBoxDays.Items.AddRange(MediaMetaData.Instance.ListManager.Days);
			repositoryItemComboBoxTimes.Items.Clear();
			repositoryItemComboBoxTimes.Items.AddRange(MediaMetaData.Instance.ListManager.Times);

			UpdateGridData(true);
			UpdateGridView();
		}

		public void SaveData()
		{
			advBandedGridViewSchedule.CloseEditor();
		}

		public void ApplyDataFromTemplate(SectionControl templateControl)
		{
			SectionData.ApplyFromTemplate(templateControl.SectionData);
		}

		public void AddProgram()
		{
			SectionData.AddProgram();
			UpdateGridData(true);
			UpdateSpotsByQuarter();
			if (advBandedGridViewSchedule.RowCount > 0)
				advBandedGridViewSchedule.FocusedRowHandle = advBandedGridViewSchedule.RowCount - 1;

			var options = new Dictionary<string, object>();
			options.Add("Advertiser", SectionData.ParentSchedule.BusinessName);
			options.Add(String.Format("{0}lyTotalSpots", SpotTitle), SectionData.Parent.TotalSpots);
			options.Add(String.Format("{0}lyAverageRate", SpotTitle), SectionData.Parent.AvgRate);
			options.Add(String.Format("{0}lyGrossInvestment", SpotTitle), SectionData.Parent.TotalCost);
			AddActivity(new UserActivity("New Program Added", options));

			OnScheduleSectionDataChanged(this, EventArgs.Empty);
		}

		private void CloneProgram(int sourceIndex, bool fullClone)
		{
			SectionData.CloneProgram(sourceIndex, fullClone);
			UpdateGridData(true);
			if (advBandedGridViewSchedule.RowCount > 0)
				advBandedGridViewSchedule.FocusedRowHandle = advBandedGridViewSchedule.RowCount - 1;

			var options = new Dictionary<string, object>();
			options.Add("Advertiser", SectionData.ParentSchedule.BusinessName);
			options.Add(String.Format("{0}lyTotalSpots", SpotTitle), SectionData.Parent.TotalSpots);
			options.Add(String.Format("{0}lyAverageRate", SpotTitle), SectionData.Parent.AvgRate);
			options.Add(String.Format("{0}lyGrossInvestment", SpotTitle), SectionData.Parent.TotalCost);
			AddActivity(new UserActivity("New Program Added", options));

			OnScheduleSectionDataChanged(this, EventArgs.Empty);
		}

		public void DeleteProgram()
		{
			var selectedProgramRow = advBandedGridViewSchedule.GetFocusedDataRow();
			if (selectedProgramRow == null) return;
			var selectedProgramIndex = selectedProgramRow[0].ToString();
			if (Utilities.Instance.ShowWarningQuestion(String.Format("Delete Line ID {0}?", selectedProgramIndex)) != DialogResult.Yes)
				return;
			SectionData.DeleteProgram(advBandedGridViewSchedule.GetDataSourceRowIndex(advBandedGridViewSchedule.FocusedRowHandle));
			UpdateGridData(true);

			var options = new Dictionary<string, object>();
			options.Add("Advertiser", SectionData.ParentSchedule.BusinessName);
			options.Add(String.Format("{0}lyTotalSpots", SpotTitle), SectionData.Parent.TotalSpots);
			options.Add(String.Format("{0}lyAverageRate", SpotTitle), SectionData.Parent.AvgRate);
			options.Add(String.Format("{0}lyGrossInvestment", SpotTitle), SectionData.Parent.TotalCost);
			AddActivity(new UserActivity("Program Deleted", options));

			OnScheduleSectionDataChanged(this, EventArgs.Empty);
		}

		public void UpdateGridView()
		{
			gridBandRate.Visible = SectionData.ShowRate | SectionData.ShowRating;
			bandedGridColumnRate.Visible = SectionData.ShowRate;
			bandedGridColumnRating.Visible = SectionData.ShowRating;
			if (SectionData.ShowRate)
				advBandedGridViewSchedule.SetColumnPosition(bandedGridColumnRate, 0, 0);
			if (SectionData.ShowRating)
				advBandedGridViewSchedule.SetColumnPosition(bandedGridColumnRating, 1, 0);
			
			bandedGridColumnCPP.Visible = SectionData.ShowCPP;
			bandedGridColumnGRP.Visible = SectionData.ShowGRP;
			bandedGridColumnCost.Visible = SectionData.ShowCost;

			gridBandProgram.Visible = SectionData.ShowProgram || SectionData.ShowLenght || SectionData.ShowDay || SectionData.ShowTime;
			bandedGridColumnName.Visible = SectionData.ShowProgram;
			bandedGridColumnLength.Visible = SectionData.ShowLenght;
			bandedGridColumnDay.Visible = SectionData.ShowDay;
			bandedGridColumnTime.Visible = SectionData.ShowTime;
			if (SectionData.ShowProgram)
				advBandedGridViewSchedule.SetColumnPosition(bandedGridColumnName, 0, 0);
			var secondRowIndex = SectionData.ShowProgram ? 1 : 0;
			var columnIndex = 0;
			if (SectionData.ShowDay)
			{
				advBandedGridViewSchedule.SetColumnPosition(bandedGridColumnDay, secondRowIndex, columnIndex);
				columnIndex++;
			}
			if (SectionData.ShowTime)
			{
				advBandedGridViewSchedule.SetColumnPosition(bandedGridColumnTime, secondRowIndex, columnIndex);
				columnIndex++;
			}
			if (SectionData.ShowLenght)
			{
				advBandedGridViewSchedule.SetColumnPosition(bandedGridColumnLength, secondRowIndex, columnIndex);
				columnIndex++;
			}

			gridBandLogo.Visible = SectionData.ShowLogo;

			bandedGridColumnStation.Visible = SectionData.ShowStation;
			bandedGridColumnDaypart.Visible = SectionData.ShowDaypart;

			UpdateColumnsAccordingScreenSize();
			UpdateSpotsByQuarter();
			UpdateRateFormat();
		}

		private void UpdateColumnsAccordingScreenSize()
		{
			gridBandStation.Visible = SectionData.ShowStation || SectionData.ShowDaypart;
			if (SectionData.ShowStation)
				advBandedGridViewSchedule.SetColumnPosition(bandedGridColumnStation, 0, 0);
			if (SectionData.ShowDaypart)
				advBandedGridViewSchedule.SetColumnPosition(bandedGridColumnDaypart, 1, 0);
			gridBandTotals.Visible = SectionData.ShowTotalSpots || SectionData.ShowCost || SectionData.ShowGRP || SectionData.ShowCPP;
			if (SectionData.ShowTotalSpots)
				advBandedGridViewSchedule.SetColumnPosition(bandedGridColumnTotalSpots, 0, 0);
			if (SectionData.ShowGRP)
				advBandedGridViewSchedule.SetColumnPosition(bandedGridColumnGRP, 1, 0);
			var secondColumnIndex = SectionData.ShowTotalSpots || SectionData.ShowGRP ? 1 : 0;
			if (SectionData.ShowCost)
				advBandedGridViewSchedule.SetColumnPosition(bandedGridColumnCost, 0, secondColumnIndex);
			if (SectionData.ShowCPP)
				advBandedGridViewSchedule.SetColumnPosition(bandedGridColumnCPP, 1, secondColumnIndex);
		}

		public void UpdateSpotsByQuarter(Quarter selectedQuarter)
		{
			if (_selectedQuarter == selectedQuarter)
				return;
			_selectedQuarter = selectedQuarter;
			UpdateSpotsByQuarter();
		}

		private void UpdateSpotsByQuarter()
		{
			if (SectionData.ShowSpots)
			{
				gridBandSpots.Visible = true;
				foreach (var column in _spotColumns)
				{
					var quarter = ((object[])column.Tag)[0] as Quarter;
					column.Visible =
						(quarter != null &&
							_selectedQuarter != null &&
							quarter.ToString() == _selectedQuarter.ToString()) ||
						_selectedQuarter == null;
				}
			}
			else
				gridBandSpots.Visible = false;
		}

		private void UpdateGridData(bool rebuildColumns)
		{
			int focussedRow = advBandedGridViewSchedule.FocusedRowHandle;
			advBandedGridViewSchedule.BeginUpdate();

			gridControlSchedule.DataSource = null;
			gridControlSchedule.DataMember = String.Empty;

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

			SectionData.GenerateDataSource();

			if (rebuildColumns)
				BuildSpotColumns();
			if (SectionData.Programs.Any())
			{
				pbNoPrograms.Visible = false;
				gridControlSchedule.Visible = true;
				gridControlSchedule.BringToFront();
				gridControlSchedule.DataSource = SectionData.DataSource;
			}
			else
			{
				gridControlSchedule.Visible = false;
				pbNoPrograms.Visible = true;
				pbNoPrograms.BringToFront();
			}
			advBandedGridViewSchedule.EndUpdate();
			if (_dragDropHelper == null && SectionData.Programs.Any())
			{
				_dragDropHelper = new GridDragDropHelper(advBandedGridViewSchedule, true, 40);
				_dragDropHelper.AfterDrop += gridControlSchedule_AfterDrop;
			}
			if (focussedRow >= 0 && focussedRow < advBandedGridViewSchedule.RowCount)
				advBandedGridViewSchedule.FocusedRowHandle = focussedRow;
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
			foreach (DataColumn column in SectionData.DataSource.Columns)
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
			gridBandSpots.Visible = _spotColumns.Count > 0 && SectionData.ShowSpots;
		}

		private void UpdateRateFormat()
		{
			if (SectionData.UseDecimalRates)
			{
				bandedGridColumnCost.DisplayFormat.FormatString = "$#,##0.00";
				bandedGridColumnCost.SummaryItem.DisplayFormat = @"{0:c2}";
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
				items.Add(new DXMenuItem("Delete this Line", OnDeleteProgram));
			}
			return items;
		}

		private void AddActivity(UserActivity activity)
		{
			BusinessObjects.Instance.ActivityManager.AddActivity(activity);
		}

		private void OnScheduleSectionDataChanged(object sender, EventArgs e)
		{
			if (DataChanged != null)
				DataChanged(this, EventArgs.Empty);
		}

		private void OnDeleteProgram(object sender, EventArgs e)
		{
			DeleteProgram();
		}
		#endregion

		#region Grid Events

		private void advBandedGridViewSchedule_CellValueChanged(object sender, CellValueChangedEventArgs e)
		{
			advBandedGridViewSchedule.CloseEditor();
			advBandedGridViewSchedule.UpdateCurrentRow();

			var options = new Dictionary<string, object>();
			options.Add("Advertiser", SectionData.ParentSchedule.BusinessName);
			options.Add("Program", advBandedGridViewSchedule.GetRowCellValue(e.RowHandle, bandedGridColumnName));
			options.Add(String.Format("{0}lyTotalSpots", SpotTitle), SectionData.Parent.TotalSpots);
			options.Add(String.Format("{0}lyAverageRate", SpotTitle), SectionData.Parent.AvgRate);
			options.Add(String.Format("{0}lyGrossInvestment", SpotTitle), SectionData.Parent.TotalCost);
			AddActivity(new UserActivity("Program Line Updated", options));
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
				if (SectionData.ShowRate)
					column = bandedGridColumnRate;
				else if (SectionData.ShowRating)
					column = bandedGridColumnRating;
				else if (SectionData.ShowLenght)
					column = bandedGridColumnLength;
				else if (SectionData.ShowDay)
					column = bandedGridColumnDay;
				else if (SectionData.ShowTime)
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
			if (!String.IsNullOrEmpty(station) && (!String.IsNullOrEmpty(daypart) || !SectionData.ShowDaypart))
				dataSource.AddRange(MediaMetaData.Instance.ListManager.SourcePrograms.Where(x => (x.Station.Equals(station) || String.IsNullOrEmpty(station)) && (!SectionData.ShowDaypart || (x.Daypart.Equals(daypart) || String.IsNullOrEmpty(daypart)))));
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
					if (SectionData.ParentSchedule.ImportDemo && SectionData.ParentSchedule.UseDemo)
					{
						var demo = program.Demos.FirstOrDefault(d => d.Name.Equals(SectionData.ParentSchedule.Demo) && d.Source.Equals(SectionData.ParentSchedule.Source) && d.DemoType == SectionData.ParentSchedule.DemoType);
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
			SectionData.ChangeProgramPosition(sourceRow, targetRow);
			UpdateGridData(false);
			UpdateSpotsByQuarter();
			advBandedGridViewSchedule.FocusedRowHandle = targetRow;

			var options = new Dictionary<string, object>();
			options.Add("Advertiser", SectionData.ParentSchedule.BusinessName);
			options.Add(String.Format("{0}lyTotalSpots", SpotTitle), SectionData.Parent.TotalSpots);
			options.Add(String.Format("{0}lyAverageRate", SpotTitle), SectionData.Parent.AvgRate);
			options.Add(String.Format("{0}lyGrossInvestment", SpotTitle), SectionData.Parent.TotalCost);
			AddActivity(new UserActivity("Change Program Position", options));
		}
		#endregion

		#region Output
		public bool ReadyForOutput
		{
			get { return SectionData.Programs.Any(); }
		}

		private string DigitalLegend
		{
			get { return String.Empty; }
		}

		private SlideType SlideType
		{
			get
			{
				return MediaMetaData.Instance.DataType == MediaDataType.TVSchedule ? SlideType.TVProgramSchedule : SlideType.RadioProgramSchedule;
			}
		}

		private Theme SelectedTheme
		{
			get
			{
				return BusinessObjects.Instance.ThemeManager
					.GetThemes(SlideType)
					.FirstOrDefault(t =>
						t.Name.Equals(MediaMetaData.Instance.SettingsManager.GetSelectedTheme(SlideType)) ||
						String.IsNullOrEmpty(MediaMetaData.Instance.SettingsManager.GetSelectedTheme(SlideType)));
			}
		}

		public IEnumerable<OutputSchedule> PrepareOutput()
		{
			var outputPages = new List<OutputSchedule>();
			var defaultProgram = SectionData.Programs.FirstOrDefault();
			if (defaultProgram == null) return outputPages;
			var defaultSpotsNotEmpy = defaultProgram.SpotsNotEmpty;
			var programsPerSlide = 12;
			programsPerSlide = SectionData.Programs.Count > programsPerSlide ? programsPerSlide : SectionData.Programs.Count;
			var totalSpotsCount = 0;
			if (SectionData.ShowSpots)
				totalSpotsCount = SectionData.ShowEmptySpots ? defaultProgram.Spots.Count : defaultSpotsNotEmpy.Length;
			var spotsIteratorLimit = totalSpotsCount;
			var spotsPerSlide = SectionData.OutputMaxPeriods.HasValue ? SectionData.OutputMaxPeriods.Value : 26;
			spotsPerSlide = totalSpotsCount == 0 || totalSpotsCount > spotsPerSlide ? spotsPerSlide : totalSpotsCount;
			var spotIntervals = new List<SpotInterval>();
			if (SectionData.OutputPerQuater)
			{
				var spots = SectionData.ShowEmptySpots ? defaultProgram.Spots.ToArray() : defaultSpotsNotEmpy;
				foreach (var quarter in SectionData.ParentSchedule.Quarters)
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
				for (var i = 0; i < SectionData.Programs.Count; i += programsPerSlide)
				{
					for (var k = spotInterval.Start; k < (spotInterval.End == 0 ? 1 : spotInterval.End); k += spotsPerSlide)
					{
						var outputPage = new OutputSchedule(SectionData);

						outputPage.Advertiser = SectionData.ParentSchedule.BusinessName;
						outputPage.DecisionMaker = SectionData.ParentSchedule.DecisionMaker;
						outputPage.Demo = String.Format("{0}{1}",
								SectionData.ParentSchedule.Demo,
								!String.IsNullOrEmpty(SectionData.ParentSchedule.Source) ? (" (" + SectionData.ParentSchedule.Source + ")") : String.Empty);
						outputPage.DigitalInfo = !SectionData.Parent.DigitalLegend.OutputOnlyOnce ||
												 ((i + programsPerSlide) >= SectionData.Programs.Count &&
												  (k + spotsPerSlide) >= totalSpotsCount) ?
							DigitalLegend :
							String.Empty;
						outputPage.Color = MediaMetaData.Instance.SettingsManager.SelectedColor;
						outputPage.Quarter = spotInterval.Name;

						outputPage.ProgramsPerSlide = programsPerSlide;
						outputPage.SpotsPerSlide = totalSpotsCount > 0 ? spotsPerSlide : 0;
						outputPage.ShowRates = SectionData.ShowRate;
						outputPage.ShowRating = SectionData.ShowRating;
						outputPage.ShowCPP = SectionData.ShowCPP;
						outputPage.ShowGRP = SectionData.ShowGRP;
						outputPage.ShowCost = SectionData.ShowCost;
						outputPage.ShowStation = SectionData.ShowStation;
						outputPage.ShowProgram = SectionData.ShowProgram;
						outputPage.ShowStationInBrackets = !SectionData.OutputNoBrackets;
						outputPage.ShowDay = SectionData.ShowDay;
						outputPage.ShowTime = SectionData.ShowTime;
						outputPage.ShowLength = SectionData.ShowLenght;
						outputPage.ShowTotalSpots = SectionData.ShowTotalSpots;
						outputPage.ShowSpots = SectionData.ShowSpots;
						outputPage.ShowLogo = SectionData.ShowLogo;

						#region Set Totals

						if (SectionData.ShowTotalPeriods)
							outputPage.Totals.Add(TotalPeriodsTag, TotalPeriodsValueFormatted);
						if (SectionData.ShowTotalSpots)
							outputPage.Totals.Add(TotalSpotsTag, TotalSpotsValueFormatted);
						if (SectionData.ShowTotalGRP)
							outputPage.Totals.Add(TotalGRPTag, TotalGRPValueFormatted);
						if (SectionData.ShowTotalCPP)
							outputPage.Totals.Add(TotalCPPTag, TotalCPPValueFormatted);
						if (SectionData.ShowAverageRate)
							outputPage.Totals.Add(AvgRateTag, AvgRateValueFormatted);
						if (SectionData.ShowTotalRate)
							outputPage.Totals.Add(TotalCostTag, TotalCostValuesFormatted);
						if (SectionData.ShowNetRate)
							outputPage.Totals.Add(NetRateTag, NetRateValueFormatted);
						if (SectionData.ShowDiscount)
							outputPage.Totals.Add(TotalDiscountTag, TotalDiscountValueFormatted);

						#endregion

						#region Set OutputProgram Values

						for (var j = 0; j < programsPerSlide; j++)
						{
							if ((i + j) < SectionData.Programs.Count)
							{
								var program = SectionData.Programs[i + j];
								var outputProgram = new OutputProgram(outputPage);
								outputProgram.Name = program.Name + (SectionData.ShowDaypart ? ("-" + program.Daypart) : string.Empty);
								outputProgram.LineID = program.Index.ToString("00");
								outputProgram.Station = program.Station;
								outputProgram.Days = program.Day;
								outputProgram.Time = program.Time;
								outputProgram.Length = program.Length;
								outputProgram.Rate = SectionData.ShowRate && program.Rate.HasValue ? program.Rate.Value.ToString(SectionData.UseDecimalRates ? "$#,##0.00" : "$#,##0") : string.Empty;
								outputProgram.Rating = SectionData.ShowRating && program.Rating.HasValue ? program.Rating.Value.ToString(SectionData.ParentSchedule.DemoType == DemoType.Rtg ? "#,###.0" : "#,##0") : string.Empty;
								outputProgram.CPP = SectionData.ShowCPP ? program.CPP.ToString("$#,###.00") : String.Empty;
								outputProgram.GRP = SectionData.ShowGRP ? program.GRP.ToString(SectionData.ParentSchedule.DemoType == DemoType.Rtg ? "#,###.0" : "#,##0") : String.Empty;
								outputProgram.TotalRate = SectionData.ShowCost ? program.Cost.ToString(SectionData.UseDecimalRates ? "$#,##0.00" : "$#,##0") : String.Empty;
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

						outputPage.TotalCost = SectionData.TotalCost.ToString(SectionData.UseDecimalRates ? "$#,##0.00" : "$#,##0");
						outputPage.TotalSpot = SectionData.TotalSpots.ToString("#,##0");
						outputPage.TotalCPP = SectionData.TotalCPP.ToString("$#,###.00");
						outputPage.TotalGRP = SectionData.TotalGRP.ToString(SectionData.ParentSchedule.DemoType == DemoType.Rtg ? "#,###.0" : "#,##0");

						#endregion

						outputPage.GetLogos();
						outputPage.PopulateScheduleReplacementsList();

						outputPages.Add(outputPage);
					}
				}
			}
			return outputPages;
		}

		public void GenerateOutput()
		{
			var outputPages = PrepareOutput();
			RegularMediaSchedulePowerPointHelper.Instance.AppendOneSheet(outputPages, SelectedTheme, MediaMetaData.Instance.SettingsManager.UseSlideMaster);
		}

		public PreviewGroup GeneratePreview()
		{
			var outputPages = PrepareOutput();
			var previewGroup = new PreviewGroup
			{
				Name = SectionData.Name.Replace("&", "&&"),
				PresentationSourcePath = Path.Combine(Core.Common.ResourceManager.Instance.TempFolder.LocalPath, Path.GetFileName(Path.GetTempFileName()))
			};
			RegularMediaSchedulePowerPointHelper.Instance.PrepareOneSheetEmail(previewGroup.PresentationSourcePath, outputPages, SelectedTheme, MediaMetaData.Instance.SettingsManager.UseSlideMaster);
			return previewGroup;
		}
		#endregion
	}
}