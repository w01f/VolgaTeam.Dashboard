using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using NewBizWiz.OnlineSchedule.DigitalPackage.BusinessClasses;

namespace NewBizWiz.OnlineSchedule.DigitalPackage.PresentationClasses
{
	public partial class ScheduleListControl : UserControl
	{
		private int _hotTrackRow = GridControl.InvalidRowHandle;
		private int HotTrackRow
		{
			get
			{
				return _hotTrackRow;
			}
			set
			{
				if (_hotTrackRow != value)
				{
					int prevHotTrackRow = _hotTrackRow;
					_hotTrackRow = value;
					gridViewFiles.RefreshRow(prevHotTrackRow);
					gridViewFiles.RefreshRow(_hotTrackRow);
					if (_hotTrackRow >= 0)
						gridControlFiles.Cursor = Cursors.Hand;
					else
						gridControlFiles.Cursor = Cursors.Default;
				}

			}
		}

		[Browsable(true)]
		public event EventHandler<ScheduleEventArgs> ScheduleChanged;

		[Browsable(true)]
		public event EventHandler<EventArgs> ScheduleCreated;

		[Browsable(true)]
		public event EventHandler<EventArgs> ScheduleCloned;

		[Browsable(true)]
		public event EventHandler<ScheduleEventArgs> ScheduleDeleted;

		public ScheduleListControl()
		{
			InitializeComponent();
		}

		public void LoadSavedSchedules(Schedule currentSchedule)
		{
			var schedules = ScheduleManager.GetShortScheduleList().ToList();
			gridControlFiles.DataSource = schedules;
			var selectedSchedule = schedules.FirstOrDefault(f => currentSchedule.ScheduleFile == null || f.FullFileName.ToLower().Equals(currentSchedule.ScheduleFile.FullName.ToLower()));
			if (selectedSchedule != null && !currentSchedule.IsNameNotAssigned)
			{
				gridViewFiles.FocusedRowHandle = schedules.IndexOf(selectedSchedule);
				gridViewFiles.OptionsSelection.EnableAppearanceFocusedRow = true;
			}
			else
				gridViewFiles.OptionsSelection.EnableAppearanceFocusedRow = false;
		}

		private void gridViewFiles_RowCellClick(object sender, RowCellClickEventArgs e)
		{
			_hotTrackRow = GridControl.InvalidRowHandle;
			var focussedRecord = gridViewFiles.GetFocusedRow() as ShortSchedule;
			if (focussedRecord == null) return;
			if (ScheduleChanged != null)
				ScheduleChanged(this, new ScheduleEventArgs { ScheduleFilePath = focussedRecord.FullFileName });
		}

		private void gridViewFiles_MouseMove(object sender, MouseEventArgs e)
		{
			var view = sender as GridView;
			var info = view.CalcHitInfo(new Point(e.X, e.Y));
			HotTrackRow = info.InRowCell ? info.RowHandle : GridControl.InvalidRowHandle;
		}

		private void barLargeButtonItemAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			if (ScheduleCreated != null)
				ScheduleCreated(this, EventArgs.Empty);
		}

		private void barLargeButtonItemClone_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			if (ScheduleCloned != null)
				ScheduleCloned(this, EventArgs.Empty);
		}

		private void barLargeButtonItemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			var focussedRecord = gridViewFiles.GetFocusedRow() as ShortSchedule;
			if (focussedRecord == null) return;
			if (ScheduleDeleted != null)
				ScheduleDeleted(this, new ScheduleEventArgs { ScheduleFilePath = focussedRecord.FullFileName });
		}

		private void toolTipController_GetActiveObjectInfo(object sender, ToolTipControllerGetActiveObjectInfoEventArgs e)
		{
			if (e.SelectedControl != gridControlFiles) return;
			var info = e.Info;
			try
			{
				var view = gridControlFiles.GetViewAt(e.ControlMousePosition) as GridView;
				if (view == null)
					return;
				var hi = view.CalcHitInfo(e.ControlMousePosition);
				if (!hi.InRowCell) return;
				var schedule = view.GetRow(hi.RowHandle) as ShortSchedule;
				if (schedule == null) return;
				var fileInfo = new List<string>();
				fileInfo.Add(Path.GetFileName(schedule.FullFileName));
				fileInfo.Add(String.Format("Created: {0}", File.GetCreationTime(schedule.FullFileName).ToString("MM/dd/yy")));
				fileInfo.Add(String.Format("Modified: {0}", File.GetLastWriteTime(schedule.FullFileName).ToString("MM/dd/yy")));
				info = new ToolTipControlInfo(new CellToolTipInfo(hi.RowHandle, hi.Column, "cell"), String.Join(Environment.NewLine, fileInfo.ToArray()));
			}
			finally
			{
				e.Info = info;
			}
		}

		private void barLargeButtonItemHelp_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			BusinessWrapper.Instance.HelpManager.OpenHelpLink("filemgr");
		}
	}
}