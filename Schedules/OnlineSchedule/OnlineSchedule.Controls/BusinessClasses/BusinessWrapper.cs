using System.IO;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.OnlineSchedule;

namespace NewBizWiz.OnlineSchedule.Controls.BusinessClasses
{
	public class BusinessWrapper
	{
		private static readonly BusinessWrapper _instance = new BusinessWrapper();

		private BusinessWrapper()
		{
			ScheduleManager = new ScheduleManager();
			HelpManager = new HelpManager(Core.OnlineSchedule.SettingsManager.Instance.HelpLinksPath);
			OutputManager = new OutputManager();
			TabPageManager = new TabPageManager(Path.Combine(Path.GetDirectoryName(typeof(TabPageManager).Assembly.Location), "digital_tab_names.xml"));
			ThemeManager = new ThemeManager(Core.OnlineSchedule.SettingsManager.Instance.ThemeCollectionPath);
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
	}
}