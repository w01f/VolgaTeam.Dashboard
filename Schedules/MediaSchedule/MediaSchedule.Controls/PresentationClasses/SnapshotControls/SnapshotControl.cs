﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
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
using NewBizWiz.CommonGUI.Common;
using NewBizWiz.CommonGUI.Preview;
using NewBizWiz.CommonGUI.ToolForms;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.MediaSchedule;
using NewBizWiz.MediaSchedule.Controls.BusinessClasses;
using NewBizWiz.MediaSchedule.Controls.InteropClasses;

namespace NewBizWiz.MediaSchedule.Controls.PresentationClasses.SnapshotControls
{
	[ToolboxItem(false)]
	//public partial class SnapshotControl : UserControl, ISnapshotSlide
	public sealed partial class SnapshotControl : XtraTabPage, ISnapshotSlide
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
		}

		#region Methods
		public void LoadData(Snapshot data)
		{
			Data = data;
			Text = Data.Name;
			repositoryItemComboBoxStations.Items.Clear();
			repositoryItemComboBoxStations.Items.AddRange(Data.Parent.Stations.Where(station => station.Available).Select(station => station.Name).ToArray());
			repositoryItemComboBoxDayparts.Items.Clear();
			repositoryItemComboBoxDayparts.Items.AddRange(Data.Parent.Dayparts.Where(daypart => daypart.Available).Select(daypart => daypart.Code).ToArray());
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
			if (Utilities.Instance.ShowWarningQuestion(String.Format("Delete Line ID {0}?", selectedProgramIndex)) != DialogResult.Yes) return;
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

			if (Data.Parent.MondayBased)
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
				items.Add(new DXMenuItem(String.Format("Clone {0} to all days", columnName), (o, args) =>
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
			_dragDropHelper.AfterDrop += gridControl_AfterDrop;
		}

		private void CloseActiveEditorsonOutSideClick(object sender, EventArgs e)
		{
			pbNoPrograms.Focus();
			advBandedGridView.CloseEditor();
			advBandedGridView.FocusedColumn = null;
		}
		#endregion

		#region Grid Event Handlers
		private void advBandedGridView_CellValueChanged(object sender, CellValueChangedEventArgs e)
		{
			advBandedGridView.CloseEditor();
			advBandedGridView.UpdateCurrentRow();
			if (DataChanged != null)
				DataChanged(this, EventArgs.Empty);
		}

		private void advBandedGridView_CustomRowCellEditForEditing(object sender, CustomRowCellEditEventArgs e)
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
				gridViewProgramSource.DoubleClick -= gridViewProgramSource_DoubleClick;
				gridControlProgramSource.DataSource = dataSource;
				gridViewProgramSource.DoubleClick += gridViewProgramSource_DoubleClick;
				e.RepositoryItem = repositoryItemPopupContainerEditProgram;
			}
			else
				e.RepositoryItem = repositoryItemTextEditProgram;
		}

		private void advBandedGridView_MouseDown(object sender, MouseEventArgs e)
		{
			var view = sender as AdvBandedGridView;
			if (view == null) return;
			var hInfo = view.CalcHitInfo(e.Location);
			if (hInfo.HitTest != BandedGridHitTest.RowCell)
				CloseActiveEditorsonOutSideClick(null, null);
		}

		private void advBandedGridView_CustomDrawFooter(object sender, RowObjectCustomDrawEventArgs e)
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

		private void advBandedGridView_RowCellClick(object sender, RowCellClickEventArgs e)
		{
			if (e.Column != bandedGridColumnLogo) return;
			var selectedProgram = advBandedGridView.GetFocusedRow() as SnapshotProgram;
			if (selectedProgram == null) return;
			using (var form = new FormImageGallery(MediaMetaData.Instance.ListManager.Images))
			{
				form.SelectedImage = selectedProgram.Logo != null ? selectedProgram.Logo.BigImage : null;
				if (form.ShowDialog() != DialogResult.OK) return;
				if (form.SelectedImageSource == null) return;
				selectedProgram.Logo = form.SelectedImageSource;
				advBandedGridView.UpdateCurrentRow();
				if (DataChanged != null)
					DataChanged(this, EventArgs.Empty);
			}
		}

		private void advBandedGridView_ShownEditor(object sender, EventArgs e)
		{
			var view = sender as AdvBandedGridView;
			var edit = view.ActiveEditor as TextEdit;
			if (edit == null) return;
			edit.Properties.BeforeShowMenu += Properties_BeforeShowMenu;
		}

		private void advBandedGridView_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
		{
			if (!e.HitInfo.InRowCell) return;
			foreach (var menuItem in GetContextMenuItems(advBandedGridView, e.HitInfo.Column, e.HitInfo.RowHandle))
				e.Menu.Items.Add(menuItem);
		}

		private void gridControl_AfterDrop(object sender, DragEventArgs e)
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

		private void Properties_BeforeShowMenu(object sender, BeforeShowMenuEventArgs e)
		{
			var items = GetContextMenuItems(advBandedGridView, advBandedGridView.FocusedColumn, advBandedGridView.FocusedRowHandle);
			if (!items.Any()) return;
			e.Menu.Items.Clear();
			foreach (var menuItem in items)
				e.Menu.Items.Add(menuItem);
		}

		private void gridViewProgramSource_DoubleClick(object sender, EventArgs e)
		{
			popupContainerControlProgramSource.OwnerEdit.ClosePopup();
		}

		private void repositoryItemPopupContainerEditProgram_CloseUp(object sender, CloseUpEventArgs e)
		{
			if (e.CloseMode != PopupCloseMode.Normal) return;
			var programSource = gridViewProgramSource.GetFocusedRow() as SourceProgram;
			var selectedProgram = advBandedGridView.GetFocusedRow() as SnapshotProgram;
			if (programSource == null || selectedProgram == null) return;
			advBandedGridView.CellValueChanged -= advBandedGridView_CellValueChanged;
			e.Value = programSource.Name;
			if (String.IsNullOrEmpty(selectedProgram.Daypart))
				selectedProgram.Daypart = programSource.Daypart;
			selectedProgram.Time = programSource.Time;
			if (String.IsNullOrEmpty(selectedProgram.Length))
				selectedProgram.Length = MediaMetaData.Instance.ListManager.Lengths.FirstOrDefault();
			advBandedGridView.CellValueChanged += advBandedGridView_CellValueChanged;
			e.AcceptValue = true;
		}

		private void repositoryItemPopupContainerEditProgram_Closed(object sender, ClosedEventArgs e)
		{
			advBandedGridView.CloseEditor();
		}
		#endregion

		#region Output
		public bool ReadyForOutput
		{
			get { return Data.Programs.Any() && (Data.ShowProgram || Data.ShowTime || Data.ShowDaypart); }
		}

		public string SlideName
		{
			get { return Data.Name; }
		}

		public string TemplateFileName
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
				return String.Format(OutputManager.SnapshotTemplateFileName,
					MediaMetaData.Instance.SettingsManager.SelectedColor,
					Data.ShowLogo ? "logo" : "no_logo",
					ProgramsPerSlide,
					String.Join("", slideSuffics));
			}
		}

		public string[][] Logos { get; set; }

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
				value = Data.Parent.FlightDates;
				if (!pageDictionary.Keys.Contains(key))
					pageDictionary.Add(key, value);
				key = "Advertiser  -  Decisionmaker";
				value = String.Format("{0}  -  {1}", Data.Parent.BusinessName, Data.Parent.DecisionMaker);
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
				if (infoParts == 1)
				{
					if (Data.ShowTime)
						value = "Time";
					else if (Data.ShowDaypart)
						value = "Daypart";
					else if (Data.ShowProgram)
						value = "Program";
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

					key = "t1";
					var totalMonday = Data.Programs.Sum(p => p.MondaySpot);
					value = totalMonday.HasValue && totalMonday.Value > 0 ? String.Format("{0}{1}", totalMonday.Value.ToString("#,###"), Data.ShowSpotsX ? "x" : String.Empty) : "-";
					if (!pageDictionary.Keys.Contains(key))
						pageDictionary.Add(key, value);

					key = "t2";
					var totalTuesday = Data.Programs.Sum(p => p.TuesdaySpot);
					value = totalTuesday.HasValue && totalTuesday.Value > 0 ? String.Format("{0}{1}", totalTuesday.Value.ToString("#,###"), Data.ShowSpotsX ? "x" : String.Empty) : "-";
					if (!pageDictionary.Keys.Contains(key))
						pageDictionary.Add(key, value);

					key = "t3";
					var totalWednesday = Data.Programs.Sum(p => p.WednesdaySpot);
					value = totalWednesday.HasValue && totalWednesday.Value > 0 ? String.Format("{0}{1}", totalWednesday.Value.ToString("#,###"), Data.ShowSpotsX ? "x" : String.Empty) : "-";
					if (!pageDictionary.Keys.Contains(key))
						pageDictionary.Add(key, value);

					key = "t4";
					var totalThursday = Data.Programs.Sum(p => p.ThursdaySpot);
					value = totalThursday.HasValue && totalThursday.Value > 0 ? String.Format("{0}{1}", totalThursday.Value.ToString("#,###"), Data.ShowSpotsX ? "x" : String.Empty) : "-";
					if (!pageDictionary.Keys.Contains(key))
						pageDictionary.Add(key, value);

					key = "t5";
					var totalFriday = Data.Programs.Sum(p => p.FridaySpot);
					value = totalFriday.HasValue && totalFriday.Value > 0 ? String.Format("{0}{1}", totalFriday.Value.ToString("#,###"), Data.ShowSpotsX ? "x" : String.Empty) : "-";
					if (!pageDictionary.Keys.Contains(key))
						pageDictionary.Add(key, value);

					key = "t6";
					var totalSaturday = Data.Programs.Sum(p => p.SaturdaySpot);
					value = totalSaturday.HasValue && totalSaturday.Value > 0 ? String.Format("{0}{1}", totalSaturday.Value.ToString("#,###"), Data.ShowSpotsX ? "x" : String.Empty) : "-";
					if (!pageDictionary.Keys.Contains(key))
						pageDictionary.Add(key, value);

					key = "t7";
					var totalSunday = Data.Programs.Sum(p => p.SundaySpot);
					value = totalSunday.HasValue && totalSunday.Value > 0 ? String.Format("{0}{1}", totalSunday.Value.ToString("#,###"), Data.ShowSpotsX ? "x" : String.Empty) : "-";
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
						var progam = Data.Programs[i + j];

						temp.Clear();
						if (Data.ShowStation && !String.IsNullOrEmpty(progam.Station))
							temp.Add(progam.Station);
						if (Data.ShowTime && !String.IsNullOrEmpty(progam.Time))
							temp.Add(progam.Time);
						if (Data.ShowDaypart || Data.ShowProgram)
						{
							var temp2 = new List<string>();
							if (Data.ShowDaypart && !String.IsNullOrEmpty(progam.Daypart))
								temp2.Add(String.Format("[{0}]", progam.Daypart));
							if (Data.ShowProgram && !String.IsNullOrEmpty(progam.Name))
								temp2.Add(String.Format("[{0}]", progam.Name));
							temp.Add(String.Join("  ", temp2));
						}
						if (!pageDictionary.Keys.Contains(key))
							pageDictionary.Add(key, String.Join(" - ", temp));
						key = String.Format("Station{0} – Time{0} - [Daypart{0}]  [Program{0}]", j + 1);
						if (!pageDictionary.Keys.Contains(key))
							pageDictionary.Add(key, String.Join(" - ", temp));

						key = (j + 1).ToString("00");
						value = progam.Index.ToString(Data.ShowLineId ? "00" : "Merge");
						if (!pageDictionary.Keys.Contains(key))
							pageDictionary.Add(key, value);

						key = String.Format("len{0}", j + 1);
						value = Data.ShowLenght && !String.IsNullOrEmpty(progam.Length) ? progam.Length : String.Empty;
						if (!pageDictionary.Keys.Contains(key))
							pageDictionary.Add(key, value);

						key = String.Format("rt{0}", j + 1);
						value = Data.ShowRate && progam.Rate.HasValue ? progam.Rate.Value.ToString(Data.UseDecimalRates ? "$#,##0.00" : "$#,##0") : String.Empty;
						if (!pageDictionary.Keys.Contains(key))
							pageDictionary.Add(key, value);

						key = String.Format("sp{0}", j + 1);
						value = String.Format("{0}{1}", progam.TotalSpots.ToString("#,###"), Data.ShowSpotsX ? "x" : String.Empty);
						if (!pageDictionary.Keys.Contains(key))
							pageDictionary.Add(key, value);

						key = String.Format("cs{0}", j + 1);
						value = progam.TotalCost.ToString(Data.UseDecimalRates ? "$#,##0.00" : "$#,##0");
						if (!pageDictionary.Keys.Contains(key))
							pageDictionary.Add(key, value);

						key = String.Format("{0}a", j + 1);
						value = progam.MondaySpot.HasValue ? String.Format("{0}{1}", progam.MondaySpot.Value.ToString("#,###"), Data.ShowSpotsX ? "x" : String.Empty) : "-";
						if (!pageDictionary.Keys.Contains(key))
							pageDictionary.Add(key, value);

						key = String.Format("{0}b", j + 1);
						value = progam.TuesdaySpot.HasValue ? String.Format("{0}{1}", progam.TuesdaySpot.Value.ToString("#,###"), Data.ShowSpotsX ? "x" : String.Empty) : "-";
						if (!pageDictionary.Keys.Contains(key))
							pageDictionary.Add(key, value);

						key = String.Format("{0}c", j + 1);
						value = progam.WednesdaySpot.HasValue ? String.Format("{0}{1}", progam.WednesdaySpot.Value.ToString("#,###"), Data.ShowSpotsX ? "x" : String.Empty) : "-";
						if (!pageDictionary.Keys.Contains(key))
							pageDictionary.Add(key, value);

						key = String.Format("{0}d", j + 1);
						value = progam.ThursdaySpot.HasValue ? String.Format("{0}{1}", progam.ThursdaySpot.Value.ToString("#,###"), Data.ShowSpotsX ? "x" : String.Empty) : "-";
						if (!pageDictionary.Keys.Contains(key))
							pageDictionary.Add(key, value);

						key = String.Format("{0}e", j + 1);
						value = progam.FridaySpot.HasValue ? String.Format("{0}{1}", progam.FridaySpot.Value.ToString("#,###"), Data.ShowSpotsX ? "x" : String.Empty) : "-";
						if (!pageDictionary.Keys.Contains(key))
							pageDictionary.Add(key, value);

						key = String.Format("{0}f", j + 1);
						value = progam.SaturdaySpot.HasValue ? String.Format("{0}{1}", progam.SaturdaySpot.Value.ToString("#,###"), Data.ShowSpotsX ? "x" : String.Empty) : "-";
						if (!pageDictionary.Keys.Contains(key))
							pageDictionary.Add(key, value);

						key = String.Format("{0}g", j + 1);
						value = progam.SundaySpot.HasValue ? String.Format("{0}{1}", progam.SundaySpot.Value.ToString("#,###"), Data.ShowSpotsX ? "x" : String.Empty) : "-";
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
				PresentationSourcePath = Path.Combine(SettingsManager.Instance.TempPath, Path.GetFileName(Path.GetTempFileName()))
			};
			RegularMediaSchedulePowerPointHelper.Instance.PrepareSnapshot(previewGroup.PresentationSourcePath, new[] { this }, selectedTheme, MediaMetaData.Instance.SettingsManager.UseSlideMaster);
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