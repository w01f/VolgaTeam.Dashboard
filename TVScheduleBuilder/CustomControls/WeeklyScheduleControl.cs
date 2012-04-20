using System;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.BandedGrid;

namespace TVScheduleBuilder.CustomControls
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class WeeklyScheduleControl : UserControl
    {
        private static WeeklyScheduleControl _instance = null;
        private bool _allowTosave = false;
        private BusinessClasses.Schedule _localSchedule;
        private List<BandedGridColumn> _spotColumns = new List<BandedGridColumn>();

        public bool SettingsNotSaved { get; set; }

        private WeeklyScheduleControl()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            if ((base.CreateGraphics()).DpiX > 96)
            {
                Font font = new Font(laTotalPeriodsTitle.Font.FontFamily, laTotalPeriodsTitle.Font.Size - 2, laTotalPeriodsTitle.Font.Style);
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
            repositoryItemComboBoxDays.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            repositoryItemComboBoxDays.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            repositoryItemComboBoxDays.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
            repositoryItemComboBoxDayparts.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            repositoryItemComboBoxDayparts.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            repositoryItemComboBoxDayparts.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
            repositoryItemComboBoxLengths.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            repositoryItemComboBoxLengths.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            repositoryItemComboBoxLengths.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
            repositoryItemComboBoxPrograms.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            repositoryItemComboBoxPrograms.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            repositoryItemComboBoxPrograms.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
            repositoryItemComboBoxStations.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            repositoryItemComboBoxStations.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            repositoryItemComboBoxStations.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
            repositoryItemComboBoxTimes.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            repositoryItemComboBoxTimes.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            repositoryItemComboBoxTimes.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
            repositoryItemSpinEdit000s.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            repositoryItemSpinEdit000s.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            repositoryItemSpinEdit000s.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
            repositoryItemSpinEditRate.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            repositoryItemSpinEditRate.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            repositoryItemSpinEditRate.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
            repositoryItemSpinEditRating.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            repositoryItemSpinEditRating.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            repositoryItemSpinEditRating.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
            repositoryItemSpinEditSpot.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            repositoryItemSpinEditSpot.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            repositoryItemSpinEditSpot.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
            BusinessClasses.ScheduleManager.Instance.SettingsSaved += new EventHandler<BusinessClasses.SavingingEventArgs>((sender, e) =>
            {
                if (sender != this)
                    LoadSchedule(e.QuickSave);
            });
        }

        public static WeeklyScheduleControl Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new WeeklyScheduleControl();
                return _instance;
            }
        }

        public static void RemoveInstance()
        {
            try
            {
                _instance.Dispose();
            }
            catch
            {
            }
            finally
            {
                _instance = null;
            }
        }

        public bool AllowToLeaveControl
        {
            get
            {
                bool result = false;
                if (this.SettingsNotSaved)
                {
                    if (AppManager.ShowWarningQuestion("Schedule settings have changed.\nDo you want to save changes?") == System.Windows.Forms.DialogResult.Yes)
                    {
                        if (SaveSchedule())
                            result = true;
                    }
                }
                else
                    result = true;
                return result;
            }
        }

        #region Methods
        private void UpdateGrid(bool quickLoad)
        {
            int focussedRow = advBandedGridViewSchedule.FocusedRowHandle;
            advBandedGridViewSchedule.BeginUpdate();
            gridControlSchedule.DataSource = null;
            gridControlSchedule.DataMember = string.Empty;
            bandedGridColumnDay.FieldName = BusinessClasses.ScheduleSection.ProgramDayDataColumnName;
            bandedGridColumnDaypart.FieldName = BusinessClasses.ScheduleSection.ProgramDaypartDataColumnName;
            bandedGridColumnCPP.FieldName = BusinessClasses.ScheduleSection.ProgramCPPDataColumnName;
            bandedGridColumnCPP.SummaryItem.FieldName = BusinessClasses.ScheduleSection.ProgramTotalCPPDataColumnName;
            bandedGridColumnGRP.FieldName = BusinessClasses.ScheduleSection.ProgramGRPDataColumnName;
            bandedGridColumnGRP.SummaryItem.FieldName = BusinessClasses.ScheduleSection.ProgramGRPDataColumnName;
            bandedGridColumnIndex.FieldName = BusinessClasses.ScheduleSection.ProgramIndexDataColumnName;
            bandedGridColumnLength.FieldName = BusinessClasses.ScheduleSection.ProgramLengthDataColumnName;
            bandedGridColumnName.FieldName = BusinessClasses.ScheduleSection.ProgramNameDataColumnName;
            bandedGridColumnRate.FieldName = BusinessClasses.ScheduleSection.ProgramRateDataColumnName;
            bandedGridColumnRating.FieldName = BusinessClasses.ScheduleSection.ProgramRatingDataColumnName;
            bandedGridColumnStation.FieldName = BusinessClasses.ScheduleSection.ProgramStationDataColumnName;
            bandedGridColumnTime.FieldName = BusinessClasses.ScheduleSection.ProgramTimeDataColumnName;
            bandedGridColumnTotalSpots.FieldName = BusinessClasses.ScheduleSection.ProgramTotalSpotDataColumnName;
            bandedGridColumnTotalSpots.SummaryItem.FieldName = BusinessClasses.ScheduleSection.ProgramTotalSpotDataColumnName;
            bandedGridColumnCost.FieldName = BusinessClasses.ScheduleSection.ProgramCostDataColumnName;
            bandedGridColumnCost.SummaryItem.FieldName = BusinessClasses.ScheduleSection.ProgramCostDataColumnName;
            _localSchedule.WeeklySchedule.GenerateDataSource();
            if (!quickLoad)
                BuildSpotColumns();
            if (_localSchedule.WeeklySchedule.Programs.Count > 0)
            {
                gridControlSchedule.DataSource = _localSchedule.WeeklySchedule.DataSource;
                gridControlSchedule.DataMember = BusinessClasses.ScheduleSection.ProgramDataTableName;
            }
            advBandedGridViewSchedule.EndUpdate();
            if (focussedRow >= 0 && focussedRow < advBandedGridViewSchedule.RowCount)
                advBandedGridViewSchedule.FocusedRowHandle = focussedRow;
        }

        private void UpdateTotalsVisibility()
        {
            pnTotalCPP.Visible = _localSchedule.WeeklySchedule.ShowTotalCPP;
            pnTotalCPP.SendToBack();
            pnTotalGRP.Visible = _localSchedule.WeeklySchedule.ShowTotalGRP;
            pnTotalGRP.SendToBack();
            pnTotalSpots.Visible = _localSchedule.WeeklySchedule.ShowTotalSpots;
            pnTotalSpots.SendToBack();
            pnTotalPeriods.Visible = _localSchedule.WeeklySchedule.ShowTotalPeriods;
            pnTotalPeriods.SendToBack();
            pnAvgRate.Visible = _localSchedule.WeeklySchedule.ShowAverageRate;
            pnAvgRate.BringToFront();
            pnTotalCost.Visible = _localSchedule.WeeklySchedule.ShowTotalRate;
            pnTotalCost.BringToFront();
            pnNetRate.Visible = _localSchedule.WeeklySchedule.ShowNetRate;
            pnNetRate.BringToFront();
            pnAgencyDiscount.Visible = _localSchedule.WeeklySchedule.ShowDiscount;
            pnAgencyDiscount.BringToFront();
        }

        private void UpdateTotalsValues()
        {
            laTotalPeriodsValue.Text = _localSchedule.WeeklySchedule.TotalPeriods.ToString("#,##0");
            laTotalSpotsValue.Text = _localSchedule.WeeklySchedule.TotalSpots.ToString("#,##0");
            laTotalGRPValue.Text = _localSchedule.WeeklySchedule.TotalGRP.ToString(_localSchedule.RatingAsCPP ? "#,###.0" : "#,##0");
            laTotalCPPValue.Text = _localSchedule.WeeklySchedule.TotalCPP.ToString("$#,###.00");
            laAvgRateValue.Text = _localSchedule.WeeklySchedule.AvgRate.ToString("$#,###.00");
            laTotalCostValue.Text = _localSchedule.WeeklySchedule.TotalCost.ToString("$#,##0");
            laNetRateValue.Text = _localSchedule.WeeklySchedule.NetRate.ToString("$#,###.00");
            laAgencyDiscountValue.Text = _localSchedule.WeeklySchedule.Discount.ToString("$#,###.00");
        }

        private void BuildSpotColumns()
        {
            foreach (BandedGridColumn column in _spotColumns)
            {
                gridBandSpots.Columns.Remove(column);
                advBandedGridViewSchedule.Columns.Remove(column);
            }

            _spotColumns.Clear();

            gridBandSpots.Visible = false;

            foreach (DataColumn column in _localSchedule.WeeklySchedule.DataSource.Tables[BusinessClasses.ScheduleSection.ProgramDataTableName].Columns)
            {
                if (column.ColumnName.Contains(BusinessClasses.ScheduleSection.ProgramSpotDataColumnNamePrefix))
                {
                    BandedGridColumn bandedGridColumn = new BandedGridColumn();
                    bandedGridColumn.AppearanceCell.Options.UseTextOptions = true;
                    bandedGridColumn.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    bandedGridColumn.AutoFillDown = true;
                    bandedGridColumn.Caption = column.Caption;
                    bandedGridColumn.ColumnEdit = this.repositoryItemSpinEditSpot;
                    bandedGridColumn.FieldName = column.ColumnName;
                    bandedGridColumn.OptionsColumn.FixedWidth = true;
                    bandedGridColumn.RowCount = 2;
                    bandedGridColumn.Width = 45;
                    bandedGridColumn.Visible = true;
                    bandedGridColumn.SummaryItem.FieldName = column.ColumnName;
                    bandedGridColumn.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                    _spotColumns.Add(bandedGridColumn);
                    advBandedGridViewSchedule.Columns.Add(bandedGridColumn);
                    gridBandSpots.Columns.Add(bandedGridColumn);
                }
            }
            gridBandSpots.Visible = _spotColumns.Count > 0 && _localSchedule.WeeklySchedule.ShowSpots;
        }

        public void LoadSchedule(bool quickLoad)
        {
            _allowTosave = false;

            _localSchedule = BusinessClasses.ScheduleManager.Instance.GetLocalSchedule();
            _localSchedule.WeeklySchedule.DataChanged += new EventHandler<EventArgs>(WeeklySchedule_DataChanged);

            laAdvertiser.Text = _localSchedule.BusinessName;
            laDemoName.Text = _localSchedule.Demo + (!string.IsNullOrEmpty(_localSchedule.Source) ? (" (" + _localSchedule.Source + ")") : string.Empty);
            laScheduleWindow.Text = _localSchedule.FlightDateStart.HasValue && _localSchedule.FlightDateEnd.HasValue ? string.Format("{0} - {1}", new object[] { _localSchedule.FlightDateStart.Value.ToString("MM/dd/yy"), _localSchedule.FlightDateEnd.Value.ToString("MM/dd/yy") }) : string.Empty;
            laScheduleName.Text = _localSchedule.Name;
            laDemoName.Visible = !string.IsNullOrEmpty(_localSchedule.Demo);
            laDemoName.SendToBack();
            laAdvertiser.SendToBack();

            FormMain.Instance.buttonItemWeeklyScheduleRating.Enabled = _localSchedule.UseDemo;
            FormMain.Instance.buttonItemWeeklyScheduleCPP.Enabled = _localSchedule.UseDemo;
            FormMain.Instance.buttonItemWeeklyScheduleGRP.Enabled = _localSchedule.UseDemo;
            FormMain.Instance.buttonItemWeeklyScheduleTotalCPP.Enabled = _localSchedule.UseDemo;
            FormMain.Instance.buttonItemWeeklyScheduleTotalGRP.Enabled = _localSchedule.UseDemo;
            FormMain.Instance.buttonItemWeeklyScheduleRating.Text = _localSchedule.RatingAsCPP ? "Rtg" : "(000s)";
            bandedGridColumnRating.Caption = _localSchedule.RatingAsCPP ? "Rtg" : "(000s)";
            bandedGridColumnRating.ColumnEdit = _localSchedule.RatingAsCPP ? repositoryItemSpinEditRating : repositoryItemSpinEdit000s;
            FormMain.Instance.buttonItemWeeklyScheduleCPP.Text = _localSchedule.RatingAsCPP ? "CPP" : "CPM";
            FormMain.Instance.buttonItemWeeklyScheduleTotalCPP.Text = _localSchedule.RatingAsCPP ? "Overall CPP" : "Overall CPM";
            laTotalCPPTitle.Text = _localSchedule.RatingAsCPP ? "Overall CPP:" : "Overall CPM:";
            bandedGridColumnCPP.Caption = _localSchedule.RatingAsCPP ? "CPP" : "CPM";
            FormMain.Instance.buttonItemWeeklyScheduleGRP.Text = _localSchedule.RatingAsCPP ? "GRPs" : "Impr";
            FormMain.Instance.buttonItemWeeklyScheduleTotalGRP.Text = _localSchedule.RatingAsCPP ? "Total GRPs" : "Total Impr";
            laTotalGRPTitle.Text = _localSchedule.RatingAsCPP ? "Total GRPs:" : "Total Impr:";
            bandedGridColumnGRP.Caption = _localSchedule.RatingAsCPP ? "GRPs" : "Impr";
            bandedGridColumnGRP.ColumnEdit = _localSchedule.RatingAsCPP ? repositoryItemSpinEditRating : repositoryItemSpinEdit000s;
            bandedGridColumnGRP.SummaryItem.DisplayFormat = _localSchedule.RatingAsCPP ? "{0:n1}" : "{0:n0}";
            FormMain.Instance.ribbonBarWeeklyScheduleLineOptions.RecalcLayout();
            FormMain.Instance.ribbonBarWeeklyScheduleTotals.RecalcLayout();
            FormMain.Instance.ribbonPanelWeeklySchedule.PerformLayout();

            FormMain.Instance.buttonItemWeeklyScheduleRate.Checked = _localSchedule.WeeklySchedule.ShowRate;
            FormMain.Instance.buttonItemWeeklyScheduleRating.Checked = _localSchedule.WeeklySchedule.ShowRating;
            FormMain.Instance.buttonItemWeeklyScheduleCost.Checked = _localSchedule.WeeklySchedule.ShowCost;
            FormMain.Instance.buttonItemWeeklyScheduleCPP.Checked = _localSchedule.WeeklySchedule.ShowCPP;
            FormMain.Instance.buttonItemWeeklyScheduleDay.Checked = _localSchedule.WeeklySchedule.ShowDay;
            FormMain.Instance.buttonItemWeeklyScheduleDaypart.Checked = _localSchedule.WeeklySchedule.ShowDaypart;
            FormMain.Instance.buttonItemWeeklyScheduleGRP.Checked = _localSchedule.WeeklySchedule.ShowGRP;
            FormMain.Instance.buttonItemWeeklyScheduleLength.Checked = _localSchedule.WeeklySchedule.ShowLenght;
            FormMain.Instance.buttonItemWeeklyScheduleStation.Checked = _localSchedule.WeeklySchedule.ShowStation;
            FormMain.Instance.buttonItemWeeklyScheduleTime.Checked = _localSchedule.WeeklySchedule.ShowTime;
            FormMain.Instance.buttonItemWeeklyScheduleSpots.Checked = _localSchedule.WeeklySchedule.ShowSpots;

            FormMain.Instance.buttonItemWeeklyScheduleTotalPeriods.Checked = _localSchedule.WeeklySchedule.ShowTotalPeriods;
            FormMain.Instance.buttonItemWeeklyScheduleTotalSpots.Checked = _localSchedule.WeeklySchedule.ShowTotalSpots;
            FormMain.Instance.buttonItemWeeklyScheduleTotalGRP.Checked = _localSchedule.WeeklySchedule.ShowTotalGRP;
            FormMain.Instance.buttonItemWeeklyScheduleTotalCPP.Checked = _localSchedule.WeeklySchedule.ShowTotalCPP;
            FormMain.Instance.buttonItemWeeklyScheduleAvgRate.Checked = _localSchedule.WeeklySchedule.ShowAverageRate;
            FormMain.Instance.buttonItemWeeklyScheduleTotalCost.Checked = _localSchedule.WeeklySchedule.ShowTotalRate;
            FormMain.Instance.buttonItemWeeklyScheduleNetRate.Checked = _localSchedule.WeeklySchedule.ShowNetRate;
            FormMain.Instance.buttonItemWeeklyScheduleDiscount.Checked = _localSchedule.WeeklySchedule.ShowDiscount;

            bandedGridColumnRate.Visible = _localSchedule.WeeklySchedule.ShowRate;
            bandedGridColumnRating.Visible = _localSchedule.WeeklySchedule.ShowRating;
            gridBandRate.Visible = _localSchedule.WeeklySchedule.ShowRate | _localSchedule.WeeklySchedule.ShowRating;
            if (_localSchedule.WeeklySchedule.ShowRate)
                advBandedGridViewSchedule.SetColumnPosition(bandedGridColumnRate, 0, 0);
            if (_localSchedule.WeeklySchedule.ShowRating)
                advBandedGridViewSchedule.SetColumnPosition(bandedGridColumnRating, 1, 0);


            bandedGridColumnCPP.Visible = _localSchedule.WeeklySchedule.ShowCPP;
            bandedGridColumnGRP.Visible = _localSchedule.WeeklySchedule.ShowGRP;
            bandedGridColumnCost.Visible = _localSchedule.WeeklySchedule.ShowCost;

            bandedGridColumnLength.Visible = _localSchedule.WeeklySchedule.ShowLenght;
            gridBandLength.Visible = _localSchedule.WeeklySchedule.ShowLenght;

            bandedGridColumnDay.Visible = _localSchedule.WeeklySchedule.ShowDay;
            bandedGridColumnTime.Visible = _localSchedule.WeeklySchedule.ShowTime;
            gridBandDate.Visible = _localSchedule.WeeklySchedule.ShowDay | _localSchedule.WeeklySchedule.ShowTime;
            if (_localSchedule.WeeklySchedule.ShowDay)
                advBandedGridViewSchedule.SetColumnPosition(bandedGridColumnDay, 0, 0);
            if (_localSchedule.WeeklySchedule.ShowTime)
                advBandedGridViewSchedule.SetColumnPosition(bandedGridColumnTime, 1, 0);

            bandedGridColumnStation.Visible = _localSchedule.WeeklySchedule.ShowStation;
            bandedGridColumnDaypart.Visible = _localSchedule.WeeklySchedule.ShowDaypart;
            gridBandSpots.Visible = _spotColumns.Count > 0 && _localSchedule.WeeklySchedule.ShowSpots;


            repositoryItemComboBoxStations.Items.Clear();
            repositoryItemComboBoxStations.Items.AddRange(_localSchedule.Stations.Where(x => x.Available).Select(x => x.Name).ToArray());
            repositoryItemComboBoxDayparts.Items.Clear();
            repositoryItemComboBoxDayparts.Items.AddRange(_localSchedule.Dayparts.Where(x => x.Available).Select(x => x.Code).ToArray());


            if (!quickLoad)
            {
                repositoryItemComboBoxPrograms.Items.Clear();
                repositoryItemComboBoxPrograms.Items.AddRange(BusinessClasses.ListManager.Instance.ProgramNames);
                repositoryItemComboBoxLengths.Items.Clear();
                repositoryItemComboBoxLengths.Items.AddRange(BusinessClasses.ListManager.Instance.Lengths);
                repositoryItemComboBoxDays.Items.Clear();
                repositoryItemComboBoxDays.Items.AddRange(BusinessClasses.ListManager.Instance.Days);
                repositoryItemComboBoxTimes.Items.Clear();
                repositoryItemComboBoxTimes.Items.AddRange(BusinessClasses.ListManager.Instance.Times);

                UpdateTotalsVisibility();
            }

            UpdateGrid(quickLoad);
            UpdateTotalsValues();

            _allowTosave = true;
            this.SettingsNotSaved = false;
        }

        private bool SaveSchedule(string scheduleName = "")
        {
            if (!string.IsNullOrEmpty(scheduleName))
                _localSchedule.Name = scheduleName;
            advBandedGridViewSchedule.CloseEditor();
            BusinessClasses.ScheduleManager.Instance.SaveSchedule(_localSchedule, true, this);
            LoadSchedule(true);
            laScheduleName.Text = _localSchedule.Name;
            this.SettingsNotSaved = false;
            return true;
        }

        public void WeeklySchedule_DataChanged(object sender, EventArgs e)
        {
            UpdateTotalsValues();
        }

        private void AssignCloseActiveEditorsonOutSideClick(Control control)
        {
            if (control != FormMain.Instance.comboBoxEditBusinessName
                && control != FormMain.Instance.comboBoxEditClientType
                && control != FormMain.Instance.comboBoxEditDecisionMaker
                && control != FormMain.Instance.comboBoxEditDemo
                && control != FormMain.Instance.comboBoxEditSource
                && control != FormMain.Instance.dateEditFlightDatesEnd
                && control != FormMain.Instance.dateEditFlightDatesStart
                && control != FormMain.Instance.dateEditPresentationDate
                && control != FormMain.Instance.comboBoxEditDemo)
            {
                control.Click += new EventHandler(CloseActiveEditorsonOutSideClick);
                foreach (Control childControl in control.Controls)
                    AssignCloseActiveEditorsonOutSideClick(childControl);
            }
        }

        private void CloseActiveEditorsonOutSideClick(object sender, EventArgs e)
        {
            this.Focus();
            advBandedGridViewSchedule.CloseEditor();
            advBandedGridViewSchedule.FocusedColumn = null;
        }
        #endregion

        private void ScheduleControl_Load(object sender, EventArgs e)
        {
            AssignCloseActiveEditorsonOutSideClick(FormMain.Instance.ribbonControl);
            AssignCloseActiveEditorsonOutSideClick(pnBottom);
            AssignCloseActiveEditorsonOutSideClick(pnTop);
        }

        #region Ribbon Operations Events
        public void buttonItemScheduleHelp_Click(object sender, EventArgs e)
        {
            BusinessClasses.HelpManager.Instance.OpenHelpLink("week");
        }

        public void buttonItemScheduleSave_Click(object sender, EventArgs e)
        {
            if (SaveSchedule())
                AppManager.ShowInformation("Schedule Saved");
        }

        public void buttonItemScheduleSaveAs_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.InitialDirectory = ConfigurationClasses.SettingsManager.Instance.SaveFolder;
                dialog.Title = "Save Schedule As...";
                dialog.Filter = "Schedule Files|*.xml";
                dialog.FileName = _localSchedule.Name + ".xml";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    if (SaveSchedule(dialog.FileName.Replace(".xml", "")))
                        AppManager.ShowInformation("Schedule was saved");
                }
            }
        }

        public void buttonItemScheduleAdd_Click(object sender, EventArgs e)
        {
            _localSchedule.WeeklySchedule.AddProgram();
            UpdateGrid(false);
            UpdateTotalsValues();
            if (advBandedGridViewSchedule.RowCount > 0)
                advBandedGridViewSchedule.FocusedRowHandle = advBandedGridViewSchedule.RowCount - 1;
            this.SettingsNotSaved = true;
        }

        public void buttonItemScheduleDelete_Click(object sender, EventArgs e)
        {
            _localSchedule.WeeklySchedule.DeleteProgram(advBandedGridViewSchedule.GetDataSourceRowIndex(advBandedGridViewSchedule.FocusedRowHandle));
            UpdateGrid(false);
            UpdateTotalsValues();
            this.SettingsNotSaved = true;
        }
        #endregion

        #region Toggle Switch Events
        public void button_CheckedChanged(object sender, EventArgs e)
        {
            if (_allowTosave)
            {
                _localSchedule.WeeklySchedule.ShowRate = FormMain.Instance.buttonItemWeeklyScheduleRate.Checked;
                _localSchedule.WeeklySchedule.ShowRating = FormMain.Instance.buttonItemWeeklyScheduleRating.Checked;
                _localSchedule.WeeklySchedule.ShowCost = FormMain.Instance.buttonItemWeeklyScheduleCost.Checked;
                _localSchedule.WeeklySchedule.ShowCPP = FormMain.Instance.buttonItemWeeklyScheduleCPP.Checked;
                _localSchedule.WeeklySchedule.ShowDay = FormMain.Instance.buttonItemWeeklyScheduleDay.Checked;
                _localSchedule.WeeklySchedule.ShowDaypart = FormMain.Instance.buttonItemWeeklyScheduleDaypart.Checked;
                _localSchedule.WeeklySchedule.ShowGRP = FormMain.Instance.buttonItemWeeklyScheduleGRP.Checked;
                _localSchedule.WeeklySchedule.ShowLenght = FormMain.Instance.buttonItemWeeklyScheduleLength.Checked;
                _localSchedule.WeeklySchedule.ShowStation = FormMain.Instance.buttonItemWeeklyScheduleStation.Checked;
                _localSchedule.WeeklySchedule.ShowTime = FormMain.Instance.buttonItemWeeklyScheduleTime.Checked;
                _localSchedule.WeeklySchedule.ShowSpots = FormMain.Instance.buttonItemWeeklyScheduleSpots.Checked;

                _localSchedule.WeeklySchedule.ShowTotalPeriods = FormMain.Instance.buttonItemWeeklyScheduleTotalPeriods.Checked;
                _localSchedule.WeeklySchedule.ShowTotalSpots = FormMain.Instance.buttonItemWeeklyScheduleTotalSpots.Checked;
                _localSchedule.WeeklySchedule.ShowTotalGRP = FormMain.Instance.buttonItemWeeklyScheduleTotalGRP.Checked;
                _localSchedule.WeeklySchedule.ShowTotalCPP = FormMain.Instance.buttonItemWeeklyScheduleTotalCPP.Checked;
                _localSchedule.WeeklySchedule.ShowAverageRate = FormMain.Instance.buttonItemWeeklyScheduleAvgRate.Checked;
                _localSchedule.WeeklySchedule.ShowTotalRate = FormMain.Instance.buttonItemWeeklyScheduleTotalCost.Checked;
                _localSchedule.WeeklySchedule.ShowNetRate = FormMain.Instance.buttonItemWeeklyScheduleNetRate.Checked;
                _localSchedule.WeeklySchedule.ShowDiscount = FormMain.Instance.buttonItemWeeklyScheduleDiscount.Checked;

                bandedGridColumnRate.Visible = _localSchedule.WeeklySchedule.ShowRate;
                bandedGridColumnRating.Visible = _localSchedule.WeeklySchedule.ShowRating;
                gridBandRate.Visible = _localSchedule.WeeklySchedule.ShowRate | _localSchedule.WeeklySchedule.ShowRating;
                if (_localSchedule.WeeklySchedule.ShowRate)
                    advBandedGridViewSchedule.SetColumnPosition(bandedGridColumnRate, 0, 0);
                if (_localSchedule.WeeklySchedule.ShowRating)
                    advBandedGridViewSchedule.SetColumnPosition(bandedGridColumnRating, 1, 0);

                bandedGridColumnCPP.Visible = _localSchedule.WeeklySchedule.ShowCPP;
                bandedGridColumnGRP.Visible = _localSchedule.WeeklySchedule.ShowGRP;
                bandedGridColumnCost.Visible = _localSchedule.WeeklySchedule.ShowCost;

                bandedGridColumnLength.Visible = _localSchedule.WeeklySchedule.ShowLenght;
                gridBandLength.Visible = _localSchedule.WeeklySchedule.ShowLenght;

                bandedGridColumnDay.Visible = _localSchedule.WeeklySchedule.ShowDay;
                bandedGridColumnTime.Visible = _localSchedule.WeeklySchedule.ShowTime;
                gridBandDate.Visible = _localSchedule.WeeklySchedule.ShowDay | _localSchedule.WeeklySchedule.ShowTime;
                if (_localSchedule.WeeklySchedule.ShowDay)
                    advBandedGridViewSchedule.SetColumnPosition(bandedGridColumnDay, 0, 0);
                if (_localSchedule.WeeklySchedule.ShowTime)
                    advBandedGridViewSchedule.SetColumnPosition(bandedGridColumnTime, 1, 0);

                bandedGridColumnStation.Visible = _localSchedule.WeeklySchedule.ShowStation;
                bandedGridColumnDaypart.Visible = _localSchedule.WeeklySchedule.ShowDaypart;
                gridBandSpots.Visible = _spotColumns.Count > 0 && _localSchedule.WeeklySchedule.ShowSpots;

                UpdateTotalsVisibility();

                this.SettingsNotSaved = true;
            }
        }
        #endregion

        #region Grid Events
        private void advBandedGridViewSchedule_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == bandedGridColumnName && e.Value != null)
            {
                string programName = e.Value.ToString();
                BusinessClasses.SourceProgram program = BusinessClasses.ListManager.Instance.SourcePrograms.Where(x => x.Name.Equals(programName)).FirstOrDefault();
                if (program != null)
                {
                    if (advBandedGridViewSchedule.GetRowCellValue(advBandedGridViewSchedule.FocusedRowHandle, bandedGridColumnDaypart) == null || string.IsNullOrEmpty(advBandedGridViewSchedule.GetRowCellValue(advBandedGridViewSchedule.FocusedRowHandle, bandedGridColumnDaypart).ToString()))
                        advBandedGridViewSchedule.SetRowCellValue(e.RowHandle, bandedGridColumnDaypart, program.Daypart);
                    advBandedGridViewSchedule.SetRowCellValue(e.RowHandle, bandedGridColumnDay, program.Day);
                    advBandedGridViewSchedule.SetRowCellValue(e.RowHandle, bandedGridColumnTime, program.Time);
                    if (string.IsNullOrEmpty(advBandedGridViewSchedule.GetRowCellValue(e.RowHandle, bandedGridColumnLength).ToString()))
                        advBandedGridViewSchedule.SetRowCellValue(e.RowHandle, bandedGridColumnLength, BusinessClasses.ListManager.Instance.Lengths.FirstOrDefault());
                    if (_localSchedule.ImportDemo && _localSchedule.UseDemo)
                    {
                        BusinessClasses.Demo demo = program.Demos.Where(x => x.Name.Equals(_localSchedule.Demo)).FirstOrDefault();
                        if (demo != null)
                            advBandedGridViewSchedule.SetRowCellValue(e.RowHandle, bandedGridColumnRating, demo.Value);
                    }
                }
            }
            advBandedGridViewSchedule.UpdateCurrentRow();
            this.SettingsNotSaved = true;
        }

        private void advBandedGridViewSchedule_CustomDrawFooter(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
        {
            if (_spotColumns.Count > 0)
            {
                e.Painter.DrawObject(e.Info);
                DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView view = sender as DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView;
                DevExpress.XtraGrid.Views.BandedGrid.ViewInfo.AdvBandedGridViewInfo viewInfo = view.GetViewInfo() as DevExpress.XtraGrid.Views.BandedGrid.ViewInfo.AdvBandedGridViewInfo;
                BandedGridColumn column = bandedGridColumnName;
                if (_localSchedule.WeeklySchedule.ShowRate)
                    column = bandedGridColumnRate;
                else if (_localSchedule.WeeklySchedule.ShowRating)
                    column = bandedGridColumnRating;
                else if (_localSchedule.WeeklySchedule.ShowLenght)
                    column = bandedGridColumnLength;
                else if (_localSchedule.WeeklySchedule.ShowDay)
                    column = bandedGridColumnDay;
                else if (_localSchedule.WeeklySchedule.ShowTime)
                    column = bandedGridColumnTime;
                int x = viewInfo.ColumnsInfo[column].Bounds.X;
                int width = viewInfo.ColumnsInfo[column].Bounds.Width;
                string spotTotalTitle = "Totals: ";
                SizeF size = e.Appearance.CalcTextSize(e.Cache, spotTotalTitle, 50);
                int textWidth = Convert.ToInt32(size.Width) + 1;
                Rectangle textRect = new Rectangle(x + width - 50, e.Bounds.Y, textWidth, e.Bounds.Height);
                e.Appearance.DrawString(e.Cache, spotTotalTitle, textRect);
                e.Handled = true;
            }
        }

        private void advBandedGridViewSchedule_ShowingEditor(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (advBandedGridViewSchedule.FocusedColumn == bandedGridColumnName && advBandedGridViewSchedule.FocusedRowHandle >= 0)
            {
                repositoryItemComboBoxPrograms.Items.Clear();
                object station = advBandedGridViewSchedule.GetRowCellValue(advBandedGridViewSchedule.FocusedRowHandle, bandedGridColumnStation);
                object daypart = advBandedGridViewSchedule.GetRowCellValue(advBandedGridViewSchedule.FocusedRowHandle, bandedGridColumnDaypart);
                if (station != null && (daypart != null || !_localSchedule.WeeklySchedule.ShowDaypart))
                    repositoryItemComboBoxPrograms.Items.AddRange(BusinessClasses.ListManager.Instance.SourcePrograms.Where(x => (x.Station.Equals(station.ToString()) || string.IsNullOrEmpty(station.ToString())) && (!_localSchedule.WeeklySchedule.ShowDaypart || (x.Daypart.Equals(daypart.ToString()) || string.IsNullOrEmpty(daypart.ToString())))).Select(x => x.Name).Distinct().ToArray());
                else
                    repositoryItemComboBoxPrograms.Items.AddRange(BusinessClasses.ListManager.Instance.SourcePrograms.Select(x => x.Name).Distinct().ToArray());
            }
        }

        private void repositoryItemComboBoxPrograms_Closed(object sender, DevExpress.XtraEditors.Controls.ClosedEventArgs e)
        {
            advBandedGridViewSchedule.CloseEditor();
        }

        private void advBandedGridViewSchedule_MouseDown(object sender, MouseEventArgs e)
        {
            AdvBandedGridView view = sender as AdvBandedGridView;
            if (view != null)
            {
                DevExpress.XtraGrid.Views.BandedGrid.ViewInfo.BandedGridHitInfo hInfo = view.CalcHitInfo(e.Location);
                if (hInfo.HitTest != DevExpress.XtraGrid.Views.BandedGrid.ViewInfo.BandedGridHitTest.RowCell)
                    CloseActiveEditorsonOutSideClick(null, null);
            }
        }
        #endregion

        #region Output Staff
        private BusinessClasses.OutputSchedule[] PrepareOutputScheduleExcelBased()
        {
            List<BusinessClasses.OutputSchedule> outputPages = new List<BusinessClasses.OutputSchedule>();

            int programsPerSlide = 10;
            programsPerSlide = _localSchedule.WeeklySchedule.Programs.Count > programsPerSlide ? programsPerSlide : _localSchedule.WeeklySchedule.Programs.Count;

            int totalSpotsCount = 0;
            if (FormMain.Instance.buttonItemWeeklyScheduleSpots.Checked)
            {
                BusinessClasses.Program defaultProgram = _localSchedule.WeeklySchedule.Programs.FirstOrDefault();
                if (defaultProgram != null)
                    totalSpotsCount = defaultProgram.Spots.Count;
            }
            for (int i = 0; i < _localSchedule.WeeklySchedule.Programs.Count; i += programsPerSlide)
            {
                for (int k = 0; k < (totalSpotsCount == 0 ? 1 : totalSpotsCount); k += 13)
                {
                    BusinessClasses.OutputSchedule outputPage = new BusinessClasses.OutputSchedule(_localSchedule);
                    outputPage.Advertiser = _localSchedule.BusinessName;
                    outputPage.DecisionMaker = _localSchedule.DecisionMaker;
                    outputPage.Demo = _localSchedule.Demo + (!string.IsNullOrEmpty(_localSchedule.Source) ? (" (" + _localSchedule.Source + ")") : string.Empty);

                    outputPage.ProgramsPerSlide = programsPerSlide;
                    outputPage.SpotsPerSlide = totalSpotsCount > 0 ? (totalSpotsCount > 13 ? (((k + 1) * 13) < totalSpotsCount ? 13 : (totalSpotsCount - k + 13)) : totalSpotsCount) : 0;
                    outputPage.ShowRates = FormMain.Instance.buttonItemWeeklyScheduleRate.Checked;
                    outputPage.ShowRating = FormMain.Instance.buttonItemWeeklyScheduleRating.Checked;
                    outputPage.ShowCPP = FormMain.Instance.buttonItemWeeklyScheduleCPP.Checked;
                    outputPage.ShowGRP = FormMain.Instance.buttonItemWeeklyScheduleGRP.Checked;
                    outputPage.ShowCost = FormMain.Instance.buttonItemWeeklyScheduleCost.Checked;
                    outputPage.ShowSpots = FormMain.Instance.buttonItemWeeklyScheduleSpots.Checked;
                    outputPage.ShowStation = FormMain.Instance.buttonItemWeeklyScheduleStation.Checked;
                    outputPage.ShowDaypart = FormMain.Instance.buttonItemWeeklyScheduleDaypart.Checked;
                    outputPage.ShowDay = FormMain.Instance.buttonItemWeeklyScheduleDay.Checked;
                    outputPage.ShowTime = FormMain.Instance.buttonItemWeeklyScheduleTime.Checked;
                    outputPage.ShowLength = FormMain.Instance.buttonItemWeeklyScheduleLength.Checked;
                    outputPage.ShowTotalSpots = FormMain.Instance.buttonItemWeeklyScheduleTotalSpots.Checked;
                    outputPage.ShowTotalInvestment = FormMain.Instance.buttonItemWeeklyScheduleTotalCost.Checked;
                    outputPage.ShowDiscount = FormMain.Instance.buttonItemWeeklyScheduleDiscount.Checked;
                    outputPage.ShowNetCost = FormMain.Instance.buttonItemWeeklyScheduleNetRate.Checked;

                    #region Set Totals
                    if (FormMain.Instance.buttonItemWeeklyScheduleTotalPeriods.Checked)
                        outputPage.Totals.Add(laTotalPeriodsTitle.Text, laTotalPeriodsValue.Text);
                    if (FormMain.Instance.buttonItemWeeklyScheduleTotalSpots.Checked)
                        outputPage.Totals.Add(laTotalSpotsTitle.Text, laTotalSpotsValue.Text);
                    if (FormMain.Instance.buttonItemWeeklyScheduleTotalGRP.Checked)
                        outputPage.Totals.Add(laTotalGRPTitle.Text, laTotalGRPValue.Text);
                    if (FormMain.Instance.buttonItemWeeklyScheduleTotalCPP.Checked)
                        outputPage.Totals.Add(laTotalCPPTitle.Text, laTotalCPPValue.Text);
                    if (FormMain.Instance.buttonItemWeeklyScheduleAvgRate.Checked)
                        outputPage.Totals.Add(laAvgRateTitle.Text, laAvgRateValue.Text);
                    if (FormMain.Instance.buttonItemWeeklyScheduleTotalCost.Checked)
                        outputPage.Totals.Add(laTotalCostTitle.Text, laTotalCostValue.Text);
                    if (FormMain.Instance.buttonItemWeeklyScheduleNetRate.Checked)
                        outputPage.Totals.Add(laNetRateTitle.Text, laNetRateValue.Text);
                    if (FormMain.Instance.buttonItemWeeklyScheduleDiscount.Checked)
                        outputPage.Totals.Add(laAgencyDiscountTitle.Text, laAgencyDiscountValue.Text);
                    #endregion

                    #region Set OutputProgram Values
                    for (int j = 0; j < programsPerSlide; j++)
                    {
                        if ((i + j) < _localSchedule.WeeklySchedule.Programs.Count)
                        {
                            BusinessClasses.Program program = _localSchedule.WeeklySchedule.Programs[i + j];
                            BusinessClasses.OutputProgram outputProgram = new BusinessClasses.OutputProgram(outputPage);
                            outputProgram.Name = program.Name + (FormMain.Instance.buttonItemWeeklyScheduleDaypart.Checked ? ("-" + program.Daypart) : string.Empty);
                            outputProgram.LineID = program.Index.ToString();
                            outputProgram.Station = program.Station;
                            outputProgram.Days = program.Day;
                            outputProgram.Time = program.Time;
                            outputProgram.Length = program.Length;
                            outputProgram.Rate = FormMain.Instance.buttonItemWeeklyScheduleRate.Checked && program.Rate.HasValue ? program.Rate.Value.ToString("$#,##0") : string.Empty;
                            outputProgram.Rating = FormMain.Instance.buttonItemWeeklyScheduleRating.Checked && program.Rating.HasValue ? program.Rating.Value.ToString(_localSchedule.RatingAsCPP ? "#,###.0" : "#,##0") : string.Empty;
                            outputProgram.CPP = FormMain.Instance.buttonItemWeeklyScheduleCPP.Checked ? program.CPP.ToString("$#,###.00") : string.Empty;
                            outputProgram.GRP = FormMain.Instance.buttonItemWeeklyScheduleGRP.Checked ? program.GRP.ToString(_localSchedule.RatingAsCPP ? "#,###.0" : "#,##0") : string.Empty;
                            outputProgram.TotalRate = FormMain.Instance.buttonItemWeeklyScheduleCost.Checked ? program.Cost.ToString("$#,##0") : string.Empty;
                            outputProgram.TotalSpots = program.TotalSpots.ToString("#,##0");

                            #region Set Spots Values
                            for (int l = 0; l < 13; l++)
                            {
                                if ((k + l) < totalSpotsCount)
                                {
                                    string value = program.Spots[k + l].Count > 0 ? program.Spots[k + l].Count.ToString() : "-";
                                    outputProgram.Spots.Add(value);
                                }
                                else
                                    break;
                            }
                            #endregion

                            outputPage.Programs.Add(outputProgram);
                        }
                        else
                            break;
                    }
                    #endregion

                    #region Set Total Values
                    BusinessClasses.Program defaultProgram = _localSchedule.WeeklySchedule.Programs.FirstOrDefault();
                    if (defaultProgram != null)
                    {

                        for (int l = 0; l < 13; l++)
                        {
                            if ((k + l) < totalSpotsCount)
                            {
                                BusinessClasses.OutputTotalSpot outputTotalSpot = new BusinessClasses.OutputTotalSpot();
                                outputTotalSpot.Day = defaultProgram.Spots[k + l].Date.Day.ToString();
                                outputTotalSpot.Month = BusinessClasses.Spot.GetMonthAbbreviation(defaultProgram.Spots[k + l].Date.Month);
                                int sum = _localSchedule.WeeklySchedule.Programs.Select(x => x.Spots.Where(y => y.Date.Equals(defaultProgram.Spots[k + l].Date)).FirstOrDefault()).Where(z => z.Count.HasValue).Select(z => z.Count.Value).Sum();
                                outputTotalSpot.Value = sum > 0 ? sum.ToString() : "-";
                                outputPage.TotalSpots.Add(outputTotalSpot);
                            }
                            else
                                break;
                        }
                    }
                    outputPage.TotalCost = _localSchedule.WeeklySchedule.TotalCost.ToString("$#,##0");
                    outputPage.TotalSpot = _localSchedule.WeeklySchedule.TotalSpots.ToString("#,##0");
                    outputPage.TotalCPP = _localSchedule.WeeklySchedule.TotalCPP.ToString("$#,###.00");
                    outputPage.TotalGRP = _localSchedule.WeeklySchedule.TotalGRP.ToString(_localSchedule.RatingAsCPP ? "#,###.0" : "#,##0");
                    #endregion

                    outputPages.Add(outputPage);
                }
            }
            return outputPages.ToArray();
        }

        private BusinessClasses.OutputSchedule PrepareOutputScheduleGridBased()
        {
            BusinessClasses.OutputSchedule outputPage = new BusinessClasses.OutputSchedule(_localSchedule);
            BusinessClasses.Program defaultProgram = _localSchedule.WeeklySchedule.Programs.FirstOrDefault();

            int totalSpotsCount = 0;
            if (FormMain.Instance.buttonItemWeeklyScheduleSpots.Checked)
                if (defaultProgram != null)
                    totalSpotsCount = defaultProgram.Spots.Count;

            outputPage.Advertiser = _localSchedule.BusinessName;
            outputPage.DecisionMaker = _localSchedule.DecisionMaker;
            outputPage.Demo = _localSchedule.Demo + (!string.IsNullOrEmpty(_localSchedule.Source) ? (" (" + _localSchedule.Source + ")") : string.Empty);

            outputPage.ProgramsPerSlide = _localSchedule.WeeklySchedule.Programs.Count;
            outputPage.SpotsPerSlide = totalSpotsCount;
            outputPage.ShowRates = FormMain.Instance.buttonItemWeeklyScheduleRate.Checked;
            outputPage.ShowRating = FormMain.Instance.buttonItemWeeklyScheduleRating.Checked;
            outputPage.ShowCPP = FormMain.Instance.buttonItemWeeklyScheduleCPP.Checked;
            outputPage.ShowGRP = FormMain.Instance.buttonItemWeeklyScheduleGRP.Checked;
            outputPage.ShowCost = FormMain.Instance.buttonItemWeeklyScheduleCost.Checked;
            outputPage.ShowStation = FormMain.Instance.buttonItemWeeklyScheduleStation.Checked;
            outputPage.ShowDaypart = FormMain.Instance.buttonItemWeeklyScheduleDaypart.Checked;
            outputPage.ShowDay = FormMain.Instance.buttonItemWeeklyScheduleDay.Checked;
            outputPage.ShowTime = FormMain.Instance.buttonItemWeeklyScheduleTime.Checked;
            outputPage.ShowLength = FormMain.Instance.buttonItemWeeklyScheduleLength.Checked;
            outputPage.ShowTotalSpots = FormMain.Instance.buttonItemWeeklyScheduleTotalSpots.Checked;
            outputPage.ShowSpots = FormMain.Instance.buttonItemWeeklyScheduleSpots.Checked;
            outputPage.ShowTotalInvestment = FormMain.Instance.buttonItemWeeklyScheduleTotalCost.Checked;
            outputPage.ShowDiscount = FormMain.Instance.buttonItemWeeklyScheduleDiscount.Checked;
            outputPage.ShowNetCost = FormMain.Instance.buttonItemWeeklyScheduleNetRate.Checked;

            #region Set Totals
            if (FormMain.Instance.buttonItemWeeklyScheduleTotalPeriods.Checked)
                outputPage.Totals.Add(laTotalPeriodsTitle.Text, laTotalPeriodsValue.Text);
            if (FormMain.Instance.buttonItemWeeklyScheduleTotalSpots.Checked)
                outputPage.Totals.Add(laTotalSpotsTitle.Text, laTotalSpotsValue.Text);
            if (FormMain.Instance.buttonItemWeeklyScheduleTotalGRP.Checked)
                outputPage.Totals.Add(laTotalGRPTitle.Text, laTotalGRPValue.Text);
            if (FormMain.Instance.buttonItemWeeklyScheduleTotalCPP.Checked)
                outputPage.Totals.Add(laTotalCPPTitle.Text, laTotalCPPValue.Text);
            if (FormMain.Instance.buttonItemWeeklyScheduleAvgRate.Checked)
                outputPage.Totals.Add(laAvgRateTitle.Text, laAvgRateValue.Text);
            if (FormMain.Instance.buttonItemWeeklyScheduleTotalCost.Checked)
                outputPage.Totals.Add(laTotalCostTitle.Text, laTotalCostValue.Text);
            if (FormMain.Instance.buttonItemWeeklyScheduleNetRate.Checked)
                outputPage.Totals.Add(laNetRateTitle.Text, laNetRateValue.Text);
            if (FormMain.Instance.buttonItemWeeklyScheduleDiscount.Checked)
                outputPage.Totals.Add(laAgencyDiscountTitle.Text, laAgencyDiscountValue.Text);
            #endregion

            #region Set OutputProgram Values
            foreach (BusinessClasses.Program program in _localSchedule.WeeklySchedule.Programs)
            {
                BusinessClasses.OutputProgram outputProgram = new BusinessClasses.OutputProgram(outputPage);
                outputProgram.Name = program.Name + (FormMain.Instance.buttonItemWeeklyScheduleDaypart.Checked ? ("-" + program.Daypart) : string.Empty);
                outputProgram.LineID = program.Index.ToString();
                outputProgram.Station = program.Station;
                outputProgram.Days = program.Day;
                outputProgram.Time = program.Time;
                outputProgram.Length = program.Length;
                outputProgram.Rate = FormMain.Instance.buttonItemWeeklyScheduleRate.Checked && program.Rate.HasValue ? program.Rate.Value.ToString("$#,##0") : string.Empty;
                outputProgram.Rating = FormMain.Instance.buttonItemWeeklyScheduleRating.Checked && program.Rating.HasValue ? program.Rating.Value.ToString(_localSchedule.RatingAsCPP ? "#,###.0" : "#,##0") : string.Empty;
                outputProgram.CPP = FormMain.Instance.buttonItemWeeklyScheduleCPP.Checked ? program.CPP.ToString("$#,###.00") : string.Empty;
                outputProgram.GRP = FormMain.Instance.buttonItemWeeklyScheduleGRP.Checked ? program.GRP.ToString(_localSchedule.RatingAsCPP ? "#,###.0" : "#,##0") : string.Empty;
                outputProgram.TotalRate = FormMain.Instance.buttonItemWeeklyScheduleCost.Checked ? ((totalSpotsCount >= 8 ? "Cost: " : string.Empty) + program.Cost.ToString("$#,##0")) : string.Empty;
                outputProgram.TotalSpots = (totalSpotsCount >= 8 ? "Spots: " : string.Empty) + program.TotalSpots.ToString("#,##0");

                #region Set Spots Values
                foreach (BusinessClasses.Spot spot in program.Spots)
                {
                    string value = spot.Count > 0 ? spot.Count.ToString() : "-";
                    outputProgram.Spots.Add(value);
                }
                #endregion

                outputPage.Programs.Add(outputProgram);
            }
            #endregion

            #region Set Total Values
            if (defaultProgram != null)
            {
                foreach (BusinessClasses.Spot spot in defaultProgram.Spots)
                {
                    BusinessClasses.OutputTotalSpot outputTotalSpot = new BusinessClasses.OutputTotalSpot();
                    outputTotalSpot.Day = spot.Date.Day.ToString();
                    outputTotalSpot.Month = BusinessClasses.Spot.GetMonthAbbreviation(spot.Date.Month);
                    int sum = _localSchedule.WeeklySchedule.Programs.Select(x => x.Spots.Where(y => y.Date.Equals(spot.Date)).FirstOrDefault()).Where(z => z.Count.HasValue).Select(z => z.Count.Value).Sum();
                    outputTotalSpot.Value = sum > 0 ? sum.ToString() : "-";
                    outputPage.TotalSpots.Add(outputTotalSpot);
                }
            }
            outputPage.TotalCost = _localSchedule.WeeklySchedule.TotalCost.ToString("$#,##0");
            outputPage.TotalSpot = _localSchedule.WeeklySchedule.TotalSpots.ToString("#,##0");
            outputPage.TotalCPP = _localSchedule.WeeklySchedule.TotalCPP.ToString("$#,###.00");
            outputPage.TotalGRP = _localSchedule.WeeklySchedule.TotalGRP.ToString(_localSchedule.RatingAsCPP ? "#,###.0" : "#,##0");
            #endregion

            outputPage.PopulateWeeklyScheduleReplacementsList();

            return outputPage;
        }

        public void PrintOutput()
        {
            bool showResultForm = false;
            if (_localSchedule != null)
            {
                BusinessClasses.Program program = _localSchedule.WeeklySchedule.Programs.FirstOrDefault();
                if (program != null)
                {
                    using (ToolForms.FormProgress form = new ToolForms.FormProgress())
                    {
                        form.laProgress.Text = "Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!";
                        form.TopMost = true;
                        this.Enabled = false;
                        using (ToolForms.FormSelectOutput formSelect = new ToolForms.FormSelectOutput())
                        {
                            BusinessClasses.OutputSchedule outputSchedule = PrepareOutputScheduleGridBased();
                            formSelect.buttonXGrid.Enabled = _localSchedule.WeeklySchedule.Programs.Count <= 4 && outputSchedule.SpotsPerSlide <= 13 && _localSchedule.WeeklySchedule.Programs.Count <= 4 && File.Exists(Path.Combine(BusinessClasses.OutputManager.Instance.OneSheetGridBasedTemplatesFolderPath, string.Format(BusinessClasses.OutputManager.OneSheetGridBasedTemplateFileName, new object[] { outputSchedule.ProgramsPerSlide.ToString(), outputSchedule.SpotsPerSlide.ToString() })));
                            DialogResult result = formSelect.ShowDialog();
                            if (result == DialogResult.Yes)
                            {
                                form.Show();
                                InteropClasses.PowerPointHelper.Instance.AppendOneSheetGridBased(outputSchedule);
                                form.Close();
                                showResultForm = true;
                            }
                            else
                            {
                                form.Show();
                                InteropClasses.PowerPointHelper.Instance.AppendOneSheetExcelBased(PrepareOutputScheduleExcelBased(), result == DialogResult.Ignore);
                                form.Close();
                                showResultForm = true;
                            }
                        }
                        this.Enabled = true;
                    }
                    if (showResultForm)
                        using (ToolForms.FormSlideOutput form = new ToolForms.FormSlideOutput())
                        {
                            if (form.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                                AppManager.ActivateForm(FormMain.Instance.Handle, true, false);
                        }
                }
            }
        }

        public void buttonItemWeeklySchedulePowerPoint_Click(object sender, EventArgs e)
        {
            PrintOutput();
        }

        public void buttonItemWeeklyScheduleEmail_Click(object sender, EventArgs e)
        {
            if (_localSchedule != null)
            {
                BusinessClasses.Program program = _localSchedule.WeeklySchedule.Programs.FirstOrDefault();
                if (program != null)
                {
                    string tempFileName = Path.Combine(ConfigurationClasses.SettingsManager.Instance.TempPath, Path.GetFileName(Path.GetTempFileName()));
                    using (ToolForms.FormProgress form = new ToolForms.FormProgress())
                    {
                        form.laProgress.Text = "Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!";
                        form.TopMost = true;
                        this.Enabled = false;
                        BusinessClasses.OutputSchedule outputSchedule = PrepareOutputScheduleGridBased();
                        using (ToolForms.FormSelectOutput formSelect = new ToolForms.FormSelectOutput())
                        {
                            formSelect.buttonXGrid.Enabled = _localSchedule.WeeklySchedule.Programs.Count <= 4 && outputSchedule.SpotsPerSlide <= 13 && _localSchedule.WeeklySchedule.Programs.Count <= 4 && File.Exists(Path.Combine(BusinessClasses.OutputManager.Instance.OneSheetGridBasedTemplatesFolderPath, string.Format(BusinessClasses.OutputManager.OneSheetGridBasedTemplateFileName, new object[] { outputSchedule.ProgramsPerSlide.ToString(), outputSchedule.SpotsPerSlide.ToString() })));
                            DialogResult result = formSelect.ShowDialog();
                            if (result == DialogResult.Yes)
                            {
                                form.Show();
                                InteropClasses.PowerPointHelper.Instance.PrepareOneSheetEmailGridBased(tempFileName, outputSchedule);
                                form.Close();
                            }
                            else
                            {
                                form.Show();
                                InteropClasses.PowerPointHelper.Instance.PrepareOneSheetEmailExcelBased(tempFileName, PrepareOutputScheduleExcelBased(), result == DialogResult.Ignore);
                                form.Close();
                            }
                        }
                        this.Enabled = true;
                    }
                    if (File.Exists(tempFileName))
                        using (ToolForms.FormEmail formEmail = new ToolForms.FormEmail())
                        {
                            formEmail.Text = "Email this Detailed Planner";
                            formEmail.PresentationFile = tempFileName;
                            formEmail.ShowDialog();
                        }
                }
            }
        }
        #endregion
    }
}


