using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Media.Configuration;
using Asa.Business.Media.Entities.NonPersistent.Common;
using Asa.Business.Media.Entities.NonPersistent.Schedule;
using Asa.Business.Media.Entities.NonPersistent.Section.Content;
using Asa.Business.Media.Enums;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.Images;
using Asa.Common.Core.Objects.Themes;
using Asa.Common.GUI.Common;
using Asa.Common.GUI.ImageGallery;
using Asa.Common.GUI.Preview;
using Asa.Media.Controls.BusinessClasses.Managers;
using Asa.Media.Controls.BusinessClasses.Output.DigitalInfo;
using Asa.Media.Controls.BusinessClasses.Output.ProgramSchedule;
using Asa.Media.Controls.InteropClasses;
using Asa.Media.Controls.PresentationClasses.ScheduleControls.Output;
using Asa.Media.Controls.PresentationClasses.SnapshotControls.ContentEditors;
using Asa.Schedules.Common.Controls.ContentEditors.Helpers;
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
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraTab;

namespace Asa.Media.Controls.PresentationClasses.ScheduleControls.ContentEditors
{
    [ToolboxItem(false)]
    //public partial class SectionControl : UserControl
    public partial class SectionControl : XtraTabPage, ISectionEditorControl, ISectionOutputControl, ISectionItemCollectionControl
    {
        private SectionContainer _sectionContainer;
        private readonly List<BandedGridColumn> _spotColumns = new List<BandedGridColumn>();
        private readonly Font _spotColumnFont = new Font("Arial", 14);
        protected GridDragDropHelper _dragDropHelper;

        private Quarter _selectedQuarter;

        private string SpotTitle => _sectionContainer.SectionData.ParentScheduleSettings.SelectedSpotType.ToString();

        public SectionEditorType EditorType => SectionEditorType.Schedule;

        public string CollectionTitle => "Program";
        public string CollectionItemTitle => "Program";
        public bool AllowToAddItem => true;
        public bool AllowToDeleteItem => _sectionContainer != null && _sectionContainer.SectionData.Programs.Any();

        public SectionControl(SectionContainer sectionContainer)
        {
            _sectionContainer = sectionContainer;
            InitializeComponent();
            Dock = DockStyle.Fill;
            Text = MediaMetaData.Instance.DataTypeString;
        }

        #region ISectionEditorControl Memebers
        public void InitControls()
        {
            repositoryItemComboBoxDays.EnableSelectAll();
            repositoryItemComboBoxDayparts.EnableSelectAll();
            repositoryItemComboBoxLengths.EnableSelectAll();
            repositoryItemPopupContainerEditProgram.EnableSelectAll();
            repositoryItemTextEditProgram.EnableSelectAll();
            repositoryItemComboBoxStations.EnableSelectAll();
            repositoryItemComboBoxTimes.EnableSelectAll();
            repositoryItemSpinEdit000s.EnableSelectAll();
            repositoryItemSpinEditRate.EnableSelectAll();
            repositoryItemSpinEditRating.EnableSelectAll();
            repositoryItemSpinEditSpot.EnableSelectAll();

            pictureEditDefaultLogo.Image = BusinessObjects.Instance.ImageResourcesManager.ProgramScheduleNoProgramsLogo ?? pictureEditDefaultLogo.Image;

            var scaleFactor = Utilities.GetScaleFactor(CreateGraphics().DpiX);

            gridBandId.Width = (Int32)(gridBandId.Width * scaleFactor.Width);
            gridBandLogo.Width = (Int32)(gridBandLogo.Width * scaleFactor.Width);
            gridBandStation.Width = (Int32)(gridBandStation.Width * scaleFactor.Width);
            bandedGridColumnDay.Width = (Int32)(bandedGridColumnDay.Width * scaleFactor.Width);
            bandedGridColumnTime.Width = (Int32)(bandedGridColumnTime.Width * scaleFactor.Width);
            bandedGridColumnLength.Width = (Int32)(bandedGridColumnLength.Width * scaleFactor.Width);
            gridBandRate.Width = (Int32)(gridBandRate.Width * scaleFactor.Width);
            bandedGridColumnRate.Width = (Int32)(bandedGridColumnRate.Width * scaleFactor.Width);
            bandedGridColumnRating.Width = (Int32)(bandedGridColumnRating.Width * scaleFactor.Width);
            gridBandTotals.Width = (Int32)(gridBandTotals.Width * scaleFactor.Width);
            bandedGridColumnTotalSpots.Width = (Int32)(bandedGridColumnTotalSpots.Width * scaleFactor.Width);
            bandedGridColumnCost.Width = (Int32)(bandedGridColumnCost.Width * scaleFactor.Width);
            bandedGridColumnGRP.Width = (Int32)(bandedGridColumnGRP.Width * scaleFactor.Width);
            bandedGridColumnCPP.Width = (Int32)(bandedGridColumnCPP.Width * scaleFactor.Width);

            gridColumnProgramSourceStation.Width = (Int32)(gridColumnProgramSourceStation.Width * scaleFactor.Width);
            gridColumnProgramSourceDaypart.Width = (Int32)(gridColumnProgramSourceDaypart.Width * scaleFactor.Width);
            gridColumnProgramSourceDay.Width = (Int32)(gridColumnProgramSourceDay.Width * scaleFactor.Width);
            gridColumnProgramSourceTime.Width = (Int32)(gridColumnProgramSourceTime.Width * scaleFactor.Width);

            PopupStateHelper.Init(repositoryItemPopupContainerEditProgram, Common.Core.Configuration.ResourceManager.Instance.AppSettingsFolder, "ScheduleSection");
        }

        public void Release()
        {
            _spotColumns.Clear();
            gridControlProgramSource.DataSource = null;
            gridControlSchedule.DataSource = null;
            _selectedQuarter = null;
            _sectionContainer = null;
        }
        #endregion

        #region Data Methods
        public void LoadData()
        {
            bandedGridColumnRating.Caption = String.Format("{0}{1}",
                (!String.IsNullOrEmpty(_sectionContainer.SectionData.ParentScheduleSettings.Demo) ? String.Format("{0} ", _sectionContainer.SectionData.ParentScheduleSettings.Demo) : String.Empty),
                _sectionContainer.SectionData.ParentScheduleSettings.DemoType == DemoType.Rtg ? "Rtg" : "(000)");
            bandedGridColumnRating.ColumnEdit = _sectionContainer.SectionData.ParentScheduleSettings.DemoType == DemoType.Rtg ? repositoryItemSpinEditRating : repositoryItemSpinEdit000s;
            bandedGridColumnCPP.Caption = _sectionContainer.SectionData.ParentScheduleSettings.DemoType == DemoType.Rtg ? "CPP" : "CPM";
            bandedGridColumnGRP.Caption = _sectionContainer.SectionData.ParentScheduleSettings.DemoType == DemoType.Rtg ? "GRPs" : "Impr";
            bandedGridColumnGRP.ColumnEdit = _sectionContainer.SectionData.ParentScheduleSettings.DemoType == DemoType.Rtg ? repositoryItemSpinEditRating : repositoryItemSpinEdit000s;
            bandedGridColumnGRP.SummaryItem.DisplayFormat = _sectionContainer.SectionData.ParentScheduleSettings.DemoType == DemoType.Rtg ? "{0:n1}" : "{0:n0}";

            repositoryItemComboBoxStations.Items.Clear();
            repositoryItemComboBoxStations.Items.AddRange(_sectionContainer.SectionData.ParentScheduleSettings.Stations.Where(x => x.Available).OfType<object>().ToArray());
            repositoryItemComboBoxDayparts.Items.Clear();
            repositoryItemComboBoxDayparts.Items.AddRange(_sectionContainer.SectionData.ParentScheduleSettings.Dayparts.Where(x => x.Available).OfType<object>().ToArray());
            repositoryItemComboBoxLengths.Items.Clear();
            repositoryItemComboBoxLengths.Items.AddRange(MediaMetaData.Instance.ListManager.Lengths);
            repositoryItemComboBoxDays.Items.Clear();
            repositoryItemComboBoxDays.Items.AddRange(MediaMetaData.Instance.ListManager.Days);
            repositoryItemComboBoxTimes.Items.Clear();
            repositoryItemComboBoxTimes.Items.AddRange(MediaMetaData.Instance.ListManager.Times);

            UpdateGridData(true);
            UpdateGridView();
            UpdateSpotsByQuarter();
        }

        public void SaveData()
        {
            advBandedGridViewSchedule.CloseEditor();
        }

        public void AddItem()
        {
            _sectionContainer.SectionData.AddProgram();
            UpdateGridData(true);
            UpdateSpotsByQuarter();
            if (advBandedGridViewSchedule.RowCount > 0)
                advBandedGridViewSchedule.FocusedRowHandle = advBandedGridViewSchedule.RowCount - 1;

            _sectionContainer.RaiseDataChanged(new SectionDataChangedEventArgs());
        }

        private void CloneProgram(int sourceIndex, bool fullClone)
        {
            _sectionContainer.SectionData.CloneProgram(sourceIndex, fullClone);
            UpdateGridData(true);
            if (_sectionContainer.SectionData.CloneLineToTheEnd)
                advBandedGridViewSchedule.FocusedRowHandle = advBandedGridViewSchedule.RowCount - 1;
            else
                advBandedGridViewSchedule.FocusedRowHandle = sourceIndex + 1;

            _sectionContainer.RaiseDataChanged(new SectionDataChangedEventArgs());
        }

        public void DeleteItem()
        {
            var selectedProgramRow = advBandedGridViewSchedule.GetFocusedDataRow();
            if (selectedProgramRow == null) return;
            var selectedProgramIndex = selectedProgramRow[0].ToString();
            if (PopupMessageHelper.Instance.ShowWarningQuestion(String.Format("Delete Line ID {0}?", selectedProgramIndex)) != DialogResult.Yes)
                return;
            _sectionContainer.SectionData.DeleteProgram(advBandedGridViewSchedule.GetDataSourceRowIndex(advBandedGridViewSchedule.FocusedRowHandle));
            UpdateGridData(true);

            _sectionContainer.RaiseDataChanged(new SectionDataChangedEventArgs());
        }

        public void UpdateGridView()
        {
            advBandedGridViewSchedule.OptionsView.ShowFooter = _sectionContainer.SectionData.ShowSpots ||
                _sectionContainer.SectionData.ShowTotalSpots ||
                _sectionContainer.SectionData.ShowCost ||
                _sectionContainer.SectionData.ShowCPP ||
                _sectionContainer.SectionData.ShowGRP;

            gridBandRate.Visible = _sectionContainer.SectionData.ShowRate | _sectionContainer.SectionData.ShowRating;
            bandedGridColumnRate.Visible = _sectionContainer.SectionData.ShowRate;
            bandedGridColumnRating.Visible = _sectionContainer.SectionData.ShowRating;
            if (_sectionContainer.SectionData.ShowRate)
                advBandedGridViewSchedule.SetColumnPosition(bandedGridColumnRate, 0, 0);
            if (_sectionContainer.SectionData.ShowRating)
                advBandedGridViewSchedule.SetColumnPosition(bandedGridColumnRating, 1, 0);

            bandedGridColumnTotalSpots.Visible = _sectionContainer.SectionData.ShowTotalSpots;
            bandedGridColumnCPP.Visible = _sectionContainer.SectionData.ShowCPP;
            bandedGridColumnGRP.Visible = _sectionContainer.SectionData.ShowGRP;
            bandedGridColumnCost.Visible = _sectionContainer.SectionData.ShowCost;

            gridBandProgram.Visible = _sectionContainer.SectionData.ShowProgram || _sectionContainer.SectionData.ShowLenght || _sectionContainer.SectionData.ShowDay || _sectionContainer.SectionData.ShowTime;
            bandedGridColumnName.Visible = _sectionContainer.SectionData.ShowProgram;
            bandedGridColumnLength.Visible = _sectionContainer.SectionData.ShowLenght;
            bandedGridColumnDay.Visible = _sectionContainer.SectionData.ShowDay;
            bandedGridColumnTime.Visible = _sectionContainer.SectionData.ShowTime;
            if (_sectionContainer.SectionData.ShowProgram)
                advBandedGridViewSchedule.SetColumnPosition(bandedGridColumnName, 0, 0);
            var secondRowIndex = _sectionContainer.SectionData.ShowProgram ? 1 : 0;
            var columnIndex = 0;
            if (_sectionContainer.SectionData.ShowDay)
            {
                advBandedGridViewSchedule.SetColumnPosition(bandedGridColumnDay, secondRowIndex, columnIndex);
                columnIndex++;
            }
            if (_sectionContainer.SectionData.ShowTime)
            {
                advBandedGridViewSchedule.SetColumnPosition(bandedGridColumnTime, secondRowIndex, columnIndex);
                columnIndex++;
            }
            if (_sectionContainer.SectionData.ShowLenght)
            {
                advBandedGridViewSchedule.SetColumnPosition(bandedGridColumnLength, secondRowIndex, columnIndex);
                columnIndex++;
            }

            gridBandLogo.Visible = _sectionContainer.SectionData.ShowLogo;

            bandedGridColumnStation.Visible = _sectionContainer.SectionData.ShowStation;
            bandedGridColumnDaypart.Visible = _sectionContainer.SectionData.ShowDaypart;

            UpdateColumnsAccordingScreenSize();
            UpdateSpotsByQuarter();
            UpdateRateFormat();
        }

        private void UpdateColumnsAccordingScreenSize()
        {
            gridBandStation.Visible = _sectionContainer.SectionData.ShowStation || _sectionContainer.SectionData.ShowDaypart;
            if (_sectionContainer.SectionData.ShowStation)
                advBandedGridViewSchedule.SetColumnPosition(bandedGridColumnStation, 0, 0);
            if (_sectionContainer.SectionData.ShowDaypart)
                advBandedGridViewSchedule.SetColumnPosition(bandedGridColumnDaypart, 1, 0);
            gridBandTotals.Visible = _sectionContainer.SectionData.ShowTotalSpots || _sectionContainer.SectionData.ShowCost || _sectionContainer.SectionData.ShowGRP || _sectionContainer.SectionData.ShowCPP;
            if (_sectionContainer.SectionData.ShowTotalSpots)
                advBandedGridViewSchedule.SetColumnPosition(bandedGridColumnTotalSpots, 0, 0);
            if (_sectionContainer.SectionData.ShowGRP)
                advBandedGridViewSchedule.SetColumnPosition(bandedGridColumnGRP, 1, 0);
            var secondColumnIndex = _sectionContainer.SectionData.ShowTotalSpots || _sectionContainer.SectionData.ShowGRP ? 1 : 0;
            if (_sectionContainer.SectionData.ShowCost)
                advBandedGridViewSchedule.SetColumnPosition(bandedGridColumnCost, 0, secondColumnIndex);
            if (_sectionContainer.SectionData.ShowCPP)
                advBandedGridViewSchedule.SetColumnPosition(bandedGridColumnCPP, 1, secondColumnIndex);
        }

        public void UpdateSpotsByQuarter()
        {
            if (_selectedQuarter != null && _selectedQuarter.DateAnchor == _sectionContainer.SectionData.Parent.SelectedQuarter)
                return;

            _selectedQuarter = _sectionContainer.SectionData.ParentScheduleSettings.Quarters.FirstOrDefault(
                    q => q.DateAnchor == _sectionContainer.SectionData.Parent.SelectedQuarter);
            if (_sectionContainer.SectionData.ShowSpots)
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

        public void UpdateGridData(bool rebuildColumns)
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

            _sectionContainer.SectionData.GenerateDataSource();

            if (rebuildColumns)
                BuildSpotColumns();
            if (_sectionContainer.SectionData.Programs.Any())
            {
                layoutControlItemDefaultLogo.Visibility = LayoutVisibility.Never;
                layoutControlItemPrograms.Visibility = LayoutVisibility.Always;
                gridControlSchedule.DataSource = _sectionContainer.SectionData.DataSource;
            }
            else
            {
                layoutControlItemPrograms.Visibility = LayoutVisibility.Never;
                layoutControlItemDefaultLogo.Visibility = LayoutVisibility.Always;
            }
            advBandedGridViewSchedule.EndUpdate();
            if (_dragDropHelper == null && _sectionContainer.SectionData.Programs.Any())
            {
                _dragDropHelper = new GridDragDropHelper(advBandedGridViewSchedule, true, handledColumns: new[] { bandedGridColumnIndex, bandedGridColumnLogoImage });
                _dragDropHelper.AfterDrop += OnGridControlAfterDrop;
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
            var scaleFactor = Utilities.GetScaleFactor(CreateGraphics().DpiX);
            foreach (DataColumn column in _sectionContainer.SectionData.DataSource.Columns)
            {
                if (!column.ColumnName.Contains(ScheduleSection.ProgramSpotDataColumnNamePrefix)) continue;
                var bandedGridColumn = new BandedGridColumn();
                bandedGridColumn.AppearanceCell.Font = _spotColumnFont;
                bandedGridColumn.AppearanceCell.Options.UseTextOptions = true;
                bandedGridColumn.AppearanceCell.Options.UseFont = true;
                bandedGridColumn.AppearanceCell.Options.HighPriority = true;
                bandedGridColumn.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                bandedGridColumn.AutoFillDown = true;
                bandedGridColumn.Caption = column.Caption.ToUpper();
                bandedGridColumn.ColumnEdit = repositoryItemSpinEditSpot;
                bandedGridColumn.FieldName = column.ColumnName;
                bandedGridColumn.ToolTip = column.ExtendedProperties["Tooltip"] as String;
                bandedGridColumn.Tag = column.ExtendedProperties["SpotSettings"];
                bandedGridColumn.OptionsColumn.FixedWidth = true;
                bandedGridColumn.RowCount = 2;
                bandedGridColumn.Width = (Int32)(45 * scaleFactor.Width);
                bandedGridColumn.Visible = true;
                bandedGridColumn.SummaryItem.FieldName = column.ColumnName;
                bandedGridColumn.SummaryItem.SummaryType = SummaryItemType.Sum;
                bandedGridColumn.OptionsColumn.AllowSize = false;
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
            gridBandSpots.Visible = _spotColumns.Count > 0 && _sectionContainer.SectionData.ShowSpots;
        }

        private void UpdateRateFormat()
        {
            if (_sectionContainer.SectionData.UseDecimalRates)
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

        private IEnumerable<DXMenuItem> GetColumnContextMenuItems(ColumnView targetView, GridColumn targetColumn)
        {
            var items = new List<DXMenuItem>();
            if (_spotColumns.Any(sc => sc.Visible && sc == targetColumn))
            {
                items.Add(new DXMenuItem("Create Weekly Snapshot", (o, args) =>
                {
                    targetView.CloseEditor();
                    using (var form = new FormSnapshotFromSection())
                    {
                        form.SnapshotName = _sectionContainer.SectionData.Name;
                        if (form.ShowDialog(Controller.Instance.FormMain) == DialogResult.OK)
                        {
                            var newSnapshot = _sectionContainer.SectionData.CopyScheduleToSnapshot(
                                form.SnapshotName,
                                (DateTime)((object[])targetColumn.Tag)[3],
                                form.CopySpots);
                            _sectionContainer.RaiseDataChanged(new SectionDataChangedEventArgs { SnapshotsChanged = true });
                            using (var confirmation = new FormCopyContentConfirmation())
                            {
                                confirmation.Text = "Create Snapshot";
                                confirmation.simpleLabelItemTitle.Text = String.Format(confirmation.simpleLabelItemTitle.Text, "Snapshot successfully added");
                                confirmation.buttonXOK.Text = String.Format("Go to {0}", Controller.Instance.TabSnapshot.Text);
                                if (confirmation.ShowDialog(Controller.Instance.FormMain) == DialogResult.OK)
                                    ContentRibbonManager<MediaScheduleChangeInfo>.ShowRibbonTab(
                                        ContentIdentifiers.Snapshots,
                                        new SnapshotOpenEventArgs
                                        {
                                            SnapshotId = newSnapshot.UniqueID,
                                            EditorType = SnapshotEditorType.Schedule
                                        });
                            }
                        }
                    }
                }));
            }
            return items;
        }

        private IEnumerable<DXMenuItem> GetCellContextMenuItems(ColumnView targetView, GridColumn targetColumn, int targetRowHandle)
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

        private void OnDeleteProgram(object sender, EventArgs e)
        {
            DeleteItem();
        }
        #endregion

        #region Grid Events
        private void OnGridViewCellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            advBandedGridViewSchedule.CloseEditor();
            advBandedGridViewSchedule.UpdateCurrentRow();
        }

        private void OnGridViewCustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
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

        private void OnGridViewCustomDrawFooter(object sender, RowObjectCustomDrawEventArgs e)
        {
            if (_spotColumns.Count > 0)
            {
                e.Painter.DrawObject(e.Info);
                var view = sender as AdvBandedGridView;
                var viewInfo = view.GetViewInfo() as AdvBandedGridViewInfo;

                var column = bandedGridColumnName;
                if (_sectionContainer.SectionData.ShowRate)
                    column = bandedGridColumnRate;
                else if (_sectionContainer.SectionData.ShowRating)
                    column = bandedGridColumnRating;
                else if (_sectionContainer.SectionData.ShowLenght)
                    column = bandedGridColumnLength;
                else if (_sectionContainer.SectionData.ShowDay)
                    column = bandedGridColumnDay;
                else if (_sectionContainer.SectionData.ShowTime)
                    column = bandedGridColumnTime;

                var x = viewInfo.ColumnsInfo[column].Bounds.X;
                var width = viewInfo.ColumnsInfo[column].Bounds.Width;
                const string spotTotalTitle = "Totals: ";
                var titleWidth = (Int32)(50 * Utilities.GetScaleFactor(CreateGraphics().DpiX).Width);
                var size = e.Appearance.CalcTextSize(e.Cache, spotTotalTitle, titleWidth);
                var textWidth = Convert.ToInt32(size.Width) + 1;
                var textRect = new Rectangle(x + width - titleWidth, e.Bounds.Y, textWidth, e.Bounds.Height);
                e.Appearance.DrawString(e.Cache, spotTotalTitle, textRect);
                e.Handled = true;
            }
        }

        private void OnGridViewCustomRowCellEditForEditing(object sender, CustomRowCellEditEventArgs e)
        {
            if (e.Column != bandedGridColumnName || advBandedGridViewSchedule.FocusedRowHandle == GridControl.InvalidRowHandle) return;
            var station = advBandedGridViewSchedule.GetRowCellValue(advBandedGridViewSchedule.FocusedRowHandle, bandedGridColumnStation) as String;
            var daypart = advBandedGridViewSchedule.GetRowCellValue(advBandedGridViewSchedule.FocusedRowHandle, bandedGridColumnDaypart) as String;
            gridColumnProgramSourceStation.Visible = String.IsNullOrEmpty(station);
            gridColumnProgramSourceDaypart.Visible = String.IsNullOrEmpty(daypart);
            var daypartFromList = MediaMetaData.Instance.ListManager.Dayparts.FirstOrDefault(x => x.Code.Equals(daypart));
            gridColumnProgramSourceName.Caption = String.Format("Program ({0})", daypartFromList != null ? daypartFromList.Name : "All Programming");
            var dataSource = new List<SourceProgram>();
            if (!String.IsNullOrEmpty(station) && (!String.IsNullOrEmpty(daypart) || !_sectionContainer.SectionData.ShowDaypart))
                dataSource.AddRange(MediaMetaData.Instance.ListManager.SourcePrograms.Where(x => (x.Station.Equals(station) || String.IsNullOrEmpty(station)) && (!_sectionContainer.SectionData.ShowDaypart || (x.Daypart.Equals(daypart) || String.IsNullOrEmpty(daypart)))));
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

        private void OnGridViewProgramSourceDoubleClick(object sender, EventArgs e)
        {
            popupContainerControlProgramSource.OwnerEdit.ClosePopup();
        }

        private void OnRepositoryItemPopupContainerEditProgramCloseUp(object sender, CloseUpEventArgs e)
        {
            if (e.CloseMode != PopupCloseMode.Normal) return;
            if (gridViewProgramSource.FocusedRowHandle != GridControl.InvalidRowHandle)
            {
                var program = MediaMetaData.Instance.ListManager.SourcePrograms.FirstOrDefault(x => x.Id.Equals(gridViewProgramSource.GetRowCellValue(gridViewProgramSource.FocusedRowHandle, gridColumnProgramSourceId).ToString()));
                if (program != null)
                {
                    advBandedGridViewSchedule.CellValueChanged -= OnGridViewCellValueChanged;
                    e.Value = program.Name;
                    if (advBandedGridViewSchedule.GetRowCellValue(advBandedGridViewSchedule.FocusedRowHandle, bandedGridColumnDaypart) == null || string.IsNullOrEmpty(advBandedGridViewSchedule.GetRowCellValue(advBandedGridViewSchedule.FocusedRowHandle, bandedGridColumnDaypart).ToString()))
                        advBandedGridViewSchedule.SetRowCellValue(advBandedGridViewSchedule.FocusedRowHandle, bandedGridColumnDaypart, program.Daypart);
                    advBandedGridViewSchedule.SetRowCellValue(advBandedGridViewSchedule.FocusedRowHandle, bandedGridColumnDay, program.Day);
                    advBandedGridViewSchedule.SetRowCellValue(advBandedGridViewSchedule.FocusedRowHandle, bandedGridColumnTime, program.Time);
                    if (string.IsNullOrEmpty(advBandedGridViewSchedule.GetRowCellValue(advBandedGridViewSchedule.FocusedRowHandle, bandedGridColumnLength).ToString()))
                        advBandedGridViewSchedule.SetRowCellValue(advBandedGridViewSchedule.FocusedRowHandle, bandedGridColumnLength, MediaMetaData.Instance.ListManager.Lengths.FirstOrDefault());
                    if (_sectionContainer.SectionData.ParentScheduleSettings.ImportDemo && _sectionContainer.SectionData.ParentScheduleSettings.UseDemo)
                    {
                        var demo = program.Demos.FirstOrDefault(d => d.Name.Equals(_sectionContainer.SectionData.ParentScheduleSettings.Demo) && d.Source.Equals(_sectionContainer.SectionData.ParentScheduleSettings.Source) && d.DemoType == _sectionContainer.SectionData.ParentScheduleSettings.DemoType);
                        if (demo != null)
                            advBandedGridViewSchedule.SetRowCellValue(advBandedGridViewSchedule.FocusedRowHandle, bandedGridColumnRating, demo.Value);
                    }
                    advBandedGridViewSchedule.CellValueChanged += OnGridViewCellValueChanged;
                }
            }
            e.AcceptValue = true;
        }

        private void OnRepositoryItemPopupContainerEditProgramClosed(object sender, ClosedEventArgs e)
        {
            advBandedGridViewSchedule.CloseEditor();
        }

        private void OnGridViewShownEditor(object sender, EventArgs e)
        {
            var view = sender as AdvBandedGridView;
            var edit = view.ActiveEditor as TextEdit;
            if (edit == null) return;
            edit.Properties.BeforeShowMenu += OnMenuBeforeShow;
        }

        private void OnMenuBeforeShow(object sender, BeforeShowMenuEventArgs e)
        {
            var items = GetCellContextMenuItems(advBandedGridViewSchedule, advBandedGridViewSchedule.FocusedColumn, advBandedGridViewSchedule.FocusedRowHandle);
            if (!items.Any()) return;
            e.Menu.Items.Clear();
            foreach (var menuItem in items)
                e.Menu.Items.Add(menuItem);
        }

        private void OnGridViewPopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (e.HitInfo.InRowCell)
            {
                foreach (var menuItem in GetCellContextMenuItems(advBandedGridViewSchedule, e.HitInfo.Column, e.HitInfo.RowHandle))
                    e.Menu.Items.Add(menuItem);
            }
            else if (e.HitInfo.InColumn)
            {
                e.Menu.Items.Clear();
                foreach (var menuItem in GetColumnContextMenuItems(advBandedGridViewSchedule, e.HitInfo.Column))
                    e.Menu.Items.Add(menuItem);
            }
        }

        private void OnGridViewScheduleRowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (e.Column != bandedGridColumnLogoImage) return;
            if (e.Clicks < 2) return;
            if (advBandedGridViewSchedule.FocusedRowHandle == GridControl.InvalidRowHandle) return;
            using (var form = new FormImageGallery(MediaMetaData.Instance.ListManager.Images))
            {
                if (form.ShowDialog(Controller.Instance.FormMain) != DialogResult.OK) return;
                if (form.SelectedImageSource == null) return;
                advBandedGridViewSchedule.SetRowCellValue(advBandedGridViewSchedule.FocusedRowHandle, bandedGridColumnLogoSource, form.SelectedImageSource.Serialize());
                advBandedGridViewSchedule.SetRowCellValue(advBandedGridViewSchedule.FocusedRowHandle, bandedGridColumnLogoImage, form.SelectedImageSource.SmallImage);
            }
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
            _sectionContainer.SectionData.ChangeProgramPosition(sourceRow, targetRow);
            UpdateGridData(false);
            UpdateSpotsByQuarter();
            advBandedGridViewSchedule.FocusedRowHandle = targetRow;
        }

        private void OnTooltipGetActiveObjectInfo(object sender, ToolTipControllerGetActiveObjectInfoEventArgs e)
        {
            if (e.SelectedControl != gridControlSchedule) return;
            var view = gridControlSchedule.GetViewAt(e.ControlMousePosition) as GridView;
            if (view == null) return;
            var hi = view.CalcHitInfo(e.ControlMousePosition);
            if (!hi.InRowCell) return;
            if (hi.Column != bandedGridColumnLogoImage) return;
            e.Info = new ToolTipControlInfo(new CellToolTipInfo(hi.RowHandle, hi.Column, "cell"), "Double-Click to change the logo…");
            e.Info.ImmediateToolTip = true;
            e.Info.Interval = 0;
        }
        #endregion

        #region Output Stuff
        private const string TotalWeeksTag = "Total Weeks";
        private const string TotalMonthsTag = "Total Months";
        private const string TotalSpotsTag = "Total Spots";
        private const string TotalGRPTag = "Total GRPs";
        private const string TotalImpsTag = "Total IMPs";
        private const string TotalCPPTag = "Overall CPP";
        private const string TotalCPMTag = "Overall CPM";
        private const string AvgRateTag = "Average Rate";
        private const string TotalCostTag = "Gross Investment";
        private const string NetRateTag = "Net Investment";
        private const string TotalDiscountTag = "Agency Discount";

        public SlideType SlideType => MediaMetaData.Instance.DataType == MediaDataType.TVSchedule ?
            SlideType.TVSchedulePrograms :
            SlideType.RadioSchedulePrograms;

        private Theme SelectedTheme
        {
            get
            {
                return BusinessObjects.Instance.ThemeManager
                    .GetThemes(SlideType)
                    .FirstOrDefault(t =>
                        t.Name.Equals(MediaMetaData.Instance.SettingsManager.GetSelectedThemeName(SlideType)) ||
                        String.IsNullOrEmpty(MediaMetaData.Instance.SettingsManager.GetSelectedThemeName(SlideType)));
            }
        }

        private IEnumerable<OutputSpotInterval> GetSpotIntervals()
        {
            var spotIntervals = new List<OutputSpotInterval>();
            var defaultProgram = _sectionContainer.SectionData.Programs.FirstOrDefault();
            var defaultSpotsNotEmpy = defaultProgram?.SpotsNotEmpty;
            if (defaultProgram == null || defaultSpotsNotEmpy == null)
                return spotIntervals;

            var totalSpotsCount = 0;
            if (_sectionContainer.SectionData.ShowSpots)
                totalSpotsCount = _sectionContainer.SectionData.ShowEmptySpots ? defaultProgram.Spots.Count : defaultSpotsNotEmpy.Length;
            var spotsIteratorLimit = totalSpotsCount;

            if (_sectionContainer.SectionData.OutputPerQuater)
            {
                var spots = _sectionContainer.SectionData.ShowEmptySpots ? defaultProgram.Spots.ToArray() : defaultSpotsNotEmpy;
                foreach (var quarter in _sectionContainer.SectionData.ParentScheduleSettings.Quarters)
                {
                    var spotInterval = new OutputSpotInterval();
                    spotInterval.Start = spotInterval.End = spotIntervals.Any() ? spotIntervals.Last().End : 0;
                    spotInterval.End += spots.Count(s => s.Quarter == quarter);
                    spotInterval.Name = String.Format("Q{0}", quarter.QuarterNumber);
                    if (spotInterval.Start == spotInterval.End) continue;
                    spotIntervals.Add(spotInterval);
                }
            }
            else
                spotIntervals.Add(new OutputSpotInterval { Start = 0, End = spotsIteratorLimit });
            return spotIntervals;
        }

        public int GetSlidesCount(bool includeDigital)
        {
            var slidesCount = 0;
            var programsPerSlide = includeDigital ? ProgramScheduleOutputModel.MaxMediaProductsCobinedWithDigital : ProgramScheduleOutputModel.MaxSingleMediaProducts;
            programsPerSlide = _sectionContainer.SectionData.Programs.Count > programsPerSlide ? programsPerSlide : _sectionContainer.SectionData.Programs.Count;
            var spotsPerSlide = _sectionContainer.SectionData.OutputMaxPeriods ?? 26;
            var spotIntervals = GetSpotIntervals().ToList();
            foreach (var spotInterval in spotIntervals)
                for (var i = 0; i < _sectionContainer.SectionData.Programs.Count; i += programsPerSlide)
                    for (var k = spotInterval.Start; k < (spotInterval.End == 0 ? 1 : spotInterval.End); k += spotsPerSlide)
                        slidesCount++;
            return slidesCount;
        }

        public IEnumerable<ProgramScheduleOutputModel> PrepareOutput(bool includeDigital)
        {
            var outputPages = new List<ProgramScheduleOutputModel>();
            var defaultProgram = _sectionContainer.SectionData.Programs.FirstOrDefault();
            if (defaultProgram == null) return outputPages;
            var defaultSpotsNotEmpy = defaultProgram.SpotsNotEmpty;
            var programsPerSlide = includeDigital ? ProgramScheduleOutputModel.MaxMediaProductsCobinedWithDigital : ProgramScheduleOutputModel.MaxSingleMediaProducts;
            programsPerSlide = _sectionContainer.SectionData.Programs.Count > programsPerSlide ? programsPerSlide : _sectionContainer.SectionData.Programs.Count;
            var totalSpotsCount = 0;
            if (_sectionContainer.SectionData.ShowSpots)
                totalSpotsCount = _sectionContainer.SectionData.ShowEmptySpots ? defaultProgram.Spots.Count : defaultSpotsNotEmpy.Length;
            var spotsPerSlide = _sectionContainer.SectionData.OutputMaxPeriods ?? 26;
            spotsPerSlide = totalSpotsCount == 0 || totalSpotsCount > spotsPerSlide ? spotsPerSlide : totalSpotsCount;
            var spotIntervals = GetSpotIntervals().ToList();
            foreach (var spotInterval in spotIntervals)
            {
                for (var i = 0; i < _sectionContainer.SectionData.Programs.Count; i += programsPerSlide)
                {
                    for (var k = spotInterval.Start; k < (spotInterval.End == 0 ? 1 : spotInterval.End); k += spotsPerSlide)
                    {
                        var outputPage = new ProgramScheduleOutputModel(_sectionContainer.SectionData);

                        outputPage.Advertiser = _sectionContainer.SectionData.ParentScheduleSettings.BusinessName;
                        outputPage.DecisionMaker = _sectionContainer.SectionData.ParentScheduleSettings.DecisionMaker;
                        outputPage.Demo = String.Format("{0}{1}",
                                _sectionContainer.SectionData.ParentScheduleSettings.Demo,
                                !String.IsNullOrEmpty(_sectionContainer.SectionData.ParentScheduleSettings.Source) ? (" (" + _sectionContainer.SectionData.ParentScheduleSettings.Source + ")") : String.Empty);

                        outputPage.IncludeDigital = includeDigital;
                        if (includeDigital)
                        {
                            var temp = new List<string>();
                            if (_sectionContainer.SectionData.DigitalInfo.ShowMonthlyInvestemt && _sectionContainer.SectionData.DigitalInfo.MonthlyInvestment.HasValue)
                                temp.Add(String.Format("Monthly Digital Investment: {0}", _sectionContainer.SectionData.DigitalInfo.MonthlyInvestment.Value.ToString("$#,###.00")));
                            if (_sectionContainer.SectionData.DigitalInfo.ShowTotalInvestemt && _sectionContainer.SectionData.DigitalInfo.TotalInvestment.HasValue)
                                temp.Add(String.Format("Total Digital Investment: {0}", _sectionContainer.SectionData.DigitalInfo.TotalInvestment.Value.ToString("$#,###.00")));
                            outputPage.DigitalInfo.SummaryInfo = String.Join("     ", temp);
                        }
                        else
                            outputPage.DigitalInfo.SummaryInfo = String.Empty;

                        outputPage.Color = MediaMetaData.Instance.SettingsManager.SelectedColor ??
                            BusinessObjects.Instance.OutputManager.ScheduleColors.Items.Select(ci => ci.Name).FirstOrDefault();
                        outputPage.Quarter = spotInterval.Name;

                        outputPage.ProgramsPerSlide = programsPerSlide;
                        outputPage.SpotsPerSlide = totalSpotsCount > 0 ? spotsPerSlide : 0;
                        outputPage.ShowRates = _sectionContainer.SectionData.ShowRate;
                        outputPage.ShowRating = _sectionContainer.SectionData.ShowRating;
                        outputPage.ShowCPP = _sectionContainer.SectionData.ShowCPP;
                        outputPage.ShowGRP = _sectionContainer.SectionData.ShowGRP;
                        outputPage.ShowCost = _sectionContainer.SectionData.ShowCost;
                        outputPage.ShowStation = _sectionContainer.SectionData.ShowStation;
                        outputPage.ShowProgram = _sectionContainer.SectionData.ShowProgram;
                        outputPage.ShowStationInBrackets = !_sectionContainer.SectionData.OutputNoBrackets;
                        outputPage.ShowDay = _sectionContainer.SectionData.ShowDay;
                        outputPage.ShowTime = _sectionContainer.SectionData.ShowTime;
                        outputPage.ShowLength = _sectionContainer.SectionData.ShowLenght;
                        outputPage.ShowTotalSpots = _sectionContainer.SectionData.ShowTotalSpots;
                        outputPage.ShowSpots = _sectionContainer.SectionData.ShowSpots;
                        outputPage.ShowLogo = _sectionContainer.SectionData.ShowLogo;

                        #region Set Totals

                        if (_sectionContainer.SectionData.ShowTotalPeriods)
                            outputPage.Totals.Add(
                                _sectionContainer.SectionData.ParentScheduleSettings.SelectedSpotType == SpotType.Week ? TotalWeeksTag : TotalMonthsTag,
                                _sectionContainer.TotalPeriodsValueFormatted);
                        if (_sectionContainer.SectionData.ShowTotalSpots)
                            outputPage.Totals.Add(TotalSpotsTag, _sectionContainer.TotalSpotsValueFormatted);
                        if (_sectionContainer.SectionData.ShowTotalGRP)
                            outputPage.Totals.Add(
                                _sectionContainer.SectionData.ParentScheduleSettings.DemoType == DemoType.Rtg ? TotalGRPTag : TotalImpsTag,
                                _sectionContainer.TotalGRPValueFormatted);
                        if (_sectionContainer.SectionData.ShowTotalCPP)
                            outputPage.Totals.Add(
                                _sectionContainer.SectionData.ParentScheduleSettings.DemoType == DemoType.Rtg ? TotalCPPTag : TotalCPMTag,
                                _sectionContainer.TotalCPPValueFormatted);
                        if (_sectionContainer.SectionData.ShowAverageRate)
                            outputPage.Totals.Add(AvgRateTag, _sectionContainer.AvgRateValueFormatted);
                        if (_sectionContainer.SectionData.ShowTotalRate)
                            outputPage.Totals.Add(TotalCostTag, _sectionContainer.TotalCostValuesFormatted);
                        if (_sectionContainer.SectionData.ShowNetRate)
                            outputPage.Totals.Add(NetRateTag, _sectionContainer.NetRateValueFormatted);
                        if (_sectionContainer.SectionData.ShowDiscount)
                            outputPage.Totals.Add(TotalDiscountTag, _sectionContainer.TotalDiscountValueFormatted);

                        #endregion

                        #region Set OutputMediaProgram Values

                        for (var j = 0; j < programsPerSlide; j++)
                        {
                            if ((i + j) < _sectionContainer.SectionData.Programs.Count)
                            {
                                var program = _sectionContainer.SectionData.Programs[i + j];
                                var outputProgram = new ProgramOutputModel(outputPage);
                                outputProgram.Name = program.Name + (_sectionContainer.SectionData.ShowDaypart ? ("-" + program.Daypart) : string.Empty);
                                outputProgram.LineID = program.Index.ToString("00");
                                outputProgram.Station = program.Station;
                                outputProgram.Days = program.Day;
                                outputProgram.Time = program.Time;
                                outputProgram.Length = program.Length;
                                outputProgram.Rate = _sectionContainer.SectionData.ShowRate && program.Rate.HasValue ? program.Rate.Value.ToString(_sectionContainer.SectionData.UseDecimalRates ? "$#,##0.00" : "$#,##0") : string.Empty;
                                outputProgram.Rating = _sectionContainer.SectionData.ShowRating && program.Rating.HasValue ? program.Rating.Value.ToString(_sectionContainer.SectionData.ParentScheduleSettings.DemoType == DemoType.Rtg ? "#,###.0" : "#,##0") : string.Empty;
                                outputProgram.CPP = _sectionContainer.SectionData.ShowCPP ? program.CPP.ToString("$#,###.00") : String.Empty;
                                outputProgram.GRP = _sectionContainer.SectionData.ShowGRP ? program.GRP.ToString(_sectionContainer.SectionData.ParentScheduleSettings.DemoType == DemoType.Rtg ? "#,###.0" : "#,##0") : String.Empty;
                                outputProgram.TotalRate = _sectionContainer.SectionData.ShowCost ? program.Cost.ToString(_sectionContainer.SectionData.UseDecimalRates ? "$#,##0.00" : "$#,##0") : String.Empty;
                                outputProgram.TotalSpots = program.TotalSpots.ToString("#,##0");
                                outputProgram.Logo = program.Logo?.Clone<ImageSource, ImageSource>();

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

                        #region Set OutputDigitalProduct Values

                        if (includeDigital && (i + programsPerSlide) >= _sectionContainer.SectionData.Programs.Count)
                        {
                            foreach (var digitalInfoRecord in _sectionContainer.SectionData.DigitalInfo.Records)
                            {
                                var outputProduct = new DigitalInfoRecordOutputModel();
                                outputProduct.LineID = String.Format("{0}", (_sectionContainer.SectionData.Programs.Count + digitalInfoRecord.Index).ToString("00"));
                                outputProduct.Logo = _sectionContainer.SectionData.DigitalInfo.ShowLogo
                                    ? digitalInfoRecord.Logo?.Clone<ImageSource, ImageSource>()
                                    : null;
                                outputProduct.Details = digitalInfoRecord.OneSheetDetails;
                                outputPage.DigitalInfo.Records.Add(outputProduct);
                                Application.DoEvents();
                            }
                        }

                        #endregion

                        #region Set Total Values
                        for (var l = 0; l < spotsPerSlide; l++)
                        {
                            if ((k + l) < spotInterval.End)
                            {
                                var outputTotalSpot = new TotalSpotOutputModel();
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

                        outputPage.TotalCost = _sectionContainer.SectionData.TotalCost.ToString(_sectionContainer.SectionData.UseDecimalRates ? "$#,##0.00" : "$#,##0");
                        outputPage.TotalSpot = _sectionContainer.SectionData.TotalSpots.ToString("#,##0");
                        outputPage.TotalCPP = _sectionContainer.SectionData.TotalCPP.ToString("$#,###.00");
                        outputPage.TotalGRP = _sectionContainer.SectionData.TotalGRP.ToString(_sectionContainer.SectionData.ParentScheduleSettings.DemoType == DemoType.Rtg ? "#,###.0" : "#,##0");

                        #endregion

                        outputPage.GetLogos();
                        outputPage.DigitalInfo.GetLogos();
                        outputPage.PopulateReplacementsList();

                        outputPages.Add(outputPage);
                    }
                }
            }
            return outputPages;
        }

        public IList<OutputItem> GetOutputItems()
        {
            var outputItems = new List<OutputItem>();

            if (_sectionContainer.SectionData.Programs.Any())
            {
                outputItems.Add(new OutputItem
                {
                    Name = MediaMetaData.Instance.DataTypeString,
                    Enabled = BusinessObjects.Instance.OutputManager.ProgramScheduleOutputConfiguration.EnablePrograms,
                    IsCurrent = TabControl != null && TabControl.SelectedTabPage == this,
                    PresentationSourcePath = Path.Combine(Common.Core.Configuration.ResourceManager.Instance.TempFolder.LocalPath,
                        Path.GetFileName(Path.GetTempFileName())),
                    SlidesCount = GetSlidesCount(false),
                    SlideGeneratingAction = (processor, destinationPresentation) =>
                    {
                        var outputPages = PrepareOutput(false);
                        processor.AppendMediaOneSheet(outputPages, SelectedTheme, MediaMetaData.Instance.SettingsManager.UseSlideMaster,
                            destinationPresentation);
                    },
                    PreviewGeneratingAction = (processor, presentationSourcePath) =>
                    {
                        var outputPages = PrepareOutput(false);
                        processor.PrepareMediaOneSheetEmail(presentationSourcePath, outputPages, SelectedTheme,
                            MediaMetaData.Instance.SettingsManager.UseSlideMaster);
                    }
                });

                if (_sectionContainer.SectionData.DigitalInfo.Records.Any())
                    outputItems.Add(new OutputItem
                    {
                        Name = String.Format("{0} + Digital", MediaMetaData.Instance.DataTypeString),
                        Enabled = BusinessObjects.Instance.OutputManager.ProgramScheduleOutputConfiguration.EnableProgramsDigital,
                        IsCurrent = false,
                        PresentationSourcePath = Path.Combine(Common.Core.Configuration.ResourceManager.Instance.TempFolder.LocalPath,
                        Path.GetFileName(Path.GetTempFileName())),
                        SlidesCount = GetSlidesCount(true),
                        SlideGeneratingAction = (processor, destinationPresentation) =>
                        {
                            var outputPages = PrepareOutput(true);
                            processor.AppendMediaOneSheet(outputPages, SelectedTheme, MediaMetaData.Instance.SettingsManager.UseSlideMaster,
                                destinationPresentation);
                        },
                        PreviewGeneratingAction = (processor, presentationSourcePath) =>
                        {
                            var outputPages = PrepareOutput(true);
                            processor.PrepareMediaOneSheetEmail(presentationSourcePath, outputPages, SelectedTheme,
                                MediaMetaData.Instance.SettingsManager.UseSlideMaster);
                        }
                    });
            }

            return outputItems;
        }
        #endregion
    }
}