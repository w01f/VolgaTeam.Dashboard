using System;
using System.Globalization;
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
using Asa.Media.Controls.BusinessClasses;
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
		public RibbonTabItem TabDigitalPackage { get; set; }
		public RibbonTabItem TabCalendar1 { get; set; }
		public RibbonTabItem TabCalendar2 { get; set; }
		public RibbonTabItem TabGallery1 { get; set; }
		public RibbonTabItem TabGallery2 { get; set; }
		public RibbonTabItem TabRateCard { get; set; }
		public RibbonTabItem TabSnapshot { get; set; }
		public RibbonTabItem TabOptions { get; set; }

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

			MediaMetaData.Instance.SettingsManager.InitThemeHelper(BusinessObjects.Instance.ThemeManager);
			MediaMetaData.Instance.SettingsManager.LoadSettings();

			Business.Common.Dictionaries.ListManager.Instance.Load();
			MediaMetaData.Instance.ListManager.Load();
			Business.Online.Dictionaries.ListManager.Instance.Load(Common.Core.Configuration.ResourceManager.Instance.OnlineListsFile);

			BusinessObjects.Instance.Init();
			BusinessObjects.Instance.ThemeManager.ThemesChanged += (o, e) => ConfigureThemeButtons();
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
				TabDigitalPackage,
				TabSnapshot,
				TabOptions,
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

			foreach (var ribbonButton in new[]
			{
				HomeSave,
				ProgramScheduleSave,
				DigitalProductSave,
				DigitalPackageSave,
				SnapshotSave,
				OptionsSave,
				Calendar1Save,
				Calendar2Save,
			})
			{
				ribbonButton.Click += ContentController.OnSaveSchedule;
			}

			foreach (var ribbonButton in new[]
			{
				HomeSaveAs,
				ProgramScheduleSaveAs,
				DigitalProductSaveAs,
				DigitalPackageSaveAs,
				SnapshotSaveAs,
				OptionsSaveAs,
				Calendar1SaveAs,
				Calendar2SaveAs,
			})
			{
				ribbonButton.Click += ContentController.OnSaveAsSchedule;
			}

			foreach (var ribbonButton in new[]
			{
				HomeHelp,
				ProgramScheduleHelp,
				DigitalProductHelp,
				DigitalPackageHelp,
				SnapshotHelp,
				OptionsHelp,
				Calendar1Help,
				Calendar2Help,
				Gallery1Help,
				Gallery2Help,
			})
			{
				ribbonButton.Click += ContentController.OnGetHelp;
			}

			foreach (var ribbonButton in new[]
			{
				ProgramSchedulePowerPoint,
				DigitalProductPowerPoint,
				DigitalPackagePowerPoint,
				SnapshotPowerPoint,
				OptionsPowerPoint,
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
				DigitalPackagePdf,
				SnapshotPdf,
				OptionsPdf,
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
				DigitalPackagePreview,
				SnapshotPreview,
				OptionsPreview,
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
				DigitalPackageEmail,
				SnapshotEmail,
				OptionsEmail,
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
				((RibbonBar) ProgramSchedulePowerPoint.ContainerControl).Text = "Important Info";
				((RibbonBar) ProgramScheduleEmail.ContainerControl).Visible = false;
				((RibbonBar) ProgramSchedulePreview.ContainerControl).Visible = false;
				Supertip.SetSuperTooltip(ProgramScheduleTheme, selectorToolTip);
				ProgramScheduleTheme.Click -= OnThemeClick;
				ProgramScheduleTheme.Click += OnThemeClick;

				DigitalProductPowerPoint.Visible = false;
				((RibbonBar) DigitalProductPowerPoint.ContainerControl).Text = "Important Info";
				((RibbonBar) DigitalProductEmail.ContainerControl).Visible = false;
				((RibbonBar) DigitalProductPreview.ContainerControl).Visible = false;
				Supertip.SetSuperTooltip(DigitalProductTheme, selectorToolTip);
				DigitalProductTheme.Click -= OnThemeClick;
				DigitalProductTheme.Click += OnThemeClick;

				DigitalPackagePowerPoint.Visible = false;
				((RibbonBar) DigitalPackagePowerPoint.ContainerControl).Text = "Important Info";
				((RibbonBar) DigitalPackageEmail.ContainerControl).Visible = false;
				((RibbonBar) DigitalPackagePreview.ContainerControl).Visible = false;
				Supertip.SetSuperTooltip(DigitalPackageTheme, selectorToolTip);
				DigitalPackageTheme.Click -= OnThemeClick;
				DigitalPackageTheme.Click += OnThemeClick;

				SnapshotPowerPoint.Visible = false;
				((RibbonBar) SnapshotPowerPoint.ContainerControl).Text = "Important Info";
				((RibbonBar) SnapshotEmail.ContainerControl).Visible = false;
				((RibbonBar) SnapshotPreview.ContainerControl).Visible = false;
				Supertip.SetSuperTooltip(SnapshotTheme, selectorToolTip);
				SnapshotTheme.Click -= OnThemeClick;
				SnapshotTheme.Click += OnThemeClick;

				OptionsPowerPoint.Visible = false;
				((RibbonBar) OptionsPowerPoint.ContainerControl).Text = "Important Info";
				((RibbonBar) OptionsEmail.ContainerControl).Visible = false;
				((RibbonBar) OptionsPreview.ContainerControl).Visible = false;
				Supertip.SetSuperTooltip(OptionsTheme, selectorToolTip);
				OptionsTheme.Click -= OnThemeClick;
				OptionsTheme.Click += OnThemeClick;
			}
			else
			{
				ProgramSchedulePowerPoint.Visible = true;
				((RibbonBar) ProgramScheduleEmail.ContainerControl).Visible = true;
				((RibbonBar) ProgramSchedulePreview.ContainerControl).Visible = true;
				ProgramScheduleTheme.Click -= OnThemeClick;

				DigitalProductPowerPoint.Visible = true;
				((RibbonBar) DigitalProductEmail.ContainerControl).Visible = true;
				((RibbonBar) DigitalProductPreview.ContainerControl).Visible = true;
				DigitalProductTheme.Click -= OnThemeClick;

				DigitalPackagePowerPoint.Visible = true;
				((RibbonBar) DigitalPackageEmail.ContainerControl).Visible = true;
				((RibbonBar) DigitalPackagePreview.ContainerControl).Visible = true;
				DigitalPackageTheme.Click -= OnThemeClick;

				SnapshotPowerPoint.Visible = true;
				((RibbonBar) SnapshotEmail.ContainerControl).Visible = true;
				((RibbonBar) SnapshotPreview.ContainerControl).Visible = true;
				SnapshotTheme.Click -= OnThemeClick;

				OptionsPowerPoint.Visible = true;
				((RibbonBar) OptionsEmail.ContainerControl).Visible = true;
				((RibbonBar) OptionsPreview.ContainerControl).Visible = true;
				OptionsTheme.Click -= OnThemeClick;

				var selectorToolTip = new SuperTooltipInfo("Slide Theme", "", "Select the PowerPoint Slide theme you want to use for this schedule", null, null, eTooltipColor.Gray);
				Supertip.SetSuperTooltip(ProgramScheduleTheme, selectorToolTip);
				Supertip.SetSuperTooltip(DigitalProductTheme, selectorToolTip);
				Supertip.SetSuperTooltip(DigitalPackageTheme, selectorToolTip);
				Supertip.SetSuperTooltip(SnapshotTheme, selectorToolTip);
				Supertip.SetSuperTooltip(OptionsTheme, selectorToolTip);
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
				DigitalPackageSpecialButtons,
				Calendar1SpecialButtons,
				Calendar2SpecialButtons,
				SnapshotSpecialButtons,
				OptionsSpecialButtons,
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
				Logo = Properties.Resources.RibbonLogo
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
		public ButtonItem SlideSettingsButton { get; set; }

		#region Home
		public RibbonPanel HomePanel { get; set; }
		public RibbonBar HomeSpecialButtons { get; set; }
		public ButtonItem HomeHelp { get; set; }
		public ButtonItem HomeSave { get; set; }
		public ButtonItem HomeSaveAs { get; set; }
		public ComboBoxEdit HomeBusinessName { get; set; }
		public ComboBoxEdit HomeDecisionMaker { get; set; }
		public DateEdit HomePresentationDate { get; set; }
		public RibbonBar HomeFlightDates { get; set; }
		public LabelItem HomeFlightDatesStart { get; set; }
		public LabelItem HomeFlightDatesEnd { get; set; }
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
		public ButtonItem ProgramScheduleSave { get; set; }
		public ButtonItem ProgramScheduleSaveAs { get; set; }
		public ButtonItem ProgramScheduleHelp { get; set; }
		#endregion

		#region Digital Product
		public RibbonPanel DigitalProductPanel { get; set; }
		public RibbonBar DigitalProductThemeBar { get; set; }
		public RibbonBar DigitalProductSpecialButtons { get; set; }
		public ButtonItem DigitalProductPreview { get; set; }
		public ButtonItem DigitalProductPowerPoint { get; set; }
		public ButtonItem DigitalProductEmail { get; set; }
		public ButtonItem DigitalProductPdf { get; set; }
		public ButtonItem DigitalProductTheme { get; set; }
		public ButtonItem DigitalProductSave { get; set; }
		public ButtonItem DigitalProductSaveAs { get; set; }
		public ButtonItem DigitalProductHelp { get; set; }
		public ButtonItem DigitalProductAdd { get; set; }
		public ButtonItem DigitalProductClone { get; set; }
		#endregion

		#region Digital Package
		public RibbonPanel DigitalPackagePanel { get; set; }
		public RibbonBar DigitalPackageThemeBar { get; set; }
		public RibbonBar DigitalPackageSpecialButtons { get; set; }
		public ButtonItem DigitalPackageHelp { get; set; }
		public ButtonItem DigitalPackageSave { get; set; }
		public ButtonItem DigitalPackageSaveAs { get; set; }
		public ButtonItem DigitalPackagePreview { get; set; }
		public ButtonItem DigitalPackageEmail { get; set; }
		public ButtonItem DigitalPackagePowerPoint { get; set; }
		public ButtonItem DigitalPackagePdf { get; set; }
		public ButtonItem DigitalPackageTheme { get; set; }
		#endregion

		#region Calendar 1
		public RibbonBar Calendar1SpecialButtons { get; set; }
		public ImageListBoxControl Calendar1MonthsList { get; set; }
		public ButtonItem Calendar1Copy { get; set; }
		public ButtonItem Calendar1Paste { get; set; }
		public ButtonItem Calendar1Clone { get; set; }
		public ButtonItem Calendar1Help { get; set; }
		public ButtonItem Calendar1Save { get; set; }
		public ButtonItem Calendar1SaveAs { get; set; }
		public ButtonItem Calendar1Preview { get; set; }
		public ButtonItem Calendar1Email { get; set; }
		public ButtonItem Calendar1PowerPoint { get; set; }
		public ButtonItem Calendar1Pdf { get; set; }
		#endregion

		#region Calendar 2
		public RibbonBar Calendar2SpecialButtons { get; set; }
		public ImageListBoxControl Calendar2MonthsList { get; set; }
		public ButtonItem Calendar2Copy { get; set; }
		public ButtonItem Calendar2Paste { get; set; }
		public ButtonItem Calendar2Clone { get; set; }
		public ButtonItem Calendar2Help { get; set; }
		public ButtonItem Calendar2Save { get; set; }
		public ButtonItem Calendar2SaveAs { get; set; }
		public ButtonItem Calendar2Preview { get; set; }
		public ButtonItem Calendar2Email { get; set; }
		public ButtonItem Calendar2PowerPoint { get; set; }
		public ButtonItem Calendar2Pdf { get; set; }
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
		public ButtonItem SnapshotSave { get; set; }
		public ButtonItem SnapshotSaveAs { get; set; }
		public ButtonItem SnapshotHelp { get; set; }
		public RibbonBar SnapshotQuarterBar { get; set; }
		public ButtonItem SnapshotQuarterButton { get; set; }
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
		public ButtonItem OptionsSave { get; set; }
		public ButtonItem OptionsSaveAs { get; set; }
		public ButtonItem OptionsHelp { get; set; }
		public RibbonBar OptionsQuarterBar { get; set; }
		public ButtonItem OptionsQuarterButton { get; set; }
		#endregion

		#region Rate Card
		public RibbonBar RateCardSpecialButtons { get; set; }
		public ButtonItem RateCardHelp { get; set; }
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
		public ButtonItem Gallery1Help { get; set; }
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
		public ButtonItem Gallery2Help { get; set; }
		public ComboBoxEdit Gallery2Sections { get; set; }
		public ComboBoxEdit Gallery2Groups { get; set; }
		#endregion

		#endregion
	}
}
