using System;
using System.ComponentModel;
using Asa.Business.Media.Entities.NonPersistent.Section.Content;
using Asa.Business.Media.Entities.NonPersistent.Section.Summary;
using Asa.Common.GUI.RetractableBar;
using Asa.Media.Controls.Properties;
using DevExpress.XtraTab;

namespace Asa.Media.Controls.PresentationClasses.ScheduleControls.Settings
{
	[ToolboxItem(false)]
	//public partial class StrategyInfoControl : UserControl
	public partial class StrategyInfoControl : XtraTabPage, ISectionSettingsControl
	{
		private bool _allowToSave;

		private StrategySummaryContent _baseSummarySettings;
		public int Order => 1;
		public bool IsAvailable => true;
		public ButtonInfo BarButton { get; }
		public ScheduleSettingsType SettingsType => ScheduleSettingsType.Strategy;
		public event EventHandler<SettingsChangedEventArgs> DataChanged;

		public StrategyInfoControl()
		{
			InitializeComponent();
			Text = "Slide Info";
			BarButton = new ButtonInfo
			{
				Tooltip = "Edit Summary Settings",
				Logo = Resources.SummaryOptionsInfo,
				Action = () => { TabControl.SelectedTabPage = this; }
			};
		}

		public void LoadSectionData(ScheduleSection sectionData)
		{
			_baseSummarySettings = sectionData.Summary.StrategySummary;

			_allowToSave = false;

			checkEditStation.Checked = _baseSummarySettings.ShowStation;
			checkEditTotalsSpots.Checked = _baseSummarySettings.ShowDescription;

			_allowToSave = true;
		}

		private void OnSettingChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			_baseSummarySettings.ShowStation = checkEditStation.Checked;
			_baseSummarySettings.ShowDescription = checkEditTotalsSpots.Checked;
			DataChanged?.Invoke(this, new SettingsChangedEventArgs { ChangedSettingsType = SettingsType });
		}
	}
}
