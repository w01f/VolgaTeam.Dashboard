using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace NewBizWiz.Dashboard.ToolForms
{
	[ToolboxItem(false)]
	public partial class WhiteBorderControl : UserControl
	{
		public WhiteBorderControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			AppManager.Instance.SetClickEventHandler(this);
			if ((base.CreateGraphics()).DpiX > 96)
			{
				buttonXSavedFiles.Font = new Font(buttonXSavedFiles.Font.FontFamily, buttonXSavedFiles.Font.Size - 2, buttonXSavedFiles.Font.Style);
			}
		}

		public AppManager.EmptyParametersDelegate OutputClick { get; set; }
		public AppManager.EmptyParametersDelegate SavedFilesClick { get; set; }

		private void buttonXOutput_Click(object sender, EventArgs e)
		{
			if (OutputClick != null)
				OutputClick();
		}

		public void EnableOutputButton(bool enable)
		{
			FormMain.Instance.buttonItemPowerPoint.Enabled = enable;
			buttonXOutput.Enabled = enable;
		}

		public void EnableSavedFilesButton(bool enable)
		{
			buttonXSavedFiles.Enabled = enable;
		}

		private void buttonXSavedFiles_Click(object sender, EventArgs e)
		{
			if (SavedFilesClick != null)
				SavedFilesClick();
		}
	}
}