using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Media.Configuration;
using Asa.Business.Media.Entities.NonPersistent.Section.Content;
using Asa.Business.Media.Entities.NonPersistent.Snapshot;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.Images;
using Asa.Common.Core.Objects.Output;
using Asa.Common.Core.Objects.Themes;
using Asa.Common.GUI.Common;
using Asa.Common.GUI.ImageGallery;
using Asa.Common.GUI.Preview;
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
using DevExpress.XtraTab;
using Asa.Media.Controls.BusinessClasses.Managers;
using Asa.Media.Controls.InteropClasses;
using DevExpress.Utils;

namespace Asa.Media.Controls.PresentationClasses.SnapshotControls
{
	[ToolboxItem(false)]
	//public partial class SnapshotControl : UserControl, ISnapshotSlide
	public sealed partial class SnapshotControl : XtraTabPage, ISnapshotSlideControl
	{
		private GridDragDropHelper _dragDropHelper;
		public Snapshot Data { get; private set; }

		public event EventHandler<EventArgs> DataChanged;

		public SnapshotControl(Snapshot data)
		{
			InitializeComponent();
			pnNoPrograms.Dock = DockStyle.Fill;
			gridControl.Dock = DockStyle.Fill;
			LoadData(data);
			if ((CreateGraphics()).DpiX > 96)
			{
				laProgramSourceInfo.Font = new Font(laProgramSourceInfo.Font.FontFamily, laProgramSourceInfo.Font.Size - 2, laProgramSourceInfo.Font.Style);
			}
		}

		#region Methods
		public void LoadData(Snapshot data)
		{
			Data = data;
			Text = Data.Name;
			repositoryItemComboBoxStations.Items.Clear();
			repositoryItemComboBoxStations.Items.AddRange(
				Data.Parent.ScheduleSettings.Stations
					.Where(station => station.Available)
					.Select(station => station.Name)
					.ToArray());
			repositoryItemComboBoxDayparts.Items.Clear();
			repositoryItemComboBoxDayparts.Items.AddRange(
				Data.Parent.ScheduleSettings.Dayparts
					.Where(daypart => daypart.Available)
					.Select(daypart => daypart.Code).ToArray());
			repositoryItemComboBoxLengths.Items.Clear();
			repositoryItemComboBoxLengths.Items.AddRange(MediaMetaData.Instance.ListManager.Lengths);
			repositoryItemComboBoxTimes.Items.Clear();
			repositoryItemComboBoxTimes.Items.AddRange(MediaMetaData.Instance.ListManager.Times);
			gridControl.DataSource = Data.Programs;
			advBandedGridView.RefreshData();
			UpdateView();
			UpdateProgramSplash();
			InitDargDropHelper();
		}

		public void SaveData()
		{
			advBandedGridView.CloseEditor();
		}

		public void Release()
		{
			if (_dragDropHelper != null)
				_dragDropHelper.AfterDrop -= OnGridControlAfterDrop;
			gridControlProgramSource.DataSource = null;
			gridControl.DataSource = null;
			DataChanged = null;
			Data = null;
		}

		public void AddProgram()
		{
			Data.AddProgram();
			gridControl.DataSource = Data.Programs;
			advBandedGridView.RefreshData();
			UpdateProgramSplash();
			InitDargDropHelper();
			if (DataChanged != null)
				DataChanged(this, EventArgs.Empty);
		}

		public void DeleteProgram()
		{
			var selectedProgram = advBandedGridView.GetFocusedRow() as SnapshotProgram;
			if (selectedProgram == null) return;
			var selectedProgramIndex = selectedProgram.Index;
			if (PopupMessageHelper.Instance.ShowWarningQuestion(
				String.Format("Delete Line ID {0}?", selectedProgramIndex)) != DialogResult.Yes) return;
			Data.DeleteProgram(advBandedGridView.GetDataSourceRowIndex(advBandedGridView.FocusedRowHandle));
			gridControl.DataSource = Data.Programs;
			advBandedGridView.RefreshData();
			UpdateProgramSplash();
			if (DataChanged != null)
				DataChanged(this, EventArgs.Empty);
		}

		public void CloneProgram(int sourceIndex, bool fullClone)
		{
			Data.CloneProgram(sourceIndex, fullClone);
			gridControl.DataSource = Data.Programs;
			advBandedGridView.RefreshData();
			if (Data.CloneLineToTheEnd)
				advBandedGridView.FocusedRowHandle = advBandedGridView.RowCount - 1;
			else
				advBandedGridView.FocusedRowHandle = sourceIndex + 1;
			UpdateProgramSplash();
			if (DataChanged != null)
				DataChanged(this, EventArgs.Empty);
		}

		private void UpdateProgramSplash()
		{
			if (Data.Programs.Any())
				gridControl.BringToFront();
			else
				pnNoPrograms.BringToFront();
		}

		public void UpdateView()
		{
			gridBandId.Visible = Data.ShowLineId;
			gridBandLogo.Visible = Data.ShowLogo;
			gridBandRate.Visible = Data.ShowRate;

			gridBandProgram.Visible = Data.ShowProgram || Data.ShowLenght || Data.ShowTime;
			bandedGridColumnName.Visible = Data.ShowProgram;
			bandedGridColumnLength.Visible = Data.ShowLenght;
			bandedGridColumnTime.Visible = Data.ShowTime;
			var secondRowIndex = 0;
			if (Data.ShowProgram)
			{
				advBandedGridView.SetColumnPosition(bandedGridColumnName, 0, 0);
				secondRowIndex++;
			}
			if (Data.ShowTime)
				advBandedGridView.SetColumnPosition(bandedGridColumnTime, secondRowIndex, 0);
			if (Data.ShowLenght)
				advBandedGridView.SetColumnPosition(bandedGridColumnLength, secondRowIndex, 1);

			gridBandStation.Visible = Data.ShowStation || Data.ShowDaypart;
			bandedGridColumnStation.Visible = Data.ShowStation;
			bandedGridColumnDaypart.Visible = Data.ShowDaypart;
			if (Data.ShowStation)
				advBandedGridView.SetColumnPosition(bandedGridColumnStation, 0, 0);
			if (Data.ShowDaypart)
				advBandedGridView.SetColumnPosition(bandedGridColumnDaypart, 1, 0);

			gridBandTotals.Visible = Data.ShowTotalSpots || Data.ShowCost;
			bandedGridColumnTotalSpots.Visible = Data.ShowTotalSpots;
			bandedGridColumnCost.Visible = Data.ShowCost;
			if (Data.ShowTotalSpots)
				advBandedGridView.SetColumnPosition(bandedGridColumnTotalSpots, 0, 0);
			if (Data.ShowCost)
				advBandedGridView.SetColumnPosition(bandedGridColumnCost, 0, 1);

			if (Data.Parent.ScheduleSettings.MondayBased)
			{
				advBandedGridView.SetColumnPosition(bandedGridColumnMondaySpot, 0, 0);
				advBandedGridView.SetColumnPosition(bandedGridColumnSundaySpot, 0, 7);
			}
			else
			{
				advBandedGridView.SetColumnPosition(bandedGridColumnSundaySpot, 0, 0);
				advBandedGridView.SetColumnPosition(bandedGridColumnMondaySpot, 0, 7);
			}

			advBandedGridView.OptionsView.ShowFooter = Data.ShowTotalRow;

			if (Data.UseDecimalRates)
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
			foreach (GridColumn column in gridBandSpots.Columns)
			{
				if (!(column.VisibleIndex > startIndex || startIndex == -1)) continue;
				if (i == 0)
					advBandedGridView.SetRowCellValue(rowHandle, column, valueToClone);
				i++;
				if (!everyOthere || i > 1)
					i = 0;
			}
		}

		private IEnumerable<DXMenuItem> GetContextMenuItems(ColumnView targetView, GridColumn targetColumn, int targetRowHandle)
		{
			var items = new List<DXMenuItem>();
			if (new[]
			{
				bandedGridColumnMondaySpot,
				bandedGridColumnThursdaySpot,
				bandedGridColumnWednesdaySpot,
				bandedGridColumnThursdaySpot,
				bandedGridColumnFridaySpot,
				bandedGridColumnSaturdaySpot,
				bandedGridColumnSundaySpot
			}.Any(sc => sc == targetColumn))
			{
				var columnName = targetColumn.ToolTip;
				items.Add(new DXMenuItem($"Clone {columnName} to all days", (o, args) =>
				{
					targetView.CloseEditor();
					var valueToClone = targetView.GetRowCellValue(targetRowHandle, targetColumn);
					CloneSpots(targetRowHandle, valueToClone, -1, false);
				}));
				items.Add(new DXMenuItem(String.Format("Clone {0} to all Remaining days", columnName), (o, args) =>
				{
					targetView.CloseEditor();
					var valueToClone = targetView.GetRowCellValue(targetRowHandle, targetColumn);
					CloneSpots(targetRowHandle, valueToClone, targetColumn.VisibleIndex, false);
				}));
				items.Add(new DXMenuItem(String.Format("Clone {0} to every other Remaining days", columnName), (o, args) =>
				{
					targetView.CloseEditor();
					var valueToClone = targetView.GetRowCellValue(targetRowHandle, targetColumn);
					CloneSpots(targetRowHandle, valueToClone, targetColumn.VisibleIndex, true);
				}));
				items.Add(new DXMenuItem("Wipe all Spots on this line", (o, args) => CloneSpots(targetRowHandle, null, -1, false)));
			}
			else if (targetColumn == bandedGridColumnIndex ||
					 targetColumn == bandedGridColumnStation ||
					 targetColumn == bandedGridColumnDaypart ||
					 targetColumn == bandedGridColumnTime ||
					 targetColumn == bandedGridColumnLogo ||
					 targetColumn == bandedGridColumnLength ||
					 targetColumn == bandedGridColumnName)
			{
				var sourceIndex = targetView.GetDataSourceRowIndex(targetRowHandle);
				items.Add(new DXMenuItem("Clone this Entire Line", (o, args) => CloneProgram(sourceIndex, true)));
				items.Add(new DXMenuItem("Clone Just this Program", (o, args) => CloneProgram(sourceIndex, false)));
				items.Add(new DXMenuItem("Delete this Line", (o, e) => DeleteProgram()));
			}
			return items;
		}

		private void InitDargDropHelper()
		{
			if (_dragDropHelper != null || !Data.Programs.Any()) return;
			_dragDropHelper = new GridDragDropHelper(advBandedGridView, true);
			_dragDropHelper.AfterDrop += OnGridControlAfterDrop;
		}

		private void CloseActiveEditorsonOutSideClick(object sender, EventArgs e)
		{
			pbNoPrograms.Focus();
			advBandedGridView.CloseEditor();
			advBandedGridView.FocusedColumn = null;
		}
		#endregion

		#region Grid Event Handlers
		private void OnGridViewCellValueChanged(object sender, CellValueChangedEventArgs e)
		{
			advBandedGridView.CloseEditor();
			advBandedGridView.UpdateCurrentRow();
			DataChanged?.Invoke(this, EventArgs.Empty);
		}

		private void OnGridViewCustomRowCellEditForEditing(object sender, CustomRowCellEditEventArgs e)
		{
			var snapshotProgram = advBandedGridView.GetRow(e.RowHandle) as SnapshotProgram;
			if (e.Column != bandedGridColumnName || snapshotProgram == null) return;
			gridColumnProgramSourceStation.Visible = String.IsNullOrEmpty(snapshotProgram.Station);
			gridColumnProgramSourceDaypart.Visible = String.IsNullOrEmpty(snapshotProgram.Daypart);
			var daypartFromList = MediaMetaData.Instance.ListManager.Dayparts.FirstOrDefault(x => x.Code.Equals(snapshotProgram.Daypart));
			gridColumnProgramSourceName.Caption = String.Format("Program ({0})", daypartFromList != null ? daypartFromList.Name : "All Programming");
			var dataSource = new List<SourceProgram>();
			if (!String.IsNullOrEmpty(snapshotProgram.Station) && (!String.IsNullOrEmpty(snapshotProgram.Daypart) || !Data.ShowDaypart))
				dataSource.AddRange(MediaMetaData.Instance.ListManager.SourcePrograms.Where(x => (x.Station.Equals(snapshotProgram.Station) || String.IsNullOrEmpty(snapshotProgram.Station)) && (!Data.ShowDaypart || (x.Daypart.Equals(snapshotProgram.Daypart) || String.IsNullOrEmpty(snapshotProgram.Daypart)))));
			else
				dataSource.AddRange(MediaMetaData.Instance.ListManager.SourcePrograms.OrderBy(sp => sp.Daypart));
			if (dataSource.Any())
			{
				gridViewProgramSource.DoubleClick -= OnGridViewProgramSourceDoubleClick;
				gridControlProgramSource.DataSource = dataSource;
				gridViewProgramSource.DoubleClick += OnGridViewProgramSourceDoubleClick;
				e.RepositoryItem = repositoryItemPopupContainerEditProgram;
			}
			else
				e.RepositoryItem = repositoryItemTextEditProgram;
		}

		private void OnGridViewMouseDown(object sender, MouseEventArgs e)
		{
			var view = sender as AdvBandedGridView;
			if (view == null) return;
			var hInfo = view.CalcHitInfo(e.Location);
			if (hInfo.HitTest != BandedGridHitTest.RowCell)
				CloseActiveEditorsonOutSideClick(null, null);
		}

		private void OnGridViewCustomDrawFooter(object sender, RowObjectCustomDrawEventArgs e)
		{
			e.Painter.DrawObject(e.Info);
			var viewInfo = ((AdvBandedGridView)sender).GetViewInfo() as AdvBandedGridViewInfo;
			var column = bandedGridColumnIndex;
			var title = "Totals: ";
			if (Data.ShowRate)
				column = bandedGridColumnRate;
			else if (Data.ShowLenght)
				column = bandedGridColumnLength;
			else if (Data.ShowTime)
				column = bandedGridColumnTime;
			else if (Data.ShowProgram)
				column = bandedGridColumnName;
			else if (Data.ShowStation)
				column = bandedGridColumnStation;
			else if (Data.ShowDaypart)
				column = bandedGridColumnDaypart;
			else if (Data.ShowLogo)
				column = bandedGridColumnLogo;
			else
				title = String.Empty;
			if (String.IsNullOrEmpty(title)) return;
			var x = viewInfo.ColumnsInfo[column].Bounds.X;
			var width = viewInfo.ColumnsInfo[column].Bounds.Width;
			var size = e.Appearance.CalcTextSize(e.Cache, title, 50);
			var textWidth = Convert.ToInt32(size.Width) + 1;
			var textRect = new Rectangle(x + width - 50, e.Bounds.Y, textWidth, e.Bounds.Height);
			e.Appearance.DrawString(e.Cache, title, textRect);
			e.Handled = true;
		}

		private void OnGridViewRowCellClick(object sender, RowCellClickEventArgs e)
		{
			if (e.Column != bandedGridColumnLogo) return;
			if (e.Clicks < 2) return;
			var selectedProgram = advBandedGridView.GetFocusedRow() as SnapshotProgram;
			if (selectedProgram == null) return;
			using (var form = new FormImageGallery(MediaMetaData.Instance.ListManager.Images))
			{
				if (form.ShowDialog() != DialogResult.OK) return;
				if (form.SelectedImageSource == null) return;
				selectedProgram.Logo = form.SelectedImageSource.Clone<ImageSource, ImageSource>();
				Data.UpdateLogo();
				advBandedGridView.UpdateCurrentRow();
				DataChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		private void OnGridViewShownEditor(object sender, EventArgs e)
		{
			var view = sender as AdvBandedGridView;
			var edit = view.ActiveEditor as TextEdit;
			if (edit == null) return;
			edit.Properties.BeforeShowMenu += OnPropertiesMenuBeforeShow;
		}

		private void OnGridViewPopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
		{
			if (!e.HitInfo.InRowCell) return;
			foreach (var menuItem in GetContextMenuItems(advBandedGridView, e.HitInfo.Column, e.HitInfo.RowHandle))
				e.Menu.Items.Add(menuItem);
		}

		private void OnGridControlAfterDrop(object sender, DragEventArgs e)
		{
			var grid = sender as GridControl;
			var view = grid.MainView as GridView;
			var hitInfo = view.CalcHitInfo(grid.PointToClient(new Point(e.X, e.Y)));
			var downHitInfo = e.Data.GetData(typeof(BandedGridHitInfo)) as BandedGridHitInfo;
			if (downHitInfo == null) return;
			var sourceRow = downHitInfo.RowHandle;
			var targetRow = hitInfo.HitTest == GridHitTest.EmptyRow ? view.DataRowCount : hitInfo.RowHandle;
			Data.ChangeProgramPosition(sourceRow, targetRow);
			gridControl.DataSource = Data.Programs;
			advBandedGridView.RefreshData();
			if (advBandedGridView.RowCount > 0)
				advBandedGridView.FocusedRowHandle = targetRow;
			if (DataChanged != null)
				DataChanged(this, EventArgs.Empty);
		}

		private void OnPropertiesMenuBeforeShow(object sender, BeforeShowMenuEventArgs e)
		{
			var items = GetContextMenuItems(advBandedGridView, advBandedGridView.FocusedColumn, advBandedGridView.FocusedRowHandle);
			if (!items.Any()) return;
			e.Menu.Items.Clear();
			foreach (var menuItem in items)
				e.Menu.Items.Add(menuItem);
		}

		private void OnGridViewProgramSourceDoubleClick(object sender, EventArgs e)
		{
			popupContainerControlProgramSource.OwnerEdit.ClosePopup();
		}

		private void OnRepositoryItemPopupContainerEditProgramCloseUp(object sender, CloseUpEventArgs e)
		{
			if (e.CloseMode != PopupCloseMode.Normal) return;
			var programSource = gridViewProgramSource.GetFocusedRow() as SourceProgram;
			var selectedProgram = advBandedGridView.GetFocusedRow() as SnapshotProgram;
			if (programSource == null || selectedProgram == null) return;
			advBandedGridView.CellValueChanged -= OnGridViewCellValueChanged;
			e.Value = programSource.Name;
			if (String.IsNullOrEmpty(selectedProgram.Daypart))
				selectedProgram.Daypart = programSource.Daypart;
			selectedProgram.Time = programSource.Time;
			if (String.IsNullOrEmpty(selectedProgram.Length))
				selectedProgram.Length = MediaMetaData.Instance.ListManager.Lengths.FirstOrDefault();
			advBandedGridView.CellValueChanged += OnGridViewCellValueChanged;
			e.AcceptValue = true;
		}

		private void OnRepositoryItemClosed(object sender, ClosedEventArgs e)
		{
			advBandedGridView.CloseEditor();
		}

		private void OnTooltipGetActiveObjectInfo(object sender, ToolTipControllerGetActiveObjectInfoEventArgs e)
		{
			if (e.SelectedControl != gridControl) return;
			var view = gridControl.GetViewAt(e.ControlMousePosition) as GridView;
			if (view == null) return;
			var hi = view.CalcHitInfo(e.ControlMousePosition);
			if (!hi.InRowCell) return;
			if (hi.Column != bandedGridColumnLogo) return;
			e.Info = new ToolTipControlInfo(new CellToolTipInfo(hi.RowHandle, hi.Column, "cell"), "Double-Click to change the logo…");
			e.Info.ImmediateToolTip = true;
			e.Info.Interval = 0;
		}
		#endregion

		#region Output
		public bool ReadyForOutput
		{
			get { return Data.Programs.Any() && (Data.ShowStation || Data.ShowProgram || Data.ShowTime || Data.ShowDaypart); }
		}

		public string SlideName
		{
			get { return Data.Name; }
		}

		public string TemplateFilePath
		{
			get
			{
				var slideSuffics = new List<string>();
				if (Data.ShowLenght)
					slideSuffics.Add("l");
				if (Data.ShowRate)
					slideSuffics.Add("r");
				if (Data.ShowTotalSpots)
					slideSuffics.Add("s");
				if (Data.ShowCost)
					slideSuffics.Add("c");
				if (!(Data.ShowLenght || Data.ShowRate || Data.ShowTotalSpots || Data.ShowCost))
					slideSuffics.Add("no_lrsc");
				return BusinessObjects.Instance.OutputManager.GetSnapshotItemFile(MediaMetaData.Instance.SettingsManager.SelectedColor, Data.ShowLogo, ProgramsPerSlide, String.Join("", slideSuffics));
			}
		}

		public string TotalRowValue
		{
			get
			{
				const string separator = "        ";
				var summaryData = Data.Parent.SnapshotSummary;
				var hasSeveralSnapshots = Data.Parent.Snapshots.Count > 1;
				var totalCostValue = String.Format("Total Cost: {0}", summaryData.TotalCost.ToString(Data.UseDecimalRates ? "$#,##0.00" : "$#,##0"));
				var totalSpotsValue = String.Format("Total Spots: {0}x", summaryData.TotalSpots);
				var values = new List<string>();
				if (hasSeveralSnapshots)
				{
					values.Add(String.Format("Weekly Spots: {0}x", Data.TotalSpots));
					values.Add(String.Format("Weekly Cost: {0}", Data.TotalCost.ToString(Data.UseDecimalRates ? "$#,##0.00" : "$#,##0")));
					values.Add(String.Format("Weeks: {0}", Data.TotalWeeks));
					values.Add(String.Format("({0}{2}{1})", totalSpotsValue, totalCostValue, separator));
				}
				else
				{
					values.Add(totalSpotsValue);
					values.Add(totalCostValue);
					values.Add(String.Format("Weekly Spots: {0}x", Data.TotalSpots));
					values.Add(String.Format("Weekly Cost: {0}", Data.TotalCost.ToString(Data.UseDecimalRates ? "$#,##0.00" : "$#,##0")));
					values.Add(String.Format("Weeks: {0}", Data.TotalWeeks));
				}
				return String.Format("{1}{0}", String.Join(separator, values), separator);
			}
		}

		public string[][] Logos { get; set; }

		public ContractSettings ContractSettings
		{
			get { return Data.ContractSettings; }
		}

		public List<Dictionary<string, string>> ReplacementsList { get; private set; }

		private int ProgramsPerSlide
		{
			get { return Data.Programs.Count <= 10 ? Data.Programs.Count : 10; }
		}

		private string[][] GetLogos()
		{
			var logos = new List<string[]>();
			if (!Data.ShowLogo) return logos.ToArray();
			var logosOnSlide = new List<string>();
			var progarmsCount = Data.Programs.Count;
			for (var i = 0; i < progarmsCount; i += ProgramsPerSlide)
			{
				logosOnSlide.Clear();
				for (int j = 0; j < ProgramsPerSlide; j++)
				{
					var fileName = String.Empty;
					if ((i + j) < progarmsCount)
					{
						var progam = Data.Programs[i + j];
						if (progam.Logo != null && progam.Logo.ContainsData)
						{
							fileName = Path.GetTempFileName();
							progam.Logo.SmallImage.Save(fileName);
						}
					}
					logosOnSlide.Add(fileName);
				}
				logos.Add(logosOnSlide.ToArray());
			}
			return logos.ToArray();
		}

		private void PopulateReplacementsList()
		{
			var key = string.Empty;
			var value = string.Empty;
			var temp = new List<string>();
			ReplacementsList = new List<Dictionary<string, string>>();
			var progarmsCount = Data.Programs.Count;

			for (var i = 0; i < progarmsCount; i += ProgramsPerSlide)
			{
				var pageDictionary = new Dictionary<string, string>();
				key = "Flightdates";
				value = Data.Parent.ScheduleSettings.FlightDates;
				if (!pageDictionary.Keys.Contains(key))
					pageDictionary.Add(key, value);
				key = "Advertiser  -  Decisionmaker";
				value = String.Format("{0}  -  {1}", Data.Parent.ScheduleSettings.BusinessName, Data.Parent.ScheduleSettings.DecisionMaker);
				if (!pageDictionary.Keys.Contains(key))
					pageDictionary.Add(key, value);

				key = "Program Info";
				var infoParts = 0;
				if (Data.ShowTime)
					infoParts++;
				if (Data.ShowDaypart)
					infoParts++;
				if (Data.ShowProgram)
					infoParts++;
				if (Data.ShowStation)
					infoParts++;
				if (infoParts == 1)
				{
					if (Data.ShowTime)
						value = "Time";
					else if (Data.ShowDaypart)
						value = "Daypart";
					else if (Data.ShowProgram)
						value = "Program";
					else if (Data.ShowStation)
						value = "Station";
				}
				else
					value = "Program Info";
				if (!pageDictionary.Keys.Contains(key))
					pageDictionary.Add(key, value);
				if (Data.ShowTotalRow)
				{
					key = "tspot";
					value = Data.ShowTotalSpots ? String.Format("{0}{1}", Data.TotalSpots.ToString("#,###"), Data.ShowSpotsX ? "x" : String.Empty) : String.Empty;
					if (!pageDictionary.Keys.Contains(key))
						pageDictionary.Add(key, value);

					key = "tcost";
					value = Data.ShowCost ? Data.TotalCost.ToString(Data.UseDecimalRates ? "$#,##0.00" : "$#,##0") : String.Empty;
					if (!pageDictionary.Keys.Contains(key))
						pageDictionary.Add(key, value);

					var totalMonday = Data.Programs.Sum(p => p.MondaySpot);
					var totalTuesday = Data.Programs.Sum(p => p.TuesdaySpot);
					var totalWednesday = Data.Programs.Sum(p => p.WednesdaySpot);
					var totalThursday = Data.Programs.Sum(p => p.ThursdaySpot);
					var totalFriday = Data.Programs.Sum(p => p.FridaySpot);
					var totalSaturday = Data.Programs.Sum(p => p.SaturdaySpot);
					var totalSunday = Data.Programs.Sum(p => p.SundaySpot);
					var mergedWeekDaySpots = new Dictionary<string, string>();
					if (Data.ShowSpotsPerWeek)
					{
						var weekDayValues = new[]
						{
								totalMonday,
								totalTuesday,
								totalWednesday,
								totalThursday,
								totalFriday,
								totalSaturday,
								totalSunday,
							};
						if ((totalMonday > 0 || mergedWeekDaySpots.Any()) &&
							weekDayValues.Any(v => v > 0))
							mergedWeekDaySpots.Add("t1", weekDayValues.Skip(1).Any(v => v > 0) ? "Merge" : String.Empty);
						weekDayValues = weekDayValues.Skip(1).ToArray();
						if ((totalTuesday > 0 || mergedWeekDaySpots.Any()) &&
							 weekDayValues.Any(v => v > 0))
							mergedWeekDaySpots.Add("t2", weekDayValues.Skip(1).Any(v => v > 0) ? "Merge" : String.Empty);
						weekDayValues = weekDayValues.Skip(1).ToArray();
						if ((totalWednesday > 0 || mergedWeekDaySpots.Any()) &&
							 weekDayValues.Any(v => v > 0))
							mergedWeekDaySpots.Add("t3", weekDayValues.Skip(1).Any(v => v > 0) ? "Merge" : String.Empty);
						weekDayValues = weekDayValues.Skip(1).ToArray();
						if ((totalThursday > 0 || mergedWeekDaySpots.Any()) &&
							weekDayValues.Any(v => v > 0))
							mergedWeekDaySpots.Add("t4", weekDayValues.Skip(1).Any(v => v > 0) ? "Merge" : String.Empty);
						weekDayValues = weekDayValues.Skip(1).ToArray();
						if ((totalFriday > 0 || mergedWeekDaySpots.Any()) &&
							weekDayValues.Any(v => v > 0))
							mergedWeekDaySpots.Add("t5", weekDayValues.Skip(1).Any(v => v > 0) ? "Merge" : String.Empty);
						weekDayValues = weekDayValues.Skip(1).ToArray();
						if ((totalSaturday > 0 || mergedWeekDaySpots.Any()) &&
							weekDayValues.Any(v => v > 0))
							mergedWeekDaySpots.Add("t6", weekDayValues.Skip(1).Any(v => v > 0) ? "Merge" : String.Empty);
						weekDayValues = weekDayValues.Skip(1).ToArray();
						if ((totalSunday > 0 || mergedWeekDaySpots.Any()) &&
							weekDayValues.Any(v => v > 0))
							mergedWeekDaySpots.Add("t7", weekDayValues.Skip(1).Any(v => v > 0) ? "Merge" : String.Empty);

						string mergedSpotValueFormat;
						switch (mergedWeekDaySpots.Values.Count)
						{
							case 1:
								mergedSpotValueFormat = "< {0}{1} >";
								break;
							case 2:
								mergedSpotValueFormat = "< ----- {0}{1} ----- >";
								break;
							default:
								mergedSpotValueFormat = "< ---------- {0}{1} ---------- >";
								break;
						}

						var mergedSpotsValue = String.Format(mergedSpotValueFormat, Data.TotalSpots.ToString("#,##0"), Data.ShowSpotsX ? "x" : String.Empty);
						foreach (var dictionaryKey in mergedWeekDaySpots.Keys.ToList())
						{
							if (mergedWeekDaySpots[dictionaryKey] == "Merge") continue;
							mergedWeekDaySpots[dictionaryKey] = mergedSpotsValue;
						}
					}

					key = "t1";
					value = mergedWeekDaySpots.ContainsKey(key) ?
							mergedWeekDaySpots[key] :
							totalMonday > 0 ? String.Format("{0}{1}", totalMonday.Value.ToString("#,###"), Data.ShowSpotsX ? "x" : String.Empty) : "-";
					if (!pageDictionary.Keys.Contains(key))
						pageDictionary.Add(key, value);

					key = "t2";
					value = mergedWeekDaySpots.ContainsKey(key) ?
							mergedWeekDaySpots[key] :
							totalTuesday > 0 ? String.Format("{0}{1}", totalTuesday.Value.ToString("#,###"), Data.ShowSpotsX ? "x" : String.Empty) : "-";
					if (!pageDictionary.Keys.Contains(key))
						pageDictionary.Add(key, value);

					key = "t3";
					value = mergedWeekDaySpots.ContainsKey(key) ?
							mergedWeekDaySpots[key] :
							totalWednesday > 0 ? String.Format("{0}{1}", totalWednesday.Value.ToString("#,###"), Data.ShowSpotsX ? "x" : String.Empty) : "-";
					if (!pageDictionary.Keys.Contains(key))
						pageDictionary.Add(key, value);

					key = "t4";
					value = mergedWeekDaySpots.ContainsKey(key) ?
							mergedWeekDaySpots[key] :
							totalThursday > 0 ? String.Format("{0}{1}", totalThursday.Value.ToString("#,###"), Data.ShowSpotsX ? "x" : String.Empty) : "-";
					if (!pageDictionary.Keys.Contains(key))
						pageDictionary.Add(key, value);

					key = "t5";
					value = mergedWeekDaySpots.ContainsKey(key) ?
							mergedWeekDaySpots[key] :
							totalFriday > 0 ? String.Format("{0}{1}", totalFriday.Value.ToString("#,###"), Data.ShowSpotsX ? "x" : String.Empty) : "-";
					if (!pageDictionary.Keys.Contains(key))
						pageDictionary.Add(key, value);

					key = "t6";
					value = mergedWeekDaySpots.ContainsKey(key) ?
							mergedWeekDaySpots[key] :
							totalSaturday > 0 ? String.Format("{0}{1}", totalSaturday.Value.ToString("#,###"), Data.ShowSpotsX ? "x" : String.Empty) : "-";
					if (!pageDictionary.Keys.Contains(key))
						pageDictionary.Add(key, value);

					key = "t7";
					value = mergedWeekDaySpots.ContainsKey(key) ?
							mergedWeekDaySpots[key] :
							totalSunday > 0 ? String.Format("{0}{1}", totalSunday.Value.ToString("#,###"), Data.ShowSpotsX ? "x" : String.Empty) : "-";
					if (!pageDictionary.Keys.Contains(key))
						pageDictionary.Add(key, value);
				}
				else
				{
					key = "Total";
					value = "Delete Row";
					if (!pageDictionary.Keys.Contains(key))
						pageDictionary.Add(key, value);
				}

				for (int j = 0; j < ProgramsPerSlide; j++)
				{
					key = String.Format("Station{0} - Time{0} - [Daypart{0}]  [Program{0}]", j + 1);

					if ((i + j) < progarmsCount)
					{
						var program = Data.Programs[i + j];

						temp.Clear();
						if (Data.ShowStation && !String.IsNullOrEmpty(program.Station))
							temp.Add(program.Station);
						if (Data.ShowTime && !String.IsNullOrEmpty(program.Time))
							temp.Add(program.Time);
						if (Data.ShowDaypart || Data.ShowProgram)
						{
							var temp2 = new List<string>();
							if (Data.ShowDaypart && !String.IsNullOrEmpty(program.Daypart))
								temp2.Add(String.Format("[{0}]", program.Daypart));
							if (Data.ShowProgram && !String.IsNullOrEmpty(program.Name))
								temp2.Add(String.Format("[{0}]", program.Name));
							temp.Add(String.Join("  ", temp2));
						}
						if (!pageDictionary.Keys.Contains(key))
							pageDictionary.Add(key, String.Join(" - ", temp));
						key = String.Format("Station{0} – Time{0} - [Daypart{0}]  [Program{0}]", j + 1);
						if (!pageDictionary.Keys.Contains(key))
							pageDictionary.Add(key, String.Join(" - ", temp));

						key = (j + 1).ToString("00");
						value = program.Index.ToString(Data.ShowLineId ? "00" : "Merge");
						if (!pageDictionary.Keys.Contains(key))
							pageDictionary.Add(key, value);

						key = String.Format("len{0}", j + 1);
						value = Data.ShowLenght && !String.IsNullOrEmpty(program.Length) ? program.Length : String.Empty;
						if (!pageDictionary.Keys.Contains(key))
							pageDictionary.Add(key, value);

						key = String.Format("rt{0}", j + 1);
						value = Data.ShowRate && program.Rate.HasValue ? program.Rate.Value.ToString(Data.UseDecimalRates ? "$#,##0.00" : "$#,##0") : String.Empty;
						if (!pageDictionary.Keys.Contains(key))
							pageDictionary.Add(key, value);

						key = String.Format("sp{0}", j + 1);
						value = String.Format("{0}{1}", program.TotalSpots.ToString("#,##0"), Data.ShowSpotsX ? "x" : String.Empty);
						if (!pageDictionary.Keys.Contains(key))
							pageDictionary.Add(key, value);

						key = String.Format("cs{0}", j + 1);
						value = program.TotalCost.ToString(Data.UseDecimalRates ? "$#,##0.00" : "$#,##0");
						if (!pageDictionary.Keys.Contains(key))
							pageDictionary.Add(key, value);

						var mergedWeekDaySpots = new Dictionary<string, string>();
						if (Data.ShowSpotsPerWeek)
						{
							var weekDayValues = new[]
							{
								program.MondaySpot,
								program.TuesdaySpot,
								program.WednesdaySpot,
								program.ThursdaySpot,
								program.FridaySpot,
								program.SaturdaySpot,
								program.SundaySpot,
							};
							if ((program.MondaySpot.HasValue || mergedWeekDaySpots.Any()) &&
								weekDayValues.Any(v => v.HasValue))
								mergedWeekDaySpots.Add(String.Format("{0}a", j + 1), weekDayValues.Skip(1).Any(v => v.HasValue) ? "Merge" : String.Empty);
							weekDayValues = weekDayValues.Skip(1).ToArray();
							if ((program.TuesdaySpot.HasValue || mergedWeekDaySpots.Any()) &&
								 weekDayValues.Any(v => v.HasValue))
								mergedWeekDaySpots.Add(String.Format("{0}b", j + 1), weekDayValues.Skip(1).Any(v => v.HasValue) ? "Merge" : String.Empty);
							weekDayValues = weekDayValues.Skip(1).ToArray();
							if ((program.WednesdaySpot.HasValue || mergedWeekDaySpots.Any()) &&
								 weekDayValues.Any(v => v.HasValue))
								mergedWeekDaySpots.Add(String.Format("{0}c", j + 1), weekDayValues.Skip(1).Any(v => v.HasValue) ? "Merge" : String.Empty);
							weekDayValues = weekDayValues.Skip(1).ToArray();
							if ((program.ThursdaySpot.HasValue || mergedWeekDaySpots.Any()) &&
								weekDayValues.Any(v => v.HasValue))
								mergedWeekDaySpots.Add(String.Format("{0}d", j + 1), weekDayValues.Skip(1).Any(v => v.HasValue) ? "Merge" : String.Empty);
							weekDayValues = weekDayValues.Skip(1).ToArray();
							if ((program.FridaySpot.HasValue || mergedWeekDaySpots.Any()) &&
								weekDayValues.Any(v => v.HasValue))
								mergedWeekDaySpots.Add(String.Format("{0}e", j + 1), weekDayValues.Skip(1).Any(v => v.HasValue) ? "Merge" : String.Empty);
							weekDayValues = weekDayValues.Skip(1).ToArray();
							if ((program.SaturdaySpot.HasValue || mergedWeekDaySpots.Any()) &&
								weekDayValues.Any(v => v.HasValue))
								mergedWeekDaySpots.Add(String.Format("{0}f", j + 1), weekDayValues.Skip(1).Any(v => v.HasValue) ? "Merge" : String.Empty);
							weekDayValues = weekDayValues.Skip(1).ToArray();
							if ((program.SundaySpot.HasValue || mergedWeekDaySpots.Any()) &&
								weekDayValues.Any(v => v.HasValue))
								mergedWeekDaySpots.Add(String.Format("{0}g", j + 1), weekDayValues.Skip(1).Any(v => v.HasValue) ? "Merge" : String.Empty);

							string mergedSpotValueFormat;
							switch (mergedWeekDaySpots.Values.Count)
							{
								case 1:
									mergedSpotValueFormat = "< {0}{1} >";
									break;
								case 2:
									mergedSpotValueFormat = "< ----- {0}{1} ----- >";
									break;
								default:
									mergedSpotValueFormat = "< ---------- {0}{1} ---------- >";
									break;
							}

							var mergedSpotsValue = String.Format(mergedSpotValueFormat, program.TotalSpots.ToString("#,##0"), Data.ShowSpotsX ? "x" : String.Empty);
							foreach (var dictionaryKey in mergedWeekDaySpots.Keys.ToList())
							{
								if (mergedWeekDaySpots[dictionaryKey] == "Merge") continue;
								mergedWeekDaySpots[dictionaryKey] = mergedSpotsValue;
							}
						}
						key = String.Format("{0}a", j + 1);
						value = mergedWeekDaySpots.ContainsKey(key) ?
							mergedWeekDaySpots[key] :
							program.MondaySpot.HasValue ? String.Format("{0}{1}", program.MondaySpot.Value.ToString("#,###"), Data.ShowSpotsX ? "x" : String.Empty) : "-";
						if (!pageDictionary.Keys.Contains(key))
							pageDictionary.Add(key, value);

						key = String.Format("{0}b", j + 1);
						value = mergedWeekDaySpots.ContainsKey(key) ?
							mergedWeekDaySpots[key] :
							program.TuesdaySpot.HasValue ? String.Format("{0}{1}", program.TuesdaySpot.Value.ToString("#,###"), Data.ShowSpotsX ? "x" : String.Empty) : "-";
						if (!pageDictionary.Keys.Contains(key))
							pageDictionary.Add(key, value);

						key = String.Format("{0}c", j + 1);
						value = mergedWeekDaySpots.ContainsKey(key) ?
							mergedWeekDaySpots[key] :
							program.WednesdaySpot.HasValue ? String.Format("{0}{1}", program.WednesdaySpot.Value.ToString("#,###"), Data.ShowSpotsX ? "x" : String.Empty) : "-";
						if (!pageDictionary.Keys.Contains(key))
							pageDictionary.Add(key, value);

						key = String.Format("{0}d", j + 1);
						value = mergedWeekDaySpots.ContainsKey(key) ?
							mergedWeekDaySpots[key] :
							program.ThursdaySpot.HasValue ? String.Format("{0}{1}", program.ThursdaySpot.Value.ToString("#,###"), Data.ShowSpotsX ? "x" : String.Empty) : "-";
						if (!pageDictionary.Keys.Contains(key))
							pageDictionary.Add(key, value);

						key = String.Format("{0}e", j + 1);
						value = mergedWeekDaySpots.ContainsKey(key) ?
							mergedWeekDaySpots[key] :
							program.FridaySpot.HasValue ? String.Format("{0}{1}", program.FridaySpot.Value.ToString("#,###"), Data.ShowSpotsX ? "x" : String.Empty) : "-";
						if (!pageDictionary.Keys.Contains(key))
							pageDictionary.Add(key, value);

						key = String.Format("{0}f", j + 1);
						value = mergedWeekDaySpots.ContainsKey(key) ?
							mergedWeekDaySpots[key] :
							program.SaturdaySpot.HasValue ? String.Format("{0}{1}", program.SaturdaySpot.Value.ToString("#,###"), Data.ShowSpotsX ? "x" : String.Empty) : "-";
						if (!pageDictionary.Keys.Contains(key))
							pageDictionary.Add(key, value);

						key = String.Format("{0}g", j + 1);
						value = mergedWeekDaySpots.ContainsKey(key) ?
							mergedWeekDaySpots[key] :
							program.SundaySpot.HasValue ? String.Format("{0}{1}", program.SundaySpot.Value.ToString("#,###"), Data.ShowSpotsX ? "x" : String.Empty) : "-";
						if (!pageDictionary.Keys.Contains(key))
							pageDictionary.Add(key, value);
					}
					else
					{
						value = "Delete Row";
						if (!pageDictionary.Keys.Contains(key))
							pageDictionary.Add(key, value);
						key = String.Format("Station{0} – Time{0} - [Daypart{0}]  [Program{0}]", j + 1);
						if (!pageDictionary.Keys.Contains(key))
							pageDictionary.Add(key, value);
					}
				}
				ReplacementsList.Add(pageDictionary);
			}
		}

		public PreviewGroup GetPreviewGroup(Theme selectedTheme)
		{
			Logos = GetLogos();
			PopulateReplacementsList();
			var previewGroup = new PreviewGroup
			{
				Name = SlideName,
				PresentationSourcePath = Path.Combine(Common.Core.Configuration.ResourceManager.Instance.TempFolder.LocalPath, Path.GetFileName(Path.GetTempFileName()))
			};
			RegularMediaSchedulePowerPointHelper.Instance.PrepareSnapshotEmail(previewGroup.PresentationSourcePath, new[] { this }, selectedTheme, MediaMetaData.Instance.SettingsManager.UseSlideMaster);
			return previewGroup;
		}

		public void Output(Theme selectedTheme)
		{
			Logos = GetLogos();
			PopulateReplacementsList();
			RegularMediaSchedulePowerPointHelper.Instance.AppendSnapshot(new[] { this }, selectedTheme, MediaMetaData.Instance.SettingsManager.UseSlideMaster);
		}
		#endregion
	}
}
