using System;
using System.Linq;
using System.IO;
using System.Diagnostics;

namespace MiniBarLoader
{
    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            ConfigurationClasses.SettingsManager.Instance.LoadMinibarSettings();
            if (ConfigurationClasses.SettingsManager.Instance.AutoRunNormal || ConfigurationClasses.SettingsManager.Instance.AutoRunHidden || ConfigurationClasses.SettingsManager.Instance.AutoRunFloat)
                if (File.Exists(ConfigurationClasses.SettingsManager.Instance.MinibarPath))
                {
                    try
                    {
                        Process[] processes = Process.GetProcesses();
                        foreach (Process process in processes.Where(x => x.ProcessName.ToLower().Contains("minibar") && !x.ProcessName.ToLower().Contains("loader")))
                            process.Kill();
                    }
                    catch
                    {
                    }

                    bool useOptions = true;
                    if (args != null && args.Length > 0)
                        useOptions = !args[0].ToLower().Equals("-f");

                    Process minibarProcess = new Process();
                    if (ConfigurationClasses.SettingsManager.Instance.AutoRunHidden && useOptions)
                        minibarProcess.StartInfo.Arguments = "-h";
                    minibarProcess.StartInfo.FileName = ConfigurationClasses.SettingsManager.Instance.MinibarPath;
                    minibarProcess.Start();
                }
        }
    }
}
