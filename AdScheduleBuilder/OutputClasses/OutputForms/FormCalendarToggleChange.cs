using System.Drawing;
using System.Windows.Forms;

namespace AdScheduleBuilder.OutputClasses.OutputForms
{
    public partial class FormCalendarToggleChange : Form
    {
        public FormCalendarToggleChange()
        {
            InitializeComponent();
            if ((base.CreateGraphics()).DpiX > 96)
            {
                buttonXDisable.Font = new Font(buttonXDisable.Font.FontFamily, buttonXDisable.Font.Size - 3, buttonXDisable.Font.Style);
                buttonXCancel.Font = new Font(buttonXCancel.Font.FontFamily, buttonXCancel.Font.Size - 3, buttonXCancel.Font.Style);
                buttonXEdit.Font = new Font(buttonXEdit.Font.FontFamily, buttonXEdit.Font.Size - 3, buttonXEdit.Font.Style);
            }
        }
    }
}
