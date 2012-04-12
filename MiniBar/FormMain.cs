using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MiniBar
{
    public partial class FormMain : Form
    {
        private static object _locker = new object();
        private static FormMain _instance;
        private Timer _hideTimer = null;

        public bool RibbonVisible { get; set; }

        public static FormMain Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new FormMain();
                return _instance;
            }
        }

        private FormMain()
        {
            InitializeComponent();

            this.RibbonVisible = true;

            ConfigurationClasses.RegistryHelper.MinibarHandle = this.Handle;

            ribbonControl.Height = 24;

            _hideTimer = new Timer();
            _hideTimer.Interval = 30;
            _hideTimer.Tick += new EventHandler(_hideTimer_Tick);
            _hideTimer.Start();

            if ((base.CreateGraphics()).DpiX > 96)
            {
                ribbonControl.Font = new Font(ribbonControl.Font.FontFamily, ribbonControl.Font.Size - 1, ribbonControl.Font.Style);
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
        }

        #region Form Event Handlers
        private void FormMain_Load(object sender, EventArgs e)
        {
            Init();
        }
        #endregion

        #region Ribbon Event Handlers
        private void ribbonTabItem_Click(object sender, EventArgs e)
        {
            ActivateExpandedForm();
            if (ribbonControl.SelectedRibbonTabItem != null)
            {
                if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemApps)
                    FormMainExpanded.Instance.ribbonTabItemApps.Select();
                else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemClipart)
                    FormMainExpanded.Instance.ribbonTabItemClipart.Select();
                else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemDashboard)
                    FormMainExpanded.Instance.ribbonTabItemDashboard.Select();
                else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemPDF)
                    FormMainExpanded.Instance.ribbonTabItemPDF.Select();
                else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemPowerPoint)
                    FormMainExpanded.Instance.ribbonTabItemPowerPoint.Select();
                else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemSalesDepot)
                    FormMainExpanded.Instance.ribbonTabItemSalesDepot.Select();
                else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemSettings)
                    FormMainExpanded.Instance.ribbonTabItemSettings.Select();
                else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemSync)
                    FormMainExpanded.Instance.ribbonTabItemSync.Select();
                else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemTools)
                    FormMainExpanded.Instance.ribbonTabItemTools.Select();
                else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemTraining)
                    FormMainExpanded.Instance.ribbonTabItemTraining.Select();
                ribbonControl.SelectedRibbonTabItem.Checked = false;
            }
        }
        #endregion

        #region Timer Ticks
        void ExpandRibbonTimer_Tick(object sender, EventArgs e)
        {
            if (ribbonControl.Height < (130 - 10))
            {
                ribbonControl.Height = ConfigurationClasses.SettingsManager.Instance.QuickRetraction ? 130 : (ribbonControl.Height + 10);
                Application.DoEvents();
                return;
            }
            ribbonControl.Height = 130;
            Application.DoEvents();
            //_expanded = true;
            ((Timer)sender).Enabled = false;
            ((Timer)sender).Dispose();
        }

        void RetractRibbonTimer_Tick(object sender, EventArgs e)
        {
            //_expanded = false;
            if (ribbonControl.Height > (24 + 10))
            {
                ribbonControl.Height = ConfigurationClasses.SettingsManager.Instance.QuickRetraction ? 24 : (ribbonControl.Height - 10);
                Application.DoEvents();
                return;
            }
            ribbonControl.Height = 24;
            Application.DoEvents();
            if (ribbonControl.SelectedRibbonTabItem != null)
                ribbonControl.SelectedRibbonTabItem.Checked = false;
            ((Timer)sender).Enabled = false;
            ((Timer)sender).Dispose();
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
            ((Timer)sender).Enabled = false;
            ((Timer)sender).Dispose();
        }

        void FadeOutTimer_Tick(object sender, EventArgs e)
        {
            if (this.Opacity > 0 && !ConfigurationClasses.SettingsManager.Instance.QuickRetraction)
            {
                this.Opacity -= 1;
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

                bool visible = this.RibbonVisible;

                visible = visible & !ConfigurationClasses.RegistryHelper.ShowHidden;
                visible = visible & !ConfigurationClasses.RegistryHelper.ShowFloat;
                visible = visible & !ConfigurationClasses.SettingsManager.Instance.VisiblePowerPoint;
                visible = visible & !ConfigurationClasses.SettingsManager.Instance.VisiblePowerPointMaximaized;

                if ((ConfigurationClasses.SettingsManager.Instance.HideAll || ConfigurationClasses.SettingsManager.Instance.HideSpecificProgram || ConfigurationClasses.SettingsManager.Instance.HideSpecificProgramMaximized) && visible)
                {
                    try
                    {
                        Process activeProcess = AppManager.Instance.GetActiveProcess();
                        visible = !activeProcess.MainWindowTitle.ToUpper().Contains(@"\\REMOTE") && !activeProcess.MainWindowTitle.ToUpper().Contains("POWERPOINT SLIDE SHOW");
                        if (visible)
                        {
                            if (ConfigurationClasses.SettingsManager.Instance.HideAll)
                                visible = !AppManager.Instance.IsProcessWindowMaximized(activeProcess) || activeProcess.ProcessName.ToLower().Contains("powerpnt");
                            else if (ConfigurationClasses.SettingsManager.Instance.HideSpecificProgram || ConfigurationClasses.SettingsManager.Instance.HideSpecificProgramMaximized)
                            {
                                visible = !(ConfigurationClasses.SettingsManager.Instance.PrimaryApplications.Where(x => activeProcess.ProcessName.ToUpper().Contains(x.ToUpper()) && !activeProcess.ProcessName.ToLower().Contains("powerpnt")).Count() > 0);
                                if (ConfigurationClasses.SettingsManager.Instance.HideSpecificProgramMaximized && !visible)
                                    visible = !AppManager.Instance.IsProcessWindowMaximized(activeProcess);
                            }
                        }
                    }
                    catch
                    {
                    }
                }
                if (!visible && !ConfigurationClasses.RegistryHelper.ShowFloat && (ConfigurationClasses.SettingsManager.Instance.VisiblePowerPoint || ConfigurationClasses.SettingsManager.Instance.VisiblePowerPointMaximaized))
                {
                    try
                    {
                        Process activeProcess = AppManager.Instance.GetActiveProcess();
                        if (activeProcess.ProcessName.ToLower().Contains("powerpnt"))
                            if (!activeProcess.MainWindowTitle.ToUpper().Contains("POWERPOINT SLIDE SHOW"))
                                visible = ConfigurationClasses.SettingsManager.Instance.VisiblePowerPointMaximaized ? AppManager.Instance.IsProcessWindowMaximized(activeProcess) || activeProcess.ProcessName.ToLower().Contains("minibar") : ConfigurationClasses.SettingsManager.Instance.VisiblePowerPoint;
                        if (!visible && activeProcess.ProcessName.ToLower().Contains("minibar"))
                        {
                            Process process = Process.GetProcesses().Where(x => x.ProcessName.ToLower().Contains("powerpnt")).FirstOrDefault();
                            if (process != null)
                                if (!process.MainWindowTitle.ToUpper().Contains("POWERPOINT SLIDE SHOW"))
                                    visible = ConfigurationClasses.SettingsManager.Instance.VisiblePowerPointMaximaized ? AppManager.Instance.IsProcessWindowMaximized(process) : !InteropClasses.WinAPIHelper.IsIconic(process.MainWindowHandle);

                        }
                    }
                    catch
                    {
                    }

                }
                ribbonControl.Visible = visible;
            }
        }
        #endregion

        #region Common Methods
        protected override void WndProc(ref Message m)
        {
            lock (AppManager.Locker)
            {
                switch (m.Msg)
                {
                    case (int)(InteropClasses.WinAPIHelper.WM_APP + 1):
                        lock (_locker)
                            this.RibbonVisible = false;
                        break;
                    case (int)(InteropClasses.WinAPIHelper.WM_APP + 2):
                        lock (_locker)
                            this.RibbonVisible = true;
                        break;
                    case (int)(InteropClasses.WinAPIHelper.WM_APP + 3):
                        lock (_locker)
                            this.RibbonVisible = false;
                        break;
                    case (int)(InteropClasses.WinAPIHelper.WM_APP + 4):
                        lock (_locker)
                            this.RibbonVisible = true;
                        break;
                    case (int)(InteropClasses.WinAPIHelper.WM_APP + 5):
                        lock (_locker)
                            this.RibbonVisible = false;
                        break;
                    case (int)(InteropClasses.WinAPIHelper.WM_APP + 6):
                        lock (_locker)
                            this.RibbonVisible = true;
                        break;
                    case (int)(InteropClasses.WinAPIHelper.WM_APP + 7):
                        lock (_locker)
                            this.RibbonVisible = false;
                        break;
                    case (int)(InteropClasses.WinAPIHelper.WM_APP + 8):
                        lock (_locker)
                            this.RibbonVisible = true;
                        break;
                    case (int)(InteropClasses.WinAPIHelper.WM_APP + 9):
                        lock (_locker)
                            this.RibbonVisible = false;
                        break;
                    case (int)(InteropClasses.WinAPIHelper.WM_APP + 10):
                        lock (_locker)
                            this.RibbonVisible = true;
                        break;
                    default:
                        base.WndProc(ref m);
                        break;
                }
            }
        }

        public void Init()
        {
            if (ribbonControl.SelectedRibbonTabItem != null)
                ribbonControl.SelectedRibbonTabItem.Checked = false;

            ribbonTabItemDashboard.Text = ConfigurationClasses.SettingsManager.Instance.DashboardName;
            ribbonTabItemSalesDepot.Text = ConfigurationClasses.SettingsManager.Instance.SalesDepotName;

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

            FormMainExpanded.Instance.Init();
            FormMainExpanded.Instance.Show();
        }

        private void ActivateExpandedForm()
        {
            FadeOut();
            FormMainExpanded.Instance.FadeIn();
            FormMainExpanded.Instance.Activate();
        }

        public void FadeIn()
        {
            Timer timer = new Timer();
            timer.Interval = 30;
            timer.Tick += new EventHandler(FadeInTimer_Tick);
            timer.Start();
        }

        public void FadeOut()
        {
            Timer timer = new Timer();
            timer.Interval = 30;
            timer.Tick += new EventHandler(FadeOutTimer_Tick);
            timer.Start();
        }
        #endregion
    }
}
