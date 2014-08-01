using System.ComponentModel;
using System.Windows.Forms;

namespace NewBizWiz.Dashboard.TabNewspaperForms
{
	[ToolboxItem(false)]
	public partial class TabNewspaperMainPage : UserControl
	{
		private static TabNewspaperMainPage _instance;

		private TabNewspaperMainPage()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
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
			Control parent = Parent;
			Parent = null;
			Controls.Clear();
			PrintScheduleBuilderControl.Instance.LoadSchedules();
			FormMain.Instance.OutsideClick = PrintScheduleBuilderControl.Instance.OutsideClick;
			Controls.Add(PrintScheduleBuilderControl.Instance);
			Parent = parent;
		}
	}
}