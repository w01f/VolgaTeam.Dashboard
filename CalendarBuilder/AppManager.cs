using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CalendarBuilder
{
    public class AppManager
    {
        public static void NewSchedule()
        {
            using (ToolForms.FormNewCalendar from = new ToolForms.FormNewCalendar())
            {
                if (from.ShowDialog() == DialogResult.OK)
                {
                    if (InteropClasses.PowerPointHelper.Instance.Connect())
                    {
                        if (!string.IsNullOrEmpty(from.ScheduleName))
                        {
                            ConfigurationClasses.RegistryHelper.MainFormHandle = FormMain.Instance.Handle;
                            string fileName = from.ScheduleName.Trim();
                            BusinessClasses.SuccessModelsManager.Instance.Load();
                            BusinessClasses.OutputManager.Instance.LoadCalendarTemplates();
                            BusinessClasses.ScheduleManager.Instance.LoadAdSchedules();
                            BusinessClasses.ScheduleManager.Instance.CreateSchedule(fileName);
                            FormMain.Instance.ShowDialog();
                            InteropClasses.PowerPointHelper.Instance.Disconnect();
                            RestoreCulture();
                            RemoveInstances();
                        }
                        else
                        {
                            AppManager.ShowWarning("Calendar Name can't be empty");
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
                dialog.Title = "Select Calendar File";
                dialog.Filter = "Calendar Files|*.xml";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    if (InteropClasses.PowerPointHelper.Instance.Connect())
                    {
                        ConfigurationClasses.RegistryHelper.MainFormHandle = FormMain.Instance.Handle;
                        string fileName = dialog.FileName;
                        ConfigurationClasses.SettingsManager.Instance.SaveFolder = new FileInfo(fileName).Directory.FullName;
                        BusinessClasses.SuccessModelsManager.Instance.Load();
                        BusinessClasses.OutputManager.Instance.LoadCalendarTemplates();
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
                BusinessClasses.SuccessModelsManager.Instance.Load();
                BusinessClasses.OutputManager.Instance.LoadCalendarTemplates();
                BusinessClasses.ScheduleManager.Instance.LoadAdSchedules();
                BusinessClasses.ScheduleManager.Instance.OpenSchedule(fileName);
                FormMain.Instance.ShowDialog();
                InteropClasses.PowerPointHelper.Instance.Disconnect();
                RestoreCulture();
                RemoveInstances();
            }
        }

        public static void ImportSchedule()
        {
            if (InteropClasses.PowerPointHelper.Instance.Connect())
            {
                BusinessClasses.ScheduleManager.Instance.LoadAdSchedules();
                using (ToolForms.FormImport form = new ToolForms.FormImport())
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        ConfigurationClasses.RegistryHelper.MainFormHandle = FormMain.Instance.Handle;
                        BusinessClasses.SuccessModelsManager.Instance.Load();
                        BusinessClasses.OutputManager.Instance.LoadCalendarTemplates();
                        BusinessClasses.ScheduleManager.Instance.ImportSchedule(form.SelectedSchedule, form.BuildAdvanced, form.BuildGraphic, form.BuildSimple);
                        FormMain.Instance.ShowDialog();
                        InteropClasses.PowerPointHelper.Instance.Disconnect();
                        RestoreCulture();
                        RemoveInstances();
                    }
                }
            }
        }

        public static void ImportSchedule(AdScheduleBuilder.BusinessClasses.Schedule sourceSchedule, bool buildAdvanced, bool buildGraphic, bool buildSimple)
        {
            if (InteropClasses.PowerPointHelper.Instance.Connect())
            {
                ConfigurationClasses.RegistryHelper.MainFormHandle = FormMain.Instance.Handle;
                BusinessClasses.SuccessModelsManager.Instance.Load();
                BusinessClasses.OutputManager.Instance.LoadCalendarTemplates();
                BusinessClasses.ScheduleManager.Instance.ImportSchedule(sourceSchedule, buildAdvanced, buildGraphic, buildSimple);
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
                return true;
            }
        }

        private static void RemoveInstances()
        {
            PresentationClasses.HomeControl.RemoveInstance();
            PresentationClasses.CalendarVisualizer.RemoveInstance();
            PresentationClasses.ModelsOfSuccessContainerControl.RemoveInstance();
            FormMain.RemoveInstance();
            BusinessClasses.ScheduleManager.Instance.RemoveInstance();
        }

        public static void ShowWarning(string text)
        {
            MessageBox.Show(text, "Ninja Calendar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        public static DialogResult ShowWarningQuestion(string text)
        {
            return MessageBox.Show(text, "Ninja Calendar", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
        }

        public static void ShowInformation(string text)
        {
            MessageBox.Show(text, "Ninja Calendar", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        public static void ActivatePowerPoint()
        {
            if (InteropClasses.PowerPointHelper.Instance.PowerPointObject != null)
            {
                IntPtr powerPointHandle = new IntPtr(InteropClasses.PowerPointHelper.Instance.PowerPointObject.HWND);
                InteropClasses.WinAPIHelper.ShowWindow(powerPointHandle, InteropClasses.WindowShowStyle.ShowMaximized);
                uint lpdwProcessId = 0;
                InteropClasses.WinAPIHelper.AttachThreadInput(InteropClasses.WinAPIHelper.GetCurrentThreadId(), InteropClasses.WinAPIHelper.GetWindowThreadProcessId(InteropClasses.WinAPIHelper.GetForegroundWindow(), out lpdwProcessId), true);
                InteropClasses.WinAPIHelper.SetForegroundWindow(powerPointHandle);
                InteropClasses.WinAPIHelper.AttachThreadInput(InteropClasses.WinAPIHelper.GetCurrentThreadId(), InteropClasses.WinAPIHelper.GetWindowThreadProcessId(InteropClasses.WinAPIHelper.GetForegroundWindow(), out lpdwProcessId), false);
            }
        }

        public static void ActivateMiniBar()
        {
            IntPtr minibarHandle = ConfigurationClasses.RegistryHelper.MinibarHandle;
            if (minibarHandle.ToInt32() == 0)
            {
                Process[] processList = Process.GetProcesses();
                foreach (Process process in processList.Where(x => x.ProcessName.Contains("MiniBar")))
                {
                    if (process.MainWindowHandle.ToInt32() != 0)
                    {
                        minibarHandle = process.MainWindowHandle;
                        break;
                    }
                }
            }
            if (minibarHandle.ToInt32() != 0)
            {
                uint lpdwProcessId = 0;
                InteropClasses.WinAPIHelper.MakeTopMost(minibarHandle);
                InteropClasses.WinAPIHelper.AttachThreadInput(InteropClasses.WinAPIHelper.GetCurrentThreadId(), InteropClasses.WinAPIHelper.GetWindowThreadProcessId(InteropClasses.WinAPIHelper.GetForegroundWindow(), out lpdwProcessId), true);
                InteropClasses.WinAPIHelper.SetForegroundWindow(minibarHandle);
                InteropClasses.WinAPIHelper.AttachThreadInput(InteropClasses.WinAPIHelper.GetCurrentThreadId(), InteropClasses.WinAPIHelper.GetWindowThreadProcessId(InteropClasses.WinAPIHelper.GetForegroundWindow(), out lpdwProcessId), false);
            }
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
