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
using NewBizWiz.Core.Common;

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
					var month = Calendar.CalendarData is CalendarSundayBased ? (MonthControl)new MonthControlSundayBased() : new MonthControlMondayBased();
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

			SelectionManager.ClearSelection();
			pnMain.Controls.Clear();
			CopyPasteManager.ResetCopy();
			foreach (var existedMonth in Months.Values)
				existedMonth.RaiseEvents(false);

			var calendarMonth = Calendar.CalendarData.Months.FirstOrDefault(x => x.Date.Equals(date));
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
											foreach (var day in SelectionManager.SelectedDays)
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
											var day = sender as DayControl;
											if (day == null) return;
											Calendar.UpdateOutputFunctions();
											Calendar.SettingsNotSaved = true;
											var options = new Dictionary<string, object>();
											options.Add("Advertiser", Calendar.CalendarData.Schedule.BusinessName);
											options.Add("Day", day.Day.Date);
											options.Add("Text", day.Day.Summary);
											Calendar.TrackActivity(new UserActivity("Edit Data", options));
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
			if (selectedDay == null) return;
			CopyPasteManager.CopyDay(selectedDay);
			var options = new Dictionary<string, object>();
			options.Add("Advertiser", Calendar.CalendarData.Schedule.BusinessName);
			options.Add("Day", CopyPasteManager.SourceDay.Date);
			options.Add("Text", selectedDay.Summary);
			Calendar.TrackActivity(new UserActivity("Copy Data", options));
		}

		public void PasteDay()
		{
			var selectedDays = SelectionManager.SelectedDays.ToArray();
			CopyPasteManager.PasteDay(selectedDays);
			if (CopyPasteManager.SourceDay == null) return;
			foreach (var day in selectedDays)
			{
				var options = new Dictionary<string, object>();
				options.Add("Advertiser", Calendar.CalendarData.Schedule.BusinessName);
				options.Add("Day", day.Date);
				options.Add("Text", CopyPasteManager.SourceDay.Summary);
				Calendar.TrackActivity(new UserActivity("Paste Data", options));
			}
		}

		public void CloneDay()
		{
			var selectedDay = SelectionManager.SelectedDays.FirstOrDefault();
			if (selectedDay == null) return;
			using (var form = new FormCloneDay(selectedDay, Calendar.CalendarData.Schedule.FlightDateStart.Value, Calendar.CalendarData.Schedule.FlightDateEnd.Value))
			{
				if (form.ShowDialog() == DialogResult.OK)
				{
					var clonedDays = Calendar.CalendarData.Days.Where(x => form.SelectedDates.Contains(x.Date)).ToList();
					CopyPasteManager.CloneDay(selectedDay, clonedDays);

					var options = new Dictionary<string, object>();
					options.Add("Advertiser", Calendar.CalendarData.Schedule.BusinessName);
					options.Add("SourceDay", selectedDay.Date);
					options.Add("DestinationDay", clonedDays.Select(cd => cd.Date));
					options.Add("Text", selectedDay.Summary);
					Calendar.TrackActivity(new UserActivity("Clone Data", options));
				}
			}
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
					noteControl.NoteChanged += (sender, e) =>
					{
						var targetNoteControl = sender as CalendarNoteControl;
						if (targetNoteControl == null) return;
						Calendar.SettingsNotSaved = true;

						var options = new Dictionary<string, object>();
						options.Add("Advertiser", Calendar.CalendarData.Schedule.BusinessName);
						options.Add("StartDay", targetNoteControl.CalendarNote.StartDay);
						options.Add("EndDay", targetNoteControl.CalendarNote.FinishDay);
						options.Add("Text", targetNoteControl.CalendarNote.Note);
						Calendar.TrackActivity(new UserActivity("Edit Note", options));
					};
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

			var options = new Dictionary<string, object>();
			options.Add("Advertiser", Calendar.CalendarData.Schedule.BusinessName);
			options.Add("StartDay", noteRange.StartDate);
			options.Add("EndDay", noteRange.FinishDate);
			if (!String.IsNullOrEmpty(noteText))
				options.Add("Text", noteText);
			Calendar.TrackActivity(new UserActivity("Add Note", options));
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
		public string Title
		{
			get { return "Calemdar"; }
		}
		public event EventHandler<EventArgs> DataSaved;
		#endregion
	}
}