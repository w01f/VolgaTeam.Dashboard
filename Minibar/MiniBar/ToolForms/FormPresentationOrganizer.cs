using System;
using System.Drawing;
using System.Windows.Forms;
using NewBizWiz.MiniBar.InteropClasses;

namespace NewBizWiz.MiniBar.ToolForms
{
	public partial class FormPresentationOrganizer : Form
	{
		public FormPresentationOrganizer()
		{
			InitializeComponent();
			if ((CreateGraphics()).DpiX > 96)
			{
				laTitle.Font = new Font(laTitle.Font.FontFamily, laTitle.Font.Size - 2, laTitle.Font.Style);
				buttonXContentsAdd.Font = new Font(buttonXContentsAdd.Font.FontFamily, buttonXContentsAdd.Font.Size - 2, buttonXContentsAdd.Font.Style);
				buttonXContentsDelete.Font = new Font(buttonXContentsDelete.Font.FontFamily, buttonXContentsDelete.Font.Size - 2, buttonXContentsDelete.Font.Style);
				buttonXClose.Font = new Font(buttonXClose.Font.FontFamily, buttonXClose.Font.Size - 2, buttonXClose.Font.Style);
			}
		}

		private void buttonXAutoUpdate_Click(object sender, EventArgs e)
		{
			TopMost = false;
			MinibarPowerPointHelper.Instance.AddContents(true);
			MinibarPowerPointHelper.Instance.AddPageNumbers();
			TopMost = true;
		}

		private void buttonXAddContents_Click(object sender, EventArgs e)
		{
			TopMost = false;
			MinibarPowerPointHelper.Instance.AddContents();
			TopMost = true;
		}

		private void buttonXDeleteContents_Click(object sender, EventArgs e)
		{
			TopMost = false;
			MinibarPowerPointHelper.Instance.DeleteContents();
			TopMost = true;
		}

		private void buttonXHeaderAdd_Click(object sender, EventArgs e)
		{
			TopMost = false;
			MinibarPowerPointHelper.Instance.AddSlideHeaderOnActiveSlide();
			TopMost = true;
		}

		private void buttonXHeaderReplace_Click(object sender, EventArgs e)
		{
			if (AppManager.Instance.ShowWarningQuestion("All Slide Headers are about to be replaced in this Presentation.\nDo you want to Proceed?") == DialogResult.Yes)
			{
				TopMost = false;
				MinibarPowerPointHelper.Instance.AddSlideHeaderOnAllSlides();
				TopMost = true;
			}
		}

		private void buttonXHeaderDelete_Click(object sender, EventArgs e)
		{
			if (AppManager.Instance.ShowWarningQuestion("All Slide Headers will be Removed from this presentation.\nDo you want to continue?") == DialogResult.Yes)
			{
				TopMost = false;
				MinibarPowerPointHelper.Instance.RemoveSlideHeaderFromAllSlides();
				TopMost = true;
			}
		}

		private void buttonXNumbersAdd_Click(object sender, EventArgs e)
		{
			TopMost = false;
			MinibarPowerPointHelper.Instance.AddPageNumbers();
			TopMost = true;
		}

		private void buttonXNumbersDelete_Click(object sender, EventArgs e)
		{
			TopMost = false;
			MinibarPowerPointHelper.Instance.RemovePageNumbers();
			TopMost = true;
		}

		private void xtraTabControl_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
		{
			if (e.Page == xtraTabPageContents)
				laTitle.Text = "Add or Remove a Contents Slide";
			else if (e.Page == xtraTabPageHeader)
				laTitle.Text = "Add or Remove a Slide Header";
			else if (e.Page == xtraTabPageNumbers)
				laTitle.Text = "Add or Remove Page Numbers";
		}
	}
}