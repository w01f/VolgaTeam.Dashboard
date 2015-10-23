using Asa.CommonGUI.RateCard;
using Asa.Core.AdSchedule;
using Asa.Core.Common;

namespace Asa.AdSchedule.Controls.BusinessClasses
{
	public class BusinessObjects
	{
		private static readonly BusinessObjects _instance = new BusinessObjects();

		public ScheduleManager ScheduleManager { get; private set; }
		public HelpManager HelpManager { get; private set; }
		public OutputManager OutputManager { get; private set; }
		public RateCardManager RateCardManager { get; private set; }
		public TabPageManager TabPageManager { get; private set; }
		public ThemeManager ThemeManager { get; private set; }
		public ActivityManager ActivityManager { get; private set; }
		public GalleryManager Gallery1Manager { get; private set; }
		public GalleryManager Gallery2Manager { get; private set; }


		private BusinessObjects()
		{
			ScheduleManager = new ScheduleManager();
			HelpManager = new HelpManager();
			OutputManager = new OutputManager();
			ThemeManager = new ThemeManager();
		}

		public static BusinessObjects Instance
		{
			get { return _instance; }
		}

		public void Init()
		{
			OutputManager.Init();
			PowerPointManager.Instance.SettingsChanged += (o, e) => OutputManager.UpdateColors();

			HelpManager.LoadHelpLinks();

			ThemeManager.Load();
			PowerPointManager.Instance.SettingsChanged += (o, e) => ThemeManager.Load();

			TabPageManager = new TabPageManager(Core.AdSchedule.ResourceManager.Instance.TabsConfigFile);
			ActivityManager = ActivityManager.OpenStorage();
			Gallery1Manager = new GalleryManager(Core.AdSchedule.ResourceManager.Instance.Gallery1ConfigFile);
			Gallery2Manager = new GalleryManager(Core.AdSchedule.ResourceManager.Instance.Gallery2ConfigFile);
			RateCardManager = new RateCardManager(Core.Common.ResourceManager.Instance.RateCardFolder);
			RateCardManager.LoadRateCards();
		}

	}
}