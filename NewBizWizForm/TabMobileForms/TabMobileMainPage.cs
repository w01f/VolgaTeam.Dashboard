using System.Windows.Forms;

namespace NewBizWizForm.TabMobileForms
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class TabMobileMainPage : UserControl
    {
        private static TabMobileMainPage _instance;

        private TabMobileMainPage()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            AppManager.Instance.SetClickEventHandler(this);
        }

        public static TabMobileMainPage Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new TabMobileMainPage();
                return _instance;
            }
        }

        public void UpdatePageAccordingToggledButton()
        {
            Control parent = this.Parent;
            this.Parent = null;
            this.Controls.Clear();
            if (FormMain.Instance.buttonItemMobileScheduleBuilder != null && FormMain.Instance.buttonItemMobileScheduleBuilder.Checked)
            {
                MobileScheduleBuilderControl.Instance.LoadSchedules();
                FormMain.Instance.OutsideClick = MobileScheduleBuilderControl.Instance.OutsideClick;
                this.Controls.Add(MobileScheduleBuilderControl.Instance);
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
