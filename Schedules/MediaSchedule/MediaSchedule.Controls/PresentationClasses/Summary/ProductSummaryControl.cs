using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Asa.Business.Common.Entities.NonPersistent.Summary;
using Asa.Business.Media.Entities.NonPersistent.Section.Summary;
using Asa.Common.GUI.Summary;

namespace Asa.Media.Controls.PresentationClasses.Summary
{
	[ToolboxItem(false)]
	class ProductSummaryControl : SectionSummaryBaseControl<SummaryProductItemControl, ProductSummaryInfoControl>
	{
		protected override bool CustomOrder
		{
			get { return false; }
		}
		public override List<CustomSummaryItem> Items
		{
			get { return SectionData.Parent.Programs.Select(ps => ps.SummaryItem).ToList(); }
		}

		public ProductSummaryControl()
		{
			buttonXAddItem.Visible = false;	
		}

		public override void LoadData(SectionSummary sectionData, bool quickLoad = false)
		{
			base.LoadData(sectionData, quickLoad);
			if (!quickLoad)
				UpdateControlsInList(null);
		}

		#region ISummaryControl Implementation
		public override string TotalMonthlyValue
		{
			get { return SummarySettings.ShowMonthly && SummarySettings.MonthlyValue.HasValue ? SummarySettings.MonthlyValue.Value.ToString("$#,##0.00") : string.Empty; }
		}

		public override string TotalTotalValue
		{
			get { return SummarySettings.ShowTotal && SummarySettings.TotalValue.HasValue ? SummarySettings.TotalValue.Value.ToString("$#,##0.00") : string.Empty; }
		}

		public override bool ShowIcons
		{
			get { return true; }
		}
		#endregion
	}
}
