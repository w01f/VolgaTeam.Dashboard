using System.ComponentModel;
using System.Windows.Forms;
using NewBizWiz.Dashboard.ToolForms;

namespace NewBizWiz.Dashboard.TabMobileForms
{
	[ToolboxItem(false)]
	public partial class TabMobileMainPage : UserControl
	{
		private static TabMobileMainPage _instance;

		private TabMobileMainPage()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			AppManager.Instance.SetClickEventHandler(this);
		}

		public static TabMobileMainPage Instance
		{
			get
			{
				if (_instance == null)
					_instance = new TabMobileMainPage();
				return _instance;
			}
		}

		public void UpdatePageAccordingToggledButton()
		{
			Control parent = Parent;
			Parent = null;
			Controls.Clear();
			if (FormMain.Instance.buttonItemMobileScheduleBuilder != null && FormMain.Instance.buttonItemMobileScheduleBuilder.Checked)
			{
				MobileScheduleBuilderControl.Instance.LoadSchedules();
				FormMain.Instance.OutsideClick = MobileScheduleBuilderControl.Instance.OutsideClick;
				Controls.Add(MobileScheduleBuilderControl.Instance);
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