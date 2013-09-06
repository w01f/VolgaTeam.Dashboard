using System.Drawing;
using NewBizWiz.Core.AdSchedule;

namespace NewBizWiz.AdSchedule.Controls.PresentationClasses.OutputClasses.OutputControls.Calendar.SettingsViewers
{
	public interface ICalendarSettingsViewer
	{
		string Title { get; }
		Image Logo { get; }
		string FormToggleChangeCaption { get; }
		string EditButtonText { get; }
		string ApplyForAllText { get; }
		bool ShowApplyForAll { get; }
		void LoadSettings(OutputCalendarControl calendarControl, MonthCalendarViewSettings settings);
		void SaveSettings();
		void ApplySettingsForAll(MonthCalendarViewSettings[] allSettings);
	}
}