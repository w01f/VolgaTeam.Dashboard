using System;
using System.Drawing;
using System.Windows.Forms;
using Asa.Core.Common;

namespace Asa.CommonGUI.ToolForms
{
	public partial class FormStart : Form
	{
		private readonly static FormStart _instance = new FormStart();
		private static int _queueCount;

		private FormStart()
		{
			InitializeComponent();
			if ((CreateGraphics()).DpiX > 96)
			{
				laTitle.Font = new Font(laTitle.Font.FontFamily, laTitle.Font.Size - 2, laTitle.Font.Style);
				laDetails.Font = new Font(laDetails.Font.FontFamily, laDetails.Font.Size - 2, laDetails.Font.Style);
			}

			Left = Screen.PrimaryScreen.WorkingArea.Width - Width - 20;
			Top = Screen.PrimaryScreen.WorkingArea.Height - Height - 20;

			pbCancel.MouseDown += (o, e) =>
			{
				var pic = (PictureBox)(o);
				pic.Top += 1;
			};

			pbCancel.MouseUp += (o, e) =>
			{
				var pic = (PictureBox)(o);
				pic.Top -= 1;
			};

			notifyIcon.Text = Utilities.Instance.Title;
			notifyIcon.BalloonTipText = Utilities.Instance.Title;
			toolStripMenuItemKillApp.Text = String.Format(toolStripMenuItemKillApp.Text, Utilities.Instance.Title);
		}

		public static void ShowProgress()
		{
			if (_instance.InvokeRequired)
				_instance.Invoke(new MethodInvoker(() =>
				{
					_instance.Show();
					Application.DoEvents();
				}));
			else
				_instance.Show();
			_queueCount++;
			Application.DoEvents();
		}

		public static void CloseProgress()
		{
			if (_queueCount <= 1)
			{
				if (_instance.InvokeRequired)
					_instance.Invoke(new MethodInvoker(() =>
					{
						_instance.Hide();
						_instance.HideTrayIcon();
						Application.DoEvents();
					}));
				else
				{
					_instance.HideTrayIcon();
					_instance.Hide();
				}
			}
			if (_queueCount > 0)
				_queueCount--;
			Application.DoEvents();
		}

		public static void SetTitle(string text)
		{
			_instance.Invoke(new MethodInvoker(() =>
			{
				_instance.laTitle.Text = text;
				SetDetails(String.Empty);
				Application.DoEvents();
			}));
		}

		public static void Destroy()
		{
			_instance.Dispose();
		}

		public static void SetDetails(string text)
		{
			_instance.Invoke(new MethodInvoker(() =>
			{
				_instance.laDetails.Text = text;
				if (!String.IsNullOrEmpty(text))
					_instance.laDetails.BringToFront();
				else
					_instance.laDetails.SendToBack();
				Application.DoEvents();
			}));
		}

		private void ShowTrayIcon()
		{
			Opacity = 0;
			notifyIcon.Visible = true;
			notifyIcon.BalloonTipText = laTitle.Text;
			notifyIcon.ShowBalloonTip(1);
		}

		private void HideTrayIcon()
		{
			notifyIcon.Visible = false;
			Opacity = 1;
		}

		private void FormProgress_Shown(object sender, EventArgs e)
		{
			laTitle.Focus();
			circularProgress.IsRunning = true;
		}

		private void pbCancel_Click(object sender, EventArgs e)
		{
			ShowTrayIcon();
		}

		private void toolStripMenuItemShowProgress_Click(object sender, EventArgs e)
		{
			HideTrayIcon();
		}

		private void toolStripMenuItemKillApp_Click(object sender, EventArgs e)
		{
			Environment.Exit(0);
		}
	}
}