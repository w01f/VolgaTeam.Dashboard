using System.Windows.Forms;

namespace NewBizWizForm.TabCableForms
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class TabCableMainPage : UserControl
    {
        private static TabCableMainPage _instance;

        private TabCableMainPage()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            AppManager.Instance.SetClickEventHandler(this);
        }

        public static TabCableMainPage Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new TabCableMainPage();
                return _instance;
            }
        }

        public void UpdatePageAccordingToggledButton()
        {
            Control parent = this.Parent;
            this.Parent = null;
            this.Controls.Clear();
            if (FormMain.Instance.buttonItemCableScheduleBuilder != null && FormMain.Instance.buttonItemCableScheduleBuilder.Checked)
            {
                this.Controls.Add(CableOverviewControl.Instance);
            }
            else
            {
                ToolForms.WhiteBorderControl borderedControl = new NewBizWizForm.ToolForms.WhiteBorderControl();
                this.Controls.Add(borderedControl);
                Control parentSecond = borderedControl.panelExTop.Parent;
                borderedControl.panelExTop.Parent = null;
                borderedControl.panelExTop.Controls.Clear();
                borderedControl.OutputClick = null;

                //if (FormMain.Instance.buttonItemPrintScheduleBuilder != null && FormMain.Instance.buttonItemPrintScheduleBuilder.Checked)
                //    borderedControl.panelExTop.Controls.Add(PrintScheduleBuilderControl.Instance);

                borderedControl.panelExTop.Parent = parentSecond;
            }
            this.Parent = parent;
        }
    }
}
