using Asa.Business.Common.Helpers;
using Asa.Business.Media.Configuration;
using Asa.Business.Media.Contexts;
using Asa.Business.Solutions.Common.Helpers;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.RateCard;
using Asa.Media.Controls.BusinessClasses.Output;
using Asa.Media.Controls.InteropClasses;

namespace Asa.Media.Controls.BusinessClasses.Managers
{
	public class BusinessObjects
	{
		public static BusinessObjects Instance { get; } = new BusinessObjects();

		public MediaScheduleManager ScheduleManager { get; }
		public ScheduleTemplatesManager ScheduleTemplatesManager { get; }
		public SolutionsManager SolutionsManager { get; }
		public SlideManager SlideManager { get; }
		public HelpManager HelpManager { get; }
		public OutputManager OutputManager { get; }
		public TabPageManager TabPageManager { get; private set; }
		public FormStyleManager FormStyleManager { get; private set; }
		public ThemeManager ThemeManager { get; }
		public ActivityManager ActivityManager { get; private set; }
		public GalleryManager Gallery1Manager { get; private set; }
		public GalleryManager Gallery2Manager { get; private set; }
		public RateCardManager RateCardManager { get; private set; }
		public ImageResourcesManager ImageResourcesManager { get; }
		public BrowserManager BrowserManager { get; }

		public PowerPointManager<MediaSchedulePowerPointProcessor> PowerPointManager { get; }

		private BusinessObjects()
		{
			OutputManager = new OutputManager();
			ScheduleManager = new MediaScheduleManager();
			ScheduleTemplatesManager = new ScheduleTemplatesManager();
			SolutionsManager = new SolutionsManager();
			SlideManager = new SlideManager();
			HelpManager = new HelpManager();
			ThemeManager = new ThemeManager();
			ImageResourcesManager = new ImageResourcesManager();
			PowerPointManager = new PowerPointManager<MediaSchedulePowerPointProcessor>();
			BrowserManager = new BrowserManager();
		}

		public void Init()
		{
			ScheduleManager.Init();
			AsyncHelper.RunSync(ScheduleTemplatesManager.Init);

			SolutionsManager.LoadSolutions(ResourceManager.Instance.SolutionsConfigFile);
			SolutionsManager.LoadSolutionData(AppProfileManager.Instance.AppDataFolder);

			OutputManager.Init();
			SlideSettingsManager.Instance.SettingsChanged += (o, e) => OutputManager.UpdateColors();

			HelpManager.LoadHelpLinks();

			PowerPointManager.Init();

			ThemeManager.Load();
			SlideSettingsManager.Instance.SettingsChanged += (o, e) => ThemeManager.Load();

			SlideManager.Load();

			TabPageManager = new TabPageManager(ResourceManager.Instance.TabsConfigFile);
			FormStyleManager = new FormStyleManager(ResourceManager.Instance.FormStyleConfigFile);
			ActivityManager = ActivityManager.OpenStorage();
			Gallery1Manager = new GalleryManager(ResourceManager.Instance.Gallery1ConfigFile);
			Gallery2Manager = new GalleryManager(ResourceManager.Instance.Gallery2ConfigFile);
			RateCardManager = new RateCardManager(Common.Core.Configuration.ResourceManager.Instance.RateCardFolder);
			RateCardManager.LoadRateCards();

			ImageResourcesManager.Load();

			BrowserManager.Init(ResourceManager.Instance.BrowserConfigFile);
		}
	}
}