using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace AdBAR
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            bool instance;
            var mutex = new Mutex(true, Application.ProductName, out instance);

            if (!instance)
            {
                // Search for instances of this application
                var c = Process.GetCurrentProcess();
                foreach (var p in Process.GetProcessesByName(c.ProcessName).Where(p => p.Id != c.Id))
                    p.Kill();
            }

            var f = new FormBar { Opacity = 0 };
            Application.Run(f);
            GC.KeepAlive(mutex); 
        }
    }
}
