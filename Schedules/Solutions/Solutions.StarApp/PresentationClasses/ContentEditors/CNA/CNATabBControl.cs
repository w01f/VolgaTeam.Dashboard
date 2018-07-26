using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Business.Solutions.StarApp.Configuration;
using Asa.Business.Solutions.StarApp.Configuration.CNA;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;
using Asa.Solutions.Common.PresentationClasses.Output;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors.CNA
{
	[ToolboxItem(false)]
	public sealed partial class CNATabBControl : ChildTabBaseControl
	{
		private CNATabBInfo CustomTabInfo => (CNATabBInfo)TabInfo;

		public CNATabBControl(IChildTabPageContainer slideContainer, StarChildTabInfo tabInfo) : base(slideContainer, tabInfo)
		{
			InitializeComponent();

			comboBoxEditTabBCombo1.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			comboBoxEditTabBCombo2.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			comboBoxEditTabBCombo3.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			comboBoxEditTabBCombo4.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			comboBoxEditTabBCombo5.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();

			comboBoxEditTabBCombo1.Properties.Items.AddRange(SlideContainer.StarInfo.ClientGoalsLists.Goals.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditTabBCombo1.Properties.NullText =
				SlideContainer.StarInfo.ClientGoalsLists.Goals.Where(item => item.IsPlaceholder).ElementAtOrDefault(0)?.Value ??
				comboBoxEditTabBCombo1.Properties.NullText;
			comboBoxEditTabBCombo2.Properties.Items.AddRange(SlideContainer.StarInfo.ClientGoalsLists.Goals.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditTabBCombo2.Properties.NullText =
				SlideContainer.StarInfo.ClientGoalsLists.Goals.Where(item => item.IsPlaceholder).ElementAtOrDefault(1)?.Value ??
				comboBoxEditTabBCombo2.Properties.NullText;
			comboBoxEditTabBCombo3.Properties.Items.AddRange(SlideContainer.StarInfo.ClientGoalsLists.Goals.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditTabBCombo3.Properties.NullText =
				SlideContainer.StarInfo.ClientGoalsLists.Goals.Where(item => item.IsPlaceholder).ElementAtOrDefault(2)?.Value ??
				comboBoxEditTabBCombo3.Properties.NullText;
			comboBoxEditTabBCombo4.Properties.Items.AddRange(SlideContainer.StarInfo.ClientGoalsLists.Goals.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditTabBCombo4.Properties.NullText =
				SlideContainer.StarInfo.ClientGoalsLists.Goals.Where(item => item.IsPlaceholder).ElementAtOrDefault(3)?.Value ??
				comboBoxEditTabBCombo4.Properties.NullText;
			comboBoxEditTabBCombo5.Properties.Items.AddRange(SlideContainer.StarInfo.ClientGoalsLists.Goals.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditTabBCombo5.Properties.NullText =
				SlideContainer.StarInfo.ClientGoalsLists.Goals.Where(item => item.IsPlaceholder).ElementAtOrDefault(4)?.Value ??
				comboBoxEditTabBCombo5.Properties.NullText;

			clipartEditContainerTabB1.Init(ImageClipartObject.FromImage(CustomTabInfo.Clipart1Image), CustomTabInfo.Clipart1Configuration, TabPageContainer.ParentControl);
			clipartEditContainerTabB1.EditValueChanged += OnEditValueChanged;
			clipartEditContainerTabB2.Init(ImageClipartObject.FromImage(CustomTabInfo.Clipart2Image), CustomTabInfo.Clipart2Configuration, TabPageContainer.ParentControl);
			clipartEditContainerTabB2.EditValueChanged += OnEditValueChanged;
		}

		public override void LoadData()
		{
			_allowToSave = false;

			clipartEditContainerTabB1.LoadData(SlideContainer.EditedContent.CNAState.TabB.Clipart1);
			clipartEditContainerTabB2.LoadData(SlideContainer.EditedContent.CNAState.TabB.Clipart2);
			comboBoxEditTabBCombo1.EditValue = SlideContainer.EditedContent.CNAState.TabB.Combo1 ??
											   SlideContainer.StarInfo.ClientGoalsLists.Goals
												   .Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(0);
			comboBoxEditTabBCombo2.EditValue = SlideContainer.EditedContent.CNAState.TabB.Combo2 ??
											   SlideContainer.StarInfo.ClientGoalsLists.Goals
												   .Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(1);
			comboBoxEditTabBCombo3.EditValue = SlideContainer.EditedContent.CNAState.TabB.Combo3 ??
											   SlideContainer.StarInfo.ClientGoalsLists.Goals
												   .Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(2);
			comboBoxEditTabBCombo4.EditValue = SlideContainer.EditedContent.CNAState.TabB.Combo4 ??
											   SlideContainer.StarInfo.ClientGoalsLists.Goals
												   .Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(3);
			comboBoxEditTabBCombo5.EditValue = SlideContainer.EditedContent.CNAState.TabB.Combo5 ??
											   SlideContainer.StarInfo.ClientGoalsLists.Goals
												   .Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(4);

			_allowToSave = true;
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;

			SlideContainer.EditedContent.CNAState.TabB.Clipart1 = clipartEditContainerTabB1.GetActiveClipartObject();
			SlideContainer.EditedContent.CNAState.TabB.Clipart2 = clipartEditContainerTabB2.GetActiveClipartObject();

			SlideContainer.EditedContent.CNAState.TabB.Combo1 = SlideContainer.StarInfo.ClientGoalsLists.Goals.Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(0) != comboBoxEditTabBCombo1.EditValue ?
				comboBoxEditTabBCombo1.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditTabBCombo1.EditValue as String } :
				null;
			SlideContainer.EditedContent.CNAState.TabB.Combo2 = SlideContainer.StarInfo.ClientGoalsLists.Goals.Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(1) != comboBoxEditTabBCombo2.EditValue ?
				comboBoxEditTabBCombo2.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditTabBCombo2.EditValue as String } :
				null;
			SlideContainer.EditedContent.CNAState.TabB.Combo3 = SlideContainer.StarInfo.ClientGoalsLists.Goals.Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(2) != comboBoxEditTabBCombo3.EditValue ?
				comboBoxEditTabBCombo3.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditTabBCombo3.EditValue as String } :
				null;
			SlideContainer.EditedContent.CNAState.TabB.Combo4 = SlideContainer.StarInfo.ClientGoalsLists.Goals.Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(3) != comboBoxEditTabBCombo4.EditValue ?
				comboBoxEditTabBCombo4.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditTabBCombo4.EditValue as String } :
				null;
			SlideContainer.EditedContent.CNAState.TabB.Combo5 = SlideContainer.StarInfo.ClientGoalsLists.Goals.Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(4) != comboBoxEditTabBCombo5.EditValue ?
				comboBoxEditTabBCombo5.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditTabBCombo5.EditValue as String } :
				null;
		}


		public override ListDataItem GetSlideHeaderValue()
		{
			return SlideContainer.EditedContent.CNAState.TabB.SlideHeader ??
				   TabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault);
		}

		public override void ApplySlideHeaderValue(ListDataItem slideHeaderValue)
		{
			SlideContainer.EditedContent.CNAState.TabB.SlideHeader =
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

			var clipart1 = SlideContainer.EditedContent.CNAState.TabB.Clipart1 ?? ImageClipartObject.FromImage(CustomTabInfo.Clipart1Image);
			if (clipart1 != null)
				outputDataPackage.ClipartItems.Add("CP02BCLIPART1", clipart1);

			var clipart2 = SlideContainer.EditedContent.CNAState.TabB.Clipart2 ?? ImageClipartObject.FromImage(CustomTabInfo.Clipart2Image);
			if (clipart2 != null)
				outputDataPackage.ClipartItems.Add("CP02BCLIPART2", clipart2);

			var slideHeader = (SlideContainer.EditedContent.CNAState.TabB.SlideHeader ?? TabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault))?.Value;
			outputDataPackage.TextItems.Add("CP02BHEADER".ToUpper(), slideHeader);
			outputDataPackage.TextItems.Add("HEADER".ToUpper(), slideHeader);

			var comboItemKeys = new[]
			{
					"CP02BCombo1",
					"CP02BCombo2",
					"CP02BCombo3",
					"CP02BCombo4",
					"CP02BCombo5"
				};

			var comboItemValues = new List<string>();
			var combo1 = (SlideContainer.EditedContent.CNAState.TabB.Combo1 ?? SlideContainer.StarInfo.ClientGoalsLists.Goals.Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(0))?.Value;
			if (!String.IsNullOrWhiteSpace(combo1))
				comboItemValues.Add(combo1);

			var combo2 = (SlideContainer.EditedContent.CNAState.TabB.Combo2 ?? SlideContainer.StarInfo.ClientGoalsLists.Goals.Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(1))?.Value;
			if (!String.IsNullOrWhiteSpace(combo2))
				comboItemValues.Add(combo2);

			var combo3 = (SlideContainer.EditedContent.CNAState.TabB.Combo3 ?? SlideContainer.StarInfo.ClientGoalsLists.Goals.Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(2))?.Value;
			if (!String.IsNullOrWhiteSpace(combo3))
				comboItemValues.Add(combo3);

			var combo4 = (SlideContainer.EditedContent.CNAState.TabB.Combo4 ?? SlideContainer.StarInfo.ClientGoalsLists.Goals.Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(3))?.Value;
			if (!String.IsNullOrWhiteSpace(combo4))
				comboItemValues.Add(combo4);

			var combo5 = (SlideContainer.EditedContent.CNAState.TabB.Combo5 ?? SlideContainer.StarInfo.ClientGoalsLists.Goals.Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(4))?.Value;
			if (!String.IsNullOrWhiteSpace(combo5))
				comboItemValues.Add(combo5);

			switch (comboItemValues.Count)
			{
				case 1:
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarCNAFile(clipart2 != null ? "CP02B-1.pptx" : "CP02B-6.pptx");
					break;
				case 2:
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarCNAFile(clipart2 != null ? "CP02B-2.pptx" : "CP02B-7.pptx");
					break;
				case 3:
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarCNAFile(clipart2 != null ? "CP02B-3.pptx" : "CP02B-8.pptx");
					break;
				case 4:
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarCNAFile(clipart2 != null ? "CP02B-4.pptx" : "CP02B-9.pptx");
					break;
				case 5:
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarCNAFile(clipart2 != null ? "CP02B-5.pptx" : "CP02B-10.pptx");
					break;
				default:
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarCNAFile("CP02A-1.pptx");
					break;
			}

			for (int i = 0; i < comboItemKeys.Length; i++)
				outputDataPackage.TextItems.Add(comboItemKeys[i].ToUpper(), comboItemValues.ElementAtOrDefault(i) ?? String.Empty);

			return outputDataPackage;
		}
		#endregion
	}
}