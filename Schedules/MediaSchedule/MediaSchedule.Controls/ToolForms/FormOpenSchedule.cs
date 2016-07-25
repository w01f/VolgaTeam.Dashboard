using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Common.Entities.NonPersistent.ScheduleTemplates;
using Asa.Business.Media.Configuration;
using Asa.Business.Media.Entities.NonPersistent.Schedule;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.ToolForms;
using Asa.Media.Controls.BusinessClasses.Managers;
using DevComponents.DotNetBar.Metro;
using DevExpress.Data;
using DevExpress.XtraBars;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;

namespace Asa.Media.Controls.ToolForms
{
	public partial class FormOpenSchedule : MetroForm
	{
		public FormOpenSchedule()
		{
			InitializeComponent();
			barStaticItemLogo.Glyph = BusinessObjects.Instance.ImageResourcesManager.MainAppRibbonLogo ?? barStaticItemLogo.Glyph;
			gridColumnSchedulesLastModifiedDate.SortIndex = 0;
			gridColumnSchedulesLastModifiedDate.SortOrder = ColumnSortOrder.Descending;
		}

		private void OnFormLoad(object sender, EventArgs e)
		{
			LoadSchedules();
			xtraTabPageSchedules.PageEnabled = _scheduleList.Any();
			xtraTabPageTemplates.PageEnabled = !FileStorageManager.Instance.UseLocalMode;
			if (!xtraTabPageSchedules.PageEnabled && FileStorageManager.Instance.UseLocalMode)
				xtraTabControl.SelectedTabPage = xtraTabPageTemplates;
		}

		private void OnScheduleOpenItemClick(object sender, ItemClickEventArgs e)
		{
			if (xtraTabControl.SelectedTabPage == xtraTabPageSchedules)
				OpenSchedule();
			else if (xtraTabControl.SelectedTabPage == xtraTabPageTemplates)
				OpenTemplate();
		}

		private void OnScheduleDeleteDeleteItemClick(object sender, ItemClickEventArgs e)
		{
			DeleteSchedule();
		}

		private void OnExitItemClick(object sender, ItemClickEventArgs e)
		{
			DialogResult = DialogResult.None;
			Close();
		}

		private void OnSelectedTabPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
		{
			if (e.Page == xtraTabPageTemplates && _scheduleTemplateList == null)
				LoadTemplates();
			barLargeButtonItemDelete.Enabled = e.Page == xtraTabPageSchedules;
		}

		#region Schedule Management
		private readonly List<MediaScheduleModel> _scheduleList = new List<MediaScheduleModel>();
		private MediaScheduleModel SelectedScheduleModel => gridViewSchedules.GetFocusedRow() as MediaScheduleModel;

		public void LoadSchedules()
		{
			_scheduleList.AddRange(BusinessObjects.Instance.ScheduleManager.GetScheduleList<MediaScheduleModel>()
				.Where(scheduleModel => scheduleModel.Parent != BusinessObjects.Instance.ScheduleManager.ActiveSchedule));
			repositoryItemComboBoxStatus.Items.Clear();
			repositoryItemComboBoxStatus.Items.AddRange(MediaMetaData.Instance.ListManager.Statuses);
			gridControlSchedules.DataSource = _scheduleList;
			if (gridViewSchedules.RowCount > 0)
				gridViewSchedules.FocusedRowHandle = 0;
		}

		private void OpenSchedule()
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

		private void DeleteSchedule()
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

		private void OnSchedulesViewRowClick(object sender, RowClickEventArgs e)
		{
			if (e.Clicks == 2)
				OpenSchedule();
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
		#endregion

		#region Templates
		private TemplateList _scheduleTemplateList;

		private TemplateInfo SelectedTemplateInfo => gridViewTemplates.GetFocusedRow() as TemplateInfo;

		private void LoadTemplates()
		{
			FormProgress.ShowProgress("Loading Schedule List...", () =>
			{
				AsyncHelper.RunSync(async () =>
				{
					_scheduleTemplateList = await BusinessObjects.Instance.ScheduleTemplatesManager.GetTemplatesList();
				});
			}, false);
			gridControlTemplates.DataSource = _scheduleTemplateList.Items;
			if (gridViewTemplates.RowCount > 0)
				gridViewTemplates.FocusedRowHandle = 0;
		}

		private void OpenTemplate()
		{
			var templateInfo = SelectedTemplateInfo;
			if (templateInfo == null)
			{
				PopupMessageHelper.Instance.ShowWarning("Please select schedule in list");
				return;
			}
			using (var form = new FormScheduleName())
			{
				form.Text = "Import Schedule Template";
				if (form.ShowDialog(this) != DialogResult.OK) return;
				ScheduleTemplate template = null;
				FormProgress.ShowProgress("Loading Schedule...", () =>
				{
					AsyncHelper.RunSync(async () =>
					{
						template = await BusinessObjects.Instance.ScheduleTemplatesManager.GetScheduleTemplate(templateInfo.Name);
						template.Name = form.ScheduleName;
					});
				}, false);
				BusinessObjects.Instance.ScheduleManager.AddScheduleFromTemplate(template);
			}
			DialogResult = DialogResult.OK;
			Close();
		}

		private void OnTemplatesRowClick(object sender, RowClickEventArgs e)
		{
			if (e.Clicks == 2)
				OpenTemplate();
		}
		#endregion
	}
}