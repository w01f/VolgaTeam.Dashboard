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
				laDetails.Font = new Font(laDetails.Font.FontFamily, laDetails.Font.Size - 1, laDetails.Font.Style);
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

		public static void ShowProgress(string title, Action backgroundAction, bool runInMainThread = true)
		{
			using (var form = new FormProgress())
			{
				form.laTitle.Text = title;
				form.laDetails.Visible = false;
				form.Shown += async (o, e) =>
				{
					if (_mainForm.InvokeRequired)
						_mainForm.BeginInvoke(new MethodInvoker(Application.DoEvents));

					if (runInMainThread)
					{
						var taskCompleted = false;

#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
						Task.Run(() =>
						{
							if (_mainForm.InvokeRequired)
								_mainForm.BeginInvoke(new MethodInvoker(() =>
								{
									Application.DoEvents();
									backgroundAction();
								}));
							else
								backgroundAction();
							taskCompleted = true;
						});
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

						await Task.Run(() =>
						{
							while (!taskCompleted)
							{
								Thread.Sleep(100);
								if (_mainForm.InvokeRequired)
									_mainForm.BeginInvoke(new MethodInvoker(Application.DoEvents));
							}
						});
					}
					else
						await Task.Run(backgroundAction);
					form.Close();
				};
				form.ShowDialog(_mainForm);
			}
		}

		public static void ShowProgress(Form parentForm = null)
		{
			parentForm = parentForm ?? _mainForm;
			if (_instance == null)
				_instance = new FormProgress();
			_instance.Closed += (o, e) =>
			{
				_instance.Dispose();
				_instance = null;
			};
			if (parentForm != null)
			{
				_instance.StartPosition = FormStartPosition.Manual;
				_instance.Top = parentForm.Top + (parentForm.Height - _instance.Height) / 2;
				_instance.Left = parentForm.Left + (parentForm.Width - _instance.Width) / 2;
				_instance.Show(parentForm);
			}
			else
			{
				_instance.StartPosition = FormStartPosition.CenterScreen;
				_instance.Show();
			}
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
				}));
			else
			{
				_instance.Close();
				_instance = null;
			}
		}

		public static void SetTitle(string text, bool withDetails = false)
		{
			if (_instance == null)
				_instance = new FormProgress();
			_instance.BeginInvoke(new MethodInvoker(() =>
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
			_instance.BeginInvoke(new MethodInvoker(() =>
			{
				_instance.laDetails.Text = text;
				Application.DoEvents();
			}));
		}
	}
}