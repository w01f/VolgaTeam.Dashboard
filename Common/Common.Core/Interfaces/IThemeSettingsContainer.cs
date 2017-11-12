using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.Themes;

namespace Asa.Common.Core.Interfaces
{
	public interface IThemeSettingsContainer
	{
		bool ApplyThemeForAllSlideTypes { get; set; }
		void InitThemeHelper(ThemeManager themeManager);
		string GetSelectedThemeName(SlideType slideType);
		Theme GetSelectedTheme(SlideType slideType);
		void SetSelectedTheme(SlideType slideType, string themeName, bool applyForAllSlideTypes);
	}
}
