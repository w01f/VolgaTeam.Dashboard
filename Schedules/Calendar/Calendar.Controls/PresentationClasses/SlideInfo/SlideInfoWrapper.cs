using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Asa.Business.Calendar.Entities.NonPersistent;
using Asa.Calendar.Controls.PresentationClasses.Calendars;
using Asa.Common.GUI.RetractableBar;

namespace Asa.Calendar.Controls.PresentationClasses.SlideInfo
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
			PropertyChanged?.Invoke(this, e);
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
				_container.Expand(true);
			else
				_container.Collapse(true);
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

		public void Release()
		{
			_containedControl.Release();
		}
		#endregion

		private ISlideInfoControl _containedControl;
		public Control ContainedControl
		{
			get { return (Control)_containedControl; }
		}

		public ISlideInfoControl SlideInfoControl
		{
			get { return _containedControl; }
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

		IEnumerable<ButtonInfo> GetChapters();
		void LoadCurrentMonthData();
		void LoadMonth(CalendarMonth month);
		void SaveData();
		void Release();
	}
}