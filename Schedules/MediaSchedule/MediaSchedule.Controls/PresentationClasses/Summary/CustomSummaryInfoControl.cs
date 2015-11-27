using System;
using Asa.Core.Common;
using DevExpress.XtraEditors.Controls;

namespace Asa.MediaSchedule.Controls.PresentationClasses.Summary
{
	class CustomSummaryInfoControl : BaseSummaryInfoControl
	{
		private CustomSummarySettings _customSummarySettings;

		public CustomSummaryInfoControl()
		{
			spinEditTotal.ButtonClick += OnTotalButtonClick;
			spinEditMonthly.ButtonClick += OnMonthlyButtonClick;
			spinEditTotal.Properties.Buttons[1].Visible = true;
			spinEditTotal.EditValueChanged += OnTotalEditValueChanged;
			spinEditMonthly.Properties.Buttons[1].Visible = true;
			spinEditMonthly.EditValueChanged += OnMonthlyEditValueChanged;
		}

		public override void LoadData(BaseSummarySettings dataSource)
		{
			_allowToSave = false;
			_customSummarySettings = (CustomSummarySettings)dataSource;
			_allowToSave = true;

			base.LoadData(dataSource);
		}

		protected override void SaveData()
		{
			base.SaveData();

			_customSummarySettings.MonthlyValue = _customSummarySettings.TotalMonthly != spinEditMonthly.Value ? spinEditMonthly.Value : (Decimal?)null;
			_customSummarySettings.TotalValue = _customSummarySettings.TotalTotal != spinEditTotal.Value ? spinEditTotal.Value : (Decimal?)null;
		}

		public override void UpdateTotals()
		{
			spinEditMonthly.EditValue = _customSummarySettings.MonthlyValue.HasValue ? _customSummarySettings.MonthlyValue.Value : _customSummarySettings.TotalMonthly;
			spinEditTotal.EditValue = _customSummarySettings.TotalValue.HasValue ? _customSummarySettings.TotalValue.Value : _customSummarySettings.TotalTotal;
		}

		private void OnMonthlyEditValueChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			SaveData();
			RaiseDataChanged();
		}

		private void OnMonthlyButtonClick(object sender, ButtonPressedEventArgs e)
		{
			spinEditMonthly.EditValue = _customSummarySettings.TotalMonthly;
		}

		private void OnTotalEditValueChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			SaveData();
			RaiseDataChanged();
		}

		private void OnTotalButtonClick(object sender, ButtonPressedEventArgs e)
		{
			spinEditTotal.EditValue = _customSummarySettings.TotalTotal;
		}
	}
}
