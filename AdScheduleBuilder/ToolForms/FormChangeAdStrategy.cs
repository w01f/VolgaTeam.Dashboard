using System;
using System.Windows.Forms;

namespace AdScheduleBuilder.ToolForms
{
    public partial class FormChangeAdStrategy : Form
    {
        public FormChangeAdStrategy()
        {
            InitializeComponent();
        }

        private void rbSave_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSave.Checked )
            {
                ckDeleteAllAdNotes.Enabled = true;
                ckDeleteAllColorRates.Enabled = false;
                ckDeleteAllColorRates.Checked = false;
                ckDeleteAllDiscounts.Enabled = false;
                ckDeleteAllDiscounts.Checked = false;
            }
            else if (rbReset.Checked)
            {
                ckDeleteAllColorRates.Enabled = true;
                ckDeleteAllDiscounts.Enabled = true;
                ckDeleteAllAdNotes.Enabled = true;
            }
            else
            {
                ckDeleteAllAdNotes.Enabled = false;
                ckDeleteAllAdNotes.Checked = false;
                ckDeleteAllColorRates.Enabled = false;
                ckDeleteAllColorRates.Checked = false;
                ckDeleteAllDiscounts.Enabled = false;
                ckDeleteAllDiscounts.Checked = false;
            }
        }
    }
}
