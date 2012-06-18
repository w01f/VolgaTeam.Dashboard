using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MiniBar
{
    public partial class FormFloater : Form
    {
        private Timer _hideTimer = null;

        public FormFloater()
        {
            InitializeComponent();

            _hideTimer = new Timer();
            _hideTimer.Interval = 30;
            _hideTimer.Tick += new EventHandler(_hideTimer_Tick);
            _hideTimer.Start();
        }

        private void buttonXMinibar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonXSync_Click(object sender, EventArgs e)
        {
            BusinessClasses.SyncManager.RegularSynchronize();
        }

        private void _hideTimer_Tick(object sender, EventArgs e)
        {
            lock (AppManager.Locker)
            {
                bool visible = FormMain.Instance.RibbonVisible;

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
                                visible = !(ConfigurationClasses.SettingsManager.Instance.PrimaryApplications.Where(x => (activeProcess.ProcessName.ToUpper().Contains(x.ToUpper()))).Count() > 0);
                                if (ConfigurationClasses.SettingsManager.Instance.HideSpecificProgramMaximized && !visible)
                                    visible = !AppManager.Instance.IsProcessWindowMaximized(activeProcess);
                            }
                        }
                    }
                    catch
                    {
                    }
                }
                if (!visible && (ConfigurationClasses.SettingsManager.Instance.VisiblePowerPoint || ConfigurationClasses.SettingsManager.Instance.VisiblePowerPointMaximaized))
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
                try
                {
                    if (visible)
                    {
                        this.Opacity = 1;
                        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
                    }
                    else
                    {
                        this.Opacity = 0;
                        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                    }
                    Size size = visible ? new System.Drawing.Size(175, 85) : new System.Drawing.Size(0, 0);
                    this.Size = size;
                }
                catch
                {
                }
            }
        }

        private void FormFloater_Shown(object sender, EventArgs e)
        {
            lock (AppManager.Locker)
            {
                ConfigurationClasses.RegistryHelper.ShowFloat = true;
            }
            ConfigurationClasses.RegistryHelper.MinibarHandle = this.Handle;
        }

        private void FormFloater_FormClosed(object sender, FormClosedEventArgs e)
        {
            ConfigurationClasses.RegistryHelper.MinibarHandle = FormMain.Instance.Handle;
            lock (AppManager.Locker)
            {
                ConfigurationClasses.RegistryHelper.ShowFloat = false;
                _hideTimer.Stop();
            }
        }

        private void FormFloater_LocationChanged(object sender, EventArgs e)
        {
            lock (AppManager.Locker)
            {
                ConfigurationClasses.SettingsManager.Instance.FloaterTop = this.Top;
                ConfigurationClasses.SettingsManager.Instance.FloaterLeft = this.Left;
                ConfigurationClasses.SettingsManager.Instance.SaveMinibarSettings();
            }
        }
    }
}
