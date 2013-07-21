using System.ComponentModel;
using System.Windows.Forms;
using NewBizWiz.Dashboard.ToolForms;

namespace NewBizWiz.Dashboard.TabTVForms
{
	[ToolboxItem(false)]
	public partial class TabTVMainPage : UserControl
	{
		private static TabTVMainPage _instance;

		private TabTVMainPage()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
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
			Control parent = Parent;
			Parent = null;
			Controls.Clear();
			if (FormMain.Instance.buttonItemTVScheduleBuilder != null && FormMain.Instance.buttonItemTVScheduleBuilder.Checked)
			{
				TVScheduleBuilderControl.Instance.LoadSchedules();
				FormMain.Instance.OutsideClick = TVScheduleBuilderControl.Instance.OutsideClick;
				Controls.Add(TVScheduleBuilderControl.Instance);
			}
			else
			{
				var borderedControl = new WhiteBorderControl();
				Controls.Add(borderedControl);
				Control parentSecond = borderedControl.panelExTop.Parent;
				borderedControl.panelExTop.Parent = null;
				borderedControl.panelExTop.Controls.Clear();
				borderedControl.OutputClick = null;
				borderedControl.panelExTop.Parent = parentSecond;
			}
			Parent = parent;
		}
	}
}