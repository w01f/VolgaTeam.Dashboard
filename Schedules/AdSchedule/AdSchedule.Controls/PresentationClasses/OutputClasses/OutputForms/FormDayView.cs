using System;
using System.Windows.Forms;

namespace NewBizWiz.AdSchedule.Controls.PresentationClasses.OutputClasses.OutputForms
{
	public partial class FormDayView : Form
	{
		public FormDayView()
		{
			InitializeComponent();
			radioGroupCustomNote.Properties.Items.Clear();
			radioGroupDeadline.Properties.Items.Clear();
		}

		private void checkEditUseComment_CheckedChanged(object sender, EventArgs e)
		{
			memoEditCustomNote.Enabled = checkEditUseCustomNote.Checked;
			if (!checkEditUseCustomNote.Checked)
				memoEditCustomNote.EditValue = null;
		}

		private void checkEditUseDeadline_CheckedChanged(object sender, EventArgs e)
		{
			memoEditDeadline.Enabled = checkEditUseDeadline.Checked;
			if (!checkEditUseDeadline.Checked)
				memoEditDeadline.EditValue = null;
		}
	}
}