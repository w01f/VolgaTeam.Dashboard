using System.Windows.Forms;
using NewBizWiz.Core.Common;

namespace NewBizWiz.AdSchedule.Controls.ToolForms
{
	public partial class FormExportSchedule : Form
	{
		public FormExportSchedule()
		{
			InitializeComponent();
			textEditScheduleName.Enter += Utilities.Instance.Editor_Enter;
			textEditScheduleName.MouseDown += Utilities.Instance.Editor_MouseDown;
			textEditScheduleName.MouseUp += Utilities.Instance.Editor_MouseUp;
		}

		public string ScheduleName
		{
			get
			{
				if (textEditScheduleName.EditValue != null)
					return textEditScheduleName.EditValue.ToString();
				return null;
			}
			set
			{
				textEditScheduleName.EditValue = value;
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