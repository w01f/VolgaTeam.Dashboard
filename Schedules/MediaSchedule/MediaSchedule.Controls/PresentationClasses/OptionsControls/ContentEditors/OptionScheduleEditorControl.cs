using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Media.Configuration;
using Asa.Business.Media.Entities.NonPersistent.Common;
using Asa.Business.Media.Entities.NonPersistent.Option;
using Asa.Business.Media.Enums;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.Images;
using Asa.Common.Core.Objects.Output;
using Asa.Common.Core.Objects.Themes;
using Asa.Common.GUI.Common;
using Asa.Common.GUI.ImageGallery;
using Asa.Common.GUI.Preview;
using Asa.Media.Controls.BusinessClasses.Managers;
using Asa.Media.Controls.InteropClasses;
using Asa.Media.Controls.PresentationClasses.OptionsControls.Output;
using DevExpress.Utils;
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

namespace Asa.Media.Controls.PresentationClasses.OptionsControls.ContentEditors
{
	[ToolboxItem(false)]
	//public partial class OptionsControl : UserControl, IOptionsSlideControl
	public sealed partial class OptionScheduleEditorControl :
		XtraTabPage,
		IOptionSetEditorControl,
		IOptionSetCollectionEditorControl,
		IOutputItem,
		IOptionsSlideData
	{
		private bool _allowToSave;
		private GridDragDropHelper _dragDropHelper;
		private OptionSet _data;

		public OptionEditorType EditorType => OptionEditorType.Schedule;
		public string CollectionTitle => "Program";
		public string CollectionItemTitle => "Program";
		public bool AllowToAddItem => true;
		public bool AllowToDeleteItem => _data != null && _data.Programs.Any();

		public event EventHandler<EventArgs> DataChanged;

		public OptionScheduleEditorControl()
		{
			InitializeComponent();
			pnNoPrograms.Dock = DockStyle.Fill;
			gridControl.Dock = DockStyle.Fill;
			Text = MediaMetaData.Instance.DataTypeString;
		}

		#region Methods
		public void InitControls()
		{
			repositoryItemComboBoxDays.Items.Clear();
			repositoryItemComboBoxDays.Items.AddRange(MediaMetaData.Instance.ListManager.Days);
			repositoryItemComboBoxLengths.Items.Clear();
			repositoryItemComboBoxLengths.Items.AddRange(MediaMetaData.Instance.ListManager.Lengths);
			repositoryItemComboBoxTimes.Items.Clear();
			repositoryItemComboBoxTimes.Items.AddRange(MediaMetaData.Instance.ListManager.Times);

			pbNoPrograms.Image = BusinessObjects.Instance.ImageResourcesManager.OptionsNoProgramsLogo ?? pbNoPrograms.Image;
		}

		public void LoadData(OptionSet data)
		{
			_allowToSave = false;
			_data = data;
			repositoryItemComboBoxStations.Items.Clear();
			repositoryItemComboBoxStations.Items.AddRange(
				_data.Parent.ScheduleSettings.Stations
					.Where(station => station.Available)
					.Select(station => station.Name)
					.ToArray());
			gridControl.DataSource = _data.Programs;
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

		public void Release()
		{
			gridControlProgramSource.DataSource = null;
			gridControl.DataSource = null;
			_dragDropHelper.AfterDrop -= OnGridControlAfterDrop;
			DataChanged = null;
			_data = null;
		}

		public void AddItem()
		{
			_data.AddProgram();
			gridControl.DataSource = _data.Programs;
			advBandedGridView.RefreshData();
			UpdateProgramSplash();
			InitDargDropHelper();
			DataChanged?.Invoke(this, EventArgs.Empty);
		}

		public void DeleteItem()
		{
			var selectedProgram = advBandedGridView.GetFocusedRow() as OptionProgram;
			if (selectedProgram == null) return;
			var selectedProgramIndex = selectedProgram.Index;
			if (PopupMessageHelper.Instance.ShowWarningQuestion(String.Format("Delete Line ID {0}?", selectedProgramIndex)) != DialogResult.Yes) return;
			_data.DeleteProgram(advBandedGridView.GetDataSourceRowIndex(advBandedGridView.FocusedRowHandle));
			gridControl.DataSource = _data.Programs;
			advBandedGridView.RefreshData();
			UpdateProgramSplash();
			DataChanged?.Invoke(this, EventArgs.Empty);
		}

		public void CloneItem(int sourceIndex)
		{
			_data.CloneProgram(sourceIndex);
			gridControl.DataSource = _data.Programs;
			advBandedGridView.RefreshData();
			if (_data.CloneLineToTheEnd)
				advBandedGridView.FocusedRowHandle = advBandedGridView.RowCount - 1;
			else
				advBandedGridView.FocusedRowHandle = sourceIndex + 1;
			UpdateProgramSplash();
			DataChanged?.Invoke(this, EventArgs.Empty);
		}

		private void UpdateProgramSplash()
		{
			if (_data.Programs.Any())
				gridControl.BringToFront();
			else
				pnNoPrograms.BringToFront();
		}

		public void UpdateView()
		{
			_allowToSave = false;
			gridBandId.Visible = _data.ShowLineId;
			gridBandLogo.Visible = _data.ShowLogo;
			if (_data.ShowStation ||
				_data.ShowProgram ||
				_data.ShowDay ||
				_data.ShowTime ||
				_data.ShowSpots ||
				_data.ShowRate ||
				_data.ShowLenght ||
				_data.ShowCost)
			{
				gridBandOtherColumns.Visible = true;

				var positionedColumns = new SortedDictionary<int, BandedGridColumn>();
				bandedGridColumnStation.Visible = false;
				if (_data.ShowStation)
					positionedColumns.Add(
						_data.DefaultColumnPositions ?
							OptionSet.DefaultPositionStation :
							(!positionedColumns.ContainsKey(_data.PositionStation) ?
								_data.PositionStation :
								(positionedColumns.Keys.Max() + 1)),
						bandedGridColumnStation);
				bandedGridColumnName.Visible = false;
				if (_data.ShowProgram)
					positionedColumns.Add(
						_data.DefaultColumnPositions ?
							OptionSet.DefaultPositionProgram :
							(!positionedColumns.ContainsKey(_data.PositionProgram) ?
								_data.PositionProgram :
								(positionedColumns.Keys.Max() + 1)),
						bandedGridColumnName);
				bandedGridColumnDay.Visible = false;
				if (_data.ShowDay)
					positionedColumns.Add(
						_data.DefaultColumnPositions ?
							OptionSet.DefaultPositionDay :
							(!positionedColumns.ContainsKey(_data.PositionDay) ?
								_data.PositionDay :
								(positionedColumns.Keys.Max() + 1)),
						bandedGridColumnDay);
				bandedGridColumnTime.Visible = false;
				if (_data.ShowTime)
					positionedColumns.Add(
						_data.DefaultColumnPositions ?
							OptionSet.DefaultPositionTime :
							(!positionedColumns.ContainsKey(_data.PositionTime) ?
								_data.PositionTime :
								(positionedColumns.Keys.Max() + 1)),
						bandedGridColumnTime);
				bandedGridColumnLength.Visible = false;
				if (_data.ShowLenght)
					positionedColumns.Add(
						_data.DefaultColumnPositions ?
							OptionSet.DefaultPositionLenght :
							(!positionedColumns.ContainsKey(_data.PositionLenght) ?
								_data.PositionLenght :
								(positionedColumns.Keys.Max() + 1)),
						bandedGridColumnLength);
				bandedGridColumnSpots.Visible = false;
				if (_data.ShowSpots)
					positionedColumns.Add(
						_data.DefaultColumnPositions ?
							OptionSet.DefaultPositionSpots :
							(!positionedColumns.ContainsKey(_data.PositionSpots) ?
								_data.PositionSpots :
								(positionedColumns.Keys.Max() + 1)),
						bandedGridColumnSpots);
				bandedGridColumnRate.Visible = false;
				if (_data.ShowRate)
					positionedColumns.Add(
						_data.DefaultColumnPositions ?
							OptionSet.DefaultPositionRate :
							(!positionedColumns.ContainsKey(_data.PositionRate) ?
								_data.PositionRate :
								(positionedColumns.Keys.Max() + 1)),
						bandedGridColumnRate);
				bandedGridColumnCost.Visible = false;
				if (_data.ShowCost)
					positionedColumns.Add(
						_data.DefaultColumnPositions ?
							OptionSet.DefaultPositionCost :
							(!positionedColumns.ContainsKey(_data.PositionCost) ?
								_data.PositionCost :
								(positionedColumns.Keys.Max() + 1)),
						bandedGridColumnCost);
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

			switch (_data.SpotType)
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

			if (_data.UseDecimalRates)
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
			if (_data.DefaultColumnPositions) return;
			var columnPosition = 0;
			var columns = gridBandOtherColumns.Columns.OfType<BandedGridColumn>().OrderBy(c => c.ColIndex).ToList();
			var visibleColumns = columns.Where(c => c.ColIndex >= 0).ToList();
			var hiddenPosition = visibleColumns.Count;
			foreach (var column in columns)
			{
				if (column == bandedGridColumnStation)
					_data.PositionStation = column.ColIndex >= 0 ? columnPosition++ : hiddenPosition++;
				else if (column == bandedGridColumnName)
					_data.PositionProgram = column.ColIndex >= 0 ? columnPosition++ : hiddenPosition++;
				else if (column == bandedGridColumnDay)
					_data.PositionDay = column.ColIndex >= 0 ? columnPosition++ : hiddenPosition++;
				else if (column == bandedGridColumnTime)
					_data.PositionTime = column.ColIndex >= 0 ? columnPosition++ : hiddenPosition++;
				else if (column == bandedGridColumnLength)
					_data.PositionLenght = column.ColIndex >= 0 ? columnPosition++ : hiddenPosition++;
				else if (column == bandedGridColumnSpots)
					_data.PositionSpots = column.ColIndex >= 0 ? columnPosition++ : hiddenPosition++;
				else if (column == bandedGridColumnRate)
					_data.PositionRate = column.ColIndex >= 0 ? columnPosition++ : hiddenPosition++;
				else if (column == bandedGridColumnCost)
					_data.PositionCost = column.ColIndex >= 0 ? columnPosition++ : hiddenPosition++;
			}
		}

		private IEnumerable<DXMenuItem> GetContextMenuItems(ColumnView targetView, int targetRowHandle)
		{
			var items = new List<DXMenuItem>();
			var sourceIndex = targetView.GetDataSourceRowIndex(targetRowHandle);
			items.Add(new DXMenuItem("Clone this Line", (o, args) => CloneItem(sourceIndex)));
			items.Add(new DXMenuItem("Delete this Line", (o, e) => DeleteItem()));
			return items;
		}

		private void InitDargDropHelper()
		{
			if (_dragDropHelper != null || !_data.Programs.Any()) return;
			_dragDropHelper = new GridDragDropHelper(advBandedGridView, true, handledColumns: new[] { bandedGridColumnIndex, bandedGridColumnLogo });
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
			var optionsProgram = advBandedGridView.GetRow(e.RowHandle) as OptionProgram;
			if (e.Column != bandedGridColumnName || optionsProgram == null) return;
			gridColumnProgramSourceStation.Visible = String.IsNullOrEmpty(optionsProgram.Station);
			gridColumnProgramSourceDaypart.Visible = true;
			gridColumnProgramSourceName.Caption = "All Programming";
			var dataSource = new List<SourceProgram>();
			dataSource.AddRange(!String.IsNullOrEmpty(optionsProgram.Station) ?
				MediaMetaData.Instance.ListManager.SourcePrograms.Where(x => (x.Station.Equals(optionsProgram.Station) || String.IsNullOrEmpty(optionsProgram.Station))) :
				MediaMetaData.Instance.ListManager.SourcePrograms.OrderBy(sp => sp.Daypart));
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

		private void OnGridViewRowCellClick(object sender, RowCellClickEventArgs e)
		{
			if (e.Column != bandedGridColumnLogo) return;
			if (e.Clicks < 2) return;
			var selectedProgram = advBandedGridView.GetFocusedRow() as OptionProgram;
			if (selectedProgram == null) return;
			using (var form = new FormImageGallery(MediaMetaData.Instance.ListManager.Images))
			{
				if (form.ShowDialog() != DialogResult.OK) return;
				if (form.SelectedImageSource == null) return;
				selectedProgram.Logo = form.SelectedImageSource.Clone<ImageSource, ImageSource>();
				_data.UpdateLogo();
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
			foreach (var menuItem in GetContextMenuItems(advBandedGridView, e.HitInfo.RowHandle))
				e.Menu.Items.Add(menuItem);
		}

		private void OnGridViewColumnPositionChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			_data.DefaultColumnPositions = false;
			UpdateColumnPositions();
			DataChanged?.Invoke(this, EventArgs.Empty);
		}

		private void OnGridViewDragObjectOver(object sender, DragObjectOverEventArgs e)
		{
			var draggedColumn = e.DragObject as BandedGridColumn;
			var targetPositionInfo = e.DropInfo as AdvBandedColumnPositionInfo;
			if (draggedColumn != null && targetPositionInfo != null)
				e.DropInfo.Valid = draggedColumn.RowIndex == targetPositionInfo.RowIndex;
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
			_data.ChangeProgramPosition(sourceRow, targetRow);
			gridControl.DataSource = _data.Programs;
			advBandedGridView.RefreshData();
			if (advBandedGridView.RowCount > 0)
				advBandedGridView.FocusedRowHandle = targetRow;
			DataChanged?.Invoke(this, EventArgs.Empty);
		}

		private void OnPropertiesMenuBeforeShow(object sender, BeforeShowMenuEventArgs e)
		{
			var items = GetContextMenuItems(advBandedGridView, advBandedGridView.FocusedRowHandle);
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
			var selectedProgram = advBandedGridView.GetFocusedRow() as OptionProgram;
			if (programSource == null || selectedProgram == null) return;
			advBandedGridView.CellValueChanged -= OnGridViewCellValueChanged;
			e.Value = programSource.Name;
			if (String.IsNullOrEmpty(selectedProgram.Day))
				selectedProgram.Day = programSource.Day;
			selectedProgram.Time = programSource.Time;
			if (String.IsNullOrEmpty(selectedProgram.Length))
				selectedProgram.Length = MediaMetaData.Instance.ListManager.Lengths.FirstOrDefault();
			advBandedGridView.CellValueChanged += OnGridViewCellValueChanged;
			e.AcceptValue = true;
		}

		private void OnRepositoryItemPopupContainerEditProgramClosed(object sender, ClosedEventArgs e)
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
		public SlideType SlideType => MediaMetaData.Instance.DataType == MediaDataType.TVSchedule ?
			SlideType.TVOptionsPrograms :
			SlideType.RadioOptionsPrograms;
		private Theme SelectedTheme => BusinessObjects.Instance.ThemeManager.GetThemes(SlideType).FirstOrDefault(t => t.Name.Equals(MediaMetaData.Instance.SettingsManager.GetSelectedThemeName(SlideType)) || String.IsNullOrEmpty(MediaMetaData.Instance.SettingsManager.GetSelectedThemeName(SlideType)));
		public string TemplateFilePath => BusinessObjects.Instance.OutputManager.GetOptionsItemFile(
			MediaMetaData.Instance.SettingsManager.SelectedColor ?? BusinessObjects.Instance.OutputManager.ScheduleColors.Items.Select(ci => ci.Name).FirstOrDefault(),
			_data.ShowLogo || _data.DigitalInfo.ShowLogo);
		public string[][] Logos { get; set; }
		public float[] ColumnWidths { get; set; }
		public ContractSettings ContractSettings => _data.ContractSettings;
		public List<Dictionary<string, string>> ReplacementsList { get; private set; }
		private static int ProgramsPerSlide => 10;

		public IList<OutputConfiguration> GetOutputConfigurations()
		{
			var outputConfigurations = new List<OutputConfiguration>();
			if (_data.Programs.Any() && (_data.ShowLineId || _data.ShowStation || _data.ShowProgram || _data.ShowDay || _data.ShowTime || _data.ShowSpots || _data.ShowRate || _data.ShowLenght || _data.ShowCost))
			{
				outputConfigurations.Add(new OutputConfiguration(OptionSetOutputType.Program));
				if (_data.DigitalInfo.Records.Any())
					outputConfigurations.Add(new OutputConfiguration(OptionSetOutputType.ProgramAndDigital));
			}
			return outputConfigurations;
		}

		private string[][] GetLogos(bool includeDigital)
		{
			var logos = new List<string[]>();
			var logosOnSlide = new List<string>();

			var programsCount = _data.Programs.Count;
			var digitalsCount = _data.DigitalInfo.Records.Count;
			var recordsCount = includeDigital ? (programsCount + digitalsCount) : programsCount;
			var digitalStartIndex = programsCount;

			for (var i = 0; i < recordsCount; i += ProgramsPerSlide)
			{
				logosOnSlide.Clear();
				for (int j = 0; j < ProgramsPerSlide; j++)
				{
					var fileName = String.Empty;
					if ((i + j) < programsCount)
					{
						if (_data.ShowLogo)
						{
							var progam = _data.Programs[i + j];
							if (progam.Logo != null && progam.Logo.ContainsData)
							{
								fileName = Path.GetTempFileName();
								progam.Logo.SmallImage.Save(fileName);
							}
						}
					}
					else if (includeDigital && ((i + j) - digitalStartIndex) < digitalsCount)
					{
						if (_data.DigitalInfo.ShowLogo)
						{
							var digitalInfoRecord = _data.DigitalInfo.Records[(i + j) - digitalStartIndex];
							if (digitalInfoRecord.Logo != null && digitalInfoRecord.Logo.ContainsData)
							{
								fileName = Path.GetTempFileName();
								digitalInfoRecord.Logo.SmallImage.Save(fileName);
							}
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

			if (_data.ShowStation)
			{
				var columnInfo = new StationColumnInfo
				{
					Width = widths["station"],
					Index = (_data.DefaultColumnPositions ? OptionSet.DefaultPositionStation : _data.PositionStation) + 1
				};
				columnInfoList.Add(columnInfo);
			}
			if (_data.ShowProgram)
			{
				var columnInfo = new ProgramColumnInfo
				{
					Width = widths["program"],
					Index = (_data.DefaultColumnPositions ? OptionSet.DefaultPositionProgram : _data.PositionProgram) + 1
				};
				columnInfoList.Add(columnInfo);
			}
			if (_data.ShowDay)
			{
				var columnInfo = new DayColumnInfo
				{
					Width = widths["day"],
					Index = (_data.DefaultColumnPositions ? OptionSet.DefaultPositionDay : _data.PositionDay) + 1
				};
				columnInfoList.Add(columnInfo);
			}
			if (_data.ShowTime)
			{
				var columnInfo = new TimeColumnInfo
				{
					Width = widths["time"],
					Index = (_data.DefaultColumnPositions ? OptionSet.DefaultPositionTime : _data.PositionTime) + 1
				};
				columnInfoList.Add(columnInfo);
			}
			if (_data.ShowSpots)
			{
				var columnInfo = new SpotsColumnInfo(_data.SpotType)
				{
					Width = widths["spots"],
					Index = (_data.DefaultColumnPositions ? OptionSet.DefaultPositionSpots : _data.PositionSpots) + 1
				};
				columnInfoList.Add(columnInfo);
			}
			if (_data.ShowRate)
			{
				var columnInfo = new RateColumnInfo
				{
					Width = widths["rate"],
					Index = (_data.DefaultColumnPositions ? OptionSet.DefaultPositionRate : _data.PositionRate) + 1
				};
				columnInfoList.Add(columnInfo);
			}
			if (_data.ShowLenght)
			{
				var columnInfo = new LengthColumnInfo
				{
					Width = widths["length"],
					Index = (_data.DefaultColumnPositions ? OptionSet.DefaultPositionLenght : _data.PositionLenght) + 1
				};
				columnInfoList.Add(columnInfo);
			}
			if (_data.ShowCost)
			{
				var columnInfo = new CostColumnInfo
				{
					Width = widths["cost"],
					Index = (_data.DefaultColumnPositions ? OptionSet.DefaultPositionCost : _data.PositionCost) + 1
				};
				columnInfoList.Add(columnInfo);
			}
			return columnInfoList;
		}

		private Dictionary<string, float> GetColumnWidths()
		{
			var widths = new Dictionary<string, float>();
			var widthsPath = BusinessObjects.Instance.OutputManager.GetOptionsColumnsWidthFile();
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

		private void PopulateReplacementsList(bool includeDigital)
		{
			var key = string.Empty;
			var value = string.Empty;
			var temp = new List<string>();
			ReplacementsList = new List<Dictionary<string, string>>();

			var programsCount = _data.Programs.Count;
			var digitalsCount = _data.DigitalInfo.Records.Count;
			var recordsCount = includeDigital ? (programsCount + digitalsCount) : programsCount;
			var digitalStartIndex = programsCount;

			for (var i = 0; i < recordsCount; i += ProgramsPerSlide)
			{
				var pageDictionary = new Dictionary<string, string>();
				key = "Flightdates";
				value = _data.Parent.ScheduleSettings.FlightDates;
				if (!pageDictionary.Keys.Contains(key))
					pageDictionary.Add(key, value);
				key = "Advertiser  -  Decisionmaker";
				value = String.Format("{0}  -  {1}", _data.Parent.ScheduleSettings.BusinessName,
					_data.Parent.ScheduleSettings.DecisionMaker);
				if (!pageDictionary.Keys.Contains(key))
					pageDictionary.Add(key, value);

				key = "tallyinfo";
				if (_data.ShowTotalSpots || _data.ShowTotalCost || _data.ShowAverageRate)
				{
					var spotPrefix = String.Empty;
					switch (_data.SpotType)
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
					if (_data.ShowTotalSpots)
						temp.Add(String.Format("{0} Spots: {1}", spotPrefix,
							String.Format("{0}{1}", _data.TotalSpots.ToString("#,###"), _data.ShowSpotsX ? "x" : String.Empty)));
					if (_data.ShowTotalCost)
						temp.Add(String.Format("{0} Cost: {1}", spotPrefix,
							_data.TotalCost.ToString(_data.UseDecimalRates ? "$#,##0.00" : "$#,##0")));
					if (_data.ShowAverageRate)
						temp.Add(String.Format("Average Rate: {0}", _data.AvgRate.ToString(_data.UseDecimalRates ? "$#,##0.00" : "$#,##0")));
					var scheduleSummary = String.Join("     ", temp);
					if (includeDigital)
					{
						temp.Clear();
						if (_data.DigitalInfo.ShowMonthlyInvestemt && _data.DigitalInfo.MonthlyInvestment.HasValue)
							temp.Add(String.Format("Monthly Digital Investment: {0}", _data.DigitalInfo.MonthlyInvestment.Value.ToString("$#,###.00")));
						if (_data.DigitalInfo.ShowTotalInvestemt && _data.DigitalInfo.TotalInvestment.HasValue)
							temp.Add(String.Format("Total Digital Investment: {0}", _data.DigitalInfo.TotalInvestment.Value.ToString("$#,###.00")));
						var digitalSummary = String.Join("     ", temp);
						if (!String.IsNullOrEmpty(digitalSummary))
							value = String.Join(String.Format("{0}", (char)13), scheduleSummary, digitalSummary);
						else
							value = scheduleSummary;
					}
					else
						value = scheduleSummary;
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
					value = dynamicColumnInfoList.Count > columnIndex
						? dynamicColumnInfoList[columnIndex].HeaderCaption
						: "Delete Column";
					if (!pageDictionary.Keys.Contains(key))
						pageDictionary.Add(key, value);
					columnIndex++;
				}

				for (var j = 0; j < ProgramsPerSlide; j++)
				{
					key = (j + 1).ToString("00");
					value = _data.ShowLineId ? (j + 1).ToString("00") : "Delete Column";
					if (!pageDictionary.Keys.Contains(key))
						pageDictionary.Add(key, value);

					if ((i + j) < programsCount)
					{
						var progam = _data.Programs[i + j];
						columnIndex = 0;
						foreach (var columValuesTag in columValuesTags)
						{
							key = String.Format(columValuesTag, j + 1);
							value = dynamicColumnInfoList.Count > columnIndex
								? dynamicColumnInfoList[columnIndex].GetValue(progam)
								: "Delete Column";
							if (!pageDictionary.Keys.Contains(key))
								pageDictionary.Add(key, value);
							columnIndex++;
						}
					}
					else if (includeDigital && ((i + j) - digitalStartIndex) < digitalsCount)
					{
						var digitalInfoRecord = _data.DigitalInfo.Records[(i + j) - digitalStartIndex];
						columnIndex = 0;
						foreach (var columValuesTag in columValuesTags)
						{
							key = String.Format(columValuesTag, j + 1);
							if (dynamicColumnInfoList.Count > columnIndex)
							{
								if (columnIndex == 0)
								{
									temp.Clear();
									if (_data.DigitalInfo.ShowCategory)
										temp.Add(digitalInfoRecord.Category);
									if (_data.DigitalInfo.ShowSubCategory)
										temp.Add(digitalInfoRecord.SubCategory);
									if (_data.DigitalInfo.ShowProduct)
										temp.Add(digitalInfoRecord.Name);
									if (_data.DigitalInfo.ShowInfo)
										temp.Add(digitalInfoRecord.Info);
									value = String.Format("    {0}", String.Join("    ", temp));
								}
								else
									value = "Merge";
							}
							else
								value = "Delete Column";
							if (!pageDictionary.Keys.Contains(key))
								pageDictionary.Add(key, value);
							columnIndex++;
						}
					}
					else
					{
						key = String.Format(columValuesTags.First(), j + 1);
						value = "Delete Row";
						if (!pageDictionary.Keys.Contains(key))
							pageDictionary.Add(key, value);
					}
				}
				ReplacementsList.Add(pageDictionary);
			}
		}

		public PreviewGroup GeneratePreview(bool includeDigital)
		{
			Logos = GetLogos(includeDigital);
			ColumnWidths = GetColumnInfo().OrderBy(ci => ci.Index).Select(ci => ci.Width).ToArray();
			PopulateReplacementsList(includeDigital);
			var previewGroup = new PreviewGroup
			{
				Name = String.Format("{0} ({1})", _data.Name, includeDigital ? String.Format("{0} + Digital", MediaMetaData.Instance.DataTypeString) : MediaMetaData.Instance.DataTypeString),
				PresentationSourcePath = Path.Combine(Common.Core.Configuration.ResourceManager.Instance.TempFolder.LocalPath, Path.GetFileName(Path.GetTempFileName()))
			};
			RegularMediaSchedulePowerPointHelper.Instance.PrepareOptionsEmail(previewGroup.PresentationSourcePath, new[] { this }, SelectedTheme, MediaMetaData.Instance.SettingsManager.UseSlideMaster);
			return previewGroup;
		}

		public void GenerateOutput(bool includeDigital)
		{
			Logos = GetLogos(includeDigital);
			ColumnWidths = GetColumnInfo().OrderBy(ci => ci.Index).Select(ci => ci.Width).ToArray();
			PopulateReplacementsList(includeDigital);
			RegularMediaSchedulePowerPointHelper.Instance.AppendOptions(new[] { this }, SelectedTheme, MediaMetaData.Instance.SettingsManager.UseSlideMaster);
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
			public override string HeaderCaption => "Station";

			public override string GetValue(OptionProgram program)
			{
				return program.Station;
			}
		}

		internal class ProgramColumnInfo : OutputColumnInfo
		{
			public override string HeaderCaption => "Program";

			public override string GetValue(OptionProgram program)
			{
				return program.Name;
			}
		}

		internal class DayColumnInfo : OutputColumnInfo
		{
			public override string HeaderCaption => "Day";

			public override string GetValue(OptionProgram program)
			{
				return program.Day;
			}
		}

		internal class TimeColumnInfo : OutputColumnInfo
		{
			public override string HeaderCaption => "Time";

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
			public override string HeaderCaption => "Rate";

			public override string GetValue(OptionProgram program)
			{
				return program.Rate.HasValue ? program.Rate.Value.ToString(program.Parent.UseDecimalRates ? "$#,##0.00" : "$#,##0") : String.Empty;
			}
		}

		internal class LengthColumnInfo : OutputColumnInfo
		{
			public override string HeaderCaption => "Length";

			public override string GetValue(OptionProgram program)
			{
				return program.Length;
			}
		}

		internal class CostColumnInfo : OutputColumnInfo
		{
			public override string HeaderCaption => "Cost";

			public override string GetValue(OptionProgram program)
			{
				return program.Cost.HasValue ? program.Cost.Value.ToString("$#,###") : String.Empty;
			}
		}
		#endregion
	}
}
