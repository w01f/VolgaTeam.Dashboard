using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Asa.Business.Common.Entities.NonPersistent.Summary;

namespace Asa.Media.Controls.PresentationClasses.ScheduleControls.ContentEditors
{
	[ToolboxItem(false)]
	class ProductSummaryControl : SectionSummaryBaseControl<SummaryMediaProductItemControl>
	{
		public override SectionEditorType EditorType => SectionEditorType.ProductSummary;
		protected override BaseSummarySettings SummarySettings => _sectionContainer.SectionData.Summary.ProductSummary;

		protected override bool CustomOrder => false;

		public override List<CustomSummaryItem> Items
		{
			get { return _sectionContainer.SectionData.Programs.Select(ps => ps.SummaryItem).ToList(); }
		}

		public ProductSummaryControl(SectionContainer sectionContainer) : base(sectionContainer)
		{
			Text = "Product Slide";
			buttonXAddItem.Visible = false;
		}

		public override void LoadData(bool quickLoad = false)
		{
			base.LoadData(quickLoad);
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

		public override bool ShowIcons => true;
		#endregion
	}
}
