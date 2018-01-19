using System;
using System.Drawing;
using System.Windows.Forms;
using Asa.Bar.App.Configuration;

namespace Asa.Bar.App.Forms
{
	public partial class FormSplash : Form
	{
		public FormSplash()
		{
			InitializeComponent();

			BackColor = AppManager.Instance.Settings.Config.SplashBorderColor;
			panelMain.BackColor = AppManager.Instance.Settings.Config.SplashBackColor;
			laTitle.ForeColor = AppManager.Instance.Settings.Config.SplashTextColor;

			if (ResourceManager.Instance.SplashLogoFile.ExistsLocal())
				pictureBoxLogo.Image = Image.FromFile(ResourceManager.Instance.SplashLogoFile.LocalPath);
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
