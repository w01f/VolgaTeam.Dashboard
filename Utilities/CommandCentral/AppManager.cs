using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace CommandCentral
{
    public class AppManager
    {
        public delegate void NoParamDelegate();
        private static AppManager _instance = new AppManager();

        private AppManager()
        {
        }

        public static AppManager Instance
        {
            get
            {
                return _instance;
            }
        }

        public void Run()
        {
            Application.Run(FormMain.Instance);
        }

        public void ActivateMainForm()
        {
            IntPtr mainFormHandle = InteropClasses.WinAPIHelper.FindWindow(null, "Command Central      ");
            InteropClasses.WinAPIHelper.ShowWindow(mainFormHandle, InteropClasses.WindowShowStyle.ShowNormal);
            uint lpdwProcessId = 0;
            InteropClasses.WinAPIHelper.AttachThreadInput(InteropClasses.WinAPIHelper.GetCurrentThreadId(), InteropClasses.WinAPIHelper.GetWindowThreadProcessId(InteropClasses.WinAPIHelper.GetForegroundWindow(), out lpdwProcessId), true);
            InteropClasses.WinAPIHelper.SetForegroundWindow(mainFormHandle);
            InteropClasses.WinAPIHelper.AttachThreadInput(InteropClasses.WinAPIHelper.GetCurrentThreadId(), InteropClasses.WinAPIHelper.GetWindowThreadProcessId(InteropClasses.WinAPIHelper.GetForegroundWindow(), out lpdwProcessId), false);
            InteropClasses.WinAPIHelper.MakeNormal(mainFormHandle);
        }

        public void ShowWarning(string text)
        {
            MessageBox.Show(text, "Command Central", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        public void ShowInformation(string text)
        {
            MessageBox.Show(text, "Command Central", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void OpenFile(string fileName)
        {
            if (File.Exists(fileName))
            {
                try
                {
                    Process.Start(fileName);
                }
                catch
                {
                    ShowWarning("Couldn't open file.");
                }
            }
            else
                ShowWarning("File is not existed.");
        }
    }
}
