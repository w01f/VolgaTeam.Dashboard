using System;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;

namespace Asa.Reset
{
	public partial class FormProgress : MetroForm
	{
		public FormProgress()
		{
			InitializeComponent();
		}

		private void OnFormShown(object sender, EventArgs e)
		{
			InitGUIColors();
			laTitle.Focus();
			circularProgress.IsRunning = true;
		}

		private void InitGUIColors()
		{
			pnBackground.BackColor = Color.SandyBrown;
			
			laTitle.BackColor = Color.SandyBrown;
			laTitle.ForeColor = Color.White;
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
