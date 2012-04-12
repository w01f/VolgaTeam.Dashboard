using System;
using System.Windows.Forms;

namespace Reset
{
    class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            AppManager.Instance.RunForm();
        }
    }
}
