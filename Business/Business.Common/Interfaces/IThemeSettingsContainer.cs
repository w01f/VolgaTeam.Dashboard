using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.Themes;

namespace Asa.Business.Common.Interfaces
{
	public interface IThemeSettingsContainer : IBaseSettingsContainer
	{
		bool ApplyThemeForAllSlideTypes { get; set; }
		void InitThemeHelper(ThemeManager themeManager);
		string GetSelectedThemeName(SlideType slideType);
		Theme GetSelectedTheme(SlideType slideType);
		void SetSelectedTheme(SlideType slideType, string themeName, bool applyForAllSlideTypes);
	}
}
