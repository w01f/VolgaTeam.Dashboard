namespace AdScheduleBuilder.OutputClasses
{
    public interface ISettingsContainer
    {
        ConfigurationClasses.SlideBulletsState SlideBulletsState { get; }
        ConfigurationClasses.SlideHeaderState SlideHeaderState { get; }
        bool SettingsNotSaved { get; set; }
    }
}
