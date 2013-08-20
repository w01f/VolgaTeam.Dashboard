using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using NewBizWiz.Core.Calendar;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.Interop;

namespace NewBizWiz.Dashboard.TabCalendarForms
{
	[ToolboxItem(false)]
	public partial class CalendarBuilderControl : UserControl
	{
		private static CalendarBuilderControl _instance;
		private ShortSchedule[] _calendarList;

		private CalendarBuilderControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			AppManager.Instance.SetClickEventHandler(this);
			if ((CreateGraphics()).DpiX > 96)
			{
				laTitle.Font = new Font(laTitle.Font.Name, laTitle.Font.Size - 5, laTitle.Font.Style);
				laNoDataWarning.Font = new Font(laNoDataWarning.Font.Name, laNoDataWarning.Font.Size - 5, laNoDataWarning.Font.Style);
			}
		}

		public static CalendarBuilderControl Instance
		{
			get
			{
				if (_instance == null)
					_instance = new CalendarBuilderControl();
				return _instance;
			}
		}

		public void LoadCalendars()
		{
			gridViewCalendars.FocusedRowChanged -= gridViewCalendars_FocusedRowChanged;
			OutsideClick();
			_calendarList = ScheduleManager.GetShortScheduleExtendedList();
			if (!Calendar.Internal.AppManager.ProgramDataAvailable)
			{
				FormMain.Instance.buttonItemCalendarNew.Enabled = false;
				gridControlCalendars.Visible = false;
				pnNoDataWarning.Visible = true;
				gridControlCalendars.DataSource = null;
			}
			else
			{
				FormMain.Instance.buttonItemCalendarNew.Enabled = true;
				gridControlCalendars.Visible = true;
				pnNoDataWarning.Visible = false;
				repositoryItemComboBoxStatus.Items.Clear();
				repositoryItemComboBoxStatus.Items.AddRange(Core.AdSchedule.ListManager.Instance.Statuses);
				gridControlCalendars.DataSource = new BindingList<ShortSchedule>(_calendarList);
			}
			gridViewCalendars.FocusedRowChanged += gridViewCalendars_FocusedRowChanged;
		}

		public void OutsideClick()
		{
			gridViewCalendars.FocusedRowHandle = -1;
			gridViewCalendars.FocusedColumn = gridColumnBusinessName;
			gridViewCalendars.OptionsSelection.EnableAppearanceFocusedRow = false;
			FormMain.Instance.buttonItemCalendarOpen.Enabled = false;
			FormMain.Instance.buttonItemCalendarDelete.Enabled = false;
		}

		public void buttonXNewCalendar_Click(object sender, EventArgs e)
		{
			WinAPIHelper.PostMessage(RegistryHelper.MinibarHandle, WinAPIHelper.WM_APP + 9, 0, 0);
			FormMain.Instance.Opacity = 0;
			RegistryHelper.MaximizeMainForm = true;
			Calendar.Internal.FormMain.Instance.Resize -= FormMain.Instance.FormCalendarResize;
			Calendar.Internal.FormMain.Instance.Resize += FormMain.Instance.FormCalendarResize;
			Calendar.Internal.FormMain.Instance.FloaterRequested -= FormMain.Instance.buttonItemFloater_Click;
			Calendar.Internal.FormMain.Instance.FloaterRequested += FormMain.Instance.buttonItemFloater_Click;
			Calendar.Internal.AppManager.NewSchedule();
			if (!FormMain.Instance.IsDead)
			{
				FormMain.Instance.Opacity = 1;
				RegistryHelper.MainFormHandle = FormMain.Instance.Handle;
				RegistryHelper.MaximizeMainForm = false;
				LoadCalendars();
			}
			WinAPIHelper.PostMessage(RegistryHelper.MinibarHandle, WinAPIHelper.WM_APP + 10, 0, 0);
			Utilities.Instance.ActivateMiniBar();
		}

		public void buttonXOpenCalendar_Click(object sender, EventArgs e)
		{
			var selectedSchedule = gridViewCalendars.GetFocusedRow() as ShortSchedule;
			if (selectedSchedule != null)
			{
				WinAPIHelper.PostMessage(RegistryHelper.MinibarHandle, WinAPIHelper.WM_APP + 9, 0, 0);
				FormMain.Instance.Opacity = 0;
				RegistryHelper.MaximizeMainForm = true;
				Calendar.Internal.FormMain.Instance.Resize -= FormMain.Instance.FormCalendarResize;
				Calendar.Internal.FormMain.Instance.Resize += FormMain.Instance.FormCalendarResize;
				Calendar.Internal.FormMain.Instance.FloaterRequested -= FormMain.Instance.buttonItemFloater_Click;
				Calendar.Internal.FormMain.Instance.FloaterRequested += FormMain.Instance.buttonItemFloater_Click;
				Calendar.Internal.AppManager.OpenSchedule(selectedSchedule.FullFileName);
				if (!FormMain.Instance.IsDead)
				{
					FormMain.Instance.Opacity = 1;
					RegistryHelper.MainFormHandle = FormMain.Instance.Handle;
					RegistryHelper.MaximizeMainForm = false;
					LoadCalendars();
				}
				WinAPIHelper.PostMessage(RegistryHelper.MinibarHandle, WinAPIHelper.WM_APP + 10, 0, 0);
				Utilities.Instance.ActivateMiniBar();
			}
		}

		public void buttonXDeleteCalendar_Click(object sender, EventArgs e)
		{
			if (Utilities.Instance.ShowWarningQuestion("Delete this Schedule?") == DialogResult.Yes)
			{
				string fileName = _calendarList[gridViewCalendars.GetFocusedDataSourceRowIndex()].FullFileName;
				try
				{
					if (File.Exists(fileName))
						File.Delete(fileName);
				}
				catch
				{
					Utilities.Instance.ShowWarning("Couldn't delete selected schedule.");
				}
				LoadCalendars();
			}
		}

		private void gridViewCalendars_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
		{
			gridViewCalendars.OptionsSelection.EnableAppearanceFocusedRow = true;
			FormMain.Instance.buttonItemCalendarOpen.Enabled = gridViewCalendars.SelectedRowsCount > 0;
			FormMain.Instance.buttonItemCalendarDelete.Enabled = gridViewCalendars.SelectedRowsCount > 0;
		}

		private void gridViewCalendars_RowClick(object sender, RowClickEventArgs e)
		{
			gridViewCalendars_FocusedRowChanged(null, null);
			if (e.Clicks == 2)
			{
				buttonXOpenCalendar_Click(null, null);
			}
		}

		private void gridControlCalendars_Click(object sender, EventArgs e)
		{
			OutsideClick();
		}

		private void gridViewCalendars_CellValueChanged(object sender, CellValueChangedEventArgs e)
		{
			ShortSchedule schedule = _calendarList[gridViewCalendars.GetDataSourceRowIndex(e.RowHandle)];
			schedule.Save();
		}

		private void repositoryItemComboBoxStatus_Closed(object sender, ClosedEventArgs e)
		{
			gridViewCalendars.CloseEditor();
		}
	}
}