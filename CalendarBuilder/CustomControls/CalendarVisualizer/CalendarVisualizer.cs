using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CalendarBuilder.CustomControls.CalendarVisualizer
{
    public class CalendarVisualizer
    {
        private BusinessClasses.Calendar _calendar = null;
        private Control _container = null;
        private List<DayControl> _days = new List<DayControl>();

        public Dictionary<DateTime, MonthControl> Months { get; private set; }
        public List<DayControl> SelectedDays { get; private set; }

        public CalendarVisualizer()
        {
            this.Months = new Dictionary<DateTime, MonthControl>();
            this.SelectedDays = new List<DayControl>();
        }

        #region Common Methods
        public void Init(BusinessClasses.Calendar calendar, Control container, bool quickInit)
        {
            _calendar = calendar;
            _container = container;

            if (!quickInit)
            {
                ClearContainer();
                foreach (BusinessClasses.CalendarMonth month in _calendar.Months)
                {
                    List<DayControl[]> weeks = new List<DayControl[]>();
                    DateTime[][] datesByWeeks = BusinessClasses.Calendar.GetDaysByWeek(month.StartDate, month.EndDate);
                    foreach (DateTime[] weekDays in datesByWeeks)
                    {
                        List<DayControl> week = new List<DayControl>();
                        foreach (DateTime weekDay in weekDays)
                        {
                            BusinessClasses.CalendarDay calendarDay = _calendar.Days.Where(x => x.Date.Equals(weekDay)).FirstOrDefault();
                            if (calendarDay != null)
                            {
                                DayControl dayControl = new DayControl(calendarDay);
                                dayControl.DaySelected += new EventHandler<SelectDayEventArgs>((sender, e) =>
                                {
                                    CalendarControl.Instance.ApplyDayProperties();
                                    SelectDay(e.SelectedDay, e.MultiSelect);
                                    CalendarControl.Instance.CopyPaster.SetCopy();
                                });
                                dayControl.PropertiesRequested += new EventHandler<EventArgs>((sender, e) =>
                                {
                                    CalendarControl.Instance.Splash(true);
                                    CalendarControl.Instance.ChangeDayPropertiesVisibility(true);
                                    CalendarControl.Instance.Splash(false);
                                });
                                dayControl.DayCopied += new EventHandler<EventArgs>((sender, e) =>
                                {
                                    CalendarControl.Instance.CopyDay();
                                });
                                dayControl.DayPasted += new EventHandler<EventArgs>((sender, e) =>
                                {
                                    CalendarControl.Instance.PasteDay();
                                });
                                dayControl.DayCloned += new EventHandler<EventArgs>((sender, e) =>
                                {
                                    CalendarControl.Instance.CloneDay();
                                });
                                dayControl.DayDataDeleted += new EventHandler<EventArgs>((sender, e) =>
                                {
                                    CalendarControl.Instance.SettingsNotSaved = true;
                                    CalendarControl.Instance.dayPropertiesControl.LoadCurrentDayData();
                                    CalendarControl.Instance.LoadSlideInfoData(reload: true);
                                    CalendarControl.Instance.LoadGridData(reload: true);
                                });
                                CalendarControl.Instance.CopyPaster.OnSetCopy += new EventHandler<EventArgs>((sender, e) =>
                                {
                                    dayControl.toolStripMenuItemCopy.Enabled = true;
                                    dayControl.toolStripMenuItemClone.Enabled = true;
                                });
                                CalendarControl.Instance.CopyPaster.OnSetPaste += new EventHandler<EventArgs>((sender, e) =>
                                {
                                });
                                CalendarControl.Instance.CopyPaster.OnResetCopy += new EventHandler<EventArgs>((sender, e) =>
                                {
                                    dayControl.toolStripMenuItemCopy.Enabled = false;
                                    dayControl.toolStripMenuItemClone.Enabled = false;
                                });
                                CalendarControl.Instance.CopyPaster.OnResetPaste += new EventHandler<EventArgs>((sender, e) =>
                                {
                                    dayControl.toolStripMenuItemPaste.Enabled = false;
                                });
                                CalendarControl.Instance.CopyPaster.DayCopied += new EventHandler<EventArgs>((sender, e) =>
                                {
                                    dayControl.toolStripMenuItemPaste.Enabled = true;
                                });

                                week.Add(dayControl);
                                _days.Add(dayControl);
                            }
                            System.Windows.Forms.Application.DoEvents();
                        }
                        weeks.Add(week.ToArray());
                        System.Windows.Forms.Application.DoEvents();
                    }

                    MonthControl monthControl = new MonthControl(weeks.ToArray());
                    this.Months.Add(month.StartDate, monthControl);
                }
            }
            else
            {
                foreach (DayControl day in _days)
                {
                    BusinessClasses.CalendarDay calendarDay = _calendar.Days.Where(x => x.Date.Equals(day.Day.Date)).FirstOrDefault();
                    if (calendarDay != null)
                    {
                        day.Day = calendarDay;
                    }
                }
            }

        }
        #endregion

        #region Month Methods
        private void ClearContainer()
        {
            if (_container != null)
                _container.Controls.Clear();
            this.Months.Clear();
            _days.Clear();
        }

        public void ShowMonth(DateTime date)
        {
            MonthControl month = null;
            BusinessClasses.CalendarMonth calendarMonth = null;
            ClearSelection();
            _container.Controls.Clear();
            CalendarControl.Instance.CopyPaster.ResetCopy();
            if (this.Months.ContainsKey(date))
            {
                month = this.Months[date];
                calendarMonth = _calendar.Months.Where(x => x.StartDate.Equals(date)).FirstOrDefault();
            }
            if (month != null)
            {
                if (!_container.Controls.Contains(month))
                {
                    _container.Controls.Add(month);
                }
                month.BringToFront();
            }
            CalendarControl.Instance.LoadSlideInfoData(month: calendarMonth);
            CalendarControl.Instance.LoadGridData(month: calendarMonth);
        }

        public void RefreshData()
        {
            foreach (MonthControl month in this.Months.Values)
            {
                month.RefreshData();
            }
        }
        #endregion

        #region Selector Stuff
        public void ClearSelection()
        {
            foreach (DayControl day in this.SelectedDays)
                day.ChangeSelection(false);
            this.SelectedDays.Clear();
        }

        private void SelectDay(DayControl day, bool multiSelect)
        {
            if (!multiSelect)
                ClearSelection();
            day.ChangeSelection(true);
            this.SelectedDays.Add(day);

            CalendarControl.Instance.dayPropertiesControl.LoadData(day.Day);
        }
        #endregion
    }
}
