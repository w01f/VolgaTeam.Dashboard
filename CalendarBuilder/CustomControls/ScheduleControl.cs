using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.BandedGrid;

namespace TVScheduleBuilder.CustomControls
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class ScheduleControl : UserControl
    {
        private static ScheduleControl _instance = null;
        private bool _allowTosave = false;
        private BusinessClasses.Schedule _localSchedule;
        private List<BandedGridColumn> _spotColumns = new List<BandedGridColumn>();

        public bool SettingsNotSaved { get; set; }

        private ScheduleControl()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
        }

        public static ScheduleControl Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ScheduleControl();
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
        private void UpdateGrid()
        {
            int focussedRow = advBandedGridViewSchedule.FocusedRowHandle;
            advBandedGridViewSchedule.BeginUpdate();
            gridControlSchedule.DataSource = null;
            gridControlSchedule.DataMember = string.Empty;
            bandedGridColumnCPP.FieldName = BusinessClasses.Schedule.ProgramCPPDataColumnName;
            bandedGridColumnDay.FieldName = BusinessClasses.Schedule.ProgramDayDataColumnName;
            bandedGridColumnGRP.FieldName = BusinessClasses.Schedule.ProgramGRPDataColumnName;
            bandedGridColumnIndex.FieldName = BusinessClasses.Schedule.ProgramIndexDataColumnName;
            bandedGridColumnLength.FieldName = BusinessClasses.Schedule.ProgramLengthDataColumnName;
            bandedGridColumnName.FieldName = BusinessClasses.Schedule.ProgramNameDataColumnName;
            bandedGridColumnRate.FieldName = BusinessClasses.Schedule.ProgramRateDataColumnName;
            bandedGridColumnRating.FieldName = BusinessClasses.Schedule.ProgramRatingDataColumnName;
            bandedGridColumnStation.FieldName = BusinessClasses.Schedule.ProgramStationDataColumnName;
            bandedGridColumnTime.FieldName = BusinessClasses.Schedule.ProgramTimeDataColumnName;
            _localSchedule.GenerateDataSource();
            BuildSpotColumns();
            if (_localSchedule.Programs.Count > 0)
            {
                gridControlSchedule.DataSource = _localSchedule.DataSource;
                gridControlSchedule.DataMember = BusinessClasses.Schedule.ProgramDataTableName;
            }
            advBandedGridViewSchedule.EndUpdate();
            if (focussedRow >= 0 && focussedRow < advBandedGridViewSchedule.RowCount)
                advBandedGridViewSchedule.FocusedRowHandle = focussedRow;
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

            foreach (DataColumn column in _localSchedule.DataSource.Tables[BusinessClasses.Schedule.ProgramDataTableName].Columns)
            {
                if (column.ColumnName.Contains(BusinessClasses.Schedule.ProgramSpotDataColumnNamePrefix))
                {
                    BandedGridColumn bandedGridColumn = new BandedGridColumn();
                    bandedGridColumn.AppearanceCell.Options.UseTextOptions = true;
                    bandedGridColumn.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    bandedGridColumn.AutoFillDown = true;
                    bandedGridColumn.Caption = column.Caption;
                    bandedGridColumn.ColumnEdit = this.repositoryItemSpinEditSpot;
                    bandedGridColumn.FieldName = column.ColumnName;
                    bandedGridColumn.OptionsColumn.FixedWidth = true;
                    bandedGridColumn.RowCount = 1;
                    bandedGridColumn.Visible = true;
                    bandedGridColumn.SummaryItem.FieldName = column.ColumnName;
                    bandedGridColumn.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                    _spotColumns.Add(bandedGridColumn);
                    advBandedGridViewSchedule.Columns.Add(bandedGridColumn);
                    gridBandSpots.Columns.Add(bandedGridColumn);
                }
            }

            gridBandSpots.Visible = _spotColumns.Count > 0;
            advBandedGridViewSchedule.OptionsView.ShowFooter = _spotColumns.Count > 0;
        }

        public void LoadSchedule(bool quickLoad)
        {
            _allowTosave = false;

            repositoryItemComboBoxPrograms.Items.Clear();
            repositoryItemComboBoxPrograms.Items.AddRange(BusinessClasses.ListManager.Instance.ProgramNames);
            repositoryItemComboBoxStations.Items.Clear();
            repositoryItemComboBoxStations.Items.AddRange(BusinessClasses.ListManager.Instance.Stations);
            repositoryItemComboBoxLengths.Items.Clear();
            repositoryItemComboBoxLengths.Items.AddRange(BusinessClasses.ListManager.Instance.Lengths);
            repositoryItemComboBoxDays.Items.Clear();
            repositoryItemComboBoxDays.Items.AddRange(BusinessClasses.ListManager.Instance.Days);
            repositoryItemComboBoxTimes.Items.Clear();
            repositoryItemComboBoxTimes.Items.AddRange(BusinessClasses.ListManager.Instance.Times);
            comboBoxEditDemo.Properties.Items.Clear();
            comboBoxEditDemo.Properties.Items.AddRange(BusinessClasses.ListManager.Instance.Demos);

            _localSchedule = BusinessClasses.ScheduleManager.Instance.GetLocalSchedule();

            laScheduleName.Text = _localSchedule.Name;

            FormMain.Instance.checkBoxItemScheduleMonths.Checked = _localSchedule.SpotType == BusinessClasses.SpotType.Month;
            FormMain.Instance.checkBoxItemScheduleWeeks.Checked = _localSchedule.SpotType == BusinessClasses.SpotType.Week;

            checkEditRating.Checked = _localSchedule.ShowRating;
            comboBoxEditDemo.Enabled = _localSchedule.ShowRating;
            checkEditCPP.Enabled = _localSchedule.ShowRating;
            checkEditCPM.Enabled = _localSchedule.ShowRating;
            checkEditCPP.Checked = _localSchedule.RatingAsCPP;
            checkEditCPM.Checked = !_localSchedule.RatingAsCPP;
            if (!string.IsNullOrEmpty(_localSchedule.Demo))
                comboBoxEditDemo.EditValue = _localSchedule.Demo;

            FormMain.Instance.buttonItemScheduleCPP.Enabled = _localSchedule.RatingAsCPP;
            FormMain.Instance.buttonItemScheduleCPM.Enabled = !_localSchedule.RatingAsCPP;

            FormMain.Instance.comboBoxEditBusinessName.Properties.Items.Clear();
            FormMain.Instance.comboBoxEditBusinessName.Properties.Items.AddRange(BusinessClasses.ListManager.Instance.Advertisers.ToArray());
            FormMain.Instance.comboBoxEditDecisionMaker.Properties.Items.Clear();
            FormMain.Instance.comboBoxEditDecisionMaker.Properties.Items.AddRange(BusinessClasses.ListManager.Instance.DecisionMakers.ToArray());

            FormMain.Instance.comboBoxEditBusinessName.EditValue = _localSchedule.BusinessName;
            FormMain.Instance.comboBoxEditDecisionMaker.EditValue = _localSchedule.DecisionMaker;

            FormMain.Instance.dateEditPresentationDate.EditValue = _localSchedule.PresentationDate;
            FormMain.Instance.dateEditFlightDatesStart.EditValue = _localSchedule.FlightDateStart;
            FormMain.Instance.dateEditFlightDatesEnd.EditValue = _localSchedule.FlightDateEnd;

            FormMain.Instance.buttonItemScheduleRate.Checked = _localSchedule.ShowRate;
            FormMain.Instance.buttonItemScheduleCPP.Checked = _localSchedule.ShowCPP;
            FormMain.Instance.buttonItemScheduleCPP.Checked = _localSchedule.ShowCPP;
            FormMain.Instance.buttonItemScheduleCPM.Checked = _localSchedule.ShowCPP;
            FormMain.Instance.buttonItemScheduleDay.Checked = _localSchedule.ShowDay;
            FormMain.Instance.buttonItemScheduleGRP.Checked = _localSchedule.ShowGRP;
            FormMain.Instance.buttonItemScheduleLenght.Checked = _localSchedule.ShowLenght;
            FormMain.Instance.buttonItemScheduleStation.Checked = _localSchedule.ShowStation;
            FormMain.Instance.buttonItemScheduleTime.Checked = _localSchedule.ShowTime;

            bandedGridColumnRate.Visible = _localSchedule.ShowRate;
            bandedGridColumnRating.Visible = _localSchedule.ShowRating;
            gridBandRate.Visible = _localSchedule.ShowRate | _localSchedule.ShowRating;
            if (_localSchedule.ShowRate)
                advBandedGridViewSchedule.SetColumnPosition(bandedGridColumnRate, 0, 0);
            if (_localSchedule.ShowRating)
                advBandedGridViewSchedule.SetColumnPosition(bandedGridColumnRating, 1, 0);
            bandedGridColumnRating.Caption = _localSchedule.RatingAsCPP ? "Rtg" : "(000s)";
            bandedGridColumnRating.ColumnEdit = _localSchedule.RatingAsCPP ? repositoryItemSpinEditRating : repositoryItemSpinEdit000s;


            bandedGridColumnCPP.Visible = _localSchedule.ShowCPP;
            bandedGridColumnGRP.Visible = _localSchedule.ShowGRP;
            gridBandCPP.Visible = _localSchedule.ShowCPP | _localSchedule.ShowGRP;
            if (_localSchedule.ShowCPP)
                advBandedGridViewSchedule.SetColumnPosition(bandedGridColumnCPP, 0, 0);
            if (_localSchedule.ShowGRP)
                advBandedGridViewSchedule.SetColumnPosition(bandedGridColumnGRP, 1, 0);
            bandedGridColumnCPP.Caption = _localSchedule.RatingAsCPP ? "CPP" : "CPM";

            bandedGridColumnLength.Visible = _localSchedule.ShowLenght;
            gridBandLength.Visible = _localSchedule.ShowLenght;

            bandedGridColumnDay.Visible = _localSchedule.ShowDay;
            bandedGridColumnTime.Visible = _localSchedule.ShowTime;
            gridBandDate.Visible = _localSchedule.ShowDay | _localSchedule.ShowTime;
            if (_localSchedule.ShowDay)
                advBandedGridViewSchedule.SetColumnPosition(bandedGridColumnDay, 0, 0);
            if (_localSchedule.ShowTime)
                advBandedGridViewSchedule.SetColumnPosition(bandedGridColumnTime, 1, 0);

            bandedGridColumnStation.Visible = _localSchedule.ShowStation;

            UpdateGrid();

            this.SettingsNotSaved = false;

            _allowTosave = true;
        }

        private bool SaveSchedule(string scheduleName = "")
        {
            if (!string.IsNullOrEmpty(scheduleName))
                _localSchedule.Name = scheduleName;
            advBandedGridViewSchedule.CloseEditor();
            if (FormMain.Instance.comboBoxEditBusinessName.EditValue != null)
                _localSchedule.BusinessName = FormMain.Instance.comboBoxEditBusinessName.EditValue.ToString();
            else
            {
                AppManager.ShowWarning("Your schedule is missing important information!\nPlease make sure you have a Business Name before you proceed.");
                return false;
            }
            if (FormMain.Instance.comboBoxEditDecisionMaker.EditValue != null)
                _localSchedule.DecisionMaker = FormMain.Instance.comboBoxEditDecisionMaker.EditValue.ToString();
            else
            {
                AppManager.ShowWarning("Your schedule is missing important information!\nPlease make sure you have a Owner/Decision-maker before you proceed.");
                return false;
            }

            if (FormMain.Instance.dateEditPresentationDate.DateTime != null)
                _localSchedule.PresentationDate = FormMain.Instance.dateEditPresentationDate.DateTime;
            else
            {
                AppManager.ShowWarning("Your schedule is missing important information!\nPlease make sure you have a Presentation Date before you proceed.");
                return false;
            }
            if (FormMain.Instance.dateEditFlightDatesStart.DateTime != null)
            {
                _localSchedule.FlightDateStart = FormMain.Instance.dateEditFlightDatesStart.DateTime;
                if (_localSchedule.FlightDateStart.Value.DayOfWeek != DayOfWeek.Monday)
                {
                    AppManager.ShowWarning("Flight Start Date must be Monday\nFlight End Date must be Sunday\nFlight Start Date must be less then Flight End Date.");
                    return false;
                }
            }
            else
            {
                AppManager.ShowWarning("Your schedule is missing important information!\nPlease make sure you have a Flight Start Date before you proceed.");
                return false;
            }
            if (FormMain.Instance.dateEditFlightDatesEnd.DateTime != null)
            {
                _localSchedule.FlightDateEnd = FormMain.Instance.dateEditFlightDatesEnd.DateTime;
                if (_localSchedule.FlightDateEnd.Value.DayOfWeek != DayOfWeek.Sunday || _localSchedule.FlightDateEnd < _localSchedule.FlightDateStart)
                {
                    AppManager.ShowWarning("Flight Start Date must be Monday\nFlight End Date must be Sunday\nFlight Start Date must be less then Flight End Date.");
                    return false;
                }
            }
            else
            {
                AppManager.ShowWarning("Your schedule is missing important information!\nPlease make sure you have a Flight End Date before you proceed.");
                return false;
            }

            FormMain.Instance.comboBoxEditBusinessName.Properties.Items.Clear();
            FormMain.Instance.comboBoxEditBusinessName.Properties.Items.AddRange(BusinessClasses.ListManager.Instance.Advertisers.ToArray());
            FormMain.Instance.comboBoxEditDecisionMaker.Properties.Items.Clear();
            FormMain.Instance.comboBoxEditDecisionMaker.Properties.Items.AddRange(BusinessClasses.ListManager.Instance.DecisionMakers.ToArray());

            BusinessClasses.ScheduleManager.Instance.SaveSchedule(_localSchedule, false, this);
            laScheduleName.Text = _localSchedule.Name;
            this.SettingsNotSaved = false;

            return true;
        }
        #endregion

        #region Ribbon Operations Events
        public void buttonItemScheduleHelp_Click(object sender, EventArgs e)
        {
            BusinessClasses.HelpManager.Instance.OpenHelpLink("Home");
        }

        public void buttonItemScheduleSettingsSave_Click(object sender, EventArgs e)
        {
            if (SaveSchedule())
                AppManager.ShowInformation("Schedule Saved");
        }

        public void buttonItemScheduleSettingsSaveAs_Click(object sender, EventArgs e)
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
            _localSchedule.AddProgram();
            UpdateGrid();
            if (advBandedGridViewSchedule.RowCount > 0)
                advBandedGridViewSchedule.FocusedRowHandle = advBandedGridViewSchedule.RowCount - 1;
            this.SettingsNotSaved = true;
        }

        public void buttonItemScheduleDelete_Click(object sender, EventArgs e)
        {
            _localSchedule.DeleteProgram(advBandedGridViewSchedule.GetDataSourceRowIndex(advBandedGridViewSchedule.FocusedRowHandle));
            UpdateGrid();
            this.SettingsNotSaved = true;
        }

        public void buttonItemScheduleRefresh_Click(object sender, EventArgs e)
        {
            _localSchedule.RebuildSpots();
            UpdateGrid();
            this.SettingsNotSaved = true;
        }
        #endregion

        #region Editors Events
        public void SchedulePropertyEditValueChanged(object sender, EventArgs e)
        {
            if (_allowTosave)
                this.SettingsNotSaved = true;
        }

        public void FlightDateStartEditValueChanged(object sender, EventArgs e)
        {
            if (FormMain.Instance.dateEditFlightDatesStart.EditValue != null && _allowTosave)
            {
                DateTime dateStart = FormMain.Instance.dateEditFlightDatesStart.DateTime;
                _localSchedule.FlightDateStart = dateStart;
                this.SettingsNotSaved = true;
                while (dateStart.DayOfWeek != DayOfWeek.Sunday)
                    dateStart = dateStart.AddDays(1);
                FormMain.Instance.dateEditFlightDatesEnd.EditValue = dateStart;
            }
        }

        public void FlightDateEndEditValueChanged(object sender, EventArgs e)
        {
            if (FormMain.Instance.dateEditFlightDatesStart.EditValue != null && _allowTosave)
            {
                DateTime dateEnd = FormMain.Instance.dateEditFlightDatesEnd.DateTime;
                _localSchedule.FlightDateEnd = dateEnd;
                this.SettingsNotSaved = true;
            }
        }

        public void dateEditFlightDatesStart_CloseUp(object sender, DevExpress.XtraEditors.Controls.CloseUpEventArgs e)
        {
            e.AcceptValue = false;
            if (e.Value != null)
            {
                DateTime temp = DateTime.MinValue;
                DateTime.TryParse(e.Value.ToString(), out temp);
                if (temp.DayOfWeek == DayOfWeek.Monday)
                    e.AcceptValue = true;
            }
            if (!e.AcceptValue)
                AppManager.ShowWarning("Flight Start Date must be Monday");
        }

        public void dateEditFlightDatesEnd_CloseUp(object sender, DevExpress.XtraEditors.Controls.CloseUpEventArgs e)
        {
            e.AcceptValue = false;
            if (e.Value != null)
            {
                DateTime temp = DateTime.MinValue;
                DateTime.TryParse(e.Value.ToString(), out temp);
                if (temp.DayOfWeek == DayOfWeek.Sunday)
                    e.AcceptValue = true;
            }
            if (!e.AcceptValue)
                AppManager.ShowWarning("Flight End Date must be Sunday");
        }

        private void comboBoxEditDemo_EditValueChanged(object sender, EventArgs e)
        {
            if (comboBoxEditDemo.EditValue != null && _allowTosave)
            {
                _localSchedule.Demo = comboBoxEditDemo.EditValue.ToString();
                UpdateGrid();
                this.SettingsNotSaved = true;
            }
        }
        #endregion

        #region Toggle Switch Events
        public void button_CheckedChanged(object sender, EventArgs e)
        {
            if (_allowTosave)
            {
                _localSchedule.ShowRate = FormMain.Instance.buttonItemScheduleRate.Checked;
                _localSchedule.ShowCPP = FormMain.Instance.buttonItemScheduleCPP.Checked;
                _localSchedule.ShowDay = FormMain.Instance.buttonItemScheduleDay.Checked;
                _localSchedule.ShowGRP = FormMain.Instance.buttonItemScheduleGRP.Checked;
                _localSchedule.ShowLenght = FormMain.Instance.buttonItemScheduleLenght.Checked;
                _localSchedule.ShowStation = FormMain.Instance.buttonItemScheduleStation.Checked;
                _localSchedule.ShowTime = FormMain.Instance.buttonItemScheduleTime.Checked;

                bandedGridColumnRate.Visible = _localSchedule.ShowRate;
                gridBandRate.Visible = _localSchedule.ShowRate | _localSchedule.ShowRating;
                if (_localSchedule.ShowRate)
                    advBandedGridViewSchedule.SetColumnPosition(bandedGridColumnRate, 0, 0);
                if (_localSchedule.ShowRating)
                    advBandedGridViewSchedule.SetColumnPosition(bandedGridColumnRating, 1, 0);

                bandedGridColumnCPP.Visible = _localSchedule.ShowCPP;
                bandedGridColumnGRP.Visible = _localSchedule.ShowGRP;
                gridBandCPP.Visible = _localSchedule.ShowCPP | _localSchedule.ShowGRP;
                if (_localSchedule.ShowCPP)
                    advBandedGridViewSchedule.SetColumnPosition(bandedGridColumnCPP, 0, 0);
                if (_localSchedule.ShowGRP)
                    advBandedGridViewSchedule.SetColumnPosition(bandedGridColumnGRP, 1, 0);

                bandedGridColumnLength.Visible = _localSchedule.ShowLenght;
                gridBandLength.Visible = _localSchedule.ShowLenght;

                bandedGridColumnDay.Visible = _localSchedule.ShowDay;
                bandedGridColumnTime.Visible = _localSchedule.ShowTime;
                gridBandDate.Visible = _localSchedule.ShowDay | _localSchedule.ShowTime;
                if (_localSchedule.ShowDay)
                    advBandedGridViewSchedule.SetColumnPosition(bandedGridColumnDay, 0, 0);
                if (_localSchedule.ShowTime)
                    advBandedGridViewSchedule.SetColumnPosition(bandedGridColumnTime, 1, 0);

                bandedGridColumnStation.Visible = _localSchedule.ShowStation;

                this.SettingsNotSaved = true;
            }
        }

        public void buttonItemScheduleCPP_CheckedChanged(object sender, EventArgs e)
        {
            if (_allowTosave)
            {
                _allowTosave = false;
                if (sender == FormMain.Instance.buttonItemScheduleCPP)
                    FormMain.Instance.buttonItemScheduleCPM.Checked = FormMain.Instance.buttonItemScheduleCPP.Checked;
                else if (sender == FormMain.Instance.buttonItemScheduleCPM)
                    FormMain.Instance.buttonItemScheduleCPP.Checked = FormMain.Instance.buttonItemScheduleCPM.Checked;
                _allowTosave = true;
            }
        }

        public void checkBoxItemScheduleSpotType_CheckedChanged(object sender, DevComponents.DotNetBar.CheckBoxChangeEventArgs e)
        {
            if (_allowTosave)
            {
                if (FormMain.Instance.checkBoxItemScheduleMonths.Checked)
                    _localSchedule.SpotType = BusinessClasses.SpotType.Month;
                else if (FormMain.Instance.checkBoxItemScheduleWeeks.Checked)
                    _localSchedule.SpotType = BusinessClasses.SpotType.Week;
                this.SettingsNotSaved = true;
            }
        }

        private void checkEditRating_CheckedChanged(object sender, EventArgs e)
        {
            if (_allowTosave)
            {
                _localSchedule.ShowRating = checkEditRating.Checked;
                comboBoxEditDemo.Enabled = _localSchedule.ShowRating;
                checkEditCPP.Enabled = _localSchedule.ShowRating;
                checkEditCPM.Enabled = _localSchedule.ShowRating;
                bandedGridColumnRating.Visible = _localSchedule.ShowRating;
                gridBandRate.Visible = _localSchedule.ShowRate | _localSchedule.ShowRating;
                if (_localSchedule.ShowRate)
                    advBandedGridViewSchedule.SetColumnPosition(bandedGridColumnRate, 0, 0);
                if (_localSchedule.ShowRating)
                    advBandedGridViewSchedule.SetColumnPosition(bandedGridColumnRating, 1, 0);
                this.SettingsNotSaved = true;
            }
        }

        private void checkEditCPP_CheckedChanged(object sender, EventArgs e)
        {
            if (_allowTosave)
            {
                _allowTosave = false;
                if (sender == checkEditCPP)
                    checkEditCPM.Checked = !checkEditCPP.Checked;
                else if (sender == checkEditCPM)
                    checkEditCPP.Checked = !checkEditCPM.Checked;
                _localSchedule.RatingAsCPP = checkEditCPP.Checked;
                bandedGridColumnRating.Caption = _localSchedule.RatingAsCPP ? "Rtg" : "(000s)";
                bandedGridColumnRating.ColumnEdit = _localSchedule.RatingAsCPP ? repositoryItemSpinEditRating : repositoryItemSpinEdit000s;
                bandedGridColumnCPP.Caption = _localSchedule.RatingAsCPP ? "CPP" : "CPM";
                FormMain.Instance.buttonItemScheduleCPP.Enabled = _localSchedule.RatingAsCPP;
                FormMain.Instance.buttonItemScheduleCPM.Enabled = !_localSchedule.RatingAsCPP;
                _allowTosave = true;
                this.SettingsNotSaved = true;
            }
        }
        #endregion

        #region Grid Events
        private void advBandedGridViewSchedule_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            advBandedGridViewSchedule.UpdateCurrentRow();
        }

        private void advBandedGridViewSchedule_CustomDrawFooter(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
        {
            if (_spotColumns.Count > 0)
            {
                e.Painter.DrawObject(e.Info);
                DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView view = sender as DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView;
                DevExpress.XtraGrid.Views.BandedGrid.ViewInfo.AdvBandedGridViewInfo viewInfo = view.GetViewInfo() as DevExpress.XtraGrid.Views.BandedGrid.ViewInfo.AdvBandedGridViewInfo;
                BandedGridColumn column = bandedGridColumnName;
                if (_localSchedule.ShowCPP)
                    column = bandedGridColumnCPP;
                else if (_localSchedule.ShowGRP)
                    column = bandedGridColumnGRP;
                else if (_localSchedule.ShowRate)
                    column = bandedGridColumnRate;
                else if (_localSchedule.ShowRating)
                    column = bandedGridColumnRating;
                else if (_localSchedule.ShowLenght)
                    column = bandedGridColumnLength;
                else if (_localSchedule.ShowDay)
                    column = bandedGridColumnDay;
                else if (_localSchedule.ShowTime)
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
        #endregion
    }
}


