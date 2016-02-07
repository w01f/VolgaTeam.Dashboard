using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;

namespace Asa.Common.GUI.ToolForms
{
	public partial class FormProgress : MetroForm
	{
		private static FormProgress _instance;
		private static Form _mainForm;

		private FormProgress()
		{
			InitializeComponent();
			TopMost = true;
			if ((CreateGraphics()).DpiX > 96)
			{
				laTitle.Font = new Font(laTitle.Font.FontFamily, laTitle.Font.Size - 2, laTitle.Font.Style);
			}
		}

		public static void Init(Form mainForm)
		{
			_mainForm = mainForm;
		}

		private void FormProgress_Shown(object sender, EventArgs e)
		{
			laTitle.Focus();
			circularProgress.IsRunning = true;
		}

		public static void ShowProgress(string title, Action backgroundAction)
		{
			using (var form = new FormProgress())
			{
				form.laTitle.Text = title;
				form.laDetails.Visible = false;
				form.Shown += async (o, e) =>
				{
					if (_mainForm.InvokeRequired)
						_mainForm.Invoke(new MethodInvoker(Application.DoEvents));

					var taskCompleted = false;

					Task.Run(() =>
					{
						if (_mainForm.InvokeRequired)
							_mainForm.Invoke(new MethodInvoker(() =>
							{
								Application.DoEvents();
								backgroundAction();
							}));
						else
							backgroundAction();
						taskCompleted = true;
					});

					await Task.Run(() =>
					{
						while (!taskCompleted)
						{
							Thread.Sleep(100);
							if (_mainForm.InvokeRequired)
								_mainForm.Invoke(new MethodInvoker(Application.DoEvents));
						}
					});
					form.Close();
				};
				form.ShowDialog(_mainForm);
			}
		}

		public static void ShowProgress()
		{
			if (_instance == null)
				_instance = new FormProgress();
			_instance.Closed += (o, e) =>
			{
				_instance.Dispose();
				_instance = null;
			};
			_instance.Show(_mainForm);
		}

		public static void CloseProgress()
		{
			if (_instance.InvokeRequired)
				_instance.Invoke(new MethodInvoker(() => _instance.Close()));
			else
				_instance.Close();
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