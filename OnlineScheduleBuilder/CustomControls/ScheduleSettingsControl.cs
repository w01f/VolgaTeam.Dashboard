using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace OnlineScheduleBuilder.CustomControls
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class ScheduleSettingsControl : UserControl
    {
        private static ScheduleSettingsControl _instance = null;
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

        private void AssignCloseActiveEditorsOnOutSideClick(Control control)
        {
            if (control != FormMain.Instance.comboBoxEditBusinessName
                && control != FormMain.Instance.comboBoxEditDecisionMaker
                && control != FormMain.Instance.dateEditPresentationDate
                && control != FormMain.Instance.dateEditFlightDatesStart
                && control != FormMain.Instance.dateEditFlightDatesEnd)
            {
                control.Click += new EventHandler(CloseActiveEditorsonOutSideClick);
                foreach (Control childControl in control.Controls)
                    AssignCloseActiveEditorsOnOutSideClick(childControl);
            }
        }

        private void CloseActiveEditorsonOutSideClick(object sender, EventArgs e)
        {
            this.Focus();
            advBandedGridViewProducts.CloseEditor();
        }

        private void UpdateProductsCount()
        {
            laProducts.Text = "Web Sales Products: " + _localSchedule.Products.Count.ToString();
        }

        public void LoadSchedule(bool quickLoad)
        {
            _localSchedule = BusinessClasses.ScheduleManager.Instance.GetLocalSchedule();
            gridControlProducts.DataSource = new BindingList<BusinessClasses.Product>(_localSchedule.Products);
            if (!quickLoad)
            {
                laScheduleName.Text = _localSchedule.Name;

                FormMain.Instance.comboBoxEditBusinessName.Properties.Items.Clear();
                FormMain.Instance.comboBoxEditBusinessName.Properties.Items.AddRange(BusinessClasses.ListManager.Instance.Advertisers.ToArray());
                FormMain.Instance.comboBoxEditDecisionMaker.Properties.Items.Clear();
                FormMain.Instance.comboBoxEditDecisionMaker.Properties.Items.AddRange(BusinessClasses.ListManager.Instance.DecisionMakers.ToArray());

                FormMain.Instance.comboBoxEditBusinessName.EditValue = _localSchedule.BusinessName;
                FormMain.Instance.comboBoxEditDecisionMaker.EditValue = _localSchedule.DecisionMaker;

                FormMain.Instance.dateEditPresentationDate.EditValue = _localSchedule.PresentationDateObject;
                FormMain.Instance.dateEditFlightDatesStart.EditValue = _localSchedule.FlightDateStartObject;
                FormMain.Instance.dateEditFlightDatesEnd.EditValue = _localSchedule.FlightDateEndObject;

                UpdateProductsCount();

                FormMain.Instance.UpdateSimpleOutputTabPageState(_localSchedule.Products.Count > 0);
                FormMain.Instance.UpdateSummaryOutputTabPageState(_localSchedule.Products.Count > 1);
            }
            this.SettingsNotSaved = false;
        }

        private bool AllowToAddPublication()
        {
            advBandedGridViewProducts.CloseEditor();
            return FormMain.Instance.comboBoxEditBusinessName.EditValue != null &&
                FormMain.Instance.comboBoxEditDecisionMaker.EditValue != null &&
                FormMain.Instance.dateEditPresentationDate.EditValue != null &&
                FormMain.Instance.dateEditFlightDatesStart.EditValue != null &&
                FormMain.Instance.dateEditFlightDatesEnd.EditValue != null;
        }

        private bool SaveSchedule(string scheduleName = "")
        {
            if (!string.IsNullOrEmpty(scheduleName))
                _localSchedule.Name = scheduleName;
            advBandedGridViewProducts.CloseEditor();
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

            if (FormMain.Instance.dateEditPresentationDate.DateTime != null)
                _localSchedule.PresentationDate = FormMain.Instance.dateEditPresentationDate.DateTime;
            else
            {
                AppManager.ShowWarning("Your schedule is missing important information!\nPlease make sure you have a Presentation Date before you proceed.");
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
                AppManager.ShowWarning("Your schedule is missing important information!\nPlease make sure you have a Flight Start Date before you proceed.");
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
                AppManager.ShowWarning("Your schedule is missing important information!\nPlease make sure you have a Flight End Date before you proceed.");
                return false;
            }

            foreach (BusinessClasses.Product publication in _localSchedule.Products)
                if (string.IsNullOrEmpty(publication.Name))
                {
                    AppManager.ShowWarning("Your schedule is missing important information!\nPlease make sure you have a Web Product in each line before you proceed.");
                    return false;
                }

            FormMain.Instance.comboBoxEditBusinessName.Properties.Items.Clear();
            FormMain.Instance.comboBoxEditBusinessName.Properties.Items.AddRange(BusinessClasses.ListManager.Instance.Advertisers.ToArray());
            FormMain.Instance.comboBoxEditDecisionMaker.Properties.Items.Clear();
            FormMain.Instance.comboBoxEditDecisionMaker.Properties.Items.AddRange(BusinessClasses.ListManager.Instance.DecisionMakers.ToArray());

            _localSchedule.ProductPackage.UpdateWebProducts();
            BusinessClasses.ScheduleManager.Instance.SaveSchedule(_localSchedule, false, this);

            laScheduleName.Text = _localSchedule.Name;
            this.SettingsNotSaved = false;
            return true;
        }

        private void RefreshDataAfterAddProduct()
        {
            ((BindingList<BusinessClasses.Product>)gridControlProducts.DataSource).ResetBindings();
            advBandedGridViewProducts.FocusedRowHandle = advBandedGridViewProducts.RowCount - 1;
            UpdateProductsCount();
            FormMain.Instance.UpdateSimpleOutputTabPageState(_localSchedule.Products.Count > 0);
            FormMain.Instance.UpdateSummaryOutputTabPageState(_localSchedule.Products.Count > 1);
            this.SettingsNotSaved = true;
        }

        private void ScheduleSettingsControl_Load(object sender, EventArgs e)
        {
            repositoryItemComboBoxProductType.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            repositoryItemComboBoxProductType.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            repositoryItemComboBoxProductType.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
            repositoryItemComboBoxProductNames.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            repositoryItemComboBoxProductNames.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            repositoryItemComboBoxProductNames.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
            repositoryItemSpinEditSize.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            repositoryItemSpinEditSize.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            repositoryItemSpinEditSize.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);

            AssignCloseActiveEditorsOnOutSideClick(FormMain.Instance.ribbonControl);
            AssignCloseActiveEditorsOnOutSideClick(pnHeader);
        }

        public void buttonItemProductAddProduct_Click(object sender, EventArgs e)
        {
            BusinessClasses.Category category = (sender as DevComponents.DotNetBar.ButtonItem).Tag as BusinessClasses.Category;
            _localSchedule.AddProduct(category.Name);
            RefreshDataAfterAddProduct();
        }

        public void buttonItemScheduleSettingsSave_Click(object sender, EventArgs e)
        {
            if (SaveSchedule())
                AppManager.ShowInformation("Schedule Saved");
        }

        public void buttonItemScheduleHelp_Click(object sender, EventArgs e)
        {
            BusinessClasses.HelpManager.Instance.OpenHelpLink("Home");
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
                    _localSchedule.UpProduct(advBandedGridViewProducts.GetDataSourceRowIndex(advBandedGridViewProducts.FocusedRowHandle));
                    if (advBandedGridViewProducts.FocusedRowHandle > 0)
                        advBandedGridViewProducts.FocusedRowHandle--;
                    break;
                case 1:
                    _localSchedule.DownProduct(advBandedGridViewProducts.GetDataSourceRowIndex(advBandedGridViewProducts.FocusedRowHandle));
                    if (advBandedGridViewProducts.FocusedRowHandle < advBandedGridViewProducts.RowCount - 1)
                        advBandedGridViewProducts.FocusedRowHandle++;
                    break;
            }
            this.SettingsNotSaved = true;
        }

        private void repositoryItemButtonEditDelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (AppManager.ShowWarningQuestion("Are you sure you want to delete this line?") == DialogResult.Yes)
            {
                advBandedGridViewProducts.DeleteSelectedRows();
                _localSchedule.RebuildProductIndexes();
                UpdateProductsCount();
                RefreshDataAfterAddProduct();
                this.SettingsNotSaved = true;
            }
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

        private void gridViewProducts_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == gridColumnName)
            {
                if (e.Value != null)
                {
                    string value = e.Value.ToString();
                    BusinessClasses.ProductSource productSource = BusinessClasses.ListManager.Instance.ProductSources.Where(x => x.Name.Equals(value)).FirstOrDefault();
                    if (productSource != null)
                    {
                        _localSchedule.Products[advBandedGridViewProducts.GetFocusedDataSourceRowIndex()].ApplyDefaultValues();
                        advBandedGridViewProducts.RefreshData();
                    }
                }
            }
            SchedulePropertyEditValueChanged(null, null);
        }

        private void repositoryItemComboBoxProductName_Closed(object sender, DevExpress.XtraEditors.Controls.ClosedEventArgs e)
        {
            advBandedGridViewProducts.CloseEditor();
        }

        private void gridViewProducts_ShowingEditor(object sender, CancelEventArgs e)
        {
            e.Cancel = false;
            if (advBandedGridViewProducts.FocusedColumn == gridColumnName)
            {
                string category = _localSchedule.Products[advBandedGridViewProducts.GetFocusedDataSourceRowIndex()].Category;
                string subCategory = _localSchedule.Products[advBandedGridViewProducts.GetFocusedDataSourceRowIndex()].SubCategory;
                repositoryItemComboBoxProductNames.Items.Clear();
                repositoryItemComboBoxProductNames.Items.AddRange(BusinessClasses.ListManager.Instance.ProductSources.Where(x => x.Category.Name.Equals(category) && (x.SubCategory.Equals(subCategory) || string.IsNullOrEmpty(subCategory))).Select(x => x.Name).Distinct().ToArray());
            }
            else if (advBandedGridViewProducts.FocusedColumn == gridColumnType)
            {
                string category = _localSchedule.Products[advBandedGridViewProducts.GetFocusedDataSourceRowIndex()].Category;
                string subCategory = _localSchedule.Products[advBandedGridViewProducts.GetFocusedDataSourceRowIndex()].SubCategory;
                string[] subCategories = BusinessClasses.ListManager.Instance.ProductSources.Where(x => x.Category.Name.Equals(category) && !string.IsNullOrEmpty(x.SubCategory)).Select(x => x.SubCategory).Distinct().ToArray();
                if (subCategories.Length > 0)
                {
                    repositoryItemComboBoxProductType.Items.Clear();
                    repositoryItemComboBoxProductType.Items.AddRange(subCategories);
                }
                else
                    e.Cancel = true;
            }
        }

        private void repositoryItemComboBoxProductType_CloseUp(object sender, DevExpress.XtraEditors.Controls.CloseUpEventArgs e)
        {
            if (e.CloseMode == DevExpress.XtraEditors.PopupCloseMode.Normal)
            {
                e.AcceptValue = false;
                advBandedGridViewProducts.SetFocusedRowCellValue(gridColumnSubCategory, e.Value);
                advBandedGridViewProducts.CloseEditor();
            }
        }
    }
}
