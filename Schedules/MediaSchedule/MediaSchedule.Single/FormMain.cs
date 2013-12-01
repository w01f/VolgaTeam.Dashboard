using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using NewBizWiz.CommonGUI.Floater;
using NewBizWiz.CommonGUI.ToolForms;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.MediaSchedule;
using NewBizWiz.MediaSchedule.Controls;
using NewBizWiz.MediaSchedule.Controls.BusinessClasses;
using NewBizWiz.MediaSchedule.Controls.InteropClasses;
using NewBizWiz.MediaSchedule.Controls.ToolForms;
using NewBizWiz.OnlineSchedule.Controls.InteropClasses;

namespace NewBizWiz.MediaSchedule.Single
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

			#region Command Controls

			#region Home
			Controller.Instance.HomeBusinessName = comboBoxEditBusinessName;
			Controller.Instance.HomeDecisionMaker = comboBoxEditDecisionMaker;
			Controller.Instance.HomePresentationDate = dateEditPresentationDate;
			Controller.Instance.HomeFlightDatesStart = dateEditFlightDatesStart;
			Controller.Instance.HomeFlightDatesEnd = dateEditFlightDatesEnd;
			Controller.Instance.HomeWeeks = labelItemHomeFlightDatesWeeks;
			Controller.Instance.HomeSave = buttonItemHomeSave;
			Controller.Instance.HomeSaveAs = buttonItemHomeSaveAs;
			Controller.Instance.HomeHelp = buttonItemHomeHelp;
			Controller.Instance.HomeOptions = buttonItemHomeOptions;
			Controller.Instance.HomeProductAdd = buttonItemHomeDigitalProductAdd;
			Controller.Instance.HomeProductClone = buttonItemHomeDigitalProductClone;
			Controller.Instance.HomeProductDelete = buttonItemHomeDigitalProductDelete;
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
			Controller.Instance.WeeklyScheduleOptions = buttonItemWeeklyScheduleOptions;
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
			Controller.Instance.MonthlyScheduleOptions = buttonItemMonthlyScheduleOptions;
			Controller.Instance.MonthlyScheduleProgramAdd = buttonItemMonthlyScheduleProgramAdd;
			Controller.Instance.MonthlyScheduleProgramDelete = buttonItemMonthlyScheduleProgramDelete;
			#endregion

			#region Digital Product
			Controller.Instance.DigitalProductOptions = buttonItemDigitalScheduleOptions;
			Controller.Instance.DigitalProductPreview = buttonItemDigitalSchedulePreview;
			Controller.Instance.DigitalProductPowerPoint = buttonItemDigitalSchedulePowerPoint;
			Controller.Instance.DigitalProductEmail = buttonItemDigitalScheduleEmail;
			Controller.Instance.DigitalProductTheme = buttonItemDigitalScheduleTheme;
			Controller.Instance.DigitalProductSave = buttonItemDigitalScheduleSave;
			Controller.Instance.DigitalProductSaveAs = buttonItemDigitalScheduleSaveAs;
			Controller.Instance.DigitalProductHelp = buttonItemDigitalScheduleHelp;
			#endregion

			#region Digital Package
			Controller.Instance.DigitalPackageHelp = buttonItemDigitalPackageHelp;
			Controller.Instance.DigitalPackageSave = buttonItemDigitalPackageSave;
			Controller.Instance.DigitalPackageSaveAs = buttonItemDigitalPackageSaveAs;
			Controller.Instance.DigitalPackagePreview = buttonItemDigitalPackagePreview;
			Controller.Instance.DigitalPackageEmail = buttonItemDigitalPackageEmail;
			Controller.Instance.DigitalPackagePowerPoint = buttonItemDigitalPackagePowerPoint;
			Controller.Instance.DigitalPackageTheme = buttonItemDigitalPackageTheme;
			Controller.Instance.DigitalPackageOptions = buttonItemDigitalPackageOptions;
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
				dateEditFlightDatesEnd.Font = font;
				dateEditFlightDatesStart.Font = font;
				dateEditPresentationDate.Font = font;
				ribbonBarHomeBasicInfo.RecalcLayout();
				ribbonBarHomeFlightDates.RecalcLayout();
				ribbonBarHomeExit.RecalcLayout();
				ribbonBarDigitalScheduleHelp.RecalcLayout();
				ribbonBarDigitalScheduleSave.RecalcLayout();
				ribbonBarDigitalScheduleEmail.RecalcLayout();
				ribbonBarDigitalScheduleExit.RecalcLayout();
				ribbonBarDigitalScheduleHelp.RecalcLayout();
				ribbonBarDigitalSchedulePowerPoint.RecalcLayout();
				ribbonBarDigitalPackageEmail.RecalcLayout();
				ribbonBarDigitalPackageExit.RecalcLayout();
				ribbonBarDigitalPackageHelp.RecalcLayout();
				ribbonBarDigitalPackageOptions.RecalcLayout();
				ribbonBarDigitalPackagePowerPoint.RecalcLayout();
				ribbonBarDigitalPackageSave.RecalcLayout();
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
			}
			pnMain.BringToFront();
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