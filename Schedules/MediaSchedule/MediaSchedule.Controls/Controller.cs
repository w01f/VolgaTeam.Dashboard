﻿using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Asa.Business.Media.Configuration;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.Activities;
using Asa.Common.Core.Objects.Output;
using Asa.Common.GUI.Floater;
using Asa.Common.GUI.SlideSettingsEditors;
using DevComponents.DotNetBar;
using DevExpress.XtraEditors;
using Asa.Media.Controls.BusinessClasses.Managers;
using Asa.Media.Controls.InteropClasses;

namespace Asa.Media.Controls
{
	public class Controller
	{
		public static Controller Instance { get; } = new Controller();

		public ContentController ContentController { get; }
		public event EventHandler<FloaterRequestedEventArgs> FloaterRequested;

		public Form FormMain { get; set; }
		public Panel MainPanel { get; set; }
		public Panel EmptyPanel { get; set; }
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

		private Controller()
		{
			ContentController = new ContentController();
		}

		public async Task InitBusinessObjects()
		{
			await AppProfileManager.Instance.LoadProfile();

			await ResourceManager.Instance.Load();

			PowerPointManager.Instance.Init(RegularMediaSchedulePowerPointHelper.Instance);

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
				TabSolutions,
				TabCalendar1,
				TabCalendar2,
				TabGallery1,
				TabGallery2,
				TabRateCard
			})
				tabPage.Visible = false;

			ContentController.Init();

			SlideSettingsButton.Visible =
				MasterWizardManager.Instance.MasterWizards.Count > 1 ||
				(MasterWizardManager.Instance.MasterWizards.Count == 1 && SlideSettings.GetAvailableConfigurations().Count(MasterWizardManager.Instance.MasterWizards.First().Value.HasSlideConfiguration) > 1);
			Ribbon.Items.Add(SlideSettingsButton);
			SlideSettingsButton.Click += OnSlideSettingsClick;

			QatSaveButton.Click += ContentController.OnSaveSchedule;
			QatSaveAsButton.Click += ContentController.OnSaveAsSchedule;
			QatHelpButton.Click += ContentController.OnGetHelp;

			foreach (var ribbonButton in new[]
			{
				ProgramSchedulePowerPoint,
				DigitalProductPowerPoint,
				SnapshotPowerPoint,
				OptionsPowerPoint,
				SolutionsPowerPoint,
				Calendar1PowerPoint,
				Calendar2PowerPoint,
			})
			{
				ribbonButton.Click += ContentController.OnOutputPowerPoint;
			}

			foreach (var ribbonButton in new[]
			{
				ProgramSchedulePdf,
				DigitalProductPdf,
				SnapshotPdf,
				OptionsPdf,
				SolutionsPdf,
				Calendar1Pdf,
				Calendar2Pdf,
			})
			{
				ribbonButton.Click += ContentController.OnOutputPdf;
			}

			foreach (var ribbonButton in new[]
			{
				ProgramSchedulePreview,
				DigitalProductPreview,
				SnapshotPreview,
				OptionsPreview,
				SolutionsPreview,
				Calendar1Preview,
				Calendar2Preview,
			})
			{
				ribbonButton.Click += ContentController.OnPreview;
			}

			foreach (var ribbonButton in new[]
			{
				ProgramScheduleEmail,
				DigitalProductEmail,
				SnapshotEmail,
				OptionsEmail,
				SolutionsEmail,
				Calendar1Email,
				Calendar2Email,
			})
			{
				ribbonButton.Click += ContentController.OnEmail;
			}
		}

		public void ConfigureThemeButtons()
		{
			if (!BusinessObjects.Instance.ThemeManager.GetThemes(SlideType.None).Any())
			{
				var selectorToolTip = new SuperTooltipInfo("Important Info", "", "Click to get more info why output is disabled", null, null, eTooltipColor.Gray);

				ProgramSchedulePowerPoint.Visible = false;
				((RibbonBar)ProgramSchedulePowerPoint.ContainerControl).Text = "Important Info";
				((RibbonBar)ProgramScheduleEmail.ContainerControl).Visible = false;
				((RibbonBar)ProgramSchedulePreview.ContainerControl).Visible = false;
				Supertip.SetSuperTooltip(ProgramScheduleTheme, selectorToolTip);
				ProgramScheduleTheme.Click -= OnThemeClick;
				ProgramScheduleTheme.Click += OnThemeClick;

				DigitalProductPowerPoint.Visible = false;
				((RibbonBar)DigitalProductPowerPoint.ContainerControl).Text = "Important Info";
				((RibbonBar)DigitalProductEmail.ContainerControl).Visible = false;
				((RibbonBar)DigitalProductPreview.ContainerControl).Visible = false;
				Supertip.SetSuperTooltip(DigitalProductTheme, selectorToolTip);
				DigitalProductTheme.Click -= OnThemeClick;
				DigitalProductTheme.Click += OnThemeClick;

				SnapshotPowerPoint.Visible = false;
				((RibbonBar)SnapshotPowerPoint.ContainerControl).Text = "Important Info";
				((RibbonBar)SnapshotEmail.ContainerControl).Visible = false;
				((RibbonBar)SnapshotPreview.ContainerControl).Visible = false;
				Supertip.SetSuperTooltip(SnapshotTheme, selectorToolTip);
				SnapshotTheme.Click -= OnThemeClick;
				SnapshotTheme.Click += OnThemeClick;

				OptionsPowerPoint.Visible = false;
				((RibbonBar)OptionsPowerPoint.ContainerControl).Text = "Important Info";
				((RibbonBar)OptionsEmail.ContainerControl).Visible = false;
				((RibbonBar)OptionsPreview.ContainerControl).Visible = false;
				Supertip.SetSuperTooltip(OptionsTheme, selectorToolTip);
				OptionsTheme.Click -= OnThemeClick;
				OptionsTheme.Click += OnThemeClick;

				SolutionsPowerPoint.Visible = false;
				((RibbonBar)SolutionsPowerPoint.ContainerControl).Text = "Important Info";
				((RibbonBar)SolutionsEmail.ContainerControl).Visible = false;
				((RibbonBar)SolutionsPreview.ContainerControl).Visible = false;
				Supertip.SetSuperTooltip(SolutionsTheme, selectorToolTip);
				SolutionsTheme.Click -= OnThemeClick;
				SolutionsTheme.Click += OnThemeClick;
			}
			else
			{
				ProgramSchedulePowerPoint.Visible = true;
				((RibbonBar)ProgramScheduleEmail.ContainerControl).Visible = true;
				((RibbonBar)ProgramSchedulePreview.ContainerControl).Visible = true;
				ProgramScheduleTheme.Click -= OnThemeClick;

				DigitalProductPowerPoint.Visible = true;
				((RibbonBar)DigitalProductEmail.ContainerControl).Visible = true;
				((RibbonBar)DigitalProductPreview.ContainerControl).Visible = true;
				DigitalProductTheme.Click -= OnThemeClick;

				SnapshotPowerPoint.Visible = true;
				((RibbonBar)SnapshotEmail.ContainerControl).Visible = true;
				((RibbonBar)SnapshotPreview.ContainerControl).Visible = true;
				SnapshotTheme.Click -= OnThemeClick;

				OptionsPowerPoint.Visible = true;
				((RibbonBar)OptionsEmail.ContainerControl).Visible = true;
				((RibbonBar)OptionsPreview.ContainerControl).Visible = true;
				OptionsTheme.Click -= OnThemeClick;

				SolutionsPowerPoint.Visible = true;
				((RibbonBar)SolutionsEmail.ContainerControl).Visible = true;
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
				Calendar1SpecialButtons,
				Calendar2SpecialButtons,
				SnapshotSpecialButtons,
				OptionsSpecialButtons,
				SolutionsSpecialButtons,
				RateCardSpecialButtons,
				Gallery1SpecialButtons,
				Gallery2SpecialButtons
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

		public void ShowFloater(Action afterShow)
		{
			var args = new FloaterRequestedEventArgs
			{
				AfterShow = afterShow,
				Logo = ResourceManager.Instance.MainAppRibbonLogoFile.ExistsLocal() ?
					Image.FromFile(ResourceManager.Instance.MainAppRibbonLogoFile.LocalPath) :
					Properties.Resources.RibbonLogo
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

		public bool CheckPowerPointRunning()
		{
			if (RegularMediaSchedulePowerPointHelper.Instance.Connect(false))
				return true;
			if (PopupMessageHelper.Instance.ShowWarningQuestion(String.Format("PowerPoint is required to run this application.{0}Do you want to go ahead and open PowerPoint?", Environment.NewLine)) == DialogResult.Yes)
				ShowFloater(() => PowerPointManager.Instance.RunPowerPointLoader());
			return false;
		}

		private void OnSlideSettingsClick(object sender, EventArgs e)
		{
			using (var form = new FormEditSlideSettings())
			{
				form.ShowDialog(FormMain);
			}
		}

		#region Command Controls
		public ButtonItem QatSaveButton { get; set; }
		public ButtonItem QatSaveAsButton { get; set; }
		public ButtonItem QatHelpButton { get; set; }

		public ButtonItem SlideSettingsButton { get; set; }

		#region Home
		public RibbonPanel HomePanel { get; set; }
		public RibbonBar HomeSpecialButtons { get; set; }
		public ComboBoxEdit HomeBusinessName { get; set; }
		public ComboBoxEdit HomeDecisionMaker { get; set; }
		public DateEdit HomePresentationDate { get; set; }
		public RibbonBar HomeFlightDates { get; set; }
		public LabelItem HomeFlightDatesStartLogo { get; set; }
		public LabelItem HomeFlightDatesStartTitle { get; set; }
		public LabelItem HomeFlightDatesStartValue { get; set; }
		public LabelItem HomeFlightDatesEndLogo { get; set; }
		public LabelItem HomeFlightDatesEndTitle { get; set; }
		public LabelItem HomeFlightDatesEndValue { get; set; }
		#endregion

		#region Program Schedule
		public RibbonPanel ProgramSchedulePanel { get; set; }
		public RibbonBar ProgramScheduleThemeBar { get; set; }
		public RibbonBar ProgramScheduleSpecialButtons { get; set; }
		public ButtonItem ProgramScheduleNew { get; set; }
		public ButtonItem ProgramScheduleProgramAdd { get; set; }
		public ButtonItem ProgramScheduleProgramDelete { get; set; }
		public ButtonItem ProgramSchedulePreview { get; set; }
		public ButtonItem ProgramSchedulePowerPoint { get; set; }
		public ButtonItem ProgramScheduleEmail { get; set; }
		public ButtonItem ProgramSchedulePdf { get; set; }
		public ButtonItem ProgramScheduleTheme { get; set; }
		#endregion

		#region Digital Product
		public RibbonPanel DigitalProductPanel { get; set; }
		public RibbonBar DigitalProductLogoBar { get; set; }
		public RibbonBar DigitalProductThemeBar { get; set; }
		public RibbonBar DigitalProductSpecialButtons { get; set; }
		public ButtonItem DigitalProductPreview { get; set; }
		public ButtonItem DigitalProductPowerPoint { get; set; }
		public ButtonItem DigitalProductEmail { get; set; }
		public ButtonItem DigitalProductPdf { get; set; }
		public ButtonItem DigitalProductTheme { get; set; }
		public ButtonItem DigitalProductAdd { get; set; }
		public ButtonItem DigitalProductClone { get; set; }
		public ButtonItem DigitalProductDelete { get; set; }
		#endregion

		#region Calendar 1
		public RibbonBar Calendar1SpecialButtons { get; set; }
		public ImageListBoxControl Calendar1MonthsList { get; set; }
		public ButtonItem Calendar1Copy { get; set; }
		public ButtonItem Calendar1Paste { get; set; }
		public ButtonItem Calendar1Clone { get; set; }
		public ButtonItem Calendar1Preview { get; set; }
		public ButtonItem Calendar1Email { get; set; }
		public ButtonItem Calendar1PowerPoint { get; set; }
		public ButtonItem Calendar1Pdf { get; set; }
		public ButtonItem Calendar1Reset { get; set; }
		#endregion

		#region Calendar 2
		public RibbonBar Calendar2SpecialButtons { get; set; }
		public ImageListBoxControl Calendar2MonthsList { get; set; }
		public ButtonItem Calendar2Copy { get; set; }
		public ButtonItem Calendar2Paste { get; set; }
		public ButtonItem Calendar2Clone { get; set; }
		public ButtonItem Calendar2Preview { get; set; }
		public ButtonItem Calendar2Email { get; set; }
		public ButtonItem Calendar2PowerPoint { get; set; }
		public ButtonItem Calendar2Pdf { get; set; }
		public ButtonItem Calendar2Reset { get; set; }
		#endregion

		#region Snapshot
		public RibbonPanel SnapshotPanel { get; set; }
		public RibbonBar SnapshotThemeBar { get; set; }
		public RibbonBar SnapshotSpecialButtons { get; set; }
		public ButtonItem SnapshotNew { get; set; }
		public ButtonItem SnapshotProgramAdd { get; set; }
		public ButtonItem SnapshotProgramDelete { get; set; }
		public ButtonItem SnapshotPreview { get; set; }
		public ButtonItem SnapshotPowerPoint { get; set; }
		public ButtonItem SnapshotPdf { get; set; }
		public ButtonItem SnapshotEmail { get; set; }
		public ButtonItem SnapshotTheme { get; set; }
		#endregion

		#region Options
		public RibbonPanel OptionsPanel { get; set; }
		public RibbonBar OptionsThemeBar { get; set; }
		public RibbonBar OptionsSpecialButtons { get; set; }
		public ButtonItem OptionsNew { get; set; }
		public ButtonItem OptionsProgramAdd { get; set; }
		public ButtonItem OptionsProgramDelete { get; set; }
		public ButtonItem OptionsPreview { get; set; }
		public ButtonItem OptionsPowerPoint { get; set; }
		public ButtonItem OptionsPdf { get; set; }
		public ButtonItem OptionsEmail { get; set; }
		public ButtonItem OptionsTheme { get; set; }
		#endregion

		#region Solutions
		public RibbonPanel SolutionsPanel { get; set; }
		public RibbonBar SolutionsHomeBar { get; set; }
		public RibbonBar SolutionsThemeBar { get; set; }
		public RibbonBar SolutionsSpecialButtons { get; set; }
		public LabelItem SolutionsHomeLabel { get; set; }
		public ButtonItem SolutionsPreview { get; set; }
		public ButtonItem SolutionsPowerPoint { get; set; }
		public ButtonItem SolutionsPdf { get; set; }
		public ButtonItem SolutionsEmail { get; set; }
		public ButtonItem SolutionsTheme { get; set; }
		#endregion

		#region Rate Card
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
