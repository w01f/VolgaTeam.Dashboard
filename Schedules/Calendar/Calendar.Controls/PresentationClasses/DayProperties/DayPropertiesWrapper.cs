using System;
using System.Drawing;
using CalendarBuilder.BusinessClasses;
using CalendarBuilder.ConfigurationClasses;
using CalendarBuilder.PresentationClasses.Calendars;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraTab;

namespace CalendarBuilder.PresentationClasses.DayProperties
{
	public class DayPropertiesWrapper
	{
		private readonly DockPanel _container;
		private readonly ICalendarControl _parentCalendar;
		private bool _allowToSave;

		public DayPropertiesWrapper(ICalendarControl parentCalendar, DockPanel container)
		{
			_parentCalendar = parentCalendar;

			_container = container;
			_container.ClosingPanel += ClosingPanel;
			_container.ClosedPanel += ClosedPanel;
			_container.DockChanged += PanelDockChanged;
			_container.DoubleClick += PanelDoubleClick;


			ContainedControl = new DayPropertiesControl();
			ContainedControl.PropertiesSaved += PropertiesSaved;
			ContainedControl.Closed += PropertiesClosed;
			ContainedControl.PropertiesGroupChanged += PropertiesGroupChanged;
		}

		#region Container Event Handlers
		private void ClosedPanel(object sender, DockPanelEventArgs e)
		{
			if (Closed != null)
				Closed(this, new EventArgs());
			_parentCalendar.Splash(false);
		}

		private void ClosingPanel(object sender, DockPanelCancelEventArgs e)
		{
			_parentCalendar.Splash(true);
			SaveData();
			SaveSettings();
		}

		private void PanelDockChanged(object sender, EventArgs e)
		{
			if (_allowToSave)
				SaveSettings();
		}

		private void PanelDoubleClick(object sender, EventArgs e)
		{
			_allowToSave = false;
			if (_container.Dock == DockingStyle.Float)
			{
				_container.Dock = DockingStyle.Right;
			}
			else
			{
				_container.Dock = DockingStyle.Float;
				if (_parentCalendar.CalendarSettings.DayPropertiesFloatLeft != 0 && _parentCalendar.CalendarSettings.DayPropertiesFloatTop != 0)
					_container.FloatLocation = new Point(_parentCalendar.CalendarSettings.DayPropertiesFloatLeft, _parentCalendar.CalendarSettings.DayPropertiesFloatTop);
				else
					_container.FloatLocation = new Point(500, 200);
			}
			_allowToSave = true;
			SaveSettings();
		}
		#endregion

		#region Contained Control Event Handlers
		private void PropertiesSaved(object sender, EventArgs e)
		{
			if (DataSaved != null)
				DataSaved(this, new EventArgs());
		}

		private void PropertiesClosed(object sender, EventArgs e)
		{
			Close();
		}

		private void PropertiesGroupChanged(object sender, TabPageChangedEventArgs e)
		{
			_container.Text = e.Page.Tooltip;
		}
		#endregion

		#region Common Methods
		private void LoadSettings()
		{
			if (_parentCalendar.CalendarSettings.DayPropertiesFloatLeft != 0 && _parentCalendar.CalendarSettings.DayPropertiesFloatTop != 0)
				_container.FloatLocation = new Point(_parentCalendar.CalendarSettings.DayPropertiesFloatLeft, _parentCalendar.CalendarSettings.DayPropertiesFloatTop);
			else
				_container.FloatLocation = new Point(500, 200);
			_container.Dock = _parentCalendar.CalendarSettings.DayPropertiesDocked ? DockingStyle.Right : DockingStyle.Float;
		}

		private void SaveSettings()
		{
			if (_container.Visibility == DockVisibility.Visible)
				_parentCalendar.CalendarSettings.DayPropertiesDocked = _container.Dock == DockingStyle.Right;
			_parentCalendar.CalendarSettings.DayPropertiesFloatLeft = _container.FloatLocation.X;
			_parentCalendar.CalendarSettings.DayPropertiesFloatTop = _container.FloatLocation.Y;
			SettingsManager.Instance.ViewSettings.Save();
		}

		public void LoadData(CalendarDay day = null)
		{
			SaveData();
			if (day == null)
				ContainedControl.LoadCurrentDayData();
			else
				ContainedControl.LoadData(day);
		}

		public void SaveData(bool force = false)
		{
			if (ContainedControl.SettingsNotSaved && !force)
			{
				ContainedControl.SaveData();
			}
			else if (force)
				ContainedControl.SaveData();
		}

		public void Show()
		{
			_parentCalendar.Splash(true);
			_allowToSave = false;
			_container.Visibility = DockVisibility.Visible;
			LoadSettings();
			_allowToSave = true;
			if (Shown != null)
				Shown(this, new EventArgs());
			_parentCalendar.Splash(false);
		}

		public void Close()
		{
			_parentCalendar.Splash(true);
			_container.Close();
			_parentCalendar.Splash(false);
		}

		public void Decorate(CalendarStyle style)
		{
			ContainedControl.Decorate(style);
		}
		#endregion

		public DayPropertiesControl ContainedControl { get; private set; }
		public bool SettingsNotSaved
		{
			get { return ContainedControl.SettingsNotSaved; }
		}

		public event EventHandler<EventArgs> Shown;
		public event EventHandler<EventArgs> Closed;
		public event EventHandler<EventArgs> DataSaved;
	}
}