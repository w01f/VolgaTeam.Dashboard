using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using NewBizWiz.CommonGUI.Floater;
using NewBizWiz.Core.Common;
using NewBizWiz.Dashboard.InteropClasses;
using NewBizWiz.Dashboard.ToolForms;

namespace NewBizWiz.Dashboard
{
	public class AppManager
	{
		#region Delegates
		public delegate void EmptyParametersDelegate();

		public delegate void SingleParameterDelegate(bool parameter);
		#endregion

		private static readonly AppManager _instance = new AppManager();
		public HelpManager HelpManager { get; private set; }
		private readonly FloaterManager _floater = new FloaterManager();

		private AppManager()
		{
			HelpManager = new HelpManager(Core.Dashboard.SettingsManager.Instance.HelpLinksPath);
		}

		public static AppManager Instance
		{
			get { return _instance; }
		}

		public static string FormCaption
		{
			get { return SettingsManager.Instance.DashboardName + " - " + SettingsManager.Instance.Size; }
		}

		public void RunForm()
		{
			using (var form = new FormLoadSplash())
			{
				form.TopMost = true;
				var thread = new Thread(() => DashboardPowerPointHelper.Instance.SetPresentationSettings());
				form.Show();
				thread.Start();
				while (thread.IsAlive)
					Application.DoEvents();
				form.Close();
			}
			Utilities.Instance.ActivatePowerPoint(DashboardPowerPointHelper.Instance.PowerPointObject);
			FormMain.Instance.Init();
			RegistryHelper.MainFormHandle = FormMain.Instance.Handle;
			Application.Run(FormMain.Instance);
		}

		public bool RunPowerPoint()
		{
			return DashboardPowerPointHelper.Instance.Connect();
		}

		public void ActivateMainForm()
		{
			var mainFormHandle = RegistryHelper.MainFormHandle;
			if (mainFormHandle.ToInt32() == 0)
			{
				var processList = Process.GetProcesses();
				foreach (var process in processList.Where(x => x.ProcessName.ToLower().Contains("adsalesapp")).Where(process => process.MainWindowHandle.ToInt32() != 0))
				{
					mainFormHandle = process.MainWindowHandle;
					break;
				}
			}
			Utilities.Instance.ActivateForm(mainFormHandle, RegistryHelper.MaximizeMainForm, false);
		}

		public void MinimizeMainForm()
		{
			var mainFormHandle = RegistryHelper.MainFormHandle;
			if (mainFormHandle.ToInt32() == 0)
			{
				var processList = Process.GetProcesses();
				foreach (var process in processList.Where(x => x.ProcessName.ToLower().Contains("adsalesapp")))
				{
					if (process.MainWindowHandle.ToInt32() == 0) continue;
					mainFormHandle = process.MainWindowHandle;
					break;
				}
			}
			Utilities.Instance.MinimizeForm(mainFormHandle);
		}

		public static void SetAutoScrollPosition(ScrollableControl sender, Point p)
		{
			p.X = Math.Abs(p.X);
			p.Y = Math.Abs(p.Y);
			sender.AutoScrollPosition = p;
		}

		public void SetClickEventHandler(Control control)
		{
			foreach (Control childControl in control.Controls)
				SetClickEventHandler(childControl);
			if (control.GetType() != typeof(TextBoxMaskBox) && control.GetType() != typeof(TextEdit) && control.GetType() != typeof(MemoEdit) && control.GetType() != typeof(ComboBoxEdit) && control.GetType() != typeof(LookUpEdit) && control.GetType() != typeof(DateEdit) && control.GetType() != typeof(CheckedListBoxControl) && control.GetType() != typeof(SpinEdit) && control.GetType() != typeof(CheckEdit))
			{
				control.Click += ControlClick;
			}
		}

		private void ControlClick(object sender, EventArgs e)
		{
			((Control)sender).Select();
			if (((Control)sender).Parent != null)
				((Control)sender).Parent.Select();
		}

		public void ShowFloater(Action afterShow)
		{
			ShowFloater(null, afterShow);
		}

		public void ShowFloater(Form sender, Action afterShow)
		{
			const string defaultText = "GO GET YOUR BIZ!";
			var afterBack = new Action(ActivateMainForm);
			_floater.ShowFloater(sender ?? FormMain.Instance, defaultText, MasterWizardManager.Instance.DefaultLogo, afterShow, null, afterBack);
		}
	}
}