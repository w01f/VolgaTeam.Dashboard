﻿using System.Windows.Forms;

namespace RadioScheduleBuilder.ToolForms
{
    public partial class FormNewSchedule : Form
    {
        public FormNewSchedule()
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
