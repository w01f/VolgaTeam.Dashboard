using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CalendarBuilder
{
    public partial class FormMain : Form
    {
        private static FormMain _instance = null;
        private Control _currentControl = null;

        private FormMain()
        {
            InitializeComponent();
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
                ribbonBarSalesStrategy.RecalcLayout();
                ribbonBarSuccessModels.RecalcLayout();
                ribbonBarSuccessModelsExit.RecalcLayout();
                ribbonBarSuccessModelsHelp.RecalcLayout();
                ribbonBarCalendarEmail.RecalcLayout();
                ribbonBarCalendarExit.RecalcLayout();
                ribbonBarCalendarHelp.RecalcLayout();
                ribbonBarCalendarPowerPoint.RecalcLayout();
                ribbonBarCalendarSave.RecalcLayout();
                ribbonPanelHome.PerformLayout();
                ribbonPanelSuccessModels.PerformLayout();
                ribbonPanelCalendar.PerformLayout();
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
                if (CustomControls.HomeControl.Instance.AllowToLeaveControl)
                    result = true;
                else
                {
                    ribbonControl.SelectedRibbonTabChanged -= new EventHandler(ribbonControl_SelectedRibbonTabChanged);
                    ribbonControl.SelectedRibbonTabItem = ribbonTabItemHome;
                    ribbonControl.SelectedRibbonTabChanged += new EventHandler(ribbonControl_SelectedRibbonTabChanged);
                }
            }
            else if ((_currentControl == CustomControls.CalendarControl.Instance))
            {
                if (CustomControls.CalendarControl.Instance.AllowToLeaveControl)
                    result = true;
                else
                {
                    ribbonControl.SelectedRibbonTabChanged -= new EventHandler(ribbonControl_SelectedRibbonTabChanged);
                    ribbonControl.SelectedRibbonTabItem = ribbonTabItemCalendar;
                    ribbonControl.SelectedRibbonTabChanged += new EventHandler(ribbonControl_SelectedRibbonTabChanged);
                }
            }
            else
                result = true;
            return result;
        }

        public void UpdateScheduleTabs(bool enable)
        {
            ribbonTabItemCalendar.Enabled = enable;
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

            #region Calendar Events
            listBoxControlCalendar.SelectedIndexChanged += new EventHandler(CustomControls.CalendarControl.Instance.imageListBoxEditCalendar_SelectedIndexChanged);
            buttonItemCalendarSlideInfo.CheckedChanged += new EventHandler(CustomControls.CalendarControl.Instance.buttonItemCalendarSlideInfo_CheckedChanged);
            buttonItemCalendarGrid.CheckedChanged += new EventHandler(CustomControls.CalendarControl.Instance.buttonItemCalendarGrid_CheckedChanged);
            buttonItemCalendarCopy.Click += new EventHandler(CustomControls.CalendarControl.Instance.buttonItemCalendarCopy_Click);
            buttonItemCalendarPaste.Click += new EventHandler(CustomControls.CalendarControl.Instance.buttonItemCalendarPaste_Click);
            buttonItemCalendarSave.Click += new EventHandler(CustomControls.CalendarControl.Instance.buttonItemScheduleSave_Click);
            buttonItemCalendarSaveAs.Click += new EventHandler(CustomControls.CalendarControl.Instance.buttonItemScheduleSaveAs_Click);
            buttonItemCalendarPowerPoint.Click += new EventHandler(CustomControls.CalendarControl.Instance.buttonItemWeeklySchedulePowerPoint_Click);
            buttonItemCalendarEmail.Click += new EventHandler(CustomControls.CalendarControl.Instance.buttonItemWeeklyScheduleEmail_Click);
            buttonItemCalendarHelp.Click += new EventHandler(CustomControls.CalendarControl.Instance.buttonItemScheduleHelp_Click);
            #endregion

            #region Success Models Events
            buttonItemSuccessModelsHelp.Click += new EventHandler(CustomControls.ModelsOfSuccessContainerControl.Instance.buttonItemSuccessModelsHelp_Click);
            #endregion

            if (!string.IsNullOrEmpty(ConfigurationClasses.SettingsManager.Instance.SelectedWizard))
                FormMain.Instance.Text = "Quick Calendar - " + ConfigurationClasses.SettingsManager.Instance.SelectedWizard + " - " + ConfigurationClasses.SettingsManager.Instance.Size;
            ribbonControl.Enabled = false;
            using (ToolForms.FormProgress form = new ToolForms.FormProgress())
            {
                form.laProgress.Text = "Chill-Out for a few seconds...\nLoading Quick Calendar...";
                form.TopMost = true;
                System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        CustomControls.HomeControl.Instance.LoadCalendar(false);
                        System.Windows.Forms.Application.DoEvents();
                        CustomControls.CalendarControl.Instance.LoadCalendar(false);
                        System.Windows.Forms.Application.DoEvents();
                    });
                }));

                form.Show();
                System.Windows.Forms.Application.DoEvents();
                
                thread.Start();

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
            else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemCalendar)
            {
                if (AllowToLeaveCurrentControl())
                {
                    _currentControl = CustomControls.CalendarControl.Instance;
                    if (!pnMain.Controls.Contains(_currentControl))
                        pnMain.Controls.Add(CustomControls.CalendarControl.Instance);
                    CustomControls.CalendarControl.Instance.LoadSlideInfoState();
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
                result = CustomControls.HomeControl.Instance.AllowToLeaveControl;
            else if (_currentControl == CustomControls.CalendarControl.Instance)
                result = CustomControls.CalendarControl.Instance.AllowToLeaveControl;
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
