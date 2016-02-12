using System.Drawing;
using DevComponents.DotNetBar.Metro;

namespace Asa.Dashboard.ToolForms
{
	public partial class FormLoadSplash : MetroForm
	{
		public FormLoadSplash()
		{
			InitializeComponent();
			if ((CreateGraphics()).DpiX > 96)
			{
				laTopCaption.Font = new Font(laTopCaption.Font.FontFamily, laTopCaption.Font.Size - 3, laTopCaption.Font.Style);
				laBottomCaption.Font = new Font(laBottomCaption.Font.FontFamily, laBottomCaption.Font.Size - 2, laBottomCaption.Font.Style);
			}
			circularProgress1.IsRunning = true;
		}
	}
}