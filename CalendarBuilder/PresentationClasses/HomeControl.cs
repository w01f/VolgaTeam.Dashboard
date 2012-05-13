using System;
using System.Drawing;
using System.Windows.Forms;

namespace CalendarBuilder.PresentationClasses
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class HomeControl : UserControl
    {
        private static HomeControl _instance = null;
        private bool _allowTosave = false;
        private BusinessClasses.Schedule _localCalendar;
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
                    LoadCalendar(e.QuickSave);
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

        public bool AllowToLeaveControl
        {
            get
            {
                bool result = false;
                if (this.SettingsNotSaved)
                {
                    if (SaveCalendar())
                        result = true;
                }
                else
                    result = true;
                return result;
            }
        }

        #region Common Methods
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
        #endregion

        private void UncheckSalesStrategyButtons()
        {
            FormMain.Instance.buttonItemHomeSalesStrategyFaceCall.Checked = false;
            FormMain.Instance.buttonItemHomeSalesStrategyEmail.Checked = false;
            FormMain.Instance.buttonItemHomeSalesStrategyFax.Checked = false;
        }

        #region Calendar Methods
        public void LoadCalendar(bool quickLoad)
        {
            _localCalendar = BusinessClasses.ScheduleManager.Instance.GetLocalCalendar();

            if (!quickLoad)
            {
                _allowTosave = false;

                FormMain.Instance.comboBoxEditBusinessName.Properties.Items.Clear();
                FormMain.Instance.comboBoxEditBusinessName.Properties.Items.AddRange(BusinessClasses.ListManager.Instance.Advertisers.ToArray());
                FormMain.Instance.comboBoxEditDecisionMaker.Properties.Items.Clear();
                FormMain.Instance.comboBoxEditDecisionMaker.Properties.Items.AddRange(BusinessClasses.ListManager.Instance.DecisionMakers.ToArray());
                FormMain.Instance.comboBoxEditClientType.Properties.Items.Clear();
                FormMain.Instance.comboBoxEditClientType.Properties.Items.AddRange(BusinessClasses.ListManager.Instance.ClientTypes.ToArray());

                FormMain.Instance.comboBoxEditBusinessName.EditValue = !string.IsNullOrEmpty(_localCalendar.BusinessName) ? _localCalendar.BusinessName : null;
                FormMain.Instance.comboBoxEditDecisionMaker.EditValue = !string.IsNullOrEmpty(_localCalendar.DecisionMaker) ? _localCalendar.DecisionMaker : null;
                if (!string.IsNullOrEmpty(_localCalendar.ClientType))
                    FormMain.Instance.comboBoxEditClientType.SelectedIndex = FormMain.Instance.comboBoxEditClientType.Properties.Items.IndexOf(_localCalendar.ClientType);
                switch (_localCalendar.SalesStrategy)
                {
                    case BusinessClasses.SalesStrategy.Email:
                        FormMain.Instance.buttonItemHomeSalesStrategyEmail.Checked = true;
                        FormMain.Instance.buttonItemHomeSalesStrategyFax.Checked = false;
                        FormMain.Instance.buttonItemHomeSalesStrategyFaceCall.Checked = false;
                        break;
                    case BusinessClasses.SalesStrategy.Fax:
                        FormMain.Instance.buttonItemHomeSalesStrategyEmail.Checked = false;
                        FormMain.Instance.buttonItemHomeSalesStrategyFax.Checked = true;
                        FormMain.Instance.buttonItemHomeSalesStrategyFaceCall.Checked = false;
                        break;
                    case BusinessClasses.SalesStrategy.InPerson:
                        FormMain.Instance.buttonItemHomeSalesStrategyEmail.Checked = false;
                        FormMain.Instance.buttonItemHomeSalesStrategyFax.Checked = false;
                        FormMain.Instance.buttonItemHomeSalesStrategyFaceCall.Checked = true;
                        break;
                }

                FormMain.Instance.dateEditPresentationDate.EditValue = _localCalendar.PresentationDate;
                FormMain.Instance.dateEditFlightDatesStart.EditValue = _localCalendar.FlightDateStart;
                FormMain.Instance.dateEditFlightDatesEnd.EditValue = _localCalendar.FlightDateEnd;
                FormMain.Instance.ribbonPanelHome.PerformLayout();
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

        private bool SaveCalendar(string scheduleName = "")
        {
            bool quickSave = true;

            if (!string.IsNullOrEmpty(scheduleName))
                _localCalendar.Name = scheduleName;
            if (FormMain.Instance.comboBoxEditBusinessName.EditValue != null)
                _localCalendar.BusinessName = FormMain.Instance.comboBoxEditBusinessName.EditValue.ToString();
            else
            {
                AppManager.ShowWarning("Your calendar is missing important information!\nPlease make sure you have a Business Name before you proceed.");
                return false;
            }
            if (FormMain.Instance.comboBoxEditDecisionMaker.EditValue != null)
                _localCalendar.DecisionMaker = FormMain.Instance.comboBoxEditDecisionMaker.EditValue.ToString();
            else
            {
                AppManager.ShowWarning("Your calendar is missing important information!\nPlease make sure you have a Owner/Decision-maker before you proceed.");
                return false;
            }

            if (FormMain.Instance.comboBoxEditClientType.EditValue != null)
                _localCalendar.ClientType = FormMain.Instance.comboBoxEditClientType.EditValue.ToString();
            else
            {
                AppManager.ShowWarning("You must set Client type before save");
                return false;
            }

            if (FormMain.Instance.buttonItemHomeSalesStrategyEmail.Checked)
                _localCalendar.SalesStrategy = BusinessClasses.SalesStrategy.Email;
            else if (FormMain.Instance.buttonItemHomeSalesStrategyFax.Checked)
                _localCalendar.SalesStrategy = BusinessClasses.SalesStrategy.Fax;
            else if (FormMain.Instance.buttonItemHomeSalesStrategyFaceCall.Checked)
                _localCalendar.SalesStrategy = BusinessClasses.SalesStrategy.InPerson;

            if (FormMain.Instance.dateEditPresentationDate.EditValue != null)
                _localCalendar.PresentationDate = FormMain.Instance.dateEditPresentationDate.DateTime;
            else
            {
                AppManager.ShowWarning("Your calendar is missing important information!\nPlease make sure you have a Presentation Date before you proceed.");
                return false;
            }

            if (FormMain.Instance.dateEditFlightDatesStart.EditValue != null && FormMain.Instance.dateEditFlightDatesEnd.EditValue != null)
            {
                DateTime startDate = FormMain.Instance.dateEditFlightDatesStart.DateTime;
                DateTime endDate = FormMain.Instance.dateEditFlightDatesEnd.DateTime;
                if (startDate.DayOfWeek != DayOfWeek.Sunday)
                {
                    AppManager.ShowWarning("Campaign Start Date must be Sunday\nCampaign End Date must be Saturday\nCampaign Start Date must be less then Campaign End Date.");
                    return false;
                }
                if (endDate.DayOfWeek != DayOfWeek.Saturday || _localCalendar.FlightDateEnd < _localCalendar.FlightDateStart)
                {
                    AppManager.ShowWarning("Campaign Start Date must be Sunday\nCampaign End Date must be Saturday\nCampaign Start Date must be less then Campaign End Date.");
                    return false;
                }

                if (_localCalendar.FlightDateStart != startDate || _localCalendar.FlightDateEnd != endDate)
                    quickSave = false;
                _localCalendar.FlightDateStart = startDate;
                _localCalendar.FlightDateEnd = endDate;
            }
            else
            {
                AppManager.ShowWarning("Your calendar is missing important information!\nPlease make sure you have a Campaign Dates before you proceed.");
                return false;
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
            BusinessClasses.ScheduleManager.Instance.SaveCalendar(_localCalendar, quickSave, this);
            this.SettingsNotSaved = false;
            return true;
        }
        #endregion

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
                    while (dateStart.DayOfWeek != DayOfWeek.Saturday)
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
                    while (temp.DayOfWeek != DayOfWeek.Sunday)
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
                    while (temp.DayOfWeek != DayOfWeek.Saturday)
                        temp = temp.AddDays(1);
                    e.Value = temp;
                }
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
            if (SaveCalendar())
                AppManager.ShowInformation("Calendar Saved");
        }

        public void buttonItemHomeSaveAs_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.InitialDirectory = ConfigurationClasses.SettingsManager.Instance.SaveFolder;
                dialog.Title = "Save Calendar As...";
                dialog.Filter = "Calendar Files|*.xml";
                dialog.FileName = _localCalendar.Name + ".xml";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    if (SaveCalendar(dialog.FileName.Replace(".xml", "")))
                        AppManager.ShowInformation("Calendar was saved");
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

        #region Calendars Clicks
        private void pbAdvancedCalendar_Click(object sender, EventArgs e)
        {
            if (FormMain.Instance.ribbonTabItemAdvancedCalendar.Enabled)
                FormMain.Instance.ribbonTabItemAdvancedCalendar.Select();
        }

        private void pbGraphicCalendar_Click(object sender, EventArgs e)
        {
            if (FormMain.Instance.ribbonTabItemGraphicCalendar.Enabled)
                FormMain.Instance.ribbonTabItemGraphicCalendar.Select();
        }

        private void pbSimpleCalendar_Click(object sender, EventArgs e)
        {
            if (FormMain.Instance.ribbonTabItemSimpleCalendar.Enabled)
                FormMain.Instance.ribbonTabItemSimpleCalendar.Select();
        }

        private void pbTVCalendar_Click(object sender, EventArgs e)
        {
            if (FormMain.Instance.ribbonTabItemTVCalendar.Enabled)
                FormMain.Instance.ribbonTabItemTVCalendar.Select();
        }
        #endregion
    }
}
