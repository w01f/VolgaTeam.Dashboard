using System;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Metro;

namespace NewBizWiz.CommonGUI.Floater
{
	public partial class FormFloater : MetroForm
	{
		public FormFloater(int x, int y, Image logo, string text)
		{
			InitializeComponent();
			Top = y;
			Left = x - Width;
			buttonXBack.Image = logo;
			Text = String.IsNullOrEmpty(text) ? "GO GET YOUR BIZ!" : text;
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

		private void FormFloater_Shown(object sender, EventArgs e)
		{
			ControlBox = false;
			Size = new Size(345, 144);
		}
	}
}