using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace CalendarBuilder.CustomControls.SlideInfo
{
    public partial class SlideInfoControl : UserControl
    {
        private BusinessClasses.CalendarMonth _month = null;
        private bool _allowToSave = false;
        public string MonthTitle { get; set; }
        public bool SettingsNotSaved { get; set; }

        [Browsable(true)]
        [Category("Action")]
        public event EventHandler PropertiesLoaded;

        [Browsable(true)]
        [Category("Action")]
        public event EventHandler PropertiesApplied;

        [Browsable(true)]
        [Category("Action")]
        public event EventHandler PropertiesClosed;

        public SlideInfoControl()
        {
            InitializeComponent();

            #region Assign Properties Changed Event To Controls
            #region Basic
            buttonXBasicCalendarMonth.CheckedChanged += new EventHandler(propertiesControl_PropertiesChanged);
            buttonXBasicSlideTitle.CheckedChanged += new EventHandler(propertiesControl_PropertiesChanged);
            comboBoxEditBasicSlideTitle.EditValueChanged += new EventHandler(propertiesControl_PropertiesChanged);
            buttonXBasicBusinessName.CheckedChanged += new EventHandler(propertiesControl_PropertiesChanged);
            textEditBasicBusinessName.EditValueChanged += new EventHandler(propertiesControl_PropertiesChanged);
            buttonXBasicDecisionMaker.CheckedChanged += new EventHandler(propertiesControl_PropertiesChanged);
            textEditBasicDecisionMaker.EditValueChanged += new EventHandler(propertiesControl_PropertiesChanged);
            checkEditBasicApplyForAll.CheckedChanged += new EventHandler(propertiesControl_PropertiesChanged);

            comboBoxEditBasicSlideTitle.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            comboBoxEditBasicSlideTitle.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            comboBoxEditBasicSlideTitle.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
            textEditBasicBusinessName.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            textEditBasicBusinessName.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            textEditBasicBusinessName.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
            textEditBasicDecisionMaker.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            textEditBasicDecisionMaker.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            textEditBasicDecisionMaker.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
            #endregion

            #region Cost
            buttonXCostDigital.CheckedChanged += new EventHandler(propertiesControl_PropertiesChanged);
            spinEditCostDigital.EditValueChanged += new EventHandler(propertiesControl_PropertiesChanged);
            buttonXCostNewspaperCalculated.CheckedChanged += new EventHandler(propertiesControl_PropertiesChanged);
            buttonXCostNewspaperManual.CheckedChanged += new EventHandler(propertiesControl_PropertiesChanged);
            spinEditCostNewspaper.EditValueChanged += new EventHandler(propertiesControl_PropertiesChanged);
            buttonXCostTV.CheckedChanged += new EventHandler(propertiesControl_PropertiesChanged);
            spinEditCostTV.EditValueChanged += new EventHandler(propertiesControl_PropertiesChanged);
            checkEditCostApplyForAll.CheckedChanged += new EventHandler(propertiesControl_PropertiesChanged);

            spinEditCostDigital.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            spinEditCostDigital.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            spinEditCostDigital.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
            spinEditCostNewspaper.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            spinEditCostNewspaper.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            spinEditCostNewspaper.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
            spinEditCostTV.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            spinEditCostTV.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            spinEditCostTV.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
            #endregion

            #region Other Numbers
            #endregion

            #region Notes
            buttonXNotesCustomComment.CheckedChanged += new EventHandler(propertiesControl_PropertiesChanged);
            memoEditNotesCustomComment.EditValueChanged += new EventHandler(propertiesControl_PropertiesChanged);
            checkEditNotesCustomCommentApplyFoAll.CheckedChanged += new EventHandler(propertiesControl_PropertiesChanged);

            memoEditNotesCustomComment.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            memoEditNotesCustomComment.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            memoEditNotesCustomComment.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);

            buttonXOtherNumbersActiveDays.CheckedChanged += new EventHandler(propertiesControl_PropertiesChanged);
            spinEditOtherNumbersActiveDays.EditValueChanged += new EventHandler(propertiesControl_PropertiesChanged);
            buttonXOtherNumbersDigitalCPM.CheckedChanged += new EventHandler(propertiesControl_PropertiesChanged);
            spinEditOtherNumbersDigitalCPM.EditValueChanged += new EventHandler(propertiesControl_PropertiesChanged);
            buttonXOtherNumbersImpressions.CheckedChanged += new EventHandler(propertiesControl_PropertiesChanged);
            spinEditOtherNumbersImpressions.EditValueChanged += new EventHandler(propertiesControl_PropertiesChanged);
            buttonXOtherNumbersNewspaperAdsNumber.CheckedChanged += new EventHandler(propertiesControl_PropertiesChanged);
            spinEditOtherNumbersNewspaperAdsNumber.EditValueChanged += new EventHandler(propertiesControl_PropertiesChanged);
            checkEditOtherNumbersApplyForAll.CheckedChanged += new EventHandler(propertiesControl_PropertiesChanged);

            spinEditOtherNumbersActiveDays.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            spinEditOtherNumbersActiveDays.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            spinEditOtherNumbersActiveDays.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
            spinEditOtherNumbersDigitalCPM.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            spinEditOtherNumbersDigitalCPM.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            spinEditOtherNumbersDigitalCPM.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
            spinEditOtherNumbersImpressions.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            spinEditOtherNumbersImpressions.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            spinEditOtherNumbersImpressions.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
            spinEditOtherNumbersNewspaperAdsNumber.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            spinEditOtherNumbersNewspaperAdsNumber.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            spinEditOtherNumbersNewspaperAdsNumber.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
            #endregion

            #region Legend
            buttonXLegend.CheckedChanged += new EventHandler(propertiesControl_PropertiesChanged);
            gridViewLegend.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gridViewLegend_CellValueChanged);
            repositoryItemCheckEdit.EditValueChanged += new EventHandler(propertiesControl_PropertiesChanged);
            repositoryItemTextEdit.EditValueChanged += new EventHandler(propertiesControl_PropertiesChanged);
            checkEditLegendApplyForAll.CheckedChanged += new EventHandler(propertiesControl_PropertiesChanged);

            repositoryItemTextEdit.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            repositoryItemTextEdit.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            repositoryItemTextEdit.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
            #endregion

            #region Style
            buttonXThemeColorBlack.CheckedChanged += new EventHandler(propertiesControl_PropertiesChanged);
            buttonXThemeColorBlue.CheckedChanged += new EventHandler(propertiesControl_PropertiesChanged);
            buttonXThemeColorGray.CheckedChanged += new EventHandler(propertiesControl_PropertiesChanged);
            buttonXThemeColorGreen.CheckedChanged += new EventHandler(propertiesControl_PropertiesChanged);
            buttonXThemeColorOrange.CheckedChanged += new EventHandler(propertiesControl_PropertiesChanged);
            buttonXThemeColorTeal.CheckedChanged += new EventHandler(propertiesControl_PropertiesChanged);
            checkEditThemeColorApplyForAll.CheckedChanged += new EventHandler(propertiesControl_PropertiesChanged);

            buttonXLogo.CheckedChanged += new EventHandler(propertiesControl_PropertiesChanged);
            pictureEditLogo.EditValueChanged += new EventHandler(propertiesControl_PropertiesChanged);
            checkEditLogoApplyForAll.CheckedChanged += new EventHandler(propertiesControl_PropertiesChanged);

            buttonXStyleBigDate.CheckedChanged += new EventHandler(propertiesControl_PropertiesChanged);
            #endregion
            #endregion
        }

        public void LoadMonth(BusinessClasses.CalendarMonth month)
        {
            _month = month;
            LoadCurrentMonthData();
            if (this.PropertiesLoaded != null)
                this.PropertiesLoaded(null, null);
        }

        public void LoadCurrentMonthData()
        {
            if (_month != null)
            {
                _allowToSave = false;
                this.MonthTitle = "Slide Info - " + _month.StartDate.ToString("MMMM yyyy");

                #region Basic
                buttonXBasicCalendarMonth.Checked = _month.OutputData.ShowMonth;
                laBasicCalendarMonth.Text = _month.StartDate.ToString("MMMM yyyy");

                buttonXBasicSlideTitle.Checked = _month.OutputData.ShowHeader;
                comboBoxEditBasicSlideTitle.Properties.Items.Clear();
                comboBoxEditBasicSlideTitle.Properties.Items.AddRange(BusinessClasses.ListManager.Instance.OutputHeaders);
                if (!string.IsNullOrEmpty(_month.OutputData.Header))
                    comboBoxEditBasicSlideTitle.EditValue = _month.OutputData.Header;
                else
                    comboBoxEditBasicSlideTitle.EditValue = BusinessClasses.ListManager.Instance.OutputHeaders.FirstOrDefault();

                buttonXBasicBusinessName.Checked = _month.OutputData.ShowBusinessName;
                textEditBasicBusinessName.EditValue = !string.IsNullOrEmpty(_month.OutputData.BusinessName) ? _month.OutputData.BusinessName : _month.Parent.BusinessName;

                buttonXBasicDecisionMaker.Checked = _month.OutputData.ShowDecisionMaker;
                textEditBasicDecisionMaker.EditValue = !string.IsNullOrEmpty(_month.OutputData.DecisionMaker) ? _month.OutputData.DecisionMaker : _month.Parent.DecisionMaker;

                checkEditBasicApplyForAll.Checked = _month.OutputData.ApplyForAllBasic;
                #endregion

                #region Cost
                buttonXCostNewspaperCalculated.Checked = _month.OutputData.ShowPrintTotalCostCalculated;
                laCostNewspaperCalculated.Text = _month.OutputData.PrintTotalCostCalculated.ToString("$#,###.00");

                buttonXCostNewspaperManual.Checked = _month.OutputData.ShowPrintTotalCostManual;
                spinEditCostNewspaper.Value = _month.OutputData.PrintTotalCost.HasValue ? (decimal)_month.OutputData.PrintTotalCost.Value : 0;

                buttonXCostDigital.Checked = _month.OutputData.ShowDigitalTotalCost;
                spinEditCostDigital.Value = _month.OutputData.DigitalTotalCost.HasValue ? (decimal)_month.OutputData.DigitalTotalCost.Value : 0;

                buttonXCostTV.Checked = _month.OutputData.ShowTVTotalCost;
                spinEditCostTV.Value = _month.OutputData.TVTotalCost.HasValue ? (decimal)_month.OutputData.TVTotalCost.Value : 0;

                checkEditCostApplyForAll.Checked = _month.OutputData.ApplyForAlCost;
                #endregion

                #region Notes
                buttonXNotesCustomComment.Checked = _month.OutputData.ShowCustomComment;
                memoEditNotesCustomComment.EditValue = _month.OutputData.CustomComment;
                checkEditNotesCustomCommentApplyFoAll.Checked = _month.OutputData.ApplyForAllCustomComment;

                buttonXOtherNumbersActiveDays.Checked = _month.OutputData.ShowActiveDays;
                spinEditOtherNumbersActiveDays.Value = _month.OutputData.ActiveDays;

                buttonXOtherNumbersNewspaperAdsNumber.Checked = _month.OutputData.ShowPrintAdsNumber;
                spinEditOtherNumbersNewspaperAdsNumber.Value = _month.OutputData.PrintAdsNumber;

                buttonXOtherNumbersImpressions.Checked = _month.OutputData.ShowImpressions;
                spinEditOtherNumbersImpressions.Value = _month.OutputData.Impressions.HasValue ? (decimal)_month.OutputData.Impressions.Value : 0;

                buttonXOtherNumbersDigitalCPM.Checked = _month.OutputData.ShowDigitalCPM;
                spinEditOtherNumbersDigitalCPM.Value = _month.OutputData.DigitalCPM.HasValue ? (decimal)_month.OutputData.DigitalCPM.Value : 0;

                checkEditOtherNumbersApplyForAll.Checked = _month.OutputData.ApplyForAllOtherNumbers;
                #endregion

                #region Legend
                buttonXLegend.Checked = _month.OutputData.ShowLegend;
                gridControlLegend.DataSource = null;
                _month.OutputData.UpdateLegend();
                gridControlLegend.DataSource = new BindingList<BusinessClasses.CalendarLegend>(_month.OutputData.Legend);
                gridControlLegend.RefreshDataSource();
                checkEditLegendApplyForAll.Checked = _month.OutputData.ApplyForAllLegend;
                #endregion

                #region Style
                buttonXThemeColorBlack.Checked = false;
                buttonXThemeColorBlue.Checked = false;
                buttonXThemeColorGray.Checked = false;
                buttonXThemeColorGreen.Checked = false;
                buttonXThemeColorOrange.Checked = false;
                buttonXThemeColorTeal.Checked = false;
                switch (_month.OutputData.SlideColor)
                {
                    case "black":
                        buttonXThemeColorBlack.Checked = true;
                        break;
                    case "blue":
                        buttonXThemeColorBlue.Checked = true;
                        break;
                    case "gray":
                        buttonXThemeColorGray.Checked = true;
                        break;
                    case "green":
                        buttonXThemeColorGreen.Checked = true;
                        break;
                    case "orange":
                        buttonXThemeColorOrange.Checked = true;
                        break;
                    case "teal":
                        buttonXThemeColorTeal.Checked = true;
                        break;
                }
                checkEditThemeColorApplyForAll.Checked = _month.OutputData.ApplyForAllThemeColor;

                buttonXLogo.Checked = _month.OutputData.ShowLogo;
                pictureEditLogo.Image = _month.OutputData.Logo;
                checkEditLogoApplyForAll.Checked = _month.OutputData.ApplyForAllLogo;

                buttonXStyleBigDate.Checked = _month.OutputData.ShowBigDate;
                #endregion

                _allowToSave = true;
                this.SettingsNotSaved = false;
            }
        }

        public void SaveData()
        {
            if (_allowToSave)
            {
                #region Basic
                _month.OutputData.ShowMonth = buttonXBasicCalendarMonth.Checked;

                _month.OutputData.ShowHeader = buttonXBasicSlideTitle.Checked;
                _month.OutputData.Header = comboBoxEditBasicSlideTitle.EditValue != null ? comboBoxEditBasicSlideTitle.EditValue.ToString() : string.Empty;

                _month.OutputData.ShowBusinessName = buttonXBasicBusinessName.Checked;
                _month.OutputData.BusinessName = textEditBasicBusinessName.EditValue != null && !textEditBasicBusinessName.EditValue.ToString().Equals(_month.Parent.BusinessName) ? textEditBasicBusinessName.EditValue.ToString() : string.Empty;

                _month.OutputData.ShowDecisionMaker = buttonXBasicDecisionMaker.Checked;
                _month.OutputData.DecisionMaker = textEditBasicDecisionMaker.EditValue != null && !textEditBasicDecisionMaker.EditValue.ToString().Equals(_month.Parent.DecisionMaker) ? textEditBasicDecisionMaker.EditValue.ToString() : string.Empty;

                _month.OutputData.ApplyForAllBasic = checkEditBasicApplyForAll.Checked;
                if (_month.OutputData.ApplyForAllBasic)
                {
                    foreach (BusinessClasses.CalendarMonth month in _month.Parent.Months)
                    {
                        if (month != _month)
                        {
                            month.OutputData.ShowMonth = _month.OutputData.ShowMonth;
                            month.OutputData.ShowHeader = _month.OutputData.ShowHeader;
                            month.OutputData.Header = _month.OutputData.Header;
                            month.OutputData.ShowBusinessName = _month.OutputData.ShowBusinessName;
                            month.OutputData.BusinessName = _month.OutputData.BusinessName;
                            month.OutputData.ShowDecisionMaker = _month.OutputData.ShowDecisionMaker;
                            month.OutputData.DecisionMaker = _month.OutputData.DecisionMaker;
                            month.OutputData.ApplyForAllBasic = _month.OutputData.ApplyForAllBasic;
                        }
                    }
                }
                #endregion

                #region Cost
                _month.OutputData.ShowPrintTotalCostCalculated = buttonXCostNewspaperCalculated.Checked;

                _month.OutputData.ShowPrintTotalCostManual = buttonXCostNewspaperManual.Checked;
                _month.OutputData.PrintTotalCost = spinEditCostNewspaper.Value > 0 ? (double?)spinEditCostNewspaper.Value : null;

                _month.OutputData.ShowDigitalTotalCost = buttonXCostDigital.Checked;
                _month.OutputData.DigitalTotalCost = spinEditCostDigital.Value > 0 ? (double?)spinEditCostDigital.Value : null;

                _month.OutputData.ShowTVTotalCost = buttonXCostTV.Checked;
                _month.OutputData.TVTotalCost = spinEditCostTV.Value > 0 ? (double?)spinEditCostTV.Value : null;

                _month.OutputData.ApplyForAlCost = checkEditCostApplyForAll.Checked;
                if (_month.OutputData.ApplyForAlCost)
                {
                    foreach (BusinessClasses.CalendarMonth month in _month.Parent.Months)
                    {
                        if (month != _month)
                        {
                            month.OutputData.ShowPrintTotalCostCalculated = _month.OutputData.ShowPrintTotalCostCalculated;
                            month.OutputData.ShowPrintTotalCostManual = _month.OutputData.ShowPrintTotalCostManual;
                            month.OutputData.PrintTotalCost = _month.OutputData.PrintTotalCost;
                            month.OutputData.ShowDigitalTotalCost = _month.OutputData.ShowDigitalTotalCost;
                            month.OutputData.DigitalTotalCost = _month.OutputData.DigitalTotalCost;
                            month.OutputData.ShowTVTotalCost = _month.OutputData.ShowTVTotalCost;
                            month.OutputData.TVTotalCost = _month.OutputData.TVTotalCost;
                            month.OutputData.ApplyForAlCost = _month.OutputData.ApplyForAlCost;
                        }
                    }
                }
                #endregion

                #region Notes
                _month.OutputData.ShowCustomComment = buttonXNotesCustomComment.Checked;
                _month.OutputData.CustomComment = memoEditNotesCustomComment.EditValue != null ? memoEditNotesCustomComment.EditValue.ToString() : string.Empty;
                _month.OutputData.ApplyForAllCustomComment = checkEditNotesCustomCommentApplyFoAll.Checked;
                if (_month.OutputData.ApplyForAllCustomComment)
                {
                    foreach (BusinessClasses.CalendarMonth month in _month.Parent.Months)
                    {
                        if (month != _month)
                        {
                            month.OutputData.ShowCustomComment = _month.OutputData.ShowCustomComment;
                            month.OutputData.CustomComment = _month.OutputData.CustomComment;
                            month.OutputData.ApplyForAllCustomComment = _month.OutputData.ApplyForAllCustomComment;
                        }
                    }
                }

                _month.OutputData.ShowActiveDays = buttonXOtherNumbersActiveDays.Checked;
                _month.OutputData.ActiveDays = (int)spinEditOtherNumbersActiveDays.Value;

                _month.OutputData.ShowPrintAdsNumber = buttonXOtherNumbersNewspaperAdsNumber.Checked;
                _month.OutputData.PrintAdsNumber = (int)spinEditOtherNumbersNewspaperAdsNumber.Value;

                _month.OutputData.ShowImpressions = buttonXOtherNumbersImpressions.Checked;
                _month.OutputData.Impressions = spinEditOtherNumbersImpressions.Value > 0 ? (double?)spinEditOtherNumbersImpressions.Value : null;

                _month.OutputData.ShowDigitalCPM = buttonXOtherNumbersDigitalCPM.Checked;
                _month.OutputData.DigitalCPM = spinEditOtherNumbersDigitalCPM.Value > 0 ? (double?)spinEditOtherNumbersDigitalCPM.Value : null;

                _month.OutputData.ApplyForAllOtherNumbers = checkEditOtherNumbersApplyForAll.Checked;
                if (_month.OutputData.ApplyForAllOtherNumbers)
                {
                    foreach (BusinessClasses.CalendarMonth month in _month.Parent.Months)
                    {
                        if (month != _month)
                        {
                            month.OutputData.ShowActiveDays = _month.OutputData.ShowActiveDays;
                            month.OutputData.ActiveDays = _month.OutputData.ActiveDays;
                            month.OutputData.ShowPrintAdsNumber = _month.OutputData.ShowPrintAdsNumber;
                            month.OutputData.PrintAdsNumber = _month.OutputData.PrintAdsNumber;
                            month.OutputData.ShowImpressions = _month.OutputData.ShowImpressions;
                            month.OutputData.Impressions = _month.OutputData.Impressions;
                            month.OutputData.ShowDigitalCPM = _month.OutputData.ShowDigitalCPM;
                            month.OutputData.DigitalCPM = _month.OutputData.DigitalCPM;
                            month.OutputData.ApplyForAllOtherNumbers = _month.OutputData.ApplyForAllOtherNumbers;
                        }
                    }
                }
                #endregion

                #region Legend
                _month.OutputData.ShowLegend = buttonXLegend.Checked;
                gridViewLegend.CloseEditor();
                _month.OutputData.ApplyForAllLegend = checkEditLegendApplyForAll.Checked;
                if (_month.OutputData.ApplyForAllLegend)
                {
                    foreach (BusinessClasses.CalendarMonth month in _month.Parent.Months)
                    {
                        if (month != _month)
                        {
                            month.OutputData.ShowLegend = _month.OutputData.ShowLegend;
                            month.OutputData.ApplyForAllLegend = _month.OutputData.ApplyForAllLegend;
                            month.OutputData.Legend.Clear();
                            foreach (BusinessClasses.CalendarLegend legend in _month.OutputData.Legend)
                                month.OutputData.Legend.Add(legend.Clone());
                            month.OutputData.Logo = _month.OutputData.Logo;
                            month.OutputData.ApplyForAllLogo = _month.OutputData.ApplyForAllLogo;
                        }
                    }
                }
                #endregion

                #region Style
                if (buttonXThemeColorBlack.Checked)
                    _month.OutputData.SlideColor = "black";
                else if (buttonXThemeColorBlue.Checked)
                    _month.OutputData.SlideColor = "blue";
                else if (buttonXThemeColorGray.Checked)
                    _month.OutputData.SlideColor = "gray";
                else if (buttonXThemeColorGreen.Checked)
                    _month.OutputData.SlideColor = "green";
                else if (buttonXThemeColorOrange.Checked)
                    _month.OutputData.SlideColor = "orange";
                else if (buttonXThemeColorTeal.Checked)
                    _month.OutputData.SlideColor = "teal";
                _month.OutputData.ApplyForAllThemeColor = checkEditThemeColorApplyForAll.Checked;
                if (_month.OutputData.ApplyForAllThemeColor)
                {
                    foreach (BusinessClasses.CalendarMonth month in _month.Parent.Months)
                    {
                        if (month != _month)
                        {
                            month.OutputData.SlideColor = _month.OutputData.SlideColor;
                            month.OutputData.ApplyForAllThemeColor = _month.OutputData.ApplyForAllThemeColor;
                        }
                    }
                }

                _month.OutputData.ShowLogo = buttonXLogo.Checked;
                _month.OutputData.Logo = pictureEditLogo.Image;
                _month.OutputData.ApplyForAllLogo = checkEditLogoApplyForAll.Checked;
                if (_month.OutputData.ApplyForAllLogo)
                {
                    foreach (BusinessClasses.CalendarMonth month in _month.Parent.Months)
                    {
                        if (month != _month)
                        {
                            month.OutputData.ShowLogo = _month.OutputData.ShowLogo;
                            month.OutputData.Logo = _month.OutputData.Logo;
                            month.OutputData.ApplyForAllLogo = _month.OutputData.ApplyForAllLogo;
                        }
                    }
                }

                _month.OutputData.ShowBigDate = buttonXStyleBigDate.Checked;
                #endregion

                this.SettingsNotSaved = false;
            }
        }

        private void barLargeButtonItemApply_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveData();
            if (this.PropertiesApplied != null)
                this.PropertiesApplied(sender, e);
        }

        private void barLargeButtonItemClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.PropertiesClosed != null)
                this.PropertiesClosed(sender, e);
        }

        private void propertiesControl_PropertiesChanged(object sender, EventArgs e)
        {

        }

        #region Basic Event Handlers
        private void buttonXBasicCalendarMonth_CheckedChanged(object sender, EventArgs e)
        {
            laBasicCalendarMonth.Enabled = buttonXBasicCalendarMonth.Checked;
        }

        private void buttonXBasicSlideTitle_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxEditBasicSlideTitle.Enabled = buttonXBasicSlideTitle.Checked;
        }

        private void buttonXBasicBusinessName_CheckedChanged(object sender, EventArgs e)
        {
            textEditBasicBusinessName.Enabled = buttonXBasicBusinessName.Checked;
        }

        private void buttonXBasicDecisionMaker_CheckedChanged(object sender, EventArgs e)
        {
            textEditBasicDecisionMaker.Enabled = buttonXBasicDecisionMaker.Checked;
        }
        #endregion

        #region Cost Event Handlers
        private void buttonXCostNewspaperCalculated_Click(object sender, EventArgs e)
        {
            DevComponents.DotNetBar.ButtonX button = sender as DevComponents.DotNetBar.ButtonX;
            if (button != null)
            {
                if (!button.Checked)
                {
                    buttonXCostNewspaperCalculated.Checked = false;
                    buttonXCostNewspaperManual.Checked = false;
                    button.Checked = true;
                }
                else
                    button.Checked = false;
            }
        }

        private void buttonXCostNewspaperCalculated_CheckedChanged(object sender, EventArgs e)
        {
            laCostNewspaperCalculated.Enabled = buttonXCostNewspaperCalculated.Checked;
        }

        private void buttonXCostNewspaperManual_CheckedChanged(object sender, EventArgs e)
        {
            spinEditCostNewspaper.Enabled = buttonXCostNewspaperManual.Checked;
        }

        private void buttonXCostDigital_CheckedChanged(object sender, EventArgs e)
        {
            spinEditCostDigital.Enabled = buttonXCostDigital.Checked;
        }

        private void buttonXCostTV_CheckedChanged(object sender, EventArgs e)
        {
            spinEditCostTV.Enabled = buttonXCostTV.Checked;
        }
        #endregion

        #region Notes Event Handlers
        private void buttonXCustomComment_CheckedChanged(object sender, EventArgs e)
        {
            memoEditNotesCustomComment.Enabled = buttonXNotesCustomComment.Checked;
        }

        private void buttonXOtherNumbersActiveDays_CheckedChanged(object sender, EventArgs e)
        {
            spinEditOtherNumbersActiveDays.Enabled = buttonXOtherNumbersActiveDays.Checked;
        }

        private void buttonXOtherNumbersNewspaperAdsNumber_CheckedChanged(object sender, EventArgs e)
        {
            spinEditOtherNumbersNewspaperAdsNumber.Enabled = buttonXOtherNumbersNewspaperAdsNumber.Checked;
        }

        private void buttonXOtherNumbersImpressions_CheckedChanged(object sender, EventArgs e)
        {
            spinEditOtherNumbersImpressions.Enabled = buttonXOtherNumbersImpressions.Checked;
        }

        private void buttonXOtherNumbersDigitalCPM_CheckedChanged(object sender, EventArgs e)
        {
            spinEditOtherNumbersDigitalCPM.Enabled = buttonXOtherNumbersDigitalCPM.Checked;
        }
        #endregion

        #region Legend Event Handlers
        private void buttonXLegend_CheckedChanged(object sender, EventArgs e)
        {
            gridControlLegend.Enabled = buttonXLegend.Checked;
        }

        private void gridViewLegend_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            propertiesControl_PropertiesChanged(null, null);
        }
        #endregion

        #region Style Event Handlers
        private void buttonXThemeColor_Click(object sender, EventArgs e)
        {
            buttonXThemeColorBlack.Checked = false;
            buttonXThemeColorBlue.Checked = false;
            buttonXThemeColorGray.Checked = false;
            buttonXThemeColorGreen.Checked = false;
            buttonXThemeColorOrange.Checked = false;
            buttonXThemeColorTeal.Checked = false;
            (sender as DevComponents.DotNetBar.ButtonX).Checked = true;
        }

        private void buttonXLogo_CheckedChanged(object sender, EventArgs e)
        {
            pictureEditLogo.Enabled = buttonXLogo.Checked;
        }

        private void pictureEditLogo_Click(object sender, System.EventArgs e)
        {
            using (ToolForms.FormImageGallery form = new ToolForms.FormImageGallery())
            {
                if (form.ShowDialog() == DialogResult.OK && form.SelectedSource != null && form.SelectedSource.BigLogo != null)
                {
                    pictureEditLogo.Image = new System.Drawing.Bitmap(form.SelectedSource.BigLogo);
                }
            }
        }
        #endregion
    }
}
