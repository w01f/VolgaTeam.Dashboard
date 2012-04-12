using System.Windows.Forms;

namespace NewBizWizForm.TabCalendarForms
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class TabCalendarMainPage : UserControl
    {
        private static TabCalendarMainPage _instance;

        private TabCalendarMainPage()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            AppManager.Instance.SetClickEventHandler(this);
        }

        public static TabCalendarMainPage Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new TabCalendarMainPage();
                return _instance;
            }
        }

        public void UpdatePageAccordingToggledButton()
        {
            Control parent = this.Parent;
            this.Parent = null;
            this.Controls.Clear();
            if (FormMain.Instance.buttonItemCalendarBuilder != null && FormMain.Instance.buttonItemCalendarBuilder.Checked)
            {
                CalendarBuilderControl.Instance.LoadCalendars();
                FormMain.Instance.OutsideClick = CalendarBuilderControl.Instance.OutsideClick;
                this.Controls.Add(CalendarBuilderControl.Instance);
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
