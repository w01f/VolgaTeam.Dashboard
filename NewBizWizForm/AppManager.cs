using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace NewBizWizForm
{
    public class AppManager
    {
        private static AppManager _instance = new AppManager();
        public delegate void EmptyParametersDelegate();
        public delegate void SingleParameterDelegate(bool parameter);
        private IntPtr _mainFormHandle = IntPtr.Zero;

        public bool ShowCover { get; set; }

        private AppManager()
        {
            this.ShowCover = false;
        }

        public static AppManager Instance
        {
            get
            {
                return _instance;
            }
        }

        public static string FormCaption
        {
            get
            {
                return ConfigurationClasses.SettingsManager.Instance.DashboardName + " - " + ConfigurationClasses.SettingsManager.Instance.SelectedWizard + " - " + ConfigurationClasses.SettingsManager.Instance.Size;
            }
        }

        public void RunForm()
        {
            using (ToolForms.FormLoadSplash form = new ToolForms.FormLoadSplash())
            {
                form.TopMost = true;
                System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
                {
                    RunMinibar();
                    BusinessClasses.ListManager.Instance.Init();
                    InteropClasses.PowerPointHelper.Instance.SetPresentationSettings();
                }));
                thread.Start();

                form.Show();

                while (thread.IsAlive)
                    System.Windows.Forms.Application.DoEvents();
                form.Close();
            }
            FormMain.Instance.Init();
            _mainFormHandle = FormMain.Instance.Handle;
            ConfigurationClasses.RegistryHelper.MainFormHandle = FormMain.Instance.Handle;
            Application.Run(FormMain.Instance);
        }

        public bool RunPowerPoint()
        {
            return InteropClasses.PowerPointHelper.Instance.Connect();
        }

        public void RunMinibar()
        {
            Process[] processes = Process.GetProcesses();
            if (processes.Where(x => x.ProcessName.ToLower().Contains("minibar")).Count() == 0)
                if (System.IO.File.Exists(ConfigurationClasses.SettingsManager.Instance.MinibarApplicationPath))
                    Process.Start(ConfigurationClasses.SettingsManager.Instance.MinibarApplicationPath);
        }

        public void RunOneDomain()
        {
            Process[] processes = Process.GetProcesses();
            if (processes.Where(x => x.ProcessName.ToLower().Contains("onedomain")).Count() == 0)
                if (System.IO.File.Exists(ConfigurationClasses.SettingsManager.Instance.OneDomainApplicationPath))
                    Process.Start(ConfigurationClasses.SettingsManager.Instance.OneDomainApplicationPath);
        }

        public void RunSalesDepot()
        {
            Process[] processes = Process.GetProcesses();
            if (processes.Where(x => x.ProcessName.ToLower().Contains("salesdepot")).Count() == 0)
                if (System.IO.File.Exists(ConfigurationClasses.SettingsManager.Instance.SalesDepotApplicationPath))
                    Process.Start(ConfigurationClasses.SettingsManager.Instance.SalesDepotApplicationPath);
        }


        public void ActivateMainForm()
        {
            if (_mainFormHandle.ToInt32() == 0)
                _mainFormHandle = ConfigurationClasses.RegistryHelper.MainFormHandle;
            if (_mainFormHandle.ToInt32() == 0)
            {
                Process[] processList = Process.GetProcesses();
                foreach (Process process in processList.Where(x => x.ProcessName.Contains("adSALESapp")))
                {
                    if (process.MainWindowHandle.ToInt32() != 0)
                    {
                        _mainFormHandle = process.MainWindowHandle;
                        break;
                    }
                }
            }
            if (_mainFormHandle.ToInt32() != 0)
            {
                InteropClasses.WinAPIHelper.ShowWindow(_mainFormHandle, ConfigurationClasses.RegistryHelper.MaximizeMainForm? InteropClasses.WindowShowStyle.ShowMaximized:InteropClasses.WindowShowStyle.ShowNormal);
                InteropClasses.WinAPIHelper.MakeTopMost(_mainFormHandle);
                InteropClasses.WinAPIHelper.MakeNormal(_mainFormHandle);
                uint lpdwProcessId = 0;
                InteropClasses.WinAPIHelper.AttachThreadInput(InteropClasses.WinAPIHelper.GetCurrentThreadId(), InteropClasses.WinAPIHelper.GetWindowThreadProcessId(InteropClasses.WinAPIHelper.GetForegroundWindow(), out lpdwProcessId), true);
                InteropClasses.WinAPIHelper.SetForegroundWindow(_mainFormHandle);
                InteropClasses.WinAPIHelper.AttachThreadInput(InteropClasses.WinAPIHelper.GetCurrentThreadId(), InteropClasses.WinAPIHelper.GetWindowThreadProcessId(InteropClasses.WinAPIHelper.GetForegroundWindow(), out lpdwProcessId), false);
            }
        }

        public void ActivatePowerPoint()
        {
            if (InteropClasses.PowerPointHelper.Instance.PowerPointObject != null)
            {
                IntPtr powerPointHandle = new IntPtr(InteropClasses.PowerPointHelper.Instance.PowerPointObject.HWND);
                InteropClasses.WinAPIHelper.ShowWindow(powerPointHandle, InteropClasses.WindowShowStyle.ShowMaximized);
                uint lpdwProcessId = 0;
                InteropClasses.WinAPIHelper.AttachThreadInput(InteropClasses.WinAPIHelper.GetCurrentThreadId(), InteropClasses.WinAPIHelper.GetWindowThreadProcessId(InteropClasses.WinAPIHelper.GetForegroundWindow(), out lpdwProcessId), true);
                InteropClasses.WinAPIHelper.SetForegroundWindow(powerPointHandle);
                InteropClasses.WinAPIHelper.AttachThreadInput(InteropClasses.WinAPIHelper.GetCurrentThreadId(), InteropClasses.WinAPIHelper.GetWindowThreadProcessId(InteropClasses.WinAPIHelper.GetForegroundWindow(), out lpdwProcessId), false);
            }
        }

        public void ActivateMiniBar()
        {
            IntPtr minibarHandle = ConfigurationClasses.RegistryHelper.MinibarHandle;
            if (minibarHandle.ToInt32() == 0)
            {
                Process[] processList = Process.GetProcesses();
                foreach (Process process in processList.Where(x => x.ProcessName.Contains("MiniBar")))
                {
                    if (process.MainWindowHandle.ToInt32() != 0)
                    {
                        minibarHandle = process.MainWindowHandle;
                        break;
                    }
                }
            }
            if (minibarHandle.ToInt32() != 0)
            {
                uint lpdwProcessId = 0;
                InteropClasses.WinAPIHelper.MakeTopMost(minibarHandle);
                InteropClasses.WinAPIHelper.AttachThreadInput(InteropClasses.WinAPIHelper.GetCurrentThreadId(), InteropClasses.WinAPIHelper.GetWindowThreadProcessId(InteropClasses.WinAPIHelper.GetForegroundWindow(), out lpdwProcessId), true);
                InteropClasses.WinAPIHelper.SetForegroundWindow(minibarHandle);
                InteropClasses.WinAPIHelper.AttachThreadInput(InteropClasses.WinAPIHelper.GetCurrentThreadId(), InteropClasses.WinAPIHelper.GetWindowThreadProcessId(InteropClasses.WinAPIHelper.GetForegroundWindow(), out lpdwProcessId), false);
            }
        }

        public static void SetAutoScrollPosition(ScrollableControl sender, System.Drawing.Point p)
        {
            p.X = System.Math.Abs(p.X);
            p.Y = System.Math.Abs(p.Y);
            sender.AutoScrollPosition = p;
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

        public void ShowWarning(string text)
        {
            MessageBox.Show(text, ConfigurationClasses.SettingsManager.Instance.DashboardName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        public DialogResult ShowWarningQuestion(string text)
        {
            return MessageBox.Show(text, ConfigurationClasses.SettingsManager.Instance.DashboardName, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
        }

        public void ShowInformation(string text)
        {
            MessageBox.Show(text, ConfigurationClasses.SettingsManager.Instance.DashboardName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
