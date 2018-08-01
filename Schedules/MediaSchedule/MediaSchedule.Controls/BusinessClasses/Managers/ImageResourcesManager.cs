using System.Drawing;
using System.IO;
using Asa.Business.Media.Configuration;

namespace Asa.Media.Controls.BusinessClasses.Managers
{
	public class ImageResourcesManager
	{
		public Icon MainAppIcon { get; private set; }
		public Image MainAppRibbonLogo { get; private set; }
		public Image FloaterLogo { get; private set; }

		#region Start Form
		public Image StartFormBackgroundLogo { get; private set; }
		public Image StartFormLogo { get; private set; }
		public Image StartFormNewImage { get; private set; }
		public Image StartFormOpenImage { get; private set; }
		public Image StartFormQuickEditScheduleImage { get; private set; }
		public Image StartFormCancelImage { get; private set; }
		#endregion

		#region Common Ribbon Resources
		public Image RibbonOutputImage { get; private set; }
		public Image RibbonSettingsImage { get; private set; }
		#endregion

		#region Retractable Bar Resources
		public Image RetractableBarExpandImage { get; private set; }
		public Image RetractableBarCollpaseImage { get; private set; }
		#endregion

		#region Main Menu
		public Image MainMenuNewImage { get; private set; }
		public Image MainMenuOpenImage { get; private set; }
		public Image MainMenuSaveImage { get; private set; }
		public Image MainMenuSaveAsImage { get; private set; }
		public Image MainMenuOutputPdfImage { get; private set; }
		public Image MainMenuEmailImage { get; private set; }
		public Image MainMenuSlideSettingsImage { get; private set; }
		public Image MainMenuHelpImage { get; private set; }
		public Image MainMenuExitImage { get; private set; }
		#endregion

		#region Qat Menu
		public Image QatSaveImage { get; private set; }
		public Image QatSaveAsImage { get; private set; }
		public Image QatFloaterImage { get; private set; }
		public Image QatHelpImage { get; private set; }
		#endregion

		#region Home Resources
		public Image HomeDefaultTopLogo { get; private set; }
		public Image HomeDefaultBottomLogo { get; private set; }
		public Image HomeSplashBottomLogo { get; private set; }
		public Image HomeDateStartImage { get; private set; }
		public Image HomeDateEndImage { get; private set; }
		public Image HomeTopTitleImage { get; private set; }
		public Image HomeBottomTitleImage { get; private set; }
		public Image HomeWeeklyScheduleImage { get; private set; }
		public Image HomeMonthlyScheduleImage { get; private set; }
		public Image HomeSnaphotShortcutImage { get; private set; }
		public Image HomeOptionsShortcutImage { get; private set; }
		public Image HomeCalendarShortcutImage { get; private set; }
		public Image HomeNewSchedulePopupLogo { get; private set; }
		public Image HomeOpenSchedulePopupImage { get; private set; }
		public Image HomeDeleteSchedulePopupImage { get; private set; }
		public Image HomeSettingsDemoImage { get; private set; }
		public Image HomeSettingsDaypartsImage { get; private set; }
		public Image HomeSettingsCalendarTypeImage { get; private set; }
		public Image HomeSettingsFlightDatesLogo { get; private set; }
		#endregion

		#region Program Schedule Resources
		public Image ProgramScheduleRibbonLogo { get; private set; }
		public Image ProgramScheduleNoRecordsLogo { get; private set; }
		public Image ProgramScheduleNoProgramsLogo { get; private set; }
		public Image ProgramScheduleNoDigitalItemsLogo { get; private set; }
		public Image ProgramScheduleNewPopupLogo { get; private set; }
		public Image ProgramScheduleRetractableBarColumnsImage { get; private set; }
		public Image ProgramScheduleRetractableBarTotalsImage { get; private set; }
		public Image ProgramScheduleRetractableBarDigitalImage { get; private set; }
		public Image ProgramScheduleRetractableBarSummaryImage { get; private set; }
		public Image ProgramScheduleRetractableBarColorsImage { get; private set; }
		#endregion

		#region Snapshots Resources
		public Image SnapshotsRibbonLogo { get; private set; }
		public Image SnapshotsNoRecordsLogo { get; private set; }
		public Image SnapshotsNoProgramsLogo { get; private set; }
		public Image SnapshotsNoDigitalItemsLogo { get; private set; }
		public Image SnapshotsNewPopupLogo { get; private set; }
		public Image SnapshotsRetractableBarColumnsImage { get; private set; }
		public Image SnapshotsRetractableBarDigitalImage { get; private set; }
		public Image SnapshotsRetractableBarSummaryImage { get; private set; }
		public Image SnapshotsRetractableBarActiveWeeksImage { get; private set; }
		public Image SnapshotsRetractableBarColorsImage { get; private set; }
		#endregion

		#region Options Resources
		public Image OptionsRibbonLogo { get; private set; }
		public Image OptionsNoRecordsLogo { get; private set; }
		public Image OptionsNoProgramsLogo { get; private set; }
		public Image OptionsNoDigitalItemsLogo { get; private set; }
		public Image OptionsNewPopupLogo { get; private set; }
		public Image OptionsRetractableBarColumnsImage { get; private set; }
		public Image OptionsRetractableBarDigitalImage { get; private set; }
		public Image OptionsRetractableBarSummaryImage { get; private set; }
		public Image OptionsRetractableBarColorsImage { get; private set; }
		#endregion

		#region Digital Resources
		public Image DigitalProductsRibbonLogo { get; private set; }
		public Image DigitalProductsHomeMainLogo { get; private set; }
		public Image DigitalProductsHomeRightLogo { get; private set; }
		public Image DigitalProductsHomeBottomLogo { get; private set; }
		public Image DigitalStandalonePackageNoRecordsLogo { get; private set; }
		public Image DigitalRetractableBarListImage { get; private set; }
		public Image DigitalRetractableBarProductPackageImage { get; private set; }
		public Image DigitalRetractableBarStandalonePackageImage { get; private set; }
		public Image DigitalRetractableBarProductPackageFormulaImage { get; private set; }
		public Image DigitalRetractableBarStandalonePackageFormulaImage { get; private set; }
		#endregion

		#region Calendar Resources
		public Image CalendarNoDataLogo { get; private set; }
		public Image CalendarResetImage { get; private set; }
		public Image CalendarDataSourceScheduleImage { get; private set; }
		public Image CalendarDataSourceSnapshotsImage { get; private set; }
		public Image CalendarDataSourceEmptyImage { get; private set; }
		public Image CalendarDataCopyImage { get; private set; }
		public Image CalendarDataPasteImage { get; private set; }
		public Image CalendarDataCloneImage { get; private set; }
		public Image CalendarRetractableBarFavoritesImage { get; private set; }
		public Image CalendarRetractableBarStyleImage { get; private set; }
		public Image CalendarRetractableBarCommentsImage { get; private set; }
		#endregion

		#region Browser Resources
		public Image BrowserNavigationBack { get; private set; }
		public Image BrowserNavigationForward { get; private set; }
		public Image BrowserNavigationRefresh { get; private set; }
		public Image BrowserExternalChrome { get; private set; }
		public Image BrowserExternalFirefox { get; private set; }
		public Image BrowserExternalIE { get; private set; }
		public Image BrowserExternalEdge { get; private set; }
		public Image BrowserPowerPointAddSlide { get; private set; }
		public Image BrowserPowerPointAddSlides { get; private set; }
		public Image BrowserPowerPointPrint { get; private set; }
		public Image BrowserVideoAdd { get; private set; }
		public Image BrowserYoutubeAdd { get; private set; }
		public Image BrowserUrlCopy { get; private set; }
		public Image BrowserUrlEmail { get; private set; }
		#endregion

		#region Solution
		public Image SolutionToggleSwitchSkinElement { get; private set; }
		#endregion

		public void Load()
		{
			var resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "form_icon.ico");
			if (File.Exists(resourceFile))
				MainAppIcon = new Icon(resourceFile);

			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "app_logo.png");
			if (File.Exists(resourceFile))
				MainAppRibbonLogo = Image.FromFile(resourceFile);

			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "branding_image.png");
			if (File.Exists(resourceFile))
				FloaterLogo = Image.FromFile(resourceFile);

			#region Start Form
			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "launch_brand_large.png");
			if (File.Exists(resourceFile))
				StartFormBackgroundLogo = Image.FromFile(resourceFile);
			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "launch_brand.png");
			if (File.Exists(resourceFile))
				StartFormLogo = Image.FromFile(resourceFile);
			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "launch_new.png");
			if (File.Exists(resourceFile))
				StartFormNewImage = Image.FromFile(resourceFile);
			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "launch_open.png");
			if (File.Exists(resourceFile))
				StartFormOpenImage = Image.FromFile(resourceFile);
			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "launch_gitrdun.png");
			if (File.Exists(resourceFile))
				StartFormQuickEditScheduleImage = Image.FromFile(resourceFile);
			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "launch_cancel.png");
			if (File.Exists(resourceFile))
				StartFormCancelImage = Image.FromFile(resourceFile);
			#endregion

			#region Common Ribbon Resources
			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "ribbon_output.png");
			if (File.Exists(resourceFile))
				RibbonOutputImage = Image.FromFile(resourceFile);

			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "ribbon_settings.png");
			if (File.Exists(resourceFile))
				RibbonSettingsImage = Image.FromFile(resourceFile);
			#endregion

			#region Retractable Bar Resources
			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "leftbar_arrow_right.png");
			if (File.Exists(resourceFile))
				RetractableBarExpandImage = Image.FromFile(resourceFile);
			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "leftbar_arrow_left.png");
			if (File.Exists(resourceFile))
				RetractableBarCollpaseImage = Image.FromFile(resourceFile);
			#endregion

			#region Main Menu
			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "filetab_new.png");
			if (File.Exists(resourceFile))
				MainMenuNewImage = Image.FromFile(resourceFile);
			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "filetab_open.png");
			if (File.Exists(resourceFile))
				MainMenuOpenImage = Image.FromFile(resourceFile);
			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "filetab_save.png");
			if (File.Exists(resourceFile))
				MainMenuSaveImage = Image.FromFile(resourceFile);
			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "filetab_saveas.png");
			if (File.Exists(resourceFile))
				MainMenuSaveAsImage = Image.FromFile(resourceFile);
			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "filetab_savepdf.png");
			if (File.Exists(resourceFile))
				MainMenuOutputPdfImage = Image.FromFile(resourceFile);
			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "filetab_sendemail.png");
			if (File.Exists(resourceFile))
				MainMenuEmailImage = Image.FromFile(resourceFile);
			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "filetab_preferences.png");
			if (File.Exists(resourceFile))
				MainMenuSlideSettingsImage = Image.FromFile(resourceFile);
			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "filetab_help.png");
			if (File.Exists(resourceFile))
				MainMenuHelpImage = Image.FromFile(resourceFile);
			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "filetab_exit.png");
			if (File.Exists(resourceFile))
				MainMenuExitImage = Image.FromFile(resourceFile);
			#endregion

			#region Qat Menu
			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "titlebar_save.png");
			if (File.Exists(resourceFile))
				QatSaveImage = Image.FromFile(resourceFile);
			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "titlebar_saveas.png");
			if (File.Exists(resourceFile))
				QatSaveAsImage = Image.FromFile(resourceFile);
			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "titlebar_floater.png");
			if (File.Exists(resourceFile))
				QatFloaterImage = Image.FromFile(resourceFile);
			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "titlebar_help.png");
			if (File.Exists(resourceFile))
				QatHelpImage = Image.FromFile(resourceFile);
			#endregion

			#region Home Resources
			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "home_default.png");
			if (File.Exists(resourceFile))
				HomeDefaultTopLogo = Image.FromFile(resourceFile);

			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "home_default_bottom.png");
			if (File.Exists(resourceFile))
				HomeDefaultBottomLogo = Image.FromFile(resourceFile);

			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "home_bottom_right.png");
			if (File.Exists(resourceFile))
				HomeSplashBottomLogo = Image.FromFile(resourceFile);

			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "ribbon_home_cal_start.png");
			if (File.Exists(resourceFile))
				HomeDateStartImage = Image.FromFile(resourceFile);

			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "ribbon_home_cal_end.png");
			if (File.Exists(resourceFile))
				HomeDateEndImage = Image.FromFile(resourceFile);

			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "home_subtab1_top.png");
			if (File.Exists(resourceFile))
				HomeTopTitleImage = Image.FromFile(resourceFile);

			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "home_subtab1_bottom.png");
			if (File.Exists(resourceFile))
				HomeBottomTitleImage = Image.FromFile(resourceFile);

			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "home_top_1.png");
			if (File.Exists(resourceFile))
				HomeWeeklyScheduleImage = Image.FromFile(resourceFile);

			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "home_top_2.png");
			if (File.Exists(resourceFile))
				HomeMonthlyScheduleImage = Image.FromFile(resourceFile);

			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "home_bottom_3.png");
			if (File.Exists(resourceFile))
				HomeSnaphotShortcutImage = Image.FromFile(resourceFile);

			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "home_bottom_4.png");
			if (File.Exists(resourceFile))
				HomeOptionsShortcutImage = Image.FromFile(resourceFile);

			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "home_bottom_5.png");
			if (File.Exists(resourceFile))
				HomeCalendarShortcutImage = Image.FromFile(resourceFile);

			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "popup_filename.png");
			if (File.Exists(resourceFile))
				HomeNewSchedulePopupLogo = Image.FromFile(resourceFile);

			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "popup_open.png");
			if (File.Exists(resourceFile))
				HomeOpenSchedulePopupImage = Image.FromFile(resourceFile);

			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "popup_delete.png");
			if (File.Exists(resourceFile))
				HomeDeleteSchedulePopupImage = Image.FromFile(resourceFile);

			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "preferences_demo.png");
			if (File.Exists(resourceFile))
				HomeSettingsDemoImage = Image.FromFile(resourceFile);

			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "preferences_dayparts.png");
			if (File.Exists(resourceFile))
				HomeSettingsDaypartsImage = Image.FromFile(resourceFile);

			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "preferences_calendar.png");
			if (File.Exists(resourceFile))
				HomeSettingsCalendarTypeImage = Image.FromFile(resourceFile);

			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "popup_dates.png");
			if (File.Exists(resourceFile))
				HomeSettingsFlightDatesLogo = Image.FromFile(resourceFile);
			#endregion

			#region Program Schedule Resources
			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "1d_schedule_group1.png");
			if (File.Exists(resourceFile))
				ProgramScheduleRibbonLogo = Image.FromFile(resourceFile);

			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "1a_new_schedule_default.png");
			if (File.Exists(resourceFile))
				ProgramScheduleNoRecordsLogo = Image.FromFile(resourceFile);

			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "1b_new_schedule_line_default.png");
			if (File.Exists(resourceFile))
				ProgramScheduleNoProgramsLogo = Image.FromFile(resourceFile);

			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "1c_new_schedule_digital_line.png");
			if (File.Exists(resourceFile))
				ProgramScheduleNoDigitalItemsLogo = Image.FromFile(resourceFile);

			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "popup_schedule.png");
			if (File.Exists(resourceFile))
				ProgramScheduleNewPopupLogo = Image.FromFile(resourceFile);

			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "leftbar_sched_tv_radio_columns.png");
			if (File.Exists(resourceFile))
				ProgramScheduleRetractableBarColumnsImage = Image.FromFile(resourceFile);

			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "leftbar_sched_tv_radio_totals.png");
			if (File.Exists(resourceFile))
				ProgramScheduleRetractableBarTotalsImage = Image.FromFile(resourceFile);

			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "leftbar_sched_digital_info.png");
			if (File.Exists(resourceFile))
				ProgramScheduleRetractableBarDigitalImage = Image.FromFile(resourceFile);

			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "leftbar_sched_summary_info.png");
			if (File.Exists(resourceFile))
				ProgramScheduleRetractableBarSummaryImage = Image.FromFile(resourceFile);

			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "leftbar_sched_colors.png");
			if (File.Exists(resourceFile))
				ProgramScheduleRetractableBarColorsImage = Image.FromFile(resourceFile);
			#endregion

			#region Snapshots Resources
			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "2d_snapshot_group1.png");
			if (File.Exists(resourceFile))
				SnapshotsRibbonLogo = Image.FromFile(resourceFile);

			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "2a_new_snapshot_default.png");
			if (File.Exists(resourceFile))
				SnapshotsNoRecordsLogo = Image.FromFile(resourceFile);

			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "2b_new_snapshot_line_default.png");
			if (File.Exists(resourceFile))
				SnapshotsNoProgramsLogo = Image.FromFile(resourceFile);

			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "2c_new_snapshot_digital_line.png");
			if (File.Exists(resourceFile))
				SnapshotsNoDigitalItemsLogo = Image.FromFile(resourceFile);

			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "popup_snapshot.png");
			if (File.Exists(resourceFile))
				SnapshotsNewPopupLogo = Image.FromFile(resourceFile);

			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "leftbar_snap_columns.png");
			if (File.Exists(resourceFile))
				SnapshotsRetractableBarColumnsImage = Image.FromFile(resourceFile);

			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "leftbar_snap_digital_info.png");
			if (File.Exists(resourceFile))
				SnapshotsRetractableBarDigitalImage = Image.FromFile(resourceFile);

			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "leftbar_snap_summary_info.png");
			if (File.Exists(resourceFile))
				SnapshotsRetractableBarSummaryImage = Image.FromFile(resourceFile);

			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "leftbar_snap_active_weeks.png");
			if (File.Exists(resourceFile))
				SnapshotsRetractableBarActiveWeeksImage = Image.FromFile(resourceFile);

			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "leftbar_snap_colors.png");
			if (File.Exists(resourceFile))
				SnapshotsRetractableBarColorsImage = Image.FromFile(resourceFile);
			#endregion

			#region Options Resources
			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "3d_options_group1.png");
			if (File.Exists(resourceFile))
				OptionsRibbonLogo = Image.FromFile(resourceFile);

			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "3a_new_options_default.png");
			if (File.Exists(resourceFile))
				OptionsNoRecordsLogo = Image.FromFile(resourceFile);

			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "3b_new_options_line_default.png");
			if (File.Exists(resourceFile))
				OptionsNoProgramsLogo = Image.FromFile(resourceFile);

			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "3c_new_options_digital_line.png");
			if (File.Exists(resourceFile))
				OptionsNoDigitalItemsLogo = Image.FromFile(resourceFile);

			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "popup_flex.png");
			if (File.Exists(resourceFile))
				OptionsNewPopupLogo = Image.FromFile(resourceFile);

			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "leftbar_flex_columns.png");
			if (File.Exists(resourceFile))
				OptionsRetractableBarColumnsImage = Image.FromFile(resourceFile);

			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "leftbar_flex_digital_info.png");
			if (File.Exists(resourceFile))
				OptionsRetractableBarDigitalImage = Image.FromFile(resourceFile);

			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "leftbar_flex_summary_info.png");
			if (File.Exists(resourceFile))
				OptionsRetractableBarSummaryImage = Image.FromFile(resourceFile);

			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "leftbar_flex_colors.png");
			if (File.Exists(resourceFile))
				OptionsRetractableBarColorsImage = Image.FromFile(resourceFile);
			#endregion

			#region Digital Resources
			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "digital_ribbon_group_1.png");
			if (File.Exists(resourceFile))
				DigitalProductsRibbonLogo = Image.FromFile(resourceFile);

			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "digital_home.png");
			if (File.Exists(resourceFile))
				DigitalProductsHomeMainLogo = Image.FromFile(resourceFile);

			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "digital_home_right.png");
			if (File.Exists(resourceFile))
				DigitalProductsHomeRightLogo = Image.FromFile(resourceFile);

			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "digital_home_bottom.png");
			if (File.Exists(resourceFile))
				DigitalProductsHomeBottomLogo = Image.FromFile(resourceFile);

			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "4_digital_subtab_6_default.png");
			if (File.Exists(resourceFile))
				DigitalStandalonePackageNoRecordsLogo = Image.FromFile(resourceFile);

			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "leftbar_digital_list.png");
			if (File.Exists(resourceFile))
				DigitalRetractableBarListImage = Image.FromFile(resourceFile);

			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "leftbar_digital_package.png");
			if (File.Exists(resourceFile))
				DigitalRetractableBarProductPackageImage = Image.FromFile(resourceFile);

			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "leftbar_digital_alacarte.png");
			if (File.Exists(resourceFile))
				DigitalRetractableBarStandalonePackageImage = Image.FromFile(resourceFile);

			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "leftbar_digital_package_calc.png");
			if (File.Exists(resourceFile))
				DigitalRetractableBarProductPackageFormulaImage = Image.FromFile(resourceFile);

			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "leftbar_digital_alacarte_calc.png");
			if (File.Exists(resourceFile))
				DigitalRetractableBarStandalonePackageFormulaImage = Image.FromFile(resourceFile);
			#endregion

			#region Calendar Resources
			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "5_summary_calendar_default.png");
			if (File.Exists(resourceFile))
				CalendarNoDataLogo = Image.FromFile(resourceFile);

			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "ribbon_cal_reset.png");
			if (File.Exists(resourceFile))
				CalendarResetImage = Image.FromFile(resourceFile);

			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "ribbon_cal_sched.png");
			if (File.Exists(resourceFile))
				CalendarDataSourceScheduleImage = Image.FromFile(resourceFile);

			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "ribbon_cal_snap.png");
			if (File.Exists(resourceFile))
				CalendarDataSourceSnapshotsImage = Image.FromFile(resourceFile);

			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "ribbon_cal_custom.png");
			if (File.Exists(resourceFile))
				CalendarDataSourceEmptyImage = Image.FromFile(resourceFile);

			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "ribbon_cal_copy.png");
			if (File.Exists(resourceFile))
				CalendarDataCopyImage = Image.FromFile(resourceFile);

			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "ribbon_cal_paste.png");
			if (File.Exists(resourceFile))
				CalendarDataPasteImage = Image.FromFile(resourceFile);

			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "ribbon_cal_clone.png");
			if (File.Exists(resourceFile))
				CalendarDataCloneImage = Image.FromFile(resourceFile);

			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "leftbar_cal_gallery.png");
			if (File.Exists(resourceFile))
				CalendarRetractableBarFavoritesImage = Image.FromFile(resourceFile);

			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "leftbar_cal_slide_style.png");
			if (File.Exists(resourceFile))
				CalendarRetractableBarStyleImage = Image.FromFile(resourceFile);

			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "leftbar_cal_comments.png");
			if (File.Exists(resourceFile))
				CalendarRetractableBarCommentsImage = Image.FromFile(resourceFile);
			#endregion

			#region Browser Resources
			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "eo_left.png");
			if (File.Exists(resourceFile))
				BrowserNavigationBack = Image.FromFile(resourceFile);
			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "eo_right.png");
			if (File.Exists(resourceFile))
				BrowserNavigationForward = Image.FromFile(resourceFile);
			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "eo_refresh.png");
			if (File.Exists(resourceFile))
				BrowserNavigationRefresh = Image.FromFile(resourceFile);
			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "eo_chrome.png");
			if (File.Exists(resourceFile))
				BrowserExternalChrome = Image.FromFile(resourceFile);
			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "eo_firefox.png");
			if (File.Exists(resourceFile))
				BrowserExternalFirefox = Image.FromFile(resourceFile);
			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "eo_ie.png");
			if (File.Exists(resourceFile))
				BrowserExternalIE = Image.FromFile(resourceFile);
			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "eo_edge.png");
			if (File.Exists(resourceFile))
				BrowserExternalEdge = Image.FromFile(resourceFile);
			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "eo_add_slide.png");
			if (File.Exists(resourceFile))
				BrowserPowerPointAddSlide = Image.FromFile(resourceFile);
			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "eo_add_all.png");
			if (File.Exists(resourceFile))
				BrowserPowerPointAddSlides = Image.FromFile(resourceFile);
			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "eo_printer.png");
			if (File.Exists(resourceFile))
				BrowserPowerPointPrint = Image.FromFile(resourceFile);
			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "eo_video.png");
			if (File.Exists(resourceFile))
				BrowserVideoAdd = Image.FromFile(resourceFile);
			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "eo_youtube.png");
			if (File.Exists(resourceFile))
				BrowserYoutubeAdd = Image.FromFile(resourceFile);
			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "eo_copy.png");
			if (File.Exists(resourceFile))
				BrowserUrlCopy = Image.FromFile(resourceFile);
			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "eo_email.png");
			if (File.Exists(resourceFile))
				BrowserUrlEmail = Image.FromFile(resourceFile);
			#endregion

			#region Solution
			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "toggleswitch.png");
			if (File.Exists(resourceFile))
				SolutionToggleSwitchSkinElement = Image.FromFile(resourceFile);
			#endregion
		}
	}
}
