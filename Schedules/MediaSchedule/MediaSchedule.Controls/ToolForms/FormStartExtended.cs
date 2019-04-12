using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;
using Asa.Media.Controls.BusinessClasses.Managers;
using DevComponents.DotNetBar.Metro;
using DevExpress.Skins;
using DevExpress.XtraEditors;

namespace Asa.Media.Controls.ToolForms
{
	public partial class FormStartExtended : MetroForm
	{
		public FormStartExtended()
		{
			InitializeComponent();

			pictureEditNew.Buttonize();
			pictureEditOpen.Buttonize();
			pictureEditQuickEditSchedule.Buttonize();
			pictureEditExit.Buttonize();

			pictureEditLogo.Image = BusinessObjects.Instance.ImageResourcesManager.StartFormLogo ?? pictureEditLogo.Image;
			pictureEditNew.Image = BusinessObjects.Instance.ImageResourcesManager.StartFormNewImage;
			pictureEditOpen.Image = BusinessObjects.Instance.ImageResourcesManager.StartFormOpenImage;
			pictureEditQuickEditSchedule.Image = BusinessObjects.Instance.ImageResourcesManager.StartFormQuickEditScheduleImage;
			pictureEditExit.Image = BusinessObjects.Instance.ImageResourcesManager.StartFormCancelImage;

			pictureEditOpen.Enabled = !FileStorageManager.Instance.UseLocalMode ||
									  Directory.Exists(BusinessObjects.Instance.ScheduleManager.ContextPath);

			layoutControlItemNew.MaxSize = RectangleHelper.ScaleSize(layoutControlItemNew.MaxSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemNew.MinSize = RectangleHelper.ScaleSize(layoutControlItemNew.MinSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemOpen.MaxSize = RectangleHelper.ScaleSize(layoutControlItemOpen.MaxSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemOpen.MinSize = RectangleHelper.ScaleSize(layoutControlItemOpen.MinSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemQuickEditSchedule.MaxSize = RectangleHelper.ScaleSize(layoutControlItemQuickEditSchedule.MaxSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemQuickEditSchedule.MinSize = RectangleHelper.ScaleSize(layoutControlItemQuickEditSchedule.MinSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemExit.MaxSize = RectangleHelper.ScaleSize(layoutControlItemExit.MaxSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemExit.MinSize = RectangleHelper.ScaleSize(layoutControlItemExit.MinSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
		}

		private void OnNewClick(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Yes;
		}

		private void OnOpenClick(object sender, EventArgs e)
		{
			DialogResult = DialogResult.No;
		}

		private void OnQuickEditScheduleClick(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Abort;
		}

		private void OnExitClick(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
		}

		private void OnMouseHover(object sender, EventArgs e)
		{
			var pictureEdit = (PictureEdit)sender;
			pictureEdit.BackColor = BusinessObjects.Instance.FormStyleManager.Style.ToggleHoverColor ?? BackColor;
		}

		private void OnMouseLeave(object sender, EventArgs e)
		{
			var pictureEdit = (PictureEdit)sender;
			pictureEdit.BackColor = Color.White;
		}

		private void OnMouseMove(object sender, MouseEventArgs e)
		{
			OnMouseHover(sender, e);
		}
	}
}