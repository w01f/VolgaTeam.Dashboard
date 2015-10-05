using System;
using System.Drawing;
using DevComponents.DotNetBar.Metro;

namespace NewBizWiz.CommonGUI.ToolForms
{
	public partial class FormProgress : MetroForm
	{
		public FormProgress()
		{
			InitializeComponent();
			if ((base.CreateGraphics()).DpiX > 96)
			{
				laProgress.Font = new Font(laProgress.Font.FontFamily, laProgress.Font.Size - 2, laProgress.Font.Style);
			}
		}

		private void FormProgress_Shown(object sender, EventArgs e)
		{
			laProgress.Focus();
			circularProgress.IsRunning = true;
		}
	}
}