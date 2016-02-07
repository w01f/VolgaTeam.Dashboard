using System;

namespace Asa.Media.Controls.PresentationClasses.Summary
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
