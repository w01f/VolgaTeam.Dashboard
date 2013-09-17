using System.IO;
using NewBizWiz.Core.AdSchedule;
using NewBizWiz.Core.Common;

namespace NewBizWiz.AdSchedule.Controls.BusinessClasses
{
	public class BusinessWrapper
	{
		private static readonly BusinessWrapper _instance = new BusinessWrapper();

		private BusinessWrapper()
		{
			ScheduleManager = new ScheduleManager();
			HelpManager = new HelpManager(Core.AdSchedule.SettingsManager.Instance.HelpLinksPath);
			OutputManager = new OutputManager();
			RateCardManager = new RateCardManager(Core.AdSchedule.SettingsManager.Instance.RateCardPath);
			TabPageManager = new TabPageManager(Path.Combine(Path.GetDirectoryName(typeof(TabPageManager).Assembly.Location), "adsched_tab_names.xml"));
			ThemeManager = new ThemeManager(Core.AdSchedule.SettingsManager.Instance.ThemeCollectionPath);
		}

		public static BusinessWrapper Instance
		{
			get { return _instance; }
		}

		public ScheduleManager ScheduleManager { get; private set; }
		public HelpManager HelpManager { get; private set; }
		public OutputManager OutputManager { get; private set; }
		public RateCardManager RateCardManager { get; private set; }
		public TabPageManager TabPageManager { get; private set; }
		public ThemeManager ThemeManager { get; private set; }
	}
}