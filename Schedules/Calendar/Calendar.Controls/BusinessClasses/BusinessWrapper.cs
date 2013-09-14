using System.IO;
using NewBizWiz.Core.Calendar;
using NewBizWiz.Core.Common;
using SettingsManager = NewBizWiz.Core.Calendar.SettingsManager;

namespace NewBizWiz.Calendar.Controls.BusinessClasses
{
	public class BusinessWrapper
	{
		private static readonly BusinessWrapper _instance = new BusinessWrapper();

		private BusinessWrapper()
		{
			ScheduleManager = new ScheduleManager();
			HelpManager = new HelpManager(SettingsManager.Instance.HelpLinksPath);
			OutputManager = new OutputManager();
			TabPageManager = new TabPageManager(Path.Combine(Path.GetDirectoryName(typeof(TabPageManager).Assembly.Location), "cal_tab_names.xml"));
		}

		public static BusinessWrapper Instance
		{
			get { return _instance; }
		}

		public ScheduleManager ScheduleManager { get; private set; }
		public HelpManager HelpManager { get; private set; }
		public OutputManager OutputManager { get; private set; }
		public TabPageManager TabPageManager { get; private set; }
	}
}