using System.Drawing;
using System.Windows.Forms;

namespace NewBizWizForm.TabRadioForms
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class RadioOverviewControl : UserControl
    {
        private static RadioOverviewControl _instance;

        private RadioOverviewControl()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            AppManager.Instance.SetClickEventHandler(this);
            if ((base.CreateGraphics()).DpiX > 96)
            {
                laTitle.Font = new Font(laTitle.Font.FontFamily, laTitle.Font.Size - 6, laTitle.Font.Style);
            }
        }

        public static RadioOverviewControl Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new RadioOverviewControl();
                return _instance;
            }
        }
    }
}
