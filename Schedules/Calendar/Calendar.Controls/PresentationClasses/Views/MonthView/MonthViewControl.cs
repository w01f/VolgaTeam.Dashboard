using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using NewBizWiz.Calendar.Controls.PresentationClasses.Calendars;
using NewBizWiz.Calendar.Controls.ToolForms;
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
				Controller.Instance.CalendarVisualizer.CopyButtonItem.Enabled = true;
				Controller.Instance.CalendarVisualizer.CloneButtonItem.Enabled = true;
			};
			CopyPasteManager.OnResetCopy += (sender, e) =>
			{
				Controller.Instance.CalendarVisualizer.CopyButtonItem.Enabled = false;
				Controller.Instance.CalendarVisualizer.CloneButtonItem.Enabled = false;
			};
			CopyPasteManager.OnResetPaste += (sender, e) => { Controller.Instance.CalendarVisualizer.PasteButtonItem.Enabled = false; };
			CopyPasteManager.DayCopied += (sender, e) => { Controller.Instance.CalendarVisualizer.PasteButtonItem.Enabled = true; };

			CopyPasteManager.DayPasted += (sender, e) =>
			{
				Calendar.SlideInfo.LoadData(reload: true);
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
				foreach (CalendarMonth monthData in Calendar.CalendarData.Months)
				{
					var month = new MonthControl();
					Months.Add(monthData.Date, month);
				}
			}
			else
			{
				foreach (DayControl day in _days)
				{
					CalendarDay calendarDay = Calendar.CalendarData.Days.Where(x => x.Date.Equals(day.Day.Date)).FirstOrDefault();
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
			foreach (MonthControl existedMonth in Months.Values)
				existedMonth.RaiseEvents(false);

			calendarMonth = Calendar.CalendarData.Months.FirstOrDefault(x => x.Date.Equals(date));
			if (Months.ContainsKey(date))
			{
				month = Months[date];
				if (!month.HasData)
				{
					using (var form = new FormProgress())
					{
						form.laProgress.Text = "Chill-Out for a few seconds...\nLoading month first time...";
						form.TopMost = true;
						var thread = new Thread(delegate()
						{
							Invoke((MethodInvoker)delegate()
							{
								var weeks = new List<DayControl[]>();
								DateTime[][] datesByWeeks = Calendar.CalendarData.GetDaysByWeek(calendarMonth.DaysRangeBegin, calendarMonth.DaysRangeEnd);
								foreach (var weekDays in datesByWeeks)
								{
									var week = new List<DayControl>();
									foreach (DateTime weekDay in weekDays)
									{
										CalendarDay calendarDay = Calendar.CalendarData.Days.Where(x => x.Date.Equals(weekDay)).FirstOrDefault();
										if (calendarDay != null)
										{
											var dayControl = new DayControl(calendarDay);
											dayControl.AllowToPasteNote = CopyPasteManager.SourceNote != null;
											dayControl.DaySelected += (sender, e) =>
																		{
																			SelectionManager.SelectDay(e.SelectedDay.Day, e.ModifierKeys);
																			CopyPasteManager.SetCopyDay();
																		};
											dayControl.DayCopied += (sender, e) => { CopyDay(); };
											dayControl.DayPasted += (sender, e) => { PasteDay(); };
											dayControl.DayCloned += (sender, e) => { CloneDay(); };
											dayControl.DayDataDeleted += (sender, e) =>
																			{
																				foreach (CalendarDay day in SelectionManager.SelectedDays)
																				{
																					day.ClearData();
																					RefreshData();
																				}
																				Calendar.SettingsNotSaved = true;
																				Calendar.SelectedView.RefreshData();
																				Calendar.SlideInfo.LoadData(reload: true);
																				Calendar.UpdateOutputFunctions();
																			};
											dayControl.DataChanged += (sender, e) =>
																		  {
																			  Calendar.UpdateOutputFunctions();
																			  Calendar.SettingsNotSaved = true;
																		  };

											dayControl.SelectionStateRequested += (sender, e) => { SelectionManager.ProcessSelectionStateRequest(); };
											dayControl.DayMouseMove += (sender, e) =>
																			{
																				foreach (DayControl day in _days)
																					if (day.Day.BelongsToSchedules && day.ClientRectangle.Contains(day.PointToClient(Cursor.Position)) && day.RaiseEvents)
																						SelectionManager.SelectDay(day.Day, Keys.Control);
																			};
											dayControl.NoteAdded += (sender, e) =>
																		{
																			DateRange noteDateRange = Calendar.CalendarData.CalculateDateRange(SelectionManager.SelectedDays.Select(x => x.Date).ToArray()).LastOrDefault();
																			AddNote(noteDateRange);
																			RefreshData();
																			Calendar.UpdateOutputFunctions();
																		};
											dayControl.NotePasted += (sender, e) =>
																		{
																			PasteNote();
																			RefreshData();
																		};

											SelectionManager.SelectionStateResponse += (sender, e) => { dayControl.UpdateNoteMenuAccordingSelection(SelectionManager.SelectedDays.OrderBy(x => x.Date).ToArray()); };

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
											CalendarVisualizer.AssignCloseActiveEditorsonOutSideClick(dayControl);
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
							});
						});

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
			if (month != null)
			{
				if (!pnMain.Controls.Contains(month))
				{
					pnMain.Controls.Add(month);
				}
				month.BringToFront();
				month.RaiseEvents(true);
			}
		}

		public void SelectDay(CalendarDay day, bool selected)
		{
			DayControl dayControl = _days.Where(x => x.Day.Date.Equals(day.Date)).FirstOrDefault();
			if (dayControl != null)
				dayControl.ChangeSelection(selected);
		}

		#region Copy-Paste Methods and Event Handlers
		public void CopyDay()
		{
			CalendarDay selectedDay = SelectionManager.SelectedDays.FirstOrDefault();
			if (selectedDay != null)
				CopyPasteManager.CopyDay(selectedDay);
		}

		public void PasteDay()
		{
			CalendarDay[] selectedDays = SelectionManager.SelectedDays.ToArray();
			if (selectedDays != null)
				CopyPasteManager.PasteDay(selectedDays);
		}

		public void CloneDay()
		{
			CalendarDay[] clonedDays = null;
			CalendarDay selectedDay = SelectionManager.SelectedDays.FirstOrDefault();
			if (selectedDay != null)
			{
				using (var form = new FormCloneDay(selectedDay, Calendar.CalendarData.Schedule.FlightDateStart.Value, Calendar.CalendarData.Schedule.FlightDateEnd.Value))
				{
					if (form.ShowDialog() == DialogResult.OK)
						clonedDays = Calendar.CalendarData.Days.Where(x => form.SelectedDates.Contains(x.Date)).ToArray();
				}
				if (clonedDays != null)
					CopyPasteManager.CloneDay(selectedDay, clonedDays);
			}
		}
		#endregion

		#endregion

		#region Notes Methods
		private CalendarNoteControl[][] GetNotesByWeeeks(CalendarMonth month)
		{
			var monthNotes = new List<CalendarNoteControl[]>();
			DateTime[][] datesByWeeks = Calendar.CalendarData.GetDaysByWeek(month.DaysRangeBegin, month.DaysRangeEnd);
			foreach (var weekDays in datesByWeeks)
			{
				var weekNotes = new List<CalendarNoteControl>();
				foreach (DateTime weekDay in weekDays)
				{
					CalendarNote note = Calendar.CalendarData.Notes.Where(x => weekDay.Equals(x.StartDay)).FirstOrDefault();
					if (note != null)
					{
						var noteControl = new CalendarNoteControl(note);
						noteControl.NoteChanged += (sender, e) => { Calendar.SettingsNotSaved = true; };
						noteControl.NoteDeleted += (sender, e) =>
						{
							DeleteNote(note);
							RefreshData();
						};
						noteControl.NoteCopied += (sender, e) => { CopyPasteManager.CopyNote(note); };
						noteControl.NoteCloned += (sender, e) =>
						{
							using (var form = new FormCloneNote(note, Calendar.CalendarData.Schedule.FlightDateStart.Value, Calendar.CalendarData.Schedule.FlightDateEnd.Value))
							{
								if (form.ShowDialog() == DialogResult.OK)
									foreach (DateRange range in form.SelectedRanges)
										AddNote(range, note.Note);
							}
						};
						noteControl.ColorChanging += (sender, e) =>
						{
							using (var form = new FormNoteColor())
							{
								form.NoteColor = note.BackgroundColor;
								if (form.ShowDialog() == DialogResult.OK)
								{
									note.BackgroundColor = form.NoteColor;
									if (form.ApplyForAll)
										foreach (CalendarNote calendarNote in Calendar.CalendarData.Notes)
											calendarNote.BackgroundColor = note.BackgroundColor;
									foreach (MonthControl monthControl in Months.Values)
										monthControl.RefreshNotes();
								}
							}
						};
						weekNotes.Add(noteControl);
					}
				}
				monthNotes.Add(weekNotes.ToArray());
			}
			return monthNotes.ToArray();
		}

		private void AddNote(DateRange noteRange, string noteText = "")
		{
			if (noteRange != null)
			{
				Calendar.CalendarData.AddNote(noteRange, noteText);
				Calendar.SettingsNotSaved = true;

				CalendarMonth calendarMonth = Calendar.CalendarData.Months.Where(x => x.Date.Equals(new DateTime(noteRange.FinishDate.Year, noteRange.FinishDate.Month, 1))).FirstOrDefault();
				if (calendarMonth != null)
					Months[calendarMonth.Date].AddNotes(GetNotesByWeeeks(calendarMonth));
			}
		}

		private void PasteNote()
		{
			if (CopyPasteManager.SourceNote != null)
			{
				DateRange noteDateRange = Calendar.CalendarData.CalculateDateRange(SelectionManager.SelectedDays.Select(x => x.Date).ToArray()).LastOrDefault();
				AddNote(noteDateRange, CopyPasteManager.SourceNote.Note);
			}
		}

		private void DeleteNote(CalendarNote note)
		{
			CalendarMonth calendarMonth = Calendar.CalendarData.Months.Where(x => x.Date.Equals(new DateTime(note.FinishDay.Year, note.FinishDay.Month, 1))).FirstOrDefault();
			if (calendarMonth != null)
			{
				Calendar.CalendarData.DeleteNote(note);
				Calendar.SettingsNotSaved = true;
				Months[calendarMonth.Date].AddNotes(GetNotesByWeeeks(calendarMonth));
			}
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