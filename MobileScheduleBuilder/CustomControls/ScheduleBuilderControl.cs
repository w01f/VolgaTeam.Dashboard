using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MobileScheduleBuilder.CustomControls
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class ScheduleBuilderControl : UserControl
    {
        private static ScheduleBuilderControl _instance;
        private List<ProductControl> _tabPages = new List<ProductControl>();

        public bool SettingsNotSaved { get; set; }
        public bool AllowApplyValues { get; set; }
        public BusinessClasses.Schedule LocalSchedule { get; set; }

        private ScheduleBuilderControl()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            this.AllowApplyValues = false;
            BusinessClasses.ScheduleManager.Instance.SettingsSaved += new EventHandler<BusinessClasses.SavingingEventArgs>((sender, e) =>
            {
                if (sender != this)
                    LoadSchedule(e.QuickSave);
            });

            if ((base.CreateGraphics()).DpiX > 96)
            {
                Font font = new Font(styleController.Appearance.Font.FontFamily, styleController.Appearance.Font.Size - 2, styleController.Appearance.Font.Style);
                styleController.Appearance.Font = font;
                styleController.AppearanceDisabled.Font = font;
                styleController.AppearanceDropDown.Font = font;
                styleController.AppearanceDropDownHeader.Font = font;
                styleController.AppearanceFocused.Font = font;
                styleController.AppearanceReadOnly.Font = font;
                comboBoxEditSlideHeader.Font = font;
                labelControlAdvertiser.Font = font;
                labelControlPresentationDate.Font = font;
                //checkEditApplyForAllProducts.Font = font;
                labelControlOutputStatus.Font = new Font(labelControlOutputStatus.Font.FontFamily, labelControlOutputStatus.Font.Size - 3, labelControlOutputStatus.Font.Style);
                labelControlFormula.Font = new Font(labelControlFormula.Font.FontFamily, labelControlFormula.Font.Size - 2, labelControlFormula.Font.Style);
                checkEditFormulaCPM.Font = new Font(checkEditFormulaCPM.Font.FontFamily, checkEditFormulaCPM.Font.Size - 2, checkEditFormulaCPM.Font.Style);
                checkEditFormulaImpressions.Font = new Font(checkEditFormulaImpressions.Font.FontFamily, checkEditFormulaImpressions.Font.Size - 2, checkEditFormulaImpressions.Font.Style);
                checkEditFormulaInvestment.Font = new Font(checkEditFormulaInvestment.Font.FontFamily, checkEditFormulaInvestment.Font.Size - 2, checkEditFormulaInvestment.Font.Style);
            }
        }

        public static ScheduleBuilderControl Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ScheduleBuilderControl();
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

        private void AssignCloseActiveEditorsonOutSideClick(Control control)
        {
            if (control.GetType() != typeof(DevExpress.XtraEditors.TextEdit) && control.GetType() != typeof(DevExpress.XtraEditors.MemoEdit) && control.GetType() != typeof(DevExpress.XtraEditors.ComboBoxEdit) && control.GetType() != typeof(DevExpress.XtraEditors.LookUpEdit) && control.GetType() != typeof(DevExpress.XtraEditors.DateEdit) && control.GetType() != typeof(DevExpress.XtraEditors.CheckedListBoxControl) && control.GetType() != typeof(DevExpress.XtraEditors.SpinEdit) && control.GetType() != typeof(DevExpress.XtraEditors.CheckEdit))
            {
                control.Click += new EventHandler(CloseActiveEditorsonOutSideClick);
                foreach (Control childControl in control.Controls)
                    AssignCloseActiveEditorsonOutSideClick(childControl);
            }
        }

        private void CloseActiveEditorsonOutSideClick(object sender, EventArgs e)
        {
            labelControlAdvertiser.Focus();
        }

        private void ApplyProductValues(ProductControl tabPage)
        {
            if (this.AllowApplyValues)
            {
                tabPage.HideDefaultPanel();
                if (!FormMain.Instance.buttonItemSchedulesCPM.Checked)
                {
                    tabPage.Product.Formula = BusinessClasses.FormulaType.CPM;
                    this.AllowApplyValues = false;
                    checkEditFormulaCPM.Checked = true;
                    checkEditFormulaInvestment.Checked = false;
                    checkEditFormulaImpressions.Checked = false;
                    this.AllowApplyValues = true;
                }
                else
                {
                    if (checkEditFormulaCPM.Checked)
                        tabPage.Product.Formula = BusinessClasses.FormulaType.CPM;
                    else if (checkEditFormulaInvestment.Checked)
                        tabPage.Product.Formula = BusinessClasses.FormulaType.Investment;
                    else if (checkEditFormulaImpressions.Checked)
                        tabPage.Product.Formula = BusinessClasses.FormulaType.Impressions;
                }

                tabPage.Product.ShowBusinessName = FormMain.Instance.buttonItemSchedulesBusinessName.Checked;
                tabPage.Product.ShowDecisionMaker = FormMain.Instance.buttonItemSchedulesDecisionMaker.Checked;
                tabPage.Product.ShowPresentationDate = FormMain.Instance.buttonItemSchedulesPresentationDate.Checked;
                tabPage.Product.ShowProduct = FormMain.Instance.buttonItemSchedulesTitle.Checked;
                tabPage.Product.ShowActiveDays = FormMain.Instance.buttonItemSchedulesActiveDays.Checked;
                tabPage.Product.ShowAdRate = FormMain.Instance.buttonItemSchedulesAdRate.Checked;
                tabPage.Product.ShowCPMButton = FormMain.Instance.buttonItemSchedulesCPM.Checked;
                tabPage.Product.ShowDescription = FormMain.Instance.buttonItemSchedulesDescription.Checked;
                tabPage.Product.ShowDimensions = FormMain.Instance.buttonItemSchedulesDimensions.Checked;
                tabPage.Product.ShowFlightDates = FormMain.Instance.buttonItemSchedulesFlightDates.Checked;
                tabPage.Product.ShowMonthlyImpressions = FormMain.Instance.buttonItemSchedulesAvgMonthlyRate.Checked;
                tabPage.Product.ShowMonthlyInvestment = FormMain.Instance.buttonItemSchedulesTotalMonthlyRate.Checked;
                tabPage.Product.ShowComments = FormMain.Instance.buttonItemSchedulesComments.Checked;
                tabPage.Product.ShowTotalAds = FormMain.Instance.buttonItemSchedulesTotalAds.Checked;
                tabPage.Product.ShowTotalImpressions = FormMain.Instance.buttonItemSchedulesAvgTotalRate.Checked;
                tabPage.Product.ShowTotalInvestment = FormMain.Instance.buttonItemSchedulesTotalRate.Checked;
                tabPage.Product.ShowImages = FormMain.Instance.buttonItemSchedulesImageIcons.Checked;
                tabPage.Product.ShowScreenshot = FormMain.Instance.buttonItemSchedulesScreenshotViewer.Checked;
                tabPage.Product.ShowSignature = FormMain.Instance.buttonItemSchedulesSignatureLine.Checked;
                tabPage.Product.ShowWebsite = FormMain.Instance.buttonItemSchedulesWebsites.Checked;
                tabPage.WebsiteCheckedChanged();
                tabPage.UpdateView();
                tabPage.UpdateDefaultCPM();
            }
        }

        public void LoadSchedule(bool quickLoad)
        {
            this.LocalSchedule = BusinessClasses.ScheduleManager.Instance.GetLocalSchedule();

            if (!quickLoad)
            {
                comboBoxEditSlideHeader.Properties.Items.Clear();
                comboBoxEditSlideHeader.Properties.Items.AddRange(BusinessClasses.ListManager.Instance.SlideHeaders.ToArray());
                if (comboBoxEditSlideHeader.Properties.Items.Count > 0)
                    comboBoxEditSlideHeader.SelectedIndex = 0;

                bool temp = this.AllowApplyValues;
                this.AllowApplyValues = false;
                //checkEditApplyForAllProducts.Checked = this.LocalSchedule.ApplySettingsForeAllProducts;
                this.AllowApplyValues = temp;
                Application.DoEvents();

                xtraTabControlProducts.SuspendLayout();
                Application.DoEvents();
                xtraTabControlProducts.SelectedPageChanged -= new DevExpress.XtraTab.TabPageChangedEventHandler(xtraTabControlProducts_SelectedPageChanged); ;
                xtraTabControlProducts.TabPages.Clear();
                _tabPages.RemoveAll(x => !this.LocalSchedule.Products.Select(y => y.UniqueID).Contains(x.Product.UniqueID));
                foreach (BusinessClasses.Product product in this.LocalSchedule.Products)
                {
                    if (!string.IsNullOrEmpty(product.Name))
                    {
                        ProductControl productTab = _tabPages.Where(x => x.Product.UniqueID.Equals(product.UniqueID)).FirstOrDefault();
                        if (productTab == null)
                        {
                            productTab = new ProductControl();
                            _tabPages.Add(productTab);
                            Application.DoEvents();
                        }
                        productTab.Product = product;
                        productTab.LoadValues();
                        Application.DoEvents();
                    }
                }
                _tabPages.Sort((x, y) => x.Product.Index.CompareTo(y.Product.Index));
                xtraTabControlProducts.TabPages.AddRange(_tabPages.ToArray());
                Application.DoEvents();
                xtraTabControlProducts.ResumeLayout();

                LoadProduct();
                Application.DoEvents();
                xtraTabControlProducts.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(xtraTabControlProducts_SelectedPageChanged); ;
                this.AllowApplyValues = true;
            }
            else
            {
                foreach (BusinessClasses.Product product in this.LocalSchedule.Products)
                {
                    if (!string.IsNullOrEmpty(product.Name))
                    {
                        ProductControl productTab = _tabPages.Where(x => x.Product.UniqueID.Equals(product.UniqueID)).FirstOrDefault();
                        if (productTab != null)
                        {
                            productTab.Product = product;
                        }
                        Application.DoEvents();
                    }
                }
            }

            this.SettingsNotSaved = false;
        }

        private void LoadProduct()
        {
            bool tempSettingsNotSaved = this.SettingsNotSaved;
            bool temp = this.AllowApplyValues;
            this.AllowApplyValues = false;
            if (xtraTabControlProducts.SelectedTabPageIndex >= 0)
            {
                //if (xtraTabControlProducts.SelectedTabPageIndex > 0)
                //{
                //    checkEditApplyForAllProducts.Checked = false;
                //    checkEditApplyForAllProducts.Visible = false;
                //}
                //else
                //    checkEditApplyForAllProducts.Visible = true;

                BusinessClasses.Product product = (xtraTabControlProducts.TabPages[xtraTabControlProducts.SelectedTabPageIndex] as ProductControl).Product;
                FormMain.Instance.buttonItemSchedulesWebsites.CheckedChanged -= new EventHandler(CustomControls.ScheduleBuilderControl.Instance.buttonItemSchedulesTogledButton_CheckedChanged);
                FormMain.Instance.buttonItemSchedulesWebsites.Checked = product.ShowWebsite;
                FormMain.Instance.buttonItemSchedulesWebsites.CheckedChanged += new EventHandler(CustomControls.ScheduleBuilderControl.Instance.buttonItemSchedulesTogledButton_CheckedChanged);
                (xtraTabControlProducts.TabPages[xtraTabControlProducts.SelectedTabPageIndex] as ProductControl).WebsiteCheckedChanged();
                FormMain.Instance.buttonItemSchedulesBusinessName.Checked = product.ShowBusinessName;
                FormMain.Instance.buttonItemSchedulesPresentationDate.Checked = product.ShowPresentationDate;
                FormMain.Instance.buttonItemSchedulesDecisionMaker.Checked = product.ShowDecisionMaker;
                FormMain.Instance.buttonItemSchedulesTitle.Checked = product.ShowProduct;
                FormMain.Instance.buttonItemSchedulesActiveDays.Checked = product.ShowActiveDays;
                FormMain.Instance.buttonItemSchedulesAdRate.Checked = product.ShowAdRate;
                FormMain.Instance.buttonItemSchedulesTotalRate.Checked = product.ShowTotalInvestment;
                FormMain.Instance.buttonItemSchedulesTotalMonthlyRate.Checked = product.ShowMonthlyInvestment;
                FormMain.Instance.buttonItemSchedulesAvgTotalRate.Checked = product.ShowTotalImpressions;
                FormMain.Instance.buttonItemSchedulesAvgMonthlyRate.Checked = product.ShowMonthlyImpressions;
                if ((FormMain.Instance.buttonItemSchedulesAvgTotalRate.Checked && FormMain.Instance.buttonItemSchedulesTotalRate.Checked) || (FormMain.Instance.buttonItemSchedulesAvgMonthlyRate.Checked && FormMain.Instance.buttonItemSchedulesTotalMonthlyRate.Checked))
                    FormMain.Instance.buttonItemSchedulesCPM.Enabled = true;
                else
                {
                    FormMain.Instance.buttonItemSchedulesCPM.Checked = false;
                    FormMain.Instance.buttonItemSchedulesCPM.Enabled = false;
                }
                FormMain.Instance.buttonItemSchedulesCPM.Checked = product.ShowCPMButton;
                FormMain.Instance.buttonItemSchedulesDescription.Checked = product.ShowDescription;
                FormMain.Instance.buttonItemSchedulesDimensions.Checked = product.ShowDimensions;
                FormMain.Instance.buttonItemSchedulesFlightDates.Checked = product.ShowFlightDates;
                FormMain.Instance.buttonItemSchedulesComments.Checked = product.ShowComments;
                FormMain.Instance.buttonItemSchedulesTotalAds.Checked = product.ShowTotalAds;
                FormMain.Instance.buttonItemSchedulesImageIcons.Checked = product.ShowImages;
                FormMain.Instance.buttonItemSchedulesScreenshotViewer.Checked = product.ShowScreenshot;
                FormMain.Instance.buttonItemSchedulesSignatureLine.Checked = product.ShowSignature;

                labelControlPresentationDate.Visible = product.ShowPresentationDate;
                if (product.ShowBusinessName && product.ShowDecisionMaker)
                {
                    labelControlAdvertiser.Visible = true;
                    labelControlAdvertiser.Text = "Prepared For: " + product.Parent.BusinessName + "\n\n" + product.Parent.DecisionMaker;
                }
                else if (!product.ShowBusinessName && product.ShowDecisionMaker)
                {
                    labelControlAdvertiser.Visible = true;
                    labelControlAdvertiser.Text = product.Parent.DecisionMaker;
                }
                else if (product.ShowBusinessName && !product.ShowDecisionMaker)
                {
                    labelControlAdvertiser.Visible = true;
                    labelControlAdvertiser.Text = "Prepared For: " + product.Parent.BusinessName;
                }
                else
                {
                    labelControlAdvertiser.Visible = false;
                }

                if (product.ShowActiveDays ||
                    product.ShowAdRate ||
                    product.ShowBusinessName ||
                    product.ShowComments ||
                    product.ShowCPMButton ||
                    product.ShowDecisionMaker ||
                    product.ShowDescription ||
                    product.ShowDimensions ||
                    product.ShowFlightDates ||
                    product.ShowMonthlyImpressions ||
                    product.ShowMonthlyInvestment ||
                    product.ShowPresentationDate ||
                    product.ShowProduct ||
                    product.ShowTotalAds ||
                    product.ShowTotalImpressions ||
                    product.ShowTotalInvestment)
                    (xtraTabControlProducts.TabPages[xtraTabControlProducts.SelectedTabPageIndex] as ProductControl).HideDefaultPanel();


                switch (product.Formula)
                {
                    case BusinessClasses.FormulaType.CPM:
                        ScheduleBuilderControl.Instance.checkEditFormulaCPM.Checked = true;
                        ScheduleBuilderControl.Instance.checkEditFormulaInvestment.Checked = false;
                        ScheduleBuilderControl.Instance.checkEditFormulaImpressions.Checked = false;
                        break;
                    case BusinessClasses.FormulaType.Investment:
                        ScheduleBuilderControl.Instance.checkEditFormulaCPM.Checked = false;
                        ScheduleBuilderControl.Instance.checkEditFormulaInvestment.Checked = true;
                        ScheduleBuilderControl.Instance.checkEditFormulaImpressions.Checked = false;
                        break;
                    case BusinessClasses.FormulaType.Impressions:
                        ScheduleBuilderControl.Instance.checkEditFormulaCPM.Checked = false;
                        ScheduleBuilderControl.Instance.checkEditFormulaInvestment.Checked = false;
                        ScheduleBuilderControl.Instance.checkEditFormulaImpressions.Checked = true;
                        break;
                }

                pictureBoxFormula.Image = product.ShowCPMButton ? Properties.Resources.InvestmentLogo : Properties.Resources.InvestmentLogoGray;
                labelControlFormula.Enabled = product.ShowCPMButton;
                checkEditFormulaCPM.Enabled = product.ShowCPMButton;
                checkEditFormulaInvestment.Enabled = product.ShowCPMButton;
                checkEditFormulaImpressions.Enabled = product.ShowCPMButton;

                this.SettingsNotSaved = tempSettingsNotSaved;
            }

            if (xtraTabControlProducts.SelectedTabPage != null)
                (xtraTabControlProducts.SelectedTabPage as ProductControl).UpdateOutputStatus();
            this.AllowApplyValues = temp;
        }

        private bool SaveSchedule(string scheduleName = "")
        {
            if (!string.IsNullOrEmpty(scheduleName))
            {
                this.LocalSchedule.Name = scheduleName;
            }
            foreach (ProductControl product in xtraTabControlProducts.TabPages)
                product.SaveValues();
            //this.LocalSchedule.ApplySettingsForeAllProducts = checkEditApplyForAllProducts.Checked;

            BusinessClasses.ScheduleManager.Instance.SaveSchedule(this.LocalSchedule, false, this);
            this.SettingsNotSaved = false;
            return true;
        }

        private void ScheduleBuilderControl_Load(object sender, EventArgs e)
        {
            AssignCloseActiveEditorsonOutSideClick(panelExHeader);
            AssignCloseActiveEditorsonOutSideClick(pnHeader);
        }

        private void xtraTabControlProducts_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            LoadProduct();
        }

        public void buttonItemSchedulesSave_Click(object sender, EventArgs e)
        {
            SaveSchedule();
            AppManager.ShowInformation("Schedule Saved");
        }

        public void buttonItemSchedulesSaveAs_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.InitialDirectory = ConfigurationClasses.SettingsManager.Instance.SaveFolder;
                dialog.Title = "Save Schedule As...";
                dialog.Filter = "Schedule Files|*.xml";
                dialog.FileName = this.LocalSchedule.Name + ".xml";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    if (SaveSchedule(dialog.FileName.Replace(".xml", "")))
                        AppManager.ShowInformation("Schedule was saved");
                }
            }
        }

        public void buttonItemSchedulesTogledButton_CheckedChanged(object sender, EventArgs e)
        {
            if ((FormMain.Instance.buttonItemSchedulesAvgTotalRate.Checked && FormMain.Instance.buttonItemSchedulesTotalRate.Checked) || (FormMain.Instance.buttonItemSchedulesAvgMonthlyRate.Checked && FormMain.Instance.buttonItemSchedulesTotalMonthlyRate.Checked))
                FormMain.Instance.buttonItemSchedulesCPM.Enabled = true;
            else
            {
                bool temp = this.AllowApplyValues;
                this.AllowApplyValues = false;
                FormMain.Instance.buttonItemSchedulesCPM.Checked = false;
                this.AllowApplyValues = temp;
                FormMain.Instance.buttonItemSchedulesCPM.Enabled = false;
            }

            //if (checkEditApplyForAllProducts.Checked)
            //{
            //    foreach (ProductControl tabPage in xtraTabControlProducts.TabPages)
            //    {
            //        ApplyProductValues(tabPage);
            //    }
            //}
            //else
            //{
                if (xtraTabControlProducts.SelectedTabPage != null)
                    ApplyProductValues(xtraTabControlProducts.SelectedTabPage as ProductControl);
            //}

            (xtraTabControlProducts.TabPages[xtraTabControlProducts.SelectedTabPageIndex] as ProductControl).UpdateOutputStatus();
            this.SettingsNotSaved = true;
        }

        //private void checkEditApplyForAllProducts_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (checkEditApplyForAllProducts.Checked)
        //    {
        //        foreach (ProductControl tabPage in xtraTabControlProducts.TabPages)
        //            ApplyProductValues(tabPage);
        //        this.SettingsNotSaved = true;
        //    }
        //}

        private void checkEditFormula_CheckedChanged(object sender, EventArgs e)
        {
            if (this.AllowApplyValues && (sender as DevExpress.XtraEditors.CheckEdit).Checked)
            {
                this.AllowApplyValues = false;
                checkEditFormulaCPM.Checked = false;
                checkEditFormulaImpressions.Checked = false;
                checkEditFormulaInvestment.Checked = false;
                (sender as DevExpress.XtraEditors.CheckEdit).Checked = true;
                this.AllowApplyValues = true;
                buttonItemSchedulesTogledButton_CheckedChanged(null, null);
            }
        }

        public void buttonItemSchedulesPowerPoint_Click(object sender, EventArgs e)
        {
            using (ToolForms.FormSelectPublication form = new ToolForms.FormSelectPublication())
            {
                form.Text = "Mobile Schedule Slide Output";
                form.pbLogo.Image = Properties.Resources.SchedulesTitle;
                form.laTitle.Text = "You have Several Mobile Schedule tabs available for output to PowerPoint…";
                form.buttonXCurrentPublication.Text = "Send just the active Mobile Schedule Slide to PowerPoint";
                form.buttonXSelectedPublications.Text = "Send ALL SELECTED Mobile Schedule Slides to PowerPoint";
                foreach (ProductControl tabPage in _tabPages)
                {
                    tabPage.SaveValues();
                    form.checkedListBoxControlPublications.Items.Add(tabPage.Product.UniqueID, tabPage.Product.Name, CheckState.Checked, true);
                }
                DialogResult result = DialogResult.Yes;
                if (form.checkedListBoxControlPublications.Items.Count > 1)
                {
                    ConfigurationClasses.RegistryHelper.MainFormHandle = form.Handle;
                    ConfigurationClasses.RegistryHelper.MaximizeMainForm = false;
                    result = form.ShowDialog();
                    ConfigurationClasses.RegistryHelper.MaximizeMainForm = true;
                    ConfigurationClasses.RegistryHelper.MainFormHandle = FormMain.Instance.Handle;
                    if (result == DialogResult.Cancel)
                        return;
                }
                using (ToolForms.FormProgress formProgress = new ToolForms.FormProgress())
                {
                    formProgress.laProgress.Text = "Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!";
                    formProgress.TopMost = true;
                    formProgress.Show();
                    if (result == DialogResult.Yes)
                    {
                        if (xtraTabControlProducts.SelectedTabPage != null)
                            (xtraTabControlProducts.SelectedTabPage as ProductControl).Output();
                    }
                    else if (result == DialogResult.No)
                    {
                        foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in form.checkedListBoxControlPublications.Items)
                        {
                            if (item.CheckState == CheckState.Checked)
                            {
                                ProductControl tabPage = _tabPages.Where(x => x.Product.UniqueID.Equals(item.Value)).FirstOrDefault();
                                if (tabPage != null)
                                    tabPage.Output();
                            }
                        }
                    }
                    formProgress.Close();
                }
                using (ToolForms.FormSlideOutput formOutput = new ToolForms.FormSlideOutput())
                {
                    if (formOutput.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                        AppManager.ActivateForm(FormMain.Instance.Handle, true, false);
                }
            }
        }

        public void buttonItemSchedulesEmail_Click(object sender, EventArgs e)
        {
            using (ToolForms.FormSelectPublication form = new ToolForms.FormSelectPublication())
            {
                form.Text = "Mobile Schedule Email Output";
                form.pbLogo.Image = Properties.Resources.Email;
                form.laTitle.Text = "You have Several Mobile Schedules that you may choose to email…";
                form.buttonXCurrentPublication.Text = "Attach just the active Mobile Schedule Slide to my Outlook Email Message";
                form.buttonXSelectedPublications.Text = "Attach ALL SELECTED Mobile Schedule Slides to my Outlook Email Message";
                foreach (ProductControl tabPage in _tabPages)
                {
                    tabPage.SaveValues();
                    form.checkedListBoxControlPublications.Items.Add(tabPage.Product.UniqueID, tabPage.Product.Name, CheckState.Checked, true);
                }
                DialogResult result = DialogResult.Yes;
                if (form.checkedListBoxControlPublications.Items.Count > 1)
                {
                    ConfigurationClasses.RegistryHelper.MainFormHandle = form.Handle;
                    ConfigurationClasses.RegistryHelper.MaximizeMainForm = false;
                    result = form.ShowDialog();
                    ConfigurationClasses.RegistryHelper.MaximizeMainForm = true;
                    ConfigurationClasses.RegistryHelper.MainFormHandle = FormMain.Instance.Handle;
                    if (result == DialogResult.Cancel)
                        return;
                }
                using (ToolForms.FormProgress formProgress = new ToolForms.FormProgress())
                {
                    formProgress.laProgress.Text = "Chill-Out for a few seconds...\nPreparing Presentation for Email...";
                    formProgress.TopMost = true;
                    formProgress.Show();
                    string tempFileName = Path.Combine(ConfigurationClasses.SettingsManager.Instance.TempPath, Path.GetFileName(Path.GetTempFileName()));
                    if (result == DialogResult.Yes)
                        InteropClasses.PowerPointHelper.Instance.PrepareScheduleEmail(tempFileName, new BusinessClasses.Product[] { (xtraTabControlProducts.SelectedTabPage as ProductControl).Product });
                    else if (result == DialogResult.No)
                    {
                        List<BusinessClasses.Product> outputProducts = new List<BusinessClasses.Product>();
                        foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in form.checkedListBoxControlPublications.Items)
                        {
                            if (item.CheckState == CheckState.Checked)
                            {
                                ProductControl tabPage = _tabPages.Where(x => x.Product.UniqueID.Equals(item.Value)).FirstOrDefault();
                                if (tabPage != null)
                                    outputProducts.Add(tabPage.Product);
                            }
                        }
                        InteropClasses.PowerPointHelper.Instance.PrepareScheduleEmail(tempFileName, outputProducts.ToArray());
                    }
                    formProgress.Close();
                    if (File.Exists(tempFileName))
                        using (ToolForms.FormEmail formEmail = new ToolForms.FormEmail())
                        {
                            formEmail.Text = "Email this Mobile Schedule";
                            formEmail.PresentationFile = tempFileName;
                            ConfigurationClasses.RegistryHelper.MainFormHandle = formEmail.Handle;
                            ConfigurationClasses.RegistryHelper.MaximizeMainForm = false;
                            formEmail.ShowDialog();
                            ConfigurationClasses.RegistryHelper.MaximizeMainForm = true;
                            ConfigurationClasses.RegistryHelper.MainFormHandle = FormMain.Instance.Handle;
                        }
                }
            }
        }

        public void buttonItemSchedulesHelp_Click(object sender, EventArgs e)
        {
            BusinessClasses.HelpManager.Instance.OpenHelpLink("Slides");
        }
    }
}
