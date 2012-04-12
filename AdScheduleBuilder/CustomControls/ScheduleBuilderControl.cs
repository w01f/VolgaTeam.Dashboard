using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AdScheduleBuilder.CustomControls
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class ScheduleBuilderControl : UserControl
    {
        private static ScheduleBuilderControl _instance;
        private List<PublicationControl> _tabPages = new List<PublicationControl>();
        private bool _allowToSave = false;

        public bool SettingsNotSaved { get; set; }
        public BusinessClasses.Schedule LocalSchedule { get; set; }


        private ScheduleBuilderControl()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            xtraTabControlPublications.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(xtraTabControlPublications_SelectedPageChanged);
            BusinessClasses.ScheduleManager.Instance.SettingsSaved += new EventHandler<BusinessClasses.SavingingEventArgs>((sender, e) =>
            {
                if (sender != this)
                    LoadSchedule(e.QuickSave);
            });
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
                foreach (DevExpress.XtraTab.XtraTabPage tab in xtraTabControlPublications.TabPages)
                {
                    ((PublicationControl)tab).advBandedGridViewPublication.CloseEditor();
                }
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

        #region Common Methods
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
            foreach (PublicationControl control in xtraTabControlPublications.TabPages)
            {
                control.advBandedGridViewPublication.CloseEditor();
                ScheduleSettingsControl.Instance.gridViewPublications.CloseEditor();
            }
        }

        public void LoadSchedule(bool quickLoad)
        {
            this.LocalSchedule = BusinessClasses.ScheduleManager.Instance.GetLocalSchedule();
            laScheduleWindow.Text = string.Format("{0} - {1}", new object[] { this.LocalSchedule.FlightDateStart.ToString("MM/dd/yy"), this.LocalSchedule.FlightDateEnd.ToString("MM/dd/yy") });
            laScheduleName.Text = this.LocalSchedule.Name;
            laAdvertiser.Text = this.LocalSchedule.BusinessName;
            FormMain.Instance.UpdateOutputTabs(this.LocalSchedule.Publications.Select(x => x.Inserts.Count).Sum() > 0);
            if (!quickLoad)
            {
                xtraTabControlPublications.SuspendLayout();
                Application.DoEvents();
                xtraTabControlPublications.SelectedPageChanged -= new DevExpress.XtraTab.TabPageChangedEventHandler(xtraTabControlPublications_SelectedPageChanged); ;
                xtraTabControlPublications.TabPages.Clear();
                _tabPages.RemoveAll(x => !this.LocalSchedule.Publications.Select(y => y.UniqueID).Contains(x.Publication.UniqueID));
                foreach (BusinessClasses.Publication publication in this.LocalSchedule.Publications)
                {
                    if (!string.IsNullOrEmpty(publication.Name))
                    {
                        PublicationControl publicationTab = _tabPages.Where(x => x.Publication.UniqueID.Equals(publication.UniqueID)).FirstOrDefault();
                        if (publicationTab == null)
                        {
                            publicationTab = new PublicationControl();
                            _tabPages.Add(publicationTab);
                            Application.DoEvents();
                        }
                        publicationTab.Publication = publication;
                        publicationTab.Text = publication.Name.Replace("&", "&&");
                        publication.RefreshAvailableDays();
                        publicationTab.LoadInserts();
                        Application.DoEvents();
                    }
                }
                _tabPages.Sort((x, y) => x.Publication.Index.CompareTo(y.Publication.Index));
                xtraTabControlPublications.TabPages.AddRange(_tabPages.ToArray());
                Application.DoEvents();
                LoadPublication();
                xtraTabControlPublications.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(xtraTabControlPublications_SelectedPageChanged); ;
                xtraTabControlPublications.ResumeLayout();
            }
            else
            {
                foreach (BusinessClasses.Publication publication in this.LocalSchedule.Publications)
                {
                    if (!string.IsNullOrEmpty(publication.Name))
                    {
                        PublicationControl publicationTab = _tabPages.Where(x => x.Publication.UniqueID.Equals(publication.UniqueID)).FirstOrDefault();
                        if (publicationTab != null)
                        {
                            publicationTab.Publication = publication;
                            publication.RefreshAvailableDays();
                            publicationTab.LoadInserts();
                        }
                        Application.DoEvents();
                    }
                }
            }
            this.SettingsNotSaved = false;
        }

        private void ClearSettings()
        {
            _allowToSave = false;

            #region Clear Pricing
            FormMain.Instance.buttonItemAdPricingColumnInches.Checked = false;
            FormMain.Instance.buttonItemAdPricingFlat.Checked = false;
            FormMain.Instance.buttonItemAdPricingPagePercent.Checked = false;
            #endregion

            #region Clear Size
            FormMain.Instance.itemContainerAdSizeStandart.Visible = false;
            FormMain.Instance.checkBoxItemAdSizeStandartSquare.Checked = false;
            FormMain.Instance.spinEditStandartHeight.Enabled = false;
            FormMain.Instance.spinEditStandartWidth.Enabled = false;
            FormMain.Instance.spinEditStandartWidth.Value = 0;
            FormMain.Instance.spinEditStandartHeight.Value = 0;
            FormMain.Instance.spinEditStandartWidth.Value = 0;
            FormMain.Instance.laStandartEqualSign.Visible = false;
            FormMain.Instance.laStandartSquareMetric.Visible = false;
            FormMain.Instance.laStandartSquareValue.Visible = false;
            FormMain.Instance.laStandartSquareValue.Text = "0.00";
            FormMain.Instance.checkBoxItemStandartPageSize.Checked = false;
            FormMain.Instance.comboBoxEditStandartPageSize.Enabled = false;
            FormMain.Instance.comboBoxEditStandartPageSize.EditValue = null;

            FormMain.Instance.itemContainerAdSizeSharePage.Visible = false;
            FormMain.Instance.comboBoxEditRateCard.EditValue = null;
            FormMain.Instance.comboBoxEditPercentOfPage.EditValue = null;
            FormMain.Instance.comboBoxEditPercentOfPage.Enabled = false;
            FormMain.Instance.checkBoxItemSharePagePageSize.Checked = false;
            FormMain.Instance.comboBoxEditSharePagePageSize.Enabled = false;
            FormMain.Instance.comboBoxEditSharePagePageSize.EditValue = null;
            FormMain.Instance.checkedListBoxControlSharePageSquare.Items.Clear();
            FormMain.Instance.checkedListBoxControlSharePageSquare.Enabled = false;
            #endregion

            #region Clear Color
            FormMain.Instance.buttonItemColorOptionsSingle.Checked = false;
            FormMain.Instance.buttonItemColorOptionsSpot.Checked = false;
            FormMain.Instance.buttonItemColorOptionsFull.Checked = false;
            FormMain.Instance.buttonItemColorOptionsCostPerAd.Checked = false;
            FormMain.Instance.buttonItemColorOptionsCostPerAd.Enabled = false;
            FormMain.Instance.buttonItemColorOptionsPercentOfAd.Checked = false;
            FormMain.Instance.buttonItemColorOptionsPercentOfAd.Enabled = false;
            FormMain.Instance.buttonItemColorOptionsIncluded.Checked = false;
            FormMain.Instance.buttonItemColorOptionsIncluded.Enabled = false;
            FormMain.Instance.buttonItemColorOptionsPCI.Checked = false;
            FormMain.Instance.spinEditCostPerInch.Value = 0;
            FormMain.Instance.spinEditCostPerInch.Enabled = false;
            #endregion
            _allowToSave = true;
        }

        private void LoadPublication()
        {
            bool temSettingsNotSaved = this.SettingsNotSaved;
            PublicationControl publicationControl = xtraTabControlPublications.SelectedTabPage as PublicationControl;
            if (publicationControl != null)
            {
                BusinessClasses.Publication publication = publicationControl.Publication;
                FormMain.Instance.buttonItemDeleteInsert.Enabled = publication.Inserts.Count > 0;
                FormMain.Instance.buttonItemCloneInsert.Enabled = publication.Inserts.Count > 0;
                FormMain.Instance.buttonItemAdPricingPagePercent.Enabled = BusinessClasses.ListManager.Instance.ShareUnits.Count > 0;
                ClearSettings();
                LoadPricingOptions(publicationControl);
                LoadSizeOptions(publicationControl);
                LoadColorOptions(publicationControl);
                this.SettingsNotSaved = temSettingsNotSaved;
            }
        }

        private bool SaveSchedule(string scheduleName = "")
        {
            if (!string.IsNullOrEmpty(scheduleName))
            {
                this.LocalSchedule.Name = scheduleName;
                laScheduleName.Text = this.LocalSchedule.Name;
            }
            BusinessClasses.ScheduleManager.Instance.SaveSchedule(this.LocalSchedule, false, this);
            LoadSchedule(true);
            this.SettingsNotSaved = false;
            return true;
        }

        private void xtraTabControlPublications_MouseDown(object sender, MouseEventArgs e)
        {
            DevExpress.XtraTab.XtraTabControl c = sender as DevExpress.XtraTab.XtraTabControl;

            DevExpress.XtraTab.ViewInfo.XtraTabHitInfo hi = c.CalcHitInfo(new Point(e.X, e.Y));
            if (hi.HitTest == DevExpress.XtraTab.ViewInfo.XtraTabHitTest.PageHeader && e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                PublicationControl publicationControl = hi.Page as PublicationControl;
                using (ToolForms.FormCloneSchedule form = new ToolForms.FormCloneSchedule())
                {
                    if (form.ShowDialog() == DialogResult.Yes && publicationControl != null)
                    {
                        PublicationControl selectedPage = xtraTabControlPublications.SelectedTabPage as PublicationControl;
                        BusinessClasses.Publication newPublication = publicationControl.Publication.Clone();
                        xtraTabControlPublications.SelectedPageChanged -= new DevExpress.XtraTab.TabPageChangedEventHandler(xtraTabControlPublications_SelectedPageChanged); ;
                        xtraTabControlPublications.TabPages.Clear();
                        PublicationControl newPublicationTab = new PublicationControl();
                        newPublicationTab.Publication = newPublication;
                        newPublicationTab.Text = newPublication.Name.Replace("&", "&&");
                        newPublication.RefreshAvailableDays();
                        newPublicationTab.LoadInserts();
                        _tabPages.Add(newPublicationTab);
                        _tabPages.Sort((x, y) => x.Publication.Index.CompareTo(y.Publication.Index));
                        xtraTabControlPublications.TabPages.AddRange(_tabPages.ToArray());
                        xtraTabControlPublications.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(xtraTabControlPublications_SelectedPageChanged); ;
                        xtraTabControlPublications.SelectedTabPage = selectedPage;
                        this.SettingsNotSaved = true;
                    }
                }
            }
        }
        #endregion

        #region Form Events
        private void ScheduleBuilderControl_Load(object sender, EventArgs e)
        {
            AssignCloseActiveEditorsonOutSideClick(FormMain.Instance.ribbonControl);
            AssignCloseActiveEditorsonOutSideClick(pnHeader);
        }

        private void xtraTabControlPublications_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            LoadPublication();
        }
        #endregion

        #region Pricing Sections
        private void LoadPricingOptions(PublicationControl publicationControl)
        {
            _allowToSave = false;
            BusinessClasses.Publication publication = publicationControl.Publication;
            switch (publication.AdPricingStrategy)
            {
                case BusinessClasses.AdPricingStrategies.StandartPCI:
                    FormMain.Instance.buttonItemAdPricingColumnInches.Checked = true;
                    FormMain.Instance.buttonItemAdPricingFlat.Checked = false;
                    FormMain.Instance.buttonItemAdPricingPagePercent.Checked = false;
                    break;
                case BusinessClasses.AdPricingStrategies.FlatModular:
                    FormMain.Instance.buttonItemAdPricingColumnInches.Checked = false;
                    FormMain.Instance.buttonItemAdPricingFlat.Checked = true;
                    FormMain.Instance.buttonItemAdPricingPagePercent.Checked = false;
                    break;
                case BusinessClasses.AdPricingStrategies.SharePage:
                    FormMain.Instance.buttonItemAdPricingColumnInches.Checked = false;
                    FormMain.Instance.buttonItemAdPricingFlat.Checked = false;
                    FormMain.Instance.buttonItemAdPricingPagePercent.Checked = true;
                    break;
            }
            FormatAccordingPricingOptions(publicationControl);
            _allowToSave = true;
        }

        private void FormatAccordingPricingOptions(PublicationControl publicationControl)
        {
            BusinessClasses.Publication publication = publicationControl.Publication;
            switch (publication.AdPricingStrategy)
            {
                case BusinessClasses.AdPricingStrategies.StandartPCI:
                    FormMain.Instance.itemContainerAdSizeStandart.Visible = true;
                    FormMain.Instance.itemContainerAdSizeSharePage.Visible = false;
                    FormMain.Instance.checkBoxItemAdSizeStandartSquare.Enabled = false;
                    FormMain.Instance.checkBoxItemAdSizeStandartSquare.Checked = true;
                    FormMain.Instance.buttonItemColorOptionsPCI.Enabled = publicationControl.Publication.ColorOption != BusinessClasses.ColorOptions.BlackWhite;
                    FormMain.Instance.spinEditCostPerInch.Enabled = publicationControl.Publication.ColorOption != BusinessClasses.ColorOptions.BlackWhite & publicationControl.Publication.ColorPricing == BusinessClasses.ColorPricingType.CostPerInch;

                    publicationControl.gridBandPCIRate.Caption = "PCI";
                    publicationControl.gridBandADRate.Caption = "Cost (B&W)";
                    publicationControl.gridColumnPCIRate.OptionsColumn.ReadOnly = false;
                    publicationControl.gridColumnPCIRate.OptionsColumn.AllowEdit = true;
                    publicationControl.gridColumnPCIRate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                    publicationControl.gridColumnPCIRate.AppearanceCell.ForeColor = Color.Black;
                    publicationControl.repositoryItemSpinEditPCIRateDisplay.Appearance.ForeColor = Color.Black;
                    publicationControl.repositoryItemSpinEditPCIRateDisplay.AppearanceDisabled.ForeColor = Color.Black;
                    publicationControl.repositoryItemSpinEditPCIRateDisplay.AppearanceFocused.ForeColor = Color.Black;
                    publicationControl.repositoryItemSpinEditPCIRateDisplay.AppearanceReadOnly.ForeColor = Color.Black;
                    publicationControl.repositoryItemSpinEditPCIRateDisplayFirstRow.Appearance.ForeColor = Color.Black;
                    publicationControl.repositoryItemSpinEditPCIRateDisplayFirstRow.AppearanceDisabled.ForeColor = Color.Black;
                    publicationControl.repositoryItemSpinEditPCIRateDisplayFirstRow.AppearanceFocused.ForeColor = Color.Black;
                    publicationControl.repositoryItemSpinEditPCIRateDisplayFirstRow.AppearanceReadOnly.ForeColor = Color.Black;
                    publicationControl.gridColumnADRate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    publicationControl.gridColumnADRate.AppearanceCell.ForeColor = Color.Gray;
                    publicationControl.repositoryItemSpinEditADRateDisplay.Appearance.ForeColor = Color.Gray;
                    publicationControl.repositoryItemSpinEditADRateDisplay.AppearanceDisabled.ForeColor = Color.Gray;
                    publicationControl.repositoryItemSpinEditADRateDisplay.AppearanceFocused.ForeColor = Color.Gray;
                    publicationControl.repositoryItemSpinEditADRateDisplay.AppearanceReadOnly.ForeColor = Color.Gray;
                    publicationControl.repositoryItemSpinEditADRateDisplayNull.Appearance.ForeColor = Color.Gray;
                    publicationControl.repositoryItemSpinEditADRateDisplayNull.AppearanceDisabled.ForeColor = Color.Gray;
                    publicationControl.repositoryItemSpinEditADRateDisplayNull.AppearanceFocused.ForeColor = Color.Gray;
                    publicationControl.repositoryItemSpinEditADRateDisplayNull.AppearanceReadOnly.ForeColor = Color.Gray;
                    publicationControl.repositoryItemSpinEditADRateDisplayFirstRow.Appearance.ForeColor = Color.Gray;
                    publicationControl.repositoryItemSpinEditADRateDisplayFirstRow.AppearanceDisabled.ForeColor = Color.Gray;
                    publicationControl.repositoryItemSpinEditADRateDisplayFirstRow.AppearanceFocused.ForeColor = Color.Gray;
                    publicationControl.repositoryItemSpinEditADRateDisplayFirstRow.AppearanceReadOnly.ForeColor = Color.Gray;
                    publicationControl.repositoryItemSpinEditADRateDisplayNullFirstRow.Appearance.ForeColor = Color.Gray;
                    publicationControl.repositoryItemSpinEditADRateDisplayNullFirstRow.AppearanceDisabled.ForeColor = Color.Gray;
                    publicationControl.repositoryItemSpinEditADRateDisplayNullFirstRow.AppearanceFocused.ForeColor = Color.Gray;
                    publicationControl.repositoryItemSpinEditADRateDisplayNullFirstRow.AppearanceReadOnly.ForeColor = Color.Gray;
                    break;
                case BusinessClasses.AdPricingStrategies.FlatModular:
                    FormMain.Instance.itemContainerAdSizeStandart.Visible = true;
                    FormMain.Instance.itemContainerAdSizeSharePage.Visible = false;
                    FormMain.Instance.checkBoxItemAdSizeStandartSquare.Enabled = true;
                    FormMain.Instance.checkBoxItemAdSizeStandartSquare.Enabled = true;
                    FormMain.Instance.buttonItemColorOptionsPCI.Enabled = publication.SizeOptions.EnableSquare & publicationControl.Publication.ColorOption != BusinessClasses.ColorOptions.BlackWhite;
                    FormMain.Instance.spinEditCostPerInch.Enabled = publication.SizeOptions.EnableSquare & publicationControl.Publication.ColorOption != BusinessClasses.ColorOptions.BlackWhite & publicationControl.Publication.ColorPricing == BusinessClasses.ColorPricingType.CostPerInch;

                    publicationControl.gridBandPCIRate.Caption = "Package PCI";
                    publicationControl.gridBandADRate.Caption = "Package Rate";
                    publicationControl.gridColumnPCIRate.OptionsColumn.ReadOnly = true;
                    publicationControl.gridColumnPCIRate.OptionsColumn.AllowEdit = false;
                    publicationControl.gridColumnPCIRate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    publicationControl.gridColumnPCIRate.AppearanceCell.ForeColor = Color.Gray;
                    publicationControl.repositoryItemSpinEditPCIRateDisplay.Appearance.ForeColor = Color.Gray;
                    publicationControl.repositoryItemSpinEditPCIRateDisplay.AppearanceDisabled.ForeColor = Color.Gray;
                    publicationControl.repositoryItemSpinEditPCIRateDisplay.AppearanceFocused.ForeColor = Color.Gray;
                    publicationControl.repositoryItemSpinEditPCIRateDisplay.AppearanceReadOnly.ForeColor = Color.Gray;
                    publicationControl.repositoryItemSpinEditPCIRateDisplayFirstRow.Appearance.ForeColor = Color.Gray;
                    publicationControl.repositoryItemSpinEditPCIRateDisplayFirstRow.AppearanceDisabled.ForeColor = Color.Gray;
                    publicationControl.repositoryItemSpinEditPCIRateDisplayFirstRow.AppearanceFocused.ForeColor = Color.Gray;
                    publicationControl.repositoryItemSpinEditPCIRateDisplayFirstRow.AppearanceReadOnly.ForeColor = Color.Gray;
                    publicationControl.gridColumnADRate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                    publicationControl.gridColumnADRate.AppearanceCell.ForeColor = Color.Black;
                    publicationControl.repositoryItemSpinEditADRateDisplay.Appearance.ForeColor = Color.Black;
                    publicationControl.repositoryItemSpinEditADRateDisplay.AppearanceDisabled.ForeColor = Color.Black;
                    publicationControl.repositoryItemSpinEditADRateDisplay.AppearanceFocused.ForeColor = Color.Black;
                    publicationControl.repositoryItemSpinEditADRateDisplay.AppearanceReadOnly.ForeColor = Color.Black;
                    publicationControl.repositoryItemSpinEditADRateDisplayNull.Appearance.ForeColor = Color.Black;
                    publicationControl.repositoryItemSpinEditADRateDisplayNull.AppearanceDisabled.ForeColor = Color.Black;
                    publicationControl.repositoryItemSpinEditADRateDisplayNull.AppearanceFocused.ForeColor = Color.Black;
                    publicationControl.repositoryItemSpinEditADRateDisplayNull.AppearanceReadOnly.ForeColor = Color.Black;
                    publicationControl.repositoryItemSpinEditADRateDisplayFirstRow.Appearance.ForeColor = Color.Black;
                    publicationControl.repositoryItemSpinEditADRateDisplayFirstRow.AppearanceDisabled.ForeColor = Color.Black;
                    publicationControl.repositoryItemSpinEditADRateDisplayFirstRow.AppearanceFocused.ForeColor = Color.Black;
                    publicationControl.repositoryItemSpinEditADRateDisplayFirstRow.AppearanceReadOnly.ForeColor = Color.Black;
                    publicationControl.repositoryItemSpinEditADRateDisplayNullFirstRow.Appearance.ForeColor = Color.Black;
                    publicationControl.repositoryItemSpinEditADRateDisplayNullFirstRow.AppearanceDisabled.ForeColor = Color.Black;
                    publicationControl.repositoryItemSpinEditADRateDisplayNullFirstRow.AppearanceFocused.ForeColor = Color.Black;
                    publicationControl.repositoryItemSpinEditADRateDisplayNullFirstRow.AppearanceReadOnly.ForeColor = Color.Black;
                    break;
                case BusinessClasses.AdPricingStrategies.SharePage:
                    FormMain.Instance.itemContainerAdSizeStandart.Visible = false;
                    FormMain.Instance.itemContainerAdSizeSharePage.Visible = true;
                    FormMain.Instance.checkBoxItemAdSizeStandartSquare.Enabled = false;
                    FormMain.Instance.buttonItemColorOptionsPCI.Enabled = false;
                    FormMain.Instance.spinEditCostPerInch.Enabled = false;
                    FormMain.Instance.spinEditCostPerInch.Value = 0;

                    publicationControl.gridBandPCIRate.Caption = "Package PCI";
                    publicationControl.gridBandADRate.Caption = "Package Rate";
                    publicationControl.gridColumnPCIRate.OptionsColumn.ReadOnly = true;
                    publicationControl.gridColumnPCIRate.OptionsColumn.AllowEdit = false;
                    publicationControl.gridColumnPCIRate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    publicationControl.gridColumnPCIRate.AppearanceCell.ForeColor = Color.Gray;
                    publicationControl.repositoryItemSpinEditPCIRateDisplay.Appearance.ForeColor = Color.Gray;
                    publicationControl.repositoryItemSpinEditPCIRateDisplay.AppearanceDisabled.ForeColor = Color.Gray;
                    publicationControl.repositoryItemSpinEditPCIRateDisplay.AppearanceFocused.ForeColor = Color.Gray;
                    publicationControl.repositoryItemSpinEditPCIRateDisplay.AppearanceReadOnly.ForeColor = Color.Gray;
                    publicationControl.repositoryItemSpinEditPCIRateDisplayFirstRow.Appearance.ForeColor = Color.Gray;
                    publicationControl.repositoryItemSpinEditPCIRateDisplayFirstRow.AppearanceDisabled.ForeColor = Color.Gray;
                    publicationControl.repositoryItemSpinEditPCIRateDisplayFirstRow.AppearanceFocused.ForeColor = Color.Gray;
                    publicationControl.repositoryItemSpinEditPCIRateDisplayFirstRow.AppearanceReadOnly.ForeColor = Color.Gray;
                    publicationControl.gridColumnADRate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                    publicationControl.gridColumnADRate.AppearanceCell.ForeColor = Color.Black;
                    publicationControl.repositoryItemSpinEditADRateDisplay.Appearance.ForeColor = Color.Black;
                    publicationControl.repositoryItemSpinEditADRateDisplay.AppearanceDisabled.ForeColor = Color.Black;
                    publicationControl.repositoryItemSpinEditADRateDisplay.AppearanceFocused.ForeColor = Color.Black;
                    publicationControl.repositoryItemSpinEditADRateDisplay.AppearanceReadOnly.ForeColor = Color.Black;
                    publicationControl.repositoryItemSpinEditADRateDisplayNull.Appearance.ForeColor = Color.Black;
                    publicationControl.repositoryItemSpinEditADRateDisplayNull.AppearanceDisabled.ForeColor = Color.Black;
                    publicationControl.repositoryItemSpinEditADRateDisplayNull.AppearanceFocused.ForeColor = Color.Black;
                    publicationControl.repositoryItemSpinEditADRateDisplayNull.AppearanceReadOnly.ForeColor = Color.Black;
                    publicationControl.repositoryItemSpinEditADRateDisplayFirstRow.Appearance.ForeColor = Color.Black;
                    publicationControl.repositoryItemSpinEditADRateDisplayFirstRow.AppearanceDisabled.ForeColor = Color.Black;
                    publicationControl.repositoryItemSpinEditADRateDisplayFirstRow.AppearanceFocused.ForeColor = Color.Black;
                    publicationControl.repositoryItemSpinEditADRateDisplayFirstRow.AppearanceReadOnly.ForeColor = Color.Black;
                    publicationControl.repositoryItemSpinEditADRateDisplayNullFirstRow.Appearance.ForeColor = Color.Black;
                    publicationControl.repositoryItemSpinEditADRateDisplayNullFirstRow.AppearanceDisabled.ForeColor = Color.Black;
                    publicationControl.repositoryItemSpinEditADRateDisplayNullFirstRow.AppearanceFocused.ForeColor = Color.Black;
                    publicationControl.repositoryItemSpinEditADRateDisplayNullFirstRow.AppearanceReadOnly.ForeColor = Color.Black;
                    break;
            }
            FormMain.Instance.ribbonBarAdSize.RecalcLayout();
            FormMain.Instance.ribbonPanelBuildSchedules.PerformLayout();
        }

        public void buttonItemAdPricingColumnInches_Click(object sender, EventArgs e)
        {
            PublicationControl publicationControl = xtraTabControlPublications.SelectedTabPage as PublicationControl;
            if (publicationControl != null)
            {
                BusinessClasses.Publication publication = publicationControl.Publication;
                string oldPricingStrategy = string.Empty;
                switch (publication.AdPricingStrategy)
                {
                    case BusinessClasses.AdPricingStrategies.StandartPCI:
                        oldPricingStrategy = "Column Inches";
                        break;
                    case BusinessClasses.AdPricingStrategies.FlatModular:
                        oldPricingStrategy = "Flat Rate";
                        break;
                    case BusinessClasses.AdPricingStrategies.SharePage:
                        oldPricingStrategy = "% Share of Page";
                        break;
                }

                string newPricingStrategy = string.Empty;
                Image newPricingStrategyImage = null;
                if (sender == FormMain.Instance.buttonItemAdPricingColumnInches)
                {
                    newPricingStrategy = "Column Inches";
                    newPricingStrategyImage = Properties.Resources.ColumnInchesBig;
                }
                else if (sender == FormMain.Instance.buttonItemAdPricingFlat)
                {
                    newPricingStrategy = "Flat Rate";
                    newPricingStrategyImage = Properties.Resources.FlatRateBig;
                }
                else if (sender == FormMain.Instance.buttonItemAdPricingPagePercent)
                {
                    newPricingStrategy = "% Share of Page";
                    newPricingStrategyImage = Properties.Resources.SharePageBig;
                }
                if (!newPricingStrategy.Equals(oldPricingStrategy))
                {
                    using (ToolForms.FormChangeAdStrategyWarning warningForm = new ToolForms.FormChangeAdStrategyWarning())
                    {
                        warningForm.labelControlText.Text = string.Format("You are changing the Pricing Strategy from<br><b>{0}</b> to <b>{1}</b>.<br><br>Do you want to continue?", new object[] { oldPricingStrategy, newPricingStrategy });
                        warningForm.pictureBoxImage.Image = newPricingStrategyImage;
                        if (warningForm.ShowDialog() == DialogResult.Yes)
                        {
                            using (ToolForms.FormChangeAdStrategy form = new ToolForms.FormChangeAdStrategy())
                            {
                                form.laPublication.Text = publication.Name;
                                form.pictureBoxImage.Image = newPricingStrategyImage;
                                DialogResult result = publication.Inserts.Count > 0 ? form.ShowDialog() : DialogResult.OK;
                                if (result == DialogResult.OK)
                                {
                                    if (publication.Inserts.Count > 0)
                                    {
                                        if (form.rbSave.Checked)
                                            foreach (var insert in publication.Inserts)
                                                insert.SaveRates();
                                        if (form.rbReset.Checked)
                                            foreach (var insert in publication.Inserts)
                                                insert.ResetRates();
                                        if (form.rbDelete.Checked)
                                            publication.Inserts.Clear();
                                        if (form.ckDeleteAllColorRates.Checked)
                                            foreach (var insert in publication.Inserts)
                                            {
                                                insert.ColorPricing = 0;
                                                insert.ColorPricingPercent = 0;
                                                publication.ColorInchRate = 0;
                                            }
                                        if (form.ckDeleteAllDiscounts.Checked)
                                            foreach (var insert in publication.Inserts)
                                            {
                                                insert.Discounts = 0;
                                            }
                                        if (form.ckDeleteAllAdNotes.Checked)
                                            foreach (var insert in publication.Inserts)
                                            {
                                                insert.CustomComment = string.Empty;
                                                insert.Comments.Clear();
                                                insert.Section = string.Empty;
                                                insert.Deadline = string.Empty;
                                                insert.Mechanicals = null;
                                            }
                                        if (form.ckDeleteAllAdNotes.Checked || form.ckDeleteAllColorRates.Checked || form.ckDeleteAllDiscounts.Checked)
                                            publicationControl.LoadInserts();
                                    }
                                    _allowToSave = false;
                                    FormMain.Instance.buttonItemAdPricingColumnInches.Checked = false;
                                    FormMain.Instance.buttonItemAdPricingFlat.Checked = false;
                                    FormMain.Instance.buttonItemAdPricingPagePercent.Checked = false;
                                    _allowToSave = true;
                                    (sender as DevComponents.DotNetBar.ButtonItem).Checked = true;
                                }
                            }
                        }
                    }
                }
            }
        }

        public void buttonItemAdPricing_CheckedChanged(object sender, EventArgs e)
        {
            PublicationControl publicationControl = xtraTabControlPublications.SelectedTabPage as PublicationControl;
            if (publicationControl != null && _allowToSave)
            {
                BusinessClasses.Publication publication = publicationControl.Publication;
                if (FormMain.Instance.buttonItemAdPricingColumnInches.Checked)
                {
                    if (publication.AdPricingStrategy == BusinessClasses.AdPricingStrategies.SharePage)
                        publication.SizeOptions.ResetToDefaults(BusinessClasses.AdPricingStrategies.StandartPCI);
                    publication.AdPricingStrategy = BusinessClasses.AdPricingStrategies.StandartPCI;
                }
                else if (FormMain.Instance.buttonItemAdPricingFlat.Checked || FormMain.Instance.buttonItemAdPricingPagePercent.Checked)
                {
                    if (FormMain.Instance.buttonItemAdPricingFlat.Checked)
                    {
                        BusinessClasses.AdPricingStrategies prevStrategy = publication.AdPricingStrategy;
                        publication.AdPricingStrategy = BusinessClasses.AdPricingStrategies.FlatModular;
                        if (prevStrategy == BusinessClasses.AdPricingStrategies.SharePage)
                        {
                            publication.SizeOptions.ResetToDefaults(BusinessClasses.AdPricingStrategies.FlatModular);
                        }
                    }
                    else if (FormMain.Instance.buttonItemAdPricingPagePercent.Checked)
                    {
                        BusinessClasses.ColorPricingType prevColorPricing = publication.ColorPricing;
                        if (publication.AdPricingStrategy != BusinessClasses.AdPricingStrategies.SharePage)
                            publication.SizeOptions.ResetToDefaults(BusinessClasses.AdPricingStrategies.SharePage);
                        publication.AdPricingStrategy = BusinessClasses.AdPricingStrategies.SharePage;
                        if (prevColorPricing == BusinessClasses.ColorPricingType.CostPerInch)
                        {
                            switch (BusinessClasses.ListManager.Instance.DefaultColorPricing)
                            {
                                case BusinessClasses.ColorPricingType.CostPerAd:
                                    buttonItemColorOptions_Click(FormMain.Instance.buttonItemColorOptionsCostPerAd, null);
                                    break;
                                case BusinessClasses.ColorPricingType.PercentOfAdRate:
                                    buttonItemColorOptions_Click(FormMain.Instance.buttonItemColorOptionsPercentOfAd, null);
                                    break;
                                case BusinessClasses.ColorPricingType.ColorIncluded:
                                    buttonItemColorOptions_Click(FormMain.Instance.buttonItemColorOptionsIncluded, null);
                                    break;
                                case BusinessClasses.ColorPricingType.CostPerInch:
                                    buttonItemColorOptions_Click(FormMain.Instance.buttonItemColorOptionsPCI, null);
                                    break;
                            }
                        }
                    }
                }
                FormatAccordingPricingOptions(publicationControl);
                LoadSizeOptions(publicationControl);
                LoadColorOptions(publicationControl);
                publicationControl.LoadInserts();
                publicationControl.UpdateTotals();
                this.SettingsNotSaved = true;
            }
        }
        #endregion

        #region Size Section
        private void LoadSizeOptions(PublicationControl publicationControl)
        {
            _allowToSave = false;
            BusinessClasses.SizeOptions sizeOptions = publicationControl.Publication.SizeOptions;
            FormMain.Instance.comboBoxEditStandartPageSize.Properties.Items.Clear();
            FormMain.Instance.comboBoxEditStandartPageSize.Properties.Items.AddRange(BusinessClasses.ListManager.Instance.PageSizes.ToArray());
            FormMain.Instance.comboBoxEditSharePagePageSize.Properties.Items.Clear();
            FormMain.Instance.comboBoxEditSharePagePageSize.Properties.Items.AddRange(BusinessClasses.ListManager.Instance.PageSizes.ToArray());
            FormMain.Instance.comboBoxEditRateCard.Properties.Items.Clear();
            FormMain.Instance.comboBoxEditRateCard.Properties.Items.AddRange(BusinessClasses.ListManager.Instance.ShareUnits.Select(x => x.RateCard).Distinct().ToArray());
            FormMain.Instance.checkBoxItemStandartPageSize.Checked = sizeOptions.EnablePageSize;
            FormMain.Instance.comboBoxEditStandartPageSize.EditValue = sizeOptions.PageSize;
            FormMain.Instance.checkBoxItemSharePagePageSize.Checked = sizeOptions.EnablePageSize;
            FormMain.Instance.comboBoxEditSharePagePageSize.EditValue = sizeOptions.PageSize;
            switch (publicationControl.Publication.AdPricingStrategy)
            {
                case BusinessClasses.AdPricingStrategies.StandartPCI:
                case BusinessClasses.AdPricingStrategies.FlatModular:
                    FormMain.Instance.checkBoxItemAdSizeStandartSquare.Checked = sizeOptions.EnableSquare;
                    FormMain.Instance.spinEditStandartWidth.Value = (decimal)sizeOptions.Width;
                    FormMain.Instance.spinEditStandartHeight.Value = (decimal)sizeOptions.Height;
                    break;
                case BusinessClasses.AdPricingStrategies.SharePage:
                    FormMain.Instance.comboBoxEditRateCard.EditValue = sizeOptions.RateCard;
                    FormMain.Instance.comboBoxEditPercentOfPage.Properties.Items.Clear();
                    if (!string.IsNullOrEmpty(sizeOptions.RateCard))
                        FormMain.Instance.comboBoxEditPercentOfPage.Properties.Items.AddRange(BusinessClasses.ListManager.Instance.ShareUnits.Where(x => x.RateCard.Equals(sizeOptions.RateCard)).Select(x => x.PercentOfPage).Distinct().ToArray());
                    FormMain.Instance.comboBoxEditPercentOfPage.EditValue = sizeOptions.PercentOfPage;
                    FormMain.Instance.checkedListBoxControlSharePageSquare.Items.Clear();
                    BusinessClasses.ShareUnit[] shareUnits = new BusinessClasses.ShareUnit[] { };
                    if (FormMain.Instance.comboBoxEditPercentOfPage.EditValue != null && FormMain.Instance.comboBoxEditRateCard.EditValue != null)
                        shareUnits = BusinessClasses.ListManager.Instance.ShareUnits.Where(x => x.RateCard.Equals(FormMain.Instance.comboBoxEditRateCard.EditValue.ToString()) && x.PercentOfPage.Equals(FormMain.Instance.comboBoxEditPercentOfPage.EditValue.ToString())).ToArray();
                    if (shareUnits.Length > 0)
                    {
                        BusinessClasses.ShareUnit storedShareUnit = publicationControl.Publication.SizeOptions.RelatedShareUnit;
                        foreach (BusinessClasses.ShareUnit shareUnit in shareUnits)
                            FormMain.Instance.checkedListBoxControlSharePageSquare.Items.Add(shareUnit, shareUnit.Dimensions, shareUnit.Dimensions.Equals(storedShareUnit.Dimensions) ? CheckState.Checked : CheckState.Unchecked, true);
                        if (FormMain.Instance.checkedListBoxControlSharePageSquare.CheckedIndices.Count == 0)
                            FormMain.Instance.checkedListBoxControlSharePageSquare.Items[0].CheckState = CheckState.Checked;
                    }
                    break;
            }
            FormatAccordingSizeOptions(publicationControl);
            _allowToSave = true;
        }

        private void SetSizeOptions(PublicationControl publicationControl)
        {
            BusinessClasses.SizeOptions sizeOptions = publicationControl.Publication.SizeOptions;
            switch (publicationControl.Publication.AdPricingStrategy)
            {
                case BusinessClasses.AdPricingStrategies.StandartPCI:
                case BusinessClasses.AdPricingStrategies.FlatModular:
                    sizeOptions.ResetToDefaults(BusinessClasses.AdPricingStrategies.StandartPCI);
                    _allowToSave = false;
                    FormMain.Instance.comboBoxEditRateCard.EditValue = null;
                    FormMain.Instance.comboBoxEditPercentOfPage.EditValue = null;
                    FormMain.Instance.checkedListBoxControlSharePageSquare.Items.Clear();
                    _allowToSave = true;

                    sizeOptions.EnableSquare = FormMain.Instance.checkBoxItemAdSizeStandartSquare.Checked;
                    sizeOptions.Width = sizeOptions.EnableSquare ? (double)FormMain.Instance.spinEditStandartWidth.Value : 0;
                    sizeOptions.Height = sizeOptions.EnableSquare ? (double)FormMain.Instance.spinEditStandartHeight.Value : 0;
                    sizeOptions.EnablePageSize = FormMain.Instance.checkBoxItemStandartPageSize.Checked;
                    sizeOptions.PageSize = sizeOptions.EnablePageSize && !string.IsNullOrEmpty((string)FormMain.Instance.comboBoxEditStandartPageSize.EditValue) ? FormMain.Instance.comboBoxEditStandartPageSize.EditValue.ToString() : null;
                    break;
                case BusinessClasses.AdPricingStrategies.SharePage:
                    sizeOptions.ResetToDefaults(BusinessClasses.AdPricingStrategies.SharePage);
                    _allowToSave = false;
                    FormMain.Instance.checkBoxItemAdSizeStandartSquare.Checked = false;
                    FormMain.Instance.spinEditStandartHeight.Value = 0;
                    FormMain.Instance.spinEditStandartWidth.Value = 0;
                    _allowToSave = true;
                    sizeOptions.RateCard = !string.IsNullOrEmpty((string)FormMain.Instance.comboBoxEditRateCard.EditValue) ? FormMain.Instance.comboBoxEditRateCard.EditValue.ToString() : null;
                    sizeOptions.PercentOfPage = !string.IsNullOrEmpty((string)FormMain.Instance.comboBoxEditPercentOfPage.EditValue) ? FormMain.Instance.comboBoxEditPercentOfPage.EditValue.ToString() : null;
                    BusinessClasses.ShareUnit shareUnit = null;
                    foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in FormMain.Instance.checkedListBoxControlSharePageSquare.Items)
                        if (item.CheckState == CheckState.Checked)
                        {
                            shareUnit = item.Value as BusinessClasses.ShareUnit;
                            break;
                        }
                    sizeOptions.Height = shareUnit != null ? shareUnit.HeightValue : 0;
                    sizeOptions.HeightMeasure = shareUnit != null ? shareUnit.HeightMeasureUnit : sizeOptions.HeightMeasure;
                    sizeOptions.Width = shareUnit != null ? shareUnit.WidthValue : 0;
                    sizeOptions.WidthMeasure = shareUnit != null ? shareUnit.WidthMeasureUnit : sizeOptions.WidthMeasure;
                    sizeOptions.EnablePageSize = FormMain.Instance.checkBoxItemSharePagePageSize.Checked;
                    sizeOptions.EnableSquare = false;
                    sizeOptions.PageSize = sizeOptions.EnablePageSize && !string.IsNullOrEmpty((string)FormMain.Instance.comboBoxEditSharePagePageSize.EditValue) ? FormMain.Instance.comboBoxEditSharePagePageSize.EditValue.ToString() : null;
                    break;
            }
            FormatAccordingSizeOptions(publicationControl);
            publicationControl.LoadInserts();
            publicationControl.UpdateTotals();
        }

        private void FormatAccordingSizeOptions(PublicationControl publicationControl)
        {
            BusinessClasses.SizeOptions sizeOptions = publicationControl.Publication.SizeOptions;
            FormMain.Instance.spinEditStandartHeight.Enabled = sizeOptions.EnableSquare;
            FormMain.Instance.spinEditStandartWidth.Enabled = sizeOptions.EnableSquare;
            FormMain.Instance.controlContainerItemEqualSign.Visible = sizeOptions.Square.HasValue;
            FormMain.Instance.controlContainerItemSquareMetric.Visible = sizeOptions.Square.HasValue;
            FormMain.Instance.controlContainerItemSquareValue.Visible = sizeOptions.Square.HasValue;
            FormMain.Instance.laStandartSquareValue.Text = sizeOptions.Square.HasValue ? sizeOptions.Square.Value.ToString("#,##0.00") : string.Empty;
            FormMain.Instance.comboBoxEditStandartPageSize.Enabled = sizeOptions.EnablePageSize;
            FormMain.Instance.comboBoxEditSharePagePageSize.Enabled = sizeOptions.EnablePageSize;
            FormMain.Instance.buttonItemColorOptionsPCI.Enabled = sizeOptions.EnableSquare & publicationControl.Publication.AdPricingStrategy != BusinessClasses.AdPricingStrategies.SharePage & publicationControl.Publication.ColorOption != BusinessClasses.ColorOptions.BlackWhite;
            FormMain.Instance.spinEditCostPerInch.Enabled = sizeOptions.EnableSquare & publicationControl.Publication.AdPricingStrategy != BusinessClasses.AdPricingStrategies.SharePage & publicationControl.Publication.ColorOption != BusinessClasses.ColorOptions.BlackWhite & publicationControl.Publication.ColorPricing == BusinessClasses.ColorPricingType.CostPerInch;
            FormMain.Instance.comboBoxEditPercentOfPage.Enabled = !string.IsNullOrEmpty(sizeOptions.RateCard);
            FormMain.Instance.labelItemAdSizeSharePagePercentOfPage.ForeColor = !string.IsNullOrEmpty(sizeOptions.RateCard) ? Color.Black : Color.Gray;
            FormMain.Instance.checkedListBoxControlSharePageSquare.Enabled = FormMain.Instance.checkedListBoxControlSharePageSquare.ItemCount > 0;
            FormMain.Instance.checkedListBoxControlSharePageSquare.BackColor = FormMain.Instance.checkedListBoxControlSharePageSquare.ItemCount > 0 ? Color.White : Color.FromArgb(197, 214, 232);
            FormMain.Instance.labelItemAdSizeSharePageDimensions.ForeColor = FormMain.Instance.checkedListBoxControlSharePageSquare.ItemCount > 0 ? Color.Black : Color.Gray;
            FormMain.Instance.ribbonBarAdSize.RecalcLayout();
            FormMain.Instance.ribbonPanelBuildSchedules.PerformLayout();
        }

        public void checkBoxItemAdSizeStandartSquare_CheckedChanged(object sender, DevComponents.DotNetBar.CheckBoxChangeEventArgs e)
        {
            PublicationControl publicationControl = xtraTabControlPublications.SelectedTabPage as PublicationControl;
            if (publicationControl != null && _allowToSave)
            {
                if (!FormMain.Instance.checkBoxItemAdSizeStandartSquare.Checked)
                {
                    BusinessClasses.ColorPricingType prevColorPricing = publicationControl.Publication.ColorPricing;
                    if (prevColorPricing == BusinessClasses.ColorPricingType.CostPerInch)
                    {
                        switch (BusinessClasses.ListManager.Instance.DefaultColorPricing)
                        {
                            case BusinessClasses.ColorPricingType.CostPerAd:
                                buttonItemColorOptions_Click(FormMain.Instance.buttonItemColorOptionsCostPerAd, null);
                                break;
                            case BusinessClasses.ColorPricingType.PercentOfAdRate:
                                buttonItemColorOptions_Click(FormMain.Instance.buttonItemColorOptionsPercentOfAd, null);
                                break;
                            case BusinessClasses.ColorPricingType.ColorIncluded:
                                buttonItemColorOptions_Click(FormMain.Instance.buttonItemColorOptionsIncluded, null);
                                break;
                            case BusinessClasses.ColorPricingType.CostPerInch:
                                buttonItemColorOptions_Click(FormMain.Instance.buttonItemColorOptionsPCI, null);
                                break;
                        }
                    }
                }
                SetSizeOptions(publicationControl);
                this.SettingsNotSaved = true;
            }
        }

        public void checkBoxItemSizeOptions_CheckedChanged(object sender, DevComponents.DotNetBar.CheckBoxChangeEventArgs e)
        {
            PublicationControl publicationControl = xtraTabControlPublications.SelectedTabPage as PublicationControl;
            if (publicationControl != null && _allowToSave)
            {
                SetSizeOptions(publicationControl);
                this.SettingsNotSaved = true;
            }
        }

        public void spinEditStandart_EditValueChanged(object sender, EventArgs e)
        {
            PublicationControl publicationControl = xtraTabControlPublications.SelectedTabPage as PublicationControl;
            if (publicationControl != null && _allowToSave)
            {
                SetSizeOptions(publicationControl);
                this.SettingsNotSaved = true;
            }
        }

        public void comboBoxEditSizeOptions_EditValueChanged(object sender, EventArgs e)
        {
            PublicationControl publicationControl = xtraTabControlPublications.SelectedTabPage as PublicationControl;
            if (publicationControl != null && _allowToSave)
            {
                SetSizeOptions(publicationControl);
                this.SettingsNotSaved = true;
            }
        }

        public void comboBoxEditRateCard_EditValueChanged(object sender, EventArgs e)
        {
            PublicationControl publicationControl = xtraTabControlPublications.SelectedTabPage as PublicationControl;
            if (publicationControl != null && _allowToSave)
            {
                _allowToSave = false;
                FormMain.Instance.comboBoxEditPercentOfPage.EditValue = null;
                FormMain.Instance.comboBoxEditPercentOfPage.Properties.Items.Clear();
                if (FormMain.Instance.comboBoxEditRateCard.EditValue != null)
                    FormMain.Instance.comboBoxEditPercentOfPage.Properties.Items.AddRange(BusinessClasses.ListManager.Instance.ShareUnits.Where(x => x.RateCard.Equals(FormMain.Instance.comboBoxEditRateCard.EditValue.ToString())).Select(x => x.PercentOfPage).Distinct().ToArray());
                _allowToSave = true;
                SetSizeOptions(publicationControl);
                this.SettingsNotSaved = true;
            }
        }

        public void comboBoxEditPercentOfPage_EditValueChanged(object sender, EventArgs e)
        {
            PublicationControl publicationControl = xtraTabControlPublications.SelectedTabPage as PublicationControl;
            if (publicationControl != null && _allowToSave)
            {
                _allowToSave = false;
                FormMain.Instance.checkedListBoxControlSharePageSquare.Items.Clear();
                BusinessClasses.ShareUnit[] shareUnits = new BusinessClasses.ShareUnit[] { };
                if (FormMain.Instance.comboBoxEditPercentOfPage.EditValue != null && FormMain.Instance.comboBoxEditRateCard.EditValue != null)
                    shareUnits = BusinessClasses.ListManager.Instance.ShareUnits.Where(x => x.RateCard.Equals(FormMain.Instance.comboBoxEditRateCard.EditValue.ToString()) && x.PercentOfPage.Equals(FormMain.Instance.comboBoxEditPercentOfPage.EditValue.ToString())).ToArray();
                if (shareUnits.Length > 0)
                {
                    BusinessClasses.ShareUnit storedShareUnit = publicationControl.Publication.SizeOptions.RelatedShareUnit;
                    foreach (BusinessClasses.ShareUnit shareUnit in shareUnits)
                        FormMain.Instance.checkedListBoxControlSharePageSquare.Items.Add(shareUnit, shareUnit.Dimensions, shareUnit.Dimensions.Equals(storedShareUnit.Dimensions) ? CheckState.Checked : CheckState.Unchecked, true);
                    if (FormMain.Instance.checkedListBoxControlSharePageSquare.CheckedIndices.Count == 0)
                        FormMain.Instance.checkedListBoxControlSharePageSquare.Items[0].CheckState = CheckState.Checked;
                }
                _allowToSave = true;
                SetSizeOptions(publicationControl);
                this.SettingsNotSaved = true;
            }
        }

        public void checkedListBoxControlSharePageSquare_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            PublicationControl publicationControl = xtraTabControlPublications.SelectedTabPage as PublicationControl;
            if (publicationControl != null && _allowToSave)
            {
                foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in FormMain.Instance.checkedListBoxControlSharePageSquare.Items)
                    if (item != FormMain.Instance.checkedListBoxControlSharePageSquare.Items[e.Index])
                        item.CheckState = CheckState.Unchecked;
                SetSizeOptions(publicationControl);
                this.SettingsNotSaved = true;
            }
        }

        #endregion

        #region Color Section
        private void LoadColorOptions(PublicationControl publicationControl)
        {
            _allowToSave = false;
            BusinessClasses.Publication publication = publicationControl.Publication;
            switch (publication.ColorOption)
            {
                case BusinessClasses.ColorOptions.BlackWhite:
                    FormMain.Instance.buttonItemColorOptionsSingle.Checked = true;
                    break;
                case BusinessClasses.ColorOptions.SpotColor:
                case BusinessClasses.ColorOptions.FullColor:

                    if (publication.ColorOption == BusinessClasses.ColorOptions.SpotColor)
                        FormMain.Instance.buttonItemColorOptionsSpot.Checked = true;
                    else if (publication.ColorOption == BusinessClasses.ColorOptions.FullColor)
                        FormMain.Instance.buttonItemColorOptionsFull.Checked = true;
                    switch (publication.ColorPricing)
                    {
                        case BusinessClasses.ColorPricingType.CostPerAd:
                            FormMain.Instance.buttonItemColorOptionsCostPerAd.Checked = true;
                            break;
                        case BusinessClasses.ColorPricingType.PercentOfAdRate:
                            FormMain.Instance.buttonItemColorOptionsPercentOfAd.Checked = true;
                            break;
                        case BusinessClasses.ColorPricingType.ColorIncluded:
                            FormMain.Instance.buttonItemColorOptionsIncluded.Checked = true;
                            break;
                        case BusinessClasses.ColorPricingType.CostPerInch:
                            FormMain.Instance.buttonItemColorOptionsPCI.Checked = true;
                            FormMain.Instance.spinEditCostPerInch.Value = (decimal)publication.ColorInchRate;
                            break;
                    }
                    break;
            }
            FormatAccordingColorOptions(publicationControl);
            _allowToSave = true;
        }

        private void FormatAccordingColorOptions(PublicationControl publicationControl)
        {
            BusinessClasses.Publication publication = publicationControl.Publication;
            switch (publication.ColorOption)
            {
                case BusinessClasses.ColorOptions.BlackWhite:
                    FormMain.Instance.buttonItemColorOptionsCostPerAd.Enabled = false;
                    FormMain.Instance.buttonItemColorOptionsPercentOfAd.Enabled = false;
                    FormMain.Instance.buttonItemColorOptionsIncluded.Enabled = false;
                    FormMain.Instance.buttonItemColorOptionsPCI.Enabled = false;
                    FormMain.Instance.spinEditCostPerInch.Enabled = false;
                    FormMain.Instance.spinEditCostPerInch.Value = 0;

                    publicationControl.gridBandColorPricing.Caption = @"Black & White";
                    publicationControl.gridColumnColorPricing.OptionsColumn.ReadOnly = true;
                    publicationControl.gridColumnColorPricing.OptionsColumn.AllowEdit = false;
                    publicationControl.gridColumnColorPricingPercent.Visible = false;
                    publicationControl.gridColumnColorPricing.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    publicationControl.repositoryItemSpinEditColorPricingDisplay.NullText = "B-W";
                    publicationControl.repositoryItemSpinEditColorPricingDisplayFirstRow.NullText = "B-W";
                    publicationControl.repositoryItemSpinEditColorPricingDisplayFirstRow.Buttons[1].Visible = false;
                    break;
                case BusinessClasses.ColorOptions.SpotColor:
                case BusinessClasses.ColorOptions.FullColor:
                    FormMain.Instance.buttonItemColorOptionsCostPerAd.Enabled = true;
                    FormMain.Instance.buttonItemColorOptionsPercentOfAd.Enabled = true;
                    FormMain.Instance.buttonItemColorOptionsIncluded.Enabled = true;
                    FormMain.Instance.buttonItemColorOptionsPCI.Enabled = publication.SizeOptions.EnableSquare & publication.AdPricingStrategy != BusinessClasses.AdPricingStrategies.SharePage;
                    publicationControl.repositoryItemSpinEditColorPricingDisplay.NullText = "Included";
                    publicationControl.repositoryItemSpinEditColorPricingDisplayFirstRow.NullText = "Included";

                    if (publication.ColorOption == BusinessClasses.ColorOptions.SpotColor)
                    {
                        publicationControl.gridBandColorPricing.Caption = "Spot Color";
                    }
                    else if (publication.ColorOption == BusinessClasses.ColorOptions.FullColor)
                    {
                        publicationControl.gridBandColorPricing.Caption = "Full Color";
                    }
                    switch (publication.ColorPricing)
                    {
                        case BusinessClasses.ColorPricingType.CostPerAd:
                            FormMain.Instance.spinEditCostPerInch.Enabled = false;
                            FormMain.Instance.spinEditCostPerInch.Value = 0;

                            publicationControl.gridColumnColorPricing.OptionsColumn.AllowEdit = true;
                            publicationControl.gridColumnColorPricing.OptionsColumn.ReadOnly = false;
                            publicationControl.gridColumnColorPricingPercent.Visible = false;
                            publicationControl.gridColumnColorPricing.ColumnEdit = publicationControl.repositoryItemSpinEditColorPricingDisplay;
                            publicationControl.repositoryItemSpinEditColorPricingDisplayFirstRow.Buttons[1].Visible = true;
                            publicationControl.gridColumnColorPricing.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;

                            break;
                        case BusinessClasses.ColorPricingType.PercentOfAdRate:
                            FormMain.Instance.spinEditCostPerInch.Enabled = false;
                            FormMain.Instance.spinEditCostPerInch.Value = 0;

                            publicationControl.gridColumnColorPricing.OptionsColumn.AllowEdit = false;
                            publicationControl.gridColumnColorPricing.OptionsColumn.ReadOnly = true;
                            publicationControl.gridColumnColorPricingPercent.Visible = true;
                            publicationControl.gridColumnColorPricing.ColumnEdit = publicationControl.repositoryItemSpinEditDiscountRate;
                            publicationControl.repositoryItemSpinEditColorPricingDisplayFirstRow.Buttons[1].Visible = false;
                            publicationControl.gridColumnColorPricing.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
                            break;
                        case BusinessClasses.ColorPricingType.ColorIncluded:
                            FormMain.Instance.spinEditCostPerInch.Enabled = false;
                            FormMain.Instance.spinEditCostPerInch.Value = 0;

                            publicationControl.gridColumnColorPricing.OptionsColumn.AllowEdit = false;
                            publicationControl.gridColumnColorPricing.OptionsColumn.ReadOnly = true;
                            publicationControl.gridColumnColorPricingPercent.Visible = false;
                            publicationControl.gridColumnColorPricing.ColumnEdit = publicationControl.repositoryItemSpinEditColorPricingDisplay;
                            publicationControl.repositoryItemSpinEditColorPricingDisplayFirstRow.Buttons[1].Visible = false;
                            publicationControl.gridColumnColorPricing.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                            break;
                        case BusinessClasses.ColorPricingType.CostPerInch:
                            FormMain.Instance.spinEditCostPerInch.Enabled = publication.SizeOptions.EnableSquare & publication.AdPricingStrategy != BusinessClasses.AdPricingStrategies.SharePage;

                            publicationControl.gridColumnColorPricing.OptionsColumn.AllowEdit = false;
                            publicationControl.gridColumnColorPricing.OptionsColumn.ReadOnly = true;
                            publicationControl.gridColumnColorPricingPercent.Visible = false;
                            publicationControl.gridColumnColorPricing.ColumnEdit = publicationControl.repositoryItemSpinEditColorPricingDisplay;
                            publicationControl.repositoryItemSpinEditColorPricingDisplayFirstRow.Buttons[1].Visible = false;
                            publicationControl.gridColumnColorPricing.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                            break;
                    }
                    break;
            }
        }

        public void ColorOptions_Click(object sender, EventArgs e)
        {
            DevComponents.DotNetBar.ButtonItem button = (DevComponents.DotNetBar.ButtonItem)sender;
            _allowToSave = false;
            FormMain.Instance.buttonItemColorOptionsSingle.Checked = false;
            FormMain.Instance.buttonItemColorOptionsSpot.Checked = false;
            FormMain.Instance.buttonItemColorOptionsFull.Checked = false;
            _allowToSave = true;
            button.Checked = true;
        }

        public void ColorOptions_CheckedChanged(object sender, EventArgs e)
        {
            PublicationControl publicationControl = xtraTabControlPublications.SelectedTabPage as PublicationControl;
            if (publicationControl != null && _allowToSave)
            {
                BusinessClasses.Publication publication = publicationControl.Publication;
                BusinessClasses.ColorOptions prevColorOption = publication.ColorOption;
                if (FormMain.Instance.buttonItemColorOptionsSingle.Checked)
                {
                    publication.ColorOption = BusinessClasses.ColorOptions.BlackWhite;
                }
                else
                {
                    if (FormMain.Instance.buttonItemColorOptionsSpot.Checked)
                        publication.ColorOption = BusinessClasses.ColorOptions.SpotColor;
                    else if (FormMain.Instance.buttonItemColorOptionsFull.Checked)
                        publication.ColorOption = BusinessClasses.ColorOptions.FullColor;

                    if (prevColorOption == BusinessClasses.ColorOptions.BlackWhite)
                    {
                        switch (BusinessClasses.ListManager.Instance.DefaultColorPricing)
                        {
                            case BusinessClasses.ColorPricingType.CostPerAd:
                                buttonItemColorOptions_Click(FormMain.Instance.buttonItemColorOptionsCostPerAd, null);
                                break;
                            case BusinessClasses.ColorPricingType.PercentOfAdRate:
                                buttonItemColorOptions_Click(FormMain.Instance.buttonItemColorOptionsPercentOfAd, null);
                                break;
                            case BusinessClasses.ColorPricingType.ColorIncluded:
                                buttonItemColorOptions_Click(FormMain.Instance.buttonItemColorOptionsIncluded, null);
                                break;
                            case BusinessClasses.ColorPricingType.CostPerInch:
                                buttonItemColorOptions_Click(FormMain.Instance.buttonItemColorOptionsPCI, null);
                                break;
                        }
                    }
                }
                _allowToSave = false;
                FormatAccordingColorOptions(publicationControl);
                _allowToSave = true;
                publicationControl.LoadInserts();
                publicationControl.UpdateTotals();
                this.SettingsNotSaved = true;
            }
        }

        public void buttonItemColorOptions_Click(object sender, EventArgs e)
        {
            _allowToSave = false;
            FormMain.Instance.buttonItemColorOptionsCostPerAd.Checked = false;
            FormMain.Instance.buttonItemColorOptionsPercentOfAd.Checked = false;
            FormMain.Instance.buttonItemColorOptionsIncluded.Checked = false;
            FormMain.Instance.buttonItemColorOptionsPCI.Checked = false;
            _allowToSave = true;
            (sender as DevComponents.DotNetBar.ButtonItem).Checked = true;
        }

        public void buttonItemColorOptions_CheckedChanged(object sender, EventArgs e)
        {
            PublicationControl publicationControl = xtraTabControlPublications.SelectedTabPage as PublicationControl;
            if (publicationControl != null && _allowToSave)
            {
                BusinessClasses.Publication publication = publicationControl.Publication;
                FormMain.Instance.spinEditCostPerInch.Enabled = FormMain.Instance.buttonItemColorOptionsPCI.Checked;
                if (FormMain.Instance.buttonItemColorOptionsPCI.Checked)
                {
                    publication.ColorPricing = BusinessClasses.ColorPricingType.CostPerInch;
                }
                else if (FormMain.Instance.buttonItemColorOptionsCostPerAd.Checked)
                {
                    publication.ColorPricing = BusinessClasses.ColorPricingType.CostPerAd;
                    publication.ColorInchRate = 0;
                }
                else if (FormMain.Instance.buttonItemColorOptionsPercentOfAd.Checked)
                {
                    publication.ColorPricing = BusinessClasses.ColorPricingType.PercentOfAdRate;
                    publication.ColorInchRate = 0;
                }
                else if (FormMain.Instance.buttonItemColorOptionsIncluded.Checked)
                {
                    publication.ColorPricing = BusinessClasses.ColorPricingType.ColorIncluded;
                    publication.ColorInchRate = 0;
                }
                _allowToSave = false;
                FormatAccordingColorOptions(publicationControl);
                _allowToSave = true;
                publicationControl.LoadInserts();
                publicationControl.UpdateTotals();
                this.SettingsNotSaved = true;
            }
        }

        public void spinEditCostPerInch_EditValueChanged(object sender, EventArgs e)
        {
            PublicationControl publicationControl = xtraTabControlPublications.SelectedTabPage as PublicationControl;
            if (publicationControl != null && _allowToSave)
            {
                BusinessClasses.Publication publication = publicationControl.Publication;
                publication.ColorInchRate = (double)FormMain.Instance.spinEditCostPerInch.Value;
                publicationControl.LoadInserts();
                publicationControl.UpdateTotals();
                this.SettingsNotSaved = true;
            }
        }
        #endregion

        #region Buttons Clicks
        public void buttonItemAddInsert_Click(object sender, EventArgs e)
        {
            if (xtraTabControlPublications.SelectedTabPageIndex >= 0)
            {
                ((PublicationControl)xtraTabControlPublications.TabPages[xtraTabControlPublications.SelectedTabPageIndex]).AddInsert();
                FormMain.Instance.UpdateOutputTabs(this.LocalSchedule.Publications.Select(x => x.Inserts.Count).Sum() > 0);
                this.SettingsNotSaved = true;
            }
        }

        public void buttonItemDeleteInsert_Click(object sender, EventArgs e)
        {
            if (xtraTabControlPublications.SelectedTabPageIndex >= 0)
            {
                ((PublicationControl)xtraTabControlPublications.TabPages[xtraTabControlPublications.SelectedTabPageIndex]).DeleteInsert();
                FormMain.Instance.UpdateOutputTabs(this.LocalSchedule.Publications.Select(x => x.Inserts.Count).Sum() > 0);
                this.SettingsNotSaved = true;
            }
        }

        public void buttonItemCloneInsert_Click(object sender, EventArgs e)
        {
            if (xtraTabControlPublications.SelectedTabPageIndex >= 0)
            {
                ((PublicationControl)xtraTabControlPublications.TabPages[xtraTabControlPublications.SelectedTabPageIndex]).CloneInsert();
                FormMain.Instance.UpdateOutputTabs(this.LocalSchedule.Publications.Select(x => x.Inserts.Count).Sum() > 0);
                this.SettingsNotSaved = true;
            }
        }

        public void buttonItemSchedulesHelp_Click(object sender, EventArgs e)
        {
            BusinessClasses.HelpManager.Instance.OpenHelpLink("schedules");
        }

        public void buttonItemSchedulesSave_Click(object sender, EventArgs e)
        {
            if (xtraTabControlPublications.SelectedTabPageIndex >= 0)
            {
                SaveSchedule();
                AppManager.ShowInformation("Schedule Saved");
            }
        }

        public void buttonItemSchedulesSaveAs_Click(object sender, EventArgs e)
        {
            if (xtraTabControlPublications.SelectedTabPageIndex >= 0)
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
        }
        #endregion
    }
}
