using System.ComponentModel;
using System.Windows.Forms;
using NewBizWiz.Core.AdSchedule;

namespace NewBizWiz.AdSchedule.Controls.PresentationClasses.OutputClasses.OutputControls.Calendar.SettingsViewers
{
	[ToolboxItem(false)]
	public partial class TitleViewerControl : UserControl, ICalendarSettingsViewer
	{
		protected OutputCalendarControl _calendarControl = null;
		private MonthCalendarViewSettings _settings;

		public TitleViewerControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
		}

		#region ICalendarSettingsViewer Members
		public string Title
		{
			get { return "Calendar Slide Title"; }
		}

		public string FormToggleChangeCaption
		{
			get { return "Slide Title"; }
		}

		public string EditButtonText
		{
			get { return "Edit Slide Title"; }
		}

		public string ApplyForAllText
		{
			get { return "Show this same  Slide Title for all Months in this schedule"; }
		}

		public bool ShowApplyForAll
		{
			get { return true; }
		}

		public void LoadSettings(OutputCalendarControl calendarControl, MonthCalendarViewSettings settings)
		{
			_calendarControl = calendarControl;
			_settings = settings;
			textEditTitle.EditValue = _settings.Title;
		}

		public void SaveSettings()
		{
			if (_settings != null)
				_settings.Title = textEditTitle.EditValue.ToString().Trim();
		}

		public void ApplySettingsForAll(MonthCalendarViewSettings[] allSettings)
		{
			if (_settings != null)
				foreach (MonthCalendarViewSettings settings in allSettings)
					if (_settings != settings)
						settings.Title = _settings.Title;
		}
		#endregion
	}
}