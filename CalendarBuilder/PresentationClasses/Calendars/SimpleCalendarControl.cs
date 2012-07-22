namespace CalendarBuilder.PresentationClasses.Calendars
{
    [System.ComponentModel.ToolboxItem(false)]
    public class SimpleCalendarControl : CalendarControl
    {
        public SimpleCalendarControl()
            : base()
        {
            _calendarStyle = BusinessClasses.CalendarStyle.Simple;
        }
    }
}

