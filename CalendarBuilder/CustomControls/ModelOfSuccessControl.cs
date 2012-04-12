using System;
using System.Drawing;
using System.Windows.Forms;

namespace CalendarBuilder.CustomControls
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class ModelOfSuccessControl : UserControl
    {
        BusinessClasses.SuccessModel _parent = null;

        public ModelOfSuccessControl(BusinessClasses.SuccessModel parent)
        {
            InitializeComponent();
            this.Dock = DockStyle.Top;
            _parent = parent;
            laIndex.Text = (_parent.Index + 1).ToString() + ".";
            laLink.Text = _parent.Name;
            laDescription.Text = _parent.Description;
        }

        public void UpdateHeight()
        {
            Size labelSize = new Size(laDescription.Width, Int32.MaxValue);
            laDescription.Height = TextRenderer.MeasureText(laDescription.Text, laDescription.Font, labelSize, TextFormatFlags.WordBreak).Height;
            this.Height = laDescription.Height + laDescription.Top + 20;
        }

        private void laLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(_parent.Link);
            }
            catch
            {
            }
        }

        private void laDescription_MouseMove(object sender, MouseEventArgs e)
        {
            this.Parent.Focus();
        }
    }
}
