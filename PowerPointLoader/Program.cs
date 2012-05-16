using System;
using System.Linq;
using System.IO;
using System.Diagnostics;

namespace PowerPointLoader
{
    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            ConfigurationClasses.SettingsManager.Instance.LoadSharedSettings();
            InteropClasses.PowerPointHelper.Instance.Connect();
        }
    }
}
