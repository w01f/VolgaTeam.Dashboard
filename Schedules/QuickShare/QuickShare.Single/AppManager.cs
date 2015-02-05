﻿using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using NewBizWiz.CommonGUI.Common;
using NewBizWiz.CommonGUI.Floater;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.MediaSchedule;
using NewBizWiz.QuickShare.Controls.InteropClasses;

namespace NewBizWiz.QuickShare.Single
{
	public class AppManager
	{
		private static readonly AppManager _instance = new AppManager();
		private readonly FloaterManager _floater = new FloaterManager();

		private AppManager() { }

		public static AppManager Instance
		{
			get { return _instance; }
		}

		public void RunForm()
		{
			LicenseHelper.Register();
			MasterWizardManager.Instance.SetMasterWizard();
			QuickSharePowerPointHelper.Instance.SetPresentationSettings();
			Application.Run(FormMain.Instance);
		}

		public void RunPowerPoint()
		{
			QuickSharePowerPointHelper.Instance.Connect(false);
		}

		public void ActivateMainForm()
		{
			var processList = Process.GetProcesses();
			foreach (var process in processList.Where(x => x.ProcessName.ToLower().Contains(String.Format("{0}quickshare", MediaMetaData.Instance.DataTypeString.ToLower()))))
			{
				if (process.MainWindowHandle.ToInt32() == 0) continue;
				Utilities.Instance.ActivateForm(process.MainWindowHandle, true, false);
				break;
			}
		}

		public void ShowFloater(Form sender, FloaterRequestedEventArgs e)
		{
			var afterBack = new Action(ActivateMainForm);
			_floater.ShowFloater(sender ?? FormMain.Instance, e.Logo, e.AfterShow, null, afterBack);
		}
	}
}