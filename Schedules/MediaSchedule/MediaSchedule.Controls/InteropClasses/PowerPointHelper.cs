using NewBizWiz.MediaSchedule.Controls.BusinessClasses;

namespace NewBizWiz.MediaSchedule.Controls.InteropClasses
{
	public abstract partial class MediaSchedulePowerPointHelper<T> : CommonGUI.Interop.CommonPowerPointHelper<T> where T : class, new(){}

	public class RegularMediaSchedulePowerPointHelper : MediaSchedulePowerPointHelper<RegularMediaSchedulePowerPointHelper>
	{
		protected override string OneSheetTemplatePath
		{
			get { return BusinessWrapper.Instance.OutputManager.OneSheetTableBasedTemplatesFolderPath; }
		}

		protected override string CalendarTemplatePath
		{
			get { return BusinessWrapper.Instance.OutputManager.BroadcastCalendarTemlatesFolderPath; }
		}

		protected override string CalendarBackgroundPath
		{
			get { return BusinessWrapper.Instance.OutputManager.CalendarBackgroundFolderPath; }
		}

		protected override string StrategyTemplatePath
		{
			get { return BusinessWrapper.Instance.OutputManager.StrategyTemplatesFolderPath; }
		}

		protected override string OptionsTemplatePath
		{
			get { return BusinessWrapper.Instance.OutputManager.OptionsTemplatesFolderPath; }
		}

		protected override string SnapshotTemplatePath
		{
			get { return BusinessWrapper.Instance.OutputManager.SnapshotTemplatesFolderPath; }
		}

	}
}
