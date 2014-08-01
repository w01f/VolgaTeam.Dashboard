using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using NewBizWiz.CommonGUI.Common;
using NewBizWiz.CommonGUI.Floater;
using NewBizWiz.CommonGUI.ToolForms;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.MediaSchedule;
using NewBizWiz.MediaSchedule.Controls;
using NewBizWiz.MediaSchedule.Controls.BusinessClasses;
using NewBizWiz.MediaSchedule.Controls.InteropClasses;
using NewBizWiz.OnlineSchedule.Controls.InteropClasses;

namespace NewBizWiz.MediaSchedule.Single
{
	public partial class FormMain : RibbonForm
	{
		private static FormMain _instance;
		private Control _currentControl;
		public event EventHandler<FloaterRequestedEventArgs> FloaterRequested;
		
		private FormMain()
		{
			InitializeComponent();

			FormStateHelper.Init(this, Path.GetDirectoryName(typeof(FormMain).Assembly.Location), true);

			Controller.Instance.FormMain = this;
			Controller.Instance.Supertip = superTooltip;
			Controller.Instance.Ribbon = ribbonControl;
			Controller.Instance.TabHome = ribbonTabItemHome;
			Controller.Instance.TabWeeklySchedule = ribbonTabItemWeeklySchedule;
			Controller.Instance.TabMonthlySchedule = ribbonTabItemMonthlySchedule;
			Controller.Instance.TabDigitalProduct = ribbonTabItemDigitalSlides;
			Controller.Instance.TabDigitalPackage = ribbonTabItemDigitalPackage;
			Controller.Instance.TabCalendar1 = ribbonTabItemCalendar1;
			Controller.Instance.TabCalendar2 = ribbonTabItemCalendar2;
			Controller.Instance.TabSummaryLight = ribbonTabItemSummaryLight;
			Controller.Instance.TabSummaryFull = ribbonTabItemSummaryFull;
			Controller.Instance.TabStrategy = ribbonTabItemStrategy;
			Controller.Instance.TabGallery1 = ribbonTabItemGallery1;
			Controller.Instance.TabGallery2 = ribbonTabItemGallery2;
			Controller.Instance.TabRateCard = ribbonTabItemRateCard;
			Controller.Instance.TabMarketing = ribbonTabItemMarketing;
			Controller.Instance.TabProduction = ribbonTabItemProduction;

			#region Command Controls

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

			#region Weekly Schedule
			Controller.Instance.WeeklyScheduleSpecialButtons = ribbonBarWeeklyScheduleSpecialButtons;
			Controller.Instance.WeeklyScheduleHelp = buttonItemWeeklyScheduleHelp;
			Controller.Instance.WeeklyScheduleSave = buttonItemWeeklyScheduleSave;
			Controller.Instance.WeeklyScheduleSaveAs = buttonItemWeeklyScheduleSaveAs;
			Controller.Instance.WeeklySchedulePreview = buttonItemWeeklySchedulePreview;
			Controller.Instance.WeeklyScheduleEmail = buttonItemWeeklyScheduleEmail;
			Controller.Instance.WeeklySchedulePowerPoint = buttonItemWeeklySchedulePowerPoint;
			Controller.Instance.WeeklyScheduleTheme = buttonItemWeeklyScheduleTheme;
			Controller.Instance.WeeklyScheduleOptions = buttonItemWeeklyScheduleSettings;
			Controller.Instance.WeeklyScheduleProgramAdd = buttonItemWeeklyScheduleProgramAdd;
			Controller.Instance.WeeklyScheduleProgramDelete = buttonItemWeeklyScheduleProgramDelete;
			Controller.Instance.WeeklyScheduleQuarterBar = ribbonBarWeeklyScheduleQuarter;
			Controller.Instance.WeeklyScheduleQuarterButton = buttonItemWeeklyScheduleQuarter;
			#endregion

			#region Monthly Schedule
			Controller.Instance.MonthlyScheduleSpecialButtons = ribbonBarMonthlyScheduleSpecialButtons;
			Controller.Instance.MonthlyScheduleHelp = buttonItemMonthlyScheduleHelp;
			Controller.Instance.MonthlyScheduleSave = buttonItemMonthlyScheduleSave;
			Controller.Instance.MonthlyScheduleSaveAs = buttonItemMonthlyScheduleSaveAs;
			Controller.Instance.MonthlySchedulePreview = buttonItemMonthlySchedulePreview;
			Controller.Instance.MonthlyScheduleEmail = buttonItemMonthlyScheduleEmail;
			Controller.Instance.MonthlySchedulePowerPoint = buttonItemMonthlySchedulePowerPoint;
			Controller.Instance.MonthlyScheduleTheme = buttonItemMonthlyScheduleTheme;
			Controller.Instance.MonthlyScheduleOptions = buttonItemMonthlyScheduleSettings;
			Controller.Instance.MonthlyScheduleProgramAdd = buttonItemMonthlyScheduleProgramAdd;
			Controller.Instance.MonthlyScheduleProgramDelete = buttonItemMonthlyScheduleProgramDelete;
			Controller.Instance.MonthlyScheduleQuarterBar = ribbonBarWeeklyScheduleQuarter;
			Controller.Instance.MonthlyScheduleQuarterButton = buttonItemMonthlyScheduleQuarter;
			#endregion

			#region Digital Product
			Controller.Instance.DigitalProductSpecialButtons = ribbonBarDigitalScheduleSpecialButtons;
			Controller.Instance.DigitalProductPreview = buttonItemDigitalSchedulePreview;
			Controller.Instance.DigitalProductPowerPoint = buttonItemDigitalSchedulePowerPoint;
			Controller.Instance.DigitalProductEmail = buttonItemDigitalScheduleEmail;
			Controller.Instance.DigitalProductTheme = buttonItemDigitalScheduleTheme;
			Controller.Instance.DigitalProductSave = buttonItemDigitalScheduleSave;
			Controller.Instance.DigitalProductSaveAs = buttonItemDigitalScheduleSaveAs;
			Controller.Instance.DigitalProductHelp = buttonItemDigitalScheduleHelp;
			#endregion

			#region Digital Package
			Controller.Instance.DigitalPackageSpecialButtons = ribbonBarDigitalPackageSpecialButtons;
			Controller.Instance.DigitalPackageHelp = buttonItemDigitalPackageHelp;
			Controller.Instance.DigitalPackageSave = buttonItemDigitalPackageSave;
			Controller.Instance.DigitalPackageSaveAs = buttonItemDigitalPackageSaveAs;
			Controller.Instance.DigitalPackagePreview = buttonItemDigitalPackagePreview;
			Controller.Instance.DigitalPackageEmail = buttonItemDigitalPackageEmail;
			Controller.Instance.DigitalPackagePowerPoint = buttonItemDigitalPackagePowerPoint;
			Controller.Instance.DigitalPackageTheme = buttonItemDigitalPackageTheme;
			Controller.Instance.DigitalPackageOptions = buttonItemDigitalPackageSettings;
			#endregion

			#region Calendar 1
			Controller.Instance.Calendar1SpecialButtons = ribbonBarCalendar1SpecialButtons;
			Controller.Instance.Calendar1MonthsList = listBoxControlCalendar1;
			Controller.Instance.Calendar1SlideInfo = buttonItemCalendar1SlideInfo;
			Controller.Instance.Calendar1Copy = buttonItemCalendar1Copy;
			Controller.Instance.Calendar1Paste = buttonItemCalendar1Paste;
			Controller.Instance.Calendar1Clone = buttonItemCalendar1Clone;
			Controller.Instance.Calendar1Help = buttonItemCalendar1Help;
			Controller.Instance.Calendar1Save = buttonItemCalendar1Save;
			Controller.Instance.Calendar1SaveAs = buttonItemCalendar1SaveAs;
			Controller.Instance.Calendar1Preview = buttonItemCalendar1Preview;
			Controller.Instance.Calendar1Email = buttonItemCalendar1Email;
			Controller.Instance.Calendar1PowerPoint = buttonItemCalendar1PowerPoint;
			#endregion

			#region Calendar 2
			Controller.Instance.Calendar2SpecialButtons = ribbonBarCalendar2SpecialButtons;
			Controller.Instance.Calendar2MonthsList = listBoxControlCalendar2;
			Controller.Instance.Calendar2SlideInfo = buttonItemCalendar2SlideInfo;
			Controller.Instance.Calendar2Copy = buttonItemCalendar2Copy;
			Controller.Instance.Calendar2Paste = buttonItemCalendar2Paste;
			Controller.Instance.Calendar2Clone = buttonItemCalendar2Clone;
			Controller.Instance.Calendar2Help = buttonItemCalendar2Help;
			Controller.Instance.Calendar2Save = buttonItemCalendar2Save;
			Controller.Instance.Calendar2SaveAs = buttonItemCalendar2SaveAs;
			Controller.Instance.Calendar2Preview = buttonItemCalendar2Preview;
			Controller.Instance.Calendar2Email = buttonItemCalendar2Email;
			Controller.Instance.Calendar2PowerPoint = buttonItemCalendar2PowerPoint;
			#endregion

			#region Summary Light
			Controller.Instance.SummaryLightSpecialButtons = ribbonBarSummaryLightSpecialButtons;
			Controller.Instance.SummaryLightHelp = buttonItemSummaryLightHelp;
			Controller.Instance.SummaryLightSave = buttonItemSummaryLightSave;
			Controller.Instance.SummaryLightSaveAs = buttonItemSummaryLightSaveAs;
			Controller.Instance.SummaryLightPreview = buttonItemSummaryLightPreview;
			Controller.Instance.SummaryLightEmail = buttonItemSummaryLightEmail;
			Controller.Instance.SummaryLightPowerPoint = buttonItemSummaryLightPowerPoint;
			Controller.Instance.SummaryLightTheme = buttonItemSummaryLightTheme;
			Controller.Instance.SummaryLightSlideOutputToggle = checkEditSummaryLightOutputSlide;
			Controller.Instance.SummaryLightTableOutputToggle = checkEditSummaryLightOutputTable;
			#endregion

			#region Summary Full
			Controller.Instance.SummaryFullSpecialButtons = ribbonBarSummaryFullSpecialButtons;
			Controller.Instance.SummaryFullHelp = buttonItemSummaryFullHelp;
			Controller.Instance.SummaryFullSave = buttonItemSummaryFullSave;
			Controller.Instance.SummaryFullSaveAs = buttonItemSummaryFullSaveAs;
			Controller.Instance.SummaryFullPreview = buttonItemSummaryFullPreview;
			Controller.Instance.SummaryFullEmail = buttonItemSummaryFullEmail;
			Controller.Instance.SummaryFullPowerPoint = buttonItemSummaryFullPowerPoint;
			Controller.Instance.SummaryFullTheme = buttonItemSummaryFullTheme;
			Controller.Instance.SummaryFullSlideOutputToggle = checkEditSummaryFullOutputSlide;
			Controller.Instance.SummaryFullTableOutputToggle = checkEditSummaryFullOutputTable;
			#endregion

			#region Strategy
			Controller.Instance.StrategySpecialButtons = ribbonBarStrategySpecialButtons;
			Controller.Instance.StrategyHelp = buttonItemStrategyHelp;
			Controller.Instance.StrategySave = buttonItemStrategySave;
			Controller.Instance.StrategySaveAs = buttonItemStrategySaveAs;
			Controller.Instance.StrategyPreview = buttonItemStrategyPreview;
			Controller.Instance.StrategyEmail = buttonItemStrategyEmail;
			Controller.Instance.StrategyPowerPoint = buttonItemStrategyPowerPoint;
			Controller.Instance.StrategyTheme = buttonItemStrategyTheme;
			Controller.Instance.StrategyFavorites = buttonItemStrategyFavorites;
			Controller.Instance.StrategyShowStationToggle = checkEditStrategyShowStation;
			Controller.Instance.StrategyShowDescriptionToggle = checkEditStrategyShowDescription;
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

			Controller.Instance.Init();

			Controller.Instance.ScheduleChanged += (o, e) => UpdateFormTitle();
			Controller.Instance.FloaterRequested += (o, e) => AppManager.Instance.ShowFloater(this, e.AfterShow);

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
				ribbonBarDigitalPackageSettings.RecalcLayout();
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

		private void UpdateFormTitle()
		{
			if (string.IsNullOrEmpty(SettingsManager.Instance.SelectedWizard)) return;
			var shortSchedule = BusinessWrapper.Instance.ScheduleManager.GetShortSchedule();
			Text = String.Format("{0} Seller - {1} - {2} {3}", MediaMetaData.Instance.DataTypeString, SettingsManager.Instance.SelectedWizard, SettingsManager.Instance.Size, String.Format("({0})",shortSchedule!= null?shortSchedule.ShortFileName:String.Empty));
		}

		private void LoadData()
		{
			UpdateFormTitle();
			ribbonControl.Enabled = false;
			using (var form = new FormProgress())
			{
				form.laProgress.Text = "Chill-Out for a few seconds...\nLoading Schedule...";
				form.TopMost = true;
				form.Show();
				var thread = new Thread(() => Invoke((MethodInvoker) (() => Controller.Instance.LoadData())));
				thread.Start();
				while (thread.IsAlive)
					Application.DoEvents();
				form.Close();
			}
			ribbonControl.SelectedRibbonTabChanged -= ribbonControl_SelectedRibbonTabChanged;
			ribbonControl.SelectedRibbonTabItem = ribbonTabItemHome;
			ribbonControl_SelectedRibbonTabChanged(null, null);
			ribbonControl.SelectedRibbonTabChanged += ribbonControl_SelectedRibbonTabChanged;
			ribbonControl.Enabled = true;
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
			else if ((_currentControl == Controller.Instance.WeeklySchedule))
			{
				if (Controller.Instance.WeeklySchedule.AllowToLeaveControl)
					result = true;
				else
				{
					ribbonControl.SelectedRibbonTabChanged -= ribbonControl_SelectedRibbonTabChanged;
					ribbonControl.SelectedRibbonTabItem = ribbonTabItemWeeklySchedule;
					ribbonControl.SelectedRibbonTabChanged += ribbonControl_SelectedRibbonTabChanged;
				}
			}
			else if ((_currentControl == Controller.Instance.MonthlySchedule))
			{
				if (Controller.Instance.MonthlySchedule.AllowToLeaveControl)
					result = true;
				else
				{
					ribbonControl.SelectedRibbonTabChanged -= ribbonControl_SelectedRibbonTabChanged;
					ribbonControl.SelectedRibbonTabItem = ribbonTabItemMonthlySchedule;
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
			else if ((_currentControl == Controller.Instance.SummaryLight))
			{
				if (Controller.Instance.SummaryLight.AllowToLeaveControl)
					result = true;
				else
				{
					ribbonControl.SelectedRibbonTabChanged -= ribbonControl_SelectedRibbonTabChanged;
					ribbonControl.SelectedRibbonTabItem = ribbonTabItemSummaryLight;
					ribbonControl.SelectedRibbonTabChanged += ribbonControl_SelectedRibbonTabChanged;
				}
			}
			else if ((_currentControl == Controller.Instance.SummaryFull))
			{
				if (Controller.Instance.SummaryFull.AllowToLeaveControl)
					result = true;
				else
				{
					ribbonControl.SelectedRibbonTabChanged -= ribbonControl_SelectedRibbonTabChanged;
					ribbonControl.SelectedRibbonTabItem = ribbonTabItemSummaryFull;
					ribbonControl.SelectedRibbonTabChanged += ribbonControl_SelectedRibbonTabChanged;
				}
			}
			else if ((_currentControl == Controller.Instance.Strategy))
			{
				if (Controller.Instance.Strategy.AllowToLeaveControl)
					result = true;
				else
				{
					ribbonControl.SelectedRibbonTabChanged -= ribbonControl_SelectedRibbonTabChanged;
					ribbonControl.SelectedRibbonTabItem = ribbonTabItemStrategy;
					ribbonControl.SelectedRibbonTabChanged += ribbonControl_SelectedRibbonTabChanged;
				}
			}
			else
				result = true;
			return result;
		}

		private void FormMain_Shown(object sender, EventArgs e)
		{
			UpdateFormTitle();
			if (File.Exists(MediaMetaData.Instance.SettingsManager.IconPath))
				Icon = new Icon(Core.OnlineSchedule.SettingsManager.Instance.IconPath);

			Utilities.Instance.ActivatePowerPoint(MediaSchedulePowerPointHelper.Instance.PowerPointObject);
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
			if (f.WindowState != FormWindowState.Minimized)
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
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemWeeklySchedule)
			{
				if (AllowToLeaveCurrentControl())
				{
					_currentControl = Controller.Instance.WeeklySchedule;
					if (!pnMain.Controls.Contains(_currentControl))
						pnMain.Controls.Add(_currentControl);
				}
				_currentControl.BringToFront();
				pnMain.BringToFront();
			}
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemMonthlySchedule)
			{
				if (AllowToLeaveCurrentControl())
				{
					_currentControl = Controller.Instance.MonthlySchedule;
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
					Controller.Instance.BroadcastCalendar.ShowCalendar(false);
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
					Controller.Instance.CustomCalendar.ShowCalendar(false);
					_currentControl = Controller.Instance.CustomCalendar;
					if (!pnMain.Controls.Contains(_currentControl))
						pnMain.Controls.Add(_currentControl);
				}
				_currentControl.BringToFront();
				pnMain.BringToFront();
			}
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemSummaryLight)
			{
				if (AllowToLeaveCurrentControl())
				{
					_currentControl = Controller.Instance.SummaryLight;
					if (!pnMain.Controls.Contains(_currentControl))
						pnMain.Controls.Add(_currentControl);
				}
				_currentControl.BringToFront();
				pnMain.BringToFront();
			}
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemSummaryFull)
			{
				if (AllowToLeaveCurrentControl())
				{
					_currentControl = Controller.Instance.SummaryFull;
					if (!pnMain.Controls.Contains(_currentControl))
						pnMain.Controls.Add(_currentControl);
				}
				_currentControl.BringToFront();
				pnMain.BringToFront();
			}
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemStrategy)
			{
				if (AllowToLeaveCurrentControl())
				{
					_currentControl = Controller.Instance.Strategy;
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
			else if (_currentControl == Controller.Instance.WeeklySchedule)
				result = Controller.Instance.WeeklySchedule.AllowToLeaveControl;
			else if (_currentControl == Controller.Instance.MonthlySchedule)
				result = Controller.Instance.MonthlySchedule.AllowToLeaveControl;
			else if (_currentControl == Controller.Instance.DigitalProductContainer)
				result = Controller.Instance.DigitalProductContainer.AllowToLeaveControl;
			else if (_currentControl == Controller.Instance.DigitalPackage)
				result = Controller.Instance.DigitalPackage.AllowToLeaveControl;
			else if (_currentControl == Controller.Instance.BroadcastCalendar)
				result = Controller.Instance.BroadcastCalendar.AllowToLeaveControl;
			else if (_currentControl == Controller.Instance.CustomCalendar)
				result = Controller.Instance.CustomCalendar.AllowToLeaveControl;
			else if (_currentControl == Controller.Instance.SummaryLight)
				result = Controller.Instance.SummaryLight.AllowToLeaveControl;
			else if (_currentControl == Controller.Instance.SummaryFull)
				result = Controller.Instance.SummaryFull.AllowToLeaveControl;
			else if (_currentControl == Controller.Instance.Strategy)
				result = Controller.Instance.Strategy.AllowToLeaveControl;
			MediaSchedulePowerPointHelper.Instance.Disconnect(false);
			OnlineSchedulePowerPointHelper.Instance.Disconnect(false);
		}

		private void buttonItemHomeNewSchedule_Click(object sender, EventArgs e)
		{
			using (var from = new FormNewSchedule())
			{
				if (from.ShowDialog() == DialogResult.OK)
				{
					if (!string.IsNullOrEmpty(from.ScheduleName))
					{
						var fileName = BusinessWrapper.Instance.ScheduleManager.GetScheduleFileName(from.ScheduleName.Trim());
						BusinessWrapper.Instance.ActivityManager.AddActivity(new ScheduleActivity("New Created", from.ScheduleName.Trim()));
						BusinessWrapper.Instance.ScheduleManager.OpenSchedule(fileName);
						LoadData();
					}
					else
					{
						Utilities.Instance.ShowWarning("Schedule Name can't be empty");
					}
				}
				else if (!BusinessWrapper.Instance.ScheduleManager.ScheduleLoaded)
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
						BusinessWrapper.Instance.ActivityManager.AddActivity(new ScheduleActivity("Previous Opened", Path.GetFileNameWithoutExtension(fileName)));
						BusinessWrapper.Instance.ScheduleManager.OpenSchedule(fileName, false);
						LoadData();
					}
					else
					{
						Utilities.Instance.ShowWarning("Schedule Name can't be empty");
					}
				}
				else if (!BusinessWrapper.Instance.ScheduleManager.ScheduleLoaded)
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
			AppManager.Instance.ShowFloater(formSender ?? this, null);
		}
	}
}