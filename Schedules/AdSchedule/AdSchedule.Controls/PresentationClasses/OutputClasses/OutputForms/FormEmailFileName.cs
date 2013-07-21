using System.Windows.Forms;

namespace NewBizWiz.AdSchedule.Controls.PresentationClasses.OutputClasses.OutputForms
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
				return null;
			}
		}
	}
}