using System;
using System.Drawing;
using System.Windows.Forms;

namespace TVScheduleBuilder.CustomControls
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class HomeControl : UserControl
    {
        private static HomeControl _instance = null;
        private bool _allowTosave = false;
        private BusinessClasses.Schedule _localSchedule;
        public bool SettingsNotSaved { get; set; }

        private HomeControl()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            if ((base.CreateGraphics()).DpiX > 96)
            {
            }
            BusinessClasses.ScheduleManager.Instance.SettingsSaved += new EventHandler<BusinessClasses.SavingingEventArgs>((sender, e) =>
            {
                if (sender != this)
                    LoadSchedule(e.QuickSave);
            });
        }

        public static HomeControl Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new HomeControl();
                return _instance;
            }
        }

        public static void RemoveInstance()
        {
            try
            {
                _instance.Dispose();
            }
            catch
            {
            }
            finally
            {
                _instance = null;
            }
        }

        public bool AllowToLeaveControl
        {
            get
            {
                bool result = false;
                if (this.SettingsNotSaved || stationsControl.HasChanged || daypartsControl.HasChanged)
                {
                    if (AppManager.ShowWarningQuestion("Schedule settings have changed.\nDo you want to save changes?") == System.Windows.Forms.DialogResult.Yes)
                    {
                        if (SaveSchedule())
                            result = true;
                    }
                }
                else
                    result = true;
                return result;
            }
        }

        private void UncheckSalesStrategyButtons()
        {
            FormMain.Instance.buttonItemHomeSalesStrategyFaceCall.Checked = false;
            FormMain.Instance.buttonItemHomeSalesStrategyEmail.Checked = false;
            FormMain.Instance.buttonItemHomeSalesStrategyFax.Checked = false;
        }

        public void LoadSchedule(bool quickLoad)
        {
            _localSchedule = BusinessClasses.ScheduleManager.Instance.GetLocalSchedule();

            if (!quickLoad)
            {
                _allowTosave = false;

                FormMain.Instance.comboBoxEditBusinessName.Properties.Items.Clear();
                FormMain.Instance.comboBoxEditBusinessName.Properties.Items.AddRange(BusinessClasses.ListManager.Instance.Advertisers.ToArray());
                FormMain.Instance.comboBoxEditDecisionMaker.Properties.Items.Clear();
                FormMain.Instance.comboBoxEditDecisionMaker.Properties.Items.AddRange(BusinessClasses.ListManager.Instance.DecisionMakers.ToArray());
                FormMain.Instance.comboBoxEditClientType.Properties.Items.Clear();
                FormMain.Instance.comboBoxEditClientType.Properties.Items.AddRange(BusinessClasses.ListManager.Instance.ClientTypes.ToArray());

                FormMain.Instance.comboBoxEditBusinessName.EditValue = !string.IsNullOrEmpty(_localSchedule.BusinessName) ? _localSchedule.BusinessName : null;
                FormMain.Instance.comboBoxEditDecisionMaker.EditValue = !string.IsNullOrEmpty(_localSchedule.DecisionMaker) ? _localSchedule.DecisionMaker : null;
                if (!string.IsNullOrEmpty(_localSchedule.ClientType))
                    FormMain.Instance.comboBoxEditClientType.SelectedIndex = FormMain.Instance.comboBoxEditClientType.Properties.Items.IndexOf(_localSchedule.ClientType);
                switch (_localSchedule.SalesStrategy)
                {
                    case BusinessClasses.SalesStrategies.Email:
                        FormMain.Instance.buttonItemHomeSalesStrategyEmail.Checked = true;
                        FormMain.Instance.buttonItemHomeSalesStrategyFax.Checked = false;
                        FormMain.Instance.buttonItemHomeSalesStrategyFaceCall.Checked = false;
                        break;
                    case BusinessClasses.SalesStrategies.Fax:
                        FormMain.Instance.buttonItemHomeSalesStrategyEmail.Checked = false;
                        FormMain.Instance.buttonItemHomeSalesStrategyFax.Checked = true;
                        FormMain.Instance.buttonItemHomeSalesStrategyFaceCall.Checked = false;
                        break;
                    case BusinessClasses.SalesStrategies.InPerson:
                        FormMain.Instance.buttonItemHomeSalesStrategyEmail.Checked = false;
                        FormMain.Instance.buttonItemHomeSalesStrategyFax.Checked = false;
                        FormMain.Instance.buttonItemHomeSalesStrategyFaceCall.Checked = true;
                        break;
                }

                FormMain.Instance.dateEditPresentationDate.EditValue = _localSchedule.PresentationDate;
                FormMain.Instance.dateEditFlightDatesStart.EditValue = _localSchedule.FlightDateStart;
                FormMain.Instance.dateEditFlightDatesEnd.EditValue = _localSchedule.FlightDateEnd;

                FormMain.Instance.checkBoxItemHomeDemoNo.Checked = !_localSchedule.UseDemo;
                FormMain.Instance.checkBoxItemHomeDemoCustom.Checked = _localSchedule.UseDemo & !_localSchedule.ImportDemo;
                FormMain.Instance.checkBoxItemHomeDemoImport.Checked = _localSchedule.UseDemo & _localSchedule.ImportDemo;
                FormMain.Instance.itemContainerHomeDemoOptions.Visible = _localSchedule.UseDemo;
                FormMain.Instance.labelItemHomeDemoSeparator.Visible = _localSchedule.UseDemo;
                FormMain.Instance.itemContainerHomeDemoType.Visible = !_localSchedule.ImportDemo;
                FormMain.Instance.comboBoxEditDemo.Properties.Items.Clear();
                FormMain.Instance.comboBoxEditDemo.Properties.Items.AddRange(_localSchedule.ImportDemo ? BusinessClasses.ListManager.Instance.Demos : BusinessClasses.ListManager.Instance.CustomDemos);
                FormMain.Instance.comboBoxEditDemo.Enabled = FormMain.Instance.comboBoxEditDemo.Properties.Items.Count > 0;
                FormMain.Instance.checkBoxItemHomeDemoImport.Enabled = BusinessClasses.ListManager.Instance.Demos.Count > 0;
                FormMain.Instance.checkBoxItemHomeDemoImport.TextColor = BusinessClasses.ListManager.Instance.Demos.Count > 0 ? Color.Black : Color.Gray;
                FormMain.Instance.comboBoxEditDemo.EditValue = !string.IsNullOrEmpty(_localSchedule.Demo) && (FormMain.Instance.comboBoxEditDemo.Properties.Items.Count > 0 || _localSchedule.ImportDemo) ? _localSchedule.Demo : null;

                FormMain.Instance.comboBoxEditSource.Properties.Items.Clear();
                FormMain.Instance.comboBoxEditSource.Properties.Items.AddRange(BusinessClasses.ListManager.Instance.Sources);
                FormMain.Instance.checkBoxItemSource.Checked = _localSchedule.UseSource & _localSchedule.UseDemo;
                FormMain.Instance.comboBoxEditSource.Enabled = _localSchedule.UseSource;
                FormMain.Instance.comboBoxEditSource.EditValue = !string.IsNullOrEmpty(_localSchedule.Source) && FormMain.Instance.comboBoxEditSource.Properties.Items.Count > 0 && _localSchedule.UseSource ? _localSchedule.Source : null;

                FormMain.Instance.buttonItemHomeRating.Checked = _localSchedule.RatingAsCPP;
                FormMain.Instance.buttonItemHome000s.Checked = !_localSchedule.RatingAsCPP;
                FormMain.Instance.ribbonBarHomeDemo.RecalcLayout();
                FormMain.Instance.ribbonPanelHome.PerformLayout();

                stationsControl.LoadData(_localSchedule);
                daypartsControl.LoadData(_localSchedule);

                _allowTosave = true;
            }
            FormMain.Instance.UpdateScheduleTabs(FormMain.Instance.comboBoxEditBusinessName.EditValue != null &
                    FormMain.Instance.comboBoxEditDecisionMaker.EditValue != null &
                    FormMain.Instance.comboBoxEditClientType.EditValue != null &
                    FormMain.Instance.dateEditPresentationDate.EditValue != null &
                    FormMain.Instance.dateEditFlightDatesStart.EditValue != null &
                    FormMain.Instance.dateEditFlightDatesEnd.EditValue != null);
            this.SettingsNotSaved = false;
        }

        private bool SaveSchedule(string scheduleName = "")
        {
            bool quickSave = true;

            if (!string.IsNullOrEmpty(scheduleName))
                _localSchedule.Name = scheduleName;
            if (FormMain.Instance.comboBoxEditBusinessName.EditValue != null)
                _localSchedule.BusinessName = FormMain.Instance.comboBoxEditBusinessName.EditValue.ToString();
            else
            {
                AppManager.ShowWarning("Your schedule is missing important information!\nPlease make sure you have a Business Name before you proceed.");
                return false;
            }
            if (FormMain.Instance.comboBoxEditDecisionMaker.EditValue != null)
                _localSchedule.DecisionMaker = FormMain.Instance.comboBoxEditDecisionMaker.EditValue.ToString();
            else
            {
                AppManager.ShowWarning("Your schedule is missing important information!\nPlease make sure you have a Owner/Decision-maker before you proceed.");
                return false;
            }

            if (FormMain.Instance.comboBoxEditClientType.EditValue != null)
                _localSchedule.ClientType = FormMain.Instance.comboBoxEditClientType.EditValue.ToString();
            else
            {
                AppManager.ShowWarning("You must set Client type before save");
                return false;
            }

            if (FormMain.Instance.buttonItemHomeSalesStrategyEmail.Checked)
                _localSchedule.SalesStrategy = BusinessClasses.SalesStrategies.Email;
            else if (FormMain.Instance.buttonItemHomeSalesStrategyFax.Checked)
                _localSchedule.SalesStrategy = BusinessClasses.SalesStrategies.Fax;
            else if (FormMain.Instance.buttonItemHomeSalesStrategyFaceCall.Checked)
                _localSchedule.SalesStrategy = BusinessClasses.SalesStrategies.InPerson;

            if (FormMain.Instance.dateEditPresentationDate.EditValue != null)
                _localSchedule.PresentationDate = FormMain.Instance.dateEditPresentationDate.DateTime;
            else
            {
                AppManager.ShowWarning("Your schedule is missing important information!\nPlease make sure you have a Presentation Date before you proceed.");
                return false;
            }

            if (FormMain.Instance.dateEditFlightDatesStart.EditValue != null && FormMain.Instance.dateEditFlightDatesEnd.EditValue != null)
            {
                DateTime startDate = FormMain.Instance.dateEditFlightDatesStart.DateTime;
                DateTime endDate = FormMain.Instance.dateEditFlightDatesEnd.DateTime;
                if (startDate.DayOfWeek != DayOfWeek.Monday)
                {
                    AppManager.ShowWarning("Flight Start Date must be Monday\nFlight End Date must be Sunday\nFlight Start Date must be less then Flight End Date.");
                    return false;
                }
                if (endDate.DayOfWeek != DayOfWeek.Sunday || _localSchedule.FlightDateEnd < _localSchedule.FlightDateStart)
                {
                    AppManager.ShowWarning("Flight Start Date must be Monday\nFlight End Date must be Sunday\nFlight Start Date must be less then Flight End Date.");
                    return false;
                }
                if (_localSchedule.FlightDateStart.HasValue && _localSchedule.FlightDateEnd.HasValue)
                {
                    if (_localSchedule.FlightDateStart.Value != startDate || _localSchedule.FlightDateEnd.Value != endDate)
                    {
                        if (AppManager.ShowWarningQuestion("Flight Dates have been changed and all Spots will be recreated\nDo you want to proceed?") == DialogResult.Yes)
                            quickSave = false;
                        else
                            return false;
                    }
                }
                else
                    quickSave = false;
                _localSchedule.FlightDateStart = startDate;
                _localSchedule.FlightDateEnd = endDate;

                _localSchedule.WeeklySchedule.ShowRating = _localSchedule.WeeklySchedule.ShowRating & _localSchedule.UseDemo;
                _localSchedule.WeeklySchedule.ShowCPP = _localSchedule.WeeklySchedule.ShowCPP & _localSchedule.UseDemo;
                _localSchedule.WeeklySchedule.ShowGRP = _localSchedule.WeeklySchedule.ShowGRP & _localSchedule.UseDemo;
                _localSchedule.WeeklySchedule.ShowTotalCPP = _localSchedule.WeeklySchedule.ShowTotalCPP & _localSchedule.UseDemo;
                _localSchedule.WeeklySchedule.ShowTotalGRP = _localSchedule.WeeklySchedule.ShowTotalGRP & _localSchedule.UseDemo;

                _localSchedule.MonthlySchedule.ShowRating = _localSchedule.MonthlySchedule.ShowRating & _localSchedule.UseDemo;
                _localSchedule.MonthlySchedule.ShowCPP = _localSchedule.MonthlySchedule.ShowCPP & _localSchedule.UseDemo;
                _localSchedule.MonthlySchedule.ShowGRP = _localSchedule.MonthlySchedule.ShowGRP & _localSchedule.UseDemo;
                _localSchedule.MonthlySchedule.ShowTotalCPP = _localSchedule.MonthlySchedule.ShowTotalCPP & _localSchedule.UseDemo;
                _localSchedule.MonthlySchedule.ShowTotalGRP = _localSchedule.MonthlySchedule.ShowTotalGRP & _localSchedule.UseDemo;

                if (!quickSave)
                {
                    _localSchedule.WeeklySchedule.RebuildSpots();
                    _localSchedule.MonthlySchedule.RebuildSpots();
                }
            }
            else
            {
                AppManager.ShowWarning("Your schedule is missing important information!\nPlease make sure you have a Flight Dates before you proceed.");
                return false;
            }

            if (FormMain.Instance.comboBoxEditDemo.EditValue == null && !FormMain.Instance.checkBoxItemHomeDemoNo.Checked)
            {
                AppManager.ShowWarning("Please select Demo or disable it before you proceed.");
                return false;
            }

            if (stationsControl.HasChanged)
            {
                _localSchedule.Stations.Clear();
                _localSchedule.Stations.AddRange(stationsControl.GetData());
            }

            if (daypartsControl.HasChanged)
            {
                _localSchedule.Dayparts.Clear();
                _localSchedule.Dayparts.AddRange(daypartsControl.GetData());
            }

            FormMain.Instance.comboBoxEditBusinessName.Properties.Items.Clear();
            FormMain.Instance.comboBoxEditBusinessName.Properties.Items.AddRange(BusinessClasses.ListManager.Instance.Advertisers.ToArray());
            FormMain.Instance.comboBoxEditDecisionMaker.Properties.Items.Clear();
            FormMain.Instance.comboBoxEditDecisionMaker.Properties.Items.AddRange(BusinessClasses.ListManager.Instance.DecisionMakers.ToArray());
            FormMain.Instance.UpdateScheduleTabs(FormMain.Instance.comboBoxEditBusinessName.EditValue != null &
                FormMain.Instance.comboBoxEditDecisionMaker.EditValue != null &
                FormMain.Instance.comboBoxEditClientType.EditValue != null &
                FormMain.Instance.dateEditPresentationDate.EditValue != null &
                FormMain.Instance.dateEditFlightDatesStart.EditValue != null &
                FormMain.Instance.dateEditFlightDatesEnd.EditValue != null);
            BusinessClasses.ScheduleManager.Instance.SaveSchedule(_localSchedule, quickSave, this);
            this.SettingsNotSaved = false;
            stationsControl.HasChanged = false;
            daypartsControl.HasChanged = false;
            return true;
        }

        #region Toggles Switch Events
        public void buttonItemHomeSalesStrategyFaceCall_Click(object sender, EventArgs e)
        {
            UncheckSalesStrategyButtons();
            FormMain.Instance.buttonItemHomeSalesStrategyFaceCall.Checked = true;
        }

        public void buttonItemHomeSalesStrategyEmail_Click(object sender, EventArgs e)
        {
            UncheckSalesStrategyButtons();
            FormMain.Instance.buttonItemHomeSalesStrategyEmail.Checked = true;
        }

        public void buttonItemHomeSalesStrategyFax_Click(object sender, EventArgs e)
        {
            UncheckSalesStrategyButtons();
            FormMain.Instance.buttonItemHomeSalesStrategyFax.Checked = true;
        }

        public void buttonItemHomeDemos_CheckedChanged(object sender, DevComponents.DotNetBar.CheckBoxChangeEventArgs e)
        {
            if (_allowTosave)
            {
                _localSchedule.UseDemo = FormMain.Instance.checkBoxItemHomeDemoCustom.Checked | FormMain.Instance.checkBoxItemHomeDemoImport.Checked;

                _localSchedule.ImportDemo = FormMain.Instance.checkBoxItemHomeDemoImport.Checked;
                FormMain.Instance.itemContainerHomeDemoOptions.Visible = _localSchedule.UseDemo;
                FormMain.Instance.labelItemHomeDemoSeparator.Visible = _localSchedule.UseDemo;
                FormMain.Instance.itemContainerHomeDemoType.Visible = !_localSchedule.ImportDemo;
                if (!_localSchedule.UseDemo)
                    FormMain.Instance.comboBoxEditDemo.EditValue = null;

                _localSchedule.UseSource = FormMain.Instance.checkBoxItemSource.Checked & _localSchedule.UseDemo;
                FormMain.Instance.comboBoxEditSource.Enabled = _localSchedule.UseSource;
                FormMain.Instance.comboBoxEditSource.EditValue = !string.IsNullOrEmpty(_localSchedule.Source) && FormMain.Instance.comboBoxEditSource.Properties.Items.Count > 0 && _localSchedule.UseSource ? _localSchedule.Source : null;
                if (!_localSchedule.UseDemo)
                {
                    _allowTosave = false;
                    FormMain.Instance.checkBoxItemSource.Checked = false;
                    _allowTosave = true;
                }

                FormMain.Instance.comboBoxEditDemo.Properties.Items.Clear();
                FormMain.Instance.comboBoxEditDemo.Properties.Items.AddRange(_localSchedule.ImportDemo ? BusinessClasses.ListManager.Instance.Demos : BusinessClasses.ListManager.Instance.CustomDemos);
                FormMain.Instance.comboBoxEditDemo.Enabled = FormMain.Instance.comboBoxEditDemo.Properties.Items.Count > 0;
                FormMain.Instance.comboBoxEditDemo.EditValue = !string.IsNullOrEmpty(_localSchedule.Demo) && (FormMain.Instance.comboBoxEditDemo.Properties.Items.Count > 0 || _localSchedule.ImportDemo) ? _localSchedule.Demo : null;
                FormMain.Instance.ribbonBarHomeDemo.RecalcLayout();
                FormMain.Instance.ribbonPanelHome.PerformLayout();
                this.SettingsNotSaved = true;
            }
        }

        public void buttonItemHomeRatings_Click(object sender, EventArgs e)
        {
            FormMain.Instance.buttonItemHome000s.Checked = false;
            FormMain.Instance.buttonItemHomeRating.Checked = false;
            (sender as DevComponents.DotNetBar.ButtonItem).Checked = true;
        }

        public void buttonItemHomeRatings_CheckedChanged(object sender, EventArgs e)
        {
            if (_allowTosave)
            {
                _localSchedule.RatingAsCPP = FormMain.Instance.buttonItemHomeRating.Checked;
                this.SettingsNotSaved = true;
            }
        }
        #endregion

        #region Editors Events
        public void SchedulePropertyEditValueChanged(object sender, EventArgs e)
        {
            if (_allowTosave)
            {
                FormMain.Instance.UpdateScheduleTabs(FormMain.Instance.comboBoxEditBusinessName.EditValue != null &
                    FormMain.Instance.comboBoxEditDecisionMaker.EditValue != null &
                    FormMain.Instance.comboBoxEditClientType.EditValue != null &
                    FormMain.Instance.dateEditPresentationDate.EditValue != null &
                    FormMain.Instance.dateEditFlightDatesStart.EditValue != null &
                    FormMain.Instance.dateEditFlightDatesEnd.EditValue != null);
                this.SettingsNotSaved = true;
            }
        }

        public void FlightDateStartEditValueChanged(object sender, EventArgs e)
        {
            if (FormMain.Instance.dateEditFlightDatesStart.EditValue != null && _allowTosave)
            {
                DateTime dateStart = FormMain.Instance.dateEditFlightDatesStart.DateTime;
                this.SettingsNotSaved = true;
                if (FormMain.Instance.dateEditFlightDatesEnd.EditValue == null)
                {
                    while (dateStart.DayOfWeek != DayOfWeek.Sunday)
                        dateStart = dateStart.AddDays(1);
                    FormMain.Instance.dateEditFlightDatesEnd.EditValue = dateStart;
                }
            }
            SchedulePropertyEditValueChanged(null, null);
        }

        public void FlightDateEndEditValueChanged(object sender, EventArgs e)
        {
            if (FormMain.Instance.dateEditFlightDatesStart.EditValue != null && _allowTosave)
                this.SettingsNotSaved = true;
            SchedulePropertyEditValueChanged(null, null);
        }

        public void CalcWeeksOnFlightDatesChange(object sender, EventArgs e)
        {
            FormMain.Instance.labelItemFlightDatesWeeks.Text = "";
            FormMain.Instance.labelItemFlightDatesWeeks.Visible = false;
            if (FormMain.Instance.dateEditFlightDatesStart.DateTime != null && FormMain.Instance.dateEditFlightDatesEnd.DateTime != null)
            {
                TimeSpan datesRange = FormMain.Instance.dateEditFlightDatesEnd.DateTime - FormMain.Instance.dateEditFlightDatesStart.DateTime;
                int weeksCount = datesRange.Days / 7 + 1;
                FormMain.Instance.labelItemFlightDatesWeeks.Text = weeksCount.ToString() + (weeksCount > 1 ? " Weeks" : " Week");
                FormMain.Instance.labelItemFlightDatesWeeks.Visible = true;
            }
        }

        public void dateEditFlightDatesStart_CloseUp(object sender, DevExpress.XtraEditors.Controls.CloseUpEventArgs e)
        {
            if (e.Value != null)
            {
                DateTime temp = DateTime.MinValue;
                if (DateTime.TryParse(e.Value.ToString(), out temp))
                {
                    while (temp.DayOfWeek != DayOfWeek.Monday)
                        temp = temp.AddDays(-1);
                    e.Value = temp;
                }
            }
        }

        public void dateEditFlightDatesEnd_CloseUp(object sender, DevExpress.XtraEditors.Controls.CloseUpEventArgs e)
        {
            if (e.Value != null)
            {
                DateTime temp = DateTime.MinValue;
                if (DateTime.TryParse(e.Value.ToString(), out temp))
                {
                    while (temp.DayOfWeek != DayOfWeek.Sunday)
                        temp = temp.AddDays(1);
                    e.Value = temp;
                }
            }
        }

        public void comboBoxEditDemo_EditValueChanged(object sender, EventArgs e)
        {
            if (_allowTosave)
            {
                _localSchedule.Demo = FormMain.Instance.comboBoxEditDemo.EditValue != null ? FormMain.Instance.comboBoxEditDemo.EditValue.ToString() : string.Empty;
                if (_localSchedule.ImportDemo)
                {
                    if (_localSchedule.Demo.Contains("Rtg"))
                    {
                        FormMain.Instance.buttonItemHomeRating.Checked = true;
                        FormMain.Instance.buttonItemHome000s.Checked = false;
                    }
                    else if (_localSchedule.Demo.Contains("(000)"))
                    {
                        FormMain.Instance.buttonItemHomeRating.Checked = false;
                        FormMain.Instance.buttonItemHome000s.Checked = true;
                    }
                }
                this.SettingsNotSaved = true;
            }
        }

        public void comboBoxEditSource_EditValueChanged(object sender, EventArgs e)
        {
            if (_allowTosave)
            {
                _localSchedule.Source = FormMain.Instance.comboBoxEditSource.EditValue != null ? FormMain.Instance.comboBoxEditSource.EditValue.ToString() : string.Empty;
                this.SettingsNotSaved = true;
            }
        }
        #endregion

        #region Ribbon Operations Events
        public void buttonItemHomeHelp_Click(object sender, EventArgs e)
        {
            BusinessClasses.HelpManager.Instance.OpenHelpLink("Home");
        }

        public void buttonItemHomeSave_Click(object sender, EventArgs e)
        {
            if (SaveSchedule())
                AppManager.ShowInformation("Schedule Saved");
        }

        public void buttonItemHomeSaveAs_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.InitialDirectory = ConfigurationClasses.SettingsManager.Instance.SaveFolder;
                dialog.Title = "Save Schedule As...";
                dialog.Filter = "Schedule Files|*.xml";
                dialog.FileName = _localSchedule.Name + ".xml";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    if (SaveSchedule(dialog.FileName.Replace(".xml", "")))
                        AppManager.ShowInformation("Schedule was saved");
                }
            }
        }
        #endregion

        #region Picture Box Clicks Habdlers
        /// <summary>
        /// Buttonize the PictureBox 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            PictureBox pic = (PictureBox)(sender);
            pic.Top += 1;
        }

        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            PictureBox pic = (PictureBox)(sender);
            pic.Top -= 1;
        }
        #endregion

        #region Buttons Clicks Events
        private void pbWeeklySchedule_Click(object sender, EventArgs e)
        {
            FormMain.Instance.ribbonTabItemWeeklySchedule.Select();
        }

        private void pbMonthlySchedule_Click(object sender, EventArgs e)
        {
            FormMain.Instance.ribbonTabItemMonthlySchedule.Select();
        }
        #endregion

        #region Common Methods and Event Handlers
        private void pnRight_Resize(object sender, EventArgs e)
        {
            pnStations.Height = pnRight.Height / 2;
        }
        #endregion
    }
}
