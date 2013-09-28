using System;
using System.Drawing;
using DevExpress.XtraBars.Docking;
using NewBizWiz.Calendar.Controls.PresentationClasses.Calendars;
using NewBizWiz.Core.Calendar;

namespace NewBizWiz.Calendar.Controls.PresentationClasses.SlideInfo
{
	public class SlideInfoWrapper
	{
		private readonly DockPanel _container;
		private readonly ICalendarControl _parentCalendar;
		private bool _allowToSave;

		public SlideInfoWrapper(ICalendarControl parentCalendar, DockPanel container)
		{
			_parentCalendar = parentCalendar;

			_container = container;
			_container.ClosingPanel += ClosingPanel;
			_container.ClosedPanel += ClosedPanel;
			_container.DockChanged += PanelDockChanged;
			_container.DoubleClick += PanelDoubleClick;

			ContainedControl = new SlideInfoControl();
			ContainedControl.Closed += PropertiesClosed;
			ContainedControl.ThemeChanged += OnThemeChanged;
		}

		#region Container Event Handlers
		private void ClosedPanel(object sender, DockPanelEventArgs e)
		{
			if (_allowToSave)
				SaveVisibilitySettings();
			if (Closed != null)
				Closed(this, new EventArgs());
			_parentCalendar.Splash(false);
		}

		private void ClosingPanel(object sender, DockPanelCancelEventArgs e)
		{
			_parentCalendar.Splash(true);
			SaveData();
			SaveLocationSettings();
		}

		private void PanelDockChanged(object sender, EventArgs e)
		{
			if (_allowToSave)
				SaveLocationSettings();
		}

		private void PanelDoubleClick(object sender, EventArgs e)
		{
			_allowToSave = false;
			if (_container.Dock == DockingStyle.Float)
			{
				_container.Dock = DockingStyle.Left;
			}
			else
			{
				_container.Dock = DockingStyle.Float;
				if (_parentCalendar.CalendarSettings.SlideInfoFloatLeft != 0 && _parentCalendar.CalendarSettings.SlideInfoFloatTop != 0)
					_container.FloatLocation = new Point(_parentCalendar.CalendarSettings.SlideInfoFloatLeft, _parentCalendar.CalendarSettings.SlideInfoFloatTop);
				else
					_container.FloatLocation = new Point(200, 200);
			}
			_allowToSave = true;
			SaveLocationSettings();
		}
		#endregion

		#region Contained Control Event Handlers
		private void PropertiesClosed(object sender, EventArgs e)
		{
			Close();
		}

		private void OnThemeChanged(object sender, EventArgs e)
		{
			if (ThemeChanged != null)
				ThemeChanged(this, EventArgs.Empty);
		}
		#endregion

		#region Common Methods
		public void LoadVisibilitySettings()
		{
			if (_parentCalendar.CalendarSettings.SlideInfoVisible)
			{
				_parentCalendar.Splash(true);
				Show();
				_parentCalendar.Splash(false);
			}
			else
			{
				_parentCalendar.Splash(true);
				Close();
				_parentCalendar.Splash(false);
			}
			Controller.Instance.CalendarVisualizer.SlideInfoButtonItem.Checked = _parentCalendar.CalendarSettings.SlideInfoVisible;
		}

		private void LoadLocationSettings()
		{
			_container.Dock = _parentCalendar.CalendarSettings.SlideInfoDocked ? DockingStyle.Left : DockingStyle.Float;
			if (_parentCalendar.CalendarSettings.SlideInfoFloatLeft != 0 && _parentCalendar.CalendarSettings.SlideInfoFloatTop != 0)
				_container.FloatLocation = new Point(_parentCalendar.CalendarSettings.SlideInfoFloatLeft, _parentCalendar.CalendarSettings.SlideInfoFloatTop);
			else
				_container.FloatLocation = new Point(200, 200);
		}

		private void SaveVisibilitySettings()
		{
			_parentCalendar.CalendarSettings.SlideInfoVisible = _container.Visibility == DockVisibility.Visible;
			SettingsManager.Instance.ViewSettings.Save();
		}

		private void SaveLocationSettings()
		{
			if (_container.Visibility == DockVisibility.Visible)
				_parentCalendar.CalendarSettings.SlideInfoDocked = _container.Dock == DockingStyle.Left;
			_parentCalendar.CalendarSettings.SlideInfoFloatLeft = _container.FloatLocation.X;
			_parentCalendar.CalendarSettings.SlideInfoFloatTop = _container.FloatLocation.Y;
			SettingsManager.Instance.ViewSettings.Save();
		}

		public void LoadData(CalendarMonth month = null)
		{
			SaveData();
			if (month == null)
				ContainedControl.LoadCurrentMonthData();
			else
				ContainedControl.LoadMonth(month);
			_container.Text = ContainedControl.MonthTitle;
		}

		public void SaveData()
		{
			ContainedControl.SaveData();
		}

		public void Show()
		{
			_allowToSave = false;
			_container.Visibility = DockVisibility.Visible;
			LoadLocationSettings();
			_allowToSave = true;
			SaveVisibilitySettings();
			if (Shown != null)
				Shown(this, new EventArgs());
		}

		public void Close(bool allowToSave = true)
		{
			_allowToSave = allowToSave;
			_container.Close();
		}
		#endregion

		public SlideInfoControl ContainedControl { get; private set; }
		public bool SettingsNotSaved
		{
			get { return ContainedControl.SettingsNotSaved; }
		}

		public event EventHandler<EventArgs> Shown;
		public event EventHandler<EventArgs> Closed;
		public event EventHandler<EventArgs> DateSaved;
		public event EventHandler<EventArgs> ThemeChanged;
	}
}