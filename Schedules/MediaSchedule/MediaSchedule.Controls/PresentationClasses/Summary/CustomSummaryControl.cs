using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Common.Entities.NonPersistent.Summary;
using Asa.Business.Media.Entities.NonPersistent.Section.Summary;
using Asa.Common.GUI.Summary;

namespace Asa.Media.Controls.PresentationClasses.Summary
{
	class CustomSummaryControl : SectionSummaryBaseControl<SummaryCustomItemControl, CustomSummaryInfoControl>
	{
		#region Calculated properties
		private CustomSummaryContent SummaryContent
		{
			get { return (CustomSummaryContent)SectionData.Content; }
		}

		protected override bool CustomOrder
		{
			get { return true; }
		}

		public override List<CustomSummaryItem> Items
		{
			get { return SummaryContent.Items; }
		}
		#endregion

		public CustomSummaryControl()
		{
			buttonXAddItem.Visible = true;
			buttonXAddItem.Click += OnAddItem;
		}

		public override void LoadData(SectionSummary sectionData, bool quickLoad = false)
		{
			base.LoadData(sectionData, quickLoad);
			if (!quickLoad)
				UpdateControlsInList(OrderedItems.OfType<Control>().FirstOrDefault());
		}

		#region Items Management
		protected override void InitItem(SummaryCustomItemControl item)
		{
			base.InitItem(item);
			item.ItemPositionChanged += ItemOnItemPositionChanged;
			item.ItemDeleted += ItemOnItemDeleted;
			item.DataChanged += (o, e) => { SummaryContent.IsDefaultSate = false; };
		}

		private void ItemOnItemDeleted(object sender, SummaryItemEventArgs e)
		{
			SummaryContent.DeleteItem(e.SummaryItem.Data);
			_inputControls.Remove(e.SummaryItem as SummaryCustomItemControl);
			SummaryContent.ReorderItems();
			UpdateControlsInList(OrderedItems.OfType<Control>().FirstOrDefault());
			UpdateNumbers();
			UpdateTotalItems();
			UpdateTotals();
			SummaryContent.IsDefaultSate = false;
			RaiseDataChanged();

		}

		private void ItemOnItemPositionChanged(object sender, SummaryItemEventArgs e)
		{
			SummaryContent.ReorderItems();
			UpdateNumbers();
			UpdateControlsInList(e.SummaryItem as SummaryCustomItemControl);
			SummaryContent.IsDefaultSate = false;
			RaiseDataChanged();
		}

		private void UpdateNumbers()
		{
			foreach (var itemControl in _inputControls)
				itemControl.UpdateNumber();
		}

		private void OnAddItem(object sender, EventArgs e)
		{
			var newItemData = SummaryContent.AddItem();
			var focussed = AddItemToList(newItemData);
			UpdateControlsInList(focussed);
			UpdateTotalItems();
			SummaryContent.IsDefaultSate = false;
			RaiseDataChanged();
		}
		#endregion

		#region ISummaryControl Implementation
		public override string TotalMonthlyValue
		{
			get 
			{ 
				return SummaryContent.ShowMonthly && 
						(SummaryContent.MonthlyValue.HasValue || SummaryContent.TotalMonthly.HasValue) ?
					(SummaryContent.MonthlyValue.HasValue ? 
						SummaryContent.MonthlyValue.Value : 
						SummaryContent.TotalMonthly).Value.ToString("$#,##0.00") : 
					String.Empty;
			}
		}

		public override string TotalTotalValue
		{
			get
			{
				return SummaryContent.ShowTotal && 
						(SummaryContent.TotalValue.HasValue || SummaryContent.TotalTotal.HasValue) ?
					(SummaryContent.TotalValue.HasValue ? 
						SummaryContent.TotalValue.Value :
						SummaryContent.TotalTotal).Value.ToString("$#,##0.00") : 
					String.Empty;
			}
		}

		public override bool ShowIcons
		{
			get { return false; }
		}
		#endregion
	}
}
