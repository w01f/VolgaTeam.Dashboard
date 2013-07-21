using System.ComponentModel;
using System.Windows.Forms;
using NewBizWiz.Dashboard.ToolForms;

namespace NewBizWiz.Dashboard.TabiPadForms
{
	[ToolboxItem(false)]
	public partial class TabiPadMainPage : UserControl
	{
		private static TabiPadMainPage _instance;

		private TabiPadMainPage()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			AppManager.Instance.SetClickEventHandler(this);
		}

		public static TabiPadMainPage Instance
		{
			get
			{
				if (_instance == null)
					_instance = new TabiPadMainPage();
				return _instance;
			}
		}

		public void UpdatePageAccordingToggledButton()
		{
			Control parent = Parent;
			Parent = null;
			Controls.Clear();
			if (FormMain.Instance.buttonItemiPad != null && FormMain.Instance.buttonItemiPad.Checked)
			{
				Controls.Add(iPadControl.Instance);
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