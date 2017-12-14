using System;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Media.Entities.NonPersistent.Snapshot;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.RetractableBar;
using Asa.Media.Controls.BusinessClasses.Managers;
using Asa.Media.Controls.Properties;
using DevExpress.Skins;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraLayout.Utils;
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
				Logo = BusinessObjects.Instance.ImageResourcesManager.SnapshotsRetractableBarActiveWeeksImage ?? Resources.SnapshotSettingsActiveWeeks,
				Tooltip = "Open Calendar Info",
				Action = () => { TabControl.SelectedTabPage = this; }
			};

			checkedListBoxActiveWeeks.ItemHeight = (Int32)(checkedListBoxActiveWeeks.ItemHeight * Utilities.GetScaleFactor(CreateGraphics().DpiX).Height);

			simpleLabelItemTitle.MaxSize = RectangleHelper.ScaleSize(simpleLabelItemTitle.MaxSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			simpleLabelItemTitle.MinSize = RectangleHelper.ScaleSize(simpleLabelItemTitle.MinSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemSelectAll.MaxSize = RectangleHelper.ScaleSize(layoutControlItemSelectAll.MaxSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemSelectAll.MinSize = RectangleHelper.ScaleSize(layoutControlItemSelectAll.MinSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemClearAll.MaxSize = RectangleHelper.ScaleSize(layoutControlItemClearAll.MaxSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemClearAll.MinSize = RectangleHelper.ScaleSize(layoutControlItemClearAll.MinSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			simpleLabelItemWarning.MaxSize = RectangleHelper.ScaleSize(simpleLabelItemWarning.MaxSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			simpleLabelItemWarning.MinSize = RectangleHelper.ScaleSize(simpleLabelItemWarning.MinSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
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

			simpleLabelItemWarning.Visibility = checkedListBoxActiveWeeks.CheckedItems.Count == 0 ? LayoutVisibility.Always : LayoutVisibility.Never;

			_allowToSave = true;
		}

		private void OnItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
		{
			simpleLabelItemWarning.Visibility = checkedListBoxActiveWeeks.CheckedItems.Count == 0 ? LayoutVisibility.Always : LayoutVisibility.Never;

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
