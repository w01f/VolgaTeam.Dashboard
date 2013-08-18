﻿using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace NewBizWiz.Dashboard.TabHomeForms.Dashboard
{
	[ToolboxItem(false)]
	public partial class DashboardTV : UserControl
	{
		public DashboardTV()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
		}

		#region Picture Box Clicks Habdlers
		/// <summary>
		/// Buttonize the PictureBox 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pictureBox_MouseDown(object sender, MouseEventArgs e)
		{
			var pic = (PictureBox)(sender);
			pic.Top += 1;
		}

		private void pictureBox_MouseUp(object sender, MouseEventArgs e)
		{
			var pic = (PictureBox)(sender);
			pic.Top -= 1;
		}
		#endregion

		private void pbSellerPoint_Click(object sender, EventArgs e)
		{
			FormMain.Instance.ribbonTabItemTV.Select();
		}

		private void pbOnline_Click(object sender, EventArgs e)
		{
			FormMain.Instance.ribbonTabItemOnline.Select();
		}

		private void pbCalendar_Click_1(object sender, EventArgs e)
		{
			FormMain.Instance.ribbonTabItemCalendar.Select();
		}
	}
}