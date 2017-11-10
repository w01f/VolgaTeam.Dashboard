using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Asa.Business.Media.Entities.NonPersistent.Calendar;
using Asa.Business.Media.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.RetractableBar;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraLayout.Utils;

namespace Asa.Media.Controls.PresentationClasses.Calendar
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
			if (Month != null && ((BroadcastCalendar)Month.Parent).AllowSelectDataSource)
				chapters.Add(
					new ButtonInfo
					{
						Logo = Properties.Resources.CalendarOptionsDataSource,
						Tooltip = "Open Data Options",
						Action = () => { tabbedControlGroup.SelectedTabPage = layoutControlGroupDataSettings; }
					});
			chapters.AddRange(base.GetChapters());
			return chapters;
		}

		public override void LoadCurrentMonthData()
		{
			base.LoadCurrentMonthData();

			if (Month == null) return;
			_allowToSave = false;

			#region Data Source
			layoutControlGroupDataSettings.Visibility = ((BroadcastCalendar)Month.Parent).AllowSelectDataSource ? LayoutVisibility.Always : LayoutVisibility.Never;

			var broadcastCalendar = (BroadcastCalendar)Month.Parent;

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
			if (PopupMessageHelper.Instance.ShowWarningQuestion("Are you SURE you want to change Data Source and RESET your calendar to the default Information?") == DialogResult.Yes) return;
			e.Cancel = true;
		}

		protected void OnDataSourceChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			var editor = sender as CheckEdit;
			if (editor == null || !editor.Checked) return;
			var broadcastCalendar = (BroadcastCalendar)Month.Parent;
			if (checkEditDataSourceSchedule.Checked)
				broadcastCalendar.DataSourceType = BroadcastDataTypeEnum.Schedule;
			else if (checkEditDataSourceSnapshots.Checked)
				broadcastCalendar.DataSourceType = BroadcastDataTypeEnum.Snapshots;
			OnPropertyChanged(new DataSourceChangedEventArgs());
		}
	}

	class DataSourceChangedEventArgs : EventArgs { }
}
