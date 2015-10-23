using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using Asa.CommonGUI.RetractableBar;
using Asa.Core.Common;
using Asa.Core.MediaSchedule;

namespace Asa.MediaSchedule.Controls.PresentationClasses.Calendar
{
	class BroadcastSlideInfoControl : CalendarSlideInfoControl
	{
		public BroadcastSlideInfoControl()
		{
			#region Comment
			checkEditDataSourceSchedule.EditValueChanging += BeforeDataSourceChange;
			checkEditDataSourceSnapshots.EditValueChanging += BeforeDataSourceChange;
			checkEditDataSourceSchedule.CheckedChanged += OnDataSourceChanged;
			checkEditDataSourceSnapshots.CheckedChanged += OnDataSourceChanged;
			#endregion
		}

		public override IEnumerable<ButtonInfo> GetChapters()
		{
			var chapters = new List<ButtonInfo>();
			if (_month != null && ((BroadcastCalendar)_month.Parent).AllowSelectDataSource)
				chapters.Add(
					new ButtonInfo
					{
						Logo = Properties.Resources.CalendarOptionsDataSource,
						Tooltip = "Open Data Options",
						Action = () => { xtraTabControl.SelectedTabPage = xtraTabPageData; }
					});
			chapters.AddRange(base.GetChapters());
			return chapters;
		}

		public override void LoadCurrentMonthData()
		{
			base.LoadCurrentMonthData();

			if (_month == null) return;
			_allowToSave = false;

			#region Data Source
			xtraTabPageData.PageVisible = ((BroadcastCalendar)_month.Parent).AllowSelectDataSource;

			var broadcastCalendar = (BroadcastCalendar)_month.Parent;

			checkEditDataSourceSchedule.Checked = false;
			checkEditDataSourceSnapshots.Checked = false;
			switch (broadcastCalendar.DataSourceType)
			{
				case BroadcastDataTypeEnum.Schedule:
					checkEditDataSourceSchedule.Checked = true;
					break;
				case BroadcastDataTypeEnum.Snapshots:
					checkEditDataSourceSnapshots.Checked = true;
					break;
			}
			#endregion

			_allowToSave = true;
			SettingsNotSaved = false;
		}

		private void BeforeDataSourceChange(object sender, ChangingEventArgs e)
		{
			if (!_allowToSave) return;
			var value = (bool)e.NewValue;
			if (!value) return;
			if (Utilities.Instance.ShowWarningQuestion("Are you SURE you want to change Data Source and RESET your calendar to the default Information?") == DialogResult.Yes) return;
			e.Cancel = true;
		}

		protected void OnDataSourceChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			var editor = sender as CheckEdit;
			if (editor == null || !editor.Checked) return;
			var broadcastCalendar = (BroadcastCalendar)_month.Parent;
			if (checkEditDataSourceSchedule.Checked)
				broadcastCalendar.DataSourceType = BroadcastDataTypeEnum.Schedule;
			else if (checkEditDataSourceSnapshots.Checked)
				broadcastCalendar.DataSourceType = BroadcastDataTypeEnum.Snapshots;
			OnPropertyChanged(new DataSourceChangedEventArgs());
		}
	}

	class DataSourceChangedEventArgs : EventArgs { }
}
