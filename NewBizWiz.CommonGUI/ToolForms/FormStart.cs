using System;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;

namespace Asa.CommonGUI.ToolForms
{
	public partial class FormStart : MetroForm
	{
		private readonly static FormStart _instance = new FormStart();
		private static int _queueCount;
		private const string GrayTextFormat = "<font color=\"#8C8C8C\">{0}</font>";

		private FormStart()
		{
			InitializeComponent();
			TopMost = true;
			if ((CreateGraphics()).DpiX > 96)
			{
				laTitle.Font = new Font(laTitle.Font.FontFamily, laTitle.Font.Size - 2, laTitle.Font.Style);
			}
		}

		private void FormProgress_Shown(object sender, EventArgs e)
		{
			laTitle.Focus();
			circularProgress.IsRunning = true;
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
						Application.DoEvents();
					}));
				else
					_instance.Hide();
			}
			if (_queueCount > 0)
				_queueCount--;
			Application.DoEvents();
		}

		public static void SetTitle(string text, string description)
		{
			_instance.Invoke(new MethodInvoker(() =>
			{
				_instance.laTitle.Text = text;
				_instance.labelXDescription.Text = String.Format(GrayTextFormat, description);
				SetDetails(String.Empty);
				Application.DoEvents();
			}));
		}

		public static void SetDetails(string text)
		{
			_instance.Invoke(new MethodInvoker(() =>
			{
				_instance.labelXDetails.Text = String.Format(GrayTextFormat, text);
				Application.DoEvents();
			}));
		}
	}
}