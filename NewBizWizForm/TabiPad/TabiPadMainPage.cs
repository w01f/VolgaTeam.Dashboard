using System.Windows.Forms;

namespace NewBizWizForm.TabiPadForms
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class TabiPadMainPage : UserControl
    {
        private static TabiPadMainPage _instance;

        private TabiPadMainPage()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            AppManager.Instance.SetClickEventHandler(this);
        }

        public static TabiPadMainPage Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new TabiPadMainPage();
                return _instance;
            }
        }

        public void UpdatePageAccordingToggledButton()
        {
            Control parent = this.Parent;
            this.Parent = null;
            this.Controls.Clear();
            if (FormMain.Instance.buttonItemiPad != null && FormMain.Instance.buttonItemiPad.Checked)
            {
                this.Controls.Add(iPadControl.Instance);
            }
            else
            {
                ToolForms.WhiteBorderControl borderedControl = new NewBizWizForm.ToolForms.WhiteBorderControl();
                this.Controls.Add(borderedControl);
                Control parentSecond = borderedControl.panelExTop.Parent;
                borderedControl.panelExTop.Parent = null;
                borderedControl.panelExTop.Controls.Clear();
                borderedControl.OutputClick = null;
                borderedControl.panelExTop.Parent = parentSecond;
            }
            this.Parent = parent;
        }
    }
}
