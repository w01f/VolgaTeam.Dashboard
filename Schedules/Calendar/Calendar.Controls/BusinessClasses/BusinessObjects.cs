using Asa.CommonGUI.RateCard;
using Asa.Core.Calendar;
using Asa.Core.Common;

namespace Asa.Calendar.Controls.BusinessClasses
{
	public class BusinessObjects
	{
		private static readonly BusinessObjects _instance = new BusinessObjects();

		private BusinessObjects()
		{
			//ScheduleManager = new ScheduleManager();
			//HelpManager = new HelpManager(SettingsManager.Instance.HelpLinksPath);
			//OutputManager = new OutputManager();
			//TabPageManager = new TabPageManager(Path.Combine(Path.GetDirectoryName(typeof(TabPageManager).Assembly.Location), "cal_tab_names.xml"));
			////ActivityManager = new ActivityManager("calendar");
			//RateCardManager = new RateCardManager(Core.Common.SettingsManager.Instance.RateCardPath);
			//Gallery1Manager = new GalleryManager(Path.Combine(Path.GetDirectoryName(typeof(GalleryManager).Assembly.Location), "Gallery1.xml"));
			//Gallery2Manager = new GalleryManager(Path.Combine(Path.GetDirectoryName(typeof(GalleryManager).Assembly.Location), "Gallery2.xml"));
		}

		public static BusinessObjects Instance
		{
			get { return _instance; }
		}

		public ScheduleManager ScheduleManager { get; private set; }
		public ActivityManager ActivityManager { get; private set; }
		public HelpManager HelpManager { get; private set; }
		public OutputManager OutputManager { get; private set; }
		public TabPageManager TabPageManager { get; private set; }
		public RateCardManager RateCardManager { get; private set; }
		public GalleryManager Gallery1Manager { get; private set; }
		public GalleryManager Gallery2Manager { get; private set; }
	}
}