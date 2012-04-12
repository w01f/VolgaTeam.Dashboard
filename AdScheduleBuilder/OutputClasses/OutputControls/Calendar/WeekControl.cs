using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace AdScheduleBuilder.OutputClasses.OutputControls
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class WeekControl : UserControl
    {

        private MonthViewControl _parent = null;
        private List<DayControl> _dayControls = new List<DayControl>();

        private int _maxWeekDayIndex = 0;
        private int _minWeekDayIndex = 0;
        private WeekEmptySpaceControl _header = null;
        private WeekEmptySpaceControl _footer = null;

        public DayOutput[] DayOutput
        {
            get
            { 
                List<DayOutput> result = new List<DayOutput>();
                foreach(DayControl day in _dayControls)
                    result.Add(day.Output);
                return result.ToArray();
            }
        }

        public WeekControl(MonthViewControl parent)
        {
            _parent = parent;
            InitializeComponent();
            this.Dock = DockStyle.Top;
            this.Resize += new EventHandler(WeekControl_Resize);
        }

        private void FitDayControls()
        {
            double height = this.Height;
            double width = this.Width/ 7;

            if (_header != null)
            {
                _header.Top = 0;
                _header.Left = 0;
                _header.Height = (int)height;
                _header.Width = ((int)width * (_minWeekDayIndex - 1));
            }

            if (_footer != null)
            {
                _footer.Top = 0;
                _footer.Left = (int)width * _maxWeekDayIndex;
                _footer.Height = (int)height;
                _footer.Width = this.Width - ((int)width * _maxWeekDayIndex);
            }

            foreach (DayControl dayControl in _dayControls)
            {
                dayControl.Top = 0;
                dayControl.Height = (int)height;
                dayControl.Left = (dayControl.WeekDayIndex - 1) * (int)width;
                dayControl.Width = dayControl.WeekDayIndex == 7 ? this.Width - ((int)width * 6) : (int)width;
            }
        }

        public void Init(DateTime[] days)
        {
            this.Controls.Clear();
            _dayControls.Clear();
            foreach (DateTime date in days)
            {
                DayControl day = new DayControl(_parent);
                day.Init(date);
                _dayControls.Add(day);
                this.Controls.Add(day);
            }
            _maxWeekDayIndex = _dayControls.Max(x => x.WeekDayIndex);
            if (_maxWeekDayIndex < 7)
            {
                _footer = new WeekEmptySpaceControl();
                this.Controls.Add(_footer);
            }

            _minWeekDayIndex = _dayControls.Min(x => x.WeekDayIndex);
            if (_maxWeekDayIndex > 1)
            {
                _header = new WeekEmptySpaceControl();
                this.Controls.Add(_header);
            }

            FitDayControls();
        }

        public void RefreshData()
        {
            foreach (DayControl day in _dayControls)
                day.RefreshData();
        }

        void WeekControl_Resize(object sender, EventArgs e)
        {
            FitDayControls();
        }
    }
}
