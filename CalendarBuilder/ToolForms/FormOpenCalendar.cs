using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using CalendarBuilder.BusinessClasses;
using CalendarBuilder.ConfigurationClasses;
using DevExpress.XtraBars;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;

namespace CalendarBuilder.ToolForms
{
	public partial class FormOpenCalendar : Form
	{
		private ShortSchedule[] _scheduleList;

		public FormOpenCalendar()
		{
			InitializeComponent();
		}

		public string ScheduleName { get; set; }

		public void LoadSchedules()
		{
			_scheduleList = ScheduleManager.Instance.GetShortScheduleList(new DirectoryInfo(SettingsManager.Instance.SaveFolder));
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
			if (gridViewCalendar.FocusedRowHandle >= 0)
			{
				ScheduleName = _scheduleList[gridViewCalendar.GetFocusedDataSourceRowIndex()].ShortFileName;
				DialogResult = DialogResult.OK;
				Close();
			}
			else
				AppManager.ShowWarning("Please select calendar in list");
		}

		private void barLargeButtonItemDelete_ItemClick(object sender, ItemClickEventArgs e)
		{
			if (AppManager.ShowWarningQuestion("Delete this Calendar?") == DialogResult.Yes)
			{
				string fileName = _scheduleList[gridViewCalendar.GetFocusedDataSourceRowIndex()].FullFileName;
				try
				{
					if (File.Exists(fileName))
						File.Delete(fileName);
				}
				catch
				{
					AppManager.ShowWarning("Couldn't delete selected calendar.");
				}
				LoadSchedules();
			}
		}

		private void barLargeButtonItemHelp_ItemClick(object sender, ItemClickEventArgs e) {}

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
			var schedule = _scheduleList[gridViewCalendar.GetDataSourceRowIndex(e.RowHandle)];
			schedule.Save();
		}

		private void repositoryItemComboBoxStatus_Closed(object sender, ClosedEventArgs e)
		{
			gridViewCalendar.CloseEditor();
		}
	}
}