using System.Drawing;

namespace Asa.Common.Resources.Media
{
    public partial interface IMediaGraphicResources
    {
        Image HomeDefaultTopLogo { get; }
        Image HomeDefaultBottomLogo { get; }
        Image HomeSplashBottomLogo { get; }
        Image HomeDateStartImage { get; }
        Image HomeDateEndImage { get; }
        Image HomeTopTitleImage { get; }
        Image HomeBottomTitleImage { get; }
        Image HomeWeeklyScheduleImage { get; }
        Image HomeMonthlyScheduleImage { get; }
        Image HomeSnaphotShortcutImage { get; }
        Image HomeOptionsShortcutImage { get; }
        Image HomeCalendarShortcutImage { get; }
        Image HomeNewSchedulePopupLogo { get; }
        Image HomeOpenSchedulePopupImage { get; }
        Image HomeDeleteSchedulePopupImage { get; }
        Image HomeSettingsDemoImage { get; }
        Image HomeSettingsDaypartsImage { get; }
        Image HomeSettingsCalendarTypeImage { get; }
        Image HomeSettingsFlightDatesLogo { get; }
    }
}
