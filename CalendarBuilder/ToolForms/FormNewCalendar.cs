using System.Windows.Forms;

namespace CalendarBuilder.ToolForms
{
    public partial class FormNewCalendar : Form
    {
        public FormNewCalendar()
        {
            InitializeComponent();
        }

        public string ScheduleName
        {
            get 
            {
                if (textEditScheduleName.EditValue != null)
                    return textEditScheduleName.EditValue.ToString();
                else
                    return null;
            }
        }

        private void textEditScheduleName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }
    }
}
