using System.Drawing;
using System.Windows.Forms;

namespace NewBizWiz.Dashboard.ToolForms
{
	public partial class FormSlideOutput : Form
	{
		public FormSlideOutput()
		{
			InitializeComponent();
			if ((base.CreateGraphics()).DpiX > 96)
			{
				laTitle.Font = new Font(laTitle.Font.FontFamily, laTitle.Font.Size - 3, laTitle.Font.Style);
				buttonXOK.Font = new Font(buttonXOK.Font.FontFamily, buttonXOK.Font.Size - 2, buttonXOK.Font.Style);
				buttonXShow.Font = new Font(buttonXShow.Font.FontFamily, buttonXShow.Font.Size - 2, buttonXShow.Font.Style);
			}
		}
	}
}