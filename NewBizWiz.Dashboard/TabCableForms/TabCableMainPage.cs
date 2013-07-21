using System.ComponentModel;
using System.Windows.Forms;
using NewBizWiz.Dashboard.ToolForms;

namespace NewBizWiz.Dashboard.TabCableForms
{
	[ToolboxItem(false)]
	public partial class TabCableMainPage : UserControl
	{
		private static TabCableMainPage _instance;

		private TabCableMainPage()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
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
			Control parent = Parent;
			Parent = null;
			Controls.Clear();
			if (FormMain.Instance.buttonItemCableScheduleBuilder != null && FormMain.Instance.buttonItemCableScheduleBuilder.Checked)
			{
				Controls.Add(CableOverviewControl.Instance);
			}
			else
			{
				var borderedControl = new WhiteBorderControl();
				Controls.Add(borderedControl);
				Control parentSecond = borderedControl.panelExTop.Parent;
				borderedControl.panelExTop.Parent = null;
				borderedControl.panelExTop.Controls.Clear();
				borderedControl.OutputClick = null;

				//if (FormMain.Instance.buttonItemPrintScheduleBuilder != null && FormMain.Instance.buttonItemPrintScheduleBuilder.Checked)
				//    borderedControl.panelExTop.Controls.Add(PrintScheduleBuilderControl.Instance);

				borderedControl.panelExTop.Parent = parentSecond;
			}
			Parent = parent;
		}
	}
}