using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace CommandCentral
{
	[ToolboxItem(false)]
	public partial class DataControl : UserControl
	{
		private static DataControl _instance;

		private DataControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
		}

		public AppManager.NoParamDelegate ViewSource { get; set; }

		public string ButtonText
		{
			set { buttonXSourceFile.Text = value; }
		}

		public static DataControl Instance
		{
			get
			{
				if (_instance == null)
					_instance = new DataControl();
				return _instance;
			}
		}

		private void buttonXSourceFile_Click(object sender, EventArgs e)
		{
			ViewSource();
		}
	}
}