using System;
using System.IO;
using System.Windows.Forms;
using Asa.Common.Core.Configuration;
using Asa.Common.Core.Objects.FormStyle;

namespace Asa.Common.GUI.ToolForms
{
	public partial class FormDownloadProgress : Form
	{
		private static FormDownloadProgress _instance = new FormDownloadProgress();

		private FormDownloadProgress()
		{
			InitializeComponent();

			var styleSettings = new StartFormStyleConfiguration();
			styleSettings.Load(Path.Combine(ResourceManager.Instance.AppRootFolderPath, "sync_color.xml"));

			BackColor = styleSettings.SyncBorderColor ?? BackColor;
			panelMain.BackColor = styleSettings.SyncBackColor ?? panelMain.BackColor;
			laTitle.ForeColor = laDetails.ForeColor = styleSettings.SyncTextColor ?? laTitle.ForeColor;
			circularProgress.ProgressColor = styleSettings.SyncCircleColor ?? circularProgress.ProgressColor;
			circularProgress.ProgressBarType = (DevComponents.DotNetBar.eCircularProgressType)((styleSettings.SyncCircleStyle ?? 2) - 1);
			circularProgress.AnimationSpeed = styleSettings.SyncCircleSpeed ?? 150;
		}

		public static void ShowProgress(Form parent)
		{
			if (_instance == null)
				_instance = new FormDownloadProgress();
			_instance.Left = parent.Left + (parent.Width - _instance.Width - 20);
			_instance.Top = parent.Top + (parent.Height - _instance.Height - 20);
			if (_instance.InvokeRequired)
				_instance.BeginInvoke(new MethodInvoker(() =>
				{
					_instance.Show();
					Application.DoEvents();
				}));
			else
				_instance.Show();
			Application.DoEvents();
		}

		public static void CloseProgress()
		{
			if (_instance == null) return;
			if (_instance.InvokeRequired)
				_instance.BeginInvoke(new MethodInvoker(() =>
				{
					_instance.Close();
					_instance = null;
					Application.DoEvents();
				}));
			else
			{
				_instance.Close();
				_instance = null;
			}
			Application.DoEvents();
		}

		public static void SetTitle(string text)
		{
			if (_instance == null)
				_instance = new FormDownloadProgress();
			if (_instance.InvokeRequired)
				_instance.BeginInvoke(new MethodInvoker(() =>
				{
					_instance.laTitle.Text = text;
					Application.DoEvents();
					SetDetails(String.Empty);
					Application.DoEvents();
				}));
			else
			{
				_instance.laTitle.Text = text;
				Application.DoEvents();
				SetDetails(String.Empty);
				Application.DoEvents();
			}
		}

		public static void SetDetails(string text)
		{
			if (_instance == null)
				_instance = new FormDownloadProgress();
			if (_instance.InvokeRequired)
				_instance.BeginInvoke(new MethodInvoker(() =>
			{
				_instance.laDetails.Text = text;
				if (!String.IsNullOrEmpty(text))
					_instance.laDetails.BringToFront();
				else
					_instance.laDetails.SendToBack();
				Application.DoEvents();
			}));
			else
			{
				_instance.laDetails.Text = text;
				if (!String.IsNullOrEmpty(text))
					_instance.laDetails.BringToFront();
				else
					_instance.laDetails.SendToBack();
				Application.DoEvents();
			}
		}

		private void OnFormShown(object sender, EventArgs e)
		{
			laTitle.Focus();
			circularProgress.IsRunning = true;
		}
	}
}