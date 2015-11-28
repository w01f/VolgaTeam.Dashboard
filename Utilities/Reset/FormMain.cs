using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;

namespace Asa.Reset
{
	public partial class FormMain : MetroForm
	{
		public FormMain()
		{
			InitializeComponent();
		}

		private void OnFormShown(object sender, EventArgs e)
		{
			InitGUIColors();
		}

		private void InitGUIColors()
		{
			pnBackground.BackColor = Color.SandyBrown;

			laTitle.BackColor = Color.SandyBrown;
			laTitle.ForeColor = Color.White;

			laDescription.BackColor = Color.SandyBrown;
			laDescription.ForeColor = Color.White;
		}

		private void OnCancelClick(object sender, EventArgs e)
		{
			Close();
		}

		private void OnResetClick(object sender, EventArgs e)
		{
			Opacity = 0;
			using (var formWarning = new FormWarning())
			{
				if (formWarning.ShowDialog(this) == DialogResult.OK)
				{
					using (var formProgress = new FormProgress())
					{
						formProgress.Shown += async (o, args) =>
						{
							await Task.Run((Action)ResetHelper.ResetApp);
							formProgress.Close();
						};
						formProgress.ShowDialog(this);
					}
				}
			}
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
