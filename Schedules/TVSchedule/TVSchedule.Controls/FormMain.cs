using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using TVScheduleBuilder.ConfigurationClasses;
using TVScheduleBuilder.CustomControls;
using TVScheduleBuilder.ToolForms;

namespace TVScheduleBuilder
{
	public partial class FormMain : Form
	{
		private static FormMain _instance;
		private Control _currentControl;

		private FormMain()
		{
			InitializeComponent();

			ribbonTabItemDigitalSchedule.Enabled = false;

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
				ribbonBarHomeAdvertiser.RecalcLayout();
				ribbonBarHomeExit.RecalcLayout();
				ribbonBarHomeFlightDates.RecalcLayout();
				ribbonBarHomeHelp.RecalcLayout();
				ribbonBarHomeSave.RecalcLayout();
				ribbonBarMonthlyScheduleAdd.RecalcLayout();
				ribbonBarMonthlyScheduleDelete.RecalcLayout();
				ribbonBarMonthlyScheduleEmail.RecalcLayout();
				ribbonBarMonthlyScheduleExit.RecalcLayout();
				ribbonBarMonthlyScheduleHelp.RecalcLayout();
				ribbonBarMonthlyScheduleLineOptions.RecalcLayout();
				ribbonBarMonthlySchedulePowerPoint.RecalcLayout();
				ribbonBarMonthlyScheduleSave.RecalcLayout();
				ribbonBarMonthlyScheduleScheduleTotals.RecalcLayout();
				ribbonBarSalesStrategy.RecalcLayout();
				ribbonBarSuccessModels.RecalcLayout();
				ribbonBarSuccessModelsExit.RecalcLayout();
				ribbonBarSuccessModelsHelp.RecalcLayout();
				ribbonBarWeeklyScheduleAdd.RecalcLayout();
				ribbonBarWeeklyScheduleDelete.RecalcLayout();
				ribbonBarWeeklyScheduleEmail.RecalcLayout();
				ribbonBarWeeklyScheduleExit.RecalcLayout();
				ribbonBarWeeklyScheduleHelp.RecalcLayout();
				ribbonBarWeeklyScheduleLineOptions.RecalcLayout();
				ribbonBarWeeklySchedulePowerPoint.RecalcLayout();
				ribbonBarWeeklyScheduleSave.RecalcLayout();
				ribbonBarWeeklyScheduleTotals.RecalcLayout();
				ribbonPanelHome.PerformLayout();
				ribbonPanelMonthlySchedule.PerformLayout();
				ribbonPanelSuccessModels.PerformLayout();
				ribbonPanelWeeklySchedule.PerformLayout();
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
			if ((_currentControl == HomeControl.Instance))
			{
				if (HomeControl.Instance.AllowToLeaveControl())
					result = true;
				else
				{
					ribbonControl.SelectedRibbonTabChanged -= ribbonControl_SelectedRibbonTabChanged;
					ribbonControl.SelectedRibbonTabItem = ribbonTabItemHome;
					ribbonControl.SelectedRibbonTabChanged += ribbonControl_SelectedRibbonTabChanged;
				}
			}
			else if ((_currentControl == WeeklyScheduleControl.Instance))
			{
				if (WeeklyScheduleControl.Instance.AllowToLeaveControl)
					result = true;
				else
				{
					ribbonControl.SelectedRibbonTabChanged -= ribbonControl_SelectedRibbonTabChanged;
					ribbonControl.SelectedRibbonTabItem = ribbonTabItemWeeklySchedule;
					ribbonControl.SelectedRibbonTabChanged += ribbonControl_SelectedRibbonTabChanged;
				}
			}
			else if ((_currentControl == MonthlyScheduleControl.Instance))
			{
				if (MonthlyScheduleControl.Instance.AllowToLeaveControl)
					result = true;
				else
				{
					ribbonControl.SelectedRibbonTabChanged -= ribbonControl_SelectedRibbonTabChanged;
					ribbonControl.SelectedRibbonTabItem = ribbonTabItemMonthlySchedule;
					ribbonControl.SelectedRibbonTabChanged += ribbonControl_SelectedRibbonTabChanged;
				}
			}
			else
				result = true;
			return result;
		}

		public void UpdateScheduleTabs(bool enable)
		{
			ribbonTabItemWeeklySchedule.Enabled = enable;
			ribbonTabItemMonthlySchedule.Enabled = enable;
		}

		private void FormMain_ClientSizeChanged(object sender, EventArgs e)
		{
			RegistryHelper.MaximizeMainForm = IsMaximized;
		}

		private void FormMain_Shown(object sender, EventArgs e)
		{
			#region Home Events
			buttonItemHomeHelp.Click += HomeControl.Instance.buttonItemHomeHelp_Click;
			buttonItemHomeSave.Click += HomeControl.Instance.buttonItemHomeSave_Click;
			buttonItemHomeSaveAs.Click += HomeControl.Instance.buttonItemHomeSaveAs_Click;
			comboBoxEditBusinessName.EditValueChanged += HomeControl.Instance.SchedulePropertyEditValueChanged;
			comboBoxEditDecisionMaker.EditValueChanged += HomeControl.Instance.SchedulePropertyEditValueChanged;
			comboBoxEditClientType.EditValueChanged += HomeControl.Instance.SchedulePropertyEditValueChanged;
			buttonItemHomeSalesStrategyEmail.Click += HomeControl.Instance.buttonItemHomeSalesStrategyEmail_Click;
			buttonItemHomeSalesStrategyFaceCall.Click += HomeControl.Instance.buttonItemHomeSalesStrategyFaceCall_Click;
			buttonItemHomeSalesStrategyFax.Click += HomeControl.Instance.buttonItemHomeSalesStrategyFax_Click;
			buttonItemHomeSalesStrategyEmail.CheckedChanged += HomeControl.Instance.SchedulePropertyEditValueChanged;
			buttonItemHomeSalesStrategyFax.CheckedChanged += HomeControl.Instance.SchedulePropertyEditValueChanged;
			buttonItemHomeSalesStrategyFaceCall.CheckedChanged += HomeControl.Instance.SchedulePropertyEditValueChanged;
			dateEditPresentationDate.EditValueChanged += HomeControl.Instance.SchedulePropertyEditValueChanged;
			dateEditFlightDatesStart.EditValueChanged += HomeControl.Instance.FlightDateStartEditValueChanged;
			dateEditFlightDatesEnd.EditValueChanged += HomeControl.Instance.FlightDateEndEditValueChanged;
			dateEditFlightDatesStart.EditValueChanged += HomeControl.Instance.CalcWeeksOnFlightDatesChange;
			dateEditFlightDatesEnd.EditValueChanged += HomeControl.Instance.CalcWeeksOnFlightDatesChange;
			dateEditFlightDatesStart.CloseUp += HomeControl.Instance.dateEditFlightDatesStart_CloseUp;
			dateEditFlightDatesEnd.CloseUp += HomeControl.Instance.dateEditFlightDatesEnd_CloseUp;
			comboBoxEditBusinessName.Enter += Editor_Enter;
			comboBoxEditBusinessName.MouseDown += Editor_MouseDown;
			comboBoxEditBusinessName.MouseUp += Editor_MouseUp;
			comboBoxEditDecisionMaker.Enter += Editor_Enter;
			comboBoxEditDecisionMaker.MouseDown += Editor_MouseDown;
			comboBoxEditDecisionMaker.MouseUp += Editor_MouseUp;
			comboBoxEditClientType.Enter += Editor_Enter;
			comboBoxEditClientType.MouseDown += Editor_MouseDown;
			comboBoxEditClientType.MouseUp += Editor_MouseUp;
			#endregion

			#region Weekly Schedule Events
			buttonItemWeeklyScheduleHelp.Click += WeeklyScheduleControl.Instance.buttonItemScheduleHelp_Click;
			buttonItemWeeklyScheduleSave.Click += WeeklyScheduleControl.Instance.buttonItemScheduleSave_Click;
			buttonItemWeeklyScheduleSaveAs.Click += WeeklyScheduleControl.Instance.buttonItemScheduleSaveAs_Click;
			buttonItemWeeklyScheduleCPP.CheckedChanged += WeeklyScheduleControl.Instance.button_CheckedChanged;
			buttonItemWeeklyScheduleDay.CheckedChanged += WeeklyScheduleControl.Instance.button_CheckedChanged;
			buttonItemWeeklyScheduleDaypart.CheckedChanged += WeeklyScheduleControl.Instance.button_CheckedChanged;
			buttonItemWeeklyScheduleGRP.CheckedChanged += WeeklyScheduleControl.Instance.button_CheckedChanged;
			buttonItemWeeklyScheduleCost.CheckedChanged += WeeklyScheduleControl.Instance.button_CheckedChanged;
			buttonItemWeeklyScheduleLength.CheckedChanged += WeeklyScheduleControl.Instance.button_CheckedChanged;
			buttonItemWeeklyScheduleRate.CheckedChanged += WeeklyScheduleControl.Instance.button_CheckedChanged;
			buttonItemWeeklyScheduleRating.CheckedChanged += WeeklyScheduleControl.Instance.button_CheckedChanged;
			buttonItemWeeklyScheduleStation.CheckedChanged += WeeklyScheduleControl.Instance.button_CheckedChanged;
			buttonItemWeeklyScheduleTime.CheckedChanged += WeeklyScheduleControl.Instance.button_CheckedChanged;
			buttonItemWeeklyScheduleSpots.CheckedChanged += WeeklyScheduleControl.Instance.button_CheckedChanged;
			buttonItemWeeklyScheduleEmptySpots.CheckedChanged += WeeklyScheduleControl.Instance.button_CheckedChanged;
			buttonItemWeeklyScheduleTotalPeriods.CheckedChanged += WeeklyScheduleControl.Instance.button_CheckedChanged;
			buttonItemWeeklyScheduleTotalSpots.CheckedChanged += WeeklyScheduleControl.Instance.button_CheckedChanged;
			buttonItemWeeklyScheduleTotalCPP.CheckedChanged += WeeklyScheduleControl.Instance.button_CheckedChanged;
			buttonItemWeeklyScheduleTotalGRP.CheckedChanged += WeeklyScheduleControl.Instance.button_CheckedChanged;
			buttonItemWeeklyScheduleAvgRate.CheckedChanged += WeeklyScheduleControl.Instance.button_CheckedChanged;
			buttonItemWeeklyScheduleTotalCost.CheckedChanged += WeeklyScheduleControl.Instance.button_CheckedChanged;
			buttonItemWeeklyScheduleNetRate.CheckedChanged += WeeklyScheduleControl.Instance.button_CheckedChanged;
			buttonItemWeeklyScheduleDiscount.CheckedChanged += WeeklyScheduleControl.Instance.button_CheckedChanged;
			buttonItemWeeklyScheduleAdd.Click += WeeklyScheduleControl.Instance.buttonItemScheduleAdd_Click;
			buttonItemWeeklyScheduleDelete.Click += WeeklyScheduleControl.Instance.buttonItemScheduleDelete_Click;
			buttonItemWeeklySchedulePowerPoint.Click += WeeklyScheduleControl.Instance.buttonItemWeeklySchedulePowerPoint_Click;
			buttonItemWeeklyScheduleEmail.Click += WeeklyScheduleControl.Instance.buttonItemWeeklyScheduleEmail_Click;
			#endregion

			#region Monthly Schedule Events
			buttonItemMonthlyScheduleHelp.Click += MonthlyScheduleControl.Instance.buttonItemScheduleHelp_Click;
			buttonItemMonthlyScheduleSave.Click += MonthlyScheduleControl.Instance.buttonItemScheduleSave_Click;
			buttonItemMonthlyScheduleSaveAs.Click += MonthlyScheduleControl.Instance.buttonItemScheduleSaveAs_Click;
			buttonItemMonthlyScheduleCPP.CheckedChanged += MonthlyScheduleControl.Instance.button_CheckedChanged;
			buttonItemMonthlyScheduleDay.CheckedChanged += MonthlyScheduleControl.Instance.button_CheckedChanged;
			buttonItemMonthlyScheduleDaypart.CheckedChanged += MonthlyScheduleControl.Instance.button_CheckedChanged;
			buttonItemMonthlyScheduleGRP.CheckedChanged += MonthlyScheduleControl.Instance.button_CheckedChanged;
			buttonItemMonthlyScheduleCost.CheckedChanged += MonthlyScheduleControl.Instance.button_CheckedChanged;
			buttonItemMonthlyScheduleLength.CheckedChanged += MonthlyScheduleControl.Instance.button_CheckedChanged;
			buttonItemMonthlyScheduleRate.CheckedChanged += MonthlyScheduleControl.Instance.button_CheckedChanged;
			buttonItemMonthlyScheduleRating.CheckedChanged += MonthlyScheduleControl.Instance.button_CheckedChanged;
			buttonItemMonthlyScheduleStation.CheckedChanged += MonthlyScheduleControl.Instance.button_CheckedChanged;
			buttonItemMonthlyScheduleTime.CheckedChanged += MonthlyScheduleControl.Instance.button_CheckedChanged;
			buttonItemMonthlyScheduleSpots.CheckedChanged += MonthlyScheduleControl.Instance.button_CheckedChanged;
			buttonItemMonthlyScheduleEmptySpots.CheckedChanged += MonthlyScheduleControl.Instance.button_CheckedChanged;
			buttonItemMonthlyScheduleTotalPeriods.CheckedChanged += MonthlyScheduleControl.Instance.button_CheckedChanged;
			buttonItemMonthlyScheduleTotalSpots.CheckedChanged += MonthlyScheduleControl.Instance.button_CheckedChanged;
			buttonItemMonthlyScheduleTotalCPP.CheckedChanged += MonthlyScheduleControl.Instance.button_CheckedChanged;
			buttonItemMonthlyScheduleTotalGRP.CheckedChanged += MonthlyScheduleControl.Instance.button_CheckedChanged;
			buttonItemMonthlyScheduleAvgRate.CheckedChanged += MonthlyScheduleControl.Instance.button_CheckedChanged;
			buttonItemMonthlyScheduleTotalCost.CheckedChanged += MonthlyScheduleControl.Instance.button_CheckedChanged;
			buttonItemMonthlyScheduleNetRate.CheckedChanged += MonthlyScheduleControl.Instance.button_CheckedChanged;
			buttonItemMonthlyScheduleDiscount.CheckedChanged += MonthlyScheduleControl.Instance.button_CheckedChanged;
			buttonItemMonthlyScheduleAdd.Click += MonthlyScheduleControl.Instance.buttonItemScheduleAdd_Click;
			buttonItemMonthlyScheduleDelete.Click += MonthlyScheduleControl.Instance.buttonItemScheduleDelete_Click;
			buttonItemMonthlySchedulePowerPoint.Click += MonthlyScheduleControl.Instance.buttonItemMonthlySchedulePowerPoint_Click;
			buttonItemMonthlyScheduleEmail.Click += MonthlyScheduleControl.Instance.buttonItemMonthlyScheduleEmail_Click;
			#endregion

			#region Success Models Events
			buttonItemSuccessModelsHelp.Click += ModelsOfSuccessContainerControl.Instance.buttonItemSuccessModelsHelp_Click;
			#endregion

			if (!string.IsNullOrEmpty(SettingsManager.Instance.SelectedWizard))
				Instance.Text = "TV Schedule Builder - " + SettingsManager.Instance.SelectedWizard + " - " + SettingsManager.Instance.Size;
			ribbonControl.Enabled = false;
			using (var form = new FormProgress())
			{
				form.laProgress.Text = "Chill-Out for a few seconds...\nLoading TV Schedule...";
				form.TopMost = true;
				var thread = new Thread(delegate()
											{
												Invoke((MethodInvoker)delegate
																		  {
																			  HomeControl.Instance.LoadSchedule(false);
																			  WeeklyScheduleControl.Instance.LoadSchedule(false);
																			  MonthlyScheduleControl.Instance.LoadSchedule(false);
																		  });
											});
				thread.Start();

				form.Show();

				while (thread.IsAlive)
					Application.DoEvents();
				form.Close();
			}

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
					_currentControl = HomeControl.Instance;
					if (!pnMain.Controls.Contains(_currentControl))
						pnMain.Controls.Add(HomeControl.Instance);
				}
				_currentControl.BringToFront();
			}
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemWeeklySchedule)
			{
				if (AllowToLeaveCurrentControl())
				{
					_currentControl = WeeklyScheduleControl.Instance;
					if (!pnMain.Controls.Contains(_currentControl))
						pnMain.Controls.Add(WeeklyScheduleControl.Instance);
				}
				_currentControl.BringToFront();
			}
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemMonthlySchedule)
			{
				if (AllowToLeaveCurrentControl())
				{
					_currentControl = MonthlyScheduleControl.Instance;
					if (!pnMain.Controls.Contains(_currentControl))
						pnMain.Controls.Add(MonthlyScheduleControl.Instance);
				}
				_currentControl.BringToFront();
			}
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemSuccessModels)
			{
				if (AllowToLeaveCurrentControl())
				{
					_currentControl = ModelsOfSuccessContainerControl.Instance;
					if (!pnMain.Controls.Contains(_currentControl))
						pnMain.Controls.Add(ModelsOfSuccessContainerControl.Instance);
				}
				_currentControl.BringToFront();
			}
			else
				pnEmpty.BringToFront();
			pnMain.BringToFront();
		}

		private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
		{
			bool result = true;
			if (_currentControl == HomeControl.Instance)
				result = HomeControl.Instance.AllowToLeaveControl(true);
			else if (_currentControl == WeeklyScheduleControl.Instance)
				result = WeeklyScheduleControl.Instance.AllowToLeaveControl;
			else if (_currentControl == MonthlyScheduleControl.Instance)
				result = MonthlyScheduleControl.Instance.AllowToLeaveControl;
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

		#region Select All in Editor Handlers
		private bool enter;
		private bool needSelect;

		public void Editor_Enter(object sender, EventArgs e)
		{
			enter = true;
			BeginInvoke(new MethodInvoker(ResetEnterFlag));
		}

		public void Editor_MouseUp(object sender, MouseEventArgs e)
		{
			if (needSelect)
			{
				(sender as BaseEdit).SelectAll();
			}
		}

		public void Editor_MouseDown(object sender, MouseEventArgs e)
		{
			needSelect = enter;
		}

		private void ResetEnterFlag()
		{
			enter = false;
		}
		#endregion
	}
}