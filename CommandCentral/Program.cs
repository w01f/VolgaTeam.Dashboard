using System;
using System.Windows.Forms;

namespace CommandCentral
{
    static class Program
    {
        private static System.Threading.Mutex mutex; //Used to determine if the application is already open

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool firstInstance;
            string uniqueIdentifier = "Local\\CommandCentralApplication";
            mutex = new System.Threading.Mutex(false, uniqueIdentifier, out firstInstance);
            if (firstInstance)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek = DayOfWeek.Monday;
                System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = @"MM/dd/yyyy";
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
                AppManager.Instance.Run();
            }
            else
            {
                AppManager.Instance.ActivateMainForm();
            }
        }
    }
}
