using System;
using System.ComponentModel;
using System.Windows.Forms;
using NewBizWiz.MiniBar.BusinessClasses;

namespace NewBizWiz.MiniBar.SettingsForms
{
	[ToolboxItem(false)]
	public partial class ApplicationDefinitionControl : UserControl
	{
		public ApplicationDefinitionControl()
		{
			InitializeComponent();
			Dock = DockStyle.Top;
		}

		public NBWApplication Application { get; set; }

		public void UpdateView()
		{
			pbLogo.Image = Application.Image;
			laTitle.Text = Application.Title;
		}

		private void pbLogo_Click(object sender, EventArgs e)
		{
			Application.CreateShortcut();
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
	}
}