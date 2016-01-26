using System;
using System.Drawing;
using DevComponents.DotNetBar.Metro;

namespace Asa.Reset
{
	public partial class FormWarning : MetroForm
	{
		public FormWarning()
		{
			InitializeComponent();
		}

		private void OnFormShown(object sender, EventArgs e)
		{
			InitGUIColors();
		}

		private void InitGUIColors()
		{
			pnBackground.BackColor = Color.SandyBrown;
			
			laTitle.BackColor = Color.SandyBrown;
			laTitle.ForeColor = Color.White;
			
			laDescription.BackColor = Color.SandyBrown;
			laDescription.ForeColor = Color.White;

			if ((CreateGraphics()).DpiX > 96)
			{
				laTitle.Font = new Font(laTitle.Font.FontFamily, laTitle.Font.Size - 6, laTitle.Font.Style);
				laDescription.Font = new Font(laDescription.Font.FontFamily, laDescription.Font.Size - 2, laDescription.Font.Style);
			}
		}
	}
}
