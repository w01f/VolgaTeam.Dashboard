using System.Drawing;
using DevComponents.DotNetBar.Metro;

namespace Asa.AdSchedule.Controls.ToolForms
{
	public partial class FormCloneProduct : MetroForm
	{
		public FormCloneProduct()
		{
			InitializeComponent();
			if ((base.CreateGraphics()).DpiX > 96)
			{
				laHeader.Font = new Font(laHeader.Font.FontFamily, laHeader.Font.Size - 3, laHeader.Font.Style);
				buttonXCancel.Font = new Font(buttonXCancel.Font.FontFamily, buttonXCancel.Font.Size - 2, buttonXCancel.Font.Style);
				buttonXOK.Font = new Font(buttonXOK.Font.FontFamily, buttonXOK.Font.Size - 2, buttonXOK.Font.Style);
			}
		}
	}
}