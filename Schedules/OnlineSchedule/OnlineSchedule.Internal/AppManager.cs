using System.IO;
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
			using (var form = new FormNewSchedule())
			{
				if (form.ShowDialog() != DialogResult.OK) return;
				if (!OnlineSchedulePowerPointHelper.Instance.Connect()) return;
				if (!string.IsNullOrEmpty(form.ScheduleName))
				{
					RegistryHelper.MainFormHandle = FormMain.Instance.Handle;
					var fileName = form.ScheduleName.Trim();
					BusinessWrapper.Instance.ActivityManager.AddActivity(new ScheduleActivity("New Created", fileName));
					BusinessWrapper.Instance.ScheduleManager.OpenSchedule(fileName, true);
					FormMain.Instance.ShowDialog();
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
				dialog.InitialDirectory = SettingsManager.Instance.SaveFolder;
				dialog.Title = "Select Schedule File";
				dialog.Filter = "Schedule Files|*.xml";
				if (dialog.ShowDialog() != DialogResult.OK) return;
				if (!OnlineSchedulePowerPointHelper.Instance.Connect()) return;
				RegistryHelper.MainFormHandle = FormMain.Instance.Handle;
				var fileName = dialog.FileName;
				SettingsManager.Instance.SaveFolder = new FileInfo(fileName).Directory.FullName;
				BusinessWrapper.Instance.ScheduleManager.OpenSchedule(fileName);
				FormMain.Instance.ShowDialog();
				OnlineSchedulePowerPointHelper.Instance.Disconnect();
				RemoveInstances();
			}
		}

		public static void OpenSchedule(string fileName)
		{
			if (!OnlineSchedulePowerPointHelper.Instance.Connect()) return;
			RegistryHelper.MainFormHandle = FormMain.Instance.Handle;
			BusinessWrapper.Instance.ActivityManager.AddActivity(new ScheduleActivity("Previous Opened", Path.GetFileNameWithoutExtension(fileName)));
			BusinessWrapper.Instance.ScheduleManager.OpenSchedule(fileName);
			FormMain.Instance.ShowDialog();
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