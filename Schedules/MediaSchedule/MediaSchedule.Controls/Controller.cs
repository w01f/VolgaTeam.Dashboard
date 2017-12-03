using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Asa.Business.Media.Configuration;
using Asa.Business.Media.Entities.NonPersistent.Schedule;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.Activities;
using Asa.Common.Core.Objects.Output;
using Asa.Common.GUI.Floater;
using Asa.Common.GUI.SlideSettingsEditors;
using DevComponents.DotNetBar;
using DevExpress.XtraEditors;
using Asa.Media.Controls.BusinessClasses.Managers;
using Asa.Schedules.Common.Controls.ContentEditors.Helpers;
using DevExpress.XtraLayout;

namespace Asa.Media.Controls
{
	public class Controller
	{
		public static Controller Instance { get; } = new Controller();

		public ContentController ContentController { get; }
		public event EventHandler<FloaterRequestedEventArgs> FloaterRequested;

		public Form FormMain { get; set; }
		public LayoutControlItem MainPanel { get; set; }
		public LayoutControlItem EmptyPanel { get; set; }
		public SuperTooltip Supertip { get; set; }
		public RibbonControl Ribbon { get; set; }
		public RibbonTabItem TabHome { get; set; }
		public RibbonTabItem TabProgramSchedule { get; set; }
		public RibbonTabItem TabDigitalProduct { get; set; }
		public RibbonTabItem TabCalendar1 { get; set; }
		public RibbonTabItem TabCalendar2 { get; set; }
		public RibbonTabItem TabGallery1 { get; set; }
		public RibbonTabItem TabGallery2 { get; set; }
		public RibbonTabItem TabRateCard { get; set; }
		public RibbonTabItem TabSnapshot { get; set; }
		public RibbonTabItem TabOptions { get; set; }
		public RibbonTabItem TabSolutions { get; set; }
		public RibbonTabItem TabSlides { get; set; }
		public RibbonTabItem TabBrowser { get; set; }

		private Controller()
		{
			ContentController = new ContentController();
		}

		public async Task InitBusinessObjects()
		{
			await AppProfileManager.Instance.LoadProfile();

			await ResourceManager.Instance.Load();

			MasterWizardManager.Instance.Load();

			Business.Common.Dictionaries.ListManager.Instance.Load();
			MediaMetaData.Instance.ListManager.Load();
			Business.Online.Dictionaries.ListManager.Instance.Load(Common.Core.Configuration.ResourceManager.Instance.OnlineListsFile);

			BusinessObjects.Instance.Init();
			BusinessObjects.Instance.ThemeManager.ThemesChanged += (o, e) =>
			{
				MediaMetaData.Instance.SettingsManager.LoadSettings();
				ConfigureThemeButtons();
			};

			MediaMetaData.Instance.SettingsManager.InitThemeHelper(BusinessObjects.Instance.ThemeManager);
			MediaMetaData.Instance.SettingsManager.LoadSettings();

			if (ResourceManager.Instance.MainAppTitleTextFile.ExistsLocal())
				PopupMessageHelper.Instance.Title = File.ReadAllText(ResourceManager.Instance.MainAppTitleTextFile.LocalPath);
		}

		public void InitForm()
		{
			SetDefaultCulture();

			InitControls();

			ConfigureThemeButtons();

			ConfigureSpecialButtons();

			BusinessObjects.Instance.ActivityManager.AddActivity(new UserActivity("Application Started"));
		}

		private void InitControls()
		{
			foreach (var tabPage in new[]
			{
				TabHome,
				TabProgramSchedule,
				TabDigitalProduct,
				TabSnapshot,
				TabOptions,
				TabCalendar1,
				TabCalendar2,
				TabSolutions,
				TabSlides,
				TabGallery1,
				TabGallery2,
				TabRateCard,
				TabBrowser
			})
				tabPage.Visible = false;

			ContentController.Init();

			ConfigureMainMenu();

			QatSaveButton.Click += ContentController.OnSaveSchedule;
			QatSaveAsButton.Click += ContentController.OnSaveAsSchedule;
			QatHelpButton.Click += ContentController.OnGetHelp;

			Ribbon.Items.Add(RibbonExpandButton);
			Ribbon.Items.Add(RibbonCollapseButton);
			Ribbon.Items.Add(RibbonPinButton);

			foreach (var ribbonButton in new[]
			{
				ProgramSchedulePowerPoint,
				DigitalProductPowerPoint,
				SnapshotPowerPoint,
				OptionsPowerPoint,
				Calendar1PowerPoint,
				Calendar2PowerPoint,
				SolutionsPowerPoint,
				SlidesPowerPoint,
			})
			{
				ribbonButton.Click += ContentController.OnOutputPowerPoint;
			}

			foreach (var ribbonButton in new[]
			{
				ProgramSchedulePreview,
				DigitalProductPreview,
				SnapshotPreview,
				OptionsPreview,
				Calendar1Preview,
				Calendar2Preview,
				SolutionsPreview,
				SlidesPreview,
			})
			{
				ribbonButton.Click += ContentController.OnPreview;
			}

			foreach (var ribbonButton in new[]
			{
				ProgramScheduleSettings,
				SnapshotSettings,
				OptionsSettings,
			})
			{
				ribbonButton.Click += ContentController.OnEditOutputSettings;
			}

			BrowserSitesBar.Text = BusinessObjects.Instance.BrowserManager.RibbonBarTitle;
			BrowserSitesTitle.Text = BusinessObjects.Instance.BrowserManager.SiteListTitle;

			BusinessObjects.Instance.ScheduleManager.ScheduleOpened +=
				OnScheduleInfoChanged;
			ContentEditManager<MediaScheduleChangeInfo>.ScheduleInfoChanged +=
				OnScheduleInfoChanged;
		}

		private void OnScheduleInfoChanged(Object sender, EventArgs e)
		{
			foreach (var labelControl in new[]
			{
				ProgramScheduleInfo,
				DigitalProductInfo,
				Calendar1Info,
				Calendar2Info,
				SnapshotInfo,
				OptionsInfo,
				SolutionsInfo,
				SlidesInfo,
				RateCardInfo,
				Gallery1Info,
				Gallery2Info,
				BrowserInfo
			})
			{
				labelControl.Text = String.Format("<color={2}><size=+1>{0}</size></color><br><br><color=lightgray><size=-1>{1}</size></color>",
					BusinessObjects.Instance.ScheduleManager.ActiveSchedule.Settings.BusinessName,
					BusinessObjects.Instance.ScheduleManager.ActiveSchedule.Settings.FlightDates,
					BusinessObjects.Instance.FormStyleManager.Style.AccentColor.HasValue
						? BusinessObjects.Instance.FormStyleManager.Style.AccentColor.Value.ToHex()
						: "black");
			}

			foreach (var ribbonBar in new[]
			{
				ProgramScheduleInfoBar,
				DigitalProductInfoBar,
				Calendar1InfoBar,
				Calendar2InfoBar,
				SnapshotInfoBar,
				OptionsInfoBar,
				SolutionsInfoBar,
				SlidesInfoBar,
				RateCardInfoBar,
				Gallery1InfoBar,
				Gallery2InfoBar,
				BrowserInfoBar
			})
			{
				ribbonBar.RecalcLayout();
			}

			foreach (var ribbonPanel in new[]
			{
				ProgramSchedulePanel,
				DigitalProductPanel,
				Calendar1Panel,
				Calendar2Panel,
				SnapshotPanel,
				OptionsPanel,
				SolutionsPanel,
				SlidesPanel,
				RateCardPanel,
				Gallery1Panel,
				Gallery2Panel,
				BrowserPanel
			})
			{
				ribbonPanel.PerformLayout();
			}
		}

		private void ConfigureMainMenu()
		{
			var insertIndex = 0;
			var visible = true;

			var configuration = BusinessObjects.Instance.TabPageManager.TabPageSettings.FirstOrDefault(item => item.Id == ContentIdentifiers.MainMenu);
			if (configuration != null)
			{
				MenuButtonMain.Text = configuration.Name;
				insertIndex = configuration.Order;
				visible = configuration.Visible;
			}
			if (visible)
				Ribbon.Items.Insert(insertIndex, MenuButtonMain);

			MenuSaveButton.Click += ContentController.OnSaveSchedule;
			MenuSaveAsButton.Click += ContentController.OnSaveAsSchedule;
			MenuOutputPdfButton.Click += ContentController.OnOutputPdf;
			MenuEmailButton.Click += ContentController.OnEmail;
			MenuSlideSettingsButton.Visible =
				MasterWizardManager.Instance.MasterWizards.Count > 1 ||
				(MasterWizardManager.Instance.MasterWizards.Count == 1 && SlideSettings.GetAvailableConfigurations().Count(MasterWizardManager.Instance.MasterWizards.First().Value.HasSlideConfiguration) > 1);
			MenuSlideSettingsButton.Click += OnSlideSettingsClick;
			MenuHelpButton.Click += ContentController.OnGetHelp;
		}

		public void ConfigureThemeButtons()
		{
			if (!BusinessObjects.Instance.ThemeManager.GetThemes(SlideType.None).Any())
			{
				var selectorToolTip = new SuperTooltipInfo("Important Info", "", "Click to get more info why output is disabled", null, null, eTooltipColor.Gray);

				MenuEmailButton.Visible = false;
				MenuOutputPdfButton.Visible = false;

				ProgramSchedulePowerPoint.Visible = false;
				((RibbonBar)ProgramSchedulePowerPoint.ContainerControl).Text = "Important Info";
				((RibbonBar)ProgramSchedulePreview.ContainerControl).Visible = false;
				Supertip.SetSuperTooltip(ProgramScheduleTheme, selectorToolTip);
				ProgramScheduleTheme.Click -= OnThemeClick;
				ProgramScheduleTheme.Click += OnThemeClick;

				DigitalProductPowerPoint.Visible = false;
				((RibbonBar)DigitalProductPowerPoint.ContainerControl).Text = "Important Info";
				((RibbonBar)DigitalProductPreview.ContainerControl).Visible = false;
				Supertip.SetSuperTooltip(DigitalProductTheme, selectorToolTip);
				DigitalProductTheme.Click -= OnThemeClick;
				DigitalProductTheme.Click += OnThemeClick;

				SnapshotPowerPoint.Visible = false;
				((RibbonBar)SnapshotPowerPoint.ContainerControl).Text = "Important Info";
				((RibbonBar)SnapshotPreview.ContainerControl).Visible = false;
				Supertip.SetSuperTooltip(SnapshotTheme, selectorToolTip);
				SnapshotTheme.Click -= OnThemeClick;
				SnapshotTheme.Click += OnThemeClick;

				OptionsPowerPoint.Visible = false;
				((RibbonBar)OptionsPowerPoint.ContainerControl).Text = "Important Info";
				((RibbonBar)OptionsPreview.ContainerControl).Visible = false;
				Supertip.SetSuperTooltip(OptionsTheme, selectorToolTip);
				OptionsTheme.Click -= OnThemeClick;
				OptionsTheme.Click += OnThemeClick;

				SolutionsPowerPoint.Visible = false;
				((RibbonBar)SolutionsPowerPoint.ContainerControl).Text = "Important Info";
				((RibbonBar)SolutionsPreview.ContainerControl).Visible = false;
				Supertip.SetSuperTooltip(SolutionsTheme, selectorToolTip);
				SolutionsTheme.Click -= OnThemeClick;
				SolutionsTheme.Click += OnThemeClick;
			}
			else
			{
				MenuEmailButton.Visible = true;
				MenuOutputPdfButton.Visible = true;

				ProgramSchedulePowerPoint.Visible = true;
				((RibbonBar)ProgramSchedulePreview.ContainerControl).Visible = true;
				ProgramScheduleTheme.Click -= OnThemeClick;

				DigitalProductPowerPoint.Visible = true;
				((RibbonBar)DigitalProductPreview.ContainerControl).Visible = true;
				DigitalProductTheme.Click -= OnThemeClick;

				SnapshotPowerPoint.Visible = true;
				((RibbonBar)SnapshotPreview.ContainerControl).Visible = true;
				SnapshotTheme.Click -= OnThemeClick;

				OptionsPowerPoint.Visible = true;
				((RibbonBar)OptionsPreview.ContainerControl).Visible = true;
				OptionsTheme.Click -= OnThemeClick;

				SolutionsPowerPoint.Visible = true;
				((RibbonBar)SolutionsPreview.ContainerControl).Visible = true;
				SolutionsTheme.Click -= OnThemeClick;

				var selectorToolTip = new SuperTooltipInfo("Slide Theme", "", "Select the PowerPoint Slide theme you want to use for this schedule", null, null, eTooltipColor.Gray);
				Supertip.SetSuperTooltip(ProgramScheduleTheme, selectorToolTip);
				Supertip.SetSuperTooltip(DigitalProductTheme, selectorToolTip);
				Supertip.SetSuperTooltip(SnapshotTheme, selectorToolTip);
				Supertip.SetSuperTooltip(OptionsTheme, selectorToolTip);
				Supertip.SetSuperTooltip(SolutionsTheme, selectorToolTip);
			}
		}

		private void OnThemeClick(object sender, EventArgs args)
		{
			BusinessObjects.Instance.HelpManager.OpenHelpLink("NoTheme");
		}

		private void ConfigureSpecialButtons()
		{
			var specialLinkContainers = new[]
			{
				HomeSpecialButtons,
				ProgramScheduleSpecialButtons,
				DigitalProductSpecialButtons,
				SnapshotSpecialButtons,
				OptionsSpecialButtons,
				Calendar1SpecialButtons,
				Calendar2SpecialButtons,
				SolutionsSpecialButtons,
				SlidesSpecialButtons,
				RateCardSpecialButtons,
				Gallery1SpecialButtons,
				Gallery2SpecialButtons,
				BrowserSpecialButtons
			};
			foreach (var ribbonBar in specialLinkContainers)
			{
				if (Business.Online.Dictionaries.ListManager.Instance.SpecialLinksEnable)
				{
					ribbonBar.Text = Business.Online.Dictionaries.ListManager.Instance.SpecialLinksGroupName;
					var containerButton = new ButtonItem
					{
						Image = Business.Online.Dictionaries.ListManager.Instance.SpecialLinksGroupLogo,
						AutoExpandOnClick = true
					};
					Supertip.SetSuperTooltip(containerButton, new SuperTooltipInfo("Links", "", "Helpful schedule building Links and resources", null, null, eTooltipColor.Gray));
					ribbonBar.Items.Add(containerButton);
					foreach (var specialLinkButton in Business.Online.Dictionaries.ListManager.Instance.SpecialLinkButtons)
					{
						var clickAction = new Action(() => { specialLinkButton.Open(); });
						var button = new ButtonItem
						{
							Image = specialLinkButton.Logo,
							Text = String.Format("<b>{0}</b><p>{1}</p>", specialLinkButton.Name, specialLinkButton.Tooltip),
							Tag = specialLinkButton
						};
						button.Click += (o, e) => clickAction();
						containerButton.SubItems.Add(button);
					}
				}
				else
				{
					ribbonBar.Visible = false;
				}
			}
		}

		public void ShowFloater(Action afterShow, Action afterBack = null)
		{
			var args = new FloaterRequestedEventArgs
			{
				AfterShow = afterShow,
				AfterBack = afterBack,
				Logo = BusinessObjects.Instance.ImageResourcesManager.MainAppRibbonLogo ?? Properties.Resources.RibbonLogo
			};
			FloaterRequested?.Invoke(null, args);
		}

		private void SetDefaultCulture()
		{
			Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US")
			{
				DateTimeFormat =
				{
					FirstDayOfWeek = DayOfWeek.Monday,
					ShortDatePattern = @"MM/dd/yyyy"
				}
			};
			Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
		}

		public bool CheckPowerPointRunning(Action afterRun = null)
		{
			if (BusinessObjects.Instance.PowerPointManager.Processor.Connect())
				return true;
			if (PopupMessageHelper.Instance.ShowWarningQuestion(String.Format("PowerPoint needs to be running.{0}Launch it now?", Environment.NewLine)) == DialogResult.Yes)
				ShowFloater(() => BusinessObjects.Instance.PowerPointManager.RunPowerPointLoader(), afterRun);
			return false;
		}

		private void OnSlideSettingsClick(object sender, EventArgs e)
		{
			using (var form = new FormEditSlideSettings(BusinessObjects.Instance.PowerPointManager.Processor))
			{
				form.ShowDialog(FormMain);
			}
		}

		#region Command Controls
		public ButtonItem QatSaveButton { get; set; }
		public ButtonItem QatSaveAsButton { get; set; }
		public ButtonItem QatHelpButton { get; set; }

		public ApplicationButton MenuButtonMain { get; set; }
		public ButtonItem MenuSaveButton { get; set; }
		public ButtonItem MenuSaveAsButton { get; set; }
		public ButtonItem MenuEmailButton { get; set; }
		public ButtonItem MenuOutputPdfButton { get; set; }
		public ButtonItem MenuSlideSettingsButton { get; set; }
		public ButtonItem MenuHelpButton { get; set; }

		public ButtonItem RibbonCollapseButton { get; set; }
		public ButtonItem RibbonExpandButton { get; set; }
		public ButtonItem RibbonPinButton { get; set; }

		#region Home
		public RibbonPanel HomePanel { get; set; }
		public RibbonBar HomeSpecialButtons { get; set; }
		public ComboBoxEdit HomeBusinessName { get; set; }
		public ComboBoxEdit HomeDecisionMaker { get; set; }
		public DateEdit HomePresentationDate { get; set; }
		public RibbonBar HomeFlightDates { get; set; }
		public LabelItem HomeFlightDatesStartTitle { get; set; }
		public LabelItem HomeFlightDatesStartValue { get; set; }
		public LabelItem HomeFlightDatesEndTitle { get; set; }
		public LabelItem HomeFlightDatesEndValue { get; set; }
		#endregion

		#region Program Schedule
		public RibbonPanel ProgramSchedulePanel { get; set; }
		public RibbonBar ProgramScheduleThemeBar { get; set; }
		public RibbonBar ProgramScheduleSpecialButtons { get; set; }
		public RibbonBar ProgramScheduleInfoBar { get; set; }
		public LabelControl ProgramScheduleInfo { get; set; }
		public ButtonItem ProgramScheduleNew { get; set; }
		public ButtonItem ProgramScheduleProgramAdd { get; set; }
		public ButtonItem ProgramScheduleProgramDelete { get; set; }
		public ButtonItem ProgramSchedulePreview { get; set; }
		public ButtonItem ProgramSchedulePowerPoint { get; set; }
		public ButtonItem ProgramScheduleTheme { get; set; }
		public ButtonItem ProgramScheduleSettings { get; set; }
		#endregion

		#region Digital Product
		public RibbonPanel DigitalProductPanel { get; set; }
		public RibbonBar DigitalProductLogoBar { get; set; }
		public RibbonBar DigitalProductThemeBar { get; set; }
		public RibbonBar DigitalProductSpecialButtons { get; set; }
		public RibbonBar DigitalProductInfoBar { get; set; }
		public LabelControl DigitalProductInfo { get; set; }
		public ButtonItem DigitalProductPreview { get; set; }
		public ButtonItem DigitalProductPowerPoint { get; set; }
		public ButtonItem DigitalProductTheme { get; set; }
		public ButtonItem DigitalProductAdd { get; set; }
		public ButtonItem DigitalProductClone { get; set; }
		public ButtonItem DigitalProductDelete { get; set; }
		#endregion

		#region Calendar 1
		public RibbonPanel Calendar1Panel { get; set; }
		public RibbonBar Calendar1SpecialButtons { get; set; }
		public RibbonBar Calendar1InfoBar { get; set; }
		public LabelControl Calendar1Info { get; set; }
		public ButtonItem Calendar1Copy { get; set; }
		public ButtonItem Calendar1Paste { get; set; }
		public ButtonItem Calendar1Clone { get; set; }
		public ButtonItem Calendar1Preview { get; set; }
		public ButtonItem Calendar1PowerPoint { get; set; }
		public ButtonItem Calendar1Reset { get; set; }
		#endregion

		#region Calendar 2
		public RibbonPanel Calendar2Panel { get; set; }
		public RibbonBar Calendar2SpecialButtons { get; set; }
		public RibbonBar Calendar2InfoBar { get; set; }
		public LabelControl Calendar2Info { get; set; }
		public ButtonItem Calendar2Copy { get; set; }
		public ButtonItem Calendar2Paste { get; set; }
		public ButtonItem Calendar2Clone { get; set; }
		public ButtonItem Calendar2Preview { get; set; }
		public ButtonItem Calendar2PowerPoint { get; set; }
		public ButtonItem Calendar2Reset { get; set; }
		#endregion

		#region Snapshot
		public RibbonPanel SnapshotPanel { get; set; }
		public RibbonBar SnapshotThemeBar { get; set; }
		public RibbonBar SnapshotSpecialButtons { get; set; }
		public RibbonBar SnapshotInfoBar { get; set; }
		public LabelControl SnapshotInfo { get; set; }
		public ButtonItem SnapshotNew { get; set; }
		public ButtonItem SnapshotProgramAdd { get; set; }
		public ButtonItem SnapshotProgramDelete { get; set; }
		public ButtonItem SnapshotPreview { get; set; }
		public ButtonItem SnapshotPowerPoint { get; set; }
		public ButtonItem SnapshotTheme { get; set; }
		public ButtonItem SnapshotSettings { get; set; }
		#endregion

		#region Options
		public RibbonPanel OptionsPanel { get; set; }
		public RibbonBar OptionsThemeBar { get; set; }
		public RibbonBar OptionsSpecialButtons { get; set; }
		public RibbonBar OptionsInfoBar { get; set; }
		public LabelControl OptionsInfo { get; set; }
		public ButtonItem OptionsNew { get; set; }
		public ButtonItem OptionsProgramAdd { get; set; }
		public ButtonItem OptionsProgramDelete { get; set; }
		public ButtonItem OptionsPreview { get; set; }
		public ButtonItem OptionsPowerPoint { get; set; }
		public ButtonItem OptionsTheme { get; set; }
		public ButtonItem OptionsSettings { get; set; }
		#endregion

		#region Solutions
		public RibbonPanel SolutionsPanel { get; set; }
		public RibbonBar SolutionsThemeBar { get; set; }
		public RibbonBar SolutionsSpecialButtons { get; set; }
		public RibbonBar SolutionsInfoBar { get; set; }
		public LabelControl SolutionsInfo { get; set; }
		public ButtonItem SolutionsPreview { get; set; }
		public ButtonItem SolutionsPowerPoint { get; set; }
		public ButtonItem SolutionsTheme { get; set; }
		#endregion

		#region Slides
		public RibbonPanel SlidesPanel { get; set; }
		public RibbonBar SlidesLogoBar { get; set; }
		public RibbonBar SlidesSpecialButtons { get; set; }
		public RibbonBar SlidesInfoBar { get; set; }
		public LabelControl SlidesInfo { get; set; }
		public LabelItem SlidesLogoLabel { get; set; }
		public ButtonItem SlidesPreview { get; set; }
		public ButtonItem SlidesPowerPoint { get; set; }
		#endregion

		#region Rate Card
		public RibbonPanel RateCardPanel { get; set; }
		public RibbonBar RateCardSpecialButtons { get; set; }
		public RibbonBar RateCardInfoBar { get; set; }
		public LabelControl RateCardInfo { get; set; }
		public ComboBoxEdit RateCardCombo { get; set; }
		#endregion

		#region Gallery1
		public RibbonPanel Gallery1Panel { get; set; }
		public RibbonBar Gallery1SpecialButtons { get; set; }
		public RibbonBar Gallery1BrowseBar { get; set; }
		public RibbonBar Gallery1ImageBar { get; set; }
		public RibbonBar Gallery1ZoomBar { get; set; }
		public RibbonBar Gallery1CopyBar { get; set; }
		public RibbonBar Gallery1InfoBar { get; set; }
		public LabelControl Gallery1Info { get; set; }
		public ItemContainer Gallery1BrowseModeContainer { get; set; }
		public ButtonItem Gallery1View { get; set; }
		public ButtonItem Gallery1Edit { get; set; }
		public ButtonItem Gallery1ImageSelect { get; set; }
		public ButtonItem Gallery1ImageCrop { get; set; }
		public ButtonItem Gallery1ZoomIn { get; set; }
		public ButtonItem Gallery1ZoomOut { get; set; }
		public ButtonItem Gallery1Copy { get; set; }
		public ComboBoxEdit Gallery1Sections { get; set; }
		public ComboBoxEdit Gallery1Groups { get; set; }
		#endregion

		#region Gallery2
		public RibbonPanel Gallery2Panel { get; set; }
		public RibbonBar Gallery2SpecialButtons { get; set; }
		public RibbonBar Gallery2BrowseBar { get; set; }
		public RibbonBar Gallery2ImageBar { get; set; }
		public RibbonBar Gallery2ZoomBar { get; set; }
		public RibbonBar Gallery2CopyBar { get; set; }
		public RibbonBar Gallery2InfoBar { get; set; }
		public LabelControl Gallery2Info { get; set; }
		public ItemContainer Gallery2BrowseModeContainer { get; set; }
		public ButtonItem Gallery2View { get; set; }
		public ButtonItem Gallery2Edit { get; set; }
		public ButtonItem Gallery2ImageSelect { get; set; }
		public ButtonItem Gallery2ImageCrop { get; set; }
		public ButtonItem Gallery2ZoomIn { get; set; }
		public ButtonItem Gallery2ZoomOut { get; set; }
		public ButtonItem Gallery2Copy { get; set; }
		public ComboBoxEdit Gallery2Sections { get; set; }
		public ComboBoxEdit Gallery2Groups { get; set; }
		#endregion

		#region Browser
		public RibbonPanel BrowserPanel { get; set; }
		public RibbonBar BrowserSpecialButtons { get; set; }
		public RibbonBar BrowserSitesBar { get; set; }
		public LabelItem BrowserSitesTitle { get; set; }
		public ComboBoxEdit BrowserSitesCombo { get; set; }
		public RibbonBar BrowserInfoBar { get; set; }
		public LabelControl BrowserInfo { get; set; }
		#endregion

		#endregion
	}
}
