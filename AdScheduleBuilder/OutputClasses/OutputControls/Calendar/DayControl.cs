using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace AdScheduleBuilder.OutputClasses.OutputControls
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class DayControl : UserControl
    {
        private List<CalendarRecordControl> _monthViewRecords = new List<CalendarRecordControl>();
        private List<CalendarRecordControl> _dayViewRecords = new List<CalendarRecordControl>();
        private OutputForms.FormDayView _dayView = new OutputForms.FormDayView();

        public MonthViewControl ParentMonth { get; set; }
        public int WeekDayIndex { get; set; }
        public DateTime Date { get; set; }

        public DayOutput Output
        {
            get
            {
                DayOutput result = new DayOutput();
                result.RecordsCount = _monthViewRecords.Count;
                result.HasNotes = this.ParentMonth.ParentCalendar.LocalSchedule.ViewSettings.CalendarViewSettings.DayCustomNotes.Where(x => x.Day.Equals(this.Date)).Count() > 0 || this.ParentMonth.ParentCalendar.LocalSchedule.ViewSettings.CalendarViewSettings.DayDeadlines.Where(x => x.Day.Equals(this.Date)).Count() > 0;
                result.RecordsText = string.Empty;
                List<string> recordText = new List<string>();
                foreach (CalendarRecordControl day in _monthViewRecords)
                    recordText.Add(day.GetOutputText());
                if (recordText.Count > 0)
                    result.RecordsText = string.Join(";" + ((char)13).ToString(), recordText.ToArray());
                return result;
            }
        }

        public DayControl(MonthViewControl parent)
        {
            this.ParentMonth = parent;
            InitializeComponent();
        }

        private void SetWeekDayIndex()
        {
            switch (this.Date.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    this.WeekDayIndex = 1;
                    break;
                case DayOfWeek.Monday:
                    this.WeekDayIndex = 2;
                    break;
                case DayOfWeek.Tuesday:
                    this.WeekDayIndex = 3;
                    break;
                case DayOfWeek.Wednesday:
                    this.WeekDayIndex = 4;
                    break;
                case DayOfWeek.Thursday:
                    this.WeekDayIndex = 5;
                    break;
                case DayOfWeek.Friday:
                    this.WeekDayIndex = 6;
                    break;
                case DayOfWeek.Saturday:
                    this.WeekDayIndex = 7;
                    break;
            }
        }

        public void Init(DateTime date)
        {
            this.Date = date;
            SetWeekDayIndex();
            laSmallDayCaption.Text = date.Day.ToString();
            _dayView.Text = date.ToString("dddd");
            _dayView.laDayTitle.Text = date.ToString("MMMM dd, yyyy");
            _dayView.radioGroupCustomNote.Properties.Items.Add(new DevExpress.XtraEditors.Controls.RadioGroupItem(null, date.ToString("MMMM dd, yyyy")));
            _dayView.radioGroupCustomNote.Properties.Items.Add(new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Scheduled days ONLY"));
            _dayView.radioGroupCustomNote.Properties.Items.Add(new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "All Days in " + date.ToString("MMMM")));
            _dayView.radioGroupCustomNote.Properties.Items.Add(new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "All DAYS"));
            _dayView.radioGroupCustomNote.SelectedIndex = 0;
            _dayView.radioGroupDeadline.Properties.Items.Add(new DevExpress.XtraEditors.Controls.RadioGroupItem(null, date.ToString("MMMM dd, yyyy")));
            _dayView.radioGroupDeadline.Properties.Items.Add(new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Scheduled days ONLY"));
            _dayView.radioGroupDeadline.Properties.Items.Add(new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "All Days in " + date.ToString("MMMM")));
            _dayView.radioGroupDeadline.Properties.Items.Add(new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "All DAYS"));
            _dayView.radioGroupDeadline.SelectedIndex = 0;

            RefreshData();

        }

        public void RefreshData()
        {
            List<BusinessClasses.Insert> _inserts = new List<BusinessClasses.Insert>();
            _inserts.AddRange(this.ParentMonth.ParentCalendar.Inserts.Where(x => x.Date.Equals(this.Date)));
            int insertsCount = _inserts.Count;
            xtraScrollableControl.Controls.Clear();
            _monthViewRecords.Clear();
            _dayView.xtraScrollableControl.Controls.Clear();
            foreach (var insert in _inserts)
            {
                DayRecordControl monthViewRecord = new DayRecordControl(this);
                monthViewRecord.Init(insert, insertsCount, false);
                _monthViewRecords.Add(monthViewRecord);
                xtraScrollableControl.Controls.Add(monthViewRecord);
                monthViewRecord.BringToFront();

                DayRecordControl dayViewRecord = new DayRecordControl(this);
                dayViewRecord.Init(insert, insertsCount, true);
                _dayViewRecords.Add(dayViewRecord);
                _dayView.xtraScrollableControl.Controls.Add(dayViewRecord);
                dayViewRecord.BringToFront();
            }

            if (this.ParentMonth.ParentCalendar.LocalSchedule.ViewSettings.CalendarViewSettings.DayCustomNotes.Where(x => x.Day.Equals(this.Date)).Count() > 0 || this.ParentMonth.ParentCalendar.LocalSchedule.ViewSettings.CalendarViewSettings.DayDeadlines.Where(x => x.Day.Equals(this.Date)).Count() > 0)
            {
                CustomNoteRecordControl customNoteRecord = new CustomNoteRecordControl(this);
                customNoteRecord.Init(insertsCount);
                _monthViewRecords.Add(customNoteRecord);
                xtraScrollableControl.Controls.Add(customNoteRecord);
                customNoteRecord.BringToFront();
            }
        }

        public void ShowDayView()
        {
            if (this.Date >= this.ParentMonth.ParentCalendar.LocalSchedule.FlightDateStart && this.Date <= this.ParentMonth.ParentCalendar.LocalSchedule.FlightDateEnd)
            {
                string customNote = this.ParentMonth.ParentCalendar.LocalSchedule.ViewSettings.CalendarViewSettings.DayCustomNotes.Where(x => x.Day.Equals(this.Date)).Select(x => x.Info).FirstOrDefault();
                if (!string.IsNullOrEmpty(customNote))
                {
                    _dayView.checkEditUseCustomNote.Checked = true;
                    _dayView.memoEditCustomNote.EditValue = customNote;
                }
                else
                    _dayView.checkEditUseCustomNote.Checked = false;

                string deadline = this.ParentMonth.ParentCalendar.LocalSchedule.ViewSettings.CalendarViewSettings.DayDeadlines.Where(x => x.Day.Equals(this.Date)).Select(x => x.Info).FirstOrDefault();
                if (!string.IsNullOrEmpty(deadline))
                {
                    _dayView.checkEditUseDeadline.Checked = true;
                    _dayView.memoEditDeadline.EditValue = deadline;
                }
                else
                    _dayView.checkEditUseDeadline.Checked = false;
                if (_dayView.ShowDialog() == DialogResult.OK)
                {

                    #region Apply Custom Notes
                    List<DateTime> selectedCustomNotesDays = new List<DateTime>();
                    switch (_dayView.radioGroupCustomNote.SelectedIndex)
                    {
                        case 0:
                            selectedCustomNotesDays.Add(this.Date);
                            break;
                        case 1:
                            foreach (BusinessClasses.Publication publication in this.ParentMonth.ParentCalendar.LocalSchedule.Publications)
                                selectedCustomNotesDays.AddRange(publication.Inserts.Where(x => !selectedCustomNotesDays.Contains(x.Date) && x.Date != DateTime.MinValue && x.Date != DateTime.MaxValue).Select(x => x.Date).Distinct().ToArray());
                            break;
                        case 2:
                            DateTime wholeMonthDate = new DateTime(this.Date.Year, this.Date.Month, 1);
                            while (wholeMonthDate.Month == this.Date.Month)
                            {
                                if (wholeMonthDate >= this.ParentMonth.ParentCalendar.LocalSchedule.FlightDateStart && wholeMonthDate <= this.ParentMonth.ParentCalendar.LocalSchedule.FlightDateEnd)
                                    selectedCustomNotesDays.Add(wholeMonthDate);
                                wholeMonthDate = wholeMonthDate.AddDays(1);
                            }
                            break;
                        case 3:
                            DateTime wholeScheduleDate = this.ParentMonth.ParentCalendar.LocalSchedule.FlightDateStart;
                            while (wholeScheduleDate <= this.ParentMonth.ParentCalendar.LocalSchedule.FlightDateEnd)
                            {
                                selectedCustomNotesDays.Add(wholeScheduleDate);
                                wholeScheduleDate = wholeScheduleDate.AddDays(1);
                            }
                            break;
                    }

                    foreach (DateTime date in selectedCustomNotesDays)
                    {
                        ConfigurationClasses.CalendarDayInfo dayCustomNote = this.ParentMonth.ParentCalendar.LocalSchedule.ViewSettings.CalendarViewSettings.DayCustomNotes.Where(x => x.Day.Equals(date)).FirstOrDefault();
                        if (dayCustomNote == null)
                        {
                            dayCustomNote = new ConfigurationClasses.CalendarDayInfo();
                            dayCustomNote.Day = date;
                            this.ParentMonth.ParentCalendar.LocalSchedule.ViewSettings.CalendarViewSettings.DayCustomNotes.Add(dayCustomNote);
                        }
                        if (_dayView.checkEditUseCustomNote.Checked && _dayView.memoEditCustomNote.EditValue != null && !string.IsNullOrEmpty(_dayView.memoEditCustomNote.EditValue.ToString().Trim()))
                            dayCustomNote.Info = _dayView.memoEditCustomNote.EditValue.ToString().Trim();
                        else
                            dayCustomNote.Info = string.Empty;
                    }
                    #endregion

                    #region Apply Deadlines
                    List<DateTime> selectedDeadlineDays = new List<DateTime>();
                    switch (_dayView.radioGroupDeadline.SelectedIndex)
                    {
                        case 0:
                            selectedDeadlineDays.Add(this.Date);
                            break;
                        case 1:
                            foreach (BusinessClasses.Publication publication in this.ParentMonth.ParentCalendar.LocalSchedule.Publications)
                                selectedDeadlineDays.AddRange(publication.Inserts.Where(x => !selectedDeadlineDays.Contains(x.Date) && x.Date != DateTime.MinValue && x.Date != DateTime.MaxValue).Select(x => x.Date).Distinct().ToArray());
                            break;
                        case 2:
                            DateTime wholeMonthDate = new DateTime(this.Date.Year, this.Date.Month, 1);
                            while (wholeMonthDate.Month == this.Date.Month)
                            {
                                if (wholeMonthDate >= this.ParentMonth.ParentCalendar.LocalSchedule.FlightDateStart && wholeMonthDate <= this.ParentMonth.ParentCalendar.LocalSchedule.FlightDateEnd)
                                    selectedDeadlineDays.Add(wholeMonthDate);
                                wholeMonthDate = wholeMonthDate.AddDays(1);
                            }
                            break;
                        case 3:
                            DateTime wholeScheduleDate = this.ParentMonth.ParentCalendar.LocalSchedule.FlightDateStart;
                            while (wholeScheduleDate <= this.ParentMonth.ParentCalendar.LocalSchedule.FlightDateEnd)
                            {
                                selectedDeadlineDays.Add(wholeScheduleDate);
                                wholeScheduleDate = wholeScheduleDate.AddDays(1);
                            }
                            break;
                    }

                    foreach (DateTime date in selectedDeadlineDays)
                    {
                        ConfigurationClasses.CalendarDayInfo dayDeadline = this.ParentMonth.ParentCalendar.LocalSchedule.ViewSettings.CalendarViewSettings.DayDeadlines.Where(x => x.Day.Equals(date)).FirstOrDefault();
                        if (dayDeadline == null)
                        {
                            dayDeadline = new ConfigurationClasses.CalendarDayInfo();
                            dayDeadline.Day = date;
                            this.ParentMonth.ParentCalendar.LocalSchedule.ViewSettings.CalendarViewSettings.DayDeadlines.Add(dayDeadline);
                        }
                        if (_dayView.checkEditUseDeadline.Checked && _dayView.memoEditDeadline.EditValue != null && !string.IsNullOrEmpty(_dayView.memoEditDeadline.EditValue.ToString().Trim()))
                            dayDeadline.Info = _dayView.memoEditDeadline.EditValue.ToString().Trim();
                        else
                            dayDeadline.Info = string.Empty;
                    }
                    #endregion

                    this.ParentMonth.RefreshData();
                    this.ParentMonth.ParentCalendar.SettingsNotSaved = true;
                }
            }
            else
                AppManager.ShowWarning("Pick a date that is in your Schedule Window...");
        }

        private void xtraScrollableControl_DoubleClick(object sender, EventArgs e)
        {
            ShowDayView();
        }
    }
}
