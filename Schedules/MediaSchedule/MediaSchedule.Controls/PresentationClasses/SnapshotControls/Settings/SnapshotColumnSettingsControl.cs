using System;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Media.Entities.NonPersistent.Snapshot;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.RetractableBar;
using Asa.Media.Controls.Properties;
using DevExpress.Skins;
using DevExpress.XtraTab;

namespace Asa.Media.Controls.PresentationClasses.SnapshotControls.Settings
{
	//public partial class SnapshotColumnSettingsControl : UserControl
	public partial class SnapshotColumnSettingsControl : XtraTabPage, ISnapshotSettingsControl
	{
		private bool _allowToSave;
		private Snapshot _snapshotData;

		public int Order => 1;
		public bool IsAvailable => true;
		public ButtonInfo BarButton { get; }
		public SnapshotSettingsType SettingsType => SnapshotSettingsType.Schedule;
		public event EventHandler<SettingsChangedEventArgs> DataChanged;

		public SnapshotColumnSettingsControl()
		{
			InitializeComponent();
			Text = "Info";
			BarButton = new ButtonInfo
			{
				Logo = Resources.SectionSettingsInfo,
				Tooltip = "Open Schedule Info",
				Action = () => { TabControl.SelectedTabPage = this; }
			};

			var scaleFactor = Utilities.GetScaleFactor(CreateGraphics().DpiX);
			layoutControlItemStation.MaxSize = RectangleHelper.ScaleSize(layoutControlItemStation.MaxSize, scaleFactor);
			layoutControlItemStation.MinSize = RectangleHelper.ScaleSize(layoutControlItemStation.MinSize, scaleFactor);
			layoutControlItemLength.MaxSize = RectangleHelper.ScaleSize(layoutControlItemLength.MaxSize, scaleFactor);
			layoutControlItemLength.MinSize = RectangleHelper.ScaleSize(layoutControlItemLength.MinSize, scaleFactor);
			layoutControlItemProgram.MaxSize = RectangleHelper.ScaleSize(layoutControlItemProgram.MaxSize, scaleFactor);
			layoutControlItemProgram.MinSize = RectangleHelper.ScaleSize(layoutControlItemProgram.MinSize, scaleFactor);
			layoutControlItemDaypart.MaxSize = RectangleHelper.ScaleSize(layoutControlItemDaypart.MaxSize, scaleFactor);
			layoutControlItemDaypart.MinSize = RectangleHelper.ScaleSize(layoutControlItemDaypart.MinSize, scaleFactor);
			layoutControlItemTime.MaxSize = RectangleHelper.ScaleSize(layoutControlItemTime.MaxSize, scaleFactor);
			layoutControlItemTime.MinSize = RectangleHelper.ScaleSize(layoutControlItemTime.MinSize, scaleFactor);
			layoutControlItemRate.MaxSize = RectangleHelper.ScaleSize(layoutControlItemRate.MaxSize, scaleFactor);
			layoutControlItemRate.MinSize = RectangleHelper.ScaleSize(layoutControlItemRate.MinSize, scaleFactor);
			layoutControlItemWeeklySpots.MaxSize = RectangleHelper.ScaleSize(layoutControlItemWeeklySpots.MaxSize, scaleFactor);
			layoutControlItemWeeklySpots.MinSize = RectangleHelper.ScaleSize(layoutControlItemWeeklySpots.MinSize, scaleFactor);
			layoutControlItemWeeklyCost.MaxSize = RectangleHelper.ScaleSize(layoutControlItemWeeklyCost.MaxSize, scaleFactor);
			layoutControlItemWeeklyCost.MinSize = RectangleHelper.ScaleSize(layoutControlItemWeeklyCost.MinSize, scaleFactor);
			layoutControlItemTotalSpots.MaxSize = RectangleHelper.ScaleSize(layoutControlItemTotalSpots.MaxSize, scaleFactor);
			layoutControlItemTotalSpots.MinSize = RectangleHelper.ScaleSize(layoutControlItemTotalSpots.MinSize, scaleFactor);
			layoutControlItemTotalCost.MaxSize = RectangleHelper.ScaleSize(layoutControlItemTotalCost.MaxSize, scaleFactor);
			layoutControlItemTotalCost.MinSize = RectangleHelper.ScaleSize(layoutControlItemTotalCost.MinSize, scaleFactor);
			layoutControlItemLogo.MaxSize = RectangleHelper.ScaleSize(layoutControlItemLogo.MaxSize, scaleFactor);
			layoutControlItemLogo.MinSize = RectangleHelper.ScaleSize(layoutControlItemLogo.MinSize, scaleFactor);
			layoutControlItemAvgRate.MaxSize = RectangleHelper.ScaleSize(layoutControlItemAvgRate.MaxSize, scaleFactor);
			layoutControlItemAvgRate.MinSize = RectangleHelper.ScaleSize(layoutControlItemAvgRate.MinSize, scaleFactor);
			layoutControlItemLineId.MaxSize = RectangleHelper.ScaleSize(layoutControlItemLineId.MaxSize, scaleFactor);
			layoutControlItemLineId.MinSize = RectangleHelper.ScaleSize(layoutControlItemLineId.MinSize, scaleFactor);
			layoutControlItemTotalRow.MaxSize = RectangleHelper.ScaleSize(layoutControlItemTotalRow.MaxSize, scaleFactor);
			layoutControlItemTotalRow.MinSize = RectangleHelper.ScaleSize(layoutControlItemTotalRow.MinSize, scaleFactor);
		}

		public void LoadSnapshotData(Snapshot snapshotData)
		{
			_snapshotData = snapshotData;

			_allowToSave = false;

			checkEditApplyForAll.Checked = _snapshotData.Parent.SnapshotSummary.ApplySettingsForAll;
			buttonXLineId.Checked = _snapshotData.ShowLineId;
			buttonXStation.Checked = _snapshotData.ShowStation;
			buttonXLength.Checked = _snapshotData.ShowLenght;
			buttonXProgram.Checked = _snapshotData.ShowProgram;
			buttonXDaypart.Checked = _snapshotData.ShowDaypart;
			buttonXTime.Checked = _snapshotData.ShowTime;
			buttonXRate.Checked = _snapshotData.ShowRate;
			buttonXWeeklySpots.Checked = _snapshotData.ShowWeeklySpots;
			buttonXWeeklyCost.Checked = _snapshotData.ShowWeeklyCost;
			buttonXTotalSpots.Checked = _snapshotData.ShowTotalSpots;
			buttonXTotalCost.Checked = _snapshotData.ShowTotalCost;
			buttonXLogo.Checked = _snapshotData.ShowLogo;
			buttonXAvgRate.Checked = _snapshotData.ShowAverageRate;
			buttonXTotalRow.Checked = _snapshotData.ShowTotalRow;

			_allowToSave = true;

			UpdateUniversalSettingsToggleVisibility();
		}

		public void UpdateUniversalSettingsToggleVisibility()
		{
			checkEditApplyForAll.Visible = _snapshotData.Parent.Snapshots.Count > 1;
		}

		private void OnSettingsChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;

			_snapshotData.Parent.SnapshotSummary.ApplySettingsForAll = checkEditApplyForAll.Checked;
			_snapshotData.ShowLineId = buttonXLineId.Checked;
			_snapshotData.ShowStation = buttonXStation.Checked;
			_snapshotData.ShowLenght = buttonXLength.Checked;
			_snapshotData.ShowProgram = buttonXProgram.Checked;
			_snapshotData.ShowDaypart = buttonXDaypart.Checked;
			_snapshotData.ShowTime = buttonXTime.Checked;
			_snapshotData.ShowRate = buttonXRate.Checked;
			_snapshotData.ShowWeeklySpots = buttonXWeeklySpots.Checked;
			_snapshotData.ShowWeeklyCost = buttonXWeeklyCost.Checked;
			_snapshotData.ShowTotalSpots = buttonXTotalSpots.Checked;
			_snapshotData.ShowTotalCost = buttonXTotalCost.Checked;
			_snapshotData.ShowLogo = buttonXLogo.Checked;
			_snapshotData.ShowAverageRate = buttonXAvgRate.Checked;
			_snapshotData.ShowTotalRow = buttonXTotalRow.Checked;

			if (_snapshotData.Parent.SnapshotSummary.ApplySettingsForAll)
			{
				foreach (var snapshot in _snapshotData.Parent.Snapshots.Where(s => s.UniqueID != _snapshotData.UniqueID))
				{
					snapshot.ShowLineId = _snapshotData.ShowLineId;
					snapshot.ShowStation = _snapshotData.ShowStation;
					snapshot.ShowLenght = _snapshotData.ShowLenght;
					snapshot.ShowProgram = _snapshotData.ShowProgram;
					snapshot.ShowDaypart = _snapshotData.ShowDaypart;
					snapshot.ShowTime = _snapshotData.ShowTime;
					snapshot.ShowRate = _snapshotData.ShowRate;
					snapshot.ShowWeeklySpots = _snapshotData.ShowWeeklySpots;
					snapshot.ShowWeeklyCost = _snapshotData.ShowWeeklyCost;
					snapshot.ShowTotalSpots = _snapshotData.ShowTotalSpots;
					snapshot.ShowTotalCost = _snapshotData.ShowTotalCost;
					snapshot.ShowLogo = _snapshotData.ShowLogo;
					snapshot.ShowAverageRate = _snapshotData.ShowAverageRate;
					snapshot.ShowTotalRow = _snapshotData.ShowTotalRow;
				}
			}

			DataChanged?.Invoke(this, new SettingsChangedEventArgs { ChangedSettingsType = SettingsType });
		}
	}
}
