using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CalendarBuilder.PresentationClasses.Calendars
{
    public interface ICalendarControl
    {
        bool AllowToSave { get; }
        bool SettingsNotSaved { get; set; }

        BusinessClasses.Calendar CalendarData { get; }
        ConfigurationClasses.CalendarSettings CalendarSettings { get; }
        Views.IView SelectedView { get; }
        DayProperties.DayPropertiesWrapper DayProperties { get; }
        SlideInfo.SlideInfoWrapper SlideInfo { get; }

        void LeaveCalendar();
        void ShowCalendar();
        bool SaveCalendarData(string scheduleName = "");
        void LoadView();
        void SaveView();
        void Preview();
        void Print();
        void Email();
        void OpenHelp();
        void Splash(bool show);
    }
}
