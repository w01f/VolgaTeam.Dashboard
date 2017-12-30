using System;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Common.Enums;
using Asa.Business.Media.Configuration;
using Asa.Business.Media.Entities.NonPersistent.Schedule;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;
using Asa.Common.GUI.Floater;
using Asa.Common.GUI.ToolForms;
using Asa.Media.Controls;
using Asa.Media.Controls.BusinessClasses.Managers;
using Asa.Media.Controls.Properties;
using Asa.Media.Controls.ToolForms;
using Asa.Schedules.Common.Controls.ContentEditors.Enums;
using Asa.Schedules.Common.Controls.ContentEditors.Events;
using Asa.Schedules.Common.Controls.ContentEditors.Helpers;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Metro.ColorTables;
using DevExpress.Skins;
using DevExpress.XtraLayout.Utils;
using FormStart = Asa.Media.Controls.ToolForms.FormStart;

namespace Asa.Media.Single
{
	public partial class FormMain : RibbonForm
	{
		private static FormMain _instance;

		private FormMain()
		{
			InitializeComponent();

			Width = (Int32)(Screen.PrimaryScreen.Bounds.Width * 0.8);
			Height = (Int32)(Screen.PrimaryScreen.Bounds.Height * 0.8);
			Left = (Screen.PrimaryScreen.Bounds.Width - Width) / 2;
			Top = (Screen.PrimaryScreen.Bounds.Height - Height) / 2;

			pictureEditDefaultLogo.Image = BusinessObjects.Instance.ImageResourcesManager.StartFormBackgroundLogo;

			var scaleFactor = Utilities.GetScaleFactor(CreateGraphics().DpiX);
			simpleLabelItemAdvertiser.MaxSize = RectangleHelper.ScaleSize(simpleLabelItemAdvertiser.MaxSize, scaleFactor);
			simpleLabelItemAdvertiser.MinSize = RectangleHelper.ScaleSize(simpleLabelItemAdvertiser.MinSize, scaleFactor);
			simpleLabelItemFlightDates.MaxSize = RectangleHelper.ScaleSize(simpleLabelItemFlightDates.MaxSize, scaleFactor);
			simpleLabelItemFlightDates.MinSize = RectangleHelper.ScaleSize(simpleLabelItemFlightDates.MinSize, scaleFactor);

			PopupMessageHelper.Instance.MainForm = this;
		}

		public static FormMain Instance => _instance ?? (_instance = new FormMain());

		public static void RemoveInstance()
		{
			_instance.Dispose();
			_instance = null;
		}

		public void Init()
		{
			FormStateHelper.Init(this,
					Common.Core.Configuration.ResourceManager.Instance.AppSettingsFolder,
					MediaMetaData.Instance.DataTypeString,
					false)
				.LoadState();

			Icon = BusinessObjects.Instance.ImageResourcesManager.MainAppIcon ?? Icon;

			buttonItemApplicationMenuNew.Image = BusinessObjects.Instance.ImageResourcesManager.MainMenuNewImage ??
												 buttonItemApplicationMenuNew.Image;
			buttonItemApplicationMenuOpen.Image = BusinessObjects.Instance.ImageResourcesManager.MainMenuOpenImage ??
												 buttonItemApplicationMenuOpen.Image;
			buttonItemApplicationMenuSave.Image = BusinessObjects.Instance.ImageResourcesManager.MainMenuSaveImage ??
												 buttonItemApplicationMenuSave.Image;
			buttonItemApplicationMenuSaveAs.Image = BusinessObjects.Instance.ImageResourcesManager.MainMenuSaveAsImage ??
												 buttonItemApplicationMenuSaveAs.Image;
			buttonItemApplicationMenuOutputPdf.Image = BusinessObjects.Instance.ImageResourcesManager.MainMenuOutputPdfImage ??
												 buttonItemApplicationMenuOutputPdf.Image;
			buttonItemApplicationMenuEmail.Image = BusinessObjects.Instance.ImageResourcesManager.MainMenuEmailImage ??
												 buttonItemApplicationMenuEmail.Image;
			buttonItemApplicationMenuSlideSettings.Image = BusinessObjects.Instance.ImageResourcesManager.MainMenuSlideSettingsImage ??
												 buttonItemApplicationMenuSlideSettings.Image;
			buttonItemApplicationMenuHelp.Image = BusinessObjects.Instance.ImageResourcesManager.MainMenuHelpImage ??
												 buttonItemApplicationMenuHelp.Image;
			buttonItemApplicationMenuExit.Image = BusinessObjects.Instance.ImageResourcesManager.MainMenuExitImage ??
												 buttonItemApplicationMenuExit.Image;

			buttonItemQatSave.Image = BusinessObjects.Instance.ImageResourcesManager.QatSaveImage ??
									  buttonItemQatSave.Image;
			buttonItemQatSaveAs.Image = BusinessObjects.Instance.ImageResourcesManager.QatSaveAsImage ??
									  buttonItemQatSaveAs.Image;
			buttonItemQatFloater.Image = BusinessObjects.Instance.ImageResourcesManager.QatFloaterImage ??
									  buttonItemQatFloater.Image;
			buttonItemQatHelp.Image = BusinessObjects.Instance.ImageResourcesManager.QatHelpImage ??
									  buttonItemQatHelp.Image;

			if (BusinessObjects.Instance.ImageResourcesManager.HomeDateStartImage != null)
			{
				labelItemHomeFlightDatesStartTitle.Image = BusinessObjects.Instance.ImageResourcesManager.HomeDateStartImage;
				labelItemHomeFlightDatesStartTitle.Width +=
					BusinessObjects.Instance.ImageResourcesManager.HomeDateStartImage.Width - 24;
			}
			if (BusinessObjects.Instance.ImageResourcesManager.HomeDateEndImage != null)
			{
				labelItemHomeFlightDatesEndTitle.Image = BusinessObjects.Instance.ImageResourcesManager.HomeDateEndImage;
				labelItemHomeFlightDatesEndTitle.Width +=
					BusinessObjects.Instance.ImageResourcesManager.HomeDateEndImage.Width - 24;
			}
			ribbonBarHomeFlightDates.RecalcLayout();
			buttonItemHomeSettings.Image = BusinessObjects.Instance.ImageResourcesManager.RibbonSettingsImage ?? buttonItemHomeSettings.Image;
			ribbonBarHomeSettings.RecalcLayout();
			ribbonPanelHome.PerformLayout();

			if (BusinessObjects.Instance.ImageResourcesManager.ProgramScheduleRibbonLogo != null)
			{
				buttonItemProgramScheduleNew.Image = BusinessObjects.Instance.ImageResourcesManager.ProgramScheduleRibbonLogo;
				ribbonBarProgramScheduleNew.RecalcLayout();
			}
			buttonItemProgramSchedulePowerPoint.Image = BusinessObjects.Instance.ImageResourcesManager.RibbonOutputImage ?? buttonItemProgramSchedulePowerPoint.Image;
			ribbonBarProgramSchedulePowerPoint.RecalcLayout();
			buttonItemProgramSchedulePreview.Image = BusinessObjects.Instance.ImageResourcesManager.RibbonPreviewImage ?? buttonItemProgramSchedulePreview.Image;
			ribbonBarProgramSchedulePreview.RecalcLayout();
			buttonItemProgramScheduleSettings.Image = BusinessObjects.Instance.ImageResourcesManager.RibbonSettingsImage ?? buttonItemProgramScheduleSettings.Image;
			ribbonBarProgramScheduleSettings.RecalcLayout();
			ribbonPanelProgramSchedule.PerformLayout();

			if (BusinessObjects.Instance.ImageResourcesManager.SnapshotsRibbonLogo != null)
			{
				buttonItemSnapshotNew.Image = BusinessObjects.Instance.ImageResourcesManager.SnapshotsRibbonLogo;
				ribbonBarSnapshotNew.RecalcLayout();
			}
			buttonItemSnapshotPowerPoint.Image = BusinessObjects.Instance.ImageResourcesManager.RibbonOutputImage ?? buttonItemSnapshotPowerPoint.Image;
			ribbonBarSnapshotPowerPoint.RecalcLayout();
			buttonItemSnapshotPreview.Image = BusinessObjects.Instance.ImageResourcesManager.RibbonPreviewImage ?? buttonItemSnapshotPreview.Image;
			ribbonBarSnapshotPreview.RecalcLayout();
			buttonItemSnapshotSettings.Image = BusinessObjects.Instance.ImageResourcesManager.RibbonSettingsImage ?? buttonItemSnapshotSettings.Image;
			ribbonBarSnapshotSettings.RecalcLayout();
			ribbonPanelSnapshot.PerformLayout();

			if (BusinessObjects.Instance.ImageResourcesManager.OptionsRibbonLogo != null)
			{
				buttonItemOptionsNew.Image = BusinessObjects.Instance.ImageResourcesManager.OptionsRibbonLogo;
				ribbonBarOptionsNew.RecalcLayout();
			}
			buttonItemOptionsPowerPoint.Image = BusinessObjects.Instance.ImageResourcesManager.RibbonOutputImage ?? buttonItemOptionsPowerPoint.Image;
			ribbonBarOptionsPowerPoint.RecalcLayout();
			buttonItemOptionsPreview.Image = BusinessObjects.Instance.ImageResourcesManager.RibbonPreviewImage ?? buttonItemOptionsPreview.Image;
			ribbonBarOptionsPreview.RecalcLayout();
			buttonItemOptionsSettings.Image = BusinessObjects.Instance.ImageResourcesManager.RibbonSettingsImage ?? buttonItemOptionsSettings.Image;
			ribbonBarOptionsSettings.RecalcLayout();
			ribbonPanelOptions.PerformLayout();

			if (BusinessObjects.Instance.ImageResourcesManager.DigitalProductsRibbonLogo != null)
			{
				buttonItemDigitalScheduleLogo.Image = BusinessObjects.Instance.ImageResourcesManager.DigitalProductsRibbonLogo;
				ribbonBarDigitalScheduleLogo.RecalcLayout();
			}
			buttonItemDigitalSchedulePowerPoint.Image = BusinessObjects.Instance.ImageResourcesManager.RibbonOutputImage ?? buttonItemDigitalSchedulePowerPoint.Image;
			ribbonBarDigitalSchedulePowerPoint.RecalcLayout();
			buttonItemDigitalSchedulePreview.Image = BusinessObjects.Instance.ImageResourcesManager.RibbonPreviewImage ?? buttonItemDigitalSchedulePreview.Image;
			ribbonBarDigitalSchedulePreview.RecalcLayout();
			ribbonPanelDigitalSchedule.PerformLayout();

			buttonItemCalendar1DataSourceSchedule.Image = BusinessObjects.Instance.ImageResourcesManager.CalendarDataSourceScheduleImage ?? buttonItemCalendar1DataSourceSchedule.Image;
			ribbonBarCalendar1DataSourceSchedule.RecalcLayout();
			buttonItemCalendar1DataSourceSnapshots.Image = BusinessObjects.Instance.ImageResourcesManager.CalendarDataSourceSnapshotsImage ?? buttonItemCalendar1DataSourceSnapshots.Image;
			ribbonBarCalendar1DataSourceSnapshots.RecalcLayout();
			buttonItemCalendar1DataSourceEmpty.Image = BusinessObjects.Instance.ImageResourcesManager.CalendarDataSourceEmptyImage ?? buttonItemCalendar1DataSourceEmpty.Image;
			ribbonBarCalendar1DataSourceEmpty.RecalcLayout();
			buttonItemCalendar1Reset.Image = BusinessObjects.Instance.ImageResourcesManager.CalendarResetImage ?? buttonItemCalendar1Reset.Image;
			ribbonBarCalendar1Reset.RecalcLayout();
			buttonItemCalendar1Copy.Image = BusinessObjects.Instance.ImageResourcesManager.CalendarDataCopyImage ?? buttonItemCalendar1Copy.Image;
			buttonItemCalendar1Paste.Image = BusinessObjects.Instance.ImageResourcesManager.CalendarDataPasteImage ?? buttonItemCalendar1Paste.Image;
			buttonItemCalendar1Clone.Image = BusinessObjects.Instance.ImageResourcesManager.CalendarDataCloneImage ?? buttonItemCalendar1Clone.Image;
			ribbonBarCalendar1Edit.RecalcLayout();
			buttonItemCalendar1PowerPoint.Image = BusinessObjects.Instance.ImageResourcesManager.RibbonOutputImage ?? buttonItemCalendar1PowerPoint.Image;
			ribbonBarCalendar1PowerPoint.RecalcLayout();
			buttonItemCalendar1Preview.Image = BusinessObjects.Instance.ImageResourcesManager.RibbonPreviewImage ?? buttonItemCalendar1Preview.Image;
			ribbonBarCalendar1Preview.RecalcLayout();
			ribbonPanelCalendar1.PerformLayout();

			buttonItemCalendar2Reset.Image = BusinessObjects.Instance.ImageResourcesManager.CalendarResetImage ?? buttonItemCalendar2Reset.Image;
			ribbonBarCalendar2Reset.RecalcLayout();
			buttonItemCalendar2Copy.Image = BusinessObjects.Instance.ImageResourcesManager.CalendarDataCopyImage ?? buttonItemCalendar2Copy.Image;
			buttonItemCalendar2Paste.Image = BusinessObjects.Instance.ImageResourcesManager.CalendarDataPasteImage ?? buttonItemCalendar2Paste.Image;
			buttonItemCalendar2Clone.Image = BusinessObjects.Instance.ImageResourcesManager.CalendarDataCloneImage ?? buttonItemCalendar2Clone.Image;
			ribbonBarCalendar2Edit.RecalcLayout();
			buttonItemCalendar2PowerPoint.Image = BusinessObjects.Instance.ImageResourcesManager.RibbonOutputImage ?? buttonItemCalendar2PowerPoint.Image;
			ribbonBarCalendar2PowerPoint.RecalcLayout();
			buttonItemCalendar2Preview.Image = BusinessObjects.Instance.ImageResourcesManager.RibbonPreviewImage ?? buttonItemCalendar2Preview.Image;
			ribbonBarCalendar2Preview.RecalcLayout();
			ribbonPanelCalendar2.PerformLayout();

			buttonItemSolutionsPowerPoint.Image = BusinessObjects.Instance.ImageResourcesManager.RibbonOutputImage ?? buttonItemSolutionsPowerPoint.Image;
			ribbonBarSolutionsPowerPoint.RecalcLayout();
			buttonItemSolutionsPreview.Image = BusinessObjects.Instance.ImageResourcesManager.RibbonPreviewImage ?? buttonItemSolutionsPreview.Image;
			ribbonBarSolutionsPreview.RecalcLayout();
			ribbonPanelSolutions.PerformLayout();

			buttonItemSlidesPowerPoint.Image = BusinessObjects.Instance.ImageResourcesManager.RibbonOutputImage ?? buttonItemSlidesPowerPoint.Image;
			ribbonBarSlidesPowerPoint.RecalcLayout();
			buttonItemSlidesPreview.Image = BusinessObjects.Instance.ImageResourcesManager.RibbonPreviewImage ?? buttonItemSlidesPreview.Image;
			ribbonBarSlidesPreview.RecalcLayout();
			ribbonPanelSlides.PerformLayout();

			if (BusinessObjects.Instance.FormStyleManager.Style.AccentColor.HasValue)
				styleManager.MetroColorParameters = new MetroColorGeneratorParameters(
						styleManager.MetroColorParameters.CanvasColor,
						BusinessObjects.Instance.FormStyleManager.Style.AccentColor.Value);

			Controller.Instance.FormMain = this;
			Controller.Instance.MainPanelContainer = layoutControlGroupContainer;
			Controller.Instance.MainPanel = layoutControlItemMainContainer;
			Controller.Instance.EmptyPanel = emptySpaceItem;
			Controller.Instance.Supertip = superTooltip;
			Controller.Instance.Ribbon = ribbonControl;
			Controller.Instance.TabHome = ribbonTabItemHome;
			Controller.Instance.TabProgramSchedule = ribbonTabItemProgramSchedule;
			Controller.Instance.TabDigitalProduct = ribbonTabItemDigitalSchedule;
			Controller.Instance.TabCalendar1 = ribbonTabItemCalendar1;
			Controller.Instance.TabCalendar2 = ribbonTabItemCalendar2;
			Controller.Instance.TabGallery1 = ribbonTabItemGallery1;
			Controller.Instance.TabGallery2 = ribbonTabItemGallery2;
			Controller.Instance.TabRateCard = ribbonTabItemRateCard;
			Controller.Instance.TabOptions = ribbonTabItemOptions;
			Controller.Instance.TabSnapshot = ribbonTabItemSnapshot;
			Controller.Instance.TabSolutions = ribbonTabItemSolutions;
			Controller.Instance.TabSlides = ribbonTabItemSlides;
			Controller.Instance.TabBrowser = ribbonTabItemBrowser;

			ContentStatusBarManager.Instance.StatusBar = barBottom;
			ContentStatusBarManager.Instance.StatusBarMainItemsContainer = itemContainerStatusBarMainContentInfo;
			ContentStatusBarManager.Instance.StatusBarAdditionalItemsContainer = itemContainerStatusBarAdditionalContentInfo;
			ContentStatusBarManager.Instance.TextColor = BusinessObjects.Instance.FormStyleManager.Style.StatusBarTextColor;

			FormProgress.Init(this);

			#region Command Controls
			Controller.Instance.QatSaveButton = buttonItemQatSave;
			Controller.Instance.QatSaveAsButton = buttonItemQatSaveAs;
			Controller.Instance.QatHelpButton = buttonItemQatHelp;

			Controller.Instance.MenuButtonMain = applicationButtonApplicationMenu;
			Controller.Instance.MenuSaveButton = buttonItemApplicationMenuSave;
			Controller.Instance.MenuSaveAsButton = buttonItemApplicationMenuSaveAs;
			Controller.Instance.MenuOutputPdfButton = buttonItemApplicationMenuOutputPdf;
			Controller.Instance.MenuEmailButton = buttonItemApplicationMenuEmail;
			Controller.Instance.MenuSlideSettingsButton = buttonItemApplicationMenuSlideSettings;
			Controller.Instance.MenuHelpButton = buttonItemApplicationMenuHelp;

			Controller.Instance.RibbonCollapseButton = buttonItemCollapse;
			Controller.Instance.RibbonExpandButton = buttonItemExpand;
			Controller.Instance.RibbonPinButton = buttonItemPin;

			Controller.Instance.ScheduleInfoContainer = layoutControlGroupHeader;
			Controller.Instance.ScheduleInfoAdvertiser = simpleLabelItemAdvertiser;
			Controller.Instance.ScheduleInfoFlightDates = simpleLabelItemFlightDates;

			#region Home
			Controller.Instance.HomePanel = ribbonPanelHome;
			Controller.Instance.HomeSpecialButtons = ribbonBarHomeSpecialButtons;
			Controller.Instance.HomeBusinessName = comboBoxEditBusinessName;
			Controller.Instance.HomeDecisionMaker = comboBoxEditDecisionMaker;
			Controller.Instance.HomePresentationDate = dateEditPresentationDate;
			Controller.Instance.HomeFlightDates = ribbonBarHomeFlightDates;
			Controller.Instance.HomeFlightDatesStartTitle = labelItemHomeFlightDatesStartTitle;
			Controller.Instance.HomeFlightDatesStartValue = labelItemHomeFlightDatesStartValue;
			Controller.Instance.HomeFlightDatesEndTitle = labelItemHomeFlightDatesEndTitle;
			Controller.Instance.HomeFlightDatesEndValue = labelItemHomeFlightDatesEndValue;
			Controller.Instance.HomeSettings = buttonItemHomeSettings;
			#endregion

			#region Program Schedule
			Controller.Instance.ProgramSchedulePanel = ribbonPanelProgramSchedule;
			Controller.Instance.ProgramScheduleThemeBar = ribbonBarProgramScheduleTheme;
			Controller.Instance.ProgramScheduleSpecialButtons = ribbonBarProgramScheduleSpecialButtons;
			Controller.Instance.ProgramSchedulePreview = buttonItemProgramSchedulePreview;
			Controller.Instance.ProgramSchedulePowerPoint = buttonItemProgramSchedulePowerPoint;
			Controller.Instance.ProgramScheduleTheme = buttonItemProgramScheduleTheme;
			Controller.Instance.ProgramScheduleNew = buttonItemProgramScheduleNew;
			Controller.Instance.ProgramScheduleProgramAdd = buttonItemProgramScheduleProgramAdd;
			Controller.Instance.ProgramScheduleProgramDelete = buttonItemProgramScheduleProgramDelete;
			Controller.Instance.ProgramScheduleSettings = buttonItemProgramScheduleSettings;
			#endregion

			#region Digital Product
			Controller.Instance.DigitalProductPanel = ribbonPanelDigitalSchedule;
			Controller.Instance.DigitalProductLogoBar = ribbonBarDigitalScheduleLogo;
			Controller.Instance.DigitalProductThemeBar = ribbonBarDigitalScheduleTheme;
			Controller.Instance.DigitalProductSpecialButtons = ribbonBarDigitalScheduleSpecialButtons;
			Controller.Instance.DigitalProductPreview = buttonItemDigitalSchedulePreview;
			Controller.Instance.DigitalProductPowerPoint = buttonItemDigitalSchedulePowerPoint;
			Controller.Instance.DigitalProductTheme = buttonItemDigitalScheduleTheme;
			Controller.Instance.DigitalProductAdd = buttonItemDigitalScheduleProductAdd;
			Controller.Instance.DigitalProductClone = buttonItemDigitalScheduleProductClone;
			Controller.Instance.DigitalProductDelete = buttonItemDigitalScheduleProductDelete;
			#endregion

			#region Calendar 1
			Controller.Instance.Calendar1Panel = ribbonPanelCalendar1;
			Controller.Instance.Calendar1SpecialButtons = ribbonBarCalendar1SpecialButtons;
			Controller.Instance.Calendar1Copy = buttonItemCalendar1Copy;
			Controller.Instance.Calendar1Paste = buttonItemCalendar1Paste;
			Controller.Instance.Calendar1Clone = buttonItemCalendar1Clone;
			Controller.Instance.Calendar1Preview = buttonItemCalendar1Preview;
			Controller.Instance.Calendar1PowerPoint = buttonItemCalendar1PowerPoint;
			Controller.Instance.Calendar1DataSourceSchedule = buttonItemCalendar1DataSourceSchedule;
			Controller.Instance.Calendar1DataSourceSnapshots = buttonItemCalendar1DataSourceSnapshots;
			Controller.Instance.Calendar1DataSourceEmpty = buttonItemCalendar1DataSourceEmpty;
			Controller.Instance.Calendar1Reset = buttonItemCalendar1Reset;
			#endregion

			#region Calendar 2
			Controller.Instance.Calendar2Panel = ribbonPanelCalendar2;
			Controller.Instance.Calendar2SpecialButtons = ribbonBarCalendar2SpecialButtons;
			Controller.Instance.Calendar2Copy = buttonItemCalendar2Copy;
			Controller.Instance.Calendar2Paste = buttonItemCalendar2Paste;
			Controller.Instance.Calendar2Clone = buttonItemCalendar2Clone;
			Controller.Instance.Calendar2Preview = buttonItemCalendar2Preview;
			Controller.Instance.Calendar2PowerPoint = buttonItemCalendar2PowerPoint;
			Controller.Instance.Calendar2Reset = buttonItemCalendar2Reset;
			#endregion

			#region Snapshot
			Controller.Instance.SnapshotPanel = ribbonPanelSnapshot;
			Controller.Instance.SnapshotThemeBar = ribbonBarSnapshotTheme;
			Controller.Instance.SnapshotSpecialButtons = ribbonBarSnapshotSpecialButtons;
			Controller.Instance.SnapshotPreview = buttonItemSnapshotPreview;
			Controller.Instance.SnapshotPowerPoint = buttonItemSnapshotPowerPoint;
			Controller.Instance.SnapshotTheme = buttonItemSnapshotTheme;
			Controller.Instance.SnapshotNew = buttonItemSnapshotNew;
			Controller.Instance.SnapshotProgramAdd = buttonItemSnapshotProgramAdd;
			Controller.Instance.SnapshotProgramDelete = buttonItemSnapshotProgramDelete;
			Controller.Instance.SnapshotSettings = buttonItemSnapshotSettings;
			#endregion

			#region Options
			Controller.Instance.OptionsPanel = ribbonPanelOptions;
			Controller.Instance.OptionsThemeBar = ribbonBarOptionsTheme;
			Controller.Instance.OptionsSpecialButtons = ribbonBarOptionsSpecialButtons;
			Controller.Instance.OptionsPreview = buttonItemOptionsPreview;
			Controller.Instance.OptionsPowerPoint = buttonItemOptionsPowerPoint;
			Controller.Instance.OptionsTheme = buttonItemOptionsTheme;
			Controller.Instance.OptionsNew = buttonItemOptionsNew;
			Controller.Instance.OptionsProgramAdd = buttonItemOptionsProgramAdd;
			Controller.Instance.OptionsProgramDelete = buttonItemOptionsProgramDelete;
			Controller.Instance.OptionsSettings = buttonItemOptionsSettings;
			#endregion

			#region Solutions
			Controller.Instance.SolutionsPanel = ribbonPanelSolutions;
			Controller.Instance.SolutionsThemeBar = ribbonBarSolutionsTheme;
			Controller.Instance.SolutionsSpecialButtons = ribbonBarSolutionsSpecialButtons;
			Controller.Instance.SolutionsPreview = buttonItemSolutionsPreview;
			Controller.Instance.SolutionsPowerPoint = buttonItemSolutionsPowerPoint;
			Controller.Instance.SolutionsTheme = buttonItemSolutionsTheme;
			#endregion

			#region Slides
			Controller.Instance.SlidesPanel = ribbonPanelSlides;
			Controller.Instance.SlidesLogoBar = ribbonBarSlidesLogo;
			Controller.Instance.SlidesSpecialButtons = ribbonBarSlidesSpecialButtons;
			Controller.Instance.SlidesLogoLabel = labelItemSlideHome;
			Controller.Instance.SlidesPreview = buttonItemSlidesPreview;
			Controller.Instance.SlidesPowerPoint = buttonItemSlidesPowerPoint;
			#endregion

			#region Gallery 1
			Controller.Instance.Gallery1Panel = ribbonPanelGallery1;
			Controller.Instance.Gallery1SpecialButtons = ribbonBarGallery1SpecialButtons;
			Controller.Instance.Gallery1BrowseBar = ribbonBarGallery1Browse;
			Controller.Instance.Gallery1ImageBar = ribbonBarGallery1Image;
			Controller.Instance.Gallery1ZoomBar = ribbonBarGallery1Zoom;
			Controller.Instance.Gallery1CopyBar = ribbonBarGallery1Copy;
			Controller.Instance.Gallery1BrowseModeContainer = itemContainerGallery1BrowseContentType;
			Controller.Instance.Gallery1View = buttonItemGallery1View;
			Controller.Instance.Gallery1Edit = buttonItemGallery1Edit;
			Controller.Instance.Gallery1ImageSelect = buttonItemGallery1ImageSelect;
			Controller.Instance.Gallery1ImageCrop = buttonItemGallery1ImageCrop;
			Controller.Instance.Gallery1ZoomIn = buttonItemGallery1ZoomIn;
			Controller.Instance.Gallery1ZoomOut = buttonItemGallery1ZoomOut;
			Controller.Instance.Gallery1Copy = buttonItemGallery1Copy;
			Controller.Instance.Gallery1Sections = comboBoxEditGallery1Sections;
			Controller.Instance.Gallery1Groups = comboBoxEditGallery1Groups;
			#endregion

			#region Gallery 2
			Controller.Instance.Gallery2Panel = ribbonPanelGallery2;
			Controller.Instance.Gallery2SpecialButtons = ribbonBarGallery2SpecialButtons;
			Controller.Instance.Gallery2BrowseBar = ribbonBarGallery2Browse;
			Controller.Instance.Gallery2ImageBar = ribbonBarGallery2Image;
			Controller.Instance.Gallery2ZoomBar = ribbonBarGallery2Zoom;
			Controller.Instance.Gallery2CopyBar = ribbonBarGallery2Copy;
			Controller.Instance.Gallery2BrowseModeContainer = itemContainerGallery2BrowseContentType;
			Controller.Instance.Gallery2View = buttonItemGallery2View;
			Controller.Instance.Gallery2Edit = buttonItemGallery2Edit;
			Controller.Instance.Gallery2ImageSelect = buttonItemGallery2ImageSelect;
			Controller.Instance.Gallery2ImageCrop = buttonItemGallery2ImageCrop;
			Controller.Instance.Gallery2ZoomIn = buttonItemGallery2ZoomIn;
			Controller.Instance.Gallery2ZoomOut = buttonItemGallery2ZoomOut;
			Controller.Instance.Gallery2Copy = buttonItemGallery2Copy;
			Controller.Instance.Gallery2Sections = comboBoxEditGallery2Sections;
			Controller.Instance.Gallery2Groups = comboBoxEditGallery2Groups;
			#endregion

			#region Rate Card
			Controller.Instance.RateCardPanel = ribbonPanelRateCard;
			Controller.Instance.RateCardSpecialButtons = ribbonBarRateCardSpecialButtons;
			Controller.Instance.RateCardCombo = comboBoxEditRateCards;
			#endregion

			#region Browser
			Controller.Instance.BrowserPanel = ribbonPanelBrowser;
			Controller.Instance.BrowserSpecialButtons = ribbonBarBrowserSpecialButtons;
			Controller.Instance.BrowserSitesBar = ribbonBarBrowserSites;
			Controller.Instance.BrowserSitesTitle = labelItemBrowserSites;
			Controller.Instance.BrowserSitesCombo = comboBoxEditBrowserSites;
			#endregion

			#endregion

			Controller.Instance.InitForm();

			BusinessObjects.Instance.ScheduleManager.ScheduleOpened += (o, e) => UpdateFormTitle();
			BusinessObjects.Instance.ScheduleManager.ScheduleNameChanged += (o, e) => UpdateFormTitle();
			Controller.Instance.FloaterRequested += (o, e) => AppManager.Instance.ShowFloater(this, e);
		}

		private void UpdateFormTitle()
		{
			var schedule = BusinessObjects.Instance.ScheduleManager.ActiveSchedule;
			Text = String.Format("{0} v{1} - {2}",
				PopupMessageHelper.Instance.Title,
				FileStorageManager.Instance.Version,
				schedule?.Name);
		}

		private void LoadData()
		{
			UpdateFormTitle();
			ContentRibbonManager<MediaScheduleChangeInfo>.ShowRibbonTab(ContentIdentifiers.ScheduleSettings);
			if (BusinessObjects.Instance.ScheduleManager.ActiveSchedule.Settings.EditMode == ScheduleEditMode.Regular)
				Controller.Instance.CheckPowerPointRunning();
		}

		private void AddNewRegularSchedule()
		{
			using (var form = new FormScheduleName())
			{
				form.pictureEditLogo.Image = BusinessObjects.Instance.ImageResourcesManager.HomeNewSchedulePopupLogo ?? form.pictureEditLogo.Image;
				if (form.ShowDialog(this) != DialogResult.OK) return;
				BusinessObjects.Instance.ScheduleManager.AddReqularSchedule(form.ScheduleName);
			}
		}

		private void AddNewQuickEditSchedule()
		{
			BusinessObjects.Instance.ScheduleManager.AddQuickEditSchedule();
		}

		private void OpenSchedule()
		{
			using (var from = new FormOpenSchedule())
			{
				from.ShowDialog(this);
			}
		}

		private void OnFormMainShown(object sender, EventArgs e)
		{
			Utilities.ActivatePowerPoint(BusinessObjects.Instance.PowerPointManager.Processor.PowerPointObject);
			UpdateFormTitle();

			itemContainerStatusBarMainContentInfo.SubItems.Clear();
			var appInfoLabel = new LabelItem();
			appInfoLabel.Text = String.Format("{0}  v{1}",
				PopupMessageHelper.Instance.Title,
				FileStorageManager.Instance.Version
			);
			if (BusinessObjects.Instance.FormStyleManager.Style.StatusBarTextColor.HasValue)
				appInfoLabel.ForeColor = BusinessObjects.Instance.FormStyleManager.Style.StatusBarTextColor.Value;
			itemContainerStatusBarMainContentInfo.SubItems.Add(appInfoLabel);
			barBottom.RecalcLayout();

			AppManager.Instance.ActivateMainForm(WindowState == FormWindowState.Maximized);

			using (var formStart = new FormStart())
			{
				formStart.buttonXOpen.Enabled = BusinessObjects.Instance.ScheduleManager.GetScheduleList<MediaScheduleModel>().Any() ||
					!FileStorageManager.Instance.UseLocalMode;
				var result = formStart.ShowDialog(this);
				if (result == DialogResult.Yes)
					AddNewRegularSchedule();
				else if (result == DialogResult.No)
					OpenSchedule();
				else if (result == DialogResult.Abort)
					AddNewQuickEditSchedule();
				if (BusinessObjects.Instance.ScheduleManager.ActiveSchedule != null)
				{
					emptySpaceItem.Visibility = LayoutVisibility.Always;
					layoutControlItemDefaultLogo.Visibility = LayoutVisibility.Never;
					ribbonControl.Enabled = true;
					LoadData();
				}
				else
					Close();
			}
		}

		private void FormMainResize(object sender, EventArgs e)
		{
			var f = sender as Form;
			if (f.WindowState != FormWindowState.Minimized && f.Tag != FloaterManager.FloatedMarker)
				Opacity = 1;
		}

		private void OnFormMainClosing(object sender, FormClosingEventArgs e)
		{
			var savingArgs = new ContentSavingEventArgs { SavingReason = ContentSavingReason.AppClosing };
			ContentEditManager<MediaScheduleChangeInfo>.ProcessContentEditChanges(
				Controller.Instance.ContentController.ActiveEditor,
				savingArgs);
		}

		private void OnFormMainClosed(object sender, FormClosedEventArgs e)
		{
			BusinessObjects.Instance.PowerPointManager.Processor.Disconnect();
		}

		private void OnNewScheduleClick(object sender, EventArgs e)
		{
			var savingArgs = new ContentSavingEventArgs { SavingReason = ContentSavingReason.ScheduleChanging };
			ContentEditManager<MediaScheduleChangeInfo>.ProcessContentEditChanges(
				Controller.Instance.ContentController.ActiveEditor,
				savingArgs);
			if (!savingArgs.Cancel)
				AddNewRegularSchedule();
		}

		private void OnOpenScheduleClick(object sender, EventArgs e)
		{
			var savingArgs = new ContentSavingEventArgs { SavingReason = ContentSavingReason.ScheduleChanging };
			ContentEditManager<MediaScheduleChangeInfo>.ProcessContentEditChanges(
				Controller.Instance.ContentController.ActiveEditor,
				savingArgs);
			if (!savingArgs.Cancel)
				OpenSchedule();
		}

		private void OnExitClick(object sender, EventArgs e)
		{
			Close();
		}

		private void OnFloaterClick(object sender, EventArgs e)
		{
			var formSender = sender as Form;
			AppManager.Instance.ShowFloater(
				formSender ?? this,
				new FloaterRequestedEventArgs
				{
					Logo = BusinessObjects.Instance.ImageResourcesManager.MainAppRibbonLogo ?? Resources.RibbonLogo
				});
		}

		private void OnRibbonExpandedChanged(object sender, EventArgs e)
		{
			buttonItemExpand.Visible = !ribbonControl.Expanded;
			buttonItemCollapse.Visible = ribbonControl.Expanded;
			buttonItemPin.Visible = false;
			ribbonControl.RecalcLayout();
		}

		private void OnRibbonAfterPanelPopup(object sender, EventArgs e)
		{
			buttonItemExpand.Visible = false;
			buttonItemCollapse.Visible = false;
			buttonItemPin.Visible = true;
			ribbonControl.RecalcLayout();
		}

		private void OnRibbonAfterPanelPopupClose(object sender, EventArgs e)
		{
			buttonItemExpand.Visible = !ribbonControl.Expanded;
			buttonItemCollapse.Visible = ribbonControl.Expanded;
			buttonItemPin.Visible = false;
			ribbonControl.RecalcLayout();
		}

		private void OnRibbonExpandClick(object sender, EventArgs e)
		{
			ribbonControl.Expanded = true;
		}

		private void OnRibbonCollapseClick(object sender, EventArgs e)
		{
			ribbonControl.Expanded = false;
		}

		private void OnRibbonPinClick(object sender, EventArgs e)
		{
			ribbonControl.Expanded = true;
		}
	}
}