using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MobileScheduleBuilder
{
    public partial class FormMain : Form
    {
        private static FormMain _instance = null;
        private Control _currentControl = null;

        private FormMain()
        {
            InitializeComponent();

            LoadCategories();

            #region Schedule Settings Events
            buttonItemScheduleHelp.Click += new EventHandler(CustomControls.ScheduleSettingsControl.Instance.buttonItemScheduleHelp_Click);
            buttonItemScheduleSave.Click += new EventHandler(CustomControls.ScheduleSettingsControl.Instance.buttonItemScheduleSettingsSave_Click);
            buttonItemScheduleSaveAs.Click += new EventHandler(CustomControls.ScheduleSettingsControl.Instance.buttonItemScheduleSettingsSaveAs_Click);
            comboBoxEditBusinessName.EditValueChanged += new EventHandler(CustomControls.ScheduleSettingsControl.Instance.SchedulePropertyEditValueChanged);
            comboBoxEditDecisionMaker.EditValueChanged += new EventHandler(CustomControls.ScheduleSettingsControl.Instance.SchedulePropertyEditValueChanged);
            dateEditFlightDatesStart.EditValueChanged += new EventHandler(CustomControls.ScheduleSettingsControl.Instance.SchedulePropertyEditValueChanged);
            dateEditFlightDatesStart.EditValueChanged += new EventHandler(CustomControls.ScheduleSettingsControl.Instance.FlightDateStartEditValueChanged);
            dateEditFlightDatesEnd.EditValueChanged += new EventHandler(CustomControls.ScheduleSettingsControl.Instance.SchedulePropertyEditValueChanged);
            dateEditFlightDatesStart.EditValueChanged += new EventHandler(CustomControls.ScheduleSettingsControl.Instance.CalcWeeksOnFlightDatesChange);
            dateEditFlightDatesEnd.EditValueChanged += new EventHandler(CustomControls.ScheduleSettingsControl.Instance.CalcWeeksOnFlightDatesChange);
            dateEditFlightDatesStart.CloseUp += new DevExpress.XtraEditors.Controls.CloseUpEventHandler(CustomControls.ScheduleSettingsControl.Instance.dateEditFlightDatesStart_CloseUp);
            dateEditFlightDatesEnd.CloseUp += new DevExpress.XtraEditors.Controls.CloseUpEventHandler(CustomControls.ScheduleSettingsControl.Instance.dateEditFlightDatesEnd_CloseUp);
            comboBoxEditBusinessName.Enter += new EventHandler(Editor_Enter);
            comboBoxEditBusinessName.MouseDown += new MouseEventHandler(Editor_MouseDown);
            comboBoxEditBusinessName.MouseUp += new MouseEventHandler(Editor_MouseUp);
            comboBoxEditDecisionMaker.Enter += new EventHandler(Editor_Enter);
            comboBoxEditDecisionMaker.MouseDown += new MouseEventHandler(Editor_MouseDown);
            comboBoxEditDecisionMaker.MouseUp += new MouseEventHandler(Editor_MouseUp);
            #endregion

            #region Schedule Builder Events
            buttonItemSchedulesSave.Click += new EventHandler(CustomControls.ScheduleBuilderControl.Instance.buttonItemSchedulesSave_Click);
            buttonItemSchedulesSaveAs.Click += new EventHandler(CustomControls.ScheduleBuilderControl.Instance.buttonItemSchedulesSaveAs_Click);
            buttonItemSchedulesTitle.CheckedChanged += new EventHandler(CustomControls.ScheduleBuilderControl.Instance.buttonItemSchedulesTogledButton_CheckedChanged);
            buttonItemSchedulesActiveDays.CheckedChanged += new EventHandler(CustomControls.ScheduleBuilderControl.Instance.buttonItemSchedulesTogledButton_CheckedChanged);
            buttonItemSchedulesAdRate.CheckedChanged += new EventHandler(CustomControls.ScheduleBuilderControl.Instance.buttonItemSchedulesTogledButton_CheckedChanged);
            buttonItemSchedulesAvgMonthlyRate.CheckedChanged += new EventHandler(CustomControls.ScheduleBuilderControl.Instance.buttonItemSchedulesTogledButton_CheckedChanged);
            buttonItemSchedulesAvgTotalRate.CheckedChanged += new EventHandler(CustomControls.ScheduleBuilderControl.Instance.buttonItemSchedulesTogledButton_CheckedChanged);
            buttonItemSchedulesDecisionMaker.CheckedChanged += new EventHandler(CustomControls.ScheduleBuilderControl.Instance.buttonItemSchedulesTogledButton_CheckedChanged);
            buttonItemSchedulesBusinessName.CheckedChanged += new EventHandler(CustomControls.ScheduleBuilderControl.Instance.buttonItemSchedulesTogledButton_CheckedChanged);
            buttonItemSchedulesCPM.CheckedChanged += new EventHandler(CustomControls.ScheduleBuilderControl.Instance.buttonItemSchedulesTogledButton_CheckedChanged);
            buttonItemSchedulesDescription.CheckedChanged += new EventHandler(CustomControls.ScheduleBuilderControl.Instance.buttonItemSchedulesTogledButton_CheckedChanged);
            buttonItemSchedulesDimensions.CheckedChanged += new EventHandler(CustomControls.ScheduleBuilderControl.Instance.buttonItemSchedulesTogledButton_CheckedChanged);
            buttonItemSchedulesFlightDates.CheckedChanged += new EventHandler(CustomControls.ScheduleBuilderControl.Instance.buttonItemSchedulesTogledButton_CheckedChanged);
            buttonItemSchedulesPresentationDate.CheckedChanged += new EventHandler(CustomControls.ScheduleBuilderControl.Instance.buttonItemSchedulesTogledButton_CheckedChanged);
            buttonItemSchedulesComments.CheckedChanged += new EventHandler(CustomControls.ScheduleBuilderControl.Instance.buttonItemSchedulesTogledButton_CheckedChanged);
            buttonItemSchedulesTotalAds.CheckedChanged += new EventHandler(CustomControls.ScheduleBuilderControl.Instance.buttonItemSchedulesTogledButton_CheckedChanged);
            buttonItemSchedulesTotalMonthlyRate.CheckedChanged += new EventHandler(CustomControls.ScheduleBuilderControl.Instance.buttonItemSchedulesTogledButton_CheckedChanged);
            buttonItemSchedulesTotalRate.CheckedChanged += new EventHandler(CustomControls.ScheduleBuilderControl.Instance.buttonItemSchedulesTogledButton_CheckedChanged);
            buttonItemSchedulesWebsites.CheckedChanged += new EventHandler(CustomControls.ScheduleBuilderControl.Instance.buttonItemSchedulesTogledButton_CheckedChanged);
            buttonItemSchedulesImageIcons.CheckedChanged += new EventHandler(CustomControls.ScheduleBuilderControl.Instance.buttonItemSchedulesTogledButton_CheckedChanged);
            buttonItemSchedulesSignatureLine.CheckedChanged += new EventHandler(CustomControls.ScheduleBuilderControl.Instance.buttonItemSchedulesTogledButton_CheckedChanged);
            buttonItemSchedulesScreenshotViewer.CheckedChanged += new EventHandler(CustomControls.ScheduleBuilderControl.Instance.buttonItemSchedulesTogledButton_CheckedChanged);
            buttonItemSchedulesPowerPoint.Click += new EventHandler(CustomControls.ScheduleBuilderControl.Instance.buttonItemSchedulesPowerPoint_Click);
            buttonItemSchedulesEmail.Click += new EventHandler(CustomControls.ScheduleBuilderControl.Instance.buttonItemSchedulesEmail_Click);
            buttonItemSchedulesHelp.Click += new EventHandler(CustomControls.ScheduleBuilderControl.Instance.buttonItemSchedulesHelp_Click);
            #endregion

            #region MobilePackage Events
            buttonItemMobilePackageSave.Click += new EventHandler(CustomControls.MobilePackageControl.Instance.buttonItemMobilePackageSave_Click);
            buttonItemMobilePackageSaveAs.Click += new EventHandler(CustomControls.MobilePackageControl.Instance.buttonItemMobilePackageSaveAs_Click);
            buttonItemMobilePackageTitle.CheckedChanged += new EventHandler(CustomControls.MobilePackageControl.Instance.buttonItemMobilePackageTogledButton_CheckedChanged);
            buttonItemMobilePackageActiveDays.CheckedChanged += new EventHandler(CustomControls.MobilePackageControl.Instance.buttonItemMobilePackageTogledButton_CheckedChanged);
            buttonItemMobilePackageAdRate.CheckedChanged += new EventHandler(CustomControls.MobilePackageControl.Instance.buttonItemMobilePackageTogledButton_CheckedChanged);
            buttonItemMobilePackageAvgMonthlyRate.CheckedChanged += new EventHandler(CustomControls.MobilePackageControl.Instance.buttonItemMobilePackageTogledButton_CheckedChanged);
            buttonItemMobilePackageAvgTotalRate.CheckedChanged += new EventHandler(CustomControls.MobilePackageControl.Instance.buttonItemMobilePackageTogledButton_CheckedChanged);
            buttonItemMobilePackageDecisionMaker.CheckedChanged += new EventHandler(CustomControls.MobilePackageControl.Instance.buttonItemMobilePackageTogledButton_CheckedChanged);
            buttonItemMobilePackageBusinessName.CheckedChanged += new EventHandler(CustomControls.MobilePackageControl.Instance.buttonItemMobilePackageTogledButton_CheckedChanged);
            buttonItemMobilePackageCPM.CheckedChanged += new EventHandler(CustomControls.MobilePackageControl.Instance.buttonItemMobilePackageTogledButton_CheckedChanged);
            buttonItemMobilePackageDescription.CheckedChanged += new EventHandler(CustomControls.MobilePackageControl.Instance.buttonItemMobilePackageTogledButton_CheckedChanged);
            buttonItemMobilePackageDimensions.CheckedChanged += new EventHandler(CustomControls.MobilePackageControl.Instance.buttonItemMobilePackageTogledButton_CheckedChanged);
            buttonItemMobilePackageFlightDates.CheckedChanged += new EventHandler(CustomControls.MobilePackageControl.Instance.buttonItemMobilePackageTogledButton_CheckedChanged);
            buttonItemMobilePackagePresentationDate.CheckedChanged += new EventHandler(CustomControls.MobilePackageControl.Instance.buttonItemMobilePackageTogledButton_CheckedChanged);
            buttonItemMobilePackageComments.CheckedChanged += new EventHandler(CustomControls.MobilePackageControl.Instance.buttonItemMobilePackageTogledButton_CheckedChanged);
            buttonItemMobilePackageTotalAds.CheckedChanged += new EventHandler(CustomControls.MobilePackageControl.Instance.buttonItemMobilePackageTogledButton_CheckedChanged);
            buttonItemMobilePackageTotalMonthlyRate.CheckedChanged += new EventHandler(CustomControls.MobilePackageControl.Instance.buttonItemMobilePackageTogledButton_CheckedChanged);
            buttonItemMobilePackageTotalRate.CheckedChanged += new EventHandler(CustomControls.MobilePackageControl.Instance.buttonItemMobilePackageTogledButton_CheckedChanged);
            buttonItemMobilePackageWebsites.CheckedChanged += new EventHandler(CustomControls.MobilePackageControl.Instance.buttonItemMobilePackageTogledButton_CheckedChanged);
            buttonItemMobilePackageImageIcons.CheckedChanged += new EventHandler(CustomControls.MobilePackageControl.Instance.buttonItemMobilePackageTogledButton_CheckedChanged);
            buttonItemMobilePackageSignatureLine.CheckedChanged += new EventHandler(CustomControls.MobilePackageControl.Instance.buttonItemMobilePackageTogledButton_CheckedChanged);
            buttonItemMobilePackageScreenshotViewer.CheckedChanged += new EventHandler(CustomControls.MobilePackageControl.Instance.buttonItemMobilePackageTogledButton_CheckedChanged);
            buttonItemMobilePackagePowerPoint.Click += new EventHandler(CustomControls.MobilePackageControl.Instance.buttonItemMobilePackagePowerPoint_Click);
            buttonItemMobilePackageEmail.Click += new EventHandler(CustomControls.MobilePackageControl.Instance.buttonItemMobilePackageEmail_Click);
            buttonItemMobilePackageHelp.Click += new EventHandler(CustomControls.MobilePackageControl.Instance.buttonItemMobilePackageHelp_Click);
            #endregion

            #region Product Summary Events
            buttonItemProductSummarySave.Click += new EventHandler(CustomControls.ProductSummaryControl.Instance.buttonItemProductummarySave_Click);
            buttonItemProductSummarySaveAs.Click += new EventHandler(CustomControls.ProductSummaryControl.Instance.buttonItemProductummarySaveAs_Click);

            buttonItemProductSummaryActiveDays.CheckedChanged += new EventHandler(CustomControls.ProductSummaryControl.Instance.buttonItemProductSummaryGridPreviewOptions_CheckedChanged);
            buttonItemProductSummaryAdRate.CheckedChanged += new EventHandler(CustomControls.ProductSummaryControl.Instance.buttonItemProductSummaryGridPreviewOptions_CheckedChanged);
            buttonItemProductSummaryDimensions.CheckedChanged += new EventHandler(CustomControls.ProductSummaryControl.Instance.buttonItemProductSummaryGridPreviewOptions_CheckedChanged);
            buttonItemProductSummaryTotalAds.CheckedChanged += new EventHandler(CustomControls.ProductSummaryControl.Instance.buttonItemProductSummaryGridPreviewOptions_CheckedChanged);
            buttonItemProductSummaryWebsites.CheckedChanged += new EventHandler(CustomControls.ProductSummaryControl.Instance.buttonItemProductSummaryGridPreviewOptions_CheckedChanged);

            buttonItemProductSummaryCPM.CheckedChanged += new EventHandler(CustomControls.ProductSummaryControl.Instance.buttonItemProductSummaryGridColumnsOptions_CheckedChanged);
            buttonItemProductSummaryImpressions.CheckedChanged += new EventHandler(CustomControls.ProductSummaryControl.Instance.buttonItemProductSummaryGridColumnsOptions_CheckedChanged);
            buttonItemProductSummaryInvestment.CheckedChanged += new EventHandler(CustomControls.ProductSummaryControl.Instance.buttonItemProductSummaryGridColumnsOptions_CheckedChanged);

            buttonItemProductSummaryMonthlyImpressions.CheckedChanged += new EventHandler(CustomControls.ProductSummaryControl.Instance.buttonItemProductSummaryTotalsOptions_CheckedChanged);
            buttonItemProductSummaryMonthlyInvestment.CheckedChanged += new EventHandler(CustomControls.ProductSummaryControl.Instance.buttonItemProductSummaryTotalsOptions_CheckedChanged);
            buttonItemProductSummaryTotalImpressions.CheckedChanged += new EventHandler(CustomControls.ProductSummaryControl.Instance.buttonItemProductSummaryTotalsOptions_CheckedChanged);
            buttonItemProductSummaryTotalInvestment.CheckedChanged += new EventHandler(CustomControls.ProductSummaryControl.Instance.buttonItemProductSummaryTotalsOptions_CheckedChanged);

            buttonItemProductSummaryHelp.Click += new EventHandler(CustomControls.ProductSummaryControl.Instance.buttonItemProductSummaryHelp_Click);
            buttonItemProductSummaryPowerPoint.Click += new EventHandler(CustomControls.ProductSummaryControl.Instance.buttonItemProductSummaryPowerPoint_Click);
            buttonItemProductSummaryEmail.Click += new EventHandler(CustomControls.ProductSummaryControl.Instance.buttonItemProductSummaryEmail_Click);
            #endregion

            #region Product Bundle Events
            buttonItemProductBundleSave.Click += new EventHandler(CustomControls.ProductBundleControl.Instance.buttonItemProductummarySave_Click);
            buttonItemProductBundleSaveAs.Click += new EventHandler(CustomControls.ProductBundleControl.Instance.buttonItemProductummarySaveAs_Click);

            buttonItemProductBundleActiveDays.CheckedChanged += new EventHandler(CustomControls.ProductBundleControl.Instance.buttonItemProductBundleGridPreviewOptions_CheckedChanged);
            buttonItemProductBundleAdRate.CheckedChanged += new EventHandler(CustomControls.ProductBundleControl.Instance.buttonItemProductBundleGridPreviewOptions_CheckedChanged);
            buttonItemProductBundleDimensions.CheckedChanged += new EventHandler(CustomControls.ProductBundleControl.Instance.buttonItemProductBundleGridPreviewOptions_CheckedChanged);
            buttonItemProductBundleTotalAds.CheckedChanged += new EventHandler(CustomControls.ProductBundleControl.Instance.buttonItemProductBundleGridPreviewOptions_CheckedChanged);
            buttonItemProductBundleWebsites.CheckedChanged += new EventHandler(CustomControls.ProductBundleControl.Instance.buttonItemProductBundleGridPreviewOptions_CheckedChanged);

            buttonItemProductBundleCPM.CheckedChanged += new EventHandler(CustomControls.ProductBundleControl.Instance.buttonItemProductBundleGridColumnsOptions_CheckedChanged);
            buttonItemProductBundleImpressions.CheckedChanged += new EventHandler(CustomControls.ProductBundleControl.Instance.buttonItemProductBundleGridColumnsOptions_CheckedChanged);
            buttonItemProductBundleInvestment.CheckedChanged += new EventHandler(CustomControls.ProductBundleControl.Instance.buttonItemProductBundleGridColumnsOptions_CheckedChanged);

            buttonItemProductBundleMonthlyImpressions.CheckedChanged += new EventHandler(CustomControls.ProductBundleControl.Instance.buttonItemProductBundleTotalsOptions_CheckedChanged);
            buttonItemProductBundleMonthlyInvestment.CheckedChanged += new EventHandler(CustomControls.ProductBundleControl.Instance.buttonItemProductBundleTotalsOptions_CheckedChanged);
            buttonItemProductBundleTotalImpressions.CheckedChanged += new EventHandler(CustomControls.ProductBundleControl.Instance.buttonItemProductBundleTotalsOptions_CheckedChanged);
            buttonItemProductBundleTotalInvestment.CheckedChanged += new EventHandler(CustomControls.ProductBundleControl.Instance.buttonItemProductBundleTotalsOptions_CheckedChanged);

            buttonItemProductBundleHelp.Click += new EventHandler(CustomControls.ProductBundleControl.Instance.buttonItemProductBundleHelp_Click);
            buttonItemProductBundlePowerPoint.Click += new EventHandler(CustomControls.ProductBundleControl.Instance.buttonItemProductBundlePowerPoint_Click);
            buttonItemProductBundleEmail.Click += new EventHandler(CustomControls.ProductBundleControl.Instance.buttonItemProductBundleEmail_Click);
            #endregion

            #region Success Models Events
            buttonItemSuccessModelsHelp.Click += new EventHandler(CustomControls.ModelsOfSuccessContainerControl.Instance.buttonItemSuccessModelsHelp_Click);
            #endregion

            ribbonTabItemSuccessModels.Enabled = false;

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
                dateEditFlightDatesEnd.Font = font;
                dateEditFlightDatesStart.Font = font;
                dateEditPresentationDate.Font = font;
                ribbonBarAdvertiser.RecalcLayout();
                ribbonBarFlightDates.RecalcLayout();
                ribbonBarHomeExit.RecalcLayout();
                ribbonBarProductBundleCombinedTotals.RecalcLayout();
                ribbonBarProductBundleEmail.RecalcLayout();
                ribbonBarProductBundleExit.RecalcLayout();
                ribbonBarProductBundleHelp.RecalcLayout();
                ribbonBarProductBundleLogo.RecalcLayout();
                ribbonBarProductBundlePowerPoint.RecalcLayout();
                ribbonBarProductBundleProductDetails.RecalcLayout();
                ribbonBarProductBundleSave.RecalcLayout();
                ribbonBarProductSummaryEmail.RecalcLayout();
                ribbonBarProductSummaryExit.RecalcLayout();
                ribbonBarProductSummaryHelp.RecalcLayout();
                ribbonBarProductSummaryLogo.RecalcLayout();
                ribbonBarProductSummaryPowerPoint.RecalcLayout();
                ribbonBarProductSummaryProductDetails.RecalcLayout();
                ribbonBarProductSummarySave.RecalcLayout();
                ribbonBarRoductSummaryCombinedTotals.RecalcLayout();
                ribbonBarScheduleHelp.RecalcLayout();
                ribbonBarScheduleSave.RecalcLayout();
                ribbonBarSchedulesBasicInformation.RecalcLayout();
                ribbonBarSchedulesCampaignDetails.RecalcLayout();
                ribbonBarSchedulesEmail.RecalcLayout();
                ribbonBarSchedulesExit.RecalcLayout();
                ribbonBarSchedulesHelp.RecalcLayout();
                ribbonBarSchedulesPowerPoint.RecalcLayout();
                ribbonBarSchedulesSaveAs.RecalcLayout();
                ribbonBarSchedulesSlideOptions.RecalcLayout();
                ribbonBarSchedulesTitle.RecalcLayout();
                ribbonBarSuccessModels.RecalcLayout();
                ribbonBarSuccessModelsExit.RecalcLayout();
                ribbonBarSuccessModelsHelp.RecalcLayout();
                ribbonBarMobilePackageBasicInformation.RecalcLayout();
                ribbonBarMobilePackageCampaignDetails.RecalcLayout();
                ribbonBarMobilePackageEmail.RecalcLayout();
                ribbonBarMobilePackageExit.RecalcLayout();
                ribbonBarMobilePackageHelp.RecalcLayout();
                ribbonBarMobilePackageOptions.RecalcLayout();
                ribbonBarMobilePackagePowerPoint.RecalcLayout();
                ribbonBarMobilePackageSaveAs.RecalcLayout();
                ribbonBarMobilePackageTitle.RecalcLayout();
                ribbonPanelBuildSchedules.PerformLayout();
                ribbonPanelScheduleSettings.PerformLayout();
                ribbonPanelProductBundle.PerformLayout();
                ribbonPanelProductSummary.PerformLayout();
                ribbonPanelSuccessModels.PerformLayout();
                ribbonPanelMobilePackage.PerformLayout();
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

        public void UpdateSimpleOutputTabPageState(bool enable)
        {
            ribbonTabItemBuildSchedules.Enabled = enable;
            ribbonTabItemMobilePackage.Enabled = enable;
        }

        public void UpdateSummaryOutputTabPageState(bool enable)
        {
            ribbonTabItemProductBundle.Enabled = enable;
            ribbonTabItemProductSummary.Enabled = enable;
        }

        public void LoadCategories()
        {
            ribbonPanelScheduleSettings.SuspendLayout();
            ribbonPanelScheduleSettings.Controls.Add(ribbonBarHomeExit);
            ribbonPanelScheduleSettings.Controls.Add(ribbonBarScheduleHelp);
            ribbonPanelScheduleSettings.Controls.Add(ribbonBarScheduleSave);
            int leftPosition = 287;
            if (BusinessClasses.ListManager.Instance.Categories.Count > 5)
            {
                DevComponents.DotNetBar.ButtonItem categoryListButton = new DevComponents.DotNetBar.ButtonItem();
                categoryListButton.Image = System.IO.File.Exists(ConfigurationClasses.SettingsManager.Instance.InventoryImagePath) ? new Bitmap(ConfigurationClasses.SettingsManager.Instance.InventoryImagePath) : Properties.Resources.Inventory;
                categoryListButton.ImagePaddingHorizontal = 8;
                categoryListButton.SubItemsExpandWidth = 14;
                categoryListButton.AutoExpandOnClick = true;
                superTooltip.SetSuperTooltip(categoryListButton, new DevComponents.DotNetBar.SuperTooltipInfo("Mobile Sales Inventory", "", "Select the Mobile Sales Products you want to sell", null, null, DevComponents.DotNetBar.eTooltipColor.Gray));

                foreach (BusinessClasses.Category category in BusinessClasses.ListManager.Instance.Categories)
                {
                    DevComponents.DotNetBar.ButtonItem categoryButton = new DevComponents.DotNetBar.ButtonItem();
                    categoryButton.Image = category.Logo;
                    categoryButton.Text = "<b>" + category.TooltipTitle + "</b><p>" + category.TooltipValue + "</p>";
                    categoryButton.ImagePaddingHorizontal = 8;
                    categoryButton.SubItemsExpandWidth = 14;
                    categoryButton.Tag = category;
                    categoryButton.Click += new System.EventHandler(CustomControls.ScheduleSettingsControl.Instance.buttonItemProductAddProduct_Click);
                    categoryListButton.SubItems.Add(categoryButton);
                }

                DevComponents.DotNetBar.RibbonBar categoryListRibbonBar = new DevComponents.DotNetBar.RibbonBar();
                categoryListRibbonBar.AutoOverflowEnabled = true;
                categoryListRibbonBar.Dock = System.Windows.Forms.DockStyle.Left;
                categoryListRibbonBar.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] { categoryListButton });
                categoryListRibbonBar.Location = new System.Drawing.Point(leftPosition, 0);
                categoryListRibbonBar.Name = leftPosition.ToString();
                categoryListRibbonBar.Size = new System.Drawing.Size(79, 135);
                categoryListRibbonBar.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
                categoryListRibbonBar.Text = "What do you want to sell?";
                categoryListRibbonBar.Click += new System.EventHandler(pnMain_Click);

                ribbonPanelScheduleSettings.Controls.Add(categoryListRibbonBar);
                leftPosition += 70;
            }
            else
            {
                foreach (BusinessClasses.Category category in BusinessClasses.ListManager.Instance.Categories)
                {
                    DevComponents.DotNetBar.ButtonItem categoryButton = new DevComponents.DotNetBar.ButtonItem();
                    categoryButton.Image = category.Logo;
                    categoryButton.ImagePaddingHorizontal = 8;
                    categoryButton.SubItemsExpandWidth = 14;
                    superTooltip.SetSuperTooltip(categoryButton, new DevComponents.DotNetBar.SuperTooltipInfo(category.TooltipTitle, "", category.TooltipValue, null, null, DevComponents.DotNetBar.eTooltipColor.Gray));
                    categoryButton.Tag = category;
                    categoryButton.Click += new System.EventHandler(CustomControls.ScheduleSettingsControl.Instance.buttonItemProductAddProduct_Click);

                    DevComponents.DotNetBar.RibbonBar categoryRibbonBar = new DevComponents.DotNetBar.RibbonBar();
                    categoryRibbonBar.AutoOverflowEnabled = true;
                    categoryRibbonBar.Dock = System.Windows.Forms.DockStyle.Left;
                    categoryRibbonBar.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] { categoryButton });
                    categoryRibbonBar.Location = new System.Drawing.Point(leftPosition, 0);
                    categoryRibbonBar.Name = leftPosition.ToString();
                    categoryRibbonBar.Size = new System.Drawing.Size(79, 135);
                    categoryRibbonBar.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
                    categoryRibbonBar.Text = category.Name;
                    categoryRibbonBar.Click += new System.EventHandler(pnMain_Click);

                    ribbonPanelScheduleSettings.Controls.Add(categoryRibbonBar);
                    leftPosition += 70;
                }
            }
            ribbonPanelScheduleSettings.Controls.Add(ribbonBarFlightDates);
            ribbonPanelScheduleSettings.Controls.Add(ribbonBarAdvertiser);
            ribbonPanelScheduleSettings.ResumeLayout(false);
        }

        private bool AllowToLeaveCurrentControl()
        {
            bool result = false;
            if ((_currentControl == CustomControls.ScheduleSettingsControl.Instance))
            {
                if (CustomControls.ScheduleSettingsControl.Instance.AllowToLeaveControl)
                    result = true;
                else
                {
                    ribbonControl.SelectedRibbonTabChanged -= new EventHandler(ribbonControl_SelectedRibbonTabChanged);
                    ribbonControl.SelectedRibbonTabItem = ribbonTabItemScheduleSettings;
                    ribbonControl.SelectedRibbonTabChanged += new EventHandler(ribbonControl_SelectedRibbonTabChanged);
                }
            }
            else if ((_currentControl == CustomControls.ScheduleBuilderControl.Instance))
            {
                if (CustomControls.ScheduleBuilderControl.Instance.AllowToLeaveControl)
                    result = true;
                else
                {
                    ribbonControl.SelectedRibbonTabChanged -= new EventHandler(ribbonControl_SelectedRibbonTabChanged);
                    ribbonControl.SelectedRibbonTabItem = ribbonTabItemBuildSchedules;
                    ribbonControl.SelectedRibbonTabChanged += new EventHandler(ribbonControl_SelectedRibbonTabChanged);
                }
            }
            else if ((_currentControl == CustomControls.MobilePackageControl.Instance))
            {
                if (CustomControls.MobilePackageControl.Instance.AllowToLeaveControl)
                    result = true;
                else
                {
                    ribbonControl.SelectedRibbonTabChanged -= new EventHandler(ribbonControl_SelectedRibbonTabChanged);
                    ribbonControl.SelectedRibbonTabItem = ribbonTabItemMobilePackage;
                    ribbonControl.SelectedRibbonTabChanged += new EventHandler(ribbonControl_SelectedRibbonTabChanged);
                }
            }
            else if ((_currentControl == CustomControls.ProductSummaryControl.Instance))
            {
                if (CustomControls.ProductSummaryControl.Instance.AllowToLeaveControl)
                    result = true;
                else
                {
                    ribbonControl.SelectedRibbonTabChanged -= new EventHandler(ribbonControl_SelectedRibbonTabChanged);
                    ribbonControl.SelectedRibbonTabItem = ribbonTabItemProductSummary;
                    ribbonControl.SelectedRibbonTabChanged += new EventHandler(ribbonControl_SelectedRibbonTabChanged);
                }
            }
            else if ((_currentControl == CustomControls.ProductBundleControl.Instance))
            {
                if (CustomControls.ProductBundleControl.Instance.AllowToLeaveControl)
                    result = true;
                else
                {
                    ribbonControl.SelectedRibbonTabChanged -= new EventHandler(ribbonControl_SelectedRibbonTabChanged);
                    ribbonControl.SelectedRibbonTabItem = ribbonTabItemProductBundle;
                    ribbonControl.SelectedRibbonTabChanged += new EventHandler(ribbonControl_SelectedRibbonTabChanged);
                }
            }
            else
                result = true;
            return result;
        }

        private void FormMain_Shown(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ConfigurationClasses.SettingsManager.Instance.SelectedWizard))
                FormMain.Instance.Text = "Mobile Schedule Builder - " + ConfigurationClasses.SettingsManager.Instance.SelectedWizard + " - " + ConfigurationClasses.SettingsManager.Instance.Size;
            ribbonControl.Enabled = false;
            using (ToolForms.FormProgress form = new ToolForms.FormProgress())
            {
                form.laProgress.Text = "Chill-Out for a few seconds...\nLoading Mobile Schedule...";
                form.TopMost = true;
                System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        CustomControls.ScheduleSettingsControl.Instance.LoadSchedule(false);
                        CustomControls.ScheduleBuilderControl.Instance.LoadSchedule(false);
                        CustomControls.MobilePackageControl.Instance.LoadSchedule(false);
                        CustomControls.ProductSummaryControl.Instance.LoadSchedule();
                        CustomControls.ProductBundleControl.Instance.LoadSchedule();
                    });
                }));
                thread.Start();

                form.Show();

                while (thread.IsAlive)
                    System.Windows.Forms.Application.DoEvents();
                form.Close();
            }

            ribbonControl.SelectedRibbonTabItem = ribbonTabItemScheduleSettings;
            ribbonControl_SelectedRibbonTabChanged(null, null);
            ribbonControl.SelectedRibbonTabChanged += new EventHandler(ribbonControl_SelectedRibbonTabChanged);
            ribbonControl.Enabled = true;
        }

        public void ribbonControl_SelectedRibbonTabChanged(object sender, EventArgs e)
        {
            if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemScheduleSettings)
            {
                if (AllowToLeaveCurrentControl())
                {
                    _currentControl = CustomControls.ScheduleSettingsControl.Instance;
                    if (!pnMain.Controls.Contains(_currentControl))
                        pnMain.Controls.Add(CustomControls.ScheduleSettingsControl.Instance);
                }
                _currentControl.BringToFront();
            }
            else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemBuildSchedules)
            {
                if (AllowToLeaveCurrentControl())
                {
                    _currentControl = CustomControls.ScheduleBuilderControl.Instance;
                    if (!pnMain.Controls.Contains(_currentControl))
                        pnMain.Controls.Add(CustomControls.ScheduleBuilderControl.Instance);
                }
                _currentControl.BringToFront();
            }
            else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemMobilePackage)
            {
                if (AllowToLeaveCurrentControl())
                {
                    _currentControl = CustomControls.MobilePackageControl.Instance;
                    if (!pnMain.Controls.Contains(_currentControl))
                        pnMain.Controls.Add(CustomControls.MobilePackageControl.Instance);
                }
                _currentControl.BringToFront();
            }
            else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemProductSummary)
            {
                if (AllowToLeaveCurrentControl())
                {
                    _currentControl = CustomControls.ProductSummaryControl.Instance;
                    if (!pnMain.Controls.Contains(_currentControl))
                        pnMain.Controls.Add(CustomControls.ProductSummaryControl.Instance);
                }
                _currentControl.BringToFront();
            }
            else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemProductBundle)
            {
                if (AllowToLeaveCurrentControl())
                {
                    _currentControl = CustomControls.ProductBundleControl.Instance;
                    if (!pnMain.Controls.Contains(_currentControl))
                        pnMain.Controls.Add(CustomControls.ProductBundleControl.Instance);
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
            if (_currentControl == CustomControls.ScheduleSettingsControl.Instance)
                result = CustomControls.ScheduleSettingsControl.Instance.AllowToLeaveControl;
            else if (_currentControl == CustomControls.ScheduleBuilderControl.Instance)
                result = CustomControls.ScheduleBuilderControl.Instance.AllowToLeaveControl;
            else if (_currentControl == CustomControls.MobilePackageControl.Instance)
                result = CustomControls.MobilePackageControl.Instance.AllowToLeaveControl;
            else if (_currentControl == CustomControls.ProductSummaryControl.Instance)
                result = CustomControls.ProductSummaryControl.Instance.AllowToLeaveControl;
            else if (_currentControl == CustomControls.ProductBundleControl.Instance)
                result = CustomControls.ProductBundleControl.Instance.AllowToLeaveControl;
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
