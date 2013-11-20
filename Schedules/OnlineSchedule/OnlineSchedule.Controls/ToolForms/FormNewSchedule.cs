using System.Windows.Forms;

namespace NewBizWiz.OnlineSchedule.Controls.PresentationClasses.ToolForms
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
				return null;
			}
		}

		private void textEditScheduleName_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				DialogResult = DialogResult.OK;
				Close();
			}
		}
	}
}