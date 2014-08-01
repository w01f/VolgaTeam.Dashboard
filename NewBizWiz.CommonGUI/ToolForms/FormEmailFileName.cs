using DevComponents.DotNetBar.Metro;

namespace NewBizWiz.CommonGUI.ToolForms
{
	public partial class FormEmailFileName : MetroForm
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