﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using NewBizWiz.AdSchedule.Controls;
using NewBizWiz.Core.AdSchedule;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.Interop;
using ListManager = NewBizWiz.Core.AdSchedule.ListManager;
using SettingsManager = NewBizWiz.Core.Common.SettingsManager;

namespace NewBizWiz.Dashboard.TabNewspaperForms
{
	[ToolboxItem(false)]
	public partial class PrintScheduleBuilderControl : UserControl
	{
		private static PrintScheduleBuilderControl _instance;
		private ShortSchedule[] _scheduleList;

		private PrintScheduleBuilderControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			AppManager.Instance.SetClickEventHandler(this);
			if ((base.CreateGraphics()).DpiX > 96)
			{
				laTitle.Font = new Font(laTitle.Font.Name, laTitle.Font.Size - 5, laTitle.Font.Style);
				laNoDataWarning.Font = new Font(laNoDataWarning.Font.Name, laNoDataWarning.Font.Size - 5, laNoDataWarning.Font.Style);
				laNoSlidesWarningText1.Font = new Font(laNoSlidesWarningText1.Font.Name, laNoSlidesWarningText1.Font.Size - 3, laNoSlidesWarningText1.Font.Style);
				laNoSlidesWarningText2.Font = new Font(laNoSlidesWarningText2.Font.Name, laNoSlidesWarningText2.Font.Size - 3, laNoSlidesWarningText2.Font.Style);
				laNoSlidesWarningText3.Font = new Font(laNoSlidesWarningText3.Font.Name, laNoSlidesWarningText3.Font.Size - 3, laNoSlidesWarningText3.Font.Style);
			}
		}

		public static PrintScheduleBuilderControl Instance
		{
			get
			{
				if (_instance == null)
					_instance = new PrintScheduleBuilderControl();
				return _instance;
			}
		}

		public void LoadSchedules()
		{
			gridViewSchedules.FocusedRowChanged -= gridViewSchedules_FocusedRowChanged;
			OutsideClick();
			_scheduleList = AdSchedule.Internal.AppManager.GetShortScheduleList();
			if (!AdSchedule.Internal.AppManager.ProgramDataAvailable)
			{
				FormMain.Instance.buttonItemNewspaperNew.Enabled = false;
				gridControlSchedules.Visible = false;
				pnNoSlidesWarning.Visible = false;
				pnNoDataWarning.Visible = true;
				gridControlSchedules.DataSource = null;
			}
			else if (!Directory.Exists(MasterWizardManager.Instance.SelectedWizard.AdScheduleSlideFolder) || Directory.GetDirectories(MasterWizardManager.Instance.SelectedWizard.AdScheduleSlideFolder).Length == 0)
			{
				FormMain.Instance.buttonItemNewspaperNew.Enabled = false;
				gridControlSchedules.Visible = false;
				pnNoSlidesWarning.Visible = true;
				laNoSlidesWarningText2.Text = string.Format("{0} {1}", new object[] { MasterWizardManager.Instance.SelectedWizard.Name, SettingsManager.Instance.Size });
				pnNoDataWarning.Visible = false;
				gridControlSchedules.DataSource = null;
			}
			else
			{
				FormMain.Instance.buttonItemNewspaperNew.Enabled = true;
				gridControlSchedules.Visible = true;
				pnNoSlidesWarning.Visible = false;
				pnNoDataWarning.Visible = false;
				repositoryItemComboBoxStatus.Items.Clear();
				repositoryItemComboBoxStatus.Items.AddRange(ListManager.Instance.Statuses);
				gridControlSchedules.DataSource = new BindingList<ShortSchedule>(_scheduleList);
			}
			gridViewSchedules.FocusedRowChanged += gridViewSchedules_FocusedRowChanged;
		}

		public void OutsideClick()
		{
			gridViewSchedules.FocusedRowHandle = -1;
			gridViewSchedules.FocusedColumn = gridColumnBusinessName;
			gridViewSchedules.OptionsSelection.EnableAppearanceFocusedRow = false;
			FormMain.Instance.buttonItemNewspaperOpen.Enabled = false;
			FormMain.Instance.buttonItemNewspaperDelete.Enabled = false;
		}

		public void ExportEventHandler(object sender, ExportEventArgs e)
		{
			WinAPIHelper.PostMessage(RegistryHelper.MinibarHandle, WinAPIHelper.WM_APP + 9, 0, 0);
			FormMain.Instance.Opacity = 0;
			RegistryHelper.MaximizeMainForm = true;
			CalendarBuilder.FormMain.Instance.Resize -= FormMain.Instance.FormCalendarResize;
			CalendarBuilder.FormMain.Instance.Resize += FormMain.Instance.FormCalendarResize;
			CalendarBuilder.FormMain.Instance.FloaterRequested -= FormMain.Instance.buttonItemFloater_Click;
			CalendarBuilder.FormMain.Instance.FloaterRequested += FormMain.Instance.buttonItemFloater_Click;
			CalendarBuilder.AppManager.ImportSchedule(e.SourceSchedule, e.BuildAdvanced, e.BuildGraphic, e.BuildSimple);
			if (!FormMain.Instance.IsDead)
			{
				FormMain.Instance.Opacity = 1;
				RegistryHelper.MainFormHandle = FormMain.Instance.Handle;
				RegistryHelper.MaximizeMainForm = false;
			}
			WinAPIHelper.PostMessage(RegistryHelper.MinibarHandle, WinAPIHelper.WM_APP + 10, 0, 0);
			Utilities.Instance.ActivateMiniBar();
		}

		public void buttonXNewSchedule_Click(object sender, EventArgs e)
		{
			WinAPIHelper.PostMessage(RegistryHelper.MinibarHandle, WinAPIHelper.WM_APP + 1, 0, 0);
			FormMain.Instance.Opacity = 0;
			RegistryHelper.MaximizeMainForm = true;
			AdSchedule.Internal.FormMain.Instance.Resize -= FormMain.Instance.FormAdScheduleResize;
			AdSchedule.Internal.FormMain.Instance.Resize += FormMain.Instance.FormAdScheduleResize;
			AdSchedule.Internal.FormMain.Instance.FloaterRequested -= FormMain.Instance.buttonItemFloater_Click;
			AdSchedule.Internal.FormMain.Instance.FloaterRequested += FormMain.Instance.buttonItemFloater_Click;
			Controller.Instance.ScheduleExported -= ExportEventHandler;
			Controller.Instance.ScheduleExported += ExportEventHandler;
			AdSchedule.Internal.AppManager.NewSchedule();
			if (!FormMain.Instance.IsDead)
			{
				FormMain.Instance.Opacity = 1;
				RegistryHelper.MainFormHandle = FormMain.Instance.Handle;
				RegistryHelper.MaximizeMainForm = false;
				LoadSchedules();
			}
			WinAPIHelper.PostMessage(RegistryHelper.MinibarHandle, WinAPIHelper.WM_APP + 2, 0, 0);
			Utilities.Instance.ActivateMiniBar();
		}

		public void buttonXOpenSchedule_Click(object sender, EventArgs e)
		{
			WinAPIHelper.PostMessage(RegistryHelper.MinibarHandle, WinAPIHelper.WM_APP + 1, 0, 0);
			FormMain.Instance.Opacity = 0;
			RegistryHelper.MaximizeMainForm = true;
			AdSchedule.Internal.FormMain.Instance.Resize -= FormMain.Instance.FormAdScheduleResize;
			AdSchedule.Internal.FormMain.Instance.Resize += FormMain.Instance.FormAdScheduleResize;
			AdSchedule.Internal.FormMain.Instance.FloaterRequested -= FormMain.Instance.buttonItemFloater_Click;
			AdSchedule.Internal.FormMain.Instance.FloaterRequested += FormMain.Instance.buttonItemFloater_Click;
			Controller.Instance.ScheduleExported -= ExportEventHandler;
			Controller.Instance.ScheduleExported += ExportEventHandler;
			AdSchedule.Internal.AppManager.OpenSchedule(_scheduleList[gridViewSchedules.GetFocusedDataSourceRowIndex()].FullFileName);
			if (!FormMain.Instance.IsDead)
			{
				FormMain.Instance.Opacity = 1;
				RegistryHelper.MainFormHandle = FormMain.Instance.Handle;
				RegistryHelper.MaximizeMainForm = false;
				LoadSchedules();
			}
			WinAPIHelper.PostMessage(RegistryHelper.MinibarHandle, WinAPIHelper.WM_APP + 2, 0, 0);
			Utilities.Instance.ActivateMiniBar();
		}

		public void buttonXDeleteSchedule_Click(object sender, EventArgs e)
		{
			if (Utilities.Instance.ShowWarningQuestion("Delete this Ad Schedule?") == DialogResult.Yes)
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

		private void gridViewSchedules_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
		{
			gridViewSchedules.OptionsSelection.EnableAppearanceFocusedRow = true;
			FormMain.Instance.buttonItemNewspaperOpen.Enabled = gridViewSchedules.SelectedRowsCount > 0;
			FormMain.Instance.buttonItemNewspaperDelete.Enabled = gridViewSchedules.SelectedRowsCount > 0;
		}

		private void gridViewSchedules_RowClick(object sender, RowClickEventArgs e)
		{
			gridViewSchedules_FocusedRowChanged(null, null);
			if (e.Clicks == 2)
			{
				buttonXOpenSchedule_Click(null, null);
			}
		}

		private void gridControlSchedules_Click(object sender, EventArgs e)
		{
			OutsideClick();
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