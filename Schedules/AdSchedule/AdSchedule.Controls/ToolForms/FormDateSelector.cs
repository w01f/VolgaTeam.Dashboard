using System;
using DevComponents.DotNetBar.Metro;

namespace NewBizWiz.AdSchedule.Controls.ToolForms
{
	public partial class FormDateSelector : MetroForm
	{
		public FormDateSelector()
		{
			InitializeComponent();
		}

		private void buttonXSelectAll_Click(object sender, EventArgs e)
		{
			checkedListBoxControlDates.CheckAll();
		}

		private void buttonXClearAll_Click(object sender, EventArgs e)
		{
			checkedListBoxControlDates.UnCheckAll();
		}
	}
}