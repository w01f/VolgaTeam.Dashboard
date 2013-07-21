using System.ComponentModel;
using System.Windows.Forms;
using NewBizWiz.Dashboard.ToolForms;

namespace NewBizWiz.Dashboard.TabCalendarForms
{
	[ToolboxItem(false)]
	public partial class TabCalendarMainPage : UserControl
	{
		private static TabCalendarMainPage _instance;

		private TabCalendarMainPage()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
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
			Control parent = Parent;
			Parent = null;
			Controls.Clear();
			if (FormMain.Instance.buttonItemCalendarBuilder != null && FormMain.Instance.buttonItemCalendarBuilder.Checked)
			{
				CalendarBuilderControl.Instance.LoadCalendars();
				FormMain.Instance.OutsideClick = CalendarBuilderControl.Instance.OutsideClick;
				Controls.Add(CalendarBuilderControl.Instance);
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