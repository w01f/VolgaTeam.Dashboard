using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using NewBizWiz.Core.Common;
using ListManager = NewBizWiz.Core.AdSchedule.ListManager;
using ScheduleManager = NewBizWiz.Core.Calendar.ScheduleManager;
using ShortSchedule = NewBizWiz.Core.Calendar.ShortSchedule;

namespace NewBizWiz.Calendar.Single
{
	public partial class FormOpenCalendar : Form
	{
		private readonly List<ShortSchedule> _scheduleList = new List<ShortSchedule>();

		public FormOpenCalendar()
		{
			InitializeComponent();
		}

		public string ScheduleFilePath { get; set; }

		public void LoadSchedules()
		{
			_scheduleList.Clear();
			_scheduleList.AddRange(ScheduleManager.GetShortScheduleExtendedList());

			gridControlCalendar.Visible = true;
			repositoryItemComboBoxStatus.Items.Clear();
			repositoryItemComboBoxStatus.Items.AddRange(ListManager.Instance.Statuses);
			gridControlCalendar.DataSource = new BindingList<ShortSchedule>(_scheduleList);
			if (gridViewCalendar.RowCount > 0)
				gridViewCalendar.FocusedRowHandle = 0;
		}

		private void FormOpenSchedule_Load(object sender, EventArgs e)
		{
			LoadSchedules();
		}

		private void barLargeButtonItemOpen_ItemClick(object sender, ItemClickEventArgs e)
		{
			var selectedSchedule = gridViewCalendar.GetFocusedRow() as ShortSchedule;
			if (selectedSchedule != null)
			{
				ScheduleFilePath = selectedSchedule.FullFileName;
				DialogResult = DialogResult.OK;
				Close();
			}
			else
				Utilities.Instance.ShowWarning("Please select calendar in list");
		}

		private void barLargeButtonItemDelete_ItemClick(object sender, ItemClickEventArgs e)
		{
			if (Utilities.Instance.ShowWarningQuestion("Delete this Calendar?") == DialogResult.Yes)
			{
				string fileName = _scheduleList[gridViewCalendar.GetFocusedDataSourceRowIndex()].FullFileName;
				try
				{
					if (File.Exists(fileName))
						File.Delete(fileName);
				}
				catch
				{
					Utilities.Instance.ShowWarning("Couldn't delete selected calendar.");
				}
				LoadSchedules();
			}
		}

		private void barLargeButtonItemExit_ItemClick(object sender, ItemClickEventArgs e)
		{
			DialogResult = DialogResult.None;
			Close();
		}

		private void gridViewSchedules_RowClick(object sender, RowClickEventArgs e)
		{
			if (e.Clicks == 2)
				barLargeButtonItemOpen_ItemClick(null, null);
		}

		private void gridViewSchedules_CellValueChanged(object sender, CellValueChangedEventArgs e)
		{
			ShortSchedule schedule = _scheduleList[gridViewCalendar.GetDataSourceRowIndex(e.RowHandle)];
			schedule.Save();
		}

		private void repositoryItemComboBoxStatus_Closed(object sender, ClosedEventArgs e)
		{
			gridViewCalendar.CloseEditor();
		}
	}
}