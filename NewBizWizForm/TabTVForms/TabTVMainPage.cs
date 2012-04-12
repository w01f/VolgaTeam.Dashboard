using System.Windows.Forms;

namespace NewBizWizForm.TabTVForms
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class TabTVMainPage : UserControl
    {
        private static TabTVMainPage _instance;

        private TabTVMainPage()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            AppManager.Instance.SetClickEventHandler(this);
        }

        public static TabTVMainPage Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new TabTVMainPage();
                return _instance;
            }
        }

        public void UpdatePageAccordingToggledButton()
        {
            Control parent = this.Parent;
            this.Parent = null;
            this.Controls.Clear();
            if (FormMain.Instance.buttonItemTVScheduleBuilder != null && FormMain.Instance.buttonItemTVScheduleBuilder.Checked)
            {
                TVScheduleBuilderControl.Instance.LoadSchedules();
                FormMain.Instance.OutsideClick = TVScheduleBuilderControl.Instance.OutsideClick;
                this.Controls.Add(TVScheduleBuilderControl.Instance);
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
