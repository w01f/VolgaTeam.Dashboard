using System;
using Asa.Business.Media.Entities.NonPersistent.Snapshot;
using Asa.Common.GUI.RetractableBar;
using Asa.Media.Controls.BusinessClasses.Managers;
using Asa.Media.Controls.PresentationClasses.Digital.DigitalInfo;
using Asa.Media.Controls.Properties;

namespace Asa.Media.Controls.PresentationClasses.SnapshotControls.Settings
{
	class SnapshotDigitalInfoSettingsControl : BaseDigitalInfoSettingsControl, ISnapshotSettingsControl
	{
		private Snapshot _snapshotData;

		public int Order => 0;
		public bool IsAvailable => true;
		public ButtonInfo BarButton { get; }
		public SnapshotSettingsType SettingsType => SnapshotSettingsType.DigitalInfo;
		public event EventHandler<SettingsChangedEventArgs> DataChanged;

		public SnapshotDigitalInfoSettingsControl()
		{
			BarButton = new ButtonInfo
			{
				Logo = BusinessObjects.Instance.ImageResourcesManager.SnapshotsRetractableBarDigitalImage ?? Resources.SectionSettingsInfo,
				Tooltip = "Open Digital Settings",
				Action = () => { TabControl.SelectedTabPage = this; }
			};
		}

		public void LoadSnapshotData(Snapshot snapshotData)
		{
			_snapshotData = snapshotData;
			_digitalInfo = _snapshotData.DigitalInfo;
			LoadData();
		}

		protected override void RaiseDataChanged()
		{
			DataChanged?.Invoke(this, new SettingsChangedEventArgs { ChangedSettingsType = SettingsType });
		}
	}
}
