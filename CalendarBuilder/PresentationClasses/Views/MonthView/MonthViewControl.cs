using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CalendarBuilder.PresentationClasses.Views.MonthView
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class MonthViewControl : UserControl, IView
    {
        private BusinessClasses.CalendarStyle _style;
        private List<DayControl> _days = new List<DayControl>();

        public ICalendarControl Calendar { get; private set; }
        public Dictionary<DateTime, Views.MonthView.MonthControl> Months { get; private set; }
        public SelectionManager SelectionManager { get; private set; }
        public CopyPasteManager CopyPasteManager { get; private set; }

        public bool SettingsNotSaved { get; set; }
        public event EventHandler<EventArgs> DataSaved;

        public MonthViewControl(ICalendarControl calendar)
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            this.Calendar = calendar;
            this.Months = new Dictionary<DateTime, MonthControl>();
            this.SelectionManager = new SelectionManager(this);

            #region Copy-Paster Initialization
            this.CopyPasteManager = new CopyPasteManager(this);
            this.CopyPasteManager.OnSetCopy += new EventHandler<EventArgs>((sender, e) =>
            {
                CalendarVisualizer.Instance.CopyButtonItem.Enabled = true;
                CalendarVisualizer.Instance.CloneButtonItem.Enabled = true;
            });
            this.CopyPasteManager.OnSetPaste += new EventHandler<EventArgs>((sender, e) =>
            {
            });
            this.CopyPasteManager.OnResetCopy += new EventHandler<EventArgs>((sender, e) =>
            {
                CalendarVisualizer.Instance.CopyButtonItem.Enabled = false;
                CalendarVisualizer.Instance.CloneButtonItem.Enabled = false;
            });
            this.CopyPasteManager.OnResetPaste += new EventHandler<EventArgs>((sender, e) =>
            {
                CalendarVisualizer.Instance.PasteButtonItem.Enabled = false;
            });
            this.CopyPasteManager.DayCopied += new EventHandler<EventArgs>((sender, e) =>
            {
                CalendarVisualizer.Instance.PasteButtonItem.Enabled = true;
            });

            this.CopyPasteManager.DayPasted += new EventHandler<EventArgs>((sender, e) =>
            {
                this.Calendar.DayProperties.LoadData();
                this.Calendar.SlideInfo.LoadData(reload: true);
                RefreshData();
                this.SettingsNotSaved = true;
            });
            #endregion
        }

        #region Interface Methods
        public void LoadData(bool reload = false)
        {
            if (!reload)
            {
                ClearContainer();
            }
            else
            {
                foreach (Views.MonthView.DayControl day in _days)
                {
                    BusinessClasses.CalendarDay calendarDay = this.Calendar.CalendarData.Days.Where(x => x.Date.Equals(day.Day.Date)).FirstOrDefault();
                    if (calendarDay != null)
                    {
                        day.Day = calendarDay;
                    }
                }
            }
            this.SettingsNotSaved = false;
        }

        public void Save()
        {
            if (this.DataSaved != null)
                this.DataSaved(this, new EventArgs());
            this.SettingsNotSaved = false;
        }

        public void RefreshData()
        {
            foreach (Views.MonthView.MonthControl month in this.Months.Values)
                month.RefreshData();
        }

        public void ChangeMonth(DateTime date)
        {
            Views.MonthView.MonthControl month = null;
            BusinessClasses.CalendarMonth calendarMonth = null;
            this.SelectionManager.ClearSelection();
            pnMain.Controls.Clear();
            this.CopyPasteManager.ResetCopy();
            calendarMonth = this.Calendar.CalendarData.Months.Where(x => x.StartDate.Equals(date)).FirstOrDefault();
            if (this.Months.ContainsKey(date))
            {
                month = this.Months[date];
            }
            else if (calendarMonth != null)
            {
                FormMain.Instance.ribbonControl.Enabled = false;
                using (ToolForms.FormProgress form = new ToolForms.FormProgress())
                {
                    form.laProgress.Text = "Chill-Out for a few seconds...\nLoading month first time...";
                    form.TopMost = true;
                    System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            List<Views.MonthView.DayControl[]> weeks = new List<Views.MonthView.DayControl[]>();
                            DateTime[][] datesByWeeks = BusinessClasses.Schedule.GetDaysByWeek(calendarMonth.StartDate, calendarMonth.EndDate);
                            foreach (DateTime[] weekDays in datesByWeeks)
                            {
                                List<Views.MonthView.DayControl> week = new List<Views.MonthView.DayControl>();
                                foreach (DateTime weekDay in weekDays)
                                {
                                    BusinessClasses.CalendarDay calendarDay = this.Calendar.CalendarData.Days.Where(x => x.Date.Equals(weekDay)).FirstOrDefault();
                                    if (calendarDay != null)
                                    {
                                        Views.MonthView.DayControl dayControl = new Views.MonthView.DayControl(calendarDay);
                                        dayControl.DaySelected += new EventHandler<Views.MonthView.SelectDayEventArgs>((sender, e) =>
                                        {
                                            this.SelectionManager.SelectDay(e.SelectedDay.Day, e.ModifierKeys);
                                            this.CopyPasteManager.SetCopy();
                                        });
                                        dayControl.PropertiesRequested += new EventHandler<EventArgs>((sender, e) =>
                                        {
                                            Calendar.DayProperties.Show();
                                        });
                                        dayControl.DayCopied += new EventHandler<EventArgs>((sender, e) =>
                                        {
                                            this.CopyDay();
                                        });
                                        dayControl.DayPasted += new EventHandler<EventArgs>((sender, e) =>
                                        {
                                            this.PasteDay();
                                        });
                                        dayControl.DayCloned += new EventHandler<EventArgs>((sender, e) =>
                                        {
                                            this.CloneDay();
                                        });
                                        dayControl.DayDataDeleted += new EventHandler<EventArgs>((sender, e) =>
                                        {
                                            foreach (BusinessClasses.CalendarDay day in this.SelectionManager.SelectedDays)
                                            {
                                                day.ClearData();
                                                RefreshData();
                                            }
                                            this.Calendar.SettingsNotSaved = true;
                                            this.Calendar.SelectedView.RefreshData();
                                            this.Calendar.DayProperties.LoadData();
                                            this.Calendar.SlideInfo.LoadData(reload: true);
                                        });
                                        dayControl.DataChanged += new EventHandler<EventArgs>((sender, e) =>
                                        {
                                            this.Calendar.SettingsNotSaved = true;
                                        });
                                        this.CopyPasteManager.OnSetCopy += new EventHandler<EventArgs>((sender, e) =>
                                        {
                                            dayControl.toolStripMenuItemCopy.Enabled = true;
                                            dayControl.toolStripMenuItemClone.Enabled = true;
                                        });
                                        this.CopyPasteManager.OnSetPaste += new EventHandler<EventArgs>((sender, e) =>
                                        {
                                        });
                                        this.CopyPasteManager.OnResetCopy += new EventHandler<EventArgs>((sender, e) =>
                                        {
                                            dayControl.toolStripMenuItemCopy.Enabled = false;
                                            dayControl.toolStripMenuItemClone.Enabled = false;
                                            dayControl.ChangeCopySource(false);
                                        });
                                        this.CopyPasteManager.OnResetPaste += new EventHandler<EventArgs>((sender, e) =>
                                        {
                                            dayControl.toolStripMenuItemPaste.Enabled = false;
                                        });
                                        this.CopyPasteManager.DayCopied += new EventHandler<EventArgs>((sender, e) =>
                                        {
                                            dayControl.toolStripMenuItemPaste.Enabled = true;
                                            dayControl.ChangeCopySource(dayControl.Day.Date.Equals(this.CopyPasteManager.Source.Date));
                                        });
                                        dayControl.Decorate(_style);
                                        CalendarVisualizer.AssignCloseActiveEditorsonOutSideClick(dayControl);
                                        week.Add(dayControl);
                                        _days.Add(dayControl);
                                    }
                                    System.Windows.Forms.Application.DoEvents();
                                }
                                weeks.Add(week.ToArray());
                                System.Windows.Forms.Application.DoEvents();
                            }
                            month = new Views.MonthView.MonthControl(weeks.ToArray());
                            this.Months.Add(calendarMonth.StartDate, month);
                        });
                    }));

                    form.Show();
                    System.Windows.Forms.Application.DoEvents();

                    thread.Start();

                    while (thread.IsAlive)
                        System.Windows.Forms.Application.DoEvents();
                    form.Close();
                }
                FormMain.Instance.ribbonControl.Enabled = true;
            }
            if (month != null)
            {
                if (!pnMain.Controls.Contains(month))
                {
                    pnMain.Controls.Add(month);
                }
                month.BringToFront();
            }
        }

        public void SelectDay(BusinessClasses.CalendarDay day, bool selected)
        {
            DayControl dayControl = _days.Where(x => x.Day.Date.Equals(day.Date)).FirstOrDefault();
            if (dayControl != null)
                dayControl.ChangeSelection(selected);
        }

        public void Decorate(BusinessClasses.CalendarStyle style)
        {
            _style = style;
        }
        #region Copy-Paste Methods and Event Handlers
        public void CopyDay()
        {
            BusinessClasses.CalendarDay selectedDay = this.SelectionManager.SelectedDays.FirstOrDefault();
            if (selectedDay != null)
                this.CopyPasteManager.Copy(selectedDay);
        }

        public void PasteDay()
        {
            BusinessClasses.CalendarDay[] selectedDays = this.SelectionManager.SelectedDays.ToArray();
            if (selectedDays != null)
                this.CopyPasteManager.Paste(selectedDays);
        }

        public void CloneDay()
        {
            BusinessClasses.CalendarDay[] clonedDays = null;
            BusinessClasses.CalendarDay selectedDay = this.SelectionManager.SelectedDays.FirstOrDefault();
            if (selectedDay != null)
            {
                using (ToolForms.FormCloneDay form = new ToolForms.FormCloneDay(selectedDay, this.Calendar.CalendarData.Schedule.FlightDateStart.Value, this.Calendar.CalendarData.Schedule.FlightDateEnd.Value))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                        clonedDays = this.Calendar.CalendarData.Days.Where(x => form.SelectedDates.Contains(x.Date)).ToArray();
                }
                if (clonedDays != null)
                    this.CopyPasteManager.Clone(selectedDay, clonedDays);
            }
        }
        #endregion
        #endregion

        #region Common Methods
        private void ClearContainer()
        {
            pnMain.Controls.Clear();
            this.Months.Clear();
            _days.Clear();
        }
        #endregion
    }
}
