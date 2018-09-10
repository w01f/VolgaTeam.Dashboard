using System.Drawing;
using Asa.Business.Solutions.Common.Interfaces;

namespace Asa.Media.Controls.BusinessClasses.Managers
{
	public class ImageResourcesManager : ISolutionsResourceManager
	{
		public Icon MainAppIcon => Resources.Common.Resource.FormIcon;
		public Image MainAppRibbonLogo => Resources.Common.Resource.AppLogo;
		public Image FloaterLogo => Resources.Common.Resource.BrandingImage;
		public Image ToggleSwitchSkinElement => Resources.Common.Resource.ToggleSwitch;

		#region Start Form
		public Image StartFormBackgroundLogo => Resources.Controls.StartForm.Resource.BackgroundLogo;
		public Image StartFormLogo => Resources.Controls.StartForm.Resource.FormLogo;
		public Image StartFormNewImage => Resources.Controls.StartForm.Resource.New;
		public Image StartFormOpenImage => Resources.Controls.StartForm.Resource.Open;
		public Image StartFormQuickEditScheduleImage => Resources.Controls.StartForm.Resource.QuickEditSchedule;
		public Image StartFormCancelImage => Resources.Controls.StartForm.Resource.Cancel;
		#endregion

		#region Common Ribbon Resources
		public Image RibbonOutputImage => Resources.Ribbon.Common.Resource.Output;
		public Image RibbonSettingsImage => Resources.Ribbon.Common.Resource.Settings;
		#endregion

		#region Retractable Bar Resources
		public Image RetractableBarExpandImage => Resources.Controls.Common.RetractableBar.Resource.Expand;
		public Image RetractableBarCollpaseImage => Resources.Controls.Common.RetractableBar.Resource.Collapse;
		#endregion

		#region Main Menu
		public Image MainMenuNewImage => Resources.Ribbon.Menu.Resource.New;
		public Image MainMenuOpenImage => Resources.Ribbon.Menu.Resource.Open;
		public Image MainMenuSaveImage => Resources.Ribbon.Menu.Resource.Save;
		public Image MainMenuSaveAsImage => Resources.Ribbon.Menu.Resource.SaveAs;
		public Image MainMenuOutputPdfImage => Resources.Ribbon.Menu.Resource.SavePdf;
		public Image MainMenuEmailImage => Resources.Ribbon.Menu.Resource.SendEmail;
		public Image MainMenuSlideSettingsImage => Resources.Ribbon.Menu.Resource.Preferences;
		public Image MainMenuHelpImage => Resources.Ribbon.Menu.Resource.Help;
		public Image MainMenuExitImage => Resources.Ribbon.Menu.Resource.Exit;
		#endregion

		#region Qat Menu
		public Image QatSaveImage => Resources.Ribbon.QuickAccessBar.Resource.Save;
		public Image QatSaveAsImage => Resources.Ribbon.QuickAccessBar.Resource.SaveAs;
		public Image QatFloaterImage => Resources.Ribbon.QuickAccessBar.Resource.Floater;
		public Image QatHelpImage => Resources.Ribbon.QuickAccessBar.Resource.Help;
		#endregion

		#region Home Resources
		public Image HomeDefaultTopLogo => Resources.Controls.Home.Resource.Default;
		public Image HomeDefaultBottomLogo => Resources.Controls.Home.Resource.DefaultBottom;
		public Image HomeSplashBottomLogo => Resources.Controls.Home.Resource.BottomRight;
		public Image HomeDateStartImage => Resources.Ribbon.Home.Resource.DateStart;
		public Image HomeDateEndImage => Resources.Ribbon.Home.Resource.DateEnd;
		public Image HomeTopTitleImage => Resources.Controls.Home.Resource.Subtab1Top;
		public Image HomeBottomTitleImage => Resources.Controls.Home.Resource.Subtab1Bottom;
		public Image HomeWeeklyScheduleImage => Resources.Controls.Home.Resource.Top1;
		public Image HomeMonthlyScheduleImage => Resources.Controls.Home.Resource.Top2;
		public Image HomeSnaphotShortcutImage => Resources.Controls.Home.Resource.Bottom1;
		public Image HomeOptionsShortcutImage => Resources.Controls.Home.Resource.Bottom2;
		public Image HomeCalendarShortcutImage => Resources.Controls.Home.Resource.Bottom3;
		public Image HomeNewSchedulePopupLogo => Resources.Controls.OpenForm.Resource.FileName;
		public Image HomeOpenSchedulePopupImage => Resources.Controls.OpenForm.Resource.Open;
		public Image HomeDeleteSchedulePopupImage => Resources.Controls.OpenForm.Resource.Delete;
		public Image HomeSettingsDemoImage => Resources.Controls.Home.Resource.PreferencesDemo;
		public Image HomeSettingsDaypartsImage => Resources.Controls.Home.Resource.PreferencesDayparts;
		public Image HomeSettingsCalendarTypeImage => Resources.Controls.Home.Resource.PreferencesCalendar;
		public Image HomeSettingsFlightDatesLogo => Resources.Controls.Home.Resource.PopupDates;
		#endregion

		#region Program Schedule Resources
		public Image ProgramScheduleRibbonLogo => Resources.Ribbon.Schedule.Resource.New;
		public Image ProgramScheduleNoRecordsLogo => Resources.Controls.Schedule.Resource.DefaultSchedule;
		public Image ProgramScheduleNoProgramsLogo => Resources.Controls.Schedule.Resource.DefaultTVRadio;
		public Image ProgramScheduleNoDigitalItemsLogo => Resources.Controls.Schedule.Resource.DefaultDigital;
		public Image ProgramScheduleNewPopupLogo => Resources.Controls.Schedule.Resource.PopupNewSchedule;
		public Image ProgramScheduleRetractableBarColumnsImage => Resources.Controls.Schedule.Resource.RetractableBarTVRadioColumns;
		public Image ProgramScheduleRetractableBarTotalsImage => Resources.Controls.Schedule.Resource.RetractableBarTVRadioTotals;
		public Image ProgramScheduleRetractableBarDigitalImage => Resources.Controls.Schedule.Resource.RetractableBarDigitalInfo;
		public Image ProgramScheduleRetractableBarSummaryImage => Resources.Controls.Schedule.Resource.RetractableBarSummaryInfo;
		public Image ProgramScheduleRetractableBarColorsImage => Resources.Controls.Schedule.Resource.RetractableBarColors;
		#endregion

		#region Snapshots Resources
		public Image SnapshotsRibbonLogo => Resources.Ribbon.Snapshots.Resource.New;
		public Image SnapshotsNoRecordsLogo => Resources.Controls.Snapshots.Resource.DefaultSnapshot;
		public Image SnapshotsNoProgramsLogo => Resources.Controls.Snapshots.Resource.DefaultProgram;
		public Image SnapshotsNoDigitalItemsLogo => Resources.Controls.Snapshots.Resource.DefaultDigital;
		public Image SnapshotsNewPopupLogo => Resources.Controls.Snapshots.Resource.PopupSnapshotNew;
		public Image SnapshotsRetractableBarColumnsImage => Resources.Controls.Snapshots.Resource.RetractableBarColumns;
		public Image SnapshotsRetractableBarDigitalImage => Resources.Controls.Snapshots.Resource.RetractableBarDigitalInfo;
		public Image SnapshotsRetractableBarSummaryImage => Resources.Controls.Snapshots.Resource.RetractableBarSummaryInfo;
		public Image SnapshotsRetractableBarActiveWeeksImage => Resources.Controls.Snapshots.Resource.RetractableBarActiveWeeks;
		public Image SnapshotsRetractableBarColorsImage => Resources.Controls.Snapshots.Resource.RetractableBarColors;
		#endregion

		#region Options Resources
		public Image OptionsRibbonLogo => Resources.Ribbon.Options.Resource.New;
		public Image OptionsNoRecordsLogo => Resources.Controls.Options.Resource.DefaultOptions;
		public Image OptionsNoProgramsLogo => Resources.Controls.Options.Resource.DefaultProgram;
		public Image OptionsNoDigitalItemsLogo => Resources.Controls.Options.Resource.DefaultDigital;
		public Image OptionsNewPopupLogo => Resources.Controls.Options.Resource.PopupOptionsNew;
		public Image OptionsRetractableBarColumnsImage => Resources.Controls.Options.Resource.RetractableBarColumns;
		public Image OptionsRetractableBarDigitalImage => Resources.Controls.Options.Resource.RetractableBarDigitalInfo;
		public Image OptionsRetractableBarSummaryImage => Resources.Controls.Options.Resource.RetractableBarSummaryInfo;
		public Image OptionsRetractableBarColorsImage => Resources.Controls.Options.Resource.RetractableBarColors;
		#endregion

		#region Digital Resources
		public Image DigitalProductsRibbonLogo => Resources.Ribbon.Digital.Resource.New;
		public Image DigitalProductsHomeMainLogo => Resources.Controls.Digital.Resource.DefaultHome;
		public Image DigitalProductsHomeRightLogo => Resources.Controls.Digital.Resource.DefaultHomeRight;
		public Image DigitalProductsHomeBottomLogo => Resources.Controls.Digital.Resource.DefaultHomeBottom;
		public Image DigitalStandalonePackageNoRecordsLogo => Resources.Controls.Digital.Resource.DefaultProduct;
		public Image DigitalRetractableBarListImage => Resources.Controls.Digital.Resource.RetractableBarList;
		public Image DigitalRetractableBarProductPackageImage => Resources.Controls.Digital.Resource.RetractableBarPackage;
		public Image DigitalRetractableBarStandalonePackageImage => Resources.Controls.Digital.Resource.RetractableBarAlacarte;
		public Image DigitalRetractableBarProductPackageFormulaImage => Resources.Controls.Digital.Resource.RetractableBarPackageFormula;
		public Image DigitalRetractableBarStandalonePackageFormulaImage => Resources.Controls.Digital.Resource.RetractableBarAlacarteFormula;
		#endregion

		#region Calendar Resources
		public Image CalendarNoDataLogo => Resources.Controls.Calendar.Resource.DefaultCalendar;
		public Image CalendarResetImage => Resources.Ribbon.Calendar.Resource.Reset;
		public Image CalendarDataSourceScheduleImage => Resources.Ribbon.Calendar.Resource.ImportFromSchedule;
		public Image CalendarDataSourceSnapshotsImage => Resources.Ribbon.Calendar.Resource.ImportFromSnapshots;
		public Image CalendarDataSourceEmptyImage => Resources.Ribbon.Calendar.Resource.Custom;
		public Image CalendarDataCopyImage => Resources.Ribbon.Calendar.Resource.Copy;
		public Image CalendarDataPasteImage => Resources.Ribbon.Calendar.Resource.Paste;
		public Image CalendarDataCloneImage => Resources.Ribbon.Calendar.Resource.Clone;
		public Image CalendarRetractableBarFavoritesImage => Resources.Controls.Calendar.Resource.RetractableBarGallery;
		public Image CalendarRetractableBarStyleImage => Resources.Controls.Calendar.Resource.RetractableBarStyle;
		public Image CalendarRetractableBarCommentsImage => Resources.Controls.Calendar.Resource.RetractableBarComments;
		#endregion

		#region Browser Resources
		public Image BrowserNavigationBack => Resources.Controls.Browser.Resource.Back;
		public Image BrowserNavigationForward => Resources.Controls.Browser.Resource.Forward;
		public Image BrowserNavigationRefresh => Resources.Controls.Browser.Resource.Refresh;
		public Image BrowserExternalChrome => Resources.Controls.Browser.Resource.Chrome;
		public Image BrowserExternalFirefox => Resources.Controls.Browser.Resource.Firefox;
		public Image BrowserExternalIE => Resources.Controls.Browser.Resource.IE;
		public Image BrowserExternalEdge => Resources.Controls.Browser.Resource.Edge;
		public Image BrowserPowerPointAddSlide => Resources.Controls.Browser.Resource.AddSlide;
		public Image BrowserPowerPointAddSlides => Resources.Controls.Browser.Resource.AddAll;
		public Image BrowserPowerPointPrint => Resources.Controls.Browser.Resource.Printer;
		public Image BrowserVideoAdd => Resources.Controls.Browser.Resource.AddVideo;
		public Image BrowserYoutubeAdd => Resources.Controls.Browser.Resource.AddVideo;
		public Image BrowserUrlCopy => Resources.Controls.Browser.Resource.Copy;
		public Image BrowserUrlEmail => Resources.Controls.Browser.Resource.Email;
		#endregion

		#region Solution
		public Image SolutionMemoPopupUp => Resources.Controls.Solutions.Resource.MemoPopupUp;
		public Image SolutionMemoPopupDown => Resources.Controls.Solutions.Resource.MemoPopupDown;
		public Image SolutionMemoPopupList => Resources.Controls.Solutions.Resource.MemoPopupList;
		#endregion
	}
}
