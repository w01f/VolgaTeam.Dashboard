using System;
using System.ComponentModel;
using System.Linq;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Business.Solutions.StarApp.Configuration;
using Asa.Business.Solutions.StarApp.Configuration.Fishing;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;
using Asa.Solutions.Common.PresentationClasses.Output;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors.Fishing
{
	[ToolboxItem(false)]
	public sealed partial class FishingTabBControl : ChildTabBaseControl
	{
		private FishingTabBInfo CustomTabInfo => (FishingTabBInfo)TabInfo;

		public FishingTabBControl(IChildTabPageContainer slideContainer, StarChildTabInfo tabInfo) : base(slideContainer, tabInfo)
		{
			InitializeComponent();

			comboBoxEditTabBCombo1.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			comboBoxEditTabBCombo2.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			comboBoxEditTabBCombo3.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			comboBoxEditTabBCombo4.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();

			comboBoxEditTabBCombo1.Properties.Items.AddRange(SlideContainer.StarInfo.TargetCustomersLists.CombinedList.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditTabBCombo1.Properties.NullText =
				SlideContainer.StarInfo.TargetCustomersLists.CombinedList.Where(item => item.IsPlaceholder).ElementAtOrDefault(0)?.Value ??
				comboBoxEditTabBCombo1.Properties.NullText;
			comboBoxEditTabBCombo2.Properties.Items.AddRange(SlideContainer.StarInfo.TargetCustomersLists.CombinedList.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditTabBCombo2.Properties.NullText =
				SlideContainer.StarInfo.TargetCustomersLists.CombinedList.Where(item => item.IsPlaceholder).ElementAtOrDefault(1)?.Value ??
				comboBoxEditTabBCombo2.Properties.NullText;
			comboBoxEditTabBCombo3.Properties.Items.AddRange(SlideContainer.StarInfo.TargetCustomersLists.CombinedList.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditTabBCombo3.Properties.NullText =
				SlideContainer.StarInfo.TargetCustomersLists.CombinedList.Where(item => item.IsPlaceholder).ElementAtOrDefault(2)?.Value ??
				comboBoxEditTabBCombo3.Properties.NullText;
			comboBoxEditTabBCombo4.Properties.Items.AddRange(SlideContainer.StarInfo.TargetCustomersLists.CombinedList.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditTabBCombo4.Properties.NullText =
				SlideContainer.StarInfo.TargetCustomersLists.CombinedList.Where(item => item.IsPlaceholder).ElementAtOrDefault(3)?.Value ??
				comboBoxEditTabBCombo4.Properties.NullText;

			clipartEditContainerTabB1.Init(ImageClipartObject.FromImage(CustomTabInfo.Clipart1Image), CustomTabInfo.Clipart1Configuration, TabPageContainer.ParentControl);
			clipartEditContainerTabB1.EditValueChanged += OnEditValueChanged;
			clipartEditContainerTabB2.Init(ImageClipartObject.FromImage(CustomTabInfo.Clipart2Image), CustomTabInfo.Clipart2Configuration, TabPageContainer.ParentControl);
			clipartEditContainerTabB2.EditValueChanged += OnEditValueChanged;
		}

		public override void LoadData()
		{
			_allowToSave = false;

			clipartEditContainerTabB1.LoadData(SlideContainer.EditedContent.FishingState.TabB.Clipart1);
			clipartEditContainerTabB2.LoadData(SlideContainer.EditedContent.FishingState.TabB.Clipart2);
			comboBoxEditTabBCombo1.EditValue = SlideContainer.EditedContent.FishingState.TabB.Combo1 ??
											   SlideContainer.StarInfo.TargetCustomersLists.CombinedList
												   .Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(0);
			comboBoxEditTabBCombo2.EditValue = SlideContainer.EditedContent.FishingState.TabB.Combo2 ??
											   SlideContainer.StarInfo.TargetCustomersLists.CombinedList
												   .Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(1);
			comboBoxEditTabBCombo3.EditValue = SlideContainer.EditedContent.FishingState.TabB.Combo3 ??
											   SlideContainer.StarInfo.TargetCustomersLists.CombinedList
												   .Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(2);
			comboBoxEditTabBCombo4.EditValue = SlideContainer.EditedContent.FishingState.TabB.Combo4 ??
											   SlideContainer.StarInfo.TargetCustomersLists.CombinedList
												   .Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(3);

			_allowToSave = true;
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;

			SlideContainer.EditedContent.FishingState.TabB.Clipart1 = clipartEditContainerTabB1.GetActiveClipartObject();
			SlideContainer.EditedContent.FishingState.TabB.Clipart2 = clipartEditContainerTabB2.GetActiveClipartObject();

			SlideContainer.EditedContent.FishingState.TabB.Combo1 = SlideContainer.StarInfo.TargetCustomersLists.CombinedList.Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(0) != comboBoxEditTabBCombo1.EditValue ?
				comboBoxEditTabBCombo1.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditTabBCombo1.EditValue as String } :
				null;
			SlideContainer.EditedContent.FishingState.TabB.Combo2 = SlideContainer.StarInfo.TargetCustomersLists.CombinedList.Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(1) != comboBoxEditTabBCombo2.EditValue ?
				comboBoxEditTabBCombo2.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditTabBCombo2.EditValue as String } :
				null;
			SlideContainer.EditedContent.FishingState.TabB.Combo3 = SlideContainer.StarInfo.TargetCustomersLists.CombinedList.Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(2) != comboBoxEditTabBCombo3.EditValue ?
				comboBoxEditTabBCombo3.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditTabBCombo3.EditValue as String } :
				null;
			SlideContainer.EditedContent.FishingState.TabB.Combo4 = SlideContainer.StarInfo.TargetCustomersLists.CombinedList.Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(3) != comboBoxEditTabBCombo4.EditValue ?
				comboBoxEditTabBCombo4.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditTabBCombo4.EditValue as String } :
				null;

			_dataChanged = false;
		}

		public override ListDataItem GetSlideHeaderValue()
		{
			return SlideContainer.EditedContent.FishingState.TabB.SlideHeader ??
				   TabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault);
		}

		public override void ApplySlideHeaderValue(ListDataItem slideHeaderValue)
		{
			SlideContainer.EditedContent.FishingState.TabB.SlideHeader =
				slideHeaderValue != TabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault) ? slideHeaderValue : null;
		}

		private void OnEditValueChanged(object sender, EventArgs e)
		{
			RaiseEditValueChanged();
		}

		#region Output
		protected override OutputDataPackage GetOutputData()
		{
			var outputDataPackage = new OutputDataPackage();

			outputDataPackage.Theme = TabPageContainer.ParentControl.SelectedTheme;

			var clipart1 = SlideContainer.EditedContent.FishingState.TabB.Clipart1 ?? ImageClipartObject.FromImage(CustomTabInfo.Clipart1Image);
			if (clipart1 != null)
				outputDataPackage.ClipartItems.Add("CP03BCLIPART1", clipart1);


			var clipart2 = SlideContainer.EditedContent.FishingState.TabB.Clipart2 ?? ImageClipartObject.FromImage(CustomTabInfo.Clipart2Image);
			if (clipart2 != null)
				outputDataPackage.ClipartItems.Add("CP03BCLIPART2", clipart2);

			var slideHeader = (SlideContainer.EditedContent.FishingState.TabB.SlideHeader ?? TabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault))?.Value;
			var combos = new[]
				{
						(SlideContainer.EditedContent.FishingState.TabB.Combo1 ?? SlideContainer.StarInfo.TargetCustomersLists.CombinedList.Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(0))?.Value,
						(SlideContainer.EditedContent.FishingState.TabB.Combo2 ?? SlideContainer.StarInfo.TargetCustomersLists.CombinedList.Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(1))?.Value,
						(SlideContainer.EditedContent.FishingState.TabB.Combo3 ?? SlideContainer.StarInfo.TargetCustomersLists.CombinedList.Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(2))?.Value,
						(SlideContainer.EditedContent.FishingState.TabB.Combo4 ?? SlideContainer.StarInfo.TargetCustomersLists.CombinedList.Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(3))?.Value,
					}
				.Where(item => !String.IsNullOrWhiteSpace(item))
				.ToList();

			switch (combos.Count)
			{
				case 1:
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarFishingFile(clipart1 != null && clipart2 != null ? "CP03B-1.pptx" : (clipart1 != null ? "CP03B-5.pptx" : "CP03B-12.pptx"));
					break;
				case 2:
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarFishingFile(clipart1 != null && clipart2 != null ? "CP03B-2.pptx" : (clipart1 != null ? "CP03B-6.pptx" : "CP03B-11.pptx"));
					break;
				case 3:
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarFishingFile(clipart1 != null && clipart2 != null ? "CP03B-3.pptx" : (clipart1 != null ? "CP03B-7.pptx" : "CP03B-10.pptx"));
					break;
				case 4:
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarFishingFile(clipart1 != null && clipart2 != null ? "CP03B-4.pptx" : (clipart1 != null ? "CP03B-8.pptx" : "CP03B-9.pptx"));
					break;
				default:
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarFishingFile(clipart1 != null && clipart2 != null ? "CP03B-1.pptx" : (clipart1 != null ? "CP03B-5.pptx" : "CP03B-12.pptx"));
					break;
			}

			outputDataPackage.TextItems.Add("CP03BHEADER".ToUpper(), slideHeader);
			outputDataPackage.TextItems.Add("HEADER".ToUpper(), slideHeader);
			outputDataPackage.TextItems.Add("CP03BCombo1".ToUpper(), combos.ElementAtOrDefault(0));
			outputDataPackage.TextItems.Add("CP03BCombo2".ToUpper(), combos.ElementAtOrDefault(1));
			outputDataPackage.TextItems.Add("CP03BCombo3".ToUpper(), combos.ElementAtOrDefault(2));
			outputDataPackage.TextItems.Add("CP03BCombo4".ToUpper(), combos.ElementAtOrDefault(3));

			return outputDataPackage;
		}
		#endregion
	}
}