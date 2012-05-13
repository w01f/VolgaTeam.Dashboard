using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace NewBizWizForm
{
    public partial class FormMain : Form
    {
        private static FormMain _instance;
        public AppManager.EmptyParametersDelegate OutputClick { get; set; }
        public AppManager.EmptyParametersDelegate OutsideClick { get; set; }
        public bool IsDead { get; set; }

        private FormMain()
        {
            _instance = this;
            InitializeComponent();
            AppManager.Instance.SetClickEventHandler(ribbonControl);
            AppManager.Instance.SetClickEventHandler(panelExMain);
            if ((base.CreateGraphics()).DpiX > 96)
            {
                ribbonControl.Font = new Font(ribbonControl.Font.FontFamily, ribbonControl.Font.Size - 1, ribbonControl.Font.Style);
            }
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

        #region Configuration Methods
        private void ApplyMasterWizard()
        {
            FormMain.Instance.Text = AppManager.FormCaption;
            System.Drawing.Image masterWizardLogo = BusinessClasses.MasterWizardManager.Instance.DefaultLogo;
            buttonItemHomeOverview.Image = masterWizardLogo;
            buttonItemOnlineLogo.Image = masterWizardLogo;
            buttonItemMobileLogo.Image = masterWizardLogo;
            buttonItemNewspaperLogo.Image = masterWizardLogo;
            buttonItemTVLogo.Image = masterWizardLogo;
            buttonItemCableLogo.Image = masterWizardLogo;
            buttonItemRadioLogo.Image = masterWizardLogo;
            buttonItemStarLogo.Image = masterWizardLogo;
            buttonItemDigitalLogo.Image = masterWizardLogo;
            buttonItemCalendarLogo.Image = masterWizardLogo;
            buttonItemiPadLogo.Image = masterWizardLogo;
            ribbonBarHomeOverview.RecalcLayout();
            ribbonPanelHome.PerformLayout();
        }

        private void SetDashboardCode()
        {
            switch (ConfigurationClasses.SettingsManager.Instance.DashboardCode)
            {
                case 1:
                    ribbonTabItemCable.Visible = false;
                    ribbonTabItemMobile.Visible = true;
                    ribbonTabItemNewspaper.Visible = true;
                    ribbonTabItemOnline.Visible = true;
                    ribbonTabItemRadio.Visible = false;
                    ribbonTabItemTV.Visible = false;
                    ribbonTabItemStar.Visible = false;
                    ribbonTabItemDigital.Visible = false;
                    ribbonTabItemCalendar.Visible = false;
                    break;
                case 2:
                    ribbonTabItemCable.Visible = false;
                    ribbonTabItemMobile.Visible = true;
                    ribbonTabItemNewspaper.Visible = false;
                    ribbonTabItemOnline.Visible = true;
                    ribbonTabItemRadio.Visible = false;
                    ribbonTabItemTV.Visible = true;
                    ribbonTabItemStar.Visible = false;
                    ribbonTabItemDigital.Visible = false;
                    ribbonTabItemCalendar.Visible = false;
                    break;
                case 3:
                    ribbonTabItemCable.Visible = false;
                    ribbonTabItemMobile.Visible = true;
                    ribbonTabItemNewspaper.Visible = false;
                    ribbonTabItemOnline.Visible = true;
                    ribbonTabItemRadio.Visible = true;
                    ribbonTabItemTV.Visible = false;
                    ribbonTabItemStar.Visible = false;
                    ribbonTabItemDigital.Visible = false;
                    ribbonTabItemCalendar.Visible = false;
                    break;
                case 4:
                    ribbonTabItemCable.Visible = false;
                    ribbonTabItemMobile.Visible = true;
                    ribbonTabItemNewspaper.Visible = true;
                    ribbonTabItemOnline.Visible = true;
                    ribbonTabItemRadio.Visible = false;
                    ribbonTabItemTV.Visible = true;
                    ribbonTabItemStar.Visible = false;
                    ribbonTabItemDigital.Visible = false;
                    ribbonTabItemCalendar.Visible = false;
                    break;
                case 5:
                    ribbonTabItemCable.Visible = false;
                    ribbonTabItemMobile.Visible = true;
                    ribbonTabItemNewspaper.Visible = false;
                    ribbonTabItemOnline.Visible = true;
                    ribbonTabItemRadio.Visible = true;
                    ribbonTabItemTV.Visible = true;
                    ribbonTabItemStar.Visible = false;
                    ribbonTabItemDigital.Visible = false;
                    ribbonTabItemCalendar.Visible = false;
                    break;
                case 6:
                    ribbonTabItemCable.Visible = true;
                    ribbonTabItemMobile.Visible = true;
                    ribbonTabItemNewspaper.Visible = false;
                    ribbonTabItemOnline.Visible = true;
                    ribbonTabItemRadio.Visible = false;
                    ribbonTabItemTV.Visible = false;
                    ribbonTabItemStar.Visible = false;
                    ribbonTabItemDigital.Visible = false;
                    ribbonTabItemCalendar.Visible = false;
                    break;
                case 7:
                    ribbonTabItemCable.Visible = true;
                    ribbonTabItemMobile.Visible = true;
                    ribbonTabItemNewspaper.Visible = true;
                    ribbonTabItemOnline.Visible = true;
                    ribbonTabItemRadio.Visible = true;
                    ribbonTabItemTV.Visible = true;
                    ribbonTabItemStar.Visible = false;
                    ribbonTabItemDigital.Visible = false;
                    ribbonTabItemCalendar.Visible = false;
                    break;
                case 8:
                    ribbonTabItemCable.Visible = false;
                    ribbonTabItemMobile.Visible = true;
                    ribbonTabItemNewspaper.Visible = false;
                    ribbonTabItemOnline.Visible = true;
                    ribbonTabItemRadio.Visible = false;
                    ribbonTabItemTV.Visible = false;
                    ribbonTabItemStar.Visible = false;
                    ribbonTabItemDigital.Visible = false;
                    ribbonTabItemCalendar.Visible = false;
                    break;
                case 9:
                    ribbonTabItemCable.Visible = false;
                    ribbonTabItemMobile.Visible = false;
                    ribbonTabItemNewspaper.Visible = true;
                    ribbonTabItemOnline.Visible = false;
                    ribbonTabItemRadio.Visible = false;
                    ribbonTabItemTV.Visible = false;
                    ribbonTabItemStar.Visible = false;
                    ribbonTabItemDigital.Visible = true;
                    ribbonTabItemCalendar.Visible = false;
                    break;
                case 10:
                    ribbonTabItemCable.Visible = false;
                    ribbonTabItemMobile.Visible = false;
                    ribbonTabItemNewspaper.Visible = false;
                    ribbonTabItemOnline.Visible = false;
                    ribbonTabItemRadio.Visible = false;
                    ribbonTabItemTV.Visible = true;
                    ribbonTabItemStar.Visible = false;
                    ribbonTabItemDigital.Visible = true;
                    ribbonTabItemCalendar.Visible = false;
                    break;
                case 11:
                    ribbonTabItemCable.Visible = false;
                    ribbonTabItemMobile.Visible = false;
                    ribbonTabItemNewspaper.Visible = false;
                    ribbonTabItemOnline.Visible = false;
                    ribbonTabItemRadio.Visible = true;
                    ribbonTabItemTV.Visible = false;
                    ribbonTabItemStar.Visible = false;
                    ribbonTabItemDigital.Visible = true;
                    ribbonTabItemCalendar.Visible = false;
                    break;
                case 12:
                    ribbonTabItemCable.Visible = false;
                    ribbonTabItemMobile.Visible = false;
                    ribbonTabItemNewspaper.Visible = true;
                    ribbonTabItemOnline.Visible = false;
                    ribbonTabItemRadio.Visible = false;
                    ribbonTabItemTV.Visible = false;
                    ribbonTabItemStar.Visible = false;
                    ribbonTabItemDigital.Visible = true;
                    ribbonTabItemCalendar.Visible = true;
                    break;
                case 13:
                    ribbonTabItemCable.Visible = false;
                    ribbonTabItemMobile.Visible = false;
                    ribbonTabItemNewspaper.Visible = false;
                    ribbonTabItemOnline.Visible = false;
                    ribbonTabItemRadio.Visible = false;
                    ribbonTabItemTV.Visible = true;
                    ribbonTabItemStar.Visible = false;
                    ribbonTabItemDigital.Visible = true;
                    ribbonTabItemCalendar.Visible = true;
                    break;
                case 14:
                    ribbonTabItemCable.Visible = false;
                    ribbonTabItemMobile.Visible = false;
                    ribbonTabItemNewspaper.Visible = false;
                    ribbonTabItemOnline.Visible = false;
                    ribbonTabItemRadio.Visible = true;
                    ribbonTabItemTV.Visible = false;
                    ribbonTabItemStar.Visible = false;
                    ribbonTabItemDigital.Visible = true;
                    ribbonTabItemCalendar.Visible = true;
                    break;
                case 15:
                    ribbonTabItemCable.Visible = false;
                    ribbonTabItemMobile.Visible = false;
                    ribbonTabItemNewspaper.Visible = true;
                    ribbonTabItemOnline.Visible = false;
                    ribbonTabItemRadio.Visible = false;
                    ribbonTabItemTV.Visible = true;
                    ribbonTabItemStar.Visible = false;
                    ribbonTabItemDigital.Visible = true;
                    ribbonTabItemCalendar.Visible = false;
                    break;
                case 16:
                    ribbonTabItemCable.Visible = false;
                    ribbonTabItemMobile.Visible = false;
                    ribbonTabItemNewspaper.Visible = true;
                    ribbonTabItemOnline.Visible = false;
                    ribbonTabItemRadio.Visible = false;
                    ribbonTabItemTV.Visible = true;
                    ribbonTabItemStar.Visible = false;
                    ribbonTabItemDigital.Visible = true;
                    ribbonTabItemCalendar.Visible = true;
                    break;
                case 17:
                    ribbonTabItemCable.Visible = false;
                    ribbonTabItemMobile.Visible = true;
                    ribbonTabItemNewspaper.Visible = true;
                    ribbonTabItemOnline.Visible = true;
                    ribbonTabItemRadio.Visible = false;
                    ribbonTabItemTV.Visible = true;
                    ribbonTabItemStar.Visible = false;
                    ribbonTabItemDigital.Visible = false;
                    ribbonTabItemCalendar.Visible = true;
                    break;
                case 18:
                    ribbonTabItemCable.Visible = false;
                    ribbonTabItemMobile.Visible = false;
                    ribbonTabItemNewspaper.Visible = false;
                    ribbonTabItemOnline.Visible = false;
                    ribbonTabItemRadio.Visible = true;
                    ribbonTabItemTV.Visible = true;
                    ribbonTabItemStar.Visible = false;
                    ribbonTabItemDigital.Visible = true;
                    ribbonTabItemCalendar.Visible = true;
                    break;
                case 30:
                    ribbonTabItemCable.Visible = false;
                    ribbonTabItemMobile.Visible = false;
                    ribbonTabItemNewspaper.Visible = false;
                    ribbonTabItemOnline.Visible = false;
                    ribbonTabItemRadio.Visible = false;
                    ribbonTabItemTV.Visible = true;
                    ribbonTabItemStar.Visible = true;
                    ribbonTabItemDigital.Visible = true;
                    ribbonTabItemCalendar.Visible = false;
                    break;
                case 31:
                    ribbonTabItemCable.Visible = false;
                    ribbonTabItemMobile.Visible = false;
                    ribbonTabItemNewspaper.Visible = false;
                    ribbonTabItemOnline.Visible = false;
                    ribbonTabItemRadio.Visible = false;
                    ribbonTabItemTV.Visible = false;
                    ribbonTabItemStar.Visible = false;
                    ribbonTabItemDigital.Visible = false;
                    ribbonTabItemCalendar.Visible = false;
                    break;
            }
        }
        #endregion

        #region GUI Event Handlers
        public void Init()
        {
            timer.Start();
            ApplyMasterWizard();
            SetDashboardCode();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            if (File.Exists(ConfigurationClasses.SettingsManager.Instance.IconPath))
                this.Icon = new Icon(ConfigurationClasses.SettingsManager.Instance.IconPath);
            buttonItemNewspaperNew.Click += new EventHandler(TabNewspaperForms.PrintScheduleBuilderControl.Instance.buttonXNewSchedule_Click);
            buttonItemNewspaperOpen.Click += new EventHandler(TabNewspaperForms.PrintScheduleBuilderControl.Instance.buttonXOpenSchedule_Click);
            buttonItemNewspaperDelete.Click += new EventHandler(TabNewspaperForms.PrintScheduleBuilderControl.Instance.buttonXDeleteSchedule_Click);
            buttonItemOnlineNew.Click += new EventHandler(TabOnlineForms.OnlineScheduleBuilderControl.Instance.buttonXNewSchedule_Click);
            buttonItemOnlineOpen.Click += new EventHandler(TabOnlineForms.OnlineScheduleBuilderControl.Instance.buttonXOpenSchedule_Click);
            buttonItemOnlineDelete.Click += new EventHandler(TabOnlineForms.OnlineScheduleBuilderControl.Instance.buttonXDeleteSchedule_Click);
            buttonItemDigitalNew.Click += new EventHandler(TabOnlineForms.OnlineScheduleBuilderControl.Instance.buttonXNewSchedule_Click);
            buttonItemDigitalOpen.Click += new EventHandler(TabOnlineForms.OnlineScheduleBuilderControl.Instance.buttonXOpenSchedule_Click);
            buttonItemDigitalDelete.Click += new EventHandler(TabOnlineForms.OnlineScheduleBuilderControl.Instance.buttonXDeleteSchedule_Click);
            buttonItemMobileNew.Click += new EventHandler(TabMobileForms.MobileScheduleBuilderControl.Instance.buttonXNewSchedule_Click);
            buttonItemMobileOpen.Click += new EventHandler(TabMobileForms.MobileScheduleBuilderControl.Instance.buttonXOpenSchedule_Click);
            buttonItemMobileDelete.Click += new EventHandler(TabMobileForms.MobileScheduleBuilderControl.Instance.buttonXDeleteSchedule_Click);
            buttonItemTVNew.Click += new EventHandler(TabTVForms.TVScheduleBuilderControl.Instance.buttonXNewSchedule_Click);
            buttonItemTVOpen.Click += new EventHandler(TabTVForms.TVScheduleBuilderControl.Instance.buttonXOpenSchedule_Click);
            buttonItemTVDelete.Click += new EventHandler(TabTVForms.TVScheduleBuilderControl.Instance.buttonXDeleteSchedule_Click);
            buttonItemCalendarNew.Click += new EventHandler(TabCalendarForms.CalendarBuilderControl.Instance.buttonXNewCalendar_Click);
            buttonItemCalendarOpen.Click += new EventHandler(TabCalendarForms.CalendarBuilderControl.Instance.buttonXOpenCalendar_Click);
            buttonItemCalendarDelete.Click += new EventHandler(TabCalendarForms.CalendarBuilderControl.Instance.buttonXDeleteCalendar_Click);
        }

        private void FormMain_Shown(object sender, EventArgs e)
        {
            AppManager.Instance.ActivatePowerPoint();
            AppManager.Instance.ActivateMainForm();
            if (AppManager.Instance.ShowCover)
            {
                buttonItemHomeCover_Click(null, null);
                AppManager.Instance.ShowCover = false;
            }
        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        public void FormAdScheduleResize(object sender, EventArgs e)
        {
            Form f = sender as Form;
            if (f.WindowState == FormWindowState.Minimized)
            {
                InteropClasses.WinAPIHelper.PostMessage(ConfigurationClasses.RegistryHelper.MinibarHandle, InteropClasses.WinAPIHelper.WM_APP + 2, 0, 0);
                f.Opacity = 0;
                AppManager.Instance.ActivateMiniBar();
            }
            else
            {
                f.Opacity = 1;
                InteropClasses.WinAPIHelper.PostMessage(ConfigurationClasses.RegistryHelper.MinibarHandle, InteropClasses.WinAPIHelper.WM_APP + 1, 0, 0);
            }
        }

        public void FormMobileScheduleResize(object sender, EventArgs e)
        {
            Form f = sender as Form;
            if (f.WindowState == FormWindowState.Minimized)
            {
                InteropClasses.WinAPIHelper.PostMessage(ConfigurationClasses.RegistryHelper.MinibarHandle, InteropClasses.WinAPIHelper.WM_APP + 4, 0, 0);
                f.Opacity = 0;
                AppManager.Instance.ActivateMiniBar();
            }
            else
            {
                f.Opacity = 1;
                InteropClasses.WinAPIHelper.PostMessage(ConfigurationClasses.RegistryHelper.MinibarHandle, InteropClasses.WinAPIHelper.WM_APP + 3, 0, 0);
            }
        }

        public void FormOnlineScheduleResize(object sender, EventArgs e)
        {
            Form f = sender as Form;
            if (f.WindowState == FormWindowState.Minimized)
            {
                InteropClasses.WinAPIHelper.PostMessage(ConfigurationClasses.RegistryHelper.MinibarHandle, InteropClasses.WinAPIHelper.WM_APP + 6, 0, 0);
                f.Opacity = 0;
                AppManager.Instance.ActivateMiniBar();
            }
            else
            {
                f.Opacity = 1;
                InteropClasses.WinAPIHelper.PostMessage(ConfigurationClasses.RegistryHelper.MinibarHandle, InteropClasses.WinAPIHelper.WM_APP + 5, 0, 0);
            }
        }

        public void FormTVScheduleResize(object sender, EventArgs e)
        {
            Form f = sender as Form;
            if (f.WindowState == FormWindowState.Minimized)
            {
                InteropClasses.WinAPIHelper.PostMessage(ConfigurationClasses.RegistryHelper.MinibarHandle, InteropClasses.WinAPIHelper.WM_APP + 8, 0, 0);
                f.Opacity = 0;
                AppManager.Instance.ActivateMiniBar();
            }
            else
            {
                f.Opacity = 1;
                InteropClasses.WinAPIHelper.PostMessage(ConfigurationClasses.RegistryHelper.MinibarHandle, InteropClasses.WinAPIHelper.WM_APP + 7, 0, 0);
            }
        }

        public void FormCalendarResize(object sender, EventArgs e)
        {
            Form f = sender as Form;
            if (f.WindowState == FormWindowState.Minimized)
            {
                InteropClasses.WinAPIHelper.PostMessage(ConfigurationClasses.RegistryHelper.MinibarHandle, InteropClasses.WinAPIHelper.WM_APP + 10, 0, 0);
                f.Opacity = 0;
                AppManager.Instance.ActivateMiniBar();
            }
            else
            {
                f.Opacity = 1;
                InteropClasses.WinAPIHelper.PostMessage(ConfigurationClasses.RegistryHelper.MinibarHandle, InteropClasses.WinAPIHelper.WM_APP + 9, 0, 0);
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (!InteropClasses.PowerPointHelper.Instance.IsActive)
            {
                InteropClasses.WinAPIHelper.PostMessage(ConfigurationClasses.RegistryHelper.MinibarHandle, InteropClasses.WinAPIHelper.WM_APP + 2, 0, 0);
                InteropClasses.WinAPIHelper.PostMessage(ConfigurationClasses.RegistryHelper.MinibarHandle, InteropClasses.WinAPIHelper.WM_APP + 4, 0, 0);
                InteropClasses.WinAPIHelper.PostMessage(ConfigurationClasses.RegistryHelper.MinibarHandle, InteropClasses.WinAPIHelper.WM_APP + 6, 0, 0);
                InteropClasses.WinAPIHelper.PostMessage(ConfigurationClasses.RegistryHelper.MinibarHandle, InteropClasses.WinAPIHelper.WM_APP + 8, 0, 0);
                InteropClasses.WinAPIHelper.PostMessage(ConfigurationClasses.RegistryHelper.MinibarHandle, InteropClasses.WinAPIHelper.WM_APP + 10, 0, 0);
                AppManager.Instance.ActivateMiniBar();
                Environment.Exit(-1);
            }
        }

        private void ribbonControl_SelectedRibbonTabChanged(object sender, EventArgs e)
        {
            Control parent = panelExMainInternal.Parent;
            panelExMainInternal.Parent = null;
            panelExMainInternal.Controls.Clear();
            this.OutsideClick = null;
            if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemHome)
            {
                TabHomeForms.TabHomeMainPage.Instance.UpdatePageAccordingToggledButton();
                panelExMainInternal.Controls.Add(TabHomeForms.TabHomeMainPage.Instance);
            }
            else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemOnline)
            {
                TabOnlineForms.TabOnlineMainPage.Instance.UpdatePageAccordingToggledButton();
                panelExMainInternal.Controls.Add(TabOnlineForms.TabOnlineMainPage.Instance);
            }
            else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemMobile)
            {
                TabMobileForms.TabMobileMainPage.Instance.UpdatePageAccordingToggledButton();
                panelExMainInternal.Controls.Add(TabMobileForms.TabMobileMainPage.Instance);
            }
            else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemNewspaper)
            {
                TabNewspaperForms.TabNewspaperMainPage.Instance.UpdatePageAccordingToggledButton();
                panelExMainInternal.Controls.Add(TabNewspaperForms.TabNewspaperMainPage.Instance);
            }
            else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemTV)
            {
                TabTVForms.TabTVMainPage.Instance.UpdatePageAccordingToggledButton();
                panelExMainInternal.Controls.Add(TabTVForms.TabTVMainPage.Instance);
            }
            else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemRadio)
            {
                TabRadioForms.TabRadioMainPage.Instance.UpdatePageAccordingToggledButton();
                panelExMainInternal.Controls.Add(TabRadioForms.TabRadioMainPage.Instance);
            }
            else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemCable)
            {
                TabCableForms.TabCableMainPage.Instance.UpdatePageAccordingToggledButton();
                panelExMainInternal.Controls.Add(TabCableForms.TabCableMainPage.Instance);
            }
            else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemDigital)
            {
                TabOnlineForms.TabOnlineMainPage.Instance.UpdatePageAccordingToggledButton();
                panelExMainInternal.Controls.Add(TabOnlineForms.TabOnlineMainPage.Instance);
            }
            else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemCalendar)
            {
                TabCalendarForms.TabCalendarMainPage.Instance.UpdatePageAccordingToggledButton();
                panelExMainInternal.Controls.Add(TabCalendarForms.TabCalendarMainPage.Instance);
            }
            else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemIPad)
            {
                TabiPadForms.TabiPadMainPage.Instance.UpdatePageAccordingToggledButton();
                panelExMainInternal.Controls.Add(TabiPadForms.TabiPadMainPage.Instance);
            }
            panelExMainInternal.Parent = parent;
        }

        private void labelItemLogo_Click(object sender, EventArgs e)
        {
            ribbonTabItemHome.Select();
            buttonItemHomeOverview_Click(null, null);
        }

        private void Outside_Click(object sender, EventArgs e)
        {
            if (OutsideClick != null)
                OutsideClick();
        }
        #endregion

        #region Ribbon Buttons's Clicks Event Handlers

        public void buttonItemExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonItemPowerPoint_Click(object sender, EventArgs e)
        {
            if (this.OutputClick != null)
                OutputClick();
        }

        private void buttonItemHelp_Click(object sender, EventArgs e)
        {
            string helpKey = string.Empty;
            if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemHome)
            {
                if (buttonItemHomeOverview.Checked)
                    helpKey = "Home";
                else if (buttonItemHomeCover.Checked)
                    helpKey = "Cover";
                else if (buttonItemLeadoffStatement.Checked)
                    helpKey = "Intro";
                else if (buttonItemClientGoals.Checked)
                    helpKey = "Needs";
                else if (buttonItemTargetCustomers.Checked)
                    helpKey = "Target";
                else if (buttonItemSimpleSummary.Checked)
                    helpKey = "Closing";
            }
            else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemOnline)
                helpKey = "Online";
            else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemMobile)
                helpKey = "Mobile";
            else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemNewspaper)
                helpKey = "Newspaper";
            else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemTV)
                helpKey = "TV";
            else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemRadio)
                helpKey = "Radio";
            else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemCable)
                helpKey = "Cable";
            else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemDigital)
                helpKey = "Digital";
            else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemCalendar)
                helpKey = "Calendar";
            else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemIPad)
                helpKey = "IPad";

            BusinessClasses.HelpManager.Instance.OpenHelpLink(helpKey);
        }

        #region Home Tab
        private void UncheckHomeButtons()
        {
            buttonItemHomeOverview.Checked = false;
            buttonItemHomeCover.Checked = false;
            buttonItemLeadoffStatement.Checked = false;
            buttonItemClientGoals.Checked = false;
            buttonItemTargetCustomers.Checked = false;
            buttonItemSimpleSummary.Checked = false;
            ribbonBarPowerPoint.Enabled = false;
        }

        private void buttonItemHomeOverview_Click(object sender, EventArgs e)
        {
            UncheckHomeButtons();
            buttonItemHomeOverview.Checked = true;
            TabHomeForms.TabHomeMainPage.Instance.UpdatePageAccordingToggledButton();
        }

        private void buttonItemHomeAdSchedule_Click(object sender, EventArgs e)
        {
            UncheckHomeButtons();
            TabHomeForms.TabHomeMainPage.Instance.UpdatePageAccordingToggledButton();
        }

        private void buttonItemHomeCleanslate_Click(object sender, EventArgs e)
        {
            InteropClasses.PowerPointHelper.Instance.AppendCleanslate();
        }

        public void buttonItemHomeCover_Click(object sender, EventArgs e)
        {
            if (buttonItemHomeCover.Checked)
                UncheckHomeButtons();
            else
            {
                UncheckHomeButtons();
                buttonItemHomeCover.Checked = true;
            }
            TabHomeForms.TabHomeMainPage.Instance.UpdatePageAccordingToggledButton();
        }

        private void buttonItemLeadoffStatement_Click(object sender, EventArgs e)
        {
            UncheckHomeButtons();
            buttonItemLeadoffStatement.Checked = true;
            TabHomeForms.TabHomeMainPage.Instance.UpdatePageAccordingToggledButton();
        }

        private void buttonItemClientGoals_Click(object sender, EventArgs e)
        {
            UncheckHomeButtons();
            buttonItemClientGoals.Checked = true;
            TabHomeForms.TabHomeMainPage.Instance.UpdatePageAccordingToggledButton();
        }

        private void buttonItemTargetCustomers_Click(object sender, EventArgs e)
        {
            UncheckHomeButtons();
            buttonItemTargetCustomers.Checked = true;
            TabHomeForms.TabHomeMainPage.Instance.UpdatePageAccordingToggledButton();
        }

        public void buttonItemSimpleSummary_Click(object sender, EventArgs e)
        {
            UncheckHomeButtons();
            buttonItemSimpleSummary.Checked = true;
            TabHomeForms.TabHomeMainPage.Instance.UpdatePageAccordingToggledButton();
        }
        #endregion

        #region iPad Tab
        #endregion
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
    }
}
