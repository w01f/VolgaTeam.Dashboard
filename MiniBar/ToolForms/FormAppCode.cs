using System;
using System.Windows.Forms;

namespace MiniBar.ToolForms
{
    public partial class FormAppCode : Form
    {
        public string Code
        {
            get
            {
                return textEditCode.EditValue != null ? textEditCode.EditValue.ToString() : string.Empty;
            }
        }

        public FormAppCode()
        {
            InitializeComponent();
            if ((base.CreateGraphics()).DpiX > 96)
            {
                laTitle.Font = new System.Drawing.Font(laTitle.Font.FontFamily, laTitle.Font.Size - 3, laTitle.Font.Style);
                laCode.Font = new System.Drawing.Font(laCode.Font.FontFamily, laCode.Font.Size - 2, laCode.Font.Style);
                textEditCode.Font = new System.Drawing.Font(textEditCode.Font.FontFamily, textEditCode.Font.Size - 4, textEditCode.Font.Style);
            }

            textEditCode.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
            textEditCode.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            textEditCode.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
        }

        private void textEditCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }
    }
}
