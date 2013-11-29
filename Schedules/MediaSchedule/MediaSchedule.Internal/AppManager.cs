using System;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.MediaSchedule;
using NewBizWiz.MediaSchedule.Controls;
using NewBizWiz.MediaSchedule.Controls.BusinessClasses;
using NewBizWiz.MediaSchedule.Controls.InteropClasses;
using NewBizWiz.MediaSchedule.Controls.ToolForms;
using NewBizWiz.OnlineSchedule.Controls.InteropClasses;

namespace NewBizWiz.MediaSchedule.Internal
{
	public class AppManager
	{
		public static bool ProgramDataAvailable
		{
			get { return MediaMetaData.Instance.ListManager.SourcePrograms.Count > 0; }
		}

		public static void NewSchedule()
		{
			using (var form = new FormNewSchedule())
			{
				if (form.ShowDialog() != DialogResult.OK) return;
				if (!(MediaSchedulePowerPointHelper.Instance.Connect(false) && OnlineSchedulePowerPointHelper.Instance.Connect(false))) return;
				if (!string.IsNullOrEmpty(form.ScheduleName))
				{
					RegistryHelper.MainFormHandle = FormMain.Instance.Handle;
					SetCulture();
					string fileName = form.ScheduleName.Trim();
					BusinessWrapper.Instance.ScheduleManager.OpenSchedule(fileName, true);
					FormMain.Instance.ShowDialog();
					MediaSchedulePowerPointHelper.Instance.Disconnect();
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

		public static void OpenSchedule()
		{
			using (var dialog = new OpenFileDialog())
			{
				dialog.InitialDirectory = MediaMetaData.Instance.SettingsManager.SaveFolder;
				dialog.Title = "Select Schedule File";
				dialog.Filter = "Schedule Files|*.xml";
				if (dialog.ShowDialog() != DialogResult.OK) return;
				if (!(MediaSchedulePowerPointHelper.Instance.Connect(false) && OnlineSchedulePowerPointHelper.Instance.Connect(false))) return;
				RegistryHelper.MainFormHandle = FormMain.Instance.Handle;
				SetCulture();
				string fileName = dialog.FileName;
				MediaMetaData.Instance.SettingsManager.SaveFolder = new FileInfo(fileName).Directory.FullName;
				BusinessWrapper.Instance.ScheduleManager.OpenSchedule(fileName);
				FormMain.Instance.ShowDialog();
				MediaSchedulePowerPointHelper.Instance.Disconnect();
				OnlineSchedulePowerPointHelper.Instance.Disconnect();
				RestoreCulture();
				RemoveInstances();
			}
		}

		public static void OpenSchedule(string fileName)
		{
			if (!(MediaSchedulePowerPointHelper.Instance.Connect(false) && OnlineSchedulePowerPointHelper.Instance.Connect(false))) return;
			RegistryHelper.MainFormHandle = FormMain.Instance.Handle;
			SetCulture();
			BusinessWrapper.Instance.ScheduleManager.OpenSchedule(fileName);
			FormMain.Instance.ShowDialog();
			MediaSchedulePowerPointHelper.Instance.Disconnect();
			OnlineSchedulePowerPointHelper.Instance.Disconnect();
			RestoreCulture();
			RemoveInstances();
		}

		public static ShortSchedule[] GetShortScheduleList()
		{
			return ScheduleManager.GetShortScheduleList();
		}

		private static void RemoveInstances()
		{
			Controller.Instance.RemoveInstance();
			FormMain.RemoveInstance();
			BusinessWrapper.Instance.ScheduleManager.RemoveInstance();
		}

		private static void SetCulture()
		{
			Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
			Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek = DayOfWeek.Monday;
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
	}
}