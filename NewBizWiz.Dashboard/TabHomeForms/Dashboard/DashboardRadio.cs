using System;
using System.ComponentModel;
using System.Windows.Forms;
using NewBizWiz.Core.Dashboard;

namespace NewBizWiz.Dashboard.TabHomeForms.Dashboard
{
	[ToolboxItem(false)]
	public partial class DashboardRadio : UserControl
	{
		public DashboardRadio()
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
			AppManager.Instance.RunProcess(SettingsManager.Instance.RadioSchedulePath);
			FormMain.Instance.Close();
		}

		private void pbOnline_Click(object sender, EventArgs e)
		{
			AppManager.Instance.RunProcess(SettingsManager.Instance.DigitalSchedulePath);
			FormMain.Instance.Close();
		}
	}
}