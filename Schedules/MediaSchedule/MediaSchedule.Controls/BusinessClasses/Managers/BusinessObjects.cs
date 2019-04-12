using System;
using System.Collections.Generic;
using System.Linq;
using Asa.Business.Common.Helpers;
using Asa.Business.Media.Configuration;
using Asa.Business.Media.Contexts;
using Asa.Business.Solutions.Common.Helpers;
using Asa.Common.Core.Dictionaries;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.OfficeInterops;
using Asa.Common.GUI.RateCard;
using Asa.Media.Controls.BusinessClasses.Output;

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
		public RibbonTabPageManager RibbonTabPageManager { get; private set; }
		public FormStyleManager FormStyleManager { get; private set; }
		public ThemeManager ThemeManager { get; }
		public ActivityManager ActivityManager { get; private set; }
		public GalleryManager Gallery1Manager { get; private set; }
		public GalleryManager Gallery2Manager { get; private set; }
		public RateCardManager RateCardManager { get; private set; }
		public ConfigManager ConfigManager { get; }
		public TextResourcesManager TextResourcesManager { get; }
		public ImageResourcesManager ImageResourcesManager { get; }
		public BrowserManager BrowserManager { get; }
		public ApplicationIdleManager IdleManager { get; }

		public PowerPointManager<PowerPointSingletonProcessor> PowerPointManager { get; }

		public AdditionalInitializationDispatcher AdditionalInitializator { get; }

		private BusinessObjects()
		{
			OutputManager = new OutputManager();
			ScheduleManager = new MediaScheduleManager();
			ScheduleTemplatesManager = new ScheduleTemplatesManager();
			SolutionsManager = new SolutionsManager();
			SlideManager = new SlideManager();
			HelpManager = new HelpManager();
			ThemeManager = new ThemeManager();
			ConfigManager = new ConfigManager();
			TextResourcesManager = new TextResourcesManager();
			ImageResourcesManager = new ImageResourcesManager();
			PowerPointManager = new PowerPointManager<PowerPointSingletonProcessor>();
			BrowserManager = new BrowserManager();
			IdleManager = new ApplicationIdleManager();

			AdditionalInitializator = new AdditionalInitializationDispatcher();
		}

		public void Init()
		{
			ListManager.Instance.Load();
			ThemeManager.Load();
			SlideSettingsManager.Instance.SettingsChanged += (o, e) => ThemeManager.Load();
			HelpManager.LoadHelpLinks();
			PowerPointManager.Init();
			RibbonTabPageManager = new RibbonTabPageManager(ResourceManager.Instance.TabsConfigFile);
			BrowserManager.Init(ResourceManager.Instance.BrowserConfigFile);

			FormStyleManager = new FormStyleManager(ResourceManager.Instance.FormStyleConfigFile);
			ActivityManager = ActivityManager.OpenStorage();
			ConfigManager.LoadConfig(ResourceManager.Instance.ConfigFile);
			TextResourcesManager.LoadTextResources(ResourceManager.Instance.TextResourcesFile);
			TextResourcesManager.LoadTabPageSettings(ResourceManager.Instance.AdditionalTextResourcesFile);
			IdleManager.LoadSettings(ResourceManager.Instance.IdleSettingsFile);

			AdditionalInitializator.Actions.Add(new InitAction(
				new[]
				{
					ContentIdentifiers.ScheduleSettings,
					ContentIdentifiers.ProgramSchedule,
					ContentIdentifiers.Snapshots,
					ContentIdentifiers.Options,
					ContentIdentifiers.DigitalProducts,
					ContentIdentifiers.BroadcastCalendar,
					ContentIdentifiers.CustomCalendar,
				},
				() =>
				{
					MediaMetaData.Instance.ListManager.Load();
					Business.Online.Dictionaries.ListManager.Instance.Load(Common.Core.Configuration.ResourceManager.Instance.OnlineListsFile);
					OutputManager.Init();
					SlideSettingsManager.Instance.SettingsChanged += (o, e) => OutputManager.UpdateColors();
				})
			);

			if (FileStorageManager.Instance.DataState == DataActualityState.Updated ||
				FileStorageManager.Instance.UseLocalMode)
			{
				AdditionalInitializator.Actions.Add(new InitAction(
					new[]
					{
						ContentIdentifiers.Solutions
					},
					() =>
					{
						SolutionsManager.LoadSolutions(ResourceManager.Instance.SolutionsConfigFile);
						SolutionsManager.LoadSolutionData(ResourceManager.Instance.SolutionsDataFolder);
					})
				);
			}
			else
			{
				SolutionsManager.LoadSolutions(ResourceManager.Instance.SolutionsConfigFile);
				SolutionsManager.LoadSolutionData(ResourceManager.Instance.SolutionsDataFolder);
			}

			AdditionalInitializator.Actions.Add(new InitAction(
				new[]
				{
					ContentIdentifiers.Slides
				},
				() =>
				{
					SlideManager.LoadSlides(Common.Core.Configuration.ResourceManager.Instance.SlideMastersFolder);
				})
			);

			AdditionalInitializator.Actions.Add(new InitAction(
				new[]
				{
					ContentIdentifiers.Gallery1,
				},
				() =>
				{
					Gallery1Manager = new GalleryManager(ResourceManager.Instance.Gallery1ConfigFile);
				})
			);

			AdditionalInitializator.Actions.Add(new InitAction(
				new[]
				{
					ContentIdentifiers.Gallery2,
				},
				() =>
				{
					Gallery2Manager = new GalleryManager(ResourceManager.Instance.Gallery2ConfigFile);
				})
			);

			AdditionalInitializator.Actions.Add(new InitAction(
				new[]
				{
					ContentIdentifiers.RateCard
				},
				() =>
				{
					RateCardManager = new RateCardManager(Common.Core.Configuration.ResourceManager.Instance.RateCardFolder);
					RateCardManager.LoadRateCards();
				})
			);
		}

		public class AdditionalInitializationDispatcher
		{
			public List<InitAction> Actions { get; }

			public AdditionalInitializationDispatcher()
			{
				Actions = new List<InitAction>();
			}

			public void RequestContentInitailization(string contentIdentifier)
			{
				var targetAction = Actions.FirstOrDefault(action =>
					action.AssignedContentIdentifiers.Any(contentIdentifier.StartsWith));
				if (targetAction == null)
					return;
				targetAction.DoInitialization();
				Actions.Remove(targetAction);
			}
		}

		public class InitAction
		{
			public List<string> AssignedContentIdentifiers { get; }
			public Action DoInitialization { get; }

			public InitAction(IList<string> assignedContentIdentifiers, Action initAction)
			{
				AssignedContentIdentifiers = new List<string>();
				AssignedContentIdentifiers.AddRange(assignedContentIdentifiers);

				DoInitialization = initAction;
			}
		}
	}
}