using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TVScheduleBuilder
{
    public partial class FormMain : Form
    {
        private static FormMain _instance = null;
        private Control _currentControl = null;

        public bool IsMaximized
        {
            get
            {
                return this.WindowState == FormWindowState.Normal ? false : true;
            }
        }

        private FormMain()
        {
            InitializeComponent();

            ribbonTabItemDigitalSchedule.Enabled = false;

            if ((base.CreateGraphics()).DpiX > 96)
            {
                Font font = new Font(styleController.Appearance.Font.FontFamily, styleController.Appearance.Font.Size - 1, styleController.Appearance.Font.Style);
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

        public static void RemoveInstance()
        {
            _instance.Dispose();
            _instance = null;
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

        private bool AllowToLeaveCurrentControl()
        {
            bool result = false;
            if ((_currentControl == CustomControls.HomeControl.Instance))
            {
                if (CustomControls.HomeControl.Instance.AllowToLeaveControl())
                    result = true;
                else
                {
                    ribbonControl.SelectedRibbonTabChanged -= new EventHandler(ribbonControl_SelectedRibbonTabChanged);
                    ribbonControl.SelectedRibbonTabItem = ribbonTabItemHome;
                    ribbonControl.SelectedRibbonTabChanged += new EventHandler(ribbonControl_SelectedRibbonTabChanged);
                }
            }
            else if ((_currentControl == CustomControls.WeeklyScheduleControl.Instance))
            {
                if (CustomControls.WeeklyScheduleControl.Instance.AllowToLeaveControl)
                    result = true;
                else
                {
                    ribbonControl.SelectedRibbonTabChanged -= new EventHandler(ribbonControl_SelectedRibbonTabChanged);
                    ribbonControl.SelectedRibbonTabItem = ribbonTabItemWeeklySchedule;
                    ribbonControl.SelectedRibbonTabChanged += new EventHandler(ribbonControl_SelectedRibbonTabChanged);
                }
            }
            else if ((_currentControl == CustomControls.MonthlyScheduleControl.Instance))
            {
                if (CustomControls.MonthlyScheduleControl.Instance.AllowToLeaveControl)
                    result = true;
                else
                {
                    ribbonControl.SelectedRibbonTabChanged -= new EventHandler(ribbonControl_SelectedRibbonTabChanged);
                    ribbonControl.SelectedRibbonTabItem = ribbonTabItemMonthlySchedule;
                    ribbonControl.SelectedRibbonTabChanged += new EventHandler(ribbonControl_SelectedRibbonTabChanged);
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
            ConfigurationClasses.RegistryHelper.MaximizeMainForm = this.IsMaximized;
        }

        private void FormMain_Shown(object sender, EventArgs e)
        {
            #region Home Events
            buttonItemHomeHelp.Click += new EventHandler(CustomControls.HomeControl.Instance.buttonItemHomeHelp_Click);
            buttonItemHomeSave.Click += new EventHandler(CustomControls.HomeControl.Instance.buttonItemHomeSave_Click);
            buttonItemHomeSaveAs.Click += new EventHandler(CustomControls.HomeControl.Instance.buttonItemHomeSaveAs_Click);
            comboBoxEditBusinessName.EditValueChanged += new EventHandler(CustomControls.HomeControl.Instance.SchedulePropertyEditValueChanged);
            comboBoxEditDecisionMaker.EditValueChanged += new EventHandler(CustomControls.HomeControl.Instance.SchedulePropertyEditValueChanged);
            comboBoxEditClientType.EditValueChanged += new EventHandler(CustomControls.HomeControl.Instance.SchedulePropertyEditValueChanged);
            buttonItemHomeSalesStrategyEmail.Click += new EventHandler(CustomControls.HomeControl.Instance.buttonItemHomeSalesStrategyEmail_Click);
            buttonItemHomeSalesStrategyFaceCall.Click += new EventHandler(CustomControls.HomeControl.Instance.buttonItemHomeSalesStrategyFaceCall_Click);
            buttonItemHomeSalesStrategyFax.Click += new EventHandler(CustomControls.HomeControl.Instance.buttonItemHomeSalesStrategyFax_Click);
            buttonItemHomeSalesStrategyEmail.CheckedChanged += new EventHandler(CustomControls.HomeControl.Instance.SchedulePropertyEditValueChanged);
            buttonItemHomeSalesStrategyFax.CheckedChanged += new EventHandler(CustomControls.HomeControl.Instance.SchedulePropertyEditValueChanged);
            buttonItemHomeSalesStrategyFaceCall.CheckedChanged += new EventHandler(CustomControls.HomeControl.Instance.SchedulePropertyEditValueChanged);
            dateEditPresentationDate.EditValueChanged += new EventHandler(CustomControls.HomeControl.Instance.SchedulePropertyEditValueChanged);
            dateEditFlightDatesStart.EditValueChanged += new EventHandler(CustomControls.HomeControl.Instance.FlightDateStartEditValueChanged);
            dateEditFlightDatesEnd.EditValueChanged += new EventHandler(CustomControls.HomeControl.Instance.FlightDateEndEditValueChanged);
            dateEditFlightDatesStart.EditValueChanged += new EventHandler(CustomControls.HomeControl.Instance.CalcWeeksOnFlightDatesChange);
            dateEditFlightDatesEnd.EditValueChanged += new EventHandler(CustomControls.HomeControl.Instance.CalcWeeksOnFlightDatesChange);
            dateEditFlightDatesStart.CloseUp += new DevExpress.XtraEditors.Controls.CloseUpEventHandler(CustomControls.HomeControl.Instance.dateEditFlightDatesStart_CloseUp);
            dateEditFlightDatesEnd.CloseUp += new DevExpress.XtraEditors.Controls.CloseUpEventHandler(CustomControls.HomeControl.Instance.dateEditFlightDatesEnd_CloseUp);
            comboBoxEditBusinessName.Enter += new EventHandler(Editor_Enter);
            comboBoxEditBusinessName.MouseDown += new MouseEventHandler(Editor_MouseDown);
            comboBoxEditBusinessName.MouseUp += new MouseEventHandler(Editor_MouseUp);
            comboBoxEditDecisionMaker.Enter += new EventHandler(Editor_Enter);
            comboBoxEditDecisionMaker.MouseDown += new MouseEventHandler(Editor_MouseDown);
            comboBoxEditDecisionMaker.MouseUp += new MouseEventHandler(Editor_MouseUp);
            comboBoxEditClientType.Enter += new EventHandler(Editor_Enter);
            comboBoxEditClientType.MouseDown += new MouseEventHandler(Editor_MouseDown);
            comboBoxEditClientType.MouseUp += new MouseEventHandler(Editor_MouseUp);
            #endregion

            #region Weekly Schedule Events
            buttonItemWeeklyScheduleHelp.Click += new EventHandler(CustomControls.WeeklyScheduleControl.Instance.buttonItemScheduleHelp_Click);
            buttonItemWeeklyScheduleSave.Click += new EventHandler(CustomControls.WeeklyScheduleControl.Instance.buttonItemScheduleSave_Click);
            buttonItemWeeklyScheduleSaveAs.Click += new EventHandler(CustomControls.WeeklyScheduleControl.Instance.buttonItemScheduleSaveAs_Click);
            buttonItemWeeklyScheduleCPP.CheckedChanged += new EventHandler(CustomControls.WeeklyScheduleControl.Instance.button_CheckedChanged);
            buttonItemWeeklyScheduleDay.CheckedChanged += new EventHandler(CustomControls.WeeklyScheduleControl.Instance.button_CheckedChanged);
            buttonItemWeeklyScheduleDaypart.CheckedChanged += new EventHandler(CustomControls.WeeklyScheduleControl.Instance.button_CheckedChanged);
            buttonItemWeeklyScheduleGRP.CheckedChanged += new EventHandler(CustomControls.WeeklyScheduleControl.Instance.button_CheckedChanged);
            buttonItemWeeklyScheduleCost.CheckedChanged += new EventHandler(CustomControls.WeeklyScheduleControl.Instance.button_CheckedChanged);
            buttonItemWeeklyScheduleLength.CheckedChanged += new EventHandler(CustomControls.WeeklyScheduleControl.Instance.button_CheckedChanged);
            buttonItemWeeklyScheduleRate.CheckedChanged += new EventHandler(CustomControls.WeeklyScheduleControl.Instance.button_CheckedChanged);
            buttonItemWeeklyScheduleRating.CheckedChanged += new EventHandler(CustomControls.WeeklyScheduleControl.Instance.button_CheckedChanged);
            buttonItemWeeklyScheduleStation.CheckedChanged += new EventHandler(CustomControls.WeeklyScheduleControl.Instance.button_CheckedChanged);
            buttonItemWeeklyScheduleTime.CheckedChanged += new EventHandler(CustomControls.WeeklyScheduleControl.Instance.button_CheckedChanged);
            buttonItemWeeklyScheduleSpots.CheckedChanged += new EventHandler(CustomControls.WeeklyScheduleControl.Instance.button_CheckedChanged);
            buttonItemWeeklyScheduleEmptySpots.CheckedChanged += new EventHandler(CustomControls.WeeklyScheduleControl.Instance.button_CheckedChanged);
            buttonItemWeeklyScheduleTotalPeriods.CheckedChanged += new EventHandler(CustomControls.WeeklyScheduleControl.Instance.button_CheckedChanged);
            buttonItemWeeklyScheduleTotalSpots.CheckedChanged += new EventHandler(CustomControls.WeeklyScheduleControl.Instance.button_CheckedChanged);
            buttonItemWeeklyScheduleTotalCPP.CheckedChanged += new EventHandler(CustomControls.WeeklyScheduleControl.Instance.button_CheckedChanged);
            buttonItemWeeklyScheduleTotalGRP.CheckedChanged += new EventHandler(CustomControls.WeeklyScheduleControl.Instance.button_CheckedChanged);
            buttonItemWeeklyScheduleAvgRate.CheckedChanged += new EventHandler(CustomControls.WeeklyScheduleControl.Instance.button_CheckedChanged);
            buttonItemWeeklyScheduleTotalCost.CheckedChanged += new EventHandler(CustomControls.WeeklyScheduleControl.Instance.button_CheckedChanged);
            buttonItemWeeklyScheduleNetRate.CheckedChanged += new EventHandler(CustomControls.WeeklyScheduleControl.Instance.button_CheckedChanged);
            buttonItemWeeklyScheduleDiscount.CheckedChanged += new EventHandler(CustomControls.WeeklyScheduleControl.Instance.button_CheckedChanged);
            buttonItemWeeklyScheduleAdd.Click += new EventHandler(CustomControls.WeeklyScheduleControl.Instance.buttonItemScheduleAdd_Click);
            buttonItemWeeklyScheduleDelete.Click += new EventHandler(CustomControls.WeeklyScheduleControl.Instance.buttonItemScheduleDelete_Click);
            buttonItemWeeklySchedulePowerPoint.Click += new EventHandler(CustomControls.WeeklyScheduleControl.Instance.buttonItemWeeklySchedulePowerPoint_Click);
            buttonItemWeeklyScheduleEmail.Click += new EventHandler(CustomControls.WeeklyScheduleControl.Instance.buttonItemWeeklyScheduleEmail_Click);
            #endregion

            #region Monthly Schedule Events
            buttonItemMonthlyScheduleHelp.Click += new EventHandler(CustomControls.MonthlyScheduleControl.Instance.buttonItemScheduleHelp_Click);
            buttonItemMonthlyScheduleSave.Click += new EventHandler(CustomControls.MonthlyScheduleControl.Instance.buttonItemScheduleSave_Click);
            buttonItemMonthlyScheduleSaveAs.Click += new EventHandler(CustomControls.MonthlyScheduleControl.Instance.buttonItemScheduleSaveAs_Click);
            buttonItemMonthlyScheduleCPP.CheckedChanged += new EventHandler(CustomControls.MonthlyScheduleControl.Instance.button_CheckedChanged);
            buttonItemMonthlyScheduleDay.CheckedChanged += new EventHandler(CustomControls.MonthlyScheduleControl.Instance.button_CheckedChanged);
            buttonItemMonthlyScheduleDaypart.CheckedChanged += new EventHandler(CustomControls.MonthlyScheduleControl.Instance.button_CheckedChanged);
            buttonItemMonthlyScheduleGRP.CheckedChanged += new EventHandler(CustomControls.MonthlyScheduleControl.Instance.button_CheckedChanged);
            buttonItemMonthlyScheduleCost.CheckedChanged += new EventHandler(CustomControls.MonthlyScheduleControl.Instance.button_CheckedChanged);
            buttonItemMonthlyScheduleLength.CheckedChanged += new EventHandler(CustomControls.MonthlyScheduleControl.Instance.button_CheckedChanged);
            buttonItemMonthlyScheduleRate.CheckedChanged += new EventHandler(CustomControls.MonthlyScheduleControl.Instance.button_CheckedChanged);
            buttonItemMonthlyScheduleRating.CheckedChanged += new EventHandler(CustomControls.MonthlyScheduleControl.Instance.button_CheckedChanged);
            buttonItemMonthlyScheduleStation.CheckedChanged += new EventHandler(CustomControls.MonthlyScheduleControl.Instance.button_CheckedChanged);
            buttonItemMonthlyScheduleTime.CheckedChanged += new EventHandler(CustomControls.MonthlyScheduleControl.Instance.button_CheckedChanged);
            buttonItemMonthlyScheduleSpots.CheckedChanged += new EventHandler(CustomControls.MonthlyScheduleControl.Instance.button_CheckedChanged);
            buttonItemMonthlyScheduleEmptySpots.CheckedChanged += new EventHandler(CustomControls.MonthlyScheduleControl.Instance.button_CheckedChanged);
            buttonItemMonthlyScheduleTotalPeriods.CheckedChanged += new EventHandler(CustomControls.MonthlyScheduleControl.Instance.button_CheckedChanged);
            buttonItemMonthlyScheduleTotalSpots.CheckedChanged += new EventHandler(CustomControls.MonthlyScheduleControl.Instance.button_CheckedChanged);
            buttonItemMonthlyScheduleTotalCPP.CheckedChanged += new EventHandler(CustomControls.MonthlyScheduleControl.Instance.button_CheckedChanged);
            buttonItemMonthlyScheduleTotalGRP.CheckedChanged += new EventHandler(CustomControls.MonthlyScheduleControl.Instance.button_CheckedChanged);
            buttonItemMonthlyScheduleAvgRate.CheckedChanged += new EventHandler(CustomControls.MonthlyScheduleControl.Instance.button_CheckedChanged);
            buttonItemMonthlyScheduleTotalCost.CheckedChanged += new EventHandler(CustomControls.MonthlyScheduleControl.Instance.button_CheckedChanged);
            buttonItemMonthlyScheduleNetRate.CheckedChanged += new EventHandler(CustomControls.MonthlyScheduleControl.Instance.button_CheckedChanged);
            buttonItemMonthlyScheduleDiscount.CheckedChanged += new EventHandler(CustomControls.MonthlyScheduleControl.Instance.button_CheckedChanged);
            buttonItemMonthlyScheduleAdd.Click += new EventHandler(CustomControls.MonthlyScheduleControl.Instance.buttonItemScheduleAdd_Click);
            buttonItemMonthlyScheduleDelete.Click += new EventHandler(CustomControls.MonthlyScheduleControl.Instance.buttonItemScheduleDelete_Click);
            buttonItemMonthlySchedulePowerPoint.Click += new EventHandler(CustomControls.MonthlyScheduleControl.Instance.buttonItemMonthlySchedulePowerPoint_Click);
            buttonItemMonthlyScheduleEmail.Click += new EventHandler(CustomControls.MonthlyScheduleControl.Instance.buttonItemMonthlyScheduleEmail_Click);
            #endregion

            #region Success Models Events
            buttonItemSuccessModelsHelp.Click += new EventHandler(CustomControls.ModelsOfSuccessContainerControl.Instance.buttonItemSuccessModelsHelp_Click);
            #endregion

            if (!string.IsNullOrEmpty(ConfigurationClasses.SettingsManager.Instance.SelectedWizard))
                FormMain.Instance.Text = "TV Schedule Builder - " + ConfigurationClasses.SettingsManager.Instance.SelectedWizard + " - " + ConfigurationClasses.SettingsManager.Instance.Size;
            ribbonControl.Enabled = false;
            using (ToolForms.FormProgress form = new ToolForms.FormProgress())
            {
                form.laProgress.Text = "Chill-Out for a few seconds...\nLoading TV Schedule...";
                form.TopMost = true;
                System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        CustomControls.HomeControl.Instance.LoadSchedule(false);
                        CustomControls.WeeklyScheduleControl.Instance.LoadSchedule(false);
                        CustomControls.MonthlyScheduleControl.Instance.LoadSchedule(false);
                    });
                }));
                thread.Start();

                form.Show();

                while (thread.IsAlive)
                    System.Windows.Forms.Application.DoEvents();
                form.Close();
            }

            ribbonControl.SelectedRibbonTabItem = ribbonTabItemHome;
            ribbonControl_SelectedRibbonTabChanged(null, null);
            ribbonControl.SelectedRibbonTabChanged += new EventHandler(ribbonControl_SelectedRibbonTabChanged);
            ribbonControl.Enabled = true;
        }

        public void ribbonControl_SelectedRibbonTabChanged(object sender, EventArgs e)
        {
            if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemHome)
            {
                if (AllowToLeaveCurrentControl())
                {
                    _currentControl = CustomControls.HomeControl.Instance;
                    if (!pnMain.Controls.Contains(_currentControl))
                        pnMain.Controls.Add(CustomControls.HomeControl.Instance);
                }
                _currentControl.BringToFront();
            }
            else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemWeeklySchedule)
            {
                if (AllowToLeaveCurrentControl())
                {
                    _currentControl = CustomControls.WeeklyScheduleControl.Instance;
                    if (!pnMain.Controls.Contains(_currentControl))
                        pnMain.Controls.Add(CustomControls.WeeklyScheduleControl.Instance);
                }
                _currentControl.BringToFront();
            }
            else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemMonthlySchedule)
            {
                if (AllowToLeaveCurrentControl())
                {
                    _currentControl = CustomControls.MonthlyScheduleControl.Instance;
                    if (!pnMain.Controls.Contains(_currentControl))
                        pnMain.Controls.Add(CustomControls.MonthlyScheduleControl.Instance);
                }
                _currentControl.BringToFront();
            }
            else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemSuccessModels)
            {
                if (AllowToLeaveCurrentControl())
                {
                    _currentControl = CustomControls.ModelsOfSuccessContainerControl.Instance;
                    if (!pnMain.Controls.Contains(_currentControl))
                        pnMain.Controls.Add(CustomControls.ModelsOfSuccessContainerControl.Instance);
                }
                _currentControl.BringToFront();
            }
            pnMain.BringToFront();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool result = true;
            if (_currentControl == CustomControls.HomeControl.Instance)
                result = CustomControls.HomeControl.Instance.AllowToLeaveControl(true);
            else if (_currentControl == CustomControls.WeeklyScheduleControl.Instance)
                result = CustomControls.WeeklyScheduleControl.Instance.AllowToLeaveControl;
            else if (_currentControl == CustomControls.MonthlyScheduleControl.Instance)
                result = CustomControls.MonthlyScheduleControl.Instance.AllowToLeaveControl;
        }

        private void buttonItemHomeExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pnMain_Click(object sender, EventArgs e)
        {
            if ((sender as Control) != null)
                (sender as Control).Focus();
        }

        #region Select All in Editor Handlers
        private bool enter = false;
        private bool needSelect = false;

        public void Editor_Enter(object sender, EventArgs e)
        {
            enter = true;
            BeginInvoke(new MethodInvoker(ResetEnterFlag));
        }

        public void Editor_MouseUp(object sender, MouseEventArgs e)
        {
            if (needSelect)
            {
                (sender as DevExpress.XtraEditors.BaseEdit).SelectAll();
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
