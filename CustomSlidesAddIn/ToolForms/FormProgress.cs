using System.Windows.Forms;

namespace CustomSlidesAddIn.ToolForms
{
    public partial class FormProgress : Form
    {
        public FormProgress()
        {
            InitializeComponent();
            if (ConfigurationClasses.SettingsManager.Instance.HighDPI)
                laProgress.Font = new System.Drawing.Font(laProgress.Font.FontFamily, laProgress.Font.Size - 2, laProgress.Font.Style);
        }
    }
}