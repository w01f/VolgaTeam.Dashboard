using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Media.Configuration;
using Asa.Business.Media.Entities.NonPersistent.Schedule;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;
using Asa.Common.GUI.ContentEditors.Enums;
using Asa.Common.GUI.ContentEditors.Events;
using Asa.Common.GUI.ContentEditors.Helpers;
using Asa.Common.GUI.Floater;
using Asa.Common.GUI.ToolForms;
using Asa.Media.Controls;
using Asa.Media.Controls.BusinessClasses.Managers;
using Asa.Media.Controls.InteropClasses;
using Asa.Media.Controls.Properties;
using Asa.Media.Controls.ToolForms;
using DevComponents.DotNetBar;
using FormStart = Asa.Media.Controls.ToolForms.FormStart;

namespace Asa.Media.Single
{
	public partial class FormMain : RibbonForm
	{
		private static FormMain _instance;

		private FormMain()
		{
			InitializeComponent();

			if ((CreateGraphics()).DpiX > 96)
			{
				var font = new Font(styleController.Appearance.Font.FontFamily, styleController.Appearance.Font.Size - 1, styleController.Appearance.Font.Style);
				ribbonControl.Font = font;
				styleController.Appearance.Font = font;
				styleController.AppearanceDisabled.Font = font;
				styleController.AppearanceDropDown.Font = font;
				styleController.AppearanceDropDownHeader.Font = font;
				styleController.AppearanceFocused.Font = font;
				styleController.AppearanceReadOnly.Font = font;
				comboBoxEditBusinessName.Font = font;
				comboBoxEditDecisionMaker.Font = font;
				dateEditPresentationDate.Font = font;

				labelItemHomeFlightDatesStartTitle.Font = new Font(labelItemHomeFlightDatesStartTitle.Font.FontFamily,
					labelItemHomeFlightDatesStartTitle.Font.Size - 1, labelItemHomeFlightDatesStartTitle.Font.Style);
				labelItemHomeFlightDatesEndTitle.Font = new Font(labelItemHomeFlightDatesEndTitle.Font.FontFamily,
					labelItemHomeFlightDatesEndTitle.Font.Size - 1, labelItemHomeFlightDatesEndTitle.Font.Style);
				labelItemHomeFlightDatesStartValue.Font = new Font(labelItemHomeFlightDatesStartValue.Font.FontFamily,
					labelItemHomeFlightDatesStartValue.Font.Size - 2, labelItemHomeFlightDatesStartValue.Font.Style);
				labelItemHomeFlightDatesEndValue.Font = new Font(labelItemHomeFlightDatesEndValue.Font.FontFamily,
					labelItemHomeFlightDatesEndValue.Font.Size - 2, labelItemHomeFlightDatesEndValue.Font.Style);

				ribbonBarHomeBasicInfo.RecalcLayout();
				ribbonBarHomeFlightDates.RecalcLayout();
				ribbonBarHomeExit.RecalcLayout();
				ribbonBarDigitalScheduleExit.RecalcLayout();
				ribbonBarDigitalSchedulePowerPoint.RecalcLayout();
				ribbonPanelDigitalSchedule.PerformLayout();
				ribbonPanelHome.PerformLayout();
			}
		}

		public static FormMain Instance
		{
			get
			{
				if (_instance == null)
					_instance = new FormMain();
				return _instance;
			}
		}

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
					true)
				.LoadState();

			Controller.Instance.FormMain = this;
			Controller.Instance.MainPanel = pnMain;
			Controller.Instance.EmptyPanel = pnEmpty;
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

			FormProgress.Init(this);

			#region Command Controls
			Controller.Instance.QatSaveButton = buttonItemQatSave;
			Controller.Instance.QatSaveAsButton = buttonItemQatSaveAs;
			Controller.Instance.QatHelpButton = buttonItemQatHelp;

			Controller.Instance.SlideSettingsButton = buttonItemSlideSettings;
			
			#region Home
			Controller.Instance.HomePanel = ribbonPanelHome;
			Controller.Instance.HomeSpecialButtons = ribbonBarHomeSpecialButtons;
			Controller.Instance.HomeBusinessName = comboBoxEditBusinessName;
			Controller.Instance.HomeDecisionMaker = comboBoxEditDecisionMaker;
			Controller.Instance.HomePresentationDate = dateEditPresentationDate;
			Controller.Instance.HomeFlightDates = ribbonBarHomeFlightDates;
			Controller.Instance.HomeFlightDatesStart = labelItemHomeFlightDatesStartValue;
			Controller.Instance.HomeFlightDatesEnd = labelItemHomeFlightDatesEndValue;
			#endregion

			#region Program Schedule
			Controller.Instance.ProgramSchedulePanel = ribbonPanelProgramSchedule;
			Controller.Instance.ProgramScheduleThemeBar = ribbonBarProgramSchedulePowerPoint;
			Controller.Instance.ProgramScheduleSpecialButtons = ribbonBarProgramScheduleSpecialButtons;
			Controller.Instance.ProgramSchedulePreview = buttonItemProgramSchedulePreview;
			Controller.Instance.ProgramScheduleEmail = buttonItemProgramScheduleEmail;
			Controller.Instance.ProgramSchedulePowerPoint = buttonItemProgramSchedulePowerPoint;
			Controller.Instance.ProgramSchedulePdf = buttonItemProgramSchedulePdf;
			Controller.Instance.ProgramScheduleTheme = buttonItemProgramScheduleTheme;
			Controller.Instance.ProgramScheduleNew = buttonItemProgramScheduleNew;
			Controller.Instance.ProgramScheduleProgramAdd = buttonItemProgramScheduleProgramAdd;
			Controller.Instance.ProgramScheduleProgramDelete = buttonItemProgramScheduleProgramDelete;
			#endregion

			#region Digital Product
			Controller.Instance.DigitalProductPanel = ribbonPanelDigitalSchedule;
			Controller.Instance.DigitalProductThemeBar = ribbonBarDigitalSchedulePowerPoint;
			Controller.Instance.DigitalProductSpecialButtons = ribbonBarDigitalScheduleSpecialButtons;
			Controller.Instance.DigitalProductPreview = buttonItemDigitalSchedulePreview;
			Controller.Instance.DigitalProductPowerPoint = buttonItemDigitalSchedulePowerPoint;
			Controller.Instance.DigitalProductPdf = buttonItemDigitalSchedulePdf;
			Controller.Instance.DigitalProductEmail = buttonItemDigitalScheduleEmail;
			Controller.Instance.DigitalProductTheme = buttonItemDigitalScheduleTheme;
			Controller.Instance.DigitalProductAdd = buttonItemDigitalScheduleProductAdd;
			Controller.Instance.DigitalProductClone = buttonItemDigitalScheduleProductClone;
			Controller.Instance.DigitalProductDelete = buttonItemDigitalScheduleProductDelete;
			#endregion

			#region Calendar 1
			Controller.Instance.Calendar1SpecialButtons = ribbonBarCalendar1SpecialButtons;
			Controller.Instance.Calendar1MonthsList = listBoxControlCalendar1;
			Controller.Instance.Calendar1Copy = buttonItemCalendar1Copy;
			Controller.Instance.Calendar1Paste = buttonItemCalendar1Paste;
			Controller.Instance.Calendar1Clone = buttonItemCalendar1Clone;
			Controller.Instance.Calendar1Preview = buttonItemCalendar1Preview;
			Controller.Instance.Calendar1Email = buttonItemCalendar1Email;
			Controller.Instance.Calendar1PowerPoint = buttonItemCalendar1PowerPoint;
			Controller.Instance.Calendar1Pdf = buttonItemCalendar1Pdf;
			#endregion

			#region Calendar 2
			Controller.Instance.Calendar2SpecialButtons = ribbonBarCalendar2SpecialButtons;
			Controller.Instance.Calendar2MonthsList = listBoxControlCalendar2;
			Controller.Instance.Calendar2Copy = buttonItemCalendar2Copy;
			Controller.Instance.Calendar2Paste = buttonItemCalendar2Paste;
			Controller.Instance.Calendar2Clone = buttonItemCalendar2Clone;
			Controller.Instance.Calendar2Preview = buttonItemCalendar2Preview;
			Controller.Instance.Calendar2Email = buttonItemCalendar2Email;
			Controller.Instance.Calendar2PowerPoint = buttonItemCalendar2PowerPoint;
			Controller.Instance.Calendar2Pdf = buttonItemCalendar2Pdf;
			#endregion

			#region Snapshot
			Controller.Instance.SnapshotPanel = ribbonPanelSnapshot;
			Controller.Instance.SnapshotThemeBar = ribbonBarSnapshotPowerPoint;
			Controller.Instance.SnapshotSpecialButtons = ribbonBarSnapshotSpecialButtons;
			Controller.Instance.SnapshotPreview = buttonItemSnapshotPreview;
			Controller.Instance.SnapshotEmail = buttonItemSnapshotEmail;
			Controller.Instance.SnapshotPowerPoint = buttonItemSnapshotPowerPoint;
			Controller.Instance.SnapshotPdf = buttonItemSnapshotPdf;
			Controller.Instance.SnapshotTheme = buttonItemSnapshotTheme;
			Controller.Instance.SnapshotNew = buttonItemSnapshotNew;
			Controller.Instance.SnapshotProgramAdd = buttonItemSnapshotProgramAdd;
			Controller.Instance.SnapshotProgramDelete = buttonItemSnapshotProgramDelete;
			#endregion

			#region Options
			Controller.Instance.OptionsPanel = ribbonPanelOptions;
			Controller.Instance.OptionsThemeBar = ribbonBarOptionsPowerPoint;
			Controller.Instance.OptionsSpecialButtons = ribbonBarOptionsSpecialButtons;
			Controller.Instance.OptionsPreview = buttonItemOptionsPreview;
			Controller.Instance.OptionsEmail = buttonItemOptionsEmail;
			Controller.Instance.OptionsPowerPoint = buttonItemOptionsPowerPoint;
			Controller.Instance.OptionsPdf = buttonItemOptionsPdf;
			Controller.Instance.OptionsTheme = buttonItemOptionsTheme;
			Controller.Instance.OptionsNew = buttonItemOptionsNew;
			Controller.Instance.OptionsProgramAdd = buttonItemOptionsProgramAdd;
			Controller.Instance.OptionsProgramDelete = buttonItemOptionsProgramDelete;
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
			Controller.Instance.RateCardSpecialButtons = ribbonBarRateCardSpecialButtons;
			Controller.Instance.RateCardCombo = comboBoxEditRateCards;
			#endregion

			#endregion

			Controller.Instance.InitForm();

			BusinessObjects.Instance.ScheduleManager.ScheduleOpened += (o, e) => UpdateFormTitle();
			BusinessObjects.Instance.ScheduleManager.ScheduleNameChanged += (o, e) => UpdateFormTitle();
			Controller.Instance.FloaterRequested += (o, e) => AppManager.Instance.ShowFloater(this, e);
			PowerPointManager.Instance.SettingsChanged += (o, e) => UpdateFormTitle();
		}

		private void UpdateFormTitle()
		{
			if (MasterWizardManager.Instance.SelectedWizard == null) return;
			var schedule = BusinessObjects.Instance.ScheduleManager.ActiveSchedule;
			Text = String.Format("{0} v{1} - {2} - {3} {4}",
				PopupMessageHelper.Instance.Title,
				FileStorageManager.Instance.Version,
				MasterWizardManager.Instance.SelectedWizard.Name,
				PowerPointManager.Instance.SlideSettings.SizeFormatted,
				schedule != null ? String.Format("({0})", schedule.Name) : String.Empty
				);
		}

		private void LoadData()
		{
			UpdateFormTitle();
			ribbonControl.SelectedRibbonTabItem = ribbonTabItemHome;
			ContentRibbonManager<MediaScheduleChangeInfo>.RaiseTabChanged();
			Controller.Instance.CheckPowerPointRunning();
		}

		private void AddNewSchedule()
		{
			using (var form = new FormScheduleName())
			{
				if (form.ShowDialog(this) != DialogResult.OK) return;
				BusinessObjects.Instance.ScheduleManager.AddSchedule(form.ScheduleName);
			}
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
			Utilities.ActivatePowerPoint(RegularMediaSchedulePowerPointHelper.Instance.PowerPointObject);
			UpdateFormTitle();
			AppManager.Instance.ActivateMainForm(WindowState == FormWindowState.Maximized);

			using (var formStart = new FormStart())
			{
				formStart.buttonXOpen.Enabled = BusinessObjects.Instance.ScheduleManager.GetScheduleList<MediaScheduleModel>().Any() || 
					!FileStorageManager.Instance.UseLocalMode;
				var result = formStart.ShowDialog(this);
				if (result == DialogResult.Yes || result == DialogResult.No)
				{
					if (result == DialogResult.Yes)
						AddNewSchedule();
					else
						OpenSchedule();
				}
				if (BusinessObjects.Instance.ScheduleManager.ActiveSchedule != null)
					LoadData();
				else
					Close();
			}
		}

		private void FormMain_Resize(object sender, EventArgs e)
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

		private void OnNewScheduleClick(object sender, EventArgs e)
		{
			var savingArgs = new ContentSavingEventArgs { SavingReason = ContentSavingReason.ScheduleChanging };
			ContentEditManager<MediaScheduleChangeInfo>.ProcessContentEditChanges(
				Controller.Instance.ContentController.ActiveEditor,
				savingArgs);
			if (!savingArgs.Cancel)
				AddNewSchedule();
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
					Logo = Resources.RibbonLogo
				});
		}
	}
}