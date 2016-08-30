using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Media.Entities.NonPersistent.Snapshot;
using Asa.Common.GUI.RetractableBar;
using Asa.Media.Controls.Properties;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraTab;
using DateRange = Asa.Business.Common.Entities.NonPersistent.Common.DateRange;

namespace Asa.Media.Controls.PresentationClasses.SnapshotControls.Settings
{
	//public partial class ActiveWeeksSettingsControl : UserControl
	public partial class ActiveWeeksSettingsControl : XtraTabPage, ISnapshotSettingsControl
	{
		private bool _allowToSave;
		private Snapshot _snapshotData;

		public int Order => 2;
		public bool IsAvailable => true;
		public ButtonInfo BarButton { get; }
		public SnapshotSettingsType SettingsType => SnapshotSettingsType.Calendar;
		public event EventHandler<SettingsChangedEventArgs> DataChanged;

		public ActiveWeeksSettingsControl()
		{
			InitializeComponent();
			Text = "Active Weeks";
			BarButton = new ButtonInfo
			{
				Logo = Resources.SnapshotSettingsActiveWeeks,
				Tooltip = "Open Calendar Info",
				Action = () => { TabControl.SelectedTabPage = this; }
			};
			if ((CreateGraphics()).DpiX > 96)
			{
				var font = new Font(styleController.Appearance.Font.FontFamily, styleController.Appearance.Font.Size - 2,
					styleController.Appearance.Font.Style);
				styleController.Appearance.Font = font;
				styleController.AppearanceDisabled.Font = font;
				styleController.AppearanceDropDown.Font = font;
				styleController.AppearanceDropDownHeader.Font = font;
				styleController.AppearanceFocused.Font = font;
				styleController.AppearanceReadOnly.Font = font;

				laActiveWeeksWarning.Font = new Font(laActiveWeeksWarning.Font.FontFamily, laActiveWeeksWarning.Font.Size - 2,
					laActiveWeeksWarning.Font.Style);

				font = new Font(buttonXSelectAll.Font.FontFamily, buttonXSelectAll.Font.Size - 2,
					buttonXSelectAll.Font.Style);
				buttonXSelectAll.Font = font;
				buttonXClearAll.Font = font;
			}
		}

		public void LoadSnapshotData(Snapshot snapshotData)
		{
			_snapshotData = snapshotData;

			_allowToSave = false;

			checkedListBoxActiveWeeks.Items.Clear();
			var scheduleWeekRanges = _snapshotData.Parent.ScheduleSettings.GetWeeks();
			var snapshotWeeks = _snapshotData.ActiveWeeks;
			checkedListBoxActiveWeeks.Items.AddRange(scheduleWeekRanges
				.Select(w =>
					new CheckedListBoxItem(
						w,
						w.Range,
						!snapshotWeeks.Any() || snapshotWeeks.Any(sw => sw.StartDate == w.StartDate && sw.FinishDate == w.FinishDate) ?
						CheckState.Checked :
						CheckState.Unchecked))
				.ToArray());

			laActiveWeeksWarning.Visible = checkedListBoxActiveWeeks.CheckedItems.Count == 0;

			_allowToSave = true;
		}

		private void OnItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
		{
			laActiveWeeksWarning.Visible = checkedListBoxActiveWeeks.CheckedItems.Count == 0;

			if (!_allowToSave) return;

			_snapshotData.ActiveWeeks.Clear();
			if (checkedListBoxActiveWeeks.CheckedItems.Count != checkedListBoxActiveWeeks.ItemCount)
				_snapshotData.ActiveWeeks.AddRange(checkedListBoxActiveWeeks.CheckedItems
					.OfType<CheckedListBoxItem>()
					.Select(item => item.Value).OfType<DateRange>());

			DataChanged?.Invoke(this, new SettingsChangedEventArgs { ChangedSettingsType = SettingsType });
		}

		private void OnSelectAll_Click(object sender, EventArgs e)
		{
			checkedListBoxActiveWeeks.CheckAll();
		}

		private void OnClearAll_Click(object sender, EventArgs e)
		{
			checkedListBoxActiveWeeks.UnCheckAll();
		}
	}
}
