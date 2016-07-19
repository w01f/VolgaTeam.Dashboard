using Asa.Business.Calendar.Configuration;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.Themes;

namespace Asa.Business.Media.Interfaces
{
	public interface IMediaSettingsManager
	{
		string SelectedColor { get; set; }
		bool UseSlideMaster { get; set; }
		CalendarSettings BroadcastCalendarSettings { get; }
		void LoadSettings();
		void SaveSettings();
		void InitThemeHelper(ThemeManager themeManager);
		string GetSelectedThemeName(SlideType slideType);
		Theme GetSelectedTheme(SlideType slideType);
		void SetSelectedTheme(SlideType slideType, string themeName);
	}
}
