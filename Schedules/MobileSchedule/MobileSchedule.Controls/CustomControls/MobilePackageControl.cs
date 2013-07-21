using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MobileScheduleBuilder.CustomControls
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class MobilePackageControl : UserControl
    {
        private static MobilePackageControl _instance;
        private ProductPackageControl _productPackage = null;

        public bool SettingsNotSaved { get; set; }
        public bool AllowApplyValues { get; set; }
        public BusinessClasses.Schedule LocalSchedule { get; set; }

        private MobilePackageControl()
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
                labelControlOutputStatus.Font = new Font(labelControlOutputStatus.Font.FontFamily, labelControlOutputStatus.Font.Size - 3, labelControlOutputStatus.Font.Style);
                labelControlFormula.Font = new Font(labelControlFormula.Font.FontFamily, labelControlFormula.Font.Size - 2, labelControlFormula.Font.Style);
                checkEditFormulaCPM.Font = new Font(checkEditFormulaCPM.Font.FontFamily, checkEditFormulaCPM.Font.Size - 2, checkEditFormulaCPM.Font.Style);
                checkEditFormulaImpressions.Font = new Font(checkEditFormulaImpressions.Font.FontFamily, checkEditFormulaImpressions.Font.Size - 2, checkEditFormulaImpressions.Font.Style);
                checkEditFormulaInvestment.Font = new Font(checkEditFormulaInvestment.Font.FontFamily, checkEditFormulaInvestment.Font.Size - 2, checkEditFormulaInvestment.Font.Style);
            }
        }

        public static MobilePackageControl Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new MobilePackageControl();
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

        public void AssignCloseActiveEditorsonOutSideClick(Control control)
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

        public void UpdateOutputStatus()
        {
            string templateName = _productPackage.ProductPackage.GetSlideSource();
            FormMain.Instance.buttonItemMobilePackagePowerPoint.Enabled = !string.IsNullOrEmpty(templateName);
            FormMain.Instance.buttonItemMobilePackageEmail.Enabled = !string.IsNullOrEmpty(templateName);
            if (!string.IsNullOrEmpty(templateName))
            {

                labelControlOutputStatus.ForeColor = Color.Green;
                labelControlOutputStatus.Text = "Slide Output AVAILABLE";
            }
            else
            {
                labelControlOutputStatus.ForeColor = Color.Red;
                labelControlOutputStatus.Text = "Slide Output DISABLED";
            }
        }

        private void ApplyProductValues()
        {
            if (this.AllowApplyValues)
            {
                _productPackage.HideDefaultPanel();

                if (!FormMain.Instance.buttonItemSchedulesCPM.Checked)
                {
                    _productPackage.ProductPackage.Formula = BusinessClasses.FormulaType.CPM;
                    this.AllowApplyValues = false;
                    checkEditFormulaCPM.Checked = true;
                    checkEditFormulaInvestment.Checked = false;
                    checkEditFormulaImpressions.Checked = false;
                    this.AllowApplyValues = true;
                }
                else
                {
                    if (checkEditFormulaCPM.Checked)
                        _productPackage.ProductPackage.Formula = BusinessClasses.FormulaType.CPM;
                    else if (checkEditFormulaInvestment.Checked)
                        _productPackage.ProductPackage.Formula = BusinessClasses.FormulaType.Investment;
                    else if (checkEditFormulaImpressions.Checked)
                        _productPackage.ProductPackage.Formula = BusinessClasses.FormulaType.Impressions;
                }

                _productPackage.ProductPackage.ShowBusinessName = FormMain.Instance.buttonItemMobilePackageBusinessName.Checked;
                _productPackage.ProductPackage.ShowDecisionMaker = FormMain.Instance.buttonItemMobilePackageDecisionMaker.Checked;
                _productPackage.ProductPackage.ShowPresentationDate = FormMain.Instance.buttonItemMobilePackagePresentationDate.Checked;
                _productPackage.ProductPackage.ShowActiveDays = FormMain.Instance.buttonItemMobilePackageActiveDays.Checked;
                _productPackage.ProductPackage.ShowAdRate = FormMain.Instance.buttonItemMobilePackageAdRate.Checked;
                _productPackage.ProductPackage.ShowCPMButton = FormMain.Instance.buttonItemMobilePackageCPM.Checked;
                _productPackage.ProductPackage.ShowFlightDates = FormMain.Instance.buttonItemMobilePackageFlightDates.Checked;
                _productPackage.ProductPackage.ShowMonthlyImpressions = FormMain.Instance.buttonItemMobilePackageAvgMonthlyRate.Checked;
                _productPackage.ProductPackage.ShowMonthlyInvestment = FormMain.Instance.buttonItemMobilePackageTotalMonthlyRate.Checked;
                _productPackage.ProductPackage.ShowComments = FormMain.Instance.buttonItemMobilePackageComments.Checked;
                _productPackage.ProductPackage.ShowTotalAds = FormMain.Instance.buttonItemMobilePackageTotalAds.Checked;
                _productPackage.ProductPackage.ShowTotalImpressions = FormMain.Instance.buttonItemMobilePackageAvgTotalRate.Checked;
                _productPackage.ProductPackage.ShowTotalInvestment = FormMain.Instance.buttonItemMobilePackageTotalRate.Checked;
                _productPackage.ProductPackage.ShowImages = FormMain.Instance.buttonItemMobilePackageImageIcons.Checked;
                _productPackage.ProductPackage.ShowScreenshot = FormMain.Instance.buttonItemMobilePackageScreenshotViewer.Checked;
                _productPackage.ProductPackage.ShowSignature = FormMain.Instance.buttonItemMobilePackageSignatureLine.Checked;
                _productPackage.ProductPackage.ShowWebsite = FormMain.Instance.buttonItemMobilePackageWebsites.Checked;
                _productPackage.WebsiteCheckedChanged();
                _productPackage.UpdateView();
            }
        }

        public void LoadSchedule(bool quickLoad)
        {
            this.LocalSchedule = BusinessClasses.ScheduleManager.Instance.GetLocalSchedule();

            if (!quickLoad || _productPackage == null)
            {
                comboBoxEditSlideHeader.Properties.Items.Clear();
                comboBoxEditSlideHeader.Properties.Items.AddRange(BusinessClasses.ListManager.Instance.SlideHeaders.ToArray());
                if (comboBoxEditSlideHeader.Properties.Items.Count > 0)
                    comboBoxEditSlideHeader.SelectedIndex = 0;

                pnMain.Controls.Clear();
                Application.DoEvents();
                _productPackage = new ProductPackageControl();
                Application.DoEvents();
                _productPackage.ProductPackage = this.LocalSchedule.ProductPackage;
                _productPackage.LoadValues();
                Application.DoEvents();
                pnMain.Controls.Add(_productPackage);
                Application.DoEvents();

                LoadProduct();
                Application.DoEvents();
                this.AllowApplyValues = true;
            }
            else
            {
                _productPackage.ProductPackage = this.LocalSchedule.ProductPackage;
                Application.DoEvents();
            }

            this.SettingsNotSaved = false;
        }

        private void LoadProduct()
        {
            bool tempSettingsNotSaved = this.SettingsNotSaved;
            bool temp = this.AllowApplyValues;
            this.AllowApplyValues = false;
            if (_productPackage != null)
            {
                FormMain.Instance.buttonItemMobilePackageWebsites.CheckedChanged -= new EventHandler(CustomControls.MobilePackageControl.Instance.buttonItemMobilePackageTogledButton_CheckedChanged);
                FormMain.Instance.buttonItemMobilePackageWebsites.Checked = _productPackage.ProductPackage.ShowWebsite;
                FormMain.Instance.buttonItemMobilePackageWebsites.CheckedChanged += new EventHandler(CustomControls.MobilePackageControl.Instance.buttonItemMobilePackageTogledButton_CheckedChanged);
                _productPackage.WebsiteCheckedChanged();
                FormMain.Instance.buttonItemMobilePackageBusinessName.Checked = _productPackage.ProductPackage.ShowBusinessName;
                FormMain.Instance.buttonItemMobilePackagePresentationDate.Checked = _productPackage.ProductPackage.ShowPresentationDate;
                FormMain.Instance.buttonItemMobilePackageDecisionMaker.Checked = _productPackage.ProductPackage.ShowDecisionMaker;
                FormMain.Instance.buttonItemMobilePackageActiveDays.Checked = _productPackage.ProductPackage.ShowActiveDays;
                FormMain.Instance.buttonItemMobilePackageAdRate.Checked = _productPackage.ProductPackage.ShowAdRate;
                FormMain.Instance.buttonItemMobilePackageTotalRate.Checked = _productPackage.ProductPackage.ShowTotalInvestment;
                FormMain.Instance.buttonItemMobilePackageTotalMonthlyRate.Checked = _productPackage.ProductPackage.ShowMonthlyInvestment;
                FormMain.Instance.buttonItemMobilePackageAvgTotalRate.Checked = _productPackage.ProductPackage.ShowTotalImpressions;
                FormMain.Instance.buttonItemMobilePackageAvgMonthlyRate.Checked = _productPackage.ProductPackage.ShowMonthlyImpressions;
                if ((FormMain.Instance.buttonItemMobilePackageAvgTotalRate.Checked && FormMain.Instance.buttonItemMobilePackageTotalRate.Checked) || (FormMain.Instance.buttonItemMobilePackageAvgMonthlyRate.Checked && FormMain.Instance.buttonItemMobilePackageTotalMonthlyRate.Checked))
                    FormMain.Instance.buttonItemMobilePackageCPM.Enabled = true;
                else
                {
                    FormMain.Instance.buttonItemMobilePackageCPM.Checked = false;
                    FormMain.Instance.buttonItemMobilePackageCPM.Enabled = false;
                }
                FormMain.Instance.buttonItemMobilePackageCPM.Checked = _productPackage.ProductPackage.ShowCPMButton;
                FormMain.Instance.buttonItemMobilePackageFlightDates.Checked = _productPackage.ProductPackage.ShowFlightDates;
                FormMain.Instance.buttonItemMobilePackageComments.Checked = _productPackage.ProductPackage.ShowComments;
                FormMain.Instance.buttonItemMobilePackageTotalAds.Checked = _productPackage.ProductPackage.ShowTotalAds;
                FormMain.Instance.buttonItemMobilePackageImageIcons.Checked = _productPackage.ProductPackage.ShowImages;
                FormMain.Instance.buttonItemMobilePackageScreenshotViewer.Checked = _productPackage.ProductPackage.ShowScreenshot;
                FormMain.Instance.buttonItemMobilePackageSignatureLine.Checked = _productPackage.ProductPackage.ShowSignature;

                labelControlPresentationDate.Visible = _productPackage.ProductPackage.ShowPresentationDate;
                if (_productPackage.ProductPackage.ShowBusinessName && _productPackage.ProductPackage.ShowDecisionMaker)
                {
                    labelControlAdvertiser.Visible = true;
                    labelControlAdvertiser.Text = "Prepared For: " + _productPackage.ProductPackage.Parent.BusinessName + "\n\n" + _productPackage.ProductPackage.Parent.DecisionMaker;
                }
                else if (!_productPackage.ProductPackage.ShowBusinessName && _productPackage.ProductPackage.ShowDecisionMaker)
                {
                    labelControlAdvertiser.Visible = true;
                    labelControlAdvertiser.Text = _productPackage.ProductPackage.Parent.DecisionMaker;
                }
                else if (_productPackage.ProductPackage.ShowBusinessName && !_productPackage.ProductPackage.ShowDecisionMaker)
                {
                    labelControlAdvertiser.Visible = true;
                    labelControlAdvertiser.Text = "Prepared For: " + _productPackage.ProductPackage.Parent.BusinessName;
                }
                else
                {
                    labelControlAdvertiser.Visible = false;
                }

                if (_productPackage.ProductPackage.ShowActiveDays ||
                    _productPackage.ProductPackage.ShowAdRate ||
                    _productPackage.ProductPackage.ShowBusinessName ||
                    _productPackage.ProductPackage.ShowComments ||
                    _productPackage.ProductPackage.ShowCPMButton ||
                    _productPackage.ProductPackage.ShowDecisionMaker ||
                    _productPackage.ProductPackage.ShowFlightDates ||
                    _productPackage.ProductPackage.ShowMonthlyImpressions ||
                    _productPackage.ProductPackage.ShowMonthlyInvestment ||
                    _productPackage.ProductPackage.ShowPresentationDate ||
                    _productPackage.ProductPackage.ShowTotalAds ||
                    _productPackage.ProductPackage.ShowTotalImpressions ||
                    _productPackage.ProductPackage.ShowTotalInvestment)
                    _productPackage.HideDefaultPanel();

                switch (_productPackage.ProductPackage.Formula)
                {
                    case BusinessClasses.FormulaType.CPM:
                        checkEditFormulaCPM.Checked = true;
                        checkEditFormulaInvestment.Checked = false;
                        checkEditFormulaImpressions.Checked = false;
                        break;
                    case BusinessClasses.FormulaType.Investment:
                        checkEditFormulaCPM.Checked = false;
                        checkEditFormulaInvestment.Checked = true;
                        checkEditFormulaImpressions.Checked = false;
                        break;
                    case BusinessClasses.FormulaType.Impressions:
                        checkEditFormulaCPM.Checked = false;
                        checkEditFormulaInvestment.Checked = false;
                        checkEditFormulaImpressions.Checked = true;
                        break;
                }

                this.SettingsNotSaved = tempSettingsNotSaved;
            }
            UpdateOutputStatus();
            this.AllowApplyValues = temp;
        }

        private bool SaveSchedule(string scheduleName = "")
        {
            if (!string.IsNullOrEmpty(scheduleName))
            {
                this.LocalSchedule.Name = scheduleName;
            }

            if (_productPackage != null)
                _productPackage.SaveValues();

            BusinessClasses.ScheduleManager.Instance.SaveSchedule(this.LocalSchedule, false, this);
            this.SettingsNotSaved = false;
            return true;
        }

        private void MobilePackageControl_Load(object sender, EventArgs e)
        {
            AssignCloseActiveEditorsonOutSideClick(panelExHeader);
            AssignCloseActiveEditorsonOutSideClick(pnHeader);
        }

        private void xtraTabControlProducts_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            LoadProduct();
        }

        public void buttonItemMobilePackageSave_Click(object sender, EventArgs e)
        {
            SaveSchedule();
            AppManager.ShowInformation("Schedule Saved");
        }

        public void buttonItemMobilePackageSaveAs_Click(object sender, EventArgs e)
        {
            using (ToolForms.FormNewSchedule from = new ToolForms.FormNewSchedule())
            {
                from.Text = "Save Schedule";
                from.laLogo.Text = "Please set a new name for your Schedule:";
                if (from.ShowDialog() == DialogResult.OK)
                {
                    if (!string.IsNullOrEmpty(from.ScheduleName))
                    {
                        if (SaveSchedule(from.ScheduleName))
                            AppManager.ShowInformation("Schedule was saved");
                    }
                    else
                    {
                        AppManager.ShowWarning("Schedule Name can't be empty");
                    }
                }
            }
        }

        public void buttonItemMobilePackageTogledButton_CheckedChanged(object sender, EventArgs e)
        {
            if ((FormMain.Instance.buttonItemMobilePackageAvgTotalRate.Checked && FormMain.Instance.buttonItemMobilePackageTotalRate.Checked) || (FormMain.Instance.buttonItemMobilePackageAvgMonthlyRate.Checked && FormMain.Instance.buttonItemMobilePackageTotalMonthlyRate.Checked))
                FormMain.Instance.buttonItemMobilePackageCPM.Enabled = true;
            else
            {
                bool temp = this.AllowApplyValues;
                this.AllowApplyValues = false;
                FormMain.Instance.buttonItemMobilePackageCPM.Checked = false;
                this.AllowApplyValues = temp;
                FormMain.Instance.buttonItemMobilePackageCPM.Enabled = false;
            }

            ApplyProductValues();
            UpdateOutputStatus();
            this.SettingsNotSaved = true;
        }

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
                buttonItemMobilePackageTogledButton_CheckedChanged(null, null);
            }
        }

        public void buttonItemMobilePackagePowerPoint_Click(object sender, EventArgs e)
        {
            if (_productPackage != null)
                _productPackage.Output();
        }

        public void buttonItemMobilePackageEmail_Click(object sender, EventArgs e)
        {
            if (_productPackage != null)
                _productPackage.Email();
        }

        public void buttonItemMobilePackageHelp_Click(object sender, EventArgs e)
        {
            BusinessClasses.HelpManager.Instance.OpenHelpLink("pkg");
        }
    }
}
