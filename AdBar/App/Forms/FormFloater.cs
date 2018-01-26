using System;
using System.Drawing;
using System.Windows.Forms;
using Asa.Bar.App.Configuration;
using Asa.Common.Core.Extensions;
using Asa.Common.Core.Helpers;

namespace Asa.Bar.App.Forms
{
	public partial class FormFloater : Form
	{
		private Rectangle _dragStartRectangle = Rectangle.Empty;

		public FormFloater()
		{
			InitializeComponent();

			BackColor = AppManager.Instance.Settings.Config.FloaterBorderColor;
			pnMain.BackColor = AppManager.Instance.Settings.Config.FloaterBackColor;

			if (ResourceManager.Instance.IconFile.ExistsLocal())
				notifyIcon.Icon = new Icon(ResourceManager.Instance.IconFile.LocalPath);
			if (ResourceManager.Instance.FloaterLogoFile.ExistsLocal())
			{
				var originanImage = Image.FromFile(ResourceManager.Instance.FloaterLogoFile.LocalPath);
				if (originanImage.Width > pictureBoxLogo.Width)
					pictureBoxLogo.Image = originanImage.Resize(new Size(pictureBoxLogo.Width, originanImage.Height));
				else
					pictureBoxLogo.Image = originanImage;
			}
			if (ResourceManager.Instance.FloaterCancelImageFile.ExistsLocal())
				pictureBoxExit.Image = Image.FromFile(ResourceManager.Instance.FloaterCancelImageFile.LocalPath);
			if (ResourceManager.Instance.ExpandFormImageFile.ExistsLocal())
				pictureBoxExpand.Image = Image.FromFile(ResourceManager.Instance.ExpandFormImageFile.LocalPath);
			if (ResourceManager.Instance.DockFloaterImageFile.ExistsLocal())
				pictureBoxDock.Image = Image.FromFile(ResourceManager.Instance.DockFloaterImageFile.LocalPath);

			pictureBoxLogo.Buttonize();
			pictureBoxExpand.Buttonize();
			pictureBoxDock.Buttonize();
			pictureBoxExit.Buttonize();

			if (!AppManager.Instance.Settings.Config.ShowDockFloaterButton)
			{
				pictureBoxExpand.Top = pictureBoxDock.Top;
				pictureBoxDock.Visible = false;
			}
		}

		private void OnFormShown(object sender, EventArgs e)
		{
			Move += OnFormMove;
		}

		private void OnFormClosed(object sender, FormClosedEventArgs e)
		{
			notifyIcon.Visible = false;
		}

		private void OnFormMove(object sender, EventArgs e)
		{
			AppManager.Instance.Settings.UserSettings.FloaterLocationLeft = Left;
			AppManager.Instance.Settings.UserSettings.FloaterLocationTop = Top;
			AppManager.Instance.Settings.UserSettings.Save();
		}

		private void OnExitButtonClick(object sender, EventArgs e)
		{
			DialogResult = DialogResult.No;
		}

		private void OnExpandButtonClick(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Yes;
		}

		private void OnDockButtonClick(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Abort;
		}

		private void OnButtonMouseMove(object sender, MouseEventArgs e)
		{
			if (e.Button != MouseButtons.Left) return;
			if (_dragStartRectangle.IsEmpty) return;
			if (!_dragStartRectangle.Contains(e.Location))
			{
				WinAPIHelper.ReleaseCapture();
				WinAPIHelper.SendMessage(Handle, WinAPIHelper.WM_NCLBUTTONDOWN, WinAPIHelper.HTCAPTION, IntPtr.Zero);
			}
		}

		private void OnButtonMouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button != MouseButtons.Left) return;
			_dragStartRectangle = new Rectangle(
				new Point(
					e.X - (SystemInformation.DragSize.Width / 2),
					e.Y - (SystemInformation.DragSize.Height / 2)),
				SystemInformation.DragSize);
		}

		private void OnButtonMouseUp(object sender, MouseEventArgs e)
		{
			_dragStartRectangle = Rectangle.Empty;
		}

		private void OnToolStripMenuItemCenterScreenClick(object sender, EventArgs e)
		{
			var screen = Screen.PrimaryScreen;
			Top = screen.Bounds.Height / 2;
			Left = (screen.Bounds.Width - Width) / 2;
		}

		private void OnToolStripMenuItemDockClick(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Abort;
		}
	}
}