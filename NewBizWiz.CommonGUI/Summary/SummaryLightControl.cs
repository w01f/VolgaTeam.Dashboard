using System;
using System.Linq;
using System.Windows.Forms;

namespace NewBizWiz.CommonGUI.Summary
{
	public abstract class SummaryLightControl : SummaryBaseControl<SummaryProductItemControl>
	{
		protected SummaryLightControl()
		{
			buttonXAddItem.Visible = false;
			spinEditTotal.Properties.Buttons[1].Visible = false;
			spinEditTotal.EditValueChanged += spinEditTotal_EditValueChanged;
			spinEditMonthly.Properties.Buttons[1].Visible = false;
			spinEditMonthly.EditValueChanged += spinEditMonthly_EditValueChanged;
		}

		protected override bool CustomOrder
		{
			get { return false; }
		}

		public override string TotalMonthlyValue
		{
			get { return checkEditMonthlyInvestment.Checked && Schedule.ProductSummary.MonthlyValue.HasValue ? Schedule.ProductSummary.MonthlyValue.Value.ToString("$#,##0.00") : string.Empty; }
		}

		public override string TotalTotalValue
		{
			get { return checkEditTotalInvestment.Checked && Schedule.ProductSummary.TotalValue.HasValue ? Schedule.ProductSummary.TotalValue.Value.ToString("$#,##0.00") : string.Empty; }
		}

		protected override void LoadItems(bool quickLoad)
		{
			base.LoadItems(quickLoad);
			if(!quickLoad)
				UpdateControlsInList(null);
		}

		protected override void UpdateTotals()
		{
			spinEditMonthly.EditValue = Schedule.ProductSummary.MonthlyValue;
			spinEditTotal.EditValue = Schedule.ProductSummary.TotalValue;
		}

		private void spinEditMonthly_EditValueChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			Schedule.ProductSummary.MonthlyValue = (Decimal?)spinEditMonthly.EditValue;
			SettingsNotSaved = true;
		}

		private void spinEditTotal_EditValueChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			Schedule.ProductSummary.TotalValue = (Decimal?)spinEditTotal.EditValue;
			SettingsNotSaved = true;
		}
	}
}
