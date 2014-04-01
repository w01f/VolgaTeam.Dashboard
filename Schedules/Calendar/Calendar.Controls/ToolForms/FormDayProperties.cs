using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Layout;
using NewBizWiz.Core.Calendar;
using NewBizWiz.Core.Common;

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

		public void LoadImages(List<ImageSource> images)
		{
			if (!images.Any())
			{
				pnLogo.Visible = false;
				Height = pnComment.Height + 20;
			}
			gridControlLogoGallery.DataSource = images;
			var index = images.IndexOf(_day.Logo);
			layoutViewLogoGallery.FocusedRowHandle = index != GridControl.InvalidRowHandle ? layoutViewLogoGallery.GetRowHandle(index) : 0;
		}

		private void SaveData()
		{
			var selectedLogo = layoutViewLogoGallery.GetFocusedRow() as ImageSource;
			_day.Logo = selectedLogo ?? new ImageSource();
			_day.Comment = checkEditComment.Checked ? memoEditComment.EditValue as String : null;
		}

		private void layoutViewLogoGallery_CustomFieldValueStyle(object sender, DevExpress.XtraGrid.Views.Layout.Events.LayoutViewFieldValueStyleEventArgs e)
		{
			var view = sender as LayoutView;
			if (view.FocusedRowHandle != e.RowHandle) return;
			e.Appearance.BackColor = Color.Orange;
			e.Appearance.BackColor2 = Color.Orange;
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
