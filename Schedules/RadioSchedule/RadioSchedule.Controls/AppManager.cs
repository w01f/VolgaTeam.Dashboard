using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace RadioScheduleBuilder
{
    public class AppManager
    {
        public static void NewSchedule()
        {
            using (ToolForms.FormNewSchedule from = new ToolForms.FormNewSchedule())
            {
                if (from.ShowDialog() == DialogResult.OK)
                {
                    if (InteropClasses.PowerPointHelper.Instance.Connect())
                    {
                        if (!string.IsNullOrEmpty(from.ScheduleName))
                        {
                            ConfigurationClasses.RegistryHelper.MainFormHandle = FormMain.Instance.Handle;
                            SetCulture();
                            string fileName = from.ScheduleName.Trim();
                            BusinessClasses.SuccessModelsManager.Instance.Load();
                            BusinessClasses.ScheduleManager.Instance.OpenSchedule(fileName, true);
                            FormMain.Instance.ShowDialog();
                            InteropClasses.PowerPointHelper.Instance.Disconnect();
                            RestoreCulture();
                            RemoveInstances();
                        }
                        else
                        {
                            AppManager.ShowWarning("Schedule Name can't be empty");
                        }
                    }
                }
            }
        }

        public static void OpenSchedule()
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.InitialDirectory = ConfigurationClasses.SettingsManager.Instance.SaveFolder;
                dialog.Title = "Select Schedule File";
                dialog.Filter = "Schedule Files|*.xml";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    if (InteropClasses.PowerPointHelper.Instance.Connect())
                    {
                        ConfigurationClasses.RegistryHelper.MainFormHandle = FormMain.Instance.Handle;
                        SetCulture();
                        string fileName = dialog.FileName;
                        ConfigurationClasses.SettingsManager.Instance.SaveFolder = new FileInfo(fileName).Directory.FullName;
                        BusinessClasses.ScheduleManager.Instance.OpenSchedule(fileName);
                        FormMain.Instance.ShowDialog();
                        InteropClasses.PowerPointHelper.Instance.Disconnect();
                        RestoreCulture();
                        RemoveInstances();
                    }
                }
            }
        }

        public static void OpenSchedule(string fileName)
        {
            if (InteropClasses.PowerPointHelper.Instance.Connect())
            {
                ConfigurationClasses.RegistryHelper.MainFormHandle = FormMain.Instance.Handle;
                SetCulture();
                BusinessClasses.SuccessModelsManager.Instance.Load();
                BusinessClasses.ScheduleManager.Instance.OpenSchedule(fileName);
                FormMain.Instance.ShowDialog();
                InteropClasses.PowerPointHelper.Instance.Disconnect();
                RestoreCulture();
                RemoveInstances();
            }
        }

        public static BusinessClasses.ShortSchedule[] GetShortScheduleList()
        {
            DirectoryInfo saveFolder = new DirectoryInfo(ConfigurationClasses.SettingsManager.Instance.SaveFolder);
            if (saveFolder.Exists)
                return BusinessClasses.ScheduleManager.Instance.GetShortScheduleList(saveFolder);
            else
                return null;
        }

        public static bool ProgramDataAvailable
        {
            get
            {
                return BusinessClasses.ListManager.Instance.SourcePrograms.Count > 0;
            }
        }

        private static void RemoveInstances()
        {
            CustomControls.HomeControl.RemoveInstance();
            CustomControls.WeeklyScheduleControl.RemoveInstance();
            CustomControls.MonthlyScheduleControl.RemoveInstance();
            CustomControls.ModelsOfSuccessContainerControl.RemoveInstance();
            FormMain.RemoveInstance();
            BusinessClasses.ScheduleManager.Instance.RemoveInstance();
        }

        public static void ShowWarning(string text)
        {
            MessageBox.Show(text, "Radio Schedule Builder", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        public static DialogResult ShowWarningQuestion(string text)
        {
            return MessageBox.Show(text, "Radio Schedule Builder", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
        }

        public static void ShowInformation(string text)
        {
            MessageBox.Show(text, "Radio Schedule Builder", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private static void SetCulture()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek = DayOfWeek.Monday;
            System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = @"MM/dd/yyyy";
            System.Threading.Thread.CurrentThread.CurrentUICulture = System.Threading.Thread.CurrentThread.CurrentCulture;
        }

        private static void RestoreCulture()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
            System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek = DayOfWeek.Monday;
            System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = @"MM/dd/yyyy";
            System.Threading.Thread.CurrentThread.CurrentUICulture = System.Threading.Thread.CurrentThread.CurrentCulture;
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

        public static void ActivateForm(IntPtr handle, bool maximized, bool topMost)
        {
            InteropClasses.WinAPIHelper.ShowWindow(handle, maximized ? InteropClasses.WindowShowStyle.ShowMaximized : InteropClasses.WindowShowStyle.ShowNormal);
            uint lpdwProcessId = 0;
            InteropClasses.WinAPIHelper.AttachThreadInput(InteropClasses.WinAPIHelper.GetCurrentThreadId(), InteropClasses.WinAPIHelper.GetWindowThreadProcessId(InteropClasses.WinAPIHelper.GetForegroundWindow(), out lpdwProcessId), true);
            InteropClasses.WinAPIHelper.SetForegroundWindow(handle);
            InteropClasses.WinAPIHelper.AttachThreadInput(InteropClasses.WinAPIHelper.GetCurrentThreadId(), InteropClasses.WinAPIHelper.GetWindowThreadProcessId(InteropClasses.WinAPIHelper.GetForegroundWindow(), out lpdwProcessId), false);
            if (topMost)
                InteropClasses.WinAPIHelper.MakeTopMost(handle);
            else
                InteropClasses.WinAPIHelper.MakeNormal(handle);
        }

        public static string GetLetterByDigit(int digit)
        {
            switch (digit)
            {
                case 1:
                    return "A";
                case 2:
                    return "B";
                case 3:
                    return "C";
                case 4:
                    return "D";
                case 5:
                    return "E";
                case 6:
                    return "F";
                default:
                    return "";
            }
        }
    }
}
