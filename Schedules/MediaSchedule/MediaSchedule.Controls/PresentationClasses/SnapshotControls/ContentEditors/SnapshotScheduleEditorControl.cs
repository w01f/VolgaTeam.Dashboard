using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Media.Configuration;
using Asa.Business.Media.Entities.NonPersistent.Common;
using Asa.Business.Media.Entities.NonPersistent.Snapshot;
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
using Asa.Media.Controls.PresentationClasses.SnapshotControls.Output;
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
using DevExpress.XtraTab;

namespace Asa.Media.Controls.PresentationClasses.SnapshotControls.ContentEditors
{
	[ToolboxItem(false)]
	//public partial class SnapshotScheduleEditorControl : UserControl
	public sealed partial class SnapshotScheduleEditorControl : XtraTabPage,
		ISnapshotEditorControl,
		ISnapshotCollectionEditorControl,
		IOutputItem,
		ISnapshotSlideData
	{
		private GridDragDropHelper _dragDropHelper;
		private Snapshot _data;

		public SnapshotEditorType EditorType => SnapshotEditorType.Schedule;
		public string CollectionTitle => "Program";
		public string CollectionItemTitle => "Program";
		public bool AllowToAddItem => true;
		public bool AllowToDeleteItem => _data != null && _data.Programs.Any();

		public event EventHandler<EventArgs> DataChanged;

		public SnapshotScheduleEditorControl()
		{
			InitializeComponent();
			Text = MediaMetaData.Instance.DataTypeString;
			pnNoPrograms.Dock = DockStyle.Fill;
			gridControl.Dock = DockStyle.Fill;
			if ((CreateGraphics()).DpiX > 96)
			{
				laProgramSourceInfo.Font = new Font(laProgramSourceInfo.Font.FontFamily, laProgramSourceInfo.Font.Size - 2, laProgramSourceInfo.Font.Style);
			}
		}

		#region Methods
		public void InitControls()
		{
			repositoryItemComboBoxLengths.Items.Clear();
			repositoryItemComboBoxLengths.Items.AddRange(MediaMetaData.Instance.ListManager.Lengths);
			repositoryItemComboBoxTimes.Items.Clear();
			repositoryItemComboBoxTimes.Items.AddRange(MediaMetaData.Instance.ListManager.Times);

			pbNoPrograms.Image = BusinessObjects.Instance.ImageResourcesManager.SnapshotsNoProgramsLogo ?? pbNoPrograms.Image;
		}

		public void LoadData(Snapshot data)
		{
			_data = data;

			repositoryItemComboBoxStations.Items.Clear();
			repositoryItemComboBoxStations.Items.AddRange(
				_data.Parent.ScheduleSettings.Stations
					.Where(station => station.Available)
					.Select(station => station.Name)
					.ToArray());
			repositoryItemComboBoxDayparts.Items.Clear();
			repositoryItemComboBoxDayparts.Items.AddRange(
				_data.Parent.ScheduleSettings.Dayparts
					.Where(daypart => daypart.Available)
					.Select(daypart => daypart.Code).ToArray());

			gridControl.DataSource = _data.Programs;
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
			var selectedProgram = advBandedGridView.GetFocusedRow() as SnapshotProgram;
			if (selectedProgram == null) return;
			var selectedProgramIndex = selectedProgram.Index;
			if (PopupMessageHelper.Instance.ShowWarningQuestion(
				String.Format("Delete Line ID {0}?", selectedProgramIndex)) != DialogResult.Yes) return;
			_data.DeleteProgram(advBandedGridView.GetDataSourceRowIndex(advBandedGridView.FocusedRowHandle));
			gridControl.DataSource = _data.Programs;
			advBandedGridView.RefreshData();
			UpdateProgramSplash();
			DataChanged?.Invoke(this, EventArgs.Empty);
		}

		public void CloneItem(int sourceIndex, bool fullClone)
		{
			_data.CloneProgram(sourceIndex, fullClone);
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
			gridBandId.Visible = _data.ShowLineId;
			gridBandLogo.Visible = _data.ShowLogo;
			gridBandRate.Visible = _data.ShowRate;

			gridBandProgram.Visible = _data.ShowProgram || _data.ShowLenght || _data.ShowTime;
			bandedGridColumnName.Visible = _data.ShowProgram;
			bandedGridColumnLength.Visible = _data.ShowLenght;
			bandedGridColumnTime.Visible = _data.ShowTime;
			var secondRowIndex = 0;
			if (_data.ShowProgram)
			{
				advBandedGridView.SetColumnPosition(bandedGridColumnName, 0, 0);
				secondRowIndex++;
			}
			if (_data.ShowTime)
				advBandedGridView.SetColumnPosition(bandedGridColumnTime, secondRowIndex, 0);
			if (_data.ShowLenght)
				advBandedGridView.SetColumnPosition(bandedGridColumnLength, secondRowIndex, 1);

			gridBandStation.Visible = _data.ShowStation || _data.ShowDaypart;
			bandedGridColumnStation.Visible = _data.ShowStation;
			bandedGridColumnDaypart.Visible = _data.ShowDaypart;
			if (_data.ShowStation)
				advBandedGridView.SetColumnPosition(bandedGridColumnStation, 0, 0);
			if (_data.ShowDaypart)
				advBandedGridView.SetColumnPosition(bandedGridColumnDaypart, 1, 0);

			gridBandTotals.Visible = _data.ShowWeeklySpots || _data.ShowWeeklyCost;
			bandedGridColumnTotalSpots.Visible = _data.ShowWeeklySpots;
			bandedGridColumnCost.Visible = _data.ShowWeeklyCost;
			if (_data.ShowWeeklySpots)
				advBandedGridView.SetColumnPosition(bandedGridColumnTotalSpots, 0, 0);
			if (_data.ShowWeeklyCost)
				advBandedGridView.SetColumnPosition(bandedGridColumnCost, 0, 1);

			if (_data.Parent.ScheduleSettings.MondayBased)
			{
				advBandedGridView.SetColumnPosition(bandedGridColumnMondaySpot, 0, 0);
				advBandedGridView.SetColumnPosition(bandedGridColumnSundaySpot, 0, 7);
			}
			else
			{
				advBandedGridView.SetColumnPosition(bandedGridColumnSundaySpot, 0, 0);
				advBandedGridView.SetColumnPosition(bandedGridColumnMondaySpot, 0, 7);
			}

			advBandedGridView.OptionsView.ShowFooter = _data.ShowTotalRow;

			if (_data.UseDecimalRates)
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
				bandedGridColumnCost.SummaryItem.DisplayFormat = @"{0:c0}";
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

		private IEnumerable<DXMenuItem> GetCellContextMenuItems(ColumnView targetView, GridColumn targetColumn, int targetRowHandle)
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
				items.Add(new DXMenuItem("Clone this Entire Line", (o, args) => CloneItem(sourceIndex, true)));
				items.Add(new DXMenuItem("Clone Just this Program", (o, args) => CloneItem(sourceIndex, false)));
				items.Add(new DXMenuItem("Delete this Line", (o, e) => DeleteItem()));
			}
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
			var snapshotProgram = advBandedGridView.GetRow(e.RowHandle) as SnapshotProgram;
			if (e.Column != bandedGridColumnName || snapshotProgram == null) return;
			gridColumnProgramSourceStation.Visible = String.IsNullOrEmpty(snapshotProgram.Station);
			gridColumnProgramSourceDaypart.Visible = String.IsNullOrEmpty(snapshotProgram.Daypart);
			var daypartFromList = MediaMetaData.Instance.ListManager.Dayparts.FirstOrDefault(x => x.Code.Equals(snapshotProgram.Daypart));
			gridColumnProgramSourceName.Caption = String.Format("Program ({0})", daypartFromList != null ? daypartFromList.Name : "All Programming");
			var dataSource = new List<SourceProgram>();
			if (!String.IsNullOrEmpty(snapshotProgram.Station) && (!String.IsNullOrEmpty(snapshotProgram.Daypart) || !_data.ShowDaypart))
				dataSource.AddRange(MediaMetaData.Instance.ListManager.SourcePrograms.Where(x => (x.Station.Equals(snapshotProgram.Station) || String.IsNullOrEmpty(snapshotProgram.Station)) && (!_data.ShowDaypart || (x.Daypart.Equals(snapshotProgram.Daypart) || String.IsNullOrEmpty(snapshotProgram.Daypart)))));
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
				CloseActiveEditorsonOutSideClick(sender, EventArgs.Empty);
		}

		private void OnGridViewCustomDrawFooter(object sender, RowObjectCustomDrawEventArgs e)
		{
			e.Painter.DrawObject(e.Info);
			var viewInfo = (AdvBandedGridViewInfo)((AdvBandedGridView)sender).GetViewInfo();
			var column = bandedGridColumnIndex;
			var title = "Totals: ";
			if (_data.ShowRate)
				column = bandedGridColumnRate;
			else if (_data.ShowLenght)
				column = bandedGridColumnLength;
			else if (_data.ShowTime)
				column = bandedGridColumnTime;
			else if (_data.ShowProgram)
				column = bandedGridColumnName;
			else if (_data.ShowStation)
				column = bandedGridColumnStation;
			else if (_data.ShowDaypart)
				column = bandedGridColumnDaypart;
			else if (_data.ShowLogo)
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
				_data.UpdateLogo();
				advBandedGridView.UpdateCurrentRow();
				DataChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		private void OnGridViewShownEditor(object sender, EventArgs e)
		{
			var view = (AdvBandedGridView)sender;
			var edit = view.ActiveEditor as TextEdit;
			if (edit == null) return;
			edit.Properties.BeforeShowMenu += OnPropertiesMenuBeforeShow;
		}

		private void OnGridViewPopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
		{
			if (!e.HitInfo.InRowCell) return;
			foreach (var menuItem in GetCellContextMenuItems(advBandedGridView, e.HitInfo.Column, e.HitInfo.RowHandle))
				e.Menu.Items.Add(menuItem);
		}

		private void OnGridControlAfterDrop(object sender, DragEventArgs e)
		{
			var grid = (GridControl)sender;
			var view = (GridView)grid.MainView;
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
			var items = GetCellContextMenuItems(advBandedGridView, advBandedGridView.FocusedColumn, advBandedGridView.FocusedRowHandle).ToList();
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
			e.Info = new ToolTipControlInfo(
				new CellToolTipInfo(hi.RowHandle, hi.Column, "cell"),
				"Double-Click to change the logo…")
			{
				ImmediateToolTip = true,
				Interval = 0
			};
		}
		#endregion

		#region Output
		public SlideType SlideType => MediaMetaData.Instance.DataType == MediaDataType.TVSchedule
			? SlideType.TVSnapshotPrograms
			: SlideType.RadioSnapshotPrograms;

		private Theme SelectedTheme
		{
			get
			{
				var selectedTheme = MediaMetaData.Instance.SettingsManager.GetSelectedThemeName(SlideType);
				return BusinessObjects.Instance.ThemeManager.GetThemes(SlideType).FirstOrDefault(t => t.Name.Equals(selectedTheme) || String.IsNullOrEmpty(selectedTheme));
			}
		}

		public string TemplateFilePath { get; private set; }

		public string TotalRowValue { get; private set; }

		public string[][] Logos { get; set; }

		public ContractSettings ContractSettings => _data.ContractSettings;

		public List<Dictionary<string, string>> ReplacementsList { get; private set; }

		private int GetProgramsPerSlide(bool includeDigital)
		{
			var programsCount = _data.Programs.Count;
			var digitalsCount = _data.DigitalInfo.Records.Count;
			var recordsCount = includeDigital ? (programsCount + digitalsCount) : programsCount;

			return recordsCount <= 10 ? recordsCount : 10;
		}

		public IList<OutputConfiguration> GetOutputConfigurations()
		{
			var outputConfigurations = new List<OutputConfiguration>();
			if (_data.Programs.Any() && (_data.ShowStation || _data.ShowProgram || _data.ShowTime || _data.ShowDaypart))
			{
				outputConfigurations.Add(new OutputConfiguration(
					SnapshotOutputType.Program,
					_data.Programs.Count / GetProgramsPerSlide(false) + (_data.Programs.Count % GetProgramsPerSlide(false) > 0 ? 1 : 0)));
				if (_data.DigitalInfo.Records.Any())
					outputConfigurations.Add(new OutputConfiguration(
						SnapshotOutputType.ProgramAndDigital,
						(_data.Programs.Count + _data.DigitalInfo.Records.Count) / GetProgramsPerSlide(true) +
							((_data.Programs.Count + _data.DigitalInfo.Records.Count) % GetProgramsPerSlide(true) > 0 ? 1 : 0)));
			}
			return outputConfigurations;
		}

		private void GetTemplatePath(bool includeDigital)
		{
			var slideSuffics = new List<string>();
			if (_data.ShowLenght)
				slideSuffics.Add("l");
			if (_data.ShowRate)
				slideSuffics.Add("r");
			if (_data.ShowWeeklySpots)
				slideSuffics.Add("s");
			if (_data.ShowWeeklyCost)
				slideSuffics.Add("c");
			if (!(_data.ShowLenght || _data.ShowRate || _data.ShowWeeklySpots || _data.ShowWeeklyCost))
				slideSuffics.Add("no_lrsc");
			TemplateFilePath = BusinessObjects.Instance.OutputManager.GetSnapshotItemFile(
				MediaMetaData.Instance.SettingsManager.SelectedColor ?? BusinessObjects.Instance.OutputManager.ScheduleColors.Items.Select(ci => ci.Name).FirstOrDefault(),
				_data.ShowLogo,
				GetProgramsPerSlide(includeDigital),
				String.Join("", slideSuffics));
		}

		private string[][] GetLogos(bool includeDigital)
		{
			var logos = new List<string[]>();
			if (!_data.ShowLogo) return logos.ToArray();
			var logosOnSlide = new List<string>();

			var programsCount = _data.Programs.Count;
			var digitalsCount = _data.DigitalInfo.Records.Count;
			var recordsCount = includeDigital ? (programsCount + digitalsCount) : programsCount;
			var digitalStartIndex = programsCount;

			for (var i = 0; i < recordsCount; i += GetProgramsPerSlide(includeDigital))
			{
				logosOnSlide.Clear();
				for (int j = 0; j < GetProgramsPerSlide(includeDigital); j++)
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

			for (var i = 0; i < recordsCount; i += GetProgramsPerSlide(includeDigital))
			{
				var pageDictionary = new Dictionary<string, string>();
				key = "Flightdates";
				value = _data.Parent.ScheduleSettings.FlightDates;
				if (!pageDictionary.Keys.Contains(key))
					pageDictionary.Add(key, value);
				key = "Advertiser  -  Decisionmaker";
				value = String.Format("{0}  -  {1}", _data.Parent.ScheduleSettings.BusinessName, _data.Parent.ScheduleSettings.DecisionMaker);
				if (!pageDictionary.Keys.Contains(key))
					pageDictionary.Add(key, value);

				key = "Program Info";
				var infoParts = 0;
				if (_data.ShowTime)
					infoParts++;
				if (_data.ShowDaypart)
					infoParts++;
				if (_data.ShowProgram)
					infoParts++;
				if (_data.ShowStation)
					infoParts++;
				if (infoParts == 1)
				{
					if (_data.ShowTime)
						value = "Time";
					else if (_data.ShowDaypart)
						value = "Daypart";
					else if (_data.ShowProgram)
						value = "Program";
					else if (_data.ShowStation)
						value = "Station";
				}
				else
					value = "Program Info";
				if (!pageDictionary.Keys.Contains(key))
					pageDictionary.Add(key, value);
				if (_data.ShowTotalRow)
				{
					key = "tspot";
					value = _data.ShowWeeklySpots ? String.Format("{0}{1}", _data.WeeklySpots.ToString("#,###"), _data.ShowSpotsX ? "x" : String.Empty) : String.Empty;
					if (!pageDictionary.Keys.Contains(key))
						pageDictionary.Add(key, value);

					key = "tcost";
					value = _data.ShowWeeklyCost ? _data.WeeklyCost.ToString(_data.UseDecimalRates ? "$#,##0.00" : "$#,##0") : String.Empty;
					if (!pageDictionary.Keys.Contains(key))
						pageDictionary.Add(key, value);

					var totalMonday = _data.Programs.Sum(p => p.MondaySpot);
					var totalTuesday = _data.Programs.Sum(p => p.TuesdaySpot);
					var totalWednesday = _data.Programs.Sum(p => p.WednesdaySpot);
					var totalThursday = _data.Programs.Sum(p => p.ThursdaySpot);
					var totalFriday = _data.Programs.Sum(p => p.FridaySpot);
					var totalSaturday = _data.Programs.Sum(p => p.SaturdaySpot);
					var totalSunday = _data.Programs.Sum(p => p.SundaySpot);
					var mergedWeekDaySpots = new Dictionary<string, string>();
					if (_data.ShowSpotsPerWeek)
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
							mergedWeekDaySpots.Add("t1", weekDayValues.Skip(1).Any(v => v > 0) ? "MergeSnapshot" : String.Empty);
						weekDayValues = weekDayValues.Skip(1).ToArray();
						if ((totalTuesday > 0 || mergedWeekDaySpots.Any()) &&
							 weekDayValues.Any(v => v > 0))
							mergedWeekDaySpots.Add("t2", weekDayValues.Skip(1).Any(v => v > 0) ? "MergeSnapshot" : String.Empty);
						weekDayValues = weekDayValues.Skip(1).ToArray();
						if ((totalWednesday > 0 || mergedWeekDaySpots.Any()) &&
							 weekDayValues.Any(v => v > 0))
							mergedWeekDaySpots.Add("t3", weekDayValues.Skip(1).Any(v => v > 0) ? "MergeSnapshot" : String.Empty);
						weekDayValues = weekDayValues.Skip(1).ToArray();
						if ((totalThursday > 0 || mergedWeekDaySpots.Any()) &&
							weekDayValues.Any(v => v > 0))
							mergedWeekDaySpots.Add("t4", weekDayValues.Skip(1).Any(v => v > 0) ? "MergeSnapshot" : String.Empty);
						weekDayValues = weekDayValues.Skip(1).ToArray();
						if ((totalFriday > 0 || mergedWeekDaySpots.Any()) &&
							weekDayValues.Any(v => v > 0))
							mergedWeekDaySpots.Add("t5", weekDayValues.Skip(1).Any(v => v > 0) ? "MergeSnapshot" : String.Empty);
						weekDayValues = weekDayValues.Skip(1).ToArray();
						if ((totalSaturday > 0 || mergedWeekDaySpots.Any()) &&
							weekDayValues.Any(v => v > 0))
							mergedWeekDaySpots.Add("t6", weekDayValues.Skip(1).Any(v => v > 0) ? "MergeSnapshot" : String.Empty);
						weekDayValues = weekDayValues.Skip(1).ToArray();
						if ((totalSunday > 0 || mergedWeekDaySpots.Any()) &&
							weekDayValues.Any(v => v > 0))
							mergedWeekDaySpots.Add("t7", weekDayValues.Skip(1).Any(v => v > 0) ? "MergeSnapshot" : String.Empty);

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

						var mergedSpotsValue = String.Format(mergedSpotValueFormat, _data.WeeklySpots.ToString("#,##0"), _data.ShowSpotsX ? "x" : String.Empty);
						foreach (var dictionaryKey in mergedWeekDaySpots.Keys.ToList())
						{
							if (mergedWeekDaySpots[dictionaryKey] == "MergeSnapshot") continue;
							mergedWeekDaySpots[dictionaryKey] = mergedSpotsValue;
						}
					}

					key = "t1";
					value = mergedWeekDaySpots.ContainsKey(key) ?
							mergedWeekDaySpots[key] :
							totalMonday > 0 ? String.Format("{0}{1}", totalMonday.Value.ToString("#,###"), _data.ShowSpotsX ? "x" : String.Empty) : "-";
					if (!pageDictionary.Keys.Contains(key))
						pageDictionary.Add(key, value);

					key = "t2";
					value = mergedWeekDaySpots.ContainsKey(key) ?
							mergedWeekDaySpots[key] :
							totalTuesday > 0 ? String.Format("{0}{1}", totalTuesday.Value.ToString("#,###"), _data.ShowSpotsX ? "x" : String.Empty) : "-";
					if (!pageDictionary.Keys.Contains(key))
						pageDictionary.Add(key, value);

					key = "t3";
					value = mergedWeekDaySpots.ContainsKey(key) ?
							mergedWeekDaySpots[key] :
							totalWednesday > 0 ? String.Format("{0}{1}", totalWednesday.Value.ToString("#,###"), _data.ShowSpotsX ? "x" : String.Empty) : "-";
					if (!pageDictionary.Keys.Contains(key))
						pageDictionary.Add(key, value);

					key = "t4";
					value = mergedWeekDaySpots.ContainsKey(key) ?
							mergedWeekDaySpots[key] :
							totalThursday > 0 ? String.Format("{0}{1}", totalThursday.Value.ToString("#,###"), _data.ShowSpotsX ? "x" : String.Empty) : "-";
					if (!pageDictionary.Keys.Contains(key))
						pageDictionary.Add(key, value);

					key = "t5";
					value = mergedWeekDaySpots.ContainsKey(key) ?
							mergedWeekDaySpots[key] :
							totalFriday > 0 ? String.Format("{0}{1}", totalFriday.Value.ToString("#,###"), _data.ShowSpotsX ? "x" : String.Empty) : "-";
					if (!pageDictionary.Keys.Contains(key))
						pageDictionary.Add(key, value);

					key = "t6";
					value = mergedWeekDaySpots.ContainsKey(key) ?
							mergedWeekDaySpots[key] :
							totalSaturday > 0 ? String.Format("{0}{1}", totalSaturday.Value.ToString("#,###"), _data.ShowSpotsX ? "x" : String.Empty) : "-";
					if (!pageDictionary.Keys.Contains(key))
						pageDictionary.Add(key, value);

					key = "t7";
					value = mergedWeekDaySpots.ContainsKey(key) ?
							mergedWeekDaySpots[key] :
							totalSunday > 0 ? String.Format("{0}{1}", totalSunday.Value.ToString("#,###"), _data.ShowSpotsX ? "x" : String.Empty) : "-";
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

				for (var j = 0; j < GetProgramsPerSlide(includeDigital); j++)
				{
					if ((i + j) < programsCount)
					{
						var program = _data.Programs[i + j];

						key = (j + 1).ToString("00");
						value = program.Index.ToString(_data.ShowLineId ? "00" : "MergeSnapshot");
						if (!pageDictionary.Keys.Contains(key))
							pageDictionary.Add(key, value);

						key = String.Format("Station{0} - Time{0} - [Daypart{0}]  [Program{0}]", j + 1);
						temp.Clear();
						if (_data.ShowStation && !String.IsNullOrEmpty(program.Station))
							temp.Add(program.Station);
						if (_data.ShowTime && !String.IsNullOrEmpty(program.Time))
							temp.Add(program.Time);
						if (_data.ShowDaypart || _data.ShowProgram)
						{
							var temp2 = new List<string>();
							if (_data.ShowDaypart && !String.IsNullOrEmpty(program.Daypart))
								temp2.Add(String.Format("[{0}]", program.Daypart));
							if (_data.ShowProgram && !String.IsNullOrEmpty(program.Name))
								temp2.Add(String.Format("[{0}]", program.Name));
							temp.Add(String.Join("  ", temp2));
						}
						value = String.Join(" - ", temp);
						if (!pageDictionary.Keys.Contains(key))
							pageDictionary.Add(key, value);

						key = String.Format("Station{0} – Time{0} - [Daypart{0}]  [Program{0}]", j + 1);
						if (!pageDictionary.Keys.Contains(key))
							pageDictionary.Add(key, value);

						key = String.Format("len{0}", j + 1);
						value = _data.ShowLenght && !String.IsNullOrEmpty(program.Length) ? program.Length : String.Empty;
						if (!pageDictionary.Keys.Contains(key))
							pageDictionary.Add(key, value);

						key = String.Format("rt{0}", j + 1);
						value = _data.ShowRate && program.Rate.HasValue ? program.Rate.Value.ToString(_data.UseDecimalRates ? "$#,##0.00" : "$#,##0") : String.Empty;
						if (!pageDictionary.Keys.Contains(key))
							pageDictionary.Add(key, value);

						key = String.Format("sp{0}", j + 1);
						value = String.Format("{0}{1}", program.TotalSpots.ToString("#,##0"), _data.ShowSpotsX ? "x" : String.Empty);
						if (!pageDictionary.Keys.Contains(key))
							pageDictionary.Add(key, value);

						key = String.Format("cs{0}", j + 1);
						value = program.TotalCost.ToString(_data.UseDecimalRates ? "$#,##0.00" : "$#,##0");
						if (!pageDictionary.Keys.Contains(key))
							pageDictionary.Add(key, value);

						var mergedWeekDaySpots = new Dictionary<string, string>();
						if (_data.ShowSpotsPerWeek)
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
								mergedWeekDaySpots.Add(String.Format("{0}a", j + 1), weekDayValues.Skip(1).Any(v => v.HasValue) ? "MergeSnapshot" : String.Empty);
							weekDayValues = weekDayValues.Skip(1).ToArray();
							if ((program.TuesdaySpot.HasValue || mergedWeekDaySpots.Any()) &&
								 weekDayValues.Any(v => v.HasValue))
								mergedWeekDaySpots.Add(String.Format("{0}b", j + 1), weekDayValues.Skip(1).Any(v => v.HasValue) ? "MergeSnapshot" : String.Empty);
							weekDayValues = weekDayValues.Skip(1).ToArray();
							if ((program.WednesdaySpot.HasValue || mergedWeekDaySpots.Any()) &&
								 weekDayValues.Any(v => v.HasValue))
								mergedWeekDaySpots.Add(String.Format("{0}c", j + 1), weekDayValues.Skip(1).Any(v => v.HasValue) ? "MergeSnapshot" : String.Empty);
							weekDayValues = weekDayValues.Skip(1).ToArray();
							if ((program.ThursdaySpot.HasValue || mergedWeekDaySpots.Any()) &&
								weekDayValues.Any(v => v.HasValue))
								mergedWeekDaySpots.Add(String.Format("{0}d", j + 1), weekDayValues.Skip(1).Any(v => v.HasValue) ? "MergeSnapshot" : String.Empty);
							weekDayValues = weekDayValues.Skip(1).ToArray();
							if ((program.FridaySpot.HasValue || mergedWeekDaySpots.Any()) &&
								weekDayValues.Any(v => v.HasValue))
								mergedWeekDaySpots.Add(String.Format("{0}e", j + 1), weekDayValues.Skip(1).Any(v => v.HasValue) ? "MergeSnapshot" : String.Empty);
							weekDayValues = weekDayValues.Skip(1).ToArray();
							if ((program.SaturdaySpot.HasValue || mergedWeekDaySpots.Any()) &&
								weekDayValues.Any(v => v.HasValue))
								mergedWeekDaySpots.Add(String.Format("{0}f", j + 1), weekDayValues.Skip(1).Any(v => v.HasValue) ? "MergeSnapshot" : String.Empty);
							weekDayValues = weekDayValues.Skip(1).ToArray();
							if ((program.SundaySpot.HasValue || mergedWeekDaySpots.Any()) &&
								weekDayValues.Any(v => v.HasValue))
								mergedWeekDaySpots.Add(String.Format("{0}g", j + 1), weekDayValues.Skip(1).Any(v => v.HasValue) ? "MergeSnapshot" : String.Empty);

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

							var mergedSpotsValue = String.Format(mergedSpotValueFormat, program.TotalSpots.ToString("#,##0"), _data.ShowSpotsX ? "x" : String.Empty);
							foreach (var dictionaryKey in mergedWeekDaySpots.Keys.ToList())
							{
								if (mergedWeekDaySpots[dictionaryKey] == "MergeSnapshot") continue;
								mergedWeekDaySpots[dictionaryKey] = mergedSpotsValue;
							}
						}
						key = String.Format("{0}a", j + 1);
						value = mergedWeekDaySpots.ContainsKey(key) ?
							mergedWeekDaySpots[key] :
							program.MondaySpot.HasValue ? String.Format("{0}{1}", program.MondaySpot.Value.ToString("#,###"), _data.ShowSpotsX ? "x" : String.Empty) : "-";
						if (!pageDictionary.Keys.Contains(key))
							pageDictionary.Add(key, value);

						key = String.Format("{0}b", j + 1);
						value = mergedWeekDaySpots.ContainsKey(key) ?
							mergedWeekDaySpots[key] :
							program.TuesdaySpot.HasValue ? String.Format("{0}{1}", program.TuesdaySpot.Value.ToString("#,###"), _data.ShowSpotsX ? "x" : String.Empty) : "-";
						if (!pageDictionary.Keys.Contains(key))
							pageDictionary.Add(key, value);

						key = String.Format("{0}c", j + 1);
						value = mergedWeekDaySpots.ContainsKey(key) ?
							mergedWeekDaySpots[key] :
							program.WednesdaySpot.HasValue ? String.Format("{0}{1}", program.WednesdaySpot.Value.ToString("#,###"), _data.ShowSpotsX ? "x" : String.Empty) : "-";
						if (!pageDictionary.Keys.Contains(key))
							pageDictionary.Add(key, value);

						key = String.Format("{0}d", j + 1);
						value = mergedWeekDaySpots.ContainsKey(key) ?
							mergedWeekDaySpots[key] :
							program.ThursdaySpot.HasValue ? String.Format("{0}{1}", program.ThursdaySpot.Value.ToString("#,###"), _data.ShowSpotsX ? "x" : String.Empty) : "-";
						if (!pageDictionary.Keys.Contains(key))
							pageDictionary.Add(key, value);

						key = String.Format("{0}e", j + 1);
						value = mergedWeekDaySpots.ContainsKey(key) ?
							mergedWeekDaySpots[key] :
							program.FridaySpot.HasValue ? String.Format("{0}{1}", program.FridaySpot.Value.ToString("#,###"), _data.ShowSpotsX ? "x" : String.Empty) : "-";
						if (!pageDictionary.Keys.Contains(key))
							pageDictionary.Add(key, value);

						key = String.Format("{0}f", j + 1);
						value = mergedWeekDaySpots.ContainsKey(key) ?
							mergedWeekDaySpots[key] :
							program.SaturdaySpot.HasValue ? String.Format("{0}{1}", program.SaturdaySpot.Value.ToString("#,###"), _data.ShowSpotsX ? "x" : String.Empty) : "-";
						if (!pageDictionary.Keys.Contains(key))
							pageDictionary.Add(key, value);

						key = String.Format("{0}g", j + 1);
						value = mergedWeekDaySpots.ContainsKey(key) ?
							mergedWeekDaySpots[key] :
							program.SundaySpot.HasValue ? String.Format("{0}{1}", program.SundaySpot.Value.ToString("#,###"), _data.ShowSpotsX ? "x" : String.Empty) : "-";
						if (!pageDictionary.Keys.Contains(key))
							pageDictionary.Add(key, value);
					}
					else if (includeDigital && ((i + j) - digitalStartIndex) < digitalsCount)
					{
						var digitalInfoRecord = _data.DigitalInfo.Records[(i + j) - digitalStartIndex];

						key = (j + 1).ToString("00");
						value = (digitalInfoRecord.Index + digitalStartIndex).ToString(_data.ShowLineId ? "00" : "MergeDigital");
						if (!pageDictionary.Keys.Contains(key))
							pageDictionary.Add(key, value);

						key = String.Format("Station{0} - Time{0} - [Daypart{0}]  [Program{0}]", j + 1);
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
						if (!pageDictionary.Keys.Contains(key))
							pageDictionary.Add(key, value);

						key = String.Format("Station{0} – Time{0} - [Daypart{0}]  [Program{0}]", j + 1);
						if (!pageDictionary.Keys.Contains(key))
							pageDictionary.Add(key, value);

						key = String.Format("len{0}", j + 1);
						if (!pageDictionary.Keys.Contains(key))
							pageDictionary.Add(key, "MergeDigital");

						key = String.Format("rt{0}", j + 1);
						if (!pageDictionary.Keys.Contains(key))
							pageDictionary.Add(key, "MergeDigital");

						key = String.Format("sp{0}", j + 1);
						if (!pageDictionary.Keys.Contains(key))
							pageDictionary.Add(key, "MergeDigital");

						key = String.Format("cs{0}", j + 1);
						if (!pageDictionary.Keys.Contains(key))
							pageDictionary.Add(key, "MergeDigital");

						key = String.Format("{0}a", j + 1);
						if (!pageDictionary.Keys.Contains(key))
							pageDictionary.Add(key, "MergeDigital");

						key = String.Format("{0}b", j + 1);
						if (!pageDictionary.Keys.Contains(key))
							pageDictionary.Add(key, "MergeDigital");

						key = String.Format("{0}c", j + 1);
						if (!pageDictionary.Keys.Contains(key))
							pageDictionary.Add(key, "MergeDigital");

						key = String.Format("{0}d", j + 1);
						if (!pageDictionary.Keys.Contains(key))
							pageDictionary.Add(key, "MergeDigital");

						key = String.Format("{0}e", j + 1);
						if (!pageDictionary.Keys.Contains(key))
							pageDictionary.Add(key, "MergeDigital");

						key = String.Format("{0}f", j + 1);
						if (!pageDictionary.Keys.Contains(key))
							pageDictionary.Add(key, "MergeDigital");

						key = String.Format("{0}g", j + 1);
						if (!pageDictionary.Keys.Contains(key))
							pageDictionary.Add(key, "MergeDigital");
					}
					else
					{
						key = String.Format("Station{0} – Time{0} - [Daypart{0}]  [Program{0}]", j + 1);
						value = "Delete Row";
						if (!pageDictionary.Keys.Contains(key))
							pageDictionary.Add(key, value);
						if (!pageDictionary.Keys.Contains(key))
							pageDictionary.Add(key, value);
					}
				}
				ReplacementsList.Add(pageDictionary);
			}
		}

		private void PrepareTotalRow(bool includeDigital)
		{
			const string separator = "        ";
			var summaryData = _data.Parent.SnapshotSummary;
			var hasSeveralSnapshots = _data.Parent.Snapshots.Count > 1;

			var totalValues = new List<string>();
			if (_data.ShowTotalCost)
				totalValues.Add(String.Format("Total Cost: {0}",
					summaryData.TotalCost.ToString(_data.UseDecimalRates ? "$#,##0.00" : "$#,##0")));
			if (_data.ShowTotalSpots)
				totalValues.Add(String.Format("Total Spots: {0}x", summaryData.TotalSpots));

			var values = new List<string>();
			if (hasSeveralSnapshots)
			{
				if (_data.ShowWeeklySpots)
					values.Add(String.Format("Weekly Spots: {0}x", _data.WeeklySpots));
				if (_data.ShowWeeklyCost)
					values.Add(String.Format("Weekly Cost: {0}",
					_data.WeeklyCost.ToString(_data.UseDecimalRates ? "$#,##0.00" : "$#,##0")));
				values.Add(String.Format("Weeks: {0}", _data.TotalWeeks));
				if (_data.ShowAverageRate)
					values.Add(String.Format("Average Rate: {0}",
					_data.AvgRate.ToString(_data.UseDecimalRates ? "$#,##0.00" : "$#,##0")));
				if (totalValues.Any())
					values.Add(String.Format("({0})", String.Join(separator, totalValues)));
			}
			else
			{
				values.AddRange(totalValues);
				if (_data.ShowWeeklySpots)
					values.Add(String.Format("Weekly Spots: {0}x", _data.WeeklySpots));
				if (_data.ShowWeeklyCost)
					values.Add(String.Format("Weekly Cost: {0}",
					_data.WeeklyCost.ToString(_data.UseDecimalRates ? "$#,##0.00" : "$#,##0")));
				values.Add(String.Format("Weeks: {0}", _data.TotalWeeks));
				if (_data.ShowAverageRate)
					values.Add(String.Format("Average Rate: {0}",
					_data.AvgRate.ToString(_data.UseDecimalRates ? "$#,##0.00" : "$#,##0")));
			}
			var scheduleSummary = String.Format("{1}{0}", String.Join(separator, values), separator);
			if (includeDigital)
			{
				values.Clear();
				if (_data.DigitalInfo.ShowMonthlyInvestemt && _data.DigitalInfo.MonthlyInvestment.HasValue)
					values.Add(String.Format("Monthly Digital Investment: {0}",
						_data.DigitalInfo.MonthlyInvestment.Value.ToString("$#,###.00")));
				if (_data.DigitalInfo.ShowTotalInvestemt && _data.DigitalInfo.TotalInvestment.HasValue)
					values.Add(String.Format("Total Digital Investment: {0}",
						_data.DigitalInfo.TotalInvestment.Value.ToString("$#,###.00")));
				var digitalSummary = String.Join("     ", values);
				if (!String.IsNullOrEmpty(digitalSummary))
					TotalRowValue = String.Join(String.Format("{0}", (char)13), scheduleSummary, String.Format("       {0}", digitalSummary));
				else
					TotalRowValue = scheduleSummary;
			}
			else
				TotalRowValue = scheduleSummary;
		}

		public PreviewGroup GeneratePreview(bool includeDigital)
		{
			Logos = GetLogos(includeDigital);
			PopulateReplacementsList(includeDigital);
			PrepareTotalRow(includeDigital);
			GetTemplatePath(includeDigital);
			var previewGroup = new PreviewGroup
			{
				Name = String.Format("{0} ({1})", _data.Name, includeDigital ? String.Format("{0} + Digital", MediaMetaData.Instance.DataTypeString) : MediaMetaData.Instance.DataTypeString),
				PresentationSourcePath = Path.Combine(Common.Core.Configuration.ResourceManager.Instance.TempFolder.LocalPath, Path.GetFileName(Path.GetTempFileName()))
			};
			RegularMediaSchedulePowerPointHelper.Instance.PrepareSnapshotEmail(previewGroup.PresentationSourcePath, new[] { this }, SelectedTheme, MediaMetaData.Instance.SettingsManager.UseSlideMaster);
			return previewGroup;
		}

		public void GenerateOutput(bool includeDigital)
		{
			Logos = GetLogos(includeDigital);
			PopulateReplacementsList(includeDigital);
			PrepareTotalRow(includeDigital);
			GetTemplatePath(includeDigital);
			RegularMediaSchedulePowerPointHelper.Instance.AppendSnapshot(new[] { this }, SelectedTheme, MediaMetaData.Instance.SettingsManager.UseSlideMaster);
		}
		#endregion
	}
}
