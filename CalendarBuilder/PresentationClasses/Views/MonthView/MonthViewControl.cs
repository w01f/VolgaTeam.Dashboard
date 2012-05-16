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
        public Dictionary<DateTime, MonthControl> Months { get; private set; }
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
            this.CopyPasteManager.OnSetCopyDay += new EventHandler<EventArgs>((sender, e) =>
            {
                CalendarVisualizer.Instance.CopyButtonItem.Enabled = true;
                CalendarVisualizer.Instance.CloneButtonItem.Enabled = true;
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
                foreach (BusinessClasses.CalendarMonth monthData in this.Calendar.CalendarData.Months)
                {
                    MonthControl month = new MonthControl(this.Calendar.CalendarData.Schedule.SundayBased);
                    this.Months.Add(monthData.Date, month);
                }
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
            calendarMonth = this.Calendar.CalendarData.Months.Where(x => x.Date.Equals(date)).FirstOrDefault();
            if (this.Months.ContainsKey(date))
            {
                month = this.Months[date];
                if (!month.HasData)
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
                                DateTime[][] datesByWeeks = this.Calendar.CalendarData.GetDaysByWeek(calendarMonth.DaysRangeBegin, calendarMonth.DaysRangeEnd);
                                foreach (DateTime[] weekDays in datesByWeeks)
                                {
                                    List<Views.MonthView.DayControl> week = new List<Views.MonthView.DayControl>();
                                    foreach (DateTime weekDay in weekDays)
                                    {
                                        BusinessClasses.CalendarDay calendarDay = this.Calendar.CalendarData.Days.Where(x => x.Date.Equals(weekDay)).FirstOrDefault();
                                        if (calendarDay != null)
                                        {
                                            Views.MonthView.DayControl dayControl = new Views.MonthView.DayControl(calendarDay);
                                            dayControl.AllowToPasteNote = this.CopyPasteManager.SourceNote != null;
                                            dayControl.DaySelected += new EventHandler<Views.MonthView.SelectDayEventArgs>((sender, e) =>
                                            {
                                                this.SelectionManager.SelectDay(e.SelectedDay.Day, e.ModifierKeys);
                                                this.CopyPasteManager.SetCopyDay();
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

                                            dayControl.SelectionStateRequested += new EventHandler<EventArgs>((sender, e) =>
                                            {
                                                this.SelectionManager.ProcessSelectionStateRequest();
                                            });
                                            dayControl.DayMouseMove += new EventHandler<MouseEventArgs>((sender, e) =>
                                            {
                                                foreach (DayControl day in _days)
                                                    if (day.Day.BelongsToSchedules && day.ClientRectangle.Contains(day.PointToClient(Cursor.Position)))
                                                        this.SelectionManager.SelectDay(day.Day, Keys.Control);
                                            });
                                            dayControl.NoteAdded += new EventHandler<EventArgs>((sender, e) =>
                                            {
                                                BusinessClasses.DateRange noteDateRange = this.Calendar.CalendarData.CalculateDateRange(this.SelectionManager.SelectedDays.Select(x => x.Date).ToArray()).LastOrDefault();
                                                AddNote(noteDateRange);
                                                RefreshData();
                                            });
                                            dayControl.NotePasted += new EventHandler<EventArgs>((sender, e) =>
                                            {
                                                PasteNote();
                                                RefreshData();
                                            });

                                            this.SelectionManager.SelectionStateResponse += new EventHandler<EventArgs>((sender, e) =>
                                            {
                                                dayControl.UpdateNoteMenuAccordingSelection(this.SelectionManager.SelectedDays.OrderBy(x => x.Date).ToArray());
                                            });

                                            this.CopyPasteManager.OnSetCopyDay += new EventHandler<EventArgs>((sender, e) =>
                                            {
                                                dayControl.toolStripMenuItemCopy.Enabled = true;
                                                dayControl.toolStripMenuItemClone.Enabled = true;
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
                                                dayControl.AllowToPasteNote = false;
                                            });
                                            this.CopyPasteManager.DayCopied += new EventHandler<EventArgs>((sender, e) =>
                                            {
                                                dayControl.toolStripMenuItemPaste.Enabled = true;
                                                dayControl.ChangeCopySource(dayControl.Day.Date.Equals(this.CopyPasteManager.SourceDay.Date));
                                            });
                                            this.CopyPasteManager.NoteCopied += new EventHandler<EventArgs>((sender, e) =>
                                            {
                                                dayControl.AllowToPasteNote = true;
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
                                month.AddDays(weeks.ToArray());
                                month.AddNotes(GetNotesByWeeeks(calendarMonth));
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
                this.CopyPasteManager.CopyDay(selectedDay);
        }

        public void PasteDay()
        {
            BusinessClasses.CalendarDay[] selectedDays = this.SelectionManager.SelectedDays.ToArray();
            if (selectedDays != null)
                this.CopyPasteManager.PasteDay(selectedDays);
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
                    this.CopyPasteManager.CloneDay(selectedDay, clonedDays);
            }
        }
        #endregion
        #endregion

        #region Notes Methods
        private CalendarNoteControl[][] GetNotesByWeeeks(BusinessClasses.CalendarMonth month)
        {
            List<Views.MonthView.CalendarNoteControl[]> monthNotes = new List<Views.MonthView.CalendarNoteControl[]>();
            DateTime[][] datesByWeeks = this.Calendar.CalendarData.GetDaysByWeek(month.DaysRangeBegin, month.DaysRangeEnd);
            foreach (DateTime[] weekDays in datesByWeeks)
            {
                List<Views.MonthView.CalendarNoteControl> weekNotes = new List<Views.MonthView.CalendarNoteControl>();
                foreach (DateTime weekDay in weekDays)
                {
                    BusinessClasses.CalendarNote note = this.Calendar.CalendarData.Notes.Where(x => weekDay.Equals(x.StartDay)).FirstOrDefault();
                    if (note != null)
                    {
                        CalendarNoteControl noteControl = new CalendarNoteControl(note);
                        noteControl.NoteChanged += new EventHandler<EventArgs>((sender, e) =>
                        {
                            this.Calendar.SettingsNotSaved = true;
                        });
                        noteControl.NoteDeleted += new EventHandler<EventArgs>((sender, e) =>
                        {
                            DeleteNote(note);
                            RefreshData();
                        });
                        noteControl.NoteCopied += new EventHandler<EventArgs>((sender, e) =>
                        {
                            this.CopyPasteManager.CopyNote(note);
                        });
                        noteControl.NoteCloned += new EventHandler<EventArgs>((sender, e) =>
                        {
                            using (ToolForms.FormCloneNote form = new ToolForms.FormCloneNote(note, this.Calendar.CalendarData.Schedule.FlightDateStart.Value, this.Calendar.CalendarData.Schedule.FlightDateEnd.Value))
                            {
                                if (form.ShowDialog() == DialogResult.OK)
                                    foreach (BusinessClasses.DateRange range in form.SelectedRanges)
                                        AddNote(range, note.Note);
                            }
                        });
                        noteControl.ColorChanging += new EventHandler<EventArgs>((sender, e) =>
                        {
                            using (ToolForms.FormNoteColor form = new ToolForms.FormNoteColor())
                            {
                                form.NoteColor = note.BackgroundColor;
                                if (form.ShowDialog() == DialogResult.OK)
                                {
                                    note.BackgroundColor = form.NoteColor;
                                    if (form.ApplyForAll)
                                        foreach (BusinessClasses.CalendarNote calendarNote in this.Calendar.CalendarData.Notes)
                                            calendarNote.BackgroundColor = note.BackgroundColor;
                                    foreach (MonthControl monthControl in this.Months.Values)
                                        monthControl.RefreshNotes();
                                }
                            }
                        });
                        weekNotes.Add(noteControl);
                    }
                }
                monthNotes.Add(weekNotes.ToArray());
            }
            return monthNotes.ToArray();
        }

        private void AddNote(BusinessClasses.DateRange noteRange, string noteText = "")
        {
            if (noteRange != null)
            {
                this.Calendar.CalendarData.AddNote(noteRange, noteText);
                this.Calendar.SettingsNotSaved = true;

                BusinessClasses.CalendarMonth calendarMonth = this.Calendar.CalendarData.Months.Where(x => x.Date.Equals(new DateTime(noteRange.FinishDate.Year, noteRange.FinishDate.Month, 1))).FirstOrDefault();
                if (calendarMonth != null)
                    this.Months[calendarMonth.Date].AddNotes(GetNotesByWeeeks(calendarMonth));
            }
        }

        private void PasteNote()
        {
            if (this.CopyPasteManager.SourceNote != null)
            {
                BusinessClasses.DateRange noteDateRange = this.Calendar.CalendarData.CalculateDateRange(this.SelectionManager.SelectedDays.Select(x => x.Date).ToArray()).LastOrDefault();
                AddNote(noteDateRange, this.CopyPasteManager.SourceNote.Note);
            }
        }

        private void DeleteNote(BusinessClasses.CalendarNote note)
        {
            BusinessClasses.CalendarMonth calendarMonth = this.Calendar.CalendarData.Months.Where(x => x.Date.Equals(new DateTime(note.FinishDay.Year, note.FinishDay.Month, 1))).FirstOrDefault();
            if (calendarMonth != null)
            {
                this.Calendar.CalendarData.DeleteNote(note);
                this.Calendar.SettingsNotSaved = true;
                this.Months[calendarMonth.Date].AddNotes(GetNotesByWeeeks(calendarMonth));
            }
        }
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
