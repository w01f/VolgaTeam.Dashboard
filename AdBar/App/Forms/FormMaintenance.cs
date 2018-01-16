using System;
using System.Windows.Forms;

namespace Asa.Bar.App.Forms
{
	public partial class FormMaintenance : Form
	{
		public FormMaintenance()
		{
			InitializeComponent();

			BackColor = AppManager.Instance.Settings.Config.SplashBorderColor;
			panelMain.BackColor = AppManager.Instance.Settings.Config.SplashBackColor;
			laTitle.ForeColor = AppManager.Instance.Settings.Config.SplashTextColor;

			laTitle.Text = AppManager.Instance.Settings.MaintenanceConfig.InfoText ?? laTitle.Text;

			timer.Interval = AppManager.Instance.Settings.MaintenanceConfig.InfoDelay * 1000;

			Shown += OnFormShown;
		}

		private void OnFormShown(Object sender, EventArgs e)
		{
			timer.Start();
		}

		private void OnTimerTick(object sender, EventArgs e)
		{
			timer.Stop();
			Close();
		}

		#region Drag and Move Form Processing
		private const int WM_NCHITTEST = 0x84;
		private const int HTCLIENT = 0x1;
		private const int HTCAPTION = 0x2;
		protected override void WndProc(ref Message m)
		{
			switch (m.Msg)
			{
				case WM_NCHITTEST:
					base.WndProc(ref m);
					if ((int)m.Result == HTCLIENT)
					{
						m.Result = (IntPtr)HTCAPTION;
					}

					return;
			}

			base.WndProc(ref m);
		}
		#endregion
	}
}
