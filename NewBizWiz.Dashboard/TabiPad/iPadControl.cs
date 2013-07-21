using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace NewBizWiz.Dashboard.TabiPadForms
{
	[ToolboxItem(false)]
	public partial class iPadControl : UserControl
	{
		private static iPadControl _instance;

		private iPadControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			AppManager.Instance.SetClickEventHandler(this);
			if ((base.CreateGraphics()).DpiX > 96)
			{
				laTitle.Font = new Font(laTitle.Font.FontFamily, laTitle.Font.Size - 5, laTitle.Font.Style);
				laHint.Font = new Font(laHint.Font.FontFamily, laHint.Font.Size - 3, laHint.Font.Style);
			}
		}

		public static iPadControl Instance
		{
			get
			{
				if (_instance == null)
					_instance = new iPadControl();
				return _instance;
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
	}
}