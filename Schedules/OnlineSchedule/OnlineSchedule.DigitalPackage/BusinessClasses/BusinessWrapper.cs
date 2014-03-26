using System.IO;
using NewBizWiz.CommonGUI.RateCard;
using NewBizWiz.Core.Common;

namespace NewBizWiz.OnlineSchedule.DigitalPackage.BusinessClasses
{
	public class BusinessWrapper
	{
		private static readonly BusinessWrapper _instance = new BusinessWrapper();

		private BusinessWrapper()
		{
			ScheduleManager = new ScheduleManager();
			HelpManager = new HelpManager(SettingsManager.Instance.HelpLinksPath);
			OutputManager = new OutputManager();
			ThemeManager = new ThemeManager(Path.Combine(Core.Common.SettingsManager.Instance.ThemeCollectionPath, Core.Common.SettingsManager.Instance.SlideMasterFolder));
			TabPageManager = new TabPageManager(Path.Combine(Path.GetDirectoryName(typeof(TabPageManager).Assembly.Location), "webquick_tab_names.xml"));
			RateCardManager = new RateCardManager(Core.Common.SettingsManager.Instance.RateCardPath);
			SettingsManager.Instance.InitThemeHelper(ThemeManager);
		}

		public static BusinessWrapper Instance
		{
			get { return _instance; }
		}

		public ScheduleManager ScheduleManager { get; private set; }
		public HelpManager HelpManager { get; private set; }
		public OutputManager OutputManager { get; private set; }
		public TabPageManager TabPageManager { get; private set; }
		public ThemeManager ThemeManager { get; private set; }
		public RateCardManager RateCardManager { get; private set; }
	}
}