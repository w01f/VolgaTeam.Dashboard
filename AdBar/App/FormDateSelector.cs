using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AdBAR
{
    public partial class FormDateSelector : Form
    {
        public DateTime DateTime { get; set; }

        public FormDateSelector(DateTime dateTime)
        {
            

            InitializeComponent();
            dateTimeInputTime.MinDate = DateTime.Now;

            DateTime = dateTime;
            dateTimeInputTime.Value = dateTimeInputTime.MinDate.CompareTo(DateTime) > 0 ? dateTimeInputTime.MinDate : DateTime;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            DateTime = dateTimeInputTime.Value;
            DialogResult = DialogResult.OK;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
