using System;
using System.Drawing;
using System.Windows.Forms;
using NewBizWiz.Core.Common;

namespace NewBizWiz.Dashboard
{
	public partial class FormFloater : Form
	{
		public FormFloater(int inix, int iniy, int lastx, int lasty, Image defaultImage, string defaultText)
		{
			InitializeComponent();

			if (lastx == int.MinValue || lasty == int.MinValue)
			{
				Top = iniy;
				Left = inix - Width;
			}
			else
			{
				Top = lasty;
				Left = lastx;
			}

			if ((base.CreateGraphics()).DpiX > 96)
			{
				Font = new Font(Font.FontFamily, Font.Size - 1, Font.Style);
			}

			Text = SettingsManager.Instance.DashboardName;
			buttonItemDashboard.Image = defaultImage;
			ribbonBarDashboard.Text = defaultText;
		}

		private void buttonItemExit_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.No;
		}

		private void buttonItemDefaultStar_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Yes;
		}
	}
}