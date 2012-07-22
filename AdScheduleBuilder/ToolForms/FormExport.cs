using System.Windows.Forms;

namespace AdScheduleBuilder.ToolForms
{
    public partial class FormExport : Form
    {
        public bool BuildAdvanced
        {
            get
            {
                return checkEditAdvanced.Checked;
            }
        }

        public bool BuildGraphic
        {
            get
            {
                return checkEditGraphic.Checked;
            }
        }

        public bool BuildSimple
        {
            get
            {
                return checkEditSimple.Checked;
            }
        }

        public FormExport()
        {
            InitializeComponent();
            if ((base.CreateGraphics()).DpiX > 96)
            {
                laCalendarType.Font = new System.Drawing.Font(laCalendarType.Font.Name, laCalendarType.Font.Size - 3, laCalendarType.Font.Style);
            }
        }
    }
}
