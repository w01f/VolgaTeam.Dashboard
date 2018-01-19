using System;
using System.Drawing;
using System.Windows.Forms;
using Asa.Common.Core.Helpers;
using DevComponents.DotNetBar;

namespace Asa.Common.GUI.Floater
{
	public partial class FormFloater : Form
	{
		public FormFloater(int x, int y, string text, Image logo)
		{
			InitializeComponent();
			Top = y;
			Left = x - Width;
			buttonXBack.Image = logo;
			labelCaption.Text = String.IsNullOrEmpty(text) ? "adSALESapps.com" : text;
			superTooltip.SetSuperTooltip(buttonXBack, new SuperTooltipInfo("Restore", "", String.Format("Restore {0} Application", String.IsNullOrEmpty(text) ? "adSALESapps Dashboard" : text), null, null, eTooltipColor.Gray));
		}

		private void buttonItemHide_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.No;
		}

		private void buttonItemBack_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Yes;
		}

		private void OnCaptionMouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button != MouseButtons.Left) return;
			WinAPIHelper.ReleaseCapture();
			WinAPIHelper.SendMessage(Handle, WinAPIHelper.WM_NCLBUTTONDOWN, WinAPIHelper.HTCAPTION, IntPtr.Zero);
		}
	}
}