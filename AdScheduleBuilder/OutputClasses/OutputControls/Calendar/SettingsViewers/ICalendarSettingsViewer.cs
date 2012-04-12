namespace AdScheduleBuilder.OutputClasses.OutputControls.Calendar.SettingsViewers
{
    public interface ICalendarSettingsViewer
    {
        string Title { get; }
        string FormToggleChangeCaption { get; }
        string EditButtonText { get; }
        string ApplyForAllText { get; }
        bool ShowApplyForAll { get; }
        void LoadSettings(ConfigurationClasses.MonthCalendarViewSettings settings);
        void SaveSettings();
        void ApplySettingsForAll(ConfigurationClasses.MonthCalendarViewSettings[] allSettings);
    }
}
