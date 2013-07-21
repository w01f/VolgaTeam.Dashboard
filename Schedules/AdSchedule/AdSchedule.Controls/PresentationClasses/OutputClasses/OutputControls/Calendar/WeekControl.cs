using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace NewBizWiz.AdSchedule.Controls.PresentationClasses.OutputClasses.OutputControls
{
	[ToolboxItem(false)]
	public partial class WeekControl : UserControl
	{
		private readonly List<DayControl> _dayControls = new List<DayControl>();
		private readonly MonthViewControl _parent;
		private WeekEmptySpaceControl _footer;
		private WeekEmptySpaceControl _header;

		private int _maxWeekDayIndex;
		private int _minWeekDayIndex;

		public WeekControl(MonthViewControl parent)
		{
			_parent = parent;
			InitializeComponent();
			Dock = DockStyle.Top;
			Resize += WeekControl_Resize;
		}

		public DayOutput[] DayOutput
		{
			get
			{
				var result = new List<DayOutput>();
				foreach (DayControl day in _dayControls)
					result.Add(day.Output);
				return result.ToArray();
			}
		}

		private void FitDayControls()
		{
			double height = Height;
			double width = Width / 7;

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
				_footer.Width = Width - ((int)width * _maxWeekDayIndex);
			}

			foreach (DayControl dayControl in _dayControls)
			{
				dayControl.Top = 0;
				dayControl.Height = (int)height;
				dayControl.Left = (dayControl.WeekDayIndex - 1) * (int)width;
				dayControl.Width = dayControl.WeekDayIndex == 7 ? Width - ((int)width * 6) : (int)width;
			}
		}

		public void Init(DateTime[] days)
		{
			Controls.Clear();
			_dayControls.Clear();
			foreach (DateTime date in days)
			{
				var day = new DayControl(_parent);
				day.Init(date);
				_dayControls.Add(day);
				Controls.Add(day);
			}
			_maxWeekDayIndex = _dayControls.Max(x => x.WeekDayIndex);
			if (_maxWeekDayIndex < 7)
			{
				_footer = new WeekEmptySpaceControl();
				Controls.Add(_footer);
			}

			_minWeekDayIndex = _dayControls.Min(x => x.WeekDayIndex);
			if (_maxWeekDayIndex > 1)
			{
				_header = new WeekEmptySpaceControl();
				Controls.Add(_header);
			}

			FitDayControls();
		}

		public void RefreshData()
		{
			_dayControls.ForEach(d => d.RefreshData());
		}

		public void ApplyThemeColor(Color colorLight, Color colorDark)
		{
			if (_footer != null)
				_footer.ApplyThemeColor(colorLight);
			if (_header != null)
				_header.ApplyThemeColor(colorLight);
			_dayControls.ForEach(d => d.ApplyThemeColor(colorLight, colorDark));
		}

		private void WeekControl_Resize(object sender, EventArgs e)
		{
			FitDayControls();
		}
	}
}