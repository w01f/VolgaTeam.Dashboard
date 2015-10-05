using System;
using System.IO;
using NewBizWiz.CommonGUI.RateCard;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.MediaSchedule;

namespace NewBizWiz.MediaSchedule.Controls.BusinessClasses
{
	public class BusinessWrapper
	{
		private static readonly BusinessWrapper _instance = new BusinessWrapper();

		private BusinessWrapper()
		{
			//OutputManager = new OutputManager();
			//ScheduleManager = new ScheduleManager();
			//HelpManager = new HelpManager(MediaMetaData.Instance.SettingsManager.HelpLinksPath);
			//TabPageManager = new TabPageManager(Path.Combine(Path.GetDirectoryName(typeof(TabPageManager).Assembly.Location), String.Format("{0}_tab_names.xml", MediaMetaData.Instance.DataTypeString)));
			//ThemeManager = new ThemeManager(Path.Combine(SettingsManager.Instance.ThemeCollectionPath, SettingsManager.Instance.SlideMasterFolder));
			////ActivityManager = new ActivityManager(MediaMetaData.Instance.SettingsManager.ActivityTrackName);
			//Gallery1Manager = new GalleryManager(Path.Combine(Path.GetDirectoryName(typeof(GalleryManager).Assembly.Location), "Gallery1.xml"));
			//Gallery2Manager = new GalleryManager(Path.Combine(Path.GetDirectoryName(typeof(GalleryManager).Assembly.Location), "Gallery2.xml"));
			//RateCardManager = new RateCardManager(SettingsManager.Instance.RateCardPath);
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
		public ActivityManager ActivityManager { get; private set; }
		public GalleryManager Gallery1Manager { get; private set; }
		public GalleryManager Gallery2Manager { get; private set; }
		public RateCardManager RateCardManager { get; private set; }
	}
}