using System;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using EO.Internal;
using Font = System.Drawing.Font;

namespace NewBizWiz.CommonGUI.ToolForms
{
	public partial class FormProgress : MetroForm
	{
		private readonly static FormProgress _instance = new FormProgress();
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
			_instance.Show();
			_queueCount++;
			Application.DoEvents();
		}

		public static void CloseProgress()
		{
			_queueCount--;
			if (_queueCount == 0)
				_instance.Hide();
			Application.DoEvents();
		}

		public static void SetTitle(string text, bool withDetails = false)
		{
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
			_instance.Invoke(new MethodInvoker(() =>
			{
				_instance.laDetails.Text = text;
				Application.DoEvents();
			}));
		}
	}
}