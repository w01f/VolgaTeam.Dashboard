﻿using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace NewBizWiz.Dashboard.TabHomeForms.Dashboard
{
	[ToolboxItem(false)]
	public partial class DashboardCode8Control : UserControl
	{
		public DashboardCode8Control()
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

		private void pbOnline_Click(object sender, EventArgs e)
		{
			FormMain.Instance.ribbonTabItemOnline.Select();
		}

		private void pbMobile_Click(object sender, EventArgs e)
		{
			FormMain.Instance.ribbonTabItemMobile.Select();
		}
	}
}