using System.Threading.Tasks;
using NewBizWiz.CommonGUI.RateCard;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.MediaSchedule;

namespace NewBizWiz.MediaSchedule.Controls.BusinessClasses
{
	public class BusinessObjects
	{
		private static readonly BusinessObjects _instance = new BusinessObjects();

		public static BusinessObjects Instance
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

		private BusinessObjects()
		{
			OutputManager = new OutputManager();
			ScheduleManager = new ScheduleManager();
			HelpManager = new HelpManager();
			ThemeManager = new ThemeManager();
		}

		public async Task Init()
		{
			OutputManager.Init();
			PowerPointManager.Instance.SettingsChanged += (o, e) => OutputManager.UpdateColors();

			HelpManager.LoadHelpLinks();
			
			ThemeManager.Load();
			PowerPointManager.Instance.SettingsChanged += (o, e) => ThemeManager.Load();
			
			TabPageManager = new TabPageManager(Core.MediaSchedule.ResourceManager.Instance.TabsConfigFile);
			ActivityManager = ActivityManager.OpenStorage();
			Gallery1Manager = new GalleryManager(Core.MediaSchedule.ResourceManager.Instance.Gallery1ConfigFile);
			Gallery2Manager = new GalleryManager(Core.MediaSchedule.ResourceManager.Instance.Gallery2ConfigFile);
			RateCardManager = new RateCardManager(Core.Common.ResourceManager.Instance.RateCardFolder);
			RateCardManager.LoadRateCards();
		}
	}
}