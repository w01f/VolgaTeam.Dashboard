using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Common.Entities.NonPersistent.ScheduleTemplates;
using Asa.Business.Common.Enums;
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
			barLargeButtonItemOpen.Glyph = BusinessObjects.Instance.ImageResourcesManager.HomeOpenSchedulePopupImage ?? barLargeButtonItemOpen.Glyph;
			barLargeButtonItemDelete.Glyph = BusinessObjects.Instance.ImageResourcesManager.HomeDeleteSchedulePopupImage ?? barLargeButtonItemDelete.Glyph;
			gridColumnRegularSchedulesLastModifiedDate.SortIndex = 0;
			gridColumnRegularSchedulesLastModifiedDate.SortOrder = ColumnSortOrder.Descending;
			gridColumnQuickEditSchedulesLastModifiedDate.SortIndex = 0;
			gridColumnQuickEditSchedulesLastModifiedDate.SortOrder = ColumnSortOrder.Descending;
		}

		private void OnFormLoad(object sender, EventArgs e)
		{
			LoadSchedules();
			xtraTabPageRegularSchedules.PageEnabled = _regularScheduleList.Any();
			xtraTabPageQuickEditSchedules.PageEnabled = _quickScheduleList.Any();
			xtraTabPageTemplates.PageEnabled = !FileStorageManager.Instance.UseLocalMode;
			if (_regularScheduleList.Any())
				xtraTabControl.SelectedTabPage = xtraTabPageRegularSchedules;
			else if (_quickScheduleList.Any())
				xtraTabControl.SelectedTabPage = xtraTabPageQuickEditSchedules;
			else if (FileStorageManager.Instance.UseLocalMode)
				xtraTabControl.SelectedTabPage = xtraTabPageTemplates;
		}

		private void OnScheduleOpenItemClick(object sender, ItemClickEventArgs e)
		{
			if (xtraTabControl.SelectedTabPage == xtraTabPageRegularSchedules || xtraTabControl.SelectedTabPage == xtraTabPageQuickEditSchedules)
				OpenSchedule();
			else if (xtraTabControl.SelectedTabPage == xtraTabPageTemplates)
				OpenTemplate();
		}

		private void OnScheduleDeleteDeleteItemClick(object sender, ItemClickEventArgs e)
		{
			DeleteSchedule();
		}

		private void OnSelectedTabPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
		{
			if (e.Page == xtraTabPageTemplates && _scheduleTemplateList == null)
				LoadTemplates();
			barLargeButtonItemDelete.Enabled = e.Page == xtraTabPageRegularSchedules || e.Page == xtraTabPageQuickEditSchedules;
		}

		#region Schedule Management
		private readonly List<MediaScheduleModel> _regularScheduleList = new List<MediaScheduleModel>();
		private readonly List<MediaScheduleModel> _quickScheduleList = new List<MediaScheduleModel>();

		public void LoadSchedules()
		{
			var allSchedules = BusinessObjects.Instance.ScheduleManager.GetScheduleList<MediaScheduleModel>()
				.Where(scheduleModel => scheduleModel.Parent != BusinessObjects.Instance.ScheduleManager.ActiveSchedule).ToList();
			_regularScheduleList.AddRange(allSchedules.Where(s => s.EditMode == ScheduleEditMode.Regular));
			_quickScheduleList.AddRange(allSchedules.Where(s => s.EditMode == ScheduleEditMode.Quick));
			repositoryItemComboBoxStatus.Items.Clear();
			repositoryItemComboBoxStatus.Items.AddRange(MediaMetaData.Instance.ListManager.Statuses);
			gridControlRegularSchedules.DataSource = _regularScheduleList;
			gridControlQuickEditSchedules.DataSource = _quickScheduleList;
			if (gridViewRegularSchedules.RowCount > 0)
				gridViewRegularSchedules.FocusedRowHandle = 0;
		}

		private void OpenSchedule()
		{
			MediaScheduleModel schedule = null;
			if (xtraTabControl.SelectedTabPage == xtraTabPageRegularSchedules)
				schedule = gridViewRegularSchedules.GetFocusedRow() as MediaScheduleModel;
			else if (xtraTabControl.SelectedTabPage == xtraTabPageQuickEditSchedules)
				schedule = gridViewQuickEditSchedules.GetFocusedRow() as MediaScheduleModel;
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
			MediaScheduleModel schedule = null;
			if (xtraTabControl.SelectedTabPage == xtraTabPageRegularSchedules)
				schedule = gridViewRegularSchedules.GetFocusedRow() as MediaScheduleModel;
			else if (xtraTabControl.SelectedTabPage == xtraTabPageQuickEditSchedules)
				schedule = gridViewQuickEditSchedules.GetFocusedRow() as MediaScheduleModel;
			if (schedule == null)
			{
				PopupMessageHelper.Instance.ShowWarning("Please select schedule in list");
				return;
			}
			if (PopupMessageHelper.Instance.ShowWarningQuestion("Delete this file?") != DialogResult.Yes) return;
			BusinessObjects.Instance.ScheduleManager.DeleteSchedule(schedule.Parent);
			if (xtraTabControl.SelectedTabPage == xtraTabPageRegularSchedules)
			{
				_regularScheduleList.Remove(schedule);
				gridViewRegularSchedules.RefreshData();
			}
			else if (xtraTabControl.SelectedTabPage == xtraTabPageQuickEditSchedules)
			{
				_quickScheduleList.Remove(schedule);
				gridViewQuickEditSchedules.RefreshData();
			}
		}

		private void OnSchedulesViewRowClick(object sender, RowClickEventArgs e)
		{
			if (e.Clicks == 2)
				OpenSchedule();
		}

		private void OnScheduleStatusChanged(object sender, CellValueChangedEventArgs e)
		{
			var schedule = gridViewRegularSchedules.GetFocusedRow() as MediaScheduleModel;
			BusinessObjects.Instance.ScheduleManager.SaveScheduleModel(schedule);
		}

		private void OnStatusComboBoxClosed(object sender, ClosedEventArgs e)
		{
			gridViewRegularSchedules.CloseEditor();
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