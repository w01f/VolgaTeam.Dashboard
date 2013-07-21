using System.Drawing;
using System.Windows.Forms;

namespace NewBizWiz.MiniBar.ToolForms
{
	public partial class FormAppCode : Form
	{
		public FormAppCode()
		{
			InitializeComponent();
			if ((base.CreateGraphics()).DpiX > 96)
			{
				laTitle.Font = new Font(laTitle.Font.FontFamily, laTitle.Font.Size - 3, laTitle.Font.Style);
				laCode.Font = new Font(laCode.Font.FontFamily, laCode.Font.Size - 2, laCode.Font.Style);
				textEditCode.Font = new Font(textEditCode.Font.FontFamily, textEditCode.Font.Size - 4, textEditCode.Font.Style);
			}

			textEditCode.MouseUp += FormMain.Instance.Editor_MouseUp;
			textEditCode.MouseDown += FormMain.Instance.Editor_MouseDown;
			textEditCode.Enter += FormMain.Instance.Editor_Enter;
		}

		public string Code
		{
			get { return textEditCode.EditValue != null ? textEditCode.EditValue.ToString() : string.Empty; }
		}

		private void textEditCode_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				DialogResult = DialogResult.OK;
				Close();
			}
		}
	}
}