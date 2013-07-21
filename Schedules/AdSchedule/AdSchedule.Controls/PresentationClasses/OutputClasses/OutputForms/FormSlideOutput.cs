using System;
using System.Drawing;
using System.Windows.Forms;
using NewBizWiz.AdSchedule.Controls.InteropClasses;
using NewBizWiz.Core.Common;

namespace NewBizWiz.AdSchedule.Controls.PresentationClasses.OutputClasses.OutputForms
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

		private void FormSlideOutput_Shown(object sender, EventArgs e)
		{
			Utilities.Instance.ActivatePowerPoint(AdSchedulePowerPointHelper.Instance.PowerPointObject);
			Utilities.Instance.ActivateMiniBar();
			Utilities.Instance.ActivateForm(Handle, false, true);
		}
	}
}