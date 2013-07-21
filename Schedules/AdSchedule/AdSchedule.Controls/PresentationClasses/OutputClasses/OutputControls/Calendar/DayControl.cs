using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using NewBizWiz.AdSchedule.Controls.PresentationClasses.OutputClasses.OutputForms;
using NewBizWiz.Core.AdSchedule;
using NewBizWiz.Core.Common;

namespace NewBizWiz.AdSchedule.Controls.PresentationClasses.OutputClasses.OutputControls
{
	[ToolboxItem(false)]
	public partial class DayControl : UserControl
	{
		private readonly FormDayView _dayView = new FormDayView();
		private readonly List<CalendarRecordControl> _dayViewRecords = new List<CalendarRecordControl>();
		private readonly List<CalendarRecordControl> _monthViewRecords = new List<CalendarRecordControl>();

		public DayControl(MonthViewControl parent)
		{
			ParentMonth = parent;
			InitializeComponent();
		}

		public MonthViewControl ParentMonth { get; set; }
		public int WeekDayIndex { get; set; }
		public DateTime Date { get; set; }

		public DayOutput Output
		{
			get
			{
				var result = new DayOutput();
				result.RecordsCount = _monthViewRecords.Count;
				result.HasNotes = ParentMonth.ParentCalendar.LocalSchedule.ViewSettings.CalendarViewSettings.DayCustomNotes.Where(x => x.Day.Equals(Date)).Count() > 0 || ParentMonth.ParentCalendar.LocalSchedule.ViewSettings.CalendarViewSettings.DayDeadlines.Where(x => x.Day.Equals(Date)).Count() > 0;
				result.RecordsText = string.Empty;
				var recordText = new List<string>();
				foreach (CalendarRecordControl day in _monthViewRecords)
					recordText.Add(day.GetOutputText());
				if (recordText.Count > 0)
					result.RecordsText = string.Join(";" + ((char)13).ToString(), recordText.ToArray());
				return result;
			}
		}

		private void SetWeekDayIndex()
		{
			switch (Date.DayOfWeek)
			{
				case DayOfWeek.Sunday:
					WeekDayIndex = 1;
					break;
				case DayOfWeek.Monday:
					WeekDayIndex = 2;
					break;
				case DayOfWeek.Tuesday:
					WeekDayIndex = 3;
					break;
				case DayOfWeek.Wednesday:
					WeekDayIndex = 4;
					break;
				case DayOfWeek.Thursday:
					WeekDayIndex = 5;
					break;
				case DayOfWeek.Friday:
					WeekDayIndex = 6;
					break;
				case DayOfWeek.Saturday:
					WeekDayIndex = 7;
					break;
			}
		}

		public void Init(DateTime date)
		{
			Date = date;
			SetWeekDayIndex();
			laSmallDayCaption.Text = date.Day.ToString();
			_dayView.Text = date.ToString("dddd");
			_dayView.laDayTitle.Text = date.ToString("MMMM dd, yyyy");
			_dayView.radioGroupCustomNote.Properties.Items.Add(new RadioGroupItem(null, date.ToString("MMMM dd, yyyy")));
			_dayView.radioGroupCustomNote.Properties.Items.Add(new RadioGroupItem(null, "Scheduled days ONLY"));
			_dayView.radioGroupCustomNote.Properties.Items.Add(new RadioGroupItem(null, "All Days in " + date.ToString("MMMM")));
			_dayView.radioGroupCustomNote.Properties.Items.Add(new RadioGroupItem(null, "All DAYS"));
			_dayView.radioGroupCustomNote.SelectedIndex = 0;
			_dayView.radioGroupDeadline.Properties.Items.Add(new RadioGroupItem(null, date.ToString("MMMM dd, yyyy")));
			_dayView.radioGroupDeadline.Properties.Items.Add(new RadioGroupItem(null, "Scheduled days ONLY"));
			_dayView.radioGroupDeadline.Properties.Items.Add(new RadioGroupItem(null, "All Days in " + date.ToString("MMMM")));
			_dayView.radioGroupDeadline.Properties.Items.Add(new RadioGroupItem(null, "All DAYS"));
			_dayView.radioGroupDeadline.SelectedIndex = 0;
			ApplyThemeColor(Color.White, Color.LightGray);

			RefreshData();
		}

		public void RefreshData()
		{
			var _inserts = new List<Insert>();
			_inserts.AddRange(ParentMonth.ParentCalendar.Inserts.Where(x => x.Date.Equals(Date)));
			int insertsCount = _inserts.Count;
			xtraScrollableControl.Controls.Clear();
			_monthViewRecords.Clear();
			_dayView.xtraScrollableControl.Controls.Clear();
			foreach (Insert insert in _inserts)
			{
				var monthViewRecord = new DayRecordControl(this);
				monthViewRecord.Init(insert, insertsCount, false);
				_monthViewRecords.Add(monthViewRecord);
				xtraScrollableControl.Controls.Add(monthViewRecord);
				monthViewRecord.BringToFront();

				var dayViewRecord = new DayRecordControl(this);
				dayViewRecord.Init(insert, insertsCount, true);
				_dayViewRecords.Add(dayViewRecord);
				_dayView.xtraScrollableControl.Controls.Add(dayViewRecord);
				dayViewRecord.BringToFront();
			}

			if (ParentMonth.ParentCalendar.LocalSchedule.ViewSettings.CalendarViewSettings.DayCustomNotes.Where(x => x.Day.Equals(Date)).Count() > 0 || ParentMonth.ParentCalendar.LocalSchedule.ViewSettings.CalendarViewSettings.DayDeadlines.Where(x => x.Day.Equals(Date)).Count() > 0)
			{
				var customNoteRecord = new CustomNoteRecordControl(this);
				customNoteRecord.Init(insertsCount);
				_monthViewRecords.Add(customNoteRecord);
				xtraScrollableControl.Controls.Add(customNoteRecord);
				customNoteRecord.BringToFront();
			}
		}

		public void ShowDayView()
		{
			if (Date >= ParentMonth.ParentCalendar.LocalSchedule.FlightDateStart && Date <= ParentMonth.ParentCalendar.LocalSchedule.FlightDateEnd)
			{
				string customNote = ParentMonth.ParentCalendar.LocalSchedule.ViewSettings.CalendarViewSettings.DayCustomNotes.Where(x => x.Day.Equals(Date)).Select(x => x.Info).FirstOrDefault();
				if (!string.IsNullOrEmpty(customNote))
				{
					_dayView.checkEditUseCustomNote.Checked = true;
					_dayView.memoEditCustomNote.EditValue = customNote;
				}
				else
					_dayView.checkEditUseCustomNote.Checked = false;

				string deadline = ParentMonth.ParentCalendar.LocalSchedule.ViewSettings.CalendarViewSettings.DayDeadlines.Where(x => x.Day.Equals(Date)).Select(x => x.Info).FirstOrDefault();
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
					var selectedCustomNotesDays = new List<DateTime>();
					switch (_dayView.radioGroupCustomNote.SelectedIndex)
					{
						case 0:
							selectedCustomNotesDays.Add(Date);
							break;
						case 1:
							foreach (PrintProduct publication in ParentMonth.ParentCalendar.LocalSchedule.PrintProducts)
								selectedCustomNotesDays.AddRange(publication.Inserts.Where(x => !selectedCustomNotesDays.Contains(x.Date) && x.Date != DateTime.MinValue && x.Date != DateTime.MaxValue).Select(x => x.Date).Distinct().ToArray());
							break;
						case 2:
							var wholeMonthDate = new DateTime(Date.Year, Date.Month, 1);
							while (wholeMonthDate.Month == Date.Month)
							{
								if (wholeMonthDate >= ParentMonth.ParentCalendar.LocalSchedule.FlightDateStart && wholeMonthDate <= ParentMonth.ParentCalendar.LocalSchedule.FlightDateEnd)
									selectedCustomNotesDays.Add(wholeMonthDate);
								wholeMonthDate = wholeMonthDate.AddDays(1);
							}
							break;
						case 3:
							DateTime wholeScheduleDate = ParentMonth.ParentCalendar.LocalSchedule.FlightDateStart;
							while (wholeScheduleDate <= ParentMonth.ParentCalendar.LocalSchedule.FlightDateEnd)
							{
								selectedCustomNotesDays.Add(wholeScheduleDate);
								wholeScheduleDate = wholeScheduleDate.AddDays(1);
							}
							break;
					}

					foreach (DateTime date in selectedCustomNotesDays)
					{
						CalendarDayInfo dayCustomNote = ParentMonth.ParentCalendar.LocalSchedule.ViewSettings.CalendarViewSettings.DayCustomNotes.Where(x => x.Day.Equals(date)).FirstOrDefault();
						if (dayCustomNote == null)
						{
							dayCustomNote = new CalendarDayInfo();
							dayCustomNote.Day = date;
							ParentMonth.ParentCalendar.LocalSchedule.ViewSettings.CalendarViewSettings.DayCustomNotes.Add(dayCustomNote);
						}
						if (_dayView.checkEditUseCustomNote.Checked && _dayView.memoEditCustomNote.EditValue != null && !string.IsNullOrEmpty(_dayView.memoEditCustomNote.EditValue.ToString().Trim()))
							dayCustomNote.Info = _dayView.memoEditCustomNote.EditValue.ToString().Trim();
						else
							dayCustomNote.Info = string.Empty;
					}
					#endregion

					#region Apply Deadlines
					var selectedDeadlineDays = new List<DateTime>();
					switch (_dayView.radioGroupDeadline.SelectedIndex)
					{
						case 0:
							selectedDeadlineDays.Add(Date);
							break;
						case 1:
							foreach (PrintProduct publication in ParentMonth.ParentCalendar.LocalSchedule.PrintProducts)
								selectedDeadlineDays.AddRange(publication.Inserts.Where(x => !selectedDeadlineDays.Contains(x.Date) && x.Date != DateTime.MinValue && x.Date != DateTime.MaxValue).Select(x => x.Date).Distinct().ToArray());
							break;
						case 2:
							var wholeMonthDate = new DateTime(Date.Year, Date.Month, 1);
							while (wholeMonthDate.Month == Date.Month)
							{
								if (wholeMonthDate >= ParentMonth.ParentCalendar.LocalSchedule.FlightDateStart && wholeMonthDate <= ParentMonth.ParentCalendar.LocalSchedule.FlightDateEnd)
									selectedDeadlineDays.Add(wholeMonthDate);
								wholeMonthDate = wholeMonthDate.AddDays(1);
							}
							break;
						case 3:
							DateTime wholeScheduleDate = ParentMonth.ParentCalendar.LocalSchedule.FlightDateStart;
							while (wholeScheduleDate <= ParentMonth.ParentCalendar.LocalSchedule.FlightDateEnd)
							{
								selectedDeadlineDays.Add(wholeScheduleDate);
								wholeScheduleDate = wholeScheduleDate.AddDays(1);
							}
							break;
					}

					foreach (DateTime date in selectedDeadlineDays)
					{
						CalendarDayInfo dayDeadline = ParentMonth.ParentCalendar.LocalSchedule.ViewSettings.CalendarViewSettings.DayDeadlines.Where(x => x.Day.Equals(date)).FirstOrDefault();
						if (dayDeadline == null)
						{
							dayDeadline = new CalendarDayInfo();
							dayDeadline.Day = date;
							ParentMonth.ParentCalendar.LocalSchedule.ViewSettings.CalendarViewSettings.DayDeadlines.Add(dayDeadline);
						}
						if (_dayView.checkEditUseDeadline.Checked && _dayView.memoEditDeadline.EditValue != null && !string.IsNullOrEmpty(_dayView.memoEditDeadline.EditValue.ToString().Trim()))
							dayDeadline.Info = _dayView.memoEditDeadline.EditValue.ToString().Trim();
						else
							dayDeadline.Info = string.Empty;
					}
					#endregion

					ParentMonth.RefreshData();
					ParentMonth.ParentCalendar.SettingsNotSaved = true;
				}
			}
			else
				Utilities.Instance.ShowWarning("Pick a date that is in your Schedule Window...");
		}

		public void ApplyThemeColor(Color colorLight, Color colorDark)
		{
			if (!(Date >= ParentMonth.ParentCalendar.LocalSchedule.FlightDateStart && Date <= ParentMonth.ParentCalendar.LocalSchedule.FlightDateEnd))
			{
				laSmallDayCaption.BackColor = colorDark;
				laSmallDayCaption.ForeColor = Color.White;
				xtraScrollableControl.BackColor = colorLight;
			}
			else
			{
				laSmallDayCaption.BackColor = colorLight;
				laSmallDayCaption.ForeColor = Color.Black;
				xtraScrollableControl.BackColor = Color.White;
			}
		}

		private void xtraScrollableControl_DoubleClick(object sender, EventArgs e)
		{
			ShowDayView();
		}

		private void DayControl_Paint(object sender, PaintEventArgs e)
		{
			Rectangle rect;
			if (e.ClipRectangle.Top == 0)
				rect = new Rectangle(e.ClipRectangle.Left, e.ClipRectangle.Top, e.ClipRectangle.Width, Height);
			else
				rect = new Rectangle(e.ClipRectangle.Left, 0, e.ClipRectangle.Width, e.ClipRectangle.Bottom);
			for (int i = 0; i < 1; i++)
			{
				ControlPaint.DrawBorder(e.Graphics, rect, Color.DarkGray, ButtonBorderStyle.Solid);
				rect.X = rect.X + 1;
				rect.Y = rect.Y + 1;
				rect.Width = rect.Width - 2;
				rect.Height = rect.Height - 2;
			}
		}
	}
}