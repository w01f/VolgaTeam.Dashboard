using System;
using Asa.Business.Media.Entities.NonPersistent.Section.Content;
using Asa.Common.GUI.RetractableBar;

namespace Asa.Media.Controls.PresentationClasses.ScheduleControls.Settings
{
	class ProductSummaryInfoControl : BaseSummaryInfoControl
	{
		public ProductSummaryInfoControl()
		{
			spinEditTotal.Properties.Buttons[1].Visible = false;
			spinEditTotal.EditValueChanged += OnTotalsEditValueChanged;
			spinEditMonthly.Properties.Buttons[1].Visible = false;
			spinEditMonthly.EditValueChanged += OnTotalsEditValueChanged;
		}

		public override ScheduleSettingsType SettingsType => ScheduleSettingsType.ProductSummary;

		public override void LoadSectionData(ScheduleSection sectionData)
		{
			_baseSummarySettings = sectionData.Summary.ProductSummary;
			base.LoadSectionData(sectionData);
		}

		protected override void SaveData()
		{
			base.SaveData();
			_baseSummarySettings.MonthlyValue = (Decimal?)spinEditMonthly.EditValue;
			_baseSummarySettings.TotalValue = (Decimal?)spinEditTotal.EditValue;
		}

		public override void UpdateTotals()
		{
			spinEditMonthly.EditValue = _baseSummarySettings.MonthlyValue;
			spinEditTotal.EditValue = _baseSummarySettings.TotalValue;
		}

		private void OnTotalsEditValueChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			SaveData();
			RaiseDataChanged();
		}
	}
}
