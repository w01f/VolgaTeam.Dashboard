using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NewBizWiz.AdSchedule.Controls.ToolForms
{
	public partial class FormDateSelector : Form
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
