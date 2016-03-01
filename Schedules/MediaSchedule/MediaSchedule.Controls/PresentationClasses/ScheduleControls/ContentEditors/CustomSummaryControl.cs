using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Common.Entities.NonPersistent.Summary;
using Asa.Business.Media.Entities.NonPersistent.Section.Summary;
using Asa.Common.GUI.Summary;

namespace Asa.Media.Controls.PresentationClasses.ScheduleControls.ContentEditors
{
	class CustomSummaryControl : SectionSummaryBaseControl<SummaryMediaCustomControl>
	{
		#region Calculated properties
		public override SectionEditorType EditorType=>SectionEditorType.CustomSummary;
		protected override BaseSummarySettings SummarySettings => _sectionContainer.SectionData.Summary.CustomSummary;

		private CustomSummaryContent SummaryContent => _sectionContainer.SectionData.Summary.CustomSummary;

		protected override bool CustomOrder
		{
			get { return true; }
		}

		public override List<CustomSummaryItem> Items => SummaryContent.Items;
		#endregion

		public CustomSummaryControl(SectionContainer sectionContainer) : base(sectionContainer)
		{
			Text = "Summary Slide";
			buttonXAddItem.Visible = true;
			buttonXAddItem.Click += OnAddItem;
		}

		public override void LoadData(bool quickLoad = false)
		{
			base.LoadData(quickLoad);
			if (!quickLoad)
				UpdateControlsInList(OrderedItems.OfType<Control>().FirstOrDefault());
		}

		#region Items Management
		protected override void InitItem(SummaryMediaCustomControl item)
		{
			base.InitItem(item);
			item.ItemPositionChanged += ItemOnItemPositionChanged;
			item.ItemDeleted += ItemOnItemDeleted;
		}

		private void ItemOnItemDeleted(object sender, SummaryItemEventArgs e)
		{
			SummaryContent.DeleteItem(e.SummaryItem.Data);
			_inputControls.Remove(e.SummaryItem as SummaryMediaCustomControl);
			SummaryContent.ReorderItems();
			UpdateControlsInList(OrderedItems.OfType<Control>().FirstOrDefault());
			UpdateNumbers();
			UpdateTotalItems();
			UpdateTotals();
			RaiseDataChanged();
		}

		private void ItemOnItemPositionChanged(object sender, SummaryItemEventArgs e)
		{
			SummaryContent.ReorderItems();
			UpdateNumbers();
			UpdateControlsInList(e.SummaryItem as SummaryCustomItemControl);
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
					(SummaryContent.MonthlyValue ?? SummaryContent.TotalMonthly).Value.ToString("$#,##0.00") : 
					String.Empty;
			}
		}

		public override string TotalTotalValue
		{
			get
			{
				return SummaryContent.ShowTotal && 
						(SummaryContent.TotalValue.HasValue || SummaryContent.TotalTotal.HasValue) ?
					(SummaryContent.TotalValue ?? SummaryContent.TotalTotal).Value.ToString("$#,##0.00") : 
					String.Empty;
			}
		}

		public override bool ShowIcons => false;

		#endregion
	}
}
