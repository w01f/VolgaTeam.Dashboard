using Asa.Business.Common.Entities.Helpers;
using Asa.Business.Media.Configuration;
using Asa.Business.Media.Contexts;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.RateCard;
using Asa.Media.Controls.BusinessClasses.Output;

namespace Asa.Media.Controls.BusinessClasses.Managers
{
	public class BusinessObjects
	{
		public static BusinessObjects Instance { get; } = new BusinessObjects();

		public MediaScheduleManager ScheduleManager { get; }
		public ScheduleTemplatesManager ScheduleTemplatesManager { get; }
		public HelpManager HelpManager { get; }
		public OutputManager OutputManager { get; }
		public TabPageManager TabPageManager { get; private set; }
		public ThemeManager ThemeManager { get; }
		public ActivityManager ActivityManager { get; private set; }
		public GalleryManager Gallery1Manager { get; private set; }
		public GalleryManager Gallery2Manager { get; private set; }
		public RateCardManager RateCardManager { get; private set; }

		private BusinessObjects()
		{
			OutputManager = new OutputManager();
			ScheduleManager = new MediaScheduleManager();
			ScheduleTemplatesManager = new ScheduleTemplatesManager();
			HelpManager = new HelpManager();
			ThemeManager = new ThemeManager();
		}

		public void Init()
		{
			ScheduleManager.Init();
			AsyncHelper.RunSync(ScheduleTemplatesManager.Init);

			OutputManager.Init();
			PowerPointManager.Instance.SettingsChanged += (o, e) => OutputManager.UpdateColors();

			HelpManager.LoadHelpLinks();

			ThemeManager.Load();
			PowerPointManager.Instance.SettingsChanged += (o, e) => ThemeManager.Load();

			TabPageManager = new TabPageManager(ResourceManager.Instance.TabsConfigFile);
			ActivityManager = ActivityManager.OpenStorage();
			Gallery1Manager = new GalleryManager(ResourceManager.Instance.Gallery1ConfigFile);
			Gallery2Manager = new GalleryManager(ResourceManager.Instance.Gallery2ConfigFile);
			RateCardManager = new RateCardManager(Common.Core.Configuration.ResourceManager.Instance.RateCardFolder);
			RateCardManager.LoadRateCards();
		}
	}
}