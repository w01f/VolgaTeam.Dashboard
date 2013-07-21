﻿using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using NewBizWiz.Core.Common;
using NewBizWiz.Dashboard.Properties;

namespace NewBizWiz.Dashboard.TabHomeForms.Dashboard
{
	[ToolboxItem(false)]
	public partial class DashboardCode4Control : UserControl
	{
		public DashboardCode4Control()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			if (!Directory.Exists(MasterWizardManager.Instance.SelectedWizard.TVScheduleSlideFolder) || Directory.GetDirectories(MasterWizardManager.Instance.SelectedWizard.TVScheduleSlideFolder).Length == 0)
			{
				pbTVSmall.Image = Resources.HomeTVSmallDisabled;
				pbTVSmall.Enabled = false;
			}
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

		private void pbNewspaper_Click(object sender, EventArgs e)
		{
			FormMain.Instance.ribbonTabItemNewspaper.Select();
		}

		private void pbOnline_Click(object sender, EventArgs e)
		{
			FormMain.Instance.ribbonTabItemOnline.Select();
		}

		private void pbMobile_Click(object sender, EventArgs e)
		{
			FormMain.Instance.ribbonTabItemMobile.Select();
		}

		private void pbTV_Click(object sender, EventArgs e)
		{
			if (pbTVSmall.Enabled)
				FormMain.Instance.ribbonTabItemTV.Select();
		}
	}
}