using System.ComponentModel;
using System.Windows.Forms;
using NewBizWiz.Dashboard.ToolForms;

namespace NewBizWiz.Dashboard.TabOnlineForms
{
	[ToolboxItem(false)]
	public partial class TabOnlineMainPage : UserControl
	{
		private static TabOnlineMainPage _instance;

		private TabOnlineMainPage()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
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
			Control parent = Parent;
			Parent = null;
			Controls.Clear();
			OnlineScheduleBuilderControl.Instance.LoadSchedules();
			FormMain.Instance.OutsideClick = OnlineScheduleBuilderControl.Instance.OutsideClick;
			Controls.Add(OnlineScheduleBuilderControl.Instance);
			Parent = parent;
		}
	}
}