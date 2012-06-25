using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace MiniBar
{
    class AppManager
    {
        private static AppManager _instance = new AppManager();
        public delegate void SingleParamDelegate(object parameter);
        public static object Locker { get; set; }
        private Dictionary<int, IntPtr> processesAndMaximizedWindows = new Dictionary<int, IntPtr>();

        public bool ShowHidden { get; set; }
        public bool ShowFloat { get; set; }

        public static AppManager Instance
        {
            get
            {
                return _instance;
            }
        }

        private AppManager()
        {
            Microsoft.Win32.SystemEvents.SessionSwitch += new Microsoft.Win32.SessionSwitchEventHandler(SystemEvents_SessionSwitch);
        }

        private void SystemEvents_SessionSwitch(object sender, Microsoft.Win32.SessionSwitchEventArgs e)
        {
            if (e.Reason == Microsoft.Win32.SessionSwitchReason.SessionUnlock && !ConfigurationClasses.RegistryHelper.ShowFloat && !ConfigurationClasses.RegistryHelper.ShowHidden)
            {
                RunMinibarLoader();
            }
        }

        public void RunForm()
        {
            UpdateSyncFiles();
            if (LoadSettings())
            {
                InteropClasses.PowerPointHelper.Instance.Connect();
                Application.Run(FormMain.Instance);
            }
        }

        public void ActivateForm(IntPtr handle, bool makeNormal)
        {
            uint lpdwProcessId = 0;
            InteropClasses.WinAPIHelper.MakeTopMost(handle);
            InteropClasses.WinAPIHelper.AttachThreadInput(InteropClasses.WinAPIHelper.GetCurrentThreadId(), InteropClasses.WinAPIHelper.GetWindowThreadProcessId(InteropClasses.WinAPIHelper.GetForegroundWindow(), out lpdwProcessId), true);
            InteropClasses.WinAPIHelper.SetForegroundWindow(handle);
            InteropClasses.WinAPIHelper.AttachThreadInput(InteropClasses.WinAPIHelper.GetCurrentThreadId(), InteropClasses.WinAPIHelper.GetWindowThreadProcessId(InteropClasses.WinAPIHelper.GetForegroundWindow(), out lpdwProcessId), false);
            if (makeNormal)
                InteropClasses.WinAPIHelper.MakeNormal(handle);
        }

        private bool LoadSettings()
        {
            bool result = false;
            ConfigurationClasses.SettingsManager.Instance.CreateStaticFolders();
            ConfigurationClasses.SettingsManager.Instance.LoadSharedSettings();
            if (result = BusinessClasses.SyncManager.CheckSyncSetting())
            {
                ConfigurationClasses.SettingsManager.Instance.LoadMinibarSettings();
                ConfigurationClasses.SettingsManager.Instance.LoadMinibarApplicationSettings();
                ConfigurationClasses.SettingsManager.Instance.SaveMinibarSettings();
                BusinessClasses.SyncManager.SchedulerSyncInBackground();
            }
            return result;
        }

        private void UpdateSyncFiles()
        {
            System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
            {
                try
                {
                    DirectoryInfo source = new DirectoryInfo(ConfigurationClasses.SettingsManager.Instance.SyncFilesSourcePath);
                    if (source.Exists)
                    {
                        foreach (FileInfo sourceFile in source.GetFiles())
                        {
                            string destinationFilePath = Path.Combine(ConfigurationClasses.SettingsManager.Instance.SyncSettingsFolderPath, sourceFile.Name);
                            if (File.Exists(destinationFilePath))
                            {
                                while (Process.GetProcesses().Where(x => x.ProcessName.ToLower().Contains(Path.GetFileNameWithoutExtension(destinationFilePath).ToLower())).Count() > 0)
                                    System.Threading.Thread.Sleep(1000);
                                if (File.GetLastWriteTime(destinationFilePath) >= sourceFile.LastWriteTime)
                                    continue;
                                try
                                {
                                    File.SetAttributes(destinationFilePath, FileAttributes.Normal);
                                }
                                catch
                                {
                                }
                            }
                            try
                            {
                                sourceFile.CopyTo(destinationFilePath, true);
                            }
                            catch
                            {
                            }
                        }
                    }
                }
                catch
                {
                }
            }));
            thread.Start();
        }

        public void ShowWarning(string text)
        {
            MessageBox.Show(text, "adSALESapps.com", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        public DialogResult ShowWarningQuestion(string text)
        {
            return MessageBox.Show(text, "adSALESapps.com", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
        }

        public void ShowInformation(string text)
        {
            MessageBox.Show(text, "adSALESapps.com", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #region Run Internal Apps
        public void RunDashboard(string parameter = "")
        {
            if (File.Exists(ConfigurationClasses.SettingsManager.Instance.DashboardPath))
            {
                Process process = new Process();
                process.StartInfo.FileName = ConfigurationClasses.SettingsManager.Instance.DashboardPath;
                process.StartInfo.Arguments = parameter;
                process.Start();
            }
            else
                ShowWarning("Couldn't find Dashboard app");
        }

        public void RunSalesDepot()
        {
            if (File.Exists(ConfigurationClasses.SettingsManager.Instance.SalesDepotExecutablePath))
                Process.Start(ConfigurationClasses.SettingsManager.Instance.SalesDepotExecutablePath);
            else
                ShowWarning("Couldn't find Sales Depot app");
        }

        public void RunSalesDepotRemote()
        {
            if (File.Exists(ConfigurationClasses.SettingsManager.Instance.SalesDepotExecutablePath))
            {
                Process process = new Process();
                process.StartInfo.Arguments = "-remote";
                process.StartInfo.FileName = ConfigurationClasses.SettingsManager.Instance.SalesDepotExecutablePath;
                process.Start();
            }
            else
                ShowWarning("Couldn't find Remote Libraries app");
        }

        public void RunMinibarLoader()
        {
            if (File.Exists(ConfigurationClasses.SettingsManager.Instance.MinibarLoaderPath))
            {
                Process process = new Process();
                process.StartInfo.Arguments = "-f";
                process.StartInfo.FileName = ConfigurationClasses.SettingsManager.Instance.MinibarLoaderPath;
                process.Start();
            }
            else
                ShowWarning("Couldn't find Minibar Loader app");
        }

        public void RunPowerPointLoader()
        {
            if (File.Exists(ConfigurationClasses.SettingsManager.Instance.PowerPointLoaderPath))
            {
                Process process = new Process();
                process.StartInfo.FileName = ConfigurationClasses.SettingsManager.Instance.PowerPointLoaderPath;
                process.Start();

                Thread thread = new Thread(new System.Threading.ThreadStart(delegate()
                {
                    while (Process.GetProcesses().Where(x => x.ProcessName.ToLower().Contains(Path.GetFileNameWithoutExtension(ConfigurationClasses.SettingsManager.Instance.PowerPointLoaderPath).ToLower())).Count() > 0)
                        System.Threading.Thread.Sleep(1000);
                }));
                thread.Start();

                while (thread.IsAlive)
                    Application.DoEvents();
            }
            else
                ShowWarning("Couldn't find PowerPointLoader app");
        }

        public void RunClientLogos()
        {
            if (File.Exists(ConfigurationClasses.SettingsManager.Instance.ClientLogosPath))
            {
                Process.Start(ConfigurationClasses.SettingsManager.Instance.ClientLogosPath);
            }
            else
                ShowWarning("Couldn't find Client Logos app");
        }

        public void RunSalesGallery()
        {
            if (File.Exists(ConfigurationClasses.SettingsManager.Instance.SalesGalleryPath))
            {
                Process.Start(ConfigurationClasses.SettingsManager.Instance.SalesGalleryPath);
            }
            else
                ShowWarning("Couldn't find Sales Gallery app");
        }

        public void RunWebArt()
        {
            if (File.Exists(ConfigurationClasses.SettingsManager.Instance.WebArtPath))
            {
                Process.Start(ConfigurationClasses.SettingsManager.Instance.WebArtPath);
            }
            else
                ShowWarning("Couldn't find Web Art app");
        }

        public void KillPowerPoint()
        {
            foreach (Process process in Process.GetProcesses().Where(x => x.ProcessName.ToUpper().Equals("POWERPNT")))
                process.Kill();
        }

        public void KillExcel()
        {
            foreach (Process process in Process.GetProcesses().Where(x => x.ProcessName.ToUpper().Equals("EXCEL")))
                process.Kill();
        }

        public void KillFMAutoSync()
        {
            foreach (Process process in Process.GetProcesses().Where(x => x.ProcessName.ToUpper().Equals("AUTOFMSYNC")))
                process.Kill();
        }
        #endregion

        public bool AplicationDetected()
        {
            return Process.GetProcesses().Where(x => x.ProcessName.Contains("adSALESapp") || x.ProcessName.Contains("ProSlides") || x.ProcessName.Contains("OneDomain")).Count() > 0;
        }

        public void CloseActiveApplications()
        {
            Process[] processList = Process.GetProcesses();
            foreach (Process process in processList.Where(x => x.ProcessName.ToLower().Contains("adsalesapp") || x.ProcessName.ToLower().Contains("proslides") || x.ProcessName.ToLower().Contains("onedomain") || x.ProcessName.ToLower().Contains("salesdepot") || x.ProcessName.ToLower().Contains("medialibrary")))
                process.Kill();
        }

        public Process GetActiveProcess()
        {
            uint lpdwProcessId = 0;
            InteropClasses.WinAPIHelper.GetWindowThreadProcessId(InteropClasses.WinAPIHelper.GetForegroundWindow(), out lpdwProcessId);
            Process activeProcess = Process.GetProcessById((int)lpdwProcessId);
            if (activeProcess != null)
                return activeProcess;
            else
                return null;
        }

        public void WipeSoftware()
        {
            CloseActiveApplications();

            if (File.Exists(ConfigurationClasses.SettingsManager.Instance.ResetPath))
                Process.Start(ConfigurationClasses.SettingsManager.Instance.ResetPath);
        }

        public void ReleaseComObject(object o)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(o);
            }
            catch
            {
            }
            finally
            {
                o = null;
            }
        }

        public void SetClickEventHandler(Control control)
        {
            foreach (Control childControl in control.Controls)
                SetClickEventHandler(childControl);
            if (control.GetType() != typeof(DevExpress.XtraEditors.TextBoxMaskBox) && control.GetType() != typeof(DevExpress.XtraEditors.TextEdit) && control.GetType() != typeof(DevExpress.XtraEditors.MemoEdit) && control.GetType() != typeof(DevExpress.XtraEditors.ComboBoxEdit) && control.GetType() != typeof(DevExpress.XtraEditors.LookUpEdit) && control.GetType() != typeof(DevExpress.XtraEditors.DateEdit) && control.GetType() != typeof(DevExpress.XtraEditors.CheckedListBoxControl) && control.GetType() != typeof(DevExpress.XtraEditors.SpinEdit) && control.GetType() != typeof(DevExpress.XtraEditors.CheckEdit))
            {
                control.Click += new EventHandler(ControlClick);
            }
        }

        private void ControlClick(object sender, EventArgs e)
        {
            ((Control)sender).Select();
            if (((Control)sender).Parent != null)
                ((Control)sender).Parent.Select();
        }

        public bool IsProcessWindowMaximized(Process p)
        {
            bool result = false;
            IntPtr handle;
            InteropClasses.WINDOWPLACEMENT placement = new InteropClasses.WINDOWPLACEMENT();
            placement.length = Marshal.SizeOf(placement);
            if (processesAndMaximizedWindows.ContainsKey(p.Id))
            {
                handle = processesAndMaximizedWindows[p.Id];
            }
            else
            {
                processesAndMaximizedWindows.Add(p.Id, p.MainWindowHandle);
                handle = p.MainWindowHandle;
            }

            if (InteropClasses.WinAPIHelper.GetWindowPlacement(handle, out placement))
            {
                if (placement.showCmd == 3) // WM_MAXIMIZED
                {
                    processesAndMaximizedWindows[p.Id] = handle;
                    result = true;
                }
            }
            else if (processesAndMaximizedWindows[p.Id] != p.MainWindowHandle)
            {
                processesAndMaximizedWindows[p.Id] = p.MainWindowHandle;
                result = IsProcessWindowMaximized(p);
            }
            return result;
        }
    }
}
