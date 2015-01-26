using System;
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
using DevExpress.XtraGrid.Dragging;
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

namespace NewBizWiz.MediaSchedule.Controls.PresentationClasses.OptionsControls
{
	[ToolboxItem(false)]
	//public partial class OptionsControl : UserControl, IOptionsSlide
	public sealed partial class OptionsControl : XtraTabPage, IOptionsSlide
	{
		private bool _allowToSave;
		private GridDragDropHelper _dragDropHelper;
		public OptionSet Data { get; private set; }

		public event EventHandler<EventArgs> DataChanged;

		public OptionsControl(OptionSet data)
		{
			InitializeComponent();
			pnNoPrograms.Dock = DockStyle.Fill;
			gridControl.Dock = DockStyle.Fill;
			LoadData(data);
		}

		#region Methods
		public void LoadData(OptionSet data)
		{
			_allowToSave = false;
			Data = data;
			Text = Data.Name;
			repositoryItemComboBoxStations.Items.Clear();
			repositoryItemComboBoxStations.Items.AddRange(Data.Parent.Stations.Where(station => station.Available).Select(station => station.Name).ToArray());
			repositoryItemComboBoxDays.Items.Clear();
			repositoryItemComboBoxDays.Items.AddRange(MediaMetaData.Instance.ListManager.Days);
			repositoryItemComboBoxLengths.Items.Clear();
			repositoryItemComboBoxLengths.Items.AddRange(MediaMetaData.Instance.ListManager.Lengths);
			repositoryItemComboBoxTimes.Items.Clear();
			repositoryItemComboBoxTimes.Items.AddRange(MediaMetaData.Instance.ListManager.Times);
			gridControl.DataSource = Data.Programs;
			advBandedGridView.RefreshData();
			UpdateProgramSplash();
			InitDargDropHelper();
			_allowToSave = true;

			UpdateView();
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
			var selectedProgram = advBandedGridView.GetFocusedRow() as OptionProgram;
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

		public void CloneProgram(int sourceIndex)
		{
			Data.CloneProgram(sourceIndex);
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
			_allowToSave = false;
			gridBandId.Visible = Data.ShowLineId;
			gridBandLogo.Visible = Data.ShowLogo;
			if (Data.ShowStation ||
				Data.ShowProgram ||
				Data.ShowDay ||
				Data.ShowTime ||
				Data.ShowSpots ||
				Data.ShowRate ||
				Data.ShowLenght ||
				Data.ShowCost)
			{
				gridBandOtherColumns.Visible = true;

				var positionedColumns = new SortedDictionary<int, BandedGridColumn>();
				bandedGridColumnStation.Visible = false;
				if (Data.ShowStation)
					positionedColumns.Add(Data.PositionStation, bandedGridColumnStation);
				bandedGridColumnName.Visible = false;
				if (Data.ShowProgram)
					positionedColumns.Add(Data.PositionProgram, bandedGridColumnName);
				bandedGridColumnDay.Visible = false;
				if (Data.ShowDay)
					positionedColumns.Add(Data.PositionDay, bandedGridColumnDay);
				bandedGridColumnTime.Visible = false;
				if (Data.ShowTime)
					positionedColumns.Add(Data.PositionTime, bandedGridColumnTime);
				bandedGridColumnSpots.Visible = false;
				if (Data.ShowSpots)
					positionedColumns.Add(Data.PositionSpots, bandedGridColumnSpots);
				bandedGridColumnRate.Visible = false;
				if (Data.ShowRate)
					positionedColumns.Add(Data.PositionRate, bandedGridColumnRate);
				bandedGridColumnLength.Visible = false;
				if (Data.ShowLenght)
					positionedColumns.Add(Data.PositionLenght, bandedGridColumnLength);
				bandedGridColumnCost.Visible = false;
				if (Data.ShowCost)
					positionedColumns.Add(Data.PositionCost, bandedGridColumnCost);
				var position = 1;
				foreach (var valuePair in positionedColumns)
				{
					advBandedGridView.SetColumnPosition(valuePair.Value, 0, position);
					position++;
				}
			}
			else
				gridBandOtherColumns.Visible = false;

			UpdateColumnPositions();

			switch (Data.SpotType)
			{
				case SpotType.Week:
					bandedGridColumnSpots.Caption = String.Format("Weekly{0}Spots", Environment.NewLine);
					break;
				case SpotType.Month:
					bandedGridColumnSpots.Caption = String.Format("Monthly{0}Spots", Environment.NewLine);
					break;
				case SpotType.Total:
					bandedGridColumnSpots.Caption = String.Format("Total{0}Spots", Environment.NewLine);
					break;
			}

			if (Data.UseDecimalRates)
			{
				bandedGridColumnCost.DisplayFormat.FormatString = "$#,##0.00";
				repositoryItemSpinEditRate.DisplayFormat.FormatString = "$#,##0.00";
				repositoryItemSpinEditRate.EditFormat.FormatString = "$#,##0.00";
				repositoryItemSpinEditRate.IsFloatValue = true;
			}
			else
			{
				bandedGridColumnCost.DisplayFormat.FormatString = "$#,##0";
				repositoryItemSpinEditRate.DisplayFormat.FormatString = "$#,##0";
				repositoryItemSpinEditRate.EditFormat.FormatString = "$#,##0";
				repositoryItemSpinEditRate.IsFloatValue = false;
			}

			_allowToSave = true;
		}

		private void UpdateColumnPositions()
		{
			var columnPosition = 0;
			var columns = gridBandOtherColumns.Columns.OfType<BandedGridColumn>().OrderBy(c => c.ColIndex).ToList();
			var visibleColumns = columns.Where(c => c.ColIndex >= 0).ToList();
			var hiddenPosition = visibleColumns.Count;
			foreach (var column in columns)
			{
				if (column == bandedGridColumnStation)
					Data.PositionStation = column.ColIndex >= 0 ? columnPosition++ : hiddenPosition++;
				else if (column == bandedGridColumnName)
					Data.PositionProgram = column.ColIndex >= 0 ? columnPosition++ : hiddenPosition++;
				else if (column == bandedGridColumnDay)
					Data.PositionDay = column.ColIndex >= 0 ? columnPosition++ : hiddenPosition++;
				else if (column == bandedGridColumnTime)
					Data.PositionTime = column.ColIndex >= 0 ? columnPosition++ : hiddenPosition++;
				else if (column == bandedGridColumnSpots)
					Data.PositionSpots = column.ColIndex >= 0 ? columnPosition++ : hiddenPosition++;
				else if (column == bandedGridColumnRate)
					Data.PositionRate = column.ColIndex >= 0 ? columnPosition++ : hiddenPosition++;
				else if (column == bandedGridColumnLength)
					Data.PositionLenght = column.ColIndex >= 0 ? columnPosition++ : hiddenPosition++;
				else if (column == bandedGridColumnCost)
					Data.PositionCost = column.ColIndex >= 0 ? columnPosition++ : hiddenPosition++;
			}
		}

		private IEnumerable<DXMenuItem> GetContextMenuItems(ColumnView targetView, int targetRowHandle)
		{
			var items = new List<DXMenuItem>();
			var sourceIndex = targetView.GetDataSourceRowIndex(targetRowHandle);
			items.Add(new DXMenuItem("Clone this Line", (o, args) => CloneProgram(sourceIndex)));
			items.Add(new DXMenuItem("Delete this Line", (o, e) => DeleteProgram()));
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
			var optionsProgram = advBandedGridView.GetRow(e.RowHandle) as OptionProgram;
			if (e.Column != bandedGridColumnName || optionsProgram == null) return;
			gridColumnProgramSourceStation.Visible = String.IsNullOrEmpty(optionsProgram.Station);
			gridColumnProgramSourceDaypart.Visible = true;
			gridColumnProgramSourceName.Caption = String.Format("All Programming");
			var dataSource = new List<SourceProgram>();
			dataSource.AddRange(!String.IsNullOrEmpty(optionsProgram.Station) ?
				MediaMetaData.Instance.ListManager.SourcePrograms.Where(x => (x.Station.Equals(optionsProgram.Station) || String.IsNullOrEmpty(optionsProgram.Station))) :
				MediaMetaData.Instance.ListManager.SourcePrograms.OrderBy(sp => sp.Daypart));
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

		private void advBandedGridView_RowCellClick(object sender, RowCellClickEventArgs e)
		{
			if (e.Column != bandedGridColumnLogo) return;
			var selectedProgram = advBandedGridView.GetFocusedRow() as OptionProgram;
			if (selectedProgram == null) return;
			using (var form = new FormImageGallery(MediaMetaData.Instance.ListManager.Images))
			{
				form.SelectedImage = selectedProgram.Logo != null ? selectedProgram.Logo.BigImage : null;
				if (form.ShowDialog() != DialogResult.OK) return;
				if (form.SelectedImageSource == null) return;
				selectedProgram.Logo = form.SelectedImageSource;
				Data.UpdateLogo();
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
			foreach (var menuItem in GetContextMenuItems(advBandedGridView, e.HitInfo.RowHandle))
				e.Menu.Items.Add(menuItem);
		}

		private void advBandedGridView_ColumnPositionChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			UpdateColumnPositions();
			if (DataChanged != null)
				DataChanged(this, EventArgs.Empty);
		}

		private void advBandedGridView_DragObjectOver(object sender, DragObjectOverEventArgs e)
		{
			var draggedColumn = e.DragObject as BandedGridColumn;
			var targetPositionInfo = e.DropInfo as AdvBandedColumnPositionInfo;
			if (draggedColumn != null && targetPositionInfo != null)
				e.DropInfo.Valid = draggedColumn.RowIndex == targetPositionInfo.RowIndex;
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
			var items = GetContextMenuItems(advBandedGridView, advBandedGridView.FocusedRowHandle);
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
			var selectedProgram = advBandedGridView.GetFocusedRow() as OptionProgram;
			if (programSource == null || selectedProgram == null) return;
			advBandedGridView.CellValueChanged -= advBandedGridView_CellValueChanged;
			e.Value = programSource.Name;
			if (String.IsNullOrEmpty(selectedProgram.Day))
				selectedProgram.Day = programSource.Day;
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
			get { return Data.Programs.Any() && (Data.ShowLineId || Data.ShowStation || Data.ShowProgram || Data.ShowDay || Data.ShowTime || Data.ShowSpots || Data.ShowRate || Data.ShowLenght || Data.ShowCost); }
		}

		public string SlideName
		{
			get { return Data.Name; }
		}

		public string TemplateFileName
		{
			get
			{
				return String.Format(OutputManager.OptionsTemplateFileName,
					MediaMetaData.Instance.SettingsManager.SelectedColor,
					Data.ShowLogo ? "_logo" : String.Empty);
			}
		}

		public string[][] Logos { get; set; }

		public float[] ColumnWidths { get; set; }

		public List<Dictionary<string, string>> ReplacementsList { get; private set; }

		private int ProgramsPerSlide
		{
			get { return 10; }
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

		private IEnumerable<OutputColumnInfo> GetColumnInfo()
		{
			var columnInfoList = new List<OutputColumnInfo>();
			var widths = GetColumnWidths();

			if (Data.ShowStation)
			{
				var columnInfo = new StationColumnInfo
				{
					Width = widths["station"],
					Index = Data.PositionStation + 1
				};
				columnInfoList.Add(columnInfo);
			}
			if (Data.ShowProgram)
			{
				var columnInfo = new ProgramColumnInfo
				{
					Width = widths["program"],
					Index = Data.PositionProgram + 1
				};
				columnInfoList.Add(columnInfo);
			}
			if (Data.ShowDay)
			{
				var columnInfo = new DayColumnInfo
				{
					Width = widths["day"],
					Index = Data.PositionDay + 1
				};
				columnInfoList.Add(columnInfo);
			}
			if (Data.ShowTime)
			{
				var columnInfo = new TimeColumnInfo
				{
					Width = widths["time"],
					Index = Data.PositionTime + 1
				};
				columnInfoList.Add(columnInfo);
			}
			if (Data.ShowSpots)
			{
				var columnInfo = new SpotsColumnInfo(Data.SpotType)
				{
					Width = widths["spots"],
					Index = Data.PositionSpots + 1
				};
				columnInfoList.Add(columnInfo);
			}
			if (Data.ShowRate)
			{
				var columnInfo = new RateColumnInfo
				{
					Width = widths["rate"],
					Index = Data.PositionRate + 1
				};
				columnInfoList.Add(columnInfo);
			}
			if (Data.ShowLenght)
			{
				var columnInfo = new LengthColumnInfo
				{
					Width = widths["length"],
					Index = Data.PositionLenght + 1
				};
				columnInfoList.Add(columnInfo);
			}
			if (Data.ShowCost)
			{
				var columnInfo = new CostColumnInfo
				{
					Width = widths["cost"],
					Index = Data.PositionCost + 1
				};
				columnInfoList.Add(columnInfo);
			}
			return columnInfoList;
		}

		private Dictionary<string, float> GetColumnWidths()
		{
			var widths = new Dictionary<string, float>();
			var widthsPath = Path.Combine(BusinessWrapper.Instance.OutputManager.OptionsTemplatesFolderPath, OutputManager.OptionsColumnWidthsFileName);
			if (File.Exists(widthsPath))
			{
				var columnName = String.Empty;
				foreach (var line in File.ReadAllLines(widthsPath))
				{
					decimal temp;
					var value = line;
					if (value.StartsWith("."))
						value = "0" + value;
					if (value.StartsWith("*"))
						columnName = value.Replace("*", String.Empty);
					else if (Decimal.TryParse(value, out temp))
						widths.Add(columnName, (float)temp);
				}
			}
			return widths;
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

				key = "tallyinfo";
				if (Data.ShowTotalSpots || Data.ShowTotalCost || Data.ShowAverageRate)
				{
					var spotPrefix = String.Empty;
					switch (Data.SpotType)
					{
						case SpotType.Week:
							spotPrefix = "Weekly";
							break;
						case SpotType.Month:
							spotPrefix = "Monthly";
							break;
						case SpotType.Total:
							spotPrefix = "Total";
							break;
					}
					temp.Clear();
					if (Data.ShowTotalSpots)
						temp.Add(String.Format("{0} Spots: {1}", spotPrefix, String.Format("{0}{1}", Data.TotalSpots.ToString("#,###"), Data.ShowSpotsX ? "x" : String.Empty)));
					if (Data.ShowTotalCost)
						temp.Add(String.Format("{0} Cost: {1}", spotPrefix, Data.TotalCost.ToString(Data.UseDecimalRates ? "$#,##0.00" : "$#,##0")));
					if (Data.ShowAverageRate)
						temp.Add(String.Format("Average Rate: {0}", Data.AvgRate.ToString(Data.UseDecimalRates ? "$#,##0.00" : "$#,##0")));
					value = String.Join("     ", temp);
				}
				else
					value = String.Empty;
				if (!pageDictionary.Keys.Contains(key))
					pageDictionary.Add(key, value);

				var dynamicColumnInfoList = GetColumnInfo().OrderBy(ci => ci.Index).ToList();
				var columHeaderTags = new[] { "Station", "Program", "Day", "Time", "WMT   Spots", "Rate", "Length", "Cost" };
				var columValuesTags = new[] { "station{0}", "program{0}", "day{0}", "time{0}", "sp{0}", "rt{0}", "len{0}", "cs{0}" };
				var columnIndex = 0;
				foreach (var columHeaderTag in columHeaderTags)
				{
					key = columHeaderTag;
					value = dynamicColumnInfoList.Count > columnIndex ?
						dynamicColumnInfoList[columnIndex].HeaderCaption :
						"Delete Column";
					if (!pageDictionary.Keys.Contains(key))
						pageDictionary.Add(key, value);
					columnIndex++;
				}

				for (int j = 0; j < ProgramsPerSlide; j++)
				{
					key = (j + 1).ToString("00");
					if ((i + j) < progarmsCount)
					{
						var progam = Data.Programs[i + j];
						value = Data.ShowLineId ? progam.Index.ToString("00") : "Delete Column";
						if (!pageDictionary.Keys.Contains(key))
							pageDictionary.Add(key, value);

						columnIndex = 0;
						foreach (var columValuesTag in columValuesTags)
						{
							key = String.Format(columValuesTag, j + 1);
							value = dynamicColumnInfoList.Count > columnIndex ?
								dynamicColumnInfoList[columnIndex].GetValue(progam) :
								"Delete Column";
							if (!pageDictionary.Keys.Contains(key))
								pageDictionary.Add(key, value);
							columnIndex++;
						}
					}
					else
					{
						value = "Delete Row";
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
			ColumnWidths = GetColumnInfo().OrderBy(ci => ci.Index).Select(ci => ci.Width).ToArray();
			PopulateReplacementsList();
			var previewGroup = new PreviewGroup
			{
				Name = SlideName,
				PresentationSourcePath = Path.Combine(SettingsManager.Instance.TempPath, Path.GetFileName(Path.GetTempFileName()))
			};
			RegularMediaSchedulePowerPointHelper.Instance.PrepareOptions(previewGroup.PresentationSourcePath, new[] { this }, selectedTheme, MediaMetaData.Instance.SettingsManager.UseSlideMaster);
			return previewGroup;
		}

		public void Output(Theme selectedTheme)
		{
			Logos = GetLogos();
			ColumnWidths = GetColumnInfo().OrderBy(ci => ci.Index).Select(ci => ci.Width).ToArray();
			PopulateReplacementsList();
			RegularMediaSchedulePowerPointHelper.Instance.AppendOptions(new[] { this }, selectedTheme, MediaMetaData.Instance.SettingsManager.UseSlideMaster);
		}

		internal abstract class OutputColumnInfo
		{
			public abstract string HeaderCaption { get; }
			public int Index { get; set; }
			public float Width { get; set; }

			public abstract string GetValue(OptionProgram program);
		}

		internal class StationColumnInfo : OutputColumnInfo
		{
			public override string HeaderCaption
			{
				get { return "Station"; }
			}

			public override string GetValue(OptionProgram program)
			{
				return program.Station;
			}
		}

		internal class ProgramColumnInfo : OutputColumnInfo
		{
			public override string HeaderCaption
			{
				get { return "Program"; }
			}

			public override string GetValue(OptionProgram program)
			{
				return program.Name;
			}
		}

		internal class DayColumnInfo : OutputColumnInfo
		{
			public override string HeaderCaption
			{
				get { return "Day"; }
			}

			public override string GetValue(OptionProgram program)
			{
				return program.Day;
			}
		}

		internal class TimeColumnInfo : OutputColumnInfo
		{
			public override string HeaderCaption
			{
				get { return "Time"; }
			}

			public override string GetValue(OptionProgram program)
			{
				return program.Time;
			}
		}

		internal class SpotsColumnInfo : OutputColumnInfo
		{
			private readonly SpotType _spotType;
			public override string HeaderCaption
			{
				get
				{
					switch (_spotType)
					{
						case SpotType.Week:
							return String.Format("Weekly{0}Spots", (char)13);
						case SpotType.Month:
							return String.Format("Monthly{0}Spots", (char)13);
						case SpotType.Total:
							return String.Format("Total{0}Spots", (char)13);
						default:
							return String.Empty;
					}
				}
			}

			public SpotsColumnInfo(SpotType spotType)
			{
				_spotType = spotType;
			}

			public override string GetValue(OptionProgram program)
			{
				return program.Spot.HasValue ? String.Format("{0}{1}", program.Spot.Value.ToString("#,###"), program.Parent.ShowSpotsX ? "x" : String.Empty) : "-";
			}
		}

		internal class RateColumnInfo : OutputColumnInfo
		{
			public override string HeaderCaption
			{
				get { return "Rate"; }
			}

			public override string GetValue(OptionProgram program)
			{
				return program.Rate.HasValue ? program.Rate.Value.ToString(program.Parent.UseDecimalRates ? "$#,##0.00" : "$#,##0") : String.Empty;
			}
		}

		internal class LengthColumnInfo : OutputColumnInfo
		{
			public override string HeaderCaption
			{
				get { return "Length"; }
			}

			public override string GetValue(OptionProgram program)
			{
				return program.Length;
			}
		}

		internal class CostColumnInfo : OutputColumnInfo
		{
			public override string HeaderCaption
			{
				get { return "Cost"; }
			}

			public override string GetValue(OptionProgram program)
			{
				return program.Cost.HasValue ? program.Cost.Value.ToString("$#,###") : String.Empty;
			}
		}
		#endregion
	}
}
