using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Media.Configuration;
using Asa.Business.Media.Entities.NonPersistent.Schedule;
using Asa.Business.Media.Enums;
using Asa.Common.Core.Helpers;
using Asa.Media.Controls.BusinessClasses;
using DevComponents.DotNetBar.Metro;
using DevExpress.XtraBars;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using Asa.Media.Controls.Properties;

namespace Asa.Media.Controls.ToolForms
{
	public partial class FormOpenSchedule : MetroForm
	{
		private readonly List<MediaScheduleModel> _scheduleList = new List<MediaScheduleModel>();

		private MediaScheduleModel SelectedScheduleModel
		{
			get { return gridViewSchedules.GetFocusedRow() as MediaScheduleModel; }
		}

		public FormOpenSchedule()
		{
			InitializeComponent();
		}

		public void LoadSchedules()
		{
			_scheduleList.AddRange(BusinessObjects.Instance.ScheduleManager.GetScheduleList<MediaScheduleModel>()
				.Where(scheduleModel => scheduleModel.Parent != BusinessObjects.Instance.ScheduleManager.ActiveSchedule));
			gridControlSchedules.Visible = true;
			repositoryItemComboBoxStatus.Items.Clear();
			repositoryItemComboBoxStatus.Items.AddRange(MediaMetaData.Instance.ListManager.Statuses);
			gridControlSchedules.DataSource = _scheduleList;
			if (gridViewSchedules.RowCount > 0)
				gridViewSchedules.FocusedRowHandle = 0;
		}

		private void FormOpenSchedule_Load(object sender, EventArgs e)
		{
			LoadSchedules();
		}

		private void OnScheduleOpenItemClick(object sender, ItemClickEventArgs e)
		{
			var schedule = SelectedScheduleModel;
			if (schedule == null)
			{
				PopupMessageHelper.Instance.ShowWarning("Please select schedule in list");
				return;
			}
			DialogResult = DialogResult.OK;
			Close();
			BusinessObjects.Instance.ScheduleManager.OpenSchedule(schedule.Parent);
		}

		private void OnScheduleDeleteDeleteItemClick(object sender, ItemClickEventArgs e)
		{
			var schedule = SelectedScheduleModel;
			if (schedule == null)
			{
				PopupMessageHelper.Instance.ShowWarning("Please select schedule in list");
				return;
			}
			if (PopupMessageHelper.Instance.ShowWarningQuestion("Delete this Schedule?") != DialogResult.Yes) return;
			_scheduleList.Remove(schedule);
			BusinessObjects.Instance.ScheduleManager.DeleteSchedule(schedule.Parent);
			gridViewSchedules.RefreshData();
		}

		private void OnExitItemClick(object sender, ItemClickEventArgs e)
		{
			DialogResult = DialogResult.None;
			Close();
		}

		private void OnSchedulesViewRowClick(object sender, RowClickEventArgs e)
		{
			if (e.Clicks == 2)
				OnScheduleOpenItemClick(null, null);
		}

		private void OnScheduleStatusChanged(object sender, CellValueChangedEventArgs e)
		{
			var schedule = SelectedScheduleModel;
			BusinessObjects.Instance.ScheduleManager.SaveScheduleModel(schedule);
		}

		private void OnStatusComboBoxClosed(object sender, ClosedEventArgs e)
		{
			gridViewSchedules.CloseEditor();
		}
	}
}