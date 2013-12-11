using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using NewBizWiz.AdSchedule.Controls.BusinessClasses;
using NewBizWiz.AdSchedule.Controls.InteropClasses;
using NewBizWiz.Core.AdSchedule;
using NewBizWiz.Core.Common;
using ListManager = NewBizWiz.Core.AdSchedule.ListManager;

namespace NewBizWiz.AdSchedule.Controls.PresentationClasses.OutputClasses.OutputControls
{
	[ToolboxItem(false)]
	public partial class MonthViewControl : UserControl
	{
		private readonly List<WeekControl> _weekControls = new List<WeekControl>();
		private MonthCalendarViewSettings _settings;

		public MonthViewControl(ICalendarControl parent)
		{
			InitializeComponent();
			ParentCalendar = parent;
			Dock = DockStyle.Fill;
			DayOutput = new List<DayOutput>();
			Resize += MonthViewControl_Resize;
		}

		public ICalendarControl ParentCalendar { get; private set; }
		public DateTime Month { get; private set; }
		public List<DayOutput> DayOutput { get; set; }

		public MonthCalendarViewSettings Settings
		{
			get { return _settings; }
			set
			{
				_settings = value;
				UpdateMonthLegend();
			}
		}

		private void FitHeader()
		{
			double width = Width / 7;
			pnSunday.Width = (int)width;
			pnMonday.Width = (int)width;
			pnTuesday.Width = (int)width;
			pnWednesday.Width = (int)width;
			pnThursday.Width = (int)width;
			pnFriday.Width = (int)width;
			pnSaturday.Width = Width - ((int)width * 6);
		}

		private void FitWeekControls()
		{
			double height = pnData.Height / _weekControls.Count;
			for (int i = 0; i < _weekControls.Count; i++)
			{
				if (i == (_weekControls.Count - 1))
					_weekControls[i].Height = pnData.Height - ((int)height * i);
				else
					_weekControls[i].Height = (int)height;
			}
		}

		private void UpdateMonthLegend()
		{
			var _newLegends = new List<CalendarLegend>();
			var _legendsFromPublication = new List<CalendarLegend>();

			var legend = new CalendarLegend();
			legend.Code = "bw";
			legend.Description = "black and white";
			_legendsFromPublication.Add(legend);

			legend = new CalendarLegend();
			legend.Code = "sc";
			legend.Description = "spot color";
			_legendsFromPublication.Add(legend);

			legend = new CalendarLegend();
			legend.Code = "fc";
			legend.Description = "full color";
			_legendsFromPublication.Add(legend);

			foreach (string publication in ParentCalendar.Inserts.Where(x => x.Date.HasValue && x.Date.Value.Month == _settings.Month.Month && x.Date.Value.Year == _settings.Month.Year).GroupBy(x => x.Publication).Select(x => x.Key).Distinct())
			{
				legend = new CalendarLegend();
				legend.Description = publication;
				var printProductSource = ListManager.Instance.PublicationSources.FirstOrDefault(x => x.Name.Equals(publication));
				if (printProductSource != null)
					legend.Code = printProductSource.Abbreviation;
				else
					legend.Code = (publication.Length > 3 ? publication.Substring(0, 3) : publication).ToUpper();
				_legendsFromPublication.Add(legend);
			}
			foreach (var insert in ParentCalendar.Inserts.Where(x => x.Date.HasValue && x.Date.Value.Month == _settings.Month.Month && x.Date.Value.Year == _settings.Month.Year))
			{
				foreach (var section in insert.Sections)
				{
					legend = new CalendarLegend();
					legend.Description = section.Name;
					legend.Code = section.Code;
					if (!_legendsFromPublication.Select(x => x.Description).Contains(legend.Description))
						_legendsFromPublication.Add(legend);
				}
				if (!string.IsNullOrEmpty(insert.CustomSection))
				{
					legend = new CalendarLegend();
					legend.Description = insert.CustomSection;
					legend.Code = insert.CustomSection.Length > 2 ? insert.CustomSection.Substring(0, 2).ToUpper() : insert.CustomSection;
					if (!_legendsFromPublication.Select(x => x.Description).Contains(legend.Description))
						_legendsFromPublication.Add(legend);
				}
			}
			_newLegends.AddRange(_settings.Legend.Where(x => _legendsFromPublication.Select(y => y.Description).Contains(x.Description)));
			_newLegends.AddRange(_legendsFromPublication.Where(x => !_settings.Legend.Select(y => y.Description).Contains(x.Description)));
			_settings.Legend.Clear();
			_settings.Legend.AddRange(_newLegends);
		}

		public void Init(DateTime month)
		{
			Month = month;
			pnData.Controls.Clear();
			_weekControls.Clear();
			DateTime[][] weeks = CalendarHelper.GetDaysByWeek(Month, Month.AddMonths(1).AddDays(-1));
			foreach (var days in weeks)
			{
				var week = new WeekControl(this);
				week.Init(days);
				_weekControls.Add(week);
				pnData.Controls.Add(week);
				week.BringToFront();
			}
		}

		public void RefreshData()
		{
			foreach (WeekControl week in _weekControls)
				week.RefreshData();
			_weekControls.ForEach(w => w.ApplyThemeColor(Settings.Parent.SlideColorLight, Settings.Parent.SlideColorDark));
		}

		public void PrepareOutput()
		{
			DayOutput.Clear();
			foreach (WeekControl week in _weekControls)
				DayOutput.AddRange(week.DayOutput);
		}

		private void MonthViewControl_Resize(object sender, EventArgs e)
		{
			FitWeekControls();
			FitHeader();
		}

		private void Weekdays_Paint(object sender, PaintEventArgs e)
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

		#region Output Staff
		public string SlideColor
		{
			get { return _settings.Parent.SlideColor; }
		}

		public string SlideName
		{
			get
			{
				string result = string.Empty;
				CalendarTemplate template = BusinessWrapper.Instance.OutputManager.CalendarTemplates.Where(x => x.IsLarge == _settings.Parent.ShowBigDate && x.HasLogo == _settings.Parent.ShowLogo && x.Color.ToLower().Equals(_settings.Parent.SlideColor) && x.Month.ToLower().Equals(_settings.Month.ToString("MMM-yy").ToLower())).FirstOrDefault();
				if (template != null)
					result = template.TemplateName;
				return result;
			}
		}

		public int SlideRGB
		{
			get
			{
				int result = Color.Black.ToArgb();
				switch (_settings.Parent.SlideColor)
				{
					case "black":
						result = Information.RGB(0, 0, 0);
						break;
					case "blue":
						result = Information.RGB(0, 0, 102);
						break;
					case "gray":
						result = Information.RGB(0, 0, 0);
						break;
					case "green":
						result = Information.RGB(0, 51, 0);
						break;
					case "orange":
						result = Information.RGB(153, 0, 0);
						break;
					case "teal":
						result = Information.RGB(0, 51, 102);
						break;
				}
				return result;
			}
		}

		public string SlideMasterName
		{
			get
			{
				string result = string.Empty;
				CalendarTemplate template = BusinessWrapper.Instance.OutputManager.CalendarTemplates.Where(x => x.IsLarge == _settings.Parent.ShowBigDate && x.HasLogo == _settings.Parent.ShowLogo && x.Color.ToLower().Equals(_settings.Parent.SlideColor) && x.Month.ToLower().Equals(_settings.Month.ToString("MMM-yy").ToLower())).FirstOrDefault();
				if (template != null)
					result = template.SlideMaster;
				return result;
			}
		}


		public string LogoFile
		{
			get
			{
				string result = string.Empty;
				if (_settings.Parent.ShowLogo && _settings.Logo != null)
				{
					result = Path.GetTempFileName();
					_settings.Logo.Save(result);
				}
				return result;
			}
		}

		public string SlideTitle
		{
			get { return _settings.Parent.ShowTitle ? _settings.Title : string.Empty; }
		}

		public string BusinessName
		{
			get { return _settings.Parent.ShowBusinessName ? (ParentCalendar.LocalSchedule.BusinessName + (!string.IsNullOrEmpty(ParentCalendar.LocalSchedule.AccountNumber) ? (" - " + ParentCalendar.LocalSchedule.AccountNumber) : string.Empty)) : string.Empty; }
		}

		public string DecisionMaker
		{
			get { return _settings.Parent.ShowDecisionMaker ? ParentCalendar.LocalSchedule.DecisionMaker : string.Empty; }
		}

		public string MonthText
		{
			get
			{
				string result = string.Empty;
				result = _settings.Month.ToString("MMMM yyyy");
				return result;
			}
		}

		public string BackgroundFileName
		{
			get { return String.Format("{0}{1}.png", _settings.Month.ToString("MMM").ToLower(), (_settings.Parent.ShowBigDate ? "1" : "2")); }
		}

		public string Comments
		{
			get
			{
				var result = new StringBuilder();
				if (_settings.Parent.ShowComments)
					result.AppendLine(_settings.Comments);
				if (_settings.Parent.ShowDigital && _settings.DigitalLegend.Enabled) // && )
				{
					if (!String.IsNullOrEmpty(_settings.DigitalLegend.Info) && _settings.DigitalLegend.AllowEdit)
						result.AppendLine(_settings.DigitalLegend.Info);
					else if (!_settings.DigitalLegend.AllowEdit)
						result.AppendLine(ParentCalendar.LocalSchedule.GetDigitalInfo(_settings.DigitalLegend.RequestOptions));
				}
				return result.ToString();
			}
		}

		public string TagA
		{
			get
			{
				string result = string.Empty;
				if (_settings.Parent.ShowTotalCost)
					result = "Monthly Investment: " + ParentCalendar.Inserts.Where(x => x.Date.HasValue && x.Date.Value.Month == _settings.Month.Month && x.Date.Value.Year == _settings.Month.Year).Select(x => x.FinalRate).Sum().ToString("$#,###.00");
				else if (_settings.Parent.ShowAvgCost)
					result = "Average Ad Cost: " + (ParentCalendar.Inserts.Any(x => x.Date.HasValue && x.Date.Value.Month == _settings.Month.Month && x.Date.Value.Year == _settings.Month.Year) ? ParentCalendar.Inserts.Where(x => x.Date.HasValue && x.Date.Value.Month == _settings.Month.Month && x.Date.Value.Year == _settings.Month.Year).Select(x => x.FinalRate).Average().ToString("$#,###.00") : "$.00");
				else if (_settings.Parent.ShowTotalAds)
					result = "Total Ads: " + ParentCalendar.Inserts.Count(x => x.Date.HasValue && x.Date.Value.Month == _settings.Month.Month && x.Date.Value.Year == _settings.Month.Year);
				else if (_settings.Parent.ShowActiveDays)
					result = "Total Active Days: " + ParentCalendar.Inserts.Where(x => x.Date.HasValue && x.Date.Value.Month == _settings.Month.Month && x.Date.Value.Year == _settings.Month.Year).GroupBy(x => x.Date).Count();
				return result;
			}
		}

		public string TagB
		{
			get
			{
				string result = string.Empty;
				if (_settings.Parent.ShowAvgCost && _settings.Parent.ShowTotalCost)
					result = "Average Ad Cost: " + (ParentCalendar.Inserts.Any(x => x.Date.HasValue && x.Date.Value.Month == _settings.Month.Month && x.Date.Value.Year == _settings.Month.Year) ? ParentCalendar.Inserts.Where(x => x.Date.HasValue && x.Date.Value.Month == _settings.Month.Month && x.Date.Value.Year == _settings.Month.Year).Select(x => x.FinalRate).Average().ToString("$#,###.00") : "$.00");
				else if (_settings.Parent.ShowTotalAds)
					result = "Total Ads: " + ParentCalendar.Inserts.Count(x => x.Date.HasValue && x.Date.Value.Month == _settings.Month.Month && x.Date.Value.Year == _settings.Month.Year);
				else if (_settings.Parent.ShowActiveDays)
					result = "Total Active Days: " + ParentCalendar.Inserts.Where(x => x.Date.HasValue && x.Date.Value.Month == _settings.Month.Month && x.Date.Value.Year == _settings.Month.Year).GroupBy(x => x.Date).Count();
				return result;
			}
		}

		public string TagC
		{
			get
			{
				string result = string.Empty;
				if (_settings.Parent.ShowAvgCost && _settings.Parent.ShowTotalCost && _settings.Parent.ShowTotalAds)
					result = "Total Ads: " + ParentCalendar.Inserts.Count(x => x.Date.HasValue && x.Date.Value.Month == _settings.Month.Month && x.Date.Value.Year == _settings.Month.Year);
				else if (_settings.Parent.ShowActiveDays)
					result = "Total Active Days: " + ParentCalendar.Inserts.Where(x => x.Date.HasValue && x.Date.Value.Month == _settings.Month.Month && x.Date.Value.Year == _settings.Month.Year).GroupBy(x => x.Date).Count();
				return result;
			}
		}

		public string TagD
		{
			get
			{
				string result = string.Empty;
				if (_settings.Parent.ShowAvgCost && _settings.Parent.ShowTotalCost && _settings.Parent.ShowTotalAds && _settings.Parent.ShowActiveDays)
					result = "Total Active Days: " + ParentCalendar.Inserts.Where(x => x.Date.HasValue && x.Date.Value.Month == _settings.Month.Month && x.Date.Value.Year == _settings.Month.Year).GroupBy(x => x.Date).Count();
				return result;
			}
		}

		public string Legend
		{
			get
			{
				string result = string.Empty;
				if (_settings.Parent.ShowLegend)
				{
					result = string.Join(";  ", _settings.Legend.Select(x => x.StringRepresentation));
				}
				return result;
			}
		}

		public DateTime OutputMonth
		{
			get { return _settings.Month; }
		}

		public void PrintOutput()
		{
			Enabled = false;
			AdSchedulePowerPointHelper.Instance.AppendCalendar(new[] { this });
			Enabled = true;
		}
		#endregion
	}
}