using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MiniBar
{
    public partial class FormMainExpanded : Form
    {
        private static object _locker = new object();
        private static FormMainExpanded _instance;
        private bool _expanded = false;
        private bool _comboOpened = false;
        private Timer _formMouseLeaveTimer;
        private Timer _hideTimer = null;
        private bool _allowToSave = false;

        public static FormMainExpanded Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new FormMainExpanded();
                return _instance;
            }
        }

        private FormMainExpanded()
        {
            InitializeComponent();

            ribbonControl.Height = 130;

            _formMouseLeaveTimer = new Timer();
            _formMouseLeaveTimer.Interval = 2000;
            _formMouseLeaveTimer.Tick += new EventHandler(FormMouseLeaveTimer_Tick);

            _hideTimer = new Timer();
            _hideTimer.Interval = 30;
            _hideTimer.Tick += new EventHandler(_hideTimer_Tick);
            _hideTimer.Start();

            ApplyPowerPointLogo();

            if ((base.CreateGraphics()).DpiX > 96)
            {
                Font font = new Font(styleController.Appearance.Font.FontFamily, styleController.Appearance.Font.Size - 2, styleController.Appearance.Font.Style);
                styleController.Appearance.Font = font;
                styleController.AppearanceDisabled.Font = font;
                styleController.AppearanceDropDown.Font = font;
                styleController.AppearanceDropDownHeader.Font = font;
                styleController.AppearanceFocused.Font = font;
                styleController.AppearanceReadOnly.Font = font;
                comboBoxEditMeetingID.Font = font;
                comboBoxEditPowerPointStyle.Font = font;
                textEditLocation.Font = font;
                ribbonControl.Font = new Font(ribbonControl.Font.FontFamily, ribbonControl.Font.Size - 1, ribbonControl.Font.Style);
                ribbonBarApps.RecalcLayout();
                ribbonBarAppsExit.RecalcLayout();
                ribbonBarAppsHelp.RecalcLayout();
                ribbonBarClipartClientLogos.RecalcLayout();
                ribbonBarClipartExit.RecalcLayout();
                ribbonBarClipartHelp.RecalcLayout();
                ribbonBarClipartSalesGallery.RecalcLayout();
                ribbonBarClipartWebArt.RecalcLayout();
                ribbonBarDashboard.RecalcLayout();
                ribbonBarDashboardExit.RecalcLayout();
                ribbonBarDashboardHelp.RecalcLayout();
                ribbonBarDashboardStarterSlides.RecalcLayout();
                ribbonBarToolsExit.RecalcLayout();
                ribbonBarToolsHelp.RecalcLayout();
                ribbonBarToolsSave.RecalcLayout();
                ribbonBarPdfEmailPdf.RecalcLayout();
                ribbonBarPdfEmailPpt.RecalcLayout();
                ribbonBarPdfExit.RecalcLayout();
                ribbonBarPdfHelp.RecalcLayout();
                ribbonBarPdfSavePdf.RecalcLayout();
                ribbonBarPowerPointExit.RecalcLayout();
                ribbonBarPowerPointHelp.RecalcLayout();
                ribbonBarPowerPointLaunch.RecalcLayout();
                ribbonBarPowerPointPresentationSettings.RecalcLayout();
                ribbonBarPowerPointSlideTemplate.RecalcLayout();
                ribbonBarSalesDepot.RecalcLayout();
                ribbonBarSalesDepotRemote.RecalcLayout();
                ribbonBarSalesDepotHelp.RecalcLayout();
                ribbonBarSetingsHelp.RecalcLayout();
                ribbonBarSettingsExit.RecalcLayout();
                ribbonBarSettingsMinibar.RecalcLayout();
                ribbonBarSettingsStartup.RecalcLayout();
                ribbonBarSlidesExit.RecalcLayout();
                ribbonBarSyncExit.RecalcLayout();
                ribbonBarSyncHelp.RecalcLayout();
                ribbonBarSyncHourly.RecalcLayout();
                ribbonBarSyncStart.RecalcLayout();
                ribbonBarSyncStatus.RecalcLayout();
                ribbonBarTrainingExit.RecalcLayout();
                ribbonBarTrainingHelp.RecalcLayout();
                ribbonBarTrainingLiveMeeting.RecalcLayout();
                ribbonBarTrainingWebcast.RecalcLayout();
                ribbonPanelApps.PerformLayout();
                ribbonPanelClipart.PerformLayout();
                ribbonPanelDashboard.PerformLayout();
                ribbonPanelTools.PerformLayout();
                ribbonPanelPDF.PerformLayout();
                ribbonPanelPowerPoint.PerformLayout();
                ribbonPanelSalesDepot.PerformLayout();
                ribbonPanelSettings.PerformLayout();
                ribbonPanelSync.PerformLayout();
                ribbonPanelTraining.PerformLayout();
            }

            #region Init Activity Recording Events
            buttonItemAppsHelp.Click += new EventHandler(ribbonTabItem_Click);
            buttonItemClipartClientLogos.Click += new EventHandler(ribbonTabItem_Click);
            buttonItemClipartHelp.Click += new EventHandler(ribbonTabItem_Click);
            buttonItemClipartSalesGallery.Click += new EventHandler(ribbonTabItem_Click);
            buttonItemClipartWebArt.Click += new EventHandler(ribbonTabItem_Click);
            buttonItemDashboard.Click += new EventHandler(ribbonTabItem_Click);
            buttonItemDashboardCleanslate.Click += new EventHandler(ribbonTabItem_Click);
            buttonItemDashboardCover.Click += new EventHandler(ribbonTabItem_Click);
            buttonItemDashboardHelp.Click += new EventHandler(ribbonTabItem_Click);
            buttonItemPdfEmailPdf.Click += new EventHandler(ribbonTabItem_Click);
            buttonItemPdfEmailPpt.Click += new EventHandler(ribbonTabItem_Click);
            buttonItemPdfHelp.Click += new EventHandler(ribbonTabItem_Click);
            buttonItemPdfSavePdf.Click += new EventHandler(ribbonTabItem_Click);
            buttonItemPowerPointHelp.Click += new EventHandler(ribbonTabItem_Click);
            buttonItemPowerPointLaunch.Click += new EventHandler(ribbonTabItem_Click);
            buttonItemPowerPointSize1.Click += new EventHandler(ribbonTabItem_Click);
            buttonItemPowerPointSize2.Click += new EventHandler(ribbonTabItem_Click);
            buttonItemPowerPointSize3.Click += new EventHandler(ribbonTabItem_Click);
            buttonItemPowerPointSize4.Click += new EventHandler(ribbonTabItem_Click);
            buttonItemPowerPointSize5.Click += new EventHandler(ribbonTabItem_Click);
            buttonItemPowerPointTemplateDisabled.Click += new EventHandler(ribbonTabItem_Click);
            buttonItemPowerPointTemplateEnabled.Click += new EventHandler(ribbonTabItem_Click);
            buttonItemSalesDepotHelp.Click += new EventHandler(ribbonTabItem_Click);
            buttonItemSalesDepotLogo.Click += new EventHandler(ribbonTabItem_Click);
            buttonItemSettingsDesktop.Click += new EventHandler(ribbonTabItem_Click);
            buttonItemSettingsHelp.Click += new EventHandler(ribbonTabItem_Click);
            buttonItemSettingsKillExcel.Click += new EventHandler(ribbonTabItem_Click);
            buttonItemSettingsKilPowerPoint.Click += new EventHandler(ribbonTabItem_Click);
            buttonItemSettingsMinibar.Click += new EventHandler(ribbonTabItem_Click);
            buttonItemSettingsMonitor1.Click += new EventHandler(ribbonTabItem_Click);
            buttonItemSettingsMonitor2.Click += new EventHandler(ribbonTabItem_Click);
            buttonItemSettingsReset.Click += new EventHandler(ribbonTabItem_Click);
            buttonItemSettingsSoftwareUpdates.Click += new EventHandler(ribbonTabItem_Click);
            buttonItemSyncHelp.Click += new EventHandler(ribbonTabItem_Click);
            buttonItemSyncHourlyOff.Click += new EventHandler(ribbonTabItem_Click);
            buttonItemSyncHourlyOn.Click += new EventHandler(ribbonTabItem_Click);
            buttonItemSyncStart.Click += new EventHandler(ribbonTabItem_Click);
            buttonItemToolsContent.Click += new EventHandler(ribbonTabItem_Click);
            buttonItemToolsHelp.Click += new EventHandler(ribbonTabItem_Click);
            buttonItemToolsPageNumbers.Click += new EventHandler(ribbonTabItem_Click);
            buttonItemToolsSave.Click += new EventHandler(ribbonTabItem_Click);
            buttonItemToolsSlideHeader.Click += new EventHandler(ribbonTabItem_Click);
            buttonItemTrainingHelp.Click += new EventHandler(ribbonTabItem_Click);
            buttonItemTrainingLiveMeeting.Click += new EventHandler(ribbonTabItem_Click);
            buttonItemTrainingLocationCopy.Click += new EventHandler(ribbonTabItem_Click);
            buttonItemTrainingMeetingIDCopy.Click += new EventHandler(ribbonTabItem_Click);
            #endregion
        }

        #region Form Event Handlers
        private void FormMain_Deactivate(object sender, EventArgs e)
        {
            ActivateRetractedForm();
        }
        private void comboBoxEdit_Popup(object sender, EventArgs e)
        {
            _comboOpened = true;
        }

        private void comboBoxEdit_Closed(object sender, DevExpress.XtraEditors.Controls.ClosedEventArgs e)
        {
            _comboOpened = false;
        }
        #endregion

        #region Timer Ticks
        void FormMouseLeaveTimer_Tick(object sender, EventArgs e)
        {
            Point pos = Control.MousePosition;
            bool inForm = pos.X >= Left && pos.Y >= Top && pos.X < Right && pos.Y < Bottom;
            if (!inForm && _expanded && !_comboOpened)
                ActivateRetractedForm();
        }

        void FadeInTimer_Tick(object sender, EventArgs e)
        {
            if (this.Opacity < 1 && !ConfigurationClasses.SettingsManager.Instance.QuickRetraction)
            {
                this.Opacity += 0.07;
                Application.DoEvents();
                return;
            }
            this.Opacity = 1;
            Application.DoEvents();
            _expanded = true;
            _formMouseLeaveTimer.Start();
            ((Timer)sender).Enabled = false;
            ((Timer)sender).Dispose();
        }

        void FadeOutTimer_Tick(object sender, EventArgs e)
        {
            _expanded = false;
            if (this.Opacity > 0 && !ConfigurationClasses.SettingsManager.Instance.QuickRetraction)
            {
                this.Opacity -= 0.07;
                Application.DoEvents();
                return;
            }
            this.Opacity = 0;
            Application.DoEvents();
            ((Timer)sender).Enabled = false;
            ((Timer)sender).Dispose();
        }

        private void _hideTimer_Tick(object sender, EventArgs e)
        {
            lock (AppManager.Locker)
            {
                Screen screen = Screen.PrimaryScreen;
                int screensCount = Screen.AllScreens.Length;
                bool primaryOnLeftSide = true;
                if (screensCount > 1)
                {
                    screen = ConfigurationClasses.SettingsManager.Instance.OnPrimaryScreen ? Screen.PrimaryScreen : Screen.AllScreens.Where(x => !x.Primary).FirstOrDefault();
                    primaryOnLeftSide = screen.WorkingArea.X >= 0;
                }
                if (screen == null)
                    screen = Screen.PrimaryScreen;

                this.Top = screen.WorkingArea.Bottom - this.Height;
                if (ConfigurationClasses.SettingsManager.Instance.OnPrimaryScreen)
                    this.Left = (screen.WorkingArea.Width - this.Width) / 2;
                else if (screensCount > 1)
                {
                    if (primaryOnLeftSide)
                        this.Left = Screen.PrimaryScreen.WorkingArea.Width + (screen.WorkingArea.Width - this.Width) / 2;
                    else
                        this.Left = (screen.WorkingArea.Width + this.Width) / 2 * -1;
                }
            }
        }
        #endregion

        #region Buttons Clicks
        private void ribbonTabItem_Click(object sender, EventArgs e)
        {
            BusinessClasses.ServiceDataManager.Instance.WriteActivity();
        }

        private void buttonItemSalesDepot_Click(object sender, EventArgs e)
        {
            AppManager.Instance.RunSalesDepot();
        }

        private void buttonItemSalesDepotRemote_Click(object sender, EventArgs e)
        {
            AppManager.Instance.RunSalesDepotRemote();
        }

        private void buttonItemExit_Click(object sender, EventArgs e)
        {
            BusinessClasses.ServiceDataManager.Instance.WriteActivity();
            if (ConfigurationClasses.SettingsManager.Instance.CloseFloat)
            {
                ShowFloater();
            }
            else if (AppManager.Instance.ShowWarningQuestion("Are you Sure You want to close the Minibar?") == System.Windows.Forms.DialogResult.Yes)
            {
                AppManager.Instance.ShowInformation("To open Minibar in the future, double-click desktop shortcut for Minibar...");
                if (ConfigurationClasses.SettingsManager.Instance.CloseHide)
                    lock (AppManager.Locker)
                    {
                        ConfigurationClasses.RegistryHelper.ShowHidden = true;
                    }
                else if (ConfigurationClasses.SettingsManager.Instance.CloseShutdown)
                    Application.Exit();
            }
        }

        private void buttonItemHelp_Click(object sender, EventArgs e)
        {
            BusinessClasses.HelpManager.Instance.OpenHelpLink(ribbonControl.Items.IndexOf(ribbonControl.SelectedRibbonTabItem) + 1);
        }
        #endregion

        #region PowerPoint Methods
        private void ApplyPowerPointLogo()
        {
            buttonItemPowerPointLaunch.Image = Properties.Resources.PowerPointLaunch2010;
            string pptExecutableLocation = string.Format(@"{0}\Microsoft Office\OFFICE14\POWERPNT.exe", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
            if (File.Exists(pptExecutableLocation))
                buttonItemPowerPointLaunch.Image = Properties.Resources.PowerPointLaunch2010;
            else
            {
                pptExecutableLocation = string.Format(@"c:\Program Files\Microsoft Office\OFFICE14\POWERPNT.exe", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
                if (File.Exists(pptExecutableLocation))
                    buttonItemPowerPointLaunch.Image = Properties.Resources.PowerPointLaunch2010;
                else
                {
                    pptExecutableLocation = string.Format(@"{0}\Microsoft Office\OFFICE12\POWERPNT.exe", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
                    if (File.Exists(pptExecutableLocation))
                        buttonItemPowerPointLaunch.Image = Properties.Resources.PowerPointLaunch2007;
                    else
                    {
                        pptExecutableLocation = string.Format(@"c:\Program Files\Microsoft Office\OFFICE12\POWERPNT.exe", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
                        if (File.Exists(pptExecutableLocation))
                            buttonItemPowerPointLaunch.Image = Properties.Resources.PowerPointLaunch2007;
                        else
                        {
                            pptExecutableLocation = string.Format(@"{0}\Microsoft Office\OFFICE11\POWERPNT.exe", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
                            if (File.Exists(pptExecutableLocation))
                                buttonItemPowerPointLaunch.Image = Properties.Resources.PowerPointLaunch2003;
                        }
                    }
                }
            }
            ribbonBarPowerPointLaunch.RecalcLayout();
            ribbonPanelPowerPoint.PerformLayout();
        }


        private void RunPowerPoint()
        {
            AppManager.Instance.RunPowerPointLoader();
        }

        private void SetPresentationSettings()
        {
            if (ConfigurationClasses.SettingsManager.Instance.Orientation.Equals("Landscape"))
            {
                if (ConfigurationClasses.SettingsManager.Instance.SizeWidth == 10 && ConfigurationClasses.SettingsManager.Instance.SizeHeght == 7.5)
                {
                    buttonItemPowerPointSize1.Checked = true;
                    buttonItemPowerPointSize2.Checked = false;
                    buttonItemPowerPointSize3.Checked = false;
                    buttonItemPowerPointSize4.Checked = false;
                    buttonItemPowerPointSize5.Checked = false;
                }
                else if (ConfigurationClasses.SettingsManager.Instance.SizeWidth == 10.75 && ConfigurationClasses.SettingsManager.Instance.SizeHeght == 8.25)
                {
                    buttonItemPowerPointSize1.Checked = false;
                    buttonItemPowerPointSize2.Checked = true;
                    buttonItemPowerPointSize3.Checked = false;
                    buttonItemPowerPointSize4.Checked = false;
                    buttonItemPowerPointSize5.Checked = false;
                }
                else if (ConfigurationClasses.SettingsManager.Instance.SizeWidth == 10 && ConfigurationClasses.SettingsManager.Instance.SizeHeght == 5.63)
                {
                    buttonItemPowerPointSize1.Checked = false;
                    buttonItemPowerPointSize2.Checked = false;
                    buttonItemPowerPointSize3.Checked = true;
                    buttonItemPowerPointSize4.Checked = false;
                    buttonItemPowerPointSize5.Checked = false;
                }
            }
            else
            {
                if (ConfigurationClasses.SettingsManager.Instance.SizeWidth == 10 && ConfigurationClasses.SettingsManager.Instance.SizeHeght == 7.5)
                {
                    buttonItemPowerPointSize1.Checked = false;
                    buttonItemPowerPointSize2.Checked = false;
                    buttonItemPowerPointSize3.Checked = false;
                    buttonItemPowerPointSize4.Checked = true;
                    buttonItemPowerPointSize5.Checked = false;
                }
                else if (ConfigurationClasses.SettingsManager.Instance.SizeWidth == 10.75 && ConfigurationClasses.SettingsManager.Instance.SizeHeght == 8.25)
                {
                    buttonItemPowerPointSize1.Checked = false;
                    buttonItemPowerPointSize2.Checked = false;
                    buttonItemPowerPointSize3.Checked = false;
                    buttonItemPowerPointSize4.Checked = false;
                    buttonItemPowerPointSize5.Checked = true;
                }
            }
        }

        private void SelectMasterWizard(string name)
        {
            BusinessClasses.MasterWizard masterWizard = null;
            BusinessClasses.MasterWizardManager.Instance.MasterWizards.TryGetValue(name, out masterWizard);
            BusinessClasses.MasterWizardManager.Instance.SelectedWizard = masterWizard;
            if (BusinessClasses.MasterWizardManager.Instance.SelectedWizard != null)
            {
                ConfigurationClasses.SettingsManager.Instance.SelectedWizard = masterWizard.Name;

                UpdateSlideSize();

                UpdateSlideTemplateStatus();

                UpdateApplicationsStatus();

                ConfigurationClasses.SettingsManager.Instance.SaveSharedSettings();
            }
        }

        private void UpdateSlideSize()
        {
            buttonItemPowerPointSize1.Enabled = BusinessClasses.MasterWizardManager.Instance.SelectedWizard.Has43;
            if (buttonItemPowerPointSize1.Checked && !buttonItemPowerPointSize1.Enabled)
                buttonItemPowerPointSize1.Checked = false;
            buttonItemPowerPointSize2.Enabled = BusinessClasses.MasterWizardManager.Instance.SelectedWizard.Has54;
            if (buttonItemPowerPointSize2.Checked && !buttonItemPowerPointSize2.Enabled)
                buttonItemPowerPointSize2.Checked = false;
            buttonItemPowerPointSize3.Enabled = BusinessClasses.MasterWizardManager.Instance.SelectedWizard.Has169;
            if (buttonItemPowerPointSize3.Checked && !buttonItemPowerPointSize3.Enabled)
                buttonItemPowerPointSize3.Checked = false;
            buttonItemPowerPointSize4.Enabled = BusinessClasses.MasterWizardManager.Instance.SelectedWizard.Has34;
            if (buttonItemPowerPointSize4.Checked && !buttonItemPowerPointSize4.Enabled)
                buttonItemPowerPointSize4.Checked = false;
            buttonItemPowerPointSize5.Enabled = BusinessClasses.MasterWizardManager.Instance.SelectedWizard.Has45;
            if (buttonItemPowerPointSize5.Checked && !buttonItemPowerPointSize5.Enabled)
                buttonItemPowerPointSize5.Checked = false;

            if (!buttonItemPowerPointSize1.Checked && !buttonItemPowerPointSize2.Checked && !buttonItemPowerPointSize3.Checked && !buttonItemPowerPointSize4.Checked && !buttonItemPowerPointSize5.Checked)
            {
                if (buttonItemPowerPointSize1.Enabled)
                    buttonItemPowerPointSize1.Checked = true;
                else if (buttonItemPowerPointSize2.Enabled)
                    buttonItemPowerPointSize2.Checked = true;
                else if (buttonItemPowerPointSize3.Enabled)
                    buttonItemPowerPointSize3.Checked = true;
                else if (buttonItemPowerPointSize4.Enabled)
                    buttonItemPowerPointSize4.Checked = true;
                else if (buttonItemPowerPointSize5.Enabled)
                    buttonItemPowerPointSize5.Checked = true;
            }
        }

        private void UpdateSlideTemplateStatus()
        {
            if (buttonItemPowerPointSize1.Checked)
            {
                if (!BusinessClasses.MasterWizardManager.Instance.SelectedWizard.HasSlideTemplate43)
                {
                    _allowToSave = false;
                    buttonItemPowerPointTemplateDisabled.Checked = true;
                    _allowToSave = true;
                    ribbonBarPowerPointSlideTemplate.Enabled = false;
                    ribbonBarPowerPointSlideTemplate.Enabled = false;
                }
                else
                {
                    _allowToSave = false;
                    buttonItemPowerPointTemplateDisabled.Checked = !ConfigurationClasses.SettingsManager.Instance.SlideTemplateEnabled;
                    _allowToSave = true;
                    buttonItemPowerPointTemplateEnabled.Checked = ConfigurationClasses.SettingsManager.Instance.SlideTemplateEnabled;
                    ribbonBarPowerPointSlideTemplate.Enabled = true;
                }
            }
            else if (buttonItemPowerPointSize2.Checked)
            {
                if (!BusinessClasses.MasterWizardManager.Instance.SelectedWizard.HasSlideTemplate54)
                {
                    _allowToSave = false;
                    buttonItemPowerPointTemplateDisabled.Checked = true;
                    _allowToSave = true;
                    ribbonBarPowerPointSlideTemplate.Enabled = false;
                    ribbonBarPowerPointSlideTemplate.Enabled = false;
                }
                else
                {
                    _allowToSave = false;
                    buttonItemPowerPointTemplateDisabled.Checked = !ConfigurationClasses.SettingsManager.Instance.SlideTemplateEnabled;
                    _allowToSave = true;
                    buttonItemPowerPointTemplateEnabled.Checked = ConfigurationClasses.SettingsManager.Instance.SlideTemplateEnabled;
                    ribbonBarPowerPointSlideTemplate.Enabled = true;
                }
            }
            else if (buttonItemPowerPointSize3.Checked)
            {
                if (!BusinessClasses.MasterWizardManager.Instance.SelectedWizard.HasSlideTemplate169)
                {
                    _allowToSave = false;
                    buttonItemPowerPointTemplateDisabled.Checked = true;
                    _allowToSave = true;
                    ribbonBarPowerPointSlideTemplate.Enabled = false;
                    ribbonBarPowerPointSlideTemplate.Enabled = false;
                }
                else
                {
                    _allowToSave = false;
                    buttonItemPowerPointTemplateDisabled.Checked = !ConfigurationClasses.SettingsManager.Instance.SlideTemplateEnabled;
                    _allowToSave = true;
                    buttonItemPowerPointTemplateEnabled.Checked = ConfigurationClasses.SettingsManager.Instance.SlideTemplateEnabled;
                    ribbonBarPowerPointSlideTemplate.Enabled = true;
                }
            }
            else if (buttonItemPowerPointSize4.Checked)
            {
                if (!BusinessClasses.MasterWizardManager.Instance.SelectedWizard.HasSlideTemplate34)
                {
                    _allowToSave = false;
                    buttonItemPowerPointTemplateDisabled.Checked = true;
                    _allowToSave = true;
                    ribbonBarPowerPointSlideTemplate.Enabled = false;
                    ribbonBarPowerPointSlideTemplate.Enabled = false;
                }
                else
                {
                    _allowToSave = false;
                    buttonItemPowerPointTemplateDisabled.Checked = !ConfigurationClasses.SettingsManager.Instance.SlideTemplateEnabled;
                    _allowToSave = true;
                    buttonItemPowerPointTemplateEnabled.Checked = ConfigurationClasses.SettingsManager.Instance.SlideTemplateEnabled;
                    ribbonBarPowerPointSlideTemplate.Enabled = true;
                }
            }
            else if (buttonItemPowerPointSize5.Checked)
            {
                if (!BusinessClasses.MasterWizardManager.Instance.SelectedWizard.HasSlideTemplate45)
                {
                    _allowToSave = false;
                    buttonItemPowerPointTemplateDisabled.Checked = true;
                    _allowToSave = true;
                    ribbonBarPowerPointSlideTemplate.Enabled = false;
                    ribbonBarPowerPointSlideTemplate.Enabled = false;
                }
                else
                {
                    _allowToSave = false;
                    buttonItemPowerPointTemplateDisabled.Checked = !ConfigurationClasses.SettingsManager.Instance.SlideTemplateEnabled;
                    _allowToSave = true;
                    buttonItemPowerPointTemplateEnabled.Checked = ConfigurationClasses.SettingsManager.Instance.SlideTemplateEnabled;
                    ribbonBarPowerPointSlideTemplate.Enabled = true;
                }
            }
        }

        private void buttonItemSize_Click(object sender, EventArgs e)
        {
            if (AppManager.Instance.AplicationDetected())
            {
                if (AppManager.Instance.ShowWarningQuestion("All active applications will be closed before you change PowerPoint settings.\nDo you want to continue?") == System.Windows.Forms.DialogResult.Yes)
                    AppManager.Instance.CloseActiveApplications();
                else
                    return;
            }
            if (InteropClasses.PowerPointHelper.Instance.PowerPointDetected())
            {
                using (ToolForms.FormFormatChangeNotification form = new ToolForms.FormFormatChangeNotification())
                {
                    DevComponents.DotNetBar.ButtonItem buttonPressed = (sender as DevComponents.DotNetBar.ButtonItem);
                    string currentFormatText = ConfigurationClasses.SettingsManager.Instance.SlideSize;
                    string futureFormatText = string.Empty;
                    if (buttonPressed == buttonItemPowerPointSize1)
                        futureFormatText = "Landscape 4 x 3";
                    else if (buttonPressed == buttonItemPowerPointSize2)
                        futureFormatText = "Landscape 5 x 4";
                    else if (buttonPressed == buttonItemPowerPointSize3)
                        futureFormatText = "Landscape 16 x 9";
                    else if (buttonPressed == buttonItemPowerPointSize4)
                        futureFormatText = "Portrait 3 x 4";
                    else if (buttonPressed == buttonItemPowerPointSize5)
                        futureFormatText = "Portrait 4 x 5";
                    form.labelControlCurrentState.Text = "Your curent presentation is: " + currentFormatText;
                    form.labelControlFutureState.Text = "You want to change your presentation to: " + futureFormatText;
                    if (form.ShowDialog() != System.Windows.Forms.DialogResult.Yes)
                        return;
                }
            }
            buttonItemPowerPointSize1.Checked = false;
            buttonItemPowerPointSize2.Checked = false;
            buttonItemPowerPointSize3.Checked = false;
            buttonItemPowerPointSize4.Checked = false;
            buttonItemPowerPointSize5.Checked = false;
            (sender as DevComponents.DotNetBar.ButtonItem).Checked = true;
            UpdateSlideTemplateStatus();
            ConfigurationClasses.SettingsManager.Instance.SaveSharedSettings();
            InteropClasses.PowerPointHelper.Instance.Connect();
            InteropClasses.PowerPointHelper.Instance.SetPresentationSettings();
        }

        private void buttonItemSize_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as DevComponents.DotNetBar.ButtonItem).Checked)
            {
                if (buttonItemPowerPointSize1.Checked)
                {
                    ConfigurationClasses.SettingsManager.Instance.SizeWidth = 10;
                    ConfigurationClasses.SettingsManager.Instance.SizeHeght = 7.5;
                    ConfigurationClasses.SettingsManager.Instance.Orientation = "Landscape";
                }
                else if (buttonItemPowerPointSize2.Checked)
                {
                    ConfigurationClasses.SettingsManager.Instance.SizeWidth = 10.75;
                    ConfigurationClasses.SettingsManager.Instance.SizeHeght = 8.25;
                    ConfigurationClasses.SettingsManager.Instance.Orientation = "Landscape";
                }
                else if (buttonItemPowerPointSize3.Checked)
                {
                    ConfigurationClasses.SettingsManager.Instance.SizeWidth = 10;
                    ConfigurationClasses.SettingsManager.Instance.SizeHeght = 5.63;
                    ConfigurationClasses.SettingsManager.Instance.Orientation = "Landscape";
                }
                else if (buttonItemPowerPointSize4.Checked)
                {
                    ConfigurationClasses.SettingsManager.Instance.SizeWidth = 10;
                    ConfigurationClasses.SettingsManager.Instance.SizeHeght = 7.5;
                    ConfigurationClasses.SettingsManager.Instance.Orientation = "Portrait";
                }
                else if (buttonItemPowerPointSize5.Checked)
                {
                    ConfigurationClasses.SettingsManager.Instance.SizeWidth = 10.75;
                    ConfigurationClasses.SettingsManager.Instance.SizeHeght = 8.25;
                    ConfigurationClasses.SettingsManager.Instance.Orientation = "Portrait";
                }
                UpdateApplicationsStatus();
            }
        }

        private void comboBoxEditPowerPointStyle_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            e.Cancel = false;
            if (AppManager.Instance.AplicationDetected())
            {
                if (AppManager.Instance.ShowWarningQuestion("All active applications will be closed before you change PowerPoint settings.\nDo you want to continue?") == System.Windows.Forms.DialogResult.Yes)
                {
                    AppManager.Instance.CloseActiveApplications();
                    e.Cancel = false;
                }
                else
                {
                    e.Cancel = true;
                    return;
                }
            }
            if (InteropClasses.PowerPointHelper.Instance.PowerPointDetected())
            {
                using (ToolForms.FormFormatChangeNotification form = new ToolForms.FormFormatChangeNotification())
                {
                    string currentFormatText = ConfigurationClasses.SettingsManager.Instance.SelectedWizard;
                    string futureFormatText = e.NewValue != null ? e.NewValue.ToString() : string.Empty;
                    form.labelControlCurrentState.Text = "Your curent wizard is: " + currentFormatText;
                    form.labelControlFutureState.Text = "You want to change your wizard to: " + futureFormatText;
                    if (form.ShowDialog() == System.Windows.Forms.DialogResult.Yes)
                        e.Cancel = false;
                    else
                        e.Cancel = true;
                }
            }
        }

        private void comboBoxEditPowerPointStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxEditPowerPointStyle.SelectedIndex >= 0)
            {
                SelectMasterWizard(comboBoxEditPowerPointStyle.EditValue.ToString());
            }
        }

        private void buttonItemPowerPointTemplate_CheckedChanged(object sender, EventArgs e)
        {
            if (_allowToSave)
            {
                ConfigurationClasses.SettingsManager.Instance.SlideTemplateEnabled = buttonItemPowerPointTemplateEnabled.Checked;
                ConfigurationClasses.SettingsManager.Instance.SaveSharedSettings();
            }
        }

        private void buttonItemPowerPointTemplate_Click(object sender, EventArgs e)
        {
            if (AppManager.Instance.AplicationDetected())
            {
                if (AppManager.Instance.ShowWarningQuestion("All active applications will be closed before you change PowerPoint settings.\nDo you want to continue?") == System.Windows.Forms.DialogResult.Yes)
                    AppManager.Instance.CloseActiveApplications();
                else
                    return;
            }
            _allowToSave = false;
            _allowToSave = true;
            buttonItemPowerPointTemplateDisabled.Checked = false;
            buttonItemPowerPointTemplateEnabled.Checked = false;
            (sender as DevComponents.DotNetBar.ButtonItem).Checked = true;
        }

        private void buttonItemPowerPointLaunch_Click(object sender, EventArgs e)
        {
            RunPowerPoint();
            AppManager.Instance.ActivateForm(this.Handle, false);
        }
        #endregion

        #region Dashboard Methods
        private void buttonItemDashboard_Click(object sender, EventArgs e)
        {
            AppManager.Instance.RunDashboard();
        }

        private void buttonItemDashboardCover_Click(object sender, EventArgs e)
        {
            using (ToolForms.FormAddCover form = new ToolForms.FormAddCover())
            {
                DialogResult result = form.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.Yes)
                    AppManager.Instance.RunDashboard("showcover");
                else if (result == System.Windows.Forms.DialogResult.No)
                {
                    if (!InteropClasses.PowerPointHelper.Instance.PowerPointDetected())
                    {
                        if (AppManager.Instance.ShowWarningQuestion("You need to first open PowerPoint\nDo you want to do that now?") == System.Windows.Forms.DialogResult.Yes)
                            RunPowerPoint();
                        else
                            return;
                    }
                    InteropClasses.PowerPointHelper.Instance.Connect();
                    InteropClasses.PowerPointHelper.Instance.AppendGenericCover(true);
                    AppManager.Instance.ActivateForm(this.Handle, false);
                }
            }
        }

        private void buttonItemDashboardCleanslate_Click(object sender, EventArgs e)
        {
            if (!InteropClasses.PowerPointHelper.Instance.PowerPointDetected())
            {
                if (AppManager.Instance.ShowWarningQuestion("You need to first open PowerPoint\nDo you want to do that now?") == System.Windows.Forms.DialogResult.Yes)
                    RunPowerPoint();
                else
                    return;
            }
            InteropClasses.PowerPointHelper.Instance.Connect();
            InteropClasses.PowerPointHelper.Instance.AppendCleanslate();
            AppManager.Instance.ActivateForm(this.Handle, false);
        }
        #endregion

        #region Apps Methods
        private void LoadNBWApplication()
        {
            if (BusinessClasses.NBWApplicationsManager.Instance.NBWApplications.Count > 0)
            {
                foreach (BusinessClasses.NBWApplication nbwApplication in BusinessClasses.NBWApplicationsManager.Instance.NBWApplications)
                {
                    AddAppDefinition(nbwApplication);
                }
                ribbonBarApps.RecalcLayout();
                ribbonPanelApps.PerformLayout();
            }
        }

        private void UpdateApplicationsStatus()
        {
            foreach (BusinessClasses.NBWApplication nbwApplication in BusinessClasses.NBWApplicationsManager.Instance.NBWApplications)
            {
                if (nbwApplication.UseSlideTemplates && BusinessClasses.MasterWizardManager.Instance.SelectedWizard != null)
                {
                    string slideTemplatesFolderPath = string.Empty;
                    if (nbwApplication.UseWizard)
                        slideTemplatesFolderPath = Path.Combine(BusinessClasses.MasterWizardManager.Instance.SelectedWizard.Folder.FullName, ConfigurationClasses.SettingsManager.Instance.SlideFolder, nbwApplication.SlideTemplatesPath);
                    else
                        slideTemplatesFolderPath = Path.Combine(BusinessClasses.MasterWizardManager.ScheduleBuildersFolder, ConfigurationClasses.SettingsManager.Instance.SlideFolder, nbwApplication.SlideTemplatesPath);
                    if (Directory.Exists(slideTemplatesFolderPath))
                    {
                        nbwApplication.AppLabel.Enabled = true;
                        nbwApplication.AppButton.Enabled = true;
                        //nbwApplication.AppButton.Visible = true;
                        //nbwApplication.DisabledButton.Visible = false;
                    }
                    else
                    {
                        nbwApplication.AppLabel.Enabled = false;
                        nbwApplication.AppButton.Enabled = false;
                        //nbwApplication.AppButton.Visible = false;
                        //nbwApplication.DisabledButton.Visible = true;
                    }
                }
            }
        }

        private void AddAppDefinition(BusinessClasses.NBWApplication nbwApplication)
        {
            DevComponents.DotNetBar.ItemContainer itemContainerApp = new DevComponents.DotNetBar.ItemContainer();
            itemContainerApp.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
                nbwApplication.AppButton,
                nbwApplication.DisabledButton,
                nbwApplication.AppLabel});
            galleryContainerApps.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] { itemContainerApp });
            superTooltip.SetSuperTooltip(nbwApplication.DisabledButton, new DevComponents.DotNetBar.SuperTooltipInfo("This app is DISABLED", string.Empty, "Check your PowerPoint slide size on the first Tab of this minibar", null, null, DevComponents.DotNetBar.eTooltipColor.Default, true, false, new System.Drawing.Size(0, 0)));
        }
        #endregion

        #region Clipart Methods
        private void buttonItemClipartClientLogos_Click(object sender, EventArgs e)
        {
            AppManager.Instance.RunClientLogos();
        }

        private void buttonItemClipartSalesGallery_Click(object sender, EventArgs e)
        {
            AppManager.Instance.RunSalesGallery();
        }

        private void buttonItemClipartWebArt_Click(object sender, EventArgs e)
        {
            AppManager.Instance.RunWebArt();
        }
        #endregion

        #region PDF Methods
        private void buttonItemPdfSavePdf_Click(object sender, EventArgs e)
        {
            bool toContinue = false;
            string presentationName = string.Empty;
            if (InteropClasses.PowerPointHelper.Instance.PowerPointDetected())
            {
                InteropClasses.PowerPointHelper.Instance.Connect();
                if (InteropClasses.PowerPointHelper.Instance.Is2003)
                {
                    AppManager.Instance.ShowWarning("Your Version of PowerPoint will not Convert to PDF.\nPDF Converting  Only works with Office 2007 and 2010.");
                    return;
                }
                if (InteropClasses.PowerPointHelper.Instance.IsActive && InteropClasses.PowerPointHelper.Instance.PowerPointObject.WindowState != Microsoft.Office.Interop.PowerPoint.PpWindowState.ppWindowMinimized)
                {
                    presentationName = InteropClasses.PowerPointHelper.Instance.ActiveFileName;
                    if (!string.IsNullOrEmpty(presentationName))
                        toContinue = true;
                }
            }

            if (toContinue)
            {
                if (AppManager.Instance.ShowWarningQuestion("Do you want to save " + presentationName + " as a PDF?") == System.Windows.Forms.DialogResult.Yes)
                {
                    using (SaveFileDialog dialog = new SaveFileDialog())
                    {
                        dialog.FileName = presentationName.Replace(".pptx", "").Replace(".ppt", "") + ".pdf";
                        dialog.Title = "Save Presentation As PDF";
                        dialog.Filter = "Adobe PDF Files|*.pdf";
                        dialog.DefaultExt = "*.pdf";
                        if (dialog.ShowDialog() == DialogResult.OK)
                        {
                            InteropClasses.PowerPointHelper.Instance.SavePDF(dialog.FileName);
                            if (File.Exists(dialog.FileName))
                                Process.Start(dialog.FileName);
                        }
                    }
                }
            }
            else
                AppManager.Instance.ShowWarning("There is no active PowerPoint Presentation.\nOpen your presentation and try again.");
        }

        private void buttonItemPdfEmailPdf_Click(object sender, EventArgs e)
        {
            bool toContinue = false;
            string presentationName = string.Empty;
            if (InteropClasses.PowerPointHelper.Instance.PowerPointDetected())
            {
                InteropClasses.PowerPointHelper.Instance.Connect();
                if (InteropClasses.PowerPointHelper.Instance.Is2003)
                {
                    AppManager.Instance.ShowWarning("Your Version of PowerPoint will not Convert to PDF.\nPDF Converting  Only works with Office 2007 and 2010.");
                    return;
                }
                if (InteropClasses.PowerPointHelper.Instance.IsActive && InteropClasses.PowerPointHelper.Instance.PowerPointObject.WindowState != Microsoft.Office.Interop.PowerPoint.PpWindowState.ppWindowMinimized)
                {
                    presentationName = InteropClasses.PowerPointHelper.Instance.ActiveFileName;
                    if (!string.IsNullOrEmpty(presentationName))
                        toContinue = true;
                }
            }

            if (toContinue)
            {
                if (AppManager.Instance.ShowWarningQuestion("Do you want to Email a PDF Version of " + presentationName + "?") == System.Windows.Forms.DialogResult.Yes)
                {
                    string fileName = Path.Combine(Path.GetTempPath(), presentationName.Replace(".pptx", "").Replace(".ppt", "") + ".pdf");
                    InteropClasses.PowerPointHelper.Instance.SavePDF(fileName);
                    if (InteropClasses.OutlookHelper.Instance.Open())
                    {
                        InteropClasses.OutlookHelper.Instance.CreateMessage(fileName);
                        InteropClasses.OutlookHelper.Instance.Close();
                    }
                    else
                        AppManager.Instance.ShowWarning("Couldn't open Outlook");
                }
            }
            else
                AppManager.Instance.ShowWarning("There is no active PowerPoint Presentation.\nOpen your presentation and try again.");
        }

        private void buttonItemPdfEmailPpt_Click(object sender, EventArgs e)
        {
            bool toContinue = false;
            string presentationName = string.Empty;
            if (InteropClasses.PowerPointHelper.Instance.PowerPointDetected())
            {
                InteropClasses.PowerPointHelper.Instance.Connect();
                if (InteropClasses.PowerPointHelper.Instance.IsActive && InteropClasses.PowerPointHelper.Instance.PowerPointObject.WindowState != Microsoft.Office.Interop.PowerPoint.PpWindowState.ppWindowMinimized)
                {
                    presentationName = InteropClasses.PowerPointHelper.Instance.ActiveFileName;
                    if (!string.IsNullOrEmpty(presentationName))
                        toContinue = true;
                }
            }

            if (toContinue)
            {
                if (AppManager.Instance.ShowWarningQuestion("Do you want to Email " + presentationName + "?") == System.Windows.Forms.DialogResult.Yes)
                {
                    string fileName = Path.Combine(Path.GetTempPath(), presentationName.Replace(".pptx", "").Replace(".ppt", "") + ".pdf");
                    if (InteropClasses.OutlookHelper.Instance.Open())
                    {
                        InteropClasses.OutlookHelper.Instance.CreateMessage(InteropClasses.PowerPointHelper.Instance.ActiveFilePath);
                        InteropClasses.OutlookHelper.Instance.Close();
                    }
                    else
                        AppManager.Instance.ShowWarning("Couldn't open Outlook");
                }
            }
            else
                AppManager.Instance.ShowWarning("There is no active PowerPoint Presentation.\nOpen your presentation and try again.");
        }
        #endregion

        #region Tools Methods
        private void buttonItemToolsContent_Click(object sender, EventArgs e)
        {
            if (!InteropClasses.PowerPointHelper.Instance.PowerPointDetected())
            {
                AppManager.Instance.ShowWarning("You have no Active PowerPoint Presentation.");
                return;
            }
            InteropClasses.PowerPointHelper.Instance.Connect();
            AppManager.Instance.ActivateForm(this.Handle, false);
            using (ToolForms.FormSlideContentTools form = new ToolForms.FormSlideContentTools())
            {
                form.ShowDialog();
            }
            AppManager.Instance.ActivateForm(this.Handle, false);
        }

        private void buttonItemToolsPageNumbers_Click(object sender, EventArgs e)
        {
            if (!InteropClasses.PowerPointHelper.Instance.PowerPointDetected())
            {
                AppManager.Instance.ShowWarning("You have no Active PowerPoint Presentation.");
                return;
            }
            InteropClasses.PowerPointHelper.Instance.Connect();
            AppManager.Instance.ActivateForm(this.Handle, false);
            using (ToolForms.FormPageNumbersTools form = new ToolForms.FormPageNumbersTools())
            {
                form.ShowDialog();
            }
            AppManager.Instance.ActivateForm(this.Handle, false);
        }

        private void buttonItemToolsSlideHeader_Click(object sender, EventArgs e)
        {
            if (!InteropClasses.PowerPointHelper.Instance.PowerPointDetected())
            {
                AppManager.Instance.ShowWarning("You have no Active PowerPoint Presentation.");
                return;
            }
            InteropClasses.PowerPointHelper.Instance.Connect();
            AppManager.Instance.ActivateForm(this.Handle, false);
            using (ToolForms.FormSlideHeadersTools form = new ToolForms.FormSlideHeadersTools())
            {
                form.ShowDialog();
            }
            AppManager.Instance.ActivateForm(this.Handle, false);
        }

        private void buttonItemToolsSave_Click(object sender, EventArgs e)
        {
            if (!InteropClasses.PowerPointHelper.Instance.PowerPointDetected())
            {
                AppManager.Instance.ShowWarning("You have no Active PowerPoint Presentation.");
                return;
            }
            InteropClasses.PowerPointHelper.Instance.Connect();
            AppManager.Instance.ActivateForm(this.Handle, false);
            InteropClasses.PowerPointHelper.Instance.AddContents(true);
            InteropClasses.PowerPointHelper.Instance.AddPageNumbers();
            AppManager.Instance.ActivateForm(this.Handle, false);
        }
        #endregion

        #region Settings Methods
        private void buttonItemSettingsPowerPoint_Click(object sender, EventArgs e)
        {
            if (AppManager.Instance.ShowWarningQuestion("ARE YOU SURE YOU WANT TO STOP ALL POWERPOINT PROCESSES?\nSave any Active PowerPoint Presentations that may be running...") == System.Windows.Forms.DialogResult.Yes)
                AppManager.Instance.KillPowerPoint();
        }

        private void buttonItemSettingsExcel_Click(object sender, EventArgs e)
        {
            if (AppManager.Instance.ShowWarningQuestion("ARE YOU SURE YOU WANT TO STOP ALL EXCEL PROCESSES?\nSave any Active PowerPoint Presentations that may be running...") == System.Windows.Forms.DialogResult.Yes)
                AppManager.Instance.KillExcel();
        }

        private void buttonItemSettingsKillFMAutoSync_Click(object sender, EventArgs e)
        {
            if (AppManager.Instance.ShowWarningQuestion("Are you sure you want to terminate FM AutoSync process?") == System.Windows.Forms.DialogResult.Yes)
                AppManager.Instance.KillFMAutoSync();
        }

        private void buttonItemSettingsReset_Click(object sender, EventArgs e)
        {
            if (AppManager.Instance.ShowWarningQuestion("THIS ACTION WILL REMOVE ALL SETTINGS!\n\nARE YOU SURE YOU WANT TO DO THIS?") == System.Windows.Forms.DialogResult.Yes)
                if (AppManager.Instance.ShowWarningQuestion("OKAY!\n\nARE YOU ABSOLUTELY SURE YOU WANT TO RESET YOUR SOFTWARE!") == System.Windows.Forms.DialogResult.Yes)
                {
                    AppManager.Instance.WipeSoftware();
                }
        }

        private void buttonItemSettingsDesktop_Click(object sender, EventArgs e)
        {
            using (SettingsForms.FormShortcuts form = new SettingsForms.FormShortcuts())
            {
                form.ShowDialog();
            }
        }

        private void buttonItemSettingsMinibar_Click(object sender, EventArgs e)
        {
            using (SettingsForms.FormMinibarOptions form = new SettingsForms.FormMinibarOptions())
            {
                form.ShowDialog();
            }
        }

        private void buttonItemSyncHourly_Click(object sender, EventArgs e)
        {
            buttonItemSyncHourlyOn.Checked = false;
            buttonItemSyncHourlyOff.Checked = false;
            (sender as DevComponents.DotNetBar.ButtonItem).Checked = !(sender as DevComponents.DotNetBar.ButtonItem).Checked;
        }

        private void buttonItemSyncHourly_CheckedChanged(object sender, EventArgs e)
        {
            ConfigurationClasses.SettingsManager.Instance.SyncHourly = buttonItemSyncHourlyOn.Checked;
            ConfigurationClasses.SettingsManager.Instance.SaveMinibarSettings();
            BusinessClasses.SyncManager.SchedulerSyncInBackground();
        }

        private void buttonItemSettingsMonitor_Click(object sender, EventArgs e)
        {
            buttonItemSettingsMonitor1.Checked = false;
            buttonItemSettingsMonitor2.Checked = false;
            (sender as DevComponents.DotNetBar.ButtonItem).Checked = !(sender as DevComponents.DotNetBar.ButtonItem).Checked;
        }

        private void buttonItemSettingsMonitor_CheckedChanged(object sender, EventArgs e)
        {
            lock (AppManager.Locker)
            {
                ConfigurationClasses.SettingsManager.Instance.OnPrimaryScreen = buttonItemSettingsMonitor1.Checked;
                ConfigurationClasses.SettingsManager.Instance.SaveMinibarSettings();
            }
        }
        #endregion

        #region Training Methods
        private void buttonItemTrainingMeetingIDCopy_Click(object sender, EventArgs e)
        {
            if (comboBoxEditMeetingID.EditValue != null)
            {
                Clipboard.SetText(comboBoxEditMeetingID.EditValue.ToString());
                AppManager.Instance.ShowInformation("Meeting ID was copied to clipboard successfully");
            }
            else
                AppManager.Instance.ShowWarning("Select Meeting ID to copy");
        }
        private void buttonItemTrainingLocation_Click(object sender, EventArgs e)
        {
            if (textEditLocation.EditValue != null)
            {
                Clipboard.SetText(textEditLocation.EditValue.ToString().Trim());
                AppManager.Instance.ShowInformation("Location was copied to clipboard successfully");
            }
            else
                AppManager.Instance.ShowWarning("Select Location to copy");
        }

        private void buttonItemTrainingLiveMeeting_Click(object sender, EventArgs e)
        {
            if (File.Exists(@"C:\Program Files\Microsoft Office\Live Meeting 8\Console\PWConsole.exe"))
            {
                Process.Start(@"C:\Program Files\Microsoft Office\Live Meeting 8\Console\PWConsole.exe");
                return;
            }
            if (File.Exists(@"C:\Program Files (x86)\Microsoft Office\Live Meeting 8\Console\PWConsole.exe"))
            {
                Process.Start(@"C:\Program Files (x86)\Microsoft Office\Live Meeting 8\Console\PWConsole.exe");
                return;
            }
            AppManager.Instance.ShowWarning("LiveMeeting was not found");
        }
        #endregion

        #region Sync Methods
        private void buttonItemSyncStart_Click(object sender, EventArgs e)
        {
            BusinessClasses.SyncManager.RegularSynchronize();
        }

        public void DisplayNextSync(object param)
        {
            DateTime dt = (DateTime)param;
            labelItemNextSyncValue.Text = dt.ToString("MM/dd/yy h:mm tt");
            ribbonBarSyncStatus.RecalcLayout();
            ribbonPanelSync.PerformLayout();
        }
        public void DisplayLastSync(object param)
        {
            DateTime dt = (DateTime)param;
            labelItemLastSyncValue.Text = dt.ToString("MM/dd/yy h:mm tt");
            ribbonBarSyncStatus.RecalcLayout();
            ribbonPanelSync.PerformLayout();
        }
        #endregion

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

        #region Common Methods
        public void Init()
        {
            DisplayLastSync(ConfigurationClasses.SettingsManager.Instance.LastSync);

            if (ribbonControl.SelectedRibbonTabItem != null)
                ribbonControl.SelectedRibbonTabItem.Checked = false;
            ribbonTabItemDashboard.Text = ConfigurationClasses.SettingsManager.Instance.DashboardName;
            labelItemDashboard.Text = ConfigurationClasses.SettingsManager.Instance.DashboardName;
            if (File.Exists(ConfigurationClasses.SettingsManager.Instance.DashboardLogoPath) && File.Exists(ConfigurationClasses.SettingsManager.Instance.DashboardIconPath))
                buttonItemDashboard.Image = new Bitmap(ConfigurationClasses.SettingsManager.Instance.DashboardLogoPath);
            ribbonBarDashboard.RecalcLayout();
            ribbonPanelDashboard.PerformLayout();

            ribbonTabItemSalesDepot.Text = ConfigurationClasses.SettingsManager.Instance.SalesDepotName;
            if (File.Exists(ConfigurationClasses.SettingsManager.Instance.SalesDepotLogoPath) && File.Exists(ConfigurationClasses.SettingsManager.Instance.SalesDepotIconPath))
                buttonItemSalesDepotLogo.Image = new Bitmap(ConfigurationClasses.SettingsManager.Instance.SalesDepotLogoPath);

            labelItemSalesDepot.Text = ConfigurationClasses.SettingsManager.Instance.SalesDepotName;
            ribbonBarSalesDepot.RecalcLayout();

            ribbonBarSalesDepotRemote.Visible = System.IO.Directory.Exists(ConfigurationClasses.SettingsManager.Instance.SalesDepotRemoteRootPath) & ConfigurationClasses.SettingsManager.Instance.UseRemoteSalesDepot;

            ribbonPanelSalesDepot.PerformLayout();

            SetPresentationSettings();

            comboBoxEditPowerPointStyle.EditValueChanging -= new DevExpress.XtraEditors.Controls.ChangingEventHandler(comboBoxEditPowerPointStyle_EditValueChanging);
            comboBoxEditPowerPointStyle.Properties.Items.Clear();
            foreach (string masterWizard in BusinessClasses.MasterWizardManager.Instance.MasterWizards.Keys)
                comboBoxEditPowerPointStyle.Properties.Items.Add(masterWizard);

            int selectedIndex = comboBoxEditPowerPointStyle.Properties.Items.IndexOf(ConfigurationClasses.SettingsManager.Instance.SelectedWizard);
            if (selectedIndex < 0)
                selectedIndex = 0;

            if (comboBoxEditPowerPointStyle.Properties.Items.Count > 0)
                comboBoxEditPowerPointStyle.SelectedIndex = selectedIndex;
            comboBoxEditPowerPointStyle.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(comboBoxEditPowerPointStyle_EditValueChanging);

            comboBoxEditMeetingID.Properties.Items.Clear();
            foreach (string meetingID in ConfigurationClasses.SettingsManager.Instance.MeetingIDs)
                comboBoxEditMeetingID.Properties.Items.Add(meetingID);
            if (comboBoxEditMeetingID.Properties.Items.Count > 0)
                comboBoxEditMeetingID.SelectedIndex = 0;
            textEditLocation.EditValue = ConfigurationClasses.SettingsManager.Instance.Location;

            buttonItemSyncHourlyOn.Checked = ConfigurationClasses.SettingsManager.Instance.SyncHourly;
            buttonItemSyncHourlyOff.Checked = !ConfigurationClasses.SettingsManager.Instance.SyncHourly;

            buttonItemSettingsMonitor1.Checked = ConfigurationClasses.SettingsManager.Instance.OnPrimaryScreen;
            buttonItemSettingsMonitor2.Checked = !ConfigurationClasses.SettingsManager.Instance.OnPrimaryScreen;

            LoadNBWApplication();

            bool appVisible = BusinessClasses.NBWApplicationsManager.Instance.NBWApplications.Count > 0;
            bool salesDepotVisisble = Directory.GetDirectories(ConfigurationClasses.SettingsManager.Instance.LibraryPath).Length > 0;
            bool clipartVisible = Directory.GetDirectories(ConfigurationClasses.SettingsManager.Instance.ClipartPath).Length > 0;

            if (!salesDepotVisisble && !clipartVisible && !appVisible)
            {
                ribbonTabItemTraining.Text = "Webcast Training";
                ribbonTabItemSync.Text = "Synchronize";
                ribbonTabItemSettings.Text = "My Settings";
            }
            else if (!salesDepotVisisble && !clipartVisible)
            {
                ribbonTabItemTraining.Text = "Webcast Training";
                ribbonTabItemSync.Text = "Synchronize";
            }
            else if (!salesDepotVisisble && !appVisible)
            {
                ribbonTabItemTraining.Text = "Webcast Training";
                ribbonTabItemSync.Text = "Synchronize";
            }
            else if (!clipartVisible && !appVisible)
            {
                ribbonTabItemTraining.Text = "Webcast Training";
            }
            else if (!salesDepotVisisble)
            {
                ribbonTabItemSync.Text = "Synchronize";
            }

            ribbonTabItemApps.Visible = appVisible;
            ribbonTabItemSalesDepot.Visible = salesDepotVisisble;
            ribbonTabItemClipart.Visible = clipartVisible;

            ribbonTabItemDashboard.Enabled = ConfigurationClasses.SettingsManager.Instance.SlidesReadyFull;
            ribbonTabItemApps.Enabled = ConfigurationClasses.SettingsManager.Instance.SlidesReadyFull;
            ribbonTabItemTools.Enabled = ConfigurationClasses.SettingsManager.Instance.SlidesReadyFull;
            ribbonTabItemSettings.Enabled = ConfigurationClasses.SettingsManager.Instance.SlidesReadyFull;

            ribbonBarSettingsMonitors.Visible = Screen.AllScreens.Length > 1;

            if (ConfigurationClasses.SettingsManager.Instance.AutoRunFloat)
                ShowFloater();
        }

        private void ShowFloater()
        {
            FormFloater form = new FormFloater();
            form.StartPosition = FormStartPosition.Manual;
            int defaultTop = Screen.PrimaryScreen.Bounds.Height - form.Height - 50;
            int defaultLeft = Screen.PrimaryScreen.Bounds.Width - form.Width - 50;
            form.Location = new Point(ConfigurationClasses.SettingsManager.Instance.FloaterLeft == 0 || ConfigurationClasses.SettingsManager.Instance.FloaterLeft > defaultLeft ? defaultLeft : ConfigurationClasses.SettingsManager.Instance.FloaterLeft, ConfigurationClasses.SettingsManager.Instance.FloaterTop == 0 || ConfigurationClasses.SettingsManager.Instance.FloaterTop > defaultTop ? defaultTop : ConfigurationClasses.SettingsManager.Instance.FloaterTop);
            form.Show();
        }

        private void ActivateRetractedForm()
        {
            _formMouseLeaveTimer.Stop();
            FadeOut();
            FormMain.Instance.FadeIn();
        }

        public void FadeIn()
        {
            if (!_expanded)
            {
                Timer timer = new Timer();
                timer.Interval = 30;
                timer.Tick += new EventHandler(FadeInTimer_Tick);
                timer.Start();
            }
        }

        public void FadeOut()
        {
            if (_expanded)
            {
                Timer timer = new Timer();
                timer.Interval = 30;
                timer.Tick += new EventHandler(FadeOutTimer_Tick);
                timer.Start();
            }
        }
        #endregion
    }
}
