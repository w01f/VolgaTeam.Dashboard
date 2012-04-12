using System;
using System.Drawing;
using System.Windows.Forms;

namespace MiniBar.ToolForms
{
    public partial class FormPageNumbersTools : Form
    {
        public FormPageNumbersTools()
        {
            InitializeComponent();
            if ((base.CreateGraphics()).DpiX > 96)
            {
                laTitle.Font = new Font(laTitle.Font.FontFamily, laTitle.Font.Size - 2, laTitle.Font.Style);
                buttonXAdd.Font = new Font(buttonXAdd.Font.FontFamily, buttonXAdd.Font.Size - 2, buttonXAdd.Font.Style);
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
            InteropClasses.PowerPointHelper.Instance.AddPageNumbers();
            this.TopMost = true;
        }

        private void buttonXDelete_Click(object sender, EventArgs e)
        {
            this.TopMost = false;
            InteropClasses.PowerPointHelper.Instance.RemovePageNumbers();
            this.TopMost = true;
        }
    }
}
