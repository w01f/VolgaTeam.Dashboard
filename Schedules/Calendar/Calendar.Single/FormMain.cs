using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using NewBizWiz.Calendar.Controls;
using NewBizWiz.Calendar.Controls.BusinessClasses;
using NewBizWiz.Calendar.Controls.InteropClasses;
using NewBizWiz.CommonGUI;
using NewBizWiz.CommonGUI.ToolForms;
using NewBizWiz.Core.Calendar;
using NewBizWiz.Core.Common;
using SettingsManager = NewBizWiz.Core.Common.SettingsManager;

namespace NewBizWiz.Calendar.Single
{
	public partial class FormMain : Form
	{
		private static FormMain _instance;
		private Control _currentControl;

		private FormMain()
		{
			InitializeComponent();

			FormStateHelper.Init(this, Path.GetDirectoryName(typeof(FormMain).Assembly.Location), true);

			Controller.Instance.FormMain = this;
			Controller.Instance.Supertip = superTooltip;
			Controller.Instance.Ribbon = ribbonControl;
			Controller.Instance.TabHome = ribbonTabItemHome;
			Controller.Instance.TabCalendar = ribbonTabItemCalendar;
			Controller.Instance.TabGrid = ribbonTabItemGrid;
			Controller.Instance.TabRateCard = ribbonTabItemRateCard;
			Controller.Instance.TabGallery1 = ribbonTabItemGallery1;
			Controller.Instance.TabGallery2 = ribbonTabItemGallery2;

			#region Command Controls
			#region Home
			Controller.Instance.HomePanel = ribbonPanelHome;
			Controller.Instance.HomeSpecialButtons = ribbonBarHomeSpecialButtons;
			Controller.Instance.HomeHelp = buttonItemHomeHelp;
			Controller.Instance.HomeSave = buttonItemHomeSave;
			Controller.Instance.HomeSaveAs = buttonItemHomeSaveAs;
			Controller.Instance.HomeAccountNumberText = textEditAccountNumber;
			Controller.Instance.HomeAccountNumberCheck = checkBoxItemHomeAccountNumber;
			Controller.Instance.HomeBusinessName = comboBoxEditBusinessName;
			Controller.Instance.HomeDecisionMaker = comboBoxEditDecisionMaker;
			Controller.Instance.HomeClientType = comboBoxEditClientType;
			Controller.Instance.HomePresentationDate = dateEditPresentationDate;
			Controller.Instance.HomeFlightDatesStart = dateEditFlightDatesStart;
			Controller.Instance.HomeFlightDatesEnd = dateEditFlightDatesEnd;
			Controller.Instance.HomeWeeks = labelItemFlightDatesWeeks;
			#endregion

			#region Calendar
			Controller.Instance.CalendarSpecialButtons = ribbonBarCalendarSpecialButtons;
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

			#region Grid
			Controller.Instance.GridSpecialButtons = ribbonBarGridSpecialButtons;
			Controller.Instance.GridMonthsList = listBoxControlGrid;
			Controller.Instance.GridSlideInfo = buttonItemGridSlideInfo;
			Controller.Instance.GridCopy = buttonItemGridCopy;
			Controller.Instance.GridPaste = buttonItemGridPaste;
			Controller.Instance.GridClone = buttonItemGridClone;
			Controller.Instance.GridHelp = buttonItemGridHelp;
			Controller.Instance.GridSave = buttonItemGridSave;
			Controller.Instance.GridSaveAs = buttonItemGridSaveAs;
			Controller.Instance.GridPreview = buttonItemGridPreview;
			Controller.Instance.GridEmail = buttonItemGridEmail;
			Controller.Instance.GridPowerPoint = buttonItemGridPowerPoint;
			#endregion

			#region Rate Card
			Controller.Instance.RateCardSpecialButtons = ribbonBarRateCardSpecialButtons;
			Controller.Instance.RateCardHelp = buttonItemRateCardHelp;
			Controller.Instance.RateCardCombo = comboBoxEditRateCards;
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
				ribbonBarHomeExit.RecalcLayout();
				ribbonBarHomeFlightDates.RecalcLayout();
				ribbonPanelHome.PerformLayout();
			}
		}

		public bool IsMaximized
		{
			get { return WindowState == FormWindowState.Normal ? false : true; }
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
				Text = String.Format("Ninja Calendar BETA - {0} - {1} ({2})", SettingsManager.Instance.SelectedWizard, SettingsManager.Instance.Size, BusinessWrapper.Instance.ScheduleManager.GetShortSchedule().ShortFileName);
		}

		private bool AllowToLeaveCurrentControl()
		{
			bool result = false;
			if ((_currentControl == Controller.Instance.HomeControl))
			{
				if (Controller.Instance.HomeControl.AllowToLeaveControl)
					result = true;
				else
				{
					ribbonControl.SelectedRibbonTabChanged -= ribbonControl_SelectedRibbonTabChanged;
					ribbonControl.SelectedRibbonTabItem = ribbonTabItemHome;
					ribbonControl.SelectedRibbonTabChanged += ribbonControl_SelectedRibbonTabChanged;
				}
			}
			else if (_currentControl == Controller.Instance.CalendarVisualizer.SelectedCalendarControl)
			{
				if (Controller.Instance.CalendarVisualizer.SelectedCalendarControl != null)
					Controller.Instance.CalendarVisualizer.SelectedCalendarControl.LeaveCalendar();
				result = true;
			}
			else
				result = true;
			return result;
		}

		public void LoadData()
		{
			UpdateFormTitle();
			ribbonControl.Enabled = false;
			using (var form = new FormProgress())
			{
				form.laProgress.Text = "Chill-Out for a few seconds...\nLoading Ninja Calendar...";
				form.TopMost = true;
				var thread = new Thread(delegate()
				{
					Invoke((MethodInvoker)delegate()
					{
						Controller.Instance.LoadData();
						Application.DoEvents();
					});
				});

				form.Show();
				Application.DoEvents();

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

		private void FormMain_Resize(object sender, EventArgs e)
		{
			var f = sender as Form;
			if (f.WindowState != FormWindowState.Minimized)
				Opacity = 1;
		}

		private void FormMain_ClientSizeChanged(object sender, EventArgs e)
		{
			RegistryHelper.MaximizeMainForm = WindowState == FormWindowState.Maximized;
		}

		private void FormMain_Shown(object sender, EventArgs e)
		{
			#region Home Events
			comboBoxEditBusinessName.Enter += Utilities.Instance.Editor_Enter;
			comboBoxEditBusinessName.MouseDown += Utilities.Instance.Editor_MouseDown;
			comboBoxEditBusinessName.MouseUp += Utilities.Instance.Editor_MouseUp;
			comboBoxEditDecisionMaker.Enter += Utilities.Instance.Editor_Enter;
			comboBoxEditDecisionMaker.MouseDown += Utilities.Instance.Editor_MouseDown;
			comboBoxEditDecisionMaker.MouseUp += Utilities.Instance.Editor_MouseUp;
			comboBoxEditClientType.Enter += Utilities.Instance.Editor_Enter;
			comboBoxEditClientType.MouseDown += Utilities.Instance.Editor_MouseDown;
			comboBoxEditClientType.MouseUp += Utilities.Instance.Editor_MouseUp;
			#endregion

			#region Graphic Calendar Events
			#endregion

			if (!string.IsNullOrEmpty(SettingsManager.Instance.SelectedWizard))
				Text = String.Format("Ninja Calendar BETA - {0} - {1}", SettingsManager.Instance.SelectedWizard, SettingsManager.Instance.Size);

			if (File.Exists(Core.Calendar.SettingsManager.Instance.IconPath))
				Icon = new Icon(Core.Calendar.SettingsManager.Instance.IconPath);

			Utilities.Instance.ActivatePowerPoint(CalendarPowerPointHelper.Instance.PowerPointObject);
			AppManager.ActivateMainForm();

			using (var formStart = new FormStart())
			{
				formStart.buttonXOpen.Enabled = ScheduleManager.GetShortScheduleExtendedList().Length > 0;
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

		public void ribbonControl_SelectedRibbonTabChanged(object sender, EventArgs e)
		{
			pnEmpty.BringToFront();
			if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemHome)
			{
				if (AllowToLeaveCurrentControl())
				{
					_currentControl = Controller.Instance.HomeControl;
					if (!pnMain.Controls.Contains(_currentControl))
						pnMain.Controls.Add(_currentControl);
				}
				_currentControl.BringToFront();
			}
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemCalendar)
			{
				if (AllowToLeaveCurrentControl())
					_currentControl = Controller.Instance.CalendarVisualizer.SelectCalendar(pnMain, false) as Control;
				_currentControl.BringToFront();
			}
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemGrid)
			{
				if (AllowToLeaveCurrentControl())
					_currentControl = Controller.Instance.CalendarVisualizer.SelectCalendar(pnMain, true) as Control;
				_currentControl.BringToFront();
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
			}
			pnMain.BringToFront();
			if (WindowState == FormWindowState.Normal)
			{
				Width++;
				Width--;
			}
		}

		private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
		{
			bool result = true;
			if (_currentControl != null && _currentControl == Controller.Instance.HomeControl)
				result = Controller.Instance.HomeControl.AllowToLeaveControl;
			else if (_currentControl!= null && _currentControl == Controller.Instance.CalendarVisualizer.SelectedCalendarControl)
				Controller.Instance.CalendarVisualizer.SelectedCalendarControl.LeaveCalendar();
			CalendarPowerPointHelper.Instance.Disconnect(false);
		}

		private void buttonItemHomeNewSchedule_Click(object sender, EventArgs e)
		{
			using (var from = new FormNewSchedule())
			{
				if (from.ShowDialog() == DialogResult.OK)
				{
					if (!string.IsNullOrEmpty(from.ScheduleName))
					{
						string fileName = from.ScheduleName.Trim();
						BusinessWrapper.Instance.ActivityManager.AddActivity(new ScheduleActivity("New Created", from.ScheduleName.Trim()));
						BusinessWrapper.Instance.ScheduleManager.CreateSchedule(fileName);
						LoadData();
					}
					else
					{
						Utilities.Instance.ShowWarning("Schedule Name can't be empty");
					}
				}
				else if (!BusinessWrapper.Instance.ScheduleManager.CalendarLoaded)
					Close();
			}
		}

		private void buttonItemHomeOpenSchedule_Click(object sender, EventArgs e)
		{
			using (var from = new FormOpenCalendar())
			{
				if (from.ShowDialog() == DialogResult.OK)
				{
					BusinessWrapper.Instance.ScheduleManager.OpenSchedule(from.ScheduleFilePath);
					BusinessWrapper.Instance.ActivityManager.AddActivity(new ScheduleActivity("Previous Opened", Path.GetFileNameWithoutExtension(from.ScheduleFilePath)));
					LoadData();
				}
				else if (!BusinessWrapper.Instance.ScheduleManager.CalendarLoaded)
					Close();
			}
		}

		private void buttonItemExit_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void buttonItemFloater_Click(object sender, EventArgs e)
		{
			var formSender = sender as Form;
			AppManager.Instance.ShowFloater(formSender ?? this, null);
		}

		private void pnMain_Click(object sender, EventArgs e)
		{
			if ((sender as Control) != null)
				(sender as Control).Focus();
		}
	}
}