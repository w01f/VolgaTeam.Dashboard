using NewBizWiz.MediaSchedule.Controls.InteropClasses;
using NewBizWiz.QuickShare.Controls.BusinessClasses;

namespace NewBizWiz.QuickShare.Controls.InteropClasses
{
	public class QuickSharePowerPointHelper : MediaSchedulePowerPointHelper<QuickSharePowerPointHelper>
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
	}
}
