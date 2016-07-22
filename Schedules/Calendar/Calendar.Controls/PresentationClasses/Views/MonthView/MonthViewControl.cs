﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Calendar.Entities.NonPersistent;
using Asa.Business.Common.Entities.NonPersistent.Common;
using Asa.Calendar.Controls.PresentationClasses.Calendars;
using Asa.Calendar.Controls.ToolForms;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Interfaces;
using Asa.Common.Core.Objects.Images;
using DevExpress.XtraPrinting.Native;

namespace Asa.Calendar.Controls.PresentationClasses.Views.MonthView
{
	[ToolboxItem(false)]
	public partial class MonthViewControl : UserControl, IView
	{
		private readonly List<DayControl> _days = new List<DayControl>();

		public Dictionary<DateTime, MonthControl> Months { get; private set; }
		public SelectionManager SelectionManager { get; private set; }

		#region IView Members
		public ICalendarControl Calendar { get; private set; }
		public CopyPasteManager CopyPasteManager { get; private set; }

		public bool SettingsNotSaved { get; set; }
		public string Title
		{
			get { return "Calendar"; }
		}
		public event EventHandler<EventArgs> DataSaved;
		#endregion

		public MonthViewControl(ICalendarControl calendar)
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			Calendar = calendar;
			Months = new Dictionary<DateTime, MonthControl>();
			SelectionManager = new SelectionManager(this);
			CopyPasteManager = new CopyPasteManager(this);
		}

		#region Interface Methods
		public void LoadData()
		{
			Release();
			foreach (var monthData in Calendar.CalendarContent.Months)
			{
				var month = (MonthControl)ReflectionHelper.GetControlInstance(typeof(MonthControl), monthData.GetType());
				Months.Add(monthData.Date, month);
			}
		}

		public void Save()
		{
			DataSaved?.Invoke(this, new EventArgs());
			SettingsNotSaved = false;
		}

		public void RefreshData()
		{
			foreach (var month in Months)
			{
				var calendarMonth = Calendar.CalendarContent.Months.FirstOrDefault(x => x.Date.Equals(month.Key));
				if (calendarMonth == null) continue;
				month.Value.RefreshData(Calendar.GetColorSchema(calendarMonth.OutputData.SlideColor));
				Calendar.UpdateDataManagementAndOutputFunctions();
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

			var calendarMonth = Calendar.CalendarContent.Months.FirstOrDefault(x => x.Date.Equals(date));
			if (calendarMonth == null) return;
			if (Months.ContainsKey(date))
			{
				month = Months[date];
				if (!month.HasData)
				{
					var weeks = new List<DayControl[]>();
					var datesByWeeks = Calendar.CalendarContent.GetDaysByWeek(calendarMonth.DaysRangeBegin, calendarMonth.DaysRangeEnd);
					foreach (var weekDays in datesByWeeks)
					{
						var week = new List<DayControl>();
						foreach (var calendarDay in weekDays.Select(weekDay => Calendar.CalendarContent.Days.FirstOrDefault(x => x.Date.Equals(weekDay))))
						{
							if (calendarDay != null)
							{
								var dayControl = new DayControl(calendarDay);
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
									Calendar.CalendarView.RefreshData();
									Calendar.SlideInfo.LoadData();
									Calendar.UpdateDataManagementAndOutputFunctions();
								};
								dayControl.DataChanged += (sender, e) =>
								{
									var day = sender as DayControl;
									if (day == null) return;
									Calendar.UpdateDataManagementAndOutputFunctions();
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
									var noteDateRange = Calendar.CalendarContent.CalculateDateRange(SelectionManager.SelectedDays.Select(x => x.Date).ToArray()).LastOrDefault();
									AddNote(noteDateRange);
									RefreshData();
									Calendar.UpdateDataManagementAndOutputFunctions();
								};
								dayControl.NotePasted += (sender, e) =>
								{
									PasteNote();
									RefreshData();
								};
								dayControl.ImageCopied += (sender, e) => CopyImage();
								dayControl.ImagePasted += (sender, e) =>
								{
									ImageSource imageSource = null;
									var clipboardImage = ClipboardHelper.GetImageFormClipboard();
									if (clipboardImage != null)
										imageSource = ImageSource.FromImage(clipboardImage);
									else if (Clipboard.ContainsText(TextDataFormat.Html))
									{
										var textContent = Clipboard.GetText(TextDataFormat.Html);
										try
										{
											imageSource = ImageSource.FromString(textContent);
										}
										catch { }
									}
									PasteImage(imageSource);

								};
								dayControl.ImageDeleted += (sender, e) =>
								{
									foreach (var day in SelectionManager.SelectedDays)
									{
										day.Logo = new ImageSource();
										RefreshData();
									}
									Calendar.SettingsNotSaved = true;
									Calendar.CalendarView.RefreshData();
									Calendar.UpdateDataManagementAndOutputFunctions();
								};

								SelectionManager.SelectionStateResponse += (sender, e) => dayControl.UpdateNoteMenuAccordingSelection(SelectionManager.SelectedDays.OrderBy(x => x.Date).ToList());

								CopyPasteManager.CopyDaySet += (sender, e) =>
								{
									dayControl.toolStripMenuItemCopy.Enabled = true;
									dayControl.toolStripMenuItemClone.Enabled = true;
								};
								CopyPasteManager.CopyReset += (sender, e) =>
								{
									dayControl.toolStripMenuItemCopy.Enabled = false;
									dayControl.toolStripMenuItemClone.Enabled = false;
									dayControl.ChangeCopySource(false);
								};
								CopyPasteManager.PasteReset += (sender, e) =>
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
				}
				month.RefreshData(Calendar.GetColorSchema(calendarMonth.OutputData.SlideColor));
			}
			if (month == null) return;
			if (!pnMain.Controls.Contains(month))
			{
				pnMain.Controls.Add(month);
			}
			month.BringToFront();
			month.RaiseEvents(true);
		}

		public void Release()
		{
			SelectionManager.Release();
			CopyPasteManager.Release();

			pnMain.Controls.Clear();

			_days.Clear();

			Months.ForEach(m => m.Value.Release());
			Months.Clear();
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
		}

		public void PasteDay()
		{
			var selectedDays = SelectionManager.SelectedDays.ToArray();
			CopyPasteManager.PasteDay(selectedDays);
		}

		public void CopyImage()
		{
			var selectedDay = SelectionManager.SelectedDays.FirstOrDefault();
			if (selectedDay == null || !selectedDay.Logo.ContainsData) return;
			Clipboard.SetText(String.Format("<ImageSource>{0}</ImageSource>", selectedDay.Logo.Serialize()), TextDataFormat.Html);
		}

		public void PasteImage(ImageSource imageSource)
		{
			var selectedDays = SelectionManager.SelectedDays.ToArray();
			CopyPasteManager.PasteImage(selectedDays, imageSource);
		}

		public void CloneDay()
		{
			var selectedDay = SelectionManager.SelectedDays.FirstOrDefault();
			if (selectedDay == null) return;
			using (var form = new FormCloneDay(selectedDay, Calendar.CalendarContent.Settings.FlightDateStart.Value, Calendar.CalendarContent.Settings.FlightDateEnd.Value))
			{
				form.OnHelpClick = () => Calendar.OpenHelp("clone");
				if (form.ShowDialog() == DialogResult.OK)
				{
					var clonedDays = Calendar.CalendarContent.Days.Where(x => form.SelectedDates.Contains(x.Date)).ToList();
					CopyPasteManager.CloneDay(selectedDay, clonedDays);
				}
			}
		}
		#endregion

		#endregion

		#region Notes Methods
		private CalendarNoteControl[][] GetNotesByWeeeks(CalendarMonth month)
		{
			var monthNotes = new List<CalendarNoteControl[]>();
			var datesByWeeks = Calendar.CalendarContent.GetDaysByWeek(month.DaysRangeBegin, month.DaysRangeEnd);
			foreach (var weekDays in datesByWeeks)
			{
				var weekNotes = new List<CalendarNoteControl>();
				foreach (var weekDay in weekDays)
				{
					var note = Calendar.CalendarContent.Notes.FirstOrDefault(x => weekDay.Equals(x.StartDay));
					if (note == null) continue;
					var noteControl = (CalendarNoteControl)ReflectionHelper.GetControlInstance(typeof(CalendarNoteControl), note.GetType(), note);
					noteControl.NoteChanged += (sender, e) =>
					{
						var targetNoteControl = sender as CalendarNoteControl;
						if (targetNoteControl == null) return;
						Calendar.SettingsNotSaved = true;
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
						using (var form = new FormCloneNote(note, Calendar.CalendarContent.Settings.FlightDateStart.Value, Calendar.CalendarContent.Settings.FlightDateEnd.Value))
						{
							form.OnHelpClick = () => Calendar.OpenHelp("ninjanotesclone");
							if (form.ShowDialog() != DialogResult.OK) return;
							foreach (var range in form.SelectedRanges)
								AddNote(range, note.Note.Clone());
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
								foreach (var calendarNote in Calendar.CalendarContent.Notes)
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
			Calendar.CalendarContent.AddNote(noteRange, noteText);
			Calendar.SettingsNotSaved = true;

			var calendarMonth = Calendar.CalendarContent.Months.FirstOrDefault(x => x.DaysRangeBegin <= noteRange.FinishDate.Value.Date && x.DaysRangeEnd >= noteRange.FinishDate.Value.Date);
			if (calendarMonth != null)
			{
				var notes = GetNotesByWeeeks(calendarMonth);
				Months[calendarMonth.Date].AddNotes(notes);
				var justAddedNote = notes
					.SelectMany(array => array.Where(note => note.CalendarNote.StartDay == noteRange.StartDate &&
						note.CalendarNote.FinishDay == noteRange.FinishDate))
					.FirstOrDefault();
				if (justAddedNote != null)
					justAddedNote.Focus();
			}
		}

		private void AddNote(DateRange noteRange, ITextItem note)
		{
			if (noteRange == null) return;
			Calendar.CalendarContent.AddNote(noteRange, note);
			Calendar.SettingsNotSaved = true;

			var calendarMonth = Calendar.CalendarContent.Months.FirstOrDefault(x => x.DaysRangeBegin <= noteRange.FinishDate.Value.Date && x.DaysRangeEnd >= noteRange.FinishDate.Value.Date);
			if (calendarMonth != null)
				Months[calendarMonth.Date].AddNotes(GetNotesByWeeeks(calendarMonth));
		}

		private void PasteNote()
		{
			if (CopyPasteManager.SourceNote == null) return;
			var noteDateRange = Calendar.CalendarContent.CalculateDateRange(SelectionManager.SelectedDays.Select(x => x.Date).ToArray()).LastOrDefault();
			AddNote(noteDateRange, CopyPasteManager.SourceNote.Note.Clone());
		}

		private void DeleteNote(CalendarNote note)
		{
			var calendarMonth = Calendar.CalendarContent.Months.FirstOrDefault(x => x.DaysRangeBegin <= note.FinishDay.Date && x.DaysRangeEnd >= note.FinishDay.Date);
			if (calendarMonth == null) return;
			Calendar.CalendarContent.DeleteNote(note);
			Calendar.SettingsNotSaved = true;
			Months[calendarMonth.Date].AddNotes(GetNotesByWeeeks(calendarMonth));
		}
		#endregion
	}
}