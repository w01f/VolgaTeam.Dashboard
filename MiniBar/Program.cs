using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace MiniBar
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            var currentProcess = Process.GetCurrentProcess();

            foreach (var p in Process.GetProcessesByName(currentProcess.ProcessName))
            {
                try
                {
                    if (p.Id != currentProcess.Id)
                        p.Kill();
                }
                catch
                {
                }
            }
            AppManager.Locker = new object();
            if (args != null && args.Length > 0)
                AppManager.Instance.ShowHidden = args[0].ToLower().Equals("-h");

            lock (AppManager.Locker)
            {
                ConfigurationClasses.RegistryHelper.ShowHidden = AppManager.Instance.ShowHidden;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            AppManager.Instance.RunForm();
        }
    }
}
