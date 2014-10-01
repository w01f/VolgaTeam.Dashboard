﻿using System.IO;
using System.Windows.Forms;
using NewBizWiz.CommonGUI.ToolForms;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.MediaSchedule;
using NewBizWiz.MediaSchedule.Controls;
using NewBizWiz.MediaSchedule.Controls.BusinessClasses;
using NewBizWiz.MediaSchedule.Controls.InteropClasses;
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
				if (!(RegularMediaSchedulePowerPointHelper.Instance.Connect(false) && OnlineSchedulePowerPointHelper.Instance.Connect(false))) return;
				if (!string.IsNullOrEmpty(form.ScheduleName))
				{
					RegistryHelper.MainFormHandle = FormMain.Instance.Handle;
					string fileName = form.ScheduleName.Trim();
					BusinessWrapper.Instance.ActivityManager.AddActivity(new ScheduleActivity("New Created", form.ScheduleName.Trim()));
					BusinessWrapper.Instance.ScheduleManager.OpenSchedule(fileName, true);
					FormMain.Instance.ShowDialog();
					RegularMediaSchedulePowerPointHelper.Instance.Disconnect();
					OnlineSchedulePowerPointHelper.Instance.Disconnect();
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
				if (!(RegularMediaSchedulePowerPointHelper.Instance.Connect(false) && OnlineSchedulePowerPointHelper.Instance.Connect(false))) return;
				RegistryHelper.MainFormHandle = FormMain.Instance.Handle;
				string fileName = dialog.FileName;
				MediaMetaData.Instance.SettingsManager.SaveFolder = new FileInfo(fileName).Directory.FullName;
				BusinessWrapper.Instance.ActivityManager.AddActivity(new ScheduleActivity("Previous Opened", Path.GetFileNameWithoutExtension(fileName)));
				BusinessWrapper.Instance.ScheduleManager.OpenSchedule(fileName);
				FormMain.Instance.ShowDialog();
				RegularMediaSchedulePowerPointHelper.Instance.Disconnect();
				OnlineSchedulePowerPointHelper.Instance.Disconnect();
				RemoveInstances();
			}
		}

		public static void OpenSchedule(string fileName)
		{
			if (!(RegularMediaSchedulePowerPointHelper.Instance.Connect(false) && OnlineSchedulePowerPointHelper.Instance.Connect(false))) return;
			RegistryHelper.MainFormHandle = FormMain.Instance.Handle;
			BusinessWrapper.Instance.ScheduleManager.OpenSchedule(fileName);
			FormMain.Instance.ShowDialog();
			RegularMediaSchedulePowerPointHelper.Instance.Disconnect();
			OnlineSchedulePowerPointHelper.Instance.Disconnect();
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
	}
}