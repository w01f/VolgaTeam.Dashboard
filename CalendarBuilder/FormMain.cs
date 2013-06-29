using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using CalendarBuilder.BusinessClasses;
using CalendarBuilder.ConfigurationClasses;
using CalendarBuilder.PresentationClasses;
using CalendarBuilder.ToolForms;
using DevExpress.XtraEditors;

namespace CalendarBuilder
{
	public partial class FormMain : Form
	{
		private static FormMain _instance;
		private Control _currentControl;

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
				ribbonBarHomeAdvertiser.RecalcLayout();
				ribbonBarHomeExit.RecalcLayout();
				ribbonBarHomeFlightDates.RecalcLayout();
				ribbonBarHomeHelp.RecalcLayout();
				ribbonBarHomeSave.RecalcLayout();
				ribbonBarHomeSalesStrategy.RecalcLayout();
				ribbonBarSuccessModels.RecalcLayout();
				ribbonBarSuccessModelsExit.RecalcLayout();
				ribbonBarSuccessModelsHelp.RecalcLayout();
				ribbonBarAdvancedCalendarFloater.RecalcLayout();
				ribbonBarAdvancedCalendarExit.RecalcLayout();
				ribbonBarAdvancedCalendarHelp.RecalcLayout();
				ribbonBarAdvancedCalendarOutput.RecalcLayout();
				ribbonBarAdvancedCalendarSave.RecalcLayout();
				ribbonPanelHome.PerformLayout();
				ribbonPanelSuccessModels.PerformLayout();
				ribbonPanelAdvancedCalendar.PerformLayout();
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
		public event EventHandler<EventArgs> FloaterRequested;

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
				if (HomeControl.Instance.AllowToLeaveControl)
					result = true;
				else
				{
					ribbonControl.SelectedRibbonTabChanged -= ribbonControl_SelectedRibbonTabChanged;
					ribbonControl.SelectedRibbonTabItem = ribbonTabItemHome;
					ribbonControl.SelectedRibbonTabChanged += ribbonControl_SelectedRibbonTabChanged;
				}
			}
			else if (_currentControl == CalendarVisualizer.Instance.SelectedCalendarControl)
			{
				if (CalendarVisualizer.Instance.SelectedCalendarControl != null)
					CalendarVisualizer.Instance.SelectedCalendarControl.LeaveCalendar();
				result = true;
			}
			else
				result = true;
			return result;
		}

		public void UpdateScheduleTabs(bool enable)
		{
			ribbonTabItemAdvancedCalendar.Enabled = enable;
			ribbonTabItemGraphicCalendar.Enabled = enable;
			ribbonTabItemSimpleCalendar.Enabled = enable;
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
																			  HomeControl.Instance.LoadCalendar(false);
																			  Application.DoEvents();
																			  CalendarVisualizer.Instance.LoadData();
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

			ribbonControl.SelectedRibbonTabItem = ribbonTabItemHome;
			ribbonControl_SelectedRibbonTabChanged(null, null);
			ribbonControl.SelectedRibbonTabChanged += ribbonControl_SelectedRibbonTabChanged;
			ribbonControl.Enabled = true;
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
			buttonItemHomeCalendarTypeSunday.Click += HomeControl.Instance.buttonItemHomeCalendarType_Click;
			buttonItemHomeCalendarTypeMonday.Click += HomeControl.Instance.buttonItemHomeCalendarType_Click;
			buttonItemHomeCalendarTypeSunday.CheckedChanged += HomeControl.Instance.buttonItemHomeCalendarType_CheckedChanged;
			buttonItemHomeCalendarTypeMonday.CheckedChanged += HomeControl.Instance.buttonItemHomeCalendarType_CheckedChanged;
			buttonItemHomeProductsDigital.CheckedChanged += HomeControl.Instance.buttonItemHomeProducts_CheckedChanged;
			buttonItemHomeProductsNewspaper.CheckedChanged += HomeControl.Instance.buttonItemHomeProducts_CheckedChanged;
			buttonItemHomeProductsTV.CheckedChanged += HomeControl.Instance.buttonItemHomeProducts_CheckedChanged;
			buttonItemHomeProductsRadio.CheckedChanged += HomeControl.Instance.buttonItemHomeProducts_CheckedChanged;
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

			#region Advanced Calendar Events
			listBoxControlAdvancedCalendar.SelectedIndexChanged += CalendarVisualizer.Instance.imageListBoxEditCalendar_SelectedIndexChanged;
			buttonItemAdvancedCalendarSlideInfo.CheckedChanged += CalendarVisualizer.Instance.buttonItemCalendarSlideInfo_CheckedChanged;
			buttonItemAdvancedCalendarMonth.Click += CalendarVisualizer.Instance.buttonItemCalendarView_Click;
			buttonItemAdvancedCalendarGrid.Click += CalendarVisualizer.Instance.buttonItemCalendarView_Click;
			buttonItemAdvancedCalendarMonth.CheckedChanged += CalendarVisualizer.Instance.buttonItemCalendarView_CheckedChanged;
			buttonItemAdvancedCalendarGrid.CheckedChanged += CalendarVisualizer.Instance.buttonItemCalendarView_CheckedChanged;
			buttonItemAdvancedCalendarCopy.Click += CalendarVisualizer.Instance.buttonItemCalendarCopy_Click;
			buttonItemAdvancedCalendarPaste.Click += CalendarVisualizer.Instance.buttonItemCalendarPaste_Click;
			buttonItemAdvancedCalendarClone.Click += CalendarVisualizer.Instance.buttonItemCalendarClone_Click;
			buttonItemAdvancedCalendarSave.Click += CalendarVisualizer.Instance.buttonItemCalendarSave_Click;
			buttonItemAdvancedCalendarSaveAs.Click += CalendarVisualizer.Instance.buttonItemCalendarSaveAs_Click;
			buttonItemAdvancedCalendarPreview.Click += CalendarVisualizer.Instance.buttonItemCalendarPreview_Click;
			buttonItemAdvancedCalendarPowerPoint.Click += CalendarVisualizer.Instance.buttonItemCalendarPowerPoint_Click;
			buttonItemAdvancedCalendarEmail.Click += CalendarVisualizer.Instance.buttonItemCalendarEmail_Click;
			buttonItemAdvancedCalendarHelp.Click += CalendarVisualizer.Instance.buttonItemCalendarHelp_Click;
			#endregion

			#region Graphic Calendar Events
			listBoxControlGraphicCalendar.SelectedIndexChanged += CalendarVisualizer.Instance.imageListBoxEditCalendar_SelectedIndexChanged;
			buttonItemGraphicCalendarSlideInfo.CheckedChanged += CalendarVisualizer.Instance.buttonItemCalendarSlideInfo_CheckedChanged;
			buttonItemGraphicCalendarMonth.Click += CalendarVisualizer.Instance.buttonItemCalendarView_Click;
			buttonItemGraphicCalendarGrid.Click += CalendarVisualizer.Instance.buttonItemCalendarView_Click;
			buttonItemGraphicCalendarMonth.CheckedChanged += CalendarVisualizer.Instance.buttonItemCalendarView_CheckedChanged;
			buttonItemGraphicCalendarGrid.CheckedChanged += CalendarVisualizer.Instance.buttonItemCalendarView_CheckedChanged;
			buttonItemGraphicCalendarCopy.Click += CalendarVisualizer.Instance.buttonItemCalendarCopy_Click;
			buttonItemGraphicCalendarPaste.Click += CalendarVisualizer.Instance.buttonItemCalendarPaste_Click;
			buttonItemGraphicCalendarClone.Click += CalendarVisualizer.Instance.buttonItemCalendarClone_Click;
			buttonItemGraphicCalendarSave.Click += CalendarVisualizer.Instance.buttonItemCalendarSave_Click;
			buttonItemGraphicCalendarSaveAs.Click += CalendarVisualizer.Instance.buttonItemCalendarSaveAs_Click;
			buttonItemGraphicCalendarPreview.Click += CalendarVisualizer.Instance.buttonItemCalendarPreview_Click;
			buttonItemGraphicCalendarPowerPoint.Click += CalendarVisualizer.Instance.buttonItemCalendarPowerPoint_Click;
			buttonItemGraphicCalendarEmail.Click += CalendarVisualizer.Instance.buttonItemCalendarEmail_Click;
			buttonItemGraphicCalendarHelp.Click += CalendarVisualizer.Instance.buttonItemCalendarHelp_Click;
			#endregion

			#region Simple Calendar Events
			listBoxControlSimpleCalendar.SelectedIndexChanged += CalendarVisualizer.Instance.imageListBoxEditCalendar_SelectedIndexChanged;
			buttonItemSimpleCalendarSlideInfo.CheckedChanged += CalendarVisualizer.Instance.buttonItemCalendarSlideInfo_CheckedChanged;
			buttonItemSimpleCalendarMonth.Click += CalendarVisualizer.Instance.buttonItemCalendarView_Click;
			buttonItemSimpleCalendarGrid.Click += CalendarVisualizer.Instance.buttonItemCalendarView_Click;
			buttonItemSimpleCalendarMonth.CheckedChanged += CalendarVisualizer.Instance.buttonItemCalendarView_CheckedChanged;
			buttonItemSimpleCalendarGrid.CheckedChanged += CalendarVisualizer.Instance.buttonItemCalendarView_CheckedChanged;
			buttonItemSimpleCalendarCopy.Click += CalendarVisualizer.Instance.buttonItemCalendarCopy_Click;
			buttonItemSimpleCalendarPaste.Click += CalendarVisualizer.Instance.buttonItemCalendarPaste_Click;
			buttonItemSimpleCalendarClone.Click += CalendarVisualizer.Instance.buttonItemCalendarClone_Click;
			buttonItemSimpleCalendarSave.Click += CalendarVisualizer.Instance.buttonItemCalendarSave_Click;
			buttonItemSimpleCalendarSaveAs.Click += CalendarVisualizer.Instance.buttonItemCalendarSaveAs_Click;
			buttonItemSimpleCalendarPreview.Click += CalendarVisualizer.Instance.buttonItemCalendarPreview_Click;
			buttonItemSimpleCalendarPowerPoint.Click += CalendarVisualizer.Instance.buttonItemCalendarPowerPoint_Click;
			buttonItemSimpleCalendarEmail.Click += CalendarVisualizer.Instance.buttonItemCalendarEmail_Click;
			buttonItemSimpleCalendarHelp.Click += CalendarVisualizer.Instance.buttonItemCalendarHelp_Click;
			#endregion

			#region Success Models Events
			buttonItemSuccessModelsHelp.Click += ModelsOfSuccessContainerControl.Instance.buttonItemSuccessModelsHelp_Click;
			#endregion

			if (!string.IsNullOrEmpty(SettingsManager.Instance.SelectedWizard))
				Instance.Text = "Ninja Calendar BETA - " + SettingsManager.Instance.SelectedWizard + " - " + SettingsManager.Instance.Size;

			LoadData();
		}

		public void ribbonControl_SelectedRibbonTabChanged(object sender, EventArgs e)
		{
			pnEmpty.BringToFront();
			if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemHome)
			{
				if (AllowToLeaveCurrentControl())
				{
					_currentControl = HomeControl.Instance;
					if (!pnMain.Controls.Contains(_currentControl))
						pnMain.Controls.Add(_currentControl);
				}
				_currentControl.BringToFront();
			}
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemAdvancedCalendar)
			{
				if (AllowToLeaveCurrentControl())
					_currentControl = CalendarVisualizer.Instance.SelectCalendar(pnMain, CalendarStyle.Advanced) as Control;
				_currentControl.BringToFront();
			}
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemGraphicCalendar)
			{
				if (AllowToLeaveCurrentControl())
					_currentControl = CalendarVisualizer.Instance.SelectCalendar(pnMain, CalendarStyle.Graphic) as Control;
				_currentControl.BringToFront();
			}
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemSimpleCalendar)
			{
				if (AllowToLeaveCurrentControl())
					_currentControl = CalendarVisualizer.Instance.SelectCalendar(pnMain, CalendarStyle.Simple) as Control;
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
			pnMain.BringToFront();
		}

		private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
		{
			bool result = true;
			if (_currentControl == HomeControl.Instance)
				result = HomeControl.Instance.AllowToLeaveControl;
			else if (_currentControl == CalendarVisualizer.Instance.SelectedCalendarControl)
				CalendarVisualizer.Instance.SelectedCalendarControl.LeaveCalendar();
		}

		private void buttonItemFloater_Click(object sender, EventArgs e)
		{
			if (FloaterRequested != null)
				FloaterRequested(this, e);
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

		private void ribbonTabItemHome_Click(object sender, EventArgs e)
		{

		}
	}
}