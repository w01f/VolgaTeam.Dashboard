using System;
using System.Drawing;
using System.Windows.Forms;
using Asa.Common.Core.Helpers;
using DevComponents.DotNetBar.Metro;

namespace Asa.Media.Controls.PresentationClasses.ScheduleControls.ContentEditors
{
	public partial class FormCopyContentConfirmation : MetroForm
	{
		public FormCopyContentConfirmation()
		{
			InitializeComponent();
			if ((CreateGraphics()).DpiX > 96)
			{
				buttonXOK.Font = new Font(buttonXOK.Font.FontFamily, buttonXOK.Font.Size - 2, buttonXOK.Font.Style);
				buttonXClose.Font = new Font(buttonXClose.Font.FontFamily, buttonXClose.Font.Size - 2, buttonXClose.Font.Style);
			}
		}
	}
}