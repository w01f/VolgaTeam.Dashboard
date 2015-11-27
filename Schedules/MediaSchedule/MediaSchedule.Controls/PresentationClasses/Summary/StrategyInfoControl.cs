using System;
using System.ComponentModel;
using Asa.Core.MediaSchedule;
using DevExpress.XtraTab;

namespace Asa.MediaSchedule.Controls.PresentationClasses.Summary
{
	[ToolboxItem(false)]
	//public partial class StrategyInfoControl : UserControl
	public partial class StrategyInfoControl : XtraTabPage
	{
		private bool _allowToSave;

		private StrategySummaryContent _baseSummarySettings;

		public event EventHandler<EventArgs> DataChanged;

		public StrategyInfoControl()
		{
			InitializeComponent();
			Text = "Slide Info";
		}

		public void LoadData(StrategySummaryContent dataSource)
		{
			_baseSummarySettings = dataSource;
			_allowToSave = false;

			checkEditStation.Checked = _baseSummarySettings.ShowStation;
			checkEditTotalsSpots.Checked = _baseSummarySettings.ShowDescription;

			_allowToSave = true;
		}

		private void SaveData()
		{
			_baseSummarySettings.ShowStation = checkEditStation.Checked;
			_baseSummarySettings.ShowDescription = checkEditTotalsSpots.Checked;
		}

		private void OnSettingChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			SaveData();
			RaiseDataChanged();
		}

		protected void RaiseDataChanged()
		{
			if (DataChanged != null)
				DataChanged(this, EventArgs.Empty);
		}
	}
}
