using System;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using NewBizWiz.AdSchedule.Controls;
using NewBizWiz.AdSchedule.Controls.BusinessClasses;
using NewBizWiz.AdSchedule.Controls.InteropClasses;
using NewBizWiz.AdSchedule.Controls.ToolForms;
using NewBizWiz.Core.Common;
using NewBizWiz.OnlineSchedule.Controls.InteropClasses;
using SettingsManager = NewBizWiz.Core.AdSchedule.SettingsManager;

namespace NewBizWiz.AdSchedule.Internal
{
	public class AppManager
	{
		public static bool ProgramDataAvailable
		{
			get { return Core.AdSchedule.ListManager.Instance.PublicationSources.Count > 1; }
		}

		public static void NewSchedule()
		{
			using (var from = new FormNewSchedule())
			{
				if (from.ShowDialog() == DialogResult.OK)
				{
					if (AdSchedulePowerPointHelper.Instance.Connect(false) && OnlineSchedulePowerPointHelper.Instance.Connect(false))
					{
						if (!string.IsNullOrEmpty(from.ScheduleName))
						{
							RegistryHelper.MainFormHandle = FormMain.Instance.Handle;
							RegistryHelper.MaximizeMainForm = true;
							SetCulture();
							BusinessWrapper.Instance.OutputManager.LoadCalendarTemplates();
							BusinessWrapper.Instance.ScheduleManager.OpenSchedule(from.ScheduleName.Trim(), true);
							FormMain.Instance.ShowDialog();
							AdSchedulePowerPointHelper.Instance.Disconnect();
							OnlineSchedulePowerPointHelper.Instance.Disconnect();
							RestoreCulture();
							RemoveInstances();
						}
						else
						{
							ShowWarning("Schedule Name can't be empty");
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
				dialog.Title = "Select Schedule File";
				dialog.Filter = "Schedule Files|*.xml";
				if (dialog.ShowDialog() == DialogResult.OK)
				{
					if (AdSchedulePowerPointHelper.Instance.Connect(false) && OnlineSchedulePowerPointHelper.Instance.Connect(false))
					{
						RegistryHelper.MainFormHandle = FormMain.Instance.Handle;
						RegistryHelper.MaximizeMainForm = true;
						SetCulture();
						SettingsManager.Instance.SaveFolder = new FileInfo(dialog.FileName).Directory.FullName;
						BusinessWrapper.Instance.OutputManager.LoadCalendarTemplates();
						BusinessWrapper.Instance.ScheduleManager.OpenSchedule(dialog.FileName);
						FormMain.Instance.ShowDialog();
						AdSchedulePowerPointHelper.Instance.Disconnect();
						OnlineSchedulePowerPointHelper.Instance.Disconnect();
						RestoreCulture();
						RemoveInstances();
					}
				}
			}
		}

		public static void OpenSchedule(string fileName)
		{
			if (AdSchedulePowerPointHelper.Instance.Connect(false) && OnlineSchedulePowerPointHelper.Instance.Connect(false))
			{
				RegistryHelper.MainFormHandle = FormMain.Instance.Handle;
				RegistryHelper.MaximizeMainForm = true;
				SetCulture();
				BusinessWrapper.Instance.OutputManager.LoadCalendarTemplates();
				BusinessWrapper.Instance.ScheduleManager.OpenSchedule(fileName);
				FormMain.Instance.ShowDialog();
				AdSchedulePowerPointHelper.Instance.Disconnect();
				OnlineSchedulePowerPointHelper.Instance.Disconnect();
				RestoreCulture();
				RemoveInstances();
			}
		}

		private static void RemoveInstances()
		{
			Controller.Instance.RemoveInstance();
			BusinessWrapper.Instance.ScheduleManager.RemoveInstance();
			FormMain.RemoveInstance();
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

		public static void ShowWarning(string text)
		{
			Utilities.Instance.ShowWarning(text);
		}

		public static DialogResult ShowWarningQuestion(string text)
		{
			return Utilities.Instance.ShowWarningQuestion(text);
		}

		public static void ShowInformation(string text)
		{
			Utilities.Instance.ShowInformation(text);
		}
	}
}