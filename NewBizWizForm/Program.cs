using System;
using System.Windows.Forms;

namespace NewBizWizForm
{
    static class Program
    {
        private static System.Threading.Mutex mutex; //Used to determine if the application is already open
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            bool firstInstance;
            string uniqueIdentifier = "Local\\NewBizWizApplication";
            mutex = new System.Threading.Mutex(false, uniqueIdentifier, out firstInstance);
            bool firstRun = false;
            ConfigurationClasses.SettingsManager.Instance.LoadSharedSettings();
            ConfigurationClasses.SettingsManager.Instance.CheckStaticFolders(out firstRun);
            if (firstRun)
            {
                AppManager.Instance.ShowWarning("Dashboard Unavailable: You do not have any Files....");
                return;
            }
            if (firstInstance)
            {
                ConfigurationClasses.RegistryHelper.MaximizeMainForm = false;
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
                System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek = DayOfWeek.Monday;
                System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = @"MM/dd/yyyy";
                System.Threading.Thread.CurrentThread.CurrentUICulture = System.Threading.Thread.CurrentThread.CurrentCulture;
                if (args != null && args.Length > 0)
                    AppManager.Instance.ShowCover = args[0].ToLower().Equals("showcover");

                if (AppManager.Instance.RunPowerPoint())
                    AppManager.Instance.RunForm();

            }
            else
            {
                if (AppManager.Instance.RunPowerPoint())
                {
                    AppManager.Instance.ActivatePowerPoint();
                    AppManager.Instance.ActivateMiniBar();
                    AppManager.Instance.ActivateMainForm();
                }
            }
        }
    }
}
