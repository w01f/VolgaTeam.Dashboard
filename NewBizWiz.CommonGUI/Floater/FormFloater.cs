using System;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;

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
			ribbonBarBack.Text = String.IsNullOrEmpty(text) ? "GO GET YOUR BIZ!" : text;
			superTooltip.SetSuperTooltip(buttonItemBack, new SuperTooltipInfo("Restore", "", String.Format("Restore {0} Application", String.IsNullOrEmpty(text) ? "adSALESapps Dashboard" : text), null, null, eTooltipColor.Gray));
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