using System.IO;
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
			using (var form = new FormNewSchedule())
			{
				if (form.ShowDialog() != DialogResult.OK) return;
				if (!AdSchedulePowerPointHelper.Instance.Connect(false) || !OnlineSchedulePowerPointHelper.Instance.Connect(false)) return;
				if (!string.IsNullOrEmpty(form.ScheduleName))
				{
					RegistryHelper.MainFormHandle = FormMain.Instance.Handle;
					RegistryHelper.MaximizeMainForm = true;
					BusinessWrapper.Instance.OutputManager.TemplatesManager.LoadCalendarTemplates();
					BusinessWrapper.Instance.ActivityManager.AddActivity(new ScheduleActivity("New Created", form.ScheduleName.Trim()));
					BusinessWrapper.Instance.ScheduleManager.OpenSchedule(form.ScheduleName.Trim(), true);
					FormMain.Instance.ShowDialog();
					AdSchedulePowerPointHelper.Instance.Disconnect();
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
				if (!AdSchedulePowerPointHelper.Instance.Connect(false) || !OnlineSchedulePowerPointHelper.Instance.Connect(false)) return;
				RegistryHelper.MainFormHandle = FormMain.Instance.Handle;
				RegistryHelper.MaximizeMainForm = true;
				SettingsManager.Instance.SaveFolder = new FileInfo(dialog.FileName).Directory.FullName;
				BusinessWrapper.Instance.OutputManager.TemplatesManager.LoadCalendarTemplates();
				BusinessWrapper.Instance.ScheduleManager.OpenSchedule(dialog.FileName);
				FormMain.Instance.ShowDialog();
				AdSchedulePowerPointHelper.Instance.Disconnect();
				OnlineSchedulePowerPointHelper.Instance.Disconnect();
				RemoveInstances();
			}
		}

		public static void OpenSchedule(string fileName)
		{
			if (!AdSchedulePowerPointHelper.Instance.Connect(false) || !OnlineSchedulePowerPointHelper.Instance.Connect(false)) return;
			RegistryHelper.MainFormHandle = FormMain.Instance.Handle;
			RegistryHelper.MaximizeMainForm = true;
			BusinessWrapper.Instance.OutputManager.TemplatesManager.LoadCalendarTemplates();
			BusinessWrapper.Instance.ActivityManager.AddActivity(new ScheduleActivity("Previous Opened", Path.GetFileNameWithoutExtension(fileName)));
			BusinessWrapper.Instance.ScheduleManager.OpenSchedule(fileName);
			FormMain.Instance.ShowDialog();
			AdSchedulePowerPointHelper.Instance.Disconnect();
			OnlineSchedulePowerPointHelper.Instance.Disconnect();
			RemoveInstances();
		}

		private static void RemoveInstances()
		{
			Controller.Instance.RemoveInstance();
			BusinessWrapper.Instance.ScheduleManager.RemoveInstance();
			FormMain.RemoveInstance();
		}
	}
}