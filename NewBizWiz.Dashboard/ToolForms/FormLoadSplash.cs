using System.Drawing;
using System.Windows.Forms;

namespace NewBizWiz.Dashboard.ToolForms
{
	public partial class FormLoadSplash : Form
	{
		public FormLoadSplash()
		{
			InitializeComponent();
			if ((base.CreateGraphics()).DpiX > 96)
			{
				laTopCaption.Font = new Font(laTopCaption.Font.FontFamily, laTopCaption.Font.Size - 3, laTopCaption.Font.Style);
				laBottomCaption.Font = new Font(laBottomCaption.Font.FontFamily, laBottomCaption.Font.Size - 2, laBottomCaption.Font.Style);
			}
			circularProgress1.IsRunning = true;
		}
	}
}