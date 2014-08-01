using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;

namespace NewBizWiz.CommonGUI.ToolForms
{
	public partial class FormNewSchedule : MetroForm
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