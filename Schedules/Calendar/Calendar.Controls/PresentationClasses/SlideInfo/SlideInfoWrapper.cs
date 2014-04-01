using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
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

		private void OnPropertyChanged(object sender, EventArgs e)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, EventArgs.Empty);
		}
		#endregion

		#region Common Methods

		public void InitControl<TControl>() where TControl : ISlideInfoControl
		{
			_containedControl = (TControl)Activator.CreateInstance(typeof(TControl));
			_containedControl.Closed += PropertiesClosed;
			_containedControl.PropertyChanged += OnPropertyChanged;
		}

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
			_parentCalendar.SlideInfoButton.Checked = _parentCalendar.CalendarSettings.SlideInfoVisible;
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
			_parentCalendar.SaveSettings();
		}

		private void SaveLocationSettings()
		{
			if (_container.Visibility == DockVisibility.Visible)
				_parentCalendar.CalendarSettings.SlideInfoDocked = _container.Dock == DockingStyle.Left;
			_parentCalendar.CalendarSettings.SlideInfoFloatLeft = _container.FloatLocation.X;
			_parentCalendar.CalendarSettings.SlideInfoFloatTop = _container.FloatLocation.Y;
			_parentCalendar.SaveSettings();
		}

		public void LoadData(CalendarMonth month = null, bool allowToSave = true)
		{
			if(allowToSave)
				SaveData();
			if (month == null)
				_containedControl.LoadCurrentMonthData();
			else
				_containedControl.LoadMonth(month);
			_container.Text = _containedControl.MonthTitle;
		}

		public void SaveData()
		{
			_containedControl.SaveData();
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

		private ISlideInfoControl _containedControl;
		public Control ContainedControl
		{
			get { return _containedControl as UserControl; }
		}

		public bool SettingsNotSaved
		{
			get { return _containedControl.SettingsNotSaved; }
		}

		public event EventHandler<EventArgs> Shown;
		public event EventHandler<EventArgs> Closed;
		public event EventHandler<EventArgs> PropertyChanged;
	}

	public interface ISlideInfoControl
	{
		string MonthTitle { get; set; }
		bool SettingsNotSaved { get; set; }

		[Browsable(true)]
		[Category("Action")]
		event EventHandler Closed;

		[Browsable(true)]
		[Category("Action")]
		event EventHandler<EventArgs> PropertyChanged;

		void LoadCurrentMonthData();
		void LoadMonth(CalendarMonth month);
		void SaveData();
	}
}