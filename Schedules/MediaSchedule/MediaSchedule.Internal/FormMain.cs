using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using NewBizWiz.CommonGUI.Floater;
using NewBizWiz.CommonGUI.ToolForms;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.MediaSchedule;
using NewBizWiz.MediaSchedule.Controls;
using NewBizWiz.MediaSchedule.Controls.BusinessClasses;

namespace NewBizWiz.MediaSchedule.Internal
{
	public partial class FormMain : Form
	{
		private static FormMain _instance;
		private Control _currentControl;
		public event EventHandler<FloaterRequestedEventArgs> FloaterRequested;

		private FormMain()
		{
			InitializeComponent();

			Controller.Instance.FormMain = this;
			Controller.Instance.Supertip = superTooltip;
			Controller.Instance.Ribbon = ribbonControl;
			Controller.Instance.TabHome = ribbonTabItemHome;
			Controller.Instance.TabWeeklySchedule = ribbonTabItemWeeklySchedule;
			Controller.Instance.TabMonthlySchedule = ribbonTabItemMonthlySchedule;
			Controller.Instance.TabDigitalProduct = ribbonTabItemDigitalSlides;
			Controller.Instance.TabDigitalPackage = ribbonTabItemDigitalPackage;
			Controller.Instance.TabCalendar = ribbonTabItemCalendar;
			Controller.Instance.TabGallery = ribbonTabItemGallery;

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
			#endregion

			#region Monthly Schedule
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

			#region Calendar
			Controller.Instance.CalendarMonthsList = listBoxControlCalendar;
			Controller.Instance.CalendarSlideInfo = buttonItemCalendarSlideInfo;
			Controller.Instance.CalendarCopy = buttonItemCalendarCopy;
			Controller.Instance.CalendarPaste = buttonItemCalendarPaste;
			Controller.Instance.CalendarClone = buttonItemCalendarClone;
			Controller.Instance.CalendarHelp = buttonItemCalendarHelp;
			Controller.Instance.CalendarSave = buttonItemCalendarSave;
			Controller.Instance.CalendarSaveAs = buttonItemCalendarSaveAs;
			Controller.Instance.CalendarPreview = buttonItemCalendarPreview;
			Controller.Instance.CalendarEmail = buttonItemCalendarEmail;
			Controller.Instance.CalendarPowerPoint = buttonItemCalendarPowerPoint;
			#endregion

			#region Gallery
			Controller.Instance.GalleryBrowseBar = ribbonBarGalleryBrowse;
			Controller.Instance.GalleryImageBar = ribbonBarGalleryImage;
			Controller.Instance.GalleryZoomBar = ribbonBarGalleryZoom;
			Controller.Instance.GalleryCopyBar = ribbonBarGalleryCopy;
			Controller.Instance.GalleryScreenshots = buttonItemGalleryBrowseScreenshots;
			Controller.Instance.GalleryAdSpecs = buttonItemGalleryBrowseAdSpecs;
			Controller.Instance.GalleryView = buttonItemGalleryView;
			Controller.Instance.GalleryEdit = buttonItemGalleryEdit;
			Controller.Instance.GalleryImageSelect = buttonItemGalleryImageSelect;
			Controller.Instance.GalleryImageCrop = buttonItemGalleryImageCrop;
			Controller.Instance.GalleryZoomIn = buttonItemGalleryZoomIn;
			Controller.Instance.GalleryZoomOut = buttonItemGalleryZoomOut;
			Controller.Instance.GalleryCopy = buttonItemGalleryCopy;
			Controller.Instance.GalleryHelp = buttonItemGalleryHelp;
			Controller.Instance.GallerySections = comboBoxEditGallerySections;
			Controller.Instance.GalleryGroups = comboBoxEditGalleryGroups;
			#endregion

			#endregion

			Controller.Instance.Init();

			Controller.Instance.ScheduleChanged += (o, e) => UpdateFormTitle();
			Controller.Instance.FloaterRequested += (o, e) => ShowFloater(e);

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
			if (!string.IsNullOrEmpty(SettingsManager.Instance.SelectedWizard))
				Text = String.Format("{0} Schedule Builder - {1} - {2} ({3})", MediaMetaData.Instance.DataTypeString, SettingsManager.Instance.SelectedWizard, SettingsManager.Instance.Size, BusinessWrapper.Instance.ScheduleManager.GetShortSchedule().ShortFileName);
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
					ribbonControl.SelectedRibbonTabItem = ribbonTabItemDigitalPackage;
					ribbonControl.SelectedRibbonTabChanged += ribbonControl_SelectedRibbonTabChanged;
				}
			}
			else
				result = true;
			return result;
		}

		public void ShowFloater(FloaterRequestedEventArgs args)
		{
			if (FloaterRequested != null)
				FloaterRequested(this, args);
		}

		private void FormMain_Shown(object sender, EventArgs e)
		{
			UpdateFormTitle();
			ribbonControl.Enabled = false;
			using (var form = new FormProgress())
			{
				form.laProgress.Text = "Chill-Out for a few seconds...\nLoading Online Schedule...";
				form.TopMost = true;
				form.Show();
				var thread = new Thread(delegate() { Invoke((MethodInvoker)delegate { Controller.Instance.LoadData(); }); });
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
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemCalendar)
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
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemGallery)
			{
				if (AllowToLeaveCurrentControl())
				{
					_currentControl = Controller.Instance.Gallery;
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
		}

		private void buttonItemFloater_Click(object sender, EventArgs e)
		{
			ShowFloater(new FloaterRequestedEventArgs());
		}

		private void buttonItemHomeExit_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void pnMain_Click(object sender, EventArgs e)
		{
			if ((sender as Control) != null)
				(sender as Control).Focus();
		}
	}
}