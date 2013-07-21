using System;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.OnlineSchedule;
using NewBizWiz.OnlineSchedule.Controls;
using NewBizWiz.OnlineSchedule.Controls.BusinessClasses;
using NewBizWiz.OnlineSchedule.Controls.InteropClasses;
using NewBizWiz.OnlineSchedule.Controls.PresentationClasses.ToolForms;
using ListManager = NewBizWiz.Core.OnlineSchedule.ListManager;
using SettingsManager = NewBizWiz.Core.OnlineSchedule.SettingsManager;

namespace NewBizWiz.OnlineSchedule.Internal
{
	public class AppManager
	{
		public static bool ProgramDataAvailable
		{
			get { return ListManager.Instance.ProductSources.Count > 0; }
		}

		public static void NewSchedule()
		{
			using (var from = new FormNewSchedule())
			{
				if (from.ShowDialog() == DialogResult.OK)
				{
					if (OnlineSchedulePowerPointHelper.Instance.Connect())
					{
						if (!string.IsNullOrEmpty(from.ScheduleName))
						{
							RegistryHelper.MainFormHandle = FormMain.Instance.Handle;
							SetCulture();
							string fileName = from.ScheduleName.Trim();
							BusinessWrapper.Instance.ScheduleManager.OpenSchedule(fileName, true);
							FormMain.Instance.ShowDialog();
							OnlineSchedulePowerPointHelper.Instance.Disconnect();
							RestoreCulture();
							RemoveInstances();
						}
						else
						{
							Utilities.Instance.ShowWarning("Schedule Name can't be empty");
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
					if (OnlineSchedulePowerPointHelper.Instance.Connect())
					{
						RegistryHelper.MainFormHandle = FormMain.Instance.Handle;
						SetCulture();
						string fileName = dialog.FileName;
						SettingsManager.Instance.SaveFolder = new FileInfo(fileName).Directory.FullName;
						BusinessWrapper.Instance.ScheduleManager.OpenSchedule(fileName);
						FormMain.Instance.ShowDialog();
						OnlineSchedulePowerPointHelper.Instance.Disconnect();
						RestoreCulture();
						RemoveInstances();
					}
				}
			}
		}

		public static void OpenSchedule(string fileName)
		{
			if (OnlineSchedulePowerPointHelper.Instance.Connect())
			{
				RegistryHelper.MainFormHandle = FormMain.Instance.Handle;
				SetCulture();
				BusinessWrapper.Instance.ScheduleManager.OpenSchedule(fileName);
				FormMain.Instance.ShowDialog();
				OnlineSchedulePowerPointHelper.Instance.Disconnect();
				RestoreCulture();
				RemoveInstances();
			}
		}

		public static ShortSchedule[] GetShortScheduleList()
		{
			var saveFolder = new DirectoryInfo(SettingsManager.Instance.SaveFolder);
			if (saveFolder.Exists)
				return BusinessWrapper.Instance.ScheduleManager.GetShortScheduleList(saveFolder);
			else
				return null;
		}

		private static void RemoveInstances()
		{
			Controller.Instance.RemoveInstance();
			FormMain.RemoveInstance();
			BusinessWrapper.Instance.ScheduleManager.RemoveInstance();
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