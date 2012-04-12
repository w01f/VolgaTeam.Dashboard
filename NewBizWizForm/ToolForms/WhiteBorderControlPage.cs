using System.Drawing;
using System.Windows.Forms;

namespace NewBizWizForm.ToolForms
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class WhiteBorderControl : UserControl
    {
        public AppManager.EmptyParametersDelegate OutputClick { get; set; }
        public AppManager.EmptyParametersDelegate SavedFilesClick { get; set; }

        public WhiteBorderControl()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            AppManager.Instance.SetClickEventHandler(this);
            if ((base.CreateGraphics()).DpiX > 96)
            {
                buttonXSavedFiles.Font = new Font(buttonXSavedFiles.Font.FontFamily, buttonXSavedFiles.Font.Size - 2, buttonXSavedFiles.Font.Style);
            }
        }

        private void buttonXOutput_Click(object sender, System.EventArgs e)
        {
            if (this.OutputClick != null)
                OutputClick();
        }

        public void EnableOutputButton(bool enable)
        {
            FormMain.Instance.ribbonBarPowerPoint.Enabled = enable;
            buttonXOutput.Enabled = enable;
        }

        public void EnableSavedFilesButton(bool enable)
        {
            buttonXSavedFiles.Enabled = enable;
        }

        private void buttonXSavedFiles_Click(object sender, System.EventArgs e)
        {
            if (this.SavedFilesClick != null)
                SavedFilesClick();
        }
    }
}
