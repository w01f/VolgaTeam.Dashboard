using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using Asa.CommonGUI.Common;
using Asa.CommonGUI.Floater;
using Asa.CommonGUI.ToolForms;
using Asa.Core.Common;
using Asa.Core.MediaSchedule;
using Asa.MediaSchedule.Controls;
using Asa.MediaSchedule.Controls.BusinessClasses;
using Asa.MediaSchedule.Controls.InteropClasses;
using Asa.MediaSchedule.Controls.Properties;
using Asa.MediaSchedule.Controls.ToolForms;
using Asa.OnlineSchedule.Controls.InteropClasses;

namespace Asa.MediaSchedule.Single
{
	public partial class FormMain : RibbonForm
	{
		private static FormMain _instance;
		private Control _currentControl;
		public event EventHandler<FloaterRequestedEventArgs> FloaterRequested;

		private FormMain()
		{
			InitializeComponent();

			if ((base.CreateGraphics()).DpiX > 96)
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
				comboBoxEditClientType.Font = font;
				dateEditFlightDatesEnd.Font = font;
				dateEditFlightDatesStart.Font = font;
				dateEditPresentationDate.Font = font;
				ribbonBarHomeBasicInfo.RecalcLayout();
				ribbonBarHomeFlightDates.RecalcLayout();
				ribbonBarHomeExit.RecalcLayout();
				ribbonBarDigitalScheduleEmail.RecalcLayout();
				ribbonBarDigitalScheduleExit.RecalcLayout();
				ribbonBarDigitalSchedulePowerPoint.RecalcLayout();
				ribbonBarDigitalPackageEmail.RecalcLayout();
				ribbonBarDigitalPackageExit.RecalcLayout();
				ribbonBarDigitalPackagePowerPoint.RecalcLayout();
				ribbonPanelDigitalSlides.PerformLayout();
				ribbonPanelHome.PerformLayout();
				ribbonPanelDigitalPackage.PerformLayout();
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
			FormStateHelper.Init(this, Core.Common.ResourceManager.Instance.AppSettingsFolder, MediaMetaData.Instance.DataTypeString, false).LoadState();

			Controller.Instance.FormMain = this;
			Controller.Instance.Supertip = superTooltip;
			Controller.Instance.Ribbon = ribbonControl;
			Controller.Instance.TabHome = ribbonTabItemHome;
			Controller.Instance.TabProgramSchedule = ribbonTabItemProgramSchedule;
			Controller.Instance.TabDigitalProduct = ribbonTabItemDigitalSlides;
			Controller.Instance.TabDigitalPackage = ribbonTabItemDigitalPackage;
			Controller.Instance.TabCalendar1 = ribbonTabItemCalendar1;
			Controller.Instance.TabCalendar2 = ribbonTabItemCalendar2;
			Controller.Instance.TabSummary = ribbonTabItemSummary;
			Controller.Instance.TabGallery1 = ribbonTabItemGallery1;
			Controller.Instance.TabGallery2 = ribbonTabItemGallery2;
			Controller.Instance.TabRateCard = ribbonTabItemRateCard;
			Controller.Instance.TabOptions = ribbonTabItemOptions;
			Controller.Instance.TabSnapshot = ribbonTabItemSnapshot;

			#region Command Controls
			Controller.Instance.SlideSettingsButton = buttonItemSlideSettings;

			#region Home
			Controller.Instance.HomeSpecialButtons = ribbonBarHomeSpecialButtons;
			Controller.Instance.HomeBusinessName = comboBoxEditBusinessName;
			Controller.Instance.HomeDecisionMaker = comboBoxEditDecisionMaker;
			Controller.Instance.HomePresentationDate = dateEditPresentationDate;
			Controller.Instance.HomeFlightDatesStart = dateEditFlightDatesStart;
			Controller.Instance.HomeFlightDatesEnd = dateEditFlightDatesEnd;
			Controller.Instance.HomeWeeks = labelItemHomeFlightDatesWeeks;
			Controller.Instance.HomeSave = buttonItemHomeSave;
			Controller.Instance.HomeSaveAs = buttonItemHomeSaveAs;
			Controller.Instance.HomeHelp = buttonItemHomeHelp;
			Controller.Instance.HomeProductAdd = buttonItemHomeDigitalProductAdd;
			Controller.Instance.HomeProductClone = buttonItemHomeDigitalProductClone;
			Controller.Instance.HomeClientType = comboBoxEditClientType;
			Controller.Instance.HomeAccountNumberText = textEditAccountNumber;
			Controller.Instance.HomeAccountNumberCheck = checkBoxItemHomeAccountNumber;
			#endregion

			#region Program Schedule
			Controller.Instance.ProgramSchedulePanel = ribbonPanelProgramSchedule;
			Controller.Instance.ProgramScheduleThemeBar = ribbonBarProgramSchedulePowerPoint;
			Controller.Instance.ProgramScheduleSpecialButtons = ribbonBarProgramScheduleSpecialButtons;
			Controller.Instance.ProgramScheduleHelp = buttonItemProgramScheduleHelp;
			Controller.Instance.ProgramScheduleSave = buttonItemProgramScheduleSave;
			Controller.Instance.ProgramScheduleSaveAs = buttonItemProgramScheduleSaveAs;
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
			Controller.Instance.DigitalProductPanel = ribbonPanelDigitalSlides;
			Controller.Instance.DigitalProductThemeBar = ribbonBarDigitalSchedulePowerPoint;
			Controller.Instance.DigitalProductSpecialButtons = ribbonBarDigitalScheduleSpecialButtons;
			Controller.Instance.DigitalProductPreview = buttonItemDigitalSchedulePreview;
			Controller.Instance.DigitalProductPowerPoint = buttonItemDigitalSchedulePowerPoint;
			Controller.Instance.DigitalProductPdf = buttonItemDigitalSchedulePdf;
			Controller.Instance.DigitalProductEmail = buttonItemDigitalScheduleEmail;
			Controller.Instance.DigitalProductTheme = buttonItemDigitalScheduleTheme;
			Controller.Instance.DigitalProductSave = buttonItemDigitalScheduleSave;
			Controller.Instance.DigitalProductSaveAs = buttonItemDigitalScheduleSaveAs;
			Controller.Instance.DigitalProductHelp = buttonItemDigitalScheduleHelp;
			#endregion

			#region Digital Package
			Controller.Instance.DigitalPackagePanel = ribbonPanelDigitalPackage;
			Controller.Instance.DigitalPackageThemeBar = ribbonBarDigitalPackagePowerPoint;
			Controller.Instance.DigitalPackageSpecialButtons = ribbonBarDigitalPackageSpecialButtons;
			Controller.Instance.DigitalPackageHelp = buttonItemDigitalPackageHelp;
			Controller.Instance.DigitalPackageSave = buttonItemDigitalPackageSave;
			Controller.Instance.DigitalPackageSaveAs = buttonItemDigitalPackageSaveAs;
			Controller.Instance.DigitalPackagePreview = buttonItemDigitalPackagePreview;
			Controller.Instance.DigitalPackageEmail = buttonItemDigitalPackageEmail;
			Controller.Instance.DigitalPackagePowerPoint = buttonItemDigitalPackagePowerPoint;
			Controller.Instance.DigitalPackagePdf = buttonItemDigitalPackagePdf;
			Controller.Instance.DigitalPackageTheme = buttonItemDigitalPackageTheme;
			#endregion

			#region Calendar 1
			Controller.Instance.Calendar1SpecialButtons = ribbonBarCalendar1SpecialButtons;
			Controller.Instance.Calendar1MonthsList = listBoxControlCalendar1;
			Controller.Instance.Calendar1Copy = buttonItemCalendar1Copy;
			Controller.Instance.Calendar1Paste = buttonItemCalendar1Paste;
			Controller.Instance.Calendar1Clone = buttonItemCalendar1Clone;
			Controller.Instance.Calendar1Help = buttonItemCalendar1Help;
			Controller.Instance.Calendar1Save = buttonItemCalendar1Save;
			Controller.Instance.Calendar1SaveAs = buttonItemCalendar1SaveAs;
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
			Controller.Instance.Calendar2Help = buttonItemCalendar2Help;
			Controller.Instance.Calendar2Save = buttonItemCalendar2Save;
			Controller.Instance.Calendar2SaveAs = buttonItemCalendar2SaveAs;
			Controller.Instance.Calendar2Preview = buttonItemCalendar2Preview;
			Controller.Instance.Calendar2Email = buttonItemCalendar2Email;
			Controller.Instance.Calendar2PowerPoint = buttonItemCalendar2PowerPoint;
			Controller.Instance.Calendar2Pdf = buttonItemCalendar2Pdf;
			#endregion

			#region Summary Light
			Controller.Instance.SummaryPanel = ribbonPanelSummary;
			Controller.Instance.SummaryThemeBar = ribbonBarSummaryPowerPoint;
			Controller.Instance.SummarySpecialButtons = ribbonBarSummarySpecialButtons;
			Controller.Instance.SummaryHelp = buttonItemSummaryHelp;
			Controller.Instance.SummarySave = buttonItemSummarySave;
			Controller.Instance.SummarySaveAs = buttonItemSummarySaveAs;
			Controller.Instance.SummaryPreview = buttonItemSummaryPreview;
			Controller.Instance.SummaryEmail = buttonItemSummaryEmail;
			Controller.Instance.SummaryPowerPoint = buttonItemSummaryPowerPoint;
			Controller.Instance.SummaryPdf = buttonItemSummaryPdf;
			Controller.Instance.SummaryTheme = buttonItemSummaryTheme;
			#endregion

			#region Snapshot
			Controller.Instance.SnapshotPanel = ribbonPanelSnapshot;
			Controller.Instance.SnapshotThemeBar = ribbonBarSnapshotPowerPoint;
			Controller.Instance.SnapshotSpecialButtons = ribbonBarSnapshotSpecialButtons;
			Controller.Instance.SnapshotHelp = buttonItemSnapshotHelp;
			Controller.Instance.SnapshotSave = buttonItemSnapshotSave;
			Controller.Instance.SnapshotSaveAs = buttonItemSnapshotSaveAs;
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
			Controller.Instance.OptionsHelp = buttonItemOptionsHelp;
			Controller.Instance.OptionsSave = buttonItemOptionsSave;
			Controller.Instance.OptionsSaveAs = buttonItemOptionsSaveAs;
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
			Controller.Instance.Gallery1Help = buttonItemGallery1Help;
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
			Controller.Instance.Gallery2Help = buttonItemGallery2Help;
			Controller.Instance.Gallery2Sections = comboBoxEditGallery2Sections;
			Controller.Instance.Gallery2Groups = comboBoxEditGallery2Groups;
			#endregion

			#region Rate Card
			Controller.Instance.RateCardSpecialButtons = ribbonBarRateCardSpecialButtons;
			Controller.Instance.RateCardHelp = buttonItemRateCardHelp;
			Controller.Instance.RateCardCombo = comboBoxEditRateCards;
			#endregion

			#endregion

			Controller.Instance.InitForm();

			Controller.Instance.ScheduleChanged += (o, e) => UpdateFormTitle();
			Controller.Instance.FloaterRequested += (o, e) => AppManager.Instance.ShowFloater(this, e);
			PowerPointManager.Instance.SettingsChanged += (o, e) => UpdateFormTitle();
		}

		private void UpdateFormTitle()
		{
			if (MasterWizardManager.Instance.SelectedWizard == null) return;
			var shortSchedule = BusinessObjects.Instance.ScheduleManager.GetShortSchedule();
			Text = String.Format("{0} v{1} - {2} - {3} {4}",
				Utilities.Instance.Title,
				FileStorageManager.Instance.Version,
				MasterWizardManager.Instance.SelectedWizard.Name,
				PowerPointManager.Instance.SlideSettings.SizeFormatted,
				shortSchedule != null ? String.Format("({0})", shortSchedule.ShortFileName) : String.Empty
				);
		}

		private void LoadData()
		{
			UpdateFormTitle();
			ribbonControl.Enabled = false;
			FormProgress.SetTitle("Chill-Out for a few seconds...\nLoading Schedule...");
			FormProgress.ShowProgress();
			var thread = new Thread(() => Invoke((MethodInvoker)(() => Controller.Instance.LoadData())));
			thread.Start();
			while (thread.IsAlive)
				Application.DoEvents();
			FormProgress.CloseProgress();
			ribbonControl.SelectedRibbonTabChanged -= ribbonControl_SelectedRibbonTabChanged;
			ribbonControl.SelectedRibbonTabItem = ribbonTabItemHome;
			ribbonControl_SelectedRibbonTabChanged(null, null);
			ribbonControl.SelectedRibbonTabChanged += ribbonControl_SelectedRibbonTabChanged;
			ribbonControl.Enabled = true;
			Controller.Instance.CheckPowerPointRunning();
		}

		private bool AllowToLeaveCurrentControl()
		{
			bool result = false;
			if ((_currentControl == Controller.Instance.HomeControl))
			{
				if (Controller.Instance.HomeControl.AllowToLeaveControl())
					result = true;
				else
				{
					ribbonControl.SelectedRibbonTabChanged -= ribbonControl_SelectedRibbonTabChanged;
					ribbonControl.SelectedRibbonTabItem = ribbonTabItemHome;
					ribbonControl.SelectedRibbonTabChanged += ribbonControl_SelectedRibbonTabChanged;
				}
			}
			else if ((_currentControl == Controller.Instance.ProgramSchedule))
			{
				if (Controller.Instance.ProgramSchedule.AllowToLeaveControl)
					result = true;
				else
				{
					ribbonControl.SelectedRibbonTabChanged -= ribbonControl_SelectedRibbonTabChanged;
					ribbonControl.SelectedRibbonTabItem = ribbonTabItemProgramSchedule;
					ribbonControl.SelectedRibbonTabChanged += ribbonControl_SelectedRibbonTabChanged;
				}
			}
			else if ((_currentControl == Controller.Instance.DigitalProductContainer))
			{
				if (Controller.Instance.DigitalProductContainer.AllowToLeaveControl)
					result = true;
				else
				{
					ribbonControl.SelectedRibbonTabChanged -= ribbonControl_SelectedRibbonTabChanged;
					ribbonControl.SelectedRibbonTabItem = ribbonTabItemDigitalSlides;
					ribbonControl.SelectedRibbonTabChanged += ribbonControl_SelectedRibbonTabChanged;
				}
			}
			else if ((_currentControl == Controller.Instance.DigitalPackage))
			{
				if (Controller.Instance.DigitalPackage.AllowToLeaveControl)
					result = true;
				else
				{
					ribbonControl.SelectedRibbonTabChanged -= ribbonControl_SelectedRibbonTabChanged;
					ribbonControl.SelectedRibbonTabItem = ribbonTabItemDigitalPackage;
					ribbonControl.SelectedRibbonTabChanged += ribbonControl_SelectedRibbonTabChanged;
				}
			}
			else if ((_currentControl == Controller.Instance.BroadcastCalendar))
			{
				if (Controller.Instance.BroadcastCalendar.AllowToLeaveControl)
					result = true;
				else
				{
					ribbonControl.SelectedRibbonTabChanged -= ribbonControl_SelectedRibbonTabChanged;
					ribbonControl.SelectedRibbonTabItem = ribbonTabItemCalendar1;
					ribbonControl.SelectedRibbonTabChanged += ribbonControl_SelectedRibbonTabChanged;
				}
			}
			else if ((_currentControl == Controller.Instance.CustomCalendar))
			{
				if (Controller.Instance.CustomCalendar.AllowToLeaveControl)
					result = true;
				else
				{
					ribbonControl.SelectedRibbonTabChanged -= ribbonControl_SelectedRibbonTabChanged;
					ribbonControl.SelectedRibbonTabItem = ribbonTabItemCalendar2;
					ribbonControl.SelectedRibbonTabChanged += ribbonControl_SelectedRibbonTabChanged;
				}
			}
			else if ((_currentControl == Controller.Instance.Summary))
			{
				if (Controller.Instance.Summary.AllowToLeaveControl)
					result = true;
				else
				{
					ribbonControl.SelectedRibbonTabChanged -= ribbonControl_SelectedRibbonTabChanged;
					ribbonControl.SelectedRibbonTabItem = ribbonTabItemSummary;
					ribbonControl.SelectedRibbonTabChanged += ribbonControl_SelectedRibbonTabChanged;
				}
			}
			else if ((_currentControl == Controller.Instance.Snapshot))
			{
				if (Controller.Instance.Snapshot.AllowToLeaveControl)
					result = true;
				else
				{
					ribbonControl.SelectedRibbonTabChanged -= ribbonControl_SelectedRibbonTabChanged;
					ribbonControl.SelectedRibbonTabItem = ribbonTabItemSnapshot;
					ribbonControl.SelectedRibbonTabChanged += ribbonControl_SelectedRibbonTabChanged;
				}
			}
			else if ((_currentControl == Controller.Instance.Options))
			{
				if (Controller.Instance.Options.AllowToLeaveControl)
					result = true;
				else
				{
					ribbonControl.SelectedRibbonTabChanged -= ribbonControl_SelectedRibbonTabChanged;
					ribbonControl.SelectedRibbonTabItem = ribbonTabItemOptions;
					ribbonControl.SelectedRibbonTabChanged += ribbonControl_SelectedRibbonTabChanged;
				}
			}
			else
				result = true;
			return result;
		}

		private void FormMain_Shown(object sender, EventArgs e)
		{
			Utilities.Instance.ActivatePowerPoint(RegularMediaSchedulePowerPointHelper.Instance.PowerPointObject);
			UpdateFormTitle();
			AppManager.Instance.ActivateMainForm();

			using (var formStart = new FormStart())
			{
				formStart.buttonXOpen.Enabled = ScheduleManager.GetShortScheduleList().Length > 0;
				var result = formStart.ShowDialog();
				if (result == DialogResult.Yes || result == DialogResult.No)
				{
					if (result == DialogResult.Yes)
						buttonItemHomeNewSchedule_Click(null, null);
					else
						buttonItemHomeOpenSchedule_Click(null, null);
				}
				else
					Application.Exit();
			}
		}

		private void FormMain_Resize(object sender, EventArgs e)
		{
			var f = sender as Form;
			if (f.WindowState != FormWindowState.Minimized && f.Tag != FloaterManager.FloatedMarker)
				Opacity = 1;
		}

		public void ribbonControl_SelectedRibbonTabChanged(object sender, EventArgs e)
		{
			if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemHome)
			{
				if (AllowToLeaveCurrentControl())
				{
					_currentControl = Controller.Instance.HomeControl;
					if (!pnMain.Controls.Contains(_currentControl))
						pnMain.Controls.Add(_currentControl);
				}
				_currentControl.BringToFront();
				pnMain.BringToFront();
			}
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemProgramSchedule)
			{
				if (AllowToLeaveCurrentControl())
				{
					_currentControl = Controller.Instance.ProgramSchedule;
					if (!pnMain.Controls.Contains(_currentControl))
						pnMain.Controls.Add(_currentControl);
				}
				_currentControl.BringToFront();
				pnMain.BringToFront();
			}
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemDigitalSlides)
			{
				if (AllowToLeaveCurrentControl())
				{
					_currentControl = Controller.Instance.DigitalProductContainer;
					if (!pnMain.Controls.Contains(_currentControl))
						pnMain.Controls.Add(_currentControl);
				}
				_currentControl.BringToFront();
				pnMain.BringToFront();
			}
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemDigitalPackage)
			{
				if (AllowToLeaveCurrentControl())
				{
					_currentControl = Controller.Instance.DigitalPackage;
					if (!pnMain.Controls.Contains(_currentControl))
						pnMain.Controls.Add(_currentControl);
				}
				_currentControl.BringToFront();
				pnMain.BringToFront();
			}
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemCalendar1)
			{
				if (AllowToLeaveCurrentControl())
				{
					if (!Controller.Instance.BroadcastCalendar.CalendarUpdated)
					{
						Controller.Instance.BroadcastCalendar.ShowCalendar(false);
						Controller.Instance.BroadcastCalendar.CalendarUpdated = true;
					}
					_currentControl = Controller.Instance.BroadcastCalendar;
					if (!pnMain.Controls.Contains(_currentControl))
						pnMain.Controls.Add(_currentControl);
				}
				_currentControl.BringToFront();
				pnMain.BringToFront();
			}
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemCalendar2)
			{
				if (AllowToLeaveCurrentControl())
				{
					if (!Controller.Instance.CustomCalendar.CalendarUpdated)
					{
						Controller.Instance.CustomCalendar.ShowCalendar(false);
						Controller.Instance.CustomCalendar.CalendarUpdated = true;
					}
					_currentControl = Controller.Instance.CustomCalendar;
					if (!pnMain.Controls.Contains(_currentControl))
						pnMain.Controls.Add(_currentControl);
				}
				_currentControl.BringToFront();
				pnMain.BringToFront();
			}
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemSummary)
			{
				if (AllowToLeaveCurrentControl())
				{
					_currentControl = Controller.Instance.Summary;
					if (!pnMain.Controls.Contains(_currentControl))
						pnMain.Controls.Add(_currentControl);
				}
				_currentControl.BringToFront();
				pnMain.BringToFront();
			}
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemSnapshot)
			{
				if (AllowToLeaveCurrentControl())
				{
					_currentControl = Controller.Instance.Snapshot;
					if (!pnMain.Controls.Contains(_currentControl))
						pnMain.Controls.Add(_currentControl);
				}
				_currentControl.BringToFront();
				pnMain.BringToFront();
			}
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemOptions)
			{
				if (AllowToLeaveCurrentControl())
				{
					_currentControl = Controller.Instance.Options;
					if (!pnMain.Controls.Contains(_currentControl))
						pnMain.Controls.Add(_currentControl);
				}
				_currentControl.BringToFront();
				pnMain.BringToFront();
			}
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemGallery1)
			{
				if (AllowToLeaveCurrentControl())
				{
					_currentControl = Controller.Instance.Gallery1;
					if (!pnMain.Controls.Contains(_currentControl))
						pnMain.Controls.Add(_currentControl);
				}
				_currentControl.BringToFront();
				pnMain.BringToFront();
			}
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemGallery2)
			{
				if (AllowToLeaveCurrentControl())
				{
					_currentControl = Controller.Instance.Gallery2;
					if (!pnMain.Controls.Contains(_currentControl))
						pnMain.Controls.Add(_currentControl);
				}
				_currentControl.BringToFront();
				pnMain.BringToFront();
			}
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemRateCard)
			{
				if (AllowToLeaveCurrentControl())
				{
					_currentControl = Controller.Instance.RateCard;
					if (!pnMain.Controls.Contains(_currentControl))
						pnMain.Controls.Add(_currentControl);
				}
				_currentControl.BringToFront();
				pnMain.BringToFront();
			}
			else
			{
				pnEmpty.Visible = true;
				_currentControl = null;
				pnEmpty.BringToFront();
			}
			if (WindowState == FormWindowState.Normal)
			{
				Width++;
				Width--;
			}
		}

		private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
		{
			bool result = true;
			if (_currentControl == Controller.Instance.HomeControl)
				result = Controller.Instance.HomeControl.AllowToLeaveControl();
			else if (_currentControl == Controller.Instance.ProgramSchedule)
				result = Controller.Instance.ProgramSchedule.AllowToLeaveControl;
			else if (_currentControl == Controller.Instance.DigitalProductContainer)
				result = Controller.Instance.DigitalProductContainer.AllowToLeaveControl;
			else if (_currentControl == Controller.Instance.DigitalPackage)
				result = Controller.Instance.DigitalPackage.AllowToLeaveControl;
			else if (_currentControl == Controller.Instance.BroadcastCalendar)
				result = Controller.Instance.BroadcastCalendar.AllowToLeaveControl;
			else if (_currentControl == Controller.Instance.CustomCalendar)
				result = Controller.Instance.CustomCalendar.AllowToLeaveControl;
			else if (_currentControl == Controller.Instance.Summary)
				result = Controller.Instance.Summary.AllowToLeaveControl;
			else if (_currentControl == Controller.Instance.Snapshot)
				result = Controller.Instance.Snapshot.AllowToLeaveControl;
			else if (_currentControl == Controller.Instance.Options)
				result = Controller.Instance.Options.AllowToLeaveControl;
			RegularMediaSchedulePowerPointHelper.Instance.Disconnect(false);
			OnlineSchedulePowerPointHelper.Instance.Disconnect(false);
		}

		private void buttonItemHomeNewSchedule_Click(object sender, EventArgs e)
		{
			using (var form = new FormNewSchedule(ScheduleManager.GetShortScheduleList().Select(s => s.ShortFileName)))
			{
				if (form.ShowDialog() == DialogResult.OK)
				{
					if (!string.IsNullOrEmpty(form.ScheduleName))
					{
						var fileName = BusinessObjects.Instance.ScheduleManager.GetScheduleFileName(form.ScheduleName.Trim());
						BusinessObjects.Instance.ActivityManager.AddActivity(new ScheduleActivity("New Created", form.ScheduleName.Trim()));
						BusinessObjects.Instance.ScheduleManager.OpenSchedule(fileName);
						LoadData();
					}
					else
					{
						Utilities.Instance.ShowWarning("Schedule Name can't be empty");
					}
				}
				else if (!BusinessObjects.Instance.ScheduleManager.ScheduleLoaded)
					Close();
			}
		}

		private void buttonItemHomeOpenSchedule_Click(object sender, EventArgs e)
		{
			using (var from = new FormOpenSchedule())
			{
				if (from.ShowDialog() == DialogResult.OK)
				{
					if (!string.IsNullOrEmpty(from.ScheduleName))
					{
						var fileName = from.ScheduleName.Trim();
						BusinessObjects.Instance.ActivityManager.AddActivity(new ScheduleActivity("Previous Opened", Path.GetFileNameWithoutExtension(fileName)));
						BusinessObjects.Instance.ScheduleManager.OpenSchedule(fileName, false);
						LoadData();
					}
					else
					{
						Utilities.Instance.ShowWarning("Schedule Name can't be empty");
					}
				}
				else if (!BusinessObjects.Instance.ScheduleManager.ScheduleLoaded)
					Close();
			}
		}

		private void buttonItemHomeExit_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void buttonItemFloater_Click(object sender, EventArgs e)
		{
			var formSender = sender as Form;
			AppManager.Instance.ShowFloater(formSender ?? this, new FloaterRequestedEventArgs { Logo = MediaMetaData.Instance.DataType == MediaDataType.TVSchedule ? Resources.TVRibbonLogo : Resources.RadioRibbonLogo });
		}
	}
}