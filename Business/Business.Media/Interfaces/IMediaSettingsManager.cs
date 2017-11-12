using Asa.Business.Calendar.Configuration;
using Asa.Business.Common.Interfaces;
using Asa.Business.Solutions.Dashboard.Configuration;
using Asa.Business.Solutions.StarApp.Configuration;
using Asa.Common.Core.Interfaces;

namespace Asa.Business.Media.Interfaces
{
	public interface IMediaSettingsManager : IThemeSettingsContainer, IDashboardSettingsContainer, IStarAppSettingsContainer
	{
		string SelectedColor { get; set; }
		bool UseSlideMaster { get; set; }
		CalendarSettings BroadcastCalendarSettings { get; }
		void LoadSettings();
	}
}
