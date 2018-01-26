using System;
using System.Drawing;
using System.Windows.Forms;
using Asa.Common.Core.Helpers;
using DevComponents.DotNetBar;

namespace Asa.Common.GUI.Floater
{
	public partial class FormFloater : Form
	{
		private Rectangle _dragStartRectangle = Rectangle.Empty;

		public FormFloater(int x, int y, string text, Image logo)
		{
			InitializeComponent();
			Top = y;
			Left = x - Width;
			buttonXBack.Image = logo;
			labelCaption.Text = String.IsNullOrEmpty(text) ? "adSALESapps.com" : text;
			superTooltip.SetSuperTooltip(buttonXBack, new SuperTooltipInfo("Restore", "", String.Format("Restore {0} Application", String.IsNullOrEmpty(text) ? "adSALESapps Dashboard" : text), null, null, eTooltipColor.Gray));
		}

		private void OnHideButtonClick(object sender, EventArgs e)
		{
			DialogResult = DialogResult.No;
		}

		private void OnBackButtonClick(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Yes;
		}

		private void OnCaptionMouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button != MouseButtons.Left) return;
			WinAPIHelper.ReleaseCapture();
			WinAPIHelper.SendMessage(Handle, WinAPIHelper.WM_NCLBUTTONDOWN, WinAPIHelper.HTCAPTION, IntPtr.Zero);
		}

		private void OnButtonMouseMove(object sender, MouseEventArgs e)
		{
			if (e.Button != MouseButtons.Left) return;
			if (_dragStartRectangle.IsEmpty) return;
			if (!_dragStartRectangle.Contains(e.Location))
			{
				WinAPIHelper.ReleaseCapture();
				WinAPIHelper.SendMessage(Handle, WinAPIHelper.WM_NCLBUTTONDOWN, WinAPIHelper.HTCAPTION, IntPtr.Zero);
			}
		}

		private void OnButtonMouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button != MouseButtons.Left) return;
			_dragStartRectangle = new Rectangle(
				new Point(
					e.X - (SystemInformation.DragSize.Width / 2),
					e.Y - (SystemInformation.DragSize.Height / 2)),
				SystemInformation.DragSize);
		}

		private void OnButtonMouseUp(object sender, MouseEventArgs e)
		{
			_dragStartRectangle = Rectangle.Empty;
		}
	}
}