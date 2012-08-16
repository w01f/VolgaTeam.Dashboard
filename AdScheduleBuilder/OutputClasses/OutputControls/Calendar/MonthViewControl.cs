using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace AdScheduleBuilder.OutputClasses.OutputControls
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class MonthViewControl : UserControl
    {
        private List<WeekControl> _weekControls = new List<WeekControl>();
        public ICalendarControl ParentCalendar { get; private set; }
        public DateTime Month { get; private set; }
        private ConfigurationClasses.MonthCalendarViewSettings _settings = null;
        public List<DayOutput> DayOutput { get; set; }

        public ConfigurationClasses.MonthCalendarViewSettings Settings
        {
            get
            {
                return _settings;
            }
            set
            {
                _settings = value;
                UpdateMonthLegend();
            }
        }

        public MonthViewControl(ICalendarControl parent)
        {
            InitializeComponent();
            this.ParentCalendar = parent;
            this.Dock = DockStyle.Fill;
            this.DayOutput = new List<DayOutput>();
            this.Resize += new EventHandler(MonthViewControl_Resize);
        }

        private void FitHeader()
        {
            double width = this.Width / 7;
            pnSunday.Width = (int)width;
            pnMonday.Width = (int)width;
            pnTuesday.Width = (int)width;
            pnWednesday.Width = (int)width;
            pnThursday.Width = (int)width;
            pnFriday.Width = (int)width;
            pnSaturday.Width = this.Width - ((int)width * 6);
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
            List<ConfigurationClasses.CalendarLegend> _newLegends = new List<ConfigurationClasses.CalendarLegend>();
            List<ConfigurationClasses.CalendarLegend> _legendsFromPublication = new List<ConfigurationClasses.CalendarLegend>();

            ConfigurationClasses.CalendarLegend legend = new ConfigurationClasses.CalendarLegend();
            legend.Code = "bw";
            legend.Description = "black and white";
            _legendsFromPublication.Add(legend);

            legend = new ConfigurationClasses.CalendarLegend();
            legend.Code = "sc";
            legend.Description = "spot color";
            _legendsFromPublication.Add(legend);

            legend = new ConfigurationClasses.CalendarLegend();
            legend.Code = "fc";
            legend.Description = "full color";
            _legendsFromPublication.Add(legend);

            foreach (string publication in this.ParentCalendar.Inserts.Where(x => x.Date.Month == _settings.Month.Month && x.Date.Year == _settings.Month.Year).GroupBy(x => x.Publication).Select(x => x.Key).Distinct())
            {
                legend = new ConfigurationClasses.CalendarLegend();
                legend.Description = publication;
                BusinessClasses.PublicationSource publicationSource = BusinessClasses.ListManager.Instance.PublicationSources.Where(x => x.Name.Equals(publication)).FirstOrDefault();
                if (publicationSource != null)
                    legend.Code = publicationSource.Abbreviation;
                else
                    legend.Code = (publication.Length > 3 ? publication.Substring(0, 3) : publication).ToUpper();
                _legendsFromPublication.Add(legend);
            }
            foreach (BusinessClasses.Insert insert in this.ParentCalendar.Inserts.Where(x => x.Date.Month == _settings.Month.Month && x.Date.Year == _settings.Month.Year))
            {
                foreach (BusinessClasses.NameCodePair section in insert.Sections)
                {
                    legend = new ConfigurationClasses.CalendarLegend();
                    legend.Description = section.Name;
                    legend.Code = section.Code;
                    if (!_legendsFromPublication.Select(x => x.Description).Contains(legend.Description))
                        _legendsFromPublication.Add(legend);
                }
                if (!string.IsNullOrEmpty(insert.CustomSection))
                {
                    legend = new ConfigurationClasses.CalendarLegend();
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
            this.Month = month;
            pnData.Controls.Clear();
            _weekControls.Clear();
            DateTime[][] weeks = CalendarHelper.GetDaysByWeek(this.Month, this.Month.AddMonths(1).AddDays(-1));
            foreach (DateTime[] days in weeks)
            {
                WeekControl week = new WeekControl(this);
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
        }

        public void PrepareOutput()
        {
            this.DayOutput.Clear();
            foreach (WeekControl week in _weekControls)
                this.DayOutput.AddRange(week.DayOutput);
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
                rect = new Rectangle(e.ClipRectangle.Left, e.ClipRectangle.Top, e.ClipRectangle.Width, this.Height);
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
            get
            {
                return _settings.Parent.SlideColor;
            }
        }

        public string SlideName
        {
            get
            {
                string result = string.Empty;
                BusinessClasses.CalendarTemplate template = BusinessClasses.OutputManager.Instance.CalendarTemplates.Where(x => x.IsLarge == _settings.Parent.ShowBigDate && x.HasLogo == _settings.Parent.ShowLogo && x.Color.ToLower().Equals(_settings.Parent.SlideColor) && x.Month.ToLower().Equals(_settings.Month.ToString("MMM-yy").ToLower())).FirstOrDefault();
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
                        result = Microsoft.VisualBasic.Information.RGB(0, 0, 0);
                        break;
                    case "blue":
                        result = Microsoft.VisualBasic.Information.RGB(0, 0, 102);
                        break;
                    case "gray":
                        result = Microsoft.VisualBasic.Information.RGB(0, 0, 0);
                        break;
                    case "green":
                        result = Microsoft.VisualBasic.Information.RGB(0, 51, 0);
                        break;
                    case "orange":
                        result = Microsoft.VisualBasic.Information.RGB(153, 0, 0);
                        break;
                    case "teal":
                        result = Microsoft.VisualBasic.Information.RGB(0, 51, 102);
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
                BusinessClasses.CalendarTemplate template = BusinessClasses.OutputManager.Instance.CalendarTemplates.Where(x => x.IsLarge == _settings.Parent.ShowBigDate && x.HasLogo == _settings.Parent.ShowLogo && x.Color.ToLower().Equals(_settings.Parent.SlideColor) && x.Month.ToLower().Equals(_settings.Month.ToString("MMM-yy").ToLower())).FirstOrDefault();
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
                    result = System.IO.Path.GetTempFileName();
                    _settings.Logo.Save(result);
                }
                return result;
            }
        }

        public string SlideTitle
        {
            get
            {
                return _settings.Parent.ShowTitle ? _settings.Title : string.Empty;
            }
        }

        public string BusinessName
        {
            get
            {
                return _settings.Parent.ShowBusinessName ? (this.ParentCalendar.LocalSchedule.BusinessName + (!string.IsNullOrEmpty(this.ParentCalendar.LocalSchedule.AccountNumber) ? (" - " + this.ParentCalendar.LocalSchedule.AccountNumber) : string.Empty)) : string.Empty;
            }
        }

        public string DecisionMaker
        {
            get
            {
                return _settings.Parent.ShowDecisionMaker ? this.ParentCalendar.LocalSchedule.DecisionMaker : string.Empty;
            }
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

        public string BackgroundSheetName
        {
            get
            {
                string result = string.Empty;
                result = _settings.Month.ToString("MMM").ToLower() + (_settings.Parent.ShowBigDate ? "1" : "2");
                return result;
            }
        }

        public string Comments
        {
            get
            {
                string result = string.Empty;
                if (_settings.Parent.ShowComments)
                    result = _settings.Comments;
                return result;
            }
        }

        public string TagA
        {
            get
            {
                string result = string.Empty;
                if (_settings.Parent.ShowTotalCost)
                    result = "Monthly Investment: " + this.ParentCalendar.Inserts.Where(x => x.Date.Month == _settings.Month.Month && x.Date.Year == _settings.Month.Year).Select(x => x.FinalRate).Sum().ToString("$#,###.00");
                else if (_settings.Parent.ShowAvgCost)
                    result = "Average Ad Cost: " + (this.ParentCalendar.Inserts.Where(x => x.Date.Month == _settings.Month.Month && x.Date.Year == _settings.Month.Year).Count() > 0 ? this.ParentCalendar.Inserts.Where(x => x.Date.Month == _settings.Month.Month && x.Date.Year == _settings.Month.Year).Select(x => x.FinalRate).Average().ToString("$#,###.00") : "$.00");
                else if (_settings.Parent.ShowTotalAds)
                    result = "Total Ads: " + this.ParentCalendar.Inserts.Where(x => x.Date.Month == _settings.Month.Month && x.Date.Year == _settings.Month.Year).Count().ToString();
                else if (_settings.Parent.ShowActiveDays)
                    result = "Total Active Days: " + this.ParentCalendar.Inserts.Where(x => x.Date.Month == _settings.Month.Month && x.Date.Year == _settings.Month.Year).GroupBy(x => x.Date).Count().ToString();
                return result;
            }
        }

        public string TagB
        {
            get
            {
                string result = string.Empty;
                if (_settings.Parent.ShowAvgCost && _settings.Parent.ShowTotalCost)
                    result = "Average Ad Cost: " + (this.ParentCalendar.Inserts.Where(x => x.Date.Month == _settings.Month.Month && x.Date.Year == _settings.Month.Year).Count() > 0 ? this.ParentCalendar.Inserts.Where(x => x.Date.Month == _settings.Month.Month && x.Date.Year == _settings.Month.Year).Select(x => x.FinalRate).Average().ToString("$#,###.00") : "$.00");
                else if (_settings.Parent.ShowTotalAds)
                    result = "Total Ads: " + this.ParentCalendar.Inserts.Where(x => x.Date.Month == _settings.Month.Month && x.Date.Year == _settings.Month.Year).Count().ToString();
                else if (_settings.Parent.ShowActiveDays)
                    result = "Total Active Days: " + this.ParentCalendar.Inserts.Where(x => x.Date.Month == _settings.Month.Month && x.Date.Year == _settings.Month.Year).GroupBy(x => x.Date).Count().ToString();
                return result;
            }
        }

        public string TagC
        {
            get
            {
                string result = string.Empty;
                if (_settings.Parent.ShowAvgCost && _settings.Parent.ShowTotalCost && _settings.Parent.ShowTotalAds)
                    result = "Total Ads: " + this.ParentCalendar.Inserts.Where(x => x.Date.Month == _settings.Month.Month && x.Date.Year == _settings.Month.Year).Count().ToString();
                else if (_settings.Parent.ShowActiveDays)
                    result = "Total Active Days: " + this.ParentCalendar.Inserts.Where(x => x.Date.Month == _settings.Month.Month && x.Date.Year == _settings.Month.Year).GroupBy(x => x.Date).Count().ToString();
                return result;
            }
        }

        public string TagD
        {
            get
            {
                string result = string.Empty;
                if (_settings.Parent.ShowAvgCost && _settings.Parent.ShowTotalCost && _settings.Parent.ShowTotalAds && _settings.Parent.ShowActiveDays)
                    result = "Total Active Days: " + this.ParentCalendar.Inserts.Where(x => x.Date.Month == _settings.Month.Month && x.Date.Year == _settings.Month.Year).GroupBy(x => x.Date).Count().ToString();
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
            get
            {
                return _settings.Month;
            }
        }

        public void PrintOutput()
        {
            this.Enabled = false;
            this.PrepareOutput();
            InteropClasses.PowerPointHelper.Instance.AppendCalendar(this);
            this.Enabled = true;
        }
        #endregion
    }
}
