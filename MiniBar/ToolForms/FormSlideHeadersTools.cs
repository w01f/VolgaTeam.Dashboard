using System;
using System.Drawing;
using System.Windows.Forms;

namespace MiniBar.ToolForms
{
    public partial class FormSlideHeadersTools : Form
    {
        public FormSlideHeadersTools()
        {
            InitializeComponent();
            if ((base.CreateGraphics()).DpiX > 96)
            {
                laTitle.Font = new Font(laTitle.Font.FontFamily, laTitle.Font.Size - 2, laTitle.Font.Style);
                buttonXAdd.Font = new Font(buttonXAdd.Font.FontFamily, buttonXAdd.Font.Size - 2, buttonXAdd.Font.Style);
                buttonXReplace.Font = new Font(buttonXReplace.Font.FontFamily, buttonXReplace.Font.Size - 2, buttonXReplace.Font.Style);
                buttonXDelete.Font = new Font(buttonXDelete.Font.FontFamily, buttonXDelete.Font.Size - 2, buttonXDelete.Font.Style);
                buttonXClose.Font = new Font(buttonXClose.Font.FontFamily, buttonXClose.Font.Size - 2, buttonXClose.Font.Style);
            }
        }

        private void buttonXClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonXAdd_Click(object sender, EventArgs e)
        {
            this.TopMost = false;
            InteropClasses.PowerPointHelper.Instance.AddSlideHeaderOnActiveSlide();
            this.TopMost = true;
        }

        private void buttonXReplace_Click(object sender, EventArgs e)
        {
            if (AppManager.Instance.ShowWarningQuestion("All Slide Headers are about to be replaced in this Presentation.\nDo you want to Proceed?") == System.Windows.Forms.DialogResult.Yes)
            {
                this.TopMost = false;
                InteropClasses.PowerPointHelper.Instance.AddSlideHeaderOnAllSlides();
                this.TopMost = true;
            }
        }

        private void buttonXDelete_Click(object sender, EventArgs e)
        {
            if (AppManager.Instance.ShowWarningQuestion("All Slide Headers will be Removed from this presentation.\nDo you want to continue?") == System.Windows.Forms.DialogResult.Yes)
            {
                this.TopMost = false;
                InteropClasses.PowerPointHelper.Instance.RemoveSlideHeaderFromAllSlides();
                this.TopMost = true;
            }
        }
    }
}
