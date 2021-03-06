﻿using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Asa.Browser.Controls.ToolForms;
using Asa.Common.Core.OfficeInterops;

namespace Asa.Browser.Single.InteropClasses
{
    class BrowserPowerPointSingleton : PowerPointSingletonProcessor
    {
        private const string LauncherTemplatesFolderName = "LauncherTemplates";
        private const string LauncherTemplate43FileName = "adSALESapps43.potx";
        private const string LauncherTemplate34FileName = "adSALESapps34.potx";
        private const string LauncherTemplate169FileName = "adSALESapps169.potx";

        public static BrowserPowerPointSingleton Instance { get; } = new BrowserPowerPointSingleton();

        public bool CheckPowerPointRunning(Image floaterLogo, Action afterRun)
        {
            if (Connect())
                return true;
            if (MessageBox.Show(String.Format("PowerPoint is required to run this application.{0}Do you want to go ahead and open PowerPoint?", Environment.NewLine), "Open PowerPoint", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
                return false;
            RunPowerPointLoader(floaterLogo, afterRun);
            return false;
        }

        private void RunPowerPointLoader(Image floaterLogo, Action afterRun)
        {
            KillPowerPoint();

            var launcherTemplateFilePath = GetLauncherTemplatePath();

            if (!String.IsNullOrEmpty(launcherTemplateFilePath))
            {
                AppManager.Instance.ShowFloater(floaterLogo, () =>
                {
                    var process = new Process();
                    process.StartInfo.FileName = launcherTemplateFilePath;
                    process.StartInfo.UseShellExecute = true;
                    process.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
                    process.Start();
                }, afterRun);
            }
        }

        private void KillPowerPoint()
        {
            try
            {
                Process.GetProcesses().Where(p => p.ProcessName.ToUpper().Contains("POWERPNT")).ToList().ForEach(p => p.Kill());
            }
            catch
            {
            }
        }

        private string GetLauncherTemplatePath()
        {
            var launcherTemplatesFolderPath = Path.Combine(
                Path.GetDirectoryName(typeof(BrowserPowerPointSingleton).Assembly.Location),
                LauncherTemplatesFolderName);

            var launcherTemplateFilePath = String.Empty;

            using (var form = new FormSlideSize())
            {
                var result = form.ShowDialog(FormMain.Instance);
                switch (result)
                {
                    case DialogResult.Yes:
                        launcherTemplateFilePath = Path.Combine(launcherTemplatesFolderPath, LauncherTemplate169FileName);
                        break;
                    case DialogResult.No:
                        launcherTemplateFilePath = Path.Combine(launcherTemplatesFolderPath, LauncherTemplate43FileName);
                        break;
                    case DialogResult.Retry:
                        launcherTemplateFilePath = Path.Combine(launcherTemplatesFolderPath, LauncherTemplate34FileName);
                        break;
                }
            }

            return launcherTemplateFilePath;
        }
    }
}
