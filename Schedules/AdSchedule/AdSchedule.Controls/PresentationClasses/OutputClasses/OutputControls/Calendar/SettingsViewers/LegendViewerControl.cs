using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using NewBizWiz.Core.AdSchedule;

namespace NewBizWiz.AdSchedule.Controls.PresentationClasses.OutputClasses.OutputControls.Calendar.SettingsViewers
{
	[ToolboxItem(false)]
	public partial class LegendViewerControl : UserControl, ICalendarSettingsViewer
	{
		protected OutputCalendarControl _calendarControl = null;
		private MonthCalendarViewSettings _settings;

		public LegendViewerControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
		}

		#region ICalendarSettingsViewer Members
		public string Title
		{
			get { return "Show Legend Codes"; }
		}

		public Image Logo
		{
			get { return Properties.Resources.GridDetails; }
		}

		public string FormToggleChangeCaption
		{
			get { return "Slide Legend Codes"; }
		}

		public string EditButtonText
		{
			get { return "Edit Legend Codes"; }
		}

		public string ApplyForAllText
		{
			get { return "Show only these legend codes for All Month Slides"; }
		}

		public bool ShowApplyForAll
		{
			get { return true; }
		}

		public void LoadSettings(OutputCalendarControl calendarControl, MonthCalendarViewSettings settings)
		{
			_calendarControl = calendarControl;
			_settings = settings;
			gridControlLegend.DataSource = new BindingList<CalendarLegend>(_settings.Legend);
			gridControlLegend.RefreshDataSource();
		}

		public void SaveSettings()
		{
			gridViewLegend.CloseEditor();
		}

		public void ApplySettingsForAll(MonthCalendarViewSettings[] allSettings)
		{
			if (_settings != null)
				foreach (var settings in allSettings)
				{
					if (_settings != settings)
					{
						settings.Legend.Clear();
						foreach (CalendarLegend legend in _settings.Legend)
							settings.Legend.Add(legend.Clone());
					}
				}
		}
		#endregion
	}
}