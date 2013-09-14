using System;
using System.Drawing;
using System.Windows.Forms;

namespace NewBizWiz.CommonGUI.Floater
{
	public partial class FormFloater : Form
	{
		public FormFloater(int x, int y, Image logo, string text)
		{
			InitializeComponent();
			Top = y;
			Left = x - Width;
			buttonItemBack.Image = logo;
			ribbonBarBack.Text = text;
			if ((CreateGraphics()).DpiX > 96)
			{
				Font = new Font(Font.FontFamily, Font.Size - 1, Font.Style);
			}
		}

		private void buttonItemHide_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.No;
		}

		private void buttonItemBack_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Yes;
		}
	}
}