namespace CalendarBuilder.PresentationClasses.Calendars
{
    [System.ComponentModel.ToolboxItem(false)]
    public class GraphicCalendarControl : CalendarControl
    {
        public GraphicCalendarControl()
            : base()
        {
            _calendarStyle = BusinessClasses.CalendarStyle.Graphic;
        }
    }
}

