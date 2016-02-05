using System;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;

namespace Asa.CommonGUI.ToolForms
{
	public partial class FormProgress : MetroForm
	{
		private static FormProgress _instance;
		private static int _queueCount;

		private FormProgress()
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
			if (_instance == null)
				_instance = new FormProgress();
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
						_instance.Close();
						Application.DoEvents();
					}));
				else
					_instance.Close();
				_instance = null;
			}
			if (_queueCount > 0)
				_queueCount--;
			Application.DoEvents();
		}

		public static void Destroy()
		{
			//_instance.Dispose();
		}

		public static void SetTitle(string text, bool withDetails = false)
		{
			if (_instance == null)
				_instance = new FormProgress();
			_instance.Invoke(new MethodInvoker(() =>
			{
				_instance.laTitle.Text = text;
				_instance.laDetails.Visible = withDetails;
				SetDetails(String.Empty);
				Application.DoEvents();
			}));
		}

		public static void SetDetails(string text)
		{
			if (_instance == null)
				_instance = new FormProgress();
			_instance.Invoke(new MethodInvoker(() =>
			{
				_instance.laDetails.Text = text;
				Application.DoEvents();
			}));
		}
	}
}