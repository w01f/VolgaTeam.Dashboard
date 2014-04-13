using System.ComponentModel;
using System.Windows.Forms;
using NewBizWiz.Dashboard.ToolForms;

namespace NewBizWiz.Dashboard.TabRadioForms
{
	[ToolboxItem(false)]
	public partial class TabRadioMainPage : UserControl
	{
		private static TabRadioMainPage _instance;

		private TabRadioMainPage()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
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
			Control parent = Parent;
			Parent = null;
			Controls.Clear();
			RadioScheduleBuilderControl.Instance.LoadSchedules();
			FormMain.Instance.OutsideClick = RadioScheduleBuilderControl.Instance.OutsideClick;
			Controls.Add(RadioScheduleBuilderControl.Instance);
			Parent = parent;
		}
	}
}