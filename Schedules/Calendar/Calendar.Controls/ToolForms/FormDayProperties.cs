using System;
using System.Windows.Forms;
using Asa.Business.Calendar.Entities.NonPersistent;
using Asa.Common.Core.Helpers;
using DevComponents.DotNetBar.Metro;
using DevExpress.Skins;

namespace Asa.Calendar.Controls.ToolForms
{
	public partial class FormDayProperties: MetroForm
	{
		private readonly CalendarDay _day;

		public FormDayProperties(CalendarDay day)
		{
			InitializeComponent();
			_day = day;
			Text = _day.Date.ToString("dddd, MMMM dd, yyyy");
			LoadData();

			layoutControlItemOK.MaxSize = RectangleHelper.ScaleSize(layoutControlItemOK.MaxSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemOK.MinSize = RectangleHelper.ScaleSize(layoutControlItemOK.MinSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MaxSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MinSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MinSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
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

		private void OnFormClosing(object sender, FormClosingEventArgs e)
		{
			if (DialogResult == DialogResult.OK)
			{
				SaveData();
			}
		}

		private void OnCommentCheckedChanged(object sender, System.EventArgs e)
		{
			memoEditComment.Enabled = checkEditComment.Checked;
			memoEditComment.EditValue = checkEditComment.Checked ? memoEditComment.EditValue : null;
		}
	}
}
