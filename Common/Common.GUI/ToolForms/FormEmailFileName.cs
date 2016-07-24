using DevComponents.DotNetBar.Metro;

namespace Asa.Common.GUI.ToolForms
{
	public partial class FormEmailFileName : MetroForm
	{
		public FormEmailFileName()
		{
			InitializeComponent();
		}

		public string FileName => textEditFileName.EditValue?.ToString();
	}
}