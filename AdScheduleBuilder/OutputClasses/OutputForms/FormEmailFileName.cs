using System.Windows.Forms;

namespace AdScheduleBuilder.OutputClasses.OutputForms
{
    public partial class FormEmailFileName : Form
    {
        public FormEmailFileName()
        {
            InitializeComponent();
        }

        public string FileName
        {
            get 
            {
                if (textEditFileName.EditValue != null)
                    return textEditFileName.EditValue.ToString();
                else
                    return null;
            }
        }
    }
}
