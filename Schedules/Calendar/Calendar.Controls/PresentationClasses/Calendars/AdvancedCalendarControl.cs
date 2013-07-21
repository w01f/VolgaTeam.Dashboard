namespace CalendarBuilder.PresentationClasses.Calendars
{
    [System.ComponentModel.ToolboxItem(false)]
    public class AdvancedCalendarControl : CalendarControl
    {
        public AdvancedCalendarControl()
            : base()
        {
            _calendarStyle = BusinessClasses.CalendarStyle.Advanced;
        }
    }
}


