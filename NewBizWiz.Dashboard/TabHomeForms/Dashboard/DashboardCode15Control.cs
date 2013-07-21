using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using NewBizWiz.Core.Common;
using NewBizWiz.Dashboard.Properties;

namespace NewBizWiz.Dashboard.TabHomeForms.Dashboard
{
	[ToolboxItem(false)]
	public partial class DashboardCode15Control : UserControl
	{
		public DashboardCode15Control()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			if (!Directory.Exists(MasterWizardManager.Instance.SelectedWizard.TVScheduleSlideFolder) || Directory.GetDirectories(MasterWizardManager.Instance.SelectedWizard.TVScheduleSlideFolder).Length == 0)
			{
				pbTV.Image = Resources.HomeTVDisabled;
				pbTV.Enabled = false;
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

		private void pbTV_Click(object sender, EventArgs e)
		{
			if (pbTV.Enabled)
				FormMain.Instance.ribbonTabItemTV.Select();
		}

		private void pbDigital_Click(object sender, EventArgs e)
		{
			FormMain.Instance.ribbonTabItemDigital.Select();
		}
	}
}