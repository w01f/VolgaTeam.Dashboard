using System;
using System.Linq;
using System.Windows.Forms;

namespace Asa.Common.GUI.Summary
{
	public abstract class SummaryFullControl : SummaryBaseControl<SummaryCustomItemControl>
	{
		protected SummaryFullControl()
		{
			buttonXAddItem.Visible = true;
			buttonXAddItem.Click += OnAddItem;
			spinEditTotal.ButtonClick += spinEditTotal_ButtonClick;
			spinEditMonthly.ButtonClick += spinEditMonthly_ButtonClick;
			spinEditTotal.Properties.Buttons[1].Visible = true;
			spinEditTotal.EditValueChanged += spinEditTotal_EditValueChanged;
			spinEditMonthly.Properties.Buttons[1].Visible = true;
			spinEditMonthly.EditValueChanged += spinEditMonthly_EditValueChanged;
		}

		protected override bool CustomOrder
		{
			get { return true; }
		}

		public override string TotalMonthlyValue
		{
			get
			{
				return checkEditMonthlyInvestment.Checked &&
						(Schedule.CustomSummary.MonthlyValue.HasValue || Schedule.CustomSummary.TotalMonthly.HasValue) ? 
					(Schedule.CustomSummary.MonthlyValue.HasValue ? 
						Schedule.CustomSummary.MonthlyValue.Value : 
						Schedule.CustomSummary.TotalMonthly).Value.ToString("$#,##0.00") : 
					String.Empty;
			}
		}

		public override string TotalTotalValue
		{
			get
			{
				return checkEditTotalInvestment.Checked && 
						(Schedule.CustomSummary.TotalValue.HasValue || Schedule.CustomSummary.TotalTotal.HasValue) ? 
					(Schedule.CustomSummary.TotalValue.HasValue ? 
						Schedule.CustomSummary.TotalValue.Value : 
						Schedule.CustomSummary.TotalTotal).Value.ToString("$#,##0.00") : 
					String.Empty;
			}
		}

		public override bool ShowIcons
		{
			get { return false; }
		}

		protected override void LoadItems(bool quickLoad)
		{
			base.LoadItems(quickLoad);
			if (!quickLoad)
				UpdateControlsInList(OrderedItems.OfType<Control>().FirstOrDefault());
		}

		protected override void InitItem(SummaryCustomItemControl item)
		{
			base.InitItem(item);
			item.ItemPositionChanged += ItemOnItemPositionChanged;
			item.ItemDeleted += ItemOnItemDeleted;
		}

		protected void UpdateNumbers()
		{
			foreach (var itemControl in _inputControls)
				itemControl.UpdateNumber();
		}

		protected override void UpdateTotals()
		{
			spinEditMonthly.EditValue = Schedule.CustomSummary.MonthlyValue.HasValue ? 
				Schedule.CustomSummary.MonthlyValue.Value : 
				Schedule.CustomSummary.TotalMonthly;
			spinEditTotal.EditValue = Schedule.CustomSummary.TotalValue.HasValue ? 
				Schedule.CustomSummary.TotalValue.Value : 
				Schedule.CustomSummary.TotalTotal;
		}

		protected virtual void OnAddItem(object sender, EventArgs e)
		{
			var newItemData = Schedule.CustomSummary.AddItem();
			var focussed = AddItemToList(newItemData);
			UpdateControlsInList(focussed);
			UpdateTotalItems();
			UpdateOutput();
			SettingsNotSaved = true;
		}

		protected virtual void ItemOnItemDeleted(object sender, SummaryItemEventArgs e)
		{
			Schedule.CustomSummary.DeleteItem(e.SummaryItem.Data);
			_inputControls.Remove(e.SummaryItem as SummaryCustomItemControl);
			Schedule.CustomSummary.ReorderItems();
			UpdateControlsInList(OrderedItems.OfType<Control>().FirstOrDefault());
			UpdateNumbers();
			UpdateTotalItems();
			UpdateTotals();
			UpdateOutput();
			SettingsNotSaved = true;
		}

		protected virtual void ItemOnItemPositionChanged(object sender, SummaryItemEventArgs e)
		{
			Schedule.CustomSummary.ReorderItems();
			UpdateNumbers();
			UpdateControlsInList(e.SummaryItem as SummaryCustomItemControl);
			SettingsNotSaved = true;
		}

		private void spinEditMonthly_EditValueChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			Schedule.CustomSummary.MonthlyValue = Schedule.CustomSummary.TotalMonthly != spinEditMonthly.Value ? 
				spinEditMonthly.Value : 
				(decimal?)null;
			SettingsNotSaved = true;
		}

		private void spinEditTotal_EditValueChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			Schedule.CustomSummary.TotalValue = Schedule.CustomSummary.TotalTotal != spinEditTotal.Value ? 
				spinEditTotal.Value : 
				(decimal?)null;
			SettingsNotSaved = true;
		}

		private void spinEditMonthly_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			spinEditMonthly.EditValue = Schedule.CustomSummary.TotalMonthly;
		}

		private void spinEditTotal_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			spinEditTotal.EditValue = Schedule.CustomSummary.TotalTotal;
		}
	}
}
