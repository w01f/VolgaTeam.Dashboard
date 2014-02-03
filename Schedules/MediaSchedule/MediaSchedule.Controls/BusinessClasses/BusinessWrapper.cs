using System;
using System.IO;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.MediaSchedule;

namespace NewBizWiz.MediaSchedule.Controls.BusinessClasses
{
	public class BusinessWrapper
	{
		private static readonly BusinessWrapper _instance = new BusinessWrapper();

		private BusinessWrapper()
		{
			OutputManager = new OutputManager();
			ScheduleManager = new ScheduleManager(OutputManager.LoadBroadcastMonthTemplates);
			HelpManager = new HelpManager(MediaMetaData.Instance.SettingsManager.HelpLinksPath);
			TabPageManager = new TabPageManager(Path.Combine(Path.GetDirectoryName(typeof(TabPageManager).Assembly.Location), String.Format("{0}_tab_names.xml", MediaMetaData.Instance.DataTypeString)));
			ThemeManager = new ThemeManager(Path.Combine(SettingsManager.Instance.ThemeCollectionPath, SettingsManager.Instance.SlideMasterFolder));
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