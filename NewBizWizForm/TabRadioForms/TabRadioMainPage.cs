using System.Windows.Forms;

namespace NewBizWizForm.TabRadioForms
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class TabRadioMainPage : UserControl
    {
        private static TabRadioMainPage _instance;

        private TabRadioMainPage()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            AppManager.Instance.SetClickEventHandler(this);
        }

        public static TabRadioMainPage Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new TabRadioMainPage();
                return _instance;
            }
        }

        public void UpdatePageAccordingToggledButton()
        {
            Control parent = this.Parent;
            this.Parent = null;
            this.Controls.Clear();
            if (FormMain.Instance.buttonItemRadioScheduleBuilder != null && FormMain.Instance.buttonItemRadioScheduleBuilder.Checked)
            {
                this.Controls.Add(RadioScheduleBuilderControl.Instance);
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
