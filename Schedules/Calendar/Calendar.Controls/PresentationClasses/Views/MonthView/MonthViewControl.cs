using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using NewBizWiz.Calendar.Controls.PresentationClasses.Calendars;
using NewBizWiz.Calendar.Controls.ToolForms;
using NewBizWiz.CommonGUI.ToolForms;
using NewBizWiz.Core.Calendar;

namespace NewBizWiz.Calendar.Controls.PresentationClasses.Views.MonthView
{
	[ToolboxItem(false)]
	public partial class MonthViewControl : UserControl, IView
	{
		private readonly List<DayControl> _days = new List<DayControl>();

		public MonthViewControl(ICalendarControl calendar)
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			Calendar = calendar;
			Months = new Dictionary<DateTime, MonthControl>();
			SelectionManager = new SelectionManager(this);

			#region Copy-Paster Initialization
			CopyPasteManager = new CopyPasteManager(this);
			CopyPasteManager.OnSetCopyDay += (sender, e) =>
			{
				calendar.CopyButton.Enabled = true;
				calendar.CloneButton.Enabled = true;
			};
			CopyPasteManager.OnResetCopy += (sender, e) =>
			{
				calendar.CopyButton.Enabled = false;
				calendar.CloneButton.Enabled = false;
			};
			CopyPasteManager.OnResetPaste += (sender, e) => { calendar.PasteButton.Enabled = false; };
			CopyPasteManager.DayCopied += (sender, e) => { calendar.PasteButton.Enabled = true; };

			CopyPasteManager.DayPasted += (sender, e) =>
			{
				Calendar.SlideInfo.LoadData();
				RefreshData();
				SettingsNotSaved = true;
			};
			#endregion
		}

		#region Interface Methods
		public void LoadData(bool reload = false)
		{
			if (!reload)
			{
				ClearContainer();
				foreach (var monthData in Calendar.CalendarData.Months)
				{
					var month = Calendar is CalendarSundayBased ? (MonthControl)new MonthControlSundayBased() : new MonthControlMondayBased();
					Months.Add(monthData.Date, month);
				}
			}
			else
			{
				foreach (var day in _days)
				{
					var calendarDay = Calendar.CalendarData.Days.FirstOrDefault(x => x.Date.Equals(day.Day.Date));
					if (calendarDay != null)
					{
						day.Day = calendarDay;
					}
				}
			}
			SettingsNotSaved = false;
		}

		public void Save()
		{
			if (DataSaved != null)
				DataSaved(this, new EventArgs());
			SettingsNotSaved = false;
		}

		public void RefreshData()
		{
			foreach (var month in Months)
			{
				var calendarMonth = Calendar.CalendarData.Months.FirstOrDefault(x => x.Date.Equals(month.Key));
				month.Value.RefreshData(calendarMonth.OutputData.SlideColorLight, calendarMonth.OutputData.SlideColorDark);
				Calendar.UpdateOutputFunctions();
			}
		}

		public void ChangeMonth(DateTime date)
		{
			MonthControl month = null;
			CalendarMonth calendarMonth = null;

			SelectionManager.ClearSelection();
			pnMain.Controls.Clear();
			CopyPasteManager.ResetCopy();
			foreach (var existedMonth in Months.Values)
				existedMonth.RaiseEvents(false);

			calendarMonth = Calendar.CalendarData.Months.FirstOrDefault(x => x.Date.Equals(date));
			if (Months.ContainsKey(date))
			{
				month = Months[date];
				if (!month.HasData)
				{
					using (var form = new FormProgress())
					{
						form.laProgress.Text = "Chill-Out for a few seconds...\nLoading month data...";
						form.TopMost = true;
						var thread = new Thread(() => Invoke((MethodInvoker)delegate()
						{
							var weeks = new List<DayControl[]>();
							var datesByWeeks = Calendar.CalendarData.GetDaysByWeek(calendarMonth.DaysRangeBegin, calendarMonth.DaysRangeEnd);
							foreach (var weekDays in datesByWeeks)
							{
								var week = new List<DayControl>();
								foreach (var calendarDay in weekDays.Select(weekDay => Calendar.CalendarData.Days.FirstOrDefault(x => x.Date.Equals(weekDay))))
								{
									if (calendarDay != null)
									{
										var dayControl = new DayControl(calendarDay, Calendar.DayImages);
										dayControl.AllowToPasteNote = CopyPasteManager.SourceNote != null;
										dayControl.DaySelected += (sender, e) =>
										{
											SelectionManager.SelectDay(e.SelectedDay.Day, e.ModifierKeys);
											CopyPasteManager.SetCopyDay();
										};
										dayControl.DayCopied += (sender, e) => CopyDay();
										dayControl.DayPasted += (sender, e) => PasteDay();
										dayControl.DayCloned += (sender, e) => CloneDay();
										dayControl.DayDataDeleted += (sender, e) =>
										{
											foreach (CalendarDay day in SelectionManager.SelectedDays)
											{
												day.ClearData();
												RefreshData();
											}
											Calendar.SettingsNotSaved = true;
											Calendar.SelectedView.RefreshData();
											Calendar.SlideInfo.LoadData();
											Calendar.UpdateOutputFunctions();
										};
										dayControl.DataChanged += (sender, e) =>
										{
											Calendar.UpdateOutputFunctions();
											Calendar.SettingsNotSaved = true;
										};

										dayControl.SelectionStateRequested += (sender, e) => SelectionManager.ProcessSelectionStateRequest();
										dayControl.DayMouseMove += (sender, e) =>
										{
											foreach (var day in _days)
												if (day.Day.BelongsToSchedules && day.ClientRectangle.Contains(day.PointToClient(Cursor.Position)) && day.RaiseEvents)
													SelectionManager.SelectDay(day.Day, Keys.Control);
										};
										dayControl.NoteAdded += (sender, e) =>
										{
											var noteDateRange = Calendar.CalendarData.CalculateDateRange(SelectionManager.SelectedDays.Select(x => x.Date).ToArray()).LastOrDefault();
											AddNote(noteDateRange);
											RefreshData();
											Calendar.UpdateOutputFunctions();
										};
										dayControl.NotePasted += (sender, e) =>
										{
											PasteNote();
											RefreshData();
										};

										SelectionManager.SelectionStateResponse += (sender, e) => dayControl.UpdateNoteMenuAccordingSelection(SelectionManager.SelectedDays.OrderBy(x => x.Date).ToArray());

										CopyPasteManager.OnSetCopyDay += (sender, e) =>
										{
											dayControl.toolStripMenuItemCopy.Enabled = true;
											dayControl.toolStripMenuItemClone.Enabled = true;
										};
										CopyPasteManager.OnResetCopy += (sender, e) =>
										{
											dayControl.toolStripMenuItemCopy.Enabled = false;
											dayControl.toolStripMenuItemClone.Enabled = false;
											dayControl.ChangeCopySource(false);
										};
										CopyPasteManager.OnResetPaste += (sender, e) =>
										{
											dayControl.toolStripMenuItemPaste.Enabled = false;
											dayControl.AllowToPasteNote = false;
										};
										CopyPasteManager.DayCopied += (sender, e) =>
										{
											dayControl.toolStripMenuItemPaste.Enabled = true;
											dayControl.ChangeCopySource(dayControl.Day.Date.Equals(CopyPasteManager.SourceDay.Date));
										};
										CopyPasteManager.NoteCopied += (sender, e) => { dayControl.AllowToPasteNote = true; };
										Calendar.AssignCloseActiveEditorsonOutSideClick(dayControl);
										week.Add(dayControl);
										_days.Add(dayControl);
									}
									Application.DoEvents();
								}
								weeks.Add(week.ToArray());
								Application.DoEvents();
							}
							month.AddDays(weeks.ToArray());
							month.AddNotes(GetNotesByWeeeks(calendarMonth));
						}));

						form.Show();
						Application.DoEvents();

						thread.Start();

						while (thread.IsAlive)
							Application.DoEvents();
						form.Close();
					}
				}
				month.RefreshData(calendarMonth.OutputData.SlideColorLight, calendarMonth.OutputData.SlideColorDark);
			}
			if (month == null) return;
			if (!pnMain.Controls.Contains(month))
			{
				pnMain.Controls.Add(month);
			}
			month.BringToFront();
			month.RaiseEvents(true);
		}

		public void SelectDay(CalendarDay day, bool selected)
		{
			var dayControl = _days.FirstOrDefault(x => x.Day.Date.Equals(day.Date));
			if (dayControl != null)
				dayControl.ChangeSelection(selected);
		}

		#region Copy-Paste Methods and Event Handlers
		public void CopyDay()
		{
			var selectedDay = SelectionManager.SelectedDays.FirstOrDefault();
			if (selectedDay != null)
				CopyPasteManager.CopyDay(selectedDay);
		}

		public void PasteDay()
		{
			var selectedDays = SelectionManager.SelectedDays.ToArray();
			if (selectedDays != null)
				CopyPasteManager.PasteDay(selectedDays);
		}

		public void CloneDay()
		{
			CalendarDay[] clonedDays = null;
			var selectedDay = SelectionManager.SelectedDays.FirstOrDefault();
			if (selectedDay == null) return;
			using (var form = new FormCloneDay(selectedDay, Calendar.CalendarData.Schedule.FlightDateStart.Value, Calendar.CalendarData.Schedule.FlightDateEnd.Value))
			{
				if (form.ShowDialog() == DialogResult.OK)
					clonedDays = Calendar.CalendarData.Days.Where(x => form.SelectedDates.Contains(x.Date)).ToArray();
			}
			if (clonedDays != null)
				CopyPasteManager.CloneDay(selectedDay, clonedDays);
		}
		#endregion

		#endregion

		#region Notes Methods
		private CalendarNoteControl[][] GetNotesByWeeeks(CalendarMonth month)
		{
			var monthNotes = new List<CalendarNoteControl[]>();
			var datesByWeeks = Calendar.CalendarData.GetDaysByWeek(month.DaysRangeBegin, month.DaysRangeEnd);
			foreach (var weekDays in datesByWeeks)
			{
				var weekNotes = new List<CalendarNoteControl>();
				foreach (var weekDay in weekDays)
				{
					var note = Calendar.CalendarData.Notes.FirstOrDefault(x => weekDay.Equals(x.StartDay));
					if (note == null) continue;
					var noteControl = new CalendarNoteControl(note);
					noteControl.NoteChanged += (sender, e) => { Calendar.SettingsNotSaved = true; };
					noteControl.NoteDeleted += (sender, e) =>
					{
						DeleteNote(note);
						RefreshData();
					};
					noteControl.NoteCopied += (sender, e) =>
					{
						CopyPasteManager.CopyNote(note);
						Calendar.SettingsNotSaved = true;
					};
					noteControl.NoteCloned += (sender, e) =>
					{
						using (var form = new FormCloneNote(note, Calendar.CalendarData.Schedule.FlightDateStart.Value, Calendar.CalendarData.Schedule.FlightDateEnd.Value))
						{
							if (form.ShowDialog() != DialogResult.OK) return;
							foreach (var range in form.SelectedRanges)
								AddNote(range, note.Note);
						}
						Calendar.SettingsNotSaved = true;
					};
					noteControl.ColorChanging += (sender, e) =>
					{
						using (var form = new FormNoteColor())
						{
							form.NoteColor = note.BackgroundColor;
							if (form.ShowDialog() != DialogResult.OK) return;
							note.BackgroundColor = form.NoteColor;
							if (form.ApplyForAll)
								foreach (var calendarNote in Calendar.CalendarData.Notes)
									calendarNote.BackgroundColor = note.BackgroundColor;
							foreach (var monthControl in Months.Values)
								monthControl.RefreshNotes();
							Calendar.SettingsNotSaved = true;
						}
					};
					weekNotes.Add(noteControl);
				}
				monthNotes.Add(weekNotes.ToArray());
			}
			return monthNotes.ToArray();
		}

		private void AddNote(DateRange noteRange, string noteText = "")
		{
			if (noteRange == null) return;
			Calendar.CalendarData.AddNote(noteRange, noteText);
			Calendar.SettingsNotSaved = true;

			var calendarMonth = Calendar.CalendarData.Months.FirstOrDefault(x => x.Date.Equals(new DateTime(noteRange.FinishDate.Year, noteRange.FinishDate.Month, 1)));
			if (calendarMonth != null)
				Months[calendarMonth.Date].AddNotes(GetNotesByWeeeks(calendarMonth));
		}

		private void PasteNote()
		{
			if (CopyPasteManager.SourceNote == null) return;
			var noteDateRange = Calendar.CalendarData.CalculateDateRange(SelectionManager.SelectedDays.Select(x => x.Date).ToArray()).LastOrDefault();
			AddNote(noteDateRange, CopyPasteManager.SourceNote.Note);
		}

		private void DeleteNote(CalendarNote note)
		{
			var calendarMonth = Calendar.CalendarData.Months.FirstOrDefault(x => x.Date.Equals(new DateTime(note.FinishDay.Year, note.FinishDay.Month, 1)));
			if (calendarMonth == null) return;
			Calendar.CalendarData.DeleteNote(note);
			Calendar.SettingsNotSaved = true;
			Months[calendarMonth.Date].AddNotes(GetNotesByWeeeks(calendarMonth));
		}
		#endregion

		#region Common Methods
		private void ClearContainer()
		{
			pnMain.Controls.Clear();
			Months.Clear();
			_days.Clear();
		}
		#endregion

		public Dictionary<DateTime, MonthControl> Months { get; private set; }
		public SelectionManager SelectionManager { get; private set; }

		#region IView Members
		public ICalendarControl Calendar { get; private set; }
		public CopyPasteManager CopyPasteManager { get; private set; }

		public bool SettingsNotSaved { get; set; }
		public event EventHandler<EventArgs> DataSaved;
		#endregion
	}
}