using System.Windows.Forms;

namespace NewBizWizForm.TabOnlineForms
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class TabOnlineMainPage : UserControl
    {
        private static TabOnlineMainPage _instance;

        private TabOnlineMainPage()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            AppManager.Instance.SetClickEventHandler(this);
        }

        public static TabOnlineMainPage Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new TabOnlineMainPage();
                return _instance;
            }
        }

        public void UpdatePageAccordingToggledButton()
        {
            Control parent = this.Parent;
            this.Parent = null;
            this.Controls.Clear();
            if (FormMain.Instance.buttonItemOnlineScheduleBuilder != null && FormMain.Instance.buttonItemOnlineScheduleBuilder.Checked)
            {
                OnlineScheduleBuilderControl.Instance.LoadSchedules();
                FormMain.Instance.OutsideClick = OnlineScheduleBuilderControl.Instance.OutsideClick;
                this.Controls.Add(OnlineScheduleBuilderControl.Instance);
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
