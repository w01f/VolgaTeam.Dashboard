using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace AdSalesAddIn.BusinessClasses
{
    public class CommonMethods
    {
        public static bool AplicationDetected()
        {
            return Process.GetProcesses().Where(x => x.ProcessName.ToLower().Contains("minibar") || x.ProcessName.ToLower().Contains("adsalesapp") || x.ProcessName.ToLower().Contains("proslides") || x.ProcessName.ToLower().Contains("onedomain") || x.ProcessName.ToLower().Contains("salesdepot") || x.ProcessName.ToLower().Contains("medialibrary")).Count() > 0;
        }

        public static void CloseActiveApplications()
        {
            KillMinibar();
            Process[] processList = Process.GetProcesses();
            foreach (Process process in processList.Where(x => x.ProcessName.ToLower().Contains("adsalesapp") || x.ProcessName.ToLower().Contains("proslides") || x.ProcessName.ToLower().Contains("onedomain") || x.ProcessName.ToLower().Contains("salesdepot") || x.ProcessName.ToLower().Contains("medialibrary")))
                process.Kill();
        }

        public static void ReleaseComObject(object o)
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

        public static void ShowWarning(string text)
        {
            MessageBox.Show(text, "adSALESapp", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        public static DialogResult ShowWarningQuestion(string text)
        {
            return MessageBox.Show(text, "adSALESapp", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
        }

        public static void ShowInformation(string text)
        {
            MessageBox.Show(text, "adSALESapp", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #region Run Internal Apps
        public static void RunDashboard(string parameter = "")
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

        public static void RunSalesDepot()
        {
            if (File.Exists(ConfigurationClasses.SettingsManager.Instance.SalesDepotExecutablePath))
                Process.Start(ConfigurationClasses.SettingsManager.Instance.SalesDepotExecutablePath);
            else
                ShowWarning("Couldn't find Sales Depot app");
        }

        public static void RunMinibar()
        {
            if (File.Exists(ConfigurationClasses.SettingsManager.Instance.MinibarPath))
            {
                Process process = new Process();
                process.StartInfo.FileName = ConfigurationClasses.SettingsManager.Instance.MinibarPath;
                process.Start();
            }
            else
                ShowWarning("Couldn't find Minibar Loader app");
        }

        public static void KillMinibar()
        {
            InteropClasses.WinAPIHelper.PostMessage(ConfigurationClasses.RegistryHelper.MinibarHandle, InteropClasses.WinAPIHelper.WM_CLOSE, 0, 0);
        }

        public static void RunClientLogos()
        {
            if (File.Exists(ConfigurationClasses.SettingsManager.Instance.ClientLogosPath))
            {
                Process.Start(ConfigurationClasses.SettingsManager.Instance.ClientLogosPath);
            }
            else
                ShowWarning("Couldn't find Client Logos app");
        }

        public static void RunSalesGallery()
        {
            if (File.Exists(ConfigurationClasses.SettingsManager.Instance.SalesGalleryPath))
            {
                Process.Start(ConfigurationClasses.SettingsManager.Instance.SalesGalleryPath);
            }
            else
                ShowWarning("Couldn't find Sales Gallery app");
        }

        public static void RunWebArt()
        {
            if (File.Exists(ConfigurationClasses.SettingsManager.Instance.WebArtPath))
            {
                Process.Start(ConfigurationClasses.SettingsManager.Instance.WebArtPath);
            }
            else
                ShowWarning("Couldn't find Web Art app");
        }

        public static bool IsHighDPI()
        {
            Label label = new Label();
            return label.CreateGraphics().DpiX > 96;
        }

        #endregion
    }
}
