﻿using System;
using System.Collections.Generic;
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
using DevExpress.XtraLayout.Utils;

namespace Asa.Media.Controls
{
	public class Controller
	{
		public static Controller Instance { get; } = new Controller();

		public ContentController ContentController { get; }
		public event EventHandler<FloaterRequestedEventArgs> FloaterRequested;

		public Form FormMain { get; set; }
		public LayoutControlGroup MainPanelContainer { get; set; }
		public LayoutControlItem MainPanel { get; set; }
		public LayoutControlItem EmptyPanel { get; set; }
		public SuperTooltip SuperTip { get; set; }
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

		private Controller()
		{
			ContentController = new ContentController();
		}

		public void InitBusinessObjects()
		{
			MasterWizardManager.Instance.Load();

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

			ConfigureOutputButtons();

			BusinessObjects.Instance.ScheduleManager.ScheduleOpened +=
				OnScheduleInfoChanged;
			ContentEditManager<MediaScheduleChangeInfo>.ScheduleInfoChanged +=
				OnScheduleInfoChanged;
			Ribbon.SelectedRibbonTabChanged +=
				OnScheduleInfoChanged;
		}

		private void OnScheduleInfoChanged(Object sender, EventArgs e)
		{
			if (ContentController.ActiveControl != null &&
				ContentController.ActiveControl.ShowScheduleInfo &&
				(!String.IsNullOrEmpty(BusinessObjects.Instance.ScheduleManager.ActiveSchedule.Settings.BusinessName) ||
				!String.IsNullOrEmpty(BusinessObjects.Instance.ScheduleManager.ActiveSchedule.Settings.FlightDates)))
			{
				ScheduleInfoAdvertiser.Text = !String.IsNullOrEmpty(BusinessObjects.Instance.ScheduleManager.ActiveSchedule.Settings.BusinessName) ?
					String.Format("<color=#CBCBCB>{0}</color>",
						BusinessObjects.Instance.ScheduleManager.ActiveSchedule.Settings.BusinessName) :
					" ";
				ScheduleInfoFlightDates.Text = !String.IsNullOrEmpty(BusinessObjects.Instance.ScheduleManager.ActiveSchedule.Settings.FlightDates) ?
					String.Format("<color=#CBCBCB>{0}</color>",
						BusinessObjects.Instance.ScheduleManager.ActiveSchedule.Settings.FlightDates) :
					" ";
				ScheduleInfoContainer.Visibility = LayoutVisibility.Always;
			}
			else
				ScheduleInfoContainer.Visibility = LayoutVisibility.Never;
		}

		private void ConfigureMainMenu()
		{
			var insertIndex = 0;
			var visible = true;

			var configuration = BusinessObjects.Instance.RibbonTabPageManager.RibbonTabPageSettings.FirstOrDefault(item => item.Id == ContentIdentifiers.MainMenu);
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

		private void ConfigureOutputButtons()
		{
			foreach (var ribbonButton in new[]
			{
				ProgramSchedulePowerPointCurrent,
				DigitalProductPowerPoint,
				SnapshotPowerPointCurrent,
				OptionsPowerPointCurrent,
				Calendar1PowerPoint,
				Calendar2PowerPoint,
				SolutionsPowerPointCurrent,
				SlidesPowerPoint
			})
			{
				ribbonButton.Click += ContentController.OnOutputPowerPointCurrent;
			}

			foreach (var ribbonButton in new[]
			{
				ProgramSchedulePowerPointAll,
				SnapshotPowerPointAll,
				OptionsPowerPointAll,
				SolutionsPowerPointAll,
			})
			{
				ribbonButton.Click += ContentController.OnOutputPowerPointAll;
			}

			foreach (var ribbonButton in new[]
			{
				ProgramSchedulePowerPoint,
				SnapshotPowerPoint,
				OptionsPowerPoint,
				SolutionsPowerPoint,
			})
			{
				ribbonButton.PopupOpen += ContentController.OnOutputPowerPointBeforePopup;
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
		}

		private void ConfigureThemeButtons()
		{
			if (!BusinessObjects.Instance.ThemeManager.GetThemes(SlideType.None).Any())
			{
				var selectorToolTip = new SuperTooltipInfo("Important Info", "", "Click to get more info why output is disabled", null, null, eTooltipColor.Gray);

				MenuEmailButton.Visible = false;
				MenuOutputPdfButton.Visible = false;

				((RibbonBar)ProgramSchedulePowerPoint.ContainerControl).Visible = false;
				SuperTip.SetSuperTooltip(ProgramScheduleTheme, selectorToolTip);
				ProgramScheduleTheme.Click -= OnThemeClick;
				ProgramScheduleTheme.Click += OnThemeClick;

				((RibbonBar)DigitalProductPowerPoint.ContainerControl).Visible = false;
				SuperTip.SetSuperTooltip(DigitalProductTheme, selectorToolTip);
				DigitalProductTheme.Click -= OnThemeClick;
				DigitalProductTheme.Click += OnThemeClick;

				((RibbonBar)SnapshotPowerPoint.ContainerControl).Visible = false;
				SuperTip.SetSuperTooltip(SnapshotTheme, selectorToolTip);
				SnapshotTheme.Click -= OnThemeClick;
				SnapshotTheme.Click += OnThemeClick;

				((RibbonBar)OptionsPowerPoint.ContainerControl).Visible = false;
				SuperTip.SetSuperTooltip(OptionsTheme, selectorToolTip);
				OptionsTheme.Click -= OnThemeClick;
				OptionsTheme.Click += OnThemeClick;

				((RibbonBar)SolutionsPowerPoint.ContainerControl).Visible = false;
				SuperTip.SetSuperTooltip(SolutionsTheme, selectorToolTip);
				SolutionsTheme.Click -= OnThemeClick;
				SolutionsTheme.Click += OnThemeClick;
			}
			else
			{
				MenuEmailButton.Visible = true;
				MenuOutputPdfButton.Visible = true;

				((RibbonBar) ProgramSchedulePowerPoint.ContainerControl).Visible = true;
				ProgramScheduleTheme.Click -= OnThemeClick;
				((RibbonBar)DigitalProductPowerPoint.ContainerControl).Visible = true;
				DigitalProductTheme.Click -= OnThemeClick;
				((RibbonBar)SnapshotPowerPoint.ContainerControl).Visible = true;
				SnapshotTheme.Click -= OnThemeClick;
				((RibbonBar)OptionsPowerPoint.ContainerControl).Visible = true;
				OptionsTheme.Click -= OnThemeClick;
				((RibbonBar)SolutionsPowerPoint.ContainerControl).Visible = true;
				SolutionsTheme.Click -= OnThemeClick;

				var selectorToolTip = new SuperTooltipInfo("Slide Theme", "", "Select the PowerPoint Slide theme you want to use for this schedule", null, null, eTooltipColor.Gray);
				SuperTip.SetSuperTooltip(ProgramScheduleTheme, selectorToolTip);
				SuperTip.SetSuperTooltip(DigitalProductTheme, selectorToolTip);
				SuperTip.SetSuperTooltip(SnapshotTheme, selectorToolTip);
				SuperTip.SetSuperTooltip(OptionsTheme, selectorToolTip);
				SuperTip.SetSuperTooltip(SolutionsTheme, selectorToolTip);
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
					SuperTip.SetSuperTooltip(containerButton, new SuperTooltipInfo("Links", "", "Helpful schedule building Links and resources", null, null, eTooltipColor.Gray));
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
				Logo = BusinessObjects.Instance.ImageResourcesManager.FloaterLogo ?? Properties.Resources.RibbonLogo
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

		public LayoutControlGroup ScheduleInfoContainer { get; set; }
		public SimpleLabelItem ScheduleInfoAdvertiser { get; set; }
		public SimpleLabelItem ScheduleInfoFlightDates { get; set; }

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
		public ButtonItem HomeSettings { get; set; }
		#endregion

		#region Program Schedule
		public RibbonPanel ProgramSchedulePanel { get; set; }
		public RibbonBar ProgramScheduleThemeBar { get; set; }
		public RibbonBar ProgramScheduleSpecialButtons { get; set; }
		public ButtonItem ProgramScheduleNew { get; set; }
		public ButtonItem ProgramScheduleProgramAdd { get; set; }
		public ButtonItem ProgramScheduleProgramDelete { get; set; }
		public ButtonItem ProgramSchedulePowerPoint { get; set; }
		public ButtonItem ProgramSchedulePowerPointCurrent { get; set; }
		public ButtonItem ProgramSchedulePowerPointAll { get; set; }
		public ButtonItem ProgramScheduleTheme { get; set; }
		public ButtonItem ProgramScheduleSettings { get; set; }
		#endregion

		#region Digital Product
		public RibbonPanel DigitalProductPanel { get; set; }
		public RibbonBar DigitalProductLogoBar { get; set; }
		public RibbonBar DigitalProductThemeBar { get; set; }
		public RibbonBar DigitalProductSpecialButtons { get; set; }
		public ButtonItem DigitalProductPowerPoint { get; set; }
		public ButtonItem DigitalProductTheme { get; set; }
		public ButtonItem DigitalProductAdd { get; set; }
		public ButtonItem DigitalProductClone { get; set; }
		public ButtonItem DigitalProductDelete { get; set; }
		#endregion

		#region Calendar 1
		public RibbonPanel Calendar1Panel { get; set; }
		public RibbonBar Calendar1SpecialButtons { get; set; }
		public ButtonItem Calendar1Copy { get; set; }
		public ButtonItem Calendar1Paste { get; set; }
		public ButtonItem Calendar1Clone { get; set; }
		public ButtonItem Calendar1PowerPoint { get; set; }
		public ButtonItem Calendar1DataSourceSchedule { get; set; }
		public ButtonItem Calendar1DataSourceSnapshots { get; set; }
		public ButtonItem Calendar1DataSourceEmpty { get; set; }
		public ButtonItem Calendar1Reset { get; set; }
		#endregion

		#region Calendar 2
		public RibbonPanel Calendar2Panel { get; set; }
		public RibbonBar Calendar2SpecialButtons { get; set; }
		public ButtonItem Calendar2Copy { get; set; }
		public ButtonItem Calendar2Paste { get; set; }
		public ButtonItem Calendar2Clone { get; set; }
		public ButtonItem Calendar2PowerPoint { get; set; }
		public ButtonItem Calendar2Reset { get; set; }
		#endregion

		#region Snapshot
		public RibbonPanel SnapshotPanel { get; set; }
		public RibbonBar SnapshotThemeBar { get; set; }
		public RibbonBar SnapshotSpecialButtons { get; set; }
		public ButtonItem SnapshotNew { get; set; }
		public ButtonItem SnapshotProgramAdd { get; set; }
		public ButtonItem SnapshotProgramDelete { get; set; }
		public ButtonItem SnapshotPowerPoint { get; set; }
		public ButtonItem SnapshotPowerPointCurrent { get; set; }
		public ButtonItem SnapshotPowerPointAll { get; set; }
		public ButtonItem SnapshotTheme { get; set; }
		public ButtonItem SnapshotSettings { get; set; }
		#endregion

		#region Options
		public RibbonPanel OptionsPanel { get; set; }
		public RibbonBar OptionsThemeBar { get; set; }
		public RibbonBar OptionsSpecialButtons { get; set; }
		public ButtonItem OptionsNew { get; set; }
		public ButtonItem OptionsProgramAdd { get; set; }
		public ButtonItem OptionsProgramDelete { get; set; }
		public ButtonItem OptionsPowerPoint { get; set; }
		public ButtonItem OptionsPowerPointCurrent { get; set; }
		public ButtonItem OptionsPowerPointAll { get; set; }
		public ButtonItem OptionsTheme { get; set; }
		public ButtonItem OptionsSettings { get; set; }
		#endregion

		#region Solutions
		public RibbonPanel SolutionsPanel { get; set; }
		public RibbonBar SolutionsThemeBar { get; set; }
		public RibbonBar SolutionsSpecialButtons { get; set; }
		public ButtonItem SolutionsPowerPoint { get; set; }
		public ButtonItem SolutionsPowerPointCurrent { get; set; }
		public ButtonItem SolutionsPowerPointAll { get; set; }
		public ButtonItem SolutionsTheme { get; set; }
		#endregion

		#region Slides
		public RibbonPanel SlidesPanel { get; set; }
		public RibbonBar SlidesLogoBar { get; set; }
		public RibbonBar SlidesSpecialButtons { get; set; }
		public LabelItem SlidesLogoLabel { get; set; }
		public ButtonItem SlidesPowerPoint { get; set; }
		#endregion

		#region Rate Card
		public RibbonPanel RateCardPanel { get; set; }
		public RibbonBar RateCardSpecialButtons { get; set; }
		public ComboBoxEdit RateCardCombo { get; set; }
		#endregion

		#region Gallery1
		public RibbonPanel Gallery1Panel { get; set; }
		public RibbonBar Gallery1SpecialButtons { get; set; }
		public RibbonBar Gallery1BrowseBar { get; set; }
		public RibbonBar Gallery1ImageBar { get; set; }
		public RibbonBar Gallery1ZoomBar { get; set; }
		public RibbonBar Gallery1CopyBar { get; set; }
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

		#endregion
	}
}
