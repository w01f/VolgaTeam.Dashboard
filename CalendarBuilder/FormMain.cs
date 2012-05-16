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
                ribbonBarHomeSalesStrategy.RecalcLayout();
                ribbonBarSuccessModels.RecalcLayout();
                ribbonBarSuccessModelsExit.RecalcLayout();
                ribbonBarSuccessModelsHelp.RecalcLayout();
                ribbonBarAdvancedCalendarEmail.RecalcLayout();
                ribbonBarAdvancedCalendarExit.RecalcLayout();
                ribbonBarAdvancedCalendarHelp.RecalcLayout();
                ribbonBarAdvancedCalendarPowerPoint.RecalcLayout();
                ribbonBarAdvancedCalendarSave.RecalcLayout();
                ribbonPanelHome.PerformLayout();
                ribbonPanelSuccessModels.PerformLayout();
                ribbonPanelAdvancedCalendar.PerformLayout();
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
            if ((_currentControl == PresentationClasses.HomeControl.Instance))
            {
                if (PresentationClasses.HomeControl.Instance.AllowToLeaveControl)
                    result = true;
                else
                {
                    ribbonControl.SelectedRibbonTabChanged -= new EventHandler(ribbonControl_SelectedRibbonTabChanged);
                    ribbonControl.SelectedRibbonTabItem = ribbonTabItemHome;
                    ribbonControl.SelectedRibbonTabChanged += new EventHandler(ribbonControl_SelectedRibbonTabChanged);
                }
            }
            else if (_currentControl == PresentationClasses.CalendarVisualizer.Instance.SelectedCalendarControl)
            {
                if (PresentationClasses.CalendarVisualizer.Instance.SelectedCalendarControl != null)
                    PresentationClasses.CalendarVisualizer.Instance.SelectedCalendarControl.LeaveCalendar();
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

        private void FormMain_Shown(object sender, EventArgs e)
        {
            #region Home Events
            buttonItemHomeHelp.Click += new EventHandler(PresentationClasses.HomeControl.Instance.buttonItemHomeHelp_Click);
            buttonItemHomeSave.Click += new EventHandler(PresentationClasses.HomeControl.Instance.buttonItemHomeSave_Click);
            buttonItemHomeSaveAs.Click += new EventHandler(PresentationClasses.HomeControl.Instance.buttonItemHomeSaveAs_Click);
            comboBoxEditBusinessName.EditValueChanged += new EventHandler(PresentationClasses.HomeControl.Instance.SchedulePropertyEditValueChanged);
            comboBoxEditDecisionMaker.EditValueChanged += new EventHandler(PresentationClasses.HomeControl.Instance.SchedulePropertyEditValueChanged);
            comboBoxEditClientType.EditValueChanged += new EventHandler(PresentationClasses.HomeControl.Instance.SchedulePropertyEditValueChanged);
            buttonItemHomeSalesStrategyEmail.Click += new EventHandler(PresentationClasses.HomeControl.Instance.buttonItemHomeSalesStrategyEmail_Click);
            buttonItemHomeSalesStrategyFaceCall.Click += new EventHandler(PresentationClasses.HomeControl.Instance.buttonItemHomeSalesStrategyFaceCall_Click);
            buttonItemHomeSalesStrategyFax.Click += new EventHandler(PresentationClasses.HomeControl.Instance.buttonItemHomeSalesStrategyFax_Click);
            buttonItemHomeSalesStrategyEmail.CheckedChanged += new EventHandler(PresentationClasses.HomeControl.Instance.SchedulePropertyEditValueChanged);
            buttonItemHomeSalesStrategyFax.CheckedChanged += new EventHandler(PresentationClasses.HomeControl.Instance.SchedulePropertyEditValueChanged);
            buttonItemHomeSalesStrategyFaceCall.CheckedChanged += new EventHandler(PresentationClasses.HomeControl.Instance.SchedulePropertyEditValueChanged);
            buttonItemHomeCalendarTypeSunday.Click += new EventHandler(PresentationClasses.HomeControl.Instance.buttonItemHomeCalendarType_Click);
            buttonItemHomeCalendarTypeMonday.Click += new EventHandler(PresentationClasses.HomeControl.Instance.buttonItemHomeCalendarType_Click);
            buttonItemHomeCalendarTypeSunday.CheckedChanged += new EventHandler(PresentationClasses.HomeControl.Instance.buttonItemHomeCalendarType_CheckedChanged);
            buttonItemHomeCalendarTypeMonday.CheckedChanged += new EventHandler(PresentationClasses.HomeControl.Instance.buttonItemHomeCalendarType_CheckedChanged);
            buttonItemHomeProductsDigital.CheckedChanged += new EventHandler(PresentationClasses.HomeControl.Instance.buttonItemHomeProducts_CheckedChanged);
            buttonItemHomeProductsNewspaper.CheckedChanged += new EventHandler(PresentationClasses.HomeControl.Instance.buttonItemHomeProducts_CheckedChanged);
            buttonItemHomeProductsTV.CheckedChanged += new EventHandler(PresentationClasses.HomeControl.Instance.buttonItemHomeProducts_CheckedChanged);
            buttonItemHomeProductsRadio.CheckedChanged += new EventHandler(PresentationClasses.HomeControl.Instance.buttonItemHomeProducts_CheckedChanged);
            dateEditPresentationDate.EditValueChanged += new EventHandler(PresentationClasses.HomeControl.Instance.SchedulePropertyEditValueChanged);
            dateEditFlightDatesStart.EditValueChanged += new EventHandler(PresentationClasses.HomeControl.Instance.FlightDateStartEditValueChanged);
            dateEditFlightDatesEnd.EditValueChanged += new EventHandler(PresentationClasses.HomeControl.Instance.FlightDateEndEditValueChanged);
            dateEditFlightDatesStart.EditValueChanged += new EventHandler(PresentationClasses.HomeControl.Instance.CalcWeeksOnFlightDatesChange);
            dateEditFlightDatesEnd.EditValueChanged += new EventHandler(PresentationClasses.HomeControl.Instance.CalcWeeksOnFlightDatesChange);
            dateEditFlightDatesStart.CloseUp += new DevExpress.XtraEditors.Controls.CloseUpEventHandler(PresentationClasses.HomeControl.Instance.dateEditFlightDatesStart_CloseUp);
            dateEditFlightDatesEnd.CloseUp += new DevExpress.XtraEditors.Controls.CloseUpEventHandler(PresentationClasses.HomeControl.Instance.dateEditFlightDatesEnd_CloseUp);
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

            #region Advanced Calendar Events
            listBoxControlAdvancedCalendar.SelectedIndexChanged += new EventHandler(PresentationClasses.CalendarVisualizer.Instance.imageListBoxEditCalendar_SelectedIndexChanged);
            buttonItemAdvancedCalendarSlideInfo.CheckedChanged += new EventHandler(PresentationClasses.CalendarVisualizer.Instance.buttonItemCalendarSlideInfo_CheckedChanged);
            buttonItemAdvancedCalendarMonth.Click += new EventHandler(PresentationClasses.CalendarVisualizer.Instance.buttonItemCalendarView_Click);
            buttonItemAdvancedCalendarGrid.Click += new EventHandler(PresentationClasses.CalendarVisualizer.Instance.buttonItemCalendarView_Click);
            buttonItemAdvancedCalendarMonth.CheckedChanged += new EventHandler(PresentationClasses.CalendarVisualizer.Instance.buttonItemCalendarView_CheckedChanged);
            buttonItemAdvancedCalendarGrid.CheckedChanged += new EventHandler(PresentationClasses.CalendarVisualizer.Instance.buttonItemCalendarView_CheckedChanged);
            buttonItemAdvancedCalendarCopy.Click += new EventHandler(PresentationClasses.CalendarVisualizer.Instance.buttonItemCalendarCopy_Click);
            buttonItemAdvancedCalendarPaste.Click += new EventHandler(PresentationClasses.CalendarVisualizer.Instance.buttonItemCalendarPaste_Click);
            buttonItemAdvancedCalendarClone.Click += new EventHandler(PresentationClasses.CalendarVisualizer.Instance.buttonItemCalendarClone_Click);
            buttonItemAdvancedCalendarSave.Click += new EventHandler(PresentationClasses.CalendarVisualizer.Instance.buttonItemScheduleSave_Click);
            buttonItemAdvancedCalendarSaveAs.Click += new EventHandler(PresentationClasses.CalendarVisualizer.Instance.buttonItemScheduleSaveAs_Click);
            buttonItemAdvancedCalendarPowerPoint.Click += new EventHandler(PresentationClasses.CalendarVisualizer.Instance.buttonItemWeeklySchedulePowerPoint_Click);
            buttonItemAdvancedCalendarEmail.Click += new EventHandler(PresentationClasses.CalendarVisualizer.Instance.buttonItemWeeklyScheduleEmail_Click);
            buttonItemAdvancedCalendarHelp.Click += new EventHandler(PresentationClasses.CalendarVisualizer.Instance.buttonItemScheduleHelp_Click);
            #endregion

            #region Graphic Calendar Events
            listBoxControlGraphicCalendar.SelectedIndexChanged += new EventHandler(PresentationClasses.CalendarVisualizer.Instance.imageListBoxEditCalendar_SelectedIndexChanged);
            buttonItemGraphicCalendarSlideInfo.CheckedChanged += new EventHandler(PresentationClasses.CalendarVisualizer.Instance.buttonItemCalendarSlideInfo_CheckedChanged);
            buttonItemGraphicCalendarMonth.Click += new EventHandler(PresentationClasses.CalendarVisualizer.Instance.buttonItemCalendarView_Click);
            buttonItemGraphicCalendarGrid.Click += new EventHandler(PresentationClasses.CalendarVisualizer.Instance.buttonItemCalendarView_Click);
            buttonItemGraphicCalendarMonth.CheckedChanged += new EventHandler(PresentationClasses.CalendarVisualizer.Instance.buttonItemCalendarView_CheckedChanged);
            buttonItemGraphicCalendarGrid.CheckedChanged += new EventHandler(PresentationClasses.CalendarVisualizer.Instance.buttonItemCalendarView_CheckedChanged);
            buttonItemGraphicCalendarCopy.Click += new EventHandler(PresentationClasses.CalendarVisualizer.Instance.buttonItemCalendarCopy_Click);
            buttonItemGraphicCalendarPaste.Click += new EventHandler(PresentationClasses.CalendarVisualizer.Instance.buttonItemCalendarPaste_Click);
            buttonItemGraphicCalendarClone.Click += new EventHandler(PresentationClasses.CalendarVisualizer.Instance.buttonItemCalendarClone_Click);
            buttonItemGraphicCalendarSave.Click += new EventHandler(PresentationClasses.CalendarVisualizer.Instance.buttonItemScheduleSave_Click);
            buttonItemGraphicCalendarSaveAs.Click += new EventHandler(PresentationClasses.CalendarVisualizer.Instance.buttonItemScheduleSaveAs_Click);
            buttonItemGraphicCalendarPowerPoint.Click += new EventHandler(PresentationClasses.CalendarVisualizer.Instance.buttonItemWeeklySchedulePowerPoint_Click);
            buttonItemGraphicCalendarEmail.Click += new EventHandler(PresentationClasses.CalendarVisualizer.Instance.buttonItemWeeklyScheduleEmail_Click);
            buttonItemGraphicCalendarHelp.Click += new EventHandler(PresentationClasses.CalendarVisualizer.Instance.buttonItemScheduleHelp_Click);
            #endregion

            #region Simple Calendar Events
            listBoxControlSimpleCalendar.SelectedIndexChanged += new EventHandler(PresentationClasses.CalendarVisualizer.Instance.imageListBoxEditCalendar_SelectedIndexChanged);
            buttonItemSimpleCalendarSlideInfo.CheckedChanged += new EventHandler(PresentationClasses.CalendarVisualizer.Instance.buttonItemCalendarSlideInfo_CheckedChanged);
            buttonItemSimpleCalendarMonth.Click += new EventHandler(PresentationClasses.CalendarVisualizer.Instance.buttonItemCalendarView_Click);
            buttonItemSimpleCalendarGrid.Click += new EventHandler(PresentationClasses.CalendarVisualizer.Instance.buttonItemCalendarView_Click);
            buttonItemSimpleCalendarMonth.CheckedChanged += new EventHandler(PresentationClasses.CalendarVisualizer.Instance.buttonItemCalendarView_CheckedChanged);
            buttonItemSimpleCalendarGrid.CheckedChanged += new EventHandler(PresentationClasses.CalendarVisualizer.Instance.buttonItemCalendarView_CheckedChanged);
            buttonItemSimpleCalendarCopy.Click += new EventHandler(PresentationClasses.CalendarVisualizer.Instance.buttonItemCalendarCopy_Click);
            buttonItemSimpleCalendarPaste.Click += new EventHandler(PresentationClasses.CalendarVisualizer.Instance.buttonItemCalendarPaste_Click);
            buttonItemSimpleCalendarClone.Click += new EventHandler(PresentationClasses.CalendarVisualizer.Instance.buttonItemCalendarClone_Click);
            buttonItemSimpleCalendarSave.Click += new EventHandler(PresentationClasses.CalendarVisualizer.Instance.buttonItemScheduleSave_Click);
            buttonItemSimpleCalendarSaveAs.Click += new EventHandler(PresentationClasses.CalendarVisualizer.Instance.buttonItemScheduleSaveAs_Click);
            buttonItemSimpleCalendarPowerPoint.Click += new EventHandler(PresentationClasses.CalendarVisualizer.Instance.buttonItemWeeklySchedulePowerPoint_Click);
            buttonItemSimpleCalendarEmail.Click += new EventHandler(PresentationClasses.CalendarVisualizer.Instance.buttonItemWeeklyScheduleEmail_Click);
            buttonItemSimpleCalendarHelp.Click += new EventHandler(PresentationClasses.CalendarVisualizer.Instance.buttonItemScheduleHelp_Click);
            #endregion

            #region Success Models Events
            buttonItemSuccessModelsHelp.Click += new EventHandler(PresentationClasses.ModelsOfSuccessContainerControl.Instance.buttonItemSuccessModelsHelp_Click);
            #endregion

            if (!string.IsNullOrEmpty(ConfigurationClasses.SettingsManager.Instance.SelectedWizard))
                FormMain.Instance.Text = "Ninja Calendar BETA - " + ConfigurationClasses.SettingsManager.Instance.SelectedWizard + " - " + ConfigurationClasses.SettingsManager.Instance.Size;
            ribbonControl.Enabled = false;
            using (ToolForms.FormProgress form = new ToolForms.FormProgress())
            {
                form.laProgress.Text = "Chill-Out for a few seconds...\nLoading Ninja Calendar...";
                form.TopMost = true;
                System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        PresentationClasses.HomeControl.Instance.LoadCalendar(false);
                        System.Windows.Forms.Application.DoEvents();
                        PresentationClasses.CalendarVisualizer.Instance.LoadData();
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
            pnEmpty.BringToFront();
            if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemHome)
            {
                if (AllowToLeaveCurrentControl())
                {
                    _currentControl = PresentationClasses.HomeControl.Instance;
                    if (!pnMain.Controls.Contains(_currentControl as Control))
                        pnMain.Controls.Add(_currentControl);
                }
                _currentControl.BringToFront();
            }
            else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemAdvancedCalendar)
            {
                if (AllowToLeaveCurrentControl())
                    _currentControl = PresentationClasses.CalendarVisualizer.Instance.SelectCalendar(pnMain,BusinessClasses.CalendarStyle.Advanced) as Control;
                _currentControl.BringToFront();
            }
            else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemGraphicCalendar)
            {
                if (AllowToLeaveCurrentControl())
                    _currentControl = PresentationClasses.CalendarVisualizer.Instance.SelectCalendar(pnMain,BusinessClasses.CalendarStyle.Graphic) as Control;
                _currentControl.BringToFront();
            }
            else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemSimpleCalendar)
            {
                if (AllowToLeaveCurrentControl())
                    _currentControl = PresentationClasses.CalendarVisualizer.Instance.SelectCalendar(pnMain, BusinessClasses.CalendarStyle.Simple) as Control;
                _currentControl.BringToFront();
            }
            else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemSuccessModels)
            {
                if (AllowToLeaveCurrentControl())
                {
                    _currentControl = PresentationClasses.ModelsOfSuccessContainerControl.Instance;
                    if (!pnMain.Controls.Contains(_currentControl))
                        pnMain.Controls.Add(PresentationClasses.ModelsOfSuccessContainerControl.Instance);
                }
                _currentControl.BringToFront();
            }
            pnMain.BringToFront();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool result = true;
            if (_currentControl == PresentationClasses.HomeControl.Instance)
                result = PresentationClasses.HomeControl.Instance.AllowToLeaveControl;
            else if (_currentControl == PresentationClasses.CalendarVisualizer.Instance.SelectedCalendarControl)
                PresentationClasses.CalendarVisualizer.Instance.SelectedCalendarControl.LeaveCalendar();
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
