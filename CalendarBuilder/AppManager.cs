using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using CalendarBuilder.BusinessClasses;
using CalendarBuilder.ConfigurationClasses;
using CalendarBuilder.InteropClasses;
using CalendarBuilder.PresentationClasses;
using CalendarBuilder.ToolForms;
using Schedule = AdScheduleBuilder.BusinessClasses.Schedule;
using ShortSchedule = CalendarBuilder.BusinessClasses.ShortSchedule;
using SuccessModelsManager = CalendarBuilder.BusinessClasses.SuccessModelsManager;

namespace CalendarBuilder
{
	public class AppManager
	{
		public static bool ProgramDataAvailable
		{
			get { return true; }
		}

		public static void NewSchedule()
		{
			using (var from = new FormNewCalendar())
			{
				if (from.ShowDialog() == DialogResult.OK)
				{
					if (PowerPointHelper.Instance.Connect())
					{
						if (!string.IsNullOrEmpty(from.ScheduleName))
						{
							RegistryHelper.MainFormHandle = FormMain.Instance.Handle;
							string fileName = from.ScheduleName.Trim();
							SuccessModelsManager.Instance.Load();
							OutputManager.Instance.LoadCalendarTemplates();
							ScheduleManager.Instance.CreateSchedule(fileName);
							FormMain.Instance.ShowDialog();
							PowerPointHelper.Instance.Disconnect();
							RestoreCulture();
							RemoveInstances();
						}
						else
						{
							ShowWarning("Calendar Name can't be empty");
						}
					}
				}
			}
		}

		public static void OpenSchedule()
		{
			using (var dialog = new OpenFileDialog())
			{
				dialog.InitialDirectory = SettingsManager.Instance.SaveFolder;
				dialog.Title = "Select Calendar File";
				dialog.Filter = "Calendar Files|*.xml";
				if (dialog.ShowDialog() == DialogResult.OK)
				{
					if (PowerPointHelper.Instance.Connect())
					{
						RegistryHelper.MainFormHandle = FormMain.Instance.Handle;
						string fileName = dialog.FileName;
						SettingsManager.Instance.SaveFolder = new FileInfo(fileName).Directory.FullName;
						SuccessModelsManager.Instance.Load();
						OutputManager.Instance.LoadCalendarTemplates();
						ScheduleManager.Instance.OpenSchedule(fileName);
						FormMain.Instance.ShowDialog();
						PowerPointHelper.Instance.Disconnect();
						RestoreCulture();
						RemoveInstances();
					}
				}
			}
		}

		public static void OpenSchedule(string fileName)
		{
			if (PowerPointHelper.Instance.Connect())
			{
				RegistryHelper.MainFormHandle = FormMain.Instance.Handle;
				SuccessModelsManager.Instance.Load();
				OutputManager.Instance.LoadCalendarTemplates();
				ScheduleManager.Instance.OpenSchedule(fileName);
				FormMain.Instance.ShowDialog();
				PowerPointHelper.Instance.Disconnect();
				RestoreCulture();
				RemoveInstances();
			}
		}

		public static void ImportSchedule(string fileName)
		{
			if (PowerPointHelper.Instance.Connect())
			{
				RegistryHelper.MainFormHandle = FormMain.Instance.Handle;
				SuccessModelsManager.Instance.Load();
				OutputManager.Instance.LoadCalendarTemplates();
				ScheduleManager.Instance.ImportSchedule(fileName, false, true, false);
				FormMain.Instance.ShowDialog();
				PowerPointHelper.Instance.Disconnect();
				RestoreCulture();
				RemoveInstances();
			}
		}

		public static void ImportSchedule(Schedule sourceSchedule, bool buildAdvanced, bool buildGraphic, bool buildSimple)
		{
			if (PowerPointHelper.Instance.Connect())
			{
				RegistryHelper.MainFormHandle = FormMain.Instance.Handle;
				SuccessModelsManager.Instance.Load();
				OutputManager.Instance.LoadCalendarTemplates();
				ScheduleManager.Instance.ImportSchedule(sourceSchedule.ScheduleFile.FullName, buildAdvanced, buildGraphic, buildSimple);
				FormMain.Instance.ShowDialog();
				PowerPointHelper.Instance.Disconnect();
				RestoreCulture();
				RemoveInstances();
			}
		}

		public static ShortSchedule[] GetShortScheduleList()
		{
			var schedules = new List<ShortSchedule>();
			var saveFolder = new DirectoryInfo(SettingsManager.Instance.SaveFolder);
			if (saveFolder.Exists)
				schedules.AddRange(ScheduleManager.Instance.GetShortScheduleList(saveFolder));
			saveFolder = new DirectoryInfo(SettingsManager.Instance.AdScheduleFolder);
			if (saveFolder.Exists)
				schedules.AddRange(ScheduleManager.Instance.GetShortScheduleList(saveFolder, false));
			return schedules.ToArray();
		}

		private static void RemoveInstances()
		{
			HomeControl.RemoveInstance();
			CalendarVisualizer.RemoveInstance();
			ModelsOfSuccessContainerControl.RemoveInstance();
			FormMain.RemoveInstance();
			ScheduleManager.Instance.RemoveInstance();
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
			Thread.CurrentThread.CurrentCulture = new CultureInfo("en-GB");
			Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek = DayOfWeek.Monday;
			Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = @"MM/dd/yyyy";
			Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
		}

		public static void ReleaseComObject(object o)
		{
			try
			{
				Marshal.ReleaseComObject(o);
			}
			catch { }
			finally
			{
				o = null;
			}
		}

		public static void ActivateForm(IntPtr handle, bool maximized, bool topMost)
		{
			WinAPIHelper.ShowWindow(handle, maximized ? WindowShowStyle.ShowMaximized : WindowShowStyle.ShowNormal);
			uint lpdwProcessId = 0;
			WinAPIHelper.AttachThreadInput(WinAPIHelper.GetCurrentThreadId(), WinAPIHelper.GetWindowThreadProcessId(WinAPIHelper.GetForegroundWindow(), out lpdwProcessId), true);
			WinAPIHelper.SetForegroundWindow(handle);
			WinAPIHelper.AttachThreadInput(WinAPIHelper.GetCurrentThreadId(), WinAPIHelper.GetWindowThreadProcessId(WinAPIHelper.GetForegroundWindow(), out lpdwProcessId), false);
			if (topMost)
				WinAPIHelper.MakeTopMost(handle);
			else
				WinAPIHelper.MakeNormal(handle);
		}

		public static void ActivatePowerPoint()
		{
			if (PowerPointHelper.Instance.PowerPointObject != null)
			{
				var powerPointHandle = new IntPtr(PowerPointHelper.Instance.PowerPointObject.HWND);
				WinAPIHelper.ShowWindow(powerPointHandle, WindowShowStyle.ShowMaximized);
				uint lpdwProcessId = 0;
				WinAPIHelper.AttachThreadInput(WinAPIHelper.GetCurrentThreadId(), WinAPIHelper.GetWindowThreadProcessId(WinAPIHelper.GetForegroundWindow(), out lpdwProcessId), true);
				WinAPIHelper.SetForegroundWindow(powerPointHandle);
				WinAPIHelper.AttachThreadInput(WinAPIHelper.GetCurrentThreadId(), WinAPIHelper.GetWindowThreadProcessId(WinAPIHelper.GetForegroundWindow(), out lpdwProcessId), false);
			}
		}

		public static void ActivateMiniBar()
		{
			IntPtr minibarHandle = RegistryHelper.MinibarHandle;
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
				WinAPIHelper.MakeTopMost(minibarHandle);
				WinAPIHelper.AttachThreadInput(WinAPIHelper.GetCurrentThreadId(), WinAPIHelper.GetWindowThreadProcessId(WinAPIHelper.GetForegroundWindow(), out lpdwProcessId), true);
				WinAPIHelper.SetForegroundWindow(minibarHandle);
				WinAPIHelper.AttachThreadInput(WinAPIHelper.GetCurrentThreadId(), WinAPIHelper.GetWindowThreadProcessId(WinAPIHelper.GetForegroundWindow(), out lpdwProcessId), false);
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