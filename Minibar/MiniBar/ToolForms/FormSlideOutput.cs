using System.Drawing;
using System.Windows.Forms;

namespace NewBizWiz.MiniBar.ToolForms
{
	public partial class FormSlideOutput : Form
	{
		public FormSlideOutput()
		{
			InitializeComponent();
			if ((base.CreateGraphics()).DpiX > 96)
			{
				laTitle.Font = new Font(laTitle.Font.FontFamily, laTitle.Font.Size - 3, laTitle.Font.Style);
			}
		}
	}
}