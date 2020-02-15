using System.Drawing;

namespace Asa.Media.Resources
{
    public partial class ResourceContainer
    {
        public Image HomeDefaultTopLogo => Controls.Home.Resource.Default;
        public Image HomeDefaultBottomLogo => Controls.Home.Resource.DefaultBottom;
        public Image HomeSplashBottomLogo => Controls.Home.Resource.BottomRight;
        public Image HomeDateStartImage => Ribbon.Home.Resource.DateStart;
        public Image HomeDateEndImage => Ribbon.Home.Resource.DateEnd;
        public Image HomeTopTitleImage => Controls.Home.Resource.Subtab1Top;
        public Image HomeBottomTitleImage => Controls.Home.Resource.Subtab1Bottom;
        public Image HomeWeeklyScheduleImage => Controls.Home.Resource.Top1;
        public Image HomeMonthlyScheduleImage => Controls.Home.Resource.Top2;
        public Image HomeSnaphotShortcutImage => Controls.Home.Resource.Bottom1;
        public Image HomeOptionsShortcutImage => Controls.Home.Resource.Bottom2;
        public Image HomeCalendarShortcutImage => Controls.Home.Resource.Bottom3;
        public Image HomeNewSchedulePopupLogo => Controls.OpenForm.Resource.FileName;
        public Image HomeOpenSchedulePopupImage => Controls.OpenForm.Resource.Open;
        public Image HomeDeleteSchedulePopupImage => Controls.OpenForm.Resource.Delete;
        public Image HomeSettingsDemoImage => Controls.Home.Resource.PreferencesDemo;
        public Image HomeSettingsDaypartsImage => Controls.Home.Resource.PreferencesDayparts;
        public Image HomeSettingsCalendarTypeImage => Controls.Home.Resource.PreferencesCalendar;
        public Image HomeSettingsFlightDatesLogo => Controls.Home.Resource.PopupDates;
    }
}
