using System;
using System.Windows.Forms;
using NewBizWiz.Core.Calendar;

namespace NewBizWiz.Calendar.Controls.ToolForms
{
	public partial class FormDayProperties : Form
	{
		private readonly CalendarDay _day;

		public FormDayProperties(CalendarDay day)
		{
			InitializeComponent();
			_day = day;
			Text = _day.Date.ToString("dddd, MMMM dd, yyyy");
			LoadData();
		}

		private void LoadData()
		{
			checkEditComment.Checked = !string.IsNullOrEmpty(_day.Comment);
			memoEditComment.EditValue = !string.IsNullOrEmpty(_day.Comment) ? _day.Comment : null;
		}

		private void SaveData()
		{
			_day.Comment = checkEditComment.Checked ? memoEditComment.EditValue as String : null;
		}

		private void FormDayProperties_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (DialogResult == DialogResult.OK)
			{
				SaveData();
			}
		}

		private void checkEditComment_CheckedChanged(object sender, System.EventArgs e)
		{
			memoEditComment.Enabled = checkEditComment.Checked;
			memoEditComment.EditValue = checkEditComment.Checked ? memoEditComment.EditValue : null;
		}
	}
}
