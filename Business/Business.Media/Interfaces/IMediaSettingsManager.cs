using Asa.Business.Calendar.Configuration;
using Asa.Business.Common.Interfaces;
using Asa.Business.Solutions.Dashboard.Configuration;

namespace Asa.Business.Media.Interfaces
{
	public interface IMediaSettingsManager : IThemeSettingsContainer, IDashboardSettingsContainer
	{
		string SelectedColor { get; set; }
		bool UseSlideMaster { get; set; }
		CalendarSettings BroadcastCalendarSettings { get; }
		void LoadSettings();
	}
}
