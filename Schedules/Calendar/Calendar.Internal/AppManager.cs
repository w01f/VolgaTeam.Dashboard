using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using NewBizWiz.Calendar.Controls;
using NewBizWiz.Calendar.Controls.BusinessClasses;
using NewBizWiz.Calendar.Controls.InteropClasses;
using NewBizWiz.Calendar.Controls.ToolForms;
using NewBizWiz.Core.AdSchedule;
using NewBizWiz.Core.Common;
using SettingsManager = NewBizWiz.Core.Calendar.SettingsManager;

namespace NewBizWiz.Calendar.Internal
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
					if (CalendarPowerPointHelper.Instance.Connect())
					{
						if (!string.IsNullOrEmpty(from.ScheduleName))
						{
							SetCulture();
							RegistryHelper.MainFormHandle = FormMain.Instance.Handle;
							string fileName = from.ScheduleName.Trim();
							BusinessWrapper.Instance.OutputManager.LoadCalendarTemplates();
							BusinessWrapper.Instance.ScheduleManager.CreateSchedule(fileName);
							FormMain.Instance.ShowDialog();
							CalendarPowerPointHelper.Instance.Disconnect();
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
					if (CalendarPowerPointHelper.Instance.Connect())
					{
						SetCulture();
						RegistryHelper.MainFormHandle = FormMain.Instance.Handle;
						string fileName = dialog.FileName;
						SettingsManager.Instance.SaveFolder = new FileInfo(fileName).Directory.FullName;
						BusinessWrapper.Instance.OutputManager.LoadCalendarTemplates();
						BusinessWrapper.Instance.ScheduleManager.OpenSchedule(fileName);
						FormMain.Instance.ShowDialog();
						CalendarPowerPointHelper.Instance.Disconnect();
						RestoreCulture();
						RemoveInstances();
					}
				}
			}
		}

		public static void OpenSchedule(string fileName)
		{
			if (CalendarPowerPointHelper.Instance.Connect())
			{
				SetCulture();
				RegistryHelper.MainFormHandle = FormMain.Instance.Handle;
				BusinessWrapper.Instance.OutputManager.LoadCalendarTemplates();
				BusinessWrapper.Instance.ScheduleManager.OpenSchedule(fileName);
				FormMain.Instance.ShowDialog();
				CalendarPowerPointHelper.Instance.Disconnect();
				RestoreCulture();
				RemoveInstances();
			}
		}

		public static void ImportSchedule(string fileName)
		{
			if (CalendarPowerPointHelper.Instance.Connect())
			{
				SetCulture();
				RegistryHelper.MainFormHandle = FormMain.Instance.Handle;
				BusinessWrapper.Instance.OutputManager.LoadCalendarTemplates();
				BusinessWrapper.Instance.ScheduleManager.ImportSchedule(fileName);
				FormMain.Instance.ShowDialog();
				CalendarPowerPointHelper.Instance.Disconnect();
				RestoreCulture();
				RemoveInstances();
			}
		}

		public static void ImportSchedule(Schedule sourceSchedule)
		{
			if (CalendarPowerPointHelper.Instance.Connect())
			{
				SetCulture();
				RegistryHelper.MainFormHandle = FormMain.Instance.Handle;
				BusinessWrapper.Instance.OutputManager.LoadCalendarTemplates();
				BusinessWrapper.Instance.ScheduleManager.ImportSchedule(sourceSchedule.ScheduleFile.FullName);
				FormMain.Instance.ShowDialog();
				CalendarPowerPointHelper.Instance.Disconnect();
				RestoreCulture();
				RemoveInstances();
			}
		}

		private static void RemoveInstances()
		{
			Controller.Instance.RemoveInstance();
			FormMain.RemoveInstance();
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

		private static void SetCulture()
		{
			Thread.CurrentThread.CurrentCulture = new CultureInfo("en-GB");
			Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek = DayOfWeek.Sunday;
			Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = @"MM/dd/yyyy";
			Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
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
			catch {}
			finally
			{
				o = null;
			}
		}
	}
}