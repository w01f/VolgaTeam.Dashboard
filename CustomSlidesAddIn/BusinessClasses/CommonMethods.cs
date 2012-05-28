using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CustomSlidesAddIn.BusinessClasses
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

        public static void KillMinibar()
        {
            InteropClasses.WinAPIHelper.PostMessage(ConfigurationClasses.RegistryHelper.MinibarHandle, InteropClasses.WinAPIHelper.WM_CLOSE, 0, 0);
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
            MessageBox.Show(text, ConfigurationClasses.SettingsManager.Instance.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        public static DialogResult ShowWarningQuestion(string text)
        {
            return MessageBox.Show(text, ConfigurationClasses.SettingsManager.Instance.ApplicationName, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
        }

        public static void ShowInformation(string text)
        {
            MessageBox.Show(text, ConfigurationClasses.SettingsManager.Instance.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static bool IsHighDPI()
        {
            Label label = new Label();
            return label.CreateGraphics().DpiX > 96;
        }
    }
}
