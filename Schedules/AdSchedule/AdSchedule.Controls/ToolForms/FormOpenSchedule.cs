using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using DevExpress.XtraBars;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using Asa.Core.AdSchedule;
using Asa.Core.Common;
using ListManager = Asa.Core.AdSchedule.ListManager;

namespace Asa.AdSchedule.Controls.ToolForms
{
	public partial class FormOpenSchedule : MetroForm
	{
		private ShortSchedule[] _scheduleList;

		public FormOpenSchedule()
		{
			InitializeComponent();
		}

		public string ScheduleName { get; set; }

		public void LoadSchedules()
		{
			_scheduleList = ScheduleManager.GetShortScheduleList();
			gridControlSchedules.Visible = true;
			repositoryItemComboBoxStatus.Items.Clear();
			repositoryItemComboBoxStatus.Items.AddRange(ListManager.Instance.Statuses);
			gridControlSchedules.DataSource = new BindingList<ShortSchedule>(_scheduleList);
			if (gridViewSchedules.RowCount > 0)
				gridViewSchedules.FocusedRowHandle = 0;
		}

		private void FormOpenSchedule_Load(object sender, EventArgs e)
		{
			LoadSchedules();
		}

		private void barLargeButtonItemOpen_ItemClick(object sender, ItemClickEventArgs e)
		{
			if (gridViewSchedules.FocusedRowHandle >= 0)
			{
				ScheduleName = _scheduleList[gridViewSchedules.GetFocusedDataSourceRowIndex()].ShortFileName;
				DialogResult = DialogResult.OK;
				Close();
			}
			else
				Utilities.Instance.ShowWarning("Please select schedule in list");
		}

		private void barLargeButtonItemDelete_ItemClick(object sender, ItemClickEventArgs e)
		{
			if (Utilities.Instance.ShowWarningQuestion("Delete this Schedule?") == DialogResult.Yes)
			{
				string fileName = _scheduleList[gridViewSchedules.GetFocusedDataSourceRowIndex()].FullFileName;
				try
				{
					if (File.Exists(fileName))
						File.Delete(fileName);
				}
				catch
				{
					Utilities.Instance.ShowWarning("Couldn't delete selected schedule.");
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
			ShortSchedule schedule = _scheduleList[gridViewSchedules.GetDataSourceRowIndex(e.RowHandle)];
			schedule.Save();
		}

		private void repositoryItemComboBoxStatus_Closed(object sender, ClosedEventArgs e)
		{
			gridViewSchedules.CloseEditor();
		}
	}
}