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
		}
	}
}
