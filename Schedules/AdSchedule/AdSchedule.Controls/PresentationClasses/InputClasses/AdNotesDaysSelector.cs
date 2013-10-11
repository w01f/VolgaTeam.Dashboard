using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace NewBizWiz.AdSchedule.Controls.PresentationClasses.InputClasses
{
	public partial class AdNotesDaysSelector : UserControl
	{
		public List<DateTime> SelectedDays { get; private set; }

		public AdNotesDaysSelector()
		{
			InitializeComponent();
			SelectedDays = new List<DateTime>();
			if ((base.CreateGraphics()).DpiX > 96)
				buttonXApplyOtherDays.Font = new Font(buttonXApplyOtherDays.Font.FontFamily, buttonXApplyOtherDays.Font.Size - 2, buttonXApplyOtherDays.Font.Style);
		}
	}
}