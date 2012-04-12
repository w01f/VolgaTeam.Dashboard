using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace AdScheduleBuilder.CustomControls
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class ScheduleSettingsControl : UserControl
    {
        private static ScheduleSettingsControl _instance;
        private BusinessClasses.Schedule _localSchedule;
        public bool SettingsNotSaved { get; set; }

        private ScheduleSettingsControl()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            this.SettingsNotSaved = false;
            BusinessClasses.ScheduleManager.Instance.SettingsSaved += new EventHandler<BusinessClasses.SavingingEventArgs>((sender, e) =>
            {
                if (sender != this)
                    LoadSchedule(e.QuickSave);
            });
        }

        public static ScheduleSettingsControl Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ScheduleSettingsControl();
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
                if (this.SettingsNotSaved)
                {
                    if (AppManager.ShowWarningQuestion("Your Schedule settings have changed.\nDo you want to save changes?") == System.Windows.Forms.DialogResult.Yes)
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

        private void AssignCloseActiveEditorsonOutSideClick(Control control)
        {
            if (control != FormMain.Instance.comboBoxEditBusinessName
                && control != FormMain.Instance.comboBoxEditClientType
                && control != FormMain.Instance.comboBoxEditDecisionMaker
                && control != FormMain.Instance.listBoxControlCalendar
                && control != FormMain.Instance.comboBoxEditRateCard
                && control != FormMain.Instance.comboBoxEditRateCards
                && control != FormMain.Instance.dateEditFlightDatesEnd
                && control != FormMain.Instance.dateEditFlightDatesStart
                && control != FormMain.Instance.dateEditPresentationDate
                && control != FormMain.Instance.spinEditStandartHeight
                && control != FormMain.Instance.spinEditStandartWidth
                && control != FormMain.Instance.comboBoxEditPercentOfPage
                && control != FormMain.Instance.comboBoxEditRateCard
                && control != FormMain.Instance.comboBoxEditSharePagePageSize
                && control != FormMain.Instance.comboBoxEditStandartPageSize
                && control != FormMain.Instance.checkedListBoxControlSharePageSquare
                && control != FormMain.Instance.spinEditCostPerInch)
            {
                control.Click += new EventHandler(CloseActiveEditorsonOutSideClick);
                foreach (Control childControl in control.Controls)
                    AssignCloseActiveEditorsonOutSideClick(childControl);
            }
        }

        private void CloseActiveEditorsonOutSideClick(object sender, EventArgs e)
        {
            this.Focus();
            gridViewPublications.CloseEditor();
        }

        private void UpdatePublicationsCount()
        {
            laPublications.Text = "Publications: " + _localSchedule.Publications.Count.ToString();
        }

        public void LoadSchedule(bool quickLoad)
        {
            _localSchedule = BusinessClasses.ScheduleManager.Instance.GetLocalSchedule();
            gridControlPublications.DataSource = new BindingList<BusinessClasses.Publication>(_localSchedule.Publications);
            laScheduleName.Text = _localSchedule.Name;
            if (!quickLoad)
            {

                repositoryItemComboBox.Items.Clear();
                repositoryItemComboBox.Items.AddRange(BusinessClasses.ListManager.Instance.PublicationSources.Where(x => !x.Name.Equals("Default")).Select(x => x.Name).Distinct().ToArray());
                FormMain.Instance.comboBoxEditBusinessName.Properties.Items.Clear();
                FormMain.Instance.comboBoxEditBusinessName.Properties.Items.AddRange(BusinessClasses.ListManager.Instance.Advertisers.ToArray());
                FormMain.Instance.comboBoxEditDecisionMaker.Properties.Items.Clear();
                FormMain.Instance.comboBoxEditDecisionMaker.Properties.Items.AddRange(BusinessClasses.ListManager.Instance.DecisionMakers.ToArray());
                FormMain.Instance.comboBoxEditClientType.Properties.Items.Clear();
                FormMain.Instance.comboBoxEditClientType.Properties.Items.AddRange(BusinessClasses.ListManager.Instance.ClientTypes.ToArray());



                FormMain.Instance.comboBoxEditBusinessName.EditValue = _localSchedule.BusinessName;
                FormMain.Instance.comboBoxEditDecisionMaker.EditValue = _localSchedule.DecisionMaker;

                if (!string.IsNullOrEmpty(_localSchedule.ClientType))
                    FormMain.Instance.comboBoxEditClientType.SelectedIndex = FormMain.Instance.comboBoxEditClientType.Properties.Items.IndexOf(_localSchedule.ClientType);

                switch (_localSchedule.SalesStrategy)
                {
                    case BusinessClasses.SalesStrategies.Email:
                        FormMain.Instance.buttonItemSalesStrategyEmail.Checked = true;
                        FormMain.Instance.buttonItemSalesStrategyFax.Checked = false;
                        FormMain.Instance.buttonItemSalesStrategyFaceCall.Checked = false;
                        break;
                    case BusinessClasses.SalesStrategies.Fax:
                        FormMain.Instance.buttonItemSalesStrategyEmail.Checked = false;
                        FormMain.Instance.buttonItemSalesStrategyFax.Checked = true;
                        FormMain.Instance.buttonItemSalesStrategyFaceCall.Checked = false;
                        break;
                    case BusinessClasses.SalesStrategies.InPerson:
                        FormMain.Instance.buttonItemSalesStrategyEmail.Checked = false;
                        FormMain.Instance.buttonItemSalesStrategyFax.Checked = false;
                        FormMain.Instance.buttonItemSalesStrategyFaceCall.Checked = true;
                        break;
                }
                FormMain.Instance.dateEditPresentationDate.EditValue = _localSchedule.PresentationDateObject;
                FormMain.Instance.dateEditFlightDatesStart.EditValue = _localSchedule.FlightDateStartObject;
                FormMain.Instance.dateEditFlightDatesEnd.EditValue = _localSchedule.FlightDateEndObject;

                UpdatePublicationsCount();

                FormMain.Instance.UpdateScheduleTab(_localSchedule.Publications.Count > 0);
                FormMain.Instance.UpdateOutputTabs(_localSchedule.Publications.Select(x => x.Inserts.Count).Sum() > 0);

                LoadView();
            }
            this.SettingsNotSaved = false;
        }

        private void LoadView()
        {
            FormMain.Instance.buttonItemSalesStrategyAbbreviation.Checked = _localSchedule.ViewSettings.HomeViewSettings.ShowCode;
            FormMain.Instance.buttonItemSalesStrategyLogo.Checked = _localSchedule.ViewSettings.HomeViewSettings.ShowLogo;
            FormMain.Instance.buttonItemSalesStrategyDelivery.Checked = _localSchedule.ViewSettings.HomeViewSettings.ShowDelivery;
            FormMain.Instance.buttonItemSalesStrategyReadership.Checked = _localSchedule.ViewSettings.HomeViewSettings.ShowReadership;
        }

        private void SaveView()
        {
            _localSchedule.ViewSettings.HomeViewSettings.ShowCode = FormMain.Instance.buttonItemSalesStrategyAbbreviation.Checked;
            _localSchedule.ViewSettings.HomeViewSettings.ShowLogo = FormMain.Instance.buttonItemSalesStrategyLogo.Checked;
            _localSchedule.ViewSettings.HomeViewSettings.ShowDelivery = FormMain.Instance.buttonItemSalesStrategyDelivery.Checked;
            _localSchedule.ViewSettings.HomeViewSettings.ShowReadership = FormMain.Instance.buttonItemSalesStrategyReadership.Checked;
        }

        private bool AllowToAddPublication()
        {
            gridViewPublications.CloseEditor();
            return FormMain.Instance.comboBoxEditBusinessName.EditValue != null &&
                FormMain.Instance.comboBoxEditDecisionMaker.EditValue != null &&
                FormMain.Instance.comboBoxEditClientType.EditValue != null &&
                FormMain.Instance.dateEditPresentationDate.EditValue != null &&
                FormMain.Instance.dateEditFlightDatesStart.EditValue != null &&
                FormMain.Instance.dateEditFlightDatesEnd.EditValue != null;
        }

        private bool SaveSchedule(string scheduleName = "")
        {
            if (!string.IsNullOrEmpty(scheduleName))
                _localSchedule.Name = scheduleName;
            gridViewPublications.CloseEditor();
            if (FormMain.Instance.comboBoxEditBusinessName.EditValue != null)
                _localSchedule.BusinessName = FormMain.Instance.comboBoxEditBusinessName.EditValue.ToString();
            else
            {
                AppManager.ShowWarning("You must set Business Name before save");
                return false;
            }
            if (FormMain.Instance.comboBoxEditDecisionMaker.EditValue != null)
                _localSchedule.DecisionMaker = FormMain.Instance.comboBoxEditDecisionMaker.EditValue.ToString();
            else
            {
                AppManager.ShowWarning("You must set Owner/Decision-maker before save");
                return false;
            }


            if (FormMain.Instance.comboBoxEditClientType.EditValue != null)
                _localSchedule.ClientType = FormMain.Instance.comboBoxEditClientType.EditValue.ToString();
            else
            {
                AppManager.ShowWarning("You must set Client type before save");
                return false;
            }

            if (FormMain.Instance.buttonItemSalesStrategyEmail.Checked)
                _localSchedule.SalesStrategy = BusinessClasses.SalesStrategies.Email;
            else if (FormMain.Instance.buttonItemSalesStrategyFax.Checked)
                _localSchedule.SalesStrategy = BusinessClasses.SalesStrategies.Fax;
            else if (FormMain.Instance.buttonItemSalesStrategyFaceCall.Checked)
                _localSchedule.SalesStrategy = BusinessClasses.SalesStrategies.InPerson;

            if (FormMain.Instance.dateEditPresentationDate.DateTime != null)
                _localSchedule.PresentationDate = FormMain.Instance.dateEditPresentationDate.DateTime;
            else
            {
                AppManager.ShowWarning("You must set Presentation Date before save");
                return false;
            }
            if (FormMain.Instance.dateEditFlightDatesStart.DateTime != null)
            {
                _localSchedule.FlightDateStart = FormMain.Instance.dateEditFlightDatesStart.DateTime;
                if (_localSchedule.FlightDateStart.DayOfWeek != DayOfWeek.Sunday)
                {
                    AppManager.ShowWarning("Flight Start Date must be Sunday\nFlight End Date must be Saturday\nFlight Start Date must be less then Flight End Date.");
                    return false;
                }
            }
            else
            {
                AppManager.ShowWarning("You must set Flight Start Dates before save");
                return false;
            }
            if (FormMain.Instance.dateEditFlightDatesEnd.DateTime != null)
            {
                _localSchedule.FlightDateEnd = FormMain.Instance.dateEditFlightDatesEnd.DateTime;
                if (_localSchedule.FlightDateEnd.DayOfWeek != DayOfWeek.Saturday || _localSchedule.FlightDateEnd < _localSchedule.FlightDateStart)
                {
                    AppManager.ShowWarning("Flight Start Date must be Sunday\nFlight End Date must be Saturday\nFlight Start Date must be less then Flight End Date.");
                    return false;
                }
            }
            else
            {
                AppManager.ShowWarning("You must set Flight End Dates before save");
                return false;
            }

            foreach (BusinessClasses.Publication publication in _localSchedule.Publications)
                if (string.IsNullOrEmpty(publication.Name))
                {
                    AppManager.ShowWarning("You must Select Name for all Publications before save");
                    return false;
                }

            SaveView();

            repositoryItemComboBox.Items.Clear();
            repositoryItemComboBox.Items.AddRange(BusinessClasses.ListManager.Instance.PublicationSources.Select(x => x.Name).Distinct().ToArray());
            FormMain.Instance.comboBoxEditBusinessName.Properties.Items.Clear();
            FormMain.Instance.comboBoxEditBusinessName.Properties.Items.AddRange(BusinessClasses.ListManager.Instance.Advertisers.ToArray());
            FormMain.Instance.comboBoxEditDecisionMaker.Properties.Items.Clear();
            FormMain.Instance.comboBoxEditDecisionMaker.Properties.Items.AddRange(BusinessClasses.ListManager.Instance.DecisionMakers.ToArray());

            BusinessClasses.ScheduleManager.Instance.SaveSchedule(_localSchedule, false, this);
            laScheduleName.Text = _localSchedule.Name;
            this.SettingsNotSaved = false;
            return true;
        }

        private void UncheckSalesStrategyButtons()
        {
            FormMain.Instance.buttonItemSalesStrategyFaceCall.Checked = false;
            FormMain.Instance.buttonItemSalesStrategyEmail.Checked = false;
            FormMain.Instance.buttonItemSalesStrategyFax.Checked = false;
        }

        private void ScheduleSettingsControl_Load(object sender, EventArgs e)
        {
            repositoryItemComboBox.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            repositoryItemComboBox.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            repositoryItemComboBox.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
            repositoryItemSpinEdit.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            repositoryItemSpinEdit.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            repositoryItemSpinEdit.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
            repositoryItemTextEdit.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            repositoryItemTextEdit.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            repositoryItemTextEdit.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);

            AssignCloseActiveEditorsonOutSideClick(FormMain.Instance.ribbonControl);
            AssignCloseActiveEditorsonOutSideClick(pnHeader);
        }

        public void buttonItemSalesStrategyFaceCall_Click(object sender, EventArgs e)
        {
            UncheckSalesStrategyButtons();
            FormMain.Instance.buttonItemSalesStrategyFaceCall.Checked = true;
        }

        public void buttonItemSalesStrategyEmail_Click(object sender, EventArgs e)
        {
            UncheckSalesStrategyButtons();
            FormMain.Instance.buttonItemSalesStrategyEmail.Checked = true;
        }

        public void buttonItemSalesStrategyFax_Click(object sender, EventArgs e)
        {
            UncheckSalesStrategyButtons();
            FormMain.Instance.buttonItemSalesStrategyFax.Checked = true;
        }


        public void buttonItemPublicationsAdd_Click(object sender, EventArgs e)
        {
            if (AllowToAddPublication())
            {
                _localSchedule.AddPublication();
                ((BindingList<BusinessClasses.Publication>)gridControlPublications.DataSource).ResetBindings();
                gridViewPublications.FocusedRowHandle = gridViewPublications.RowCount - 1;
                UpdatePublicationsCount();
                FormMain.Instance.UpdateScheduleTab(_localSchedule.Publications.Count > 0);
                FormMain.Instance.UpdateOutputTabs(_localSchedule.Publications.Select(x => x.Inserts.Count).Sum() > 0);
                this.SettingsNotSaved = true;
            }
            else
                using (ToolForms.FormAddPublicationWarning form = new ToolForms.FormAddPublicationWarning())
                {
                    form.ShowDialog();
                }
        }

        public void buttonItemPublicationsClone_Click(object sender, EventArgs e)
        {
            if (gridViewPublications.FocusedRowHandle >= 0)
            {
                int newRowHandle = gridViewPublications.FocusedRowHandle + 1;
                ((BindingList<BusinessClasses.Publication>)gridControlPublications.DataSource)[gridViewPublications.GetDataSourceRowIndex(gridViewPublications.FocusedRowHandle)].Clone();
                ((BindingList<BusinessClasses.Publication>)gridControlPublications.DataSource).ResetBindings();
                gridViewPublications.FocusedRowHandle = newRowHandle;
                UpdatePublicationsCount();
                FormMain.Instance.UpdateScheduleTab(_localSchedule.Publications.Count > 0);
                FormMain.Instance.UpdateOutputTabs(_localSchedule.Publications.Select(x => x.Inserts.Count).Sum() > 0);
                this.SettingsNotSaved = true;
            }
        }

        public void buttonItemPublicationsDelete_Click(object sender, EventArgs e)
        {
            if (AppManager.ShowWarningQuestion("Are you SURE you want to DELETE\nthis publication from your schedule?") == DialogResult.Yes)
            {
                gridViewPublications.DeleteSelectedRows();
                _localSchedule.RebuildPublicationIndexes();
                UpdatePublicationsCount();
                FormMain.Instance.UpdateScheduleTab(_localSchedule.Publications.Count > 0);
                FormMain.Instance.UpdateOutputTabs(_localSchedule.Publications.Select(x => x.Inserts.Count).Sum() > 0);
                this.SettingsNotSaved = true;
            }
        }

        public void buttonItemScheduleSettingsSave_Click(object sender, EventArgs e)
        {
            if (SaveSchedule())
                AppManager.ShowInformation("Schedule Saved");
        }

        public void buttonItemScheduleSettingsSaveAs_Click(object sender, EventArgs e)
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

        private void repositoryItemButtonEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            switch (e.Button.Index)
            {
                case 0:
                    _localSchedule.UpPublication(gridViewPublications.GetDataSourceRowIndex(gridViewPublications.FocusedRowHandle));
                    if (gridViewPublications.FocusedRowHandle > 0)
                        gridViewPublications.FocusedRowHandle--;
                    break;
                case 1:
                    _localSchedule.DownPublication(gridViewPublications.GetDataSourceRowIndex(gridViewPublications.FocusedRowHandle));
                    if (gridViewPublications.FocusedRowHandle < gridViewPublications.RowCount - 1)
                        gridViewPublications.FocusedRowHandle++;
                    break;
            }
            this.SettingsNotSaved = true;
        }

        public void FlightDateStartEditValueChanged(object sender, EventArgs e)
        {
            if (FormMain.Instance.dateEditFlightDatesStart.EditValue != null)
            {
                DateTime dateStart = FormMain.Instance.dateEditFlightDatesStart.DateTime;
                while (dateStart.DayOfWeek != DayOfWeek.Saturday)
                    dateStart = dateStart.AddDays(1);
                FormMain.Instance.dateEditFlightDatesEnd.EditValue = dateStart;
            }
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

        public void SchedulePropertyEditValueChanged(object sender, EventArgs e)
        {
            this.SettingsNotSaved = true;
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

        public void buttonItemScheduleSettingsHelp_Click(object sender, EventArgs e)
        {
            BusinessClasses.HelpManager.Instance.OpenHelpLink("home");
        }

        private void gridViewPublications_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == gridColumnName)
            {
                if (e.Value != null)
                {
                    string value = e.Value.ToString();
                    BusinessClasses.PublicationSource publicationSource = BusinessClasses.ListManager.Instance.PublicationSources.Where(x => x.Name.Equals(value)).FirstOrDefault();
                    if (publicationSource != null)
                    {
                        _localSchedule.Publications[gridViewPublications.GetFocusedDataSourceRowIndex()].ApplyDefaultValues();
                        gridViewPublications.RefreshData();
                    }
                }
            }
            SchedulePropertyEditValueChanged(null, null);
        }

        private void repositoryItemComboBox_Closed(object sender, DevExpress.XtraEditors.Controls.ClosedEventArgs e)
        {
            gridViewPublications.CloseEditor();
        }

        private void repositoryItemButtonEditChangeLogo_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            using (ToolForms.FormImageGallery form = new ToolForms.FormImageGallery())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    if (form.SelectedSource != null)
                    {
                        _localSchedule.Publications[gridViewPublications.GetFocusedDataSourceRowIndex()].BigLogo = new Bitmap(form.SelectedSource.BigLogo);
                        _localSchedule.Publications[gridViewPublications.GetFocusedDataSourceRowIndex()].SmallLogo = new Bitmap(form.SelectedSource.SmallLogo);
                        _localSchedule.Publications[gridViewPublications.GetFocusedDataSourceRowIndex()].TinyLogo = new Bitmap(form.SelectedSource.TinyLogo);
                        gridViewPublications.RefreshData();
                    }
                }
            }
        }

        public void buttonItemSalesStrategyAbbreviation_CheckedChanged(object sender, EventArgs e)
        {
            gridBandAbbreviation.Visible = FormMain.Instance.buttonItemSalesStrategyAbbreviation.Checked;
            this.SettingsNotSaved = true;
        }

        public void buttonItemSalesStrategyLogo_CheckedChanged(object sender, EventArgs e)
        {
            gridBandLogo.Visible = FormMain.Instance.buttonItemSalesStrategyLogo.Checked;
            this.SettingsNotSaved = true;
        }

        public void buttonItemSalesStrategyReadership_CheckedChanged(object sender, EventArgs e)
        {
            gridColumnDailyReadership.Visible = FormMain.Instance.buttonItemSalesStrategyReadership.Checked;
            gridColumnSundayReadership.Visible = FormMain.Instance.buttonItemSalesStrategyReadership.Checked;
            if (!FormMain.Instance.buttonItemSalesStrategyReadership.Checked && !FormMain.Instance.buttonItemSalesStrategyDelivery.Checked)
                gridColumnName.RowCount = 2;
            else
                gridColumnName.RowCount = 1;
            this.SettingsNotSaved = true;
        }

        public void buttonItemSalesStrategyDelivery_CheckedChanged(object sender, EventArgs e)
        {
            gridColumnDailyDelivery.Visible = FormMain.Instance.buttonItemSalesStrategyDelivery.Checked;
            gridColumnSundayDelivery.Visible = FormMain.Instance.buttonItemSalesStrategyDelivery.Checked;
            if (!FormMain.Instance.buttonItemSalesStrategyReadership.Checked && !FormMain.Instance.buttonItemSalesStrategyDelivery.Checked)
                gridColumnName.RowCount = 2;
            else
                gridColumnName.RowCount = 1;
            this.SettingsNotSaved = true;
        }
    }
}
