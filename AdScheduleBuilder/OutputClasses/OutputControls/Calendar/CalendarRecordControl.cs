using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AdScheduleBuilder.OutputClasses.OutputControls
{
    [System.ComponentModel.ToolboxItem(false)]
    public abstract partial class CalendarRecordControl : DevExpress.XtraEditors.LabelControl
    {
        protected DayControl _parent = null;
        protected int _insertsCount = 0;

        public CalendarRecordControl(DayControl parent)
        {
            InitializeComponent();
            _parent = parent;
            this.AllowHtmlString = true;
            this.UseMnemonic = false;
            this.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.Dock = DockStyle.Top;
            this.MouseMove += new MouseEventHandler(DayRecordControl_MouseMove);
            this.DoubleClick += new System.EventHandler(DayRecordControl_DoubleClick);
        }

        public virtual void Init(int insertsCount)
        {
            _insertsCount = insertsCount;
            RefreshData();
        }

        protected abstract string GetControlText();

        public string GetOutputText()
        {
            return Regex.Replace(GetControlText(), @"<[^>]*>", string.Empty);
        }

        protected virtual void GetFont()
        {
            if (_insertsCount <= 3)
                this.Font = new Font("Arial", 10);
            else if (_insertsCount > 3 && _insertsCount <= 6)
                this.Font = new Font("Arial", 9);
            else if (_insertsCount > 6)
                this.Font = new Font("Arial", 8);
            this.Appearance.Font = this.Font;
            this.Appearance.Options.UseFont = true;
        }

        public void RefreshData()
        {
            GetFont();
            this.Text = GetControlText();
            this.Refresh();
        }

        private void DayRecordControl_MouseMove(object sender, MouseEventArgs e)
        {
            this.Parent.Focus();
        }

        protected virtual void DayRecordControl_DoubleClick(object sender, System.EventArgs e)
        {
            _parent.ShowDayView();
        }
    }

    public class DayRecordControl : CalendarRecordControl
    {
        private BusinessClasses.Insert _insert = null;
        private bool _isDayView = false;

        public DayRecordControl(DayControl parent)
            : base(parent)
        {
        }

        public void Init(BusinessClasses.Insert insert, int insertsCount, bool isDayView)
        {
            _isDayView = isDayView;
            _insert = insert;
            base.Init(insertsCount);
        }

        protected override string GetControlText()
        {
            string text = string.Empty;

            text += "<b>" + ((_insertsCount <= 1 || _isDayView) && !_parent.ParentMonth.Settings.Parent.ShowAbbreviationOnly ? _insert.Publication : _parent.ParentMonth.Settings.GetLegendCodeByDescription(_insert.Publication)) + "</b>";

            if (!string.IsNullOrEmpty(_insert.Section) && _parent.ParentMonth.Settings.Parent.ShowSection)
                text += " " + (_isDayView ? _insert.Section : _parent.ParentMonth.Settings.GetLegendCodeByDescription(_insert.Section));

            if (!string.IsNullOrEmpty(_insert.DimensionsShort) && _parent.ParentMonth.Settings.Parent.ShowAdSize && _insert.PublicationSquare > 0)
                text += " " + _insert.DimensionsShort;

            if (!string.IsNullOrEmpty(_insert.PageSize) && _parent.ParentMonth.Settings.Parent.ShowPageSize)
                text += " " + _insert.PageSize;

            if (!string.IsNullOrEmpty(_insert.PercentOfPage) && _parent.ParentMonth.Settings.Parent.ShowPercentOfPage && BusinessClasses.ListManager.Instance.ShareUnits.Count > 0)
                text += " " + _insert.PercentOfPage;

            if (!string.IsNullOrEmpty(_insert.PublicationColor) && _parent.ParentMonth.Settings.Parent.ShowColor)
                text += " " + _insert.PublicationColor;

            if (_parent.ParentMonth.Settings.Parent.ShowCost)
                text += " " + (_insertsCount <= 3 || _insert.FinalRate < 1000 || _isDayView ? _insert.FinalRate.ToString("$#,##0") : ((_insert.FinalRate / 1000).ToString("$#,##0") + "k"));

            if (_isDayView)
                text += "<br> ";
            return text;
        }

        protected override void GetFont()
        {
            if (_isDayView)
            {
                this.Font = new Font("Arial", 10);
                this.Appearance.Font = this.Font;
                this.Appearance.Options.UseFont = true;
            }
            else
                base.GetFont();
        }

        protected override void DayRecordControl_DoubleClick(object sender, System.EventArgs e)
        {
            if (!_isDayView)
                _parent.ShowDayView();
        }
    }

    public class CustomNoteRecordControl : CalendarRecordControl
    {
        public CustomNoteRecordControl(DayControl parent)
            : base(parent)
        {
        }

        protected override string GetControlText()
        {
            List<string> text = new List<string>();
            ConfigurationClasses.CalendarDayInfo dayCustomNote = _parent.ParentMonth.ParentCalendar.LocalSchedule.ViewSettings.CalendarViewSettings.DayCustomNotes.Where(x => x.Day.Equals(_parent.Date)).FirstOrDefault();
            if (dayCustomNote != null && !string.IsNullOrEmpty(dayCustomNote.Info))
                text.Add(dayCustomNote.Info);
            ConfigurationClasses.CalendarDayInfo dayDeadline = _parent.ParentMonth.ParentCalendar.LocalSchedule.ViewSettings.CalendarViewSettings.DayDeadlines.Where(x => x.Day.Equals(_parent.Date)).FirstOrDefault();
            if (dayDeadline != null && !string.IsNullOrEmpty(dayDeadline.Info) )
                text.Add(dayDeadline.Info);
            return string.Join(", ", text.ToArray());
        }
    }
}
