using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Data;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using Asa.Calendar.Controls.PresentationClasses.Calendars;
using Asa.Calendar.Controls.ToolForms;
using Asa.Core.Calendar;
using Asa.Core.Common;

namespace Asa.Calendar.Controls.PresentationClasses.Views.GridView
{
	public partial class GridViewControl : UserControl, IView
	{
		private readonly List<CalendarDay> _days = new List<CalendarDay>();
		private bool _allowToSave;
		private CalendarMonth _selectedMonth;

		public GridViewControl(ICalendarControl calendar)
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			Calendar = calendar;

			#region Copy-Paster Initialization
			CopyPasteManager = new CopyPasteManager(this);
			CopyPasteManager.OnSetCopyDay += (sender, e) =>
			{
				Controller.Instance.CalendarVisualizer.CopyButtonItem.Enabled = true;
				toolStripMenuItemCopy.Enabled = true;
				Controller.Instance.CalendarVisualizer.CloneButtonItem.Enabled = true;
				toolStripMenuItemClone.Enabled = true;
				toolStripMenuItemDelete.Enabled = true;
			};
			CopyPasteManager.OnResetCopy += (sender, e) =>
			{
				Controller.Instance.CalendarVisualizer.CopyButtonItem.Enabled = false;
				toolStripMenuItemCopy.Enabled = false;
				Controller.Instance.CalendarVisualizer.CloneButtonItem.Enabled = false;
				toolStripMenuItemClone.Enabled = false;
				toolStripMenuItemDelete.Enabled = false;
			};
			CopyPasteManager.OnResetPaste += (sender, e) =>
			{
				Controller.Instance.CalendarVisualizer.PasteButtonItem.Enabled = false;
				toolStripMenuItemPaste.Enabled = false;
			};
			CopyPasteManager.DayCopied += (sender, e) =>
			{
				Controller.Instance.CalendarVisualizer.PasteButtonItem.Enabled = true;
				toolStripMenuItemPaste.Enabled = true;
			};
			CopyPasteManager.DayPasted += (sender, e) =>
			{
				Calendar.SlideInfo.LoadData();
				RefreshData();
				SettingsNotSaved = true;
			};
			#endregion

			repositoryItemTextEditCustomComment.Enter += Utilities.Instance.Editor_Enter;
			repositoryItemTextEditCustomComment.MouseDown += Utilities.Instance.Editor_MouseDown;
			repositoryItemTextEditCustomComment.MouseUp += Utilities.Instance.Editor_MouseUp;
			repositoryItemButtonEditCustomCommentFirstRow.Enter += Utilities.Instance.Editor_Enter;
			repositoryItemButtonEditCustomCommentFirstRow.MouseDown += Utilities.Instance.Editor_MouseDown;
			repositoryItemButtonEditCustomCommentFirstRow.MouseUp += Utilities.Instance.Editor_MouseUp;
		}

		#region Interface Methods
		public void LoadData(bool reload) { }

		public void Save()
		{
			gridViewComment.CloseEditor();
			bandedGridViewLogo.CloseEditor();
			if (DataSaved != null)
				DataSaved(this, new EventArgs());
			SettingsNotSaved = false;
		}

		public void RefreshData()
		{
			if (_selectedMonth != null)
			{
				bandedGridViewLogo.RefreshData();
				gridViewComment.RefreshData();
			}
		}

		public void ChangeMonth(DateTime date)
		{
			CalendarMonth calendarMonth = null;
			CopyPasteManager.ResetCopy();
			calendarMonth = Calendar.CalendarData.Months.Where(x => x.Date.Equals(date)).FirstOrDefault();
			if (calendarMonth != null)
			{
				_selectedMonth = calendarMonth;
				_allowToSave = false;
				_days.Clear();
				_days.AddRange(_selectedMonth.Days.Where(x => x.BelongsToSchedules).ToArray());
				_allowToSave = false;
				gridControlLogo.DataSource = null;
				gridControlLogo.DataSource = _days;
				bandedGridViewLogo.RefreshData();
				gridControlComment.DataSource = null;
				gridControlComment.DataSource = _days;
				gridViewComment.RefreshData();
				_allowToSave = true;

				CopyPasteManager.SetCopyDay();
			}
		}

		public void SelectDay(CalendarDay day, bool selected) { }

		#region Copy-Paste Methods and Event Handlers
		public void CopyDay()
		{
			CalendarDay selectedDay = GetSelectedDays().FirstOrDefault();
			if (selectedDay != null)
			{
				CopyPasteManager.CopyDay(selectedDay);
				gridViewComment.RefreshData();
				bandedGridViewLogo.RefreshData();
			}
		}

		public void PasteDay()
		{
			CalendarDay[] selectedDays = GetSelectedDays();
			if (selectedDays != null)
				CopyPasteManager.PasteDay(selectedDays);
		}

		public void CloneDay()
		{
			CalendarDay[] clonedDays = null;
			CalendarDay selectedDay = GetSelectedDays().FirstOrDefault();
			if (selectedDay != null)
			{
				using (var form = new FormCloneDay(selectedDay, Calendar.CalendarData.Schedule.FlightDateStart.Value, Calendar.CalendarData.Schedule.FlightDateEnd.Value))
				{
					if (form.ShowDialog() == DialogResult.OK)
						clonedDays = Calendar.CalendarData.Days.Where(x => form.SelectedDates.Contains(x.Date)).ToArray();
				}
				if (clonedDays != null)
					CopyPasteManager.CloneDay(selectedDay, clonedDays);
			}
		}

		public CalendarDay[] GetSelectedDays()
		{
			var selectedDays = new List<CalendarDay>();
			int[] selectedRowHandles = gridViewComment.GetSelectedRows();
			foreach (int rowHandle in selectedRowHandles)
			{
				CalendarDay selectedDay = _selectedMonth.Days.Where(x => x.BelongsToSchedules).ElementAt(rowHandle);
				if (selectedDay != null)
					selectedDays.Add(selectedDay);
			}
			return selectedDays.ToArray();
		}

		public void DeleteDayData()
		{
			CalendarDay[] selectedDays = GetSelectedDays();
			foreach (CalendarDay day in selectedDays)
				day.ClearData();
			RefreshData();
		}
		#endregion
		#endregion

		#region Common Event Handlers
		private void propertiesControl_PropertiesChanged(object sender, EventArgs e)
		{
			if (_allowToSave)
			{
				Calendar.UpdateOutputFunctions();
				SettingsNotSaved = true;
			}
		}

		private void gridView_RowCellStyle(object sender, RowCellStyleEventArgs e)
		{
			var view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
			int[] selectedRowHandles = view.GetSelectedRows();
			if (view != null && !selectedRowHandles.Contains(e.RowHandle) && e.CellValue == null)
				e.Appearance.ForeColor = Color.Gray;
			if (view != null && !selectedRowHandles.Contains(e.RowHandle) && CopyPasteManager.SourceDay != null && _days[e.RowHandle].Equals(CopyPasteManager.SourceDay.Date))
				e.Appearance.BackColor = Color.FromArgb(192, 255, 192);
		}

		private void gridView_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
		{
			if (!e.HitInfo.InRowCell)
				e.Allow = false;
		}
		#endregion

		#region Comment Event Handlers
		private void gridViewComment_CellValueChanged(object sender, CellValueChangedEventArgs e)
		{
			propertiesControl_PropertiesChanged(null, null);
		}

		private void gridViewComment_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
		{
			if (xtraTabControl.SelectedTabPage == xtraTabPageComment)
				bandedGridViewLogo.FocusedRowHandle = gridViewComment.FocusedRowHandle;
		}

		private void gridViewComment_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
		{
			if (e.Column == gridColumnComment1 || e.Column == gridColumnComment2)
			{
				if (e.RowHandle == 0)
					e.RepositoryItem = repositoryItemButtonEditCustomCommentFirstRow;
				else
					e.RepositoryItem = repositoryItemTextEditCustomComment;
			}
		}

		private void repositoryItemComboBoxCustomNoteFirstRow_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			gridViewComment.CloseEditor();
			object value = gridViewComment.GetRowCellValue(0, gridViewComment.FocusedColumn);
			for (int i = 1; i < gridViewComment.RowCount; i++)
				gridViewComment.SetRowCellValue(i, gridViewComment.FocusedColumn, value);
		}
		#endregion

		#region Logo Event Handlers
		private void bandedGridViewLogo_RowCellStyle(object sender, RowCellStyleEventArgs e)
		{
			var view = sender as BandedGridView;
			int[] selectedRowHandles = view.GetSelectedRows();
			if (view != null && !selectedRowHandles.Contains(e.RowHandle) && e.CellValue == null)
				e.Appearance.ForeColor = Color.Gray;
			if (view != null && !selectedRowHandles.Contains(e.RowHandle) && CopyPasteManager.SourceDay != null && _days[e.RowHandle].Equals(CopyPasteManager.SourceDay.Date))
				e.Appearance.BackColor = Color.FromArgb(192, 255, 192);
		}

		private void bandedGridViewLogo_CellValueChanged(object sender, CellValueChangedEventArgs e)
		{
			propertiesControl_PropertiesChanged(null, null);
		}

		private void bandedGridViewLogo_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
		{
			if (xtraTabControl.SelectedTabPage == xtraTabPageLogo)
			{
				gridViewComment.FocusedRowHandle = bandedGridViewLogo.FocusedRowHandle;
			}
		}

		private void bandedGridViewLogo_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
		{
			if (e.Column == bandedGridColumnLogoSelector)
			{
				if (e.RowHandle == 0)
					e.RepositoryItem = repositoryItemButtonEditLogoSelectorFirstRow;
				else
					e.RepositoryItem = repositoryItemButtonEditLogoSelector;
			}
		}

		private void repositoryItemButtonEditLogoSelector_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			if (e.Button.Index == 0)
			{
				bandedGridViewLogo.CloseEditor();
				object bigImage = bandedGridViewLogo.GetRowCellValue(0, bandedGridColumnBigImage);
				object smallImage = bandedGridViewLogo.GetRowCellValue(0, bandedGridColumnSmallImage);
				object tinyImage = bandedGridViewLogo.GetRowCellValue(0, bandedGridColumnTinyLogo);
				object xtraTinyImage = bandedGridViewLogo.GetRowCellValue(0, bandedGridColumnXtraTinyImage);
				for (int i = 1; i < bandedGridViewLogo.RowCount; i++)
				{
					bandedGridViewLogo.SetRowCellValue(i, bandedGridColumnBigImage, bigImage);
					bandedGridViewLogo.SetRowCellValue(i, bandedGridColumnSmallImage, smallImage);
					bandedGridViewLogo.SetRowCellValue(i, bandedGridColumnTinyLogo, tinyImage);
					bandedGridViewLogo.SetRowCellValue(i, bandedGridColumnXtraTinyImage, xtraTinyImage);
				}
			}
			else if (e.Button.Index == 1)
			{
				using (var form = new FormImageGallery())
				{
					if (form.ShowDialog() == DialogResult.OK && form.SelectedSource != null && form.SelectedSource.BigImage != null && form.SelectedSource.SmallImage != null && form.SelectedSource.TinyImage != null && form.SelectedSource.XtraTinyImage != null)
					{
						bandedGridViewLogo.SetRowCellValue(bandedGridViewLogo.FocusedRowHandle, bandedGridColumnBigImage, new Bitmap(form.SelectedSource.BigImage));
						bandedGridViewLogo.SetRowCellValue(bandedGridViewLogo.FocusedRowHandle, bandedGridColumnSmallImage, new Bitmap(form.SelectedSource.SmallImage));
						bandedGridViewLogo.SetRowCellValue(bandedGridViewLogo.FocusedRowHandle, bandedGridColumnTinyLogo, new Bitmap(form.SelectedSource.TinyImage));
						bandedGridViewLogo.SetRowCellValue(bandedGridViewLogo.FocusedRowHandle, bandedGridColumnXtraTinyImage, new Bitmap(form.SelectedSource.XtraTinyImage));
					}
				}
			}
		}

		private void bandedGridViewLogo_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (_allowToSave)
			{
				int[] rowHandles = bandedGridViewLogo.GetSelectedRows();
				gridViewComment.ClearSelection();
				foreach (int rowHandle in rowHandles)
					gridViewComment.SelectRow(rowHandle);
			}
		}
		#endregion

		#region Context Menu Event Handlers
		private void toolStripMenuItemCopy_Click(object sender, EventArgs e)
		{
			CopyDay();
		}

		private void toolStripMenuItemPaste_Click(object sender, EventArgs e)
		{
			PasteDay();
		}

		private void toolStripMenuItemClone_Click(object sender, EventArgs e)
		{
			CloneDay();
		}

		private void toolStripMenuItemDelete_Click(object sender, EventArgs e)
		{
			DeleteDayData();
		}
		#endregion

		#region IView Members
		public ICalendarControl Calendar { get; private set; }
		public CopyPasteManager CopyPasteManager { get; private set; }

		public bool SettingsNotSaved { get; set; }
		public string Title
		{
			get { return "Grid"; }
		}
		public event EventHandler<EventArgs> DataSaved;
		#endregion
	}
}