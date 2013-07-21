using System.Drawing;
using System.Windows.Forms;

namespace NewBizWiz.AdSchedule.Controls.ToolForms
{
    public partial class FormCloneSchedule : Form
    {
        public FormCloneSchedule()
        {
            InitializeComponent();
            if ((base.CreateGraphics()).DpiX > 96)
            {
                laHeader.Font = new Font(laHeader.Font.FontFamily, laHeader.Font.Size - 3, laHeader.Font.Style);
                buttonXCancel.Font = new Font(buttonXCancel.Font.FontFamily, buttonXCancel.Font.Size - 2, buttonXCancel.Font.Style);
                buttonXOK.Font = new Font(buttonXOK.Font.FontFamily, buttonXOK.Font.Size - 2, buttonXOK.Font.Style);
            }
        }
    }
}
