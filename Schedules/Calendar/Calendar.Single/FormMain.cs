using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using NewBizWiz.Calendar.Controls;
using NewBizWiz.Calendar.Controls.BusinessClasses;
using NewBizWiz.Calendar.Controls.InteropClasses;
using NewBizWiz.Calendar.Controls.ToolForms;
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

			Controller.Instance.FormMain = this;
			Controller.Instance.Supertip = superTooltip;
			Controller.Instance.Ribbon = ribbonControl;
			Controller.Instance.TabCalendar = ribbonTabItemCalendar;
			Controller.Instance.TabGrid = ribbonTabItemGrid;

			#region Command Controls
			#region Home
			Controller.Instance.HomePanel = ribbonPanelHome;
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
			#endregion

			Controller.Instance.Init();

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
				ribbonBarHomeHelp.RecalcLayout();
				ribbonBarHomeSave.RecalcLayout();
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

			if (!string.IsNullOrEmpty(SettingsManager.Instance.SelectedWizard))
				Text = String.Format("Ninja Calendar BETA - {0} - {1} ({2})", SettingsManager.Instance.SelectedWizard, SettingsManager.Instance.Size, BusinessWrapper.Instance.ScheduleManager.GetShortSchedule().ShortFileName);
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
			Utilities.Instance.ActivateMiniBar();
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
			pnMain.BringToFront();
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
			using (var from = new FormNewCalendar())
			{
				if (from.ShowDialog() == DialogResult.OK)
				{
					if (!string.IsNullOrEmpty(from.ScheduleName))
					{
						string fileName = from.ScheduleName.Trim();
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
					if (from.NeedToImport)
						BusinessWrapper.Instance.ScheduleManager.ImportSchedule(from.ScheduleFilePath);
					else
						BusinessWrapper.Instance.ScheduleManager.OpenSchedule(from.ScheduleFilePath);
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

		private void pnMain_Click(object sender, EventArgs e)
		{
			if ((sender as Control) != null)
				(sender as Control).Focus();
		}
	}
}