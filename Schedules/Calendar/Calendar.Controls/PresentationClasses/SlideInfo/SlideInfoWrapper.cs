using System;
using System.ComponentModel;
using System.Windows.Forms;
using NewBizWiz.Calendar.Controls.PresentationClasses.Calendars;
using NewBizWiz.CommonGUI.RetractableBar;
using NewBizWiz.Core.Calendar;

namespace NewBizWiz.Calendar.Controls.PresentationClasses.SlideInfo
{
	public class SlideInfoWrapper
	{
		private readonly RetractableBarControl _container;
		private readonly ICalendarControl _parentCalendar;

		public SlideInfoWrapper(ICalendarControl parentCalendar, RetractableBarControl container)
		{
			_parentCalendar = parentCalendar;

			_container = container;
			_container.StateChanged += Container_StateChanged;
		}

		#region Container Event Handlers
		private void Container_StateChanged(object sender, StateChangedEventArgs e)
		{
			_parentCalendar.Splash(true);
			SaveData();
			_parentCalendar.CalendarSettings.SlideInfoVisible = e.Expaned;
			_parentCalendar.SaveSettings();
			_parentCalendar.Splash(false);
		}
		#endregion

		#region Contained Control Event Handlers
		private void PropertiesClosed(object sender, EventArgs e)
		{
			_container.Collapse();
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
				_container.Expand(true);
				_parentCalendar.Splash(false);
			}
			else
			{
				_parentCalendar.Splash(true);
				_container.Collapse(true);
				_parentCalendar.Splash(false);
			}
		}

		public void LoadData(CalendarMonth month = null, bool allowToSave = true)
		{
			if (allowToSave)
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