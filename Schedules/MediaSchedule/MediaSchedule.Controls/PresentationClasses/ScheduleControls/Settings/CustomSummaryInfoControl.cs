using System;
using Asa.Business.Common.Entities.NonPersistent.Summary;
using Asa.Business.Media.Entities.NonPersistent.Section.Content;
using DevExpress.XtraEditors.Controls;

namespace Asa.Media.Controls.PresentationClasses.ScheduleControls.Settings
{
	class CustomSummaryInfoControl : BaseSummaryInfoControl
	{
		private CustomSummarySettings _customSummarySettings;

		public override ScheduleSettingsType SettingsType => ScheduleSettingsType.CustomSummary;

		public CustomSummaryInfoControl()
		{
			spinEditTotal.ButtonClick += OnTotalButtonClick;
			spinEditMonthly.ButtonClick += OnMonthlyButtonClick;
			spinEditTotal.Properties.Buttons[1].Visible = true;
			spinEditTotal.EditValueChanged += OnTotalEditValueChanged;
			spinEditMonthly.Properties.Buttons[1].Visible = true;
			spinEditMonthly.EditValueChanged += OnMonthlyEditValueChanged;
		}

		public override void LoadSectionData(ScheduleSection sectionData)
		{
			_baseSummarySettings = sectionData.Summary.CustomSummary;

			_allowToSave = false;
			_customSummarySettings = sectionData.Summary.CustomSummary;
			_allowToSave = true;

			base.LoadSectionData(sectionData);
		}

		protected override void SaveData()
		{
			base.SaveData();

			_customSummarySettings.MonthlyValue = _customSummarySettings.TotalMonthly != spinEditMonthly.Value ? spinEditMonthly.Value : (Decimal?)null;
			_customSummarySettings.TotalValue = _customSummarySettings.TotalTotal != spinEditTotal.Value ? spinEditTotal.Value : (Decimal?)null;
		}

		public override void UpdateTotals()
		{
			spinEditMonthly.EditValue = _customSummarySettings.MonthlyValue ?? _customSummarySettings.TotalMonthly;
			spinEditTotal.EditValue = _customSummarySettings.TotalValue ?? _customSummarySettings.TotalTotal;
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
