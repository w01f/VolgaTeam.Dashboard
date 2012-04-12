using System.Windows.Forms;

namespace NewBizWizForm.TabNewspaperForms
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class TabNewspaperMainPage : UserControl
    {
        private static TabNewspaperMainPage _instance;

        private TabNewspaperMainPage()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            AppManager.Instance.SetClickEventHandler(this);
        }

        public static TabNewspaperMainPage Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new TabNewspaperMainPage();
                return _instance;
            }
        }

        public void UpdatePageAccordingToggledButton()
        {
            Control parent = this.Parent;
            this.Parent = null;
            this.Controls.Clear();
            if (FormMain.Instance.buttonItemNewspaperScheduleBuilder != null && FormMain.Instance.buttonItemNewspaperScheduleBuilder.Checked)
            {
                PrintScheduleBuilderControl.Instance.LoadSchedules();
                FormMain.Instance.OutsideClick = PrintScheduleBuilderControl.Instance.OutsideClick;
                this.Controls.Add(PrintScheduleBuilderControl.Instance);
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
