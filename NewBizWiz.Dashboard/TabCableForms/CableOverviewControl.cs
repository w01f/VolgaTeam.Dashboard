using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace NewBizWiz.Dashboard.TabCableForms
{
	[ToolboxItem(false)]
	public partial class CableOverviewControl : UserControl
	{
		private static CableOverviewControl _instance;

		private CableOverviewControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			AppManager.Instance.SetClickEventHandler(this);
			if ((base.CreateGraphics()).DpiX > 96)
			{
				laTitle.Font = new Font(laTitle.Font.FontFamily, laTitle.Font.Size - 6, laTitle.Font.Style);
			}
		}

		public static CableOverviewControl Instance
		{
			get
			{
				if (_instance == null)
					_instance = new CableOverviewControl();
				return _instance;
			}
		}
	}
}