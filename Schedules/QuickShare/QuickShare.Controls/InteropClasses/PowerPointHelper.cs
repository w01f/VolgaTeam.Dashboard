using NewBizWiz.MediaSchedule.Controls.InteropClasses;
using NewBizWiz.QuickShare.Controls.BusinessClasses;

namespace NewBizWiz.QuickShare.Controls.InteropClasses
{
	public class QuickSharePowerPointHelper : MediaSchedulePowerPointHelper<QuickSharePowerPointHelper>
	{
		protected override string OneSheetTemplatePath
		{
			get { return BusinessWrapper.Instance.OutputManager.OneSheetTemplatesFolderPath; }
		}

		protected override string OptionsTemplatePath
		{
			get { return BusinessWrapper.Instance.OutputManager.OptionsTemplatesFolderPath; }
		}

		protected override string SnapshotTemplatePath
		{
			get { return BusinessWrapper.Instance.OutputManager.SnapshotTemplatesFolderPath; }
		}

		protected override string ContractTemplatePath
		{
			get { return BusinessWrapper.Instance.OutputManager.ContractTemplatesFolderPath; }
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
